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
    public partial class Almacenes : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlEmpresas ctrlempresa = new CtrlEmpresas();
        VtUsuarios VtUsuario = new VtUsuarios();
        CatAlmacenes mAlmacenes = new CatAlmacenes();
        VtAlmacenes vtAlmacenes = new VtAlmacenes();
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
                if (!VtUsuario.EntraAlmacenes)
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
            List<VtAlmacenes> vtAlmacen = ctrlempresa.ListaAlmacenes(TxtBuscar.Text);

            if (vtAlmacen != null)
            {
                if (vtAlmacen.Count == 0)
                {
                    return;
                }

                if (vtAlmacen[0].Sucursal == "Error")
                {
                    MsgBox(vtAlmacen[0].Nombre, "Sucursales");
                    return;
                }
                GvConsulta.DataSource = vtAlmacen;
                GvConsulta.DataBind();

                for (int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
                    ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;

                    if (ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.EntraAlmacenes;
                    }
                    if (ibdel != null)
                    {
                        ibdel.Enabled = VtUsuario.AlmacenDel;
                    }
                }
            }
        }
        private void AccionBtn()
        {
            BtnNuevo.Enabled = VtUsuario.AlmacenAdd;
        }
        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }

        protected void GvConsulta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.RowIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            mAlmacenes.IdAlm = id;

            if (id != 0)
            {
                var respuesta = ctrlempresa.AlmacenDel(mAlmacenes);
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
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            //catSucursales.IdSuc = id;

            Session["AccAlm"] = "O";
            Session["IdAlm"] = id.ToString();
            Response.Redirect("~/Views/AlmacenesDet.aspx");

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

        protected void BtnNuevo_Command(object sender, CommandEventArgs e)
        {
            BtnNuevo.Enabled = false;
            Session["AccAlm"] = "N";
            Session["IdAlm"] = "0";
            Response.Redirect("~/Views/AlmacenesDet.aspx");

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
    }
}