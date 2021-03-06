﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    //Заслуги
    public class Merit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Дата присвоения")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfAssignment { get; set; }
        [Required]
        [Display(Name = "Описание заслуги")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Поощрение за заслугу")]
        public string Promotion { get; set; }
        [Required]
        [Display(Name = "Иденитфикатор пользователя")]
        public virtual EmployeeUser Employee { get; set; }
    }
}