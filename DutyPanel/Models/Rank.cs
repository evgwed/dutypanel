using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    //Звание
    public class Rank
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Наименование звания")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Оклад")]
        public decimal Salary { get; set; }
        [Display(Name = "Сотрудники")]
        public virtual IEnumerable<EmployeeUser> EmployeeUsers { get; set; }
    }
}