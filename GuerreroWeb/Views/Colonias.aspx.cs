using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace GuerreroWeb.Views
{
    public partial class Colonias : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlCodPostal ctrlCodPostal = new CtrlCodPostal();
        VtUsuarios VtUsuario = new VtUsuarios();
        CatColonias catCol = new CatColonias();
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
                if (!VtUsuario.EntraColonias)
                {

                    Response.Redirect("~/Inicio.aspx");

                }
                TxtBuscar.Visible = false;
                BtnConslta.Visible = false;
                LblBusca1.Visible = false;
                DdlPaisB.Visible = false;
                LblBusca2.Visible = false;
                DdlEstadoB.Visible = false;
                LblBusca3.Visible = false;
                DdlCiudadB.Visible = false;
                LlenaPaises();
                LlenaEstados();
                LlenaEstados2();
                LlenaCiudades();
                LlenaCiudades2();
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
            List<VtCodPostal> vtCol = ctrlCodPostal.ListaColonias(TxtBuscar.Text, DdlPaisB.SelectedValue.ToString(), DdlEstadoB.SelectedValue.ToString(), DdlCiudadB.SelectedValue.ToString());

            if (vtCol != null)
            {
                if (vtCol.Count == 0)
                {
                    return;
                }

                if (vtCol[0].CP == "Error")
                {
                    MsgBox(vtCol[0].Colonia, "Ciudad");
                    return;
                }
                GvConsulta.DataSource = vtCol;
                GvConsulta.DataBind();

                for (int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
                    ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
                    HiddenField hfdefault = GvConsulta.Rows[i].FindControl("HfDefault") as HiddenField;
                    Image Imdefault = GvConsulta.Rows[i].FindControl("ImgDefault") as Image;

                    if (ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.ColoniaMod;
                    }
                    if (ibdel != null)
                    {
                        ibdel.Enabled = VtUsuario.ColoniaDel;
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
        private void LlenaCiudades()
        {
            DdlCiudad.DataSource = null;
            DdlCiudad.DataBind();
            List<CatCiudades> lista = ctrlCodPostal.DdlCiudades(Convert.ToInt32(DdlEstado.SelectedValue.ToString()));

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlCiudad.DataSource = lista;
                    DdlCiudad.DataTextField = "Ciudad";
                    DdlCiudad.DataValueField = "IdCiu";
                    DdlCiudad.DataBind();
                    DdlCiudad.SelectedIndex = 0;
                }
            }
            int def = ctrlCodPostal.CiuDef();
            if (def != null)
            {
                if (DdlCiudad.Items.FindByValue(def.ToString()) != null)
                {
                    DdlCiudad.SelectedValue = def.ToString();
                }
            }

        }
        private void LlenaCiudades2()
        {
            DdlCiudadB.DataSource = null;
            DdlCiudadB.DataBind();
            List<CatCiudades> lista = ctrlCodPostal.DdlCiudades(Convert.ToInt32(DdlEstadoB.SelectedValue.ToString()));

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlCiudadB.DataSource = lista;
                    DdlCiudadB.DataTextField = "Ciudad";
                    DdlCiudadB.DataValueField = "IdCiu";
                    DdlCiudadB.DataBind();
                    DdlCiudadB.SelectedIndex = 0;
                }
            }
            int def = ctrlCodPostal.CiuDef();
            if (def != null)
            {
                if (DdlCiudadB.Items.FindByValue(def.ToString()) != null)
                {
                    DdlCiudadB.SelectedValue = def.ToString();
                }
            }

        }

        protected void DdlPaisB_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaEstados2();
        }

        protected void DdlEstadoB_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaCiudades2();
        }

        protected void BtnNuevo_Command(object sender, CommandEventArgs e)
        {
            Session["AccCol"] = "N";
            HfId.Value = "0";
            TxtColonia.Text = "";
            TxtCP.Text = "";
            ChkDefault.Checked = false;
            DdlPais.SelectedValue = "0";
            DdlEstado.SelectedValue = "0";
            DdlCiudad.SelectedValue = "0";
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
                LblBusca3.Visible = false;
                DdlCiudadB.Visible = false;

            }
            else
            {
                TxtBuscar.Visible = true;
                BtnConslta.Visible = true;
                LblBusca1.Visible = true;
                DdlPaisB.Visible = true;
                LblBusca2.Visible = true;
                DdlEstadoB.Visible = true;
                LblBusca3.Visible = true;
                DdlCiudadB.Visible = true;

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
            catCol.IdCol = id;

            if (id != 0)
            {
                var respuesta = ctrlCodPostal.ColoniaDel(catCol);
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
            Session["AccCol"] = "U";
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            HfId.Value = id.ToString();

            if (id != 0)
            {
                var respuesta = ctrlCodPostal.Colonia(id);
                if (respuesta != null || respuesta.Colonia != string.Empty)
                {
                    if (respuesta.CP == "Error")
                    {
                        MsgBox(respuesta.Colonia, "Colonia");
                        return;
                    }
                    TxtColonia.Text = respuesta.Colonia;
                    ChkDefault.Checked = respuesta.EsDefault;
                    TxtCP.Text = respuesta.CP;
                    DdlPais.SelectedValue = respuesta.IdPais.ToString();
                    LlenaEstados();
                    DdlEstado.SelectedValue = respuesta.IdEst.ToString();
                    LlenaCiudades();
                    DdlCiudad.SelectedValue = respuesta.IdCiu.ToString();

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
            if (TxtColonia.Text == string.Empty)
            {
                MsgBox("Escriba el Nombre de la Colonia", "Colonia");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            if (TxtCP.Text == string.Empty)
            {
                MsgBox("Escriba el Codigo Postal de la Colonia", "Colonia");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            if (Convert.ToInt32(DdlPais.SelectedValue.ToString()) == 0)
            {
                MsgBox("Seleccione el Pais de la Colonia", "Colonia");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            if (Convert.ToInt32(DdlEstado.SelectedValue.ToString()) == 0)
            {
                MsgBox("Seleccione el Estado de la Colonia", "Colonia");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }

            catCol.IdCol = Convert.ToInt32(HfId.Value.ToString());
            catCol.Colonia = TxtColonia.Text;
            catCol.CP = TxtCP.Text;
            catCol.EsDefault = ChkDefault.Checked;
            catCol.IdCiu = Convert.ToInt32(DdlCiudad.SelectedValue);
            RespuestaSQL respuesta = null;

            if (Session["AccCol"].ToString() != string.Empty)
            {
                if (Session["AccCol"].ToString() == "N")
                {
                    respuesta = ctrlCodPostal.ColoniaAdd(catCol);
                }
                if (Session["AccCol"].ToString() == "U")
                {
                    respuesta = ctrlCodPostal.ColoniaMod(catCol);
                }


                if (respuesta != null)
                {
                    if (respuesta.Codigo != 0)
                    {
                        MsgBox(respuesta.Mensaje, "Colonia");
                        return;

                    }
                    Session["AccCol"] = null;
                    HfId.Value = "";
                    VtMdl.Visible = false;
                    LlenaTabla();
                    BtnAceptar_Command(null, null);

                }

            }

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session["AccCol"] = null;
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
            LlenaCiudades();
            VtMdl.Visible = true;
            MpeVtMdl.Show();

        }

        protected void DdlCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            VtMdl.Visible = true;
            MpeVtMdl.Show();
        }

    }
}