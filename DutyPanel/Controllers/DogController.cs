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
    public class DogController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Dog/

        public ActionResult Index()
        {
            var wardogs = db.WarDogs.Include(w => w.DogOwner);
            return View(wardogs.ToList());
        }

        //
        // GET: /Dog/Details/5

        public ActionResult Details(int id = 0)
        {
            WarDog wardog = db.WarDogs.Find(id);
            if (wardog == null)
            {
                return HttpNotFound();
            }
            return View(wardog);
        }

        //
        // GET: /Dog/Create

        public ActionResult Create()
        {
            if (db.OperativeWorkers.Where(m => m.IsHaveDog == false).Count() == 0)
            {
                ViewData["ErrorText"] = "Нельзя создать запись о новой собаке. У всех оперативных работников есть собака.";
            }
            ViewBag.IdDog = new SelectList(db.Users, "Id", "Password");//вижла сама это сделала,зачем???!!!
            ViewData["DogOwner"] = new SelectList(db.OperativeWorkers.Where(m=>m.IsHaveDog==false), "Id", "LastName");
            return View();
        }

        //
        // POST: /Dog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WarDog wardog)
        {
            wardog.DogOwner = db.OperativeWorkers.Find(Convert.ToInt32(Request.Form["DogOwner"]));
            db.WarDogs.Add(wardog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Dog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WarDog wardog = db.WarDogs.Find(id);
            if (wardog == null)
            {
                return HttpNotFound();
            }
            ViewData["DogOwner"] = new SelectList(db.OperativeWorkers, "Id", "LastName");
            ViewBag.IdDog = new SelectList(db.Users, "Id", "Password", wardog.IdDog);
            return View(wardog);
        }

        //
        // POST: /Dog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WarDog wardog)
        {
            wardog.DogOwner = db.OperativeWorkers.Find(Convert.ToInt32(Request.Form["DogOwner"]));
            db.Entry(wardog).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Dog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WarDog wardog = db.WarDogs.Find(id);
            if (wardog == null)
            {
                return HttpNotFound();
            }
            return View(wardog);
        }

        //
        // POST: /Dog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WarDog wardog = db.WarDogs.Find(id);
            db.WarDogs.Remove(wardog);
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