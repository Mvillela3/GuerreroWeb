using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace GuerreroWeb.Controllers
{
    public class DBConexion
    {
#if Debug
    public static string Base1 = "PRODUCCION";
#elif Release
    public static string Base1 = "PRODUCCION";
#else
        public static string Base1 = "PRUEBAS";
#endif

        ModConexion mconexion = new ModConexion();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[Base1].ConnectionString);
        public SqlConnection AbreConexion()
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }
        public SqlConnection CierraConexion()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;

        }

        public class DBComandos
        {
            //ModConexion mconexion;//= new ModConexion();
            SqlCommand Cmd = new SqlCommand();
            public SqlCommand Comandos(SqlConnection con1, string sql, CommandType tipo, List<SqlParameter> parametros)
            {
                Cmd.CommandType = tipo;
                Cmd.CommandText = sql;
                Cmd.Parameters.Clear();
                for (int i = 0; i < parametros.Count; i++)
                {
                    Cmd.Parameters.Add(parametros[i]);
                }
                Cmd.Connection = con1;

                return Cmd;
                //return new SqlCommand(pcomando, pcnn as SqlConnection);
            }

        }
        public class DBSpid
        {
            public string GetSPID()
            {
                //DatosAuthResponse datosRespuesta = new DatosAuthResponse();
                //Usuario usuario1 = new Usuario();
                //var listaParams = new List<SqlParameter>();
                String sql = "";
                DBConexion conexion = new DBConexion();
                DBComandos Cmd = new DBComandos();

                var listaParams = new List<SqlParameter>();
                //listaParams.Clear;
                //listaParams.Add(new SqlParameter("@pswd", datos.Password));

                sql = "SELECT @@SPID AS 'Id'";
                try
                {
                    using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader["Id"].ToString();
                        }

                    }
                    return "0";

                }
                catch (Exception)
                {
                    return "0";
                }

            }

        }


    }
}