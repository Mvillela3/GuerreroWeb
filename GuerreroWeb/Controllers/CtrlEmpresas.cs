using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static GuerreroWeb.Controllers.DBConexion;
using System.Drawing;
using AjaxControlToolkit;
using GuerreroWeb.Views;

namespace GuerreroWeb.Controllers
{
    public class CtrlEmpresas
    {
        //private ModConexion mconexion;
        private DBConexion conexion = new DBConexion();
        private DBComandos Cmd = new DBComandos();
        public List<VtEmpresas> ListaEmpresas()
        {
            var Lista = new List<VtEmpresas>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtEmpresas ";
            sql += " order by IdEmpresa";

            listaParams.Clear();
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var VtEmpresa = new VtEmpresas();
                        VtEmpresa.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"].ToString());
                        VtEmpresa.Empresa = reader["Empresa"].ToString();
                        VtEmpresa.NombreCom = reader["NombreCom"].ToString();
                        VtEmpresa.RFC = reader["RFC"].ToString();
                        VtEmpresa.Direccion = reader["Direccion"].ToString();
                        VtEmpresa.NoExt = reader["NoExt"].ToString();
                        VtEmpresa.NoInt = reader["NoInt"].ToString();
                        VtEmpresa.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        VtEmpresa.CP = reader["CP"].ToString();
                        VtEmpresa.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtEmpresa.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtEmpresa.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtEmpresa.IdRFiscal = Convert.ToInt32(reader["IdRFiscal"].ToString());
                        VtEmpresa.PaginaWeb = reader["PaginaWeb"].ToString();
                        VtEmpresa.Email = reader["Email"].ToString();
                        VtEmpresa.Telefono1 = reader["Telefono1"].ToString();
                        VtEmpresa.Telefono2 = reader["Telefono2"].ToString();
                        VtEmpresa.DireccionC = reader["DireccionC"].ToString();
                        VtEmpresa.Colonia = reader["Colonia"].ToString();
                        VtEmpresa.Ciudad = reader["Ciudad"].ToString();
                        VtEmpresa.Estado = reader["Estado"].ToString();
                        VtEmpresa.Pais = reader["Pais"].ToString();
                        VtEmpresa.RegimenF = reader["RegimenF"].ToString();
                        VtEmpresa.ClvRFiscal = reader["ClvRFiscal"].ToString();
                        Lista.Add(VtEmpresa);
                    }
                }
            }
            catch (Exception ex)
            {
                var VtEmpresa = new VtEmpresas();
                VtEmpresa.IdEmpresa = ex.HResult;
                VtEmpresa.Empresa = "Error";
                VtEmpresa.NombreCom = "Error al llenar la Empresa " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(VtEmpresa);
            }
            return Lista;

        }
        public VtEmpresas Empresas(int Id)
        {
            var VtEmpresa = new VtEmpresas();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtEmpresas ";
            sql += " where IdEmpresa = @IdEmpresa order by IdEmpresa";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdEmpresa", Id));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VtEmpresa.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"].ToString());
                        VtEmpresa.Empresa = reader["Empresa"].ToString();
                        VtEmpresa.NombreCom = reader["NombreCom"].ToString();
                        VtEmpresa.RFC = reader["RFC"].ToString();
                        VtEmpresa.Direccion = reader["Direccion"].ToString();
                        VtEmpresa.NoExt = reader["NoExt"].ToString();
                        VtEmpresa.NoInt = reader["NoInt"].ToString();
                        VtEmpresa.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        VtEmpresa.CP = reader["CP"].ToString();
                        VtEmpresa.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtEmpresa.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtEmpresa.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtEmpresa.IdRFiscal = Convert.ToInt32(reader["IdRFiscal"].ToString());
                        VtEmpresa.PaginaWeb = reader["PaginaWeb"].ToString();
                        VtEmpresa.Email = reader["Email"].ToString();
                        VtEmpresa.Telefono1 = reader["Telefono1"].ToString();
                        VtEmpresa.Telefono2 = reader["Telefono2"].ToString();
                        VtEmpresa.DireccionC = reader["DireccionC"].ToString();
                        VtEmpresa.Colonia = reader["Colonia"].ToString();
                        VtEmpresa.Ciudad = reader["Ciudad"].ToString();
                        VtEmpresa.Estado = reader["Estado"].ToString();
                        VtEmpresa.Pais = reader["Pais"].ToString();
                        VtEmpresa.RegimenF = reader["RegimenF"].ToString();
                        VtEmpresa.ClvRFiscal = reader["ClvRFiscal"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                VtEmpresa.IdEmpresa = ex.HResult;
                VtEmpresa.Empresa = "Error";
                VtEmpresa.NombreCom = "Error al llenar La Empresa " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return VtEmpresa;

        }
        public RespuestaSQL EmpresaMod(ModEmpresas Empresa)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpEmpresaMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdEmpresa", Empresa.IdEmpresa));
            listaParams.Add(new SqlParameter("@Direccion", Empresa.Direccion));
            listaParams.Add(new SqlParameter("@NoExt", Empresa.NoExt));
            listaParams.Add(new SqlParameter("@NoInt", Empresa.NoInt));
            listaParams.Add(new SqlParameter("@IdCol", Empresa.IdCol));
            listaParams.Add(new SqlParameter("@CP", Empresa.CP));
            listaParams.Add(new SqlParameter("@IdCiu", Empresa.IdCiu));
            listaParams.Add(new SqlParameter("@IdEst", Empresa.IdEst));
            listaParams.Add(new SqlParameter("@IdPais", Empresa.IdPais));
            listaParams.Add(new SqlParameter("@IdRFiscal", Empresa.IdRFiscal));
            listaParams.Add(new SqlParameter("@PaginaWeb", Empresa.PaginaWeb));
            listaParams.Add(new SqlParameter("@Email", Empresa.Email));
            listaParams.Add(new SqlParameter("@Telefono1", Empresa.Telefono1));
            listaParams.Add(new SqlParameter("@Telefono2", Empresa.Telefono2));
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
                {
                    reader.Read();
                    if (reader.HasRows)
                    {
                        Respuesta.Codigo = Convert.ToInt32(reader["Codigo"].ToString());
                        Respuesta.Mensaje = reader["Mensaje"].ToString();
                        Respuesta.ID = Convert.ToInt32(reader["Codigo"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = ex.HResult;
                Respuesta.ID = 0;
                Respuesta.Mensaje = "Error al Modificar la Empresa " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public VtSucursales Sucursales(int Id)
        {
            var VtSucursales = new VtSucursales();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtSucursales ";
            sql += " where IdSuc = @IdSuc order by IdSuc";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdSuc", Id));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VtSucursales.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
                        VtSucursales.Sucursal = reader["Sucursal"].ToString();
                        VtSucursales.Nombre = reader["Nombre"].ToString();
                        VtSucursales.Direccion = reader["Direccion"].ToString();
                        VtSucursales.NoExt = reader["NoExt"].ToString();
                        VtSucursales.NoInt = reader["NoInt"].ToString();
                        VtSucursales.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        VtSucursales.CP = reader["CP"].ToString();
                        VtSucursales.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtSucursales.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtSucursales.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtSucursales.Telefono1 = reader["Telefono1"].ToString();
                        VtSucursales.Telefono2 = reader["Telefono2"].ToString();
                        VtSucursales.DireccionC = reader["DireccionC"].ToString();
                        VtSucursales.Colonia = reader["Colonia"].ToString();
                        VtSucursales.Ciudad = reader["Ciudad"].ToString();
                        VtSucursales.Estado = reader["Estado"].ToString();
                        VtSucursales.Pais = reader["Pais"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                VtSucursales.IdSuc = ex.HResult;
                VtSucursales.Sucursal = "Error";
                VtSucursales.Nombre = "Error al llenar La Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return VtSucursales;

        }

        public List<VtSucursales> ListaSucursales(string Sucursal)
        {
            if (Sucursal == string.Empty)
            {
                Sucursal = "%%%";
            }
            else
            {
                Sucursal = "%" + Sucursal + "%";
            }
            var Lista = new List<VtSucursales>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtSucursales ";
            sql += " where Sucursal like @Sucursal or Nombre like @Sucursal order by Nombre";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Sucursal", Sucursal));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var VtSucursales = new VtSucursales();
                        VtSucursales.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
                        VtSucursales.Sucursal = reader["Sucursal"].ToString();
                        VtSucursales.Nombre = reader["Nombre"].ToString();
                        VtSucursales.Direccion = reader["Direccion"].ToString();
                        VtSucursales.NoExt = reader["NoExt"].ToString();
                        VtSucursales.NoInt = reader["NoInt"].ToString();
                        VtSucursales.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        VtSucursales.CP = reader["CP"].ToString();
                        VtSucursales.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtSucursales.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtSucursales.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtSucursales.Telefono1 = reader["Telefono1"].ToString();
                        VtSucursales.Telefono2 = reader["Telefono2"].ToString();
                        VtSucursales.DireccionC = reader["DireccionC"].ToString();
                        VtSucursales.Colonia = reader["Colonia"].ToString();
                        VtSucursales.Ciudad = reader["Ciudad"].ToString();
                        VtSucursales.Estado = reader["Estado"].ToString();
                        VtSucursales.Pais = reader["Pais"].ToString();
                        Lista.Add(VtSucursales);
                    }
                }
            }
            catch (Exception ex)
            {
                var VtSucursales = new VtSucursales();
                VtSucursales.IdSuc = ex.HResult;
                VtSucursales.Sucursal = "Error";
                VtSucursales.Nombre = "Error al llenar la Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(VtSucursales);
            }
            return Lista;

        }

        public List<CatSucursales> DdlSucursales()
        {
            var Lista = new List<CatSucursales>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatSucursales ";
            sql += " order by Nombre";

            listaParams.Clear();

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatSucursales = new CatSucursales();
                        CatSucursales.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
                        CatSucursales.Sucursal = reader["Sucursal"].ToString();
                        CatSucursales.Nombre = reader["Nombre"].ToString();
                        CatSucursales.Direccion = reader["Direccion"].ToString();
                        CatSucursales.NoExt = reader["NoExt"].ToString();
                        CatSucursales.NoInt = reader["NoInt"].ToString();
                        CatSucursales.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        CatSucursales.IdCiu = Convert.ToInt32(reader["idCiu"].ToString());
                        CatSucursales.IdEst = Convert.ToInt32(reader["idEst"].ToString());
                        CatSucursales.IdPais = Convert.ToInt32(reader["idPais"].ToString());
                        CatSucursales.CP = reader["CP"].ToString();
                        Lista.Add(CatSucursales);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatSucursales = new CatSucursales();
                CatSucursales.IdSuc = ex.HResult;
                CatSucursales.Sucursal = "Error";
                CatSucursales.Nombre = "Error al llenar las Sucursales " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatSucursales);
            }
            return Lista;

        }

        public RespuestaSQL SucursalAdd(CatSucursales modCatSucursales)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpSucursalAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Sucursal", modCatSucursales.Sucursal));
            listaParams.Add(new SqlParameter("@Nombre", modCatSucursales.Nombre));
            listaParams.Add(new SqlParameter("@Direccion", modCatSucursales.Direccion));
            listaParams.Add(new SqlParameter("@NoExt", modCatSucursales.NoExt));
            listaParams.Add(new SqlParameter("@NoInt", modCatSucursales.NoInt));
            listaParams.Add(new SqlParameter("@IdCol", modCatSucursales.IdCol));
            listaParams.Add(new SqlParameter("@idCiu", modCatSucursales.IdCiu));
            listaParams.Add(new SqlParameter("@idEst", modCatSucursales.IdEst));
            listaParams.Add(new SqlParameter("@idPais", modCatSucursales.IdPais));
            listaParams.Add(new SqlParameter("@CP", modCatSucursales.CP));
            listaParams.Add(new SqlParameter("@Telefono1", modCatSucursales.Telefono1));
            listaParams.Add(new SqlParameter("@Telefono2", modCatSucursales.Telefono2));

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
                Respuesta.Mensaje = "Error al Guardar La Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL SucursalMod(CatSucursales modCatSucursales)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpSucursalMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdSuc", modCatSucursales.IdSuc));
            listaParams.Add(new SqlParameter("@Sucursal", modCatSucursales.Sucursal));
            listaParams.Add(new SqlParameter("@Nombre", modCatSucursales.Nombre));
            listaParams.Add(new SqlParameter("@Direccion", modCatSucursales.Direccion));
            listaParams.Add(new SqlParameter("@NoExt", modCatSucursales.NoExt));
            listaParams.Add(new SqlParameter("@NoInt", modCatSucursales.NoInt));
            listaParams.Add(new SqlParameter("@IdCol", modCatSucursales.IdCol));
            listaParams.Add(new SqlParameter("@idCiu", modCatSucursales.IdCiu));
            listaParams.Add(new SqlParameter("@idEst", modCatSucursales.IdEst));
            listaParams.Add(new SqlParameter("@idPais", modCatSucursales.IdPais));
            listaParams.Add(new SqlParameter("@CP", modCatSucursales.CP));
            listaParams.Add(new SqlParameter("@Telefono1", modCatSucursales.Telefono1));
            listaParams.Add(new SqlParameter("@Telefono2", modCatSucursales.Telefono2));

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
                Respuesta.Mensaje = "Error al Modificar la Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL SucursalDel(CatSucursales modCatSucursales)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpSucursalDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdSuc", modCatSucursales.IdSuc));

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
                Respuesta.Mensaje = "Error al Eliminar La Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<CatAlmacenes> DdlAlmacenes()
        {
            var Lista = new List<CatAlmacenes>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatAlmacenes ";
            sql += " order by Nombre";

            listaParams.Clear();

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatAlmacenes = new CatAlmacenes();
                        CatAlmacenes.IdAlm = Convert.ToInt32(reader["IdAlm"].ToString());
                        CatAlmacenes.Almacen = reader["Almacen"].ToString();
                        CatAlmacenes.Nombre = reader["Nombre"].ToString();
                        CatAlmacenes.Direccion = reader["Direccion"].ToString();
                        CatAlmacenes.NoExt = reader["NoExt"].ToString();
                        CatAlmacenes.NoInt = reader["NoInt"].ToString();
                        CatAlmacenes.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        CatAlmacenes.IdCiu = Convert.ToInt32(reader["idCiu"].ToString());
                        CatAlmacenes.IdEst = Convert.ToInt32(reader["idEst"].ToString());
                        CatAlmacenes.IdPais = Convert.ToInt32(reader["idPais"].ToString());
                        CatAlmacenes.CP = reader["CP"].ToString();
                        CatAlmacenes.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
                        CatAlmacenes.IdEncargado = Convert.ToInt32(reader["IdEncargado"].ToString());
                        Lista.Add(CatAlmacenes);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatAlmacenes = new CatAlmacenes();
                CatAlmacenes.IdAlm = ex.HResult;
                CatAlmacenes.Almacen = "Error";
                CatAlmacenes.Nombre = "Error al llenar los Almacenes " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatAlmacenes);
            }
            return Lista;

        }
        public RespuestaSQL AlmacenAdd(CatAlmacenes modCatAlmacenes)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpAlmacenAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Almacen", modCatAlmacenes.Almacen));
            listaParams.Add(new SqlParameter("@Nombre", modCatAlmacenes.Nombre));
            listaParams.Add(new SqlParameter("@Direccion", modCatAlmacenes.Direccion));
            listaParams.Add(new SqlParameter("@NoExt", modCatAlmacenes.NoExt));
            listaParams.Add(new SqlParameter("@NoInt", modCatAlmacenes.NoInt));
            listaParams.Add(new SqlParameter("@IdCol", modCatAlmacenes.IdCol));
            listaParams.Add(new SqlParameter("@idCiu", modCatAlmacenes.IdCiu));
            listaParams.Add(new SqlParameter("@idEst", modCatAlmacenes.IdEst));
            listaParams.Add(new SqlParameter("@idPais", modCatAlmacenes.IdPais));
            listaParams.Add(new SqlParameter("@CP", modCatAlmacenes.CP));
            listaParams.Add(new SqlParameter("@IdSuc", modCatAlmacenes.IdSuc));
            listaParams.Add(new SqlParameter("@IdEncargado", modCatAlmacenes.IdEncargado));

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
                Respuesta.Mensaje = "Error al Guardar el Almacen " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL AlmacenMod(CatAlmacenes modCatAlmacenes)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpAlmacenMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdAlm", modCatAlmacenes.IdAlm));
            listaParams.Add(new SqlParameter("@Almacen", modCatAlmacenes.Almacen));
            listaParams.Add(new SqlParameter("@Nombre", modCatAlmacenes.Nombre));
            listaParams.Add(new SqlParameter("@Direccion", modCatAlmacenes.Direccion));
            listaParams.Add(new SqlParameter("@NoExt", modCatAlmacenes.NoExt));
            listaParams.Add(new SqlParameter("@NoInt", modCatAlmacenes.NoInt));
            listaParams.Add(new SqlParameter("@IdCol", modCatAlmacenes.IdCol));
            listaParams.Add(new SqlParameter("@idCiu", modCatAlmacenes.IdCiu));
            listaParams.Add(new SqlParameter("@idEst", modCatAlmacenes.IdEst));
            listaParams.Add(new SqlParameter("@idPais", modCatAlmacenes.IdPais));
            listaParams.Add(new SqlParameter("@CP", modCatAlmacenes.CP));
            listaParams.Add(new SqlParameter("@IdSuc", modCatAlmacenes.IdSuc));
            listaParams.Add(new SqlParameter("@IdEncargado", modCatAlmacenes.IdEncargado));

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
                Respuesta.Mensaje = "Error al Modificar el Almacen " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL AlmacenDel(CatAlmacenes modCatAlmacenes)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpAlmacenDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdAlm", modCatAlmacenes.IdAlm));

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
                Respuesta.Mensaje = "Error al Eliminar el Almacen " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<CatDepartamentos> DdlDepartamentos()
        {
            var Lista = new List<CatDepartamentos>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT IdDepto, Departamento ";
            sql += " FROM dbo.CatDepartamentos ";
            sql += " union Select 0 as IdDepto, ' Seleccione una Opcion' as Departamento";
            sql += " order by Departamento";

            listaParams.Clear();

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatDepartamentos = new CatDepartamentos();
                        CatDepartamentos.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
                        CatDepartamentos.Departamento = reader["Departamento"].ToString();
                        Lista.Add(CatDepartamentos);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatDepartamentos = new CatDepartamentos();
                CatDepartamentos.IdDepto = ex.HResult;
                //CatDepartamentos.Almacen = "Error";
                CatDepartamentos.Departamento = "Error al llenar los Departamentos " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatDepartamentos);
            }
            return Lista;

        }
        public List<CatDepartamentos> ListaDepartamentos(string Depto)
        {
            if (Depto == string.Empty)
            {
                Depto = "%%%";
            }
            else
            {
                Depto = "%" + Depto.Trim() + "%";
            }
            var Lista = new List<CatDepartamentos>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatDepartamentos ";
            sql += " where Departamento like @Depto order by Departamento";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Depto", Depto));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatDepartamentos = new CatDepartamentos();
                        CatDepartamentos.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
                        CatDepartamentos.Departamento = reader["Departamento"].ToString();
                        Lista.Add(CatDepartamentos);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatDepartamentos = new CatDepartamentos();
                CatDepartamentos.IdDepto = ex.HResult;
                //CatDepartamentos.Almacen = "Error";
                CatDepartamentos.Departamento = "Error al llenar los Departamentos " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatDepartamentos);
            }
            return Lista;

        }
        public CatDepartamentos Departamento(int Depto)
        {
            var CatDepartamentos = new CatDepartamentos();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatDepartamentos ";
            sql += " where IdDepto like @Depto order by Departamento";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Depto", Depto));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CatDepartamentos.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
                        CatDepartamentos.Departamento = reader["Departamento"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CatDepartamentos.IdDepto = ex.HResult;
                //CatDepartamentos.Almacen = "Error";
                CatDepartamentos.Departamento = "Error al llenar el Departamento " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return CatDepartamentos;

        }
        public RespuestaSQL DepartamentoAdd(CatDepartamentos modCatDepartamentos)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpDepartamentoAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Departamento", modCatDepartamentos.Departamento));

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
                Respuesta.Mensaje = "Error al Guardar el Departamento " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL DepartamentoMod(CatDepartamentos modCatDepartamentos)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpDepartamentoMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdDepto", modCatDepartamentos.IdDepto));
            listaParams.Add(new SqlParameter("@Departamento", modCatDepartamentos.Departamento));

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
                Respuesta.Mensaje = "Error al Modificar el Departamento " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL DepartamentoDel(CatDepartamentos modCatDepartamentos)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpDepartamentoDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdDepto", modCatDepartamentos.IdDepto));

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
                Respuesta.Mensaje = "Error al Eliminar el Departamento " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<VtAlmacenes> ListaAlmacenes(string Almacen)
        {
            if (Almacen == string.Empty)
            {
                Almacen = "%%%";
            }
            else
            {
                Almacen = "%" + Almacen + "%";
            }
            var Lista = new List<VtAlmacenes>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtAlmacenes ";
            sql += " where Almacen like @Almacen or Nombre like @Almacen order by Nombre";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Almacen", Almacen));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var VtAlmacenes = new VtAlmacenes();
                        VtAlmacenes.IdAlm = Convert.ToInt32(reader["IdAlm"].ToString());
                        VtAlmacenes.Almacen = reader["Almacen"].ToString();
                        VtAlmacenes.Nombre = reader["Nombre"].ToString();
                        VtAlmacenes.Direccion = reader["Direccion"].ToString();
                        VtAlmacenes.NoExt = reader["NoExt"].ToString();
                        VtAlmacenes.NoInt = reader["NoInt"].ToString();
                        VtAlmacenes.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        VtAlmacenes.CP = reader["CP"].ToString();
                        VtAlmacenes.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtAlmacenes.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtAlmacenes.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtAlmacenes.DireccionC = reader["DireccionC"].ToString();
                        VtAlmacenes.Colonia = reader["Colonia"].ToString();
                        VtAlmacenes.Ciudad = reader["Ciudad"].ToString();
                        VtAlmacenes.Estado = reader["Estado"].ToString();
                        VtAlmacenes.Pais = reader["Pais"].ToString();
                        VtAlmacenes.Sucursal = reader["Sucursal"].ToString();
                        VtAlmacenes.SucursalNom = reader["SucursalNom"].ToString();
                        VtAlmacenes.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
                        VtAlmacenes.IdEncargado = Convert.ToInt32(reader["IdEncargado"].ToString());
                        VtAlmacenes.Encargado = reader["Encargado"].ToString();
                        Lista.Add(VtAlmacenes);
                    }
                }
            }
            catch (Exception ex)
            {
                var VtAlmacenes = new VtAlmacenes();
                VtAlmacenes.IdAlm = ex.HResult;
                VtAlmacenes.Almacen = "Error";
                VtAlmacenes.Nombre = "Error al llenar la Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(VtAlmacenes);
            }
            return Lista;


        }
        public VtAlmacenes Almacenes(int Id)
        {
            var VtAlmacenes = new VtAlmacenes();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtAlmacenes ";
            sql += " where IdAlm = @IdAlm order by IdAlm";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdAlm", Id));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VtAlmacenes.IdAlm = Convert.ToInt32(reader["IdAlm"].ToString());
                        VtAlmacenes.Almacen = reader["Almacen"].ToString();
                        VtAlmacenes.Nombre = reader["Nombre"].ToString();
                        VtAlmacenes.Direccion = reader["Direccion"].ToString();
                        VtAlmacenes.NoExt = reader["NoExt"].ToString();
                        VtAlmacenes.NoInt = reader["NoInt"].ToString();
                        VtAlmacenes.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        VtAlmacenes.CP = reader["CP"].ToString();
                        VtAlmacenes.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtAlmacenes.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtAlmacenes.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtAlmacenes.DireccionC = reader["DireccionC"].ToString();
                        VtAlmacenes.Colonia = reader["Colonia"].ToString();
                        VtAlmacenes.Ciudad = reader["Ciudad"].ToString();
                        VtAlmacenes.Estado = reader["Estado"].ToString();
                        VtAlmacenes.Pais = reader["Pais"].ToString();
                        VtAlmacenes.Sucursal = reader["Sucursal"].ToString();
                        VtAlmacenes.SucursalNom = reader["SucursalNom"].ToString();
                        VtAlmacenes.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
                        VtAlmacenes.IdEncargado = Convert.ToInt32(reader["IdEncargado"].ToString());
                        VtAlmacenes.Encargado = reader["Encargado"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                VtAlmacenes.IdAlm = ex.HResult;
                VtAlmacenes.Almacen = "Error";
                VtAlmacenes.Nombre = "Error al llenar el Almacen " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return VtAlmacenes;

        }
		public List<VtCajas> ListaCajas(string Caja, int IdSuc)
		{
			if (Caja == string.Empty)
			{
				Caja = "%%%";
			}
			else
			{
				Caja = "%" + Caja + "%";
			}
			var Lista = new List<VtCajas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtCajas ";
			sql += " where Caja like @Caja and IdSuc like @IdSuc order by Caja";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Caja", Caja));
			listaParams.Add(new SqlParameter("@IdSuc", IdSuc));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtCajas = new VtCajas();
						VtCajas.IdCaja = Convert.ToInt32(reader["IdCaja"].ToString());
						VtCajas.Caja = reader["Caja"].ToString();
						VtCajas.NoCaja = reader["NoCaja"].ToString();
						VtCajas.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
						VtCajas.Sucursal = reader["Sucursal"].ToString();
						VtCajas.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
						VtCajas.Departamento = reader["Departamento"].ToString();
						VtCajas.Consecutivo = Convert.ToInt32(reader["Consecutivo"].ToString());
						VtCajas.Activo = Convert.ToBoolean(reader["Activo"].ToString()); 
                        
                        Lista.Add(VtCajas);
					}
				}
			}
			catch (Exception ex)
			{
				var VtCajas = new VtCajas();
				VtCajas.IdCaja = ex.HResult;
				VtCajas.Caja = "Error";
				VtCajas.Sucursal = "Error al llenar la Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(VtCajas);
			}
			return Lista;


		}
		public List<VtCajas> DdlCajas(int IdSuc)
		{
			var Lista = new List<VtCajas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtCajas ";
			sql += " where IdSuc like @IdSuc order by Caja";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdSuc", IdSuc));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtCajas = new VtCajas();
						VtCajas.IdCaja = Convert.ToInt32(reader["IdCaja"].ToString());
						VtCajas.Caja = reader["Caja"].ToString();
						VtCajas.NoCaja = reader["NoCaja"].ToString();
						VtCajas.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
						VtCajas.Sucursal = reader["Sucursal"].ToString();
						VtCajas.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
						VtCajas.Departamento = reader["Departamento"].ToString();
						VtCajas.Consecutivo = Convert.ToInt32(reader["Consecutivo"].ToString());
						VtCajas.Activo = Convert.ToBoolean(reader["Activo"].ToString());

						Lista.Add(VtCajas);
					}
				}
			}
			catch (Exception ex)
			{
				var VtCajas = new VtCajas();
				VtCajas.IdCaja = ex.HResult;
				VtCajas.Caja = "Error";
				VtCajas.Sucursal = "Error al llenar la Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(VtCajas);
			}
			return Lista;


		}
		public RespuestaSQL CajaAdd(CatCajas modCatCajas)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpCajaAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Caja", modCatCajas.Caja));
			listaParams.Add(new SqlParameter("@NoCaja", modCatCajas.NoCaja));
			listaParams.Add(new SqlParameter("@IdSuc", modCatCajas.IdSuc));
			listaParams.Add(new SqlParameter("@IdDepto", modCatCajas.IdDepto));
			listaParams.Add(new SqlParameter("@Consecutivo", modCatCajas.Consecutivo));
			listaParams.Add(new SqlParameter("@Activo", modCatCajas.Activo));
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
				Respuesta.Mensaje = "Error al Guardar la Caja " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL CajaMod(CatCajas modCatCajas)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpCajaMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCaja", modCatCajas.IdCaja));
			listaParams.Add(new SqlParameter("@Caja", modCatCajas.Caja));
			listaParams.Add(new SqlParameter("@NoCaja", modCatCajas.NoCaja));
			listaParams.Add(new SqlParameter("@IdSuc", modCatCajas.IdSuc));
			listaParams.Add(new SqlParameter("@IdDepto", modCatCajas.IdDepto));
			listaParams.Add(new SqlParameter("@Consecutivo", modCatCajas.Consecutivo));
			listaParams.Add(new SqlParameter("@Activo", modCatCajas.Activo));

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
				Respuesta.Mensaje = "Error al Modificar la Caja " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL CajaDel(int IdCaja)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpCajaDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCaja", IdCaja));

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
				Respuesta.Mensaje = "Error al Eliminar la Caja " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public CatCajas Caja(int IdCaja)
		{
			var CatCajas = new CatCajas();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatCajas ";
			sql += " where IdCaja like @IdCaja order by Caja";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCaja", IdCaja));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						CatCajas.IdCaja = Convert.ToInt32(reader["IdCaja"].ToString());
						CatCajas.Caja = reader["Caja"].ToString();
						CatCajas.NoCaja = reader["NoCaja"].ToString();
						CatCajas.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
						CatCajas.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
						CatCajas.Consecutivo = Convert.ToInt32(reader["Consecutivo"].ToString());
						CatCajas.Activo = Convert.ToBoolean(reader["Activo"].ToString());

					}
				}
			}
			catch (Exception ex)
			{
				CatCajas.IdCaja = ex.HResult;
				//CatCajas.Caja = "Error";
				CatCajas.Caja = "Error al llenar la Sucursal " + ex.Message.ToString().Replace("'", "-") + ".";

			}
			return CatCajas;


		}
		public List<ModEmpleadoCumple> EmpleadosCumple(string Sucursal)
		{


			string sql = "Select NombreC, Sucursal, day(convert(datetime, FeNac, 105)) as Dia ";
			sql += " from VtEmpleados  ";
			sql += " where Sucursal = @Sucursal and month(convert(datetime, FeNac, 105)) = month(cast(getdate() as date)) ";
            sql += " Order by Dia";

			List<ModEmpleadoCumple> lista = new List<ModEmpleadoCumple>();
			var listaParams = new List<SqlParameter>();
			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Sucursal", Sucursal));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var lista1 = new ModEmpleadoCumple();
						lista1.NombreC = reader["NombreC"].ToString();
						lista1.Sucursal = reader["Sucursal"].ToString();
						lista1.Dia = reader["Dia"].ToString();

						lista.Add(lista1);
					}

				}


			}
			catch (Exception ex)
			{

				var lista1 = new ModEmpleadoCumple();
				lista1.Dia = ex.HResult.ToString();
				lista1.Sucursal = "Error";
				lista1.NombreC = "Error al Consultar las Configuraciones " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(lista1);
			}
			return lista;

		}

	}
}