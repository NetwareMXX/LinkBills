using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;


namespace ReportesCheverolet
{
    public class Datos
    {
        #region Datos
        string cadena;
        public Datos()
        {
            cadena = System.Configuration.ConfigurationManager.ConnectionStrings["ReportesConnectionString"].ConnectionString;
        }
        public string Cadena { get { return this.cadena; } }
        #endregion
        #region Sin Transacción
        public string RegresaValor(string ProcedimientoAlmacenado, SqlParameter[] Parametros)
        {
            SqlConnection Conexion = new SqlConnection(this.cadena);
            string regresa = "";
            if (Conexion.State == ConnectionState.Open) { Conexion.Close(); }
            try
            {
                Conexion.Open();
                SqlCommand Comando = Conexion.CreateCommand();
                Comando.CommandTimeout = 300;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = ProcedimientoAlmacenado;
                if (Parametros != null) { Comando.Parameters.AddRange(Parametros); }
                regresa = Comando.ExecuteScalar().ToString();
                Conexion.Close();
                Conexion.Dispose();
            }
            catch (Exception e)
            {
                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                    Conexion.Dispose();
                }
                regresa = e.Message.ToString();
            }
            return regresa;
        }
        public string ValidaEjecucion(string ProcedimientoAlmacenado, SqlParameter[] Parametros)  //Transaccion
        {
            SqlConnection Conexion = new SqlConnection(this.cadena);
            string regresa = "OK";
            if (Conexion.State == ConnectionState.Open) { Conexion.Close(); }
            try
            {
                Conexion.Open();
                SqlCommand Comando = Conexion.CreateCommand();
                Comando.CommandTimeout = 3600;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = ProcedimientoAlmacenado;
                if (Parametros != null) { Comando.Parameters.AddRange(Parametros); }
                Comando.ExecuteNonQuery();
                Conexion.Close();
                Conexion.Dispose();
            }
            catch (Exception e)
            {
                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                    Conexion.Dispose();
                }
                regresa = e.Message.ToString();
            }
            return regresa;
        }
        public string RegresaValorParametro(string ProcedimientoAlmacenado, SqlParameter[] Parametros, SqlParameter ParametroRetorna)
        {
            SqlConnection Conexion = new SqlConnection(this.cadena);
            string regresa = "";
            if (Conexion.State == ConnectionState.Open) { Conexion.Close(); }
            try
            {
                Conexion.Open();
                SqlCommand Comando = Conexion.CreateCommand();
                Comando.CommandTimeout = 300;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = ProcedimientoAlmacenado;
                if (Parametros != null) { Comando.Parameters.AddRange(Parametros); }
                ParametroRetorna.Direction = ParameterDirection.Output;
                Comando.Parameters.Add(ParametroRetorna);
                Comando.ExecuteScalar();
                regresa = ParametroRetorna.Value.ToString();
                Conexion.Close();
                Conexion.Dispose();
            }
            catch (Exception e)
            {
                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                    Conexion.Dispose();
                }
                regresa = e.ToString();
            }
            return regresa;
        }
        public DataSet RegresaDataSet(string ProcedimientoAlmacenado, SqlParameter[] Parametros)
        {
            SqlConnection Conexion = new SqlConnection(this.cadena);
            if (Conexion.State == ConnectionState.Open) { Conexion.Close(); }
            try
            {
                SqlCommand Comando = Conexion.CreateCommand();
                Comando.CommandTimeout = 300;
                Conexion.Open();
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = ProcedimientoAlmacenado;
                if (Parametros != null) { Comando.Parameters.AddRange(Parametros); }
                using (SqlDataAdapter Adaptador = new SqlDataAdapter(Comando))
                {
                    DataSet Ds = new DataSet();
                    Adaptador.Fill(Ds);
                    Adaptador.Dispose();
                    Conexion.Close();
                    Conexion.Dispose();
                    return Ds;
                }
            }
            catch (Exception e)
            {
                if (Conexion.State == ConnectionState.Open) { Conexion.Close(); Conexion.Dispose(); }
                throw e;
            }
        }
        public DataTable RegresaDataTable(string ProcedimientoAlmacenado, SqlParameter[] Parametros)
        {
            SqlConnection Conexion = new SqlConnection(this.cadena);
            if (Conexion.State == ConnectionState.Open) { Conexion.Close(); }
            try
            {
                SqlCommand Comando = Conexion.CreateCommand();
                Comando.CommandTimeout = 300;
                Conexion.Open();
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = ProcedimientoAlmacenado;
                if (Parametros != null) { Comando.Parameters.AddRange(Parametros); }
                using (SqlDataAdapter Adaptador = new SqlDataAdapter(Comando))
                {
                    DataTable Dt = new DataTable();
                    Adaptador.Fill(Dt);
                    Adaptador.Dispose();
                    Conexion.Close();
                    Conexion.Dispose();
                    return Dt;
                }
            }
            catch (Exception e)
            {
                if (Conexion.State == ConnectionState.Open) { Conexion.Close(); Conexion.Dispose(); }
                throw e;
            }
        }
        public string ValidaEjecutaConsultaString(string Consulta)
        {
            string Regresa = "OK";
            SqlConnection cnn = new SqlConnection(this.cadena);
            try
            {
                SqlCommand cmd = new SqlCommand(Consulta, cnn);
                cmd.CommandTimeout = 300;
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                cnn.Dispose();
            }
            catch (Exception e)
            {
                if (cnn.State == ConnectionState.Open) { cnn.Close(); cnn.Dispose(); }
                Regresa = e.ToString();
            }
            return Regresa;
        }
        #endregion
        #region Excel
        public string ConectaExcel(string Ext, string Ruta)
        {
            string Regresa = "";
            switch (Ext.ToLower())
            {
                case ".xls":
                    Regresa = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Ruta + ";Extended Properties=Excel 8.0;";
                    //Regresa = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Ruta + ";Extended Properties='Excel 8.0;HDR=Yes'";
                    break;
                case ".xlsx":
                    Regresa = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Ruta + ";Extended Properties='Excel 8.0;HDR=Yes'";
                    break;
            }
            return Regresa;
        }
        public string NombrePestañaExcel(string Ruta, string Ext)
        {
            string Cadena = ConectaExcel(Ext, Ruta);
            string Pestaña = "", UltimoCaracter = "";
            OleDbConnection Conexion = new OleDbConnection(Cadena);
            try
            {
                Conexion.Open();
                DataTable excelSchema = Conexion.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in excelSchema.Rows)
                {
                    if (!row["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                    {
                        Pestaña = row["TABLE_NAME"].ToString();
                        UltimoCaracter = Pestaña.Substring((Pestaña.Length - 1), 1);
                        if (UltimoCaracter == "$")
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                    Conexion.Dispose();
                }
                throw e;
            }
            finally
            {
                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                    Conexion.Dispose();
                }
            }
            return Pestaña;
        }
        #endregion
    }
}