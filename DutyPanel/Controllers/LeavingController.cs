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
    //Контроллер для управления записями о выездах оперативных групп
    public class LeavingController : Controller
    {
        private DataContext db = new DataContext();

        //Получение списка выездов оперативных групп
        // GET: /Leaving/

        public ActionResult Index()
        {
            var leavingsgroups = db.LeavingsGroups.Include(l => l.Protocol);
            //Отдельная фильтрация для Оперативного работника
            if (Session["User"] is OperativeWorker)
            {
                OperationalGroup gr = db.OperationalGroups.Find((Session["User"] as OperativeWorker).Group.IdGroup);
                return View(db.LeavingsGroups.Where(m => m.Group.IdGroup == gr.IdGroup).ToList());
            }
            //Отделаьная фильратция для водителя
            if (Session["User"] is Driver)
            {
                OperationalGroup gr = db.OperationalGroups.Find((Session["User"] as Driver).Group.IdGroup);
                return View(db.LeavingsGroups.Where(m => m.Group.IdGroup == gr.IdGroup).ToList());
            }
            return View(db.LeavingsGroups.OrderBy(m=>m.Group.IdGroup).ToList());
        }

        //Отображение записи о выезде оперативной группы
        // GET: /Leaving/Details/5

        public ActionResult Details(int id = 0)
        {
            LeavingGroup leavinggroup = db.LeavingsGroups.Find(id);
            if (leavinggroup == null)
            {
                return HttpNotFound();
            }
            return View(leavinggroup);
        }

        //Создание новой записи о выезде оперативной группы
        // GET: /Leaving/Create

        public ActionResult Create(int id = 0)
        {
            if (db.Statements.Find(id) != null)
            {
                ViewBag.IdLeaving = new SelectList(db.Protocols, "NumberProtocol", "PlaceOfPreparation");
                Session["Statement"] = db.Statements.Find(id);
                ViewData["Group"] = new SelectList(db.OperationalGroups, "IdGroup", "IdGroup");
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        //Создание новой записи о выезде оперативной группы
        // POST: /Leaving/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeavingGroup leavinggroup)
        {
            leavinggroup.Statement = db.Statements.Find((Session["Statement"] as Statement).NumberStatement);
            db.Statements.Find((Session["Statement"] as Statement).NumberStatement).Status = StatementStatus.Processed;
            Session["Statement"] = null;
            leavinggroup.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            db.LeavingsGroups.Add(leavinggroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Удаление записи о выезде оперативной группы
        // GET: /Leaving/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LeavingGroup leavinggroup = db.LeavingsGroups.Find(id);
            if (leavinggroup == null)
            {
                return HttpNotFound();
            }
            return View(leavinggroup);
        }

        //Удаление записи о выезде оперативной группы
        // POST: /Leaving/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeavingGroup leavinggroup = db.LeavingsGroups.Find(id);
            db.LeavingsGroups.Remove(leavinggroup);
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