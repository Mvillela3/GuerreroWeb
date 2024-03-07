using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GuerreroWeb.Views
{
	public partial class Movimientos : System.Web.UI.Page
	{
		CtrlUsuarios ctrlusuario = new CtrlUsuarios();
		CtrlModulos ctrlModulos = new CtrlModulos();
		VtUsuarios VtUsuario = new VtUsuarios();
		VtMovimientos VtMovto = new VtMovimientos();
		ModMovimientos mMovto = new ModMovimientos();

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
				if (!VtUsuario.EntraMovtos)
				{

					Response.Redirect("~/Inicio.aspx");

				}
				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				DdlModuloB.Visible = false;
				LlenaModulo();
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
			List<VtMovimientos> vtMovto = ctrlModulos.ListaMovimientos(TxtBuscar.Text, Convert.ToInt32(DdlModuloB.SelectedValue.ToString()));

			if (vtMovto != null)
			{
				if (vtMovto.Count == 0)
				{
					return;
				}

				if (vtMovto[0].Tipo == "Error")
				{
					MsgBox(vtMovto[0].Tipo, "Documentos");
					return;
				}
				GvConsulta.DataSource = vtMovto;
				GvConsulta.DataBind();

				for (int i = 0; i < GvConsulta.Rows.Count; i++)
				{
					ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
					ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
					HiddenField hfactivo = GvConsulta.Rows[i].FindControl("HfActivo") as HiddenField;
					Image ImActivo = GvConsulta.Rows[i].FindControl("ImgActivo") as Image;
					HiddenField hfinv = GvConsulta.Rows[i].FindControl("HfInv") as HiddenField;
					Image Iminv = GvConsulta.Rows[i].FindControl("ImgInv") as Image;
					HiddenField hfcont = GvConsulta.Rows[i].FindControl("HfCont") as HiddenField;
					Image Imcont = GvConsulta.Rows[i].FindControl("ImgCont") as Image;
					HiddenField hfcxc = GvConsulta.Rows[i].FindControl("HfCXC") as HiddenField;
					Image Imcxc = GvConsulta.Rows[i].FindControl("ImgCXC") as Image;
					HiddenField hfcxp = GvConsulta.Rows[i].FindControl("HfCXP") as HiddenField;
					Image Imcxp = GvConsulta.Rows[i].FindControl("ImgCXP") as Image;

					if (ibeditar != null)
					{
						ibeditar.Enabled = VtUsuario.MovtosMod;
					}
					if (ibdel != null)
					{
						ibdel.Enabled = VtUsuario.MovtosDel;
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

					if (Iminv != null && hfinv != null)
					{
						if (Convert.ToBoolean(hfinv.Value.ToString()))
						{
							Iminv.ImageUrl = @"~/Resources/Check1.png";
						}
						else
						{
							Iminv.ImageUrl = @"~/Resources/Check2.png";
						}
					}

					if (Imcont != null && hfcont != null)
					{
						if (Convert.ToBoolean(hfinv.Value.ToString()))
						{
							Imcont.ImageUrl = @"~/Resources/Check1.png";
						}
						else
						{
							Imcont.ImageUrl = @"~/Resources/Check2.png";
						}
					}

					if (Imcxc != null && hfcxc != null)
					{
						if (Convert.ToBoolean(hfcxc.Value.ToString()))
						{
							Imcxc.ImageUrl = @"~/Resources/Check1.png";
						}
						else
						{
							Imcxc.ImageUrl = @"~/Resources/Check2.png";
						}
					}

					if (Imcxp != null && hfcxp != null)
					{
						if (Convert.ToBoolean(hfcxp.Value.ToString()))
						{
							Imcxp.ImageUrl = @"~/Resources/Check1.png";
						}
						else
						{
							Imcxp.ImageUrl = @"~/Resources/Check2.png";
						}
					}

				}
			}
		}
		private void AccionBtn()
		{
			BtnNuevo.Enabled = VtUsuario.MovtosAdd;
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void LlenaModulo()
		{
			DdlModulo.DataSource = null;
			DdlModulo.DataBind();
			DdlModuloB.DataSource = null;
			DdlModuloB.DataBind();

			List<ModModulos> lista = ctrlModulos.ListaModulo();

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
		public void llenaTipo(int modulo)
		{
			DdlTipo.Items.Clear();
			var tipos = new TipoMovtos();
			var lista = new List<TipoMovtos>();

			if (modulo == 1 || modulo == 2 || modulo == 3)
			{
				tipos.Tipo = "E";
				tipos.TipoDesc = "ENTRADA";
				lista.Add(tipos);

				tipos = new TipoMovtos();
				tipos.Tipo = "S";
				tipos.TipoDesc = "SALIDA";
				lista.Add(tipos);

				tipos = new TipoMovtos();
				tipos.Tipo = "N";
				tipos.TipoDesc = "NO APLICA";
				lista.Add(tipos);
			}
			if (modulo == 4 || modulo == 5 )
			{
				tipos.Tipo = "A";
				tipos.TipoDesc = "ABONO";
				lista.Add(tipos);

				tipos = new TipoMovtos();
				tipos.Tipo = "C";
				tipos.TipoDesc = "CARGO";
				lista.Add(tipos);
			}
			if (modulo == 6)
			{
				tipos.Tipo = "D";
				tipos.TipoDesc = "DIARIO";
				lista.Add(tipos);

				tipos = new TipoMovtos(); 
				tipos.Tipo = "I";
				tipos.TipoDesc = "INGRESO";
				lista.Add(tipos);

				tipos = new TipoMovtos(); 
				tipos.Tipo = "G";
				tipos.TipoDesc = "EGRESO";
				lista.Add(tipos);
			}
			if (lista.Count > 0)
			{
				DdlTipo.DataSource = lista;
				DdlTipo.DataTextField = "TipoDesc";
				DdlTipo.DataValueField = "Tipo";
				DdlTipo.DataBind();

			}

		}
		private void LlenaDatos(int IdMovto)
		{

		}

		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccMov"] = "N";
			HfId.Value = "0";
			TxtDocumento.Text = "";
			ChkActivo.Checked = true;
			ChkCXC.Checked = false;
			ChkCont.Checked = false;
			ChkInv.Checked = false;
			ChkCXP.Checked = false;

			DdlModulo.SelectedIndex = 0;
			llenaTipo(Convert.ToInt32( DdlModulo.SelectedValue.ToString()));

			DdlModulo.Enabled = true;
			DdlTipo.Enabled = true;
			
			TxtDocumento.Focus();

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
				var respuesta = ctrlModulos.MovimientoDel(id);
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
			Session["AccMov"] = "U";
			// #### cuando borra las el registro
			GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			HfId.Value = id.ToString();

			if (id != 0)
			{
				var respuesta = ctrlModulos.Movimiento(id);
				if (respuesta != null || respuesta.Movimiento != string.Empty)
				{
					int largo = respuesta.Movimiento.Length;

					if (largo >= 5)
					{
						largo = 5;
					}

					if (respuesta.Movimiento.Substring(0, largo) == "Error")
					{
						MsgBox(respuesta.Movimiento, "Eliminar");
						return;
					}
					TxtDocumento.Text = respuesta.Movimiento;
					DdlModulo.SelectedValue = respuesta.IdMod.ToString();
					ChkActivo.Checked = respuesta.Activo;
					llenaTipo(Convert.ToInt32(DdlModulo.SelectedValue.ToString()));

					DdlTipo.SelectedValue = respuesta.Tipo.ToString();
					ChkInv.Checked = respuesta.AfectaInv;
					ChkCont.Checked = respuesta.AfectaCont;
					ChkCXC.Checked = respuesta.AfectaCXC;
					ChkCXP.Checked = respuesta.AfectaCXP;

					TxtDocumento.Focus();

					VtMdl.Visible = true;
					MpeVtMdl.Show();

					DdlModulo.Enabled = false;
					DdlTipo.Enabled = false;
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

		protected void BtnGuardar_Click(object sender, EventArgs e)
		{
			if (TxtDocumento.Text == string.Empty)
			{
				LblMensaje1.Text = "Este Campo es Obligatorio";
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";
				return;

			}
			mMovto.IdMovto = Convert.ToInt32(HfId.Value.ToString());
			mMovto.Movimiento = TxtDocumento.Text;
			mMovto.IdMod = Convert.ToInt32(DdlModulo.SelectedValue.ToString());
			mMovto.Activo = ChkActivo.Checked;
			mMovto.Tipo = DdlTipo.SelectedValue.ToString();
			mMovto.AfectaCont = ChkCont.Checked;
			mMovto.AfectaInv = ChkInv.Checked;
			mMovto.AfectaCXP = ChkCXP.Checked;
			mMovto.AfectaCXC = ChkCXC.Checked;

			RespuestaSQL respuesta = null;

			if (Session["AccMov"].ToString() != string.Empty)
			{
				if (Session["AccMov"].ToString() == "N")
				{
					respuesta = ctrlModulos.MovimientoAdd(mMovto);
				}
				if (Session["AccMov"].ToString() == "U")
				{
					respuesta = ctrlModulos.MovimientoMod(mMovto);
				}


				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Documentos");
						return;

					}
					Session["AccMov"] = null;
					HfId.Value = "";
					VtMdl.Visible = false;
					LlenaTabla();
					BtnAceptar_Command(null, null);

				}

			}

		}

		protected void BtnCancelar_Click(object sender, EventArgs e)
		{
			Session["AccMov"] = null;
			HfId.Value = "";
			VtMdl.Visible = false;

		}

		protected void DdModulo_SelectedIndexChanged(object sender, EventArgs e)
		{
			llenaTipo(Convert.ToInt32(DdlModulo.SelectedValue.ToString()));
			VtMdl.Visible = true;
			MpeVtMdl.Show();

		}

		protected void TxtDocumento_TextChanged(object sender, EventArgs e)
		{
			if (TxtDocumento.Text.Trim() == string.Empty)
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
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: 'none';";
			}

		}

		protected void BtnAceptar_Command(object sender, CommandEventArgs e)
		{

		}

        protected void DdlModuloB_SelectedIndexChanged(object sender, EventArgs e)
        {
			LlenaTabla();
        }
    }
}