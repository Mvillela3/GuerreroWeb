<%@ Page Title="Detalle de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/UsuariosDet.aspx.cs" Inherits="GuerreroWeb.Views.UsuariosDet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script language="javascript">
		function avisos1(objeto) {
			var largo;
			var objeto2;

			//alert(objeto);
			if (objeto == "MainContent_TxtUsuario") {
				largo = document.getElementById(objeto).value;

				document.getElementById("MainContent_LblAvisoUsu").innerHTML = "Este Campo es Obligatorio1";
				if (largo.length == 0) {
					document.getElementById("MainContent_LblAvisoUsu").style.display = "";
					//alert("si");
				}
				else {
					document.getElementById("MainContent_LblAvisoUsu").style.display = "none";
					//alert("no");
				}
			}
			if (objeto == "MainContent_TxtNombre") {
				largo = document.getElementById(objeto).value;

				if (largo.length == 0) {
					document.getElementById("MainContent_LblAvisoNom").style.display = "";
				}
				else {
					document.getElementById("MainContent_LblAvisoNom").style.display = "none";
				}
			}
			if (objeto == "MainContent_TxtPwd1") {
				largo = document.getElementById(objeto).value;

				if (largo.length == 0) {
					document.getElementById("MainContent_LblAvisoPwd1").style.display = "";
				}
				else {
					document.getElementById("MainContent_LblAvisoPwd1").style.display = "none";
				}
			}
			if (objeto == "MainContent_TxtPwd2") {
				largo = document.getElementById(objeto).value;
				objeto2 = "MainContent_TxtPwd1";

				if (document.getElementById(objeto).value != document.getElementById(objeto2).value || largo == 0) {
					document.getElementById("MainContent_LblAvisoPwd2").style.display = "";
				}
				else {
					document.getElementById("MainContent_LblAvisoPwd2").style.display = "none";
				}
			}

		}
		function aviso3(objeto, mensaje) {
			alert(objeto);
			alert(mensaje);
			//document.getElementById(objeto).innerHTML = mensaje;
			//document.getElementById(objeto).style.display = "";
		}
	</script>
	<table width="100%">
		<tr>
			<td colspan="6" style="background-color: black;padding: 10px 10px 10px 10px;">
				<asp:Label ID="LblTitulo" runat="server" Text="Detalle del Usuario" Font-Bold="True" ForeColor="White"></asp:Label>
			</td>
		</tr>
		<tr style="background-color: orange;">
			<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px;">
				<asp:ImageButton ID="BtnEditar" runat="server" ImageUrl="~/Resources/Editar2.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="True" ToolTip="Editar" OnCommand="BtnEditar_Command" />
			</td>
			<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px;">
				<asp:ImageButton ID="BtnGuardar" runat="server" ImageUrl="~/Resources/Guardar.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="False" ToolTip="Guardar" OnCommand="BtnGuardar_Command" OnClientClick="return confirm('¿Están Correctos los Datos?');" />

			</td>
			<td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px;">
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
				<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label0" runat="server" Text="Usuario:" Font-Bold="True"></asp:Label>
				</td>
				<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
					<div class="coldiv1">
						<asp:TextBox ID="TxtUsuario" runat="server" MaxLength="20" onblur="avisos1(this.id);" CssClass="coldiv1" onfocus="this.select()" OnTextChanged="TxtUsuario_TextChanged" ViewStateMode="Enabled"></asp:TextBox>
					</div>
					<div class="coldiv1">
						<asp:Label ID="LblAvisoUsu" runat="server" onclick="avisos1(this.id);" Text="" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
					</div>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label1" runat="server" Text="Nombre:" Font-Bold="True"></asp:Label>
				</td>
				<td class="campo_cen_izq" colspan="2" style="padding: 5px 0 5px 0;">
					<div class="coldiv1">
						<asp:TextBox ID="TxtNombre" runat="server" MaxLength="100" onfocus="this.select()" onblur="avisos1(this.id);" CssClass="coldiv1" OnTextChanged="TxtNombre_TextChanged" ViewStateMode="Enabled"></asp:TextBox>
					</div>
					<div class="coldiv1">
						<asp:Label ID="LblAvisoNom" runat="server" Text="Este Campo es Obligatorio" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
					</div>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<br />
				</td>
			</tr>
			<tr>
				<td style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label7" runat="server" Text="Password:" Font-Bold="True"></asp:Label>
				</td>
				<td style="padding: 5px 0 5px 0;" class="org_col">
					<div class="coldiv1">
						<asp:TextBox ID="TxtPwd1" runat="server" TextMode="Password" MaxLength="20" onblur="avisos1(this.id);" onfocus="this.select()" OnTextChanged="TxtPwd1_TextChanged" ViewStateMode="Enabled"></asp:TextBox>
					</div>
					<div class="coldiv1">
						<asp:Label ID="LblAvisoPwd1" runat="server" Text="Este Campo es Obligatorio" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
					</div>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label8" runat="server" Text="Confirma Password:" Font-Bold="True"></asp:Label>
				</td>
				<td style="padding: 5px 0 5px 0;" class="org_col">
					<div class="coldiv1">
						<asp:TextBox ID="TxtPwd2" runat="server" TextMode="Password" MaxLength="20" onblur="avisos1(this.id);" onfocus="this.select()" OnTextChanged="TxtPwd2_TextChanged" ViewStateMode="Enabled"></asp:TextBox>
					</div>
					<div class="coldiv1">
						<asp:Label ID="LblAvisoPwd2" runat="server" Text="Los Password no Coinciden" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
					</div>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label9" runat="server" Text="Departamento:" Font-Bold="True"></asp:Label>
				</td>
				<td style="padding: 5px 0 5px 0;" class="org_col">
					<asp:DropDownList ID="DdlDepto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlDepto_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
					<asp:Label ID="LblAvisoDep" runat="server" Text="Seleccione un Departamento" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label10" runat="server" Text="Telefono:" Font-Bold="True"></asp:Label>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<asp:TextBox ID="TxtTel" runat="server" onfocus="this.select()" MaxLength="20" TextMode="Phone" ViewStateMode="Enabled"></asp:TextBox>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label11" runat="server" Text="Email:" Font-Bold="True"></asp:Label>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<asp:TextBox ID="TxtEmail" runat="server" onfocus="this.select()" MaxLength="100" AutoPostBack="True" TextMode="Email" ViewStateMode="Enabled"></asp:TextBox>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<asp:Label ID="Label2" runat="server" Text="Estatus:" Font-Bold="True"></asp:Label>
				</td>
				<td style="padding: 5px 0 5px 0;">
					<asp:DropDownList ID="DdlEstatus" runat="server" AutoPostBack="True" ViewStateMode="Enabled">
						<asp:ListItem Text="ACTIVO" Value="ACTIVO"></asp:ListItem>
						<asp:ListItem Text="BAJA" Value="BAJA"></asp:ListItem>
						<asp:ListItem Text="BLOQUEADO" Value="BLOQUEADO"></asp:ListItem>
					</asp:DropDownList>
				</td>
			</tr>
		</table>
<%--		<div class="coldiv1 campo_cen_izq org_row">
			<div runat="server" id="DivPEstaña1" class="coldiv6 campo_cen_cen">
				<asp:Button ID="BtnPestana1" runat="server" Text="Sucursales" Font-Bold="True" Font-Size="Medium" CssClass="Activo" BorderStyle="None" EnableTheming="False" />
			</div>
			<div runat="Server" id="DivPestana2" class="coldiv6 campo_cen_cen">
				<asp:Button ID="BtnPestana2" runat="server" Text="Perfil" Font-Bold="True" Font-Size="Medium" CssClass="inActivo" BorderStyle="None" EnableTheming="False" />
			</div>

		</div>--%>
		<div class="coldiv1">
			<ajaxToolkit:TabContainer ID="TcPestana" runat="server" ActiveTabIndex="0" Height="400px" Width="100%" BackColor="#FF6600" ScrollBars="Auto" ViewStateMode="Enabled" VerticalStripWidth="800px" CssClass="Tabjax">
				<ajaxToolkit:TabPanel ID="Pestana1" runat="server" Height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div1" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana1" runat="server" Text="Sucursales" Font-Bold="True" Font-Size="Small" CssClass="Activo"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate >
						<div class="coldiv1 org_col" >
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label3" runat="server" Text="Sucursal" Font-Bold="True" Font-Size="Medium"></asp:Label>
								</div>
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:DropDownList ID="DdlSucursal" runat="server" OnSelectedIndexChanged="DdlSucursal_SelectedIndexChanged" AutoPostBack="True" ViewStateMode="Enabled"></asp:DropDownList>
									<asp:Label ID="LblAvisoSuc" runat="server" Text="Agrege al Menos Una Sucursal" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Visible="False"></asp:Label>

								</div>
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:ImageButton ID="BtnSucursal" runat="server" ImageAlign="Middle" Height="30px" Width="30px" ImageUrl="~/Resources/IcoNuevo.png" OnCommand="BtnSucursal_Command" />
								</div>
							</div>
							<div class="coldiv1">
								<asp:GridView ID="GvSucursal" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" width="400px" OnRowDataBound="GvSucursal_RowDataBound" OnRowDeleting="GvSucursal_RowDeleting">
									<Columns>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:ImageButton ID="BtnDel" runat="server" ImageUrl="~/Resources/IcoEliminar.png" Height="40" Width="40" CommandName="Delete" ToolTip="Eliminar" OnClientClick="return confirm('¿Está Seguro de Eliminar el Registro?');" />
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="20%" VerticalAlign="Middle" />
											<ItemStyle HorizontalAlign="Center" Width="20%" VerticalAlign="Middle" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Id" Visible="False">
											<ItemTemplate>
												<asp:Label runat="server" ID="LblId" Text='<%# Bind("IdUsuSuc") %>'></asp:Label>
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="40px" />
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="30px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Sucursal">
											<ItemTemplate>
												<asp:Label runat="server" ID="LblSucursal" Text='<%# Bind("Sucursal") %>'></asp:Label>
											</ItemTemplate>
											<FooterStyle Width="5%" />
											<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" Height="40px" />
											<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30%" Height="30px" />
										</asp:TemplateField>

									</Columns>
									<AlternatingRowStyle BackColor="#CCCCCC" />
									<FooterStyle BackColor="#CCCCCC" />
									<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
									<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
									<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
									<SortedAscendingCellStyle BackColor="#F1F1F1" />
									<SortedAscendingHeaderStyle BackColor="Gray" />
									<SortedDescendingCellStyle BackColor="#CAC9C9" />
									<SortedDescendingHeaderStyle BackColor="#383838" />

								</asp:GridView>
							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>
				<ajaxToolkit:TabPanel ID="Pestana2" runat="server" height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div2" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana2" runat="server" Text="Perfil" Font-Bold="True" Font-Size="Small" CssClass="inActivo"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate>
						<div class="coldiv1 org_col">
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label5" runat="server" Text="Perfil" Font-Size="Medium" Font-Bold="True"></asp:Label>
								</div>
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:DropDownList ID="DdlPerfil" runat="server" AutoPostBack="True" ViewStateMode="Enabled" OnSelectedIndexChanged="DdlPerfil_SelectedIndexChanged"></asp:DropDownList>
									<asp:Label ID="LblAvisoPer" runat="server" Text="Seleccione un Perfil" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Visible="False"></asp:Label>
								</div>
							</div>
							<div class="coldiv1">
								<asp:TreeView ID="TvPerfil" runat="server" CssClass="TvPerfil" Font-Bold="True" Font-Size="Medium" ShowLines="True">
									<HoverNodeStyle Font-Bold="True" Height="20px" Width="20px" />
									<LeafNodeStyle height="20px" width="20px" />
									<RootNodeStyle Font-Bold="True" Height="20px" Width="20px" />
								</asp:TreeView>
							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>
			</ajaxToolkit:TabContainer>
		</div>																			

	</div>

</asp:Content>
