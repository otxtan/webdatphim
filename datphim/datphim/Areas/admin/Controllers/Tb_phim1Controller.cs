using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using datphim.Models;

namespace datphim.Areas.admin.Controllers
{
    public class Tb_phim1Controller : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_phim1
        public ActionResult Index()
        {
            var tb_phim = db.Tb_phim.Include(t => t.Tb_PhanLoaiPhim).Include(t => t.Tb_TheLoai);
            return View(tb_phim.ToList());
        }

        // GET: admin/Tb_phim1/Details/5
        public ActionResult Details(long? id)
        {
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

        // GET: admin/Tb_phim1/Create
        public ActionResult Create()
        {
            ViewBag.Ma_Loai = new SelectList(db.Tb_PhanLoaiPhim, "Ma_Loai", "TenTiengViet");
            ViewBag.Ma_TheLoai = new SelectList(db.Tb_TheLoai, "Ma_TheLoai", "TenTiengViet");
            return View();
        }

        // POST: admin/Tb_phim1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_Phim,TenPhim,ChatLuong,Ma_Loai,Ma_TheLoai,DaoDien,DienVien,NgayKhoiChieu,ThoiLuong,NgonNgu,NoiDung,AnhBia,Trailer,available")] Tb_phim tb_phim)
        {
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

        // GET: admin/Tb_phim1/Edit/5
        public ActionResult Edit(long? id)
        {
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

        // POST: admin/Tb_phim1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_Phim,TenPhim,ChatLuong,Ma_Loai,Ma_TheLoai,DaoDien,DienVien,NgayKhoiChieu,ThoiLuong,NgonNgu,NoiDung,AnhBia,Trailer,available")] Tb_phim tb_phim)
        {
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

        // GET: admin/Tb_phim1/Delete/5
        public ActionResult Delete(long? id)
        {
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

        // POST: admin/Tb_phim1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
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
