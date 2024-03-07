using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static GuerreroWeb.Controllers.DBConexion;
using GuerreroWeb.Views;
using System.Reflection;

namespace GuerreroWeb.Controllers
{
	public class CtrlModulos
	{
		private DBConexion conexion = new DBConexion();
		private DBComandos Cmd = new DBComandos();
		public List<ModModulos> ListaModulo()
		{
			var Lista = new List<ModModulos>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.Modulos ";
			sql += " order by Modulo";

			listaParams.Clear();

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var ModModulos = new ModModulos();
						ModModulos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						ModModulos.Modulo = reader["Modulo"].ToString();
						ModModulos.Nombre = reader["Nombre"].ToString();
						ModModulos.Activo = Convert.ToBoolean(reader["Activo"].ToString());
						Lista.Add(ModModulos);
					}
				}
			}
			catch (Exception ex)
			{
				var ModModulos = new ModModulos();
				ModModulos.IdMod = ex.HResult;
				ModModulos.Modulo = "Error";
				ModModulos.Nombre = "Error al llenar los Modulos" + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(ModModulos);
			}
			return Lista;

		}
		public List<VtMovimientos> ListaMovimientos(string Movimiento, int IdMod)
		{
			if( Movimiento.Length == 0)
			{
				Movimiento = "%%%";
			}
			else
			{
				Movimiento = "%" + Movimiento + "%";
			}
			var Lista = new List<VtMovimientos>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtMovimientos ";
			sql += " where IdMod = @IdMod and Movimiento like @Movimiento order by Movimiento";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMod", IdMod));
			listaParams.Add(new SqlParameter("@Movimiento", Movimiento));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtMovimientos = new VtMovimientos();
						VtMovimientos.IdMovto = Convert.ToInt32(reader["IdMovto"].ToString());
						VtMovimientos.Movimiento = reader["Movimiento"].ToString();
						VtMovimientos.Activo = Convert.ToBoolean(reader["Activo"].ToString());
						VtMovimientos.AfectaInv = Convert.ToBoolean(reader["AfectaInv"].ToString());
						VtMovimientos.AfectaCont = Convert.ToBoolean(reader["AfectaCont"].ToString());
						VtMovimientos.AfectaCXC = Convert.ToBoolean(reader["AfectaCXC"].ToString());
						VtMovimientos.AfectaCXP = Convert.ToBoolean(reader["AfectaCXP"].ToString());
						VtMovimientos.Tipo = reader["Tipo"].ToString();
						VtMovimientos.TipoDesc = reader["TipoDesc"].ToString();
						VtMovimientos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						VtMovimientos.Modulo = reader["Modulo"].ToString();
						VtMovimientos.ModuloNom = reader["ModuloNom"].ToString(); 
						Lista.Add(VtMovimientos);
					}
				}
			}
			catch (Exception ex)
			{
				var VtMovimientos = new VtMovimientos();
				VtMovimientos.IdMovto = ex.HResult;
				VtMovimientos.Tipo = "Error";
				VtMovimientos.Movimiento = "Error al llenar los Documentos" + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(VtMovimientos);
			}
			return Lista;

		}
		public ModMovimientos Movimiento(int IdMovto)
		{
			var Movimientos = new ModMovimientos();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtMovimientos ";
			sql += " where IdMovto = @IdMovto order by Modulo";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMovto", IdMovto));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						Movimientos.IdMovto = Convert.ToInt32(reader["IdMovto"].ToString());
						Movimientos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						Movimientos.Movimiento = reader["Movimiento"].ToString();
						Movimientos.Tipo = reader["Tipo"].ToString();
						Movimientos.Activo = Convert.ToBoolean(reader["Activo"].ToString());
						Movimientos.AfectaInv = Convert.ToBoolean(reader["AfectaInv"].ToString());
						Movimientos.AfectaCont = Convert.ToBoolean(reader["AfectaCont"].ToString());
						Movimientos.AfectaCXP = Convert.ToBoolean(reader["AfectaCXP"].ToString());
						Movimientos.AfectaCXC = Convert.ToBoolean(reader["AfectaCXC"].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				Movimientos.IdMovto = ex.HResult;
				//Movimientos.Modulo = "Error";
				Movimientos.Movimiento = "Error al llenar los Documentos" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Movimientos;

		}
		public RespuestaSQL MovimientoAdd(ModMovimientos modMovimientos)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpMovtoAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMod", modMovimientos.IdMod));
			listaParams.Add(new SqlParameter("@Movimiento", modMovimientos.Movimiento));
			listaParams.Add(new SqlParameter("@Tipo", modMovimientos.Tipo));
			listaParams.Add(new SqlParameter("@Activo", modMovimientos.Activo));
			listaParams.Add(new SqlParameter("@AfectaInv", modMovimientos.AfectaInv));
			listaParams.Add(new SqlParameter("@AfectaCont", modMovimientos.AfectaCont));
			listaParams.Add(new SqlParameter("@AfectaCXP", modMovimientos.AfectaCXP));
			listaParams.Add(new SqlParameter("@AfectaCXC", modMovimientos.AfectaCXC));

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
				Respuesta.Mensaje = "Error al Guardar el Movimiento" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL MovimientoMod(ModMovimientos modMovimientos)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpMovtoMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMovto", modMovimientos.IdMovto)); 
			listaParams.Add(new SqlParameter("@IdMod", modMovimientos.IdMod));
			listaParams.Add(new SqlParameter("@Movimiento", modMovimientos.Movimiento));
			listaParams.Add(new SqlParameter("@Tipo", modMovimientos.Tipo));
			listaParams.Add(new SqlParameter("@Activo", modMovimientos.Activo));
			listaParams.Add(new SqlParameter("@AfectaInv", modMovimientos.AfectaInv));
			listaParams.Add(new SqlParameter("@AfectaCont", modMovimientos.AfectaCont));
			listaParams.Add(new SqlParameter("@AfectaCXP", modMovimientos.AfectaCXP));
			listaParams.Add(new SqlParameter("@AfectaCXC", modMovimientos.AfectaCXC));

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
				Respuesta.Mensaje = "Error al Modificar el Movimiento" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL MovimientoDel(int IdMovto)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpMovtoDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMovto", IdMovto));

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
				Respuesta.Mensaje = "Error al Eliminar el Movimiento" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<ModMovimientos> DllMovtos(int IdModulo)
		{
			var lista = new List<ModMovimientos>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.Movimientos ";
			sql += " where IdMod = @IdMod order by Movimiento";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMod", IdModulo));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var Movimientos = new ModMovimientos();
						Movimientos.IdMovto = Convert.ToInt32(reader["IdMovto"].ToString());
						Movimientos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						Movimientos.Movimiento = reader["Movimiento"].ToString();
						Movimientos.Tipo = reader["Tipo"].ToString();
						Movimientos.Activo = Convert.ToBoolean(reader["Activo"].ToString());
						Movimientos.AfectaInv = Convert.ToBoolean(reader["AfectaInv"].ToString());
						Movimientos.AfectaCont = Convert.ToBoolean(reader["AfectaCont"].ToString());
						Movimientos.AfectaCXP = Convert.ToBoolean(reader["AfectaCXP"].ToString());
						Movimientos.AfectaCXC = Convert.ToBoolean(reader["AfectaCXC"].ToString());
						lista.Add(Movimientos);
					}
				}
			}
			catch (Exception ex)
			{
				var Movimientos = new ModMovimientos();
				Movimientos.IdMovto = ex.HResult;
				Movimientos.Tipo = "Error";
				Movimientos.Movimiento = "Error al llenar los Documentos" + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(Movimientos);
			}
			return lista;

		}
		public List<ModModulos> DllModulos()
		{
			var Lista = new List<ModModulos>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.Modulos ";
			sql += " order by Modulo";

			listaParams.Clear();

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var Modulos = new ModModulos();
						Modulos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						Modulos.Modulo = reader["Modulo"].ToString();
						Modulos.Nombre = reader["Nombre"].ToString();
						Modulos.Activo = Convert.ToBoolean(reader["Activo"].ToString());

						Lista.Add(Modulos);
					}
				}
			}
			catch (Exception ex)
			{
				var Modulos = new ModModulos();
				Modulos.IdMod = ex.HResult;
				Modulos.Modulo = "Error";
				Modulos.Nombre = "Error al llenar los Modulos" + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(Modulos);
			}
			return Lista;

		}

	}
}