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
    public class Tb_HoaDonController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_HoaDon
        public ActionResult Index()
        {
            var tb_HoaDon = db.Tb_HoaDon.Include(t => t.Tb_LichChieu_PhongChieu).Include(t => t.Tb_NguoiDung);
            return View(tb_HoaDon.ToList());
        }

        // GET: admin/Tb_HoaDon/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_HoaDon tb_HoaDon = db.Tb_HoaDon.Find(id);
            if (tb_HoaDon == null)
            {
                return HttpNotFound();
            }
            return View(tb_HoaDon);
        }

        // GET: admin/Tb_HoaDon/Create
        public ActionResult Create()
        {
            ViewBag.Ma_LichChieu_PhongChieu = new SelectList(db.Tb_LichChieu_PhongChieu, "Ma_LichChieu_PhongChieu", "Ma_LichChieu_PhongChieu");
            ViewBag.Ma_LoaiGhe = new SelectList(db.Tb_LoaiGhe, "Ma_LoaiGhe", "TenLoai");
            ViewBag.UserName = new SelectList(db.Tb_NguoiDung, "UserName", "UserName");
            return View();
        }

        // POST: admin/Tb_HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_HoaDon,UserName,Ma_LichChieu_PhongChieu,Ma_LoaiGhe,GiaTien,SoLuong,NgayTao,ThanhTien")] Tb_HoaDon tb_HoaDon)
        {
            if (ModelState.IsValid)
            {
                tb_HoaDon.NgayTao = DateTime.UtcNow;
                db.Tb_HoaDon.Add(tb_HoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ma_LichChieu_PhongChieu = new SelectList(db.Tb_LichChieu_PhongChieu, "Ma_LichChieu_PhongChieu", "Ma_PhongChieu", tb_HoaDon.Ma_LichChieu_PhongChieu);
            
            ViewBag.UserName = new SelectList(db.Tb_NguoiDung, "UserName", "PassWord", tb_HoaDon.UserName);
            return View(tb_HoaDon);
        }

        // GET: admin/Tb_HoaDon/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_HoaDon tb_HoaDon = db.Tb_HoaDon.Find(id);
            if (tb_HoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ma_LichChieu_PhongChieu = new SelectList(db.Tb_LichChieu_PhongChieu, "Ma_LichChieu_PhongChieu", "Ma_PhongChieu", tb_HoaDon.Ma_LichChieu_PhongChieu);
            
            ViewBag.UserName = new SelectList(db.Tb_NguoiDung, "UserName", "PassWord", tb_HoaDon.UserName);
            return View(tb_HoaDon);
        }

        // POST: admin/Tb_HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_HoaDon,UserName,Ma_LichChieu_PhongChieu,Ma_LoaiGhe,GiaTien,SoLuong,NgayTao,ThanhTien")] Tb_HoaDon tb_HoaDon)
        {
            if (ModelState.IsValid)
            {
                tb_HoaDon.NgayTao = DateTime.Now;
                db.Entry(tb_HoaDon).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ma_LichChieu_PhongChieu = new SelectList(db.Tb_LichChieu_PhongChieu, "Ma_LichChieu_PhongChieu", "Ma_PhongChieu", tb_HoaDon.Ma_LichChieu_PhongChieu);
            
            ViewBag.UserName = new SelectList(db.Tb_NguoiDung, "UserName", "PassWord", tb_HoaDon.UserName);
            return View(tb_HoaDon);
        }

        // GET: admin/Tb_HoaDon/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_HoaDon tb_HoaDon = db.Tb_HoaDon.Find(id);
            if (tb_HoaDon == null)
            {
                return HttpNotFound();
            }
            return View(tb_HoaDon);
        }

        // POST: admin/Tb_HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Tb_HoaDon tb_HoaDon = db.Tb_HoaDon.Find(id);
            db.Tb_HoaDon.Remove(tb_HoaDon);
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
