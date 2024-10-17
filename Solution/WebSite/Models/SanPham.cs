using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class SanPham
    {
 
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public double GiaSP { get; set; }
        public int BaoHanh { get; set; }
        public double MaHinh { get; set; }
        public string CPU { get; set; }
        public int Ram { get; set; }
        public int BoNho { get; set; }
        public string VGA { get; set; }
        public int Pin { get; set; }
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MaSX { get; set; }
        public string TenSX { get; set; }
        public static SanPham getSanPham(DataRow dr)
        {
            SanPham sp = new SanPham();
            sp.MaSP = dr[0].ToString();
            sp.TenSP = dr[1].ToString();
            sp.SoLuong = Convert.ToInt32(dr[2]);
            sp.GiaSP = Convert.ToDouble(dr[3]);
            sp.BaoHanh = Convert.ToInt32(dr[4]);
            sp.MaHinh = Convert.ToDouble(dr[5]);
            sp.CPU = dr[6].ToString();
            sp.Ram = Convert.ToInt32(dr[7]);
            sp.BoNho = Convert.ToInt32(dr[8]);
            sp.VGA = dr[9].ToString();
            sp.Pin = Convert.ToInt32(dr[10]);
            sp.MaLoai = dr[11].ToString();
            sp.TenLoai = dr[12].ToString();
            sp.MaSX = dr[13].ToString();
            sp.TenSX = dr[14].ToString();
            return sp;
        }
        public static SanPham getSP(DataRow dr)
        {
            SanPham sp = new SanPham();
            sp.MaSP = dr[0].ToString();
            sp.TenSP = dr[1].ToString();
            sp.SoLuong = Convert.ToInt32(dr[2]);
            sp.GiaSP = Convert.ToDouble(dr[3]);
            sp.BaoHanh = Convert.ToInt32(dr[4]);
            sp.MaHinh = Convert.ToDouble(dr[5]);
            sp.CPU = dr[6].ToString();
            sp.Ram = Convert.ToInt32(dr[7]);
            sp.BoNho = Convert.ToInt32(dr[8]);
            sp.VGA = dr[9].ToString();
            sp.Pin = Convert.ToInt32(dr[10]);
            sp.MaLoai = dr[11].ToString();
            sp.MaSX = dr[12].ToString();
            return sp;
        }
        public static List<SanPham> toList2(DataTable dt)
        {
            List<SanPham> lst = new List<SanPham>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(getSP(dr));
            }
            return lst;
        }
        public static List<SanPham> toList(DataTable dt)
        {
            List<SanPham> lst = new List<SanPham>();
            foreach(DataRow dr in dt.Rows)
            {
                lst.Add(getSanPham(dr));
            }
            return lst;
        }
    }
}