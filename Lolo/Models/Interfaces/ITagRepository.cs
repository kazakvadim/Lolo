using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models.Interfaces
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAllTags();
       // IEnumerable<Tag> GetPostTags(Post post);
        void AddTag(Tag tag);
        void AddTagToPost(Tag tag, int id);
    }
}
