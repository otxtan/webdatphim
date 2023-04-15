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
    public class Tb_PhongController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_Phong
        public ActionResult Index()
        {
            return View(db.Tb_Phong.ToList());
        }

        // GET: admin/Tb_Phong/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Phong tb_Phong = db.Tb_Phong.Find(id);
            if (tb_Phong == null)
            {
                return HttpNotFound();
            }
            return View(tb_Phong);
        }

        // GET: admin/Tb_Phong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Tb_Phong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_PhongChieu,LoaiPhong,SoLuongGhe")] Tb_Phong tb_Phong)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Phong.Add(tb_Phong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_Phong);
        }

        // GET: admin/Tb_Phong/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Phong tb_Phong = db.Tb_Phong.Find(id);
            if (tb_Phong == null)
            {
                return HttpNotFound();
            }
            return View(tb_Phong);
        }

        // POST: admin/Tb_Phong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_PhongChieu,LoaiPhong,SoLuongGhe")] Tb_Phong tb_Phong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Phong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Phong);
        }

        // GET: admin/Tb_Phong/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Phong tb_Phong = db.Tb_Phong.Find(id);
            if (tb_Phong == null)
            {
                return HttpNotFound();
            }
            return View(tb_Phong);
        }

        // POST: admin/Tb_Phong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tb_Phong tb_Phong = db.Tb_Phong.Find(id);
            db.Tb_Phong.Remove(tb_Phong);
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
