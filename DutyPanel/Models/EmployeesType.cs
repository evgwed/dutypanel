using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    public enum TypeDuty : int
    {
        //Старшый оперативный дежурный
        SeniorDuty = 1,
        //Дежурный
        Duty = 2,
        //Помощник оперативного дежурного(по службе 02)
        AssistantDuty = 3
    }
    //Дужурный
    public class Duty : EmployeeUser
    {
        [Required]
        [Display(Name = "Тип должности")]
        public TypeDuty TypeDuty { get; set; }
        public virtual ICollection<Statement> Statments { get; set; }
    }
    //Оперативнй работник
    public class OperativeWorker : EmployeeUser
    {
        [Required]
        [Display(Name = "Является начальником группы")]
        public bool IsHeadOfGroup { get; set; }
        [Required]
        [Display(Name = "Номер табельного оружия")]
        public int NumberServiceWeapon { get; set; }
        [Required]
        [Display(Name = "Инвентарный номер наручников")]
        public int AccessionNumberHandCuffs { get; set; }
        [Required]
        [Display(Name = "Наличие служебной собаки")]
        public bool IsHaveDog { get; set; }
        [Display(Name = "Служебная собака")]
        public virtual WarDog WarDog { get; set; }
        [Display(Name = "Оперативная группа")]
        public virtual OperationalGroup Group { get; set; }
    }
    //Водитель
    public class Driver : EmployeeUser
    {
        [Required]
        [Display(Name = "Номер водительского удостоверения")]
        public int LicenseNumber { get; set; }
        [Required]
        [Display(Name = "Дата получения водительских прав")]
        public DateTime? DateReceiptLicense { get; set; }
        [Required]
        [Display(Name = "Категория водительских прав")]
        public string CategoryLicense { get; set; }
        [Required]
        [Display(Name = "Номер медицинской справки на вождение")]
        public int NumberMedicalCertificates { get; set; }
        [Display(Name = "Рабочий автомобиль")]
        public virtual Car WorkingCar { get; set; }
        [Display(Name = "Оперативная группа")]
        public virtual OperationalGroup Group { get; set; }
    }
}