using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class TaiKhoan
    {
        public string MaTK { get; set; }
        public string TenTK { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public int SDT { get; set; }
        public string Email { get; set; }
        public int Quyen { get; set; }

        public static TaiKhoan getTaiKhoan(DataRow dr)
        {
            TaiKhoan tk = new TaiKhoan();
            tk.MaTK = dr[0].ToString();
            tk.TenTK = dr[1].ToString();
            tk.MatKhau = dr[2].ToString();
            tk.HoTen = dr[3].ToString();
            tk.DiaChi = dr[4].ToString();
            tk.SDT = dr[5].ToString() != "" ? Convert.ToInt32(dr[5]) : 0;
            tk.Email = dr[6].ToString();
            tk.Quyen = Convert.ToInt32(dr[7]);
            return tk;
        }
        public static List<TaiKhoan> toList(DataTable dt)
        {
            List<TaiKhoan> lst = new List<TaiKhoan>();
            foreach(DataRow dr in dt.Rows)
            {
                lst.Add(getTaiKhoan(dr));
            }
            return lst;
        }
    }
}