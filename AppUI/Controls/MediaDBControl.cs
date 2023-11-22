using AppDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppUI.Utilities;
using AppDB;

namespace AppUI
{
    public partial class MediaDBControl : UserControl
    {
        public string currentDirectory;

        IList<string> files;
        IList<string> filteredFiles;
        MediaDBService db;

        public MediaDBControl()
        {
            InitializeComponent();
        }

        public MediaDBControl(string directory, MediaDBService dBService)
        {
            InitializeComponent();
            currentDirectory = directory;
            this.db = dBService;
            files = new List<string>(Directory.EnumerateFiles(currentDirectory).ToList());
            //listView1.DataContext = files;
            //listView1.Update();

            


        }

        private void MediaDBControl_Load(object sender, EventArgs e)
        {

            files = (Directory.EnumerateFiles(currentDirectory).ToList());
            filteredFiles = files;
            var listItems = files.Select(f => new ListViewItem(Path.GetFileName(f))).ToArray();
            listView1.Columns.Add("File list", listItems.OrderByDescending(s => s.Text.Length).Select(s => (int)(s.Text.Length * 6.4)).FirstOrDefault(800));
            listView1.Items.AddRange(listItems);

            comboBox2.Items.AddRange(db.GetTagTypes().Select(t => t.TypeName).ToArray());
            comboBox2.SelectedIndex = 0;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            var autoCompleteSource = new AutoCompleteStringCollection();
            autoCompleteSource.AddRange(db.GetTags().Select(t => t.Tag1).ToArray());
            textBox2.AutoCompleteCustomSource = autoCompleteSource;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;



        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Update listview to display only files matching search query
            // search query supports checks for wether filename contains search term. Supports negative - 
            // supports multiple queries that are treated as AND

            // sqlite uses % as * and _ as ?(single character wildcard)

            var searchString = textBox1.Text;
            var searchTerms =
                searchString.Trim().Split(' ')
                    .Select(s =>
                    {
                        var tmps = s.Trim();
                        bool neg = tmps.StartsWith('-');
                        if (neg) tmps = tmps.Substring(1);
                        return new { Negative = neg, Wildcard = new Wildcard("*" + tmps.Trim('*') + "*", RegexOptions.IgnoreCase) };
                    })
                    .AsEnumerable();

            // xor for handling negative and positive searches
            var newFileList = files
                    .Select(f => new {FullPath = f, FileName = Path.GetFileName(f)})
                    .Where(f => searchTerms.All(s => s.Negative ^ s.Wildcard.IsMatch(f.FileName)))
                    .Select(f => f.FullPath);
            var newListItems = newFileList.Select(f => new ListViewItem(f)).ToArray();
            listView1.Columns.Clear();
            listView1.Columns.Add("File list", newListItems.OrderByDescending(s => s.Text.Length).Select(s => (int)(s.Text.Length * 6.4)).FirstOrDefault(800));
            listView1.Items.Clear();
            filteredFiles = newFileList.ToList();
            listView1.Items.AddRange(newListItems);
        }

        // add tag to current search range
        private void button1_Click(object sender, EventArgs e)
        {
            var tag = db.AddTagIfNotExists(comboBox1.Text, comboBox2.Text);

            var media = db.AddMediaRangeIfNotExists(filteredFiles);
            db.AddTagToMedia(media, tag);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            var searchString = comboBox1.Text;
            if (searchString.Length < 3) { return; }
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(db.QueryTagsContain(searchString).Select(t => t.Tag1).ToArray());
            comboBox1.Select(searchString.Length, 0);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
