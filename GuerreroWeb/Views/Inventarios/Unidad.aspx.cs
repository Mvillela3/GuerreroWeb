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
	public partial class Unidad : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios VtUsuario = new VtUsuarios();
		CtrlInvCatalogos ctrlInv = new CtrlInvCatalogos();
		CatUnidad mUnidad = new CatUnidad();
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

			List<CatUnidad> vtUnidad = ctrlInv.ListaUnidad(TxtBuscar.Text);

			if (vtUnidad != null)
			{
				if (vtUnidad.Count == 0)
				{
					return;
				}
				int largo = vtUnidad[0].ClaveSAT.Length;
				if (largo >= 5)
				{
					largo = 5;
				}

				if (vtUnidad[0].ClaveSAT.Substring(0, largo) == "Error")
				{
					MsgBox(vtUnidad[0].Descripcion, "Unidades");
					return;
				}
				GvConsulta.DataSource = vtUnidad;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hfmedio = GvConsulta.Rows[i].FindControl("HfMedios") as HiddenField;
					Image Immedio = GvConsulta.Rows[i].FindControl("ImgMedios") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.UnidadMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.UnidadDel;
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

		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccTal"] = "N";
			HfId.Value = "0";
			TxtUnidad.Text = "";
			TxtUnidad.Focus();
			TxtDescripcion.Text = "";
			TxtClave.Text = "";

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

			string id = (fila.FindControl("LblId") as Label).Text;


			if (id != string.Empty)
			{
				var respuesta = ctrlInv.UnidadDel(id);
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
			Session["AccUni"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			string id = (fila.FindControl("LblId") as Label).Text;
			HfId.Value = id.ToString();

			if (id != string.Empty)
			{
				var respuesta = ctrlInv.Unidad(id);
				if (respuesta != null || respuesta.ClaveSAT != string.Empty)
				{
					int largo = respuesta.ClaveSAT.Length;

					if (largo >= 5)
					{
						largo = 5;
					}

					if (respuesta.ClaveSAT.Substring(0, largo) == "Error")
					{
						MsgBox(respuesta.Descripcion, "Modificar");
						return;
					}
					TxtUnidad.Text = respuesta.Unidad;
					TxtUnidad.Focus();

					TxtDescripcion.Text = respuesta.Descripcion;
					TxtClave.Text = respuesta.ClaveSAT;

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

		protected void TxtUnidad_TextChanged(object sender, EventArgs e)
		{
			if (TxtUnidad.Text.Trim() == string.Empty)
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

		protected void TxtDescripcion_TextChanged(object sender, EventArgs e)
		{
			if (TxtDescripcion.Text.Trim() == string.Empty)
			{
				LblMensaje2.Visible = true;
				LblMensaje2.Attributes["Style"] = "display: '';";
			}
			else
			{
				LblMensaje2.Visible = false;
				LblMensaje2.Attributes["Style"] = "display: 'none';";

			}
		}

		protected void TxtClave_TextChanged(object sender, EventArgs e)
		{
			if (TxtClave.Text.Trim() == string.Empty)
			{
				LblMensaje3.Visible = true;
				LblMensaje3.Attributes["Style"] = "display: '';";
			}
			else
			{
				LblMensaje3.Visible = false;
				LblMensaje3.Attributes["Style"] = "display: 'none';";

			}

		}

		protected void BtnGuardar_Click(object sender, EventArgs e)
		{
			if (TxtUnidad.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mUnidad.Unidad = HfId.Value.ToString();
			mUnidad.Descripcion = TxtDescripcion.Text;
			mUnidad.ClaveSAT = TxtClave.Text;

			RespuestaSQL respuesta = null;

			if (Session["AccUni"].ToString() != string.Empty)
			{
				if (Session["AccUni"].ToString() == "N")
				{
					respuesta = ctrlInv.UnidadAdd(mUnidad);
				}
				if (Session["AccUni"].ToString() == "U")
				{
					respuesta = ctrlInv.UnidadMod(mUnidad);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Color");
						return;

					}
					Session["AccUni"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccUni"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

		}
	}
}