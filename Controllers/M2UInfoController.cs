using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class M2UInfoController : Controller
    {
        // GET: M2UInfo
        public ActionResult Index()
        {
            string uid=Session["uid"].ToString();
            //string uid = "1000000000";
            if (Session["has_login"] == null)
            {
                if (Session["ReturnToM2UInfo"] == null) Session["ReturnToM2UInfo"] = "true";
                else Session["ReturnToM2UInfo"] = null;
                return Redirect("/Login");
            }
            else
            {
                Session.Remove("ReturnToM2UInfo");
                MovieBusinessLogic.User user = new MovieBusinessLogic.User();
                var m2u_list = user.getM2UModels(uid).Select(m2u_info => new M2UInfo()
                {
                    mid=m2u_info.mid,
                    M2Utime=m2u_info.M2Utime,
                    M2Utext=m2u_info.M2Utext,
                    U2Mtime=m2u_info.U2Mtime,
                }).ToList();
                var resView = new M2UInfo()
                {
                   uid=uid,
                   m2UInfos=m2u_list,              
                };
                return View(resView);
            }
        }
    }
}