using Microsoft.Win32;
using Monocle;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Image_View
{
    public partial class form : Form
    {
        private string currentFileName;
        private bool framed = false;
        private bool dark = false;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] val, int size);

        public form(string filePath = null)
        {
            InitializeComponent();
            ApplySystemTheme();
            toolStrip.Renderer = new FixedRenderer();
            editBox.Renderer = new FixedRenderer();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            if (!string.IsNullOrEmpty(filePath))
                LoadImage(filePath);
        }

        private void ApplySystemTheme()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
            {
                if (key?.GetValue("AppsUseLightTheme") is int theme && theme == 0)
                {
                    DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
                    BackColor = editBox.BackColor = Color.FromArgb(255, 25, 25, 25);
                    toolStrip.ForeColor = editBox.ForeColor = SystemColors.Window;
                    dark = true;
                }
            }
        }

        private void LoadImage(string path)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 8192))
                using (var tempImage = Image.FromStream(fs, false, false))
                {
                    pictureBox.Image = CloneImage(tempImage);
                    currentFileName = path;
                    UpdateTitle();
                    pictureBox.InvalidateBoth();
                }
            }
            catch { }
        }

        private Image CloneImage(Image source)
        {
            var format = (source.PixelFormat == PixelFormat.Format32bppArgb ||
                          source.PixelFormat == PixelFormat.Format32bppPArgb ||
                          source.RawFormat.Equals(ImageFormat.Png))
                ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb;
            var clone = new Bitmap(source.Width, source.Height, format);
            using (var g = Graphics.FromImage(clone))
            {
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                g.DrawImage(source, 0, 0, source.Width, source.Height);
            }
            return clone;
        }

        private void UpdateTitle()
        {
            var crop = pictureBox.GetCrop();
            var img = pictureBox.Image;
            int width = crop?.Width ?? img?.Width ?? 0;
            int height = crop?.Height ?? img?.Height ?? 0;
            Text = img != null ? $"{width} x {height}   {Path.GetFileName(currentFileName)}" : "Monocle";
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
        }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
                LoadImage(files[0]);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "All image files (*.png;*.jpg;*.ico;*.jpeg;*.bmp;*.tiff;*.jpe;*.jfif;*.exif;*.gif)|*.png;*.jpg;*.ico;*.jpeg;*.bmp;*.tiff;*.jpe;*.jfif;*.exif;*.gif|PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg;*.jpe;*.jfif;*.exif)|*.jpg;*.jpeg;*.jpe;*.jfif;*.exif|TIFF (*.tiff)|*.tiff|BMP (*.bmp)|*.bmp|ICO (*.ico)|*.ico",
                FilterIndex = 1,
                RestoreDirectory = true
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    LoadImage(dialog.FileName);
            }
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e) => pictureBox.Focus();

        private void ThemeButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;
            pictureBox.isFramed = !pictureBox.isFramed;
            pictureBox.Invalidate();
            themeButton.Text = framed ? "◇" : "◈";
            framed = !framed;
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;
            pictureBox.Undo();
            UpdateTitle();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            editBox.Visible = false;
            if (pictureBox.Image != null && e.Button == MouseButtons.Right)
            {
                toolStrip.Visible = !toolStrip.Visible;
                pictureBox.Invalidate();
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            foreach (var codec in ImageCodecInfo.GetImageDecoders())
                if (codec.FormatID == format.Guid)
                    return codec;
            return null;
        }

        private int PickExtension(string ext)
        {
            ext = ext.ToLower();
            if (ext == ".jpg" || ext == ".jpeg") return 2;
            if (ext == ".bmp") return 3;
            return 1;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            string dir = !string.IsNullOrEmpty(currentFileName) && currentFileName != "Untitled.png"
                ? Path.GetDirectoryName(currentFileName)
                : Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            using (var dialog = new SaveFileDialog
            {
                InitialDirectory = dir,
                Filter = "PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|BMP (*.bmp)|*.bmp|ICO (*.ico)|*.ico",
                FilterIndex = PickExtension(Path.GetExtension(currentFileName)),
                FileName = Path.GetFileName(currentFileName),
                RestoreDirectory = true
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var imageToSave = pictureBox.GetVisible())
                    {
                        string ext = Path.GetExtension(dialog.FileName).ToLower();

                        if (ext == ".jpg" || ext == ".jpeg")
                        {
                            using (var flat = new Bitmap(imageToSave.Width, imageToSave.Height, PixelFormat.Format24bppRgb))
                            using (var g = Graphics.FromImage(flat))
                            using (var encoderParams = new EncoderParameters(1))
                            {
                                g.Clear(Color.White);
                                g.DrawImage(imageToSave, 0, 0);
                                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 95L);
                                flat.Save(dialog.FileName, GetEncoder(ImageFormat.Jpeg), encoderParams);
                            }
                        }
                        else if (ext == ".ico")
                            imageToSave.Save(dialog.FileName, ImageFormat.Icon);
                        else if (ext == ".bmp")
                            imageToSave.Save(dialog.FileName, ImageFormat.Bmp);
                        else
                            imageToSave.Save(dialog.FileName);
                        currentFileName = dialog.FileName;
                        UpdateTitle();
                    }
                }
                catch { }
            }
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z) RestoreButton_Click(sender, e);
            else if (e.Control && e.KeyCode == Keys.V) PasteFromClipboard();
            else if (e.Control && e.KeyCode == Keys.G) GridButton_Click(sender, e);
        }

        private void PasteFromClipboard()
        {
            if (!Clipboard.ContainsImage()) return;
            using (var clipboardImage = Clipboard.GetImage())
            {
                pictureBox.Image = CloneImage(clipboardImage);
                if (string.IsNullOrEmpty(currentFileName))
                    currentFileName = "Untitled.png";
                UpdateTitle();
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            try
            {
                var imageToPrint = pictureBox.GetVisible();
                var pd = new PrintDocument();

                pd.PrintPage += (o, args) =>
                {
                    var printArea = args.MarginBounds;
                    float scale = Math.Min((float)printArea.Width / imageToPrint.Width,
                                          (float)printArea.Height / imageToPrint.Height);
                    int scaledWidth = (int)(imageToPrint.Width * scale);
                    int scaledHeight = (int)(imageToPrint.Height * scale);
                    int x = printArea.Left + (printArea.Width - scaledWidth) / 2;
                    args.Graphics.DrawImage(imageToPrint, x, printArea.Top, scaledWidth, scaledHeight);
                };

                pd.EndPrint += (o, args) => imageToPrint?.Dispose();

                using (var printDialog = new PrintDialog { Document = pd })
                {
                    if (printDialog.ShowDialog() == DialogResult.OK)
                        pd.Print();
                    else
                        imageToPrint?.Dispose();
                }
            }
            catch { }
        }

        private void EditButton_Click(object sender, EventArgs e) => editBox.Visible = !editBox.Visible;

        private void Rotate90Button_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;
            pictureBox.Rotate90();
            UpdateTitle();
        }

        private void Rotate270Button_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;
            pictureBox.Rotate270();
            UpdateTitle();
        }

        private void MirrorButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;
            pictureBox.Mirror();
            UpdateTitle();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (pictureBox.Image != null) UpdateTitle();
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;
            pictureBox.isGridding = !pictureBox.isGridding;
            pictureBox.Invalidate();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z) RestoreButton_Click(sender, e);
            else if (e.Control && e.KeyCode == Keys.V) PasteFromClipboard();
            else if (e.Control && e.KeyCode == Keys.G) GridButton_Click(sender, e);
        }

        private void RedirectButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(currentFileName));
                using (var imageToEdit = pictureBox.GetVisible())
                    imageToEdit.Save(tempPath, ImageFormat.Png);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "rundll32.exe",
                    Arguments = $"shell32.dll,OpenAs_RunDLL {tempPath}",
                    UseShellExecute = false
                });
            }
            catch { }
        }

        private void ResizeButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            var crop = pictureBox.GetCrop();
            int currentWidth = crop?.Width ?? pictureBox.Image.Width;
            int currentHeight = crop?.Height ?? pictureBox.Image.Height;

            using (var dialog = new ResizeForm(currentWidth, currentHeight, dark))
            {
                if (dialog.ShowDialog() != DialogResult.OK || (dialog.NewWidth == currentWidth && dialog.NewHeight == currentHeight)) return;

                try
                {
                    pictureBox.Resize(dialog.NewWidth, dialog.NewHeight, dialog.Mode);
                    UpdateTitle();
                }
                catch { }
            }
        }
    }
}