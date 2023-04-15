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
    public class Tb_LoaiGheController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_LoaiGhe
        public ActionResult Index()
        {
            return View(db.Tb_LoaiGhe.ToList());
        }

        // GET: admin/Tb_LoaiGhe/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_LoaiGhe tb_LoaiGhe = db.Tb_LoaiGhe.Find(id);
            if (tb_LoaiGhe == null)
            {
                return HttpNotFound();
            }
            return View(tb_LoaiGhe);
        }

        // GET: admin/Tb_LoaiGhe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Tb_LoaiGhe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_LoaiGhe,TenLoai,GiaTien")] Tb_LoaiGhe tb_LoaiGhe)
        {
            if (ModelState.IsValid)
            {
                db.Tb_LoaiGhe.Add(tb_LoaiGhe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_LoaiGhe);
        }

        // GET: admin/Tb_LoaiGhe/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_LoaiGhe tb_LoaiGhe = db.Tb_LoaiGhe.Find(id);
            if (tb_LoaiGhe == null)
            {
                return HttpNotFound();
            }
            return View(tb_LoaiGhe);
        }

        // POST: admin/Tb_LoaiGhe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_LoaiGhe,TenLoai,GiaTien")] Tb_LoaiGhe tb_LoaiGhe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_LoaiGhe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_LoaiGhe);
        }

        // GET: admin/Tb_LoaiGhe/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_LoaiGhe tb_LoaiGhe = db.Tb_LoaiGhe.Find(id);
            if (tb_LoaiGhe == null)
            {
                return HttpNotFound();
            }
            return View(tb_LoaiGhe);
        }

        // POST: admin/Tb_LoaiGhe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tb_LoaiGhe tb_LoaiGhe = db.Tb_LoaiGhe.Find(id);
            db.Tb_LoaiGhe.Remove(tb_LoaiGhe);
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
