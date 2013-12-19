using DutyPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace DutyPanel.Controllers
{
    //Контроллер для управление главной страницей
    public class HomeController : Controller
    {
        public DataContext db = new DataContext();
        //Отображение главной страницы
        // GET: /Home/

        public ActionResult Index(bool res = false, string IsMiniStyle = "default")
        {
            //Запись о удачно поданном интернет заявлении
            if (res)
            {
                ViewData["InfoText"] = "Ваше заявление подано, скоро с вами свяжется дежурный для уточнения информации.";
            }
            //Изменение шаблона отображения страницы
            if (IsMiniStyle == "mini")
            {
                Session["Style"] = "mini";
            }
            else
            {
                Session["Style"] = "default";
            }
            //Необходимо для отображения тестовых пользователей
            if(db.Users.Count()!=0)
                ViewBag.Users = db.Users.Take(4);
            return View();
        }
        //Авторизация от имени одного из тестовых пользователей
        //GET: Home/TMP_Enter
        public ActionResult TMP_Enter(string role)
        {
            switch (role) { 
                case "admin":
                    Session["User"] = db.AdminUsers.First();
                    break;
                case "duty":
                    Session["User"] = db.Dutys.First();
                    break;
                case "driver":
                    Session["User"] = db.Drivers.First();
                    break;
                case "ow":
                    Session["User"] = db.OperativeWorkers.First();
                    break;
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}
