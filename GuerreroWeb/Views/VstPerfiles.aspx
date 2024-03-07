<%@ Page Title="Catalogos de Perfiles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/VstPerfiles.aspx.cs" Inherits="GuerreroWeb.Views.VstPerfiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div style="width: 100%; height: 75vh;" class="mh-100">
		<table width="100%">
			<tr>
				<td colspan="4" style="background-color: black; padding: 10px 10px 10px 10px;">
					<asp:Label ID="LblTitulo" runat="server" Text="Catálogo de Perfiles" ForeColor="White" Font-Bold="True"></asp:Label>
				</td>
			</tr>
			<tr style="background-color: orange;">
				<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px;">
					<asp:ImageButton ID="BtnNuevo" runat="server" ImageUrl="~/Resources/nuevo.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="True" ViewStateMode="Enabled" OnCommand="BtnNuevo_Command" />
				</td>
				<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px;">
					<br />
				</td>
				<td class="campo_cen_cen" width="40px" style="padding: 10px 10px 10px 10px;">
					<br />
				</td>
				<td class="campo_cen_izq" style="padding: 10px 10px 10px 10px;">
					<asp:ImageButton ID="BtnBuscar" runat="server" ImageUrl="~/Resources/Buscar.png" CssClass="btn btnblock bg-light" Height="40px" OnCommand="BtnBuscar_Command" ViewStateMode="Enabled" />
				</td>
			</tr>
			<tr id="DivBuscar" runat="server" style="background-color: orange;">
				<td colspan="4">
					<asp:Panel ID="PnlConsulta" runat="server" DefaultButton="BtnConslta" CssClass="campo_cen_izq org-row">
						<table>
							<tr>
								<td class="w-25 campo_cen_der">
									<asp:TextBox ID="TxtBuscar" runat="server" onfocus="this.select()" MaxLength="50" CssClass="coldiv1"></asp:TextBox>
								</td>
								<td class="campo_cen_cen">
									<asp:ImageButton ID="BtnConslta" runat="server" ImageUrl="~/Resources/IcoBuscar.png" Height="30px" Width="30px" OnCommand="BtnConslta_Command" />
								</td>
								<td class="w-25 campo_cen_cen">
									<br />
								</td>
								<td class="campo_cen_der">
									<br />
								</td>
								<td class="w-25 campo_cen_cen">
									<br />
								</td>
								<td class="campo_cen_der">
									<br />
								</td>
								<td class="w-25 campo_cen_cen">
									<br />
								</td>
								<td class="w-25">
									<br />
								</td>
							</tr>
						</table>

					</asp:Panel>
				</td>
			</tr>
		</table>
		<div style="overflow-x: scroll; overflow-y: hidden; width: 100%; height: 100%;">
			<div class="campo_top_izq" style="width: 50%; height: 490px">

				<asp:GridView ID="GvConsulta" runat="server" Width="100%" Height="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowCustomPaging="False" ShowFooter="False" AllowPaging="True" ShowHeaderWhenEmpty="True" ViewStateMode="Enabled" OnRowDataBound="GvConsulta_RowDataBound" OnRowDeleting="GvConsulta_RowDeleting" OnRowEditing="GvConsulta_RowEditing" OnPageIndexChanging="GvConsulta_PageIndexChanging">
					<AlternatingRowStyle BackColor="#CCCCCC" />
					<Columns>
						<asp:TemplateField HeaderText="">
							<ItemTemplate>
								<asp:ImageButton ID="BtnEditar" runat="server" ImageUrl="~/Resources/IcoEditar.png" Height="40" Width="40" CommandName="Edit" ToolTip="Modificar" />
								<asp:ImageButton ID="BtnDel" runat="server" ImageUrl="~/Resources/IcoEliminar.png" Height="40" Width="40" CommandName="Delete" ToolTip="Eliminar" OnClientClick="return confirm('¿Está Seguro de Eliminar el Registro?');" />
							</ItemTemplate>
							<HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" />
							<ItemStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Id" Visible="False">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblId" Text='<%# Bind("IdPerfil") %>'></asp:Label>
							</ItemTemplate>
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="40px" />
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Perfil">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblPerfil" Text='<%# Bind("Perfil") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="40px" />
							<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" Height="30px" />
						</asp:TemplateField>
					</Columns>
					<FooterStyle BackColor="Orange" Font-Bold="True" ForeColor="Black" />
					<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Font-Size="Larger" HorizontalAlign="Center" VerticalAlign="Middle" />
					<PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
					<PagerStyle BackColor="Black" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" Font-Size="X-Large" VerticalAlign="Middle" />
					<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
					<SortedAscendingCellStyle BackColor="#F1F1F1" />
					<SortedAscendingHeaderStyle BackColor="#808080" />
					<SortedDescendingCellStyle BackColor="#CAC9C9" />
					<SortedDescendingHeaderStyle BackColor="#383838" />
				</asp:GridView>
			</div>
		</div>
	</div>

</asp:Content>
