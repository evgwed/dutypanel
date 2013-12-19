using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DutyPanel.Models;
using DutyPanel.Models.ModelForSQL;

namespace DutyPanel.Controllers
{
    //Контроллер управления автомобилями
    public class CarController : Controller
    {
        private DataContext db = new DataContext();

        //Получение списка автомобилей
        // GET: /Car/

        public ActionResult Index()
        {
            return View(db.Cars.ToList());
        }

        //Получение подробной информации об автомобиле по id
        // GET: /Car/Details/5

        public ActionResult Details(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            //Хранимая в бд функция получения ФИО главного водителя автомобиля по номеру авто
            var q = db.Database.SqlQuery<FIO>("SELECT * FROM [dbo].[GetCarFIO](N'"+car.NumberCar+"')");
            FIO tmp_fio = q.FirstOrDefault();
            if (tmp_fio.FirstName == null)
            {
                ViewData["FIO"] = "У автомобиля нет водителей.";
            }
            else
            {
                ViewData["FIO"] = tmp_fio.LastName + " " + tmp_fio.FirstName + " " + tmp_fio.SecondName;
            }
            return View(car);
        }

        //Создание записи о новом автомобиле
        // GET: /Car/Create

        public ActionResult Create()
        {
            return View();
        }

        //Создание записи о новом автомобиле
        // POST: /Car/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        //Редакитрование записи об автомобиле
        // GET: /Car/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //Редакитрование записи об автомобиле
        // POST: /Car/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        //Удаление записи об автомобиле
        // GET: /Car/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //Удаление записи об автомобиле
        // POST: /Car/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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