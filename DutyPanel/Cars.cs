//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DutyPanel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cars
    {
        public Cars()
        {
            this.Drivers = new HashSet<Drivers>();
        }
    
        public int IdCar { get; set; }
        public string NumberCar { get; set; }
        public bool IsPlaceFoDetainees { get; set; }
        public string BrandCar { get; set; }
        public string ModelCar { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public string Color { get; set; }
        public string FuelType { get; set; }
        public System.DateTime DateLastRefueling { get; set; }
        public int SeatsCount { get; set; }
        public bool IsSpecSignal { get; set; }
        public string DesiredCategoryDriving { get; set; }
    
        public virtual ICollection<Drivers> Drivers { get; set; }
    }
}
