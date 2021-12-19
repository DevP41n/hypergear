using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HyperGear.Models
{
    public class Item
    {
        public SanPham Product { get; set; }
        public ChiTietDonHang chitiet { get; set; }
        public DonDatHang dondat { get; set; }
        public int Quantity { get; set; }
    }
}