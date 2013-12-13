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
        public ActionResult CreatDuty()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreatDuty(Duty duty_usr)
        {
            duty_usr.PenaltiesCount = 0;
            duty_usr.DateRegistr = DateTime.Now;
            duty_usr.Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            db.Dutys.Add(duty_usr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult CreatOperativeWorker()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            List<SelectListItem> for_query = new List<SelectListItem>();
            foreach (OperationalGroup item in db.OperationalGroups)
            {
                for_query.Add(new SelectListItem() { Value = item.IdGroup.ToString(), Text = "группа № " + item.IdGroup.ToString() });
            }
            ViewData["Group"] = for_query;
            return View();
        }
        [HttpPost]
        public ActionResult CreatOperativeWorker(OperativeWorker ow_usr)
        {
            ow_usr.PenaltiesCount = 0;
            ow_usr.DateRegistr = DateTime.Now;
            ow_usr.IsHeadOfGroup = false;
            ow_usr.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            ow_usr.Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            db.OperativeWorkers.Add(ow_usr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CreatDriver()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            if (db.OperationalGroups.Where(m => m.Driver == null).Count() != 0)
            {
                List<SelectListItem> for_query = new List<SelectListItem>();
                foreach (OperationalGroup item in db.OperationalGroups.Where(m=>m.Driver==null))
                {
                    for_query.Add(new SelectListItem() { Value = item.IdGroup.ToString(), Text = "группа № " + item.IdGroup.ToString() });
                }
                ViewData["Group"] = for_query;
                ViewData["WorkingCar"] = new SelectList(db.Cars, "IdCar", "NumberCar");
            }
            else
            {
                ViewData["ErrorText"] = "Нет оперативных групп, где бы можно было работать создаваемому водителю, поэтому необходимо в начале создать новую оперативную группу.";
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreatDriver(Driver driver_usr)
        {
            driver_usr.PenaltiesCount = 0;
            driver_usr.DateRegistr = DateTime.Now;
            driver_usr.Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            driver_usr.WorkingCar = db.Cars.Find(Convert.ToInt32(Request.Form["WorkingCar"]));
            driver_usr.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            db.Drivers.Add(driver_usr);
            db.SaveChanges();
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
            else
            {
                if (employeeuser is Duty)
                {
                    return View("EditDuty", db.Dutys.Find(id));
                }
                else
                {
                    if (employeeuser is OperativeWorker)
                    {
                        ViewData["Group"] = new SelectList(db.OperationalGroups, "IdGroup", "IdGroup");
                        return View("EditOperativeWorker", db.OperativeWorkers.Find(id));
                    }
                    else
                    {
                        ViewData["Group"] = new SelectList(db.OperationalGroups, "IdGroup", "IdGroup");
                        return View("EditDriver", db.Drivers.Find(id));
                    }
                }

            }
        }
        [HttpPost]
        public ActionResult EditDuty(DutyPanel.Models.Duty duty_usr)
        {
            duty_usr.Rank = db.Ranks.Find(duty_usr.Id);
            duty_usr.DateLastEditedRank = db.EmployeeUsers.Find(duty_usr.Id).DateLastEditedRank;
            duty_usr.TypeDuty = TypeDuty.Duty;
            db.Entry(duty_usr).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditOperativeWorker(DutyPanel.Models.OperativeWorker ow_usr)
        {
            ow_usr.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            ow_usr.Rank = db.Ranks.Find(ow_usr.Id);
            ow_usr.DateLastEditedRank = db.EmployeeUsers.Find(ow_usr.Id).DateLastEditedRank;
            db.Entry(ow_usr).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditDriver(DutyPanel.Models.Driver d_usr)
        {
            d_usr.Rank = db.Ranks.Find(d_usr.Id);
            d_usr.DateLastEditedRank = db.EmployeeUsers.Find(d_usr.Id).DateLastEditedRank;
            d_usr.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            db.Entry(d_usr).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditRank(int id = 0)
        {
            if (db.EmployeeUsers.Find(id) == null)
                return HttpNotFound();
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult EditRank(EmployeeUser empl_usr)
        {
            db.EmployeeUsers.Find(empl_usr.Id).DateLastEditedRank = DateTime.Now;
            db.EmployeeUsers.Find(empl_usr.Id).Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            db.Entry(db.EmployeeUsers.Find(empl_usr.Id)).State = EntityState.Modified;
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