//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HyperGear.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            this.DanhMucs = new HashSet<DanhMuc>();
        }
    
        public int MaMn { get; set; }
        public string TenMn { get; set; }
        public string Slug { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }
        public Nullable<int> Created_by { get; set; }
        public Nullable<System.DateTime> Updated_at { get; set; }
        public Nullable<int> Updated_by { get; set; }
        public Nullable<int> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhMuc> DanhMucs { get; set; }
    }
}
