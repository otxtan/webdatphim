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
    public class Tb_TheLoaiController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_TheLoai
        public ActionResult Index()
        {
            return View(db.Tb_TheLoai.ToList());
        }

        // GET: admin/Tb_TheLoai/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_TheLoai tb_TheLoai = db.Tb_TheLoai.Find(id);
            if (tb_TheLoai == null)
            {
                return HttpNotFound();
            }
            return View(tb_TheLoai);
        }

        // GET: admin/Tb_TheLoai/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Tb_TheLoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma_TheLoai,TenTiengViet")] Tb_TheLoai tb_TheLoai)
        {
            if (ModelState.IsValid)
            {
                db.Tb_TheLoai.Add(tb_TheLoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_TheLoai);
        }

        // GET: admin/Tb_TheLoai/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_TheLoai tb_TheLoai = db.Tb_TheLoai.Find(id);
            if (tb_TheLoai == null)
            {
                return HttpNotFound();
            }
            return View(tb_TheLoai);
        }

        // POST: admin/Tb_TheLoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma_TheLoai,TenTiengViet")] Tb_TheLoai tb_TheLoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_TheLoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_TheLoai);
        }

        // GET: admin/Tb_TheLoai/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_TheLoai tb_TheLoai = db.Tb_TheLoai.Find(id);
            if (tb_TheLoai == null)
            {
                return HttpNotFound();
            }
            return View(tb_TheLoai);
        }

        // POST: admin/Tb_TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tb_TheLoai tb_TheLoai = db.Tb_TheLoai.Find(id);
            db.Tb_TheLoai.Remove(tb_TheLoai);
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
