using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    public class Detention
    {
        [Key]
        [Display(Name = "Номер задержания")]
        public int NumberDetention { get; set; }
        [Required]
        [Display(Name = "Фамилия задержанного")]
        public string DetentionLastName { get; set; }
        [Required]
        [Display(Name = "Имя задержанного")]
        public string DetentionFirstName { get; set; }
        [Display(Name = "Отчество задержанного")]
        public string DetentionSecondName { get; set; }
        [Required]
        [Display(Name = "Дата и время задержания")]
        public DateTime? DateOfDetention { get; set; }
        [Required]
        [Display(Name = "Место задержания")]
        public string PleaceDetention { get; set; }
        [Required]
        [Display(Name = "Основания для задержания")]
        public string BaseDetention { get; set; }
        [Required]
        [Display(Name = "Личные вещи задержанного")]
        public string Things { get; set; }
        [Required]
        [Display(Name = "Были уведомлены родственники")]
        public bool IsNotifiRelatives { get; set; }
        [Required]
        [Display(Name = "Протокол")]
        public virtual Protocol Protocol { get; set; }
    }
}