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
    public class EmployeeController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(db.EmployeeUsers.ToList());
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            EmployeeUser employeeuser = db.EmployeeUsers.Find(id);
            if (employeeuser == null)
            {
                return HttpNotFound();
            }
            return View(employeeuser);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeUsers euser)
        {
            switch (Request.Form["TypeUser"])
            {
                case "1":
                    return RedirectToAction("CreatDuty");
                case "2":
                    return RedirectToAction("CreatOperativeWorker");
                case "3":
                    return RedirectToAction("CreatDriver");

            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EmployeeUser employeeuser = db.EmployeeUsers.Find(id);
            if (employeeuser == null)
            {
                return HttpNotFound();
            }
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View(employeeuser);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeUser employeeuser)
        {
            employeeuser.Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            if (ModelState.IsValid)
            {
                db.Entry(employeeuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeuser);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EmployeeUser employeeuser = db.EmployeeUsers.Find(id);
            if (employeeuser == null)
            {
                return HttpNotFound();
            }
            return View(employeeuser);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeUser employeeuser = db.EmployeeUsers.Find(id);
            db.EmployeeUsers.Remove(employeeuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreatDuty()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View();
        }

        public ActionResult CreatOperativeWorker()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View();
        }
        public ActionResult CreatDriver()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}