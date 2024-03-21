using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static GuerreroWeb.Controllers.DBConexion;
using GuerreroWeb.Views.Inventarios;

namespace GuerreroWeb.Controllers
{

	public class CtrlInvCatalogos
	{
		private DBConexion conexion = new DBConexion();
		private DBComandos Cmd = new DBComandos();

		public List<CatLineas> ListaLineas(string Linea)
		{
			if(Linea == string.Empty)
			{
				Linea = "%%%";
			}
			else
			{
				Linea = "%" + Linea + "%"; ;
			}
			var lista = new List<CatLineas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatLineas ";
			sql += " where Linea like @Linea order by Linea";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Linea", Linea));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatLineas = new CatLineas();
						CatLineas.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						CatLineas.Linea = reader["Linea"].ToString();
						CatLineas.UsaTalla = Convert.ToBoolean(reader["UsaTalla"].ToString());
						CatLineas.UsaColor = Convert.ToBoolean(reader["UsaColor"].ToString());
						lista.Add(CatLineas);
					}
				}
			}
			catch (Exception ex)
			{
				var CatLineas = new CatLineas();
				CatLineas.IdLinea = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatLineas.Linea = "Error al llenar las Lineas " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatLineas);
			}
			return lista;

		}
		public List<CatLineas> DdlLineas()
		{
			var lista = new List<CatLineas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatLineas ";
			sql += " order by Linea";

			listaParams.Clear();

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatLineas = new CatLineas();
						CatLineas.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						CatLineas.Linea = reader["Linea"].ToString();
						CatLineas.UsaTalla = Convert.ToBoolean(reader["UsaTalla"].ToString());
						CatLineas.UsaColor = Convert.ToBoolean(reader["UsaColor"].ToString());
						lista.Add(CatLineas);
					}
				}
			}
			catch (Exception ex)
			{
				var CatLineas = new CatLineas();
				CatLineas.IdLinea = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatLineas.Linea = "Error al llenar las Lineas " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatLineas);
			}
			return lista;

		}
		public List<CatLineas> DdlLineas2(string Tipo)
		{
			var lista = new List<CatLineas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatLineas ";
			if(Tipo == "SERVICIO" )
			{
				sql += " where Linea = 'SERVICIOS' ";
			}
			else
			{
				sql += " where Linea <> 'SERVICIOS' ";
			}

			sql += " order by Linea";

			listaParams.Clear();

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatLineas = new CatLineas();
						CatLineas.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						CatLineas.Linea = reader["Linea"].ToString();
						CatLineas.UsaTalla = Convert.ToBoolean(reader["UsaTalla"].ToString());
						CatLineas.UsaColor = Convert.ToBoolean(reader["UsaColor"].ToString());
						lista.Add(CatLineas);
					}
				}
			}
			catch (Exception ex)
			{
				var CatLineas = new CatLineas();
				CatLineas.IdLinea = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatLineas.Linea = "Error al llenar las Lineas " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatLineas);
			}
			return lista;

		}
		public CatLineas Linea(int IdLinea)
		{
			var lista = new CatLineas();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatLineas ";
			sql += " where IdLinea = @IdLinea order by Linea";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdLinea", IdLinea));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						lista.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						lista.Linea = reader["Linea"].ToString();
						lista.UsaTalla = Convert.ToBoolean(reader["UsaTalla"].ToString());
						lista.UsaColor = Convert.ToBoolean(reader["UsaColor"].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				lista.IdLinea = ex.HResult;
				//CatLineas.Tipo = "Error";
				lista.Linea = "Error al llenar la Linea " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return lista;

		}
		public RespuestaSQL LineaAdd(CatLineas modCatLineas)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpLineaAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Linea", modCatLineas.Linea));
			listaParams.Add(new SqlParameter("@UsaTalla", modCatLineas.UsaTalla));
			listaParams.Add(new SqlParameter("@UsaColor", modCatLineas.UsaColor));

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
				Respuesta.Mensaje = "Error al Agregar La Linea " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL LineaMod(CatLineas modCatLineas)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpLineaMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdLinea", modCatLineas.IdLinea));
			listaParams.Add(new SqlParameter("@Linea", modCatLineas.Linea));
			listaParams.Add(new SqlParameter("@UsaTalla", modCatLineas.UsaTalla));
			listaParams.Add(new SqlParameter("@UsaColor", modCatLineas.UsaColor));

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
				Respuesta.Mensaje = "Error al Modificar La Linea " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL LineaDel(int IdLinea)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpLineaDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdLinea", IdLinea));

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
				Respuesta.Mensaje = "Error al Modificar La Linea " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtCategorias> ListaCategorias(string categoria, int IdLinea)
		{
			if (categoria == string.Empty)
			{
				categoria = "%%%";
			}
			else
			{
				categoria = "%" + categoria + "%"; ;
			}
			var lista = new List<VtCategorias>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtCategorias ";
			sql += " where Categoria like @Categoria and IdLinea = @IdLinea order by Categoria";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Categoria", categoria));
			listaParams.Add(new SqlParameter("@IdLinea", IdLinea));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtCategorias = new VtCategorias();
						VtCategorias.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtCategorias.Categoria = reader["Categoria"].ToString();
						VtCategorias.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtCategorias.Linea = reader["Linea"].ToString(); 
						lista.Add(VtCategorias);
					}
				}
			}
			catch (Exception ex)
			{
				var VtCategorias = new VtCategorias();
				VtCategorias.IdCat = ex.HResult;
				VtCategorias.Linea = "Error";
				VtCategorias.Categoria = "Error al llenar las Categorias " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(VtCategorias);
			}
			return lista;

		}
		public List<CatCategorias> DdlCategoria(int IdLinea)
		{
			var lista = new List<CatCategorias>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatCategorias ";
			sql += " where IdLinea = @IdLinea order by Categoria";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdLinea", IdLinea));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatCategorias = new CatCategorias();
						CatCategorias.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						CatCategorias.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						CatCategorias.Categoria = reader["Categoria"].ToString(); 
						lista.Add(CatCategorias);
					}
				}
			}
			catch (Exception ex)
			{
				var CatCategorias = new CatCategorias();
				CatCategorias.IdCat = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatCategorias.Categoria = "Error al llenar las Categorias " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatCategorias);
			}
			return lista;

		}
		public CatCategorias Categoria(int IdCat)
		{
			var CatCategorias = new CatCategorias();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatCategorias ";
			sql += " where IdCat = @IdCat order by Categoria";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", IdCat));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						CatCategorias.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						CatCategorias.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						CatCategorias.Categoria = reader["Categoria"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				CatCategorias.IdCat = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatCategorias.Categoria = "Error al llenar la Categoria " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return CatCategorias;

		}
		public RespuestaSQL CategoriaAdd(CatCategorias modCatCategorias)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpCategoriaAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdLinea", modCatCategorias.IdLinea));
			listaParams.Add(new SqlParameter("@Categoria", modCatCategorias.Categoria));

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
				Respuesta.Mensaje = "Error al Agregar La Categoria " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL CategoriaMod(CatCategorias modCatCategorias)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpCategoriaMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", modCatCategorias.IdCat));
			listaParams.Add(new SqlParameter("@IdLinea", modCatCategorias.IdLinea));
			listaParams.Add(new SqlParameter("@Categoria", modCatCategorias.Categoria));

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
				Respuesta.Mensaje = "Error al Modificar La Categoria " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL CategoriaDel(int IdCat)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpCategoriaDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", IdCat));

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
				Respuesta.Mensaje = "Error al Eliminar La Categoria " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtFamilias> ListaFamilias(string Familia, int IdLinea, int IdCat)
		{
			if (Familia == string.Empty)
			{
				Familia = "%%%";
			}
			else
			{
				Familia = "%" + Familia + "%"; ;
			}
			var lista = new List<VtFamilias>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtFamilias ";
			sql += " where Familia like @Familia and IdCat = @IdCat and IdLinea = @IdLinea order by Familia";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Familia", Familia));
			listaParams.Add(new SqlParameter("@IdCat", IdCat));
			listaParams.Add(new SqlParameter("@IdLinea", IdLinea));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtFamilias = new VtFamilias();
						VtFamilias.IdFam = Convert.ToInt32(reader["IdFam"].ToString());
						VtFamilias.Familia = reader["Familia"].ToString();
						VtFamilias.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtFamilias.Categoria = reader["Categoria"].ToString();
						VtFamilias.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtFamilias.Linea = reader["Linea"].ToString();
						
						lista.Add(VtFamilias);
					}
				}
			}
			catch (Exception ex)
			{
				var VtFamilias = new VtFamilias();
				VtFamilias.IdFam = ex.HResult;
				VtFamilias.Categoria = "Error";
				VtFamilias.Familia = "Error al llenar las Familias " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(VtFamilias);
			}
			return lista;

		}
		public List<CatFamilias> DdlFamilia(int IdCat)
		{
			var lista = new List<CatFamilias>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatFamilias ";
			sql += " where IdCat = @IdCat order by Familia";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", IdCat));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatFamilias = new CatFamilias();
						CatFamilias.IdFam = Convert.ToInt32(reader["IdFam"].ToString());
						CatFamilias.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						CatFamilias.Familia = reader["Familia"].ToString(); 
						
						lista.Add(CatFamilias);
					}
				}
			}
			catch (Exception ex)
			{
				var CatFamilias = new CatFamilias();
				CatFamilias.IdCat = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatFamilias.Familia = "Error al llenar las Familias " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatFamilias);
			}
			return lista;

		}
		public VtFamilias Familia(int IdFam)
		{
			var VtFamilias = new VtFamilias();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtFamilias ";
			sql += " where IdFam = @IdFam order by Familia";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdFam", IdFam));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						VtFamilias.IdFam = Convert.ToInt32(reader["IdFam"].ToString());
						VtFamilias.Familia = reader["Familia"].ToString();
						VtFamilias.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtFamilias.Categoria = reader["Categoria"].ToString();
						VtFamilias.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtFamilias.Linea = reader["Linea"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				VtFamilias.IdFam = ex.HResult;
				VtFamilias.Categoria = "Error";
				VtFamilias.Familia = "Error al llenar la Familia " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return VtFamilias;

		}
		public RespuestaSQL FamiliaAdd(CatFamilias modCatFamilias)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpFamiliaAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", modCatFamilias.IdCat));
			listaParams.Add(new SqlParameter("@Familia", modCatFamilias.Familia));

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
				Respuesta.Mensaje = "Error al Agregar La Familia " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL FamiliaMod(CatFamilias modCatFamilias)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpFamiliaMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdFam", modCatFamilias.IdFam));
			listaParams.Add(new SqlParameter("@IdCat", modCatFamilias.IdCat));
			listaParams.Add(new SqlParameter("@Familia", modCatFamilias.Familia));

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
				Respuesta.Mensaje = "Error al Modificar La Familia " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL FamiliaDel(int IdFam)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpFamiliaDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdFam", IdFam));

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
				Respuesta.Mensaje = "Error al Eliminar La Familia " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtMarcas> ListaMarcas(string Marca, int IdLinea)
		{
			if (Marca == string.Empty)
			{
				Marca = "%%%";
			}
			else
			{
				Marca = "%" + Marca + "%"; ;
			}
			var lista = new List<VtMarcas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtMarcas ";
			sql += " where Marca like @Marca and IdLinea = @IdLinea order by Marca";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Marca", Marca));
			listaParams.Add(new SqlParameter("@IdLinea", IdLinea));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtMarcas = new VtMarcas();
						VtMarcas.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
						VtMarcas.Marca = reader["Marca"].ToString();
						VtMarcas.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString()); 
						VtMarcas.Linea = reader["Linea"].ToString();

						lista.Add(VtMarcas);
					}
				}
			}
			catch (Exception ex)
			{
				var VtMarcas = new VtMarcas();
				VtMarcas.IdMarca = ex.HResult;
				VtMarcas.Linea = "Error";
				VtMarcas.Marca = "Error al llenar las Marcas " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(VtMarcas);
			}
			return lista;

		}
		public List<CatMarcas> DdlMarca(int IdLinea)
		{
			var lista = new List<CatMarcas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatMarcas ";
			sql += " where IdLinea = @IdLinea order by Marca";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdLinea", IdLinea));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatMarcas = new CatMarcas();
						CatMarcas.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
						CatMarcas.Marca = reader["Marca"].ToString();
						CatMarcas.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());

						lista.Add(CatMarcas);
					}
				}
			}
			catch (Exception ex)
			{
				var CatMarcas = new CatMarcas();
				CatMarcas.IdMarca = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatMarcas.Marca = "Error al llenar las Marcas " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatMarcas);
			}
			return lista;

		}
		public CatMarcas Marca(int IdMarca)
		{
			var CatMarcas = new CatMarcas();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatMarcas ";
			sql += " where IdMarca = @IdMarca order by Marca";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMarca", IdMarca));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						CatMarcas.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
						CatMarcas.Marca = reader["Marca"].ToString();
						CatMarcas.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());

					}
				}
			}
			catch (Exception ex)
			{
				CatMarcas.IdMarca = ex.HResult;
				//CatMarcas.Categoria = "Error";
				CatMarcas.Marca = "Error al llenar la Marca " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return CatMarcas;

		}
		public RespuestaSQL MarcaAdd(CatMarcas modCatMarcas)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpMarcaAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Marca", modCatMarcas.Marca));
			listaParams.Add(new SqlParameter("@IdLinea", modCatMarcas.IdLinea));

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
				Respuesta.Mensaje = "Error al Agregar La Marca " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL MarcaMod(CatMarcas modCatMarcas)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpMarcaMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMarca", modCatMarcas.IdMarca));
			listaParams.Add(new SqlParameter("@Marca", modCatMarcas.Marca));
			listaParams.Add(new SqlParameter("@IdLinea", modCatMarcas.IdLinea));

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
				Respuesta.Mensaje = "Error al Modificar La Marca " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL MarcaDel(int IdMarca)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpMarcaDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMarca", IdMarca));

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
				Respuesta.Mensaje = "Error al Eliminar La Marca " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtModelos> ListaModelos(string Modelo, string IdMarca)
		{
			if (Modelo == string.Empty)
			{
				Modelo = "%%%";
			}
			else
			{
				Modelo = "%" + Modelo + "%"; ;
			}
			if (IdMarca == string.Empty)
			{
				IdMarca = "%%%";
			}
			var lista = new List<VtModelos>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtModelos ";
			sql += " where Modelo like @Modelo and IdMarca like @IdMarca order by Modelo";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Modelo", Modelo));
			listaParams.Add(new SqlParameter("@IdMarca", IdMarca));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtModelos = new VtModelos();
						VtModelos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						VtModelos.Modelo = reader["Modelo"].ToString();
						VtModelos.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
						VtModelos.Marca = reader["Marca"].ToString();
						VtModelos.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtModelos.Linea = reader["Linea"].ToString();

						lista.Add(VtModelos);
					}
				}
			}
			catch (Exception ex)
			{
				var VtModelos = new VtModelos();
				VtModelos.IdMod = ex.HResult;
				VtModelos.Marca = "Error";
				VtModelos.Modelo = "Error al llenar los Modelos " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(VtModelos);
			}
			return lista;

		}
		public List<CatModelos> DdlModelos(int IdMarca)
		{
			var lista = new List<CatModelos>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatModelos ";
			sql += " where IdMarca = @IdMarca order by Modelo";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMarca", IdMarca));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatModelos = new CatModelos();
						CatModelos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						CatModelos.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
						CatModelos.Modelo = reader["Modelo"].ToString();

						lista.Add(CatModelos);
					}
				}
			}
			catch (Exception ex)
			{
				var CatModelos = new CatModelos();
				CatModelos.IdMod = ex.HResult;
				//CatModelos.Tipo = "Error";
				CatModelos.Modelo = "Error al llenar los Modelos " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatModelos);
			}
			return lista;

		}
		public VtModelos Modelo(int IdModelo)
		{
			var VtModelos = new VtModelos();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtModelos ";
			sql += " where IdModelo = @IdModelo order by Modelo ";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdModelo", IdModelo));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						VtModelos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						VtModelos.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
						VtModelos.Modelo = reader["Modelo"].ToString();
						VtModelos.Marca = reader["Marca"].ToString();
						VtModelos.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtModelos.Linea = reader["Linea"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				VtModelos.IdMod = ex.HResult;
				VtModelos.Marca = "Error";
				VtModelos.Modelo = "Error al llenar el Modelo " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return VtModelos;

		}
		public RespuestaSQL ModeloAdd(CatModelos modCatModelos)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpModeloAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMarca", modCatModelos.IdMarca));
			listaParams.Add(new SqlParameter("@Modelo", modCatModelos.Modelo));

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
				Respuesta.Mensaje = "Error al Agregar el Modelo " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL ModeloMod(CatModelos modCatModelos)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpModeloMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMod", modCatModelos.IdMod));
			listaParams.Add(new SqlParameter("@IdMarca", modCatModelos.IdMarca));
			listaParams.Add(new SqlParameter("@Modelo", modCatModelos.Modelo));

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
				Respuesta.Mensaje = "Error al Modificar el Modelo " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL ModeloDel(int IdMod)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpModeloDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdMod", IdMod));

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
				Respuesta.Mensaje = "Error al Eliminar el Modelo " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtColores> ListaColores(string Color, string IdCat)
		{
			if (Color == string.Empty)
			{
				Color = "%%%";
			}
			else
			{
				Color = "%" + Color + "%"; ;
			}
			if (IdCat == string.Empty)
			{
				IdCat = "%%%";
			}
			var lista = new List<VtColores>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtColores ";
			sql += " where Color like @Color and IdCat like @IdCat order by Color";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Color", Color));
			listaParams.Add(new SqlParameter("@IdCat", IdCat));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtColores = new VtColores();
						VtColores.IdColor = Convert.ToInt32(reader["IdColor"].ToString());
						VtColores.Color = reader["Color"].ToString();
						VtColores.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtColores.Categoria = reader["Categoria"].ToString();
						VtColores.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtColores.Linea = reader["Linea"].ToString();

						lista.Add(VtColores);
					}
				}
			}
			catch (Exception ex)
			{
				var VtColores = new VtColores();
				VtColores.IdColor = ex.HResult;
				VtColores.Categoria = "Error";
				VtColores.Color = "Error al llenar los Colores " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(VtColores);
			}
			return lista;

		}
		public List<CatColores> DdlColores(int IdCat)
		{
			var lista = new List<CatColores>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatColores ";
			sql += " where IdCat = @IdCat order by Color";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", IdCat));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatColores = new CatColores();
						CatColores.IdColor = Convert.ToInt32(reader["IdColor"].ToString());
						CatColores.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						CatColores.Color = reader["Color"].ToString();

						lista.Add(CatColores);
					}
				}
			}
			catch (Exception ex)
			{
				var CatColores = new CatColores();
				CatColores.IdColor = ex.HResult;
				//CatModelos.Tipo = "Error";
				CatColores.Color = "Error al llenar los Colores " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatColores);
			}
			return lista;

		}
		public VtColores Color(int IdColor)
		{
			var VtColores = new VtColores();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtColores ";
			sql += " where IdColor = @IdColor order by Color";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdColor", IdColor));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						VtColores.IdColor = Convert.ToInt32(reader["IdColor"].ToString());
						VtColores.Color = reader["Color"].ToString();
						VtColores.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtColores.Categoria = reader["Categoria"].ToString();
						VtColores.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtColores.Linea = reader["Linea"].ToString();

					}
				}
			}
			catch (Exception ex)
			{
				VtColores.IdColor = ex.HResult;
				VtColores.Categoria = "Error";
				VtColores.Color = "Error al llenar el Color " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return VtColores;

		}
		public RespuestaSQL ColorAdd(CatColores modCatColores)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpColorAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", modCatColores.IdCat));
			listaParams.Add(new SqlParameter("@Color", modCatColores.Color));

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
				Respuesta.Mensaje = "Error al Agregar el Color " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL ColorMod(CatColores modCatColores)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpColorMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdColor", modCatColores.IdColor));
			listaParams.Add(new SqlParameter("@IdCat", modCatColores.IdCat));
			listaParams.Add(new SqlParameter("@Color", modCatColores.Color));

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
				Respuesta.Mensaje = "Error al Modificar el Color " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL ColorDel(int IdColor)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpColorDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdColor", IdColor));

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
				Respuesta.Mensaje = "Error al Eliminar el Color " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtTallas> ListaTallas(string Talla, string IdCat)
		{
			if (Talla == string.Empty)
			{
				Talla = "%%%";
			}
			else
			{
				Talla = "%" + Talla + "%"; ;
			}
			if (IdCat == string.Empty)
			{
				IdCat = "%%%";
			}
			var lista = new List<VtTallas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtTallas ";
			sql += " where Talla like @Talla and IdCat like @IdCat order by Talla";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Talla", Talla));
			listaParams.Add(new SqlParameter("@IdCat", IdCat));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtTallas = new VtTallas();
						VtTallas.IdTalla = Convert.ToInt32(reader["IdTalla"].ToString());
						VtTallas.Talla = reader["Talla"].ToString();
						VtTallas.RangoIni = Convert.ToInt32(reader["RangoIni"].ToString());
						VtTallas.RangoFin = Convert.ToInt32(reader["RangoFin"].ToString());
						VtTallas.ManejaMed = Convert.ToBoolean(reader["ManejaMed"].ToString());
						VtTallas.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtTallas.Categoria = reader["Categoria"].ToString();
						VtTallas.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtTallas.Linea = reader["Linea"].ToString();

						lista.Add(VtTallas);
					}
				}
			}
			catch (Exception ex)
			{
				var VtTallas = new VtTallas();
				VtTallas.IdTalla = ex.HResult;
				VtTallas.Categoria = "Error";
				VtTallas.Talla = "Error al llenar las Tallas " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(VtTallas);
			}
			return lista;

		}
		public List<CatTallas> DdlTalla(int IdCat)
		{
			var lista = new List<CatTallas>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatTallas ";
			sql += " where IdCat = @IdCat order by Talla";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", IdCat));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatTallas = new CatTallas();
						CatTallas.IdTalla = Convert.ToInt32(reader["IdTalla"].ToString());
						CatTallas.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						CatTallas.Talla = reader["Talla"].ToString();
						CatTallas.RangoIni = Convert.ToInt32(reader["RangoIni"].ToString());
						CatTallas.RangoFin = Convert.ToInt32(reader["RangoFin"].ToString());
						CatTallas.ManejaMed = Convert.ToBoolean(reader["ManejaMed"].ToString());

						lista.Add(CatTallas);
					}
				}
			}
			catch (Exception ex)
			{
				var CatTallas = new CatTallas();
				CatTallas.IdTalla = ex.HResult;
				//CatModelos.Tipo = "Error";
				CatTallas.Talla = "Error al llenar las Tallas " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatTallas);
			}
			return lista;

		}
		public VtTallas Talla(int IdTalla)
		{
			var VtTallas = new VtTallas();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtTallas ";
			sql += " where IdTalla = @IdTalla order by Talla";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdTalla", IdTalla));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						VtTallas.IdTalla = Convert.ToInt32(reader["IdTalla"].ToString());
						VtTallas.Talla = reader["Talla"].ToString();
						VtTallas.RangoIni = Convert.ToInt32(reader["RangoIni"].ToString());
						VtTallas.RangoFin = Convert.ToInt32(reader["RangoFin"].ToString());
						VtTallas.ManejaMed = Convert.ToBoolean(reader["ManejaMed"].ToString());
						VtTallas.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtTallas.Categoria = reader["Categoria"].ToString();
						VtTallas.IdLinea = Convert.ToInt32(reader["IdLinea"].ToString());
						VtTallas.Linea = reader["Linea"].ToString();

					}
				}
			}
			catch (Exception ex)
			{
				VtTallas.IdTalla = ex.HResult;
				VtTallas.Categoria = "Error";
				VtTallas.Talla = "Error al llenar la Talla " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return VtTallas;

		}
		public RespuestaSQL TallaAdd(CatTallas modCatTallas)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpTallaAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdCat", modCatTallas.IdCat));
			listaParams.Add(new SqlParameter("@Talla", modCatTallas.Talla));
			listaParams.Add(new SqlParameter("@RangoIni", modCatTallas.RangoIni));
			listaParams.Add(new SqlParameter("@RangoFin", modCatTallas.RangoFin));
			listaParams.Add(new SqlParameter("@ManejaMed", modCatTallas.ManejaMed));

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
				Respuesta.Mensaje = "Error al Agregar la Talla " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL TallaMod(CatTallas modCatTallas)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpTallaMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdTalla", modCatTallas.IdTalla));
			listaParams.Add(new SqlParameter("@IdCat", modCatTallas.IdCat));
			listaParams.Add(new SqlParameter("@Talla", modCatTallas.Talla));
			listaParams.Add(new SqlParameter("@RangoIni", modCatTallas.RangoIni));
			listaParams.Add(new SqlParameter("@RangoFin", modCatTallas.RangoFin));
			listaParams.Add(new SqlParameter("@ManejaMed", modCatTallas.ManejaMed));

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
				Respuesta.Mensaje = "Error al Modificar la Talla " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL TallaDel(int IdTalla)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpTallaDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdTalla", IdTalla));

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
				Respuesta.Mensaje = "Error al Eliminar la Talla " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<CatUnidad> ListaUnidad(string Unidad)
		{
			if (Unidad == string.Empty)
			{
				Unidad = "%%%";
			}
			else
			{
				Unidad = "%" + Unidad + "%"; ;
			}

			var lista = new List<CatUnidad>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatUnidad ";
			sql += " where Unidad like @Unidad order by Unidad";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Unidad", Unidad));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatUnidad = new CatUnidad();
						CatUnidad.Unidad = reader["Unidad"].ToString();
						CatUnidad.Descripcion = reader["Descripcion"].ToString();
						CatUnidad.ClaveSAT = reader["ClaveSAT"].ToString();

						lista.Add(CatUnidad);
					}
				}
			}
			catch (Exception ex)
			{
				var CatUnidad = new CatUnidad();
				CatUnidad.Unidad = ex.HResult.ToString();
				CatUnidad.ClaveSAT = "Error";
				CatUnidad.Descripcion = "Error al llenar las Unidades " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatUnidad);
			}
			return lista;

		}
		public List<CatUnidad> DdlUnidad()
		{
			var lista = new List<CatUnidad>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatUnidad ";
			sql += " order by Unidad";

			listaParams.Clear();

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatUnidad = new CatUnidad();
						CatUnidad.Unidad = reader["Unidad"].ToString();
						CatUnidad.Descripcion = reader["Descripcion"].ToString();
						CatUnidad.ClaveSAT = reader["ClaveSAT"].ToString();

						lista.Add(CatUnidad);
					}
				}
			}
			catch (Exception ex)
			{
				var CatUnidad = new CatUnidad();
				CatUnidad.Unidad = ex.HResult.ToString();
				CatUnidad.ClaveSAT = "Error";
				CatUnidad.Descripcion = "Error al llenar las Unidades " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatUnidad);
			}
			return lista;

		}
		public List<CatUnidad> DdlUnidad2(string Tipo)
		{
			var lista = new List<CatUnidad>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatUnidad ";
			if(Tipo == "SERVICIO")
			{
				sql += " where Descripcion like '%Servicio%' ";
			}
			if(Tipo == "PAQUETE")
			{
				sql += " where Descripcion like '%Paquete%' ";
			}
			if (Tipo == "PRODUCTO")
			{
				sql += " where Descripcion not like '%Paquete%' and Descripcion not like '%Servicio%' ";
			}
			sql += " order by Unidad";

			listaParams.Clear();

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var CatUnidad = new CatUnidad();
						CatUnidad.Unidad = reader["Unidad"].ToString();
						CatUnidad.Descripcion = reader["Descripcion"].ToString();
						CatUnidad.ClaveSAT = reader["ClaveSAT"].ToString();

						lista.Add(CatUnidad);
					}
				}
			}
			catch (Exception ex)
			{
				var CatUnidad = new CatUnidad();
				CatUnidad.Unidad = ex.HResult.ToString();
				CatUnidad.ClaveSAT = "Error";
				CatUnidad.Descripcion = "Error al llenar las Unidades " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(CatUnidad);
			}
			return lista;

		}

		public CatUnidad Unidad(string Unidad)
		{
			var CatUnidad = new CatUnidad();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.CatUnidad ";
			sql += " where Unidad = @Unidad order by Unidad";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Unidad", Unidad));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						CatUnidad.Unidad = reader["Unidad"].ToString();
						CatUnidad.Descripcion = reader["Descripcion"].ToString();
						CatUnidad.ClaveSAT = reader["ClaveSAT"].ToString();

					}
				}
			}
			catch (Exception ex)
			{
				CatUnidad.Unidad = ex.HResult.ToString();
				CatUnidad.ClaveSAT = "Error";
				CatUnidad.Descripcion = "Error al llenar la Unidad " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return CatUnidad;

		}
		public RespuestaSQL UnidadAdd(CatUnidad modCatUnidad)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpUnidadAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Unidad", modCatUnidad.Unidad));
			listaParams.Add(new SqlParameter("@Descripcion", modCatUnidad.Descripcion));
			listaParams.Add(new SqlParameter("@ClaveSAT", modCatUnidad.ClaveSAT));

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
				Respuesta.Mensaje = "Error al Agregar la Unidad " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL UnidadMod(CatUnidad modCatUnidad)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpUnidadMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Unidad", modCatUnidad.Unidad));
			listaParams.Add(new SqlParameter("@Descripcion", modCatUnidad.Descripcion));
			listaParams.Add(new SqlParameter("@ClaveSAT", modCatUnidad.ClaveSAT));

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
				Respuesta.Mensaje = "Error al Modificar la Unidad " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL UnidadDel(string Unidad)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpUnidadDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Unidad", Unidad));

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
				Respuesta.Mensaje = "Error al Eliminar la Unidad " + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtArticulos> ListaArticulos(string Articulo, string IdLin, string IdCat, string IdFam)
		{
			if (Articulo == string.Empty)
			{
				Articulo = "%%%";
			}
			else
			{
				Articulo = "%" + Articulo + "%"; ;
			}
			if(IdLin == "0" || IdLin == string.Empty)
			{
				IdLin = "%%%";
			}
			if (IdCat == "0" || IdCat == string.Empty)
			{
				IdCat = "%%%";
			}
			if (IdFam == "0" || IdFam == string.Empty)
			{
				IdFam = "%%%";
			}

			var lista = new List<VtArticulos>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtArticulos ";
			sql += " where (Descripcion like @Articulo or Codigo like @Articulo or BarCode like @Articulo) and IdLin like @IdLin and IdCat like @IdCat and IdFam like @IdFam order by Descripcion";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Articulo", Articulo));
			listaParams.Add(new SqlParameter("@IdLin", IdLin));
			listaParams.Add(new SqlParameter("@IdCat", IdCat));
			listaParams.Add(new SqlParameter("@IdFam", IdFam));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtArticulos = new VtArticulos();
						VtArticulos.IdArt = Convert.ToInt32(reader["IdArt"].ToString());
						VtArticulos.Codigo = reader["Codigo"].ToString();
						VtArticulos.BarCode = reader["BarCode"].ToString();
						VtArticulos.Descripcion = reader["Descripcion"].ToString();
						VtArticulos.idLin = Convert.ToInt32(reader["idLin"].ToString());
						VtArticulos.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtArticulos.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
						VtArticulos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						VtArticulos.CostoProm = Convert.ToDecimal(reader["CostoProm"].ToString());
						VtArticulos.UltCosto = Convert.ToDecimal(reader["UltCosto"].ToString());
						VtArticulos.UltCompra = Convert.ToDateTime(reader["UltCompra"].ToString());
						VtArticulos.Precio = Convert.ToDecimal(reader["Precio"].ToString());
						VtArticulos.Contado = Convert.ToDecimal(reader["Contado"].ToString());
						VtArticulos.PrecioMin = Convert.ToDecimal(reader["PrecioMin"].ToString());
						VtArticulos.Unidad = reader["Unidad"].ToString();
						VtArticulos.EsPaquete = Convert.ToBoolean(reader["EsPaquete"].ToString());
						VtArticulos.EsServicio = Convert.ToBoolean(reader["EsServicio"].ToString());
						VtArticulos.CodigoCFDI = reader["CodigoCFDI"].ToString();
						VtArticulos.CContable1 = reader["CContable1"].ToString();
						VtArticulos.CContable2 = reader["CContable2"].ToString();
						VtArticulos.CContable3 = reader["CContable3"].ToString();
						VtArticulos.IdImpuesto1 = Convert.ToInt32(reader["IdImpuesto1"].ToString());
						VtArticulos.IdImpuesto2 = Convert.ToInt32(reader["IdImpuesto2"].ToString());
						VtArticulos.IdImpuesto3 = Convert.ToInt32(reader["IdImpuesto3"].ToString());
						VtArticulos.UsuAdd = reader["UsuAdd"].ToString();
						VtArticulos.FechaAdd = Convert.ToDateTime(reader["FechaAdd"].ToString());
						VtArticulos.UsuMod = reader["UsuMod"].ToString();
						VtArticulos.FechaMod = Convert.ToDateTime(reader["FechaMod"].ToString());
						VtArticulos.UsuDel = reader["UsuDel"].ToString();
						VtArticulos.FechaDel = Convert.ToDateTime(reader["FechaDel"].ToString());
						VtArticulos.Estatus = reader["Estatus"].ToString();
						VtArticulos.IdFam = Convert.ToInt32(reader["IdFam"].ToString());
						VtArticulos.Linea = reader["Linea"].ToString();
						VtArticulos.Categoria = reader["Categoria"].ToString();
						VtArticulos.Familia = reader["Familia"].ToString();
						VtArticulos.Marca = reader["Marca"].ToString();
						VtArticulos.Modelo = reader["Modelo"].ToString();
						VtArticulos.UsuarioAdd = reader["UsuarioAdd"].ToString();

						lista.Add(VtArticulos);
					}
				}
			}
			catch (Exception ex)
			{
				var VtArticulos = new VtArticulos();
				VtArticulos.IdArt = ex.HResult;
				VtArticulos.Codigo = "Error";
				VtArticulos.Descripcion = "Error al llenar los Articulos " + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(VtArticulos);
			}
			return lista;

		}
		public VtArticulos Articulo(int IdArt)
		{
			var VtArticulos = new VtArticulos();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtArticulos ";
			sql += " where IdArt = @IdArt order by Descripcion";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdArt", IdArt));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						VtArticulos.IdArt = Convert.ToInt32(reader["IdArt"].ToString());
						VtArticulos.Codigo = reader["Codigo"].ToString();
						VtArticulos.BarCode = reader["BarCode"].ToString();
						VtArticulos.Descripcion = reader["Descripcion"].ToString();
						VtArticulos.idLin = Convert.ToInt32(reader["idLin"].ToString());
						VtArticulos.IdCat = Convert.ToInt32(reader["IdCat"].ToString());
						VtArticulos.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
						VtArticulos.IdMod = Convert.ToInt32(reader["IdMod"].ToString());
						VtArticulos.CostoProm = Convert.ToDecimal(reader["CostoProm"].ToString());
						VtArticulos.UltCosto = Convert.ToDecimal(reader["UltCosto"].ToString());
						VtArticulos.UltCompra = Convert.ToDateTime(reader["UltCompra"].ToString());
						VtArticulos.Precio = Convert.ToDecimal(reader["Precio"].ToString());
						VtArticulos.Contado = Convert.ToDecimal(reader["Contado"].ToString());
						VtArticulos.PrecioMin = Convert.ToDecimal(reader["PrecioMin"].ToString());
						VtArticulos.Unidad = reader["Unidad"].ToString();
						VtArticulos.EsPaquete = Convert.ToBoolean(reader["EsPaquete"].ToString());
						VtArticulos.EsServicio = Convert.ToBoolean(reader["EsServicio"].ToString());
						VtArticulos.CodigoCFDI = reader["CodigoCFDI"].ToString();
						VtArticulos.CContable1 = reader["CContable1"].ToString();
						VtArticulos.CContable2 = reader["CContable2"].ToString();
						VtArticulos.CContable3 = reader["CContable3"].ToString();
						VtArticulos.IdImpuesto1 = Convert.ToInt32(reader["IdImpuesto1"].ToString());
						VtArticulos.IdImpuesto2 = Convert.ToInt32(reader["IdImpuesto2"].ToString());
						VtArticulos.IdImpuesto3 = Convert.ToInt32(reader["IdImpuesto3"].ToString());
						VtArticulos.UsuAdd = reader["UsuAdd"].ToString();
						VtArticulos.FechaAdd = Convert.ToDateTime(reader["FechaAdd"].ToString());
						VtArticulos.UsuMod = reader["UsuMod"].ToString();
						VtArticulos.FechaMod = Convert.ToDateTime(reader["FechaMod"].ToString());
						VtArticulos.UsuDel = reader["UsuDel"].ToString();
						VtArticulos.FechaDel = Convert.ToDateTime(reader["FechaDel"].ToString());
						VtArticulos.Estatus = reader["Estatus"].ToString();
						VtArticulos.IdFam = Convert.ToInt32(reader["IdFam"].ToString());
						VtArticulos.Linea = reader["Linea"].ToString();
						VtArticulos.Categoria = reader["Categoria"].ToString();
						VtArticulos.Familia = reader["Familia"].ToString();
						VtArticulos.Marca = reader["Marca"].ToString();
						VtArticulos.Modelo = reader["Modelo"].ToString();
						VtArticulos.UsuarioAdd = reader["UsuarioAdd"].ToString();

					}
				}
			}
			catch (Exception ex)
			{
				VtArticulos.IdArt = ex.HResult;
				VtArticulos.Codigo = "Error";
				VtArticulos.Descripcion = "Error al llenar El Articulo" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return VtArticulos;

		}
		public RespuestaSQL ArticuloAdd(CatArticulos modCatArticulos)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpArticuloAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Codigo", modCatArticulos.Codigo));
			listaParams.Add(new SqlParameter("@BarCode", modCatArticulos.BarCode));
			listaParams.Add(new SqlParameter("@Descripcion", modCatArticulos.Descripcion));
			listaParams.Add(new SqlParameter("@idLin", modCatArticulos.idLin));
			listaParams.Add(new SqlParameter("@IdCat", modCatArticulos.IdCat));
			listaParams.Add(new SqlParameter("@IdMarca", modCatArticulos.IdMarca));
			listaParams.Add(new SqlParameter("@IdMod", modCatArticulos.IdMod));
			listaParams.Add(new SqlParameter("@CostoProm", modCatArticulos.CostoProm));
			listaParams.Add(new SqlParameter("@UltCosto", modCatArticulos.UltCosto));
			listaParams.Add(new SqlParameter("@UltCompra", modCatArticulos.UltCompra));
			listaParams.Add(new SqlParameter("@Precio", modCatArticulos.Precio));
			listaParams.Add(new SqlParameter("@Contado", modCatArticulos.Contado));
			listaParams.Add(new SqlParameter("@PrecioMin", modCatArticulos.PrecioMin));
			listaParams.Add(new SqlParameter("@Unidad", modCatArticulos.Unidad));
			listaParams.Add(new SqlParameter("@EsPaquete", modCatArticulos.EsPaquete));
			listaParams.Add(new SqlParameter("@EsServicio", modCatArticulos.EsServicio));
			listaParams.Add(new SqlParameter("@CodigoCFDI", modCatArticulos.CodigoCFDI));
			listaParams.Add(new SqlParameter("@CContable1", modCatArticulos.CContable1));
			listaParams.Add(new SqlParameter("@CContable2", modCatArticulos.CContable2));
			listaParams.Add(new SqlParameter("@CContable3", modCatArticulos.CContable3));
			listaParams.Add(new SqlParameter("@IdImpuesto1", modCatArticulos.IdImpuesto1));
			listaParams.Add(new SqlParameter("@IdImpuesto2", modCatArticulos.IdImpuesto2));
			listaParams.Add(new SqlParameter("@IdImpuesto3", modCatArticulos.IdImpuesto3));
			listaParams.Add(new SqlParameter("@UsuAdd", modCatArticulos.UsuAdd));
			listaParams.Add(new SqlParameter("@Estatus", modCatArticulos.Estatus));
			listaParams.Add(new SqlParameter("@IdFam", modCatArticulos.IdFam));

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
				Respuesta.Mensaje = "Error al Agregar el Articulo" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL ArticuloMod(CatArticulos modCatArticulos)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpArticuloMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdArt", modCatArticulos.IdArt));
			listaParams.Add(new SqlParameter("@Codigo", modCatArticulos.Codigo));
			listaParams.Add(new SqlParameter("@BarCode", modCatArticulos.BarCode));
			listaParams.Add(new SqlParameter("@Descripcion", modCatArticulos.Descripcion));
			listaParams.Add(new SqlParameter("@idLin", modCatArticulos.idLin));
			listaParams.Add(new SqlParameter("@IdCat", modCatArticulos.IdCat));
			listaParams.Add(new SqlParameter("@IdMarca", modCatArticulos.IdMarca));
			listaParams.Add(new SqlParameter("@IdMod", modCatArticulos.IdMod));
			listaParams.Add(new SqlParameter("@CostoProm", modCatArticulos.CostoProm));
			listaParams.Add(new SqlParameter("@UltCosto", modCatArticulos.UltCosto));
			listaParams.Add(new SqlParameter("@UltCompra", modCatArticulos.UltCompra));
			listaParams.Add(new SqlParameter("@Precio", modCatArticulos.Precio));
			listaParams.Add(new SqlParameter("@Contado", modCatArticulos.Contado));
			listaParams.Add(new SqlParameter("@PrecioMin", modCatArticulos.PrecioMin));
			listaParams.Add(new SqlParameter("@Unidad", modCatArticulos.Unidad));
			listaParams.Add(new SqlParameter("@EsPaquete", modCatArticulos.EsPaquete));
			listaParams.Add(new SqlParameter("@EsServicio", modCatArticulos.EsServicio));
			listaParams.Add(new SqlParameter("@CodigoCFDI", modCatArticulos.CodigoCFDI));
			listaParams.Add(new SqlParameter("@CContable1", modCatArticulos.CContable1));
			listaParams.Add(new SqlParameter("@CContable2", modCatArticulos.CContable2));
			listaParams.Add(new SqlParameter("@CContable3", modCatArticulos.CContable3));
			listaParams.Add(new SqlParameter("@IdImpuesto1", modCatArticulos.IdImpuesto1));
			listaParams.Add(new SqlParameter("@IdImpuesto2", modCatArticulos.IdImpuesto2));
			listaParams.Add(new SqlParameter("@IdImpuesto3", modCatArticulos.IdImpuesto3));
			listaParams.Add(new SqlParameter("@UsuMod", modCatArticulos.UsuMod));
			listaParams.Add(new SqlParameter("@Estatus", modCatArticulos.Estatus));
			listaParams.Add(new SqlParameter("@IdFam", modCatArticulos.IdFam));

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
				Respuesta.Mensaje = "Error al Modificar el Articulo" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL ArticuloDel(int IdArt, string UsuMod)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpArticuloDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdArt", IdArt));
			listaParams.Add(new SqlParameter("@UsuMod", UsuMod));

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
				Respuesta.Mensaje = "Error al Eliminar el Articulo" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public List<VtArticulosColores> ListaArticulosColores(int IdArt, int IdCat)
		{
			var lista = new List<VtArticulosColores>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtArticulosColores ";
			sql += " where IdArt = @IdArt and IdCat = @IdCat order by Color";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdArt", IdArt));
			listaParams.Add(new SqlParameter("@IdCat", IdCat));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtArticulosColores = new VtArticulosColores();
						VtArticulosColores.IdArtColor = Convert.ToInt32(reader["IdArtColor"].ToString());
						VtArticulosColores.IdArt = Convert.ToInt32(reader["IdArt"].ToString());
						VtArticulosColores.IdColor = Convert.ToInt32(reader["IdColor"].ToString());
						VtArticulosColores.Color = reader["Color"].ToString();
						VtArticulosColores.IdCat = Convert.ToInt32(reader["IdCat"].ToString()); 
						
						lista.Add(VtArticulosColores);
					}
				}
			}
			catch (Exception ex)
			{
				var VtArticulosColores = new VtArticulosColores();
				VtArticulosColores.IdArtColor = ex.HResult;
				//VtArticulosColores.Codigo = "Error";
				VtArticulosColores.Color = "Error al llenar los Articulos" + ex.Message.ToString().Replace("'", "-") + ".";
				lista.Add(VtArticulosColores);
			}
			return lista;

		}

	}
}