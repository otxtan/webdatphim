using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using datphim.Models;

namespace datphim.Controllers
{
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan
        private datphimchuanEntities db = new datphimchuanEntities();
        public ActionResult Index()
        {
            string user = Session["UserName"].ToString();
            long maHD = Convert.ToInt64(((from s in db.Tb_HoaDon.OrderByDescending(s => s.Ma_HoaDon) where s.UserName == user select s.Ma_HoaDon)).FirstOrDefault());
            var tb_Ve1 = db.Tb_Ve.Include(t => t.Tb_HoaDon).Include(t => t.Tb_LichChieu_PhongChieu).Include(t => t.Tb_PhongGhe).Where(s => s.Ma_HoaDon == maHD);
            var tb_HoaDon = db.Tb_HoaDon.Include(t => t.Tb_LichChieu_PhongChieu).Include(t => t.Tb_NguoiDung).Where(s => s.UserName == user).Where(s=>s.Ma_HoaDon==maHD);
            ViewBag.Hoadon = tb_HoaDon.ToList();
            return View(tb_Ve1.ToList());
        }
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string lstGhe = formCollection["cho"];
            string[] chongoi = lstGhe.Split(',');
            // kiem tra ghe ton tai ?
            
            string Ma_LichChieu_PhongChieu1 = "";
            string Ma_PhongChieu = "";
            Tb_HoaDon tb_HoaDon = new Tb_HoaDon();
                string user = Session["UserName"].ToString();
            
            //string[] tenChoNgoi=null;
            string ngaychieu = "";
                long maHD = 0;
            string giochieu = "";
            if (TempData.ContainsKey("Ma_LichChieu_PhongChieu") && TempData.ContainsKey("Ma_PhongChieu"))
            {

                Ma_LichChieu_PhongChieu1 = TempData["Ma_LichChieu_PhongChieu"].ToString();

                Ma_PhongChieu = TempData["Ma_PhongChieu"].ToString();
                // tách chỗ
                
                //tenChoNgoi = new string[chongoi.Length];
                long tongtien = 0;
                //lấy lịch, suất chiếu
                long maLichChieu = Convert.ToInt64(Ma_LichChieu_PhongChieu1);
                
              
                for (int i = 0; i < chongoi.Length; i++)
                {
                    string mpg = chongoi[i];
                    long mpg1 = Convert.ToInt64(mpg);
                    long tontai = Convert.ToInt64((from s in db.Tb_Ve.Where(s => s.Ma_LichChieu_PhongChieu == maLichChieu).Where(s=>s.Ma_PhongGhe==mpg1) select s).Count());
                    if (tontai > 0)
                    {
                        TempData["thongbao"] = "Bạn quá chậm đã có người nhanh tay hơn bạn!";
                        return RedirectToAction("ChonGhe","Home",new { Ma_LichChieu_PhongChieu =Convert.ToInt64(Ma_LichChieu_PhongChieu1), Ma_PhongChieu });

                    }    
                    string loaighe = (from s in db.Tb_PhongGhe.Where(s => s.Ma_PhongGhe == mpg1) select s.Ma_LoaiGhe).SingleOrDefault().ToString();
                    // tenChoNgoi[i] = (from s in db.Tb_PhongGhe.Where(s => s.Ma_PhongGhe == mpg1) select s.TenGhe).SingleOrDefault().ToString();
                    long tien = Convert.ToInt64((from s in db.Tb_LoaiGhe.Where(s => s.Ma_LoaiGhe == loaighe) select s.GiaTien).SingleOrDefault());
                    tongtien += tien;

                }
                tb_HoaDon.UserName = user;
                tb_HoaDon.Ma_LichChieu_PhongChieu = maLichChieu;
                tb_HoaDon.GiaTien = tongtien;
                tb_HoaDon.SoLuong = chongoi.Length;
                tb_HoaDon.NgayTao = DateTime.Now;
                tb_HoaDon.SoLuong = chongoi.Length;
                tb_HoaDon.ThanhTien = tongtien;
                db.Tb_HoaDon.Add(tb_HoaDon);
                db.SaveChanges();
                maHD = Convert.ToInt64(((from s in db.Tb_HoaDon.OrderByDescending(s => s.Ma_HoaDon) where s.UserName == user select s.Ma_HoaDon)).FirstOrDefault());
                //add vé
                for (int i = 0; i < chongoi.Length; i++)
                {
                    Tb_Ve tb_Ve = new Tb_Ve();
                    tb_Ve.Ma_HoaDon = maHD;
                    tb_Ve.Ma_LichChieu_PhongChieu = maLichChieu;
                    tb_Ve.Ma_PhongGhe = Convert.ToInt64(chongoi[i]);
                    db.Tb_Ve.Add(tb_Ve);
                    db.SaveChanges();
                }

            }
            else
                return RedirectToAction("Index");
             var tb_Ve1 = db.Tb_Ve.Include(t => t.Tb_HoaDon).Include(t => t.Tb_LichChieu_PhongChieu).Include(t => t.Tb_PhongGhe).Where(s=>s.Ma_HoaDon==maHD).ToList();
           
            var tb_HoaDon1 = db.Tb_HoaDon.Include(t => t.Tb_LichChieu_PhongChieu).Include(t => t.Tb_NguoiDung).Where(s => s.UserName == user).Where(s => s.Ma_HoaDon == maHD);
            ViewBag.Hoadon = tb_HoaDon1.ToList() ;
           
            return View(tb_Ve1.ToList());
        }    
      
    }
}