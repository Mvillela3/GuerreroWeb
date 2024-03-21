using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime.Misc;
using System.Drawing;

namespace GuerreroWeb.Views.Inventarios
{
	public partial class ArticulosDet : System.Web.UI.Page
	{
		CtrlUsuarios ctrlUsuario = new CtrlUsuarios();
		CtrlFiscal ctrlFiscal = new CtrlFiscal();
		CtrlInvCatalogos ctrlInv = new CtrlInvCatalogos();
		VtUsuarios VtUsuario = new VtUsuarios();
		VtArticulos VtArticulo = new VtArticulos();
		CatArticulos MArticulo = new CatArticulos();
		private string usuario = "";
		private string Accion = "";
		private int IdArt = 0;


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
			if (Session["AccArt"] == null)
			{
				Response.Redirect("~/Views(Inventarios/Articulos.aspx");
			}
			else
			{
				Accion = Session["AccArt"].ToString();
			}

			if (!VtUsuario.EntraUsuarios)
			{

				Response.Redirect("~/Views/Inicio.aspx");
			}

			if (Session["IdArt"] == null)
			{
				Response.Redirect("~/Views/Inventarios/Articulos.aspx");
			}
			else
			{
				IdArt = Convert.ToInt32(Session["IdArt"].ToString());
			}

			if (!IsPostBack)
			{
				LlenaLineas();
				LlenaCategoria();
				LlenaFamilia();
				LlenaMarca();
				LlenaModelo();
				LlenaImpuestos();

				if (Accion == "N")
				{
					TxtCodigo.Text = "";
					TxtCodigoCFDI.Text = "";
					TxtDescripcion.Text = "";
					TxtPrecio.Text = "0.00";
					TxtPrecioCont.Text = "0.00";
					TxtPrecioMin.Text = "0.00";
					TxtCContable1.Text = "";
					TxtCContable2.Text = "";
					TxtCContable3.Text = "";
					DdlLinea.SelectedIndex = 0;
					DdlCatego.SelectedIndex = 0;
					DdlEstatus.SelectedValue = "ACTIVO";
					DdlFamilia.SelectedIndex = 0;
					DdlMarca.SelectedIndex = 0;
					DdlModelo.SelectedIndex = 0;
					DdlTipo.SelectedIndex = 0;

					if(DdlTipo.SelectedValue == "PRODUCTO" || DdlTipo.SelectedValue == "SERVICIO")
					{
						Pestana4.Visible = false;
					}
					else
					{
						Pestana4.Visible = true;
					}
					DdlImpuesto1.SelectedValue = "4";
					DdlImpuesto2.SelectedValue = "1";
					DdlImpuesto3.SelectedValue = "1";
					RevisaLineas();
					LlenaUnidad();
					DdlMedida.SelectedValue = "pza";
				}
				else
				{
					LlenaConsulta();
				}
				TxtCodigo.Focus();

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
			VtUsuario = ctrlUsuario.VtUsuario(usuario);

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
			if (Accion == "U")
			{
				BtnGuardar.Enabled = VtUsuario.ArticuloMod;
			}
			if (Accion == "N")
			{
				BtnGuardar.Enabled = VtUsuario.ArticuloAdd;
			}
		}
		private void AccionBtn()
		{
			if (Accion == "O")
			{
				BtnEditar.Enabled = VtUsuario.ArticuloMod;
				BtnGuardar.Enabled = false;

				TxtCodigo.ReadOnly = true;
				TxtDescripcion.ReadOnly = true;
				TxtBarCode.ReadOnly = true;
				TxtPrecio.ReadOnly = true;
				TxtPrecioCont.ReadOnly = true;
				TxtPrecioMin.ReadOnly = true;
				TxtCodigoCFDI.ReadOnly = true;
				TxtCContable1.ReadOnly = true;
				TxtCContable2.ReadOnly = true;
				TxtCContable3.ReadOnly = true;

				DdlLinea.Enabled = false;
				DdlCatego.Enabled = false;
				DdlEstatus.Enabled = false;
				DdlFamilia.Enabled = false;
				DdlMarca.Enabled = false;
				DdlModelo.Enabled = false;
				DdlTipo.Enabled = false;
				DdlImpuesto1.Enabled = false;
				DdlImpuesto2.Enabled = false;
				DdlImpuesto3.Enabled = false;

			}
			if (Accion == "U")
			{
				BtnEditar.Enabled = false;
				BtnGuardar.Enabled = true;

				TxtCodigo.ReadOnly = true;
				TxtDescripcion.ReadOnly = false;
				TxtBarCode.ReadOnly = false;
				TxtPrecio.ReadOnly = false;
				TxtPrecioCont.ReadOnly = false;
				TxtPrecioMin.ReadOnly = false;
				TxtCodigoCFDI.ReadOnly = false;
				TxtCContable1.ReadOnly = false;
				TxtCContable2.ReadOnly = false;
				TxtCContable3.ReadOnly = false;

				DdlLinea.Enabled = false;
				DdlCatego.Enabled = false;
				DdlEstatus.Enabled = true;
				DdlFamilia.Enabled = true;
				DdlMarca.Enabled = true;
				DdlModelo.Enabled = true;
				DdlTipo.Enabled = false;
				DdlImpuesto1.Enabled = true;
				DdlImpuesto2.Enabled = true;
				DdlImpuesto3.Enabled = true;

			}
			if (Accion == "N")
			{
				BtnEditar.Enabled = false;
				BtnGuardar.Enabled = VtUsuario.ArticuloAdd;

				TxtCodigo.ReadOnly = false;
				TxtDescripcion.ReadOnly = false;
				TxtBarCode.ReadOnly = false;
				TxtPrecio.ReadOnly = false;
				TxtPrecioCont.ReadOnly = false;
				TxtPrecioMin.ReadOnly = false;
				TxtCodigoCFDI.ReadOnly = false;
				TxtCContable1.ReadOnly = false;
				TxtCContable2.ReadOnly = false;
				TxtCContable3.ReadOnly = false;

				DdlLinea.Enabled = true;
				DdlCatego.Enabled = true;
				DdlEstatus.Enabled = true;
				DdlFamilia.Enabled = true;
				DdlMarca.Enabled = true;
				DdlModelo.Enabled = true;
				DdlTipo.Enabled = true;
				DdlImpuesto1.Enabled = true;
				DdlImpuesto2.Enabled = true;
				DdlImpuesto3.Enabled = true;

			}
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void LlenaLineas()
		{
			DdlLinea.DataSource = null;
			DdlLinea.DataBind();
			DdlLinea.Items.Clear();

			List<CatLineas> lista = ctrlInv.DdlLineas2(DdlTipo.SelectedValue.ToString());

			if (lista != null)
			{
				if (lista.Count > 0)
				{

					DdlLinea.DataSource = lista;
					DdlLinea.DataTextField = "Linea";
					DdlLinea.DataValueField = "IdLinea";
					DdlLinea.DataBind();
					DdlLinea.SelectedIndex = 0;
				}
			}
		}
		private void LlenaCategoria()
		{
			DdlCatego.DataSource = null;
			DdlCatego.DataBind();
			DdlCatego.Items.Clear();

			if (DdlLinea.Items.Count > 0)
			{
				var lista = ctrlInv.DdlCategoria(Convert.ToInt32(DdlLinea.SelectedValue.ToString()));

				if (lista != null)
				{
					if (lista.Count > 0)
					{
						DdlCatego.DataSource = lista;
						DdlCatego.DataTextField = "Categoria";
						DdlCatego.DataValueField = "IdCat";
						DdlCatego.DataBind();
						DdlCatego.SelectedIndex = 0;

					}
					else
					{
						List<CatCategorias> lista2 = new List<CatCategorias>();
						CatCategorias valores = new CatCategorias();

						valores.IdCat = 0;
						valores.Categoria = "Sin Categorias";
						lista2.Add(valores);

						DdlCatego.DataSource = lista2;
						DdlCatego.DataTextField = "Categoria";
						DdlCatego.DataValueField = "IdCat";
						DdlCatego.DataBind();
						DdlCatego.SelectedIndex = 0;

					}

				}

			}
		}
		private void LlenaFamilia()
		{
			DdlFamilia.DataSource = null;
			DdlFamilia.DataBind();
			DdlFamilia.Items.Clear();

			if (DdlCatego.Items.Count > 0)
			{
				var lista = ctrlInv.DdlFamilia(Convert.ToInt32(DdlCatego.SelectedValue.ToString()));

				if (lista != null)
				{
					if (lista.Count > 0)
					{
						DdlFamilia.DataSource = lista;
						DdlFamilia.DataTextField = "Familia";
						DdlFamilia.DataValueField = "IdFam";
						DdlFamilia.DataBind();
						DdlFamilia.SelectedIndex = 0;

					}
					else
					{
						List<CatFamilias> lista2 = new List<CatFamilias>();
						CatFamilias valores	= new CatFamilias();

						valores.IdFam = 0;
						valores.Familia = "Sin Familias";
						lista2.Add(valores);

						DdlFamilia.DataSource = lista2;
						DdlFamilia.DataTextField = "Familia";
						DdlFamilia.DataValueField = "IdFam";
						DdlFamilia.DataBind();
						DdlFamilia.SelectedIndex = 0;

					}
				}

			}
		}
		private void LlenaMarca()
		{
			
			DdlMarca.DataSource = null;
			DdlMarca.DataBind();
			DdlMarca.Items.Clear();

			if(DdlLinea.Items.Count > 0)
			{
				var lista = ctrlInv.DdlMarca(Convert.ToInt32(DdlLinea.SelectedValue.ToString()));

				if (lista != null)
				{
					if (lista.Count > 0)
					{
						DdlMarca.DataSource = lista;
						DdlMarca.DataTextField = "Marca";
						DdlMarca.DataValueField = "IdMarca";
						DdlMarca.DataBind();
						DdlMarca.SelectedIndex = 0;

					}
					else
					{
						List<CatMarcas> lista2 = new List<CatMarcas>();
						CatMarcas valores = new CatMarcas();

						valores.IdMarca = 0;
						valores.Marca = "Sin Marcas";
						lista2.Add(valores);

						DdlMarca.DataSource = lista2;
						DdlMarca.DataTextField = "Marca";
						DdlMarca.DataValueField = "IdMarca";
						DdlMarca.DataBind();
						DdlMarca.SelectedIndex = 0;

					}

				}

			}
		}
		private void LlenaModelo()
		{
			DdlModelo.DataSource = null;
			DdlModelo.DataBind();
			DdlModelo.Items.Clear();

			if (DdlMarca.Items.Count > 0)
			{
				var lista = ctrlInv.DdlModelos(Convert.ToInt32(DdlMarca.SelectedValue.ToString()));

				if (lista != null)
				{
					if (lista.Count > 0)
					{
						DdlModelo.DataSource = lista;
						DdlModelo.DataTextField = "Modelo";
						DdlModelo.DataValueField = "IdMod";
						DdlModelo.DataBind();
						DdlModelo.SelectedIndex = 0;

					}
					else
					{
						List<CatModelos> lista2 = new List<CatModelos>();
						CatModelos valores = new CatModelos();

						valores.IdMod = 0;
						valores.Modelo = "Sin Modelos";
						lista2.Add(valores);

						DdlModelo.DataSource = lista2;
						DdlModelo.DataTextField = "Modelo";
						DdlModelo.DataValueField = "IdMod";
						DdlModelo.DataBind();
						DdlModelo.SelectedIndex = 0;

					}
				}

			}

		}
		private void LlenaUnidad()
		{
			DdlMedida.DataSource = null;
			DdlMedida.DataBind();
			DdlMedida.Items.Clear();

			if (DdlTipo.Items.Count > 0)
			{
				var lista = ctrlInv.DdlUnidad2(DdlTipo.SelectedValue.ToString());

				if (lista != null)
				{
					if (lista.Count > 0)
					{
						if (lista[0].ClaveSAT != "Error")
						{
							DdlMedida.DataSource = lista;
							DdlMedida.DataTextField = "Descripcion";
							DdlMedida.DataValueField = "Unidad";
							DdlMedida.DataBind();
							DdlMedida.SelectedIndex = 0;

						}

					}
				}

			}

		}
		private void LlenaImpuestos()
		{
			DdlImpuesto1.DataSource = null;
			DdlImpuesto1.DataBind();
			DdlImpuesto1.Items.Clear();

			DdlImpuesto2.DataSource = null;
			DdlImpuesto2.DataBind();
			DdlImpuesto2.Items.Clear();

			DdlImpuesto3.DataSource = null;
			DdlImpuesto3.DataBind();
			DdlImpuesto3.Items.Clear();

			List<CatImpuestos> lista = ctrlFiscal.DdlImpuestos();

			if (lista != null)
			{
				if (lista.Count > 0)
				{

					DdlImpuesto1.DataSource = lista;
					DdlImpuesto1.DataTextField = "Impuesto";
					DdlImpuesto1.DataValueField = "IdImp";
					DdlImpuesto1.DataBind();
					DdlImpuesto1.SelectedIndex = 0;

					DdlImpuesto2.DataSource = lista;
					DdlImpuesto2.DataTextField = "Impuesto";
					DdlImpuesto2.DataValueField = "IdImp";
					DdlImpuesto2.DataBind();
					DdlImpuesto2.SelectedIndex = 0;

					DdlImpuesto3.DataSource = lista;
					DdlImpuesto3.DataTextField = "Impuesto";
					DdlImpuesto3.DataValueField = "IdImp";
					DdlImpuesto3.DataBind();
					DdlImpuesto3.SelectedIndex = 0;
				}
			}
		}
		private void RevisaLineas()
		{
			CatLineas Linea = ctrlInv.Linea(Convert.ToInt32(DdlLinea.SelectedValue.ToString()));

			if(Linea != null)
			{
				int largo = Linea.Linea.Length;

				if(largo >= 5)
				{
					largo = 5;
				}
				Pestana5.Visible = false;
				Pestana6.Visible = false;

				if(Linea.Linea.Substring(0,largo) != "Error") 
				{
					if(DdlTipo.SelectedValue == "PRODUCTO" )
					{
						Pestana5.Visible = Linea.UsaColor;
						Pestana6.Visible = Linea.UsaTalla;
					}
				}
			}

		}
		private void LlenaConsulta()
		{
			var lista = ctrlInv.Articulo(IdArt);
			if (lista != null)
			{
				if (lista.Codigo.Substring(0, 5) == "Error")
				{
					MsgBox(lista.Descripcion, "Articulo");
					return;
				}
				TxtCodigo.Text = lista.Codigo;
				TxtDescripcion.Text = lista.Descripcion;
				TxtCodigoCFDI.Text = lista.CodigoCFDI;
				TxtPrecio.Text = lista.Precio.ToString();
				TxtPrecioCont.Text = lista.Contado.ToString();
				TxtPrecioMin.Text = lista.PrecioMin.ToString();
				TxtCContable1.Text = lista.CContable1;
				TxtCContable2.Text = lista.CContable2;
				TxtCContable3.Text = lista.CContable3;
				LblCostoProm.Text = lista.CostoProm.ToString();
				LblUltCompra.Text = lista.UltCompra.ToString("dd/mm/yyyy");
				LblUltCosto.Text = lista.UltCosto.ToString();

				DdlLinea.SelectedValue = lista.idLin.ToString();
				RevisaLineas();

				DdlEstatus.SelectedValue = lista.Estatus;
				LlenaCategoria();
				DdlCatego.SelectedValue = lista.IdCat.ToString();
				LlenaFamilia();
				DdlFamilia.SelectedValue = lista.IdFam.ToString();

				if(lista.EsPaquete == false && lista.EsServicio == false)
				{
					DdlTipo.SelectedIndex = 0;
					Pestana4.Visible = false;
				}
				if(lista.EsPaquete && !lista.EsServicio)
				{
					DdlTipo.SelectedIndex = 2;
					Pestana4.Visible = true;
				}
				if (!lista.EsPaquete && lista.EsServicio)
				{
					DdlTipo.SelectedIndex = 1;
					Pestana4.Visible = false;
				}
				DdlImpuesto1.SelectedValue = lista.IdImpuesto1.ToString();
				DdlImpuesto2.SelectedValue = lista.IdImpuesto2.ToString();
				DdlImpuesto3.SelectedValue = lista.IdImpuesto3.ToString();
			}
		}

		protected void BtnEditar_Command(object sender, CommandEventArgs e)
		{
			BtnEditar.Enabled = false;
			Session["AccArt"] = "U";
			Session["IdArt"] = IdArt;
			Response.Redirect("~/Views/Inventarios/ArticulosDet.aspx");

		}

		protected void BtnGuardar_Command(object sender, CommandEventArgs e)
		{
			if (TxtDescripcion.Text.Trim() == string.Empty)
			{
				LblMensaje2.Text = "Este Campo es Obligatorio";

				//LblAvisoUsu.Attributes["innerText"] = "Este Campo es Obligatorio";
				//ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				LblMensaje2.Visible = true;
				return;
			}
			//if (TxtNombre.Text.Trim() == string.Empty)
			//{
			//	//ExecJava(TxtNombre.ID, "Este Campo es Obligatorio");
			//	LblAvisoNom.Attributes["Style"] = "display: '';";
			//	//LblAvisoNom.Visible = true;
			//	return;
			//}
			//if (TxtPwd1.Text.Trim() == string.Empty)
			//{
			//	//LblAvisoPwd1.Visible = true;
			//	LblAvisoPwd1.Attributes["Style"] = "display: '';";
			//	//ExecJava(TxtPwd1.ID, "Este Campo es Obligatorio");
			//	return;
			//}
			//if (TxtPwd1.Text != TxtPwd2.Text)
			//{
			//	//LblAvisoPwd2.Visible = true;
			//	LblAvisoPwd2.Attributes["Style"] = "display: '';";
			//	//ExecJava(TxtPwd2.ID,"Los Passwords no Coinciden");
			//	return;
			//}
			//if (DdlDepto.SelectedValue.ToString() == "0")
			//{
			//	LblAvisoDep.Visible = true;
			//	return;
			//}
			//if (GvSucursal.Rows.Count == 0 && Accion == "U")
			//{
			//	LblAvisoSuc.Visible = true;
			//	TcPestana.ActiveTabIndex = 0;
			//	return;
			//}
			//if (DdlPerfil.SelectedIndex.ToString() == "0")
			//{
			//	LblAvisoPer.Visible = true;
			//	TcPestana.ActiveTabIndex = 1;
			//	return;
			//}
			MArticulo.IdArt = IdArt;
			MArticulo.Codigo = TxtCodigo.Text;
			MArticulo.Descripcion = TxtDescripcion.Text;
			MArticulo.idLin = Convert.ToInt32(DdlLinea.SelectedValue.ToString());
			MArticulo.IdCat = Convert.ToInt32(DdlCatego.SelectedValue.ToString());
			MArticulo.IdFam = Convert.ToInt32(DdlFamilia.SelectedValue.ToString());
			MArticulo.IdMarca = Convert.ToInt32(DdlMarca.SelectedValue.ToString());
			MArticulo.IdMod = Convert.ToInt32(DdlModelo.SelectedValue.ToString());
			MArticulo.UsuAdd = usuario;
			MArticulo.UsuDel = usuario;
			MArticulo.UsuMod = usuario;
			MArticulo.BarCode = TxtBarCode.Text;
			MArticulo.Estatus = DdlEstatus.SelectedValue.ToString();
			MArticulo.CostoProm = Convert.ToDecimal(LblCostoProm.Text);
			MArticulo.UltCosto = Convert.ToDecimal(LblUltCosto.Text);
			MArticulo.UltCompra = Convert.ToDateTime(LblUltCompra.Text);
			MArticulo.Precio = Convert.ToDecimal(TxtPrecio.Text);
			MArticulo.PrecioMin = Convert.ToDecimal(TxtPrecioMin.Text);
			MArticulo.Contado = Convert.ToDecimal(TxtPrecioCont.Text);
			MArticulo.Unidad = DdlMedida.SelectedValue.ToString();
			MArticulo.CodigoCFDI = TxtCodigoCFDI.Text;
			MArticulo.CContable1 = TxtCContable1.Text;
			MArticulo.CContable2 = TxtCContable2.Text;
			MArticulo.CContable3 = TxtCContable3.Text;
			MArticulo.IdImpuesto1 = Convert.ToInt32(DdlImpuesto1.SelectedValue.ToString());
			MArticulo.IdImpuesto2 = Convert.ToInt32(DdlImpuesto2.SelectedValue.ToString());
			MArticulo.IdImpuesto3 = Convert.ToInt32(DdlImpuesto3.SelectedValue.ToString());
			MArticulo.FechaAdd = DateTime.Today;
			MArticulo.FechaMod = DateTime.Today;
			MArticulo.FechaDel = DateTime.Today;

			if (DdlTipo.SelectedValue.ToString() == "PRODUCTO")
			{
				MArticulo.EsPaquete = false;
				MArticulo.EsServicio = false;
			}
			if (DdlTipo.SelectedValue.ToString() == "SERVICIO")
			{
				MArticulo.EsPaquete = false;
				MArticulo.EsServicio = true;
			}
			if (DdlTipo.SelectedValue.ToString() == "PAQUETE")
			{
				MArticulo.EsPaquete = true;
				MArticulo.EsServicio = false;
			}



			RespuestaSQL respuesta = null;
			if (Accion == "N")
			{
				respuesta = ctrlInv.ArticuloAdd(MArticulo);

			}
			if (Accion == "U")
			{
				respuesta = ctrlInv.ArticuloMod(MArticulo);

			}

			if (respuesta != null)
			{
				if (respuesta.Codigo != 0)
				{
					if (respuesta.Codigo == 14)
					{
						LblMensaje1.Text = "El Articulo ya Existe";
						LblMensaje1.Visible = true;
						//LblAvisoUsu.Attributes["innerHTML"] = "El usuario ya Existe";
						LblMensaje1.Attributes["Style"] = "display: '';";
						//ExecJava("LblAvisoUsu", "El usuario ya Existe");
						//LblAvisoUsu.Visible = true;
					}
					else
					{
						MsgBox(respuesta.Mensaje, "Usuario");
					}
					return;
				}
				BtnGuardar.Enabled = false;
				Session["AccArt"] = "O";
				Session["IdArt"] = respuesta.ID;
				Response.Redirect("~/Views/Inventarios/ArticulosDet.aspx");

			}

		}

		protected void BtnCancelar_Command(object sender, CommandEventArgs e)
		{
			BtnCancelar.Enabled = false;
			if (Accion == "O")
			{
				Session["AccArt"] = null;
				Session["IdArt"] = null;
				Response.Redirect("~/Views/Inventarios/Articulos.aspx");
			}
			if (Accion == "U")
			{
				Session["AccArt"] = "O";
				Response.Redirect("~/Views/Inventarios/ArticulosDet.aspx");

			}
			if (Accion == "N")
			{
				Session["AccArt"] = null;
				Session["IdArt"] = null;
				Response.Redirect("~/Views/Inventarios/Articulos.aspx");
			}

		}

		protected void DdlLinea_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaCategoria();
			LlenaFamilia();
			LlenaMarca();
			LlenaModelo();
			RevisaLineas();
		}

		protected void DdlCatego_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaFamilia();
		}

		protected void DdlFamilia_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		protected void DdlMarca_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaModelo();
		}

		protected void DdlTipo_SelectedIndexChanged(object sender, EventArgs e)
		{
			LlenaLineas();
			LlenaCategoria();
			LlenaMarca();
			LlenaModelo();
			LlenaUnidad();
			RevisaLineas();

			if (DdlTipo.SelectedValue == "PAQUETE")
			{
				Pestana4.Visible = true;
			}
			if (DdlTipo.SelectedValue == "SERVICIO")
			{
				Pestana4.Visible = false;
			}
			if (DdlTipo.SelectedValue == "PRODUCTO")
			{
				Pestana4.Visible = false;
				DdlMedida.SelectedValue = "pza";
			}
		}

		protected void TxtUsuario_TextChanged(object sender, EventArgs e)
		{
			if(TxtCodigo.Text == string.Empty)
			{
				LblMensaje1.Visible = true;
			}
			else
			{
				LblMensaje1.Visible = false;
			}
		}

		protected void TxtDescripcion_TextChanged(object sender, EventArgs e)
		{
			if (TxtDescripcion.Text == string.Empty)
			{
				LblMensaje2.Visible = true;
			}
			else
			{
				LblMensaje2.Visible = false;
			}
		}

		protected void TcPestana_ActiveTabChanged(object sender, EventArgs e)
		{
			LblPestana1.BackColor = Color.Yellow;
			LblPestana2.BackColor = Color.Yellow;
			LblPestana3.BackColor = Color.Yellow;
			LblPestana4.BackColor = Color.Yellow;
			LblPestana5.BackColor = Color.Yellow;
			LblPestana6.BackColor = Color.Yellow;
			LblPestana7.BackColor = Color.Yellow;


			if (TcPestana.ActiveTabIndex == 0)
			{
				LblPestana1.BackColor = Color.Orange;
			}
			if (TcPestana.ActiveTabIndex == 1)
			{
				LblPestana2.BackColor = Color.Orange;
			}
			if (TcPestana.ActiveTabIndex == 2)
			{
				LblPestana3.BackColor = Color.Orange;
			}
			if (TcPestana.ActiveTabIndex == 3)
			{
				LblPestana4.BackColor = Color.Orange;
			}
			if (TcPestana.ActiveTabIndex == 4)
			{
				LblPestana5.BackColor = Color.Orange;
			}
			if (TcPestana.ActiveTabIndex == 5)
			{
				LblPestana6.BackColor = Color.Orange;
			}
			if (TcPestana.ActiveTabIndex == 6)
			{
				LblPestana7.BackColor = Color.Orange;
			}
		}
	}
}
