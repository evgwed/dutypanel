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
    
    public partial class Drivers
    {
        public int Id { get; set; }
        public int WorkingCar_IdCar { get; set; }
        public int LicenseNumber { get; set; }
        public System.DateTime DateReceiptLicense { get; set; }
        public string CategoryLicense { get; set; }
        public int NumberMedicalCertificates { get; set; }
    
        public virtual Cars Cars { get; set; }
        public virtual EmployeeUsers EmployeeUsers { get; set; }
        public virtual OperationalGroups OperationalGroups { get; set; }
    }
}
