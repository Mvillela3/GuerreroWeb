using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Drawing;

namespace GuerreroWeb.Views.Inventarios
{
	public partial class Tallas : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios VtUsuario = new VtUsuarios();
		CtrlInvCatalogos ctrlInv = new CtrlInvCatalogos();
		CatTallas mTalla = new CatTallas();

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
				if (!VtUsuario.EntraTallas)
				{

					Response.Redirect("~/Inicio.aspx");

				}

				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				DdlLineaB.Visible = false;
				LblBusca2.Visible = false;
				DdlCategoriaB.Visible = false;


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
			string marca = "";

			if (DdlCategoriaB.SelectedValue.ToString() != string.Empty)
			{
				marca = DdlCategoriaB.SelectedValue.ToString();
			}


			List<VtTallas> vtTalla = ctrlInv.ListaTallas(TxtBuscar.Text, marca);

			if (vtTalla != null)
			{
				if (vtTalla.Count == 0)
				{
					return;
				}
				int largo = vtTalla[0].Categoria.Length;
				if (largo >= 5)
				{
					largo = 5;
				}

				if (vtTalla[0].Categoria.Substring(0, largo) == "Error")
				{
					MsgBox(vtTalla[0].Talla, "Modelos");
					return;
				}
				GvConsulta.DataSource = vtTalla;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hfmedio = GvConsulta.Rows[i].FindControl("HfMedios") as HiddenField;
					Image Immedio = GvConsulta.Rows[i].FindControl("ImgMedios") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.TallaMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.TallaDel;
					}
					if (Immedio != null && hfmedio != null)
					{
						if (Convert.ToBoolean(hfmedio.Value.ToString()))
						{
							Immedio.ImageUrl = @"~/Resources/Check1.png";
						}
						else
						{
							Immedio.ImageUrl = @"~/Resources/Check2.png";
						}
					}

				}
			}
		}
		private void AccionBtn()
		{
			BtnNuevo.Enabled = VtUsuario.TallaAdd;
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
			Session["AccTal"] = "N";
			HfId.Value = "0";
			TxtTalla.Text = "";
			TxtTalla.Focus();
			TxtRInicial.Text = "0";
			TxtRFinal.Text = "0";
			ChkMedios.Checked = false;

			DdlLinea.SelectedIndex = 0;
			if (DdlCategoria.Items.Count > 0)
			{
				DdlCategoria.SelectedIndex = 0;
			}

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
				DdlLineaB.Visible = false;
				LblBusca2.Visible = false;
				DdlCategoriaB.Visible = false;

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;
				LblBusca1.Visible = true;
				DdlLineaB.Visible = true;
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
				var respuesta = ctrlInv.TallaDel(id);
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
			Session["AccTal"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			HfId.Value = id.ToString();

			if (id != 0)
			{
				var respuesta = ctrlInv.Talla(id);
				if (respuesta != null || respuesta.Categoria != string.Empty)
				{
					int largo = respuesta.Categoria.Length;

					if (largo >= 5)
					{
						largo = 5;
					}

					if (respuesta.Categoria.Substring(0, largo) == "Error")
					{
						MsgBox(respuesta.Talla, "Modificar");
						return;
					}
					TxtTalla.Text = respuesta.Talla;
					TxtTalla.Focus();

					TxtRInicial.Text = respuesta.RangoIni.ToString();
					TxtRFinal.Text = respuesta.RangoFin.ToString();
					ChkMedios.Checked = respuesta.ManejaMed;

					DdlLinea.SelectedValue = respuesta.IdLinea.ToString();
					LlenaCategoria();

					if (DdlCategoria.Items.Count > 0)
					{
						DdlCategoria.SelectedValue = respuesta.IdCat.ToString();
					}

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

		protected void TxtTalla_TextChanged(object sender, EventArgs e)
		{
			if (TxtTalla.Text.Trim() == string.Empty)
			{
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
			}
			else
			{
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
			if (TxtTalla.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mTalla.IdTalla = Convert.ToInt32(HfId.Value.ToString());
			mTalla.Talla = TxtTalla.Text;
			mTalla.RangoIni = Convert.ToInt32(TxtRInicial.Text);
			mTalla.RangoFin = Convert.ToInt32(TxtRFinal.Text);
			mTalla.ManejaMed = ChkMedios.Checked;
			mTalla.IdCat = Convert.ToInt32(DdlCategoria.SelectedValue.ToString());

			RespuestaSQL respuesta = null;

			if (Session["AccTal"].ToString() != string.Empty)
			{
				if (Session["AccTal"].ToString() == "N")
				{
					respuesta = ctrlInv.TallaAdd(mTalla);
				}
				if (Session["AccTal"].ToString() == "U")
				{
					respuesta = ctrlInv.TallaMod(mTalla);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Color");
						return;

					}
					Session["AccTal"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccTal"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

		}
	}
}