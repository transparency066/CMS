using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieModel;
using MovieBusinessLogic;
using System.Web.UI.WebControls;

namespace MovieWeb.Controllers
{
    public class SendMessageController : Controller
    {
        // GET: MessageSend
        Admin admin = new Admin();
        public ActionResult Index()
        {
            return View();
        }

        //发送消息
        public JsonResult SendMess()
        {
            //if (Request.QueryString["UID"] != null)
            //{
                Message message = new Message()
                {
                    UID = Request.QueryString["UID"],
                    ComplaintTime = DateTime.Parse(Request.QueryString["ComplaintTime"]),
                    AdminID = Request.QueryString["AdminID"],
                    ReplyTime = DateTime.Parse(Request.QueryString["Replytime"]),
                    Text = Request.QueryString["ReplyMessage"]
                };
                int flag;
                flag = admin.SendMess(message.UID,message.ComplaintTime,message.AdminID,message.ReplyTime,message.Text);
                if (flag == 1)
                { message.flag = 1; }
                else message.flag = 0;
                return Json(message, JsonRequestBehavior.AllowGet);
            //}
            //else return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}