using AppDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB
{
    public class MediaDBService
    {
        private media_databaseContext _context = new media_databaseContext();

        public MediaDBService() { }

        public void AddMediaRangeIfNotExists(IEnumerable<string> mediaList)
        {

        }

    }
}
