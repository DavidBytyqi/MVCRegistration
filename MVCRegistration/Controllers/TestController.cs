using MVCRegistration.Models;
using System;

using System.Collections.Generic;

using System.Globalization;

using System.Linq;

using System.Threading;

using System.Web;

using System.Web.Mvc;


namespace MVCTutorial.Controllers

{

    public class TestController : Controller

    {


        public ActionResult Index()
        {

            return View();

        }

        public ActionResult Registration()
        {

            return View();

        }

        [HttpPost]

        public JsonResult RegisterUser(RegistrationViewModel model)
        {

            MVCDatabaseEntities db = new MVCDatabaseEntities();


            SiteUser siteUser = new SiteUser();

            siteUser.UserName = model.UserName;

            siteUser.EmailId = model.EmailId;

            siteUser.Password = model.Password;

            siteUser.Address = model.Address;

            siteUser.RoleId = 3;

            db.SiteUsers.Add(siteUser);

            db.SaveChanges();


            return Json("Success", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Login()
        {
            return View();
        }
         public JsonResult LoginUser(RegistrationViewModel model) {

            MVCDatabaseEntities db = new MVCDatabaseEntities();

            SiteUser user = db.SiteUsers.SingleOrDefault(x => x.EmailId == model.EmailId && x.Password == model.Password);
        
            string result = "fail";
            if (user != null)
            {

                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;
                if (user.RoleId == 3)
                {
                    result = "GeneralUser";

                }
                else if (user.RoleId == 1)
                {
                    result = "Admin";

                }

            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");

        }

    }

}