using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace poligramvc.Models
{
    public class BDConn
    {
        public static IDbConnection AbreConexion()
        {
            IDbConnection con;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnBD"].ConnectionString);
            con.Open();

            return con;
        }

        public static void CierraConexion(IDbConnection con)
        {
            con.Dispose();
            con = null;
        }

        public static SqlConnection getConnection()
        {
            SqlConnection conn = new SqlConnection();
            string CadenaDeConexion = ConfigurationManager.ConnectionStrings["ConnBD"].ConnectionString;
            conn.ConnectionString = CadenaDeConexion;
            conn.Open();
            return conn;
        }

        public static string getCadenaConexion()
        {
            SqlConnection conn = new SqlConnection();
            string CadenaDeConexion = ConfigurationManager.ConnectionStrings["ConnBD"].ConnectionString;
            return CadenaDeConexion;
        }

        public static DataTable ConsultarBD(string nombreStore, Dictionary<string, string> parametros)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = BDConn.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (KeyValuePair<String, String> entry in parametros)
                {
                    cmd.Parameters.Add(new SqlParameter(entry.Key, entry.Value));
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt.Columns.Add("Columna1");
                DataRow Row = dt.NewRow();
                Row["Columna1"] = e.Message.ToString();
                //workRow[1] = "ERROR";
                dt.Rows.Add(Row);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #region Seleccionar datos con Store Procedure
        public static DataTable ConsultarBD(string nombreStore, List<parametros> parametros)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = null;
            try
            {
                conn = BDConn.getConnection();
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var p in parametros)
                {
                    switch (p.tipodato)
                    {
                        case "string":
                            cmd.Parameters.Add(p.parametro, SqlDbType.VarChar).Value = p.valorCadena;
                            break;
                        case "int":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Int).Value = p.valorEntero;
                            break;
                        case "double":
                            cmd.Parameters.Add(p.parametro, SqlDbType.BigInt).Value = p.valorDouble;
                            break;
                        case "datetime":
                            cmd.Parameters.Add(p.parametro, SqlDbType.DateTime).Value = p.valorFecha ?? null;
                            break;
                    }
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }
            catch
            {
                dt.Columns.Add("FolioAsignado");
                DataRow Row = dt.NewRow();
                Row["FolioAsignado"] = "ERROR";
                //workRow[1] = "ERROR";
                dt.Rows.Add(Row);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion

        #region Ejecutar Store Procedure con parametro de salida int
        public static int EjecutarStoreProc(string nombreStore, List<parametros> parametros, Boolean ParamOutInt, string nombreParamOut)
        {
            int exito = 0;
            SqlConnection conn = BDConn.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandTimeout = 200;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var p in parametros)
                {
                    switch (p.tipodato)
                    {
                        case "string":
                            cmd.Parameters.Add(p.parametro, SqlDbType.VarChar).Value = p.valorCadena;
                            break;
                        case "int":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Int).Value = p.valorEntero;
                            break;
                        case "double":
                            cmd.Parameters.Add(p.parametro, SqlDbType.BigInt).Value = p.valorDouble;
                            break;
                        case "datetime":
                            cmd.Parameters.Add(p.parametro, SqlDbType.DateTime).Value = p.valorFecha ?? null;
                            break;
                    }
                }
                if (ParamOutInt == true)
                {
                    cmd.Parameters.Add(nombreParamOut, SqlDbType.Int);
                    cmd.Parameters[nombreParamOut].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    exito = (Int32)cmd.Parameters[nombreParamOut].Value;

                }
                else
                {
                    cmd.ExecuteNonQuery();
                    exito = 1;
                }

            }
            catch (Exception e)
            {
                exito = -1;
            }
            finally
            {
                conn.Close();
            }

            return exito;
        }
        #endregion

        #region Ejecutar Store Procedure con parametro de salida string
        public static string EjecutarStoreProc_string(string nombreStore, List<parametros> parametros, Boolean ParamOutInt, string nombreParamOut)
        {
            string exito = "";
            SqlConnection conn = BDConn.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandTimeout = 200;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var p in parametros)
                {
                    switch (p.tipodato)
                    {
                        case "string":
                            cmd.Parameters.Add(p.parametro, SqlDbType.VarChar).Value = p.valorCadena;
                            break;
                        case "int":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Int).Value = p.valorEntero;
                            break;
                        case "double":
                            cmd.Parameters.Add(p.parametro, SqlDbType.BigInt).Value = p.valorDouble;
                            break;
                        case "decimal":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Money).Value = p.valorDecimal;
                            break;
                        case "datetime":
                            cmd.Parameters.Add(p.parametro, SqlDbType.DateTime).Value = p.valorFecha ?? null;
                            break;
                        case "date":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Date).Value = p.valorFecha ?? null;
                            break;
                        case "boleano":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Bit).Value = p.valorBoleano;
                            break;
                    }
                }
                if (ParamOutInt == true)
                {
                    cmd.Parameters.Add(nombreParamOut, SqlDbType.VarChar, 50);
                    cmd.Parameters[nombreParamOut].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    exito = cmd.Parameters[nombreParamOut].Value.ToString();

                }
                else
                {
                    cmd.ExecuteNonQuery();
                    exito = "OK";
                }

            }
            catch (Exception e)
            {
                exito = e.ToString();
            }
            finally
            {
                conn.Close();
            }

            return exito;
        }
        #endregion

    }

    #region clase para parámetros de store
    public class parametros
    {
        public string parametro { get; set; }
        public string tipodato { get; set; }
        public string valorCadena { get; set; }
        public int valorEntero { get; set; }
        public double valorDouble { get; set; }
        public Decimal valorDecimal { get; set; }
        public DateTime? valorFecha { get; set; }
        public bool valorBoleano { get; set; }
    }
    #endregion
}