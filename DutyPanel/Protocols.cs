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
    
    public partial class Protocols
    {
        public Protocols()
        {
            this.Detentions = new HashSet<Detentions>();
        }
    
        public int NumberProtocol { get; set; }
        public System.DateTime DateOfPreparation { get; set; }
        public string PlaceOfPreparation { get; set; }
        public bool IsHaveDelayed { get; set; }
    
        public virtual ICollection<Detentions> Detentions { get; set; }
        public virtual LeavingGroups LeavingGroups { get; set; }
    }
}
