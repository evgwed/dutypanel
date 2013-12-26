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
    //Контроллер для управления записями о заслугах сотрудников
    public class MeritController : Controller
    {
        private DataContext db = new DataContext();

        //Получения списка заслуг
        // GET: /Merit/

        public ActionResult Index()
        {
            return View(db.Merits.OrderBy(m=>m.Employee.LastName).ToList());
        }

        //Отображение записи о заслуге сотрудника
        // GET: /Merit/Details/5

        public ActionResult Details(int id = 0)
        {
            Merit merit = db.Merits.Find(id);
            if (merit == null)
            {
                return HttpNotFound();
            }
            return View(merit);
        }

        //Создание новой записи о заслуге сотрудника
        // GET: /Merit/Create

        public ActionResult Create(int id = 0)
        {
            if (db.EmployeeUsers.Find(id) != null)
            {
                Session["Employee"] = db.EmployeeUsers.Find(id);
                return View();
            }
            else
            { 
                return HttpNotFound(); 
            }
        }

        //Создание новой записи о заслуге сотрудника
        // POST: /Merit/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Merit merit)
        {
            merit.Employee = db.EmployeeUsers.Find((Session["Employee"] as EmployeeUser).Id);
            db.Merits.Add(merit);
            db.SaveChanges();
            Session["Employee"] = null;
            return RedirectToAction("Index");
        }

        //Редактирование записи о заслуге сотрудника
        // GET: /Merit/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Merit merit = db.Merits.Find(id);
            if (merit == null)
            {
                return HttpNotFound();
            }
            return View(merit);
        }

        //Редакитрование записи о заслуге сотрудника
        // POST: /Merit/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Merit merit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(merit);
        }

        //Удаление записи о заслуге сотрудника
        // GET: /Merit/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Merit merit = db.Merits.Find(id);
            if (merit == null)
            {
                return HttpNotFound();
            }
            return View(merit);
        }

        //Удаление записи о заслуге сотрудника
        // POST: /Merit/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Merit merit = db.Merits.Find(id);
            db.Merits.Remove(merit);
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