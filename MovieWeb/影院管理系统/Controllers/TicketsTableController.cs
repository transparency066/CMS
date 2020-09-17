using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieWeb.Controllers
{
    public class TicketsTableController : Controller
    {
        // GET: TicketsTable
        private MovieBusinessLogic.Admin admin = new MovieBusinessLogic.Admin();
        public ActionResult TicketTable()
        {
                ViewBag.data = admin.queryTicket();
                ViewBag.admin = Session["aid"];
                return View();
        }
    }
}