using Screening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Screening.Controllers
{
    public class ScreeningController : Controller
    {
        // GET: Screening
        public ActionResult Index()
        {
            MovieBusinessLogic.ScreeningManager sc_ma = new MovieBusinessLogic.ScreeningManager();
            var screening_info = sc_ma.GetAllScreenings().Select(s_l => new ScreeningDataModel()
            {
                screening_id = s_l.screening_id,
                film_id=s_l.film_id,
                start_time=s_l.start_time,
                hall_id=s_l.hall_id,
            }).ToList() ;
            var screening_list = new ScreeningList()
            {
                ScreeningDataList=screening_info,
            };

            return View(screening_list);
        }

        [HttpPost]
        public ActionResult Index(string screening_id)
        {
            MovieBusinessLogic.ScreeningManager sc_ma = new MovieBusinessLogic.ScreeningManager();
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