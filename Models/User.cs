using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HyperGear.Models
{
    public class User
    {
        HyperGearEntities db = new HyperGearEntities();
        public long InsertForFacebook(KhachHang entity)
        {
            var user = db.KhachHangs.SingleOrDefault(x => x.Taikhoan == entity.HoTen);
            if (user == null)
            {
                db.KhachHangs.Add(entity);
                db.SaveChanges();
                return entity.MaKH;
            }
            else
            {
                return user.MaKH;
            }

        }
    }
}