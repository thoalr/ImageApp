using AppUI.Utilities;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace AppUI
{
    public partial class ImageFileBrowseControl : UserControl
    {

        public event EventHandler<string> CurrentImageChanged;

        IList<string> imageFiles = new List<string>();
        IList<string> filteredFiles = new List<string>();
        int currentIndex = 0;

        string[] validExtensions = { ".jpg", ".JPG", ".jpeg", ".png", ".gif", ".webp" };

        public ImageFileBrowseControl()
        {
            InitializeComponent();
            //imageBox1.Image = Image.FromFile(imageFilePath);
            imageViewerControl1.NextImage += OnNextImage;
            imageViewerControl1.PrevImage += OnPrevImage;
        }

        void OnNextImage(object? sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % filteredFiles.Count;
            SetImage();
            //imageViewerControl1.Focus();
            CurrentImageChanged.Invoke(this, filteredFiles[currentIndex]);
        }
        void OnPrevImage(object? sender, EventArgs e)
        {
            currentIndex = (currentIndex - 1 + filteredFiles.Count) % filteredFiles.Count;
            SetImage();
            CurrentImageChanged.Invoke(this, filteredFiles[currentIndex]);
        }

        void SetImage()
        {
            imageViewerControl1.SetImage(filteredFiles[currentIndex]);
        }


        public void InitBrowsing(string file)
        {
            var directory = Path.GetDirectoryName(file);
            if (!Directory.Exists(directory)) { return; }
            imageFiles = Directory
                .EnumerateFiles(directory)
                .Where(f => validExtensions.Any(e => Path.GetExtension(f).EndsWith(e, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            filteredFiles = imageFiles.Select(s=>s).ToList();
            currentIndex = imageFiles.IndexOf(file);
            if (currentIndex == -1) { return; }
            imageViewerControl1.SetImage(file);
            imageViewerControl1.Focus();
        }


        private void ImageFileBrowseControl_Load(object sender, EventArgs e)
        {

        }

        private void imageViewerControl1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void imageViewerControl1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void imageViewerControl1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            // search query supports checks for wether filename contains search term. Supports negative - 
            // supports multiple queries that are treated as AND

            // sqlite uses % as * and _ as ?(single character wildcard)

            var currentFile = filteredFiles[currentIndex];

            var searchString = textBox1.Text;
            var searchTerms =
                searchString.Trim().Split(' ').Where(s => (s != "-" && s.Length > 0))
                    .Select(s =>
                    {
                        var tmps = s.Trim();
                        bool neg = tmps.StartsWith('-');
                        if (neg) tmps = tmps.Substring(1);
                        
                        return new { Negative = neg, Wildcard = new Wildcard("*" + tmps.Trim('*') + "*", RegexOptions.IgnoreCase) };
                    })
                    .AsEnumerable();

            // xor for handling negative and positive searches
            var newFileList = imageFiles.Select(f => Path.GetFileName(f)).Where(f => searchTerms.All(s => s.Negative ^ s.Wildcard.IsMatch(f)));
            
            filteredFiles = newFileList.ToList();
            if(filteredFiles.Contains(currentFile))
            {
                currentIndex = filteredFiles.IndexOf(currentFile);
            }
            else
            {
                currentIndex = 0;
            }
        }
    }
}
