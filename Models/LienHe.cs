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
    using System.ComponentModel.DataAnnotations;

    public partial class LienHe
    {
        public int MaLH { get; set; }
        [Display(Name = "Tên Cửa Hàng")]
        public string TenCH { get; set; }
        [Display(Name = "Nội Dung")]
        public string NoiDung { get; set; }
        [Display(Name = "Điện Thoại")]
        public string DienThoai { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }
        public Nullable<int> Created_by { get; set; }
        public Nullable<System.DateTime> Updated_at { get; set; }
        public Nullable<int> Updated_by { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
