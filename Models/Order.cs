using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HyperGear.Models
{
    public class Order
    {
        HyperGearEntities db = new HyperGearEntities();
        public int oMaSP { get; set; }
        public string oTenSP { get; set; }
        public string oAnh { get; set; }
        public int oSoluong { get; set; }
        public double oGia { get; set; }
        public string oSlug { get; set; }
        public double oThanhTien 
        {
            get { return oSoluong * oGia; }
        }

        public Order(int MaSP)
        {
            oMaSP = MaSP;
            SanPham sp = db.SanPhams.Single(n => n.MaSP == oMaSP);
            oTenSP = sp.TenSP;
            oAnh = sp.Anh;
            oSoluong = 1;
            oGia = double.Parse(sp.Gia.ToString());
            oSlug = sp.Slug;
        }

    }
}