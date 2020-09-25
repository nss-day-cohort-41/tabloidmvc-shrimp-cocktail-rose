using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        void AddTag(Tag tag);
        void AddPostTag(PostTag postTag);
        void DeletePostTag(PostTag postTag);
        void DeleteTag(int tagId);
        List<Tag> GetAll();
        List<Tag> GetPostTags(int postId);
        Tag GetTagById(int tagId);
        void UpdateTag(Tag tag);
    }
}