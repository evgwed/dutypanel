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
    
    public partial class Merits
    {
        public int Id { get; set; }
        public System.DateTime DateOfAssignment { get; set; }
        public string Description { get; set; }
        public string Promotion { get; set; }
        public int Employee_Id { get; set; }
    
        public virtual EmployeeUsers EmployeeUsers { get; set; }
    }
}
