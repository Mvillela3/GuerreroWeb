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
	public partial class Articulos : System.Web.UI.Page
	{
		CtrlUsuarios ctrlUsuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		CtrlInvCatalogos ctrlInv = new CtrlInvCatalogos();
		VtUsuarios VtUsuario = new VtUsuarios();
		VtArticulos VtArticulo = new VtArticulos();
		CatArticulos MArticulo = new CatArticulos();
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
				if (!VtUsuario.EntraArticulos)
				{

					Response.Redirect("~/Inicio.aspx");

				}
				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				DdlLineaB.Visible = false;
				LblBusca2.Visible = false;
				DdlCategoriaB.Visible = false;
				LblBusca3.Visible = false;
				DdlFamiliaB.Visible = false;
				LlenaLineas();
				LlenaCategoria();
				LlenaFamilia();

				LlenaTabla();
			}
			AccionBtn();

		}
		private void ChecaUsuario(string usuario)
		{
			VtUsuario = ctrlUsuario.VtUsuario(usuario);

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
			string Lin = DdlLineaB.SelectedValue.ToString();
			string cat = DdlCategoriaB.SelectedValue.ToString();
			string fam = DdlFamiliaB.SelectedValue.ToString();


			var vtArticulo = ctrlInv.ListaArticulos(TxtBuscar.Text, Lin, cat, fam);

			if (vtArticulo != null)
			{
				if (vtArticulo.Count == 0)
				{
					return;
				}

				int largo = vtArticulo[0].Codigo.Length;
				if (largo >= 5)
				{
					largo = 5;
				}

				if (vtArticulo[0].Codigo.Substring(0, largo) == "Error")
				{
					MsgBox(vtArticulo[0].Descripcion, "Articulos");
					return;
				}
				GvConsulta.DataSource = vtArticulo;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.ArticuloMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.ArticuloDel;
					}
				}
			}
		}
		private void AccionBtn()
		{
			BtnNuevo.Enabled = VtUsuario.ArticuloAdd;
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void LlenaLineas()
		{
			DdlLineaB.DataSource = null;
			DdlLineaB.DataBind();
			List<CatLineas> lista = ctrlInv.DdlLineas();

			if (lista != null)
			{
				if (lista.Count > 0)
				{

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
		private void LlenaFamilia()
		{
			DdlFamiliaB.DataSource = null;
			DdlFamiliaB.DataBind();

			var lista = ctrlInv.DdlFamilia(Convert.ToInt32(DdlCategoriaB.SelectedValue.ToString()));

			if (lista != null)
			{
				if (lista.Count > 0)
				{
					DdlFamiliaB.DataSource = lista;
					DdlFamiliaB.DataTextField = "Familia";
					DdlFamiliaB.DataValueField = "IdFam";
					DdlFamiliaB.DataBind();
					DdlFamiliaB.SelectedIndex = 0;

				}
			}
		}


		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccArt"] = "N";
			Session["IdArt"] = "0";
			Response.Redirect("~/Views/Inventarios/ArticulosDet.aspx");

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
				LblBusca3.Visible = false;
				DdlFamiliaB.Visible = false;

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;
				LblBusca1.Visible = true;
				DdlLineaB.Visible = true;
				LblBusca2.Visible = true;
				DdlCategoriaB.Visible = true;
				LblBusca3.Visible = true;
				DdlFamiliaB.Visible = true;

			}

		}

		protected void BtnConslta_Command(object sender, CommandEventArgs e)
		{
			LlenaTabla();
		}

		protected void DdlLineasB_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaCategoria();
			LlenaFamilia();
			LlenaTabla();
		}

		protected void DdlCategoriaB_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaFamilia();
			LlenaTabla();

		}

		protected void DdlFamiliaB_SelectedIndexChanged(object sender, EventArgs e)
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
				var respuesta = ctrlInv.ArticuloDel(id, usuario);
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
			// #### cuando Edita el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

			if (id != 0)
			{
				Session["AccArt"] = "O";
				Session["IdArt"] = id.ToString();

				Response.Redirect("~/Views/Inventarios/ArticulosDet.aspx");

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
	}
}