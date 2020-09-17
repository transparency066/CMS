using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MovieWeb.Controllers
{
    public class TicketsController : Controller
    {
        public ActionResult Index()
        {
            var uid = Session["uid"];
            if (uid == null)
            {
                return RedirectToRoute(new { controller = "account", action = "login" });//重定向
            }
            return View();
        }

        public ActionResult Round(string id)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                MovieModel.Movie movie = MovieBusinessLogic.Movie.GetModel(" and 影片ID='" + id + "' ");
                return View(movie);
            }
            return View();
        }

        public ActionResult XSeats(string id, string yt, string cc)
        {
            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(yt) && !string.IsNullOrWhiteSpace(cc))
            {
                MovieModel.Movie movie = MovieBusinessLogic.Movie.GetModel(" and 影片ID='" + id + "' ");
                MovieModel.Hall ytModel = MovieBusinessLogic.Hall.GetModel(" and 放映厅id='" + yt + "' ");
                ViewBag.YTModel = ytModel;
                ViewBag.cc = cc;
                return View(movie);
            }
            return View();
        }
        [HttpPost]
        public JsonResult PayAction(string cc, string zw)
        {
            if (!string.IsNullOrWhiteSpace(cc) && !string.IsNullOrWhiteSpace(zw))
            {
                List<int> zwList = JsonConvert.DeserializeObject<List<int>>(zw);

                if (zwList != null && zwList.Count > 0)
                {
                    int i = 0;
                    foreach (int a in zwList)
                    {
                        var uid = Session["uid"];
                        MovieModel.Tickets yp = new MovieModel.Tickets();
                        yp.影票ID = DateTime.Now.ToString("yyMMddHHmmssfff");
                        yp.拥有者账号ID = uid.ToString();//后期自己加入用户身份信息
                        yp.场次ID = cc;
                        yp.座位ID = a;
                        yp.购票时间 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //0,未领取，1，已经领取，2，已经使用 3，退票 后期根据自己需求修改
                        yp.影票状态 = 0;
                        i += MovieBusinessLogic.Tickets.Add(yp);
                    }
                    return Json(new { state = true, msg = "成功购入电影票" + i + "张！" },
         JsonRequestBehavior.AllowGet);
                }

                return Json(new { state = false, msg = "未获得座次信息" },
           JsonRequestBehavior.AllowGet);
            }
            return Json(new { state = false, msg = "未获得相关信息" },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult Refund()
        {
            var uid = Session["uid"];
            if (uid == null)
            {
                return RedirectToRoute(new { controller = "account", action = "login" });//重定向
            }
            else
            {
                uid = uid.ToString();
            }
            string sqlString = " and 拥有者账号ID='" + uid + "'  and (影票状态=0 or 影票状态=1) and 场次ID in (select 场次ID from 场次 where `开映时间`>date_format(now(), '%Y-%d-%m %H:%i:%s')) ";
            List<MovieModel.Tickets> list = MovieBusinessLogic.Tickets.GetList(sqlString);
            return View(list);
        }
         
        public void RefundAction(string id)
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

            yp.影票状态 = 3;
            if (MovieBusinessLogic.Tickets.Update(yp) > 0)
            {
                HttpContext.Response.Write("<script type =\"text/javascript\">alert('退票成功');window.history.back();</script>");
                HttpContext.Response.End(); return;
            }
            HttpContext.Response.Write("<script type =\"text/javascript\">alert('退票失败');window.history.back();</script>");
            HttpContext.Response.End(); return;
        }
    }
}