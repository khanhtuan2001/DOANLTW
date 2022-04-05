using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;

namespace ChickenGang_Project.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbChickenGang data = new dbChickenGang();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.id == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.isoluong);
            }
            return tsl;
        }
        private int TongSoluongSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;

        }

        private double TongTien()
        {
            double tt = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluon = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.id == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(collection["txtSolg"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //if (Session["TaiKhoan"] == null || Session["Taikhoan"].ToString() == "")
            //{
            //    return RedirectToAction("DangNhap", "NguoiDung");
            //}
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return View(lstGiohang);
        }

        public ActionResult DatHang(FormCollection collection)
        {
            DonDatHang dh = new DonDatHang();
            ThanhVien kh = (ThanhVien)Session["Taikhoan"];

            if (Session["TaiKhoan"] == null)
            {
                KhachHang kk = new KhachHang();
                kk.TenKH = collection["tenkh"];
                kk.Email = collection["email"];
                var diachi = collection["diachigiao"];
                kk.DiaChi = diachi;
                kk.SoDienThoai = collection["sdt"];
                data.KhachHangs.Add(kk);
                data.SaveChanges();
                SanPham s = new SanPham();
                List<GioHang> gh = Laygiohang();
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);

                dh.DiaChiGiaoHang = diachi;
                dh.MaKH = kk.MaKH;
                dh.NgayDat = DateTime.Now;
                dh.NgayGiao = DateTime.Parse(ngaygiao);
                dh.TinhTrangGiaoHang = false;
                dh.DaThanhToan = false;
                data.DonDatHangs.Add(dh);
                data.SaveChanges();
                foreach (var item in gh)
                {
                    ChiTietDonDat ctdh = new ChiTietDonDat();
                    ctdh.MaDDH = dh.MaDDH;
                    ctdh.TenSP = item.ten;
                    ctdh.MaSP = item.id;
                    ctdh.SoLuong = item.isoluong;
                    ctdh.DonGia = (decimal)item.giaban;
                    s = data.SanPhams.Single(n => n.MaSP == item.id);
                    s.SoLuongTon -= ctdh.SoLuong;
                    data.SaveChanges();
                    data.ChiTietDonDats.Add(ctdh);

                }
                data.SaveChanges();
                Session["GioHang"] = null;
            }
            else if (Session["TaiKhoan"] != null)
            {
                KhachHang kk = new KhachHang();

                SanPham s = new SanPham();
                List<GioHang> gh = Laygiohang();
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
                var diachi = collection["diachigiao"];
                dh.DiaChiGiaoHang = diachi;
                //dh.MaKH = kh.MaThanhVien;
                dh.NgayDat = DateTime.Now;
                dh.NgayGiao = DateTime.Parse(ngaygiao);
                dh.TinhTrangGiaoHang = false;
                dh.DaThanhToan = false;
                kk.TenKH = kh.HoTen;
                kk.Email = kh.Email;
                kk.SoDienThoai = kh.SoDienThoai;
                kk.DiaChi = kh.DiaChi;
                data.KhachHangs.Add(kk);
                data.DonDatHangs.Add(dh);
                data.SaveChanges();
                foreach (var item in gh)
                {
                    ChiTietDonDat ctdh = new ChiTietDonDat();
                    ctdh.MaDDH = dh.MaDDH;
                    ctdh.MaSP = item.id;
                    ctdh.SoLuong = item.isoluong;
                    ctdh.DonGia = (decimal)item.giaban;
                    s = data.SanPhams.Single(n => n.MaSP == item.id);
                    s.SoLuongTon -= ctdh.SoLuong;
                    data.SaveChanges();
                    data.ChiTietDonDats.Add(ctdh);

                }
                data.SaveChanges();
                Session["GioHang"] = null;
            }


            return RedirectToAction("XacnhanDonhang", "GioHang");
        }
        public ActionResult xacnhandonhang()
        {
            return View();
        }
    }
}