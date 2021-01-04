using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLyKhachSan.Models;

namespace QLyKhachSan.Areas.Admin.Controllers
{
    public class PHONGTHUEsController : Controller
    {
        private QuanLyKhachSanEntities db = new QuanLyKhachSanEntities();

        // GET: Admin/PHONGTHUEs
        public ActionResult Index()
        {
            var pHONGTHUEs = db.PHONGTHUEs.Include(p => p.KHACHHANG).Include(p => p.PHONG);
            return View(pHONGTHUEs.ToList());
        }

        // GET: Admin/PHONGTHUEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGTHUE pHONGTHUE = db.PHONGTHUEs.Find(id);
            if (pHONGTHUE == null)
            {
                return HttpNotFound();
            }
            return View(pHONGTHUE);
        }

        // GET: Admin/PHONGTHUEs/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MAKH = new SelectList(db.KHACHHANGs, "MAKH", "TENKH");
        //    ViewBag.SOPHONG = new SelectList(db.PHONGs, "SOPHONG", "LOAIPHONG");
        //    return View();
        //}

        // POST: Admin/PHONGTHUEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
 

        // GET: Admin/PHONGTHUEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGTHUE pHONGTHUE = db.PHONGTHUEs.Find(id);
            if (pHONGTHUE == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAKH = new SelectList(db.KHACHHANGs, "MAKH", "TENKH", pHONGTHUE.MAKH);
            ViewBag.SOPHONG = new SelectList(db.PHONGs, "SOPHONG", "LOAIPHONG", pHONGTHUE.SOPHONG);
            return View(pHONGTHUE);
        }

        // POST: Admin/PHONGTHUEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADK,MAKH,SOPHONG,NGAYDEN,NGAYDI")] PHONGTHUE pHONGTHUE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHONGTHUE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAKH = new SelectList(db.KHACHHANGs, "MAKH", "TENKH", pHONGTHUE.MAKH);
            ViewBag.SOPHONG = new SelectList(db.PHONGs, "SOPHONG", "LOAIPHONG", pHONGTHUE.SOPHONG);
            return View(pHONGTHUE);
        }

        // GET: Admin/PHONGTHUEs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGTHUE pHONGTHUE = db.PHONGTHUEs.Find(id);
            if (pHONGTHUE == null)
            {
                return HttpNotFound();
            }
            return View(pHONGTHUE);
        }

        // POST: Admin/PHONGTHUEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PHONGTHUE pHONGTHUE = db.PHONGTHUEs.Find(id);
            db.PHONGTHUEs.Remove(pHONGTHUE);
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
