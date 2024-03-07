<%@ Page Title="Configuración del Sistema" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/Configuracion.aspx.cs" Inherits="GuerreroWeb.Views.Configuracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<table width="100%">
		<tr>
			<td colspan="6" style="background-color: black;padding: 10px 10px 10px 10px">
				<asp:Label ID="LblTitulo" runat="server" Text="Configuración del Sistema" ForeColor="White" Font-Bold="True"></asp:Label>
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
				<asp:ImageButton ID="BtnCancelar" runat="server" ImageUrl="~/Resources/Cancelar.png" CssClass="btn btnblock bg-light" Height="40px" ToolTip="Deshacer" OnCommand="BtnCancelar_Command" />
			</td>
			<td class="campo_cen_izq" style="padding: 10px 10px 10px 10px">
				<br />
			</td>
			<td class="campo_cen_izq" style="padding: 10px 10px 10px 10px">
				<br />
			</td>
			<td class="campo_cen_izq" style="padding: 10px 10px 10px 10px">
				<br />
			</td>
		</tr>
	</table>
	<div class="conteiner" style="background-image: url(../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;" width="100%">
		<table width="100%">
			<tr>
				<td class="campo_cen_der">
					<asp:Label ID="LblSuc" runat="server" Text="Activa CXC:" Font-Bold="True"></asp:Label>
				</td>
				<td class="campo_cen_izq">
					<asp:CheckBox ID="ChkCXC" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
				<td class="campo_cen_der">
					<asp:Label ID="Label1" runat="server" Text="Activa Ventas:" Font-Bold="True"></asp:Label>
				</td>
				<td class="campo_cen_izq" >
					<asp:CheckBox ID="ChkVentas" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
				<td class="campo_cen_der">
					<asp:Label ID="Label2" runat="server" Text="Activa Inventarios:" Font-Bold="True"></asp:Label>
				</td>
				<td class="campo_cen_izq">
					<asp:CheckBox ID="ChkInv" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>

			</tr>
			<tr>
				<td class="campo_cen_der">
					<asp:Label ID="Label7" runat="server" Text="Activa Compras:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:CheckBox ID="ChkCompras" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
				<td class="campo_cen_der">
					<asp:Label ID="Label8" runat="server" Text="Activa CXP:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:CheckBox ID="ChkCXP" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
				<td class="campo_cen_der">
					<asp:Label ID="Label9" runat="server" Text="Activa Contabilidad:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:CheckBox ID="ChkConta" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
			</tr>
			<tr>
				<td class="campo_cen_der">
					<asp:Label ID="Label3" runat="server" Text="Activa Wati:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:CheckBox ID="ChkWati" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
				<td class="campo_cen_der">
					<asp:Label ID="Label6" runat="server" Text="Activa Reportes:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:CheckBox ID="ChkReporte" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
				<td>
					<br />
				</td>
				<td>
					<br />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label10" runat="server" Text="No. Decimal Vista:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:DropDownList ID="DdlDecimal1" runat="server" ViewStateMode="Enabled">
						<asp:ListItem Text="0" Value="0"></asp:ListItem>
						<asp:ListItem Text="1" Value="1"></asp:ListItem>
						<asp:ListItem Text="2" Value="2"></asp:ListItem>
						<asp:ListItem Text="3" Value="3"></asp:ListItem>
						<asp:ListItem Text="4" Value="4"></asp:ListItem>
					</asp:DropDownList>
				</td>
				<td>
					<asp:Label ID="Label11" runat="server" Text="No. Decimal Operacion:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:DropDownList ID="DdlDecimal2" runat="server" ViewStateMode="Enabled">
						<asp:ListItem Text="0" Value="0"></asp:ListItem>
						<asp:ListItem Text="1" Value="1"></asp:ListItem>
						<asp:ListItem Text="2" Value="2"></asp:ListItem>
						<asp:ListItem Text="3" Value="3"></asp:ListItem>
						<asp:ListItem Text="4" Value="4"></asp:ListItem>
					</asp:DropDownList>
				</td>
				<td>
					<br />
				</td>
				<td>
					<br />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label4" runat="server" Text="Ventas Sin Existencia:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:CheckBox ID="ChkVenta0" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
				<td>
					<asp:Label ID="Label5" runat="server" Text="Salida Sin Existencia:" Font-Bold="True"></asp:Label>
				</td>
				<td>
					<asp:CheckBox ID="ChkSalida0" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
				<td>
					<br />
				</td>
				<td>
					<br />
				</td>
			</tr>

		</table>
	</div>

</asp:Content>
