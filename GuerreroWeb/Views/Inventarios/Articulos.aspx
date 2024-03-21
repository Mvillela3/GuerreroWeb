<%@ Page Title="Catálogo de Árticulos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/Inventarios/Articulos.aspx.cs" Inherits="GuerreroWeb.Views.Inventarios.Articulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<div style="width: 100%; height: 75vh;" class="mh-100">
		<table width="100%">
			<tr>
				<td colspan="4" style="background-color: black; padding: 10px 10px 10px 10px;">
					<asp:Label ID="LblTitulo" runat="server" Text="Catálogo de Árticulos" ForeColor="White" Font-Bold="True"></asp:Label>
				</td>
			</tr>
			<tr style="background-color: orange;">
				<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px;">
					<asp:ImageButton ID="BtnNuevo" runat="server" ImageUrl="~/Resources/nuevo.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="True" ViewStateMode="Enabled" OnCommand="BtnNuevo_Command" />
					<asp:Button ID="BtnVtMdl" runat="server" Text="MostrarModal" Style="display: none;" ViewStateMode="Enabled" />
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
								<td class="campo_cen_der">
									<asp:Label ID="LblBusca1" runat="server" Text="Linea:" Font-Bold="True"></asp:Label>
								</td>
								<td class="w-25 campo_cen_cen">
									<asp:DropDownList ID="DdlLineaB" runat="server" AutoPostBack="True" Width="100%" OnSelectedIndexChanged="DdlLineasB_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
								</td>
								<td class="campo_cen_der">
									<asp:Label ID="LblBusca2" runat="server" Text="Categoría:" Font-Bold="True"></asp:Label>
								</td>
								<td class="w-25 campo_cen_cen">
									<asp:DropDownList ID="DdlCategoriaB" runat="server" AutoPostBack="True" Width="100%" OnSelectedIndexChanged="DdlCategoriaB_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
								</td>
								<td class="campo_cen_der">
									<asp:Label ID="LblBusca3" runat="server" Text="Familia:" Font-Bold="True"></asp:Label>
								</td>
								<td class="w-25 campo_cen_cen">
									<asp:DropDownList ID="DdlFamiliaB" runat="server" AutoPostBack="True" Width="100%" OnSelectedIndexChanged="DdlFamiliaB_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
								</td>
								<td class="w-25">
									<asp:ImageButton ID="BtnConslta" runat="server" ImageUrl="~/Resources/IcoBuscar.png" Height="30px" Width="30px" OnCommand="BtnConslta_Command" />
								</td>
							</tr>
						</table>

					</asp:Panel>
				</td>
			</tr>
		</table>
		<div class="coldiv1" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%;" >

		<div style="overflow-x: scroll; overflow-y: scroll; width: 100%; height: 100%;">
			<div class="campo_top_izq" style="width: 100%; height: 490px">

				<asp:GridView ID="GvConsulta" runat="server" Width="100%" Height="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowPaging="True" ShowHeaderWhenEmpty="True" ViewStateMode="Enabled" OnRowDataBound="GvConsulta_RowDataBound" OnRowDeleting="GvConsulta_RowDeleting" OnRowEditing="GvConsulta_RowEditing" OnPageIndexChanging="GvConsulta_PageIndexChanging">
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
								<asp:Label runat="server" ID="LblId" Text='<%# Bind("IdArt") %>'></asp:Label>
							</ItemTemplate>
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="40px" />
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Codigo">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblCodigo" Text='<%# Bind("Codigo") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Estatus">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblEstatus" Text='<%# Bind("Estatus") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Descripcion">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblDescripcion" Text='<%# Bind("Descripcion") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Linea">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblRangoFin" Text='<%# Bind("Linea") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Cátegoria">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblCategoria" Text='<%# Bind("Categoria") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Familia">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblFamilia" Text='<%# Bind("Familia") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Marca">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblMarca" Text='<%# Bind("Marca") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Modelo">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblModelo" Text='<%# Bind("Modelo") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Costo Promedio">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblCostoP" Text='<%# Bind("CostoProm") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="20%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Precio">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblPrecio" Text='<%# Bind("Precio") %>'></asp:Label>
							</ItemTemplate>
							<FooterStyle Width="5%" />
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
							<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="20%" Height="30px" />
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
	</div>


</asp:Content>
