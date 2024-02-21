using GuerreroWeb.Controllers;
using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuerreroWeb
{
    public partial class LogIn : System.Web.UI.Page
    {
        CtrlUsuarios Usuarios = new CtrlUsuarios();
        VtUsuarios mUsuario = new VtUsuarios();
        ModUsuarios musuario = new ModUsuarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenaUsuarios();
            }

        }
        private void LlenaUsuarios()
        {
            var lista = Usuarios.DdlUsuarios();
            if (lista == null)
            {

            }
            if (lista.Count > 0)
            {
                DdlUsuario.DataSource = lista;
                DdlUsuario.DataTextField = "Usuario";
                DdlUsuario.DataValueField = "Usuario";
                DdlUsuario.DataBind();
            }
        }

        protected void BtnEntrar_Command(object sender, CommandEventArgs e)
        {
            string usu1 = DdlUsuario.SelectedItem.Text;
            string pwd1 = TxtPwd.Text;

            if (DdlUsuario.SelectedItem == null)
            {
                LblUsu.Visible = true;
                return;
            }
            if(TxtPwd.Text.Length == 0)
            {
                LblPwd.Visible = true;
            }

            LblMensaje.Text = "";
            LblMensaje.Visible = false;

            if (pwd1.Length == 0 || usu1.Length == 0)
            {
                return;
            }
            var acceso = Usuarios.Acceso(usu1, pwd1);

            if (acceso == null)
            {
                return;
            }
            if (acceso.Codigo > 0)
            {
                LblMensaje.Text = acceso.Mensaje;
                LblMensaje.Visible = true;
            }
            if (acceso.Codigo == 0)
            {
                Session["Usuario"] = usu1;
                Response.Redirect("~/Inicio.aspx");
            }

        }

        protected void TxtPwd_TextChanged(object sender, EventArgs e)
        {
            if(TxtPwd.Text.Length == 0)
            {
                LblPwd.Visible=true;
            }
            else
            {
                LblPwd.Visible = false;
            }
        }

        protected void DdlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DdlUsuario.SelectedItem.Text.Length == 0)
            {
                LblUsu.Visible=true;
            }
            else
            {
                LblUsu.Visible = false;
            }
        }
    }
}