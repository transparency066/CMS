using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieModel;
using MovieBusinessLogic;
using System.Web.UI.WebControls;

namespace 影院管理系统.Controllers
{
    public class MessageSendController : Controller
    {
        // GET: MessageSend
        Manager manager = new Manager();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SendMess()
        {

            if (Request.QueryString["UID"] != null)
            {
                MessageSend message = new MessageSend()
                {
                    UID = Request.QueryString["UID"],
                    ComplaintTime = DateTime.Parse(Request.QueryString["ComplaintTime"]),
                    AdminID = Request.QueryString["AdminID"],
                    ReplyTime = DateTime.Parse(Request.QueryString["Replytime"]),
                    Message = Request.QueryString["ReplyMessage"]
                };
                int flag;
                flag = manager.SendMess(message);
                if (flag == 1)
                { message.flag = 1; }
                else message.flag = 0;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}