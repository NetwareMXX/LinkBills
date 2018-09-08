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
    public class VentaController : Controller
    {
        #region Ventas
        // GET: Venta
        [AuthorizeRoles("Ventas")]
        public ActionResult Registrar()
        {
            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            PerfilManager _perfilMan = new PerfilManager();
            TipoDePolizasManager _tipoMan = new TipoDePolizasManager();
            TablasManager _tablaMan = new TablasManager();

            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Facturas", "Ventas");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();

            List<SelectListItem> myTipo = new List<SelectListItem>();
            List<TiposDePolizas> _tiposPolizas = _tipoMan.obtenerTipoPolizas();

            List<SelectListItem> myTabla = new List<SelectListItem>();
            List<Tablas> _tablas = _tablaMan.obtenerTablasValida();

            foreach (Perfiles nivel in _perfiles)
            {
                if (nivel.Perfil == "Administrador")
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString(), Selected = true });
                else
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString() });
            }
            ViewBag.Niveles = myNivel;

            foreach (TiposDePolizas nivel in _tiposPolizas)
            {

                myTipo.Add(new SelectListItem() { Text = nivel.TipoDePoliza, Value = nivel.TipoDePoliza.ToString() });

            }
            ViewBag.Polizas = myTipo;
            int _contador = 0;
            foreach (Tablas nivel in _tablas)
            {
                if (_contador == 0)
                    myTabla.Add(new SelectListItem() { Text = nivel.Tabla, Value = nivel.IdTabla.ToString(), Selected = true });
                else
                    myTabla.Add(new SelectListItem() { Text = nivel.Tabla, Value = nivel.IdTabla.ToString() });
                ++_contador;
            }
            ViewBag.Tablas = myTabla;
            return View();
        }

        [HttpGet]
        public JsonResult obtenerCostos(int idMes)
        {
            var seguridad = new VentaManager();
            SeguridadManager _seguridadx = new SeguridadManager();
            int _idusuario = 0;
            _seguridadx.getIdUsuario(User.Identity.Name, ref _idusuario);

            string _success = "";
            var _htm = seguridad.obtenerCostos(idMes, _idusuario, ref _success);

            

            var jsonResult = Json(new
            {
                Success = _success == "" ? "OK" : _success,
                Data = _htm == null ? "<p>Ocurrio El Error: " + _success + "</p><p> Consulte Al Administrador del Sistema</p>" : _htm
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;


        }
        [HttpGet]
        public JsonResult obtenerFacturasSerie(string IdMovimientosCostos, string TipoDePolizas, int IdTabla)
        {


            var seguridad = new VentaManager();
            SeguridadManager _seguridadx = new SeguridadManager();
            int _idusuario = 0;
            _seguridadx.getIdUsuario(User.Identity.Name, ref _idusuario);

            string _success = "";
            var _htm = seguridad.obtenerFacturasPorSerie(IdMovimientosCostos, TipoDePolizas, IdTabla, _idusuario, ref _success);
            return Json(new
            {
                Success = _success == "" ? "OK" : _success,
                Data = _htm == null ? "Ocurrio El Error: " + _success + "</p><p> Consulte Al Administrador del Sistema" : _htm
            }, JsonRequestBehavior.AllowGet);




        }
        [HttpGet]
        public JsonResult obtenerFacturasFactura(string IdMovimientosCostos, string TipoDePolizas, int IdTabla)
        {


            var seguridad = new VentaManager();
            SeguridadManager _seguridadx = new SeguridadManager();
            int _idusuario = 0;
            _seguridadx.getIdUsuario(User.Identity.Name, ref _idusuario);

            string _success = "";
            var _htm = seguridad.obtenerFacturasPorFactura(IdMovimientosCostos, TipoDePolizas, IdTabla, _idusuario, ref _success);
            return Json(new
            {
                Success = _success == "" ? "OK" : _success,
                Data = _htm == null ? "Ocurrio El Error: " + _success + "</p><p> Consulte Al Administrador del Sistema" : _htm
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult desligarCorrecion(int idTabla, int movConsPol, int movConsMov, int movMes, string movTipoPol, int eliminarFactura, int eliminarSerie)
        {

            var seguridad = new CostoManager();
            string _success = "";
            var _htm = seguridad.desligarCorrecion(idTabla, movConsPol, movConsMov, movMes, movTipoPol, eliminarFactura, eliminarSerie, (long)HttpContext.Session["IdSesion"], ref _success);
            return Json(new
            {
                Success = _success == "" ? "OK" : _success,
                Data = _htm == null ? "Ocurrio El Error: " + _success + "</p><p> Consulte Al Administrador del Sistema" : _htm
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Libro
        [AuthorizeRoles("Libro")]
        public ActionResult Libro()
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
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Facturas", "Libro");
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
            int _idusuario = 0;
            _seguridad.getIdUsuario(User.Identity.Name, ref _idusuario);
            ViewBag.IdUsuario = _idusuario;


            return View();
        }
        #endregion  
    }
}