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
	public partial class Marcas : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios VtUsuario = new VtUsuarios();
		CtrlInvCatalogos ctrlInv = new CtrlInvCatalogos();
		CatMarcas mMarca = new CatMarcas();

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
				if (!VtUsuario.EntraMarcas)
				{

					Response.Redirect("~/Inicio.aspx");

				}

				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				DdlLineaB.Visible = false;

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
			var vtMarca = ctrlInv.ListaMarcas(TxtBuscar.Text, Convert.ToInt32(DdlLineaB.SelectedValue.ToString()));

			if (vtMarca != null)
			{
				if (vtMarca.Count == 0)
				{
					return;
				}
				int largo = vtMarca[0].Marca.Length;
				if (largo >= 5)
				{
					largo = 5;
				}

				if (vtMarca[0].Marca.Substring(0, largo) == "Error")
				{
					MsgBox(vtMarca[0].Marca, "Marcas");
					return;
				}
				GvConsulta.DataSource = vtMarca;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hfactivo = GvConsulta.Rows[i].FindControl("HfActivo") as HiddenField;
					Image ImActivo = GvConsulta.Rows[i].FindControl("ImgActivo") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.MarcasMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.MarcasDel;
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
			BtnNuevo.Enabled = VtUsuario.MarcasAdd;
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
			Session["AccMar"] = "N";
			HfId.Value = "0";
			TxtMarca.Text = "";
			TxtMarca.Focus();

			DdlLinea.SelectedIndex = 0;

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

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;
				LblBusca1.Visible = true;
				DdlLineaB.Visible = true;

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
				var respuesta = ctrlInv.MarcaDel(id);
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
			Session["AccMar"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			HfId.Value = id.ToString();

			if (id != 0)
			{
				var respuesta = ctrlInv.Marca(id);
				if (respuesta != null || respuesta.Marca != string.Empty)
				{
					int largo = respuesta.Marca.Length;

					if (largo >= 5)
					{
						largo = 5;
					}

					if (respuesta.Marca.Substring(0, largo) == "Error")
					{
						MsgBox(respuesta.Marca, "Modificar");
						return;
					}
					TxtMarca.Text = respuesta.Marca;
					TxtMarca.Focus();

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

		protected void TxtMarca_TextChanged(object sender, EventArgs e)
		{
			if (TxtMarca.Text.Trim() == string.Empty)
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
			if (TxtMarca.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mMarca.IdMarca = Convert.ToInt32(HfId.Value.ToString());
			mMarca.Marca = TxtMarca.Text;
			mMarca.IdLinea = Convert.ToInt32(DdlLinea.SelectedValue.ToString());

			RespuestaSQL respuesta = null;

			if (Session["AccMar"].ToString() != string.Empty)
			{
				if (Session["AccMar"].ToString() == "N")
				{
					respuesta = ctrlInv.MarcaAdd(mMarca);
				}
				if (Session["AccMar"].ToString() == "U")
				{
					respuesta = ctrlInv.MarcaMod(mMarca);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Marcas");
						return;

					}
					Session["AccMar"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccMar"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

		}

		protected void DdlLineasB_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaTabla();
		}
	}
}