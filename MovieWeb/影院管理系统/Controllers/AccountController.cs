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
        public ActionResult Login(Account account)
        {
            if (account.UserName == null | account.PassWord == null) return View();
            if (account.UserName.Length != 10 || account.PassWord.Length > 20) return View();
            int LoginCode = user.Login(account.UserName, account.PassWord);
            if (LoginCode == -1)//登录失败
            {
                if (account.UserName != null && account.PassWord != null) ViewBag.LoginCode = 0;
                else ViewBag.LoginCode = -1;
                return View();
            }
            else if (LoginCode == 0)//用户登录成功
            {
                Session["uid"] = account.UserName;
                if (Session["ReturnToWishList"] != null) return RedirectToAction("Index", "WishList");
                else if (Session["ReturnToViewMessage"] != null) return RedirectToAction("Index", "ViewMessage");
                else if (Session["ReturnToSubmitComplaint"] != null) return RedirectToAction("Index", "SubmitComplaint");
                else return RedirectToAction("Index", "Home");
            }
            else//管理员登录成功
            {
                Session["aid"] = account.UserName;
                return RedirectToAction("Index", "UserInfo");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (account.UserName == null || account.PassWord == null || account.Name == null || account.PhoneNumber == null) return View();
            if (account.UserName.Length != 10 || account.PassWord.Length > 20||account.PhoneNumber.Length!=11) return View();
            int RegisterCode = user.Register(account.UserName, account.PassWord,account.Name,account.PhoneNumber,account.Sex);
            if (RegisterCode == 0)//注册失败
            {
                if (account.UserName != null && account.PassWord != null && account.Name != null && account.PhoneNumber != null) ViewBag.LoginCode = 0;
                else ViewBag.RegisterCode = -1;
                return View();
            }
            else
            {
                Session["uid"] = account.UserName;
                if (Session["ReturnToWishList"] != null) return RedirectToAction("Index", "WishList");
                else if (Session["ReturnToViewMessage"] != null) return RedirectToAction("Index", "ViewMessage");
                else if (Session["ReturnToSubmitComlaint"] != null) return RedirectToAction("Index", "SubmitComlaint");
                else return RedirectToAction("Index", "Home");
            }
        }
    }
}