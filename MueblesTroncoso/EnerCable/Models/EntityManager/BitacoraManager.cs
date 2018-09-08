
using System;
using System.Linq;
using EnerCable.Models.DB;
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
    public class BitacoraManager
    {
        #region obtenerEstatus
        public List<LogView> obtenerBitacora(string fechaInicial, string fechaFinal, string accion, string clientes)
        {
            List<LogView> _lista = new List<LogView>();
            try
            {
                string Sp = "dbo.ReporteBitacora";
                SqlParameter[] Parametros = new SqlParameter[4];
                Parametros[0] = new SqlParameter("@FechaInicial", fechaInicial);
                Parametros[1] = new SqlParameter("@FechaFinal", fechaFinal);
                Parametros[2] = new SqlParameter("@Accion", accion);
                Parametros[3] = new SqlParameter("@Usuarios", clientes);

                DataSet _ds = new Datos().RegresaDataSet(Sp, Parametros);
                foreach (DataRow _row in _ds.Tables[0].Rows)
                {

                    _lista.Add(new LogView()
                    {
                        Usuario = _row["Usuario"].ToString()
                        ,
                        Ip = _row["Ip"].ToString()
                        ,
                        Accion = _row["Accion"].ToString()
                        ,
                        FechaSistema = _row["FechaSistema"].ToString()

                    });
                }
                return _lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}