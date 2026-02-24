using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Image_View;

namespace Monocle
{
    public partial class ResizeForm : Form
    {
        private bool isSmooth = true;
        public int NewWidth { get; private set; }
        public int NewHeight { get; private set; }
        public InterpolationMode Mode => isSmooth ? InterpolationMode.HighQualityBicubic : InterpolationMode.NearestNeighbor;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] val, int size);

        private static readonly int[] darkAttr = new[] { 1 };

        public ResizeForm(int currentWidth, int currentHeight, bool dark)
        {
            InitializeComponent();
            toolStrip.Renderer = new FixedRenderer();
            widthBox.Text = currentWidth.ToString();
            heightBox.Text = currentHeight.ToString();

            if (dark)
                GetDark();
        }

        public void GetDark()
        {
            DwmSetWindowAttribute(Handle, 20, darkAttr, 4);
            var bg = Color.FromArgb(25, 25, 25);
            toolStrip.BackColor = widthBox.BackColor = heightBox.BackColor = bg;
            toolStrip.ForeColor = widthBox.ForeColor = heightBox.ForeColor = SystemColors.Window;
        }

        private void SmoothButton_Click(object sender, EventArgs e)
        {
            isSmooth = !isSmooth;
            smoothButton.Text = isSmooth ? " ▞  Bicubic" : " ▞  Nearest Neighbor";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(widthBox.Text, out int w) && w > 0 && w < 100000 &&
                int.TryParse(heightBox.Text, out int h) && h > 0 && h < 100000)
            {
                NewWidth = w;
                NewHeight = h;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}