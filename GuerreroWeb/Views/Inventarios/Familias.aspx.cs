using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuerreroWeb.Views.Inventarios
{
	public partial class Familias : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios VtUsuario = new VtUsuarios();
		CtrlInvCatalogos ctrlInv = new CtrlInvCatalogos();
		CatFamilias mFamilia = new CatFamilias();
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
				if (!VtUsuario.EntraFamilias)
				{

					Response.Redirect("~/Inicio.aspx");

				}

				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				DdlLineaB.Visible = false;
				LblBusca2.Visible = false;
				DdlCategoriaB.Visible = false;

				LblBusca1.Visible = false;
				LlenaLineas();
				LlenaCategoria();
				LlenaCategoria2();
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
			var vtFamilia = ctrlInv.ListaFamilias(TxtBuscar.Text, Convert.ToInt32(DdlLineaB.SelectedValue.ToString()), Convert.ToInt32(DdlCategoriaB.SelectedValue.ToString()));

			if (vtFamilia != null)
			{
				if (vtFamilia.Count == 0)
				{
					return;
				}
				int largo = vtFamilia[0].Categoria.Length;
				if (largo >= 5)
				{
					largo = 5;
				}

				if (vtFamilia[0].Categoria.Substring(0, largo) == "Error")
				{
					MsgBox(vtFamilia[0].Familia, "Familias");
					return;
				}
				GvConsulta.DataSource = vtFamilia;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hfactivo = GvConsulta.Rows[i].FindControl("HfActivo") as HiddenField;
					Image ImActivo = GvConsulta.Rows[i].FindControl("ImgActivo") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.FamiliaMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.FamiliaDel;
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
			BtnNuevo.Enabled = VtUsuario.CatAdd;
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void LlenaLineas()
		{
			DdlLinea.DataSource = null;
			DdlLinea.DataBind();
			DdlLineaB.DataSource = null;
			DdlLineaB.DataBind();
			List<CatLineas> lista = ctrlInv.DdlLineas();

			if (lista != null)
			{
				if (lista.Count > 0)
				{
					DdlLinea.DataSource = lista;
					DdlLinea.DataTextField = "Linea";
					DdlLinea.DataValueField = "IdLinea";
					DdlLinea.DataBind();
					DdlLinea.SelectedIndex = 0;

					DdlLineaB.DataSource = lista;
					DdlLineaB.DataTextField = "Linea";
					DdlLineaB.DataValueField = "IdLinea";
					DdlLineaB.DataBind();
					DdlLineaB.SelectedIndex = 0;
				}
			}
		}
		private void LlenaCategoria()
		{
			DdlCategoria.DataSource = null;
			DdlCategoria.DataBind();

			var lista = ctrlInv.DdlCategoria(Convert.ToInt32(DdlLinea.SelectedValue.ToString()));

			if (lista != null)
			{
				if (lista.Count > 0)
				{
					DdlCategoria.DataSource = lista;
					DdlCategoria.DataTextField = "Categoria";
					DdlCategoria.DataValueField = "IdCat";
					DdlCategoria.DataBind();
					DdlCategoria.SelectedIndex = 0;

				}
			}
		}
		private void LlenaCategoria2()
		{
			DdlCategoriaB.DataSource = null;
			DdlCategoriaB.DataBind();

			var lista = ctrlInv.DdlCategoria(Convert.ToInt32(DdlLineaB.SelectedValue.ToString()));

			if (lista != null)
			{
				if (lista.Count > 0)
				{
					DdlCategoriaB.DataSource = lista;
					DdlCategoriaB.DataTextField = "Categoria";
					DdlCategoriaB.DataValueField = "IdCat";
					DdlCategoriaB.DataBind();
					DdlCategoriaB.SelectedIndex = 0;

				}
			}
		}

		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccFam"] = "N";
			HfId.Value = "0";
			TxtFamilia.Text = "";
			DdlLinea.SelectedIndex = 0;

			TxtFamilia.Focus();

			VtMdl.Visible = true;
			MpeVtMdl.Show();

		}

		protected void BtnBuscar_Command(object sender, CommandEventArgs e)
		{
			if (TxtBuscar.Visible)
			{
				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				DdlLineaB.Visible = false;
				LblBusca1.Visible = false;
				LblBusca2.Visible = false;
				DdlCategoriaB.Visible = false;

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;
				DdlLineaB.Visible = true;
				LblBusca1.Visible = true;
				LblBusca2.Visible = true;
				DdlCategoriaB.Visible = true;

			}

		}

		protected void BtnConslta_Command(object sender, CommandEventArgs e)
		{
			LlenaTabla();
		}

		protected void DdlLineasB_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaCategoria2();
			LlenaTabla();
		}

		protected void DdlCategoriaB_SelectedIndexChanged(object sender, EventArgs e)
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
				var respuesta = ctrlInv.FamiliaDel(id);
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
			Session["AccFam"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			HfId.Value = id.ToString();

			if (id != 0)
			{
				var respuesta = ctrlInv.Familia(id);
				if (respuesta != null || respuesta.Familia != string.Empty)
				{

					if (respuesta.Categoria == "Error")
					{
						MsgBox(respuesta.Familia, "Modificar");
						return;
					}
					TxtFamilia.Text = respuesta.Familia;
					DdlLinea.SelectedValue = respuesta.IdLinea.ToString();
					DdlCategoria.SelectedValue = respuesta.IdCat.ToString();

					TxtFamilia.Focus();

					VtMdl.Visible = true;
					MpeVtMdl.Show();

				}
			}

		}

		protected void TxtFamilia_TextChanged(object sender, EventArgs e)
		{
			if (TxtFamilia.Text.Trim() == string.Empty)
			{
				//	ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				//LblMensaje1.Text = "";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
			}
			else
			{
				//ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				//LblMensaje1.Text = "";
				LblMensaje1.Visible = false;
				LblMensaje1.Attributes["Style"] = "display: 'none';";
			}

		}

		protected void DdlLinea_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaCategoria();
			VtMdl.Visible = true;
			MpeVtMdl.Show();

		}

		protected void BtnGuardar_Click(object sender, EventArgs e)
		{
			if (TxtFamilia.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mFamilia.IdCat = Convert.ToInt32(HfId.Value.ToString());
			mFamilia.Familia = TxtFamilia.Text;
			mFamilia.IdCat = Convert.ToInt32(DdlCategoria.SelectedValue.ToString());

			RespuestaSQL respuesta = null;

			if (Session["AccFam"].ToString() != string.Empty)
			{
				if (Session["AccFam"].ToString() == "N")
				{
					respuesta = ctrlInv.FamiliaAdd(mFamilia);
				}
				if (Session["AccFam"].ToString() == "U")
				{
					respuesta = ctrlInv.FamiliaMod(mFamilia);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Familia");
						return;

					}
					Session["AccFam"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccFam"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

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
	}
}