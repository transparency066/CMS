using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieBusinessLogic;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class SubmitComplaintController : Controller
    {
        // GET: MovieComplaint
        User user = new User();

        //默认页面
        public ActionResult Index()
        {
            if (Session["uid"] == null)
            {
                if (Session["ReturnToSubmitComplaint"] == null) Session["ReturnToSubmitComplaint"] = "true";
                else Session["ReturnToSubmitComplaint"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string uid = System.Web.HttpContext.Current.Session["uid"].ToString();
                var complaints = user.GetAllComplaints(uid).Select(complaint => new Complaint()
                {
                    ComplaintTime = complaint.ComplaintTime,
                    UID = complaint.UID,
                    ComplaintText = complaint.ComplaintText,
                    ReplyFlag = complaint.ReplyFlag
                }).ToList();
                var comView = new Complaint
                {
                    Complaints = complaints
                };
                return View(comView);
            }
        }

        //提交反馈
        [HttpPost]
        public ActionResult PutIn(FormCollection txt)
        {
            string content = txt["Complaint"];
            DateTime time = DateTime.Now;
            int reply = 0;
            int complainCode = 0;
            //if (Session["uid"] == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{
            string uid = System.Web.HttpContext.Current.Session["uid"].ToString();
            if (user.Complain(time, uid, content, reply) > 0)
            {
                complainCode = 1;
                ViewBag.complainCode = 1;
            }
            else
            {
                ViewBag.complainCode = 0;
            }
            return Index();
            //}
        }
    }
}