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

        public ActionResult Index()
        {
            ViewBag.Users = db.Users;
            return View();
        }
        
    }
}
