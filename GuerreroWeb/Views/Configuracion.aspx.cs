using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuerreroWeb.Views
{
	public partial class Configuracion : System.Web.UI.Page
	{
		CtrlUsuarios ctrlUsuario = new CtrlUsuarios();
		VtUsuarios vtUsuario = new VtUsuarios();
		ModConfig mConfig = new ModConfig();
		CtrlConfig ctrlConfig = new CtrlConfig();

		private string usuario = "";
		private string Accion = "";
		private int IdConfig = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["Usuario"] != null)
			{
				usuario = Session["Usuario"].ToString();
			}
			if (usuario.Length == 0)
			{
				Response.Redirect("~/Login.aspx");
			}

			ChecaUsuario(usuario);
			if (Session["AccConf"] == null)
			{
				Accion = "O";
				//Response.Redirect("~/Views/Usuarios.aspx");
			}
			else
			{
				Accion = Session["AccConf"].ToString();
			}

			if (!vtUsuario.EntraConfiguraciones)
			{

				Response.Redirect("~/Views/Inicio.aspx");
			}

			IdConfig = 1;

			if (!IsPostBack)
			{
				LlenaConsulta(IdConfig);
			}
			AccionBtn();
			if (Accion == "U")
			{
				BtnCancelar.OnClientClick = "return confirm('¿Esta seguro que desea Deshacer los Cambios?');";
			}
			else if (Accion == "N")
			{
				BtnCancelar.OnClientClick = "return confirm('¿Esta seguro que desea Salir?');";
			}
			else
			{
				BtnCancelar.OnClientClick = "";
			}

		}
		private void ChecaUsuario(string usuario)
		{
			vtUsuario = ctrlUsuario.VtUsuario(usuario);

			if (vtUsuario != null)
			{
				if (vtUsuario.Usuario == "Error")
				{
					Response.Redirect("~/Inicio.aspx");
				}
			}
			else
			{
				Response.Redirect("~/Inicio.aspx");
			}
			if (Accion == "U")
			{
				BtnGuardar.Enabled = vtUsuario.EntraConfiguraciones;
			}
			if (Accion == "N")
			{
				BtnGuardar.Enabled = vtUsuario.EntraConfiguraciones;
			}
		}
		private void AccionBtn()
		{
			if (Accion == "O")
			{
				BtnEditar.Enabled = vtUsuario.EntraConfiguraciones;
				BtnGuardar.Enabled = false;

				ChkCompras.Enabled = false;
				ChkConta.Enabled = false;
				ChkCXC.Enabled = false;
				ChkCXP.Enabled = false;
				ChkInv.Enabled = false;
				ChkSalida0.Enabled = false;
				ChkVenta0.Enabled = false;
				ChkVentas.Enabled = false;
				ChkReporte.Enabled = false;
				ChkWati.Enabled = false;
				DdlDecimal1.Enabled = false;
				DdlDecimal2.Enabled = false;

			}
			if (Accion == "U")
			{
				BtnEditar.Enabled = false;
				BtnGuardar.Enabled = true;

				ChkCompras.Enabled = true;
				ChkConta.Enabled = true;
				ChkCXC.Enabled = true;
				ChkCXP.Enabled = true;
				ChkInv.Enabled = true;
				ChkSalida0.Enabled = true;
				ChkVenta0.Enabled = true;
				ChkVentas.Enabled = true;
				ChkReporte.Enabled = true;
				ChkWati.Enabled = true;
				DdlDecimal1.Enabled = true;
				DdlDecimal2.Enabled = true;

			}
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void LlenaConsulta(int IdConfig)
		{
			var lista = ctrlConfig.Configuracion(IdConfig);

			if (lista != null)
			{
				//if (lista.IdConf == "Error")
				//{
				//	return;
				//}

				ChkCompras.Checked = lista.ActivoComp;
				ChkConta.Checked = lista.ActivoCont;
				ChkCXC.Checked = lista.ActivoCXC;
				ChkCXP.Checked = lista.ActivoCXP;
				ChkInv.Checked = lista.ActivoInv;
				ChkSalida0.Checked = lista.SalidaExt0;
				ChkVenta0.Checked = lista.VendeExt0;
				ChkVentas.Checked = lista.ActivoVenta;
				ChkWati.Checked = lista.ActivoWati;
				ChkReporte.Checked = lista.ActivoReporte;


				DdlDecimal1.SelectedValue = lista.Decimales1.ToString();
				DdlDecimal2.SelectedValue = lista.Decimales2.ToString();

			}
		}

		protected void BtnEditar_Command(object sender, CommandEventArgs e)
		{
			Session["AccConf"] = "U";
			Session["IdConf"] = 1;
			Response.Redirect("~/Views/Configuracion.aspx");

		}

		protected void BtnGuardar_Command(object sender, CommandEventArgs e)
		{

			mConfig.IdConf = 1;
			mConfig.ActivoInv = ChkInv.Checked;
			mConfig.ActivoCXC = ChkCXC.Checked;
			mConfig.ActivoCont = ChkConta.Checked;
			mConfig.ActivoCXP = ChkCXP.Checked;
			mConfig.ActivoComp = ChkCompras.Checked;
			mConfig.ActivoVenta = ChkVentas.Checked;
			mConfig.Decimales1 = Convert.ToInt32(DdlDecimal1.SelectedValue.ToString());
			mConfig.Decimales2 = Convert.ToInt32(DdlDecimal2.SelectedValue.ToString());
			mConfig.VendeExt0 = ChkVenta0.Checked;
			mConfig.SalidaExt0 = ChkSalida0.Checked;
			mConfig.ActivoWati = ChkWati.Checked;
			mConfig.ActivoReporte = ChkReporte.Checked;

			RespuestaSQL respuesta = null;

			respuesta = ctrlConfig.ConfigMod(mConfig);

			if (respuesta != null)
			{
				if (respuesta.Codigo != 0)
				{
					MsgBox(respuesta.Mensaje, "Configuracion");
					return;
				}
				Session["AccConf"] = "O";

				Session["IdConf"] = 1;
				Response.Redirect("~/Views/Configuracion.aspx");

			}

		}

		protected void BtnCancelar_Command(object sender, CommandEventArgs e)
		{
			if (Accion == "O")
			{
				Session["AccConf"] = null;
				Session["IdConf"] = null;
				Response.Redirect("~/Inicio.aspx");
			}
			if (Accion == "U")
			{
				Session["AccConf"] = "O";

				Response.Redirect("~/Views/Configuracion.aspx");

			}

		}
	}
}