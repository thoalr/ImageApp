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
        private media_databaseContext _context = new media_databaseContext();

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

        public IEnumerable<Media> GetMediaFromTags(IEnumerable<Tag> hasTags, IEnumerable<Tag> hasNotTags)
        {
            return null;
        }

        

    }
}
