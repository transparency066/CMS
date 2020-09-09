using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;
using MovieModel;
using MovieBusinessLogic;

namespace MovieWeb.Controllers
{
    public class ViewComplaintController : Controller
    {
        // GET: UserComplaint
        //查看反馈页面
        public ActionResult Index()
        {
            Admin admin = new Admin();
            var complaint_list = admin.UserComplaint().Select(complaint => new MovieWeb.Models.Complaint()
            {
                UID = complaint.UID,
                ComplaintText = complaint.ComplaintText,
                ComplaintTime = complaint.ComplaintTime,
                ReplyFlag = complaint.ReplyFlag
            }).ToList();
            var compView = new MovieWeb.Models.Complaint()
            {
                Complaints = complaint_list,
            };
            return View(compView);
        }
    }
}