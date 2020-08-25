using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieModel;
using MovieWeb.Models;

namespace 影院管理系统.Controllers
{
    public class UserComplaintController : Controller
    {
        // GET: UserComplaint
        public ActionResult Index()
        {
            MovieBusinessLogic.Manager manager = new MovieBusinessLogic.Manager();
            var complaint_list = manager.UserComplaint().Select(complaint=>new Complaint()
            {
                UID=complaint.UID,
                ComplaintText=complaint.Complaint,
                ComplaintTime=complaint.ComplaintTime
            }).ToList();
            var compView = new Complaint()
            {
                Complaints = complaint_list,
            };
            return View(compView);
        }
    }
}