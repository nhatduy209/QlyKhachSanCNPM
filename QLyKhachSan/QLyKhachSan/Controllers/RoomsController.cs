﻿using QLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace QLyKhachSan.Controllers
{
    public class RoomsController : Controller
    {
        // GET: Rooms
        public ActionResult Index()
        {
            var db = new QuanLyKhachSanEntities();
            var lsRooms = db.PHONGs.ToList();

            return View(lsRooms);
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rooms/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Booking(string dtp_from,string dtp_to , string people, string quantity , string pool , string bar , string gym , string rooms ,string fullname , string phonenum)
        {
            var db = new  QuanLyKhachSanEntities();
            var PhongThue = new PHONGTHUE();
            string idPhongThue = new GenerateIDPhongThue().generateID();
            // check phòng 
            
            var phongTrong = db.PHONGs.Where(x => x.LOAIPHONG == rooms && x.TRANGTHAI == "false").FirstOrDefault();
            if(phongTrong == null)
            {
                return Content("nullRooms");
            }    
            // tạo thông tin khách hàng 
            var khachHang = new KHACHHANG();
            khachHang.MAKH = new GenerateIDPhongThue().generateIDKH();
            khachHang.TENKH = fullname;
            khachHang.SDT = Convert.ToInt32(phonenum);
            db.KHACHHANGs.Add(khachHang);

            // lưu database 
            db.SaveChanges();


            // tạo thống tin cho phòng thuê 
            phongTrong.TRANGTHAI = "true";
            PhongThue.MADK = idPhongThue;
            PhongThue.NGAYDI  = Convert.ToDateTime(dtp_to);
            PhongThue.NGAYDEN = Convert.ToDateTime(dtp_from);
            PhongThue.SOPHONG = phongTrong.SOPHONG;
            PhongThue.MAKH = khachHang.MAKH;

            db.PHONGTHUEs.Add(PhongThue);
            db.SaveChanges();

            // tạo thông tin lưu dịch vụ   
            if(pool == "true")
            {
                string idBooking = new GenerateIDPhongThue().generateIDServiceBooking();
                var DVKH = new DVKH();
                DVKH.ID = idBooking;
                DVKH.MADV = "1";
                DVKH.MADK = idPhongThue;
                db.DVKHs.Add(DVKH);
                db.SaveChanges();
            }
            if (bar == "true")
            {
                string idBooking = new GenerateIDPhongThue().generateIDServiceBooking();
                var DVKH = new DVKH();
                DVKH.ID = idBooking;
                DVKH.MADV = "2";
                DVKH.MADK = idPhongThue;
                db.DVKHs.Add(DVKH);
                db.SaveChanges();
            }
             if (gym == "true")
            {
                string idBooking = new GenerateIDPhongThue().generateIDServiceBooking();
                var DVKH = new DVKH();
                DVKH.ID = idBooking;
                DVKH.MADV = "3";
                DVKH.MADK = idPhongThue;
                db.DVKHs.Add(DVKH);
                db.SaveChanges();
            }
        


            return Content("true");
         }
       
    }
}
