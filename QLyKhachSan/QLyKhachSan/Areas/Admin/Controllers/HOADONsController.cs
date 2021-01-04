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
    public class HOADONsController : Controller
    {
        private QuanLyKhachSanEntities db = new QuanLyKhachSanEntities();

        // GET: Admin/HOADONs
        public ActionResult Index()
        {
            var hOADONs = db.HOADONs.Include(h => h.PHONGTHUE);
            return View(hOADONs.ToList());
        }

        // GET: Admin/HOADONs/Details/5
        public ActionResult Details(string id)
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
        //public ActionResult Create()
        //{
        //    ViewBag.MADK = new SelectList(db.PHONGTHUEs, "MADK", "MAKH");
        //    return View();
        //}

        //// POST: Admin/HOADONs/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MAHD,NGAY,TONGTIEN,MADK")] HOADON hOADON)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HOADONs.Add(hOADON);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MADK = new SelectList(db.PHONGTHUEs, "MADK", "MAKH", hOADON.MADK);
        //    return View(hOADON);
        //}


        [HttpGet]
        public ActionResult CreateBill(string id)
        {

            // đưa thông tin hóa đơn vào database 
            HOADON hd = new HOADON();
            hd.MAHD = new GenerateIDPhongThue().generateIDBill();
            hd.MADK = id;
            hd.NGAY = DateTime.Now;
            double TongTien = 0;
            var Sumdichvu = db.DVKHs.Where(x => x.MADK == id).ToList();
            foreach (var sumdv in Sumdichvu)
            {
                TongTien += Convert.ToDouble(sumdv.DICHVU.GIADV);
            }

            var Phongthue = db.PHONGTHUEs.Where(a => a.MADK == id).SingleOrDefault();
            var SumPhong = db.PHONGs.Where(x => x.SOPHONG == Phongthue.SOPHONG).SingleOrDefault();
            TimeSpan songay = Convert.ToDateTime(Phongthue.NGAYDI) - Convert.ToDateTime(Phongthue.NGAYDEN); 
            TongTien += Convert.ToDouble(SumPhong.GIAPHONG);
           
            
            hd.TONGTIEN = TongTien * songay.Days;
            db.HOADONs.Add(hd);

            // trả trạng thái phòng về phòng trống 
            SumPhong.TRANGTHAI = "false";
            hd.PHONGDATHUE = SumPhong.SOPHONG;
            db.SaveChanges();
                    
            return Content("/Admin/HOADONs/Index");
        }

        // GET: Admin/HOADONs/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.MADK = new SelectList(db.PHONGTHUEs, "MADK", "MAKH", hOADON.MADK);
            return View(hOADON);
        }

        // POST: Admin/HOADONs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAHD,NGAY,TONGTIEN,MADK")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADK = new SelectList(db.PHONGTHUEs, "MADK", "MAKH", hOADON.MADK);
            return View(hOADON);
        }

        // GET: Admin/HOADONs/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
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
