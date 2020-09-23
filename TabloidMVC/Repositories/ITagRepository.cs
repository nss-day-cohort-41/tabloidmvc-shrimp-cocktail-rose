using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        void AddTag(Tag tag);
        List<Tag> GetAll();
    }
}