using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
namespace Troncoso.Models.EntityManager
{
    public class NotaDeCreditoTxtManager
    {

        string Ruta = System.Configuration.ConfigurationManager.AppSettings["rutaLocal"].ToString();
        string RutaCopiado = System.Configuration.ConfigurationManager.AppSettings["rutaCopiado"].ToString();
        Boolean UsaRutacopiado = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["usaRutaCopiado"].ToString());

        public string GenerarTxtNotaDeCredito(int IdNotaDeCredito)
        {
            string Resultado = "OK";
            try
            {
                CrearDirectorio(Ruta);
                string RutaNota = Ruta + IdNotaDeCredito.ToString() + ".txt";
                EliminarArchivo(RutaNota);

                

                DataSet DsNC = DatosNotaDeCreditoTxt(IdNotaDeCredito);
                DataSet DsNCD = DatosDetalleNotaDeCreditoTxt(IdNotaDeCredito);
                DataSet DsNCCFDI = DatosCFDIRelacionadoNotaDeCreditoTxt(IdNotaDeCredito);

                StreamWriter sw = new StreamWriter(RutaNota);

                string SubTotal = DsNC.Tables[0].Rows[0]["SubTotal"].ToString();
                string Total = DsNC.Tables[0].Rows[0]["Total"].ToString();
                SubTotal = "".PadLeft(16 - SubTotal.Length) + SubTotal;
                Total = "".PadLeft(16 - Total.Length) + Total;

                sw.WriteLine("[CFD]");
                sw.WriteLine("FOLIO;" + DsNC.Tables[0].Rows[0]["Folio"].ToString());
                sw.WriteLine("SERIE; " + DsNC.Tables[0].Rows[0]["Serie"].ToString());
                sw.WriteLine("FECHA;" + DsNC.Tables[0].Rows[0]["Fecha"].ToString());
                sw.WriteLine("SELLO;" + DsNC.Tables[0].Rows[0]["Sello"].ToString());
                sw.WriteLine("NOCERTIFICADO;" + DsNC.Tables[0].Rows[0]["NumeroCertificado"].ToString());
                sw.WriteLine("FORMADEPAGO;" + DsNC.Tables[0].Rows[0]["FormaDePago"].ToString());
                sw.WriteLine("CONDICIONESDEPAGO;" + DsNC.Tables[0].Rows[0]["CondicionesDePago"].ToString());
                sw.WriteLine("SUBTOTAL;" + SubTotal);
                sw.WriteLine("DESCUENTO;" + DsNC.Tables[0].Rows[0]["Descuento"].ToString());
                sw.WriteLine("MONEDA;" + DsNC.Tables[0].Rows[0]["Moneda"].ToString());
                sw.WriteLine("TIPODECAMBIO;" + DsNC.Tables[0].Rows[0]["TipoDeCambio"].ToString());
                sw.WriteLine("TOTAL;" + Total);
                sw.WriteLine("TIPODECOMPROBANTE;" + DsNC.Tables[0].Rows[0]["TipoDeDocumento"].ToString());
                //sw.WriteLine("TIPODECOMPROBANTE;E");
                sw.WriteLine("METODODEPAGO;" + DsNC.Tables[0].Rows[0]["MetodoDePago"].ToString());
                sw.WriteLine("LUGAREXPEDICION;" + DsNC.Tables[0].Rows[0]["LugarDeExpedicion"].ToString());
                sw.WriteLine("CONFIRMACION; ");
                sw.WriteLine("");

                if (DsNCCFDI.Tables[0].Rows.Count > 0)
                {
                    sw.WriteLine("[CFDIRELACIONADOS]");
                    sw.WriteLine("TIPORELACION;" + DsNC.Tables[0].Rows[0]["TipoRelacion"].ToString());
                    //sw.WriteLine("TIPORELACION;04");
                    for (int i = 0; i < DsNCCFDI.Tables[0].Rows.Count; i++)
                    {
                        sw.WriteLine("TIPORELACIONUUID;" + DsNCCFDI.Tables[0].Rows[i]["CFDIRelacionado"].ToString());
                    }
                    sw.WriteLine("");
                }

                sw.WriteLine("[EMISOR]");
                sw.WriteLine("RFC;" + DsNC.Tables[0].Rows[0]["RFCEmpresa"].ToString());
                sw.WriteLine("NOMBRE;" + DsNC.Tables[0].Rows[0]["NombreEmpresa"].ToString());
                sw.WriteLine("REGIMENFISCAL;" + DsNC.Tables[0].Rows[0]["RegimenFiscal"].ToString());

                sw.WriteLine("");
                sw.WriteLine("[RECEPTOR]");
                sw.WriteLine("RFC;" + DsNC.Tables[0].Rows[0]["RFCCliente"].ToString());
                sw.WriteLine("RAZONSOCIAL;" + DsNC.Tables[0].Rows[0]["Cliente"].ToString());
                sw.WriteLine("RESIDENCIAFISCAL;" + DsNC.Tables[0].Rows[0]["ResidenciaFiscal"].ToString());
                sw.WriteLine("NUMREGIDTRIB;" + DsNC.Tables[0].Rows[0]["NumRegIdTrib"].ToString());
                sw.WriteLine("USOCFDI;" + DsNC.Tables[0].Rows[0]["UsoCFDI"].ToString());
                sw.WriteLine("");

                for (int i = 0; i < DsNCD.Tables[0].Rows.Count; i++)
                {
                    sw.WriteLine("[CONCEPTOS]");
                    sw.WriteLine("CLAVEPRODSERV;" + DsNCD.Tables[0].Rows[i]["ClaveProductoServicio"].ToString());
                    sw.WriteLine("CANTIDAD;" + DsNCD.Tables[0].Rows[i]["Cantidad"].ToString());
                    sw.WriteLine("CLAVEUNIDAD;" + DsNCD.Tables[0].Rows[i]["TipoArticulo"].ToString());
                    sw.WriteLine("UNIDAD;" + DsNCD.Tables[0].Rows[i]["Unidad"].ToString());
                    sw.WriteLine("DESCRIPCION;" + DsNCD.Tables[0].Rows[i]["Descripcion"].ToString());
                    sw.WriteLine("PRECIO;" + DsNCD.Tables[0].Rows[i]["PrecioUnitario"].ToString());
                    sw.WriteLine("IMPORTE;" + DsNCD.Tables[0].Rows[i]["Importe"].ToString());
                    sw.WriteLine("DESCUENTO;" + DsNCD.Tables[0].Rows[i]["Descuento"].ToString());
                    sw.WriteLine("[IMPUESTO_DETALLE_TRASLADO];");
                    sw.WriteLine("BASE;" + DsNCD.Tables[0].Rows[i]["Base"].ToString());
                    sw.WriteLine("CODIGO;" + DsNCD.Tables[0].Rows[i]["ClaveImpuestoTrasladado"].ToString());
                    sw.WriteLine("TIPOFACTOR;" + DsNCD.Tables[0].Rows[i]["TipoFactorImpuestoTrasladado"].ToString());
                    sw.WriteLine("TASAOCUOTA;" + DsNCD.Tables[0].Rows[i]["TasaCuota"].ToString());
                    sw.WriteLine("IMPORTE;" + DsNCD.Tables[0].Rows[i]["ImporteImpuestos"].ToString());
                    sw.WriteLine("");
                }

                sw.WriteLine("[IMPUESTO_TOTAL_TRASLADADOS]");
                sw.WriteLine("TOTAL;" + DsNC.Tables[0].Rows[0]["TotalImpuestosTrasladados"].ToString());
                sw.WriteLine("CODIGO;" + DsNC.Tables[0].Rows[0]["ClaveImpuestoTrasladado"].ToString());
                sw.WriteLine("TIPOFACTOR;" + DsNCD.Tables[0].Rows[0]["TipoFactorImpuestoTrasladado"].ToString());
                sw.WriteLine("TASAOCUOTA;" + DsNCD.Tables[0].Rows[0]["TasaCuota"].ToString());
                sw.WriteLine("IMPORTE;" + DsNC.Tables[0].Rows[0]["TotalImpuestosTrasladados"].ToString());
                sw.WriteLine("");
                sw.Close();

                if (UsaRutacopiado)
                {
                    CrearDirectorio(RutaCopiado);
                    string RutaNotaCopiado = RutaCopiado + IdNotaDeCredito.ToString() + ".txt";
                    EliminarArchivo(RutaNotaCopiado);


                    System.IO.File.Copy(RutaNota, RutaNotaCopiado, true);

                }
            }
            catch (Exception Error)
            {
                Resultado = Error.Message;
            }
            return Resultado;
        }
        public void CrearDirectorio(string Ruta)
        {
            try
            {
                if (!Directory.Exists(Ruta))
                {
                    Directory.CreateDirectory(Ruta);
                }
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }
        public void EliminarArchivo(string RutaEliminar)
        {
            try
            {
                if (System.IO.File.Exists(RutaEliminar)) { System.IO.File.Delete(RutaEliminar); }
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }
        private DataSet DatosNotaDeCreditoTxt(int IdNotaDeCredito)
        {
            try
            {
                string Sp = "dbo.DatosNotaDeCreditoTxt";
                SqlParameter[] Parametros = new SqlParameter[1];
                Parametros[0] = new SqlParameter("@IdNotaDeCredito", IdNotaDeCredito);
                return new Datos().RegresaDataSet(Sp, Parametros);
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }
        private DataSet DatosDetalleNotaDeCreditoTxt(int IdNotaDeCredito)
        {
            try
            {
                string Sp = "dbo.DatosDetalleNotaDeCreditoTxt";
                SqlParameter[] Parametros = new SqlParameter[1];
                Parametros[0] = new SqlParameter("@IdNotaDeCredito", IdNotaDeCredito);
                return new Datos().RegresaDataSet(Sp, Parametros);
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }
        private DataSet DatosCFDIRelacionadoNotaDeCreditoTxt(int IdNotaDeCredito)
        {
            try
            {
                string Sp = "dbo.DatosCFDIRelacionadoNotaDeCreditoTxt";
                SqlParameter[] Parametros = new SqlParameter[1];
                Parametros[0] = new SqlParameter("@IdNotaDeCredito", IdNotaDeCredito);
                return new Datos().RegresaDataSet(Sp, Parametros);
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }
    }
}