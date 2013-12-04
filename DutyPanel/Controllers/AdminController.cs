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
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(db.AdminUsers.ToList());
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id = 0)
        {
            AdminUser adminuser = db.AdminUsers.Find(id);
            if (adminuser == null)
            {
                return HttpNotFound();
            }
            return View(adminuser);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminUser adminuser)
        {
                adminuser.DateOfLastEntry = DateTime.Now;
                db.AdminUsers.Add(adminuser);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AdminUser adminuser = db.AdminUsers.Find(id);
            if (adminuser == null)
            {
                return HttpNotFound();
            }
            return View(adminuser);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminUser adminuser)
        {
            if (ModelState.IsValid)
            {
                adminuser.DateOfLastEntry = DateTime.Now;
                db.Entry(adminuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminuser);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AdminUser adminuser = db.AdminUsers.Find(id);
            if (adminuser == null)
            {
                return HttpNotFound();
            }
            return View(adminuser);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (db.AdminUsers.Count() > 1)
            {
                AdminUser adminuser = db.AdminUsers.Find(id);
                db.AdminUsers.Remove(adminuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["InfoText"] = "Ошибка удаления! В системе даолжен быть хотя бы один администратор.";
                return View(db.AdminUsers.Find(id));
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}