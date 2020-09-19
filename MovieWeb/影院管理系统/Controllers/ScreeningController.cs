using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class ScreeningController : Controller
    {
        // GET: Screening
        public ActionResult Index()
        {
            MovieBusinessLogic.Admin sc_ma = new MovieBusinessLogic.Admin();
            var screening_info = sc_ma.GetAllScreenings().Select(s_l => new ScreeningDataModel()
            {
                screening_id = s_l.screening_id,
                film_id = s_l.film_id,
                start_time = s_l.start_time,
                hall_id = s_l.hall_id,
            }).ToList();
            var screening_list = new ScreeningList()
            {
                ScreeningDataList = screening_info,
            };

            return View(screening_list);
        }

        public ActionResult Modify()
        {
            if (ControllerContext.RouteData.GetRequiredString("id") == null) return HttpNotFound();
            string now_screening_id = ControllerContext.RouteData.GetRequiredString("id");
            MovieBusinessLogic.Admin sc = new MovieBusinessLogic.Admin();

            var HallInfo = new ScreeningDataModel()
            {
                screening_id = sc.GetScreeningsById(now_screening_id).screening_id,
                hall_id = sc.GetScreeningsById(now_screening_id).hall_id,
                film_id = sc.GetScreeningsById(now_screening_id).film_id,
                start_time = sc.GetScreeningsById(now_screening_id).start_time,
            };
            return View(HallInfo);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ScreeningDataModel newData)
        {
            if (ModelState.IsValid)
            {
                if (newData.start_time < DateTime.Now)
                {
                    ViewBag.error = "请输入有效时间";
                    return View();
                }
                MovieBusinessLogic.Admin sc_ma = new MovieBusinessLogic.Admin();
                if (!sc_ma.CheckHallUsed(newData.hall_id, newData.start_time))
                {
                    ViewBag.error = "该时间段已被占用";
                    return View();
                }
                if (!sc_ma.CheckScreening(newData.screening_id))
                {
                    ViewBag.Screening_error = "该场次ID已经存在";
                    return View();
                }
                string screening_id = newData.screening_id;
                string film_id = newData.film_id;
                string hall_id = newData.hall_id;
                DateTime start_time = newData.start_time;
                sc_ma.CreateScreeningInfoById(screening_id, film_id, hall_id, start_time);
                return RedirectToAction("Index");
            }
            else return View();
        }

        [HttpPost]
        public ActionResult Modify(ScreeningDataModel newData)
        {
            if (ModelState.IsValid)
            {
                if (newData.start_time < DateTime.Now)
                {
                    ViewBag.error = "请输入有效时间";
                    return View();
                }

                MovieBusinessLogic.Admin sc_ma = new MovieBusinessLogic.Admin();
                if (!sc_ma.CheckHallUsed(newData.hall_id, newData.start_time))
                {
                    ViewBag.error = "该时间段已被占用";
                    return View();
                }


                string screening_id = newData.screening_id;
                string film_id = newData.film_id;
                string hall_id = newData.hall_id;
                DateTime start_time = newData.start_time;
                sc_ma.ModifyScreeningInfoById(screening_id, film_id, hall_id, start_time);
                return Redirect("/Screening");
            }
            else return View();

        }

        [HttpPost]
        public ActionResult Index(string screening_id)
        {
            MovieBusinessLogic.Admin sc_ma = new MovieBusinessLogic.Admin();
            sc_ma.DeleteScreeningInfoById(screening_id);
            var screening_info = sc_ma.GetAllScreenings().Select(s_l => new ScreeningDataModel()
            {
                screening_id = s_l.screening_id,
                film_id = s_l.film_id,
                start_time = s_l.start_time,
                hall_id = s_l.hall_id,
            }).ToList();
            var screening_list = new ScreeningList()
            {
                ScreeningDataList = screening_info,
            };




            return View(screening_list);
        }
    }
}