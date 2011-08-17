using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoRMRepositoryBase.Models;

namespace NoRMRepositoryBase.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {

        private PostRepository _postRpository = null;
        private BlogRepository _blogRepository = null;

        public PostsController() {
            _postRpository = new PostRepository() { Settings = Settings };
            _blogRepository = new BlogRepository() { Settings = Settings };
        }

        //
        // GET: /Admin/Posts/

        public ActionResult Index()
        {
            return View(_postRpository.FindAll());
        }

        //
        // GET: /Admin/Posts/Details/5

        public ActionResult Details(string id)
        {
            return View(_postRpository.FindById(id));
        }

        //
        // GET: /Admin/Posts/Create

        public ActionResult Create()
        {

            ViewData["Blogs"] = new SelectList(_blogRepository.FindAll(), "Id", "Name");

            return View();
        } 

        //
        // POST: /Admin/Posts/Create

        [HttpPost]
        public ActionResult Create([Bind]Post post)
        {
            try
            {                
                _postRpository.Create(post);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Posts/Edit/5
 
        public ActionResult Edit(string id)
        {

            Post post = _postRpository.FindById(id);
            ViewData["Blogs"] = new SelectList(_blogRepository.FindAll(), "Id", "Name", post.Id);

            return View(post);
        }

        //
        // POST: /Admin/Posts/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind]Post post)
        {
            try
            {

                _postRpository.Save(post);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
