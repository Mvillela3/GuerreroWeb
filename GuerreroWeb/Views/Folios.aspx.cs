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
	public partial class Folios : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		CtrlConfig ctrlConfig = new CtrlConfig();
		CtrlModulos ctrlModulo = new CtrlModulos();
		VtUsuarios VtUsuario = new VtUsuarios();
		VtFolios VtFolios = new VtFolios();
		CatFolios mFolio = new CatFolios();

		private string usuario = "";

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
			//DivBuscar.Height = "0";

			ChecaUsuario(usuario);
			if (!IsPostBack)
			{
				if (!VtUsuario.EntraFolios)
				{

					Response.Redirect("~/Inicio.aspx");

				}
				DdlModuloB.Visible = false;
				LlenaSucursal();
				LlenaModulo();
				LlenaSubMod();
				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				//LlenaModulo();
				LlenaTabla();
			}
			AccionBtn();

		}
		private void ChecaUsuario(string usuario)
		{
			VtUsuario = ctrlusuario.VtUsuario(usuario);

			if (VtUsuario != null)
			{
				if (VtUsuario.Usuario == "Error")
				{
					Response.Redirect("~/Inicio.aspx");
				}
			}
			else
			{
				Response.Redirect("~/Inicio.aspx");
			}
		}
		private void LlenaTabla()
		{
			GvConsulta.DataSource = null;
			GvConsulta.DataBind();
			var vtFolio = ctrlConfig.ListaFolios(TxtBuscar.Text, Convert.ToInt32(DdlModuloB.SelectedValue.ToString()));

			if (vtFolio != null)
			{
				if (vtFolio.Count == 0)
				{
					return;
				}

				if (vtFolio[0].Modulo == "Error")
				{
					MsgBox(vtFolio[0].Movimiento, "Documentos");
					return;
				}
				GvConsulta.DataSource = vtFolio;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hfactivo = GvConsulta.Rows[i].FindControl("HfActivo") as HiddenField;
					Image ImActivo = GvConsulta.Rows[i].FindControl("ImgActivo") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.FolioMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.FolioDel;
					}
					if (ImActivo != null && hfactivo != null)
					{
						if (Convert.ToBoolean(hfactivo.Value.ToString()))
						{
							ImActivo.ImageUrl = @"~/Resources/Check1.png";
						}
						else
						{
							ImActivo.ImageUrl = @"~/Resources/Check2.png";
						}
					}

				}
			}
		}
		private void AccionBtn()
		{
			BtnNuevo.Enabled = VtUsuario.FolioAdd;
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void LlenaSucursal()
		{
			DdlSucursal.DataSource = null;
			DdlSucursal.DataBind();
			List<CatSucursales> lista = ctrlEmpresa.DdlSucursales();

			if (lista != null)
			{
				if (lista.Count > 0)
				{
					DdlSucursal.DataSource = lista;
					DdlSucursal.DataTextField = "Sucursal";
					DdlSucursal.DataValueField = "IdSuc";
					DdlSucursal.DataBind();
					DdlSucursal.SelectedIndex = 0;

				}
			}
		}
		private void LlenaModulo()
		{
			DdlModulo.DataSource = null;
			DdlModulo.DataBind();
			DdlModuloB.DataSource = null;
			DdlModuloB.DataBind();

			List<ModModulos> lista = ctrlModulo.DllModulos();

			if (lista != null)
			{
				if (lista.Count > 0)
				{
					DdlModulo.DataSource = lista;
					DdlModulo.DataTextField = "Nombre";
					DdlModulo.DataValueField = "IdMod";
					DdlModulo.DataBind();
					DdlModulo.SelectedIndex = 0;

					DdlModuloB.DataSource = lista;
					DdlModuloB.DataTextField = "Nombre";
					DdlModuloB.DataValueField = "IdMod";
					DdlModuloB.DataBind();
					DdlModuloB.SelectedIndex = 0;
				}
			}
		}
		private void LlenaSubMod()
		{
			DdlSubMod.DataSource = null;
			DdlSubMod.DataBind();
			List<ModMovimientos> lista = ctrlModulo.DllMovtos(Convert.ToInt32(DdlModulo.SelectedValue.ToString()));

			if (lista != null)
			{
				if (lista.Count > 0)
				{
					DdlSubMod.DataSource = lista;
					DdlSubMod.DataTextField = "Movimiento";
					DdlSubMod.DataValueField = "IdMovto";
					DdlSubMod.DataBind();
					DdlSubMod.SelectedIndex = 0;

				}
			}
		}

		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccFol"] = "N";
			HfId.Value = "0";
			TxtSerie.Text = "";
			DdlSubMod.SelectedIndex = 0;
			DdlSucursal.SelectedIndex = 0;
			DdlModulo.SelectedIndex = 0;
			TxtConse.Text = "0";

			VtMdl.Visible = true;
			MpeVtMdl.Show();

		}

		protected void BtnBuscar_Command(object sender, CommandEventArgs e)
		{
			if (TxtBuscar.Visible)
			{
				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				DdlModuloB.Visible = false;

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;
				LblBusca1.Visible = true;
				DdlModuloB.Visible = true;

			}

		}

		protected void BtnConslta_Command(object sender, CommandEventArgs e)
		{
			LlenaTabla();
		}

		protected void GvConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GvConsulta.PageIndex = e.NewPageIndex;
			LlenaTabla();
		}

		protected void GvConsulta_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.RowIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);


			if (id != 0)
			{
				var respuesta = ctrlConfig.FolioDel(id);
				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Eliminar");
						return;
					}
				}
			}
			LlenaTabla();

		}

		protected void GvConsulta_RowEditing(object sender, GridViewEditEventArgs e)
		{
			Session["AccFol"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			HfId.Value = id.ToString();

			if (id != 0)
			{
				var respuesta = ctrlConfig.Folio(id);
				if (respuesta != null || respuesta.Serie != string.Empty)
				{
					int largo = respuesta.Serie.Length;

					if (largo >= 5)
					{
						largo = 5;
					}

					if (respuesta.Serie.Substring(0, largo) == "Error")
					{
						MsgBox(respuesta.Serie, "Eliminar");
						return;
					}
					DdlModulo.SelectedValue = respuesta.IdMod.ToString();
					DdlSubMod.SelectedValue = respuesta.IdMovto.ToString();
					DdlSucursal.SelectedValue = respuesta.IdSuc.ToString();
					TxtConse.Text = respuesta.Consecutivo.ToString();
					TxtSerie.Text = respuesta.Serie;

					VtMdl.Visible = true;
					MpeVtMdl.Show();

				}
			}

		}

		protected void GvConsulta_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			// ### ACCIONES DENTRO DEL GV ###########################################################################
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				foreach (ImageButton button in e.Row.Cells[1].Controls.OfType<ImageButton>())
				{
					if (button.CommandName == "Delete")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvConsulta, "Delete$" + e.Row.RowIndex);
					}
					if (button.CommandName == "Edit")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvConsulta, "Edit$" + e.Row.RowIndex);
					}
					if (button.CommandName == "Update")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvConsulta, "Update$" + e.Row.RowIndex);
					}
					if (button.CommandName == "Cancel")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvConsulta, "Cancel$" + e.Row.RowIndex);
					}
					if (button.CommandName == "Select")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvConsulta, "Select$" + e.Row.RowIndex);
					}
				}

			}

		}

		protected void DdlModulo_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaSubMod();
			VtMdl.Visible = true;
			MpeVtMdl.Show();

		}

		protected void BtnGuardar_Click(object sender, EventArgs e)
		{
			if (TxtSerie.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mFolio.IdFolio = Convert.ToInt32(HfId.Value.ToString());
			mFolio.IdEmpresa = 1;
			mFolio.Serie = TxtSerie.Text;
			mFolio.Consecutivo = Convert.ToInt32(TxtConse.Text);
			mFolio.IdSuc = Convert.ToInt32(DdlSucursal.SelectedValue.ToString());
			mFolio.IdMod = Convert.ToInt32(DdlModulo.SelectedValue.ToString());
			mFolio.IdMovto = Convert.ToInt32(DdlSubMod.SelectedValue.ToString());

			RespuestaSQL respuesta = null;

			if (Session["AccFol"].ToString() != string.Empty)
			{
				if (Session["AccFol"].ToString() == "N")
				{
					respuesta = ctrlConfig.FolioAdd(mFolio);
				}
				if (Session["AccFol"].ToString() == "U")
				{
					respuesta = ctrlConfig.FolioMod(mFolio);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Documentos");
						return;

					}
					Session["AccFol"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccFol"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

		}

		protected void TxtSerie_TextChanged(object sender, EventArgs e)
		{
			if (TxtSerie.Text.Trim() == string.Empty)
			{
				//	ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
			}
			else
			{
				//ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: 'none';";
			}

		}

        protected void DdlModuloB_SelectedIndexChanged(object sender, EventArgs e)
        {
			LlenaTabla();
        }
    }
}