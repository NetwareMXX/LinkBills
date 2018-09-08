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
    public class TipoDePolizasManager
    {
        #region obtenerTipoPolizas

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
        #endregion
        #region Resgistrar Tipo Polizas

        public string agregarTipoPolizas(TiposDePolizas cuenta, long idSesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {
                    int _x = db.GuardarTiposDePolizas
                        (cuenta.IdTipoDePoliza,cuenta.TipoDePoliza,cuenta.AplicaNombreCuenta,
                        cuenta.AplicaCondicionNombreCuenta,cuenta.Posicion,cuenta.Longitud,cuenta.TipoExtraccion,
                        cuenta.DatoValidar,cuenta.Cate,cuenta.AplicaFacturaEnSerie,
                        idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string eliminarTipoPoliza(int idTipo, long idsesion)
        {

            try
            {

                string Sp = "dbo.EliminarTiposDePolizas";
                SqlParameter[] Parametros = new SqlParameter[2];
                Parametros[0] = new SqlParameter("@IdTipoDePoliza", idTipo);
                Parametros[1] = new SqlParameter("@IdSesion", idsesion);
                string _valor = new Datos().ValidaEjecucion(Sp, Parametros);
                return _valor;
            }
            catch (Exception Error)
            {
                throw Error;

            }
        }
        #endregion

    }
}