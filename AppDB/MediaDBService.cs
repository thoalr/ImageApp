using AppDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AppDB
{
    public class MediaDBService
    {
        private static media_databaseContext _context = new media_databaseContext();

        public MediaDBService() 
        {

        }

        public IEnumerable<Media> AddMediaRangeIfNotExists(IEnumerable<string> mediaFilePathList)
        {
            var existingMedia = _context.Media.Where(m => mediaFilePathList.Contains(m.Location));
            var newMedia = new List<Media>();
            foreach (var file in mediaFilePathList)
            {
                if (existingMedia.Any(m => m.Location == file)) { continue; }
                else
                {
                    newMedia.Add(new Media { Location = file });
                }
            }
            _context.AddRange(newMedia);
            _context.SaveChanges();
            return existingMedia.Concat(newMedia);
        }

        public void AddTagToMedia(IEnumerable<Media> medias, Tag tag)
        {
            foreach (var media in medias)
            {
                if(!media.Tags.Contains(tag))
                    media.Tags.Add(tag);
            }
            _context.UpdateRange(medias);
            _context.SaveChanges();
        }

        
        public Tag AddTagIfNotExists(string tag, string tagType) 
        {
            var dbTagType = AddTagTypeIfNotExists(tagType);
            var existingTag = this.QueryTagExists(tag, dbTagType);
            if (existingTag== null)
            {
                existingTag = new Tag { Tag1 = tag, TagType = dbTagType };
                _context.Tags.Add(existingTag);
                _context.SaveChanges();
            }
            return existingTag;
        }

        public TagType AddTagTypeIfNotExists(string tagType) 
        {
            var existingTagType = this.QueryTagTypeExists(tagType);
            if (existingTagType == null)
            {
                existingTagType = new TagType { TypeName = tagType };
                _context.TagTypes.Add(existingTagType);
                _context.SaveChanges();
            }
            return existingTagType;
        }

        public Tag? QueryTagExists(string tag, TagType tagType)
        {
            return _context.Tags.Where(t => t.Tag1 == tag && t.TagType == tagType).ToList().DefaultIfEmpty(null).First();
        }

        public TagType? QueryTagTypeExists(string tagType)
        {
            return _context.TagTypes.Where(t => t.TypeName == tagType).ToList().DefaultIfEmpty(null).First();
        }

        public IEnumerable<Media> GetMedia(IEnumerable<string> filePaths)
        {
            var existingMedia = _context.Media.Where(m => filePaths.Contains(m.Location));
            return existingMedia;
        }

        public IEnumerable<Media> GetMedia()
        {
            var existingMedia = _context.Media.Select(s=>s);
            return existingMedia;
        }

        public IEnumerable<TagType> GetTagTypes()
        {
            var tagTypes = _context.TagTypes.Select(s=>s);
            return tagTypes;
        }

        public IEnumerable<Tag> GetTags()
        {
            var tags = _context.Tags.Select(s => s);
            return tags;
        }

        public IEnumerable<Tag> QueryTagsContain(string searchString)
        {
            var result = _context.Tags.Where(t => t.Tag1.Contains(searchString));
            return result;
        }


        /// <summary>
        /// Parses search string into tags that should be and should not be associated with a media
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>First item are the tags the media should have. The second item are the tags the media should not have</returns>
        public Tuple<IEnumerable<Tag> , IEnumerable<Tag>> ParseSearchString(string searchString)
        {
            var searchTerms =
                searchString.Trim().Split(' ').Where(s => (s != "-" && s.Length > 0))
                    .Select(s =>
                    {
                        var tags = new List<Tag>();
                        var tmps = s.Trim();
                        bool neg = tmps.StartsWith('-');
                        if (neg) tmps = tmps.Substring(1);
                        if (!tmps.Contains('*')) tags.AddRange(_context.Tags.Where(t => t.Tag1 == tmps).Take(1));
                        if (tmps.EndsWith('*') && tmps.StartsWith('*'))
                            tags.AddRange(_context.Tags.Where(t => t.Tag1.Contains(tmps.Trim('*'))).ToList());
                        else if (tmps.EndsWith('*'))
                            tags.AddRange(_context.Tags.Where(t => t.Tag1.StartsWith(tmps.Trim('*'))).ToList());
                        else if (tmps.StartsWith('*'))
                            tags.AddRange(_context.Tags.Where(t => t.Tag1.EndsWith(tmps.Trim('*'))).ToList());
                        return new { Negative = neg, Tags = tags };
                    })
                    .AsEnumerable();
            var hasTags = searchTerms.Where(st => !st.Negative).SelectMany(st => st.Tags);
            var hasNotTags = searchTerms.Where(st => st.Negative).SelectMany(st => st.Tags);
            return new Tuple<IEnumerable<Tag>, IEnumerable<Tag>>(hasTags, hasNotTags);
        }

        public IEnumerable<Media> GetMediaFromTags(IEnumerable<Tag> hasTags, IEnumerable<Tag> hasNotTags)
        {
            // probably pretty expensive operation
            //var newFileList = dbc.Media
            //    .Where(m =>
            //        hasTags.All(t => m.Tags.Contains(t))
            //        &&
            //        hasNotTags.Any(t => m.Tags.Contains(t))
            //        );
            
            // attempt to optimize by successively querying smaller and smaller sets
            var filteredMedia = _context.Media.Select(s => s);
            foreach (var tag in hasTags)
            {
                filteredMedia = filteredMedia.Where(m => m.Tags.Contains(tag));
            }
            // is it faster to start with negatives or positives?
            foreach (var tag in hasNotTags)
            {
                filteredMedia = filteredMedia.Where(m => !m.Tags.Contains(tag));
            }
            return filteredMedia;
        }

        

    }
}
