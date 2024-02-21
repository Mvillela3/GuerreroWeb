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
    public partial class Sucursales : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlEmpresas ctrlempresa = new CtrlEmpresas();
        VtUsuarios VtUsuario = new VtUsuarios();
        ModEmpresas mEmpresa = new ModEmpresas();
        CatSucursales catSucursales = new CatSucursales();
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
                if (!VtUsuario.EntraSucursales)
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
            List<VtSucursales> vtSucursales = ctrlempresa.ListaSucursales(TxtBuscar.Text);

            if (vtSucursales != null)
            {
                if (vtSucursales.Count == 0)
                {
                    return;
                }

                if (vtSucursales[0].Sucursal == "Error")
                {
                    MsgBox(vtSucursales[0].Nombre, "Sucursales");
                    return;
                }
                GvConsulta.DataSource = vtSucursales;
                GvConsulta.DataBind();

                for (int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
                    ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;

                    if (ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.EntraSucursales;
                    }
                    if (ibdel != null)
                    {
                        ibdel.Enabled = VtUsuario.SucursalDel;
                    }
                }
            }
        }
        private void AccionBtn()
        {
            BtnNuevo.Enabled = VtUsuario.SucursalAdd;
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

        protected void GvConsulta_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void GvConsulta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.RowIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            catSucursales.IdSuc = id;

            if (id != 0)
            {
                var respuesta = ctrlempresa.SucursalDel(catSucursales);
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
        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }

        protected void BtnConslta_Command(object sender, CommandEventArgs e)
        {
            LlenaTabla();
        }

        protected void BtnNuevo_Command(object sender, CommandEventArgs e)
        {
            Session["AccSuc"] = "N";
            Session["IdSuc"] = "0";
            Response.Redirect("~/Views/SucursalesDet.aspx");
        }

        protected void GvConsulta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            //catSucursales.IdSuc = id;

            Session["AccSuc"] = "O";
            Session["IdSuc"] = id.ToString();
            Response.Redirect("~/Views/SucursalesDet.aspx");

        }

        protected void GvConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvConsulta.PageIndex = e.NewPageIndex;
            LlenaTabla();

        }
    }
}