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
    public class HoangHuuHuyenController : Controller
    {
        // GET: HoangHuuHuyen
        public ActionResult Index()
        {
            ViewData["list"] = NhaSX.toList(Connect_SQL.Query("SELECT * FROM dbo.NhaSX"));
            return View();
        }
        [HttpPost]
        public ActionResult Them(string ten)
        {
            SqlParameter[] parameters =
                {
                    new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten }
                };
            Connect_SQL.Query("insertSanXuat", parameters);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SanPham()
        {
            return View();
        }
    }
}