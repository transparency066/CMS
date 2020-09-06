using MovieBusinessLogic;
using Screening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Screening.Controllers
{
    public class HallController : Controller
    {
        // GET: Hall
        public ActionResult Index()
        {
            if (ControllerContext.RouteData.GetRequiredString("id") == null) return HttpNotFound();
            string now_screening_id = ControllerContext.RouteData.GetRequiredString("id");
            MovieBusinessLogic.HallManager hall_manager = new HallManager();

            var HallInfo = new HallDataModel()
            {
                screening_id = hall_manager.GetHallById(now_screening_id).screening_id,
                hall_id=hall_manager.GetHallById(now_screening_id).hall_id,
                total= hall_manager.GetHallById(now_screening_id).total,
                left= hall_manager.GetHallById(now_screening_id).left,
            };
            return View(HallInfo);
        }
    }
}