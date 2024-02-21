using GuerreroWeb.Models;
using GuerreroWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Collections;
//using System.Windows.Forms;

namespace GuerreroWeb.Views
{
	public partial class UsuariosDet : System.Web.UI.Page
	{
		CtrlUsuarios ctrlUsuario = new CtrlUsuarios();
		CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();
		VtUsuarios vtUsuario = new VtUsuarios();
		ModUsuarios mUsuario = new ModUsuarios();
		VtPerfiles vtPerfil = new VtPerfiles();

		private string usuario = "";
		private string Accion = "";
		private int IdUsu = 0;

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
			if (Session["AccUsu"] == null)
			{
				Response.Redirect("~/Views/Usuarios.aspx");
			}
			else
			{
				Accion = Session["AccUsu"].ToString();
			}

			if (!vtUsuario.EntraUsuarios)
			{

				Response.Redirect("~/Views/Inicio.aspx");
			}

			if (Session["IdUsu"] == null)
			{
				Response.Redirect("~/Views/Usuarios.aspx");
			}
			else
			{
				IdUsu = Convert.ToInt32(Session["IdUsu"].ToString());
			}

			if (!IsPostBack)
			{
				LlenaDeptos();
				LlenaPerfiles();
				LlenaSuc();
				if(Accion == "N")
				{
					TxtUsuario.Text = "";
					TxtNombre.Text = "";
					TxtPwd1.Text = "";
					TxtPwd2.Text = "";
					TxtEmail.Text = "";
					TxtTel.Text = "";
					DdlDepto.SelectedValue = "0";
					DdlEstatus.SelectedValue = "ACTIVO";
					DdlPerfil.SelectedValue = "0";


				}
				else
				{
					LlenaConsulta();
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
				BtnGuardar.Enabled = vtUsuario.UsuarioMod;
			}
			if (Accion == "N")
			{
				BtnGuardar.Enabled = vtUsuario.UsuarioAdd;
			}
		}
		private void AccionBtn()
		{
			if (Accion == "O")
			{
				BtnEditar.Enabled = vtUsuario.UsuarioMod;
				BtnGuardar.Enabled = false;

				TxtUsuario.ReadOnly = true;
				TxtNombre.ReadOnly = true;
				TxtPwd1.ReadOnly = true;
				TxtPwd2.ReadOnly = true;
				TxtEmail.ReadOnly = true;
				TxtTel.ReadOnly = true;

				DdlDepto.Enabled = false;
				DdlPerfil.Enabled = false;
				DdlEstatus.Enabled = false;
				DdlSucursal.Enabled = false;
				BtnSucursal.Enabled = false;
				DdlPerfil.Enabled = false;

			}
			if (Accion == "U")
			{
				BtnEditar.Enabled = false;
				BtnGuardar.Enabled = true;

				TxtUsuario.ReadOnly = true;
				TxtNombre.ReadOnly = false;
				TxtPwd1.ReadOnly = false;
				TxtPwd2.ReadOnly = false;
				TxtEmail.ReadOnly = false;
				TxtTel.ReadOnly = false;

				DdlDepto.Enabled = true;
				if (vtUsuario.IdPerfil == 1)
				{
					DdlEstatus.Enabled = true;
					DdlSucursal.Enabled = true;
					BtnSucursal.Enabled = true;
					DdlPerfil.Enabled = true;

				}
				else
				{
					DdlEstatus.Enabled = false;
					DdlSucursal.Enabled = false;
					BtnSucursal.Enabled = false;
					DdlPerfil.Enabled = false;
				}

			}
			if (Accion == "N")
			{
				BtnEditar.Enabled = false;
				BtnGuardar.Enabled = vtUsuario.UsuarioAdd;

				TxtUsuario.ReadOnly = false;
				TxtNombre.ReadOnly = false;
				TxtPwd1.ReadOnly = false;
				TxtPwd2.ReadOnly = false;
				TxtEmail.ReadOnly = false;
				TxtTel.ReadOnly = false;

				if (vtUsuario.IdPerfil == 1)
				{
					DdlEstatus.Enabled = false;
					DdlSucursal.Enabled = true;
					BtnSucursal.Enabled = false;
					DdlPerfil.Enabled = true;

				}
				else
				{
					DdlEstatus.Enabled = false;
					DdlSucursal.Enabled = false;
					BtnSucursal.Enabled = false;
					DdlPerfil.Enabled = false;
				}
				DdlDepto.Enabled = true;
			}
		}
		private void MsgBox(string msg, string titulo)
		{
			ScriptManager.RegisterStartupScript(this, typeof(string), titulo, "alert('" + msg + "'); ", true);

		}
		private void LlenaDeptos()
		{
			DdlDepto.DataSource = null;
			DdlDepto.DataBind();

			var lista = ctrlEmpresa.DdlDepartamentos();

			if (lista != null)
			{
				if (lista.Count > 0 && lista[0].Departamento.Substring(0, 5) != "Error")
				{

					DdlDepto.DataSource = lista;
					DdlDepto.DataTextField = "Departamento";
					DdlDepto.DataValueField = "IdDepto";
					DdlDepto.DataBind();
					DdlDepto.SelectedValue = "0";
				}
			}

		}
		private void LlenaPerfiles()
		{
			DdlPerfil.DataSource = null;
			DdlPerfil.DataBind();

			var lista = ctrlUsuario.DdlPerfiles();

			if (lista != null)
			{
				if (lista.Count > 0 && lista[0].Perfil.Substring(0, 5) != "Error")
				{

					DdlPerfil.DataSource = lista;
					DdlPerfil.DataTextField = "Perfil";
					DdlPerfil.DataValueField = "IdPerfil";
					DdlPerfil.DataBind();
					DdlPerfil.SelectedValue = "0";
				}
			}

		}
		public void LlenaSuc()
		{
			var lista = ctrlEmpresa.DdlSucursales();

			if(lista != null)
			{
				if(lista.Count > 0)
				{
					int largo = lista[0].Sucursal.Length;

					if (lista[0].Sucursal.Length >= 5)
					{
						largo = 5;
					}
					if (lista[0].Sucursal.Substring(0, largo) == "Error")
					{
						MsgBox(lista[0].Nombre, "Usuario");
						return;

					}

					DdlSucursal.DataSource = lista;
					DdlSucursal.DataTextField = "Sucursal";
					DdlSucursal.DataValueField = "IdSuc";
					DdlSucursal.DataBind();
				}
			}
		}
		private void LlenaConsulta()
		{
			var lista = ctrlUsuario.Usuario(IdUsu);
			if(lista != null)
			{
				if(lista.Usuario.Substring(0,5) == "Error")
				{
					MsgBox(lista.Nombre, "Usuario");
					return;
				}
				TxtUsuario.Text = lista.Usuario;
				TxtNombre.Text = lista.Nombre;
				TxtPwd1.Attributes["Value"] = lista.PWD;
				TxtPwd2.Attributes["Value"] = lista.PWD;
				TxtTel.Text = lista.Telefono;
				TxtEmail.Text = lista.Email;
				DdlDepto.SelectedValue = lista.IdDepto.ToString();
				DdlEstatus.SelectedValue = lista.Estatus;
				DdlPerfil.SelectedValue = lista.IdPerfil.ToString();
				LlenaUsuSuc(IdUsu);
				llenaArbolPerfil(lista.IdPerfil);

			}
		}
		private void LlenaUsuSuc(int id)
		{
			GvSucursal.DataSource = null;
			GvSucursal.DataBind();

			var lista = ctrlUsuario.ListaUsuSuc(id);
			if (lista != null)
			{
				if(lista.Count > 0)
				{
					if (lista[0].Sucursal == "Error")
					{
						MsgBox(lista[0].Nombre, "Usuario");
						return;
					}

					GvSucursal.DataSource = lista;
					GvSucursal.DataBind();

					for (int i = 0; i < lista.Count; i++)
					{
						ImageButton ibdel = GvSucursal.Rows[i].FindControl("BtnDel") as ImageButton;

						if (ibdel != null)
						{
							if (Accion == "O")
							{
								ibdel.Enabled = false;
							}
							else
							{
								if (vtUsuario.IdPerfil == 1)
								{
									ibdel.Enabled = true;
								}
								else
								{
									ibdel.Enabled = false;
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
			Session["AccUsu"] = "U";
			Session["IdUsu"] = IdUsu;
			Response.Redirect("~/Views/UsuariosDet.aspx");
		}

		protected void BtnGuardar_Command(object sender, CommandEventArgs e)
		{
			if(TxtUsuario.Text.Trim() == string.Empty)
			{
				LblAvisoUsu.Text = "Este Campo es Obligatorio";
				
				//LblAvisoUsu.Attributes["innerText"] = "Este Campo es Obligatorio";
				//ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				LblAvisoUsu.Visible = true;
				return;
			}
			if(TxtNombre.Text.Trim() == string.Empty)
			{
				//ExecJava(TxtNombre.ID, "Este Campo es Obligatorio");
				LblAvisoNom.Attributes["Style"] = "display: '';";
				//LblAvisoNom.Visible = true;
				return;
			}
			if(TxtPwd1.Text.Trim() == string.Empty)
			{
				//LblAvisoPwd1.Visible = true;
				LblAvisoPwd1.Attributes["Style"] = "display: '';";
				//ExecJava(TxtPwd1.ID, "Este Campo es Obligatorio");
				return;
			}
			if(TxtPwd1.Text != TxtPwd2.Text)
			{
				//LblAvisoPwd2.Visible = true;
				LblAvisoPwd2.Attributes["Style"] = "display: '';";
				//ExecJava(TxtPwd2.ID,"Los Passwords no Coinciden");
				return;
			}
			if(DdlDepto.SelectedValue.ToString() == "0")
			{
				LblAvisoDep.Visible = true;
				return;
			}
			if(GvSucursal.Rows.Count == 0 && Accion == "U")
			{
				LblAvisoSuc.Visible = true;
				TcPestana.ActiveTabIndex = 0;
				return;
			}
			if(DdlPerfil.SelectedIndex.ToString() == "0")
			{
				LblAvisoPer.Visible = true;
				TcPestana.ActiveTabIndex = 1;
				return;
			}
			mUsuario.IdUsu = IdUsu;
			mUsuario.Usuario = TxtUsuario.Text;
			mUsuario.Nombre = TxtNombre.Text;
			mUsuario.PWD = TxtPwd1.Text;
			mUsuario.IdPerfil = Convert.ToInt32(DdlPerfil.SelectedValue.ToString());
			mUsuario.IdDepto = Convert.ToInt32(DdlDepto.SelectedValue.ToString());
			mUsuario.Email = TxtEmail.Text;
			mUsuario.Telefono = TxtTel.Text;
			mUsuario.Estatus = DdlEstatus.SelectedValue.ToString();

			RespuestaSQL respuesta = null;
			if (Accion == "N")
			{
				respuesta = ctrlUsuario.UsuarioAdd(mUsuario);

			}
			if (Accion== "U")
			{
				respuesta = ctrlUsuario.UsuarioMod(mUsuario);

			}

			if (respuesta != null)
			{
				if (respuesta.Codigo != 0)
				{
					if(respuesta.Codigo == 14)
					{
						LblAvisoUsu.Text = "El usuario ya Existe";
						LblAvisoUsu.Visible = true;
						//LblAvisoUsu.Attributes["innerHTML"] = "El usuario ya Existe";
						LblAvisoUsu.Attributes["Style"] = "display: '';";
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
				Session["AccUsu"] = "O";
				Session["IdUSu"] = respuesta.ID;
				Response.Redirect("~/Views/UsuariosDet.aspx");

			}

		}

		protected void BtnCancelar_Command(object sender, CommandEventArgs e)
		{
			BtnCancelar.Enabled = false;
			if (Accion == "O")
			{
				Session["AccUsu"] = null;
				Session["IdUsu"] = null;
				Response.Redirect("~/Views/Usuarios.aspx");
			}
			if (Accion == "U")
			{
				Session["AccUsu"] = "O";
				Response.Redirect("~/Views/UsuariosDet.aspx");

			}
			if(Accion == "N")
			{
				Session["AccUsu"] = null;
				Session["IdUsu"] = null;
				Response.Redirect("~/Views/Usuarios.aspx");
			}

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
				//if(lista.Count > 0)
				//{
				//	TvPerfil.DataSource = lista;
				//	TvPerfil.DataBind();
				//}
				if(lista.Count > 0)
				{
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
							System.Web.UI.WebControls.TreeNode nodo = new System.Web.UI.WebControls.TreeNode(lista[i].IdNodo);

							nodo.Text = lista[i].Descripcion;
							nodo.ImageUrl = icono;
							nodo.Value = lista[i].IdNodo;
							//TvPerfil.Nodes.Add(new TreeNode(lista[i].Descripcion, lista[i].IdNodo, icono));

							TvPerfil.Nodes.Add(nodo);
							Nonodo1++;
							idnodo1 = lista[i].IdNodo;
							Nonodo2 = -1;
							TvPerfil.Nodes[Nonodo1].Collapse();

						}
						else
						{
							System.Web.UI.WebControls.TreeNode nodo = new System.Web.UI.WebControls.TreeNode(lista[i].IdNodo);

							nodo.Text = lista[i].Descripcion;
							nodo.ImageUrl = icono;
							nodo.Value = lista[i].IdNodo;

							if (idnodo1 == lista[i].IdNodoPadre)
							{
								TvPerfil.Nodes[Nonodo1].ChildNodes.Add(nodo);
								//idnodo2 = lista[i].IdNodo;
								Nonodo2++;
								//Nonodo2 = -1;
								//TvPerfil.Nodes[Nonodo1].ChildN

							}
							else
							{
								System.Web.UI.WebControls.TreeNode nodo2 = new System.Web.UI.WebControls.TreeNode(lista[i].IdNodo);

								nodo2.Text = lista[i].Descripcion;
								nodo2.ImageUrl = icono;
								nodo2.Value = lista[i].IdNodo;

								if (idnodo2 != lista[i].IdNodoPadre)
								{
									//Nonodo2++;
									if (Nonodo2 < 0)
									{
										Nonodo2 = 0;
									}

									idnodo2 = lista[i].IdNodoPadre;
									//Response.Write("Nonodo2:" + Nonodo2.ToString() + " - IdNodo2: " + idnodo2 + " - NodoPadre: " + lista[i].IdNodoPadre + " - nodo: "  + lista[i].IdNodo.Substring(6,2) + "<br/>");
									TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].ChildNodes.Add(nodo2);
									TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].Collapse();

								}
								else
								{
									//Response.Write("Nonodo2:" + Nonodo2.ToString() + " - IdNodo2: " + idnodo2 + " - NodoPadre: " + lista[i].IdNodoPadre + " - nodo: " + lista[i].IdNodo.Substring(6, 2) + "<br/>");
									TvPerfil.Nodes[Nonodo1].ChildNodes[Nonodo2].ChildNodes.Add(nodo2);

								}

							}


						}


					}
				}
			}

			//TvPerfil.Nodes[0].ChildNodes.Add(new TreeNode("Mango", "1", "~/Resources/Check1.png"));
			//TvPerfil.Nodes[0].ChildNodes.Add(new TreeNode("Apple", "2"));
			//TvPerfil.Nodes[0].ChildNodes.Add(new TreeNode("Pineapple", "3"));
			//TvPerfil.Nodes[0].ChildNodes.Add(new TreeNode("Orange", "4"));
			//TvPerfil.Nodes[0].ChildNodes.Add(new TreeNode("Grapes", "5"));

			//TvPerfil.Nodes.Add(new TreeNode("Vegetables", "10"));
			//TvPerfil.Nodes[1].ChildNodes.Add(new TreeNode("Carrot", "11"));
			//TvPerfil.Nodes[1].ChildNodes.Add(new TreeNode("Cauliflower", "12"));
			//TvPerfil.Nodes[1].ChildNodes.Add(new TreeNode("Potato", "13"));
			//TvPerfil.Nodes[1].ChildNodes.Add(new TreeNode("Tomato", "14"));
			//TvPerfil.Nodes[1].ChildNodes.Add(new TreeNode("Onion", "15"));
		}
		protected void DdlPerfil_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DdlPerfil.SelectedValue == "0")
			{
				LblAvisoPer.Visible = true;
				TvPerfil.Nodes.Clear();
				return;
			}
			else
			{
				LblAvisoPer.Visible = false;
			}

			llenaArbolPerfil(Convert.ToInt32(DdlPerfil.SelectedValue.ToString()));
		}

		protected void BtnSucursal_Command(object sender, CommandEventArgs e)
		{
			var lista = new UsuariosSuc();
			lista.IdUsuSuc = 0;
			lista.IdUsu = IdUsu;
			lista.IdSuc = Convert.ToInt32(DdlSucursal.SelectedValue.ToString());

			var respuesta = ctrlUsuario.UsuarioSucAdd(lista);
			if (respuesta != null)
			{
				if (respuesta.Codigo != 0)
				{
					MsgBox(respuesta.Mensaje, "Sucursal");
					return;
				}
			}
			LlenaUsuSuc(IdUsu);

		}

		protected void GvSucursal_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			// #### cuando borra las el registro
			GridViewRow fila = GvSucursal.Rows[e.RowIndex];

			int id = Convert.ToInt32((fila.FindControl("LblId") as Label).Text);
			var lista = new UsuariosSuc();
			lista.IdUsuSuc = id;
			
			if (id != 0)
			{
				var respuesta = ctrlUsuario.UsuarioSucDel(lista);
				if (respuesta != null)
				{
					if (respuesta.Codigo != 0)
					{
						MsgBox(respuesta.Mensaje, "Sucursal");
						return;
					}
				}
			}
			LlenaUsuSuc(IdUsu);
		}

		protected void GvSucursal_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			// ### ACCIONES DENTRO DEL GV ###########################################################################
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				foreach (ImageButton button in e.Row.Cells[1].Controls.OfType<ImageButton>())
				{
					if (button.CommandName == "Delete")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvSucursal, "Delete$" + e.Row.RowIndex);
					}
					if (button.CommandName == "Edit")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvSucursal, "Edit$" + e.Row.RowIndex);
					}
					if (button.CommandName == "Update")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvSucursal, "Update$" + e.Row.RowIndex);
					}
					if (button.CommandName == "Cancel")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvSucursal, "Cancel$" + e.Row.RowIndex);
					}
					if (button.CommandName == "Select")
					{
						button.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvSucursal, "Select$" + e.Row.RowIndex);
					}
				}

			}

		}

		protected void TxtUsuario_TextChanged(object sender, EventArgs e)
		{
			if(TxtUsuario.Text.Trim() == string.Empty)
			{
				//	ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				LblAvisoUsu.Text = "";
				LblAvisoUsu.Visible = true;
				LblAvisoUsu.Attributes["Style"] = "display: '';";
			}
			else
			{
				//ExecJava(TxtUsuario.ID, "Este Campo es Obligatorio");
				LblAvisoUsu.Text = "";
				LblAvisoUsu.Visible = true;
				LblAvisoUsu.Attributes["Style"] = "display: 'none';";
			}
		}

		protected void TxtNombre_TextChanged(object sender, EventArgs e)
		{
			//if (TxtNombre.Text.Trim() == string.Empty)
			//{
			//	ExecJava(TxtNombre.ID, "Este Campo es Obligatorio");
			//	//LblAvisoNom.Visible = true;
			//}
			//else
			//{
			//	ExecJava(TxtNombre.ID, "Este Campo es Obligatorio");
			//	//LblAvisoNom.Visible = false;
			//}

		}

		protected void TxtPwd1_TextChanged(object sender, EventArgs e)
		{
			TxtPwd1.Attributes["Value"] = TxtPwd1.Text;
			//if (TxtPwd1.Text.Trim() == string.Empty)
			//{
			//	ExecJava(TxtPwd1.ID, "Este Campo es Obligatorio");
			//	//LblAvisoPwd1.Visible = true;
			//}
			//else
			//{
			//	ExecJava(TxtPwd1.ID, "Este Campo es Obligatorio");
			//	//LblAvisoPwd1.Visible = false;
			//}

		}

		protected void TxtPwd2_TextChanged(object sender, EventArgs e)
		{
			TxtPwd2.Attributes["Value"] = TxtPwd2.Text;
			//if (TxtPwd2.Text.Trim() != TxtPwd1.Text.Trim())
			//{
			//	ExecJava(TxtPwd2.ID, "Los Passwords no Coinciden");
			//	//LblAvisoPwd2.Visible = true;
			//}
			//else
			//{
			//	ExecJava(TxtPwd2.ID, "Los Passwords no Coinciden");
			//	//LblAvisoPwd2.Visible = false;
			//}
		}

		protected void DdlSucursal_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DdlSucursal.SelectedValue == "0")
			{
				LblAvisoSuc.Visible = true;
			}
			else
			{
				LblAvisoSuc.Visible = false;
			}
		}

		protected void DdlDepto_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DdlDepto.SelectedValue == "0")
			{
				LblAvisoDep.Visible = true;
			}
			else
			{
				LblAvisoDep.Visible = false;
			}
		}
	}
}