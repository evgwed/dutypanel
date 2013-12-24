using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    //Служеный автомобиль
    public class Car
    {
        [Key]
        [Display(Name = "идентификатор автомобиля")]
        public int IdCar { get; set; }
        [Required]
        [Display(Name = "Номер автомобиля")]
        public string NumberCar { get; set; }
        [Required]
        [Display(Name = "Наличие места для задержания")]
        public bool IsPlaceFoDetainees { get; set; }
        [Required]
        [Display(Name = "Марка")]
        public string BrandCar { get; set; }
        [Required]
        [Display(Name = "Модель")]
        public string ModelCar { get; set; }
        [Required]
        [Display(Name = "Дата выпуска")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        [Display(Name = "Цвет кузова")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Тип заправляемого топлива")]
        public string FuelType { get; set; }
        [Required]
        [Display(Name = "Последняя дата заправки")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DateLastRefueling { get; set; }
        [Required]
        [Display(Name = "Число посадочных мест")]
        public int SeatsCount { get; set; }
        [Required]
        [Display(Name = "Наличие специализированных сигналов")]
        public bool IsSpecSignal { get; set; }
        [Required]
        [Display(Name = "Необходимая категория водительских прав")]
        public string DesiredCategoryDriving { get; set; }
        [Display(Name = "Водители")]
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}