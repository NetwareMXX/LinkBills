using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace ReportesCheverolet
{
    public partial class Reporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {


                if (Request.QueryString["asasdasda"] != null && Request.QueryString["asdasdaasfv"] != null)
                {
                    int _idmes = 5, _idusuario = 1;
                    if (int.TryParse(Request.QueryString["asasdasda"], out _idmes) && int.TryParse(Request.QueryString["asdasdaasfv"], out _idusuario))
                    {

                        try
                        {
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Chevrolet_Libro.rdlc");
                            string Sp = "dbo.ProcesarLibro";
                            SqlParameter[] Parametros = new SqlParameter[2];
                            Parametros[0] = new SqlParameter("@IdMes", _idmes);
                            Parametros[1] = new SqlParameter("@IdUsuario", _idusuario);
                            System.Data.DataSet _valor = new Datos().RegresaDataSet(Sp, Parametros);
                            ReportDataSource RDS = new ReportDataSource("Datos", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS);

                            Sp = "dbo.BuscarCostos";
                            SqlParameter[] Parametros2 = new SqlParameter[1];
                            Parametros2[0] = new SqlParameter("@IdUsuario", _idusuario);
                            _valor = new Datos().RegresaDataSet(Sp, Parametros2);
                            ReportDataSource RDS2 = new ReportDataSource("Costos", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS2);

                            SqlParameter[] Parametros3 = new SqlParameter[1];
                            Parametros3[0] = new SqlParameter("@IdUsuario", _idusuario);
                            Sp = "dbo.BuscarVentas";
                            _valor = new Datos().RegresaDataSet(Sp, Parametros3);
                            ReportDataSource RDS3 = new ReportDataSource("Ventas", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS3);
                            ReportViewer1.LocalReport.Refresh();

                        }
                        catch (SqlException sqlex)
                        {
                            Response.Redirect("SinPermiso.html");
                        }
                        catch (Exception ex)
                        {
                            Response.Redirect("SinPermiso.html");
                        } 

                    }
                    else
                    {
                        Response.Redirect("SinPermiso.html");
                    }
                }
                else
                {
                    Response.Redirect("SinPermiso.html");
                }












            }
        }
    }
}