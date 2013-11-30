using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    public class Protocol
    {
        [Key]
        [Display(Name = "Номер протокола")]
        public int NumberProtocol { get; set; }
        [Required]
        [Display(Name = "Дата составления")]
        public DateTime? DateOfPreparation { get; set; }
        [Required]
        [Display(Name = "Место составления")]
        public string PlaceOfPreparation { get; set; }
        [Required]
        [Display(Name = "Имеются задержанные")]
        public bool IsHaveDelayed { get; set; }
        [Display(Name = "Оперативная группа")]
        public virtual LeavingGroup Leaving { get; set; }
        [Display(Name = "Задержания")]
        public virtual IEnumerable<Detention> Detentions { get; set; }
    }
}