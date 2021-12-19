using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HyperGear.Models
{
    public class ListOrder
    {
        public int MaDonHang { get; set; }
        public int Tinhtranggiaohang { get; set; }
        public DateTime Ngaydat { get; set; }
        public DateTime? Ngaygiao { get; set; }
        public string DeliveryName { get; set; }
        public double SAmount { get; set; }

    }
}