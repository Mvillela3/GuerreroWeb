using GuerreroWeb.Controllers;
using GuerreroWeb.Models;
using System;
using System.Configuration;
using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using static GuerreroWeb.Controllers.DBConexion;
//using System.Web.Services;
//using System.Web.Script.Services;

using GuerreroWeb.Views;
using System.Web.Services;

namespace GuerreroWeb
{
	public partial class LogIn : System.Web.UI.Page
    {
        CtrlUsuarios Usuarios = new CtrlUsuarios();
        VtUsuarios mUsuario = new VtUsuarios();
        ModUsuarios musuario = new ModUsuarios();
		private static DBConexion conexion = new DBConexion();
		private static DBComandos Cmd = new DBComandos();

		protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenaUsuarios();
				TxtUsuario.Focus();
            }


        }
        private void LlenaUsuarios()
        {
   //         CbxUsuario.DataSource = null;
   //         CbxUsuario.DataBind();

   //         var lista = Usuarios.DdlUsuarios();
   //         if (lista == null)
   //         {

   //         }
   //         if (lista.Count > 0)
   //         {
			//	CbxUsuario.DataSource = lista;
			//	CbxUsuario.DataTextField = "Usuario";
			//	CbxUsuario.DataValueField = "Usuario";
			//	CbxUsuario.DataBind();

			//}
		}

        protected void BtnEntrar_Command(object sender, CommandEventArgs e)
        {
			//string usu1 = CbxUsuario.SelectedItem.Text;
			string usu1 = TxtUsuario.Text;
            string pwd1 = TxtPwd.Text;

            //if (CbxUsuario.SelectedItem == null)
			if (TxtUsuario.Text == string.Empty)
			{
				LblMensaje1.Visible = true;
				LblMensaje1.Attributes["Style"] = "display: '';";

				return;
            }
            if(TxtPwd.Text.Length == 0)
            {
                LblMensaje2.Visible = true;
				LblMensaje2.Attributes["Style"] = "display: '';";
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
                LblMensaje2.Visible=true;
				LblMensaje2.Attributes["Style"] = "display: '';";

			}
			else
            {
                LblMensaje2.Visible = false;
				LblMensaje2.Attributes["Style"] = "display: 'none';";
			}
		}

		//protected void CbxUsuario_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	if (CbxUsuario.SelectedItem.Text.Length == 0)
		//	{
		//		LblMensaje1.Visible = true;
		//		LblMensaje1.Attributes["Style"] = "display: '';";

		//	}
		//	else
		//	{
		//		LblMensaje1.Visible = false;
		//		LblMensaje1.Attributes["Style"] = "display: 'none';";
		//	}

		//}

		//protected void CbxUsuario_TextChanged(object sender, EventArgs e)
		//{
		//	if (CbxUsuario.Text == string.Empty)
		//	{
		//		LblMensaje1.Visible = true;
		//		LblMensaje1.Attributes["Style"] = "display: '';";

		//	}
		//	else
		//	{
		//		LblMensaje1.Visible = false;
		//		LblMensaje1.Attributes["Style"] = "display: 'none';";
		//	}

		//}

		[WebMethod]
		//[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
		public static List<string> AutoCompleta(string prefixText)
		{


			//List<string> Lista = Usuarios.AutoCompletaUsuarios(prefixText);
			List<string> Lista = new List<string>();

			Lista.Add("juan");
			Lista.Add("pedro");
			Lista.Add("jose");
			Lista.Add("josue");
			Lista.Add("patricia");
			Lista.Add("laura");

			return Lista;

		}

		protected void AutoCompleteExtender1_DataBinding(object sender, EventArgs e)
		{
			//List<string> Lista = Usuarios.AutoCompletaUsuarios(TxtUsuario.Text);
			//AutoCompleteExtender1.DataBind();


			LblMensaje.Visible = true;
		}
	}
}