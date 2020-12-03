using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLyKhachSan.Models;

namespace QLyKhachSan.Controllers
{
    public class AuthController : Controller
    {
        QuanLyKhachSanEntities db = new QuanLyKhachSanEntities();
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var data = db.NHANVIENs.Where(s => s.username.Equals(username) && s.password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["User"] = data.FirstOrDefault();
                    if (data.FirstOrDefault().roleID == 1)
                    {
                        return Redirect("/Admin/homeadmin");
                    }
                    return Redirect("/");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
    }
}