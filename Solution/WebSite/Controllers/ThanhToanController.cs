using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThanhToan()
        {
            if(Session["user"] == null) return RedirectToAction("Login", "Home");
            try
            {
                string[] ma = Request.Form.GetValues("masp");
                string[] sl = Request.Form.GetValues("soluong");
                if (ma.Length != sl.Length)
                {
                    return new HttpStatusCodeResult(404);
                }
                for (int i = 0; i < ma.Length; i++)
                {
                    if (Regex.IsMatch(ma[i], @"^\d+$") && Regex.IsMatch(sl[i], @"^\d+$") && Convert.ToInt32(sl[i]) > 0)
                    {
                        SqlParameter[] parameters =
                        {
                            new SqlParameter("@masp", SqlDbType.Int) { Value = ma[i]},
                            new SqlParameter("@matk", SqlDbType.Int) { Value = Session["id"]},
                            new SqlParameter("@sl", SqlDbType.Int) { Value = sl[i]},
                            new SqlParameter("@ngay", SqlDbType.Date) { Value = DateTime.Now.ToString("yyyy-MM-dd")}
                        };
                        Connect_SQL.NonQuery("insertHoaDon", parameters);
                    }
                }
                Session["cart"] = null;

            }
            catch
            {
                return new HttpStatusCodeResult(404);
            }
            return RedirectToAction("donHang", "GioHang");
        }
    }
}