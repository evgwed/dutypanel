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
    public class StatementOController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /StatementO/

        public ActionResult Index()
        {
            return View(db.OralStatements.ToList());
        }

        //
        // GET: /StatementO/Details/5

        public ActionResult Details(int id = 0)
        {
            OralStatement oralstatement = db.OralStatements.Find(id);
            if (oralstatement == null)
            {
                return HttpNotFound();
            }
            return View(oralstatement);
        }

        //
        // GET: /StatementO/Create

        public ActionResult Create()
        {
            if (Session["User"] is AdminUser)
            {
                ViewData["Duty"] = new SelectList(db.Dutys, "Id", "LastName");
            }
            return View();
        }

        //
        // POST: /StatementO/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OralStatement oralstatement)
        {
            if (Session["User"] is Duty)
            {
                oralstatement.Duty = db.Dutys.Find((Session["User"] as Duty).Id);
            }
            if (Session["User"] is AdminUser)
            {
                oralstatement.Duty = db.Dutys.Find(Convert.ToInt32(Request.Form["Duty"]));
            }
                db.OralStatements.Add(oralstatement);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        //
        // GET: /StatementO/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OralStatement oralstatement = db.OralStatements.Find(id);
            if (oralstatement == null)
            {
                return HttpNotFound();
            }
            return View(oralstatement);
        }

        //
        // POST: /StatementO/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OralStatement oralstatement)
        {
                oralstatement.Duty = db.Dutys.Find((Session["User"] as Duty).Id);
            if (ModelState.IsValid)
            {
                db.Entry(oralstatement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oralstatement);
        }

        //
        // GET: /StatementO/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OralStatement oralstatement = db.OralStatements.Find(id);
            if (oralstatement == null)
            {
                return HttpNotFound();
            }
            return View(oralstatement);
        }

        //
        // POST: /StatementO/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OralStatement oralstatement = db.OralStatements.Find(id);
            db.Statements.Remove(oralstatement);
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