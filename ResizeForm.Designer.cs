namespace Lorgnette
{
    partial class ResizeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.widthBox = new System.Windows.Forms.ToolStripTextBox();
            this.xLabel = new System.Windows.Forms.ToolStripLabel();
            this.heightBox = new System.Windows.Forms.ToolStripTextBox();
            this.smoothButton = new System.Windows.Forms.ToolStripButton();
            this.okButton = new System.Windows.Forms.ToolStripButton();
            this.cancelButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip.CanOverflow = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.widthBox,
            this.xLabel,
            this.heightBox,
            this.smoothButton,
            this.okButton,
            this.cancelButton});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip.ShowItemToolTips = false;
            this.toolStrip.Size = new System.Drawing.Size(164, 93);
            this.toolStrip.TabIndex = 3;
            // 
            // widthBox
            // 
            this.widthBox.AutoSize = false;
            this.widthBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthBox.Margin = new System.Windows.Forms.Padding(5, 6, 0, 0);
            this.widthBox.Name = "widthBox";
            this.widthBox.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.widthBox.Size = new System.Drawing.Size(64, 21);
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = false;
            this.xLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.xLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.xLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xLabel.Margin = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.xLabel.Name = "xLabel";
            this.xLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.xLabel.Size = new System.Drawing.Size(23, 23);
            this.xLabel.Text = "×";
            // 
            // heightBox
            // 
            this.heightBox.AutoSize = false;
            this.heightBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.heightBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightBox.Margin = new System.Windows.Forms.Padding(0, 6, 5, 0);
            this.heightBox.Name = "heightBox";
            this.heightBox.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.heightBox.Size = new System.Drawing.Size(65, 21);
            // 
            // smoothButton
            // 
            this.smoothButton.AutoSize = false;
            this.smoothButton.AutoToolTip = false;
            this.smoothButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.smoothButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.smoothButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smoothButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.smoothButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.smoothButton.Margin = new System.Windows.Forms.Padding(1, 5, 0, 1);
            this.smoothButton.Name = "smoothButton";
            this.smoothButton.Size = new System.Drawing.Size(162, 29);
            this.smoothButton.Text = " ▞  Bicubic";
            this.smoothButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.smoothButton.Click += new System.EventHandler(this.SmoothButton_Click);
            // 
            // okButton
            // 
            this.okButton.AutoSize = false;
            this.okButton.AutoToolTip = false;
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.okButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.okButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.okButton.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(81, 29);
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSize = false;
            this.cancelButton.AutoToolTip = false;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cancelButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(81, 29);
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ResizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(164, 93);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResizeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resize";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripTextBox widthBox;
        private System.Windows.Forms.ToolStripButton smoothButton;
        private System.Windows.Forms.ToolStripButton okButton;
        private System.Windows.Forms.ToolStripButton cancelButton;
        private System.Windows.Forms.ToolStripTextBox heightBox;
        private System.Windows.Forms.ToolStripLabel xLabel;
    }
}