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
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection txt)
        {
            string content = txt["Complaint"];
            DateTime time = DateTime.Now;
            int reply = 0;
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string uid = System.Web.HttpContext.Current.Session["uid"].ToString();
                if (manager.Complain(time, uid, content, reply) > 0)
                {
                    Response.Write("提交成功,谢谢您的反馈！");
                }
                else
                {
                    Response.Write("提交失败！");

                }

                return View();
            }
        }
    }
}