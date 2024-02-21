using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Drawing;

namespace GuerreroWeb.Views
{
    public partial class EmpresasDet : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlEmpresas ctrlempresa = new CtrlEmpresas();
        VtUsuarios MUsuario = new VtUsuarios();
        ModEmpresas mEmpresa = new ModEmpresas();
        VtEmpresas vtEmpresa = new VtEmpresas();
        MsgBoxIcon msgBoxIcon = new MsgBoxIcon();
        ModMsgBox modMsgBox = new ModMsgBox();
        DialogResult RespuestaMsg = new DialogResult();
        CtrlCodPostal ctrlCodPostal = new CtrlCodPostal();
        CtrlFiscal ctrlFiscal = new CtrlFiscal();
        private string usuario = "";
        private string Accion = "";
        private int IdEmp = 0;

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

            ChecaUsuario(usuario);
            if (Session["AccEmp"] == null)
            {
                Response.Redirect("~/Views/Empresas.aspx");
            }
            else
            {
                Accion = Session["AccEmp"].ToString();
            }

            if (!MUsuario.EntraEmpresa)
            {
                //modMsgBox.Mensaje = "";
                //modMsgBox.Icono = msgBoxIcon.Information;
                //modMsgBox.Boton = MessageBoxButtons.OK;

                //MsgBoxGro(modMsgBox);

                Response.Redirect("~/Views/Inicio.aspx");
            }

            if (Session["IdEmp"] == null)
            {
                Response.Redirect("~/Views/Empresas.aspx");
            }
            else
            {
                IdEmp = Convert.ToInt32(Session["IdEmp"].ToString());
            }

            if (!IsPostBack)
            {
                LlenaPaises();
                LlenaRegimenF();
                LlenaEmpresas();
            }
            AccionBtn();
            if(Accion == "U")
            {
                BtnCancelar.OnClientClick = "return confirm('¿Esta seguro que desea Deshacer los Cambios?');";
            }
            else
            {
                BtnCancelar.OnClientClick = "";
            }
        }
        private void ChecaUsuario(string usuario)
        {
            MUsuario = ctrlusuario.VtUsuario(usuario);

            if (MUsuario != null)
            {
                if (MUsuario.Usuario == "Error")
                {
                    Response.Redirect("~/Inicio.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Inicio.aspx");
            }
            BtnGuardar.Enabled = MUsuario.EmpresaMod;
        }
        private void LlenaEmpresas()
        {
            VtEmpresas vtEmpresas = ctrlempresa.Empresas(IdEmp);

            if (vtEmpresas != null)
            {
                if (vtEmpresas.Empresa == "Error")
                {
                    return;
                }

                LblEmpresa.Text = vtEmpresas.Empresa;
                LblNombreCom.Text = vtEmpresas.NombreCom;
                LblRFC.Text = vtEmpresas.RFC;
                TxtCP.Text = vtEmpresas.CP;
                TxtDir.Text = vtEmpresas.Direccion;
                TxtEmail.Text = vtEmpresas.Email;
                TxtTel1.Text = vtEmpresas.Telefono1;
                TxtTel2.Text = vtEmpresas.Telefono2;
                TxtWeb.Text = vtEmpresas.PaginaWeb;

                DdlPais.SelectedValue = vtEmpresas.IdPais.ToString();
                LlenaEstados();
                DdlEstado.SelectedValue = vtEmpresas.IdEst.ToString();
                LlenaCiudades();
                DdlCiu.SelectedValue = vtEmpresas.IdCiu.ToString();
                LlenaColonias();
                DdlCol.SelectedValue = vtEmpresas.IdCol.ToString();
                DdlRFiscal.SelectedValue = vtEmpresas.IdRFiscal.ToString();
            }
        }
        private void AccionBtn()
        {
            if (Accion == "O")
            {
                BtnEditar.Enabled = MUsuario.EmpresaMod;
                BtnGuardar.Enabled = false;

                TxtCP.ReadOnly = true;
                TxtDir.ReadOnly = true;
                TxtEmail.ReadOnly = true;
                TxtNoExt.ReadOnly = true;
                TxtNoInt.ReadOnly = true;
                TxtTel1.ReadOnly = true;
                TxtTel2.ReadOnly = true;
                TxtWeb.ReadOnly = true;

                DdlCiu.Enabled = false;
                DdlCol.Enabled = false;
                DdlEstado.Enabled = false;
                DdlPais.Enabled = false;
                DdlRFiscal.Enabled = false;
            }
            if (Accion == "U") 
            {
                BtnEditar.Enabled = false;
                BtnGuardar.Enabled = MUsuario.EmpresaMod;

                TxtCP.ReadOnly = false;
                TxtDir.ReadOnly = false;
                TxtEmail.ReadOnly = false;
                TxtNoExt.ReadOnly = false;
                TxtNoInt.ReadOnly = false;
                TxtTel1.ReadOnly = false;
                TxtTel2.ReadOnly = false;
                TxtWeb.ReadOnly = false;

                DdlCiu.Enabled = true;
                DdlCol.Enabled = true;
                DdlEstado.Enabled = true;
                DdlPais.Enabled = true;
                DdlRFiscal.Enabled = true;

            }
        }
        private void LlenaPaises()
        {
            DdlPais.DataSource = null;
            DdlPais.DataBind();

            List<CatPaises> lista = ctrlCodPostal.DdlPaises();

            if(lista!=null)
            {
                if(lista.Count > 0)
                {
                    DdlPais.DataSource = lista;
                    DdlPais.DataTextField = "Pais";
                    DdlPais.DataValueField = "IdPais";
                    DdlPais.DataBind();
                    DdlPais.SelectedIndex = 0;
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
        private void LlenaCiudades()
        {
            DdlCiu.DataSource = null;
            DdlCiu.DataBind();
            List<CatCiudades> lista = ctrlCodPostal.DdlCiudades(Convert.ToInt32(DdlEstado.SelectedValue.ToString()));

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlCiu.DataSource = lista;
                    DdlCiu.DataTextField = "Ciudad";
                    DdlCiu.DataValueField = "IdCiu";
                    DdlCiu.DataBind();
                    DdlCiu.SelectedIndex = 0;
                }
            }
            int def = ctrlCodPostal.CiuDef();
            if (def != null)
            {
                if (DdlCiu.Items.FindByValue(def.ToString()) != null)
                {
                    DdlCiu.SelectedValue = def.ToString();
                }
            }

        }
        private void LlenaColonias()
        {
            DdlCol.DataSource = null;
            DdlCol.DataBind();
            List<CatColonias> lista = ctrlCodPostal.DdlColonias(Convert.ToInt32(DdlCiu.SelectedValue.ToString()), TxtCP.Text);

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlCol.DataSource = lista;
                    DdlCol.DataTextField = "Colonia";
                    DdlCol.DataValueField = "IdCol";
                    DdlCol.DataBind();
                    DdlCol.SelectedIndex = 0;
                }
            }
            int def = ctrlCodPostal.ColDef();
            if (def != null)
            {
                if (DdlCol.Items.FindByValue(def.ToString()) != null)
                {
                    DdlCol.SelectedValue = def.ToString();
                }
            }

        }
        private void LlenaRegimenF()
        {
            List<CatRegimenFiscal> lista = ctrlFiscal.DdlRegimenF();

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlRFiscal.DataSource = lista;
                    DdlRFiscal.DataTextField = "RegimenF";
                    DdlRFiscal.DataValueField = "IdRFiscal";
                    DdlRFiscal.DataBind();
                    DdlRFiscal.SelectedIndex = 0;
                }
            }
        }
        private void MsgBoxGro(ModMsgBox datos)
        {
            LblMensaje.Text = datos.Mensaje;

            if (datos.Boton == MessageBoxButtons.OK)
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
            if (datos.Boton == MessageBoxButtons.OKCancel)
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
            if (datos.Icono == msgBoxIcon.Warning || datos.Icono == msgBoxIcon.Exclamation)
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
            AjVtModal.Show();

        }

        protected void BtnEditar_Command(object sender, CommandEventArgs e)
        {
            BtnEditar.Enabled = false;
            Session["AccEmp"] = "U";
            Session["IdEmp"] = IdEmp;
            Response.Redirect("~/Views/EmpresasDet.aspx");
        }

        protected void BtnOk_Command(object sender, CommandEventArgs e)
        {
            if(modMsgBox.Boton == MessageBoxButtons.OK)
            {
                RespuestaMsg = DialogResult.OK;
            }
            if(modMsgBox.Boton == MessageBoxButtons.YesNoCancel)
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

        protected void BtnAceptar_Command(object sender, CommandEventArgs e)
        {
            VtMdl.Visible = false;
        }

        protected void DdlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaEstados();
            LlenaCiudades();
            LlenaColonias();
        }

        protected void DdlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaCiudades();
            LlenaColonias();
        }

        protected void DdlCiu_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtCP.Text = "";
            LlenaColonias();
        }

        protected void BtnGuardar_Command(object sender, CommandEventArgs e)
        {
            BtnGuardar.Enabled = false;
            mEmpresa.IdEmpresa = 1;
            mEmpresa.Empresa = LblEmpresa.Text;
            mEmpresa.NombreCom = LblNombreCom.Text;
            mEmpresa.RFC = LblRFC.Text;
            mEmpresa.Direccion = TxtDir.Text;
            mEmpresa.NoExt = TxtNoExt.Text;
            mEmpresa.NoInt = TxtNoInt.Text;
            mEmpresa.IdPais = Convert.ToInt32(DdlPais.SelectedValue.ToString());
            mEmpresa.IdEst = Convert.ToInt32(DdlEstado.SelectedValue.ToString());
            mEmpresa.IdCiu = Convert.ToInt32(DdlCiu.SelectedValue.ToString());
            mEmpresa.IdCol = Convert.ToInt32(DdlCol.SelectedValue.ToString());
            mEmpresa.CP = TxtCP.Text;
            mEmpresa.IdRFiscal = Convert.ToInt32(DdlRFiscal.SelectedValue.ToString());
            mEmpresa.PaginaWeb = TxtWeb.Text;
            mEmpresa.Email = TxtEmail.Text;
            mEmpresa.Telefono1 = TxtTel1.Text;
            mEmpresa.Telefono2 = TxtTel2.Text;
            RespuestaSQL respuesta = ctrlempresa.EmpresaMod(mEmpresa);

            if(respuesta != null)
            {
                if(respuesta.Codigo != 0)
                {
                    MsgBox(respuesta.Mensaje, "Empresa");
                    return;
                }
                Session["AccEmp"] = null;
                Session["IdEmp"] = null;
                Response.Redirect("~/Views/Empresas.aspx");

            }
        }

        protected void BtnCancelar_Command(object sender, CommandEventArgs e)
        {
            BtnCancelar.Enabled = false;
            if (Accion == "O")
            {
                Session["AccEmp"] = null;
                Session["IdEmp"] = null;
                Response.Redirect("~/Views/Empresas.aspx");
            }
            if(Accion == "U")
            {
                Session["AccEmp"] = "O";
                Response.Redirect("~/Views/EmpresasDet.aspx");

            }

        }
        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }

        protected void DdlCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cp = ctrlCodPostal.BuscaCP(Convert.ToInt32(DdlCol.SelectedValue.ToString()));

            if (cp != null)
            {
                if (cp.Substring(0, 5) != "Error")
                {
                    TxtCP.Text = cp;
                }
            }

        }

        protected void TxtCP_TextChanged(object sender, EventArgs e)
        {
            if (TxtCP.Text.Length == 5)
            {
                VtCodPostal cp = ctrlCodPostal.BuscaDatosCp(TxtCP.Text);

                if (cp != null)
                {
                    if (cp.Ciudad != "Error")
                    {
                        if (DdlPais.Items.FindByValue(cp.IdPais.ToString()) != null)
                        {
                            DdlPais.SelectedValue = cp.IdPais.ToString();
                        }
                        LlenaEstados();
                        if (DdlEstado.Items.FindByValue(cp.IdEst.ToString()) != null)
                        {
                            DdlEstado.SelectedValue = cp.IdEst.ToString();
                        }
                        LlenaCiudades();
                        if (DdlCiu.Items.FindByValue(cp.IdCiu.ToString()) != null)
                        {
                            DdlCiu.SelectedValue = cp.IdCiu.ToString();
                        }
                        LlenaColonias();
                        if (DdlCol.Items.FindByValue(cp.IdCol.ToString()) != null)
                        {
                            DdlCol.SelectedValue = cp.IdCol.ToString();
                        }
                        //TxtCP.Text = cp;
                    }
                }

            }
            if (TxtCP.Text == string.Empty)
            {
                LlenaColonias();
            }

        }
    }
}