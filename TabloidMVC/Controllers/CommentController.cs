using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        public CommentController(IPostRepository postRepository,
            ICommentRepository commentRepository,
            IUserProfileRepository userProfileRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userProfileRepository = userProfileRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var vm = new CommentPostViewModel();
            vm.Post = _postRepository.GetPublishedPostById(id);
           
            vm.Comments = _commentRepository.GetAll(id);
            vm.UserId = GetCurrentUserProfileId();
            
            return View(vm);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, Comment comment)
        {
            try
            {
                //_commentRepository
                comment.PostId = id;
                comment.UserProfileId = GetCurrentUserProfileId();
                comment.CreateDateTime = DateAndTime.Now;
               
                comment.User = new UserProfile();
                _commentRepository.AddComment(comment);
               return RedirectToAction("Details", "Post", new { id = id } );
            }

            catch (Exception ex)
            {
                return View(comment);
            }
           
        }


        public IActionResult Delete(int id)
        {
            Comment comment = _commentRepository.GetCommentById(id);

            return View(comment);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Comment comment)
        {
            try
            {
                var vm = new CommentPostViewModel();

                vm.Comments = _commentRepository.GetAll(comment.PostId);
                vm.Post = _postRepository.GetPublishedPostById(comment.PostId);

                int postId = comment.PostId;
                _commentRepository.Delete(id);
               ///errror is here


              
                return RedirectToAction("details", "Comment", new { id = vm.Post.Id });
            }

            catch
            {
                return View(comment);
            }
        }
        
        public ActionResult Edit(int id)
        {
            Comment comment = _commentRepository.GetById(id);
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                _commentRepository.UpdateComment(comment);
            return Redirect($"~/Post/Details/{comment.PostId}");

            } catch (Exception ex)
            {
                return View(comment);
            }

        }
    }
}
