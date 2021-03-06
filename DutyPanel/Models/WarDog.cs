﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    //Класс служебной собаки
    public class WarDog
    {
        [Key]
        [Display(Name = "Личный номер собаки")]
        public int IdDog { get; set; }
        [Required]
        [Display(Name = "Хозяин собаки")]
        public virtual OperativeWorker DogOwner { get; set; }
        [Required]
        [Display(Name = "Кличка")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Порода")]
        public string Breed { get; set; }
        [Required]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirthday { get; set; }
        [Required]
        [Display(Name = "Дата взятия на службу")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCommencementOfService { get; set; }
        [Required]
        [Display(Name = "Дата последнего осмотра ветеринаром")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateLastInspection { get; set; }
    }
}