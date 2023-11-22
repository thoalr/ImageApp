using AppDB;
using AppDB.Models;
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
    public partial class ImageDBBrowseControl : UserControl
    {

        public event EventHandler<string> CurrentImageChanged;

        //List<Media> imageFiles = new List<Media>();
        List<Media> filteredFiles = new List<Media>();
        int currentIndex = 0;

        string[] validExtensions = { ".jpg", ".JPG", ".jpeg", ".png", ".gif", ".webp" };
        MediaDBService db;

        public ImageDBBrowseControl(MediaDBService db)
        {
            InitializeComponent();
            //imageBox1.Image = Image.FromFile(imageFilePath);
            imageViewerControl1.NextImage += OnNextImage;
            imageViewerControl1.PrevImage += OnPrevImage;
            this.db = db;
        }

        void OnNextImage(object? sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % filteredFiles.Count;
            SetImage();
            //imageViewerControl1.Focus();
            CurrentImageChanged.Invoke(this, filteredFiles[currentIndex].Location);
        }
        void OnPrevImage(object? sender, EventArgs e)
        {
            currentIndex = (currentIndex - 1 + filteredFiles.Count) % filteredFiles.Count;
            SetImage();
            CurrentImageChanged.Invoke(this, filteredFiles[currentIndex].Location);
        }

        void SetImage()
        {
            imageViewerControl1.SetImage(filteredFiles[currentIndex].Location);
        }



        public void InitBrowsing(string initialFile)
        {
            var list = db.GetMedia().ToList();

            // imageFiles = list.Select(x => x).ToList();
            filteredFiles = list.Select(s => s).ToList();
            currentIndex = list.FindIndex(m => m.Location == initialFile);
            if (currentIndex == -1) { currentIndex = 0; }
            imageViewerControl1.SetImage(filteredFiles[currentIndex].Location);
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

        }

        private (bool, List<string>) stringToSearchTerms(string input)
        {
            return (false, new List<string>() { input });
        }

        private void FilterTags()
        {
            
            // search query supports checks for wether filename contains search term. Supports negative - 
            // supports multiple queries that are treated as AND
            // tags can have wildcards

            // sqlite uses % as * and _ as ?(single character wildcard)

            var currentFile = filteredFiles[currentIndex];

            var searchString = textBox1.Text;
            
            var (hasTags, hasNotTags) = db.ParseSearchString(searchString);
            var filteredMedia = db.GetMediaFromTags(hasTags, hasNotTags);
            
            filteredFiles = filteredMedia.ToList();
            if (filteredFiles.Contains(currentFile))
            {
                currentIndex = filteredFiles.IndexOf(currentFile);
            }
            else
            {
                currentIndex = 0;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode) {
                case Keys.Enter:
                    FilterTags();
                    break;

            }
        }
    }
}
