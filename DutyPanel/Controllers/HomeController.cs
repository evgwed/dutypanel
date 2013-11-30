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
        public ActionResult CreatStatement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatStatement(DutyPanel.Models.InternetStatement statement)
        {
            statement.IpAdress = Request.UserHostAddress;
            statement.InfoBrowser = Request.Browser.Browser;
            db.Statements.Add(statement);
            db.SaveChanges();
            return RedirectToAction("DetailsStatement", new { id = statement.NumberStatement });
        }
        public ActionResult DetailsStatement(int id)
        {
            return View(db.InternetStatements.Find(id));
        }
    }
}
