using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MovieWeb.Models;
using MovieBusinessLogic;

namespace MovieWeb.Controllers
{
    public class FilmController : Controller
    {

        User user = new User();

        public ActionResult Index(String id)
        {
            id = id == null ? "0" : id;
            ViewBag.mark = 0;
            if (id != null)
            {
                ViewBag.Data = user.getFilmById(id);
                // 如果登录了，去获取该账户是否评分了
                string mark = user.getMark(id);
                //ViewBag.mark = 8;
                ViewBag.mark = (mark == "") ? 0 : float.Parse(mark);
            }

            Session["movieID"] = id;//用于寻找对应影片的用户评论

            string type = ViewBag.Data["类型"];
            string InitialId = ViewBag.Data["影片ID"];
            string[] InitialType = type.Split(' ');
            var recommendMovieList = user.GetRecommendMovies(InitialType, InitialId).Select(movie => new RecommendMovie
            {
                ID = movie.ID,
                Name = movie.Name,
                Type = movie.Type,
                Url = movie.Url
            }).ToList();
            if(Session["uid"]==null)
            {
                var resView = new RecommendMovie()
                {
                    recommendMovies = recommendMovieList,
                    TEXT = "加入收藏",
                    CLASS = "wait_add",
                };
                return View(resView);
            }
            else
            {
                if (!user.isInWishList(Session["uid"].ToString(), Session["movieID"].ToString()))
                {
                    var resView = new RecommendMovie()
                    {
                        recommendMovies = recommendMovieList,
                        TEXT = "加入收藏",
                        CLASS = "wait_add",
                    };
                    return View(resView);
                }
                else
                {
                    var resView = new RecommendMovie()
                    {
                        recommendMovies = recommendMovieList,
                        TEXT = "取消收藏",
                        CLASS = "wait_delete",
                    };
                    return View(resView);
                }
            }   
        }

        //详情页AddWish
        [HttpPost]
        public ActionResult AddWish(string addMovieID)
        {
            Session.Remove("ReturnToWishList");
            if (Session["uid"] == null) return Redirect("/Account");
            string uid = Session["uid"].ToString();
            MovieBusinessLogic.User user = new MovieBusinessLogic.User();
            DateTime nowtime = DateTime.Now;
            if (user.isInWishList(uid, addMovieID) == false)
            {
                var resView = new RecommendMovie()
                {
                    TEXT = "取消收藏",
                    CLASS = "wait_delete",
                };
                user.addWishModels(uid, addMovieID, nowtime);
                return PartialView("FilmPart1", resView);
            }
            else
            {
                var resView = new RecommendMovie()
                {
                    TEXT = "加入收藏",
                    CLASS = "wait_add",
                };
                user.deleteWishModels(uid, addMovieID);
                return PartialView("FilmPart1", resView);
            }
        }

        public ActionResult Search(String key)
        {
            if (Session["uid"] == null) return Redirect("/Account/login");
            if (key == null) key = "1";
            var searchMovies = user.QueryFilm(key).Select(movie => new SearchMovie
            {
                ID = movie.ID,
                Name = movie.Name,
                Type = movie.Type,
                Score = movie.Score,
                Url = movie.Url
            }).ToList();
            var rankMovies = user.GetRankMovies().Select(movie => new RankMovie
            {
                ID = movie.ID,
                Name = movie.Name,
                Tickets = movie.Tickets,
                Url = movie.Url
            }).ToList();
            var searchPageMovies = new SearchPageMovies
            {
                searchMovies = searchMovies,
                rankMovies = rankMovies
            };
            return View(searchPageMovies);
        }
        public void Rate(String id, String s)
        {
            // 验证是否登录 应替换为相应方法
            if (Session["uid"] == null) return;
            string uid = Session["uid"].ToString();
            user.Rate(id, s, uid);
            return;
        }

    }
}