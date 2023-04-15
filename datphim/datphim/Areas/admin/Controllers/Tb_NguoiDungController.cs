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
    public class Tb_NguoiDungController : Controller
    {
        private datphimchuanEntities db = new datphimchuanEntities();

        // GET: admin/Tb_NguoiDung
        public ActionResult Index()
        {
            return View(db.Tb_NguoiDung.ToList());
        }

        // GET: admin/Tb_NguoiDung/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(id);
            if (tb_NguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(tb_NguoiDung);
        }

        // GET: admin/Tb_NguoiDung/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Tb_NguoiDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,PassWord,TenKH,SDT,NgaySinh,Email")] Tb_NguoiDung tb_NguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Tb_NguoiDung.Add(tb_NguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_NguoiDung);
        }

        // GET: admin/Tb_NguoiDung/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(id);
            if (tb_NguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(tb_NguoiDung);
        }

        // POST: admin/Tb_NguoiDung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,PassWord,TenKH,SDT,NgaySinh,Email")] Tb_NguoiDung tb_NguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_NguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_NguoiDung);
        }

        // GET: admin/Tb_NguoiDung/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(id);
            if (tb_NguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(tb_NguoiDung);
        }

        // POST: admin/Tb_NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tb_NguoiDung tb_NguoiDung = db.Tb_NguoiDung.Find(id);
            db.Tb_NguoiDung.Remove(tb_NguoiDung);
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
