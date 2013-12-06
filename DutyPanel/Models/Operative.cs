using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
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
    public class LeavingGroup
    {
        [Key]
        public int IdLeaving { get; set; }
        [Required]
        [Display(Name = "Время выезда")]
        public DateTime? TimeLeaving { get; set; }
        [Display(Name = "Заявление")]
        public virtual Statement Statement { get; set; }
        [Required]
        [Display(Name = "Опертаивная группа")]
        public virtual OperationalGroup Group { get; set; }
        [Display(Name = "Составленныей протокол")]
        public virtual Protocol Protocol { get; set; }
    }
}