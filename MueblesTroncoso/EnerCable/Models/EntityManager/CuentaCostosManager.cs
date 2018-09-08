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
    public class CuentaCostosManager
    {
        #region CuentasCostos
        #region obtenerCuentasCostos

        public List<vwCuentasCostos> obtenerCuentas()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.vwCuentasCostos
                           select usu;


                List<vwCuentasCostos> _usuario = menu.OrderBy(x => x.Cuenta).ToList();

                return _usuario;

            }
        }
        #endregion
        #region Resgistrar CuentasCostos

        public string agregarCuentas(CuentasCostos cuenta, long idSesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {
                    int _x = db.GuardarCuentasCostos(cuenta.IdCuentaCosto, cuenta.Cuenta, cuenta.Nombre, cuenta.IdTipoCuenta, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        public string eliminarCuentasCostos(int idCuenta, long idsesion)
        {

            try
            {

                string Sp = "dbo.EliminarCuentasCostos";
                SqlParameter[] Parametros = new SqlParameter[2];
                Parametros[0] = new SqlParameter("@IdCuentaCosto", idCuenta);
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
        #region CuentasVentas
        #region obtenerCuentasVentas

        public List<vwCuentasVentas> obtenerCuentasVentas()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.vwCuentasVentas
                           select usu;


                List<vwCuentasVentas> _usuario = menu.OrderBy(x => x.Cuenta).ToList();

                return _usuario;

            }
        }
        public string eliminarCuentasVentas(int idCuenta, long idsesion)
        {

            try
            {

                string Sp = "dbo.EliminarCuentasVentas";
                SqlParameter[] Parametros = new SqlParameter[2];
                Parametros[0] = new SqlParameter("@IdCuentaVenta", idCuenta);
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
        #region Resgistrar CuentasVentas

        public string agregarCuentasVentas(CuentasVentas cuenta, long idSesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {
                    int _x = db.GuardarCuentasVentas(cuenta.IdCuentaVenta, cuenta.Cuenta, cuenta.Nombre, cuenta.IdTipoCuenta, idSesion);
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
        public List<TipoCuentas> obtenerTipoCuentas()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.TipoCuentas
                           select usu;


                List<TipoCuentas> _usuario = menu.OrderBy(x => x.TipoCuenta).ToList();

                return _usuario;

            }
        }
    }
}