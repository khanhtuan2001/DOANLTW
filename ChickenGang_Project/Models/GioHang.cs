using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ChickenGang_Project.Models;

namespace ChickenGang_Project.Models
{
    public class GioHang
    {
        dbChickenGang data = new dbChickenGang();
        public int id { get; set; }
        [Display(Name = "Tên ")]
        public string ten { get; set; }
        [Display(Name = "Ảnh bia")]
        public string hinh { get; set; }
        [Display(Name = "Giá bán")]
        public Double giaban { get; set; }
        [Display(Name = "Số lượng")]
        public int isoluong { get; set; }
        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return isoluong * giaban; }
        }


        public GioHang(int id)
        {
            this.id = id;
            SanPham rubik = data.SanPhams.Single(n => n.MaSP == id);
            ten = rubik.TenSP;
            hinh = rubik.HinhAnh;
            giaban = double.Parse(rubik.DonGia.ToString());
            isoluong = 1;
        }
    }
}