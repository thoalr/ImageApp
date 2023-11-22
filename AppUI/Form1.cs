using AppDB.Models;
using System;
using System.Windows.Forms;

namespace AppUI
{
    public partial class Form1 : Form
    {



        MediaDBControl mediaDBControl1;
        media_databaseContext dbc = new media_databaseContext();
        ImageFileBrowseControl fileBrowseControl1;
        ImageDBBrowseControl dBBrowseControl1;

        string? lastFileFromFileSystem;
        enum AppView
        {
            Empty,
            FolderImageBrowser,
            DatabaseImageBrowser,
            FolderMediaDBBrowser,
            DatabaseBrowser
        }
        AppView currentView = AppView.Empty;

        enum ImageViewMode
        {
            Single,
            DoubleLefToRight,
            DoubleRightToLeft
        }
        ImageViewMode imageViewMode = ImageViewMode.Single;
        int viewModeOffset = 0;

        public Form1()
        {
            InitializeComponent();
            //Init();

            var args = Environment.GetCommandLineArgs();

            if (args != null)
            {
                if (args.Length >= 2)
                {
                    if (Path.Exists(args[1]))
                    {
                        var initialFile = args[1];

                        SetBrowserControl(initialFile, AppView.FolderImageBrowser);

                    }
                }
            }
            else
            {
                // start browsing database
                // if database is empty open ___????___
            }

            //InitTestSkiaImageBox();

        }

        //private void InitTestSkiaImageBox()
        //{
        //    fileBrowseControl1 = new ImageFileBrowseControl();
        //    var startingImage = @"D:\Anime pictures\__named__3\__amane_kanata_and_amane_kanata_hololive_drawn_by_buket_pudding_i__1e47cbfcad571522bb093ac25c63c64e.png";
        //    fileBrowseControl1.SetImage(startingImage);
        //    fileBrowseControl1.Dock = DockStyle.Fill;
        //    this.Controls.Add(fileBrowseControl1);
        //}

        //private void InitTest()
        //{
        //    var startingImage = @"D:\Anime pictures\__named__3\__amane_kanata_and_amane_kanata_hololive_drawn_by_buket_pudding_i__1e47cbfcad571522bb093ac25c63c64e.png";
        //    var startingDir = @"D:\Anime pictures\__sakura_kyouko__\";
        //    dbc = new media_databaseContext();
        //    mediaDBControl1 = new MediaDBControl() { currentDirectory = startingDir, dbc = dbc };
        //    mediaDBControl1.Dock = DockStyle.Fill;
        //    //mediaDBControl1.MinimumSize = new Size(1020, 580);
        //    //this.MinimumSize = new Size(1040, 600);
        //    this.Controls.Add(mediaDBControl1);

        //    //mediaDBControl1.Parent = this;

        //    //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        //}

        private void InitEmpty()
        {

        }

        private void SetBrowserControl(string? file, AppView view)
        {
            if (file != null & view == AppView.Empty)
            {
                view = AppView.FolderImageBrowser;
            }
            if (file == null && (view != AppView.Empty || view != AppView.DatabaseImageBrowser))
            {
                return;
            }
            switch (view)
            {
                case AppView.Empty:
                    mediaDBControl1?.Hide();
                    fileBrowseControl1?.Hide();
                    break;
                case AppView.FolderImageBrowser:
                    mediaDBControl1?.Hide();
                    dBBrowseControl1?.Hide();
                    if (fileBrowseControl1 == null)
                    {
                        fileBrowseControl1 = new ImageFileBrowseControl();
                        fileBrowseControl1.Dock = DockStyle.Fill;
                        //fileBrowseControl1.InitBrowsing(file!);
                        fileBrowseControl1.CurrentImageChanged += OnImageChanged;
                        this.Controls.Add(fileBrowseControl1);
                    }
                    fileBrowseControl1.InitBrowsing(file!);
                    fileBrowseControl1!.Show();
                    break;
                case AppView.DatabaseImageBrowser:
                    mediaDBControl1?.Hide();
                    fileBrowseControl1?.Hide();
                    if (dBBrowseControl1 == null)
                    {
                        if(dbc.Media.Count() == 0)
                        {
                            MessageBox.Show("No images in database\nBrowsing not available");
                            return;
                        }
                        dBBrowseControl1 = new ImageDBBrowseControl(dbc);
                        dBBrowseControl1.Dock = DockStyle.Fill;
                        dBBrowseControl1!.CurrentImageChanged += OnImageChanged;
                        this.Controls.Add(dBBrowseControl1);
                    }
                    dBBrowseControl1.InitBrowsing(file!);
                    dBBrowseControl1!.Show();
                    break;
                case AppView.FolderMediaDBBrowser:
                    dBBrowseControl1?.Hide();
                    fileBrowseControl1?.Hide();
                    if (mediaDBControl1 == null)
                    {
                        mediaDBControl1 = new MediaDBControl(Path.GetDirectoryName(file!)!, dbc);
                        //mediaDBControl1.SetImage(file);
                        this.Controls.Add(mediaDBControl1);
                    }
                    mediaDBControl1!.Show();
                    break;
                case AppView.DatabaseBrowser:
                    break;
            }
        }

        void OnImageChanged(object? sender, string newImageFile)
        {
            lastFileFromFileSystem = newImageFile;
        }

        private void FileInit(string startingFile)
        {
            lastFileFromFileSystem = startingFile;
            SetBrowserControl(startingFile, currentView);
        }
        private void FilelessInit()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void folderNavigationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lastDir = new DirectoryInfo(lastFileFromFileSystem ?? @"C:\").FullName;

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                if (lastFileFromFileSystem != null)
                {
                    openFileDialog1.FileName = Path.GetFileName(lastFileFromFileSystem);
                }

                openFileDialog1.InitialDirectory = lastDir;

                //openFileDialog1.Filter = "image files|*.jpg;*.JPG;*.jpeg;*.png;*.gif;*.webp|All files|*.*";
                openFileDialog1.Filter = "All files|*.*";

                //openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = false;


                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        FileInit(openFileDialog1.FileName);
                    }
                    catch (Exception exception)
                    {
                        var popup = MessageBox.Show("Failed to load file\n" + exception.Message);
                    }

                }
            }
        }

        private void browseFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browseFilesToolStripMenuItem.Checked) return;
            else
            {
                // uncheck other views
                browseDatabaseToolStripMenuItem.Checked = false;
                addTagsToolStripMenuItem.Checked = false;
            }
            browseFilesToolStripMenuItem.Checked = true;
            browseFilesToolStripMenuItem.CheckState = CheckState.Indeterminate;
            currentView = AppView.FolderImageBrowser;
            SetBrowserControl(lastFileFromFileSystem, AppView.FolderImageBrowser);
        }

        private void browseDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browseDatabaseToolStripMenuItem.Checked) return;
            else
            {
                // uncheck other views
                browseFilesToolStripMenuItem.Checked = false;
                addTagsToolStripMenuItem.Checked = false;
            }
            browseDatabaseToolStripMenuItem.Checked = true;
            browseDatabaseToolStripMenuItem.CheckState = CheckState.Indeterminate;
            currentView = AppView.DatabaseImageBrowser;
            SetBrowserControl(lastFileFromFileSystem, AppView.DatabaseImageBrowser);
        }

        private void addTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (addTagsToolStripMenuItem.Checked) return;
            else
            {
                // uncheck other views
                browseDatabaseToolStripMenuItem.Checked = false;
                browseFilesToolStripMenuItem.Checked = false;
            }
            addTagsToolStripMenuItem.Checked = true;
            addTagsToolStripMenuItem.CheckState = CheckState.Indeterminate;
            currentView = AppView.FolderMediaDBBrowser;
            SetBrowserControl(lastFileFromFileSystem, AppView.FolderMediaDBBrowser);
        }
    }
}