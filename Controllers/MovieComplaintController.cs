using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieBusinessLogic;
using MovieModel;

namespace MovieWeb.Controllers
{
    public class MovieComplaintController : Controller
    {
        // GET: MovieComplaint
        Manager_nyx manager = new Manager_nyx();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string uid = System.Web.HttpContext.Current.Session["uid"].ToString();
            var complaints = manager.GetAllComplaints(uid).Select(complaint => new Complaint()
            {
                ComplaintTime = complaint.ComplaintTime,
                userID = complaint.userID,
                ComplaintContent = complaint.ComplaintContent,
                isReply = complaint.isReply
            }).ToList();
            return View(complaints);
        }
        [HttpPost]
        public ActionResult Index(FormCollection txt)
        {
            string content = txt["Complaint"];
            DateTime time = DateTime.Now;
            int reply = 0;
            int complainCode = 0;
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string uid = System.Web.HttpContext.Current.Session["uid"].ToString();
                if (manager.Complain(time, uid, content, reply) > 0)
                {
                    complainCode = 1;
                    ViewBag.complainCode = 1;
                }
                else
                {
                    ViewBag.complainCode = 0;
                }
                return Index();
            }
        }
    }
}