using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: Admin/SanPham
        public ActionResult Index()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            ViewData["NhaSX"] =  new DanhMucController().loadNhaSX();
            ViewData["LoaiSP"] = new DanhMucController().loadLoaiSP();
            ViewData["list"] = loadSanPham();
            return View();
        }

        public List<SanPham> loadSanPham()
        {
            List<SanPham> lst = new List<SanPham>();
            lst = SanPham.toList(Connect_SQL.Query("loadSanPham"));
            return lst;
        }
        [HttpPost]
        public ActionResult insertSanPham(List<HttpPostedFileBase> files)
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            //if(Request.Form["tensp"] != "" && Request.Form["loaisp"] != "" && Request.Form["sx"] != "" && Request.Form["soluong"] != "" && Request.Form["ram"] != null && Request.Form["cpu"] != null && Request.Form["gia"] != null && Request.Form["baohanh"] != null && Request.Form["manhinh"] != null && Request.Form["pin"] != null && Request.Form["vga"] != null)
            try
            {
                
                string ten = Request.Form["tensp"];
                int sl = Convert.ToInt32(Request.Form["soluong"]);
                int gia = Convert.ToInt32(Request.Form["gia"]);
                int bh = Convert.ToInt32(Request.Form["baohanh"]);
                double mh = Convert.ToDouble(Request.Form["manhinh"]);
                string cpu = Request.Form["cpu"];
                int ram = Convert.ToInt32(Request.Form["ram"]);
                int bonho = Convert.ToInt32(Request.Form["bonho"]);
                string vga = Request.Form["vga"];
                int pin = Convert.ToInt32(Request.Form["pin"]);
                int loai = Convert.ToInt32(Request.Form["loaisp"]);
                int sx = Convert.ToInt32(Request.Form["sx"]);

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten },
                    new SqlParameter("@sl", SqlDbType.Int) { Value = sl },
                    new SqlParameter("@gia", SqlDbType.Int) { Value = gia },
                    new SqlParameter("@bh", SqlDbType.Int) { Value = bh },
                    new SqlParameter("@mh", SqlDbType.Float) { Value = mh },
                    new SqlParameter("@cpu", SqlDbType.NVarChar) { Value = cpu },
                    new SqlParameter("@ram", SqlDbType.Int) { Value = ram },
                    new SqlParameter("@bonho", SqlDbType.Int) { Value = bonho },
                    new SqlParameter("@vga", SqlDbType.NVarChar) { Value = vga },
                    new SqlParameter("@pin", SqlDbType.Int) { Value = pin },
                    new SqlParameter("@loai", SqlDbType.Int) { Value = loai },
                    new SqlParameter("@sx", SqlDbType.Int) { Value = sx }
                };
                Connect_SQL.NonQuery("insertSanPham", parameters);
                string ma = Connect_SQL.Query("SELECT MAX(MaSP) FROM dbo.SanPham").Rows[0][0].ToString();
                try
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        if (files[i].ContentLength > 0)
                        {
                            string _FileName = ma + "_" + i.ToString() + ".png";
                            string _path = Path.Combine(Server.MapPath("~/Image"), _FileName);
                            files[i].SaveAs(_path);
                        }
                    }
                }
                catch
                {

                }
            }
            catch
            {

            }
            
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult updateSanPham()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            if (Request.Form["ma"] != null && Request.Form["tensp"] != null && Request.Form["loaisp"] != null && Request.Form["sx"] != null && Request.Form["soluong"] != null && Request.Form["ram"] != null && Request.Form["cpu"] != null && Request.Form["gia"] != null && Request.Form["baohanh"] != null && Request.Form["manhinh"] != null && Request.Form["pin"] != null && Request.Form["vga"] != null)
            {
                int ma = Convert.ToInt32(Request.Form["ma"]);
                string ten = Request.Form["tensp"];
                int sl = Convert.ToInt32(Request.Form["soluong"]);
                int gia = Convert.ToInt32(Request.Form["gia"]);
                int bh = Convert.ToInt32(Request.Form["baohanh"]);
                double mh = Convert.ToDouble(Request.Form["manhinh"]);
                string cpu = Request.Form["cpu"];
                int ram = Convert.ToInt32(Request.Form["ram"]);
                int bonho = Convert.ToInt32(Request.Form["bonho"]);
                string vga = Request.Form["vga"];
                int pin = Convert.ToInt32(Request.Form["pin"]);
                int loai = Convert.ToInt32(Request.Form["loaisp"]);
                int sx = Convert.ToInt32(Request.Form["sx"]);

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ma", SqlDbType.Int) { Value = ma },
                    new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten },
                    new SqlParameter("@sl", SqlDbType.Int) { Value = sl },
                    new SqlParameter("@gia", SqlDbType.Int) { Value = gia },
                    new SqlParameter("@bh", SqlDbType.Int) { Value = bh },
                    new SqlParameter("@mh", SqlDbType.Float) { Value = mh },
                    new SqlParameter("@cpu", SqlDbType.NVarChar) { Value = cpu },
                    new SqlParameter("@ram", SqlDbType.Int) { Value = ram },
                    new SqlParameter("@bonho", SqlDbType.Int) { Value = bonho },
                    new SqlParameter("@vga", SqlDbType.NVarChar) { Value = vga },
                    new SqlParameter("@pin", SqlDbType.Int) { Value = pin },
                    new SqlParameter("@loai", SqlDbType.Int) { Value = loai },
                    new SqlParameter("@sx", SqlDbType.Int) { Value = sx }
                };
                Connect_SQL.NonQuery("updateSanPham", parameters);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult deleteSanPham(string id)
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            if (id != null)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ma", SqlDbType.Int) { Value = id }
                };
                Connect_SQL.NonQuery("deleteSanPham", parameters);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}