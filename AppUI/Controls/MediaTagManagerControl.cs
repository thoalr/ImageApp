﻿using AppDB.Models;
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
    public partial class MediaTagManagerControl : UserControl
    {
        public string currentDirectory;

        IList<string> files;
        IList<string> filteredFiles;
        public media_databaseContext dbc;


        public MediaTagManagerControl()
        {
            InitializeComponent();
        }

        public MediaTagManagerControl(string directory, media_databaseContext dbc)
        {

        }

        private void MediaTagManagerControl_Load(object sender, EventArgs e)
        {

        }
    }
}
