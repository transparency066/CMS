using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace MovieWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()//显示主页
        {
            return View();
        }

        public ActionResult Cancel()
        {
            Session["uid"] = null;
            return RedirectToAction("Index");
        }

    }

}