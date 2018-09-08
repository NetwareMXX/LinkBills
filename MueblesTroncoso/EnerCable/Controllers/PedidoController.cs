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
    public class PedidoController : Controller
    {
        #region Catalogo
        [AuthorizeRoles("Pedidos Facturados")]
        public ActionResult Registrar()
        {

            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            PerfilManager _perfilMan = new PerfilManager();
            PedidosManager _pedidoMan = new PedidosManager();
            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }

            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Facturas", "Pedidos Facturados");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<SelectListItem> usoCfdi = new List<SelectListItem>();
            List<SelectListItem> tipoCfdi = new List<SelectListItem>();

            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();
            List<CatalogoCfdi> _usocfdi = _pedidoMan.obtenerUsoCfdi();
            List<CatalogoCfdi> _tipocfdi = _pedidoMan.obtenerTipoCfdi();


            foreach (Perfiles nivel in _perfiles)
            {
                if (nivel.Perfil == "Administrador")
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString(), Selected = true });
                else
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString() });
            }
            foreach (CatalogoCfdi nivel in _usocfdi)
            {

                usoCfdi.Add(new SelectListItem() { Text = nivel.Descripcion, Value = nivel.Codigo.ToString() });
            }
            foreach (CatalogoCfdi nivel in _tipocfdi)
            {

                tipoCfdi.Add(new SelectListItem() { Text = nivel.Descripcion, Value = nivel.Codigo.ToString() });
            }



            ViewBag.Niveles = myNivel;
            ViewBag.Uso = usoCfdi;
            ViewBag.Tipo = tipoCfdi;





            return View();
        }

        [HttpGet]
        public JsonResult obtenerPedidosFacturados(int tipoBusqueda, string fechaInicial, string fechaFinal, string folio, string cliente)
        {
            var seguridad = new PedidosManager();
            return Json(seguridad.obtenerPedidosFacturados(tipoBusqueda, fechaInicial, fechaFinal, folio, cliente), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult obtenerDetallePedido(int IdPedido)
        {
            var seguridad = new PedidosManager();
            PedidosFacturadosCliente _cliente = new PedidosFacturadosCliente();
            string _detcliente = seguridad.obtenerDetalleClientePorPedido(IdPedido, ref _cliente);
            return Json(new
            {
                Success = "OK",
                Detalle = seguridad.obtenerDetallePorPedido(IdPedido),
                Datos = _detcliente,
                Fecha = seguridad.obtenerFechaPedido(),
                Cliente = _cliente
            }, JsonRequestBehavior.AllowGet);



        }
        [HttpPost]
        public JsonResult guardarNota(NotaCreditoHeader nota, List<PedidosFacturadosDetalle> detalle)
        {
            var seguridad = new PedidosManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.guardarNota(
                 nota, detalle,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }
        [HttpPost]
        public JsonResult desligarNota(int idNota)
        {
            var seguridad = new PedidosManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.desligarNota(
                 idNota,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }


        #endregion
    }
}