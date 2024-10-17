using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class DonHang
    {
        public string MaDH { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string MaTK { get; set; }
        public string HoTen { get; set; }
        public int Soluong { get; set; }
        public double TongTien { get; set; }
        public string NgayDat { get; set; }
        public int TinTrang { get; set; }
        public static DonHang getDonHang(DataRow dr)
        {
            DonHang dh = new DonHang();
            dh.MaDH = dr[0].ToString();
            dh.MaSP = dr[1].ToString();
            dh.TenSP = dr[2].ToString();
            dh.MaTK = dr[3].ToString();
            dh.HoTen = dr[4].ToString();
            dh.Soluong = Convert.ToInt32(dr[5]);
            dh.TongTien = Convert.ToDouble(dr[6]);
            dh.NgayDat = Convert.ToDateTime(dr[7]).ToString("dd-MM-yyyy");
            dh.TinTrang = Convert.ToInt32(dr[8]);
            return dh;
        }
        public static List<DonHang> toList(DataTable dt)
        {
            List<DonHang> lst = new List<DonHang>();
            foreach(DataRow dr in dt.Rows)
            {
                lst.Add(getDonHang(dr));
            }
            return lst;
        }
    }
}