using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static GuerreroWeb.Controllers.DBConexion;

namespace GuerreroWeb.Controllers
{
    public class CtrlFiscal
    {
        private DBConexion conexion = new DBConexion();
        private DBComandos Cmd = new DBComandos();

        public List<CatFormaPago> DdlFormaPago()
        {
            var Lista = new List<CatFormaPago>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatFormaPago ";
            sql += " order by Descripcion";

            listaParams.Clear();
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatFormaPago = new CatFormaPago();
                        CatFormaPago.Codigo = reader["Codigo"].ToString();
                        CatFormaPago.Descripcion = reader["Descripcion"].ToString();

                        Lista.Add(CatFormaPago);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatFormaPago = new CatFormaPago();
                CatFormaPago.Codigo = ex.HResult.ToString();
                CatFormaPago.Descripcion = "Error al llenar las Formas de Pago" + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatFormaPago);
            }
            return Lista;

        }
        public RespuestaSQL FormaPAdd(CatFormaPago modCatFormaPago)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpFormaPAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Codigo", modCatFormaPago.Codigo));
            listaParams.Add(new SqlParameter("@Descripcion", modCatFormaPago.Descripcion));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Guardar la Forma de Pago" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL FormaPMod(CatFormaPago modCatFormaPago)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpFormaPMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Codigo", modCatFormaPago.Codigo));
            listaParams.Add(new SqlParameter("@Descripcion", modCatFormaPago.Descripcion));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Modificar la Forma de Pago" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL FormaPDel(CatFormaPago modCatFormaPago)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpFormaPDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Codigo", modCatFormaPago.Codigo));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Eliminar la Formade Pago" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<CatImpuestos> DdlImpuestos()
        {
            var Lista = new List<CatImpuestos>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatImpuestos ";
            sql += " order by Impuesto";

            listaParams.Clear();
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatImpuestos = new CatImpuestos();
                        CatImpuestos.IdImp = Convert.ToInt32(reader["IdImp"].ToString());
                        CatImpuestos.Impuesto = reader["Impuesto"].ToString();
                        CatImpuestos.Retencion = Convert.ToBoolean(reader["Retencion"].ToString());
                        CatImpuestos.Traslada = Convert.ToBoolean(reader["Traslada"].ToString());
                        CatImpuestos.ClaveSat = reader["ClaveSat"].ToString();
                        CatImpuestos.Porcentaje = Convert.ToDecimal(reader["Porcentaje"].ToString());

                        Lista.Add(CatImpuestos);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatImpuestos = new CatImpuestos();
                CatImpuestos.IdImp = ex.HResult;
                CatImpuestos.ClaveSat = "Error";
                CatImpuestos.Impuesto = "Error al llenar los Impuestos" + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatImpuestos);
            }
            return Lista;

        }
        public RespuestaSQL ImpuestosAdd(CatImpuestos modCatImpuestos)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpImpuestosAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Impuesto", modCatImpuestos.Impuesto));
            listaParams.Add(new SqlParameter("@Retencion", modCatImpuestos.Retencion));
            listaParams.Add(new SqlParameter("@Traslada", modCatImpuestos.Traslada));
            listaParams.Add(new SqlParameter("@ClaveSat", modCatImpuestos.ClaveSat));
            listaParams.Add(new SqlParameter("@Porcentaje", modCatImpuestos.Porcentaje));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Guardar El Impuesto" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL ImpuestoMod(CatImpuestos modCatImpuestos)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpImpuestoMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdImp", modCatImpuestos.IdImp));
            listaParams.Add(new SqlParameter("@Impuesto", modCatImpuestos.Impuesto));
            listaParams.Add(new SqlParameter("@Retencion", modCatImpuestos.Retencion));
            listaParams.Add(new SqlParameter("@Traslada", modCatImpuestos.Traslada));
            listaParams.Add(new SqlParameter("@ClaveSat", modCatImpuestos.ClaveSat));
            listaParams.Add(new SqlParameter("@Porcentaje", modCatImpuestos.Porcentaje));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Modificar el Impuesto" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL ImpuestoDel(CatImpuestos modCatImpuestos)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpImpuestoDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdImp", modCatImpuestos.IdImp));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Eliminar El Impuesto" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<CatMetodoPago> DdlMetPago()
        {
            var Lista = new List<CatMetodoPago>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatMetodoPago ";
            sql += " order by MetodoPago";

            listaParams.Clear();
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatMetodoPago = new CatMetodoPago();
                        CatMetodoPago.IdMetodo = Convert.ToInt32(reader["IdMetodo"].ToString());
                        CatMetodoPago.Clave = reader["Clave"].ToString();
                        CatMetodoPago.MetodoPago = reader["MetodoPago"].ToString();

                        Lista.Add(CatMetodoPago);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatMetodoPago = new CatMetodoPago();
                CatMetodoPago.IdMetodo = ex.HResult;
                CatMetodoPago.Clave = "Error";
                CatMetodoPago.MetodoPago = "Error al llenar los Impuestos" + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatMetodoPago);
            }
            return Lista;

        }
        public RespuestaSQL MetodoPAdd(CatMetodoPago modCatMetodoPago)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpMetodoPAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Clave", modCatMetodoPago.Clave));
            listaParams.Add(new SqlParameter("@MetodoPago", modCatMetodoPago.MetodoPago));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Guardar El Metodo de Pago" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL MetodoPMod(CatMetodoPago modCatMetodoPago)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpMetodoPMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdMetodo", modCatMetodoPago.IdMetodo));
            listaParams.Add(new SqlParameter("@Clave", modCatMetodoPago.Clave));
            listaParams.Add(new SqlParameter("@MetodoPago", modCatMetodoPago.MetodoPago));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Modificar el Metodo de Pago" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL MetodoPDel(CatMetodoPago modCatMetodoPago)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpMetodoPDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdMetodo", modCatMetodoPago.IdMetodo));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Eliminar El Metodo de Pago" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<CatRegimenFiscal> DdlRegimenF()
        {
            var Lista = new List<CatRegimenFiscal>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatRegimenFiscal ";
            sql += " order by RegimenF";

            listaParams.Clear();
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatRegimenFiscal = new CatRegimenFiscal();
                        CatRegimenFiscal.IdRFiscal = Convert.ToInt32(reader["IdRFiscal"].ToString());
                        CatRegimenFiscal.Clave = reader["Clave"].ToString();
                        CatRegimenFiscal.RegimenF = reader["RegimenF"].ToString();
                        Lista.Add(CatRegimenFiscal);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatRegimenFiscal = new CatRegimenFiscal();
                CatRegimenFiscal.IdRFiscal = ex.HResult;
                CatRegimenFiscal.Clave = "Error";
                CatRegimenFiscal.RegimenF = "Error al llenar los Impuestos" + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatRegimenFiscal);
            }
            return Lista;

        }
        public RespuestaSQL RegimenFAdd(CatRegimenFiscal modCatRegimenFiscal)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpRegimenFAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Clave", modCatRegimenFiscal.Clave));
            listaParams.Add(new SqlParameter("@RegimenF", modCatRegimenFiscal.RegimenF));
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Guardar El Regimen Fiscal" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL RegimenFMod(CatRegimenFiscal modCatRegimenFiscal)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpRegimenFMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdRFiscal", modCatRegimenFiscal.IdRFiscal));
            listaParams.Add(new SqlParameter("@Clave", modCatRegimenFiscal.Clave));
            listaParams.Add(new SqlParameter("@RegimenF", modCatRegimenFiscal.RegimenF));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Modificar el Regimen Fiscal" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL RegimenFDel(CatRegimenFiscal modCatRegimenFiscal)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpRegimenFDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdRFiscal", modCatRegimenFiscal.IdRFiscal));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["ID"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Eliminar El Regimen Fiscal" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }

    }
}