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
	public partial class Categorias : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios VtUsuario = new VtUsuarios();
		CtrlInvCatalogos ctrlInv = new CtrlInvCatalogos();
		CatCategorias mCatego= new CatCategorias();

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
				if (!VtUsuario.EntraCategoria)
				{

					Response.Redirect("~/Inicio.aspx");

				}

				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				DdlLineaB.Visible = false;
				LblBusca1.Visible = false;
				LlenaLineas();
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
			var vtCatego = ctrlInv.ListaCategorias(TxtBuscar.Text, Convert.ToInt32( DdlLineaB.SelectedValue.ToString()));

			if (vtCatego != null)
			{
				if (vtCatego.Count == 0)
				{
					return;
				}
				int largo = vtCatego[0].Linea.Length;
				if (largo >= 5)
				{
					largo = 5;
				}

				if (vtCatego[0].Linea.Substring(0, largo) == "Error")
				{
					MsgBox(vtCatego[0].Categoria, "Categorias");
					return;
				}
				GvConsulta.DataSource = vtCatego;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hfactivo = GvConsulta.Rows[i].FindControl("HfActivo") as HiddenField;
					Image ImActivo = GvConsulta.Rows[i].FindControl("ImgActivo") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.CatMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.CatDel;
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

		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccCat"] = "N";
			HfId.Value = "0";
			TxtCategoria.Text = "";
			DdlLinea.SelectedIndex = 0;

			TxtCategoria.Focus();

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

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;
				DdlLineaB.Visible = true;
				LblBusca1.Visible = true;

			}

		}

		protected void BtnConslta_Command(object sender, CommandEventArgs e)
		{
			LlenaTabla();
		}

		protected void DdlLineasB_SelectedIndexChanged(object sender, EventArgs e)
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
				var respuesta = ctrlInv.CategoriaDel(id);
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
			Session["AccCat"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			HfId.Value = id.ToString();

			if (id != 0)
			{
				var respuesta = ctrlInv.Categoria(id);
				if (respuesta != null || respuesta.Categoria != string.Empty)
				{
					int largo = respuesta.Categoria.Length;

					if (largo >= 5)
					{
						largo = 5;
					}

					if (respuesta.Categoria.Substring(0, largo) == "Error")
					{
						MsgBox(respuesta.Categoria, "Modificar");
						return;
					}
					TxtCategoria.Text = respuesta.Categoria;
					DdlLinea.SelectedValue = respuesta.IdLinea.ToString();

					TxtCategoria.Focus();

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

		protected void TxtCategoria_TextChanged(object sender, EventArgs e)
		{
			if (TxtCategoria.Text.Trim() == string.Empty)
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

		protected void BtnGuardar_Click(object sender, EventArgs e)
		{
			if (TxtCategoria.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mCatego.IdCat = Convert.ToInt32(HfId.Value.ToString());
			mCatego.Categoria = TxtCategoria.Text;
			mCatego.IdLinea = Convert.ToInt32(DdlLinea.SelectedValue.ToString());

			RespuestaSQL respuesta = null;

			if (Session["AccCat"].ToString() != string.Empty)
			{
				if (Session["AccCat"].ToString() == "N")
				{
					respuesta = ctrlInv.CategoriaAdd(mCatego);
				}
				if (Session["AccCat"].ToString() == "U")
				{
					respuesta = ctrlInv.CategoriaMod(mCatego);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Categoria");
						return;

					}
					Session["AccCat"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccCat"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

		}
	}
}