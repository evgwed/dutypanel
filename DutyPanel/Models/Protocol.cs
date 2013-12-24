using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    //Протокол
    public class Protocol
    {
        [Key]
        [Display(Name = "Номер протокола")]
        public int NumberProtocol { get; set; }
        [Required]
        [Display(Name = "Дата составления")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
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
        public virtual ICollection<Detention> Detentions { get; set; }
    }
}