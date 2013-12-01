using DutyPanel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DutyPanel.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public DataContext db = new DataContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(DutyPanel.Users usr)
        {
            var q = from r in db.Users where (r.Email == usr.Email) && (r.Password == usr.Password) select r;
            if (q.Count() > 0)
            {
                User tmp_user = q.First();
                Session.Add("User", tmp_user);
                if (tmp_user is AdminUser)
                {
                    db.AdminUsers.Find(tmp_user.Id).DateOfLastEntry = DateTime.Now;
                    db.SaveChanges();
                }
                ViewData["ErrorText"] = null;
                return View("Index");
            }
            else
            {
                ViewData["ErrorText"] = "Ошибка авторизации. Проверьте правильность e-mail или пароля.";
                return View();
            }
        }/*
        public ActionResult Edit(int idu)
        {
            DutyPanel.Models.User tmp_usr = db.Users.Find(idu);
            if (tmp_usr is Duty)
            { 
                return RedirectToAction("EditDuty", new {id = idu});
            }
            return View();
        }*/
        public ActionResult EditDuty(int id)
        {
            SelectListItem[] rank_list = new SelectListItem[db.Ranks.Count()];
            int i = 0;
            foreach(Rank rank_tmp in db.Ranks)
            {
                rank_list[i] = new SelectListItem();
                rank_list[i].Selected = false;
                rank_list[i].Text = rank_tmp.Name;
                rank_list[i].Value = rank_tmp.Id.ToString();
                i++;
            }
            ViewData["Rank"] = new SelectList(db.Ranks, "Id", "Name");
            return View(db.Dutys.Find(id));
        }
        [HttpPost]
        public ActionResult EditDuty(DutyPanel.Models.Duty duty_usr)
        {
            duty_usr.Rank = db.Ranks.Find(Convert.ToInt32(Request.Form["Rank"]));

            db.Entry(duty_usr).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            Session["User"] = duty_usr;
            ViewData["InfoText"] = "Данные о пользователе обновлены";
            return RedirectToAction("Index");
        }
        /*[HttpPost]
        public ActionResult Edit(DutyPanel.Models.User usr)
        {
            db.Entry(usr).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            if (usr is AdminUser)
            {
                Session["User"] = usr as AdminUser;
            }
            if (usr is Duty)
            {
                Session["User"] = usr as Duty;
            }
            if (usr is OperativeWorker)
            {
                Session["User"] = usr as OperativeWorker;
            }
            if (usr is Driver)
            {
                Session["User"] = usr as Driver;
            }
            return RedirectToAction("Index");
        }*/

        /// <summary>
        /// profile/creatadmin - создание тестовых пользователей
        /// </summary>
        /// <returns></returns>
        public ActionResult CreatAdmin()
        {
            if (db.AdminUsers.Count() == 0)
            {
                db.AdminUsers.Add(new AdminUser() { LastName = "Prohorov", FirstName = "Evgeniy", SecondName = "Eduardovich", PassportNumber = 342234, WhoGivePassport = "UFMS Russia", SubdivisionPasport = 345345, DateGivePassport = DateTime.Parse("12.12.2008"), RegistrationAddress = "Russia", DateOfLastEntry = DateTime.Now, ContactPhone = "+7-344-343-34-34", Email = "evgwed@gmail.com", Password = "110149" });
                Rank r = new Rank() { Name = "Сержант", Salary = 34.34M };
                db.Ranks.Add(r);
                db.Dutys.Add(new Duty() {ContactPhone="+3-333-333-33-33", DateGivePassport = DateTime.Parse("23.02.1923"), DateIssueServiceCertificate = DateTime.Parse("23.10.2010"), DateLastEditedRank = DateTime.Parse("11.11.12"), DateRegistr= DateTime.Now, EmployeeNumber=32423, FirstName="Ivan", LastName="Ivanov", NumberServiceCertificate= 23, PassportNumber=34432234, PenaltiesCount=0, RegistrationAddress="Risdss", SecondName="Ilya", SubdivisionPasport=34234, TypeDuty= TypeDuty.Duty, WhoGivePassport="Ya", WorkingPhone="34-34-34", Email="duty@mail.ru", Password="duty", Rank = r});
                Car cr = new Car() { BrandCar="Mersedes", Color="Hot Red", DateLastRefueling= DateTime.Now, DesiredCategoryDriving="B", FuelType="92,95", IsPlaceFoDetainees= false, IsSpecSignal= true, ModelCar="S600", NumberCar="r345AM", ReleaseDate= DateTime.Now, SeatsCount=5};
                db.Cars.Add(cr);
                Driver dr = new Driver() { CategoryLicense = "A, B",  ContactPhone = "+33-33-33", DateGivePassport = DateTime.Now, DateIssueServiceCertificate = DateTime.Now, DateLastEditedRank = DateTime.Now, DateReceiptLicense = DateTime.Now, DateRegistr = DateTime.Now, EmployeeNumber = 342322, FirstName = "Dima", LastName = "Driverov", LicenseNumber = 34322, NumberMedicalCertificates = 342, NumberServiceCertificate = 2333, PassportNumber = 3422, PenaltiesCount = 0, Rank = r, RegistrationAddress = "ULSTU", SecondName = "Pinkov", SubdivisionPasport = 342342, WhoGivePassport = "On", WorkingPhone = "23422", Email="driver@mail.ru", Password="driver", WorkingCar = cr };
                db.Drivers.Add(dr);
                OperationalGroup opg = new OperationalGroup() { Driver= dr};
                db.OperationalGroups.Add(opg);
                OperativeWorker ow = new OperativeWorker() {AccessionNumberHandCuffs=3242, ContactPhone="234234", DateGivePassport= DateTime.Now, DateIssueServiceCertificate = DateTime.Now, DateLastEditedRank= DateTime.Now, DateRegistr= DateTime.Now, EmployeeNumber=342, FirstName="Kolya", Group = opg, IsHaveDog = false, IsHeadOfGroup = true, LastName="Operativov", NumberServiceCertificate=34, NumberServiceWeapon=342, PassportNumber=3423423, PenaltiesCount=1, Rank = r, RegistrationAddress="Derevnya", SecondName="sdf", SubdivisionPasport=342, WhoGivePassport="Esdd", WorkingPhone="234234", Email="operworker@mail.ru", Password="operworker"};
                db.OperativeWorkers.Add(ow);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
