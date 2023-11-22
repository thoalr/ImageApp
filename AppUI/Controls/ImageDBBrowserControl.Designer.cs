namespace AppUI
{
    partial class ImageDBBrowseControl
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
            textBox1 = new TextBox();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            imageViewerControl1 = new ImageViewerControl();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(84, 23);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(791, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 23);
            label1.Margin = new Padding(3, 3, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(75, 26);
            label1.TabIndex = 2;
            label1.Text = "Search query";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(imageViewerControl1, 0, 2);
            tableLayoutPanel1.Controls.Add(textBox1, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(878, 543);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // imageViewerControl1
            // 
            tableLayoutPanel1.SetColumnSpan(imageViewerControl1, 2);
            imageViewerControl1.Dock = DockStyle.Fill;
            imageViewerControl1.Location = new Point(3, 52);
            imageViewerControl1.Name = "imageViewerControl1";
            imageViewerControl1.Size = new Size(872, 488);
            imageViewerControl1.TabIndex = 0;
            imageViewerControl1.MouseDown += imageViewerControl1_MouseDown;
            imageViewerControl1.MouseMove += imageViewerControl1_MouseMove;
            imageViewerControl1.MouseUp += imageViewerControl1_MouseUp;
            // 
            // ImageDBBrowseControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ImageDBBrowseControl";
            Size = new Size(878, 543);
            Load += ImageFileBrowseControl_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private ImageViewerControl imageViewerControl1;
    }
}
