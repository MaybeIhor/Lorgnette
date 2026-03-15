namespace Image_View
{
    partial class form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form));
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.themeButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.printButton = new System.Windows.Forms.ToolStripButton();
            this.restoreButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.editButton = new System.Windows.Forms.ToolStripButton();
            this.editBox = new System.Windows.Forms.ToolStrip();
            this.rotate90Button = new System.Windows.Forms.ToolStripButton();
            this.rotate270Button = new System.Windows.Forms.ToolStripButton();
            this.mirrorButton = new System.Windows.Forms.ToolStripButton();
            this.gridButton = new System.Windows.Forms.ToolStripButton();
            this.resizeButton = new System.Windows.Forms.ToolStripButton();
            this.redirectButton = new System.Windows.Forms.ToolStripButton();
            this.pictureBox = new Image_View.DontBlurBox();
            this.toolStrip.SuspendLayout();
            this.editBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // openFile
            // 
            this.openFile.AutoUpgradeEnabled = false;
            // 
            // openButton
            // 
            this.openButton.AutoSize = false;
            this.openButton.AutoToolTip = false;
            this.openButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openButton.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.openButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(50, 29);
            this.openButton.Text = "☰";
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // themeButton
            // 
            this.themeButton.AutoSize = false;
            this.themeButton.AutoToolTip = false;
            this.themeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.themeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.themeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.themeButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.themeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.themeButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.themeButton.Name = "themeButton";
            this.themeButton.Size = new System.Drawing.Size(50, 29);
            this.themeButton.Text = "◇";
            this.themeButton.Click += new System.EventHandler(this.ThemeButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = false;
            this.saveButton.AutoToolTip = false;
            this.saveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(91, 29);
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // printButton
            // 
            this.printButton.AutoSize = false;
            this.printButton.AutoToolTip = false;
            this.printButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.printButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.printButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(91, 29);
            this.printButton.Text = "Print";
            this.printButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // restoreButton
            // 
            this.restoreButton.AutoSize = false;
            this.restoreButton.AutoToolTip = false;
            this.restoreButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.restoreButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.restoreButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.restoreButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restoreButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(91, 29);
            this.restoreButton.Text = "Return";
            this.restoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip.CanOverflow = false;
            this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.themeButton,
            this.saveButton,
            this.printButton,
            this.editButton,
            this.restoreButton});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip.ShowItemToolTips = false;
            this.toolStrip.Size = new System.Drawing.Size(466, 31);
            this.toolStrip.TabIndex = 2;
            // 
            // editButton
            // 
            this.editButton.AutoSize = false;
            this.editButton.AutoToolTip = false;
            this.editButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(91, 29);
            this.editButton.Text = "Edit";
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // editBox
            // 
            this.editBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.editBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.editBox.AutoSize = false;
            this.editBox.BackColor = System.Drawing.SystemColors.Window;
            this.editBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.editBox.CanOverflow = false;
            this.editBox.Dock = System.Windows.Forms.DockStyle.None;
            this.editBox.GripMargin = new System.Windows.Forms.Padding(0);
            this.editBox.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.editBox.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.editBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotate90Button,
            this.rotate270Button,
            this.mirrorButton,
            this.gridButton,
            this.resizeButton,
            this.redirectButton});
            this.editBox.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.editBox.Location = new System.Drawing.Point(82, 50);
            this.editBox.Name = "editBox";
            this.editBox.Padding = new System.Windows.Forms.Padding(0);
            this.editBox.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.editBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.editBox.ShowItemToolTips = false;
            this.editBox.Size = new System.Drawing.Size(302, 31);
            this.editBox.TabIndex = 3;
            this.editBox.Visible = false;
            // 
            // rotate90Button
            // 
            this.rotate90Button.AutoSize = false;
            this.rotate90Button.AutoToolTip = false;
            this.rotate90Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rotate90Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.rotate90Button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotate90Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rotate90Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotate90Button.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.rotate90Button.Name = "rotate90Button";
            this.rotate90Button.Size = new System.Drawing.Size(50, 29);
            this.rotate90Button.Text = "↻";
            this.rotate90Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rotate90Button.Click += new System.EventHandler(this.Rotate90Button_Click);
            // 
            // rotate270Button
            // 
            this.rotate270Button.AutoSize = false;
            this.rotate270Button.AutoToolTip = false;
            this.rotate270Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rotate270Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.rotate270Button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotate270Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.rotate270Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotate270Button.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.rotate270Button.Name = "rotate270Button";
            this.rotate270Button.Size = new System.Drawing.Size(50, 29);
            this.rotate270Button.Text = "↺";
            this.rotate270Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rotate270Button.Click += new System.EventHandler(this.Rotate270Button_Click);
            // 
            // mirrorButton
            // 
            this.mirrorButton.AutoSize = false;
            this.mirrorButton.AutoToolTip = false;
            this.mirrorButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mirrorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mirrorButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mirrorButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mirrorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mirrorButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.mirrorButton.Name = "mirrorButton";
            this.mirrorButton.Size = new System.Drawing.Size(50, 29);
            this.mirrorButton.Text = "◭";
            this.mirrorButton.Click += new System.EventHandler(this.MirrorButton_Click);
            // 
            // gridButton
            // 
            this.gridButton.AutoSize = false;
            this.gridButton.AutoToolTip = false;
            this.gridButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gridButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.gridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gridButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.gridButton.Name = "gridButton";
            this.gridButton.Size = new System.Drawing.Size(50, 29);
            this.gridButton.Text = "✛";
            this.gridButton.Click += new System.EventHandler(this.GridButton_Click);
            // 
            // resizeButton
            // 
            this.resizeButton.AutoSize = false;
            this.resizeButton.AutoToolTip = false;
            this.resizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.resizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resizeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resizeButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resizeButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(50, 29);
            this.resizeButton.Text = "◰";
            this.resizeButton.Click += new System.EventHandler(this.ResizeButton_Click);
            // 
            // redirectButton
            // 
            this.redirectButton.AutoSize = false;
            this.redirectButton.AutoToolTip = false;
            this.redirectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.redirectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.redirectButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redirectButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.redirectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redirectButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.redirectButton.Name = "redirectButton";
            this.redirectButton.Size = new System.Drawing.Size(50, 29);
            this.redirectButton.Text = "▷";
            this.redirectButton.Click += new System.EventHandler(this.RedirectButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.ErrorImage = null;
            this.pictureBox.Image = null;
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(0, 31);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(466, 367);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            this.pictureBox.Text = "pictureBox";
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.pictureBox.MouseEnter += new System.EventHandler(this.PictureBox_MouseEnter);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            // 
            // form
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(466, 398);
            this.Controls.Add(this.editBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.toolStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(318, 275);
            this.Name = "form";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Lorgnette";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_KeyUp);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.editBox.ResumeLayout(false);
            this.editBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton themeButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton printButton;
        private System.Windows.Forms.ToolStripButton restoreButton;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton editButton;
        private System.Windows.Forms.ToolStrip editBox;
        private System.Windows.Forms.ToolStripButton rotate270Button;
        private System.Windows.Forms.ToolStripButton mirrorButton;
        private System.Windows.Forms.ToolStripButton redirectButton;
        private System.Windows.Forms.ToolStripButton rotate90Button;
        private DontBlurBox pictureBox;
        private System.Windows.Forms.ToolStripButton gridButton;
        private System.Windows.Forms.ToolStripButton resizeButton;
    }
}

