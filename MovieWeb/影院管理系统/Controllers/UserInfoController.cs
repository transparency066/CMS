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
    public class UserInfoController : Controller
    {
        Admin admin = new Admin();
        //默认页面
        public ActionResult Index()
        {
            //manager.InsertUser();
            return View();
        }

        //添加用户页面
        public ActionResult Insert()
        {
            return View();
        }

        //删除用户页面
        public ActionResult Delete()
        {
            return View();
        }

        //更新用户页面
        public ActionResult Update()
        {
            return View();
        }

        //添加用户功能
        public JsonResult SendNewUser()
        {
            if (Request.QueryString["Account"] != null)
            {
                int sex;
                if (Request.QueryString["Sex"] == "1") sex = 1;
                else sex = 0;
                MovieModel.Account userinf = new MovieModel.Account()
                {
                    UserName = Request.QueryString["Account"],
                    PassWord = Request.QueryString["Password"],
                    Name = Request.QueryString["Name"],
                    PhoneNumber = Request.QueryString["Phone"],
                    Sex = sex
                };
                int flag;
                flag = admin.InsertUser(userinf.UserName, userinf.PassWord, userinf.Name, userinf.PhoneNumber, userinf.Sex);
                if (flag == 1)
                { userinf.flag = 1; }
                else userinf.flag = 0;
                return Json(userinf, JsonRequestBehavior.AllowGet);
            }
            else return Json(false, JsonRequestBehavior.AllowGet);

        }

        //搜索用户功能
        public JsonResult SearchtheUser()
        {
            MovieModel.Account userinf = new MovieModel.Account()
            {
                UserName = Request.QueryString["Account"]
            };
            MovieModel.Account userinf1 = admin.SearchUser(userinf.UserName);
            return Json(userinf1, JsonRequestBehavior.AllowGet);

        }

        //删除用户功能
        public JsonResult DeletetheUser()
        {
            MovieModel.Account userinf = new MovieModel.Account()
            {
                UserName = Request.QueryString["Account"]
            };
            int flag = admin.DeleteUser(userinf.UserName);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        //更新用户功能
        public JsonResult UpdatetheUser()
        {
            int sex;
            if (Request.QueryString["Sex"] == "1") sex = 1;
            else sex = 0;
            MovieModel.Account userinf = new MovieModel.Account()
            {
                UserName = Request.QueryString["Account"],
                PassWord = Request.QueryString["Password"],
                Name = Request.QueryString["Name"],
                PhoneNumber = Request.QueryString["Phone"],
                Sex = sex
            };
            string currentacc = Request.QueryString["current"];
            int flag = admin.UpdateUser(userinf.UserName,userinf.PassWord,userinf.Name,userinf.PhoneNumber,userinf.Sex,currentacc);
            userinf.flag = flag;
            return Json(userinf, JsonRequestBehavior.AllowGet);
        }

        //获取所有用户
        public ActionResult UAllinf()
        {
            var posts = admin.GetUserinfs().Select(post => new MovieWeb.Models.Account()
            {
                UserName = post.UserName,
                Name = post.Name,
                PassWord = post.PassWord,
                PhoneNumber = post.PhoneNumber,
                Sex = post.Sex

            }).ToList();
            var postsList = new AllUListViewModel()
            {
                Count = posts.Count,
                AllUs = posts
            };
            return View(postsList);
        }
    }
}