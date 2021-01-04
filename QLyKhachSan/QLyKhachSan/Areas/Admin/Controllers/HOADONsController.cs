using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLyKhachSan.Controllers;
using QLyKhachSan.Models;

namespace QLyKhachSan.Areas.Admin.Controllers
{
    [CheckPermission("Admin", "Nhân Viên")]

    public class HOADONsController : Controller
    {
        private QuanLyKhachSanEntities db = new QuanLyKhachSanEntities();

        // GET: Admin/HOADONs
        public ActionResult Index()
        {
            var hOADONs = db.HOADONs.Include(h => h.DICHVU).Include(h => h.KHACHHANG);
            return View(hOADONs.ToList());
        }

        // GET: Admin/HOADONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // GET: Admin/HOADONs/Create
        public ActionResult Create()
        {
            ViewBag.MADV = new SelectList(db.DICHVUs, "MADV", "TENDV");
            ViewBag.MAKH = new SelectList(db.KHACHHANGs, "MAKH", "TENKH");
            return View();
        }

        // POST: Admin/HOADONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAHD,TENHD,MADV,MAKH,NGAY,TONGTIEN")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.HOADONs.Add(hOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADV = new SelectList(db.DICHVUs, "MADV", "TENDV", hOADON.MADV);
            ViewBag.MAKH = new SelectList(db.KHACHHANGs, "MAKH", "TENKH", hOADON.MAKH);
            return View(hOADON);
        }

        // GET: Admin/HOADONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADV = new SelectList(db.DICHVUs, "MADV", "TENDV", hOADON.MADV);
            ViewBag.MAKH = new SelectList(db.KHACHHANGs, "MAKH", "TENKH", hOADON.MAKH);
            return View(hOADON);
        }

        // POST: Admin/HOADONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAHD,TENHD,MADV,MAKH,NGAY,TONGTIEN")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADV = new SelectList(db.DICHVUs, "MADV", "TENDV", hOADON.MADV);
            ViewBag.MAKH = new SelectList(db.KHACHHANGs, "MAKH", "TENKH", hOADON.MAKH);
            return View(hOADON);
        }

        // GET: Admin/HOADONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // POST: Admin/HOADONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOADON hOADON = db.HOADONs.Find(id);
            db.HOADONs.Remove(hOADON);
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
