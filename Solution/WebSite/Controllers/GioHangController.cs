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
    public class GioHangController : Controller
    {
        // GET: GioHang
       public ActionResult Add(string id)
        {
            if(Regex.IsMatch(id, @"^\d+$"))
            {
                List<GioHang> lst = (List<GioHang>)Session["cart"];
                if (lst == null)
                {
                    lst = new List<GioHang>();
                }
                for(int i = 0; i < lst.Count; i++)
                {
                    if(lst[i].Ma == id)
                    {
                        lst[i].SoLuong++;
                        Session["cart"] = lst;
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                SanPham sp = SanPham.getSP(Connect_SQL.Query("SELECT * FROM dbo.SanPham WHERE MaSP = " + id).Rows[0]);
                if(sp != null)
                {
                    GioHang gh = new GioHang(sp.MaSP, sp.TenSX + sp.TenSP, 1, sp.GiaSP);
                    lst.Add(gh);
                    Session["cart"] = lst;
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Remove(string id)
        {
            List<GioHang> lst = (List<GioHang>)Session["cart"];
            if (Regex.IsMatch(id, @"^\d+$") && lst != null)
            {
                int i = -1;
                for (i = 0; i < lst.Count; i++)
                {
                    if(lst[i].Ma == id)
                    {
                        break;
                    }
                }
                if(i != -1)
                {
                    lst.RemoveAt(i);
                    Session["cart"] = lst;
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult donHang()
        {
            if(Session["id"] == null) return RedirectToAction("Login", "Home");
            SqlParameter[] parameters =
            {
                new SqlParameter("@ma", SqlDbType.Int) { Value = Session["id"]}
            };
            ViewData["lst"] = DonHang.toList(Connect_SQL.Query("getHoaDon", parameters));
            return View();
        }
    }
}