using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class SeatsListController : Controller
    {


        // GET: SeatsList
        public ActionResult Index()
        {
            if (ControllerContext.RouteData.GetRequiredString("id") == null) return HttpNotFound();
            string now_screening_id = ControllerContext.RouteData.GetRequiredString("id");


            MovieBusinessLogic.Admin sc_ma = new MovieBusinessLogic.Admin();


            var screening_info = sc_ma.GetSeatsByHallId(now_screening_id).Select(s_l => new MovieModel.SeatsModel()
            {
                hall_id = s_l.hall_id,
                seats_id = s_l.seats_id,
                x = s_l.x,
                y = s_l.y,
                flag = s_l.flag

            }).ToList();
            var screening_list = new SeatsListModel()
            {
                SeatsList = screening_info,
            };
            return View(screening_list);
        }
    }


}