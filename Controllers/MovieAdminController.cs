using MovieBusinessLogic;
using MovieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 影院管理系统.Models;

namespace 影院管理系统.Controllers
{
    public class MovieAdminController : Controller
    {
        // GET: MovieAdmin
        Manager manager = new Manager();
        public ActionResult MIndex()
        {
            return View();
        }

        public ActionResult MInsert()
        {
            return View();
        }

        public ActionResult MDelete()
        {
            return View();
        }

        public ActionResult MUpdate()
        {
            return View();
        }

        public ActionResult MAllinf()
        {
            var posts = manager.GetMovieinfs().Select(post => new AllMViewModel()
            {
                ID=post.ID,
                Name=post.Name,
                Type=post.Type,
                Time=post.Time,
                Ondate=post.Ondate,
                Outdate=post.Outdate,
                Price=post.Price,
                Intro=post.Intro,
                Url=post.Url

            }).ToList();
            var postsList = new AllMListViewModel()
            {
                Count=posts.Count,
                AllMs=posts
            };
            return View(postsList);
        }

        public JsonResult SendNewMovie()
        {
            Movieinf movieinf = new Movieinf()
            {
                ID=Request.QueryString["newid"],
                Name=Request.QueryString["name"],
                Type=Request.QueryString["type"],
                Time=Request.QueryString["time"],
                Ondate=Request.QueryString["ondate"],
                Outdate=Request.QueryString["outdate"],
                Price=float.Parse(Request.QueryString["price"]),
                Intro=Request.QueryString["introduction"],
                Url=Request.QueryString["url"]
            };
            int flag = manager.InsertMovie(movieinf);
            movieinf.flag = flag;
            return Json(movieinf, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchtheMovie()
        {
            Movieinf movieinf = new Movieinf()
            {
                ID = Request.QueryString["id"]
            };
            Movieinf movieinf1 = manager.SearchMovie(movieinf);
            return Json(movieinf1, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeletetheMovie()
        {
            Movieinf movieinf = new Movieinf()
            {
                ID = Request.QueryString["id"]
            };
            int flag = manager.DeleteMovie(movieinf);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatetheMovie()
        {
            Movieinf movieinf = new Movieinf()
            {
                ID = Request.QueryString["newid"],
                Name = Request.QueryString["name"],
                Type = Request.QueryString["type"],
                Time = Request.QueryString["time"],
                Ondate = Request.QueryString["ondate"],
                Outdate = Request.QueryString["outdate"],
                Price = float.Parse(Request.QueryString["price"]),
                Intro = Request.QueryString["introduction"],
                Url = Request.QueryString["url"]
            };
            string currentid = Request.QueryString["currentid"];
            int flag = manager.UpdateMovie(movieinf, currentid);
            movieinf.flag = flag;
            return Json(movieinf, JsonRequestBehavior.AllowGet);
        }
       
      
    }
}