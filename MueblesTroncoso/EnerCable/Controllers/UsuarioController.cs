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
namespace Troncoso.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        #region Login
        
        public ActionResult Index()
        {

            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Home", "Home");
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();

            return View();
        }
        public ActionResult SinPermiso()
        {
            return View();
        }
        public ActionResult LogIn()
        {

            return View();

        }
        [HttpPost]
        public ActionResult LogIn(UsuarioLoginView ULV, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                SeguridadManager UM = new SeguridadManager();
                string password = UM.GetUserPassword(ULV.Usuario);

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "Usuario No Identificado.");
                else
                {
                    if (ULV.Password.Equals(new Encriptacion().Desencriptar(password)))
                    {
                        string ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ipaddress == "" || ipaddress == null)
                            ipaddress = Request.ServerVariables["REMOTE_ADDR"];

                        ResultadoSesionView _resultado = UM.crearSesion(ULV.Usuario, new Encriptacion().Encriptar(ULV.Password), ipaddress);
                        if (_resultado.IdSesion > 0)
                        {
                            HttpContext.Session.Add("IdSesion", _resultado.IdSesion);

                            FormsAuthentication.SetAuthCookie(ULV.Usuario, false);
                            FormsAuthentication.RedirectFromLoginPage(ULV.Usuario, false);

                        }
                        else ModelState.AddModelError("", _resultado.Resultado);
                    }
                    else
                    {
                        ModelState.AddModelError("", "El Password Proporcionado es Incorrecto");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(ULV);
        }
        #endregion
        #region LogOut
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Usuario");
        }
        #endregion
        #region Catalogo
        [AuthorizeRoles("Usuarios")]
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
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Seguridad", "Usuarios");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<SelectListItem> myStatus = new List<SelectListItem>();
            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();
            List<Estatus> _estatus = _perfilMan.obtenerStatus();
            foreach (Perfiles nivel in _perfiles)
            {
                if (nivel.Perfil == "Administrador")
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString(), Selected = true });
                else
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString() });
            }
            foreach (Estatus nivel in _estatus)
            {
                if (nivel.Estatus1 == "Activo")
                    myStatus.Add(new SelectListItem() { Text = nivel.Estatus1, Value = nivel.IdEstatus.ToString(), Selected = true });
                else
                    myStatus.Add(new SelectListItem() { Text = nivel.Estatus1, Value = nivel.IdEstatus.ToString() });
            }
            ViewBag.Niveles = myNivel;
            ViewBag.Estatus = myStatus;
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerUsuarios()
        {
            var seguridad = new UsuarioManager();
            return Json(seguridad.obtenerUsuarios(), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult agregarUsuarios(Usuarios usuario)
        {
            var seguridad = new UsuarioManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.agregarUsuario(
                 usuario,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }


        #endregion
    }
}