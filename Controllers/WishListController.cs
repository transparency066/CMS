using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult Index()
        {
            string uid=Session["uid"].ToString();
            //string uid = "1000000000";
            if (Session["has_login"] == null)
            {
                if (Session["ReturnToWishList"] == null) Session["ReturnToWishList"] = "true";
                else Session["ReturnToWishList"] = null;
                return Redirect("/Login");
            }
            else
            {
                Session.Remove("ReturnToWishList");
                MovieBusinessLogic.User user = new MovieBusinessLogic.User();
                var wish_list = user.getWishModels(uid).Select(wish_info => new WishList()
                {
                    movieID = wish_info.movieID,
                    movieName = wish_info.movieName,
                    wishTime = wish_info.wishTime,
                    movieImgPath = wish_info.movieImgPath,
                    upTime = wish_info.upTime,
                    downTime = wish_info.downTime,
                    movieDetail = wish_info.movieDetail,
                }).ToList();
                var resView = new WishList()
                {
                    uid=uid,
                    wishLists=wish_list,
                };
                return View(resView);
            }
        }

        //添加影片至收藏夹
        [HttpPost]
        public JsonResult AddWish(string addMovieID)
        {
            Session.Remove("ReturnToWishList");
            string uid = Session["uid"].ToString();
            //string uid = "1000000000";
            MovieBusinessLogic.User user = new MovieBusinessLogic.User();
            DateTime nowtime = DateTime.Now;
            if (user.addWishModels(uid,addMovieID,nowtime)) { }
            else
            {
                user.deleteWishModels(uid, addMovieID);
            }
            return Json("收藏夹更新成功");
        }

        //收藏夹删除影片
        [HttpPost]
        public ActionResult Index(string DeleteWishId)
        {
            Session.Remove("ReturnToWishList");
            string uid = Session["uid"].ToString();
            //string uid = "1000000000";
            string movieID = DeleteWishId;
            MovieBusinessLogic.User user = new MovieBusinessLogic.User();
            user.deleteWishModels(uid, movieID);
            var wish_list = user.getWishModels(uid).Select(wish_info => new WishList()
            {
                movieID = wish_info.movieID,
                movieName = wish_info.movieName,
                wishTime = wish_info.wishTime,
                movieImgPath=wish_info.movieImgPath,
                upTime=wish_info.upTime,
                downTime=wish_info.downTime,
                movieDetail=wish_info.movieDetail,
            }).ToList();
            var resView = new WishList()
            {
                uid=uid,
                wishLists=wish_list,
            };
            return View(resView);
        }
    }
}