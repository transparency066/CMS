using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 影院管理系统.Controllers
{
    public class TicketsTableController : Controller
    {
        // GET: TicketsTable
        private MovieBusinessLogic.Admin admin = new MovieBusinessLogic.Admin();
        public ActionResult TicketTable()
        {
            if (Session["aid"] != null)
            {
                ViewBag.data = admin.queryTicket();
                ViewBag.admin = Session["aid"];
                return View();
            }
            else
                throw new UnauthorizedAccessException("权限不足");
        }
    }
}