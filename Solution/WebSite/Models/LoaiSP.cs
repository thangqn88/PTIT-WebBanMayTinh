using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class LoaiSP
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }

        public static LoaiSP getLoaiSP(DataRow dr)
        {
            LoaiSP sx = new LoaiSP();
            sx.MaLoai = dr[0].ToString();
            sx.TenLoai = dr[1].ToString();
            return sx;
        }
        public static List<LoaiSP> toList(DataTable dt)
        {
            List<LoaiSP> lst = new List<LoaiSP>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(getLoaiSP(dr));
            }
            return lst;
        }
    }
}