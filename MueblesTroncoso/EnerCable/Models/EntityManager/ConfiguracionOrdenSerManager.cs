using System;
using System.Linq;

using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Troncoso.Security;
using Troncoso.Models.ViewModel;
using System.Web;
using System.Web.Mvc;
using EnerCable.Models.DB;
namespace Troncoso.Models.EntityManager
{
    public class ConfiguracionOrdenSerManager
    {
        #region obtenerConfiguracion

        public List<ConfiguracionOrdenSer> obtenerConfiguracion()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.ConfiguracionOrdenSer
                           select usu;
                

                List<ConfiguracionOrdenSer> _orden = menu.OrderBy(x => x.IdConfiguracionOrdenSer).ToList();
                return _orden;

            }
        }
        #endregion
        #region Resgistrar orderServer

        public string agregarOrderServer(ConfiguracionOrdenSer confi, long idSesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {
                    int _x = db.GuardarConfiguracionOrdenSer(confi.LongitudSerieEnSer_Orden, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
    }
}