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
    public class DetentionController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Detention/

        public ActionResult Index(string filter)
        {
            if (filter == "notified")
            {
                return View(db.Detentions.Where(m=>m.IsNotifiRelatives==true).ToList());
            }
            if (filter == "notnotifed")
            {
                return View(db.Detentions.Where(m=>m.IsNotifiRelatives==false).ToList());
            }
            return View(db.Detentions.ToList());
        }

        //
        // GET: /Detention/Details/5

        public ActionResult Details(int id = 0)
        {
            Detention detention = db.Detentions.Find(id);
            if (detention == null)
            {
                return HttpNotFound();
            }
            return View(detention);
        }

        //
        // GET: /Detention/Create

        public ActionResult Create(int id=0)
        {
            if (db.Protocols.Find(id) != null)
            {
                Session["Protocol"] = db.Protocols.Find(id); 
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        //
        // POST: /Detention/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Detention detention)
        {
            detention.Protocol = db.Protocols.Find((Session["Protocol"] as Protocol).NumberProtocol);
            db.Detentions.Add(detention);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Detention/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Detention detention = db.Detentions.Find(id);
            if (detention == null)
            {
                return HttpNotFound();
            }
            return View(detention);
        }

        //
        // POST: /Detention/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Detention detention)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detention).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(detention);
        }

        //
        // GET: /Detention/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Detention detention = db.Detentions.Find(id);
            if (detention == null)
            {
                return HttpNotFound();
            }
            return View(detention);
        }

        //
        // POST: /Detention/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detention detention = db.Detentions.Find(id);
            db.Detentions.Remove(detention);
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