<%@ Page Title="Detalles de Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/PerfilesDet.aspx.cs" Inherits="GuerreroWeb.Views.PerfilesDet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script language="javascript">
		function avisos1(objeto) {
			var largo;
			var objeto2;

			//alert(objeto);
			if (objeto == "MainContent_TxtPerfil") {
				largo = document.getElementById(objeto).value;

				document.getElementById("MainContent_LblAvisoPer").innerHTML = "Este Campo es Obligatorio1";
				if (largo.length == 0) {
					document.getElementById("MainContent_LblAvisoPer").style.display = "";
					//alert("si");
				}
				else {
					document.getElementById("MainContent_LblAvisoPer").style.display = "none";
					//alert("no");
				}
			}

		}
	</script>
	<table width="100%">
		<tr>
			<td colspan="6" style="background-color: black; padding: 10px 10px 10px 10px">
				<asp:Label ID="LblTitulo" runat="server" Text="Detalle del Perfil" Font-Bold="True" ForeColor="White"></asp:Label>
			</td>
		</tr>
		<tr style="background-color: orange;">
			<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
				<asp:ImageButton ID="BtnEditar" runat="server" ImageUrl="~/Resources/Editar2.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="True" ToolTip="Editar" OnCommand="BtnEditar_Command" />
			</td>
			<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
				<asp:ImageButton ID="BtnGuardar" runat="server" ImageUrl="~/Resources/Guardar.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="False" ToolTip="Guardar" OnCommand="BtnGuardar_Command" OnClientClick="return confirm('¿Están Correctos los Datos?');" />

			</td>
			<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
				<asp:ImageButton ID="BtnCancelar" runat="server" ImageUrl="~/Resources/Cancelar.png" CssClass="btn btnblock opcion bg-light" Height="40px" ToolTip="Deshacer" OnCommand="BtnCancelar_Command" />
			</td>
			<td class="campo_cen_izq">
				<br />
			</td>
			<td class="campo_cen_izq">
				<br />
			</td>
			<td class="campo_cen_izq">
				<br />
			</td>
		</tr>
	</table>
	<div class="conteiner" style="background-image: url(../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;" width="100%">
		<table width="100%">
			<tr>
				<td class="coldiv5 campo_cen_izq" style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label0" runat="server" Text="Perfil:" Font-Bold="True"></asp:Label>
				</td>
				<td colspan="2" class="campo_cen_izq" style="padding: 5px 0 5px 0;">
					<div class="coldiv1">
						<asp:TextBox ID="TxtPerfil" runat="server" MaxLength="50" onblur="avisos1(this.id);" CssClass="coldiv1" OnTextChanged="TxtPerfil_TextChanged" ViewStateMode="Enabled" width="100%"></asp:TextBox>
					</div>
					<div class="coldiv1">
						<asp:Label ID="LblAvisoPer" runat="server" onclick="avisos1(this.id);" Text="" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
					</div>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<br />
				</td>
				<td class="campo_cen_izq" colspan="2" style="padding: 5px 0 5px 0;">
				</td>
			</tr>
			<tr>
				<td style="padding: 5px 0 5px 0;" class="campo_top_izq">
					<asp:Label ID="Label7" runat="server" Text="Permisos:" Font-Bold="True"></asp:Label>
				</td>
				<td colspan="5" style="padding: 5px 0 5px 0;" class="org_col campo_top_izq">
					<div style="overflow-x: hidden; overflow-y: scroll; width: 100%; height: 100%;">
							<div class="campo_top_izq" style="width: 100%; height: 490px">
								<asp:TreeView ID="TvPerfil" runat="server" CssClass="TvPerfil" Font-Bold="True" Font-Size="Medium" ShowLines="True" width="100%" BackColor="White">
								<HoverNodeStyle Font-Bold="True" Height="20px" Width="20px" />
								<LeafNodeStyle Height="20px" Width="20px" />
								<RootNodeStyle Font-Bold="True" Height="20px" Width="20px" />
							</asp:TreeView>
						</div>
					</div>
				</td>
			</tr>
		</table>

	</div>

</asp:Content>
