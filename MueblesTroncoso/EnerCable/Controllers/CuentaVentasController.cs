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
    public class CuentaVentasController : Controller
    {
        // GET: CuentaVentas
        #region Catalogo
        [AuthorizeRoles("Cuenta Ventas")]
        public ActionResult Registrar()
        {

            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            PerfilManager _perfilMan = new PerfilManager();
            CuentaCostosManager _cuentaMan = new CuentaCostosManager();
            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }

            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Catalogos", "Cuenta Ventas");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<SelectListItem> myCounts = new List<SelectListItem>();
            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();
            List<TipoCuentas> _tipos = _cuentaMan.obtenerTipoCuentas();
            foreach (Perfiles nivel in _perfiles)
            {
                if (nivel.Perfil == "Administrador")
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString(), Selected = true });
                else
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString() });
            }
            int _contador = 0;
            foreach (TipoCuentas nivel in _tipos)
            {
                if (_contador == 0)
                    myCounts.Add(new SelectListItem() { Text = nivel.TipoCuenta, Value = nivel.IdTipoCuenta.ToString(), Selected = true });
                else
                    myCounts.Add(new SelectListItem() { Text = nivel.TipoCuenta, Value = nivel.IdTipoCuenta.ToString() });
                ++_contador;
            }
            ViewBag.Niveles = myNivel;
            ViewBag.Cuentas = myCounts;
            return View();
        }

        [HttpGet]
        public JsonResult obtenerCuentas()
        {
            var seguridad = new CuentaCostosManager();
            return Json(seguridad.obtenerCuentasVentas(), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult agregarCuentas(CuentasVentas con)
        {
            var seguridad = new CuentaCostosManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.agregarCuentasVentas(con,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }
        [HttpPost]
        public JsonResult eliminarTabla(int idTabla)
        {
            var seguridad = new CuentaCostosManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.eliminarCuentasVentas(
                 idTabla,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }

        #endregion
    }
}