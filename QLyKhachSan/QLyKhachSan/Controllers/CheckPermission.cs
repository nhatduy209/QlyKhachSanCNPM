using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLyKhachSan.Models;

namespace QLyKhachSan.Controllers
{
    public class CheckPermission : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CheckPermission(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                var roleId = Convert.ToInt32(httpContext.Session["roleID"]);
                using (var context = new QuanLyKhachSanEntities()) 
                {
                    var roleObj = context.Ref_Role.Find(roleId);
                    if (context.Ref_Role.Find(roleId) == null)
                        return false;
                    string roleName = roleObj.roleName;
                    foreach (var role in allowedroles)
                    {
                        if (role == roleName) return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}