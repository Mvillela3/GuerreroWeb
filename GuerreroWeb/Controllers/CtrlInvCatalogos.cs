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
						lista.Add(CatLineas);
					}
				}
			}
			catch (Exception ex)
			{
				var CatLineas = new CatLineas();
				CatLineas.IdLinea = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatLineas.Linea = "Error al llenar las Lineas" + ex.Message.ToString().Replace("'", "-") + ".";
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
						lista.Add(CatLineas);
					}
				}
			}
			catch (Exception ex)
			{
				var CatLineas = new CatLineas();
				CatLineas.IdLinea = ex.HResult;
				//CatLineas.Tipo = "Error";
				CatLineas.Linea = "Error al llenar las Lineas" + ex.Message.ToString().Replace("'", "-") + ".";
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
					}
				}
			}
			catch (Exception ex)
			{
				lista.IdLinea = ex.HResult;
				//CatLineas.Tipo = "Error";
				lista.Linea = "Error al llenar la Linea" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Agregar La Linea" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Modificar La Linea" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Modificar La Linea" + ex.Message.ToString().Replace("'", "-") + ".";
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
				VtCategorias.Categoria = "Error al llenar las Categorias" + ex.Message.ToString().Replace("'", "-") + ".";
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
				CatCategorias.Categoria = "Error al llenar las Categorias" + ex.Message.ToString().Replace("'", "-") + ".";
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
				CatCategorias.Categoria = "Error al llenar la Categoria" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Agregar La Categoria" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Modificar La Categoria" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Eliminar La Categoria" + ex.Message.ToString().Replace("'", "-") + ".";
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
				VtFamilias.Familia = "Error al llenar las Familias" + ex.Message.ToString().Replace("'", "-") + ".";
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
				CatFamilias.Familia = "Error al llenar las Familias" + ex.Message.ToString().Replace("'", "-") + ".";
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
				VtFamilias.Familia = "Error al llenar la Familia" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Agregar La Familia" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Modificar La Familia" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Eliminar La Familia" + ex.Message.ToString().Replace("'", "-") + ".";
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
				VtMarcas.Marca = "Error al llenar las Marcas" + ex.Message.ToString().Replace("'", "-") + ".";
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
				CatMarcas.Marca = "Error al llenar las Marcas" + ex.Message.ToString().Replace("'", "-") + ".";
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
				CatMarcas.Marca = "Error al llenar la Marca" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Agregar La Marca" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Modificar La Marca" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Eliminar La Marca" + ex.Message.ToString().Replace("'", "-") + ".";
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
				VtModelos.Modelo = "Error al llenar los Modelos" + ex.Message.ToString().Replace("'", "-") + ".";
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
				CatModelos.Modelo = "Error al llenar los Modelos" + ex.Message.ToString().Replace("'", "-") + ".";
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
			sql += " where IdModelo = @IdModelo order by Modelo";

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
				VtModelos.Modelo = "Error al llenar el Modelo" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Agregar el Modelo" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Modificar el Modelo" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Eliminar el Modelo" + ex.Message.ToString().Replace("'", "-") + ".";
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
				VtColores.Color = "Error al llenar los Colores" + ex.Message.ToString().Replace("'", "-") + ".";
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
				CatColores.Color = "Error al llenar los Colores" + ex.Message.ToString().Replace("'", "-") + ".";
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
				VtColores.Color = "Error al llenar el Color" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Agregar el Color" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Modificar el Color" + ex.Message.ToString().Replace("'", "-") + ".";
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
				Respuesta.Mensaje = "Error al Eliminar el Color" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}

	}
}