using QLNS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common
{
    public class ThemSuaXoa
    {
        //Thêm nhân sự
        public bool ThemNS(NhanSu s)
        {
            bool alreadyExists = db.NhanSus.Any(x => x.ID == s.ID);
            if (s == null)
                return false;
            if (alreadyExists)
                return false;
            else
            {
                db.NhanSus.InsertOnSubmit(s);
                db.SaveChanges();
                return true;
            }
        }

        //Cập nhật thông tin nhân sự
        public bool SuaNS(int? id, NhanSu newNS)
        {
            if (id == null)
                return false;
            else
            {
                NhanSu s = db.NhanSus.FirstOrDefault(x => x.ID == id);
                s.ID = newNS.ID;
                s.Hoten = newNS.Hoten;
                db.SaveChanges();
                return true;
            }
        }

        //Xoá nhân sự
        public bool XoaNS(int? id)
        {
            if (id == null)
                return false;
            else
            {
                NhanSu s = db.NhanSus.Where(x => x.ID == id).Single();
                db.NhanSus.DeleteOnSubmit(s);
                return true;
            }
        }


    }
}
