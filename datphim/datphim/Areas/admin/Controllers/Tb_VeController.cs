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
    public class Tb_VeController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_Ve
        public ActionResult Index()
        {
            var tb_Ve = db.Tb_Ve.Include(t => t.Tb_HoaDon).Include(t => t.Tb_LichChieu_PhongChieu).Include(t => t.Tb_PhongGhe);
            return View(tb_Ve.ToList());
        }

        // GET: admin/Tb_Ve/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Ve tb_Ve = db.Tb_Ve.Find(id);
            if (tb_Ve == null)
            {
                return HttpNotFound();
            }
            return View(tb_Ve);
        }

        // GET: admin/Tb_Ve/Create
        public ActionResult Create()
        {
            ViewBag.Ma_HoaDon = new SelectList(db.Tb_HoaDon, "Ma_HoaDon", "UserName");
            ViewBag.Ma_LichChieu_PhongChieu = new SelectList(db.Tb_LichChieu_PhongChieu, "Ma_LichChieu_PhongChieu", "Ma_PhongChieu");
            ViewBag.Ma_PhongGhe = new SelectList(db.Tb_PhongGhe, "Ma_PhongGhe", "Ma_PhongChieu");
            return View();
        }

        // POST: admin/Tb_Ve/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_ve,Ma_HoaDon,Ma_LichChieu_PhongChieu,Ma_PhongGhe")] Tb_Ve tb_Ve)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Ve.Add(tb_Ve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ma_HoaDon = new SelectList(db.Tb_HoaDon, "Ma_HoaDon", "UserName", tb_Ve.Ma_HoaDon);
            ViewBag.Ma_LichChieu_PhongChieu = new SelectList(db.Tb_LichChieu_PhongChieu, "Ma_LichChieu_PhongChieu", "Ma_PhongChieu", tb_Ve.Ma_LichChieu_PhongChieu);
            ViewBag.Ma_PhongGhe = new SelectList(db.Tb_PhongGhe, "Ma_PhongGhe", "Ma_PhongChieu", tb_Ve.Ma_PhongGhe);
            return View(tb_Ve);
        }

        // GET: admin/Tb_Ve/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Ve tb_Ve = db.Tb_Ve.Find(id);
            if (tb_Ve == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ma_HoaDon = new SelectList(db.Tb_HoaDon, "Ma_HoaDon", "UserName", tb_Ve.Ma_HoaDon);
            ViewBag.Ma_LichChieu_PhongChieu = new SelectList(db.Tb_LichChieu_PhongChieu, "Ma_LichChieu_PhongChieu", "Ma_PhongChieu", tb_Ve.Ma_LichChieu_PhongChieu);
            ViewBag.Ma_PhongGhe = new SelectList(db.Tb_PhongGhe, "Ma_PhongGhe", "Ma_PhongChieu", tb_Ve.Ma_PhongGhe);
            return View(tb_Ve);
        }

        // POST: admin/Tb_Ve/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_ve,Ma_HoaDon,Ma_LichChieu_PhongChieu,Ma_PhongGhe")] Tb_Ve tb_Ve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Ve).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ma_HoaDon = new SelectList(db.Tb_HoaDon, "Ma_HoaDon", "UserName", tb_Ve.Ma_HoaDon);
            ViewBag.Ma_LichChieu_PhongChieu = new SelectList(db.Tb_LichChieu_PhongChieu, "Ma_LichChieu_PhongChieu", "Ma_PhongChieu", tb_Ve.Ma_LichChieu_PhongChieu);
            ViewBag.Ma_PhongGhe = new SelectList(db.Tb_PhongGhe, "Ma_PhongGhe", "Ma_PhongChieu", tb_Ve.Ma_PhongGhe);
            return View(tb_Ve);
        }

        // GET: admin/Tb_Ve/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Ve tb_Ve = db.Tb_Ve.Find(id);
            if (tb_Ve == null)
            {
                return HttpNotFound();
            }
            return View(tb_Ve);
        }

        // POST: admin/Tb_Ve/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Tb_Ve tb_Ve = db.Tb_Ve.Find(id);
            db.Tb_Ve.Remove(tb_Ve);
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
