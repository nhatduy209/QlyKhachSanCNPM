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
    //[CheckPermission("Admin", "Nhân Viên")]
    public class PHONGsController : Controller
    {
        private QuanLyKhachSanEntities db = new QuanLyKhachSanEntities();

        // GET: Admin/PHONGs
        public ActionResult Index()
        {
            return View(db.PHONGs.ToList());
        }

        // GET: Admin/PHONGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONG pHONG = db.PHONGs.Find(id);
            if (pHONG == null)
            {
                return HttpNotFound();
            }
            return View(pHONG);
        }

        // GET: Admin/PHONGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PHONGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create( string loaiPh , string giaPh , string mieutaPh )
        {
            PHONG ph = new PHONG();
            ph.SOPHONG = new GenerateIDPhongThue().generateIDPhong();
            ph.LOAIPHONG = loaiPh;
            ph.GIAPHONG = Convert.ToInt32(giaPh);
            ph.MIEUTAPHONG = mieutaPh;
            ph.TRANGTHAI = "false";
            var exist = db.PHONGs.Where(x => x.SOPHONG == ph.SOPHONG).SingleOrDefault();
            if (exist == null)
            {
                try
                {
                    db.PHONGs.Add(ph);
                    db.SaveChanges();
                    return Content("true");
                }
                catch
                {
                  return  Content("false");
                }
               
            }
            else
            {
                return Content("false");
            }

        }

        // GET: Admin/PHONGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONG pHONG = db.PHONGs.Find(id);
            if (pHONG == null)
            {
                return HttpNotFound();
            }
            return View(pHONG);
        }

        // POST: Admin/PHONGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SOPHONG,LOAIPHONG,GIAPHONG")] PHONG pHONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pHONG);
        }

        // GET: Admin/PHONGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONG pHONG = db.PHONGs.Find(id);
            if (pHONG == null)
            {
                return HttpNotFound();
            }
            return View(pHONG);
        }

        // POST: Admin/PHONGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHONG pHONG = db.PHONGs.Find(id);
            db.PHONGs.Remove(pHONG);
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
