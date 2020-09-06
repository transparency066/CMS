using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Screening.Models;

namespace Screening.Controllers
{
    public class SeatsListController : Controller
    {
        // GET: SeatsList
        public ActionResult Index()
        {
            if (ControllerContext.RouteData.GetRequiredString("id") == null) return HttpNotFound();
            string now_screening_id = ControllerContext.RouteData.GetRequiredString("id");


            MovieBusinessLogic.SeatsManager sc_ma = new MovieBusinessLogic.SeatsManager();


            var screening_info = sc_ma.GetSeatsByScreening_id(now_screening_id).Select(s_l => new MovieModel.SeatsModel()
            {
                screening_id = s_l.screening_id,
                x = s_l.x,
                y = s_l.y,
                available = s_l.available,
                used = s_l.used,

            }).ToList();
            var screening_list = new SeatsListModel()
            {
                SeatsList = screening_info,
            };
            return View(screening_list);
        }
    }
}