using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Screening.Models;

namespace Screening.Controllers
{
    public class CreateController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ScreeningDataModel newData)
        {
            MovieBusinessLogic.ScreeningManager sc_ma = new MovieBusinessLogic.ScreeningManager();
            string screening_id = newData.screening_id;
            string film_id = newData.film_id;
            string hall_id = newData.hall_id;
            DateTime start_time = newData.start_time;
            sc_ma.CreateScreeningInfoById(screening_id, film_id, hall_id, start_time);
            return Redirect("/Screening");

        }
    }
}