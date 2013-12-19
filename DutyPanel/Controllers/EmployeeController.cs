using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DutyPanel.Models;
using System.Data.SqlClient;
using System.Data.Common;

namespace DutyPanel.Controllers
{
    //Контроллер для управления Сотрудниками
    public class EmployeeController : Controller
    {
        private DataContext db = new DataContext();

        //отображение списка сотрудников
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(db.EmployeeUsers.ToList());
        }

        //Отображение записи о сотруднике
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            EmployeeUser employeeuser = db.EmployeeUsers.Find(id);
            if (employeeuser == null)
            {
                return HttpNotFound();
            }
            return View(employeeuser);
        }

        //Удаление записи о сотруднике
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EmployeeUser employeeuser = db.EmployeeUsers.Find(id);
            if (employeeuser == null)
            {
                return HttpNotFound();
            }
            return View(employeeuser);
        }

        //Удаление записи о сотруднике
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeUser employeeuser = db.EmployeeUsers.Find(id);
            db.EmployeeUsers.Remove(employeeuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Создание записи о сотруднике
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //Созданеи записи о сотруднике
        // POST: /Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeUsers euser)
        {
            //получение из формы типа создаваемого сотрудника и перенаправление на нужный метод
            switch (Request.Form["TypeUser"])
            {
                case "1":
                    return RedirectToAction("CreatDuty");
                case "2":
                    return RedirectToAction("CreatOperativeWorker");
                case "3":
                    return RedirectToAction("CreatDriver");

            }
            return RedirectToAction("Index");
        }
        //Созданеи записи о дежурном
        // GET: /Employee/CreateDuty
        public ActionResult CreatDuty()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View();
        }
        //Созданеи записи о дежурном
        // POST: /Employee/CreateDuty
        [HttpPost]
        public ActionResult CreatDuty(Duty duty_usr)
        {
            duty_usr.PenaltiesCount = 0;
            duty_usr.DateRegistr = DateTime.Now;
            duty_usr.TypeDuty = (TypeDuty)Convert.ToInt32(Request.Form["TypeDuty"]);
            duty_usr.Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            db.Dutys.Add(duty_usr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Созданеи записи об оперативном работнике
        // GET: /Employee/CreatOperativeWorker
        public ActionResult CreatOperativeWorker()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            List<SelectListItem> for_query = new List<SelectListItem>();
            foreach (OperationalGroup item in db.OperationalGroups)
            {
                for_query.Add(new SelectListItem() { Value = item.IdGroup.ToString(), Text = "группа № " + item.IdGroup.ToString() });
            }
            ViewData["Group"] = for_query;
            return View();
        }
        //Созданеи записи об оперативном работнике
        // POST: /Employee/CreatOperativeWorker
        [HttpPost]
        public ActionResult CreatOperativeWorker(OperativeWorker ow_usr)
        {
            ow_usr.PenaltiesCount = 0;
            ow_usr.DateRegistr = DateTime.Now;
            ow_usr.IsHeadOfGroup = false;
            ow_usr.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            ow_usr.Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            db.OperativeWorkers.Add(ow_usr);
            db.SaveChanges();
            if (ow_usr.Group.Workers.Count() == 1)
            {
                db.Database.Connection.Open();
                    // Начало транзакции
                    DbTransaction sqlTran = db.Database.Connection.BeginTransaction();

                    // Создание комнады транзакции
                    DbCommand command = db.Database.Connection.CreateCommand();
                    command.Transaction = sqlTran;

                    try
                    {
                        // Выполнение SQL команды на изменеие IsHeadOfGroup для оперативного сотрудника, который первый в оперативной группе
                        command.CommandText = @"
                            UPDATE
		                        OperativeWorkers
	                        SET
		                        OperativeWorkers.IsHeadOfGroup = 1
	                        WHERE OperativeWorkers.Id = "+ ow_usr.Id.ToString();
                        command.ExecuteNonQuery();
                        // Commit транзакции
                        sqlTran.Commit();
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        // Ошибка транзакции Commit
                        Session["TransactionError"] = "Ошибка в транзакции. Во время изменения поля IsHEadGroup возникла ошибка. "+ ex.Message+"   ";
                        try
                        {
                            // Попытка отмены транзакции
                            sqlTran.Rollback();
                        }
                        catch (Exception exRollback)
                        {
                            //Соединение закрыто или транзакция была уже отменена
                            Session["TransactionError"] += "Ошибка при отмены транзакции. ошибка при вызове метода Rollback().Соединение закрыто или транзакция была уже отменена. " + exRollback.Message;
                        }
                    }

            }
            return RedirectToAction("Index");
        }
        //Созданеи записи о водителе
        //GET: Employee/CreatDriver
        public ActionResult CreatDriver()
        {
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            if (db.OperationalGroups.Where(m => m.Driver == null).Count() != 0)
            {
                List<SelectListItem> for_query = new List<SelectListItem>();
                foreach (OperationalGroup item in db.OperationalGroups.Where(m=>m.Driver==null))
                {
                    for_query.Add(new SelectListItem() { Value = item.IdGroup.ToString(), Text = "группа № " + item.IdGroup.ToString() });
                }
                ViewData["Group"] = for_query;
                ViewData["WorkingCar"] = new SelectList(db.Cars, "IdCar", "NumberCar");
            }
            else
            {
                ViewData["ErrorText"] = "Нет оперативных групп, где бы можно было работать создаваемому водителю, поэтому необходимо в начале создать новую оперативную группу.";
            }
            return View();
        }
        //Созданеи записи о водителе
        //POST: Employee/CreatDriver
        [HttpPost]
        public ActionResult CreatDriver(Driver driver_usr)
        {
            driver_usr.PenaltiesCount = 0;
            driver_usr.DateRegistr = DateTime.Now;
            driver_usr.Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            driver_usr.WorkingCar = db.Cars.Find(Convert.ToInt32(Request.Form["WorkingCar"]));
            driver_usr.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            db.Drivers.Add(driver_usr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Редактирование записи о сотруднике
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EmployeeUser employeeuser = db.EmployeeUsers.Find(id);
            if (employeeuser == null)
            {
                return HttpNotFound();
            }
            else
            {
                //если дежурный
                if (employeeuser is Duty)
                {
                    return View("EditDuty", db.Dutys.Find(id));
                }
                else
                {
                    //если оперативный работник
                    if (employeeuser is OperativeWorker)
                    {
                        ViewData["Group"] = new SelectList(db.OperationalGroups, "IdGroup", "IdGroup");
                        return View("EditOperativeWorker", db.OperativeWorkers.Find(id));
                    }
                    else
                    {
                        //если водитель
                        ViewData["Group"] = new SelectList(db.OperationalGroups, "IdGroup", "IdGroup");
                        return View("EditDriver", db.Drivers.Find(id));
                    }
                }

            }
        }

        //Редактирование дежурного
        //POST: Employee/EditDuty
        [HttpPost]
        public ActionResult EditDuty(DutyPanel.Models.Duty duty_usr)
        {
            Duty tmp_duty = db.Dutys.Find(duty_usr.Id);
            tmp_duty.TypeDuty = (TypeDuty)Convert.ToInt32(Request.Form["TypeDuty"]);
            tmp_duty.ContactPhone = duty_usr.ContactPhone;
            tmp_duty.DateGivePassport = duty_usr.DateGivePassport;
            tmp_duty.DateIssueServiceCertificate = duty_usr.DateIssueServiceCertificate;
            tmp_duty.Email = duty_usr.Email;
            tmp_duty.EmployeeNumber = duty_usr.EmployeeNumber;
            tmp_duty.FirstName = duty_usr.FirstName;
            tmp_duty.LastName = duty_usr.LastName;
            tmp_duty.NumberServiceCertificate = duty_usr.NumberServiceCertificate;
            tmp_duty.PassportNumber = duty_usr.PassportNumber;
            tmp_duty.Password = duty_usr.Password;
            tmp_duty.PenaltiesCount = duty_usr.PenaltiesCount;
            tmp_duty.RegistrationAddress = duty_usr.RegistrationAddress;
            tmp_duty.SecondName = duty_usr.SecondName;
            tmp_duty.SubdivisionPasport = duty_usr.SubdivisionPasport;
            tmp_duty.WhoGivePassport = duty_usr.WhoGivePassport;
            tmp_duty.WorkingPhone = duty_usr.WorkingPhone;
            tmp_duty.TypeDuty = TypeDuty.AssistantDuty;
            db.Entry(tmp_duty).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
            return RedirectToAction("Index");
        }
        //Редактирование оперативного сотрудника
        //POST: Employee/EditOperativeWorker
        [HttpPost]
        public ActionResult EditOperativeWorker(DutyPanel.Models.OperativeWorker ow_usr)
        {
            OperativeWorker tmp_ow = db.OperativeWorkers.Find(ow_usr.Id);
            tmp_ow.ContactPhone = ow_usr.ContactPhone;
            tmp_ow.DateGivePassport = ow_usr.DateGivePassport;
            tmp_ow.DateIssueServiceCertificate = ow_usr.DateIssueServiceCertificate;
            tmp_ow.Email = ow_usr.Email;
            tmp_ow.EmployeeNumber = ow_usr.EmployeeNumber;
            tmp_ow.FirstName = ow_usr.FirstName;
            tmp_ow.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            tmp_ow.LastName = ow_usr.LastName;
            tmp_ow.NumberServiceCertificate = ow_usr.NumberServiceCertificate;
            tmp_ow.PassportNumber = ow_usr.PassportNumber;
            tmp_ow.Password = ow_usr.Password;
            tmp_ow.PenaltiesCount = ow_usr.PenaltiesCount;
            tmp_ow.RegistrationAddress = ow_usr.RegistrationAddress;
            tmp_ow.SecondName = ow_usr.SecondName;
            tmp_ow.SubdivisionPasport = ow_usr.SubdivisionPasport;
            tmp_ow.WhoGivePassport = ow_usr.WhoGivePassport;
            tmp_ow.WorkingPhone = ow_usr.WorkingPhone;
            tmp_ow.IsHeadOfGroup = ow_usr.IsHeadOfGroup;
            tmp_ow.IsHaveDog = ow_usr.IsHaveDog;
            tmp_ow.NumberServiceWeapon = ow_usr.NumberServiceWeapon;
            tmp_ow.AccessionNumberHandCuffs = ow_usr.AccessionNumberHandCuffs;
            db.Entry(tmp_ow).State = System.Data.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
            return RedirectToAction("Index");
        }
        //Редактирование водителя
        //POST: Employee/EditDriver
        [HttpPost]
        public ActionResult EditDriver(DutyPanel.Models.Driver d_usr)
        {
            Driver tmp_driver = db.Drivers.Find(d_usr.Id);
            tmp_driver.CategoryLicense = d_usr.CategoryLicense;
            tmp_driver.ContactPhone = d_usr.ContactPhone;
            tmp_driver.DateGivePassport = d_usr.DateGivePassport;
            tmp_driver.DateIssueServiceCertificate = d_usr.DateIssueServiceCertificate;
            tmp_driver.DateReceiptLicense = d_usr.DateReceiptLicense;
            tmp_driver.Email = d_usr.Email;
            tmp_driver.EmployeeNumber = d_usr.EmployeeNumber;
            tmp_driver.FirstName = d_usr.FirstName;
            tmp_driver.Group = db.OperationalGroups.Find(Convert.ToInt32(Request.Form["Group"]));
            tmp_driver.LastName = d_usr.LastName;
            tmp_driver.LicenseNumber = d_usr.LicenseNumber;
            tmp_driver.NumberMedicalCertificates = d_usr.NumberMedicalCertificates;
            tmp_driver.NumberServiceCertificate = d_usr.NumberServiceCertificate;
            tmp_driver.PassportNumber = d_usr.PassportNumber;
            tmp_driver.Password = d_usr.Password;
            tmp_driver.PenaltiesCount = d_usr.PenaltiesCount;
            tmp_driver.RegistrationAddress = d_usr.RegistrationAddress;
            tmp_driver.SecondName = d_usr.SecondName;
            tmp_driver.SubdivisionPasport = d_usr.SubdivisionPasport;
            tmp_driver.WhoGivePassport = d_usr.WhoGivePassport;
            tmp_driver.WorkingPhone = d_usr.WorkingPhone;
            db.Entry<Driver>(tmp_driver).State = System.Data.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = false;
            return RedirectToAction("Index");
        }
        //Изменение звания сотрдника
        //GET: Employee/EditRank/5
        public ActionResult EditRank(int id = 0)
        {
            if (db.EmployeeUsers.Find(id) == null)
                return HttpNotFound();
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View();
        }
        //Изменение звания сотрдника
        //POST: Employee/EditRank/5
        [HttpPost]
        public ActionResult EditRank(EmployeeUser empl_usr)
        {
            db.EmployeeUsers.Find(empl_usr.Id).DateLastEditedRank = DateTime.Now;
            db.EmployeeUsers.Find(empl_usr.Id).Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));
            db.Entry(db.EmployeeUsers.Find(empl_usr.Id)).State = EntityState.Modified;
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