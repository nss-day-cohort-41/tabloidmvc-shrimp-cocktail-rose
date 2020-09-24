using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var vm = new CommentPostViewModel();
            vm.Post = _postRepository.GetPublishedPostById(id);
            vm.Comments = _commentRepository.GetAll(id, _userProfileRepository);
            
            return View(vm);
        }
    }
}
