using Blockchain_Thuoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Blockchain_Thuoc.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private ThuocModel db = new ThuocModel();

        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection Dangnhap)
        {
            string tk = Dangnhap["TaiKhoan"].ToString();
            string mk = Dangnhap["MatKhau"].ToString();
            var islogin = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan.Equals(tk) && x.MatKhau.Equals(mk));
            if (islogin == null)
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("Dangnhap");
            }
            // Tài khoản đăng nhập là quản trị viên
            else if (islogin.MaVaiTro == 1)
            {
                Session["userAdmin"] = islogin;
                return RedirectToAction("Index", "HomeAdmin");
            }
            // Tài khoản đăng nhập là cơ quan kiểm định
            else if(islogin.MaVaiTro == 2)
            {
                Session["userAdmin"] = islogin;
                return RedirectToAction("Quytrinhchuaxacthuc", "KiemDinhs");
            }
            // Tài khoản đăng nhập là nhà sản xuất
            else if (islogin.MaVaiTro == 3)
            {
                Session["userAdmin"] = islogin;
                return RedirectToAction("Index","QuyTrinhs");
            }else
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["userAdmin"] = null;
            return RedirectToAction("Dangnhap", "HomeAdmin");
        }
        public ActionResult DangXuatBs()
        {
            Session["userBS"] = null;
            return RedirectToAction("Dangnhap", "HomeAdmin");
        }

    }
}