using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AppUI
{
    public partial class ImageFileBrowseControl : UserControl
    {
        public EventHandler<string> CurrentImageChanged;

        IList<string> imageFiles = new List<string>();
        int currentIndex = 0;

        string[] validExtensions = { ".jpg", "JPG","jpeg","png","gif","webp" };

        public ImageFileBrowseControl()
        {
            InitializeComponent();
            //imageBox1.Image = Image.FromFile(imageFilePath);
            imageViewerControl1.NextImage += OnNextImage;
            imageViewerControl1.PrevImage += OnPrevImage;
        }

        void OnNextImage(object? sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % imageFiles.Count;
            SetImage();
            //imageViewerControl1.Focus();
            CurrentImageChanged.Invoke(this, imageFiles[currentIndex]);
        }
        void OnPrevImage(object? sender, EventArgs e)
        {
            currentIndex = (currentIndex -1 + imageFiles.Count) % imageFiles.Count;
            SetImage();
            CurrentImageChanged.Invoke(this, imageFiles[currentIndex]);
        }

        void SetImage()
        {
            imageViewerControl1.SetImage(imageFiles[currentIndex]);
        }


        public void InitBrowsing(string file)
        {
            var directory = Path.GetDirectoryName(file);
            if (!Directory.Exists(directory)) { return; }
            imageFiles = Directory
                .EnumerateFiles(directory)
                .Where(f => validExtensions.Any(e => Path.GetExtension(f).Contains(e,StringComparison.OrdinalIgnoreCase)))
                .ToList();
            currentIndex = imageFiles.IndexOf(file);
            if (currentIndex == -1) { return; }
            imageViewerControl1.SetImage(file);
            imageViewerControl1.Focus();
        }


        private void ImageFileBrowseControl_Load(object sender, EventArgs e)
        {

        }


    }
}
