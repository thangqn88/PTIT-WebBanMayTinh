using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class NhaSX
    {
        public string MaSX { get; set; }
        public string TenSX { get; set; }

        public static NhaSX getNhaSX(DataRow dr)
        {
            NhaSX sx = new NhaSX();
            sx.MaSX = dr[0].ToString();
            sx.TenSX = dr[1].ToString();
            return sx;
        }
        public static List<NhaSX> toList(DataTable dt)
        {
            List<NhaSX> lst = new List<NhaSX>();
            foreach(DataRow dr in dt.Rows)
            {
                lst.Add(getNhaSX(dr));
            }
            return lst;
        }
    }
}