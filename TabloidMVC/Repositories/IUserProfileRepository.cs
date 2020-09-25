using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        void AddUser(UserProfile user);
        void DeleteUser(int id);
        void ReactivateUser(int id);
        List<UserProfile> GetAll();
        List<UserProfile> GetDeactivated();
        UserProfile GetByEmail(string email);
        UserProfile GetById(int id);
    }
}