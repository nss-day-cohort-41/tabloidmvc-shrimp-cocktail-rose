using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class PostTagController : Controller
    {

        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;

        public PostTagController(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }
        // GET: PostTagController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PostTagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

  
        // GET: PostTagController/Edit/5
        public ActionResult Edit(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);
            List<Tag> postTags = _tagRepository.GetPostTags(post.Id);
            List<Tag> allTags = _tagRepository.GetAll();

            PostTagMgmtViewModel vm = new PostTagMgmtViewModel()
            {
                Post = post,
                PostTags = postTags,
                AllTags = allTags
            };
            return View(vm);
        }

        // POST: PostTagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, List<Tag> AllTags)
        {
            
            try
            {
                
                
                return RedirectToAction("Index","PostController");
            }
            catch
            {
                return View();
            }
        }

    }
}
