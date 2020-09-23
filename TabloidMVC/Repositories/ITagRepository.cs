using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        void AddTag(Tag tag);
        void DeleteTag(int tagId);
        List<Tag> GetAll();
        Tag GetTagById(int tagId);
    }
}