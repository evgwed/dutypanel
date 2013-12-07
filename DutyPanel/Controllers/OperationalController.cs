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
            IEnumerable<OperativeWorker> workers = db.OperativeWorkers.Where(m => m.IsHeadOfGroup != true);
            ViewData["Workers"] = new SelectList(workers, "Id", "LastName");
            IEnumerable<Driver> drivers = db.Drivers.Where(m => m.Group == null);
            ViewData["Driver"] = new SelectList(drivers, "Id", "LastName");
            if (workers.Count() == 0)
            {
                ViewData["InfoText"] = "Нет свободных оперативных работников, которы бы могли сать главой оперативной группы.";
            }
            if (drivers.Count() == 0)
            {
                ViewData["InfoText"] = "Нет свободных водителей, которые могут совершать выезд оперативной группы";
            }
            if (workers.Count() == 0&&drivers.Count() == 0)
            {
                ViewData["InfoText"] = "Нет свободных оперативных работников, которы бы могли сать главой оперативной группы. Нет свободных водителей, которые могут совершать выезд оперативной группы";
            }
            return View();
        }

        //
        // POST: /Operational/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OperationalGroup operationalgroup)
        {
            db.OperationalGroups.Add(operationalgroup);
            if (Request.Form["Driver"] != null)
            {
                db.Drivers.Find(Convert.ToInt32(Request.Form["Driver"])).Group = db.OperationalGroups.Find(operationalgroup.IdGroup);
            }
            if (Request.Form["Workers"] != null)
            {
                db.OperativeWorkers.Find(Convert.ToInt32(Request.Form["Workers"])).Group = db.OperationalGroups.Find(operationalgroup.IdGroup);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
            IEnumerable<OperativeWorker> w_arr = db.OperativeWorkers.Where(s => s.Group.IdGroup == operationalgroup.IdGroup);
            foreach (OperativeWorker item in w_arr)
            {
                db.OperativeWorkers.Find(item.Id).IsHeadOfGroup = false;
                db.OperativeWorkers.Find(item.Id).Group = null;
            }
            db.Drivers.Find(operationalgroup.Driver.Id).Group = null;
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