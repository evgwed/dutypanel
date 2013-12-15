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
    public class StatementWController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /StatementW/

        public ActionResult Index()
        {
            return View(db.WrittenStatements.ToList());
        }

        //
        // GET: /StatementW/Details/5

        public ActionResult Details(int id = 0)
        {
            WrittenStatement writtenstatement = db.WrittenStatements.Find(id);
            if (writtenstatement == null)
            {
                return HttpNotFound();
            }
            return View(writtenstatement);
        }

        //
        // GET: /StatementW/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StatementW/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WrittenStatement writtenstatement)
        {
            writtenstatement.Status = StatementStatus.Received;
            writtenstatement.DateStatment = DateTime.Now;
            writtenstatement.Duty = db.Dutys.Find((Session["User"] as Duty).Id);
            db.Statements.Add(writtenstatement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /StatementW/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WrittenStatement writtenstatement = db.WrittenStatements.Find(id);
            if (writtenstatement == null)
            {
                return HttpNotFound();
            }
            return View(writtenstatement);
        }

        //
        // POST: /StatementW/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WrittenStatement writtenstatement)
        {
            writtenstatement.Status = (StatementStatus)Convert.ToInt32(Request.Form["Status"]);
            writtenstatement.Duty = db.Dutys.Find((Session["User"] as Duty).Id);
            if (ModelState.IsValid)
            {
                db.Entry(writtenstatement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(writtenstatement);
        }

        //
        // GET: /StatementW/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WrittenStatement writtenstatement = db.WrittenStatements.Find(id);
            if (writtenstatement == null)
            {
                return HttpNotFound();
            }
            return View(writtenstatement);
        }

        //
        // POST: /StatementW/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WrittenStatement writtenstatement = db.WrittenStatements.Find(id);
            db.WrittenStatements.Remove(writtenstatement);
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