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
	public partial class Cajas : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios VtUsuario = new VtUsuarios();
		VtCajas VtCajas = new VtCajas();
		CatCajas mCajas = new CatCajas();

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
				if (!VtUsuario.EntraCajas)
				{

					Response.Redirect("~/Inicio.aspx");

				}
				LlenaSucursal();
				LlenaDeptos();
				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				DdlSucursalB.Visible = false;
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
			var vtCajas = ctrlEmpresa.ListaCajas(TxtBuscar.Text, Convert.ToInt32(DdlSucursalB.SelectedValue.ToString()));

			if (vtCajas != null)
			{
				if (vtCajas.Count == 0)
				{
					return;
				}

				if (vtCajas[0].Caja == "Error")
				{
					MsgBox(vtCajas[0].Sucursal, "Documentos");
					return;
				}
				GvConsulta.DataSource = vtCajas;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hfactivo = GvConsulta.Rows[i].FindControl("HfActivo") as HiddenField;
					Image ImActivo = GvConsulta.Rows[i].FindControl("ImgActivo") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.CajaMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.CajaDel;
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
			BtnNuevo.Enabled = VtUsuario.CajaAdd;
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void LlenaSucursal()
		{
			DdlSucursal.DataSource = null;
			DdlSucursal.DataBind();
			DdlSucursalB.DataSource = null;
			DdlSucursalB.DataBind();
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

					DdlSucursalB.DataSource = lista;
					DdlSucursalB.DataTextField = "Sucursal";
					DdlSucursalB.DataValueField = "IdSuc";
					DdlSucursalB.DataBind();
					DdlSucursalB.SelectedIndex = 0;
				}
			}
		}
		private void LlenaDeptos()
		{
			DdlDepto.DataSource = null;
			DdlDepto.DataBind();

			var lista = ctrlEmpresa.DdlDepartamentos();

			if (lista != null)
			{
				if (lista.Count > 0 && lista[0].Departamento.Substring(0, 5) != "Error")
				{

					DdlDepto.DataSource = lista;
					DdlDepto.DataTextField = "Departamento";
					DdlDepto.DataValueField = "IdDepto";
					DdlDepto.DataBind();
					DdlDepto.SelectedValue = "0";
				}
			}

		}

		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccCaj"] = "N";
			HfId.Value = "0";
			TxtCaja.Text = "";
			TxtNoCaja.Text = "00";
			ChkActivo.Checked = true;
			DdlSucursal.SelectedIndex = 0;
			DdlDepto.SelectedIndex = 0;
			TxtConse.Text = "0";

			TxtCaja.Focus();
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
				DdlSucursalB.Visible = false;

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;
				LblBusca1.Visible = true;
				DdlSucursalB.Visible = true;

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
				var respuesta = ctrlEmpresa.CajaDel(id);
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
			Session["AccCaj"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			HfId.Value = id.ToString();

			if (id != 0)
			{
				var respuesta = ctrlEmpresa.Caja(id);
				if (respuesta != null || respuesta.Caja != string.Empty)
				{
					int largo = respuesta.Caja.Length;

					if (largo >= 5)
					{
						largo = 5;
					}

					if (respuesta.Caja.Substring(0, largo) == "Error")
					{
						MsgBox(respuesta.Caja, "Eliminar");
						return;
					}
					TxtCaja.Text = respuesta.Caja;
					DdlSucursal.SelectedValue = respuesta.IdSuc.ToString();
					ChkActivo.Checked = respuesta.Activo;

					DdlDepto.SelectedValue = respuesta.IdDepto.ToString();
					TxtConse.Text = respuesta.Consecutivo.ToString();
					TxtNoCaja.Text = respuesta.NoCaja;
					TxtCaja.Focus();

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

		protected void TxtCaja_TextChanged(object sender, EventArgs e)
		{
			if (TxtCaja.Text.Trim() == string.Empty)
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
			if (TxtCaja.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mCajas.IdCaja = Convert.ToInt32(HfId.Value.ToString());
			mCajas.Caja = TxtCaja.Text;
			mCajas.NoCaja = TxtNoCaja.Text;
			mCajas.Consecutivo = Convert.ToInt32(TxtConse.Text);
			mCajas.IdSuc = Convert.ToInt32(DdlSucursal.SelectedValue.ToString());
			mCajas.Activo = ChkActivo.Checked;
			mCajas.IdDepto = Convert.ToInt32(DdlDepto.SelectedValue.ToString());

			RespuestaSQL respuesta = null;

			if (Session["AccCaj"].ToString() != string.Empty)
			{
				if (Session["AccCaj"].ToString() == "N")
				{
					respuesta = ctrlEmpresa.CajaAdd(mCajas);
				}
				if (Session["AccCaj"].ToString() == "U")
				{
					respuesta = ctrlEmpresa.CajaMod(mCajas);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Documentos");
						return;

					}
					Session["AccCaj"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccCaj"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

		}

        protected void DdlSucursalB_SelectedIndexChanged(object sender, EventArgs e)
        {
			LlenaTabla();
        }
    }
}