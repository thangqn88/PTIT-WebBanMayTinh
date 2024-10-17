using Newtonsoft.Json;
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
    public class DanhMucController : Controller
    {
        // GET: Admin/DanhMuc
        public ActionResult Index()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            return View();
        }
        public ActionResult LoaiSanPham()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            ViewData["list"] = loadLoaiSP();
            return View();
        }
        public List<LoaiSP> loadLoaiSP()
        {
            List<LoaiSP> lst = new List<LoaiSP>();
            lst = LoaiSP.toList(Connect_SQL.Query("loadLoaiSP"));
            return lst;
        }
        [HttpPost]
        public ActionResult insertLoaiSP()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            if (Request.Form["ten"] != null)
            {
                string ten = Request.Form["ten"];
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten }
                };
                Connect_SQL.NonQuery("insertLoaiSP", parameters);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult updateLoaiSP()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            if (Request.Form["ten"] != null && Request.Form["ma"] != null)
            {
                int ma = Convert.ToInt32(Request.Form["ma"]);
                string ten = Request.Form["ten"];
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ma", SqlDbType.Int) { Value = ma },
                    new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten }
                };
                Connect_SQL.NonQuery("updateLoaiSP", parameters);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult deleteLoaiSP()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            if (Request.Form["ma"] != null)
            {
                int ma = Convert.ToInt32(Request.Form["ma"]);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ma", SqlDbType.Int) { Value = ma }
                };
                Connect_SQL.NonQuery("deleteLoaiSP", parameters);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult NhaSanXuat()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            ViewData["list"] = loadNhaSX();
            return View();
        }

        public List<NhaSX> loadNhaSX()
        {
            List<NhaSX> lst = new List<NhaSX>();
            lst = NhaSX.toList(Connect_SQL.Query("loadSanXuat"));
            return lst;
        }

        public ActionResult insertNhaSX()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            if (Request.Form["ten"] != null)
            {
                string ten = Request.Form["ten"];
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten }
                };
                Connect_SQL.NonQuery("insertSanXuat", parameters);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult updateNhaSX()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            if (Request.Form["ten"] != null && Request.Form["ma"] != null)
            {
                int ma = Convert.ToInt32(Request.Form["ma"]);
                string ten = Request.Form["ten"];
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ma", SqlDbType.Int) { Value = ma },
                    new SqlParameter("@ten", SqlDbType.NVarChar) { Value = ten }
                };
                Connect_SQL.NonQuery("updateSanXuat", parameters);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult deleteNhaSX()
        {
            if (Session["user"] == null || Convert.ToInt32(Session["admin"]) != 1)
                return new HttpStatusCodeResult(404);
            if (Request.Form["ma"] != null)
            {
                int ma = Convert.ToInt32(Request.Form["ma"]);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ma", SqlDbType.Int) { Value = ma }
                };
                Connect_SQL.NonQuery("deleteSanXuat", parameters);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}