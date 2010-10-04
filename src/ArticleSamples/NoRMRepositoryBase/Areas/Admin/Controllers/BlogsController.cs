using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoRMRepositoryBase.Models;

namespace NoRMRepositoryBase.Areas.Admin.Controllers
{
    public class BlogsController : AdminBaseController
    {

        private BlogRepository _repository = null;

        public BlogsController() {
            _repository = new BlogRepository() { Settings = Settings };
        }

        //
        // GET: /Admin/Blog/

        public ActionResult Index()
        {
            return View(_repository.FindAll());
        }

        //
        // GET: /Admin/Blog/Details/5

        public ActionResult Details(string id)
        {
            return View(_repository.FindById(id));
        }

        //
        // GET: /Admin/Blog/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Blog/Create

        [HttpPost]
        public ActionResult Create([Bind]Blog blog)
        {
            try
            {
                // TODO: Add insert logic here
                _repository.Create(blog);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Blog/Edit/5
 
        public ActionResult Edit(string id)
        {
            return View(_repository.FindById(id));
        }

        //
        // POST: /Admin/Blog/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind]Blog blog)
        {
            try
            {
                _repository.Save(blog);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // POST: /Admin/Blog/Delete/5

        public ActionResult Delete(string id)
        {
            try
            {
                _repository.Remove(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
