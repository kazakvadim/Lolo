using Lolo.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _appDbContext;

        public TagRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddTag(Tag tag)
        {

            //    #var zx = _appDbContext.Tags.ToList();
            //_appDbContext.Tags.Add(tag);
            var z = _appDbContext.Tags.ToList();
            //_appDbContext.SaveChanges();
            //
        }

        public void AddTagToPost(Tag tag, int id)
        {
            var post = _appDbContext.Posts.FirstOrDefault(p => p.PostId == id);
            _appDbContext.AddRange(new PostTag {Post = post, Tag = tag});
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _appDbContext.Tags;
        }

        
    }
}
