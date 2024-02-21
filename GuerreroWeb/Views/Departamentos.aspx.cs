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
    public partial class Departamentos : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlEmpresas ctrlempresa = new CtrlEmpresas();
        VtUsuarios VtUsuario = new VtUsuarios();
        CatDepartamentos catDepto = new CatDepartamentos();
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
                if (!VtUsuario.EntraDepto)
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
            List<CatDepartamentos> vtDepto = ctrlempresa.ListaDepartamentos(TxtBuscar.Text);

            if (vtDepto != null)
            {
                if (vtDepto.Count == 0)
                {
                    return;
                }

                if (vtDepto[0].Departamento.Substring(0,5) == "Error")
                {
                    MsgBox(vtDepto[0].Departamento, "Departamento");
                    return;
                }
                GvConsulta.DataSource = vtDepto;
                GvConsulta.DataBind();

                for (int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
                    ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;

                    if (ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.DeptoMod;
                    }
                    if (ibdel != null)
                    {
                        ibdel.Enabled = VtUsuario.DeptoDel;
                    }
                }
            }
        }
        private void AccionBtn()
        {
            BtnNuevo.Enabled = VtUsuario.DeptoAdd;
        }
        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }

        protected void BtnNuevo_Command(object sender, CommandEventArgs e)
        {
            Session["AccDep"] = "N";
            HfId.Value = "0";
            TxtDepto.Text = "";
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

        protected void GvConsulta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.RowIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            catDepto.IdDepto = id;

            if (id != 0)
            {
                var respuesta = ctrlempresa.DepartamentoDel(catDepto);
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

            Session["AccDep"] = "U";
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            HfId.Value = id.ToString();

            if (id != 0)
            {
                var respuesta = ctrlempresa.Departamento(id);
                if (respuesta != null || respuesta.Departamento != string.Empty)
                {
                    if (respuesta.Departamento.Substring(0,5) == "Error")
                    {
                        MsgBox(respuesta.Departamento, "Eliminar");
                        return;
                    }
                    TxtDepto.Text = respuesta.Departamento;
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

        protected void BtnAceptar_Command(object sender, CommandEventArgs e)
        {

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TxtDepto.Text == string.Empty)
            {
                MsgBox("Escriba el Nombre del Departamento", "Departamento");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }

            catDepto.IdDepto = Convert.ToInt32(HfId.Value.ToString());
            catDepto.Departamento = TxtDepto.Text;

            RespuestaSQL respuesta = null;

            if (Session["AccDep"].ToString() != string.Empty)
            {
                if (Session["AccDep"].ToString() == "N")
                {
                    respuesta = ctrlempresa.DepartamentoAdd(catDepto);
                }
                if (Session["AccDep"].ToString() == "U")
                {
                    respuesta = ctrlempresa.DepartamentoMod(catDepto);
                }


                if (respuesta != null)
                {
                    if (respuesta.Codigo != 0)
                    {
                        MsgBox(respuesta.Mensaje, "Departamento");
                        return;

                    }
                    Session["AccDep"] = null;
                    HfId.Value = "";
                    VtMdl.Visible = false;
                    LlenaTabla();
                    BtnAceptar_Command(null, null);

                }

            }

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            HfId.Value = "";
            Session["AccDep"] = null;
            VtMdl.Visible = true;
        }

        protected void GvConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvConsulta.PageIndex = e.NewPageIndex;
            LlenaTabla();

        }
    }
}