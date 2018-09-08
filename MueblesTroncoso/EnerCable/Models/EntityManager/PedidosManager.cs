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
    public class PedidosManager
    {
        #region obtenerPedidosFacturados
        public List<PedidosFacturados> obtenerPedidosFacturados(int tipoBusqueda, string fechaInicial, string fechaFinal, string folio, string cliente)
        {

            List<PedidosFacturados> _pedidos = new List<PedidosFacturados>();
            try
            {

                string Sp = "dbo.BuscarPedidosFacturados";
                SqlParameter[] Parametros = new SqlParameter[5];
                Parametros[0] = new SqlParameter("@TipoBusqueda", tipoBusqueda);
                Parametros[1] = new SqlParameter("@FechaInicial", fechaInicial);
                Parametros[2] = new SqlParameter("@FechaFinal", fechaFinal);
                Parametros[3] = new SqlParameter("@Folio", folio);
                Parametros[4] = new SqlParameter("@Cliente", cliente);



                DataSet ds = new Datos().RegresaDataSet(Sp, Parametros);
                foreach (DataRow _fila in ds.Tables[0].Rows)
                {
                    _pedidos.Add(new PedidosFacturados()
                    {
                        IdPedido = (int)_fila["IdPedido"],
                        Cliente = _fila["Cliente"].ToString(),
                        FechaPedido = _fila["FechaPedido"].ToString(),
                        Folio = _fila["Folio"].ToString(),
                        Total = (decimal)_fila["Total"],
                        IdNotaDeCredito = (int)_fila["IdNotaDeCredito"]
                    });
                }
                return _pedidos;

            }
            catch (Exception Error)
            {
                throw Error;
            }

            /*using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {

                return db.BuscarPedidosFacturadosStored(tipoBusqueda, fechaInicial, fechaFinal, folio, cliente).ToList();
            }*/
        }



        #endregion
        #region obtenerDetallePorPedido
        public string obtenerDetallePorPedido(int idPedido)
        {

            List<PedidosFacturadosDetalle> _pedidos = new List<PedidosFacturadosDetalle>();
            try
            {

                string Sp = "dbo.RegresaDetallePorPedido";
                SqlParameter[] Parametros = new SqlParameter[1];
                Parametros[0] = new SqlParameter("@IdPedido", idPedido);



                DataSet ds = new Datos().RegresaDataSet(Sp, Parametros);
                foreach (DataRow _fila in ds.Tables[0].Rows)
                {
                    _pedidos.Add(new PedidosFacturadosDetalle()
                    {
                        IdPedido = (int)_fila["IdPedido"],
                        Consecutivo = (int)_fila["Consecutivo"],
                        IdArticulo = (int)_fila["IdArticulo"],
                        ClaveArticulo = _fila["ClaveArticulo"].ToString(),
                        Descripcion = _fila["Descripcion"].ToString(),
                        Cantidad = (decimal)_fila["Cantidad"],
                        Unidad = _fila["Unidad"].ToString(),
                        PrecioUnitario = (decimal)_fila["PrecioUnitario"],
                        SubTotal = (decimal)_fila["SubTotal"],
                        Total = (decimal)_fila["Total"],
                        TotalDescuento = (decimal)_fila["TotalDescuento"],
                        DescuentoPorPieza = (decimal)_fila["DescuentoPorPieza"],
                        IdTienda = (int)_fila["IdTienda"]
                    });
                }
                return this.PintarHtmlDetallePedido(_pedidos);


            }
            catch (Exception Error)
            {
                throw Error;

            }

            //using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            //{

            //    return this.PintarHtmlDetallePedido(db.RegresaDetallePorPedido(idPedido).ToList());
            //}
        }
        public string PintarHtmlDetallePedido(List<PedidosFacturadosDetalle> _detalles)
        {
            StringBuilder _html = new StringBuilder();
            if (_detalles.Count <= 0) return "<p>Sin Información</p>";
            else
            {
                _html.Append("<div class='card'>");
                _html.Append("<div class='header bg-light-blue'>");
                _html.Append("           <h2>");
                _html.Append("Fecha de Emisión");
                _html.Append("  </h2>");

                _html.Append("</div>");
                _html.Append("<div class='body'>");








                _html.Append("<table class='table'>");
                _html.Append("      <thead>");
                _html.Append("          <tr>");
                _html.Append("<th><input id='cchMain'  type='checkbox' onclick='checkTodo(this);' class='chkMain filled-in'><label class='ccMainLabel' for='cchMain'>Seleccionar Todos</label></th>");
                _html.Append("              <th>Cantidad Original</th>");
                _html.Append("              <th>Cantidad</th>");
                _html.Append("              <th>Clave</th>");
                _html.Append("              <th>Descripcion</th>");
                _html.Append("              <th>Unidad</th>");
                _html.Append("              <th>Precio</th>");
                _html.Append("              <th>SubTotal</th>");
                _html.Append("              <th>Desc. Pza</th>");
                _html.Append("              <th>Desc. Total</th>");
                _html.Append("              <th>Total</th>");
                _html.Append("                        </tr>");
                _html.Append("</thead>");
                _html.Append("<tbody>");

                int _contado = 1;

                foreach (PedidosFacturadosDetalle _nivel in _detalles)
                {
                    _html.Append("<tr>");
                    _html.Append("<td scope='row'><input onclick='checkaEsta(this);' descripcion='" + _nivel.Descripcion + "' claveArticulo='" + _nivel.ClaveArticulo + "' idTienda='" + _nivel.IdTienda + "' consecutivo='" + _nivel.Consecutivo + "' idarticulo='" + _nivel.IdArticulo + "' posicion='" + _contado + "' id='chk" + _contado + "' valor='" + _nivel.IdPedido + "' type='checkbox' class='chkPer filled-in'><label class='ccMainLabel' for='chk" + _contado + "'>" + _nivel.Consecutivo + "</label></td>");
                    _html.Append("<td>" + _nivel.Cantidad + "</td>");
                    _html.Append("<td> <input class='numerico' type='number' posicion='" + _contado + "' value='" + _nivel.Cantidad + "' name='txtCantidad" + _contado + "' id='txtCantidad" + _contado + "' min='1' max='" + _nivel.Cantidad + "'></td>");
                    _html.Append("<td id='tdCveArticulo_" + _contado + "'>" + _nivel.ClaveArticulo + "</td>");
                    _html.Append("<td id='tdDescArticulo_" + _contado + "'>" + _nivel.Descripcion + "</td>");
                    _html.Append("<td id='tdUnidad_" + _contado + "'>" + _nivel.Unidad + "</td>");
                    _html.Append("<td id='tdPuArticulo_" + _contado + "'>" + _nivel.PrecioUnitario + "</td>");
                    _html.Append("<td id='tdSubTotalArticulo_" + _contado + "'>" + _nivel.SubTotal + "</td>");
                    _html.Append("<td id='tdDescPzaArticulo_" + _contado + "'>" + _nivel.DescuentoPorPieza + "</td>");
                    _html.Append("<td id='tdDescTotalArticulo_" + _contado + "'>" + _nivel.TotalDescuento + "</td>");
                    _html.Append("<td id='tdTotalArticulo_" + _contado + "'>" + _nivel.Total + "</td>");



                    _html.Append("</tr>");
                    ++_contado;

                }


                _html.Append("</tbody>");
                _html.Append("</table>");
                _html.Append("        </div>");
                _html.Append("</div>");

            }
            return _html.ToString();

        }
        #endregion
        #region obtenerDetallePorPedido
        public string obtenerDetalleClientePorPedido(int idPedido, ref PedidosFacturadosCliente _cliente)
        {
            List<PedidosFacturadosCliente> _pedidos = new List<PedidosFacturadosCliente>();
            try
            {

                string Sp = "dbo.RegresaDatosClienteFactura";
                SqlParameter[] Parametros = new SqlParameter[1];
                Parametros[0] = new SqlParameter("@IdPedido", idPedido);



                DataSet ds = new Datos().RegresaDataSet(Sp, Parametros);
                foreach (DataRow _fila in ds.Tables[0].Rows)
                {
                    _pedidos.Add(new PedidosFacturadosCliente()
                    {
                        IdClienteFactura = _fila["IdClienteFactura"].ToString(),
                        Cliente = _fila["Cliente"].ToString(),
                        Calle = _fila["Calle"].ToString(),
                        Colonia = _fila["Colonia"].ToString(),
                        CP = _fila["CP"].ToString(),
                        DelegacionMunicipio = _fila["DelegacionMunicipio"].ToString(),
                        Estado = _fila["Estado"].ToString(),
                        Pais = _fila["Pais"].ToString(),
                        RFC = _fila["RFC"].ToString(),
                        TasaIva = (decimal)_fila["FactorIVA"],

                        SubTotal = (decimal)_fila["SubTotal"],
                        Iva = (decimal)_fila["Iva"],
                        Total = (decimal)_fila["Total"],
                        IdNotaCredito = (int)_fila["IdNotaDeCredito"],
                        UsoCfdi = _fila["UsoCFDI"].ToString(),
                        TipoDocumento = _fila["TipoDeDocumento"].ToString(),
                        CfdiRelacionado = _fila["CFDIRelacionado"].ToString().Replace("|", "\n")



                    });

                }
                if (_pedidos.Count > 0) _cliente = _pedidos[0];
                return this.PintarHtmlDetalleClientePedido(_pedidos);


            }
            catch (Exception Error)
            {
                throw Error;

            }



            /*   using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
               {

                   return this.PintarHtmlDetalleClientePedido(db.RegresaDatosClienteFactura(idPedido).ToList());
               }*/
        }
        public string PintarHtmlDetalleClientePedido(List<PedidosFacturadosCliente> _detalles)
        {
            StringBuilder _html = new StringBuilder();

            if (_detalles.Count <= 0) return "<p>Sin Información</p>";
            else
            {
                _html.Append("<div class='card'>");
                _html.Append("<div class='header bg-light-blue'>");
                _html.Append("           <h2>");
                _html.Append("Datos del Receptor");
                _html.Append("  </h2>");
                _html.Append("<ul class='header-dropdown m-r-0'>");
                _html.Append("                <li>");
                _html.Append("                    <a href='javascript:void(0);'>");
                _html.Append("                                  <i class='material-icons'>info_outline</i>");
                _html.Append("                     </a>");
                _html.Append(" </li>");
                _html.Append("</ul>");
                _html.Append("</div>");
                _html.Append("<div class='body'>");
                _html.Append("<p>" + _detalles[0].Cliente + "</p>");
                _html.Append("<p>" + _detalles[0].Calle + "</p>");
                _html.Append("<p>" + _detalles[0].Colonia + "</p>");
                _html.Append("<p>C.P. " + _detalles[0].CP + "</p>");
                _html.Append("<p>" + _detalles[0].DelegacionMunicipio + "," + _detalles[0].Estado + "," + _detalles[0].Pais + "</p>");
                _html.Append("<p>RFC: " + _detalles[0].RFC + "</p>");
                _html.Append("<input type='hidden' value='" + _detalles[0].TasaIva + "' id='tasaIva' />");
                _html.Append("<input type='hidden' value='" + _detalles[0].IdClienteFactura + "' id='clienteFactura' />");

                _html.Append("        </div>");
                _html.Append("</div>");

            }
            return _html.ToString();

        }
        #endregion
        #region obtenerFechaPedido
        public string obtenerFechaPedido()
        {

            StringBuilder _html = new StringBuilder();
            _html.Append("<div class='card'>");
            _html.Append("<div class='header bg-light-blue'>");
            _html.Append("           <h2>");
            _html.Append("Fecha de Emisión");
            _html.Append("  </h2>");
            _html.Append("</div>");
            _html.Append("<div class='body'>");
            _html.Append("<center><p>" + DateTime.Now.ToString("s") + "</p></center>");
            _html.Append("        </div>");
            _html.Append("</div>");
            return _html.ToString();
        }

        #endregion

        #region Catalogos
        public List<CatalogoCfdi> obtenerUsoCfdi()
        {
            List<CatalogoCfdi> _catalogo = new List<CatalogoCfdi>();
            try
            {

                string Sp = "dbo.ComboCatalogoUsoCFDI";
                //SqlParameter[] Parametros = new SqlParameter[1];
                //Parametros[0] = new SqlParameter("@IdPedido", idPedido);



                DataSet ds = new Datos().RegresaDataSet(Sp, null);
                foreach (DataRow _fila in ds.Tables[0].Rows)
                {
                    _catalogo.Add(new CatalogoCfdi()
                    {
                        Codigo = _fila["Clave"].ToString(),
                        Descripcion = _fila["Descripcion"].ToString()

                    });
                }
                return _catalogo;
            }
            catch (Exception Error)
            {
                throw Error;

            }
        }
        public List<CatalogoCfdi> obtenerTipoCfdi()
        {
            List<CatalogoCfdi> _catalogo = new List<CatalogoCfdi>();
            try
            {

                string Sp = "dbo.ComboCatalogoTipoDeDocumento";
                //SqlParameter[] Parametros = new SqlParameter[1];
                //Parametros[0] = new SqlParameter("@IdPedido", idPedido);



                DataSet ds = new Datos().RegresaDataSet(Sp, null);
                foreach (DataRow _fila in ds.Tables[0].Rows)
                {
                    _catalogo.Add(new CatalogoCfdi()
                    {
                        Codigo = _fila["Clave"].ToString(),
                        Descripcion = _fila["Descripcion"].ToString()

                    });
                }
                return _catalogo;
            }
            catch (Exception Error)
            {
                throw Error;

            }
        }
        #endregion
        #region GuardarNota
        public string guardarNota(NotaCreditoHeader _nota, List<PedidosFacturadosDetalle> _detalle, long idsesion)
        {

            try
            {

                string Sp = "dbo.GuardarNotaDeCredito";
                SqlParameter[] Parametros = new SqlParameter[9];
                Parametros[0] = new SqlParameter("@IdPedido", _nota.IdPedido);
                Parametros[1] = new SqlParameter("@IdClienteFactura", _nota.IdClienteFactura);
                Parametros[2] = new SqlParameter("@UsoCfdi", _nota.UsoCfdi);
                Parametros[3] = new SqlParameter("@TipoDeDocumento", _nota.TipoDeDocumento);
                Parametros[4] = new SqlParameter("@SubTotal", _nota.SubTotal);
                Parametros[5] = new SqlParameter("@Iva", _nota.Iva);
                Parametros[6] = new SqlParameter("@Total", _nota.Total);
                Parametros[7] = new SqlParameter("@TasaIva", _nota.TasaIva);
                Parametros[8] = new SqlParameter("@IdSesion", idsesion);




                string _valor = new Datos().RegresaValor(Sp, Parametros);
                foreach (PedidosFacturadosDetalle _det in _detalle)
                {
                    this.guardarDetalleNota(_det, idsesion, int.Parse(_valor));
                }
                if (_nota.CfdiRelacionado == null) _nota.CfdiRelacionado = string.Empty;
                _nota.CfdiRelacionado = _nota.CfdiRelacionado.Trim();
                if (_nota.CfdiRelacionado != string.Empty)
                {
                    string[] _cfdis = _nota.CfdiRelacionado.Split('\n');
                    foreach (string _cfd in _cfdis)
                    {
                        if (_cfd.Trim() != string.Empty)
                        {
                            string _rel = this.guardarDetalleCfdiRelacionado(int.Parse(_valor), _nota.IdPedido, _cfd.Trim(), idsesion);
                        }
                    }

                }


                var Respuesta = new NotaDeCreditoTxtManager().GenerarTxtNotaDeCredito(int.Parse(_valor));
                return _valor;



            }
            catch (Exception Error)
            {
                throw Error;

            }
        }
        public string guardarDetalleNota(PedidosFacturadosDetalle _nota, long idsesion, int idNota)
        {

            try
            {

                string Sp = "dbo.GuardarDetalleNotaDeCredito";
                SqlParameter[] Parametros = new SqlParameter[14];
                Parametros[0] = new SqlParameter("@IdNotaDeCredito", idNota);
                Parametros[1] = new SqlParameter("@IdPedido", _nota.IdPedido);
                Parametros[2] = new SqlParameter("@Consecutivo", _nota.Consecutivo);
                Parametros[3] = new SqlParameter("@IdArticulo", _nota.IdArticulo);
                Parametros[4] = new SqlParameter("@ClaveArticulo", _nota.ClaveArticulo);
                Parametros[5] = new SqlParameter("@Descripcion", _nota.Descripcion);
                Parametros[6] = new SqlParameter("@Cantidad", _nota.Cantidad);
                Parametros[7] = new SqlParameter("@Unidad", _nota.Unidad);
                Parametros[8] = new SqlParameter("@PrecioUnitario", _nota.PrecioUnitario);
                Parametros[9] = new SqlParameter("@SubTotal", _nota.SubTotal);
                Parametros[10] = new SqlParameter("@TotalDescuento", _nota.TotalDescuento);
                Parametros[11] = new SqlParameter("@DescuentoPorPieza", _nota.DescuentoPorPieza);
                Parametros[12] = new SqlParameter("@Total", _nota.Total);
                Parametros[13] = new SqlParameter("@IdSesion", idsesion);
                string _valor = new Datos().ValidaEjecucion(Sp, Parametros);
                return _valor;
            }
            catch (Exception Error)
            {
                throw Error;

            }
        }
        public string guardarDetalleCfdiRelacionado(int idNota, int idPedido, string relacionado, long idsesion)
        {

            try
            {

                string Sp = "dbo.GuardarCFDIRelacionado";
                SqlParameter[] Parametros = new SqlParameter[4];
                Parametros[0] = new SqlParameter("@IdPedido", idPedido);
                Parametros[1] = new SqlParameter("@IdNotadeCredito", idNota);
                Parametros[2] = new SqlParameter("@CFDIRelacionado", relacionado);
                Parametros[3] = new SqlParameter("@IdSesion", idsesion);
                string _valor = new Datos().ValidaEjecucion(Sp, Parametros);
                return _valor;
            }
            catch (Exception Error)
            {
                throw Error;

            }
        }

        #endregion
        #region desligarNota
        public string desligarNota(int idNota, long idsesion)
        {

            try
            {

                string Sp = "dbo.EliminarNotaDeCredito";
                SqlParameter[] Parametros = new SqlParameter[2];
                Parametros[0] = new SqlParameter("@IdNotaDeCredito", idNota);
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