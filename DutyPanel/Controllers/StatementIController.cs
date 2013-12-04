using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DutyPanel.Models;

namespace DutyPanel.Controllers
{
    public class StatementIController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /StatementI/

        public ActionResult Index()
        {
            return View(db.InternetStatements.ToList());
        }

        //
        // GET: /StatementI/Details/5

        public ActionResult Details(int id = 0)
        {
            InternetStatement internetstatement = db.InternetStatements.Find(id);
            if (internetstatement == null)
            {
                return HttpNotFound();
            }
            return View(internetstatement);
        }

        //
        // GET: /StatementI/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StatementI/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InternetStatement internetstatement)
        {
            internetstatement.InfoBrowser = Request.Browser.Browser;
            internetstatement.IpAdress = Request.UserHostAddress;
            db.InternetStatements.Add(internetstatement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /StatementI/Edit/5

        public ActionResult Edit(int id = 0)
        {
            InternetStatement internetstatement = db.InternetStatements.Find(id);
            if (internetstatement == null)
            {
                return HttpNotFound();
            }
            return View(internetstatement);
        }

        //
        // POST: /StatementI/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InternetStatement internetstatement)
        {
                db.Entry(internetstatement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        //
        // GET: /StatementI/Delete/5

        public ActionResult Delete(int id = 0)
        {
            InternetStatement internetstatement = db.InternetStatements.Find(id);
            if (internetstatement == null)
            {
                return HttpNotFound();
            }
            return View(internetstatement);
        }

        //
        // POST: /StatementI/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InternetStatement internetstatement = db.InternetStatements.Find(id);
            db.InternetStatements.Remove(internetstatement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}