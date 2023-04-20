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
    [customFilter]
    public class Tb_ThanhToanController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_ThanhToan
        public ActionResult Index()
        {
            return View(db.Tb_ThanhToan.ToList());
        }

        // GET: admin/Tb_ThanhToan/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_ThanhToan tb_ThanhToan = db.Tb_ThanhToan.Find(id);
            if (tb_ThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(tb_ThanhToan);
        }

        // GET: admin/Tb_ThanhToan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Tb_ThanhToan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_TT,TenTiengViet,TenNguoiNhan,SoTaiKhoan")] Tb_ThanhToan tb_ThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ThanhToan.Add(tb_ThanhToan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_ThanhToan);
        }

        // GET: admin/Tb_ThanhToan/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_ThanhToan tb_ThanhToan = db.Tb_ThanhToan.Find(id);
            if (tb_ThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(tb_ThanhToan);
        }

        // POST: admin/Tb_ThanhToan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_TT,TenTiengViet,TenNguoiNhan,SoTaiKhoan")] Tb_ThanhToan tb_ThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_ThanhToan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_ThanhToan);
        }

        // GET: admin/Tb_ThanhToan/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_ThanhToan tb_ThanhToan = db.Tb_ThanhToan.Find(id);
            if (tb_ThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(tb_ThanhToan);
        }

        // POST: admin/Tb_ThanhToan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long? id)
        {
            Tb_ThanhToan tb_ThanhToan = db.Tb_ThanhToan.Find(id);
            db.Tb_ThanhToan.Remove(tb_ThanhToan);
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
