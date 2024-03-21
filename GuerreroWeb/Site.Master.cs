using GuerreroWeb.Controllers;
using GuerreroWeb.Models;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuerreroWeb
{
    public partial class SiteMaster : MasterPage
    {
        CtrlUsuarios Usuarios = new CtrlUsuarios();
        VtUsuarios VUsuario = new VtUsuarios();
        ModConfig mConfig = new ModConfig();
        CtrlConfig ctrlConfig = new CtrlConfig();
		string Ruta1 = Path.GetPathRoot(@"\");
		string Ruta2 = Path.GetPathRoot(@"\") + @"/Construccion.aspx";

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
                Response.Redirect(Ruta1 +"/Default.aspx");
            }
            if (!IsPostBack)
            {
                mConfig = ctrlConfig.Configuracion(1);
                VUsuario = Usuarios.VtUsuario(usuario);
            }

            if (usuarioDet == null)
            {
                Response.Redirect(Ruta1 + "/Default.aspx");
            }

            if (usuarioDet.Usuario == "Error")
            {
                Response.Redirect("~/Default.aspx");
            }


            if (!IsPostBack)
            {
                LblNomUsuario.Text = "Bienvenido " + VUsuario.Nombre;
                MenuPermiso(VUsuario);

            }
        }

        protected void BtnInicio_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(Ruta1 + "/Inicio.aspx");
        }

        protected void BtnCerrarS_Command(object sender, CommandEventArgs e)
        {
            Session["Usuario"] = "";
            Response.Redirect(Ruta1+"/Login.aspx");

        }
        private void MenuPermiso(VtUsuarios vUsuarios)
        {

			// modulo CXC
			LbCxc.Visible = false;
			LbCxcAbonos.Visible = false;
			LbCxcCargos.Visible = false;

			// modulo Ventas
			LbVentas.Visible = false;
			LbVentasPed.Visible = false;
			LbVentasVen.Visible = false;
			LbVentasPed.Visible = false;
			LbClientes.Visible = false;

			// modulo Inventarios
			LbInv.Visible = false;
			LbInvMInv.Visible = false;
			LbInvArticulos.Visible = false;
			LbInvLineas.Visible = false;
			LbInvCat.Visible = false;
			LbInvFam.Visible = false;
			LbInvMarcas.Visible = false;
			LbInvModelos.Visible = false;
			LbInvCol.Visible = false;
			LbInvTal.Visible = false;
			LbInvUni.Visible = false;

			// modulo Compras
			LbCompras.Visible = false;
			LbComprasOC.Visible = false;
			LbComprasCom.Visible = false;
			LbComprasDev.Visible = false;
			LbProveedores.Visible = false;

			// modulo CXP
			LbCxp.Visible = false;
			LbCxpCargos.Visible = false;
			LbCxpAbonos.Visible = false;

			// modulo Configuraciones
			LbConf.Visible = false;
			LbConfEmpresa.Visible = false;
			LbConfSist.Visible = false;
			LbConfMod.Visible = false;
			LbConfMovtos.Visible = false;
			LbConfFolios.Visible = false;
			LbConfUsuarios.Visible = false;
			LbConfPerfiles.Visible = false;
			LbConfSuc.Visible = false;
			LbConfAlm.Visible = false;
			LbConfDeptos.Visible = false;
			LbConfPais.Visible = false;
			LbConfEst.Visible = false;
			LbConfCiu.Visible = false;
			LbConfCol.Visible = false;
			LbCajas.Visible = false;


			// modulo contabilidad
			LbContabilidad.Visible = false;
			LbDiario.Visible = false;
			LbIngresos.Visible = false;
			LbEgreso.Visible = false;
			LbFPago.Visible = false;
			LbMPago.Visible = false;
			LbImpuestos.Visible = false;
			LbRFiscal.Visible = false;

			// modulo wati
			LbWati.Visible = false;
            LbWatiConsulta.Visible = false;
            LbWatiEnvios.Visible = false;
            LbWatiPlan.Visible = false;
			LbAuto.Visible = false;

			// modulo Reportes
            LbRep.Visible = false;
			LbRepCxc.Visible = false;
			LbRepVen.Visible = false;
			LbRepInv.Visible = false;
			LbRepCxp.Visible = false;
			LbRepConta.Visible = false;


			// modulo CXC
			if (vUsuarios.EntraCxc && mConfig.ActivoCXC)
            {
				LbCxc.Visible = vUsuarios.EntraCxc;
			}
			LbCxcAbonos.Visible = vUsuarios.EntraCxc;
            LbCxcCargos.Visible = vUsuarios.EntraCxc;

			// modulo Ventas
			if (vUsuarios.EntraVentas && mConfig.ActivoVenta)
            {
				LbVentas.Visible = vUsuarios.EntraVentas;
			}
			LbVentasPed.Visible = vUsuarios.EntraVentas;
            LbVentasVen.Visible = vUsuarios.EntraVentas;
            LbVentasPed.Visible = vUsuarios.EntraVentas;
			LbClientes.Visible = vUsuarios.EntraClientes;

			// modulo Inventarios
			if (vUsuarios.EntraInv && mConfig.ActivoInv)
            {
				LbInv.Visible = vUsuarios.EntraInv;
			}
			LbInvMInv.Visible = vUsuarios.EntraMovInv;
			LbInvArticulos.Visible = vUsuarios.EntraArticulos;
            LbInvLineas.Visible = vUsuarios.EntraLineas;
            LbInvCat.Visible = vUsuarios.EntraCategoria;
			LbInvFam.Visible = vUsuarios.EntraFamilias;
			LbInvMarcas.Visible = vUsuarios.EntraMarcas;
            LbInvModelos.Visible = vUsuarios.EntraModelos;
            LbInvCol.Visible = vUsuarios.EntraColores;
			LbInvTal.Visible = vUsuarios.EntraTallas;
			LbInvUni.Visible = vUsuarios.EntraUnidad;

			// modulo Compras
			if (vUsuarios.EntraCompras && mConfig.ActivoComp)
            {
				LbCompras.Visible = vUsuarios.EntraCompras;
			}
			LbComprasOC.Visible = vUsuarios.EntraCompras;
            LbComprasCom.Visible = vUsuarios.EntraCompras;
            LbComprasDev.Visible = vUsuarios.EntraCompras;
			LbProveedores.Visible = vUsuarios.EntraProv;

			// modulo CXP
			if (vUsuarios.EntraCxp && mConfig.ActivoCXP)
            {
				LbCxp.Visible = vUsuarios.EntraCxp;
			}
			LbCxpCargos.Visible = vUsuarios.EntraCxp;
            LbCxpAbonos.Visible = vUsuarios.EntraCxp;

			// modulo Reportes
			if (vUsuarios.EntraReportes && mConfig.ActivoReporte)
            {
				LbRep.Visible = vUsuarios.EntraReportes;
			}
			LbRepCxc.Visible = vUsuarios.EntraRepCxc;
            LbRepVen.Visible = vUsuarios.EntraRepVen;
            LbRepInv.Visible = vUsuarios.EntraRepInv;
            LbRepCxp.Visible = vUsuarios.EntraRepCxp;
			LbRepConta.Visible = vUsuarios.EntraRepConta;

			// modulo Configuracion
			if (vUsuarios.EntraConfiguraciones)
			{
				LbConf.Visible = vUsuarios.EntraConfiguraciones;
			}
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
			LbCajas.Visible = vUsuarios.EntraCajas; ;

			// modulo Contabilidad
			if (vUsuarios.EntraConta && mConfig.ActivoCont)
            {
				LbContabilidad.Visible = vUsuarios.EntraConta;
			}
            LbDiario.Visible = vUsuarios.EntraConta;
			LbIngresos.Visible = vUsuarios.EntraConta;
			LbEgreso.Visible = vUsuarios.EntraConta;
            LbFPago.Visible = vUsuarios.EntraFormPago;
            LbMPago.Visible = vUsuarios.EntraMetPago;
            LbImpuestos.Visible = vUsuarios.EntraImp;
            LbRFiscal.Visible = vUsuarios.EntraRFiscal;

			// modulo Wati
			if (vUsuarios.EntraWati && mConfig.ActivoWati)
            {
				LbWati.Visible = vUsuarios.EntraWati;
			}
			LbWatiConsulta.Visible = vUsuarios.EntraWatiCons;
			LbWatiEnvios.Visible = vUsuarios.EntraWatiEnv;
			LbWatiPlan.Visible = vUsuarios.EntraWatiPlan;
            LbAuto.Visible = vUsuarios.EntraWatiAuto;

		}

		protected void LbCxcCargos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbCxcAbonos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbDiario_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbIngresos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbEgreso_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LBCtaContable_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbFPago_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbMPago_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbImpuestos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbRFiscal_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbCxpCargos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbCxpAbonos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbComprasOC_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbComprasCom_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbComprasDev_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbProveedores_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbInvMInv_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbInvArticulos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Articulos.aspx");
		}

		protected void LbInvLineas_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Lineas.aspx");

		}

		protected void LbInvCat_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Categorias.aspx");

		}

		protected void LbInvMarcas_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Marcas.aspx");
		}

		protected void LbInvModelos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Modelos.aspx");
		}

		protected void LbInvCol_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Colores.aspx");
		}

		protected void LbInvTal_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Tallas.aspx");
		}

		protected void LbInvUni_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Unidad.aspx");
		}

		protected void LbVentasPed_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbVentasVen_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbVentasDev_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbClientes_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbWatiEnvios_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbWatiConsulta_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbWatiPlan_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbAuto_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbRepCxc_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbRepVen_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbRepInv_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbRepCom_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbRepCxp_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbRepConta_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta2);
		}

		protected void LbConfEmpresa_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1+ "/Views/Empresas.aspx");
		}

		protected void LbConfSist_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Configuracion.aspx");
		}

		protected void LbConfMovtos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Movimientos.aspx");
		}

		protected void LbConfFolios_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Folios.aspx");
		}

		protected void LbConfUsuarios_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Usuarios.aspx");
		}

		protected void LbConfPerfiles_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/VstPerfiles.aspx");
		}

		protected void LbConfSuc_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Sucursales.aspx");
		}

		protected void LbConfAlm_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Almacenes.aspx");
		}

		protected void LbConfDeptos_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Departamentos.aspx");
		}

		protected void LbCajas_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Cajas.aspx");
		}

		protected void LbConfPais_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Paises.aspx");
		}

		protected void LbConfEst_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Estados.aspx");
		}

		protected void LbConfCiu_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Ciudades.aspx");
		}

		protected void LbConfCol_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Colonias.aspx");
		}

		protected void LbInvFam_Command(object sender, CommandEventArgs e)
		{
			Response.Redirect(Ruta1 + "/Views/Inventarios/Familias.aspx");
		}
	}
}