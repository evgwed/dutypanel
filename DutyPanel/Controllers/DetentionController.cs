using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DutyPanel.Models;
using System.Data.SqlClient;
using DutyPanel.Models.ModelForSQL;

namespace DutyPanel.Controllers
{
    //Контроллер для управления задержаниями /Detention
    public class DetentionController : Controller
    {
        private DataContext db = new DataContext();

        //Отображение списка задержаний с поддержкой фильтрации
        // GET: /Detention/
        public ActionResult Index(string filter)
        {
            //Фильтр: были уведомлены родственники
            if (filter == "notified")
            {
                return View(db.Detentions.Where(m => m.IsNotifiRelatives == true).ToList());
            }
            //Фильтр: не были уведомлены родственники
            if (filter == "notnotifed")
            {
                return View(db.Detentions.Where(m => !(m.IsNotifiRelatives)).ToList());
            }
            //Отображение без фильтра
            return View(db.Detentions.ToList());
        }

        //Подробное отображение задержания
        // GET: /Detention/Details/5
        public ActionResult Details(int id = 0)
        {
            Detention detention = db.Detentions.Find(id);
            if (detention == null)
            {
                return HttpNotFound();
            }
            //SQL запрос на получение ФИО главы оператвной группы, которая произвела задержание(хранимая процедура)
            var q = db.Database.SqlQuery<FIO>("GetDetention @Lastname, @Firstname, @Secondname", new SqlParameter("Lastname", detention.DetentionLastName), new SqlParameter("Firstname", detention.DetentionFirstName), new SqlParameter("Secondname", detention.DetentionSecondName));
            FIO tmp_fio = q.FirstOrDefault();
            ViewData["FIO"] = tmp_fio.LastName + " " + tmp_fio.FirstName + " " + tmp_fio.SecondName;
            return View(detention);
        }

        //Создания записи о новом задержании
        // GET: /Detention/Create
        public ActionResult Create(int id = 0)
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

        //Создания записи о новом задержании
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

        //Редактирование записи о задержании
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

        //Редактирование записи о задержании
        // POST: /Detention/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Detention detention)
        {
            Detention det_tmp = db.Detentions.Find(detention.NumberDetention);
            det_tmp.BaseDetention = detention.BaseDetention;
            det_tmp.DateOfDetention = detention.DateOfDetention;
            det_tmp.DetentionFirstName = detention.DetentionFirstName;
            det_tmp.DetentionLastName = detention.DetentionLastName;
            det_tmp.DetentionSecondName = detention.DetentionSecondName;
            det_tmp.IsNotifiRelatives = detention.IsNotifiRelatives;
            det_tmp.PleaceDetention = detention.PleaceDetention;
            det_tmp.Things = detention.Things;
            db.Entry(det_tmp).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
            return RedirectToAction("Index");
        }

        //Удаление записи о задержании
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

        //Удаление записи о задержании
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
        //Поиск задержания по ФИО задержанного лица
        // GET: /Detention/FindDetention
        public ActionResult FindDetention()
        {
            return View(db.Detentions.ToList());
        }
        //Поиск задержания по ФИО задержанного лица
        // POST: /Detention/FindDetention
        [HttpPost]
        public ActionResult FindDetention(IEnumerable<DutyPanel.Models.Detention> none)
        {
            string last = Request.Form["LastName"],
                   first = Request.Form["FirstName"],
                   second = Request.Form["SecondName"];
            //Поиск задержания по ФИО задержанного
            if (last.Length != 0 && first.Length != 0 && second.Length != 0)
            {
                ViewData["FindText"] = "Поиск задержанного с именем " + first + ", фамилией " + last + " и отчеством "+second+".";
                return View(db.Detentions.Where(m => (m.DetentionLastName == last) && (m.DetentionFirstName == first) && (m.DetentionSecondName == second)).ToList());
            }
            //Поиск задержания без отчества задеражанного
            if (last.Length != 0 && first.Length != 0 && second.Length == 0)
            {
                ViewData["FindText"] = "Поиск задержанного с именем " + first + " и фамилией " + last + ".";
                return View(db.Detentions.Where(m => (m.DetentionLastName == last) && (m.DetentionFirstName == first)).ToList());
            }
            ViewData["FindText"] = null;
            return View(db.Detentions.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}