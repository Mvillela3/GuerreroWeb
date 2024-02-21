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
    public partial class Usuarios : System.Web.UI.Page
    {
        CtrlUsuarios ctrlUsuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios VtUsuario = new VtUsuarios();
		ModUsuarios MUsuario = new ModUsuarios();
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
                if (!VtUsuario.EntraUsuarios)
                {

                    Response.Redirect("~/Inicio.aspx");

                }
                TxtBuscar.Visible = false;
                BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				DdlDepto.Visible = false;
				LblBusca2.Visible= false;
				DdlEstatus.Visible = false;
				LblBusca3.Visible = false;
				DdlPerfil.Visible = false;
				LlenaDeptos();
				LlenaPerfiles();
				DdlEstatus.SelectedValue = "ACTIVO";
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
            var vtUsu = ctrlUsuario.ListaUsuarios(TxtBuscar.Text, DdlDepto.SelectedValue.ToString(),DdlPerfil.SelectedValue.ToString(), DdlEstatus.SelectedValue.ToString());

            if (vtUsu != null)
            {
                if (vtUsu.Count == 0)
                {
                    return;
                }

                if (vtUsu[0].Usuario == "Error")
                {
                    MsgBox(vtUsu[0].Nombre, "Usuarios");
                    return;
                }
                GvConsulta.DataSource = vtUsu;
                GvConsulta.DataBind();

                for (int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
                    ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
                    HiddenField hfdefault = GvConsulta.Rows[i].FindControl("HfDefault") as HiddenField;
                    Image Imdefault = GvConsulta.Rows[i].FindControl("ImgDefault") as Image;

                    if (ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.UsuarioMod;
                    }
                    if (ibdel != null)
                    {
                        ibdel.Enabled = VtUsuario.UsuarioDel;
                    }
                    if (Imdefault != null && hfdefault != null)
                    {
                        if (Convert.ToBoolean(hfdefault.Value.ToString()))
                        {
                            Imdefault.ImageUrl = @"~/Resources/Check1.png";
                        }
                        else
                        {
                            Imdefault.ImageUrl = @"~/Resources/Check2.png";
                        }
                    }
                }
            }
        }
        private void AccionBtn()
        {
            BtnNuevo.Enabled = VtUsuario.UsuarioAdd;
        }
        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }
        private void LlenaDeptos()
        {
			DdlDepto.DataSource = null;
			DdlDepto.DataBind();

			var lista = ctrlEmpresa.DdlDepartamentos();

			if (lista != null)
			{
				if (lista.Count > 0 && lista[0].Departamento.Substring(0,5) != "Error")
				{

					DdlDepto.DataSource = lista;
					DdlDepto.DataTextField = "Departamento";
					DdlDepto.DataValueField = "IdDepto";
					DdlDepto.DataBind();
					DdlDepto.SelectedValue = "0";
				}
			}

		}
		private void LlenaPerfiles()
        {
			DdlPerfil.DataSource = null;
			DdlPerfil.DataBind();

			var lista = ctrlUsuario.DdlPerfiles();

			if (lista != null)
			{
				if (lista.Count > 0 && lista[0].Perfil.Substring(0,5) != "Error")
				{

					DdlPerfil.DataSource = lista;
					DdlPerfil.DataTextField = "Perfil";
					DdlPerfil.DataValueField = "IdPerfil";
					DdlPerfil.DataBind();
					DdlPerfil.SelectedValue = "0";
				}
			}

		}

		protected void BtnNuevo_Command(object sender, CommandEventArgs e)
		{
			Session["AccUsu"] = "N";
            Session["IdUsu"] = "0";
            Response.Redirect("~/Views/UsuariosDet.aspx");
		}

		protected void BtnBuscar_Command(object sender, CommandEventArgs e)
		{
			if (TxtBuscar.Visible)
			{
				TxtBuscar.Visible = false;
				BtnConslta.Visible = false;
				LblBusca1.Visible = false;
				DdlDepto.Visible = false;
				LblBusca2.Visible = false;
				DdlPerfil.Visible = false;
				LblBusca3.Visible = false;
				DdlEstatus.Visible = false;

			}
			else
			{
				TxtBuscar.Visible = true;
				BtnConslta.Visible = true;
				LblBusca1.Visible = true;
				DdlDepto.Visible = true;
				LblBusca2.Visible = true;
				DdlPerfil.Visible = true;
				LblBusca3.Visible = true;
				DdlEstatus.Visible = true;

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
			// #### cuando borra el registro
			GridViewRow fila = GvConsulta.Rows[e.RowIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			MUsuario.IdUsu = id;

			if (id != 0)
			{
				var respuesta = ctrlUsuario.UsuarioDel(MUsuario);
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
				Session["AccUsu"] = "O";
				Session["IdUsu"] = id.ToString();

				Response.Redirect("~/Views/UsuariosDet.aspx");
				
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