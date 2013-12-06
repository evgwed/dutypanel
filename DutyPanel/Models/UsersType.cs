using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    //Пользователь
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string SecondName { get; set; }
        [Required]
        [Display(Name = "Серия и номер паспорта")]
        public int PassportNumber { get; set; }
        [Required]
        [Display(Name = "Кем выдан паспорт")]
        public string WhoGivePassport { get; set; }
        [Required]
        [Display(Name = "Дата выдачи паспорта")]
        public DateTime? DateGivePassport { get; set; }
        [Required]
        [Display(Name = "Код подразделения")]
        public int SubdivisionPasport { get; set; }
        [Required]
        [Display(Name = "Адрес прописки")]
        public string RegistrationAddress { get; set; }
        [Required]
        [Display(Name = "Контактный телефон")]
        public string ContactPhone { get; set; }
    }
    //Администратор
    public class AdminUser : User
    {
        [Required]
        [Display(Name = "Дата последнего входа")]
        public DateTime? DateOfLastEntry { get; set; }
    }
    //Сотрудник
    public class EmployeeUser : User
    {
        [Required]
        [Display(Name = "Личный номер сотрудника")]
        public int EmployeeNumber { get; set; }
        [Required]
        [Display(Name = "Дата регистрации в ситеме")]
        public DateTime? DateRegistr { get; set; }
        [Required]
        [Display(Name = "Дата последнего изменения звания")]
        public DateTime? DateLastEditedRank { get; set; }
        [Required]
        [Display(Name = "Рабочий телефон")]
        public string WorkingPhone { get; set; }
        [Required]
        [Display(Name = "Количество взысканий")]
        public int PenaltiesCount { get; set; }
        [Required]
        [Display(Name = "Номер служебного удостоверения")]
        public int NumberServiceCertificate { get; set; }
        [Required]
        [Display(Name = "Дата выдачи служебного удостоверения")]
        public DateTime? DateIssueServiceCertificate { get; set; }
        [Required]
        [Display(Name = "Идентификатор звания")]
        public virtual Rank Rank { get; set; }
        [Display(Name = "Заслуги")]
        public virtual ICollection<Merit> Merits { get; set; }
    }
}