using System.Web;
using System.Web.Mvc;
using Troncoso.Models.EntityManager;
using EnerCable.Models.DB;
namespace Troncoso.Security
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] userAssignedRoles;
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = true;
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                SeguridadManager UM = new SeguridadManager();
                foreach (var roles in userAssignedRoles)
                {
                    authorize = UM.IsUserInRole(httpContext.User.Identity.Name, roles);
                    if (authorize)
                        return authorize;
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Usuario/SinPermiso");
        }
        public static string Nodo()
        {
           // return "";
            if (HttpContext.Current.Request.Url.Host == "localhost")
            {
                return "";
            }
            else
            {
                return "/Reporte";
            }
        }
    }
}