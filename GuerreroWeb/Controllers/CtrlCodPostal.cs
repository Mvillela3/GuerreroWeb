using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static GuerreroWeb.Controllers.DBConexion;
using System.Web.UI;
using System.Collections;

namespace GuerreroWeb.Controllers
{
    public class CtrlCodPostal
    {
        private DBConexion conexion = new DBConexion();
        private DBComandos Cmd = new DBComandos();
        
        public List<CatPaises> DdlPaises()
        {
            var Lista = new List<CatPaises>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT IdPais, Pais, Abreviatura, EsDefault ";
            sql += " FROM dbo.CatPaises ";
            sql += " union Select 0 as IdPais, 'Seleccione una Opcion' as Pais, '000' as Abreviatura, cast(0 as bit) as EsDefault ";
            sql += " order by Abreviatura";

            listaParams.Clear();
            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatPaises = new CatPaises();
                        CatPaises.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        CatPaises.Pais = reader["Pais"].ToString();
                        CatPaises.Abreviatura = reader["Abreviatura"].ToString();
                        CatPaises.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        
                        Lista.Add(CatPaises);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatPaises = new CatPaises();
                CatPaises.IdPais = ex.HResult;
                CatPaises.Abreviatura = "Error";
                CatPaises.Pais = "Error al llenar los Paises " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatPaises);
            }
            return Lista;

        }
        public List<CatPaises> ListaPaises(string Pais)
        {
            var Lista = new List<CatPaises>();
            var listaParams = new List<SqlParameter>();

            if(Pais == string.Empty)
            {
                Pais = "%%%";
            }
            else
            {
                Pais = "%" + Pais + "%";
            }
            String sql = "SELECT * ";
            sql += " FROM dbo.CatPaises ";
            sql += " where Pais like @Pais or Abreviatura like @Pais order by Pais";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Pais", Pais));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatPaises = new CatPaises();
                        CatPaises.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        CatPaises.Pais = reader["Pais"].ToString();
                        CatPaises.Abreviatura = reader["Abreviatura"].ToString();
                        CatPaises.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());

                        Lista.Add(CatPaises);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatPaises = new CatPaises();
                CatPaises.IdPais = ex.HResult;
                CatPaises.Abreviatura = "Error";
                CatPaises.Pais = "Error al llenar los Paises " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatPaises);
            }
            return Lista;

        }
        public List<CatEstados> DdlEstados(int IdPais)
        {
            var Lista = new List<CatEstados>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatEstados ";
            sql += " Where IdPais = @IdPais ";
            sql += " union Select 0 as IdEst, 'Seleccione una Opcion' as Estado, '000' as Abreviatura, cast(0 as bit) as EsDefault, 0 as IdPais ";
            sql += " order by Abreviatura";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdPais", IdPais));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatEstados = new CatEstados();
                        CatEstados.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        CatEstados.Estado = reader["Estado"].ToString();
                        CatEstados.Abreviatura = reader["Abreviatura"].ToString();
                        CatEstados.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        CatEstados.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        Lista.Add(CatEstados);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatEstados = new CatEstados();
                CatEstados.IdEst = ex.HResult;
                CatEstados.Abreviatura = "Error";
                CatEstados.Estado = "Error al llenar los Estados " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatEstados);
            }
            return Lista;

        }
        public List<CatCiudades> DdlCiudades(int IdEst)
        {
            var Lista = new List<CatCiudades>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT IdCiu, Ciudad, EsDefault, IdEst ";
            sql += " FROM dbo.CatCiudades ";
            sql += " Where IdEst = @IdEst ";
            sql += " union Select 0 as IdCiu, 'Seleccione una Opcion' as Ciudad, cast(0 as bit) as EsDefault, 0 as IdEst ";
            sql += " order by Ciudad ";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdEst", IdEst));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatCiudades = new CatCiudades();
                        CatCiudades.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        CatCiudades.Ciudad = reader["Ciudad"].ToString();
                        CatCiudades.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        CatCiudades.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        
                        Lista.Add(CatCiudades);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatCiudades = new CatCiudades();
                CatCiudades.IdCiu = ex.HResult;
                CatCiudades.Ciudad = "Error al llenar las Ciudades " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatCiudades);
            }
            return Lista;

        }
        public List<CatColonias> DdlColonias(int IdCiu, string CP)
        {
            var Lista = new List<CatColonias>();
            var listaParams = new List<SqlParameter>();

            if(CP == string.Empty)
            {
                CP = "%%%";
            }
            else
            {
                CP = "%" + CP + "%";
            }

            String sql = "SELECT IdCol, Colonia, CP, EsDefault, IdCiu ";
            sql += " FROM dbo.CatColonias ";
            sql += " Where IdCiu = @IdCiu and CP like @CP ";
            sql += " union Select 0 as IdCol, 'Seleccione una Opcion' as Colonia, cast(0 as bit) as EsDefault, 0 as IdCiu ";
            sql += " order by Colonia";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdCiu", IdCiu));
            listaParams.Add(new SqlParameter("@CP", CP));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var CatColonias = new CatColonias();
                        CatColonias.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        CatColonias.Colonia = reader["Colonia"].ToString();
                        CatColonias.CP = reader["CP"].ToString();
                        CatColonias.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        CatColonias.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());

                        Lista.Add(CatColonias);
                    }
                }
            }
            catch (Exception ex)
            {
                var CatColonias = new CatColonias();
                CatColonias.IdCol = ex.HResult;
                CatColonias.CP = "Error";
                CatColonias.Colonia = "Error al llenar las Colonias " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(CatColonias);
            }
            return Lista;

        }
        public CatPaises Pais(int Id)
        {
            var CatPaises = new CatPaises();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatPaises ";
            sql += " where IdPais like @IDPais order by Pais";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdPais", Id));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CatPaises.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        CatPaises.Pais = reader["Pais"].ToString();
                        CatPaises.Abreviatura = reader["Abreviatura"].ToString();
                        CatPaises.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                CatPaises.IdPais = ex.HResult;
                CatPaises.Abreviatura = "Error";
                CatPaises.Pais = "Error al llenar los Paises " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return CatPaises;

        }

        public RespuestaSQL PaisAdd(CatPaises modCatPaises)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpPaisAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Pais", modCatPaises.Pais));
            listaParams.Add(new SqlParameter("@Abreviatura", modCatPaises.Abreviatura));
            listaParams.Add(new SqlParameter("@EsDefault", modCatPaises.EsDefault));

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
                Respuesta.Mensaje = "Error al Guardar el Pais " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL PaisMod(CatPaises modCatPaises)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpPaisMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdPais", modCatPaises.IdPais));
            listaParams.Add(new SqlParameter("@Pais", modCatPaises.Pais));
            listaParams.Add(new SqlParameter("@Abreviatura", modCatPaises.Abreviatura));
            listaParams.Add(new SqlParameter("@EsDefault", modCatPaises.EsDefault));

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
                Respuesta.Mensaje = "Error al Modificar el Pais " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL PaisDel(CatPaises modCatPaises)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpPaisDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdPais", modCatPaises.IdPais));

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
                Respuesta.Mensaje = "Error al Eliminar el Pais " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<VtEstados> ListaEstados(String Estado, string IdPais)
        {
            if (Estado == string.Empty)
            {
                Estado = "%%%";

            }
            else
            {
                Estado = "%" + Estado + "%";
            }
            if(IdPais == string.Empty || IdPais == "0")
            {
                IdPais = "%%%";
            }

            var Lista = new List<VtEstados>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtEstados ";
            sql += " Where Estado like @Estado and IdPais like @IdPais order by Estado";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Estado", Estado));
            listaParams.Add(new SqlParameter("@IdPais", IdPais));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var VtEstados = new VtEstados();
                        VtEstados.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtEstados.Estado = reader["Estado"].ToString();
                        VtEstados.Abreviatura = reader["Abreviatura"].ToString();
                        VtEstados.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        VtEstados.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtEstados.Pais = reader["Pais"].ToString();
                        VtEstados.PaisAbrev = reader["PaisAbrev"].ToString();
                        Lista.Add(VtEstados);
                    }
                }
            }
            catch (Exception ex)
            {
                var VtEstados = new VtEstados();
                VtEstados.IdEst = ex.HResult;
                VtEstados.Abreviatura = "Error";
                VtEstados.Estado = "Error al llenar los Estados " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(VtEstados);
            }
            return Lista;

        }

        public CatEstados Estado(int IdEst)
        {
            var CatEstados = new CatEstados();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.CatEstados ";
            sql += " Where IdEst = @IdEst order by Estado";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdEst", IdEst));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CatEstados.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        CatEstados.Estado = reader["Estado"].ToString();
                        CatEstados.Abreviatura = reader["Abreviatura"].ToString();
                        CatEstados.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        CatEstados.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CatEstados.IdEst = ex.HResult;
                CatEstados.Abreviatura = "Error";
                CatEstados.Estado = "Error al llenar los Estados " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return CatEstados;

        }

        public RespuestaSQL EstadoAdd(CatEstados modCatEstados)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpEstadoAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Estado", modCatEstados.Estado));
            listaParams.Add(new SqlParameter("@Abreviatura", modCatEstados.Abreviatura));
            listaParams.Add(new SqlParameter("@EsDefault", modCatEstados.EsDefault));
            listaParams.Add(new SqlParameter("@IdPais", modCatEstados.IdPais));

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
                Respuesta.Mensaje = "Error al Guardar el Estado " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL EstadoMod(CatEstados modCatEstados)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpEstadoMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdEst", modCatEstados.IdEst));
            listaParams.Add(new SqlParameter("@Estado", modCatEstados.Estado));
            listaParams.Add(new SqlParameter("@Abreviatura", modCatEstados.Abreviatura));
            listaParams.Add(new SqlParameter("@EsDefault", modCatEstados.EsDefault));
            listaParams.Add(new SqlParameter("@IdPais", modCatEstados.IdPais));

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
                Respuesta.Mensaje = "Error al Modificar el Estado " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL EstadoDel(CatEstados modCatEstados)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpEstadoDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdEst", modCatEstados.IdEst));

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
                Respuesta.Mensaje = "Error al Eliminar el Estado " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<VtCiudades> ListaCiudades(String Ciudad, string IdEst, string IdPais)
        {
            if (Ciudad == string.Empty)
            {
                Ciudad = "%%%";

            }
            else
            {
                Ciudad = "%" + Ciudad + "%";
            }
            if (IdEst == string.Empty || IdEst == "0")
            {
                IdEst = "%%%";
            }
            if (IdPais == string.Empty || IdPais == "0")
            {
                IdPais = "%%%";
            }

            var Lista = new List<VtCiudades>();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtCiudades ";
            sql += " Where Ciudad like @Ciudad and IdEst like @IdEst and IdPais like @IdPais order by Ciudad";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Ciudad", Ciudad));
            listaParams.Add(new SqlParameter("@IdPais", IdPais));
            listaParams.Add(new SqlParameter("@IdEst", IdEst));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var VtCiudades = new VtCiudades();
                        VtCiudades.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtCiudades.Ciudad = reader["Ciudad"].ToString();
                        VtCiudades.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        VtCiudades.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtCiudades.Estado = reader["Estado"].ToString();
                        VtCiudades.Abreviatura = reader["Abreviatura"].ToString();
                        VtCiudades.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtCiudades.Pais = reader["Pais"].ToString();
                        VtCiudades.PaisAbrev = reader["PaisAbrev"].ToString();
                        Lista.Add(VtCiudades);
                    }
                }
            }
            catch (Exception ex)
            {
                var VtCiudades = new VtCiudades();
                VtCiudades.IdEst = ex.HResult;
                VtCiudades.Abreviatura = "Error";
                VtCiudades.Estado = "Error al llenar las Ciudades " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(VtCiudades);
            }
            return Lista;

        }
        public VtCiudades Ciudad(int IdCiu)
        {

            var VtCiudades = new VtCiudades();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtCiudades ";
            sql += " Where IdCiu = @IdCiu order by Ciudad";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdCiu", IdCiu));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VtCiudades.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtCiudades.Ciudad = reader["Ciudad"].ToString();
                        VtCiudades.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        VtCiudades.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtCiudades.Estado = reader["Estado"].ToString();
                        VtCiudades.Abreviatura = reader["Abreviatura"].ToString();
                        VtCiudades.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtCiudades.Pais = reader["Pais"].ToString();
                        VtCiudades.PaisAbrev = reader["PaisAbrev"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                VtCiudades.IdCiu = ex.HResult;
                VtCiudades.Abreviatura = "Error";
                VtCiudades.Ciudad = "Error al llenar las Ciudades " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return VtCiudades;

        }

        public RespuestaSQL CiudadAdd(CatCiudades modCatCiudades)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpCiudadAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Ciudad", modCatCiudades.Ciudad));
            listaParams.Add(new SqlParameter("@EsDefault", modCatCiudades.EsDefault));
            listaParams.Add(new SqlParameter("@IdEst", modCatCiudades.IdEst));

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
                Respuesta.Mensaje = "Error al Guardar la Ciudad " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL CiudadMod(CatCiudades modCatCiudades)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpCiudadMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdCiu", modCatCiudades.IdCiu));
            listaParams.Add(new SqlParameter("@Ciudad", modCatCiudades.Ciudad));
            listaParams.Add(new SqlParameter("@EsDefault", modCatCiudades.EsDefault));
            listaParams.Add(new SqlParameter("@IdEst", modCatCiudades.IdEst));

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
                Respuesta.Mensaje = "Error al Modificar la Ciudad " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL CiudadDel(CatCiudades modCatCiudades)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpCiudadDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdCiu", modCatCiudades.IdCiu));

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
                Respuesta.Mensaje = "Error al Eliminar la Ciudad " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public List<VtCodPostal> ListaColonias(string Colonia, string IdPais, string IdEst, string IdCiu)
        {
            var Lista = new List<VtCodPostal>();
            var listaParams = new List<SqlParameter>();

            if (Colonia == string.Empty)
            {
                Colonia = "%%%";
            }
            else
            {
                Colonia = "%" + Colonia + "%";
            }
            if (IdPais == string.Empty || IdPais == "0")
            {
                IdPais = "%%%";
            }
            if (IdEst == string.Empty || IdEst == "0")
            {
                IdEst = "%%%";
            }
            if (IdCiu == string.Empty || IdCiu == "0")
            {
                IdCiu = "%%%";
            }

            String sql = "SELECT IdCol, Colonia, EsDefault, CP, IdCiu, Ciudad, IdEst, Estado, IdPais, Pais ";
            sql += " FROM dbo.VtCodPostal ";
            sql += " Where (Colonia like @Colonia or CP like @Colonia) and IdCiu like @IdCiu and IdEst like @IdEst and IdPais like @IdPais ";
            sql += " order by Colonia";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Colonia", Colonia));
            listaParams.Add(new SqlParameter("@IdCiu", IdCiu));
            listaParams.Add(new SqlParameter("@IdEst", IdEst));
            listaParams.Add(new SqlParameter("@IdPais", IdPais));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var VtCodPostal = new VtCodPostal();
                        VtCodPostal.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        VtCodPostal.Colonia = reader["Colonia"].ToString();
                        VtCodPostal.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        VtCodPostal.CP = reader["CP"].ToString();
                        VtCodPostal.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtCodPostal.Ciudad = reader["Ciudad"].ToString();
                        VtCodPostal.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtCodPostal.Estado = reader["Estado"].ToString();
                        VtCodPostal.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtCodPostal.Pais = reader["Pais"].ToString();

                        Lista.Add(VtCodPostal);
                    }
                }
            }
            catch (Exception ex)
            {
                var VtCodPostal = new VtCodPostal();
                VtCodPostal.IdCol = ex.HResult;
                VtCodPostal.CP = "Error";
                VtCodPostal.Colonia = "Error al llenar las Colonias " + ex.Message.ToString().Replace("'", "-") + ".";
                Lista.Add(VtCodPostal);
            }
            return Lista;

        }

        public VtCodPostal Colonia(int IdCol)
        {

            var VtCodPostal = new VtCodPostal();
            var listaParams = new List<SqlParameter>();

            String sql = "SELECT * ";
            sql += " FROM dbo.VtCodPostal ";
            sql += " Where IdCol = @IdCol order by Colonia";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdCol", IdCol));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VtCodPostal.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        VtCodPostal.Colonia = reader["Colonia"].ToString();
                        VtCodPostal.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        VtCodPostal.CP = reader["CP"].ToString();
                        VtCodPostal.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        VtCodPostal.Ciudad = reader["Ciudad"].ToString();
                        VtCodPostal.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        VtCodPostal.Estado = reader["Estado"].ToString();
                        VtCodPostal.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        VtCodPostal.Pais = reader["Pais"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                VtCodPostal.IdCol = ex.HResult;
                VtCodPostal.CP = "Error";
                VtCodPostal.Colonia = "Error al llenar las Ciudades " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return VtCodPostal;

        }

        public RespuestaSQL ColoniaAdd(CatColonias modCatColonias)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpColoniaAdd";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@Colonia", modCatColonias.Colonia));
            listaParams.Add(new SqlParameter("@CP", modCatColonias.CP));
            listaParams.Add(new SqlParameter("@EsDefault", modCatColonias.EsDefault));
            listaParams.Add(new SqlParameter("@IdCiu", modCatColonias.IdCiu));

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
                Respuesta.Mensaje = "Error al Guardar la Colonia " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL ColoniaMod(CatColonias modCatColonias)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpColoniaMod";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdCol", modCatColonias.IdCol));
            listaParams.Add(new SqlParameter("@Colonia", modCatColonias.Colonia));
            listaParams.Add(new SqlParameter("@CP", modCatColonias.CP));
            listaParams.Add(new SqlParameter("@EsDefault", modCatColonias.EsDefault));
            listaParams.Add(new SqlParameter("@IdCiu", modCatColonias.IdCiu));

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
                Respuesta.Mensaje = "Error al Modificar la Colonia " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public RespuestaSQL ColoniaDel(CatColonias modCatColonias)
        {
            var Respuesta = new RespuestaSQL();
            var listaParams = new List<SqlParameter>();

            String sql = "dbo.SpColoniaDel";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdCol", modCatColonias.IdCol));

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
                Respuesta.Mensaje = "Error al Eliminar la Colonia " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public string BuscaCP(int id)
        {
            string Respuesta = "";
            var listaParams = new List<SqlParameter>();

            String sql = "select * from dbo.CatColonias where IdCol = @IdCol ";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@IdCol", id));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Respuesta = reader["CP"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta = "Error al Modificar la Colonia " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return Respuesta;

        }
        public VtCodPostal BuscaDatosCp(string CP)
        {
            VtCodPostal lista = new VtCodPostal();
            lista.IdCol = 1;
            lista.Colonia = "";
            lista.EsDefault = false;
            lista.CP = "";
            lista.IdCiu = 1;
            lista.Ciudad = "";
            lista.IdEst = 1;
            lista.Estado = "";
            lista.IdPais = 1;
            lista.Pais = "MÉXICO";

            var listaParams = new List<SqlParameter>();

            String sql = "select top 1 * from dbo.VtCodPostal where CP = @CP ";

            listaParams.Clear();
            listaParams.Add(new SqlParameter("@CP", CP));

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.IdCol = Convert.ToInt32(reader["IdCol"].ToString());
                        lista.Colonia = reader["Colonia"].ToString();
                        lista.EsDefault = Convert.ToBoolean(reader["EsDefault"].ToString());
                        lista.CP = reader["CP"].ToString();
                        lista.IdCiu = Convert.ToInt32(reader["IdCiu"].ToString());
                        lista.Ciudad = reader["Ciudad"].ToString();
                        lista.IdEst = Convert.ToInt32(reader["IdEst"].ToString());
                        lista.Estado = reader["Estado"].ToString();
                        lista.IdPais = Convert.ToInt32(reader["IdPais"].ToString());
                        lista.Pais = reader["Pais"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lista.IdCol = ex.HResult;
                lista.Ciudad = "Error";
                lista.Colonia = "Error al Buscar Datos de los Codigo Postales " + ex.Message.ToString().Replace("'", "-") + ".";
            }
            return lista;

        }
        public int PaisDef()
        {
            int lista = 0;
            var listaParams = new List<SqlParameter>();

            String sql = "select * from dbo.CatPaises where EsDefault = 1 ";

            listaParams.Clear();

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista = Convert.ToInt32(reader["IdPais"].ToString());
                    }
                }
            }
            catch (Exception )
            {
                lista = 0;
            }
            return lista;

        }
        public int EstDef()
        {
            int lista = 0;
            var listaParams = new List<SqlParameter>();

            String sql = "select * from dbo.CatEstados where EsDefault = 1 ";

            listaParams.Clear();

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista = Convert.ToInt32(reader["IdEst"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                lista = 0;
            }
            return lista;

        }
        public int CiuDef() 
        {
            int lista = 0;
            var listaParams = new List<SqlParameter>();

            String sql = "select * from dbo.CatCiudades where EsDefault = 1 ";

            listaParams.Clear();

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista = Convert.ToInt32(reader["IdCiu"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                lista = 0;
            }
            return lista;

        }
        public int ColDef()
        {
            int lista = 0;
            var listaParams = new List<SqlParameter>();

            String sql = "select * from dbo.CatColonias where EsDefault = 1 ";

            listaParams.Clear();

            try
            {
                using (var reader = Cmd.Comandos(conexion.AbreConexion(), sql, CommandType.Text, listaParams).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista = Convert.ToInt32(reader["IdCol"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                lista = 0;
            }
            return lista;

        }
    }
}