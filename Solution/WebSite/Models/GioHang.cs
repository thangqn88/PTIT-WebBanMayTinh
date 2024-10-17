using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class GioHang
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public int SoLuong { get; set; }
        public double Gia {get; set;}

        public GioHang()
        {
            this.Ma = "";
            this.Ten = "";
            this.SoLuong = 0;
            this.Gia = 0;
        }
        public GioHang(string ma, string ten, int sl, double gia)
        {
            this.Ma = ma;
            this.Ten = ten;
            this.SoLuong = sl;
            this.Gia = gia;
        }
    }
}