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
    public class OperationalController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Operational/

        public ActionResult Index()
        {
            return View(db.OperationalGroups.ToList());
        }

        //
        // GET: /Operational/Details/5

        public ActionResult Details(int id = 0)
        {
            OperationalGroup operationalgroup = db.OperationalGroups.Find(id);
            if (operationalgroup == null)
            {
                return HttpNotFound();
            }
            return View(operationalgroup);
        }

        //
        // GET: /Operational/Create

        public ActionResult Create()
        {
            ViewBag.IdGroup = new SelectList(db.Users, "Id", "Password");
            return View();
        }

        //
        // POST: /Operational/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OperationalGroup operationalgroup)
        {
                db.OperationalGroups.Add(operationalgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        //
        // GET: /Operational/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OperationalGroup operationalgroup = db.OperationalGroups.Find(id);
            if (operationalgroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGroup = new SelectList(db.Users, "Id", "Password", operationalgroup.IdGroup);
            return View(operationalgroup);
        }

        //
        // POST: /Operational/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OperationalGroup operationalgroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operationalgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGroup = new SelectList(db.Users, "Id", "Password", operationalgroup.IdGroup);
            return View(operationalgroup);
        }

        //
        // GET: /Operational/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OperationalGroup operationalgroup = db.OperationalGroups.Find(id);
            if (operationalgroup == null)
            {
                return HttpNotFound();
            }
            return View(operationalgroup);
        }

        //
        // POST: /Operational/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OperationalGroup operationalgroup = db.OperationalGroups.Find(id);
            db.OperationalGroups.Remove(operationalgroup);
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