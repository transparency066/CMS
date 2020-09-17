using MovieBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class HallController : Controller
    {
        // GET: Hall
        public ActionResult Index()
        {

            if (ControllerContext.RouteData.GetRequiredString("id") == null) return HttpNotFound();
            string now_screening_id = ControllerContext.RouteData.GetRequiredString("id");
            MovieBusinessLogic.Admin hall_manager = new Admin();

            var HallInfo = new HallDataModel()
            {

                hall_id = hall_manager.GetHallById(now_screening_id).hall_id,
                total = hall_manager.GetHallById(now_screening_id).total,
                location = hall_manager.GetHallById(now_screening_id).location,
            };
            return View(HallInfo);
        }
    }
}