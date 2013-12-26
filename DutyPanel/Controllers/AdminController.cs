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
    //Контроллер для упраавления записями об администраторах системы
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();

        //Получение списка администраторов системы
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(db.AdminUsers.OrderBy(m=>m.LastName).ToList());
        }

        //Получение подробной информации о администраторе
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

        //Создание новой записи об администраторе
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //Создание новой записи об администратор
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

        //Редактирование записи об администратор
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

        //Редактирование записи об администратор
        // POST: /Admin/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminUser adminuser)
        {
            AdminUser tmp_admin = db.AdminUsers.Find(adminuser.Id);
            tmp_admin.ContactPhone = adminuser.ContactPhone;
            tmp_admin.DateGivePassport = adminuser.DateGivePassport;
            tmp_admin.Email = adminuser.Email;
            tmp_admin.FirstName = adminuser.FirstName;
            tmp_admin.LastName = adminuser.LastName;
            tmp_admin.PassportNumber = adminuser.PassportNumber;
            tmp_admin.Password = adminuser.Password;
            tmp_admin.RegistrationAddress = adminuser.RegistrationAddress;
            tmp_admin.SecondName = adminuser.SecondName;
            tmp_admin.SubdivisionPasport = adminuser.SubdivisionPasport;
            tmp_admin.WhoGivePassport = adminuser.WhoGivePassport;
            db.Entry(tmp_admin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Удаление записи об администратор
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

        //Удаление
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
                //Запрет на удаление единственного в системе администратора
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