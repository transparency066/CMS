using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieBusinessLogic;
using Screening.Models;

namespace Screening.Controllers
{
    public class ModifyController : Controller
    {
        // GET: Modify
        public ActionResult Index()
        {
            if (ControllerContext.RouteData.GetRequiredString("id") == null) return HttpNotFound();
            string now_screening_id = ControllerContext.RouteData.GetRequiredString("id");
            MovieBusinessLogic.ScreeningManager sc = new ScreeningManager();

            var HallInfo = new ScreeningDataModel()
            {
                screening_id = sc.GetScreeningsById(now_screening_id).screening_id,
                hall_id = sc.GetScreeningsById(now_screening_id).hall_id,
                film_id = sc.GetScreeningsById(now_screening_id).film_id,
                start_time = sc.GetScreeningsById(now_screening_id).start_time,
            };
            return View(HallInfo);
        }



       [HttpPost]
        public ActionResult Modified(ScreeningDataModel newData)
        {
            MovieBusinessLogic.ScreeningManager sc_ma = new MovieBusinessLogic.ScreeningManager();
            string screening_id = newData.screening_id;
            string film_id = newData.film_id;
            string hall_id = newData.hall_id;
            DateTime start_time = newData.start_time;
            sc_ma.ModifyScreeningInfoById(screening_id, film_id, hall_id, start_time);
            return Redirect("/Screening");
            
        }
    }
}