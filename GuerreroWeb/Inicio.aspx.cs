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
    public partial class Inicio : System.Web.UI.Page
    {
        CtrlUsuarios Usuarios = new CtrlUsuarios();
        VtUsuarios mUsuario = new VtUsuarios();
        ModUsuarios musuario = new ModUsuarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            string usuario = "";

            if (Session["Usuario"] != null)
            {
                usuario = Session["Usuario"].ToString();
            }


            if (usuario.Length == 0)
            {
                Response.Redirect("~/Login.aspx");
            }

            var usuarioDet = Usuarios.VtUsuario(usuario);

            if (usuarioDet == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (usuarioDet.Usuario == "Error")
            {
                Response.Redirect("~/Login.aspx");
            }

            string prueba = "Charles from time to time opened his eyes, his mind grew weary, and, sleep coming upon him, he soon fell into a doze wherein, his recent sensations blending with memories, he became conscious of a double self, at once student and married man, lying in his bed as but now, and crossing the operation theatre as of old. The warm smell of poultices mingled in his brain with the fresh odour of dew; he heard the iron rings rattling along the curtain-rods of the bed and saw his wife sleeping. As he passed Vassonville he came upon a boy sitting on the grass at the edge of a ditch.";
            mUsuario.Usuario = usuarioDet.Usuario;
            Label1.Text = prueba;

        }

        
    }
}