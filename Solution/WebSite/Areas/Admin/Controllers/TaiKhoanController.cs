using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: Admin/TaiKhoan
        public ActionResult Index()
        {
            ViewData["lst"] = TaiKhoan.toList(Connect_SQL.Query("SELECT * FROM dbo.TaiKhoan"));
            return View();
        }
        public ActionResult Info(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new EmptyResult();
            }
            ViewData["info"] = TaiKhoan.getTaiKhoan(Connect_SQL.Query("SELECT * FROM dbo.TaiKhoan WHERE MaTK = " + id).Rows[0]);
            return View();
        }
    }
}