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
    //Контроллер для управления записями об оператиных группах
    public class OperationalController : Controller
    {
        private DataContext db = new DataContext();

        //Отображение списка оперативных групп
        // GET: /Operational/

        public ActionResult Index()
        {
            return View(db.OperationalGroups.ToList());
        }

        //Отображение записи об оперативной группе
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

        //Создание новой записи об оперативной группе
        // GET: /Operational/Create

        public ActionResult Create()
        {
            ViewBag.IdGroup = new SelectList(db.Users, "Id", "Password");
            return View();
        }

        //Созданеи новой записи об оперативной группе
        // POST: /Operational/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OperationalGroup operationalgroup)
        {
            db.OperationalGroups.Add(operationalgroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Удаление записи об оперативной группе
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

        //Удаление записи об оперативной группе
        // POST: /Operational/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OperationalGroup operationalgroup = db.OperationalGroups.Find(id);
            IEnumerable<OperativeWorker> w_arr = db.OperativeWorkers.Where(s => s.Group.IdGroup == operationalgroup.IdGroup);
            //Открепление оперативных сотрудников от удаляемой группы
            foreach (OperativeWorker item in w_arr)
            {
                db.OperativeWorkers.Find(item.Id).IsHeadOfGroup = false;
                db.OperativeWorkers.Find(item.Id).Group = null;
            }
            //Открепление водителя от удаляемой оперативной группы
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