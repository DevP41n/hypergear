using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        private HyperGearEntities db = new HyperGearEntities();
        public ActionResult Post()
        {
            var post = db.TinTucs.Where(x => x.Status == 1);
            return View(post);
        }

        public ActionResult Details(string id)
        {
            var post = db.TinTucs.Where(x => x.Slug == id).First();
            return View(post);
        }
    }
}