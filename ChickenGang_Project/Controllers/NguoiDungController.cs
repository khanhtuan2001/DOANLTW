using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;
using CaptchaMvc;
using CaptchaMvc.HtmlHelpers;


namespace ChickenGang_Project.Controllers
{
    public class NguoiDungController : Controller
    {
        dbChickenGang db = new dbChickenGang();

        // GET: NguoiDung



        //Dang Ky
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangky(FormCollection f, ThanhVien tv)
        {

            var HoTen = f["HoTen"];
            var TaiKhoan = f["TaiKhoan"];
            var MatKhau = f["MatKhau"];
            var XacNhanMatKhau = f["XacNhanMatKhau"];
            var DiaChi = f["DiaChi"];
            var Email = f["Email"];
            var SoDienThoai = f["SoDienThoai"];
            var CauHoi = f["CauHoi"];
            var CauTraLoi = f["CauTraLoi"];


            // Code for validating the CAPTCHA  
            if (this.IsCaptchaValid("Captcha is not valid"))
            {

                ViewBag.ThongBao = "Thêm thành công";
                if (String.IsNullOrEmpty(XacNhanMatKhau))
                {
                    ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
                }
                else
                {
                    if (!MatKhau.Equals(XacNhanMatKhau))
                    {
                        ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                    }
                    else
                    {
                        tv.HoTen = HoTen;
                        tv.TaiKhoan = TaiKhoan;
                        tv.MatKhau = MatKhau;
                        tv.DiaChi = DiaChi;
                        tv.Email = Email;
                        tv.SoDienThoai = SoDienThoai;
                        tv.CauHoi = CauHoi;
                        tv.CauTraLoi = CauTraLoi;
                        tv.MaLoaiTV = 1;
                      
                        db.ThanhViens.Add(tv);
                        db.SaveChanges();

                        //return RedirectToAction("DangNhap");
                        return View();

                    }
                }
                
            }
            ViewBag.ThongBao = "Sai mã Captcha";

            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        //Dang Nhap
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //Kiem tra UserName & Password
            string sTaiKhoan = f["uname"];
            string sMatKhau = f["psw"];

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
          
            //Account ac = db.Accounts.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if ( tv != null)
            {
               
                if(Session["MaLoaiAccount"]== tv)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Session["TaiKhoan"] = tv;
                    return RedirectToAction("Index", "Home");
                }
        
               

            }


            return Content("Tài khoản hoặc mật khẩu không đúng!!!");
        }

        public ActionResult QuayLai()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}