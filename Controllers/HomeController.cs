using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Blockchain_Thuoc.Common;
using Blockchain_Thuoc.Models;

namespace Blockchain_Thuoc.Controllers
{
    public class HomeController : Controller
    {
        ThuocModel db = new ThuocModel();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimKiem(FormCollection form)
        {
            var searchString = form["searchString"];
            var sp = from l in db.SanPhams
                     select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.TenSanPham.Contains(searchString));
            }

            return View(sp);
        }
        // GET: Sanpham
        public ActionResult SanphamPartialView()
        {

            var sp = db.SanPhams.ToList();
            return PartialView(sp);
        }

        public ActionResult XemChiTietSP(int? id)
        {
            int demQTDaXacThuc = 0;
            SanPham sanPham = db.SanPhams.Find(id);
            var quyTrinh = db.QuyTrinhs.Where(x => x.MaSanPham == id).ToList();
            // Duyệt từng quy trình sản xuất sản phẩm và kiểm tra chữ kí
            foreach (var qt in quyTrinh)
            {
                if(qt.TrangThai != 1)
                {
                    demQTDaXacThuc++;
                }
            }
            // Nếu tất cả các quy trình sản xuất đều được xác thực là đúng
            if(demQTDaXacThuc == 0)
            {
                sanPham.TrangThai = 1;
                db.SaveChanges();
            }
            else if(demQTDaXacThuc > 0)
            {
                sanPham.TrangThai = 0;
                db.SaveChanges();
            }
            ViewBag.TrangthaiSP = sanPham.TrangThai;
            ViewBag.TenSP = sanPham.TenSanPham;
            ViewBag.AnhSP = sanPham.HinhAnh;
            ViewBag.MoTa = sanPham.MoTa;
            return View(quyTrinh);
        }

        public void XacThuc()
        {
           

        }
    }
}