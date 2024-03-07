using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static GuerreroWeb.Controllers.DBConexion;
using GuerreroWeb.Views;

namespace GuerreroWeb.Controllers
{
	public class CtrlConfig
	{
		private DBConexion conexion = new DBConexion();
		private DBComandos Cmd = new DBComandos();

		public ModConfig Configuracion(int IdEmpresa)
		{
			var Configuracion = new ModConfig();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.Configuracion ";
			sql += " where IdEmpresa = @IdEmpresa order by IdEmpresa";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdEmpresa", IdEmpresa));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						Configuracion.IdConf = Convert.ToInt32(reader["IdConf"].ToString());
						Configuracion.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"].ToString());
						Configuracion.Decimales1 = Convert.ToInt32(reader["Decimales1"].ToString());
						Configuracion.Decimales2 = Convert.ToInt32(reader["Decimales2"].ToString());
						Configuracion.VendeExt0 = Convert.ToBoolean(reader["VendeExt0"].ToString());
						Configuracion.SalidaExt0 = Convert.ToBoolean(reader["SalidaExt0"].ToString());
						Configuracion.ActivoInv = Convert.ToBoolean(reader["ActivoInv"].ToString());
						Configuracion.ActivoVenta = Convert.ToBoolean(reader["ActivoVenta"].ToString());
						Configuracion.ActivoComp = Convert.ToBoolean(reader["ActivoComp"].ToString());
						Configuracion.ActivoCXC = Convert.ToBoolean(reader["ActivoCXC"].ToString());
						Configuracion.ActivoCXP = Convert.ToBoolean(reader["ActivoCXP"].ToString());
						Configuracion.ActivoCont = Convert.ToBoolean(reader["ActivoCont"].ToString());
						Configuracion.ActivoWati = Convert.ToBoolean(reader["ActivoWati"].ToString());
						Configuracion.ActivoReporte = Convert.ToBoolean(reader["ActivoReporte"].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				Configuracion.IdConf = ex.HResult;
				//Configuracion.Descripcion = "Error al llenar las Formas de Pago" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Configuracion;

		}
		public RespuestaSQL ConfigMod(ModConfig modConfiguracion)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpConfigMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdConf", modConfiguracion.IdConf));
			listaParams.Add(new SqlParameter("@IdEmpresa", modConfiguracion.IdEmpresa));
			listaParams.Add(new SqlParameter("@Decimales1", modConfiguracion.Decimales1));
			listaParams.Add(new SqlParameter("@Decimales2", modConfiguracion.Decimales2));
			listaParams.Add(new SqlParameter("@VendeExt0", modConfiguracion.VendeExt0));
			listaParams.Add(new SqlParameter("@SalidaExt0", modConfiguracion.SalidaExt0));
			listaParams.Add(new SqlParameter("@ActivoInv", modConfiguracion.ActivoInv));
			listaParams.Add(new SqlParameter("@ActivoVenta", modConfiguracion.ActivoVenta));
			listaParams.Add(new SqlParameter("@ActivoComp", modConfiguracion.ActivoComp));
			listaParams.Add(new SqlParameter("@ActivoCXC", modConfiguracion.ActivoCXC));
			listaParams.Add(new SqlParameter("@ActivoCXP", modConfiguracion.ActivoCXP));
			listaParams.Add(new SqlParameter("@ActivoCont", modConfiguracion.ActivoCont));
			listaParams.Add(new SqlParameter("@ActivoWati", modConfiguracion.ActivoWati));
			listaParams.Add(new SqlParameter("@ActivoReporte", modConfiguracion.ActivoReporte));

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
				Respuesta.Mensaje = "Error al Modificar las Configuraciones" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtFolios> ListaFolios(string Modulo, int IdMod)
		{
			if (Modulo == string.Empty)
			{
				Modulo = "%%%";
			}
			else
			{
				Modulo = "%" + Modulo + "%";
			}
			var Lista = new List<VtFolios>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtFolios ";
			sql += " where Modulo like @Modulo and IdMod = @IdMod order by Movimiento";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMod", IdMod));
			listaParams.Add(new SqlParameter("@Modulo", Modulo));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtFolios = new VtFolios();
						VtFolios.IdFolio = Convert.ToInt32(reader["IdFolio"].ToString());
						VtFolios.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"].ToString());
						VtFolios.Empresa = reader["Empresa"].ToString();
						VtFolios.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						VtFolios.Modulo = reader["Modulo"].ToString();
						VtFolios.IdMovto = Convert.ToInt32(reader["IdMovto"].ToString());
						VtFolios.Movimiento = reader["Movimiento"].ToString();
						VtFolios.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
						VtFolios.Sucursal = reader["Sucursal"].ToString();
						VtFolios.Serie = reader["Serie"].ToString();
						VtFolios.Consecutivo = Convert.ToInt32(reader["Consecutivo"].ToString());

						Lista.Add(VtFolios);
					}
				}
			}
			catch (Exception ex)
			{
				var VtFolios = new VtFolios();
				VtFolios.IdFolio = ex.HResult;
				VtFolios.Modulo = "Error";
				VtFolios.Movimiento = "Error al llenar los Folios " + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(VtFolios);
			}
			return Lista;

		}
		public RespuestaSQL FolioAdd(CatFolios modFolios)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpFolioAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdEmpresa", modFolios.IdEmpresa));
			listaParams.Add(new SqlParameter("@IdMod", modFolios.IdMod));
			listaParams.Add(new SqlParameter("@IdMovto", modFolios.IdMovto));
			listaParams.Add(new SqlParameter("@IdSuc", modFolios.IdSuc));
			listaParams.Add(new SqlParameter("@Serie", modFolios.Serie));
			listaParams.Add(new SqlParameter("@Consecutivo", modFolios.Consecutivo));

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
				Respuesta.Mensaje = "Error al Guardar el Folio " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL FolioMod(CatFolios modFolios)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpFolioMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdFolio", modFolios.IdFolio));
			listaParams.Add(new SqlParameter("@IdEmpresa", modFolios.IdEmpresa));
			listaParams.Add(new SqlParameter("@IdMod", modFolios.IdMod));
			listaParams.Add(new SqlParameter("@IdMovto", modFolios.IdMovto));
			listaParams.Add(new SqlParameter("@IdSuc", modFolios.IdSuc));
			listaParams.Add(new SqlParameter("@Serie", modFolios.Serie));
			listaParams.Add(new SqlParameter("@Consecutivo", modFolios.Consecutivo));

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
				Respuesta.Mensaje = "Error al Modificar el Folio " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL FolioDel(int IdFolio)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpFolioDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdFolio", IdFolio));

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
				Respuesta.Mensaje = "Error al Eliminar el Folio " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public CatFolios Folio(int IdFol)
		{
			var ModFolios = new CatFolios();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.Folios ";
			sql += " where IdFol = @IdFol order by Movimientos";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdFol", IdFol));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						ModFolios.IdFolio = Convert.ToInt32(reader["IdFolio"].ToString());
						ModFolios.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"].ToString());
						ModFolios.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						ModFolios.IdMovto = Convert.ToInt32(reader["IdMovto"].ToString());
						ModFolios.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
						ModFolios.Serie = reader["Serie"].ToString();
						ModFolios.Consecutivo = Convert.ToInt32(reader["Consecutivo"].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				ModFolios.IdFolio = ex.HResult;
				//ModFolios.Serie = "Error";
				ModFolios.Serie = "Error al llenar los Folios " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return ModFolios;

		}

	}
}