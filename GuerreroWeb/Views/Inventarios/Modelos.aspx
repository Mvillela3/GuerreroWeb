<%@ Page Title="Catálogo de Modelos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/Inventarios/Modelos.aspx.cs" Inherits="GuerreroWeb.Views.Inventarios.Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script language="javascript">
	function avisos1(objeto) {
			var largo;
			var objeto2;

			//alert(objeto);
			if (objeto == "MainContent_TxtModelo") {
				largo = document.getElementById(objeto).value;

				document.getElementById("MainContent_LblMensaje1").innerHTML = "Este Campo es Obligatorio";
				if (largo.length == 0) {
					document.getElementById("MainContent_LblMensaje1").style.display = "";
					//alert("si");
				}
				else {
					document.getElementById("MainContent_LblAvisoPer").style.display = "none";
					//alert("no");
				}
			}

		}
	</script>

	<div style="width: 100%; height: 75vh;" class="mh-100">
		<table width="100%">
			<tr>
				<td colspan="4" style="background-color: black;padding: 10px 10px 10px 10px">
					<asp:Label ID="LblTitulo" runat="server" Text="Catálogo de Marcas" ForeColor="White" Font-Bold="True"></asp:Label>
				</td>
			</tr>
			<tr style="background-color: orange;">
				<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
					<asp:ImageButton ID="BtnNuevo" runat="server" ImageUrl="~/Resources/nuevo.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="True" ViewStateMode="Enabled" OnCommand="BtnNuevo_Command" />
					<asp:Button ID="BtnVtMdl" runat="server" Text="MostrarModal" Style="display: none;" ViewStateMode="Enabled" />
				</td>
				<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
					<br />
				</td>
				<td class="campo_cen_cen" width="40px" style="padding: 10px 10px 10px 10px">
					<br />
				</td>
				<td class="campo_cen_izq" style="padding: 10px 10px 10px 10px">
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
									<asp:Label ID="LblBusca2" runat="server" Text="Marca:" Font-Bold="True"></asp:Label>
								</td>
								<td class="w-25 campo_cen_cen">
									<asp:DropDownList ID="DdlMarcaB" runat="server" AutoPostBack="True" Width="100%" OnSelectedIndexChanged="DdlMarcaB_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
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
		<div style="overflow-x: scroll; overflow-y: scroll; width: 50%; height: 100%;">
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
								<asp:Label runat="server" ID="LblId" Text='<%# Bind("IdMod") %>'></asp:Label>
							</ItemTemplate>
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="40px" />
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="30px" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Modelo">
							<ItemTemplate>
								<asp:Label runat="server" ID="LblModelo" Text='<%# Bind("Modelo") %>'></asp:Label>
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

	<asp:Panel ID="VtMdl" runat="server" CssClass="campo_cen_cen" Style="width: 700px; height: 500px" ViewStateMode="Enabled" Visible="False">
		<div class="campo_cen_cen alin-col FondoMsg VtnMsg" style="width: 700px; height: 500px">
			<table width="100%">
				<tr>
					<td colspan="4">
						<asp:Label ID="Label1" runat="server" Text="Detalle del Modelo" Font-Bold="True" Font-Size="Larger"></asp:Label>
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<br />
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<br />
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<br />
					</td>
				</tr>
				<tr>
					<td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
						<asp:Label ID="Label2" runat="server" Text="Modelo:" BorderStyle="None" Font-Size="Medium" Style="text-align: justify; padding: 0  10px  0  10px" ViewStateMode="Enabled" Font-Bold="True"></asp:Label>
						<asp:Label ID="LblMensaje1" runat="server" Text="" onclick="avisos1(this.id);" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
					</td>
					<td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
						<asp:TextBox ID="TxtModelo" runat="server" Width="100%" MaxLength="60" ViewStateMode="Enabled" onblur="avisos1(this.id);" CssClass="coldiv1" onfocus="this.select()" OnTextChanged="TxtModelo_TextChanged"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
						<asp:Label ID="Label3" runat="server" Text="Línea:" BorderStyle="None" Font-Size="Medium" Style="text-align: justify; padding: 0  10px  0  10px" ViewStateMode="Enabled" Font-Bold="True"></asp:Label>
					</td>
					<td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
						<asp:DropDownList ID="DdlLinea" runat="server" ViewStateMode="Enabled" AutoPostBack="True" OnSelectedIndexChanged="DdlLinea_SelectedIndexChanged"></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
						<asp:Label ID="Label4" runat="server" BorderStyle="None" Font-Bold="True" Font-Size="Medium" Style="text-align: justify; padding: 0  10px  0  10px" Text="Marca:" ViewStateMode="Enabled"></asp:Label>
					</td>
					<td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
						<asp:DropDownList ID="DdlMarca" runat="server" ViewStateMode="Enabled">
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<br />
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<br />
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<br />
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<br />
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<br />
					</td>
				</tr>
				<tr>
					<td class="campo_cen_cen w-25">
						<br />
					</td>
					<td class="campo_cen_cen w-25" style="padding: 10px 0  10px  0">
						<asp:Button ID="BtnGuardar" runat="server" CssClass="btn" Text="Guardar" Width="103" Height="31" BackColor="Lime" ViewStateMode="Enabled" Font-Bold="True" OnClientClick="return confirm('¿Están Correctos los Datos?');" OnClick="BtnGuardar_Click" />
					</td>
					<td class="campo_cen_cen w-25" style="padding: 10px 0  10px  0">
						<asp:Button ID="BtnCancelar" runat="server" CssClass="btn" Text="Cancelar" Width="103" Height="31" BackColor="#FF5050" ViewStateMode="Enabled" Font-Bold="True" OnClientClick="return confirm('¿Está seguro de no Guardar los Datos?');" OnClick="BtnCancelar_Click" />
					</td>
					<td class="campo_cen_cen w-25">
						<br />
					</td>
				</tr>
			</table>
			<asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" Style="display: none;" OnCommand="BtnAceptar_Command" />
		</div>
	</asp:Panel>



	<ajaxToolkit:ModalPopupExtender ID="MpeVtMdl" runat="server" BehaviorID="MpeVtMdl" DynamicServicePath="" OkControlID="BtnAceptar" TargetControlID="BtnVtMdl" CancelControlID="BtnAceptar" PopupControlID="VtMdl" BackgroundCssClass="FndContMdl">
	</ajaxToolkit:ModalPopupExtender>

	<asp:HiddenField ID="HfId" runat="server" ViewStateMode="Enabled" />

</asp:Content>
