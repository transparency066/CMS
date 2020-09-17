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
        User user = new User();
        //默认页面
        public ActionResult Index()
        {
            var uid = Session["uid"];
            if (uid == null)
            {
                if (Session["ReturnToUserCenter"] == null) Session["ReturnToUserCenter"] = "true";
                else Session["ReturnToUserCenter"] = null;
                return RedirectToAction("Login", "Account");
            }
            Session.Remove("ReturnToUserCenter");
            var entity = uc.GetUserInfoByUID(uid.ToString());
            if (entity == null)
            {
                entity = new Users();
            }
            return View(entity);
        }

        //更新用户页面
        public ActionResult Update(Users dto)
        {
            int result;
            if (dto.Phone.Length != 11)
            {
                Session["UserCenterPhoneLength"] = 1;
                return RedirectToAction("Index");
            }
            result = uc.UpdateUserInfo(dto.UserID, dto.NickName, dto.Phone, dto.Sex);
            Session["UserCenterResult"] = 1;
            return RedirectToAction("Index");
        }

        public ActionResult HistoryList()
        {
            var uid = Session["uid"];
            if (uid == null)
            {
                if (Session["ReturnToHistoryList"] == null) Session["ReturnToHistoryList"] = "true";
                else Session["ReturnToHistoryList"] = null;
                return RedirectToAction("Login", "Account");
            }
            Session.Remove("ReturnToHistoryList");
            var list = uc.GetTickList(uid.ToString());
            if (list == null)
            {
                list = new List<Ticket>();
            }
            ViewBag.List = list;

            return View();
        }
        public void Rate(String id, String s)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                HttpContext.Response.Write("<script type =\"text/javascript\">alert('无此id信息');window.history.back();</script>");
                HttpContext.Response.End();
                return;
            }

            MovieModel.Tickets yp = MovieBusinessLogic.Tickets.GetModel(" and 影票ID='" + id + "' ");
            if (yp == null)
            {
                HttpContext.Response.Write("<script type =\"text/javascript\">alert('无此id信息');window.history.back();</script>");
                HttpContext.Response.End(); return;
            }

            if (yp.影票状态 != 0 && yp.影票状态 != 1)
            {
                HttpContext.Response.Write("<script type =\"text/javascript\">alert('影票已经使用或超期');window.history.back();</script>");
                HttpContext.Response.End();
                return;
            }
            // 验证是否登录 应替换为相应方法
            if (Session["uid"] == null)
            {
                HttpContext.Response.Write("<script type =\"text/javascript\">alert('请先登录');window.location='/Account/login';</script>");
                HttpContext.Response.End();
                return;
            }
            string uid = Session["uid"].ToString();
            var temp = user.Rate(id, s, uid);
            HttpContext.Response.Write("<script type =\"text/javascript\">alert('评分成功');window.location = '/UserCenter/HistoryList';</script>");
            HttpContext.Response.End();
            return;
        }
    }
}