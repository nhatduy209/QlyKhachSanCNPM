using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLyKhachSan.Controllers;
namespace QLyKhachSan.Areas.Admin.Controllers
{
    [CheckPermission("Admin", "Nhân Viên")]
    public class HomeAdminController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChartBar(int? id)
        {
            if (id == null)
            {
                DateTime monthNow = DateTime.Now;
                ViewBag.month = monthNow.Month;
                Session["month"] = monthNow.Month;
            }
            else
            {
                Session["month"] = id;
                ViewBag.month = id;
            }
            return View();
        }
    }
}