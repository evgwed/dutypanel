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
    //Контроллер для управления протоколами
    public class ProtocolController : Controller
    {
        private DataContext db = new DataContext();

        //Получение списка протоколов
        // GET: /Protocol/

        public ActionResult Index()
        {
            var protocols = db.Protocols.Include(p => p.Leaving);
            return View(protocols.ToList());
        }

        //Отображение записи о протоколе
        // GET: /Protocol/Details/5

        public ActionResult Details(int id = 0)
        {
            Protocol protocol = db.Protocols.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            return View(protocol);
        }

        //Создание записи о протоколе
        // GET: /Protocol/Create

        public ActionResult Create(int id = 0)
        {
            if (db.LeavingsGroups.Find(id) != null)
            {
                Session["LeavingGroup"] = db.LeavingsGroups.Find(id);
                ViewBag.NumberProtocol = new SelectList(db.LeavingsGroups, "IdLeaving", "IdLeaving");
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        //Создание записи о протоколе
        // POST: /Protocol/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Protocol protocol)
        {
            protocol.Leaving = db.LeavingsGroups.Find((Session["LeavingGroup"] as LeavingGroup).IdLeaving);
            if (ModelState.IsValid)
            {
                db.Protocols.Add(protocol);
                //изменяется статус заявления на "Отправлен ответ заявителю"
                LeavingGroup tmp_leaving = db.LeavingsGroups.Find((Session["LeavingGroup"] as LeavingGroup).IdLeaving);
                db.Statements.Find(tmp_leaving.Statement.NumberStatement).Status = StatementStatus.Reply;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NumberProtocol = new SelectList(db.LeavingsGroups, "IdLeaving", "IdLeaving", protocol.NumberProtocol);
            return View(protocol);
        }

        //Редактирование записи о протоколе
        // GET: /Protocol/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Protocol protocol = db.Protocols.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            ViewBag.NumberProtocol = new SelectList(db.LeavingsGroups, "IdLeaving", "IdLeaving", protocol.NumberProtocol);
            return View(protocol);
        }

        //Редактирование записи о протоколе
        // POST: /Protocol/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Protocol protocol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(protocol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NumberProtocol = new SelectList(db.LeavingsGroups, "IdLeaving", "IdLeaving", protocol.NumberProtocol);
            return View(protocol);
        }

        //Удаление записи о протоколе
        // GET: /Protocol/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Protocol protocol = db.Protocols.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            return View(protocol);
        }

        //удаление записи о протоколе
        // POST: /Protocol/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Protocol protocol = db.Protocols.Find(id);
            db.Protocols.Remove(protocol);
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