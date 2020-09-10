using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class FilmController : Controller
    {

        private MovieBusinessLogic.User user = new MovieBusinessLogic.User();
       

        public ActionResult Index(String id)
        {
            id = id == null ? "0" : id;
            ViewBag.mark = 0;
            if (id != null) {
                ViewBag.Data = user.getFilmById(id);
                // 如果登录了，去获取该账户是否评分了
                string mark = user.getMark(id);
                ViewBag.mark = (mark == "") ? 0 :float.Parse(mark);
            }
            return View();
        }

        public ActionResult Search(String key)
        {
            if (key == null) key = "1";
            ViewBag.Message = "Search Key : "+key;
            ViewBag.Data = user.QueryFilm(key);
            return View();
        }
        public int Rate(String id,String s)
        {
            // 验证是否登录 应替换为相应方法
            if (Session["uid"] == null) return 403;
            string uid = Session["uid"].ToString();
            return user.Rate(id,s,uid) ? 200:500;
        }

    }
}