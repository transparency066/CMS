using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace MovieWeb.Controllers
{
    public class CommentController : Controller
    {
        private MovieBusinessLogic.Admin admin = new MovieBusinessLogic.Admin();
        public ActionResult Comments()
        {
            ViewBag.data = admin.queryComments();
            ViewBag.admin = Session["aid"];
            return View();
        }
        public String DelComment(String pid,String id)
        {
            if (Session["aid"] != null)
            {
                if (id == null || pid == null) return "参数不足 <a href='/film/comments'>返回</a>";
                admin.delComment(pid,id);
                ViewBag.admin = Session["aid"];
                return "success <a href='/Comment/comments'>返回</a>";
            }
            else
                return "权限不足 <a href='/Comment/comments'>返回</a>";
        }
    }
}