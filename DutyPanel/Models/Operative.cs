using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    //Оперативная группа
    public class OperationalGroup
    {
        [Key]
        public int IdGroup { get; set; }
        [Display(Name = "Водитель")]
        public virtual Driver Driver { get; set; }
        [Display(Name = "Оперативные сотрудники")]
        public virtual ICollection<OperativeWorker> Workers { get; set; }
        [Display(Name = "Выездые оперативной группы")]
        public virtual ICollection<LeavingGroup> Leavings { get; set; }
    }
    //Выезд оперативной группы
    public class LeavingGroup
    {
        [Key]
        public int IdLeaving { get; set; }
        [Required]
        [Display(Name = "Время выезда")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? TimeLeaving { get; set; }
        [Required]
        [Display(Name = "Заявление")]
        public virtual Statement Statement { get; set; }
        [Required]
        [Display(Name = "Оперативная группа")]
        public virtual OperationalGroup Group { get; set; }
        [Display(Name = "Составленный протокол")]
        public virtual Protocol Protocol { get; set; }
    }
}