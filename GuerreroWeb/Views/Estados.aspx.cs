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
    public partial class Estados : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlCodPostal ctrlCodPostal = new CtrlCodPostal();
        VtUsuarios VtUsuario = new VtUsuarios();
        CatEstados catEst = new CatEstados();
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
                if (!VtUsuario.EntraEstados)
                {

                    Response.Redirect("~/Inicio.aspx");

                }
                TxtBuscar.Visible = false;
                BtnConslta.Visible = false;
                LblBusca1.Visible = false;
                DdlPaisB.Visible = false;
                LlenaPaises();
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
            List<VtEstados> vtEst = ctrlCodPostal.ListaEstados(TxtBuscar.Text, DdlPaisB.SelectedValue.ToString());

            if (vtEst != null)
            {
                if (vtEst.Count == 0)
                {
                    return;
                }

                if (vtEst[0].Abreviatura == "Error")
                {
                    MsgBox(vtEst[0].Estado, "Estado");
                    return;
                }
                GvConsulta.DataSource = vtEst;
                GvConsulta.DataBind();

                for (int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;
                    ImageButton ibdel = GvConsulta.Rows[i].FindControl("BtnDel") as ImageButton;
                    HiddenField hfdefault = GvConsulta.Rows[i].FindControl("HfDefault") as HiddenField;
                    Image Imdefault = GvConsulta.Rows[i].FindControl("ImgDefault") as Image;

                    if (ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.EstadoMod;
                    }
                    if (ibdel != null)
                    {
                        ibdel.Enabled = VtUsuario.EstadoDel;
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
            BtnNuevo.Enabled = VtUsuario.EstadoAdd;
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

        protected void BtnNuevo_Command(object sender, CommandEventArgs e)
        {
            Session["AccEst"] = "N";
            HfId.Value = "0";
            TxtEstado.Text = "";
            TxtAbrevia.Text = "";
            ChkDefault.Checked = false;
            DdlPais.SelectedIndex = 0;
            TxtEstado.Focus();

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

            }
            else
            {
                TxtBuscar.Visible = true;
                BtnConslta.Visible = true;
                LblBusca1.Visible = true;
                DdlPaisB.Visible = true;

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

            catEst.IdEst = id;

            if (id != 0)
            {
                var respuesta = ctrlCodPostal.EstadoDel(catEst);
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
            Session["AccEst"] = "U";
            // #### cuando borra las el registro
            GridViewRow fila = GvConsulta.Rows[e.NewEditIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);

            HfId.Value = id.ToString();

            if (id != 0)
            {
                var respuesta = ctrlCodPostal.Estado(id);
                if (respuesta != null || respuesta.Estado != string.Empty)
                {
                    if (respuesta.Abreviatura == "Error")
                    {
                        MsgBox(respuesta.Estado, "Eliminar");
                        return;
                    }
                    TxtEstado.Text = respuesta.Estado;
                    TxtAbrevia.Text = respuesta.Abreviatura;
                    ChkDefault.Checked = respuesta.EsDefault;
                    DdlPais.SelectedValue = respuesta.IdPais.ToString();
                    TxtEstado.Focus();

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
            if (TxtEstado.Text == string.Empty)
            {
                MsgBox("Escriba el Nombre del Estado", "Estado");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            if (TxtAbrevia.Text == string.Empty)
            {
                MsgBox("Escriba la Abreviatura del Estado", "Estado");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            if(Convert.ToInt32(DdlPais.SelectedValue.ToString()) == 0)
            {
                MsgBox("Seleccione el Pais del Estado", "Estado");
                VtMdl.Visible = true;
                MpeVtMdl.Show();
                return;

            }
            catEst.IdEst = Convert.ToInt32(HfId.Value.ToString());
            catEst.Estado = TxtEstado.Text;
            catEst.Abreviatura = TxtAbrevia.Text;
            catEst.EsDefault = ChkDefault.Checked;
            catEst.IdPais = Convert.ToInt32(DdlPais.SelectedValue);
            RespuestaSQL respuesta = null;

            if (Session["AccEst"].ToString() != string.Empty)
            {
                if (Session["AccEst"].ToString() == "N")
                {
                    respuesta = ctrlCodPostal.EstadoAdd(catEst);
                }
                if (Session["AccEst"].ToString() == "U")
                {
                    respuesta = ctrlCodPostal.EstadoMod(catEst);
                }


                if (respuesta != null)
                {
                    if (respuesta.Codigo != 0)
                    {
                        MsgBox(respuesta.Mensaje, "Estado");
                        return;

                    }
                    Session["AccEst"] = null;
                    HfId.Value = "";
                    VtMdl.Visible = false;
                    LlenaTabla();
                    BtnAceptar_Command(null, null);

                }

            }

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session["AccEst"] = null;
            HfId.Value = "";
            VtMdl.Visible = false;

        }

        protected void BtnAceptar_Command(object sender, CommandEventArgs e)
        {

        }

        protected void GvConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvConsulta.PageIndex = e.NewPageIndex;
            LlenaTabla();

        }

        protected void DdlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            VtMdl.Visible = true;
            MpeVtMdl.Show();

        }

		protected void DdlPaisB_SelectedIndexChanged(object sender, EventArgs e)
		{
            LlenaTabla();
		}
	}
}