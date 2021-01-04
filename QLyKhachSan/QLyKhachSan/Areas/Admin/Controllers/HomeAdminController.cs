using QLyKhachSan.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult ChartBar()
        {
            return View();
        }
    }
}