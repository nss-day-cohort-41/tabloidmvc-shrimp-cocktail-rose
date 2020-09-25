using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq.Expressions;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using System.Linq;

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
                PostId = id,
                PostTags = postTags,
                AllTags = allTags
            };
            return View(vm);
        }

        // POST: PostTagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string[] PostTagsChecks)
        {

            Post post = _postRepository.GetPublishedPostById(id);
            List<Tag> postTags = _tagRepository.GetPostTags(post.Id);
            List<Tag> allTags = _tagRepository.GetAll();

            PostTagMgmtViewModel vm = new PostTagMgmtViewModel()
            {
                PostId = id,
                PostTags = postTags,
                AllTags = allTags
            };

            
     
            try
            {
                // Original Post Tag ID's
                List<int> OrgTagIds = postTags.Select(tag => tag.Id).ToList();
                // Newly-Set Post Tag ID's
                List<int> AdjTagIds = Array.ConvertAll(PostTagsChecks, int.Parse).ToList();
                // Tag ID's to remove
                List<int> deprecatedPostTags = OrgTagIds.Except(AdjTagIds).ToList();
                // Tag ID's to add
                List<int> newPostTags = AdjTagIds.Except(OrgTagIds).ToList();

                if (deprecatedPostTags.Count > 0)
                {
                    foreach(int tagId in deprecatedPostTags) { 
                        PostTag postTag = new PostTag()
                        {
                            PostId = id,
                            TagId = tagId
                        };
                        _tagRepository.DeletePostTag(postTag); };
                }
                if (newPostTags.Count > 0)
                {
                    foreach (int tagId in newPostTags) {
                        PostTag postTag = new PostTag()
                        {
                            PostId = id,
                            TagId = tagId
                        };
                        _tagRepository.AddPostTag(postTag); 
                    };
                }
                
                
                return RedirectToAction("Details", "Post", new { id = vm.PostId } );
            }
            catch
            {
                return View(vm);
            }
        }

    }
}
