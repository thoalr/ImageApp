namespace AppUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            tagsToolStripMenuItem = new ToolStripMenuItem();
            tagTypesToolStripMenuItem = new ToolStripMenuItem();
            groupToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            browseFilesToolStripMenuItem = new ToolStripMenuItem();
            browseDatabaseToolStripMenuItem = new ToolStripMenuItem();
            addTagsToolStripMenuItem = new ToolStripMenuItem();
            modeToolStripMenuItem = new ToolStripMenuItem();
            folderNavigationToolStripMenuItem = new ToolStripMenuItem();
            dataToolStripMenuItem = new ToolStripMenuItem();
            comicMangaModeToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, modeToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1194, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(180, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tagsToolStripMenuItem, tagTypesToolStripMenuItem, groupToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // tagsToolStripMenuItem
            // 
            tagsToolStripMenuItem.Name = "tagsToolStripMenuItem";
            tagsToolStripMenuItem.Size = new Size(180, 22);
            tagsToolStripMenuItem.Text = "Tags";
            // 
            // tagTypesToolStripMenuItem
            // 
            tagTypesToolStripMenuItem.Name = "tagTypesToolStripMenuItem";
            tagTypesToolStripMenuItem.Size = new Size(180, 22);
            tagTypesToolStripMenuItem.Text = "Tag Types";
            // 
            // groupToolStripMenuItem
            // 
            groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            groupToolStripMenuItem.Size = new Size(180, 22);
            groupToolStripMenuItem.Text = "Group";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { browseFilesToolStripMenuItem, browseDatabaseToolStripMenuItem, addTagsToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // browseFilesToolStripMenuItem
            // 
            browseFilesToolStripMenuItem.Name = "browseFilesToolStripMenuItem";
            browseFilesToolStripMenuItem.Size = new Size(180, 22);
            browseFilesToolStripMenuItem.Text = "Browse Files";
            // 
            // browseDatabaseToolStripMenuItem
            // 
            browseDatabaseToolStripMenuItem.Name = "browseDatabaseToolStripMenuItem";
            browseDatabaseToolStripMenuItem.Size = new Size(180, 22);
            browseDatabaseToolStripMenuItem.Text = "Browse Database";
            // 
            // addTagsToolStripMenuItem
            // 
            addTagsToolStripMenuItem.Name = "addTagsToolStripMenuItem";
            addTagsToolStripMenuItem.Size = new Size(180, 22);
            addTagsToolStripMenuItem.Text = "Add Tags";
            // 
            // modeToolStripMenuItem
            // 
            modeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { folderNavigationToolStripMenuItem, dataToolStripMenuItem, comicMangaModeToolStripMenuItem });
            modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            modeToolStripMenuItem.Size = new Size(50, 20);
            modeToolStripMenuItem.Text = "Mode";
            // 
            // folderNavigationToolStripMenuItem
            // 
            folderNavigationToolStripMenuItem.Name = "folderNavigationToolStripMenuItem";
            folderNavigationToolStripMenuItem.Size = new Size(185, 22);
            folderNavigationToolStripMenuItem.Text = "Folder navigation";
            folderNavigationToolStripMenuItem.Click += folderNavigationToolStripMenuItem_Click;
            // 
            // dataToolStripMenuItem
            // 
            dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            dataToolStripMenuItem.Size = new Size(185, 22);
            dataToolStripMenuItem.Text = "Database";
            // 
            // comicMangaModeToolStripMenuItem
            // 
            comicMangaModeToolStripMenuItem.Name = "comicMangaModeToolStripMenuItem";
            comicMangaModeToolStripMenuItem.Size = new Size(185, 22);
            comicMangaModeToolStripMenuItem.Text = "Comic/Manga mode";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1194, 569);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem modeToolStripMenuItem;
        private ToolStripMenuItem folderNavigationToolStripMenuItem;
        private ToolStripMenuItem dataToolStripMenuItem;
        private ToolStripMenuItem browseFilesToolStripMenuItem;
        private ToolStripMenuItem browseDatabaseToolStripMenuItem;
        private ToolStripMenuItem addTagsToolStripMenuItem;
        private ToolStripMenuItem tagsToolStripMenuItem;
        private ToolStripMenuItem tagTypesToolStripMenuItem;
        private ToolStripMenuItem groupToolStripMenuItem;
        private ToolStripMenuItem comicMangaModeToolStripMenuItem;
    }
}