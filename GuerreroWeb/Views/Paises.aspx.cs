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
    public partial class Paises : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlCodPostal ctrlCodPostal = new CtrlCodPostal();
        VtUsuarios VtUsuario = new VtUsuarios();
        CatPaises catPais = new CatPaises();
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
                if (!VtUsuario.EntraPaises)
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
            List<CatPaises> vtPais = ctrlCodPostal.ListaPaises(TxtBuscar.Text);

            if (vtPais != null)
            {
                if (vtPais.Count == 0)
                {
                    return;
                }

                if (vtPais[0].Abreviatura == "Error")
                {
                    MsgBox(vtPais[0].Pais, "PAises");
                    return;
                }
                GvConsulta.DataSource = vtPais;
                GvConsulta.DataBind();
                
                for (int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
                    ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
                    HiddenField hfdefault = GvConsulta.Rows[i].FindControl("HfDefault") as HiddenField;
                    Image Imdefault = GvConsulta.Rows[i].FindControl("ImgDefault") as Image;

                    if (ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.PaisMod;
                    }
                    if (ibdel != null)
                    {
                        ibdel.Enabled = VtUsuario.PaisDel;
                    }
                    if(Imdefault != null && hfdefault != null)
                    {
                        if(Convert.ToBoolean(hfdefault.Value.ToString()))
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
            BtnNuevo.Enabled = VtUsuario.PaisAdd;
        }
        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }

        protected void BtnNuevo_Command(object sender, CommandEventArgs e)
        {
            Session["AccPai"] = "N";
            HfId.Value = "0";
            TxtPais.Text = "";
            TxtAbrevia.Text = "";
            ChkDefault.Checked = false;
            TxtPais.Focus();

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

            catPais.IdPais = id;

            if (id != 0)
            {
                var respuesta = ctrlCodPostal.PaisDel(catPais);
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
            Session["AccPai"] = "U";
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            HfId.Value = id.ToString();

            if (id != 0)
            {
                var respuesta = ctrlCodPostal.Pais(id);
                if (respuesta != null || respuesta.Pais != string.Empty)
                {
                    if (respuesta.Abreviatura == "Error")
                    {
                        MsgBox(respuesta.Pais, "Eliminar");
                        return;
                    }
                    TxtPais.Text = respuesta.Pais;
                    TxtAbrevia.Text = respuesta.Abreviatura;
                    ChkDefault.Checked = respuesta.EsDefault;
                    TxtPais.Focus();

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
            if (TxtPais.Text == string.Empty)
            {
                MsgBox("Escriba el Nombre del Pais", "Pais");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            if (TxtAbrevia.Text == string.Empty)
            {
                MsgBox("Escriba la Abreviatura del Pais", "Pais");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            catPais.IdPais = Convert.ToInt32(HfId.Value.ToString());
            catPais.Pais = TxtPais.Text;
            catPais.Abreviatura = TxtAbrevia.Text;
            catPais.EsDefault = ChkDefault.Checked;

            RespuestaSQL respuesta = null;

            if (Session["AccPai"].ToString() != string.Empty)
            {
                if (Session["AccPai"].ToString() == "N")
                {
                    respuesta = ctrlCodPostal.PaisAdd(catPais);
                }
                if (Session["AccPai"].ToString() == "U")
                {
                    respuesta = ctrlCodPostal.PaisMod(catPais);
                }


                if (respuesta != null)
                {
                    if (respuesta.Codigo != 0)
                    {
                        MsgBox(respuesta.Mensaje, "Pais");
                        return;

                    }
                    Session["AccPai"] = null;
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
            Session["AccPai"] = null;
            VtMdl.Visible = false;

        }

        protected void GvConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvConsulta.PageIndex = e.NewPageIndex;
            LlenaTabla();

        }
    }
}