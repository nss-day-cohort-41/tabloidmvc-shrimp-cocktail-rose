using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public CommentController(IPostRepository postRepository,
            ICommentRepository commentRepository,
            IUserProfileRepository userProfileRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userProfileRepository = userProfileRepository;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var vm = new CommentPostViewModel();
            vm.Post = _postRepository.GetPublishedPostById(id);
            vm.Comments = _commentRepository.GetAll(id, _userProfileRepository);
            
            return View(vm);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(int id, Comment comment)
        {
            //need to add postID!!==============================================

            //need to get userId!

            return View();
        }
    }
}
