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
    
    public partial class LeavingGroups
    {
        public LeavingGroups()
        {
            this.WrittenStatements = new HashSet<WrittenStatements>();
        }
    
        public int IdLeaving { get; set; }
        public System.DateTime TimeLeaving { get; set; }
        public Nullable<int> Statement_NumberStatement { get; set; }
        public int Group_IdGroup { get; set; }
    
        public virtual OperationalGroups OperationalGroups { get; set; }
        public virtual Statements Statements { get; set; }
        public virtual Protocols Protocols { get; set; }
        public virtual ICollection<WrittenStatements> WrittenStatements { get; set; }
    }
}
