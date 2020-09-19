using MovieBusinessLogic;
using MovieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class MovieInfoController : Controller
    {
        Admin admin = new Admin();
        //默认页面
        public ActionResult Index()
        {
            return View();
        }

        //插入电影页面
        public ActionResult MInsert()
        {
            return View();
        }

        //删除电影页面
        public ActionResult MDelete()
        {
            return View();
        }

        //更新电影页面
        public ActionResult MUpdate()
        {
            return View();
        }

        //获取所有电影信息
        public ActionResult MAllinf()
        {
            var posts = admin.GetMovieinfs().Select(post => new AllMViewModel()
            {
                ID = post.ID,
                Name = post.Name,
                Type = post.Type,
                Time = post.Time,
                Ondate = post.Ondate,
                Outdate = post.Outdate,
                Price = post.Price,
                Intro = post.Intro,
                Url = post.Url

            }).ToList();
            var postsList = new AllMListViewModel()
            {
                Count = posts.Count,
                AllMs = posts
            };
            return View(postsList);
        }

        //添加电影功能
        public JsonResult SendNewMovie()
        {
            Movieinfo movieinf = new Movieinfo()
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
            int flag = admin.InsertMovie(movieinf);
            movieinf.flag = flag;
            return Json(movieinf, JsonRequestBehavior.AllowGet);
        }

        //搜索电影功能
        public JsonResult SearchtheMovie()
        {
            Movieinfo movieinf = new Movieinfo()
            {
                ID = Request.QueryString["id"]
            };
            Movieinfo movieinf1 = admin.SearchMovie(movieinf.ID);
            return Json(movieinf1, JsonRequestBehavior.AllowGet);

        }

        //删除电影功能
        public JsonResult DeletetheMovie()
        {
            string ID = Request.QueryString["id"];
            int flag = admin.DeleteMovie(ID);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        //更新电影功能
        public JsonResult UpdatetheMovie()
        {
            Movieinfo movieinf = new Movieinfo()
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
            int flag = admin.UpdateMovie(movieinf, currentid);
            movieinf.flag = flag;
            return Json(movieinf, JsonRequestBehavior.AllowGet);
        }
    }
}