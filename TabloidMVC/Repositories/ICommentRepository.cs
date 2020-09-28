using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAll(int id, IUserProfileRepository _userProfileRepository);

        public void AddComment(Comment comment);
        public void UpdateComment(Comment comment);
        Comment GetById(int id);

    }
}
