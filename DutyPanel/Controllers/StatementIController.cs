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
    //Контроллер для управления записями об интернет заявлениях
    public class StatementIController : Controller
    {
        private DataContext db = new DataContext();

        //Получение списка интернет заявлений
        // GET: /StatementI/

        public ActionResult Index()
        {
            return View(db.InternetStatements.ToList());
        }

        //Отображение записи об интернет заявлении
        // GET: /StatementI/Details/5

        public ActionResult Details(int id = 0)
        {
            InternetStatement internetstatement = db.InternetStatements.Find(id);
            if (internetstatement == null)
            {
                return HttpNotFound();
            }
            return View(internetstatement);
        }

        //Создание новой  записи об интернет заявлении
        // GET: /StatementI/Create

        public ActionResult Create()
        {
            return View();
        }

        //Создание новой записи об интернет заявлении
        // POST: /StatementI/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InternetStatement internetstatement)
        {
            internetstatement.Status = StatementStatus.Received;
            internetstatement.InfoBrowser = Request.Browser.Browser;
            internetstatement.IpAdress = GetIp(Request);
            internetstatement.DateStatment = DateTime.Now;
            db.InternetStatements.Add(internetstatement);
            db.SaveChanges();
            if (Session["User"] != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home", new { res = true });
            }
        }
        //Функция для получения реального IP пользователя
        public string GetIp(HttpRequestBase request)
        {
            string ipList = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }
            return request.ServerVariables["REMOTE_ADDR"];
        }
        //Редактирование записи об интернет заявлении
        // GET: /StatementI/Edit/5

        public ActionResult Edit(int id = 0)
        {
            InternetStatement internetstatement = db.InternetStatements.Find(id);
            if (internetstatement == null)
            {
                return HttpNotFound();
            }
            return View(internetstatement);
        }

        //Редактирование записи об интернет заявлении
        // POST: /StatementI/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InternetStatement internetstatement)
        {
            internetstatement.Status = (StatementStatus)Convert.ToInt32(Request.Form["Status"]);
            if (Session["User"] is Duty)
            { 
                internetstatement.Duty =  db.Dutys.Find((Session["User"] as Duty).Id);
            }
            db.Entry(internetstatement).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Удаление записи об интернет заявлении
        // GET: /StatementI/Delete/5

        public ActionResult Delete(int id = 0)
        {
            InternetStatement internetstatement = db.InternetStatements.Find(id);
            if (internetstatement == null)
            {
                return HttpNotFound();
            }
            return View(internetstatement);
        }

        //Удаление записи об интернет заявлении
        // POST: /StatementI/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InternetStatement internetstatement = db.InternetStatements.Find(id);
            db.InternetStatements.Remove(internetstatement);
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