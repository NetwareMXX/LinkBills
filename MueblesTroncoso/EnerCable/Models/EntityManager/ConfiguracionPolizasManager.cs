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
    public class ConfiguracionPolizasManager
    {
        #region ConfiguracionPolizas
        #region obtenerConfiguracionPolizas

        public List<vwConfiguracionPolizas> obtenerConfiguracionPolizas()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.vwConfiguracionPolizas
                           select usu;


                List<vwConfiguracionPolizas> _usuario = menu.OrderBy(x => x.TipoConfiguracion).ToList();

                return _usuario;

            }
        }
        #endregion
        #region Resgistrar CuentasCostos

        public string agregarConfiguracionPolizas(ConfiguracionPolizas cuenta, long idSesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {
                    int _x = db.GuardarConfiguracionPolizas
                        ( cuenta.IdConfiguracionPoliza,cuenta.IdTipoDePoliza,cuenta.Posicion,cuenta.Longitud,cuenta.TipoConfiguracion,
                        cuenta.TipoExtraccion,idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        #endregion
        public List<TiposDePolizas> obtenerTipoPolizas()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.TiposDePolizas
                           select usu;


                List<TiposDePolizas> _usuario = menu.OrderBy(x => x.TipoDePoliza).ToList();

                return _usuario;

            }
        }
        public string eliminarConfiguracion(int idconfiguracion, long idsesion)
        {

            try
            {

                string Sp = "dbo.EliminarConfiguracionPolizas";
                SqlParameter[] Parametros = new SqlParameter[2];
                Parametros[0] = new SqlParameter("@IdConfiguracionPoliza", idconfiguracion);
                Parametros[1] = new SqlParameter("@IdSesion", idsesion);
                string _valor = new Datos().ValidaEjecucion(Sp, Parametros);
                return _valor;
            }
            catch (Exception Error)
            {
                throw Error;

            }
        }

    }
}