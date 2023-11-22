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
        media_databaseContext dbc;

        public ImageDBBrowseControl(media_databaseContext dbc)
        {
            InitializeComponent();
            //imageBox1.Image = Image.FromFile(imageFilePath);
            imageViewerControl1.NextImage += OnNextImage;
            imageViewerControl1.PrevImage += OnPrevImage;
            this.dbc = dbc;
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
            var list = dbc.Media.ToList();

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

        private void FilterTags()
        {
            
            // search query supports checks for wether filename contains search term. Supports negative - 
            // supports multiple queries that are treated as AND
            // tags can have wildcards

            // sqlite uses % as * and _ as ?(single character wildcard)

            var currentFile = filteredFiles[currentIndex];

            var searchString = textBox1.Text;
            var searchTerms =
                searchString.Trim().Split(' ').Where(s => (s != "-" && s.Length > 0))
                    .Select(s =>
                    {
                        var tags = new List<Tag>();
                        var tmps = s.Trim();
                        bool neg = tmps.StartsWith('-');
                        if (neg) tmps = tmps.Substring(1);
                        if (!tmps.Contains('*')) tags.AddRange(dbc.Tags.Where(t => t.Tag1 == tmps).Take(1));
                        if (tmps.EndsWith('*') && tmps.StartsWith('*'))
                            tags.AddRange(dbc.Tags.Where(t => t.Tag1.Contains(tmps.Trim('*'))).ToList());
                        else if (tmps.EndsWith('*')) 
                            tags.AddRange(dbc.Tags.Where(t => t.Tag1.StartsWith(tmps.Trim('*'))).ToList());
                        else if (tmps.StartsWith('*')) 
                            tags.AddRange(dbc.Tags.Where(t => t.Tag1.EndsWith(tmps.Trim('*'))).ToList());
                        return new { Negative = neg, Tags = tags };
                    })
                    .AsEnumerable();
            var hasTags = searchTerms.Where(st => !st.Negative).SelectMany(st => st.Tags);
            var hasNotTags = searchTerms.Where(st => st.Negative).SelectMany(st => st.Tags);

            // probably pretty expensive operation
            var newFileList = dbc.Media
                .Where(m => 
                    hasTags.All(t => m.Tags.Contains(t)) 
                    && 
                    hasNotTags.Any(t => m.Tags.Contains(t))
                    );
            // attempt to optimize by successively querying smaller and smaller sets
            var filteredMedia = dbc.Media.Select(s=>s);
            foreach(var tag in hasTags)
            {
                filteredMedia = filteredMedia.Where( m => m.Tags.Contains(tag) );
            }
            foreach(var tag in hasNotTags)
            {
                filteredMedia = filteredMedia.Where(m => !m.Tags.Contains(tag));
            }
            

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
