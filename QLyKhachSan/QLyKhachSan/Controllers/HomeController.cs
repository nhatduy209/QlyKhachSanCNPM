using QLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QLyKhachSan.Controllers
{
    public class HomeController : Controller
    {
        QuanLyKhachSanEntities db = new QuanLyKhachSanEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {

            string users = f["username"].ToString();
            string password = f["password"].ToString();
            if (users.Length > 0 && password.Length > 0)
            {
                NHANVIEN user = db.NHANVIENs.SingleOrDefault(n => n.username == users && n.password == password);
                var data = db.NHANVIENs.Where(s => s.username.Equals(users) && s.password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    Session["staff"] = user.MANV;
                    if (user.roleID == 4)
                        return Content("false");
                    if (user.roleID == 3)
                        return Redirect("/Admin/PHONGs/Index");
                    if (user.roleID == 2)
                        return Redirect("/Admin/PHONGs/Index");
                    return Content("/Admin/PHONGs/Index");
                }
                else { return Content("false"); }
            }

            return Content("false");
        }


        public ActionResult ForgotPasswordNotAccept()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(FormCollection f)
        {
            string emailRegister = f["emailRegister"].ToString();
            string newPassword = RandomPassword();

            if (emailRegister.Length > 0)
            {
                // thay đổi mật khẩu trong database 
                QuanLyKhachSanEntities contextDB = new QuanLyKhachSanEntities();
                NHANVIEN users = contextDB.NHANVIENs.Where(x => x.CMND_NV == emailRegister).FirstOrDefault();
                if (users == null)
                {
                    return Content("false");
                }
                else
                {
                    users.password = newPassword;
                    contextDB.SaveChanges();
                }
                try
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("ecommerceshoppingmall@gmail.com", "admin12345678@xyz");
                    MailMessage msgobj = new MailMessage();
                    msgobj.To.Add(emailRegister);
                    msgobj.From = new MailAddress("ecommerceshoppingmall@gmail.com");
                    msgobj.Subject = "Quên mật khẩu";
                    DateTime dt = DateTime.Now;
                    msgobj.Body = "Mật khẩu mới của bạn là " + newPassword + " Hãy đặt lại mật khẩu mới ";
                    client.Send(msgobj);
                }
                catch
                {
                    return Content("false");
                }
            }
            // chuyển đến trang 
            return Content("true");
        }
        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }
    }
}