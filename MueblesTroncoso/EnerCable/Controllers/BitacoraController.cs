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
    public class BitacoraController : Controller
    {
        // GET: Bitacora

        #region Catalogo
        [AuthorizeRoles("Logs")]

        public ActionResult Registrar()
        {

            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            PerfilManager _perfilMan = new PerfilManager();
            UsuarioManager _usuarioMan = new UsuarioManager();
            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }

            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Bitacora", "Logs");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<SelectListItem> myUsers = new List<SelectListItem>();
            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();
            List<vwUsuarios> _usuarios = _usuarioMan.obtenerUsuarios();

            foreach (Perfiles nivel in _perfiles)
            {
                if (nivel.Perfil == "Administrador")
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString(), Selected = true });
                else
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString() });
            }
            foreach (vwUsuarios usu in _usuarios)
            {


                myUsers.Add(new SelectListItem() { Text = usu.Nombre + " " + usu.Paterno + " " + usu.Materno, Value = usu.Usuario, Selected = true });
            }


            ViewBag.Niveles = myNivel;
            ViewBag.Usuarios = myUsers;

            // List<vwPedidosFacturados> _factura = new PedidosManager().obtenerPedidosFacturados();
            return View();
        }

        [HttpGet]
        public JsonResult obtenerBitacora(string fechaInicial, string fechaFinal, string accion, string usuarios)
        {
            var seguridad = new BitacoraManager();
            if (fechaInicial.Trim() == string.Empty && accion == "") fechaInicial = String.Format("{0:dd-MM-yyyy}", DateTime.Now);
            if (fechaFinal.Trim() == string.Empty && accion == "") fechaFinal = String.Format("{0:dd-MM-yyyy}", DateTime.Now);
            return Json(seguridad.obtenerBitacora(fechaInicial, fechaFinal, accion, usuarios), JsonRequestBehavior.AllowGet);

        }


        #endregion
    }
}