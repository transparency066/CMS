using MovieBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieModel;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class UserCenterController : Controller
    {
        UserCenter uc = new UserCenter();
        //默认页面
        public ActionResult Index()
        {
            var uid = Session["uid"];
            if (uid == null)
            {
                return RedirectToRoute(new { controller = "account", action = "login" });//重定向
            }

            var entity = uc.GetUserInfoByUID(uid.ToString());
            if (entity == null)
            {
                entity = new 用户();
            }
            return View(entity);
        }

        //更新用户页面
        public ActionResult Update(用户 dto)
        {
            var result = false;

            result = uc.UpdateUserInfo(dto.账号ID, dto.昵称, dto.电话, dto.性别) > 0;

            return RedirectToRoute(new { controller = "UserCenter", action = "INdex" });//重定向
        }

        public ActionResult HistoryList()
        {
            var uid = Session["uid"];
            if (uid == null)
            {
                return RedirectToRoute(new { controller = "account", action = "login" });//重定向
            }
            var list = uc.GetTickList(uid.ToString());
            if (list == null)
            {
                list = new List<Ticket>();
            }
            ViewBag.List = list;

            return View();   
        }
    }
}