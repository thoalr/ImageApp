namespace AppUI
{
    partial class ImageViewerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
            SuspendLayout();
            // 
            // skglControl1
            // 
            skglControl1.BackColor = Color.Black;
            skglControl1.Dock = DockStyle.Fill;
            skglControl1.Location = new Point(0, 0);
            skglControl1.Margin = new Padding(4, 3, 4, 3);
            skglControl1.Name = "skglControl1";
            skglControl1.Size = new Size(757, 433);
            skglControl1.TabIndex = 0;
            skglControl1.VSync = true;
            skglControl1.PaintSurface += skglControl1_PaintSurface;
            skglControl1.DoubleClick += skglControl1_DoubleClick;
            skglControl1.KeyDown += skglControl1_KeyDown;
            skglControl1.MouseDown += skglControl1_MouseDown;
            skglControl1.MouseMove += skglControl1_MouseMove;
            skglControl1.MouseUp += skglControl1_MouseUp;
            skglControl1.Resize += skglControl1_Resize;
            // 
            // ImageViewerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(skglControl1);
            Name = "ImageViewerControl";
            Size = new Size(757, 433);
            DoubleClick += ImageViewerControl_DoubleClick;
            KeyDown += ImageViewerControl_KeyDown;
            MouseDown += ImageViewerControl_MouseDown;
            MouseMove += ImageViewerControl_MouseMove;
            MouseUp += ImageViewerControl_MouseUp;
            Resize += ImageViewerControl_Resize;
            ResumeLayout(false);
        }

        #endregion

        private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
    }
}
