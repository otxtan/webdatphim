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
    public class Tb_PhanLoaiPhimController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_PhanLoaiPhim
        public ActionResult Index()
        {
            return View(db.Tb_PhanLoaiPhim.ToList());
        }

        // GET: admin/Tb_PhanLoaiPhim/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_PhanLoaiPhim tb_PhanLoaiPhim = db.Tb_PhanLoaiPhim.Find(id);
            if (tb_PhanLoaiPhim == null)
            {
                return HttpNotFound();
            }
            return View(tb_PhanLoaiPhim);
        }

        // GET: admin/Tb_PhanLoaiPhim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Tb_PhanLoaiPhim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_Loai,TenTiengViet")] Tb_PhanLoaiPhim tb_PhanLoaiPhim)
        {
            if (ModelState.IsValid)
            {
                db.Tb_PhanLoaiPhim.Add(tb_PhanLoaiPhim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_PhanLoaiPhim);
        }

        // GET: admin/Tb_PhanLoaiPhim/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_PhanLoaiPhim tb_PhanLoaiPhim = db.Tb_PhanLoaiPhim.Find(id);
            if (tb_PhanLoaiPhim == null)
            {
                return HttpNotFound();
            }
            return View(tb_PhanLoaiPhim);
        }

        // POST: admin/Tb_PhanLoaiPhim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_Loai,TenTiengViet")] Tb_PhanLoaiPhim tb_PhanLoaiPhim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_PhanLoaiPhim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_PhanLoaiPhim);
        }

        // GET: admin/Tb_PhanLoaiPhim/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_PhanLoaiPhim tb_PhanLoaiPhim = db.Tb_PhanLoaiPhim.Find(id);
            if (tb_PhanLoaiPhim == null)
            {
                return HttpNotFound();
            }
            return View(tb_PhanLoaiPhim);
        }

        // POST: admin/Tb_PhanLoaiPhim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tb_PhanLoaiPhim tb_PhanLoaiPhim = db.Tb_PhanLoaiPhim.Find(id);
            db.Tb_PhanLoaiPhim.Remove(tb_PhanLoaiPhim);
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
