using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["user"] != null)
                return new HttpStatusCodeResult(404);
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
                    if (tk.TenTK != "" && tk.Quyen == 1)
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
        public ActionResult Logout()
        {
            if (Session["user"] == null)
                return new HttpStatusCodeResult(404);
            Session["user"] = null;
            Session["admin"] = null;
            Session["naem"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}