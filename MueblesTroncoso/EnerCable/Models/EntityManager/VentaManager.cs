
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
    public class VentaManager
    {
        #region obtenerCostos
        public string obtenerCostos(int idMes, int idUsuario, ref string success)
        {

            try
            {
                string Sp = "dbo.GenerarVentas";
                SqlParameter[] Parametros = new SqlParameter[2];
                Parametros[0] = new SqlParameter("@IdMes", idMes);
                Parametros[1] = new SqlParameter("@IdUsuario", idUsuario);


                DataSet _ds = new Datos().RegresaDataSet(Sp, Parametros);
                if (_ds.Tables[0].Rows.Count == 0) success = "vacio";
                return this.PintaHtml(_ds);
            }
            catch (Exception ex)
            {
                success = ex.Message;
                return null;
            }
        }
        private string PintaHtml(DataSet _ds)
        {

            StringBuilder _html = new StringBuilder();

            if (_ds.Tables[0].Rows.Count == 0) return "<p>No Se Encontraron Resultados Para los Criterios Seleccionados</p>";

            _html.Append("<table class='table table-bordered table-striped table-hover dataTable js-exportable'>");
            _html.Append("      <thead>");
            _html.Append("          <tr>");
            _html.Append("<th><input id='cchMain'   type='checkbox' " + " onclick='checkTodo(this);' class='chkMain filled-in'><label for='cchMain'>Seleccionar Todos</label></th>");
            _html.Append("              <th>C.Serie</th>");
            _html.Append("              <th>C.Factura</th>");
            _html.Append("              <th>Factura</th>");
            _html.Append("              <th>Serie</th>");
            _html.Append("              <th>Mov_TipoPol</th>");
            _html.Append("              <th>MOV_CONSPOL</th>");
            _html.Append("              <th>MOV_CONSMOV</th>");
            _html.Append("              <th>MOV_MES</th>");
            _html.Append("              <th>MOV_NUMCTA</th>");
            _html.Append("              <th>MOV_CONCEPTO</th>");
            _html.Append("              <th>MOV_DEBE</th>");
            _html.Append("              <th>MOV_HABER</th>");
            _html.Append("              <th>Total</th>");
            _html.Append("              <th>Cate</th>");
            _html.Append("              <th>MOV_FECHOPE</th>");
            _html.Append("              <th>MOV_HORAOPE</th>");
            _html.Append("                        </tr>");
            _html.Append("</thead>");
            _html.Append("<tbody>");

            int _contado = 1;

            foreach (DataRow _row in _ds.Tables[0].Rows)
            {
                _html.Append("<tr>");
                _html.Append("<td scope='row'><input tipopol='"+ _row["Mov_TipoPol"] + "' movmes='"+ _row["MOV_MES"] + "' consmov='"+ _row["MOV_CONSMOV"] + "' conspol='"+ _row["MOV_CONSPOL"] + "'  id='chk" + _contado + "' valor='" + _row["IdMovimientoVenta"] + "' type='checkbox' " + " class='chkPer filled-in'><label for='chk" + _contado + "'>" + _row["IdMovimientoVenta"] + "</label></td>");
                _html.Append("<td>" + (_row["CorreccionSerie"].ToString() == "1" ? "SI" : "<a id='aSerie" + _row["IdMovimientoVenta"] + "' onclick='return Corregir(0,1," + "\"" + _row["Mov_TipoPol"] + "\","
                  + "\"" + _row["MOV_CONSPOL"] + "\"," + "\"" + _row["MOV_CONSMOV"] + "\"," + "\"" + _row["MOV_MES"] + "\"," + _row["IdMovimientoVenta"] + ");' href='#'>NO</a>") + "</td>");
                _html.Append("<td>" + (_row["CorreccionFactura"].ToString() == "1" ? "SI" : "<a id='aFactura" + _row["IdMovimientoVenta"] + "' onclick='return Corregir(1,0," + "\"" + _row["Mov_TipoPol"] + "\","
                    + "\"" + _row["MOV_CONSPOL"] + "\"," + "\"" + _row["MOV_CONSMOV"] + "\"," + "\"" + _row["MOV_MES"] + "\"," + _row["IdMovimientoVenta"] + ");' href='#'>NO</a>") + "</td>");

                _html.Append("<td id='tdFactura" + _row["IdMovimientoVenta"] + "'>" + _row["Factura"] + "</td>");
                _html.Append("<td id='tdSerie" + _row["IdMovimientoVenta"] + "'>" + _row["Serie"] + "</td>");
                _html.Append("<td>" + _row["Mov_TipoPol"] + "</td>");
                _html.Append("<td>" + _row["MOV_CONSPOL"] + "</td>");
                _html.Append("<td>" + _row["MOV_CONSMOV"] + "</td>");
                _html.Append("<td>" + _row["MOV_MES"] + "</td>");
                _html.Append("<td>" + _row["MOV_NUMCTA"] + "</td>");
                _html.Append("<td>" + _row["MOV_CONCEPTO"] + "</td>");
                _html.Append("<td>" + _row["MOV_DEBE"] + "</td>");
                _html.Append("<td>" + _row["MOV_HABER"] + "</td>");
                _html.Append("<td>" + _row["Total"] + "</td>");
                _html.Append("<td>" + _row["Cate"] + "</td>");
                _html.Append("<td>" + _row["MOV_FECHOPE"] + "</td>");
                _html.Append("<td>" + _row["MOV_HORAOPE"] + "</td>");
                _html.Append("</tr>");
                ++_contado;

            }


            _html.Append("</tbody>");
            _html.Append("</table>");
            return _html.ToString();
        }
        #endregion
        #region obtenerFacturasPorSerie



        public string obtenerFacturasPorSerie(string IdMovimientosCostos, string TipoDePolizas, int IdTabla, int idUsuario, ref string success)
        {

            try
            {
                string Sp = "dbo.BuscarFacturasEnOtrasPolizasPorSerieVentas";
                SqlParameter[] Parametros = new SqlParameter[4];
                Parametros[0] = new SqlParameter("@IdMovimientosVentas", IdMovimientosCostos);
                Parametros[1] = new SqlParameter("@IdUsuario", idUsuario);
                Parametros[2] = new SqlParameter("@TipoDePolizas", TipoDePolizas);
                Parametros[3] = new SqlParameter("@IdTabla", IdTabla);


                return new Datos().ValidaEjecucion(Sp, Parametros);

            }
            catch (Exception ex)
            {
                success = ex.Message;
                return null;
            }
        }
        public string obtenerFacturasPorFactura(string IdMovimientosCostos, string TipoDePolizas, int IdTabla, int idUsuario, ref string success)
        {

            try
            {
                string Sp = "dbo.BuscarSerieEnOtrasPolizasPorFacturaVentas";
                SqlParameter[] Parametros = new SqlParameter[4];
                Parametros[0] = new SqlParameter("@IdMovimientosVentas", IdMovimientosCostos);
                Parametros[1] = new SqlParameter("@IdUsuario", idUsuario);
                Parametros[2] = new SqlParameter("@TipoDePolizas", TipoDePolizas);
                Parametros[3] = new SqlParameter("@IdTabla", IdTabla);


                return new Datos().ValidaEjecucion(Sp, Parametros);

            }
            catch (Exception ex)
            {
                success = ex.Message;
                return null;
            }
        }
        
        #endregion


        
    

    }
}