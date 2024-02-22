using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace GuerreroWeb.Views
{
	public partial class PerfilesDet : System.Web.UI.Page
	{
		CtrlUsuarios ctrlUsuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios vtUsuario = new VtUsuarios();
		Perfiles mPerfil = new Perfiles();
		VtPerfiles vtPerfil = new VtPerfiles();

		private string usuario = "";
		private string Accion = "";
		private int IdPer = 0;
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
			if (Session["AccPer"] == null)
			{
				Response.Redirect("~/Views/Usuarios.aspx");
			}
			else
			{
				Accion = Session["AccPer"].ToString();
			}

			if (!vtUsuario.EntraUsuarios)
			{

				Response.Redirect("~/Views/Inicio.aspx");
			}

			if (Session["IdPer"] == null)
			{
				Response.Redirect("~/Views/VstPerfiles.aspx");
			}
			else
			{
				IdPer = Convert.ToInt32(Session["IdPer"].ToString());
			}

			if (!IsPostBack)
			{
				if (Accion == "N")
				{
					TxtPerfil.Text = "";
					llenaArbolPerfil(1);


				}
				else
				{
					llenaArbolPerfil(IdPer);
				}
				//llenaArbolPerfil();
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
			vtUsuario = ctrlUsuario.VtUsuario(usuario);

			if (vtUsuario != null)
			{
				if (vtUsuario.Usuario == "Error")
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
				BtnGuardar.Enabled = vtUsuario.PerfilMod;
			}
			if (Accion == "N")
			{
				BtnGuardar.Enabled = vtUsuario.PerfilAdd;
			}
		}
		private void AccionBtn()
		{
			if (Accion == "O")
			{
				BtnEditar.Enabled = vtUsuario.PerfilMod;
				BtnGuardar.Enabled = false;

				TxtPerfil.ReadOnly = true;
				TvPerfil.Enabled = true;

			}
			if (Accion == "U")
			{
				BtnEditar.Enabled = false;
				BtnGuardar.Enabled = true;

				TxtPerfil.ReadOnly = true;
				TvPerfil.Enabled = true;

			}
			if (Accion == "N")
			{
				BtnEditar.Enabled = false;
				BtnGuardar.Enabled = vtUsuario.PerfilAdd;

				TxtPerfil.ReadOnly = false;
				TvPerfil.Enabled = true;

			}
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void llenaArbolPerfil(int IdPerfil)
		{
			TvPerfil.Nodes.Clear();

			var lista = ctrlUsuario.SpVtPerfiles(IdPerfil);
			string icono = "";
			string idnodo1 = "";
			string idnodo2 = "";
			int Nonodo1 = -1;
			int Nonodo2 = -1;


			if (lista != null)
			{

				if (lista.Count > 0)
				{
					if(Accion == "O" || Accion == "U") 
					{
						TxtPerfil.Text = lista[0].Perfil;
					}
					if(Accion == "N")
					{
						TxtPerfil.Text = "";
					}

					for (int i = 0; i < lista.Count; i++)
					{
						if (lista[i].Valor)
						{
							icono = "~/Resources/Check1.png";
						}
						else
						{
							icono = "~/Resources/Check2.png";
						}

						if (lista[i].IdNodoPadre == "00.00.00")
						{
							TreeNode nodo = new TreeNode(lista[i].IdNodo);

							nodo.Text = lista[i].Descripcion;
							nodo.Value = lista[i].NomCampo;
							nodo.SelectAction = TreeNodeSelectAction.Expand;
							nodo.NavigateUrl = null;
							nodo.Collapse();

							if (Accion == "O")
							{
								nodo.ImageUrl = icono;
								nodo.ShowCheckBox = false;

								TvPerfil.Nodes.Add(nodo);
							}
							if (Accion == "U")
							{
								nodo.ShowCheckBox = true;
								nodo.Checked = lista[i].Valor;

								TvPerfil.Nodes.Add(nodo);

							}
							if(Accion == "N")
							{
								nodo.ShowCheckBox = true;
								nodo.Checked = false;

								TvPerfil.Nodes.Add(nodo);
							}
							Nonodo1++;
							idnodo1 = lista[i].IdNodo;
							Nonodo2 = -1;

						}
						else
						{
							TreeNode nodo = new TreeNode(lista[i].IdNodo);

							nodo.Text = lista[i].Descripcion;
							//nodo.ImageUrl = icono;
							nodo.Value = lista[i].NomCampo;
							nodo.SelectAction = TreeNodeSelectAction.Expand;
							nodo.NavigateUrl = null;
							nodo.Collapse();

							if (idnodo1 == lista[i].IdNodoPadre)
							{
								if(Accion == "O")
								{
									nodo.ImageUrl = icono;
									nodo.ShowCheckBox = false;

									TvPerfil.Nodes[Nonodo1].ChildNodes.Add(nodo);
								}
								if (Accion == "U")
								{
									nodo.ShowCheckBox = true;
									nodo.Checked = lista[i].Valor;

									TvPerfil.Nodes[Nonodo1].ChildNodes.Add(nodo);

								}
								if(Accion == "N")
								{
									nodo.ShowCheckBox = true;
									nodo.Checked = false;

									TvPerfil.Nodes[Nonodo1].ChildNodes.Add(nodo);
								}
								Nonodo2++;

							}
							else
							{
								TreeNode nodo2 = new TreeNode(lista[i].IdNodo);

								nodo2.Text = lista[i].Descripcion;
								nodo2.Value = lista[i].NomCampo;
								nodo2.SelectAction = TreeNodeSelectAction.Expand;
								nodo2.NavigateUrl = null;
								nodo2.Collapse();

								if (idnodo2 != lista[i].IdNodoPadre)
								{
									if (Nonodo2 < 0)
									{
										Nonodo2 = 0;
									}

									idnodo2 = lista[i].IdNodoPadre;
									
									if(Accion == "O")
									{
										nodo2.ImageUrl = icono;
										nodo2.ShowCheckBox = false;
										nodo2.Checked = false;

										TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].ChildNodes.Add(nodo2);
									}

									if (Accion == "U")
									{
										nodo2.ShowCheckBox = true;
										nodo2.Checked = lista[i].Valor;

										TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].ChildNodes.Add(nodo2);

									}
									if(Accion == "N")
									{
										nodo2.ShowCheckBox = true;
										nodo2.Checked = false;

										TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].ChildNodes.Add(nodo2);
									}

								}
								else
								{
								
									if (Accion == "O")
									{
										nodo2.ImageUrl = icono;
										nodo2.ShowCheckBox = false;
										nodo2.Checked = false;

										TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].ChildNodes.Add(nodo2);
									}

									if (Accion == "U")
									{
										nodo2.ShowCheckBox = true;
										nodo2.Checked = lista[i].Valor;

										TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].ChildNodes.Add(nodo2);

									}
									if (Accion == "N")
									{
										nodo2.ShowCheckBox = true;
										nodo2.Checked = false;

										TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].ChildNodes.Add(nodo2);
									}

								}

							}
						}
					}
				}
			}
		}

		protected void BtnEditar_Command(object sender, CommandEventArgs e)
		{
			BtnEditar.Enabled = false;
			Session["AccPer"] = "U";
			Session["IdPer"] = IdPer;
			Response.Redirect("~/Views/PerfilesDet.aspx");

		}

		protected void BtnGuardar_Command(object sender, CommandEventArgs e)
		{
			if (TxtPerfil.Text.Trim() == string.Empty)
			{
				LblAvisoPer.Text = "Este Campo es Obligatorio";
				LblAvisoPer.Visible = true;
				//LblAvisoPer.Attributes["innerHTML"] = "Este Campo es Obligatorio";
				LblAvisoPer.Attributes["Style"] = "display: '';";
				return;
			}
			mPerfil.IdPerfil = IdPer;
			mPerfil.Perfil = TxtPerfil.Text;
			LlenaClaseTree();


			RespuestaSQL respuesta = null;
			if (Accion == "N")
			{
				respuesta = ctrlUsuario.PerfilAdd(mPerfil);

			}
			if (Accion == "U")
			{
				respuesta = ctrlUsuario.PerfilMod(mPerfil);

			}

			if (respuesta != null)
			{
				if (respuesta.Codigo != 0)
				{
					if (respuesta.Codigo == 14)
					{
						LblAvisoPer.Text = "El Perfil ya Existe";
						LblAvisoPer.Visible = true;
						//LblAvisoUsu.Attributes["innerHTML"] = "El usuario ya Existe";
						LblAvisoPer.Attributes["Style"] = "display: '';";
						//ExecJava("LblAvisoUsu", "El usuario ya Existe");
						//LblAvisoUsu.Visible = true;
					}
					else
					{
						MsgBox(respuesta.Mensaje, "Perfil");
					}
					return;
				}
				BtnGuardar.Enabled = false;
				Session["AccPer"] = "O";
				Session["IdPer"] = respuesta.ID;
				Response.Redirect("~/Views/PerfilesDet.aspx");

			}

		}

		protected void BtnCancelar_Command(object sender, CommandEventArgs e)
		{
			BtnCancelar.Enabled = false;
			if (Accion == "O")
			{
				Session["AccPer"] = null;
				Session["IdPer"] = null;
				Response.Redirect("~/Views/VstPerfiles.aspx");
			}
			if (Accion == "U")
			{
				Session["AccPer"] = "O";
				Response.Redirect("~/Views/PerfilesDet.aspx");

			}
			if (Accion == "N")
			{
				Session["AccPer"] = null;
				Session["IdPer"] = null;
				Response.Redirect("~/Views/VstPerfiles.aspx");
			}

		}

		protected void TxtPerfil_TextChanged(object sender, EventArgs e)
		{
			if (TxtPerfil.Text.Trim() == string.Empty)
			{
				//	ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				LblAvisoPer.Text = "";
				LblAvisoPer.Visible = true;
				LblAvisoPer.Attributes["Style"] = "display: '';";
			}
			else
			{
				//ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				LblAvisoPer.Text = "";
				LblAvisoPer.Visible = true;
				LblAvisoPer.Attributes["Style"] = "display: 'none';";
			}
		}
		private void LlenaClaseTree()
		{
			//List<TreeNode> lista = new List<TreeNode>();
			for (int i = 0; i < TvPerfil.Nodes.Count; i++)
			{
				SelValorTree(TvPerfil.Nodes[i].Value, TvPerfil.Nodes[i].Checked);
				for(int i2 = 0;i2 < TvPerfil.Nodes[i].ChildNodes.Count; i2++)
				{
					SelValorTree(TvPerfil.Nodes[i].ChildNodes[i2].Value, TvPerfil.Nodes[i].ChildNodes[i2].Checked);

					for (int i3 = 0; i3 < TvPerfil.Nodes[i].ChildNodes[i2].ChildNodes.Count; i3++)
					{
						SelValorTree(TvPerfil.Nodes[i].ChildNodes[i2].ChildNodes[i3].Value, TvPerfil.Nodes[i].ChildNodes[i2].ChildNodes[i3].Checked);

					}
				}
				//lista.Add(TvPerfil.CheckedNodes[i]);

			}

		}
		private void SelValorTree(string campo, bool valor)
		{
			if (campo == "EntraCxc")
			{
				mPerfil.EntraCxc = valor;
			}
			if (campo == "CxcAdd")
			{
				mPerfil.CxcAdd = valor;
			}
			if (campo == "CxcMod")
			{
				mPerfil.CxcMod = valor;
			}
			if (campo == "CxcCan")
			{
				mPerfil.CxcCan = valor;
			}
			if (campo == "CxcDel")
			{
				mPerfil.CxcDel = valor;
			}
			if (campo == "EntraVentas")
			{
				mPerfil.EntraVentas = valor;
			}
			if (campo == "VentaAdd")
			{
				mPerfil.VentaAdd = valor;
			}
			if (campo == "VentaMod")
			{
				mPerfil.VentaMod = valor;
			}
			if (campo == "VentaCan")
			{
				mPerfil.VentaCan = valor;
			}
			if (campo == "VentaDel")
			{
				mPerfil.VentaDel = valor;
			}
			if (campo == "EntraInv")
			{
				mPerfil.EntraInv = valor;
			}
			if (campo == "EntraMovInv")
			{
				mPerfil.EntraMovInv = valor;
			}
			if (campo == "InvAdd")
			{
				mPerfil.InvAdd = valor;
			}
			if (campo == "InvAdd")
			{
				mPerfil.InvAdd = valor;
			}
			if (campo == "InvMod")
			{
				mPerfil.InvMod = valor;
			}
			if (campo == "InvCan")
			{
				mPerfil.InvCan = valor;
			}
			if (campo == "InvDel")
			{
				mPerfil.InvDel = valor;
			}
			if (campo == "EntraArticulos")
			{
				mPerfil.EntraArticulos = valor;
			}
			if (campo == "ArticuloAdd")
			{
				mPerfil.ArticuloAdd = valor;
			}
			if (campo == "ArticuloMod")
			{
				mPerfil.ArticuloMod = valor;
			}
			if (campo == "ArticuloDel")
			{
				mPerfil.ArticuloDel = valor;
			}
			if (campo == "EntraLineas")
			{
				mPerfil.EntraLineas = valor;
			}
			if (campo == "LineasAdd")
			{
				mPerfil.LineasAdd = valor;
			}
			if (campo == "LineasMod")
			{
				mPerfil.LineasMod = valor;
			}
			if (campo == "LineasDel")
			{
				mPerfil.LineasDel = valor;
			}
			if (campo == "EntraCategoria")
			{
				mPerfil.EntraCategoria = valor;
			}
			if (campo == "CatAdd")
			{
				mPerfil.CatAdd = valor;
			}
			if (campo == "CatMod")
			{
				mPerfil.CatMod = valor;
			}
			if (campo == "CatDel")
			{
				mPerfil.CatDel = valor;
			}
			if (campo == "EntraMarcas")
			{
				mPerfil.EntraMarcas = valor;
			}
			if (campo == "MarcasAdd")
			{
				mPerfil.MarcasAdd = valor;
			}
			if (campo == "MarcasMod")
			{
				mPerfil.MarcasMod = valor;
			}
			if (campo == "MarcasDel")
			{
				mPerfil.MarcasDel = valor;
			}
			if (campo == "EntraModelos")
			{
				mPerfil.EntraModelos = valor;
			}
			if (campo == "ModelosAdd")
			{
				mPerfil.ModelosAdd = valor;
			}
			if (campo == "ModelosMod")
			{
				mPerfil.ModelosMod = valor;
			}
			if (campo == "ModelosDel")
			{
				mPerfil.ModelosDel = valor;
			}
			if (campo == "EntraColores")
			{
				mPerfil.EntraColores = valor;
			}
			if (campo == "ColoresAdd")
			{
				mPerfil.ColoresAdd = valor;
			}
			if (campo == "ColoresMod")
			{
				mPerfil.ColoresMod = valor;
			}
			if (campo == "ColoresDel")
			{
				mPerfil.ColoresDel = valor;
			}
			if (campo == "EntraUnidad")
			{
				mPerfil.EntraUnidad = valor;
			}
			if (campo == "UnidadAdd")
			{
				mPerfil.UnidadAdd = valor;
			}
			if (campo == "UnidadMod")
			{
				mPerfil.UnidadMod = valor;
			}
			if (campo == "UnidadDel")
			{
				mPerfil.UnidadDel = valor;
			}
			if (campo == "EntraCompras")
			{
				mPerfil.EntraCompras = valor;
			}
			if (campo == "CompraAdd")
			{
				mPerfil.CompraAdd = valor;
			}
			if (campo == "CompraMod")
			{
				mPerfil.CompraMod = valor;
			}
			if (campo == "CompraCan")
			{
				mPerfil.CompraCan = valor;
			}
			if (campo == "CompraDel")
			{
				mPerfil.CompraDel = valor;
			}
			if (campo == "EntraCxp")
			{
				mPerfil.EntraCxp = valor;
			}
			if (campo == "CxpAdd")
			{
				mPerfil.CxpAdd = valor;
			}
			if (campo == "CxpMod")
			{
				mPerfil.CxpMod = valor;
			}
			if (campo == "CxpCan")
			{
				mPerfil.CxpCan = valor;
			}
			if (campo == "CxpDel")
			{
				mPerfil.CxpDel = valor;
			}
			if (campo == "EntraReportes")
			{
				mPerfil.EntraReportes = valor;
			}
			if (campo == "EntraRepCxc")
			{
				mPerfil.EntraRepCxc = valor;
			}
			if (campo == "EntraRepVen")
			{
				mPerfil.EntraRepVen = valor;
			}
			if (campo == "EntraRepInv")
			{
				mPerfil.EntraRepInv = valor;
			}
			if (campo == "EntraRepCom")
			{
				mPerfil.EntraRepCom = valor;
			}
			if (campo == "EntraRepCxp")
			{
				mPerfil.EntraRepCxp = valor;
			}
			if (campo == "EntraWati")
			{
				mPerfil.EntraWati = valor;
			}
			if (campo == "EntraWatiEnv")
			{
				mPerfil.EntraWatiEnv = valor;
			}
			if (campo == "EntraWatiCons")
			{
				mPerfil.EntraWatiCons = valor;
			}
			if (campo == "WatiConsAdd")
			{
				mPerfil.WatiConsAdd = valor;
			}
			if (campo == "WatiConsMod")
			{
				mPerfil.WatiConsMod = valor;
			}
			if (campo == "WatiConsDel")
			{
				mPerfil.WatiConsDel = valor;
			}
			if (campo == "EntraWatiPlan")
			{
				mPerfil.EntraWatiPlan = valor;
			}
			if (campo == "WatiPlanAdd")
			{
				mPerfil.WatiPlanAdd = valor;
			}
			if (campo == "WatiPlanMod")
			{
				mPerfil.WatiPlanMod = valor;
			}
			if (campo == "WatiPlanDel")
			{
				mPerfil.WatiPlanDel = valor;
			}
			if (campo == "EntraWatiAuto")
			{
				mPerfil.EntraWatiAuto = valor;
			}
			if (campo == "WatiAdd")
			{
				mPerfil.WatiAdd = valor;
			}
			if (campo == "WatiDel")
			{
				mPerfil.WatiDel = valor;
			}
			if (campo == "WatiMod")
			{
				mPerfil.WatiMod = valor;
			}
			if (campo == "EntraConfiguraciones")
			{
				mPerfil.EntraConfiguraciones = valor;
			}
			if (campo == "EntraEmpresa")
			{
				mPerfil.EntraEmpresa = valor;
			}
			if (campo == "EmpresaMod")
			{
				mPerfil.EmpresaMod = valor;
			}
			if (campo == "EntraSistema")
			{
				mPerfil.EntraSistema = valor;
			}
			if (campo == "EntraModulos")
			{
				mPerfil.EntraModulos = valor;
			}
			if (campo == "ModAdd")
			{
				mPerfil.ModAdd = valor;
			}
			if (campo == "ModMod")
			{
				mPerfil.ModMod = valor;
			}
			if (campo == "ModDel")
			{
				mPerfil.ModDel = valor;
			}
			if (campo == "EntraMovtos")
			{
				mPerfil.EntraMovtos = valor;
			}
			if (campo == "MovtosAdd")
			{
				mPerfil.MovtosAdd = valor;
			}
			if (campo == "MovtosMod")
			{
				mPerfil.MovtosMod = valor;
			}
			if (campo == "EntraFolios")
			{
				mPerfil.EntraFolios = valor;
			}
			if (campo == "FolioAdd")
			{
				mPerfil.FolioAdd = valor;
			}
			if (campo == "FolioMod")
			{
				mPerfil.FolioMod = valor;
			}
			if (campo == "FolioDel")
			{
				mPerfil.FolioDel = valor;
			}
			if (campo == "EntraUsuarios")
			{
				mPerfil.EntraUsuarios = valor;
			}
			if (campo == "UsuarioAdd")
			{
				mPerfil.UsuarioAdd = valor;
			}
			if (campo == "UsuarioMod")
			{
				mPerfil.UsuarioMod = valor;
			}
			if (campo == "UsuarioDel")
			{
				mPerfil.UsuarioDel = valor;
			}
			if (campo == "EntraPerfiles")
			{
				mPerfil.EntraPerfiles = valor;
			}
			if (campo == "PerfilAdd")
			{
				mPerfil.PerfilAdd = valor;
			}
			if (campo == "PerfilMod")
			{
				mPerfil.PerfilMod = valor;
			}
			if (campo == "PerfilDel")
			{
				mPerfil.PerfilDel = valor;
			}
			if (campo == "EntraSucursales")
			{
				mPerfil.EntraSucursales = valor;
			}
			if (campo == "SucursalAdd")
			{
				mPerfil.SucursalAdd = valor;
			}
			if (campo == "SucursalMod")
			{
				mPerfil.SucursalMod = valor;
			}
			if (campo == "SucursalDel")
			{
				mPerfil.SucursalDel = valor;
			}
			if (campo == "EntraAlmacenes")
			{
				mPerfil.EntraAlmacenes = valor;
			}
			if (campo == "AlmacenAdd")
			{
				mPerfil.AlmacenAdd = valor;
			}
			if (campo == "AlmacenMod")
			{
				mPerfil.AlmacenMod = valor;
			}
			if (campo == "AlmacenDel")
			{
				mPerfil.AlmacenDel = valor;
			}
			if (campo == "EntraDepto")
			{
				mPerfil.EntraDepto = valor;
			}
			if (campo == "DeptoAdd")
			{
				mPerfil.DeptoAdd = valor;
			}
			if (campo == "DeptoMod")
			{
				mPerfil.DeptoMod = valor;
			}
			if (campo == "DeptoDel")
			{
				mPerfil.DeptoDel = valor;
			}
			if (campo == "EntraPaises")
			{
				mPerfil.EntraPaises = valor;
			}
			if (campo == "PaisAdd")
			{
				mPerfil.PaisAdd = valor;
			}
			if (campo == "PaisMod")
			{	
				mPerfil.PaisMod = valor;
			}
			if (campo == "PaisDel")
			{
				mPerfil.PaisDel = valor;
			}
			if (campo == "EntraEstados")
			{
				mPerfil.EntraEstados = valor;
			}
			if (campo == "EstadoAdd")
			{
				mPerfil.EstadoAdd = valor;
			}
			if (campo == "EstadoMod")
			{
				mPerfil.EstadoMod = valor;
			}
			if (campo == "EstadoDel")
			{
				mPerfil.EstadoDel = valor;
			}
			if (campo == "EntraCiudades")
			{
				mPerfil.EntraCiudades = valor;
			}
			if (campo == "CiudadAdd")
			{
				mPerfil.CiudadAdd = valor;
			}
			if (campo == "CiudadMod")
			{
				mPerfil.CiudadMod = valor;
			}
			if (campo == "CiudadDel")
			{
				mPerfil.CiudadDel = valor;
			}
			if (campo == "EntraColonias")
			{
				mPerfil.EntraColonias = valor;
			}
			if (campo == "ColoniaAdd")
			{
				mPerfil.ColoniaAdd = valor;
			}
			if (campo == "ColoniaMod")
			{
				mPerfil.ColoniaMod = valor;
			}
			if (campo == "ColoniaDel")
			{
				mPerfil.ColoniaDel = valor;
			}

		}
	}
}