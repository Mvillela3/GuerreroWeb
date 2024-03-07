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
    public partial class AlmacenesDet : System.Web.UI.Page
    {
        CtrlUsuarios ctrlusuario = new CtrlUsuarios();
        CtrlEmpresas ctrlempresa = new CtrlEmpresas();
        VtUsuarios MUsuario = new VtUsuarios();
        CatAlmacenes mAlmacen = new CatAlmacenes();
        VtAlmacenes vtAlmacen = new VtAlmacenes();
        CtrlCodPostal ctrlCodPostal = new CtrlCodPostal();
        private string usuario = "";
        private string Accion = "";
        private int IdAlm = 0;

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
            if (Session["AccAlm"] == null)
            {
                Response.Redirect("~/Views/Almacenes.aspx");
            }
            else
            {
                Accion = Session["AccAlm"].ToString();
            }

            if (!MUsuario.EntraAlmacenes)
            {

                Response.Redirect("~/Views/Inicio.aspx");
            }

            if (Session["IdAlm"] == null)
            {
                Response.Redirect("~/Views/Almacen.aspx");
            }
            else
            {
                IdAlm = Convert.ToInt32(Session["IdAlm"].ToString());
            }

            if (!IsPostBack)
            {
                LlenaPaises();
                LlenaSucursal();
                LlenaEncaegado();
                LlenaConsulta();
                TxtAlmacen.Focus();
            }
            AccionBtn();
            if (Accion == "U")
            {
                BtnCancelar.OnClientClick = "return confirm('¿Esta seguro que desea Deshacer los Cambios?');";
            }
            else if (Accion == "N")
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
            if (Accion == "U")
            {
                BtnGuardar.Enabled = MUsuario.AlmacenMod;
            }
            if (Accion == "N")
            {
                BtnGuardar.Enabled = MUsuario.AlmacenAdd;
            }
        }
        private void AccionBtn()
        {
            if (Accion == "O")
            {
                BtnEditar.Enabled = MUsuario.AlmacenMod;
                BtnGuardar.Enabled = false;

                TxtAlmacen.ReadOnly = true;
                TxtNombre.ReadOnly = true;
                TxtCP.ReadOnly = true;
                TxtDir.ReadOnly = true;
                TxtNoExt.ReadOnly = true;
                TxtNoInt.ReadOnly = true;

                DdlCiu.Enabled = false;
                DdlCol.Enabled = false;
                DdlEstado.Enabled = false;
                DdlPais.Enabled = false;
                DdlSucursal.Enabled = false;
                DdlEncargado.Enabled = false;
            }
            if (Accion == "U")
            {
                BtnEditar.Enabled = false;
                BtnGuardar.Enabled = true;

                TxtAlmacen.ReadOnly = false;
                TxtNombre.ReadOnly = false;
                TxtCP.ReadOnly = false;
                TxtDir.ReadOnly = false;
                TxtNoExt.ReadOnly = false;
                TxtNoInt.ReadOnly = false;

                DdlCiu.Enabled = true;
                DdlCol.Enabled = true;
                DdlEstado.Enabled = true;
                DdlPais.Enabled = true;
                DdlSucursal.Enabled = true;
                DdlEncargado.Enabled = true;

            }
            if (Accion == "N")
            {
                BtnEditar.Enabled = false;
                BtnGuardar.Enabled = MUsuario.AlmacenAdd;

                TxtAlmacen.ReadOnly = false;
                TxtNombre.ReadOnly = false;
                TxtCP.ReadOnly = false;
                TxtDir.ReadOnly = false;
                TxtNoExt.ReadOnly = false;
                TxtNoInt.ReadOnly = false;

                DdlCiu.Enabled = true;
                DdlCol.Enabled = true;
                DdlEstado.Enabled = true;
                DdlPais.Enabled = true;
                DdlSucursal.Enabled = true;
                DdlEncargado.Enabled = true;
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
        private void LlenaSucursal()
        {
            DdlSucursal.DataSource = null;
            DdlSucursal.DataBind();
            List<CatSucursales> lista = ctrlempresa.DdlSucursales();

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlSucursal.DataSource = lista;
                    DdlSucursal.DataTextField = "Sucursal";
                    DdlSucursal.DataValueField = "IdSuc";
                    DdlSucursal.DataBind();
                    DdlSucursal.SelectedIndex = 0;
                }
            }
        }
        private void LlenaEncaegado()
        {
            DdlEncargado.DataSource = null;
            DdlEncargado.DataBind();
            List<ModUsuarios> lista = ctrlusuario.DdlUsuarios();

            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DdlEncargado.DataSource = lista;
                    DdlEncargado.DataTextField = "Nombre";
                    DdlEncargado.DataValueField = "IdUsu";
                    DdlEncargado.DataBind();
                    DdlEncargado.SelectedIndex = 0;
                }
            }
        }
        private void LlenaConsulta()
        {
            VtAlmacenes lista = ctrlempresa.Almacenes(IdAlm);

            if (lista != null)
            {
                if (lista.Sucursal == "Error")
                {
                    return;
                }

                TxtAlmacen.Text = lista.Almacen;
                TxtNombre.Text = lista.Nombre;
                TxtCP.Text = lista.CP;
                TxtDir.Text = lista.Direccion;

                DdlPais.SelectedValue = lista.IdPais.ToString();
                LlenaEstados();
                DdlEstado.SelectedValue = lista.IdEst.ToString();
                LlenaCiudades();
                DdlCiu.SelectedValue = lista.IdCiu.ToString();
                LlenaColonias();
                DdlCol.SelectedValue = lista.IdCol.ToString();
                DdlSucursal.SelectedValue = lista.IdSuc.ToString();
                DdlEncargado.SelectedValue = lista.IdEncargado.ToString();

            }
        }

        private void MsgBox(string msg, string titulo)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

        }

        protected void DdlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnEditar_Command(object sender, CommandEventArgs e)
        {
            BtnEditar.Enabled = false;
            Session["AccAlm"] = "U";
            Session["IdAlm"] = IdAlm;
            Response.Redirect("~/Views/AlmacenesDet.aspx");

        }

        protected void BtnGuardar_Command(object sender, CommandEventArgs e)
        {
            if (TxtAlmacen.Text == string.Empty)
            {
                MsgBox("Escriba la Clave del Almacen", "Almacen");
                return;

            }
            if (TxtNombre.Text == string.Empty)
            {
                MsgBox("Escriba el Nombre del Almacen", "Almacen");
                return;

            }

            BtnGuardar.Enabled = false;
            mAlmacen.IdAlm = IdAlm;
            mAlmacen.Almacen = TxtAlmacen.Text;
            mAlmacen.Nombre = TxtNombre.Text;
            mAlmacen.Direccion = TxtDir.Text;
            mAlmacen.NoExt = TxtNoExt.Text;
            mAlmacen.NoInt = TxtNoInt.Text;
            mAlmacen.IdPais = Convert.ToInt32(DdlPais.SelectedValue.ToString());
            mAlmacen.IdEst = Convert.ToInt32(DdlEstado.SelectedValue.ToString());
            mAlmacen.IdCiu = Convert.ToInt32(DdlCiu.SelectedValue.ToString());
            mAlmacen.IdCol = Convert.ToInt32(DdlCol.SelectedValue.ToString());
            mAlmacen.CP = TxtCP.Text;
            mAlmacen.IdSuc = Convert.ToInt32(DdlSucursal.SelectedValue.ToString());
            mAlmacen.IdEncargado = Convert.ToInt32(DdlEncargado.SelectedValue.ToString());
            RespuestaSQL respuesta = null;

            if (Accion == "N")
            {
                respuesta = ctrlempresa.AlmacenAdd(mAlmacen);

            }
            if (Accion == "U")
            {
                respuesta = ctrlempresa.AlmacenMod(mAlmacen);

            }

            if (respuesta != null)
            {
                if (respuesta.Codigo != 0)
                {
                    MsgBox(respuesta.Mensaje, "Almacen");
                    return;
                }
                Session["AccAlm"] = "O";

                Session["IdAlm"] = respuesta.ID.ToString();
                Response.Redirect("~/Views/AlmacenesDet.aspx");

            }

        }

        protected void BtnCancelar_Command(object sender, CommandEventArgs e)
        {
            BtnCancelar.Enabled = false;
            if(Accion == "N" || Accion == "O")
            {
                Session["AccAlm"] = null;

                Session["IdAlm"] = null;
                Response.Redirect("~/Views/Almacenes.aspx");
            }
            if(Accion == "U")
            {
                Session["AccAlm"] = "O";

                Session["IdAlm"] = IdAlm;
                Response.Redirect("~/Views/AlmacenesDet.aspx");

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