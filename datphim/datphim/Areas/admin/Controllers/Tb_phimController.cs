using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using datphim.Models;

namespace datphim.Areas.admin.Controllers
{
    public class Tb_phimController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_phim
        public ActionResult Index()
        {
          
              
             if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home", new { area = "" });
            string user = Session["UserName"].ToString().Trim();
            if (!user.Equals("admin"))
                return RedirectToAction("TrangChu", "Home", new { area = "" });
            

                var tb_phim = db.Tb_phim.Include(t => t.Tb_PhanLoaiPhim).Include(t => t.Tb_TheLoai);
            return View(tb_phim.ToList());
        }

        // GET: admin/Tb_phim/Details/5
        public ActionResult Details(long? id)
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home", new { area = "" });
            string user = Session["UserName"].ToString().Trim();
            if (!user.Equals("admin"))
                return RedirectToAction("TrangChu", "Home", new { area = "" });
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_phim tb_phim = db.Tb_phim.Find(id);
            if (tb_phim == null)
            {
                return HttpNotFound();
            }
            return View(tb_phim);
        }

        // GET: admin/Tb_phim/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home", new { area = "" });
            string user = Session["UserName"].ToString().Trim();
            if (!user.Equals("admin"))
                return RedirectToAction("TrangChu", "Home", new { area = "" });
            ViewBag.Ma_Loai = new SelectList(db.Tb_PhanLoaiPhim, "Ma_Loai", "TenTiengViet");
            ViewBag.Ma_TheLoai = new SelectList(db.Tb_TheLoai, "Ma_TheLoai", "TenTiengViet");
            return View();
        }

        // POST: admin/Tb_phim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_Phim,TenPhim,ChatLuong,Ma_Loai,Ma_TheLoai,DaoDien,DienVien,NgayKhoiChieu,ThoiLuong,NgonNgu,NoiDung,AnhBia,Trailer,available")] Tb_phim tb_phim,HttpPostedFileBase Fileupload)
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home", new { area = "" });
            string user = Session["UserName"].ToString().Trim();
            if (!user.Equals("admin"))
                return RedirectToAction("TrangChu", "Home", new { area = "" });
            if (Fileupload == null)
            {
                ViewBag.ThongBao = "Yêu Cầu Nhập Hình Ảnh";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(Fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    /*
                    var filename_2 = Path.GetFileName(Fileupload_2.FileName);
                    var path_2 = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    */

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình Ảnh đã tồn tại";
                    }
                    /*
                    else
                     if (System.IO.File.Exists(path_2))
                    {
                        ViewBag.ThongBao = "Hình Ảnh đã tồn tại";
                    }
                    */
                    else
                    {
                        Fileupload.SaveAs(path);
                        //Fileupload_2.SaveAs(path_2);
                    }

                    tb_phim.AnhBia = filename;

                }
            }
            if (ModelState.IsValid)
            {
                db.Tb_phim.Add(tb_phim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ma_Loai = new SelectList(db.Tb_PhanLoaiPhim, "Ma_Loai", "TenTiengViet", tb_phim.Ma_Loai);
            ViewBag.Ma_TheLoai = new SelectList(db.Tb_TheLoai, "Ma_TheLoai", "TenTiengViet", tb_phim.Ma_TheLoai);
            return View(tb_phim);
        }

        // GET: admin/Tb_phim/Edit/5
        public ActionResult Edit(long? id)
        {
            if (Session["UserName"] == null)
            {
                string user = Session["UserName"].ToString();
                if (user.Equals("admin"))
                    return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_phim tb_phim = db.Tb_phim.Find(id);
            if (tb_phim == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ma_Loai = new SelectList(db.Tb_PhanLoaiPhim, "Ma_Loai", "TenTiengViet", tb_phim.Ma_Loai);
            ViewBag.Ma_TheLoai = new SelectList(db.Tb_TheLoai, "Ma_TheLoai", "TenTiengViet", tb_phim.Ma_TheLoai);
            return View(tb_phim);
        }

        // POST: admin/Tb_phim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_Phim,TenPhim,ChatLuong,Ma_Loai,Ma_TheLoai,DaoDien,DienVien,NgayKhoiChieu,ThoiLuong,NgonNgu,NoiDung,AnhBia,Trailer,available")] Tb_phim tb_phim,HttpPostedFileBase Fileupload)
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home", new { area = "" });
            string user = Session["UserName"].ToString().Trim();
            if (!user.Equals("admin"))
                return RedirectToAction("TrangChu", "Home", new { area = "" });
            if (Fileupload == null)
            {
                var Nameimg = from s in db.Tb_phim where s.Ma_Phim == tb_phim.Ma_Phim select s.AnhBia;
               // var filename = Path.GetFileName(Fileupload.FileName);
                tb_phim.AnhBia = (Nameimg.FirstOrDefault()).ToString();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(Fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), filename);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình Ảnh đã tồn tại";
                    }
                  
                    else
                    {
                        Fileupload.SaveAs(path);
                        //Fileupload_2.SaveAs(path_2);
                    }

                    tb_phim.AnhBia = filename;

                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(tb_phim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ma_Loai = new SelectList(db.Tb_PhanLoaiPhim, "Ma_Loai", "TenTiengViet", tb_phim.Ma_Loai);
            ViewBag.Ma_TheLoai = new SelectList(db.Tb_TheLoai, "Ma_TheLoai", "TenTiengViet", tb_phim.Ma_TheLoai);
            return View(tb_phim);
        }

        // GET: admin/Tb_phim/Delete/5
        public ActionResult Delete(long? id)
        {
            if (Session["UserName"] == null)
            {
                string user = Session["UserName"].ToString();
                if (user.Equals("admin"))
                    return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_phim tb_phim = db.Tb_phim.Find(id);
            if (tb_phim == null)
            {
                return HttpNotFound();
            }
            return View(tb_phim);
        }

        // POST: admin/Tb_phim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (Session["UserName"] == null)
            {
                string user = Session["UserName"].ToString();
                if (user.Equals("admin"))
                    return RedirectToAction("Login");
            }
            Tb_phim tb_phim = db.Tb_phim.Find(id);
            db.Tb_phim.Remove(tb_phim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
