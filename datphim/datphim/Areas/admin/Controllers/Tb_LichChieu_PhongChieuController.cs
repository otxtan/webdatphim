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
    public class Tb_LichChieu_PhongChieuController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_LichChieu_PhongChieu
        public ActionResult Index()
        {
            var tb_LichChieu_PhongChieu = db.Tb_LichChieu_PhongChieu.Include(t => t.Tb_phim).Include(t => t.Tb_Phong);
            return View(tb_LichChieu_PhongChieu.ToList());
        }

        // GET: admin/Tb_LichChieu_PhongChieu/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_LichChieu_PhongChieu tb_LichChieu_PhongChieu = db.Tb_LichChieu_PhongChieu.Find(id);
            if (tb_LichChieu_PhongChieu == null)
            {
                return HttpNotFound();
            }
            return View(tb_LichChieu_PhongChieu);
        }

        // GET: admin/Tb_LichChieu_PhongChieu/Create
        public ActionResult Create()
        {
            ViewBag.Ma_Phim = new SelectList(db.Tb_phim, "Ma_Phim", "TenPhim");
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "LoaiPhong");
            return View();
        }

        // POST: admin/Tb_LichChieu_PhongChieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_LichChieu_PhongChieu,Ma_Phim,NgayChieu,GioChieu,Ma_PhongChieu,available")] Tb_LichChieu_PhongChieu tb_LichChieu_PhongChieu)
        {
            if (ModelState.IsValid)
            {
                db.Tb_LichChieu_PhongChieu.Add(tb_LichChieu_PhongChieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ma_Phim = new SelectList(db.Tb_phim, "Ma_Phim", "TenPhim", tb_LichChieu_PhongChieu.Ma_Phim);
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "LoaiPhong", tb_LichChieu_PhongChieu.Ma_PhongChieu);
            return View(tb_LichChieu_PhongChieu);
        }

        // GET: admin/Tb_LichChieu_PhongChieu/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_LichChieu_PhongChieu tb_LichChieu_PhongChieu = db.Tb_LichChieu_PhongChieu.Find(id);
            if (tb_LichChieu_PhongChieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ma_Phim = new SelectList(db.Tb_phim, "Ma_Phim", "TenPhim", tb_LichChieu_PhongChieu.Ma_Phim);
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "LoaiPhong", tb_LichChieu_PhongChieu.Ma_PhongChieu);
            return View(tb_LichChieu_PhongChieu);
        }

        // POST: admin/Tb_LichChieu_PhongChieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_LichChieu_PhongChieu,Ma_Phim,NgayChieu,GioChieu,Ma_PhongChieu,available")] Tb_LichChieu_PhongChieu tb_LichChieu_PhongChieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_LichChieu_PhongChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ma_Phim = new SelectList(db.Tb_phim, "Ma_Phim", "TenPhim", tb_LichChieu_PhongChieu.Ma_Phim);
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "LoaiPhong", tb_LichChieu_PhongChieu.Ma_PhongChieu);
            return View(tb_LichChieu_PhongChieu);
        }

        // GET: admin/Tb_LichChieu_PhongChieu/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_LichChieu_PhongChieu tb_LichChieu_PhongChieu = db.Tb_LichChieu_PhongChieu.Find(id);
            if (tb_LichChieu_PhongChieu == null)
            {
                return HttpNotFound();
            }
            return View(tb_LichChieu_PhongChieu);
        }

        // POST: admin/Tb_LichChieu_PhongChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Tb_LichChieu_PhongChieu tb_LichChieu_PhongChieu = db.Tb_LichChieu_PhongChieu.Find(id);
            db.Tb_LichChieu_PhongChieu.Remove(tb_LichChieu_PhongChieu);
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
