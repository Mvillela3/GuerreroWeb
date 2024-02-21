using GuerreroWeb.Controllers;
using GuerreroWeb.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuerreroWeb
{
    public partial class SiteMaster : MasterPage
    {
        CtrlUsuarios Usuarios = new CtrlUsuarios();
        VtUsuarios VUsuario = new VtUsuarios();
        private ModUsuarios mUsuario = new ModUsuarios();
        VtUsuarios usuarioDet = new VtUsuarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            string usuario = "";

            if (Session["Usuario"] != null)
            {
                usuario = Session["Usuario"].ToString();
            }


            if (usuario.Length == 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!IsPostBack)
            {
                VUsuario = Usuarios.VtUsuario(usuario);
            }

            if (usuarioDet == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (usuarioDet.Usuario == "Error")
            {
                Response.Redirect("~/Default.aspx");
            }

            //VUsuario.IdUsu = usuarioDet.IdUsu;
            //VUsuario.Usuario = usuarioDet.Usuario;
            //VUsuario.Nombre = usuarioDet.Nombre;
            //VUsuario.PWD = usuarioDet.PWD;
            //VUsuario.IdPerfil = usuarioDet.IdPerfil;
            //VUsuario.IdDepto = usuarioDet.IdDepto;
            //VUsuario.Email = usuarioDet.Email;
            //VUsuario.Telefono = usuarioDet.Telefono;
            //VUsuario.Estatus = usuarioDet.Estatus;
            //VUsuario.Perfil = usuarioDet.Perfil;
            //VUsuario.EntraCxc = usuarioDet.EntraCxc;
            //VUsuario.CxcAdd = usuarioDet.CxcAdd;
            //VUsuario.CxcMod = usuarioDet.CxcMod;
            //VUsuario.CxcCan = usuarioDet.CxcCan;
            //VUsuario.CxcDel = usuarioDet.CxcDel;
            //VUsuario.EntraVentas = usuarioDet.EntraVentas;
            //VUsuario.VentaAdd = usuarioDet.VentaAdd;
            //VUsuario.VentaMod = usuarioDet.VentaMod;
            //VUsuario.VentaCan = usuarioDet.VentaCan;
            //VUsuario.VentaDel = usuarioDet.VentaDel;
            //VUsuario.EntraInv = usuarioDet.EntraInv;
            //VUsuario.InvAdd = usuarioDet.InvAdd;
            //VUsuario.InvMod = usuarioDet.InvMod;
            //VUsuario.InvCan = usuarioDet.InvCan;
            //VUsuario.InvDel = usuarioDet.InvDel;
            //VUsuario.EntraArticulos = usuarioDet.EntraArticulos;
            //VUsuario.ArticuloAdd = usuarioDet.ArticuloAdd;
            //VUsuario.ArticuloMod = usuarioDet.ArticuloMod;
            //VUsuario.ArticuloDel = usuarioDet.ArticuloDel;
            //VUsuario.EntraLineas = usuarioDet.EntraLineas;
            //VUsuario.LineasAdd = usuarioDet.LineasAdd;
            //VUsuario.LineasMod = usuarioDet.LineasMod;
            //VUsuario.LineasDel = usuarioDet.LineasDel;
            //VUsuario.EntraCategoria = usuarioDet.EntraCategoria;
            //VUsuario.CatAdd = usuarioDet.CatAdd;
            //VUsuario.CatMod = usuarioDet.CatMod;
            //VUsuario.CatDel = usuarioDet.CatDel;
            //VUsuario.EntraMarcas = usuarioDet.EntraMarcas;
            //VUsuario.MarcasAdd = usuarioDet.MarcasAdd;
            //VUsuario.MarcasMod = usuarioDet.MarcasMod;
            //VUsuario.MarcasDel = usuarioDet.MarcasDel;
            //VUsuario.EntraModelos = usuarioDet.EntraModelos;
            //VUsuario.ModelosAdd = usuarioDet.ModelosAdd;
            //VUsuario.ModelosMod = usuarioDet.ModelosMod;
            //VUsuario.ModelosDel = usuarioDet.ModelosDel;
            //VUsuario.EntraColores = usuarioDet.EntraColores;
            //VUsuario.ColoresAdd = usuarioDet.ColoresAdd;
            //VUsuario.ColoresMod = usuarioDet.ColoresMod;
            //VUsuario.ColoresDel = usuarioDet.ColoresDel;
            //VUsuario.EntraUnidad = usuarioDet.EntraUnidad;
            //VUsuario.UnidadAdd = usuarioDet.UnidadAdd;
            //VUsuario.UnidadMod = usuarioDet.UnidadMod;
            //VUsuario.UnidadDel = usuarioDet.UnidadDel;
            //VUsuario.EntraCompras = usuarioDet.EntraCompras;
            //VUsuario.CompraAdd = usuarioDet.CompraAdd;
            //VUsuario.CompraMod = usuarioDet.CompraMod;
            //VUsuario.CompraCan = usuarioDet.CompraCan;
            //VUsuario.CompraDel = usuarioDet.CompraDel;
            //VUsuario.EntraCxp = usuarioDet.EntraCxp;
            //VUsuario.CxpAdd = usuarioDet.CxpAdd;
            //VUsuario.CxpMod = usuarioDet.CxpMod;
            //VUsuario.CxpCan = usuarioDet.CxpCan;
            //VUsuario.CxpDel = usuarioDet.CxpDel;
            //VUsuario.EntraReportes = usuarioDet.EntraReportes;
            //VUsuario.EntraRepCxc = usuarioDet.EntraRepCxc;
            //VUsuario.EntraRepVen = usuarioDet.EntraRepVen;
            //VUsuario.EntraRepInv = usuarioDet.EntraRepInv;
            //VUsuario.EntraRepCom = usuarioDet.EntraRepCom;
            //VUsuario.EntraRepCxp = usuarioDet.EntraRepCxp;
            //VUsuario.EntraWati = usuarioDet.EntraWati;
            //VUsuario.EntraWatiEnv = usuarioDet.EntraWatiEnv;
            //VUsuario.EntraWatiCons = usuarioDet.EntraWatiCons;
            //VUsuario.EntraWatiPlan = usuarioDet.EntraWatiPlan;
            //VUsuario.EntraWatiAuto = usuarioDet.EntraWatiAuto;
            //VUsuario.WatiAdd = usuarioDet.WatiAdd;
            //VUsuario.WatiDel = usuarioDet.WatiDel;
            //VUsuario.WatiMod = usuarioDet.WatiMod;
            //VUsuario.EntraConfiguraciones = usuarioDet.EntraConfiguraciones;
            //VUsuario.EntraEmpresa = usuarioDet.EntraEmpresa;
            //VUsuario.EmpresaMod = usuarioDet.EmpresaMod;
            //VUsuario.EntraSistema = usuarioDet.EntraSistema;
            //VUsuario.EntraModulos = usuarioDet.EntraModulos;
            //VUsuario.ModAdd = usuarioDet.ModAdd;
            //VUsuario.ModMod = usuarioDet.ModMod;
            //VUsuario.ModDel = usuarioDet.ModDel;
            //VUsuario.EntraMovtos = usuarioDet.EntraMovtos;
            //VUsuario.MovtosAdd = usuarioDet.MovtosAdd;
            //VUsuario.MovtosMod = usuarioDet.MovtosMod;
            //VUsuario.MovtosDel = usuarioDet.MovtosDel;
            //VUsuario.EntraFolios = usuarioDet.EntraFolios;
            //VUsuario.FolioAdd = usuarioDet.FolioAdd;
            //VUsuario.FolioMod = usuarioDet.FolioMod;
            //VUsuario.FolioDel = usuarioDet.FolioDel;
            //VUsuario.EntraUsuarios = usuarioDet.EntraUsuarios;
            //VUsuario.UsuarioAdd = usuarioDet.UsuarioAdd;
            //VUsuario.UsuarioMod = usuarioDet.UsuarioMod;
            //VUsuario.UsuarioDel = usuarioDet.UsuarioDel;
            //VUsuario.EntraPerfiles = usuarioDet.EntraPerfiles;
            //VUsuario.PerfilAdd = usuarioDet.PerfilAdd;
            //VUsuario.PerfilMod = usuarioDet.PerfilMod;
            //VUsuario.PerfilDel = usuarioDet.PerfilDel;
            //VUsuario.EntraSucursales = usuarioDet.EntraSucursales;
            //VUsuario.SucursalAdd = usuarioDet.SucursalAdd;
            //VUsuario.SucursalMod = usuarioDet.SucursalMod;
            //VUsuario.SucursalDel = usuarioDet.SucursalDel;
            //VUsuario.EntraAlmacenes = usuarioDet.EntraAlmacenes;
            //VUsuario.AlmacenAdd = usuarioDet.AlmacenAdd;
            //VUsuario.AlmacenMod = usuarioDet.AlmacenMod;
            //VUsuario.AlmacenDel = usuarioDet.AlmacenDel;
            //VUsuario.EntraPaises = usuarioDet.EntraPaises;
            //VUsuario.PaisAdd = usuarioDet.PaisAdd;
            //VUsuario.PaisMod = usuarioDet.PaisMod;
            //VUsuario.PaisDel = usuarioDet.PaisDel;
            //VUsuario.EntraEstados = usuarioDet.EntraEstados;
            //VUsuario.EstadoAdd = usuarioDet.EstadoAdd;
            //VUsuario.EstadoMod = usuarioDet.EstadoMod;
            //VUsuario.EstadoDel = usuarioDet.EstadoDel;
            //VUsuario.EntraCiudades = usuarioDet.EntraCiudades;
            //VUsuario.CiudadAdd = usuarioDet.CiudadAdd;
            //VUsuario.CiudadMod = usuarioDet.CiudadMod;
            //VUsuario.CiudadDel = usuarioDet.CiudadDel;
            //VUsuario.EntraColonias = usuarioDet.EntraColonias;
            //VUsuario.ColoniaAdd = usuarioDet.ColoniaAdd;
            //VUsuario.ColoniaMod = usuarioDet.ColoniaMod;
            //VUsuario.ColoniaDel = usuarioDet.ColoniaDel;

            if (!IsPostBack)
            {
                LblNomUsuario.Text = "Bienvenido " + VUsuario.Nombre;
                MenuPermiso(VUsuario);

            }
        }

        protected void BtnInicio_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("~/Inicio.aspx");
        }

        protected void BtnCerrarS_Command(object sender, CommandEventArgs e)
        {
            Session["Usuario"] = "";
            Response.Redirect("~/Login.aspx");

        }
        private void MenuPermiso(VtUsuarios vUsuarios)
        {
            LbCxc.Visible = vUsuarios.EntraCxc;
            LbCxcAbonos.Visible = vUsuarios.EntraCxc;
            LbCxcCargos.Visible = vUsuarios.EntraCxc;
            LbVentas.Visible = vUsuarios.EntraVentas;
            LbVentasPed.Visible = vUsuarios.EntraVentas;
            LbVentasVen.Visible = vUsuarios.EntraVentas;
            LbVentasPed.Visible = vUsuarios.EntraVentas;
            LbInv.Visible = vUsuarios.EntraInv;
            LbInvArticulos.Visible = vUsuarios.EntraArticulos;
            LbInvLineas.Visible = vUsuarios.EntraLineas;
            LbInvCat.Visible = vUsuarios.EntraCategoria;
            LbInvMarcas.Visible = vUsuarios.EntraMarcas;
            LbInvModelos.Visible = vUsuarios.EntraModelos;
            LbInvCol.Visible = vUsuarios.EntraColores;
            LbUnidades.Visible = vUsuarios.EntraUnidad;
            LbCompras.Visible = vUsuarios.EntraCompras;
            LbComprasOC.Visible = vUsuarios.EntraCompras;
            LbComprasCom.Visible = vUsuarios.EntraCompras;
            LbComprasDev.Visible = vUsuarios.EntraCompras;
            LbCxp.Visible = vUsuarios.EntraCxp;
            LbCxpCargos.Visible = vUsuarios.EntraCxp;
            LbCxpAbonos.Visible = vUsuarios.EntraCxp;
            LbRep.Visible = vUsuarios.EntraReportes;
            LbRepCxc.Visible = vUsuarios.EntraRepCxc;
            LbRepVen.Visible = vUsuarios.EntraRepVen;
            LbRepInv.Visible = vUsuarios.EntraRepInv;
            LbRepCxp.Visible = vUsuarios.EntraRepCxp;
            LbConf.Visible = vUsuarios.EntraConfiguraciones;
            LbConfEmpresa.Visible = vUsuarios.EntraEmpresa;
            LbConfSist.Visible = vUsuarios.EntraSistema;
            LbConfMod.Visible = vUsuarios.EntraModulos;
            LbConfMovtos.Visible = vUsuarios.EntraMovtos;
            LbConfFolios.Visible = vUsuarios.EntraFolios;
            LbConfUsuarios.Visible = vUsuarios.EntraUsuarios;
            LbConfPerfiles.Visible = vUsuarios.EntraPerfiles;
            LbConfSuc.Visible = vUsuarios.EntraSucursales;
            LbConfAlm.Visible = vUsuarios.EntraAlmacenes;
            LbConfDeptos.Visible = vUsuarios.EntraDepto;
            LbConfPais.Visible = vUsuarios.EntraPaises;
            LbConfEst.Visible = vUsuarios.EntraEstados;
            LbConfCiu.Visible = vUsuarios.EntraCiudades;
            LbConfCol.Visible = vUsuarios.EntraColonias;

        }
    }
}