using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    //Перечисление статусов заявлений
    public enum StatementStatus:int
    {
        //Заявление принято
        Received = 1,
        //Заявление обрабатывается
        Processed = 2,
        //Отправлен ответ заявителю
        Reply = 3
    }
    //Класс заявление
    public class Statement
    {
        [Key]
        [Display(Name = "Номер заявления")]
        public int NumberStatement { get; set; }
        [Required]
        [Display(Name = "Фимилия заявителя")]
        public string DeclarantLastName { get; set; }
        [Required]
        [Display(Name = "Имя заявителя")]
        public string DeclarantFirstName { get; set; }
        [Display(Name = "Отчество заявителя")]
        public string DeclarantSecondName { get; set; }
        [Required]
        [Display(Name = "Дата подачи заявления")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DateStatment { get; set; }
        [Required]
        [Display(Name = "Дата происшествия")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DaetIncident { get; set; }
        [Required]
        [Display(Name = "Адрес происшествия")]
        public string Adress { get; set; }
        [Required]
        [Display(Name = "Район происшествия")]
        public string District { get; set; }
        [Required]
        [Display(Name = "Нанесенный ущерб")]
        public string Harm { get; set; }
        [Required]
        [Display(Name = "Описание происшествия")]
        public string Description { get; set; }
        [Display(Name = "Статус заявления")]
        public virtual StatementStatus Status { get; set; }
        [Display(Name = "Дежурный составитель")]
        public virtual Duty Duty { get; set; }
        [Display(Name = "Выезд группы по заявлению")]
        public virtual LeavingGroup Leaving { get; set; }
    }
    //Интернет заявление
    public class InternetStatement : Statement
    {
        [Required]
        [Display(Name = "IP адрес компьтера заявителя")]
        public string IpAdress { get; set; }
        [Required]
        [Display(Name = "Информация о браузере заявителя")]
        public string InfoBrowser { get; set; }
    }
    //Устное заявление
    public class OralStatement : Statement
    {
        [Required]
        [Display(Name = "Телефон звонящего")]
        public string PhoneCaller { get; set; }
        [Required]
        [Display(Name = "Адрес звонящего")]
        public string AdressCaller { get; set; }
    }
    //Письменное заявление
    public class WrittenStatement : Statement
    {
        [Required]
        [Display(Name = "Серия и номер паспорта заявителя")]
        public int DeclarantNumberPasport { get; set; }
        [Required]
        [Display(Name = "Кем выдан паспорт")]
        public string DeclarantWhoGivePassport { get; set; }
        [Required]
        [Display(Name = "Код подразделения")]
        public int DeclarantSubdivisionPasport { get; set; }
        [Required]
        [Display(Name = "Дата выдачи паспорта")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DeclarantDateGivePassport { get; set; }
        [Required]
        [Display(Name = "Адрес прописки заявителя")]
        public string DeclarantRegistrationAddress { get; set; }
        [Required]
        [Display(Name = "Контактный телефон заявителя")]
        public string DeclarantContactPhone { get; set; }
    }
}