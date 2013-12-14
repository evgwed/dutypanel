using DutyPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace DutyPanel.Controllers
{
    public class HomeController : Controller
    {
        public DataContext db = new DataContext();
        //
        // GET: /Home/

        public ActionResult Index(bool res = false)
        {
            if (res)
                ViewData["InfoText"] = "Ваше заявление создано, скоро с вами свяжется дежурный для уточнения информации.";
            ViewBag.Users = db.Users;
            return View();
        }
        public ActionResult TMP_Enter(string role)
        {
            switch (role) { 
                case "admin":
                    Session["User"] = db.AdminUsers.First();
                    break;
                case "duty":
                    Session["User"] = db.Dutys.First();
                    break;
                case "driver":
                    Session["User"] = db.Drivers.First();
                    break;
                case "ow":
                    Session["User"] = db.OperativeWorkers.First();
                    break;
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}
