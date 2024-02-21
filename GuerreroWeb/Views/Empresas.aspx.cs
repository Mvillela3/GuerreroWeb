using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace GuerreroWeb.Views
{
    public partial class Empresas : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlEmpresas ctrlempresa = new CtrlEmpresas();
        VtUsuarios VtUsuario = new VtUsuarios();
        ModEmpresas mEmpresa = new ModEmpresas();
        VtEmpresas vtEmpresa = new VtEmpresas();
        MsgBoxIcon msgBoxIcon = new MsgBoxIcon();
        DialogResult RespuestaMsg = new DialogResult();
        ModMsgBox modMsgBox = new ModMsgBox();


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

            if (!IsPostBack)
            {
                ChecaUsuario(usuario);
                if (!VtUsuario.EntraEmpresa)
                {

                    Response.Redirect("~/Inicio.aspx");

                }
                TxtBuscar.Visible = false;
                LlenaTabla();
            }
        }
        private void ChecaUsuario(string usuario)
        {
			VtUsuario = ctrlusuario.VtUsuario(usuario);

            if(VtUsuario != null)
            {
                if(VtUsuario.Usuario == "Error")
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
            List<VtEmpresas> vtEmpresas = ctrlempresa.ListaEmpresas();

            if(vtEmpresas != null)
            {
                if(vtEmpresas.Count == 0)
                {
                    return;
                }

                if (vtEmpresas[0].Empresa == "Error")
                {
                    return;
                }
                GvConsulta.DataSource = vtEmpresas;
                GvConsulta.DataBind();

                for(int i = 0; i < GvConsulta.Rows.Count; i++)
                {
                    ImageButton ibeditar = GvConsulta.Rows[i].FindControl("BtnEditar") as ImageButton;

                    if(ibeditar != null)
                    {
                        ibeditar.Enabled = VtUsuario.EntraEmpresa;
                    }
                }
            }
        }

        protected void BtnBuscar_Command(object sender, CommandEventArgs e)
        {
            if (TxtBuscar.Visible)
            {
                TxtBuscar.Visible= false;
            }
            else
            {
                TxtBuscar.Visible = true;
            }
        }

        protected void GvConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            GridViewRow fila = GvConsulta.Rows[GvConsulta.SelectedIndex];

            int id = Convert.ToInt32((fila.FindControl("LblId") as System.Web.UI.WebControls.Label).Text);

            if (id != 0)
            {
                Session["AccEmp"] = "O";
                Session["IdEmp"] = id.ToString();
                Response.Redirect("~/Views/EmpresasDet.aspx");
            }

        }

        protected void BtnEditar_Command(object sender, CommandEventArgs e)
        {
            //LblMensaje.Text = "Prueba de mensaje modal";

            modMsgBox.Mensaje = "Prueba de mensaje modal";
            modMsgBox.Icono = msgBoxIcon.Question;
            modMsgBox.Boton = MessageBoxButtons.YesNo;

            MsgBoxGro(modMsgBox);

            if(RespuestaMsg == DialogResult.Yes)
            {
                LblTitulo.Text = "respuesta si";
            }
            if (RespuestaMsg == DialogResult.No)
            {
                LblTitulo.Text = "respuesta No";
            }

            // string script = "$('#mymodal').modal('show');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", script, true);

            //AjVtModal.Show();
            //return false;

            //LblMensaje.CssClass = "alert alert-info";
            //LblMensaje.Text = "prueba de mensaje1";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Usuario", "alert('Tu Usuario no tiene permisos para entrar al modulo'); ", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //Response.Write("<script language='JavaScript'> var WinSettings = 'center:yes;resizable:no;dialogHeight:450px;dialogWidth:800px';var MyArgs = window.showModalDialog('../Construccion.aspx',MyArgs,WinSettings);");


            //int fila = GvConsulta.SelectedIndex;

            //if (fila == -1)
            //{
            //    return;
            //}

        }
        private void MsgBoxGro(ModMsgBox datos)
        {
            LblMensaje.Text = datos.Mensaje;

            if(datos.Boton == MessageBoxButtons.OK)
            {
                BtnSi.Visible = false;
                BtnOk.Visible = true;
                BtnNo.Visible = false;
                BtnOk.Text = "OK";
                BtnOk.BackColor = Color.FromArgb(128, 255, 255);
            }
            if (datos.Boton == MessageBoxButtons.YesNo)
            {
                BtnSi.Visible = true;
                BtnOk.Visible = false;
                BtnNo.Visible = true;
                BtnSi.Text = "Si";
                BtnNo.Text = "No";
                BtnSi.BackColor = Color.Lime;
                BtnNo.BackColor = Color.Red;
            }
            if(datos.Boton == MessageBoxButtons.OKCancel)
            {
                BtnSi.Visible = true;
                BtnOk.Visible = false;
                BtnNo.Visible = true;
                BtnSi.Text = "Ok";
                BtnNo.Text = "No";
                BtnSi.BackColor = Color.Lime;
                BtnNo.BackColor = Color.Red;

            }
            if (datos.Boton == MessageBoxButtons.YesNoCancel)
            {
                BtnSi.Visible = true;
                BtnOk.Visible = true;
                BtnNo.Visible = true;
                BtnSi.Text = "Si";
                BtnOk.Text = "No";
                BtnNo.Text = "Cancelar";
                BtnSi.BackColor = Color.Lime;
                BtnOk.BackColor = Color.FromArgb(255, 128, 0);
                BtnNo.BackColor = Color.Red;

            }
            if (datos.Boton == MessageBoxButtons.AbortRetryIgnore)
            {
                BtnSi.Visible = true;
                BtnOk.Visible = true;
                BtnNo.Visible = true;
                BtnSi.Text = "Si";
                BtnOk.Text = "No";
                BtnNo.Text = "Cancelar";
                BtnSi.BackColor = Color.Lime;
                BtnOk.BackColor = Color.FromArgb(255, 128, 0);
                BtnNo.BackColor = Color.Red;

            }
            if(datos.Icono == msgBoxIcon.Warning || datos.Icono == msgBoxIcon.Exclamation)
            {
                ImgModal.ImageUrl = "~/Resources/Advertencia.png";
            }
            if (datos.Icono == msgBoxIcon.Error || datos.Icono == msgBoxIcon.Stop || datos.Icono == msgBoxIcon.Hand)
            {
                ImgModal.ImageUrl = "~/Resources/Error.png";
            }
            if (datos.Icono == msgBoxIcon.Question)
            {
                ImgModal.ImageUrl = "~/Resources/Interrogacion.png";
            }
            if (datos.Icono == msgBoxIcon.OK)
            {
                ImgModal.ImageUrl = "~/Resources/Ok.png";
            }
            if (datos.Icono == msgBoxIcon.Information || datos.Icono == msgBoxIcon.Asterisk)
            {
                ImgModal.ImageUrl = "~/Resources/Informacion.png";
            }
            if (datos.Icono == msgBoxIcon.None)
            {
                ImgModal.ImageUrl = "";
            }

            VtMdl.Visible = true;
            //ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);

            VtMdl_ModalPopupExtender.Show();
            //VtMdl_PopupControlExtender.registerdataIthem;
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
            //VtMdl.Visible = false;
        }

        protected void BtnSi_Command(object sender, CommandEventArgs e)
        {
            if (modMsgBox.Boton == MessageBoxButtons.OKCancel)
            {
                RespuestaMsg = DialogResult.OK;
            }
            if (modMsgBox.Boton == MessageBoxButtons.YesNoCancel || modMsgBox.Boton == MessageBoxButtons.YesNo)
            {
                RespuestaMsg = DialogResult.Yes;
            }
            if (modMsgBox.Boton == MessageBoxButtons.AbortRetryIgnore)
            {
                RespuestaMsg = DialogResult.Abort;
            }
            BtnAceptar_Command(null, null);

        }

        protected void BtnOk_Command(object sender, CommandEventArgs e)
        {
            if (modMsgBox.Boton == MessageBoxButtons.OK)
            {
                RespuestaMsg = DialogResult.OK;
            }
            if (modMsgBox.Boton == MessageBoxButtons.YesNoCancel)
            {
                RespuestaMsg = DialogResult.No;
            }
            if (modMsgBox.Boton == MessageBoxButtons.AbortRetryIgnore)
            {
                RespuestaMsg = DialogResult.Retry;
            }
            BtnAceptar_Command(null, null);
        }

        protected void BtnNo_Command(object sender, CommandEventArgs e)
        {
            if (modMsgBox.Boton == MessageBoxButtons.YesNo)
            {
                RespuestaMsg = DialogResult.No;
            }
            if (modMsgBox.Boton == MessageBoxButtons.YesNoCancel || modMsgBox.Boton == MessageBoxButtons.OKCancel || modMsgBox.Boton == MessageBoxButtons.RetryCancel)
            {
                RespuestaMsg = DialogResult.Cancel;
            }
            if (modMsgBox.Boton == MessageBoxButtons.AbortRetryIgnore)
            {
                RespuestaMsg = DialogResult.Ignore;
            }
            BtnAceptar_Command(null, null);

        }

        protected void BtnVtMdl_Command(object sender, CommandEventArgs e)
        {
            VtMdl.Visible = false;
        }

        protected void BtnSi_Click(object sender, EventArgs e)
        {
            //BtnVtMdl_Command(null, null);

        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            //BtnVtMdl_Command(null, null);

        }

        protected void BtnNo_Click(object sender, EventArgs e)
        {
            //BtnVtMdl_Command(null, null);

        }
        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }

        protected void GvConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvConsulta.PageIndex = e.NewPageIndex;
            LlenaTabla();

        }
    }
}