using MovieBusinessLogic;
using MovieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 影院管理系统.Models;

namespace 影院管理系统.Controllers
{
    public class UserAdminController : Controller
    {
        Manager manager = new Manager();
        // GET: UserAdmin
        public ActionResult Index()
        {
            //manager.InsertUser();
            return View();
        }

        public ActionResult Insert()
        {
            return View();
        }
        
        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }
        
        public JsonResult SendNewUser()
        {
           if( Request.QueryString["Account"]!=null) 
            {
                int sex;
                if (Request.QueryString["Sex"] == "1") sex= 1;
                else sex = 0;
                Userinf userinf = new Userinf()
                {
                    Account = Request.QueryString["Account"],
                    Password = Request.QueryString["Password"],
                    Name = Request.QueryString["Name"],
                    Phone = Request.QueryString["Phone"],
                    Sex = sex
            };
                int flag;
                flag=manager.InsertUser(userinf);
                if (flag == 1)
                { userinf.flag = 1; }
                else userinf.flag = 0;
                return Json(userinf, JsonRequestBehavior.AllowGet);
            }
            else return Json(false, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SearchtheUser()
        {
            Userinf userinf = new Userinf()
            {
                Account = Request.QueryString["Account"]
            };
            Userinf userinf1= manager.SearchUser(userinf);
            return Json(userinf1, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult DeletetheUser()
        {
            Userinf userinf = new Userinf()
            {
                Account = Request.QueryString["Account"]
            };
            int flag = manager.DeleteUser(userinf);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatetheUser()
        {
            int sex;
            if (Request.QueryString["Sex"] == "1") sex = 1;
            else sex = 0;
            Userinf userinf = new Userinf()
            {
                Account = Request.QueryString["Account"],
                Password = Request.QueryString["Password"],
                Name = Request.QueryString["Name"],
                Phone = Request.QueryString["Phone"],
                Sex = sex
            };
            string currentacc = Request.QueryString["current"];
            int flag = manager.UpdateUser(userinf, currentacc);
            userinf.flag = flag;
            return Json(userinf, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UAllinf()
        {
            var posts = manager.GetUserinfs().Select(post => new AllUViewModel()
            {
                Account=post.Account,
                Name=post.Name,
              //  Password=post.Password,
                Phone=post.Phone,
                Sex=post.Sex

            }).ToList();
            var postsList = new AllUListViewModel()
            {
                Count = posts.Count,
                AllUs = posts
            };
            return View(postsList);
        }
    }
}