using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lorgnette
{
    public partial class Form : System.Windows.Forms.Form
    {
        private string currentFileName;
        private bool dark;

        public Form(string filePath = null)
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
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

            if (key?.GetValue("AppsUseLightTheme") is int theme && theme == 0)
            {
                Dwm.DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
                BackColor = editBox.BackColor = Color.FromArgb(25, 25, 25);
                toolStrip.ForeColor = editBox.ForeColor = SystemColors.Window;
                dark = true;
            }
        }

        private void LoadImage(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            try
            {
                pictureBox.Image = null;

                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete, 8192, FileOptions.SequentialScan);
                using var tempImage = Image.FromStream(fs, false, false);
                var loaded = CloneImage(tempImage);

                if (loaded == null) return;

                pictureBox.Image = loaded;
                currentFileName = path;
                UpdateTitle();
            }
            catch { }
        }

        private static Image CloneImage(Image source)
        {
            if (source == null || source.Width <= 0 || source.Height <= 0)
                return null;

            var clone = new Bitmap(source.Width, source.Height, GetPixelFormat(source));

            using var g = Graphics.FromImage(clone);
            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.DrawImage(source, 0, 0, source.Width, source.Height);

            return clone;
        }

        private static PixelFormat GetPixelFormat(Image image) => (Image.IsAlphaPixelFormat(image.PixelFormat) || (image.Flags & 16384) != 0) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb;

        private void UpdateTitle()
        {
            var crop = pictureBox.GetCrop();
            var img = pictureBox.Image;

            Text = img != null ? $"{crop?.Width ?? img.Width} × {crop?.Height ?? img.Height}   {Path.GetFileName(currentFileName)}" : "Lorgnette";
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
            using var dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "All image files (*.png;*.jpg;*.ico;*.jpeg;*.bmp;*.tiff;*.jpe;*.jfif;*.exif;*.gif)|*.png;*.jpg;*.ico;*.jpeg;*.bmp;*.tiff;*.jpe;*.jfif;*.exif;*.gif|PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg;*.jpe;*.jfif;*.exif)|*.jpg;*.jpeg;*.jpe;*.jfif;*.exif|TIFF (*.tiff)|*.tiff|BMP (*.bmp)|*.bmp|ICO (*.ico)|*.ico",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
                LoadImage(dialog.FileName);
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e) => pictureBox.Focus();

        private void ThemeButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            pictureBox.isFramed = !pictureBox.isFramed;
            pictureBox.Invalidate();
            themeButton.Text = pictureBox.isFramed ? "◈" : "◇";
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

        private static ImageCodecInfo GetEncoder(ImageFormat format) =>
            ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == format.Guid);

        private static void SaveJpeg(Image image, string path)
        {
            using var flat = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            using var g = Graphics.FromImage(flat);
            using var encoderParams = new EncoderParameters(1);

            g.Clear(Color.White);
            g.DrawImage(image, 0, 0);

            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 95L);
            flat.Save(path, GetEncoder(ImageFormat.Jpeg), encoderParams);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            string dir = !string.IsNullOrEmpty(currentFileName) && currentFileName != "Untitled.png"
                ? Path.GetDirectoryName(currentFileName)
                : Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            using var dialog = new SaveFileDialog
            {
                InitialDirectory = dir,
                Filter = "PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|BMP (*.bmp)|*.bmp|ICO (*.ico)|*.ico",
                FilterIndex = Path.GetExtension(currentFileName).ToLower() switch { ".jpg" or ".jpeg" or ".jpe" or ".jfif" or ".exif" => 2, ".bmp" => 3, _ => 1 },
                FileName = Path.GetFileName(currentFileName),
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var imageToSave = pictureBox.GetVisible();
                string ext = Path.GetExtension(dialog.FileName).ToLower();

                if (ext is ".jpg" or ".jpeg")
                    SaveJpeg(imageToSave, dialog.FileName);
                else if (ext == ".ico")
                    imageToSave.Save(dialog.FileName, ImageFormat.Icon);
                else if (ext == ".bmp")
                    imageToSave.Save(dialog.FileName, ImageFormat.Bmp);
                else
                    imageToSave.Save(dialog.FileName);

                currentFileName = dialog.FileName;
                UpdateTitle();
            }
            catch { }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.V)) { PasteFromClipboard(); return true; }
            if (keyData == (Keys.Control | Keys.Z)) { RestoreButton_Click(this, EventArgs.Empty); return true; }
            if (keyData == (Keys.Control | Keys.G)) { GridButton_Click(this, EventArgs.Empty); return true; }
            if (keyData == (Keys.Control | Keys.C)) { Clipboard.SetImage(pictureBox.GetVisible()); return true; }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PasteFromClipboard()
        {
            if (Clipboard.ContainsFileDropList())
            {
                var files = Clipboard.GetFileDropList();

                if (files.Count > 0)
                    LoadImage(files[0]);

                return;
            }

            if (!Clipboard.ContainsImage()) return;

            using var clipboardImage = Clipboard.GetImage();

            pictureBox.Image = null;
            pictureBox.Image = CloneImage(clipboardImage);

            if (string.IsNullOrEmpty(currentFileName))
                currentFileName = "Untitled.png";

            UpdateTitle();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            Image imageToPrint = null;

            try
            {
                imageToPrint = pictureBox.GetVisible();

                var pd = new PrintDocument();
                pd.PrintPage += (o, args) =>
                {
                    var printArea = args.MarginBounds;
                    float scale = Math.Min((float)printArea.Width / imageToPrint.Width, (float)printArea.Height / imageToPrint.Height);

                    int scaledWidth = (int)(imageToPrint.Width * scale);
                    int scaledHeight = (int)(imageToPrint.Height * scale);
                    int x = printArea.Left + (printArea.Width - scaledWidth) / 2;

                    args.Graphics.DrawImage(imageToPrint, x, printArea.Top, scaledWidth, scaledHeight);
                };

                using var printDialog = new PrintDialog { Document = pd };

                if (printDialog.ShowDialog() == DialogResult.OK)
                    pd.Print();
            }
            catch { }
            finally
            {
                imageToPrint?.Dispose();
            }
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
            if (pictureBox.Image != null)
                UpdateTitle();
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            pictureBox.gridMode = (pictureBox.gridMode + 1) % 3;
            pictureBox.Invalidate();
        }

        private void RedirectButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            try
            {
                string name = string.IsNullOrEmpty(currentFileName) ? "Untitled.png" : Path.GetFileName(currentFileName);
                string ext = Path.GetExtension(name).ToLowerInvariant();

                if (ext is not (".png" or ".jpg" or ".jpeg" or ".jpe" or ".jfif" or ".exif" or ".bmp" or ".ico"))
                    ext = ".png";

                string tempPath = Path.Combine(Path.GetTempPath(), name);

                using var imageToRedirect = pictureBox.GetVisible();
                if (imageToRedirect == null) return;

                if (ext is ".jpg" or ".jpeg" or ".jpe" or ".jfif" or ".exif")
                    SaveJpeg(imageToRedirect, tempPath);
                else if (ext == ".bmp")
                    imageToRedirect.Save(tempPath, ImageFormat.Bmp);
                else if (ext == ".ico")
                    imageToRedirect.Save(tempPath, ImageFormat.Icon);
                else
                    imageToRedirect.Save(tempPath, ImageFormat.Png);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "rundll32.exe",
                    Arguments = $"shell32.dll,OpenAs_RunDLL {tempPath}",
                    UseShellExecute = false,
                    CreateNoWindow = true
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

            using var dialog = new ResizeForm(currentWidth, currentHeight, dark);

            if (dialog.ShowDialog() != DialogResult.OK || (dialog.NewWidth == currentWidth && dialog.NewHeight == currentHeight))
                return;

            try
            {
                pictureBox.Resize(dialog.NewWidth, dialog.NewHeight, dialog.Mode);
                UpdateTitle();
            }
            catch { }
        }
    }
}