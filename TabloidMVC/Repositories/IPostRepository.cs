using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void DeletePost(int postId);
        List<Post> GetAllPublishedPosts();
        List<Post> GetAllUserPosts(int userProfileId);
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
        void UpdatePost(Post post);
    }
}