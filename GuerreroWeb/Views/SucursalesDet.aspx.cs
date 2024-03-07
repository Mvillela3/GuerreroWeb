using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime.Misc;
using System.Windows.Forms;

namespace GuerreroWeb.Views
{
    public partial class SucursalesDet : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlEmpresas ctrlempresa = new CtrlEmpresas();
        VtUsuarios MUsuario = new VtUsuarios();
        CatSucursales mSucursal = new CatSucursales();
        VtSucursales vtSucursal = new VtSucursales();
        //MsgBoxIcon msgBoxIcon = new MsgBoxIcon();
        //ModMsgBox modMsgBox = new ModMsgBox();
        //DialogResult RespuestaMsg = new DialogResult();
        CtrlCodPostal ctrlCodPostal = new CtrlCodPostal();
        CtrlFiscal ctrlFiscal = new CtrlFiscal();
        private string usuario = "";
        private string Accion = "";
        private int IdSuc = 0;

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
            if (Session["AccSuc"] == null)
            {
                Response.Redirect("~/Views/Sucursales.aspx");
            }
            else
            {
                Accion = Session["AccSuc"].ToString();
            }

            if (!MUsuario.EntraSucursales)
            {

                Response.Redirect("~/Views/Inicio.aspx");
            }

            if (Session["IdSuc"] == null)
            {
                Response.Redirect("~/Views/Sucursales.aspx");
            }
            else
            {
                IdSuc = Convert.ToInt32(Session["IdSuc"].ToString());
            }

            if (!IsPostBack)
            {
                LlenaPaises();
                LlenaConsulta();
                TxtSucursal.Focus();
            }
            AccionBtn();
            if (Accion == "U")
            {
                BtnCancelar.OnClientClick = "return confirm('¿Esta seguro que desea Deshacer los Cambios?');";
            }
            else if(Accion == "N")
            {
                BtnCancelar.OnClientClick = "return confirm('¿Esta seguro que desea Salir?');";
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
            if(Accion == "U")
            {
                BtnGuardar.Enabled = MUsuario.SucursalMod;
            }
            if (Accion == "N")
            {
                BtnGuardar.Enabled = MUsuario.SucursalAdd;
            }
        }
        private void AccionBtn()
        {
            //if (MUsuario.SucursalMod == false)
            //{
            //    BtnEditar.Enabled = false;
            //    BtnGuardar.Enabled = false;

            //    TxtSucursal.ReadOnly = true;
            //    TxtNombre.ReadOnly = true;
            //    TxtCP.ReadOnly = true;
            //    TxtDir.ReadOnly = true;
            //    TxtNoExt.ReadOnly = true;
            //    TxtNoInt.ReadOnly = true;
            //    TxtTel1.ReadOnly = true;
            //    TxtTel2.ReadOnly = true;

            //    DdlCiu.Enabled = false;
            //    DdlCol.Enabled = false;
            //    DdlEstado.Enabled = false;
            //    DdlPais.Enabled = false;
            //    return;
            //}
            if (Accion == "O")
            {
                BtnEditar.Enabled = MUsuario.SucursalMod;
                BtnGuardar.Enabled = false;

                TxtSucursal.ReadOnly = true;
                TxtNombre.ReadOnly = true;
                TxtCP.ReadOnly = true;
                TxtDir.ReadOnly = true;
                TxtNoExt.ReadOnly = true;
                TxtNoInt.ReadOnly = true;
                TxtTel1.ReadOnly = true;
                TxtTel2.ReadOnly = true;

                DdlCiu.Enabled = false;
                DdlCol.Enabled = false;
                DdlEstado.Enabled = false;
                DdlPais.Enabled = false;
            }
            if (Accion == "U")
            {
                BtnEditar.Enabled = false;
                BtnGuardar.Enabled = true;

                TxtSucursal.ReadOnly = false;
                TxtNombre.ReadOnly = false;
                TxtCP.ReadOnly = false;
                TxtDir.ReadOnly = false;
                TxtNoExt.ReadOnly = false;
                TxtNoInt.ReadOnly = false;
                TxtTel1.ReadOnly = false;
                TxtTel2.ReadOnly = false;

                DdlCiu.Enabled = true;
                DdlCol.Enabled = true;
                DdlEstado.Enabled = true;
                DdlPais.Enabled = true;

            }
            if(Accion == "N") 
            {
                BtnEditar.Enabled = false;
                BtnGuardar.Enabled = MUsuario.SucursalAdd;

                TxtSucursal.ReadOnly = false;
                TxtNombre.ReadOnly = false;
                TxtCP.ReadOnly = false;
                TxtDir.ReadOnly = false;
                TxtNoExt.ReadOnly = false;
                TxtNoInt.ReadOnly = false;
                TxtTel1.ReadOnly = false;
                TxtTel2.ReadOnly = false;

                DdlCiu.Enabled = true;
                DdlCol.Enabled = true;
                DdlEstado.Enabled = true;
                DdlPais.Enabled = true;
            }
        }
        private void LlenaPaises()
        {
            DdlPais.DataSource = null;
            DdlPais.DataBind();

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
                }
            }
            int def = ctrlCodPostal.PaisDef();
            if(def != null)
            {
                if(DdlPais.Items.FindByValue(def.ToString()) != null)
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
        private void LlenaConsulta()
        {
            VtSucursales lista = ctrlempresa.Sucursales(IdSuc);

            if (lista != null)
            {
                if (lista.Sucursal == "Error")
                {
                    return;
                }

                TxtSucursal.Text = lista.Sucursal;
                TxtNombre.Text = lista.Nombre;
                TxtCP.Text = lista.CP;
                TxtDir.Text = lista.Direccion;
                TxtTel1.Text = lista.Telefono1;
                TxtTel2.Text = lista.Telefono2;

                DdlPais.SelectedValue = lista.IdPais.ToString();
                LlenaEstados();
                DdlEstado.SelectedValue = lista.IdEst.ToString();
                LlenaCiudades();
                DdlCiu.SelectedValue = lista.IdCiu.ToString();
                LlenaColonias();
                DdlCol.SelectedValue = lista.IdCol.ToString();

            }
        }

        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }

        protected void BtnEditar_Command(object sender, CommandEventArgs e)
        {
            Session["AccSuc"] = "U";
            Session["IdSuc"] = IdSuc;
            Response.Redirect("~/Views/SucursalesDet.aspx");

        }

        protected void BtnGuardar_Command(object sender, CommandEventArgs e)
        {
            if (TxtSucursal.Text == string.Empty)
            {
                MsgBox("Escriba la Clave de la Sucursal", "Sucursal");
                return;

            }
            if (TxtNombre.Text == string.Empty)
            {
                MsgBox("Escriba el Nombre de la Sucursal", "Sucursal");
                return;

            }

            mSucursal.IdSuc = IdSuc;
            mSucursal.Sucursal = TxtSucursal.Text;
            mSucursal.Nombre = TxtNombre.Text;
            mSucursal.Direccion = TxtDir.Text;
            mSucursal.NoExt = TxtNoExt.Text;
            mSucursal.NoInt = TxtNoInt.Text;
            mSucursal.IdPais = Convert.ToInt32(DdlPais.SelectedValue.ToString());
            mSucursal.IdEst = Convert.ToInt32(DdlEstado.SelectedValue.ToString());
            mSucursal.IdCiu = Convert.ToInt32(DdlCiu.SelectedValue.ToString());
            mSucursal.IdCol = Convert.ToInt32(DdlCol.SelectedValue.ToString());
            mSucursal.CP = TxtCP.Text;
            mSucursal.Telefono1 = TxtTel1.Text;
            mSucursal.Telefono2 = TxtTel2.Text;
            RespuestaSQL respuesta = null;

            if (Accion == "N")
            {
                respuesta = ctrlempresa.SucursalAdd(mSucursal);

            }
            if (Accion == "U")
            {
                respuesta = ctrlempresa.SucursalMod(mSucursal);

            }

            if (respuesta != null)
            {
                if (respuesta.Codigo != 0)
                {
                    MsgBox(respuesta.Mensaje, "Sucursal");
                    return;
                }
                Session["AccSuc"] = "O";
                
                Session["IdSuc"] = respuesta.ID.ToString();
                Response.Redirect("~/Views/SucursalesDet.aspx");

            }

        }

        protected void BtnCancelar_Command(object sender, CommandEventArgs e)
        {
            if (Accion == "O")
            {
                Session["AccSuc"] = null;
                Session["IdSuc"] = null;
                Response.Redirect("~/Views/Sucursales.aspx");
            }
            if (Accion == "U")
            {
                Session["AccSuc"] = "O";
                Response.Redirect("~/Views/SucursalesDet.aspx");

            }
            if (Accion == "N")
            {
                Session["AccSuc"] = null;
                Session["IdSuc"] = null;
                Response.Redirect("~/Views/Sucursales.aspx");

            }

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

        protected void DdlCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cp = ctrlCodPostal.BuscaCP(Convert.ToInt32(DdlCol.SelectedValue.ToString()));

            if (cp != null)
            {
                if(cp.Substring(0,5) != "Error") 
                {
                    TxtCP.Text = cp;
                }
            }

        }

        protected void TxtCP_TextChanged(object sender, EventArgs e)
        {
            if(TxtCP.Text.Length == 5)
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
            if(TxtCP.Text == string.Empty)
            {
                LlenaColonias();
            }

        }
    }
}