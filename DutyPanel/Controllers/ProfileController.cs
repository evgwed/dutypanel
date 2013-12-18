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
            if (Session["User"] is Driver)
            {
                if ((Session["User"] as Driver).DateReceiptLicense < DateTime.Now.AddYears(-10))
                {
                    ViewData["Notification"] = "Вам необходимо обновить водительские права, так как срок действия истек.\n\n";
                }
            }
            else
            {
                if (Session["User"] is OperativeWorker)
                {
                    if ((Session["User"] as OperativeWorker).IsHaveDog)
                    {
                        if ((Session["User"] as OperativeWorker).WarDog.DateLastInspection < DateTime.Now.AddDays(-1))
                        {
                            ViewData["Notification"] = "Необходмио показать служебную собаку ветеренару.";
                        }
                    }
                }
            }
            return View();
        }
        public ActionResult Statements(string status)
        {
            if (status == "received")
            {
                return View(db.Statements.Where(m=>m.Status == StatementStatus.Received).OrderBy(m => m.DateStatment).ToList());
            }
            if (status == "processed")
            {
                return View(db.Statements.Where(m => m.Status == StatementStatus.Processed).OrderBy(m => m.DateStatment).ToList());
            }
            if (status == "reply")
            {
                return View(db.Statements.Where(m => m.Status == StatementStatus.Reply).OrderBy(m => m.DateStatment).ToList());
            }
            return View(db.Statements.OrderBy(m => m.DateStatment).ToList());
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
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["ErrorText"] = "Ошибка авторизации. Проверьте правильность e-mail или пароля.";
                return View();
            }
        }        
        
        /// <summary>
        /// profile/creatadmin - создание тестовых пользователей
        /// </summary>
        /// <returns></returns>
        public ActionResult CreatAdmin()
        {
            if (db.AdminUsers.Count() == 0)
            {
                db.AdminUsers.Add(new AdminUser() { LastName = "Prohorov", FirstName = "Evgeniy", SecondName = "Eduardovich", PassportNumber = 342234, WhoGivePassport = "UFMS Russia", SubdivisionPasport = 345345, DateGivePassport = DateTime.Parse("12.12.2008"), RegistrationAddress = "Russia", DateOfLastEntry = DateTime.Now, ContactPhone = "+7-344-343-34-34", Email = "evgwed@gmail.com", Password = "110149" });
                Rank r = new Rank() { Name = "Рядовой полиции", Salary = 5000.00M };
                db.Ranks.Add(r);
                db.Ranks.Add(new Rank() { Name = "Младший сержант", Salary = 6000.00M });
                db.Ranks.Add(new Rank() { Name = "Сержант", Salary = 6500.00M });
                db.Ranks.Add(new Rank() { Name = "Старший сержант", Salary = 7000.00M });
                db.Ranks.Add(new Rank() { Name = "Старшина", Salary = 7500.00M });
                db.Ranks.Add(new Rank() { Name = "Прапорщик", Salary = 8000.00M });
                db.Ranks.Add(new Rank() { Name = "Старший прапорщик", Salary = 8500.00M });
                db.Ranks.Add(new Rank() { Name = "Младший лейтенант", Salary = 9500.00M });
                db.Ranks.Add(new Rank() { Name = "Лейтенант", Salary = 10000.00M });
                db.Ranks.Add(new Rank() { Name = "Старший лейтенант", Salary = 10500.00M });
                db.Ranks.Add(new Rank() { Name = "Майор", Salary = 11500.00M });
                db.Ranks.Add(new Rank() { Name = "Подполковник", Salary = 12000.00M });
                db.Ranks.Add(new Rank() { Name = "Полковник", Salary = 13000.00M });
                db.Ranks.Add(new Rank() { Name = "Генерал-майор", Salary = 20000.00M });
                db.Ranks.Add(new Rank() { Name = "Генерал-лейтенант", Salary = 22000.00M });
                db.Ranks.Add(new Rank() { Name = "Генерал-полковник", Salary = 25000.00M });
                db.Ranks.Add(new Rank() { Name = "Генерал", Salary = 27000.00M });
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
        public ActionResult FindStatment()
        {
            return View(db.Statements.ToList());
        }
        [HttpPost]
        public ActionResult FindStatment(IEnumerable<DutyPanel.Models.Statement> d)
        {
            DateTime dateForm_min = DateTime.Parse(Request.Form["date_min"]),
                     dateForm_max = DateTime.Parse(Request.Form["date_max"]);
            string districtForm = Request.Form["district"];
            if (districtForm.Length == 0)
            {
                ViewData["FindText"] = "Необходимо запонить поле Район происшествий";
            }
            else
            {
                ViewData["FindText"] = "Результат поисказаявлений по району происшествия " + districtForm + ", начиная с даты " + dateForm_min.ToString() + " и заканчивая датой " + dateForm_max.ToString() + ".";
            }
            return View(db.Statements.Where(m=>(m.DaetIncident >= dateForm_min)&&(m.DaetIncident<= dateForm_max)&&(m.District == districtForm)));
        }
    }
}
