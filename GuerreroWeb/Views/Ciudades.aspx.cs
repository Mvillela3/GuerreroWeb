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
    public partial class Ciudades : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlCodPostal ctrlCodPostal = new CtrlCodPostal();
        VtUsuarios VtUsuario = new VtUsuarios();
        CatCiudades catCiu = new CatCiudades();
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
                if (!VtUsuario.EntraCiudades)
                {

                    Response.Redirect("~/Inicio.aspx");

                }
                TxtBuscar.Visible = false;
                BtnConslta.Visible = false;
                LblBusca1.Visible = false;
                DdlPaisB.Visible = false;
                LblBusca2.Visible = false;
                DdlEstadoB.Visible = false;
                LlenaPaises();
                LlenaEstados();
                LlenaEstados2();
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
            List<VtCiudades> vtCiu = ctrlCodPostal.ListaCiudades(TxtBuscar.Text, DdlEstadoB.SelectedValue.ToString(), DdlPaisB.SelectedValue.ToString());

            if (vtCiu != null)
            {
                if (vtCiu.Count == 0)
                {
                    return;
                }

                if (vtCiu[0].Abreviatura == "Error")
                {
                    MsgBox(vtCiu[0].Ciudad, "Ciudad");
                    return;
                }
                GvConsulta.DataSource = vtCiu;
                GvConsulta.DataBind();

                for (int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
                    ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
                    HiddenField hfdefault = GvConsulta.Rows[i].FindControl("HfDefault") as HiddenField;
                    Image Imdefault = GvConsulta.Rows[i].FindControl("ImgDefault") as Image;

                    if (ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.CiudadMod;
                    }
                    if (ibdel != null)
                    {
                        ibdel.Enabled = VtUsuario.CiudadDel;
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
            BtnNuevo.Enabled = VtUsuario.CiudadAdd;
        }
        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }
        private void LlenaPaises()
        {
            DdlPais.DataSource = null;
            DdlPais.DataBind();
            DdlPaisB.DataSource = null;
            DdlPaisB.DataBind();

            List<CatPaises> lista = ctrlCodPostal.DdlPaises();

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlPais.DataSource = lista;
                    DdlPais.DataTextField = "Pais";
                    DdlPais.DataValueField = "IdPais";
                    DdlPais.DataBind();
                    DdlPais.SelectedIndex = 0;

                    DdlPaisB.DataSource = lista;
                    DdlPaisB.DataTextField = "Pais";
                    DdlPaisB.DataValueField = "IdPais";
                    DdlPaisB.DataBind();
                    DdlPaisB.SelectedIndex = 1;
                }
            }
            int def = ctrlCodPostal.PaisDef();
            if (def != null)
            {
                if (DdlPais.Items.FindByValue(def.ToString()) != null)
                {
                    DdlPais.SelectedValue = def.ToString();
                }
            }
        }
        private void LlenaEstados()
        {
            DdlEstado.DataSource = null;
            DdlEstado.DataBind();

            List<CatEstados> lista = ctrlCodPostal.DdlEstados(Convert.ToInt32(DdlPais.SelectedValue.ToString()));

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlEstado.DataSource = lista;
                    DdlEstado.DataTextField = "Estado";
                    DdlEstado.DataValueField = "IdEst";
                    DdlEstado.DataBind();

                    DdlEstado.SelectedIndex = 0;

                }
            }
            int def = ctrlCodPostal.EstDef();
            if (def != null)
            {
                if (DdlEstado.Items.FindByValue(def.ToString()) != null)
                {
                    DdlEstado.SelectedValue = def.ToString();
                }
            }
        }
        private void LlenaEstados2()
        {
            DdlEstadoB.DataSource = null;
            DdlEstadoB.DataBind();

            List<CatEstados> lista = ctrlCodPostal.DdlEstados(Convert.ToInt32(DdlPaisB.SelectedValue.ToString()));

            if (lista != null)
            {
                if (lista.Count > 0)
                {

                    DdlEstadoB.DataSource = lista;
                    DdlEstadoB.DataTextField = "Estado";
                    DdlEstadoB.DataValueField = "IdEst";
                    DdlEstadoB.DataBind();
                    DdlEstadoB.SelectedValue = "0";
                }
            }
            int def = ctrlCodPostal.EstDef();
            if (def != null)
            {
                if (DdlEstadoB.Items.FindByValue(def.ToString()) != null)
                {
                    DdlEstadoB.SelectedValue = def.ToString();
                }
            }

        }

        protected void DdlPaisB_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaEstados2();
        }

        protected void BtnNuevo_Command(object sender, CommandEventArgs e)
        {
            Session["AccCiu"] = "N";
            HfId.Value = "0";
            TxtCiudad.Text = "";
            ChkDefault.Checked = false;
            DdlPais.SelectedValue = "0";
            DdlEstado.SelectedValue = "0";
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
                DdlPaisB.Visible = false;
                LblBusca2.Visible = false;
                DdlEstadoB.Visible = false;

            }
            else
            {
                TxtBuscar.Visible = true;
                BtnConslta.Visible = true;
                LblBusca1.Visible = true;
                DdlPaisB.Visible = true;
                LblBusca2.Visible = true;
                DdlEstadoB.Visible = true;

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
            catCiu.IdCiu = id;

            if (id != 0)
            {
                var respuesta = ctrlCodPostal.CiudadDel(catCiu);
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
            Session["AccCiu"] = "U";
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            HfId.Value = id.ToString();

            if (id != 0)
            {
                var respuesta = ctrlCodPostal.Ciudad(id);
                if (respuesta != null || respuesta.Ciudad != string.Empty)
                {
                    if (respuesta.Abreviatura == "Error")
                    {
                        MsgBox(respuesta.Ciudad, "Eliminar");
                        return;
                    }
                    TxtCiudad.Text = respuesta.Ciudad;
                    ChkDefault.Checked = respuesta.EsDefault;
                    DdlPais.SelectedValue = respuesta.IdPais.ToString();
                    DdlEstado.SelectedValue = respuesta.IdEst.ToString();
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

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TxtCiudad.Text == string.Empty)
            {
                MsgBox("Escriba el Nombre de la Ciudad", "Ciudad");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            if (Convert.ToInt32(DdlPais.SelectedValue.ToString()) == 0)
            {
                MsgBox("Seleccione el Pais del Estado", "Estado");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            if (Convert.ToInt32(DdlEstado.SelectedValue.ToString()) == 0)
            {
                MsgBox("Seleccione el Estado de la Ciudad", "Ciudad");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }

            catCiu.IdCiu = Convert.ToInt32(HfId.Value.ToString());
            catCiu.Ciudad = TxtCiudad.Text;
            catCiu.EsDefault = ChkDefault.Checked;
            catCiu.IdEst = Convert.ToInt32(DdlEstado.SelectedValue);
            RespuestaSQL respuesta = null;

            if (Session["AccCiu"].ToString() != string.Empty)
            {
                if (Session["AccCiu"].ToString() == "N")
                {
                    respuesta = ctrlCodPostal.CiudadAdd(catCiu);
                }
                if (Session["AccCiu"].ToString() == "U")
                {
                    respuesta = ctrlCodPostal.CiudadMod(catCiu);
                }


                if (respuesta != null)
                {
                    if (respuesta.Codigo != 0)
                    {
                        MsgBox(respuesta.Mensaje, "Ciudad");
                        return;

                    }
                    Session["AccCiu"] = null;
                    HfId.Value = "";
                    VtMdl.Visible = false;
                    LlenaTabla();
                    BtnAceptar_Command(null, null);

                }

            }

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session["AccCiu"] = null;
            HfId.Value = "";
            VtMdl.Visible = false;

        }

        protected void BtnAceptar_Command(object sender, CommandEventArgs e)
        {

        }

        protected void DdlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaEstados();
            VtMdl.Visible = true;
            MpeVtMdl.Show();

        }

        protected void DdlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            VtMdl.Visible = true;
            MpeVtMdl.Show();
        }

        protected void GvConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvConsulta.PageIndex = e.NewPageIndex;
            LlenaTabla();

        }
    }
}