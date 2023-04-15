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
    public class Tb_PhongGheController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_PhongGhe
        public ActionResult Index(Tb_PhongGhe tp1)
        {
             var tb_PhongGhe = db.Tb_PhongGhe.Include(t => t.Tb_LoaiGhe).Include(t => t.Tb_Phong);
               
                tb_PhongGhe = (from s in tb_PhongGhe where s.Ma_PhongChieu.Contains(tp1.Ma_PhongChieu) select s);
          
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "Ma_PhongChieu");
            return View(tb_PhongGhe.ToList());
        }

        // GET: admin/Tb_PhongGhe/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_PhongGhe tb_PhongGhe = db.Tb_PhongGhe.Find(id);
            if (tb_PhongGhe == null)
            {
                return HttpNotFound();
            }
            return View(tb_PhongGhe);
        }

        // GET: admin/Tb_PhongGhe/Create
        public ActionResult Create()
        {
            ViewBag.Ma_LoaiGhe = new SelectList(db.Tb_LoaiGhe, "Ma_LoaiGhe", "TenLoai");
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "Ma_PhongChieu");
            return View();
        }

        // POST: admin/Tb_PhongGhe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_PhongGhe,Ma_PhongChieu,TenGhe,Ma_LoaiGhe")] Tb_PhongGhe tb_PhongGhe)
        {
            if (ModelState.IsValid)
            {
                db.Tb_PhongGhe.Add(tb_PhongGhe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ma_LoaiGhe = new SelectList(db.Tb_LoaiGhe, "Ma_LoaiGhe", "TenLoai", tb_PhongGhe.Ma_LoaiGhe);
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "LoaiPhong", tb_PhongGhe.Ma_PhongChieu);
            return View(tb_PhongGhe);
        }

        // GET: admin/Tb_PhongGhe/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_PhongGhe tb_PhongGhe = db.Tb_PhongGhe.Find(id);
            if (tb_PhongGhe == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ma_LoaiGhe = new SelectList(db.Tb_LoaiGhe, "Ma_LoaiGhe", "TenLoai", tb_PhongGhe.Ma_LoaiGhe);
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "Ma_PhongChieu", tb_PhongGhe.Ma_PhongChieu);
            return View(tb_PhongGhe);
        }

        // POST: admin/Tb_PhongGhe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_PhongGhe,Ma_PhongChieu,TenGhe,Ma_LoaiGhe")] Tb_PhongGhe tb_PhongGhe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_PhongGhe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ma_LoaiGhe = new SelectList(db.Tb_LoaiGhe, "Ma_LoaiGhe", "TenLoai", tb_PhongGhe.Ma_LoaiGhe);
            ViewBag.Ma_PhongChieu = new SelectList(db.Tb_Phong, "Ma_PhongChieu", "LoaiPhong", tb_PhongGhe.Ma_PhongChieu);
            return View(tb_PhongGhe);
        }

        // GET: admin/Tb_PhongGhe/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_PhongGhe tb_PhongGhe = db.Tb_PhongGhe.Find(id);
            if (tb_PhongGhe == null)
            {
                return HttpNotFound();
            }
            return View(tb_PhongGhe);
        }

        // POST: admin/Tb_PhongGhe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Tb_PhongGhe tb_PhongGhe = db.Tb_PhongGhe.Find(id);
            db.Tb_PhongGhe.Remove(tb_PhongGhe);
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
