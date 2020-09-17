using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class ViewMessageController : Controller
    {
        //默认页面
        public ActionResult Index()
        {
            //string uid = "1000000000";
            if (Session["uid"] == null)
            {
                if (Session["ReturnToViewMessage"] == null) Session["ReturnToViewMessage"] = "true";
                else Session["ReturnToViewMessage"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string uid = Session["uid"].ToString();
                Session.Remove("ReturnToViewMessage");
                MovieBusinessLogic.User user = new MovieBusinessLogic.User();
                var messageList = user.getMessages(uid).Select(message => new Message()
                {
                    AdminID = message.AdminID,
                    ReplyTime = message.ReplyTime,
                    Text = message.Text,
                    ComplaintTime = message.ComplaintTime,
                    FeedBackText=message.FeedBackText,
                    
                }).ToList();
                var resView = new Message()
                {
                    UID = uid,
                    messages = messageList,
                };
                return View(resView);
            }
        }
    }
}