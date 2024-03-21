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
	public partial class Lineas : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios VtUsuario = new VtUsuarios();
		CtrlInvCatalogos ctrlInv = new CtrlInvCatalogos();
		CatLineas mLineas = new CatLineas();

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
				if (!VtUsuario.EntraLineas)
				{

					Response.Redirect("~/Inicio.aspx");

				}

				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;

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
			var vtLineas = ctrlInv.ListaLineas(TxtBuscar.Text);

			if (vtLineas != null)
			{
				if (vtLineas.Count == 0)
				{
					return;
				}
				int largo = vtLineas[0].Linea.Length;
				if (largo >= 5)
				{
					largo = 5;
				}

				if (vtLineas[0].Linea.Substring(0,largo) == "Error")
				{
					MsgBox(vtLineas[0].Linea, "Lineas");
					return;
				}
				GvConsulta.DataSource = vtLineas;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hftalla = GvConsulta.Rows[i].FindControl("HfTalla") as HiddenField;
					Image Imtalla = GvConsulta.Rows[i].FindControl("ImgTalla") as Image;
					HiddenField hfcolor = GvConsulta.Rows[i].FindControl("HfColor") as HiddenField;
					Image Imcolor = GvConsulta.Rows[i].FindControl("ImgColor") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.LineasMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.LineasDel;
					}
					if (Imtalla != null && hftalla != null)
					{
						if (Convert.ToBoolean(hftalla.Value.ToString()))
						{
							Imtalla.ImageUrl = @"~/Resources/Check1.png";
						}
						else
						{
							Imtalla.ImageUrl = @"~/Resources/Check2.png";
						}
					}
					if (Imcolor != null && hfcolor != null)
					{
						if (Convert.ToBoolean(hfcolor.Value.ToString()))
						{
							Imcolor.ImageUrl = @"~/Resources/Check1.png";
						}
						else
						{
							Imcolor.ImageUrl = @"~/Resources/Check2.png";
						}
					}

				}
			}
		}
		private void AccionBtn()
		{
			BtnNuevo.Enabled = VtUsuario.LineasAdd;
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}

		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccLin"] = "N";
			HfId.Value = "0";
			TxtLinea.Text = "";
			ChkColor.Checked = false;
			ChkTalla.Checked = false;

			TxtLinea.Focus();

			VtMdl.Visible = true;
			MpeVtMdl.Show();

		}

		protected void BtnBuscar_Command(object sender, CommandEventArgs e)
		{
			if (TxtBuscar.Visible)
			{
				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;

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
				var respuesta = ctrlInv.LineaDel(id);
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
			Session["AccLin"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			HfId.Value = id.ToString();

			if (id != 0)
			{
				var respuesta = ctrlInv.Linea(id);
				if (respuesta != null || respuesta.Linea != string.Empty)
				{
					int largo = respuesta.Linea.Length;

					if (largo >= 5)
					{
						largo = 5;
					}

					if (respuesta.Linea.Substring(0, largo) == "Error")
					{
						MsgBox(respuesta.Linea, "Modificar");
						return;
					}
					TxtLinea.Text = respuesta.Linea;

					TxtLinea.Focus();
					ChkColor.Checked = respuesta.UsaColor;
					ChkTalla.Checked = respuesta.UsaTalla;

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

		protected void TxtLinea_TextChanged(object sender, EventArgs e)
		{
			if (TxtLinea.Text.Trim() == string.Empty)
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
			if (TxtLinea.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mLineas.IdLinea = Convert.ToInt32(HfId.Value.ToString());
			mLineas.Linea = TxtLinea.Text;
			mLineas.UsaColor = ChkColor.Checked;
			mLineas.UsaTalla = ChkTalla.Checked;

			RespuestaSQL respuesta = null;

			if (Session["AccLin"].ToString() != string.Empty)
			{
				if (Session["AccLin"].ToString() == "N")
				{
					respuesta = ctrlInv.LineaAdd(mLineas);
				}
				if (Session["AccLin"].ToString() == "U")
				{
					respuesta = ctrlInv.LineaMod(mLineas);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Lineas");
						return;

					}
					Session["AccLin"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccLin"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

		}
	}
}