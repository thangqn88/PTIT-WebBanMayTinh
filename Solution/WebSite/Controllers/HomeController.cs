using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        static List<NhaSX> nhasx = new List<NhaSX>();
        static List<LoaiSP> loaisp = new List<LoaiSP>();
        static List<SanPham> sanpham = new List<SanPham>();
        [HttpGet]
        public ActionResult Index()
        {
            nhasx = NhaSX.toList(Connect_SQL.Query("loadSanXuat"));
            loaisp = LoaiSP.toList(Connect_SQL.Query("loadLoaiSP"));
            sanpham = loadSanPham();
            ViewData["nhasx"] = nhasx;
            ViewData["loaisp"] = loaisp;
            ViewData["list"] = sanpham;
            try
            {
                ViewBag.id = Convert.ToInt16(Request["id"]);
            }
            catch
            {
                ViewBag.id = 0;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(string submit)
        {
            try
            {
                ViewBag.id = Convert.ToInt16(Request["id"]);
            }
            catch
            {
                ViewBag.id = 0;
            }
            if (submit == "tim")
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@key", SqlDbType.NVarChar) { Value = Request.Form["search"] }
                };
                sanpham = SanPham.toList(Connect_SQL.Query("searchSanPham", parameters));
            }
            else
            {
                string radio = Request.Form["radio"];
                string masx = Request.Form["nhasx"] != "" ? Convert.ToInt32(Request.Form["nhasx"]).ToString() : "SanPham.MaSX";
                string maloai = Request.Form["loaisp"] != "" ? Convert.ToInt32(Request.Form["loaisp"]).ToString() : "SanPham.MaLoai";
                string min = Request.Form["min"] != "" ? Convert.ToInt32(Request.Form["min"]).ToString() : "0";
                string max = Request.Form["max"] != "" ? Convert.ToInt32(Request.Form["max"]).ToString() : "99999999";
                string sql = string.Format("SELECT MaSP, TenSP, SoLuong, GiaSP, BaoHanh, ManHinh, CPU, Ram, BoNho, VGA, Pin, SanPham.MaLoai, tb1.TenLoai, SanPham.MaSX, tb2.TenSX FROM dbo.SanPham, (SELECT * FROM dbo.LoaiSP) AS tb1, (SELECT * FROM dbo.NhaSX) AS tb2 WHERE SanPham.MaSX = tb2.MaSX AND SanPham.MaLoai = tb1.MaLoai AND SanPham.MaSX = {0} AND SanPham.MaLoai = {1} AND GiaSP > {2} AND GiaSP < {3} ", masx, maloai, min, max);
                if(radio != "")
                {
                    sql = sql + "ORDER BY GiaSP " + radio;
                }
                sanpham = SanPham.toList(Connect_SQL.Query(sql));
            }
            ViewData["nhasx"] = nhasx;
            ViewData["loaisp"] = loaisp;
            ViewData["list"] = sanpham;
            return View();
        }
        public List<SanPham> loadSanPham()
        {
            List<SanPham> lst = new List<SanPham>();
            lst = SanPham.toList(Connect_SQL.Query("loadSanPham"));
            return lst;
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["user"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (Session["user"] != null)
                return new HttpStatusCodeResult(404);
            if (username.Trim() != "" && password != "")
            {
                try
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@user", SqlDbType.VarChar) { Value = username },
                        new SqlParameter("@pass", SqlDbType.VarChar) { Value = password },
                    };
                    TaiKhoan tk = TaiKhoan.getTaiKhoan(Connect_SQL.Query("dangNhap", parameters).Rows[0]);
                    if (tk.TenTK != "")
                    {
                        Session["id"] = tk.MaTK;
                        Session["user"] = tk.TenTK;
                        Session["admin"] = tk.Quyen;
                        Session["name"] = tk.HoTen;
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch
                {

                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            if (Session["user"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Register(string name, string username, string email, string password, string repassword)
        {
            if (name.Trim() != "" && username.Trim() != "" && email.Trim() != "" && password.Trim() != "" && repassword.Trim() != "" && password == repassword)
            {
                try
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@user", SqlDbType.VarChar) { Value = username },
                        new SqlParameter("@pass", SqlDbType.VarChar) { Value = password },
                        new SqlParameter("@hoten", SqlDbType.NVarChar) { Value = name },
                        new SqlParameter("@email", SqlDbType.VarChar) { Value = email },
                        new SqlParameter("@quyen", SqlDbType.Int) { Value = 0 }
                    };
                    Connect_SQL.NonQuery("dangKy", parameters);
                    return RedirectToAction("Login", "Home");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            if (Session["user"] == null)
                return new HttpStatusCodeResult(404);
            Session["id"] = null;
            Session["user"] = null;
            Session["admin"] = null;
            Session["naem"] = null;
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult Info()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Home");
            ViewData["info"] = TaiKhoan.getTaiKhoan(Connect_SQL.Query("SELECT * FROM dbo.TaiKhoan WHERE MaTK = " + Session["id"]).Rows[0]);
            return View();
        }
        [HttpPost]
        public ActionResult updateInfo(string name, string username, string email, string phone, string address, string password, string repassword)
        {
            if (name.Trim() != "" && email.Trim() != "" && phone.Trim() !="" && address.Trim() != "")
            {
                try
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@ma", SqlDbType.Int) { Value = Session["id"] },
                        new SqlParameter("@hoten", SqlDbType.NVarChar) { Value = name },
                        new SqlParameter("@diachi", SqlDbType.NVarChar) { Value = address },
                        new SqlParameter("@sdt", SqlDbType.Int) { Value = phone },
                        new SqlParameter("@email", SqlDbType.VarChar) { Value = email },
                        new SqlParameter("@quyen", SqlDbType.Int) { Value = 0 }
                    };
                    Connect_SQL.NonQuery("updateTaiKhoan", parameters);
                }
                catch
                {
                    return RedirectToAction("Info", "Home");
                }
            }
            return RedirectToAction("Info", "Home");
        }
        [HttpGet]
        public ActionResult Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index", "Home");
            SqlParameter[] parameters =
            {
                new SqlParameter("@ma", SqlDbType.Int) { Value = id },
            };
            ViewData["detail"] = SanPham.getSanPham(Connect_SQL.Query("getSanPham", parameters).Rows[0]);
            return View();
        }
    }
}