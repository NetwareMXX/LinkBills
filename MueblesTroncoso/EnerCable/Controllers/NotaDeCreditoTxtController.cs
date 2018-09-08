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

namespace Troncoso.Controllers
{
    public class NotaDeCreditoTxtController : Controller
    {
        [HttpPost]
        public JsonResult GenerarTxtNotaDeCredito(int IdNotaDeCredito)
        {
            var seguridad = new UsuarioManager();
            var Respuesta = new NotaDeCreditoTxtManager().GenerarTxtNotaDeCredito(IdNotaDeCredito);
            return Json(new
            {
                Success = "OK",
                Result = ((Respuesta == "OK") ? @"C:\inetpub\wwwroot\MueblesTroncosoArchivos\NotasDeCredito\" + IdNotaDeCredito.ToString() + ".txt" : Respuesta)
            });
        }
    }
}