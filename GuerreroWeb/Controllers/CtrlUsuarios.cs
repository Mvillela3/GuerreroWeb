using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static GuerreroWeb.Controllers.DBConexion;


namespace GuerreroWeb.Controllers
{
	public class CtrlUsuarios
    {
        //private ModConexion mconexion;
        private DBConexion conexion = new DBConexion();
        private DBComandos Cmd = new DBComandos();

        public List<VtUsuarios> ListaUsuarios(string Usuario, string IdDepto, string IdPerfil, string Estatus)
        {
            if(Usuario == string.Empty)
            {
                Usuario = "%%%";
            }
            else
            {
                Usuario = "%" + Usuario + "%";
            }
            if (Estatus == string.Empty)
            {
                Estatus = "ACTIVO";
            }
            if (IdDepto == string.Empty || IdDepto == "0")
            {
                IdDepto = "%%%";
            }
            if (IdPerfil == string.Empty || IdPerfil == "0")
            {
                IdPerfil = "%%%";
            }


            var Lista = new List<VtUsuarios>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtUsuarios ";
            sql += " WHERE (Usuario like @Usuario or Nombre like @Usuario) and Estatus = @Estatus and IdDepto like @IdDepto ";
            sql += " and IdPerfil Like @IdPerfil order by Usuario";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Usuario", Usuario));
            listaParams.Add(new SqlParameter("@IdDepto", IdDepto));
            listaParams.Add(new SqlParameter("@IdPerfil", IdPerfil));
            listaParams.Add(new SqlParameter("@Estatus", Estatus));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var VtUsuarios = new VtUsuarios();
						VtUsuarios.IdUsu = Convert.ToInt32(reader["IdUsu"].ToString());
						VtUsuarios.Usuario = reader["Usuario"].ToString();
						VtUsuarios.Nombre = reader["Nombre"].ToString();
						VtUsuarios.PWD = reader["PWD"].ToString();
						VtUsuarios.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
						VtUsuarios.Departamento = reader["Departamento"].ToString();
						VtUsuarios.Email = reader["Email"].ToString();
						VtUsuarios.Telefono = reader["Telefono"].ToString();
						VtUsuarios.Estatus = reader["Estatus"].ToString();
						VtUsuarios.IdPerfil = Convert.ToInt32(reader["IdPerfil"].ToString());
						VtUsuarios.Perfil = reader["Perfil"].ToString();
						VtUsuarios.EntraCxc = Convert.ToBoolean(reader["EntraCxc"].ToString());
						VtUsuarios.CxcAdd = Convert.ToBoolean(reader["CxcAdd"].ToString());
						VtUsuarios.CxcMod = Convert.ToBoolean(reader["CxcMod"].ToString());
						VtUsuarios.CxcCan = Convert.ToBoolean(reader["CxcCan"].ToString());
						VtUsuarios.CxcDel = Convert.ToBoolean(reader["CxcDel"].ToString());
						VtUsuarios.EntraVentas = Convert.ToBoolean(reader["EntraVentas"].ToString());
						VtUsuarios.VentaAdd = Convert.ToBoolean(reader["VentaAdd"].ToString());
						VtUsuarios.VentaMod = Convert.ToBoolean(reader["VentaMod"].ToString());
						VtUsuarios.VentaCan = Convert.ToBoolean(reader["VentaCan"].ToString());
						VtUsuarios.VentaDel = Convert.ToBoolean(reader["VentaDel"].ToString());
						VtUsuarios.EntraInv = Convert.ToBoolean(reader["EntraInv"].ToString());
						VtUsuarios.InvAdd = Convert.ToBoolean(reader["InvAdd"].ToString());
						VtUsuarios.InvMod = Convert.ToBoolean(reader["InvMod"].ToString());
						VtUsuarios.InvCan = Convert.ToBoolean(reader["InvCan"].ToString());
						VtUsuarios.InvDel = Convert.ToBoolean(reader["InvDel"].ToString());
						VtUsuarios.EntraArticulos = Convert.ToBoolean(reader["EntraArticulos"].ToString());
						VtUsuarios.ArticuloAdd = Convert.ToBoolean(reader["ArticuloAdd"].ToString());
						VtUsuarios.ArticuloMod = Convert.ToBoolean(reader["ArticuloMod"].ToString());
						VtUsuarios.ArticuloDel = Convert.ToBoolean(reader["ArticuloDel"].ToString());
						VtUsuarios.EntraLineas = Convert.ToBoolean(reader["EntraLineas"].ToString());
						VtUsuarios.LineasAdd = Convert.ToBoolean(reader["LineasAdd"].ToString());
						VtUsuarios.LineasMod = Convert.ToBoolean(reader["LineasMod"].ToString());
						VtUsuarios.LineasDel = Convert.ToBoolean(reader["LineasDel"].ToString());
						VtUsuarios.EntraCategoria = Convert.ToBoolean(reader["EntraCategoria"].ToString());
						VtUsuarios.CatAdd = Convert.ToBoolean(reader["CatAdd"].ToString());
						VtUsuarios.CatMod = Convert.ToBoolean(reader["CatMod"].ToString());
						VtUsuarios.CatDel = Convert.ToBoolean(reader["CatDel"].ToString());
						VtUsuarios.EntraMarcas = Convert.ToBoolean(reader["EntraMarcas"].ToString());
						VtUsuarios.MarcasAdd = Convert.ToBoolean(reader["MarcasAdd"].ToString());
						VtUsuarios.MarcasMod = Convert.ToBoolean(reader["MarcasMod"].ToString());
						VtUsuarios.MarcasDel = Convert.ToBoolean(reader["MarcasDel"].ToString());
						VtUsuarios.EntraModelos = Convert.ToBoolean(reader["EntraModelos"].ToString());
						VtUsuarios.ModelosAdd = Convert.ToBoolean(reader["ModelosAdd"].ToString());
						VtUsuarios.ModelosMod = Convert.ToBoolean(reader["ModelosMod"].ToString());
						VtUsuarios.ModelosDel = Convert.ToBoolean(reader["ModelosDel"].ToString());
						VtUsuarios.EntraColores = Convert.ToBoolean(reader["EntraColores"].ToString());
						VtUsuarios.ColoresAdd = Convert.ToBoolean(reader["ColoresAdd"].ToString());
						VtUsuarios.ColoresMod = Convert.ToBoolean(reader["ColoresMod"].ToString());
						VtUsuarios.ColoresDel = Convert.ToBoolean(reader["ColoresDel"].ToString());
						VtUsuarios.EntraUnidad = Convert.ToBoolean(reader["EntraUnidad"].ToString());
						VtUsuarios.UnidadAdd = Convert.ToBoolean(reader["UnidadAdd"].ToString());
						VtUsuarios.UnidadMod = Convert.ToBoolean(reader["UnidadMod"].ToString());
						VtUsuarios.UnidadDel = Convert.ToBoolean(reader["UnidadDel"].ToString());
						VtUsuarios.EntraCompras = Convert.ToBoolean(reader["EntraCompras"].ToString());
						VtUsuarios.CompraAdd = Convert.ToBoolean(reader["CompraAdd"].ToString());
						VtUsuarios.CompraMod = Convert.ToBoolean(reader["CompraMod"].ToString());
						VtUsuarios.CompraCan = Convert.ToBoolean(reader["CompraCan"].ToString());
						VtUsuarios.CompraDel = Convert.ToBoolean(reader["CompraDel"].ToString());
						VtUsuarios.EntraCxp = Convert.ToBoolean(reader["EntraCxp"].ToString());
						VtUsuarios.CxpAdd = Convert.ToBoolean(reader["CxpAdd"].ToString());
						VtUsuarios.CxpMod = Convert.ToBoolean(reader["CxpMod"].ToString());
						VtUsuarios.CxpCan = Convert.ToBoolean(reader["CxpCan"].ToString());
						VtUsuarios.CxpDel = Convert.ToBoolean(reader["CxpDel"].ToString());
						VtUsuarios.EntraReportes = Convert.ToBoolean(reader["EntraReportes"].ToString());
						VtUsuarios.EntraRepCxc = Convert.ToBoolean(reader["EntraRepCxc"].ToString());
						VtUsuarios.EntraRepVen = Convert.ToBoolean(reader["EntraRepVen"].ToString());
						VtUsuarios.EntraRepInv = Convert.ToBoolean(reader["EntraRepInv"].ToString());
						VtUsuarios.EntraRepCom = Convert.ToBoolean(reader["EntraRepCom"].ToString());
						VtUsuarios.EntraRepCxp = Convert.ToBoolean(reader["EntraRepCxp"].ToString());
						VtUsuarios.EntraWati = Convert.ToBoolean(reader["EntraWati"].ToString());
						VtUsuarios.EntraWatiEnv = Convert.ToBoolean(reader["EntraWatiEnv"].ToString());
						VtUsuarios.EntraWatiCons = Convert.ToBoolean(reader["EntraWatiCons"].ToString());
						VtUsuarios.EntraWatiPlan = Convert.ToBoolean(reader["EntraWatiPlan"].ToString());
						VtUsuarios.EntraWatiAuto = Convert.ToBoolean(reader["EntraWatiAuto"].ToString());
						VtUsuarios.WatiAdd = Convert.ToBoolean(reader["WatiAdd"].ToString());
						VtUsuarios.WatiDel = Convert.ToBoolean(reader["WatiDel"].ToString());
						VtUsuarios.WatiMod = Convert.ToBoolean(reader["WatiMod"].ToString());
						VtUsuarios.EntraConfiguraciones = Convert.ToBoolean(reader["EntraConfiguraciones"].ToString());
						VtUsuarios.EntraEmpresa = Convert.ToBoolean(reader["EntraEmpresa"].ToString());
						VtUsuarios.EmpresaMod = Convert.ToBoolean(reader["EmpresaMod"].ToString());
						VtUsuarios.EntraSistema = Convert.ToBoolean(reader["EntraSistema"].ToString());
						VtUsuarios.EntraModulos = Convert.ToBoolean(reader["EntraModulos"].ToString());
						VtUsuarios.ModAdd = Convert.ToBoolean(reader["ModAdd"].ToString());
						VtUsuarios.ModMod = Convert.ToBoolean(reader["ModMod"].ToString());
						VtUsuarios.ModDel = Convert.ToBoolean(reader["ModDel"].ToString());
						VtUsuarios.EntraMovtos = Convert.ToBoolean(reader["EntraMovtos"].ToString());
						VtUsuarios.MovtosAdd = Convert.ToBoolean(reader["MovtosAdd"].ToString());
						VtUsuarios.MovtosMod = Convert.ToBoolean(reader["MovtosMod"].ToString());
						VtUsuarios.MovtosDel = Convert.ToBoolean(reader["MovtosDel"].ToString());
						VtUsuarios.EntraFolios = Convert.ToBoolean(reader["EntraFolios"].ToString());
						VtUsuarios.FolioAdd = Convert.ToBoolean(reader["FolioAdd"].ToString());
						VtUsuarios.FolioMod = Convert.ToBoolean(reader["FolioMod"].ToString());
						VtUsuarios.FolioDel = Convert.ToBoolean(reader["FolioDel"].ToString());
						VtUsuarios.EntraUsuarios = Convert.ToBoolean(reader["EntraUsuarios"].ToString());
						VtUsuarios.UsuarioAdd = Convert.ToBoolean(reader["UsuarioAdd"].ToString());
						VtUsuarios.UsuarioMod = Convert.ToBoolean(reader["UsuarioMod"].ToString());
						VtUsuarios.UsuarioDel = Convert.ToBoolean(reader["UsuarioDel"].ToString());
						VtUsuarios.EntraPerfiles = Convert.ToBoolean(reader["EntraPerfiles"].ToString());
						VtUsuarios.PerfilAdd = Convert.ToBoolean(reader["PerfilAdd"].ToString());
						VtUsuarios.PerfilMod = Convert.ToBoolean(reader["PerfilMod"].ToString());
						VtUsuarios.PerfilDel = Convert.ToBoolean(reader["PerfilDel"].ToString());
						VtUsuarios.EntraSucursales = Convert.ToBoolean(reader["EntraSucursales"].ToString());
						VtUsuarios.SucursalAdd = Convert.ToBoolean(reader["SucursalAdd"].ToString());
						VtUsuarios.SucursalMod = Convert.ToBoolean(reader["SucursalMod"].ToString());
						VtUsuarios.SucursalDel = Convert.ToBoolean(reader["SucursalDel"].ToString());
						VtUsuarios.EntraAlmacenes = Convert.ToBoolean(reader["EntraAlmacenes"].ToString());
						VtUsuarios.AlmacenAdd = Convert.ToBoolean(reader["AlmacenAdd"].ToString());
						VtUsuarios.AlmacenMod = Convert.ToBoolean(reader["AlmacenMod"].ToString());
						VtUsuarios.AlmacenDel = Convert.ToBoolean(reader["AlmacenDel"].ToString());
						VtUsuarios.EntraDepto = Convert.ToBoolean(reader["EntraDepto"].ToString());
						VtUsuarios.DeptoAdd = Convert.ToBoolean(reader["DeptoAdd"].ToString());
						VtUsuarios.DeptoMod = Convert.ToBoolean(reader["DeptoMod"].ToString());
						VtUsuarios.DeptoDel = Convert.ToBoolean(reader["DeptoDel"].ToString());
						VtUsuarios.EntraPaises = Convert.ToBoolean(reader["EntraPaises"].ToString());
						VtUsuarios.PaisAdd = Convert.ToBoolean(reader["PaisAdd"].ToString());
						VtUsuarios.PaisMod = Convert.ToBoolean(reader["PaisMod"].ToString());
						VtUsuarios.PaisDel = Convert.ToBoolean(reader["PaisDel"].ToString());
						VtUsuarios.EntraEstados = Convert.ToBoolean(reader["EntraEstados"].ToString());
						VtUsuarios.EstadoAdd = Convert.ToBoolean(reader["EstadoAdd"].ToString());
						VtUsuarios.EstadoMod = Convert.ToBoolean(reader["EstadoMod"].ToString());
						VtUsuarios.EstadoDel = Convert.ToBoolean(reader["EstadoDel"].ToString());
						VtUsuarios.EntraCiudades = Convert.ToBoolean(reader["EntraCiudades"].ToString());
						VtUsuarios.CiudadAdd = Convert.ToBoolean(reader["CiudadAdd"].ToString());
						VtUsuarios.CiudadMod = Convert.ToBoolean(reader["CiudadMod"].ToString());
						VtUsuarios.CiudadDel = Convert.ToBoolean(reader["CiudadDel"].ToString());
						VtUsuarios.EntraColonias = Convert.ToBoolean(reader["EntraColonias"].ToString());
						VtUsuarios.ColoniaAdd = Convert.ToBoolean(reader["ColoniaAdd"].ToString());
						VtUsuarios.ColoniaMod = Convert.ToBoolean(reader["ColoniaMod"].ToString());
						VtUsuarios.ColoniaDel = Convert.ToBoolean(reader["ColoniaDel"].ToString());
						VtUsuarios.WatiConsAdd = Convert.ToBoolean(reader["WatiConsAdd"].ToString());
						VtUsuarios.WatiConsMod = Convert.ToBoolean(reader["WatiConsMod"].ToString());
						VtUsuarios.WatiConsDel = Convert.ToBoolean(reader["WatiConsDel"].ToString());
						VtUsuarios.WatiPlanAdd = Convert.ToBoolean(reader["WatiPlanAdd"].ToString());
						VtUsuarios.WatiPlanMod = Convert.ToBoolean(reader["WatiPlanMod"].ToString());
						VtUsuarios.WatiPlanDel = Convert.ToBoolean(reader["WatiPlanDel"].ToString());
						VtUsuarios.EntraMovInv = Convert.ToBoolean(reader["EntraMovInv"].ToString());
						VtUsuarios.EntraConta = Convert.ToBoolean(reader["EntraConta"].ToString());
						VtUsuarios.ContaAdd = Convert.ToBoolean(reader["ContaAdd"].ToString());
						VtUsuarios.ContaMod = Convert.ToBoolean(reader["ContaMod"].ToString());
						VtUsuarios.ContaCan = Convert.ToBoolean(reader["ContaCan"].ToString());
						VtUsuarios.ContaDel = Convert.ToBoolean(reader["ContaDel"].ToString());
						VtUsuarios.EntraCatConta = Convert.ToBoolean(reader["EntraCatConta"].ToString());
						VtUsuarios.CatContaAdd = Convert.ToBoolean(reader["CatContaAdd"].ToString());
						VtUsuarios.CatContaMod = Convert.ToBoolean(reader["CatContaMod"].ToString());
						VtUsuarios.CatContaDel = Convert.ToBoolean(reader["CatContaDel"].ToString());
						VtUsuarios.EntraFormPago = Convert.ToBoolean(reader["EntraFormPago"].ToString());
						VtUsuarios.FPagoAdd = Convert.ToBoolean(reader["FPagoAdd"].ToString());
						VtUsuarios.FPagoMod = Convert.ToBoolean(reader["FPagoMod"].ToString());
						VtUsuarios.FPagoDel = Convert.ToBoolean(reader["FPagoDel"].ToString());
						VtUsuarios.EntraMetPago = Convert.ToBoolean(reader["EntraMetPago"].ToString());
						VtUsuarios.MetPagoAdd = Convert.ToBoolean(reader["MetPagoAdd"].ToString());
						VtUsuarios.MetPagoMod = Convert.ToBoolean(reader["MetPagoMod"].ToString());
						VtUsuarios.MetPagoDel = Convert.ToBoolean(reader["MetPagoDel"].ToString());
						VtUsuarios.EntraImp = Convert.ToBoolean(reader["EntraImp"].ToString());
						VtUsuarios.ImpAdd = Convert.ToBoolean(reader["ImpAdd"].ToString());
						VtUsuarios.ImpMod = Convert.ToBoolean(reader["ImpMod"].ToString());
						VtUsuarios.ImpDel = Convert.ToBoolean(reader["ImpDel"].ToString());
						VtUsuarios.EntraRFiscal = Convert.ToBoolean(reader["EntraRFiscal"].ToString());
						VtUsuarios.RfiscalAdd = Convert.ToBoolean(reader["RfiscalAdd"].ToString());
						VtUsuarios.RfiscalMod = Convert.ToBoolean(reader["RfiscalMod"].ToString());
						VtUsuarios.RfiscalDel = Convert.ToBoolean(reader["RfiscalDel"].ToString());
						VtUsuarios.EntraRepConta = Convert.ToBoolean(reader["EntraRepConta"].ToString());
						VtUsuarios.EntraClientes = Convert.ToBoolean(reader["EntraClientes"].ToString());
						VtUsuarios.CteAdd = Convert.ToBoolean(reader["CteAdd"].ToString());
						VtUsuarios.CteMod = Convert.ToBoolean(reader["CteMod"].ToString());
						VtUsuarios.CteDel = Convert.ToBoolean(reader["CteDel"].ToString());
						VtUsuarios.EntraProv = Convert.ToBoolean(reader["EntraProv"].ToString());
						VtUsuarios.ProvAdd = Convert.ToBoolean(reader["ProvAdd"].ToString());
						VtUsuarios.ProvMod = Convert.ToBoolean(reader["ProvMod"].ToString());
						VtUsuarios.ProvDel = Convert.ToBoolean(reader["ProvDel"].ToString());
						VtUsuarios.EntraCajas = Convert.ToBoolean(reader["EntraCajas"].ToString());
						VtUsuarios.CajaAdd = Convert.ToBoolean(reader["CajaAdd"].ToString());
						VtUsuarios.CajaMod = Convert.ToBoolean(reader["CajaMod"].ToString());
						VtUsuarios.CajaDel = Convert.ToBoolean(reader["CajaDel"].ToString());
						VtUsuarios.EntraTallas = Convert.ToBoolean(reader["EntraTallas"].ToString());
						VtUsuarios.TallaAdd = Convert.ToBoolean(reader["TallaAdd"].ToString());
						VtUsuarios.TallaMod = Convert.ToBoolean(reader["TallaMod"].ToString());
						VtUsuarios.TallaDel = Convert.ToBoolean(reader["TallaDel"].ToString());
						VtUsuarios.EntraFamilias = Convert.ToBoolean(reader["EntraFamilias"].ToString());
						VtUsuarios.FamiliaAdd = Convert.ToBoolean(reader["FamiliaAdd"].ToString());
						VtUsuarios.FamiliaMod = Convert.ToBoolean(reader["FamiliaMod"].ToString());
						VtUsuarios.FamiliaDel = Convert.ToBoolean(reader["FamiliaDel"].ToString());

						Lista.Add(VtUsuarios);
                    }
                }
            }
            catch (Exception ex)
            {
                var VtUsuarios = new VtUsuarios();
				VtUsuarios.IdUsu = ex.HResult;
				VtUsuarios.Usuario = "Error";
				VtUsuarios.Nombre = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(VtUsuarios);
            }
            return Lista;

        }
		public List<string> AutoCompletaUsuarios(string prefixText)
		{

			prefixText = "%" + prefixText + "%";

			var Lista = new List<string>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT top 10 Usuario  ";
			sql += " FROM dbo.Usuarios ";
			sql += " WHERE Estatus = 'Activo' ";
			sql += " and Usuario Like @Usuario ";
			sql += " order by Usuario";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Usuario", prefixText));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						string Usuarios;
						Usuarios = reader["Usuario"].ToString();
						Lista.Add(Usuarios);
					}
				}
			}
			catch (Exception ex)
			{
				string Usuarios;
				Usuarios = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(Usuarios);
			}
			return Lista;

		}

		public List<ModUsuarios> DdlUsuarios()
        {

            var Lista = new List<ModUsuarios>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT IdUsu, Nombre, Usuario  ";
            sql += " FROM dbo.Usuarios ";
            sql += " WHERE Estatus = 'Activo' ";
            //sql += " union Select 0 as IdUsu, ' Seleccione una Opción' as Nombre, ' Seleccione Una Opción' as Usuario";
            sql += " order by Usuario";

            listaParams.Clear();
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Usuarios = new ModUsuarios();
                        Usuarios.IdUsu = Convert.ToInt32(reader["IdUsu"].ToString());
                        Usuarios.Usuario = reader["Usuario"].ToString();
                        Usuarios.Nombre = reader["Nombre"].ToString();
                        Lista.Add(Usuarios);
                    }
                }
            }
            catch (Exception ex)
            {
                var Usuarios = new ModUsuarios();
                Usuarios.IdUsu = ex.HResult;
                Usuarios.Usuario = "Error";
                Usuarios.Nombre = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(Usuarios);
            }
            return Lista;

        }
        public VtUsuarios Usuario(int usuario)
        {
            var VtUsuarios = new VtUsuarios();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtUsuarios ";
            sql += " WHERE Estatus = 'Activo' and IdUsu = @IdUsu order by Usuario";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdUsu", usuario));

            try
            {
                using (SqlDataReader reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
						VtUsuarios.IdUsu = Convert.ToInt32(reader["IdUsu"].ToString());
						VtUsuarios.Usuario = reader["Usuario"].ToString();
						VtUsuarios.Nombre = reader["Nombre"].ToString();
						VtUsuarios.PWD = reader["PWD"].ToString();
						VtUsuarios.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
						VtUsuarios.Departamento = reader["Departamento"].ToString();
						VtUsuarios.Email = reader["Email"].ToString();
						VtUsuarios.Telefono = reader["Telefono"].ToString();
						VtUsuarios.Estatus = reader["Estatus"].ToString();
						VtUsuarios.IdPerfil = Convert.ToInt32(reader["IdPerfil"].ToString());
						VtUsuarios.Perfil = reader["Perfil"].ToString();
						VtUsuarios.EntraCxc = Convert.ToBoolean(reader["EntraCxc"].ToString());
						VtUsuarios.CxcAdd = Convert.ToBoolean(reader["CxcAdd"].ToString());
						VtUsuarios.CxcMod = Convert.ToBoolean(reader["CxcMod"].ToString());
						VtUsuarios.CxcCan = Convert.ToBoolean(reader["CxcCan"].ToString());
						VtUsuarios.CxcDel = Convert.ToBoolean(reader["CxcDel"].ToString());
						VtUsuarios.EntraVentas = Convert.ToBoolean(reader["EntraVentas"].ToString());
						VtUsuarios.VentaAdd = Convert.ToBoolean(reader["VentaAdd"].ToString());
						VtUsuarios.VentaMod = Convert.ToBoolean(reader["VentaMod"].ToString());
						VtUsuarios.VentaCan = Convert.ToBoolean(reader["VentaCan"].ToString());
						VtUsuarios.VentaDel = Convert.ToBoolean(reader["VentaDel"].ToString());
						VtUsuarios.EntraInv = Convert.ToBoolean(reader["EntraInv"].ToString());
						VtUsuarios.InvAdd = Convert.ToBoolean(reader["InvAdd"].ToString());
						VtUsuarios.InvMod = Convert.ToBoolean(reader["InvMod"].ToString());
						VtUsuarios.InvCan = Convert.ToBoolean(reader["InvCan"].ToString());
						VtUsuarios.InvDel = Convert.ToBoolean(reader["InvDel"].ToString());
						VtUsuarios.EntraArticulos = Convert.ToBoolean(reader["EntraArticulos"].ToString());
						VtUsuarios.ArticuloAdd = Convert.ToBoolean(reader["ArticuloAdd"].ToString());
						VtUsuarios.ArticuloMod = Convert.ToBoolean(reader["ArticuloMod"].ToString());
						VtUsuarios.ArticuloDel = Convert.ToBoolean(reader["ArticuloDel"].ToString());
						VtUsuarios.EntraLineas = Convert.ToBoolean(reader["EntraLineas"].ToString());
						VtUsuarios.LineasAdd = Convert.ToBoolean(reader["LineasAdd"].ToString());
						VtUsuarios.LineasMod = Convert.ToBoolean(reader["LineasMod"].ToString());
						VtUsuarios.LineasDel = Convert.ToBoolean(reader["LineasDel"].ToString());
						VtUsuarios.EntraCategoria = Convert.ToBoolean(reader["EntraCategoria"].ToString());
						VtUsuarios.CatAdd = Convert.ToBoolean(reader["CatAdd"].ToString());
						VtUsuarios.CatMod = Convert.ToBoolean(reader["CatMod"].ToString());
						VtUsuarios.CatDel = Convert.ToBoolean(reader["CatDel"].ToString());
						VtUsuarios.EntraMarcas = Convert.ToBoolean(reader["EntraMarcas"].ToString());
						VtUsuarios.MarcasAdd = Convert.ToBoolean(reader["MarcasAdd"].ToString());
						VtUsuarios.MarcasMod = Convert.ToBoolean(reader["MarcasMod"].ToString());
						VtUsuarios.MarcasDel = Convert.ToBoolean(reader["MarcasDel"].ToString());
						VtUsuarios.EntraModelos = Convert.ToBoolean(reader["EntraModelos"].ToString());
						VtUsuarios.ModelosAdd = Convert.ToBoolean(reader["ModelosAdd"].ToString());
						VtUsuarios.ModelosMod = Convert.ToBoolean(reader["ModelosMod"].ToString());
						VtUsuarios.ModelosDel = Convert.ToBoolean(reader["ModelosDel"].ToString());
						VtUsuarios.EntraColores = Convert.ToBoolean(reader["EntraColores"].ToString());
						VtUsuarios.ColoresAdd = Convert.ToBoolean(reader["ColoresAdd"].ToString());
						VtUsuarios.ColoresMod = Convert.ToBoolean(reader["ColoresMod"].ToString());
						VtUsuarios.ColoresDel = Convert.ToBoolean(reader["ColoresDel"].ToString());
						VtUsuarios.EntraUnidad = Convert.ToBoolean(reader["EntraUnidad"].ToString());
						VtUsuarios.UnidadAdd = Convert.ToBoolean(reader["UnidadAdd"].ToString());
						VtUsuarios.UnidadMod = Convert.ToBoolean(reader["UnidadMod"].ToString());
						VtUsuarios.UnidadDel = Convert.ToBoolean(reader["UnidadDel"].ToString());
						VtUsuarios.EntraCompras = Convert.ToBoolean(reader["EntraCompras"].ToString());
						VtUsuarios.CompraAdd = Convert.ToBoolean(reader["CompraAdd"].ToString());
						VtUsuarios.CompraMod = Convert.ToBoolean(reader["CompraMod"].ToString());
						VtUsuarios.CompraCan = Convert.ToBoolean(reader["CompraCan"].ToString());
						VtUsuarios.CompraDel = Convert.ToBoolean(reader["CompraDel"].ToString());
						VtUsuarios.EntraCxp = Convert.ToBoolean(reader["EntraCxp"].ToString());
						VtUsuarios.CxpAdd = Convert.ToBoolean(reader["CxpAdd"].ToString());
						VtUsuarios.CxpMod = Convert.ToBoolean(reader["CxpMod"].ToString());
						VtUsuarios.CxpCan = Convert.ToBoolean(reader["CxpCan"].ToString());
						VtUsuarios.CxpDel = Convert.ToBoolean(reader["CxpDel"].ToString());
						VtUsuarios.EntraReportes = Convert.ToBoolean(reader["EntraReportes"].ToString());
						VtUsuarios.EntraRepCxc = Convert.ToBoolean(reader["EntraRepCxc"].ToString());
						VtUsuarios.EntraRepVen = Convert.ToBoolean(reader["EntraRepVen"].ToString());
						VtUsuarios.EntraRepInv = Convert.ToBoolean(reader["EntraRepInv"].ToString());
						VtUsuarios.EntraRepCom = Convert.ToBoolean(reader["EntraRepCom"].ToString());
						VtUsuarios.EntraRepCxp = Convert.ToBoolean(reader["EntraRepCxp"].ToString());
						VtUsuarios.EntraWati = Convert.ToBoolean(reader["EntraWati"].ToString());
						VtUsuarios.EntraWatiEnv = Convert.ToBoolean(reader["EntraWatiEnv"].ToString());
						VtUsuarios.EntraWatiCons = Convert.ToBoolean(reader["EntraWatiCons"].ToString());
						VtUsuarios.EntraWatiPlan = Convert.ToBoolean(reader["EntraWatiPlan"].ToString());
						VtUsuarios.EntraWatiAuto = Convert.ToBoolean(reader["EntraWatiAuto"].ToString());
						VtUsuarios.WatiAdd = Convert.ToBoolean(reader["WatiAdd"].ToString());
						VtUsuarios.WatiDel = Convert.ToBoolean(reader["WatiDel"].ToString());
						VtUsuarios.WatiMod = Convert.ToBoolean(reader["WatiMod"].ToString());
						VtUsuarios.EntraConfiguraciones = Convert.ToBoolean(reader["EntraConfiguraciones"].ToString());
						VtUsuarios.EntraEmpresa = Convert.ToBoolean(reader["EntraEmpresa"].ToString());
						VtUsuarios.EmpresaMod = Convert.ToBoolean(reader["EmpresaMod"].ToString());
						VtUsuarios.EntraSistema = Convert.ToBoolean(reader["EntraSistema"].ToString());
						VtUsuarios.EntraModulos = Convert.ToBoolean(reader["EntraModulos"].ToString());
						VtUsuarios.ModAdd = Convert.ToBoolean(reader["ModAdd"].ToString());
						VtUsuarios.ModMod = Convert.ToBoolean(reader["ModMod"].ToString());
						VtUsuarios.ModDel = Convert.ToBoolean(reader["ModDel"].ToString());
						VtUsuarios.EntraMovtos = Convert.ToBoolean(reader["EntraMovtos"].ToString());
						VtUsuarios.MovtosAdd = Convert.ToBoolean(reader["MovtosAdd"].ToString());
						VtUsuarios.MovtosMod = Convert.ToBoolean(reader["MovtosMod"].ToString());
						VtUsuarios.MovtosDel = Convert.ToBoolean(reader["MovtosDel"].ToString());
						VtUsuarios.EntraFolios = Convert.ToBoolean(reader["EntraFolios"].ToString());
						VtUsuarios.FolioAdd = Convert.ToBoolean(reader["FolioAdd"].ToString());
						VtUsuarios.FolioMod = Convert.ToBoolean(reader["FolioMod"].ToString());
						VtUsuarios.FolioDel = Convert.ToBoolean(reader["FolioDel"].ToString());
						VtUsuarios.EntraUsuarios = Convert.ToBoolean(reader["EntraUsuarios"].ToString());
						VtUsuarios.UsuarioAdd = Convert.ToBoolean(reader["UsuarioAdd"].ToString());
						VtUsuarios.UsuarioMod = Convert.ToBoolean(reader["UsuarioMod"].ToString());
						VtUsuarios.UsuarioDel = Convert.ToBoolean(reader["UsuarioDel"].ToString());
						VtUsuarios.EntraPerfiles = Convert.ToBoolean(reader["EntraPerfiles"].ToString());
						VtUsuarios.PerfilAdd = Convert.ToBoolean(reader["PerfilAdd"].ToString());
						VtUsuarios.PerfilMod = Convert.ToBoolean(reader["PerfilMod"].ToString());
						VtUsuarios.PerfilDel = Convert.ToBoolean(reader["PerfilDel"].ToString());
						VtUsuarios.EntraSucursales = Convert.ToBoolean(reader["EntraSucursales"].ToString());
						VtUsuarios.SucursalAdd = Convert.ToBoolean(reader["SucursalAdd"].ToString());
						VtUsuarios.SucursalMod = Convert.ToBoolean(reader["SucursalMod"].ToString());
						VtUsuarios.SucursalDel = Convert.ToBoolean(reader["SucursalDel"].ToString());
						VtUsuarios.EntraAlmacenes = Convert.ToBoolean(reader["EntraAlmacenes"].ToString());
						VtUsuarios.AlmacenAdd = Convert.ToBoolean(reader["AlmacenAdd"].ToString());
						VtUsuarios.AlmacenMod = Convert.ToBoolean(reader["AlmacenMod"].ToString());
						VtUsuarios.AlmacenDel = Convert.ToBoolean(reader["AlmacenDel"].ToString());
						VtUsuarios.EntraDepto = Convert.ToBoolean(reader["EntraDepto"].ToString());
						VtUsuarios.DeptoAdd = Convert.ToBoolean(reader["DeptoAdd"].ToString());
						VtUsuarios.DeptoMod = Convert.ToBoolean(reader["DeptoMod"].ToString());
						VtUsuarios.DeptoDel = Convert.ToBoolean(reader["DeptoDel"].ToString());
						VtUsuarios.EntraPaises = Convert.ToBoolean(reader["EntraPaises"].ToString());
						VtUsuarios.PaisAdd = Convert.ToBoolean(reader["PaisAdd"].ToString());
						VtUsuarios.PaisMod = Convert.ToBoolean(reader["PaisMod"].ToString());
						VtUsuarios.PaisDel = Convert.ToBoolean(reader["PaisDel"].ToString());
						VtUsuarios.EntraEstados = Convert.ToBoolean(reader["EntraEstados"].ToString());
						VtUsuarios.EstadoAdd = Convert.ToBoolean(reader["EstadoAdd"].ToString());
						VtUsuarios.EstadoMod = Convert.ToBoolean(reader["EstadoMod"].ToString());
						VtUsuarios.EstadoDel = Convert.ToBoolean(reader["EstadoDel"].ToString());
						VtUsuarios.EntraCiudades = Convert.ToBoolean(reader["EntraCiudades"].ToString());
						VtUsuarios.CiudadAdd = Convert.ToBoolean(reader["CiudadAdd"].ToString());
						VtUsuarios.CiudadMod = Convert.ToBoolean(reader["CiudadMod"].ToString());
						VtUsuarios.CiudadDel = Convert.ToBoolean(reader["CiudadDel"].ToString());
						VtUsuarios.EntraColonias = Convert.ToBoolean(reader["EntraColonias"].ToString());
						VtUsuarios.ColoniaAdd = Convert.ToBoolean(reader["ColoniaAdd"].ToString());
						VtUsuarios.ColoniaMod = Convert.ToBoolean(reader["ColoniaMod"].ToString());
						VtUsuarios.ColoniaDel = Convert.ToBoolean(reader["ColoniaDel"].ToString());
						VtUsuarios.WatiConsAdd = Convert.ToBoolean(reader["WatiConsAdd"].ToString());
						VtUsuarios.WatiConsMod = Convert.ToBoolean(reader["WatiConsMod"].ToString());
						VtUsuarios.WatiConsDel = Convert.ToBoolean(reader["WatiConsDel"].ToString());
						VtUsuarios.WatiPlanAdd = Convert.ToBoolean(reader["WatiPlanAdd"].ToString());
						VtUsuarios.WatiPlanMod = Convert.ToBoolean(reader["WatiPlanMod"].ToString());
						VtUsuarios.WatiPlanDel = Convert.ToBoolean(reader["WatiPlanDel"].ToString());
						VtUsuarios.EntraMovInv = Convert.ToBoolean(reader["EntraMovInv"].ToString());
						VtUsuarios.EntraConta = Convert.ToBoolean(reader["EntraConta"].ToString());
						VtUsuarios.ContaAdd = Convert.ToBoolean(reader["ContaAdd"].ToString());
						VtUsuarios.ContaMod = Convert.ToBoolean(reader["ContaMod"].ToString());
						VtUsuarios.ContaCan = Convert.ToBoolean(reader["ContaCan"].ToString());
						VtUsuarios.ContaDel = Convert.ToBoolean(reader["ContaDel"].ToString());
						VtUsuarios.EntraCatConta = Convert.ToBoolean(reader["EntraCatConta"].ToString());
						VtUsuarios.CatContaAdd = Convert.ToBoolean(reader["CatContaAdd"].ToString());
						VtUsuarios.CatContaMod = Convert.ToBoolean(reader["CatContaMod"].ToString());
						VtUsuarios.CatContaDel = Convert.ToBoolean(reader["CatContaDel"].ToString());
						VtUsuarios.EntraFormPago = Convert.ToBoolean(reader["EntraFormPago"].ToString());
						VtUsuarios.FPagoAdd = Convert.ToBoolean(reader["FPagoAdd"].ToString());
						VtUsuarios.FPagoMod = Convert.ToBoolean(reader["FPagoMod"].ToString());
						VtUsuarios.FPagoDel = Convert.ToBoolean(reader["FPagoDel"].ToString());
						VtUsuarios.EntraMetPago = Convert.ToBoolean(reader["EntraMetPago"].ToString());
						VtUsuarios.MetPagoAdd = Convert.ToBoolean(reader["MetPagoAdd"].ToString());
						VtUsuarios.MetPagoMod = Convert.ToBoolean(reader["MetPagoMod"].ToString());
						VtUsuarios.MetPagoDel = Convert.ToBoolean(reader["MetPagoDel"].ToString());
						VtUsuarios.EntraImp = Convert.ToBoolean(reader["EntraImp"].ToString());
						VtUsuarios.ImpAdd = Convert.ToBoolean(reader["ImpAdd"].ToString());
						VtUsuarios.ImpMod = Convert.ToBoolean(reader["ImpMod"].ToString());
						VtUsuarios.ImpDel = Convert.ToBoolean(reader["ImpDel"].ToString());
						VtUsuarios.EntraRFiscal = Convert.ToBoolean(reader["EntraRFiscal"].ToString());
						VtUsuarios.RfiscalAdd = Convert.ToBoolean(reader["RfiscalAdd"].ToString());
						VtUsuarios.RfiscalMod = Convert.ToBoolean(reader["RfiscalMod"].ToString());
						VtUsuarios.RfiscalDel = Convert.ToBoolean(reader["RfiscalDel"].ToString());
						VtUsuarios.EntraRepConta = Convert.ToBoolean(reader["EntraRepConta"].ToString());
						VtUsuarios.EntraClientes = Convert.ToBoolean(reader["EntraClientes"].ToString());
						VtUsuarios.CteAdd = Convert.ToBoolean(reader["CteAdd"].ToString());
						VtUsuarios.CteMod = Convert.ToBoolean(reader["CteMod"].ToString());
						VtUsuarios.CteDel = Convert.ToBoolean(reader["CteDel"].ToString());
						VtUsuarios.EntraProv = Convert.ToBoolean(reader["EntraProv"].ToString());
						VtUsuarios.ProvAdd = Convert.ToBoolean(reader["ProvAdd"].ToString());
						VtUsuarios.ProvMod = Convert.ToBoolean(reader["ProvMod"].ToString());
						VtUsuarios.ProvDel = Convert.ToBoolean(reader["ProvDel"].ToString());
						VtUsuarios.EntraCajas = Convert.ToBoolean(reader["EntraCajas"].ToString());
						VtUsuarios.CajaAdd = Convert.ToBoolean(reader["CajaAdd"].ToString());
						VtUsuarios.CajaMod = Convert.ToBoolean(reader["CajaMod"].ToString());
						VtUsuarios.CajaDel = Convert.ToBoolean(reader["CajaDel"].ToString());
						VtUsuarios.EntraTallas = Convert.ToBoolean(reader["EntraTallas"].ToString());
						VtUsuarios.TallaAdd = Convert.ToBoolean(reader["TallaAdd"].ToString());
						VtUsuarios.TallaMod = Convert.ToBoolean(reader["TallaMod"].ToString());
						VtUsuarios.TallaDel = Convert.ToBoolean(reader["TallaDel"].ToString());
						VtUsuarios.EntraFamilias = Convert.ToBoolean(reader["EntraFamilias"].ToString());
						VtUsuarios.FamiliaAdd = Convert.ToBoolean(reader["FamiliaAdd"].ToString());
						VtUsuarios.FamiliaMod = Convert.ToBoolean(reader["FamiliaMod"].ToString());
						VtUsuarios.FamiliaDel = Convert.ToBoolean(reader["FamiliaDel"].ToString());
					}
				}
            }
            catch (Exception ex)
            {
                VtUsuarios.IdUsu = ex.HResult;
                VtUsuarios.Usuario = "Error";
                VtUsuarios.Nombre = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return VtUsuarios;

        }

        public VtUsuarios VtUsuario(string Usuario)
        {
            var VtUsuarios = new VtUsuarios();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtUsuarios ";
            sql += " WHERE Usuario = @Usuario and  Estatus = 'Activo' ";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Usuario", Usuario));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    reader.Read();
                    if (reader.HasRows)
                    {
						VtUsuarios.IdUsu = Convert.ToInt32(reader["IdUsu"].ToString());
						VtUsuarios.Usuario = reader["Usuario"].ToString();
						VtUsuarios.Nombre = reader["Nombre"].ToString();
						VtUsuarios.PWD = reader["PWD"].ToString();
						VtUsuarios.IdDepto = Convert.ToInt32(reader["IdDepto"].ToString());
						VtUsuarios.Departamento = reader["Departamento"].ToString();
						VtUsuarios.Email = reader["Email"].ToString();
						VtUsuarios.Telefono = reader["Telefono"].ToString();
						VtUsuarios.Estatus = reader["Estatus"].ToString();
						VtUsuarios.IdPerfil = Convert.ToInt32(reader["IdPerfil"].ToString());
						VtUsuarios.Perfil = reader["Perfil"].ToString();
						VtUsuarios.EntraCxc = Convert.ToBoolean(reader["EntraCxc"].ToString());
						VtUsuarios.CxcAdd = Convert.ToBoolean(reader["CxcAdd"].ToString());
						VtUsuarios.CxcMod = Convert.ToBoolean(reader["CxcMod"].ToString());
						VtUsuarios.CxcCan = Convert.ToBoolean(reader["CxcCan"].ToString());
						VtUsuarios.CxcDel = Convert.ToBoolean(reader["CxcDel"].ToString());
						VtUsuarios.EntraVentas = Convert.ToBoolean(reader["EntraVentas"].ToString());
						VtUsuarios.VentaAdd = Convert.ToBoolean(reader["VentaAdd"].ToString());
						VtUsuarios.VentaMod = Convert.ToBoolean(reader["VentaMod"].ToString());
						VtUsuarios.VentaCan = Convert.ToBoolean(reader["VentaCan"].ToString());
						VtUsuarios.VentaDel = Convert.ToBoolean(reader["VentaDel"].ToString());
						VtUsuarios.EntraInv = Convert.ToBoolean(reader["EntraInv"].ToString());
						VtUsuarios.InvAdd = Convert.ToBoolean(reader["InvAdd"].ToString());
						VtUsuarios.InvMod = Convert.ToBoolean(reader["InvMod"].ToString());
						VtUsuarios.InvCan = Convert.ToBoolean(reader["InvCan"].ToString());
						VtUsuarios.InvDel = Convert.ToBoolean(reader["InvDel"].ToString());
						VtUsuarios.EntraArticulos = Convert.ToBoolean(reader["EntraArticulos"].ToString());
						VtUsuarios.ArticuloAdd = Convert.ToBoolean(reader["ArticuloAdd"].ToString());
						VtUsuarios.ArticuloMod = Convert.ToBoolean(reader["ArticuloMod"].ToString());
						VtUsuarios.ArticuloDel = Convert.ToBoolean(reader["ArticuloDel"].ToString());
						VtUsuarios.EntraLineas = Convert.ToBoolean(reader["EntraLineas"].ToString());
						VtUsuarios.LineasAdd = Convert.ToBoolean(reader["LineasAdd"].ToString());
						VtUsuarios.LineasMod = Convert.ToBoolean(reader["LineasMod"].ToString());
						VtUsuarios.LineasDel = Convert.ToBoolean(reader["LineasDel"].ToString());
						VtUsuarios.EntraCategoria = Convert.ToBoolean(reader["EntraCategoria"].ToString());
						VtUsuarios.CatAdd = Convert.ToBoolean(reader["CatAdd"].ToString());
						VtUsuarios.CatMod = Convert.ToBoolean(reader["CatMod"].ToString());
						VtUsuarios.CatDel = Convert.ToBoolean(reader["CatDel"].ToString());
						VtUsuarios.EntraMarcas = Convert.ToBoolean(reader["EntraMarcas"].ToString());
						VtUsuarios.MarcasAdd = Convert.ToBoolean(reader["MarcasAdd"].ToString());
						VtUsuarios.MarcasMod = Convert.ToBoolean(reader["MarcasMod"].ToString());
						VtUsuarios.MarcasDel = Convert.ToBoolean(reader["MarcasDel"].ToString());
						VtUsuarios.EntraModelos = Convert.ToBoolean(reader["EntraModelos"].ToString());
						VtUsuarios.ModelosAdd = Convert.ToBoolean(reader["ModelosAdd"].ToString());
						VtUsuarios.ModelosMod = Convert.ToBoolean(reader["ModelosMod"].ToString());
						VtUsuarios.ModelosDel = Convert.ToBoolean(reader["ModelosDel"].ToString());
						VtUsuarios.EntraColores = Convert.ToBoolean(reader["EntraColores"].ToString());
						VtUsuarios.ColoresAdd = Convert.ToBoolean(reader["ColoresAdd"].ToString());
						VtUsuarios.ColoresMod = Convert.ToBoolean(reader["ColoresMod"].ToString());
						VtUsuarios.ColoresDel = Convert.ToBoolean(reader["ColoresDel"].ToString());
						VtUsuarios.EntraUnidad = Convert.ToBoolean(reader["EntraUnidad"].ToString());
						VtUsuarios.UnidadAdd = Convert.ToBoolean(reader["UnidadAdd"].ToString());
						VtUsuarios.UnidadMod = Convert.ToBoolean(reader["UnidadMod"].ToString());
						VtUsuarios.UnidadDel = Convert.ToBoolean(reader["UnidadDel"].ToString());
						VtUsuarios.EntraCompras = Convert.ToBoolean(reader["EntraCompras"].ToString());
						VtUsuarios.CompraAdd = Convert.ToBoolean(reader["CompraAdd"].ToString());
						VtUsuarios.CompraMod = Convert.ToBoolean(reader["CompraMod"].ToString());
						VtUsuarios.CompraCan = Convert.ToBoolean(reader["CompraCan"].ToString());
						VtUsuarios.CompraDel = Convert.ToBoolean(reader["CompraDel"].ToString());
						VtUsuarios.EntraCxp = Convert.ToBoolean(reader["EntraCxp"].ToString());
						VtUsuarios.CxpAdd = Convert.ToBoolean(reader["CxpAdd"].ToString());
						VtUsuarios.CxpMod = Convert.ToBoolean(reader["CxpMod"].ToString());
						VtUsuarios.CxpCan = Convert.ToBoolean(reader["CxpCan"].ToString());
						VtUsuarios.CxpDel = Convert.ToBoolean(reader["CxpDel"].ToString());
						VtUsuarios.EntraReportes = Convert.ToBoolean(reader["EntraReportes"].ToString());
						VtUsuarios.EntraRepCxc = Convert.ToBoolean(reader["EntraRepCxc"].ToString());
						VtUsuarios.EntraRepVen = Convert.ToBoolean(reader["EntraRepVen"].ToString());
						VtUsuarios.EntraRepInv = Convert.ToBoolean(reader["EntraRepInv"].ToString());
						VtUsuarios.EntraRepCom = Convert.ToBoolean(reader["EntraRepCom"].ToString());
						VtUsuarios.EntraRepCxp = Convert.ToBoolean(reader["EntraRepCxp"].ToString());
						VtUsuarios.EntraWati = Convert.ToBoolean(reader["EntraWati"].ToString());
						VtUsuarios.EntraWatiEnv = Convert.ToBoolean(reader["EntraWatiEnv"].ToString());
						VtUsuarios.EntraWatiCons = Convert.ToBoolean(reader["EntraWatiCons"].ToString());
						VtUsuarios.EntraWatiPlan = Convert.ToBoolean(reader["EntraWatiPlan"].ToString());
						VtUsuarios.EntraWatiAuto = Convert.ToBoolean(reader["EntraWatiAuto"].ToString());
						VtUsuarios.WatiAdd = Convert.ToBoolean(reader["WatiAdd"].ToString());
						VtUsuarios.WatiDel = Convert.ToBoolean(reader["WatiDel"].ToString());
						VtUsuarios.WatiMod = Convert.ToBoolean(reader["WatiMod"].ToString());
						VtUsuarios.EntraConfiguraciones = Convert.ToBoolean(reader["EntraConfiguraciones"].ToString());
						VtUsuarios.EntraEmpresa = Convert.ToBoolean(reader["EntraEmpresa"].ToString());
						VtUsuarios.EmpresaMod = Convert.ToBoolean(reader["EmpresaMod"].ToString());
						VtUsuarios.EntraSistema = Convert.ToBoolean(reader["EntraSistema"].ToString());
						VtUsuarios.EntraModulos = Convert.ToBoolean(reader["EntraModulos"].ToString());
						VtUsuarios.ModAdd = Convert.ToBoolean(reader["ModAdd"].ToString());
						VtUsuarios.ModMod = Convert.ToBoolean(reader["ModMod"].ToString());
						VtUsuarios.ModDel = Convert.ToBoolean(reader["ModDel"].ToString());
						VtUsuarios.EntraMovtos = Convert.ToBoolean(reader["EntraMovtos"].ToString());
						VtUsuarios.MovtosAdd = Convert.ToBoolean(reader["MovtosAdd"].ToString());
						VtUsuarios.MovtosMod = Convert.ToBoolean(reader["MovtosMod"].ToString());
						VtUsuarios.MovtosDel = Convert.ToBoolean(reader["MovtosDel"].ToString());
						VtUsuarios.EntraFolios = Convert.ToBoolean(reader["EntraFolios"].ToString());
						VtUsuarios.FolioAdd = Convert.ToBoolean(reader["FolioAdd"].ToString());
						VtUsuarios.FolioMod = Convert.ToBoolean(reader["FolioMod"].ToString());
						VtUsuarios.FolioDel = Convert.ToBoolean(reader["FolioDel"].ToString());
						VtUsuarios.EntraUsuarios = Convert.ToBoolean(reader["EntraUsuarios"].ToString());
						VtUsuarios.UsuarioAdd = Convert.ToBoolean(reader["UsuarioAdd"].ToString());
						VtUsuarios.UsuarioMod = Convert.ToBoolean(reader["UsuarioMod"].ToString());
						VtUsuarios.UsuarioDel = Convert.ToBoolean(reader["UsuarioDel"].ToString());
						VtUsuarios.EntraPerfiles = Convert.ToBoolean(reader["EntraPerfiles"].ToString());
						VtUsuarios.PerfilAdd = Convert.ToBoolean(reader["PerfilAdd"].ToString());
						VtUsuarios.PerfilMod = Convert.ToBoolean(reader["PerfilMod"].ToString());
						VtUsuarios.PerfilDel = Convert.ToBoolean(reader["PerfilDel"].ToString());
						VtUsuarios.EntraSucursales = Convert.ToBoolean(reader["EntraSucursales"].ToString());
						VtUsuarios.SucursalAdd = Convert.ToBoolean(reader["SucursalAdd"].ToString());
						VtUsuarios.SucursalMod = Convert.ToBoolean(reader["SucursalMod"].ToString());
						VtUsuarios.SucursalDel = Convert.ToBoolean(reader["SucursalDel"].ToString());
						VtUsuarios.EntraAlmacenes = Convert.ToBoolean(reader["EntraAlmacenes"].ToString());
						VtUsuarios.AlmacenAdd = Convert.ToBoolean(reader["AlmacenAdd"].ToString());
						VtUsuarios.AlmacenMod = Convert.ToBoolean(reader["AlmacenMod"].ToString());
						VtUsuarios.AlmacenDel = Convert.ToBoolean(reader["AlmacenDel"].ToString());
						VtUsuarios.EntraDepto = Convert.ToBoolean(reader["EntraDepto"].ToString());
						VtUsuarios.DeptoAdd = Convert.ToBoolean(reader["DeptoAdd"].ToString());
						VtUsuarios.DeptoMod = Convert.ToBoolean(reader["DeptoMod"].ToString());
						VtUsuarios.DeptoDel = Convert.ToBoolean(reader["DeptoDel"].ToString());
						VtUsuarios.EntraPaises = Convert.ToBoolean(reader["EntraPaises"].ToString());
						VtUsuarios.PaisAdd = Convert.ToBoolean(reader["PaisAdd"].ToString());
						VtUsuarios.PaisMod = Convert.ToBoolean(reader["PaisMod"].ToString());
						VtUsuarios.PaisDel = Convert.ToBoolean(reader["PaisDel"].ToString());
						VtUsuarios.EntraEstados = Convert.ToBoolean(reader["EntraEstados"].ToString());
						VtUsuarios.EstadoAdd = Convert.ToBoolean(reader["EstadoAdd"].ToString());
						VtUsuarios.EstadoMod = Convert.ToBoolean(reader["EstadoMod"].ToString());
						VtUsuarios.EstadoDel = Convert.ToBoolean(reader["EstadoDel"].ToString());
						VtUsuarios.EntraCiudades = Convert.ToBoolean(reader["EntraCiudades"].ToString());
						VtUsuarios.CiudadAdd = Convert.ToBoolean(reader["CiudadAdd"].ToString());
						VtUsuarios.CiudadMod = Convert.ToBoolean(reader["CiudadMod"].ToString());
						VtUsuarios.CiudadDel = Convert.ToBoolean(reader["CiudadDel"].ToString());
						VtUsuarios.EntraColonias = Convert.ToBoolean(reader["EntraColonias"].ToString());
						VtUsuarios.ColoniaAdd = Convert.ToBoolean(reader["ColoniaAdd"].ToString());
						VtUsuarios.ColoniaMod = Convert.ToBoolean(reader["ColoniaMod"].ToString());
						VtUsuarios.ColoniaDel = Convert.ToBoolean(reader["ColoniaDel"].ToString());
						VtUsuarios.WatiConsAdd = Convert.ToBoolean(reader["WatiConsAdd"].ToString());
						VtUsuarios.WatiConsMod = Convert.ToBoolean(reader["WatiConsMod"].ToString());
						VtUsuarios.WatiConsDel = Convert.ToBoolean(reader["WatiConsDel"].ToString());
						VtUsuarios.WatiPlanAdd = Convert.ToBoolean(reader["WatiPlanAdd"].ToString());
						VtUsuarios.WatiPlanMod = Convert.ToBoolean(reader["WatiPlanMod"].ToString());
						VtUsuarios.WatiPlanDel = Convert.ToBoolean(reader["WatiPlanDel"].ToString());
						VtUsuarios.EntraMovInv = Convert.ToBoolean(reader["EntraMovInv"].ToString());
						VtUsuarios.EntraConta = Convert.ToBoolean(reader["EntraConta"].ToString());
						VtUsuarios.ContaAdd = Convert.ToBoolean(reader["ContaAdd"].ToString());
						VtUsuarios.ContaMod = Convert.ToBoolean(reader["ContaMod"].ToString());
						VtUsuarios.ContaCan = Convert.ToBoolean(reader["ContaCan"].ToString());
						VtUsuarios.ContaDel = Convert.ToBoolean(reader["ContaDel"].ToString());
						VtUsuarios.EntraCatConta = Convert.ToBoolean(reader["EntraCatConta"].ToString());
						VtUsuarios.CatContaAdd = Convert.ToBoolean(reader["CatContaAdd"].ToString());
						VtUsuarios.CatContaMod = Convert.ToBoolean(reader["CatContaMod"].ToString());
						VtUsuarios.CatContaDel = Convert.ToBoolean(reader["CatContaDel"].ToString());
						VtUsuarios.EntraFormPago = Convert.ToBoolean(reader["EntraFormPago"].ToString());
						VtUsuarios.FPagoAdd = Convert.ToBoolean(reader["FPagoAdd"].ToString());
						VtUsuarios.FPagoMod = Convert.ToBoolean(reader["FPagoMod"].ToString());
						VtUsuarios.FPagoDel = Convert.ToBoolean(reader["FPagoDel"].ToString());
						VtUsuarios.EntraMetPago = Convert.ToBoolean(reader["EntraMetPago"].ToString());
						VtUsuarios.MetPagoAdd = Convert.ToBoolean(reader["MetPagoAdd"].ToString());
						VtUsuarios.MetPagoMod = Convert.ToBoolean(reader["MetPagoMod"].ToString());
						VtUsuarios.MetPagoDel = Convert.ToBoolean(reader["MetPagoDel"].ToString());
						VtUsuarios.EntraImp = Convert.ToBoolean(reader["EntraImp"].ToString());
						VtUsuarios.ImpAdd = Convert.ToBoolean(reader["ImpAdd"].ToString());
						VtUsuarios.ImpMod = Convert.ToBoolean(reader["ImpMod"].ToString());
						VtUsuarios.ImpDel = Convert.ToBoolean(reader["ImpDel"].ToString());
						VtUsuarios.EntraRFiscal = Convert.ToBoolean(reader["EntraRFiscal"].ToString());
						VtUsuarios.RfiscalAdd = Convert.ToBoolean(reader["RfiscalAdd"].ToString());
						VtUsuarios.RfiscalMod = Convert.ToBoolean(reader["RfiscalMod"].ToString());
						VtUsuarios.RfiscalDel = Convert.ToBoolean(reader["RfiscalDel"].ToString());
						VtUsuarios.EntraRepConta = Convert.ToBoolean(reader["EntraRepConta"].ToString());
						VtUsuarios.EntraClientes = Convert.ToBoolean(reader["EntraClientes"].ToString());
						VtUsuarios.CteAdd = Convert.ToBoolean(reader["CteAdd"].ToString());
						VtUsuarios.CteMod = Convert.ToBoolean(reader["CteMod"].ToString());
						VtUsuarios.CteDel = Convert.ToBoolean(reader["CteDel"].ToString());
						VtUsuarios.EntraProv = Convert.ToBoolean(reader["EntraProv"].ToString());
						VtUsuarios.ProvAdd = Convert.ToBoolean(reader["ProvAdd"].ToString());
						VtUsuarios.ProvMod = Convert.ToBoolean(reader["ProvMod"].ToString());
						VtUsuarios.ProvDel = Convert.ToBoolean(reader["ProvDel"].ToString());
						VtUsuarios.EntraCajas = Convert.ToBoolean(reader["EntraCajas"].ToString());
						VtUsuarios.CajaAdd = Convert.ToBoolean(reader["CajaAdd"].ToString());
						VtUsuarios.CajaMod = Convert.ToBoolean(reader["CajaMod"].ToString());
						VtUsuarios.CajaDel = Convert.ToBoolean(reader["CajaDel"].ToString());
						VtUsuarios.EntraTallas = Convert.ToBoolean(reader["EntraTallas"].ToString());
						VtUsuarios.TallaAdd = Convert.ToBoolean(reader["TallaAdd"].ToString());
						VtUsuarios.TallaMod = Convert.ToBoolean(reader["TallaMod"].ToString());
						VtUsuarios.TallaDel = Convert.ToBoolean(reader["TallaDel"].ToString());
						VtUsuarios.EntraFamilias = Convert.ToBoolean(reader["EntraFamilias"].ToString());
						VtUsuarios.FamiliaAdd = Convert.ToBoolean(reader["FamiliaAdd"].ToString());
						VtUsuarios.FamiliaMod = Convert.ToBoolean(reader["FamiliaMod"].ToString());
						VtUsuarios.FamiliaDel = Convert.ToBoolean(reader["FamiliaDel"].ToString());
					}
                }
            }
            catch (Exception ex)
            {
                VtUsuarios.IdUsu = ex.HResult;
                VtUsuarios.Usuario = "Error";
                VtUsuarios.Nombre = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return VtUsuarios;

        }
        public RespuestaSQL Acceso(string Usuario, string PWD)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpAcceso";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Usuario", Usuario));
            listaParams.Add(new SqlParameter("@Pwd", PWD));

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
                Respuesta.Mensaje = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL UsuarioSucAdd(UsuariosSuc modUsuariosSuc)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpUsuarioSucAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdUsu", modUsuariosSuc.IdUsu));
            listaParams.Add(new SqlParameter("@IdSuc", modUsuariosSuc.IdSuc));

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
                Respuesta.Mensaje = "Error al Guardar la Sucursal con el Usuario" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL UsuarioSucDel(UsuariosSuc modUsuariosSuc)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpUsuarioSucDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdUsuSuc", modUsuariosSuc.IdUsuSuc));

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
                Respuesta.Mensaje = "Error al Eliminar La Sucursal Del Usuario" + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
		public List<Perfiles> DdlPerfiles()
		{
			var Lista = new List<Perfiles>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT IdPerfil, Perfil ";
			sql += " FROM dbo.Perfiles ";
			sql += " union Select 0 as IdPerfil, ' Seleccione una Opcion' as Perfil ";
			sql += " order by Perfil";

			listaParams.Clear();
			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var Perfiles = new Perfiles();
						Perfiles.IdPerfil = Convert.ToInt32(reader["IdPerfil"].ToString());
						Perfiles.Perfil = reader["Perfil"].ToString();
                        Lista.Add(Perfiles);
					}
				}
			}
			catch (Exception ex)
			{
				var Perfiles = new Perfiles();
				Perfiles.IdPerfil = ex.HResult;
				//Perfiles.Usuario = "Error";
				Perfiles.Perfil = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(Perfiles);
			}
			return Lista;

		}
		public List<Perfiles> ListaPerfiles(string Perfil)
		{
            if(Perfil == string.Empty)
            {
                Perfil = "%%%";
            }
            else
            {
				Perfil = "%" + Perfil + "%";
			}
			var Lista = new List<Perfiles>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT IdPerfil, Perfil ";
			sql += " FROM dbo.Perfiles ";
			sql += " where Perfil like @Perfil order by Perfil";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Perfil", Perfil));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var Perfiles = new Perfiles();
						Perfiles.IdPerfil = Convert.ToInt32(reader["IdPerfil"].ToString());
						Perfiles.Perfil = reader["Perfil"].ToString();
						Lista.Add(Perfiles);
					}
				}
			}
			catch (Exception ex)
			{
				var Perfiles = new Perfiles();
				Perfiles.IdPerfil = ex.HResult;
				//Perfiles.Usuario = "Error";
				Perfiles.Perfil = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(Perfiles);
			}
			return Lista;

		}
		public RespuestaSQL UsuarioAdd(ModUsuarios modUsuarios)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpUsuarioAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Usuario", modUsuarios.Usuario));
			listaParams.Add(new SqlParameter("@Nombre", modUsuarios.Nombre));
			listaParams.Add(new SqlParameter("@PWD", modUsuarios.PWD));
			listaParams.Add(new SqlParameter("@IdPerfil", modUsuarios.IdPerfil));
			listaParams.Add(new SqlParameter("@IdDepto", modUsuarios.IdDepto));
			listaParams.Add(new SqlParameter("@Email", modUsuarios.Email));
			listaParams.Add(new SqlParameter("@Telefono", modUsuarios.Telefono));

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
				Respuesta.Mensaje = "Error al Guardar el Usuario" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL UsuarioMod(ModUsuarios modUsuarios)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpUsuarioMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdUsu", modUsuarios.IdUsu));
			listaParams.Add(new SqlParameter("@Nombre", modUsuarios.Nombre));
			listaParams.Add(new SqlParameter("@PWD", modUsuarios.PWD));
			listaParams.Add(new SqlParameter("@IdPerfil", modUsuarios.IdPerfil));
			listaParams.Add(new SqlParameter("@IdDepto", modUsuarios.IdDepto));
			listaParams.Add(new SqlParameter("@Email", modUsuarios.Email));
			listaParams.Add(new SqlParameter("@Telefono", modUsuarios.Telefono));
			listaParams.Add(new SqlParameter("@Estatus", modUsuarios.Estatus));

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
				Respuesta.Mensaje = "Error al Modificar el Usuario" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL UsuarioDel(ModUsuarios modUsuarios)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpUsuarioDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdUsu", modUsuarios.IdUsu));

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
				Respuesta.Mensaje = "Error al Eliminar el Usuario" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
        public List<VtUsuarioSuc> ListaUsuSuc(int IdUsu)
        {
			var Lista = new List<VtUsuarioSuc>();
			var listaParams = new List<SqlParameter>();

			String sql = "SELECT * ";
			sql += " FROM dbo.VtUsuarioSuc ";
			sql += " where IdUsu = @IdUsu order by Sucursal";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdUsu", IdUsu));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var VtUsuarioSuc = new VtUsuarioSuc();
						VtUsuarioSuc.IdUsuSuc = Convert.ToInt32(reader["IdUsuSuc"].ToString());
						VtUsuarioSuc.IdSuc = Convert.ToInt32(reader["IdSuc"].ToString());
						VtUsuarioSuc.Sucursal = reader["Sucursal"].ToString();
						VtUsuarioSuc.Nombre = reader["Nombre"].ToString();
						VtUsuarioSuc.IdUsu = Convert.ToInt32(reader["IdUsu"].ToString()); 
                        Lista.Add(VtUsuarioSuc);
					}
				}
			}
			catch (Exception ex)
			{
				var VtUsuarioSuc = new VtUsuarioSuc();
				VtUsuarioSuc.IdUsuSuc = ex.HResult;
				VtUsuarioSuc.Sucursal = "Error";
				VtUsuarioSuc.Nombre = "Error al llenar las Sucursales del Usuario" + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(VtUsuarioSuc);
			}
			return Lista;

		}
		public List<SpVtPerfiles> SpVtPerfiles(int IdPerfil)
		{
			var Lista = new List<SpVtPerfiles>();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpVtPerfiles ";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdPerfil", IdPerfil));

			try
			{
				using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.StoredProcedure, listaParams).ExecuteReader())
				{
					while (reader.Read())
					{
						var SpVtPerfiles = new SpVtPerfiles();
						SpVtPerfiles.IdPerfil = Convert.ToInt32(reader["IdPerfil"].ToString());
						SpVtPerfiles.Perfil = reader["Perfil"].ToString();
						SpVtPerfiles.NomCampo = reader["NomCampo"].ToString();
						SpVtPerfiles.Descripcion = reader["Descripcion2"].ToString();
						SpVtPerfiles.Valor = Convert.ToBoolean(reader["Valor"].ToString());
						SpVtPerfiles.IdNodo = reader["idNodo"].ToString();
						SpVtPerfiles.IdNodoPadre = reader["idNodoPadre"].ToString();
						Lista.Add(SpVtPerfiles);
					}
				}
			}
			catch (Exception ex)
			{
				var SpVtPerfiles = new SpVtPerfiles();
				SpVtPerfiles.IdPerfil = ex.HResult;
				SpVtPerfiles.Perfil = "Error";
				SpVtPerfiles.Descripcion = "Error al llenar los Usuarios" + ex.Message.ToString().Replace("'", "-") + ".";
				Lista.Add(SpVtPerfiles);
			}
			return Lista;

		}
		public RespuestaSQL PerfilAdd(Perfiles modPerfiles)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpPerfilAdd";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@Perfil", modPerfiles.Perfil));
			listaParams.Add(new SqlParameter("@EntraCxc", modPerfiles.EntraCxc));
			listaParams.Add(new SqlParameter("@CxcAdd", modPerfiles.CxcAdd));
			listaParams.Add(new SqlParameter("@CxcMod", modPerfiles.CxcMod));
			listaParams.Add(new SqlParameter("@CxcCan", modPerfiles.CxcCan));
			listaParams.Add(new SqlParameter("@CxcDel", modPerfiles.CxcDel));
			listaParams.Add(new SqlParameter("@EntraVentas", modPerfiles.EntraVentas));
			listaParams.Add(new SqlParameter("@VentaAdd", modPerfiles.VentaAdd));
			listaParams.Add(new SqlParameter("@VentaMod", modPerfiles.VentaMod));
			listaParams.Add(new SqlParameter("@VentaCan", modPerfiles.VentaCan));
			listaParams.Add(new SqlParameter("@VentaDel", modPerfiles.VentaDel));
			listaParams.Add(new SqlParameter("@EntraInv", modPerfiles.EntraInv));
			listaParams.Add(new SqlParameter("@InvAdd", modPerfiles.InvAdd));
			listaParams.Add(new SqlParameter("@InvMod", modPerfiles.InvMod));
			listaParams.Add(new SqlParameter("@InvCan", modPerfiles.InvCan));
			listaParams.Add(new SqlParameter("@InvDel", modPerfiles.InvDel));
			listaParams.Add(new SqlParameter("@EntraArticulos", modPerfiles.EntraArticulos));
			listaParams.Add(new SqlParameter("@ArticuloAdd", modPerfiles.ArticuloAdd));
			listaParams.Add(new SqlParameter("@ArticuloMod", modPerfiles.ArticuloMod));
			listaParams.Add(new SqlParameter("@ArticuloDel", modPerfiles.ArticuloDel));
			listaParams.Add(new SqlParameter("@EntraLineas", modPerfiles.EntraLineas));
			listaParams.Add(new SqlParameter("@LineasAdd", modPerfiles.LineasAdd));
			listaParams.Add(new SqlParameter("@LineasMod", modPerfiles.LineasMod));
			listaParams.Add(new SqlParameter("@LineasDel", modPerfiles.LineasDel));
			listaParams.Add(new SqlParameter("@EntraCategoria", modPerfiles.EntraCategoria));
			listaParams.Add(new SqlParameter("@CatAdd", modPerfiles.CatAdd));
			listaParams.Add(new SqlParameter("@CatMod", modPerfiles.CatMod));
			listaParams.Add(new SqlParameter("@CatDel", modPerfiles.CatDel));
			listaParams.Add(new SqlParameter("@EntraMarcas", modPerfiles.EntraMarcas));
			listaParams.Add(new SqlParameter("@MarcasAdd", modPerfiles.MarcasAdd));
			listaParams.Add(new SqlParameter("@MarcasMod", modPerfiles.MarcasMod));
			listaParams.Add(new SqlParameter("@MarcasDel", modPerfiles.MarcasDel));
			listaParams.Add(new SqlParameter("@EntraModelos", modPerfiles.EntraModelos));
			listaParams.Add(new SqlParameter("@ModelosAdd", modPerfiles.ModelosAdd));
			listaParams.Add(new SqlParameter("@ModelosMod", modPerfiles.ModelosMod));
			listaParams.Add(new SqlParameter("@ModelosDel", modPerfiles.ModelosDel));
			listaParams.Add(new SqlParameter("@EntraColores", modPerfiles.EntraColores));
			listaParams.Add(new SqlParameter("@ColoresAdd", modPerfiles.ColoresAdd));
			listaParams.Add(new SqlParameter("@ColoresMod", modPerfiles.ColoresMod));
			listaParams.Add(new SqlParameter("@ColoresDel", modPerfiles.ColoresDel));
			listaParams.Add(new SqlParameter("@EntraUnidad", modPerfiles.EntraUnidad));
			listaParams.Add(new SqlParameter("@UnidadAdd", modPerfiles.UnidadAdd));
			listaParams.Add(new SqlParameter("@UnidadMod", modPerfiles.UnidadMod));
			listaParams.Add(new SqlParameter("@UnidadDel", modPerfiles.UnidadDel));
			listaParams.Add(new SqlParameter("@EntraCompras", modPerfiles.EntraCompras));
			listaParams.Add(new SqlParameter("@CompraAdd", modPerfiles.CompraAdd));
			listaParams.Add(new SqlParameter("@CompraMod", modPerfiles.CompraMod));
			listaParams.Add(new SqlParameter("@CompraCan", modPerfiles.CompraCan));
			listaParams.Add(new SqlParameter("@CompraDel", modPerfiles.CompraDel));
			listaParams.Add(new SqlParameter("@EntraCxp", modPerfiles.EntraCxp));
			listaParams.Add(new SqlParameter("@CxpAdd", modPerfiles.CxpAdd));
			listaParams.Add(new SqlParameter("@CxpMod", modPerfiles.CxpMod));
			listaParams.Add(new SqlParameter("@CxpCan", modPerfiles.CxpCan));
			listaParams.Add(new SqlParameter("@CxpDel", modPerfiles.CxpDel));
			listaParams.Add(new SqlParameter("@EntraReportes", modPerfiles.EntraReportes));
			listaParams.Add(new SqlParameter("@EntraRepCxc", modPerfiles.EntraRepCxc));
			listaParams.Add(new SqlParameter("@EntraRepVen", modPerfiles.EntraRepVen));
			listaParams.Add(new SqlParameter("@EntraRepInv", modPerfiles.EntraRepInv));
			listaParams.Add(new SqlParameter("@EntraRepCom", modPerfiles.EntraRepCom));
			listaParams.Add(new SqlParameter("@EntraRepCxp", modPerfiles.EntraRepCxp));
			listaParams.Add(new SqlParameter("@EntraWati", modPerfiles.EntraWati));
			listaParams.Add(new SqlParameter("@EntraWatiEnv", modPerfiles.EntraWatiEnv));
			listaParams.Add(new SqlParameter("@EntraWatiCons", modPerfiles.EntraWatiCons));
			listaParams.Add(new SqlParameter("@EntraWatiPlan", modPerfiles.EntraWatiPlan));
			listaParams.Add(new SqlParameter("@EntraWatiAuto", modPerfiles.EntraWatiAuto));
			listaParams.Add(new SqlParameter("@WatiAdd", modPerfiles.WatiAdd));
			listaParams.Add(new SqlParameter("@WatiDel", modPerfiles.WatiDel));
			listaParams.Add(new SqlParameter("@WatiMod", modPerfiles.WatiMod));
			listaParams.Add(new SqlParameter("@EntraConfiguraciones", modPerfiles.EntraConfiguraciones));
			listaParams.Add(new SqlParameter("@EntraEmpresa", modPerfiles.EntraEmpresa));
			listaParams.Add(new SqlParameter("@EmpresaMod", modPerfiles.EmpresaMod));
			listaParams.Add(new SqlParameter("@EntraSistema", modPerfiles.EntraSistema));
			listaParams.Add(new SqlParameter("@EntraModulos", modPerfiles.EntraModulos));
			listaParams.Add(new SqlParameter("@ModAdd", modPerfiles.ModAdd));
			listaParams.Add(new SqlParameter("@ModMod", modPerfiles.ModMod));
			listaParams.Add(new SqlParameter("@ModDel", modPerfiles.ModDel));
			listaParams.Add(new SqlParameter("@EntraMovtos", modPerfiles.EntraMovtos));
			listaParams.Add(new SqlParameter("@MovtosAdd", modPerfiles.MovtosAdd));
			listaParams.Add(new SqlParameter("@MovtosMod", modPerfiles.MovtosMod));
			listaParams.Add(new SqlParameter("@MovtosDel", modPerfiles.MovtosDel));
			listaParams.Add(new SqlParameter("@EntraFolios", modPerfiles.EntraFolios));
			listaParams.Add(new SqlParameter("@FolioAdd", modPerfiles.FolioAdd));
			listaParams.Add(new SqlParameter("@FolioMod", modPerfiles.FolioMod));
			listaParams.Add(new SqlParameter("@FolioDel", modPerfiles.FolioDel));
			listaParams.Add(new SqlParameter("@EntraUsuarios", modPerfiles.EntraUsuarios));
			listaParams.Add(new SqlParameter("@UsuarioAdd", modPerfiles.UsuarioAdd));
			listaParams.Add(new SqlParameter("@UsuarioMod", modPerfiles.UsuarioMod));
			listaParams.Add(new SqlParameter("@UsuarioDel", modPerfiles.UsuarioDel));
			listaParams.Add(new SqlParameter("@EntraPerfiles", modPerfiles.EntraPerfiles));
			listaParams.Add(new SqlParameter("@PerfilAdd", modPerfiles.PerfilAdd));
			listaParams.Add(new SqlParameter("@PerfilMod", modPerfiles.PerfilMod));
			listaParams.Add(new SqlParameter("@PerfilDel", modPerfiles.PerfilDel));
			listaParams.Add(new SqlParameter("@EntraSucursales", modPerfiles.EntraSucursales));
			listaParams.Add(new SqlParameter("@SucursalAdd", modPerfiles.SucursalAdd));
			listaParams.Add(new SqlParameter("@SucursalMod", modPerfiles.SucursalMod));
			listaParams.Add(new SqlParameter("@SucursalDel", modPerfiles.SucursalDel));
			listaParams.Add(new SqlParameter("@EntraAlmacenes", modPerfiles.EntraAlmacenes));
			listaParams.Add(new SqlParameter("@AlmacenAdd", modPerfiles.AlmacenAdd));
			listaParams.Add(new SqlParameter("@AlmacenMod", modPerfiles.AlmacenMod));
			listaParams.Add(new SqlParameter("@AlmacenDel", modPerfiles.AlmacenDel));
			listaParams.Add(new SqlParameter("@EntraDepto", modPerfiles.EntraDepto));
			listaParams.Add(new SqlParameter("@DeptoAdd", modPerfiles.DeptoAdd));
			listaParams.Add(new SqlParameter("@DeptoMod", modPerfiles.DeptoMod));
			listaParams.Add(new SqlParameter("@DeptoDel", modPerfiles.DeptoDel));
			listaParams.Add(new SqlParameter("@EntraPaises", modPerfiles.EntraPaises));
			listaParams.Add(new SqlParameter("@PaisAdd", modPerfiles.PaisAdd));
			listaParams.Add(new SqlParameter("@PaisMod", modPerfiles.PaisMod));
			listaParams.Add(new SqlParameter("@PaisDel", modPerfiles.PaisDel));
			listaParams.Add(new SqlParameter("@EntraEstados", modPerfiles.EntraEstados));
			listaParams.Add(new SqlParameter("@EstadoAdd", modPerfiles.EstadoAdd));
			listaParams.Add(new SqlParameter("@EstadoMod", modPerfiles.EstadoMod));
			listaParams.Add(new SqlParameter("@EstadoDel", modPerfiles.EstadoDel));
			listaParams.Add(new SqlParameter("@EntraCiudades", modPerfiles.EntraCiudades));
			listaParams.Add(new SqlParameter("@CiudadAdd", modPerfiles.CiudadAdd));
			listaParams.Add(new SqlParameter("@CiudadMod", modPerfiles.CiudadMod));
			listaParams.Add(new SqlParameter("@CiudadDel", modPerfiles.CiudadDel));
			listaParams.Add(new SqlParameter("@EntraColonias", modPerfiles.EntraColonias));
			listaParams.Add(new SqlParameter("@ColoniaAdd", modPerfiles.ColoniaAdd));
			listaParams.Add(new SqlParameter("@ColoniaMod", modPerfiles.ColoniaMod));
			listaParams.Add(new SqlParameter("@ColoniaDel", modPerfiles.ColoniaDel));
			listaParams.Add(new SqlParameter("@WatiConsAdd", modPerfiles.WatiConsAdd));
			listaParams.Add(new SqlParameter("@WatiConsMod", modPerfiles.WatiConsMod));
			listaParams.Add(new SqlParameter("@WatiConsDel", modPerfiles.WatiConsDel));
			listaParams.Add(new SqlParameter("@WatiPlanAdd", modPerfiles.WatiPlanAdd));
			listaParams.Add(new SqlParameter("@WatiPlanMod", modPerfiles.WatiPlanMod));
			listaParams.Add(new SqlParameter("@WatiPlanDel", modPerfiles.WatiPlanDel));
			listaParams.Add(new SqlParameter("@EntraMovInv", modPerfiles.EntraMovInv));
			listaParams.Add(new SqlParameter("@EntraConta", modPerfiles.EntraConta));
			listaParams.Add(new SqlParameter("@ContaAdd", modPerfiles.ContaAdd));
			listaParams.Add(new SqlParameter("@ContaMod", modPerfiles.ContaMod));
			listaParams.Add(new SqlParameter("@ContaCan", modPerfiles.ContaCan));
			listaParams.Add(new SqlParameter("@ContaDel", modPerfiles.ContaDel));
			listaParams.Add(new SqlParameter("@EntraCatConta", modPerfiles.EntraCatConta));
			listaParams.Add(new SqlParameter("@CatContaAdd", modPerfiles.CatContaAdd));
			listaParams.Add(new SqlParameter("@CatContaMod", modPerfiles.CatContaMod));
			listaParams.Add(new SqlParameter("@CatContaDel", modPerfiles.CatContaDel));
			listaParams.Add(new SqlParameter("@EntraFormPago", modPerfiles.EntraFormPago));
			listaParams.Add(new SqlParameter("@FPagoAdd", modPerfiles.FPagoAdd));
			listaParams.Add(new SqlParameter("@FPagoMod", modPerfiles.FPagoMod));
			listaParams.Add(new SqlParameter("@FPagoDel", modPerfiles.FPagoDel));
			listaParams.Add(new SqlParameter("@EntraMetPago", modPerfiles.EntraMetPago));
			listaParams.Add(new SqlParameter("@MetPagoAdd", modPerfiles.MetPagoAdd));
			listaParams.Add(new SqlParameter("@MetPagoMod", modPerfiles.MetPagoMod));
			listaParams.Add(new SqlParameter("@MetPagoDel", modPerfiles.MetPagoDel));
			listaParams.Add(new SqlParameter("@EntraImp", modPerfiles.EntraImp));
			listaParams.Add(new SqlParameter("@ImpAdd", modPerfiles.ImpAdd));
			listaParams.Add(new SqlParameter("@ImpMod", modPerfiles.ImpMod));
			listaParams.Add(new SqlParameter("@ImpDel", modPerfiles.ImpDel));
			listaParams.Add(new SqlParameter("@EntraRFiscal", modPerfiles.EntraRFiscal));
			listaParams.Add(new SqlParameter("@RfiscalAdd", modPerfiles.RfiscalAdd));
			listaParams.Add(new SqlParameter("@RfiscalMod", modPerfiles.RfiscalMod));
			listaParams.Add(new SqlParameter("@RfiscalDel", modPerfiles.RfiscalDel));
			listaParams.Add(new SqlParameter("@EntraRepConta", modPerfiles.EntraRepConta));
			listaParams.Add(new SqlParameter("@EntraClientes", modPerfiles.EntraClientes));
			listaParams.Add(new SqlParameter("@CteAdd", modPerfiles.CteAdd));
			listaParams.Add(new SqlParameter("@CteMod", modPerfiles.CteMod));
			listaParams.Add(new SqlParameter("@CteDel", modPerfiles.CteDel));
			listaParams.Add(new SqlParameter("@EntraProv", modPerfiles.EntraProv));
			listaParams.Add(new SqlParameter("@ProvAdd", modPerfiles.ProvAdd));
			listaParams.Add(new SqlParameter("@ProvMod", modPerfiles.ProvMod));
			listaParams.Add(new SqlParameter("@ProvDel", modPerfiles.ProvDel));
			listaParams.Add(new SqlParameter("@EntraCajas", modPerfiles.EntraCajas));
			listaParams.Add(new SqlParameter("@CajaAdd", modPerfiles.CajaAdd));
			listaParams.Add(new SqlParameter("@CajaMod", modPerfiles.CajaMod));
			listaParams.Add(new SqlParameter("@CajaDel", modPerfiles.CajaDel));
			listaParams.Add(new SqlParameter("@EntraTallas", modPerfiles.EntraTallas));
			listaParams.Add(new SqlParameter("@TallaAdd", modPerfiles.TallaAdd));
			listaParams.Add(new SqlParameter("@TallaMod", modPerfiles.TallaMod));
			listaParams.Add(new SqlParameter("@TallaDel", modPerfiles.TallaDel));
			listaParams.Add(new SqlParameter("@EntraFamilias", modPerfiles.EntraFamilias));
			listaParams.Add(new SqlParameter("@FamiliaAdd", modPerfiles.FamiliaAdd));
			listaParams.Add(new SqlParameter("@FamiliaMod", modPerfiles.FamiliaMod));
			listaParams.Add(new SqlParameter("@FamiliaDel", modPerfiles.FamiliaDel));

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
				Respuesta.Mensaje = "Error al Guardar el Perfil" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL PerfilMod(Perfiles modPerfiles)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpPerfilMod";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdPerfil", modPerfiles.IdPerfil));
			listaParams.Add(new SqlParameter("@Perfil", modPerfiles.Perfil));
			listaParams.Add(new SqlParameter("@EntraCxc", modPerfiles.EntraCxc));
			listaParams.Add(new SqlParameter("@CxcAdd", modPerfiles.CxcAdd));
			listaParams.Add(new SqlParameter("@CxcMod", modPerfiles.CxcMod));
			listaParams.Add(new SqlParameter("@CxcCan", modPerfiles.CxcCan));
			listaParams.Add(new SqlParameter("@CxcDel", modPerfiles.CxcDel));
			listaParams.Add(new SqlParameter("@EntraVentas", modPerfiles.EntraVentas));
			listaParams.Add(new SqlParameter("@VentaAdd", modPerfiles.VentaAdd));
			listaParams.Add(new SqlParameter("@VentaMod", modPerfiles.VentaMod));
			listaParams.Add(new SqlParameter("@VentaCan", modPerfiles.VentaCan));
			listaParams.Add(new SqlParameter("@VentaDel", modPerfiles.VentaDel));
			listaParams.Add(new SqlParameter("@EntraInv", modPerfiles.EntraInv));
			listaParams.Add(new SqlParameter("@InvAdd", modPerfiles.InvAdd));
			listaParams.Add(new SqlParameter("@InvMod", modPerfiles.InvMod));
			listaParams.Add(new SqlParameter("@InvCan", modPerfiles.InvCan));
			listaParams.Add(new SqlParameter("@InvDel", modPerfiles.InvDel));
			listaParams.Add(new SqlParameter("@EntraArticulos", modPerfiles.EntraArticulos));
			listaParams.Add(new SqlParameter("@ArticuloAdd", modPerfiles.ArticuloAdd));
			listaParams.Add(new SqlParameter("@ArticuloMod", modPerfiles.ArticuloMod));
			listaParams.Add(new SqlParameter("@ArticuloDel", modPerfiles.ArticuloDel));
			listaParams.Add(new SqlParameter("@EntraLineas", modPerfiles.EntraLineas));
			listaParams.Add(new SqlParameter("@LineasAdd", modPerfiles.LineasAdd));
			listaParams.Add(new SqlParameter("@LineasMod", modPerfiles.LineasMod));
			listaParams.Add(new SqlParameter("@LineasDel", modPerfiles.LineasDel));
			listaParams.Add(new SqlParameter("@EntraCategoria", modPerfiles.EntraCategoria));
			listaParams.Add(new SqlParameter("@CatAdd", modPerfiles.CatAdd));
			listaParams.Add(new SqlParameter("@CatMod", modPerfiles.CatMod));
			listaParams.Add(new SqlParameter("@CatDel", modPerfiles.CatDel));
			listaParams.Add(new SqlParameter("@EntraMarcas", modPerfiles.EntraMarcas));
			listaParams.Add(new SqlParameter("@MarcasAdd", modPerfiles.MarcasAdd));
			listaParams.Add(new SqlParameter("@MarcasMod", modPerfiles.MarcasMod));
			listaParams.Add(new SqlParameter("@MarcasDel", modPerfiles.MarcasDel));
			listaParams.Add(new SqlParameter("@EntraModelos", modPerfiles.EntraModelos));
			listaParams.Add(new SqlParameter("@ModelosAdd", modPerfiles.ModelosAdd));
			listaParams.Add(new SqlParameter("@ModelosMod", modPerfiles.ModelosMod));
			listaParams.Add(new SqlParameter("@ModelosDel", modPerfiles.ModelosDel));
			listaParams.Add(new SqlParameter("@EntraColores", modPerfiles.EntraColores));
			listaParams.Add(new SqlParameter("@ColoresAdd", modPerfiles.ColoresAdd));
			listaParams.Add(new SqlParameter("@ColoresMod", modPerfiles.ColoresMod));
			listaParams.Add(new SqlParameter("@ColoresDel", modPerfiles.ColoresDel));
			listaParams.Add(new SqlParameter("@EntraUnidad", modPerfiles.EntraUnidad));
			listaParams.Add(new SqlParameter("@UnidadAdd", modPerfiles.UnidadAdd));
			listaParams.Add(new SqlParameter("@UnidadMod", modPerfiles.UnidadMod));
			listaParams.Add(new SqlParameter("@UnidadDel", modPerfiles.UnidadDel));
			listaParams.Add(new SqlParameter("@EntraCompras", modPerfiles.EntraCompras));
			listaParams.Add(new SqlParameter("@CompraAdd", modPerfiles.CompraAdd));
			listaParams.Add(new SqlParameter("@CompraMod", modPerfiles.CompraMod));
			listaParams.Add(new SqlParameter("@CompraCan", modPerfiles.CompraCan));
			listaParams.Add(new SqlParameter("@CompraDel", modPerfiles.CompraDel));
			listaParams.Add(new SqlParameter("@EntraCxp", modPerfiles.EntraCxp));
			listaParams.Add(new SqlParameter("@CxpAdd", modPerfiles.CxpAdd));
			listaParams.Add(new SqlParameter("@CxpMod", modPerfiles.CxpMod));
			listaParams.Add(new SqlParameter("@CxpCan", modPerfiles.CxpCan));
			listaParams.Add(new SqlParameter("@CxpDel", modPerfiles.CxpDel));
			listaParams.Add(new SqlParameter("@EntraReportes", modPerfiles.EntraReportes));
			listaParams.Add(new SqlParameter("@EntraRepCxc", modPerfiles.EntraRepCxc));
			listaParams.Add(new SqlParameter("@EntraRepVen", modPerfiles.EntraRepVen));
			listaParams.Add(new SqlParameter("@EntraRepInv", modPerfiles.EntraRepInv));
			listaParams.Add(new SqlParameter("@EntraRepCom", modPerfiles.EntraRepCom));
			listaParams.Add(new SqlParameter("@EntraRepCxp", modPerfiles.EntraRepCxp));
			listaParams.Add(new SqlParameter("@EntraWati", modPerfiles.EntraWati));
			listaParams.Add(new SqlParameter("@EntraWatiEnv", modPerfiles.EntraWatiEnv));
			listaParams.Add(new SqlParameter("@EntraWatiCons", modPerfiles.EntraWatiCons));
			listaParams.Add(new SqlParameter("@EntraWatiPlan", modPerfiles.EntraWatiPlan));
			listaParams.Add(new SqlParameter("@EntraWatiAuto", modPerfiles.EntraWatiAuto));
			listaParams.Add(new SqlParameter("@WatiAdd", modPerfiles.WatiAdd));
			listaParams.Add(new SqlParameter("@WatiDel", modPerfiles.WatiDel));
			listaParams.Add(new SqlParameter("@WatiMod", modPerfiles.WatiMod));
			listaParams.Add(new SqlParameter("@EntraConfiguraciones", modPerfiles.EntraConfiguraciones));
			listaParams.Add(new SqlParameter("@EntraEmpresa", modPerfiles.EntraEmpresa));
			listaParams.Add(new SqlParameter("@EmpresaMod", modPerfiles.EmpresaMod));
			listaParams.Add(new SqlParameter("@EntraSistema", modPerfiles.EntraSistema));
			listaParams.Add(new SqlParameter("@EntraModulos", modPerfiles.EntraModulos));
			listaParams.Add(new SqlParameter("@ModAdd", modPerfiles.ModAdd));
			listaParams.Add(new SqlParameter("@ModMod", modPerfiles.ModMod));
			listaParams.Add(new SqlParameter("@ModDel", modPerfiles.ModDel));
			listaParams.Add(new SqlParameter("@EntraMovtos", modPerfiles.EntraMovtos));
			listaParams.Add(new SqlParameter("@MovtosAdd", modPerfiles.MovtosAdd));
			listaParams.Add(new SqlParameter("@MovtosMod", modPerfiles.MovtosMod));
			listaParams.Add(new SqlParameter("@MovtosDel", modPerfiles.MovtosDel));
			listaParams.Add(new SqlParameter("@EntraFolios", modPerfiles.EntraFolios));
			listaParams.Add(new SqlParameter("@FolioAdd", modPerfiles.FolioAdd));
			listaParams.Add(new SqlParameter("@FolioMod", modPerfiles.FolioMod));
			listaParams.Add(new SqlParameter("@FolioDel", modPerfiles.FolioDel));
			listaParams.Add(new SqlParameter("@EntraUsuarios", modPerfiles.EntraUsuarios));
			listaParams.Add(new SqlParameter("@UsuarioAdd", modPerfiles.UsuarioAdd));
			listaParams.Add(new SqlParameter("@UsuarioMod", modPerfiles.UsuarioMod));
			listaParams.Add(new SqlParameter("@UsuarioDel", modPerfiles.UsuarioDel));
			listaParams.Add(new SqlParameter("@EntraPerfiles", modPerfiles.EntraPerfiles));
			listaParams.Add(new SqlParameter("@PerfilAdd", modPerfiles.PerfilAdd));
			listaParams.Add(new SqlParameter("@PerfilMod", modPerfiles.PerfilMod));
			listaParams.Add(new SqlParameter("@PerfilDel", modPerfiles.PerfilDel));
			listaParams.Add(new SqlParameter("@EntraSucursales", modPerfiles.EntraSucursales));
			listaParams.Add(new SqlParameter("@SucursalAdd", modPerfiles.SucursalAdd));
			listaParams.Add(new SqlParameter("@SucursalMod", modPerfiles.SucursalMod));
			listaParams.Add(new SqlParameter("@SucursalDel", modPerfiles.SucursalDel));
			listaParams.Add(new SqlParameter("@EntraAlmacenes", modPerfiles.EntraAlmacenes));
			listaParams.Add(new SqlParameter("@AlmacenAdd", modPerfiles.AlmacenAdd));
			listaParams.Add(new SqlParameter("@AlmacenMod", modPerfiles.AlmacenMod));
			listaParams.Add(new SqlParameter("@AlmacenDel", modPerfiles.AlmacenDel));
			listaParams.Add(new SqlParameter("@EntraDepto", modPerfiles.EntraDepto));
			listaParams.Add(new SqlParameter("@DeptoAdd", modPerfiles.DeptoAdd));
			listaParams.Add(new SqlParameter("@DeptoMod", modPerfiles.DeptoMod));
			listaParams.Add(new SqlParameter("@DeptoDel", modPerfiles.DeptoDel));
			listaParams.Add(new SqlParameter("@EntraPaises", modPerfiles.EntraPaises));
			listaParams.Add(new SqlParameter("@PaisAdd", modPerfiles.PaisAdd));
			listaParams.Add(new SqlParameter("@PaisMod", modPerfiles.PaisMod));
			listaParams.Add(new SqlParameter("@PaisDel", modPerfiles.PaisDel));
			listaParams.Add(new SqlParameter("@EntraEstados", modPerfiles.EntraEstados));
			listaParams.Add(new SqlParameter("@EstadoAdd", modPerfiles.EstadoAdd));
			listaParams.Add(new SqlParameter("@EstadoMod", modPerfiles.EstadoMod));
			listaParams.Add(new SqlParameter("@EstadoDel", modPerfiles.EstadoDel));
			listaParams.Add(new SqlParameter("@EntraCiudades", modPerfiles.EntraCiudades));
			listaParams.Add(new SqlParameter("@CiudadAdd", modPerfiles.CiudadAdd));
			listaParams.Add(new SqlParameter("@CiudadMod", modPerfiles.CiudadMod));
			listaParams.Add(new SqlParameter("@CiudadDel", modPerfiles.CiudadDel));
			listaParams.Add(new SqlParameter("@EntraColonias", modPerfiles.EntraColonias));
			listaParams.Add(new SqlParameter("@ColoniaAdd", modPerfiles.ColoniaAdd));
			listaParams.Add(new SqlParameter("@ColoniaMod", modPerfiles.ColoniaMod));
			listaParams.Add(new SqlParameter("@ColoniaDel", modPerfiles.ColoniaDel));
			listaParams.Add(new SqlParameter("@WatiConsAdd", modPerfiles.WatiConsAdd));
			listaParams.Add(new SqlParameter("@WatiConsMod", modPerfiles.WatiConsMod));
			listaParams.Add(new SqlParameter("@WatiConsDel", modPerfiles.WatiConsDel));
			listaParams.Add(new SqlParameter("@WatiPlanAdd", modPerfiles.WatiPlanAdd));
			listaParams.Add(new SqlParameter("@WatiPlanMod", modPerfiles.WatiPlanMod));
			listaParams.Add(new SqlParameter("@WatiPlanDel", modPerfiles.WatiPlanDel));
			listaParams.Add(new SqlParameter("@EntraMovInv", modPerfiles.EntraMovInv));
			listaParams.Add(new SqlParameter("@EntraConta", modPerfiles.EntraConta));
			listaParams.Add(new SqlParameter("@ContaAdd", modPerfiles.ContaAdd));
			listaParams.Add(new SqlParameter("@ContaMod", modPerfiles.ContaMod));
			listaParams.Add(new SqlParameter("@ContaCan", modPerfiles.ContaCan));
			listaParams.Add(new SqlParameter("@ContaDel", modPerfiles.ContaDel));
			listaParams.Add(new SqlParameter("@EntraCatConta", modPerfiles.EntraCatConta));
			listaParams.Add(new SqlParameter("@CatContaAdd", modPerfiles.CatContaAdd));
			listaParams.Add(new SqlParameter("@CatContaMod", modPerfiles.CatContaMod));
			listaParams.Add(new SqlParameter("@CatContaDel", modPerfiles.CatContaDel));
			listaParams.Add(new SqlParameter("@EntraFormPago", modPerfiles.EntraFormPago));
			listaParams.Add(new SqlParameter("@FPagoAdd", modPerfiles.FPagoAdd));
			listaParams.Add(new SqlParameter("@FPagoMod", modPerfiles.FPagoMod));
			listaParams.Add(new SqlParameter("@FPagoDel", modPerfiles.FPagoDel));
			listaParams.Add(new SqlParameter("@EntraMetPago", modPerfiles.EntraMetPago));
			listaParams.Add(new SqlParameter("@MetPagoAdd", modPerfiles.MetPagoAdd));
			listaParams.Add(new SqlParameter("@MetPagoMod", modPerfiles.MetPagoMod));
			listaParams.Add(new SqlParameter("@MetPagoDel", modPerfiles.MetPagoDel));
			listaParams.Add(new SqlParameter("@EntraImp", modPerfiles.EntraImp));
			listaParams.Add(new SqlParameter("@ImpAdd", modPerfiles.ImpAdd));
			listaParams.Add(new SqlParameter("@ImpMod", modPerfiles.ImpMod));
			listaParams.Add(new SqlParameter("@ImpDel", modPerfiles.ImpDel));
			listaParams.Add(new SqlParameter("@EntraRFiscal", modPerfiles.EntraRFiscal));
			listaParams.Add(new SqlParameter("@RfiscalAdd", modPerfiles.RfiscalAdd));
			listaParams.Add(new SqlParameter("@RfiscalMod", modPerfiles.RfiscalMod));
			listaParams.Add(new SqlParameter("@RfiscalDel", modPerfiles.RfiscalDel));
			listaParams.Add(new SqlParameter("@EntraRepConta", modPerfiles.EntraRepConta));
			listaParams.Add(new SqlParameter("@EntraClientes", modPerfiles.EntraClientes));
			listaParams.Add(new SqlParameter("@CteAdd", modPerfiles.CteAdd));
			listaParams.Add(new SqlParameter("@CteMod", modPerfiles.CteMod));
			listaParams.Add(new SqlParameter("@CteDel", modPerfiles.CteDel));
			listaParams.Add(new SqlParameter("@EntraProv", modPerfiles.EntraProv));
			listaParams.Add(new SqlParameter("@ProvAdd", modPerfiles.ProvAdd));
			listaParams.Add(new SqlParameter("@ProvMod", modPerfiles.ProvMod));
			listaParams.Add(new SqlParameter("@ProvDel", modPerfiles.ProvDel));
			listaParams.Add(new SqlParameter("@EntraCajas", modPerfiles.EntraCajas));
			listaParams.Add(new SqlParameter("@CajaAdd", modPerfiles.CajaAdd));
			listaParams.Add(new SqlParameter("@CajaMod", modPerfiles.CajaMod));
			listaParams.Add(new SqlParameter("@CajaDel", modPerfiles.CajaDel));
			listaParams.Add(new SqlParameter("@EntraTallas", modPerfiles.EntraTallas));
			listaParams.Add(new SqlParameter("@TallaAdd", modPerfiles.TallaAdd));
			listaParams.Add(new SqlParameter("@TallaMod", modPerfiles.TallaMod));
			listaParams.Add(new SqlParameter("@TallaDel", modPerfiles.TallaDel));
			listaParams.Add(new SqlParameter("@EntraFamilias", modPerfiles.EntraFamilias));
			listaParams.Add(new SqlParameter("@FamiliaAdd", modPerfiles.FamiliaAdd));
			listaParams.Add(new SqlParameter("@FamiliaMod", modPerfiles.FamiliaMod));
			listaParams.Add(new SqlParameter("@FamiliaDel", modPerfiles.FamiliaDel));

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
				Respuesta.Mensaje = "Error al Modificar el Perfil" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}
		public RespuestaSQL PerfilDel(Perfiles modPerfiles)
		{
			var Respuesta = new RespuestaSQL();
			var listaParams = new List<SqlParameter>();

			String sql = "dbo.SpPerfilDel";

			listaParams.Clear();
			listaParams.Add(new SqlParameter("@IdPerfil", modPerfiles.IdPerfil));

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
				Respuesta.Mensaje = "Error al Eliminar el Perfil" + ex.Message.ToString().Replace("'", "-") + ".";
			}
			return Respuesta;

		}

	}
}