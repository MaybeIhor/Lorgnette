using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Lorgnette
{
    public partial class DontBlurBox : PictureBox
    {
        private struct State
        {
            public Bitmap Image;
            public Rectangle? Crop;
        }

        private bool isDown;
        private Point p1, p2;
        private Rectangle imageRect;
        private double scale;

        private readonly Stack<State> history = new();
        private Rectangle? Crop => history.Count > 0 ? history.Peek().Crop : (Rectangle?)null;

        private Bitmap cachedImage;
        private (Size controlSize, Rectangle? crop) cacheKey;

        private readonly Timer resizeTimer;
        private bool isResizing;

        public int gridMode;
        public bool isFramed;

        private static readonly SolidBrush overlayBrush = new(Color.FromArgb(155, 0, 0, 0));
        private static readonly Pen crossPen = new(Color.FromArgb(155, 155, 155, 155), 1);

        public DontBlurBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            resizeTimer = new Timer { Interval = 300 };
            resizeTimer.Tick += (s, e) => { resizeTimer.Stop(); isResizing = false; InvalidateBoth(); };

            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
        }

        public new Image Image
        {
            get => history.Count > 0 ? history.Peek().Image : null;
            set
            {
                ClearHistory();

                if (value != null)
                    history.Push(new State { Image = value as Bitmap ?? new Bitmap(value), Crop = null });

                InvalidateBoth();
            }
        }

        private void ClearHistory()
        {
            foreach (var s in history)
                s.Image?.Dispose();
            history.Clear();
        }

        private void PushState(Bitmap img, Rectangle? newCrop)
        {
            history.Push(new State { Image = img, Crop = newCrop });
            InvalidateBoth();
        }

        public void InvalidateCache()
        {
            cachedImage?.Dispose();
            cachedImage = null;
            cacheKey = default;
        }

        public void InvalidateBoth()
        {
            InvalidateCache();
            Invalidate();
        }

        public void Undo()
        {
            if (history.Count <= 1) return;

            var top = history.Pop();

            if (top.Image != history.Peek().Image)
                top.Image?.Dispose();

            InvalidateBoth();
        }

        public Rectangle? GetCrop() => Crop;

        private static PixelFormat GetPixelFormat(Image image) => (Image.IsAlphaPixelFormat(image.PixelFormat) || (image.Flags & 16384) != 0) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb;

        private void ApplyFlip(RotateFlipType flip, Func<Rectangle, Size, Rectangle> transformCrop)
        {
            if (history.Count == 0) return;

            var src = history.Peek().Image;
            var bmp = src.Clone() as Bitmap;

            bmp.RotateFlip(flip);
            PushState(bmp, Crop.HasValue ? transformCrop(Crop.Value, src.Size) : (Rectangle?)null);
        }

        public void Rotate90() => ApplyFlip(RotateFlipType.Rotate90FlipNone, (c, size) => new Rectangle(size.Height - c.Y - c.Height, c.X, c.Height, c.Width));

        public void Rotate270() => ApplyFlip(RotateFlipType.Rotate270FlipNone, (c, size) => new Rectangle(c.Y, size.Width - c.X - c.Width, c.Height, c.Width));

        public void Mirror() => ApplyFlip(RotateFlipType.RotateNoneFlipX, (c, size) => new Rectangle(size.Width - c.X - c.Width, c.Y, c.Width, c.Height));

        public new void Resize(int newWidth, int newHeight, InterpolationMode mode)
        {
            if (history.Count == 0 || newWidth <= 0 || newHeight <= 0) return;

            var cur = history.Peek();
            var sourceRect = cur.Crop ?? new Rectangle(0, 0, cur.Image.Width, cur.Image.Height);

            if (sourceRect.Width <= 0 || sourceRect.Height <= 0) return;

            InvalidateCache();

            var bmp = new Bitmap(newWidth, newHeight, GetPixelFormat(cur.Image));

            using var g = Graphics.FromImage(bmp);
            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = mode;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(cur.Image, new Rectangle(0, 0, newWidth, newHeight), sourceRect, GraphicsUnit.Pixel);

            PushState(bmp, null);
        }

        public Image GetVisible()
        {
            if (history.Count == 0) return null;

            var cur = history.Peek();
            var sourceRect = cur.Crop ?? new Rectangle(0, 0, cur.Image.Width, cur.Image.Height);

            var result = new Bitmap(sourceRect.Width, sourceRect.Height, cur.Image.PixelFormat);

            using var g = Graphics.FromImage(result);
            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(cur.Image, new Rectangle(0, 0, sourceRect.Width, sourceRect.Height), sourceRect, GraphicsUnit.Pixel);

            return result;
        }

        private void CalculateImageBounds()
        {
            if (history.Count == 0) return;

            var cur = history.Peek();

            int imgW = cur.Crop?.Width ?? cur.Image.Width;
            int imgH = cur.Crop?.Height ?? cur.Image.Height;

            double imgAspect = (double)imgW / imgH;
            double ctrlAspect = (double)Width / Height;

            int w, h;

            if (imgAspect > ctrlAspect)
            {
                w = Width;
                h = (int)(Width / imgAspect);
            }
            else
            {
                h = Height;
                w = (int)(Height * imgAspect);
            }

            imageRect = new Rectangle((Width - w) / 2, (Height - h) / 2, w, h);
            scale = Math.Min((double)w / imgW, (double)h / imgH);
        }

        private Point ClampToImage(Point pt) => new(Math.Max(imageRect.Left, Math.Min(imageRect.Right, pt.X)), Math.Max(imageRect.Top, Math.Min(imageRect.Bottom, pt.Y)));

        protected override void OnResize(EventArgs e)
        {
            isResizing = true;
            resizeTimer.Stop();
            resizeTimer.Start();
            Invalidate();
        }

        private void DrawGrid(Graphics g)
        {
            if (gridMode == 0 || history.Count == 0) return;

            int divisions = gridMode == 1 ? 4 : 3;

            for (int i = 1; i < divisions; i++)
            {
                int x = imageRect.Left + imageRect.Width * i / divisions;
                int y = imageRect.Top + imageRect.Height * i / divisions;

                g.DrawLine(crossPen, x, imageRect.Top, x, imageRect.Bottom);
                g.DrawLine(crossPen, imageRect.Left, y, imageRect.Right, y);
            }
        }

        private void DrawFrame(Graphics g)
        {
            if (!isFramed || history.Count == 0) return;

            g.DrawRectangle(crossPen, imageRect.X - 1, imageRect.Y - 1, imageRect.Width + 1, imageRect.Height + 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (history.Count == 0) return;

            var cur = history.Peek();
            CalculateImageBounds();

            if (cachedImage == null || cacheKey != (Size, cur.Crop))
            {
                InvalidateCache();

                cachedImage = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                cacheKey = (Size, cur.Crop);

                using var g = Graphics.FromImage(cachedImage);
                g.Clear(BackColor);
                g.PixelOffsetMode = PixelOffsetMode.Half;

                int visibleWidth = cur.Crop?.Width ?? cur.Image.Width;
                int visibleHeight = cur.Crop?.Height ?? cur.Image.Height;
                bool fast = isResizing || visibleWidth < 512 || visibleHeight < 512;

                g.InterpolationMode = fast ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = fast ? CompositingQuality.HighSpeed : CompositingQuality.HighQuality;

                if (cur.Crop.HasValue)
                    g.DrawImage(cur.Image, imageRect, cur.Crop.Value, GraphicsUnit.Pixel);
                else
                    g.DrawImage(cur.Image, imageRect);
            }

            e.Graphics.DrawImageUnscaled(cachedImage, 0, 0);
            DrawFrame(e.Graphics);
            DrawGrid(e.Graphics);

            if (isDown && !p2.IsEmpty)
            {
                var rect = new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));

                using var region = new Region(imageRect);
                region.Exclude(rect);

                e.Graphics.FillRegion(overlayBrush, region);
                Cursor.Current = Cursors.Hand;
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (history.Count == 0 || e.Button != MouseButtons.Left)
            {
                isDown = false;
                return;
            }

            var cur = history.Peek();
            int minDim = Math.Min(cur.Crop?.Width ?? cur.Image.Width, cur.Crop?.Height ?? cur.Image.Height);

            if (minDim <= 6)
            {
                isDown = false;
                return;
            }

            p1 = SnapToPixel(ClampToImage(e.Location));
            p2 = Point.Empty;
            isDown = true;
        }

        private Point SnapToPixel(Point screenPoint)
        {
            int px = (int)Math.Round((screenPoint.X - imageRect.X) / scale);
            int py = (int)Math.Round((screenPoint.Y - imageRect.Y) / scale);

            return new Point(imageRect.X + (int)Math.Round(px * scale), imageRect.Y + (int)Math.Round(py * scale));
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isDown && history.Count > 0)
            {
                p2 = ClampToImage(e.Location);
                Invalidate();
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (history.Count == 0 || e.Button != MouseButtons.Left)
            {
                isDown = false;
                return;
            }

            Cursor.Current = Cursors.Default;
            isDown = false;

            if (!p2.IsEmpty && Math.Abs(p1.X - p2.X) > 20 && Math.Abs(p1.Y - p2.Y) > 20)
            {
                p2 = SnapToPixel(p2);
                ApplyCropFromDrag();
            }

            Invalidate();
        }

        private void ApplyCropFromDrag()
        {
            var cur = history.Peek();

            int offsetX = cur.Crop?.X ?? 0;
            int offsetY = cur.Crop?.Y ?? 0;

            int x1 = (int)Math.Round((p1.X - imageRect.X) / scale) + offsetX;
            int y1 = (int)Math.Round((p1.Y - imageRect.Y) / scale) + offsetY;
            int x2 = (int)Math.Round((p2.X - imageRect.X) / scale) + offsetX;
            int y2 = (int)Math.Round((p2.Y - imageRect.Y) / scale) + offsetY;

            int minX = Math.Max(0, Math.Min(x1, x2));
            int minY = Math.Max(0, Math.Min(y1, y2));

            int w = Math.Min(cur.Image.Width, Math.Max(x1, x2)) - minX;
            int h = Math.Min(cur.Image.Height, Math.Max(y1, y2)) - minY;

            if (w >= 6 && h >= 6)
                PushState(cur.Image, new Rectangle(minX, minY, w, h));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                InvalidateCache();
                resizeTimer?.Dispose();
                ClearHistory();
            }

            base.Dispose(disposing);
        }
    }
}