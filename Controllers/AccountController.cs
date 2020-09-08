using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class AccountController : Controller
    {
        private MovieBusinessLogic.User user = new MovieBusinessLogic.User();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Account account)
        {
            if (account.UserName.Length != 10 || account.PassWord.Length > 20) return View();
            int LoginCode = user.Login(account.UserName,account.PassWord);
            if(LoginCode == 0)//登录失败
            {
                ViewBag.LoginCode = 0;
                return View();
            }
            else if(LoginCode == 1)//用户登录成功
            {
                Session["uid"] = account.UserName;
                if (Session["ReturnToWishList"] != null) return Redirect("/WishList");
                else if (Session["ReturnToM2UInfo"] != null) return Redirect("/M2UInfo");
                else return RedirectToAction("Index", "Home");
            }
            else//管理员登录成功
            {
                Session["aid"] = account.UserName;
                return RedirectToAction("About", "Home");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.Account account)
        {
            if (account.UserName.Length != 10 || account.PassWord.Length > 20) return View();
            int RegisterCode = user.Register(account.UserName, account.PassWord);
            if(RegisterCode == 0)//注册失败
            {
                ViewBag.RegisterCode = 0;
                return View();
            }
            else
            {
                Session["uid"] = account.UserName;
                if (Session["ReturnToWishList"] != null) return Redirect("/WishList");
                else if (Session["ReturnToM2UInfo"] != null) return Redirect("/M2UInfo");
                else return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult test(Models.Account account)
        {
            return View();
        }
    }
}