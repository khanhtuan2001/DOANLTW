using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;
using PagedList;

namespace ChickenGang_Project.Controllers
{
    public class SanPhamController : Controller
    {
        dbChickenGang db = new dbChickenGang();

        // GET: SanPham
        //-----------------Sản phầm---------------//
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }

        public ActionResult SanPhamBonus()
        {
            return PartialView();
        }

        public ActionResult D_SanPham()
        {
            
            return PartialView();
        }

        //-------------------Detail--------------//
        public ActionResult Detail(int id)
        {
            var D_SanPham = db.SanPhams.Where(s => s.MaSP == id).First();
            return View(D_SanPham);
        }
       

    }
}