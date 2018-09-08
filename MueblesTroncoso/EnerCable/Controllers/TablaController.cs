/*sss*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Troncoso.Models.ViewModel;
using Troncoso.Models.EntityManager;
using EnerCable.Models.DB;
using System.Web.Security;
using Troncoso.Security;

namespace EnerCable.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        #region Catalogo
        [AuthorizeRoles("Tablas")]
        public ActionResult Registrar()
        {

            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            PerfilManager _perfilMan = new PerfilManager();
            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }

            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Catalogos", "Tablas");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();
            foreach (Perfiles nivel in _perfiles)
            {
                if (nivel.Perfil == "Administrador")
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString(), Selected = true });
                else
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString() });
            }
            
            ViewBag.Niveles = myNivel;
            return View();
        }

        [HttpGet]
        public JsonResult obtenerTablas()
        {
            var seguridad = new TablasManager();
            return Json(seguridad.obtenerTablas(), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult agregarTablas(Tablas con)
        {
            var seguridad = new TablasManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.agregarTablas(con,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }

        [HttpPost]
        public JsonResult eliminarTabla(int idTabla)
        {
            var seguridad = new TablasManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.eliminarTabla(
                 idTabla,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }



        #endregion
    }
}