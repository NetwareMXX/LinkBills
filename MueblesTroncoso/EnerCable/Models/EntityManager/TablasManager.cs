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
    public class TablasManager
    {

        #region obtenerTablas

        public List<Tablas> obtenerTablas()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.Tablas
                           select usu

                           ;


                List<Tablas> _usuario = menu.OrderByDescending(x => x.Activo).ToList();

                return _usuario;

            }
        }
        public List<Tablas> obtenerTablasValida()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.Tablas
                           where usu.Activo ==true
                           select usu

                           ;


                List<Tablas> _usuario = menu.OrderByDescending(x => x.Activo).ToList();

                return _usuario;

            }
        }
        #endregion
        #region Resgistrar Tablas

        public string agregarTablas(Tablas cuenta, long idSesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {
                    int _x = db.GuardarTablas(cuenta.IdTabla, cuenta.Tabla, cuenta.Ejercicio, (bool)cuenta.Activo, cuenta.TablaNCRBON, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string eliminarTabla(int idTabla, long idsesion)
        {

            try
            {

                string Sp = "dbo.EliminarTablas";
                SqlParameter[] Parametros = new SqlParameter[2];
                Parametros[0] = new SqlParameter("@IdTabla", idTabla);
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