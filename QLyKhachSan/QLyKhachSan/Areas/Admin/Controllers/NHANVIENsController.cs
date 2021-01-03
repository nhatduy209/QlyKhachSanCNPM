using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLyKhachSan.Models;
using QLyKhachSan.Controllers;

namespace QLyKhachSan.Areas.Admin.Controllers
{
    [CheckPermission("Admin")]
    public class NHANVIENsController : Controller
    {
        private QuanLyKhachSanEntities db = new QuanLyKhachSanEntities();
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: Admin/NHANVIENs
        public ActionResult Index()
        {
            var nHANVIENs = db.NHANVIENs.Include(n => n.Ref_Role);
            return View(nHANVIENs.ToList());
        }

        // GET: Admin/NHANVIENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // GET: Admin/NHANVIENs/Create
        public ActionResult Create()
        {
            ViewBag.roleID = new SelectList(db.Ref_Role, "roleID", "roleName");
            return View();
        }

        // POST: Admin/NHANVIENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANV,HOTEN,NGAYSINH,CMND_NV,DIACHI,roleID,username,password")] NHANVIEN nHANVIEN)
        {
            nHANVIEN.MANV = "NV-" + RandomString(22);
            if (ModelState.IsValid)
            {
                db.NHANVIENs.Add(nHANVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.roleID = new SelectList(db.Ref_Role, "roleID", "roleName", nHANVIEN.roleID);
            return View(nHANVIEN);
        }

        // GET: Admin/NHANVIENs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.roleID = new SelectList(db.Ref_Role, "roleID", "roleName", nHANVIEN.roleID);
            return View(nHANVIEN);
        }

        // POST: Admin/NHANVIENs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANV,HOTEN,NGAYSINH,CMND_NV,DIACHI,roleID,username")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHANVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roleID = new SelectList(db.Ref_Role, "roleID", "roleName", nHANVIEN.roleID);
            return View(nHANVIEN);
        }

        // GET: Admin/NHANVIENs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: Admin/NHANVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            db.NHANVIENs.Remove(nHANVIEN);
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
