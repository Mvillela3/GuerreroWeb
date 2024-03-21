<%@ Page Title="Detalle del Árticulo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/Inventarios/ArticulosDet.aspx.cs" Inherits="GuerreroWeb.Views.Inventarios.ArticulosDet" %>
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
			<td colspan="6" style="background-color: black; padding: 10px 10px 10px 10px;">
				<asp:Label ID="LblTitulo" runat="server" Text="Detalle del Articulo ó Servicio" Font-Bold="True" ForeColor="White"></asp:Label>
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
	<div class="conteiner" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;" width="100%">
		<div class="coldiv1">
			<ajaxToolkit:TabContainer ID="TcPestana" runat="server" ActiveTabIndex="0" Height="520px" Width="100%" BackColor="#FF6600" ScrollBars="Auto" ViewStateMode="Enabled" VerticalStripWidth="800px" CssClass="Tabjax" AutoPostBack="True" OnActiveTabChanged="TcPestana_ActiveTabChanged" >
<!-- ####################################################################################################### -->
				<ajaxToolkit:TabPanel ID="Pestana1" runat="server" Height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div1" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana1" runat="server" Text="General" Font-Bold="True" Font-Size="Small" CssClass="Activo" BackColor="Orange" ViewStateMode="Enabled"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate>
						<div class="coldiv1 org_col" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;">
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label3" runat="server" Text="Datos Generales" Font-Bold="True" Font-Size="Medium"></asp:Label>
								</div>
							</div>
							<div class="coldiv1">
								<table width="100%">
									<tr>
										<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label0" runat="server" Text="Codigo:" Font-Bold="True"></asp:Label>
										</td>
										<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
											<div class="coldiv1">
												<asp:TextBox ID="TxtCodigo" runat="server" MaxLength="15" onblur="avisos1(this.id);" CssClass="coldiv1" onfocus="this.select()" OnTextChanged="TxtUsuario_TextChanged" ViewStateMode="Enabled"></asp:TextBox>
											</div>
											<div class="coldiv1">
												<asp:Label ID="LblMensaje1" runat="server" onclick="avisos1(this.id);" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
											</div>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label1" runat="server" Text="Descripción:" Font-Bold="True"></asp:Label>
										</td>
										<td class="campo_cen_izq" colspan="3" style="padding: 5px 0 5px 0;">
											<div class="coldiv1">
												<asp:TextBox ID="TxtDescripcion" runat="server" MaxLength="200" onfocus="this.select()" onblur="avisos1(this.id);" CssClass="coldiv1" OnTextChanged="TxtDescripcion_TextChanged" ViewStateMode="Enabled" Width="100%"></asp:TextBox>
											</div>
											<div class="coldiv1">
												<asp:Label ID="LblMensaje2" runat="server" Text="Este Campo es Obligatorio" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
											</div>
										</td>
									</tr>
									<tr>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label7" runat="server" Text="Linea:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<div class="coldiv1">
												<asp:DropDownList ID="DdlLinea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlLinea_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
											</div>
											<div class="coldiv1">
												<asp:Label ID="LblMensaje3" runat="server" Text="Seleccione una Linea" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
											</div>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label8" runat="server" Text="Categoría:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<div class="coldiv1">
												<asp:DropDownList ID="DdlCatego" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlCatego_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
											</div>
											<div class="coldiv1">
												<asp:Label ID="LblMensaje4" runat="server" Text="Seleccione una Categoía" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
											</div>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label9" runat="server" Text="Familia:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<asp:DropDownList ID="DdlFamilia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlFamilia_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
											<asp:Label ID="LblAvisoDep" runat="server" Text="Seleccione una Familia" ForeColor="#FF3300" Font-Size="Small" Font-Bold="True" Style="display: none;"></asp:Label>
										</td>
									</tr>
									<tr>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label4" runat="server" Text="Marca:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:DropDownList ID="DdlMarca" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlMarca_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label6" runat="server" Text="Modelo:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:DropDownList ID="DdlModelo" runat="server" AutoPostBack="True" ViewStateMode="Enabled"></asp:DropDownList>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label12" runat="server" Text="Estatus:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:DropDownList ID="DdlEstatus" runat="server" AutoPostBack="True" ViewStateMode="Enabled">
												<asp:ListItem Text="ACTIVO" Value="ACTIVO"></asp:ListItem>
												<asp:ListItem Text="BAJA" Value="BAJA"></asp:ListItem>
												<asp:ListItem Text="DESCONTINUADO" Value="DESCONTINUADO"></asp:ListItem>
											</asp:DropDownList>
										</td>
									</tr>
									<tr>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label10" runat="server" Text="Unidad Medida:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:DropDownList ID="DdlMedida" runat="server" ViewStateMode="Enabled"></asp:DropDownList>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label11" runat="server" Text="Tipo de Producto:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:DropDownList ID="DdlTipo" runat="server" AutoPostBack="True" ViewStateMode="Enabled" OnSelectedIndexChanged="DdlTipo_SelectedIndexChanged">
												<asp:ListItem Text="PRODUCTO" Value="PRODUCTO"></asp:ListItem>
												<asp:ListItem Text="SERVICIO" Value="SERVICIO"></asp:ListItem>
												<asp:ListItem Text="PAQUETE" Value="PAQUETE"></asp:ListItem>
											</asp:DropDownList>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label40" runat="server" Text="Bar Code:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:TextBox ID="TxtBarCode" runat="server" MaxLength="15" onfocus="this.select()" CssClass="coldiv1" ViewStateMode="Enabled"></asp:TextBox>
										</td>
									</tr>
								</table>

							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>
<!-- ####################################################################################################### -->
				<ajaxToolkit:TabPanel ID="Pestana2" runat="server" Height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div2" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana2" runat="server" Text="Precios y Costos" BackColor="Yellow" Font-Bold="True" Font-Size="Small" CssClass="inActivo" Width="150px" ViewStateMode="Enabled"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate>
						<div class="coldiv1 org_col" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;">
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label5" runat="server" Text="Precios y Costos" Font-Size="Medium" Font-Bold="True"></asp:Label>
								</div>
							</div>
							<div class="coldiv1">
								<table width="100%">
									<tr>
										<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label2" runat="server" Text="Precio:" Font-Bold="True"></asp:Label>
										</td>
										<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
											<div class="coldiv1">
												<asp:TextBox ID="TxtPrecio" runat="server" MaxLength="15" onblur="avisos1(this.id);" CssClass="coldiv1" onfocus="this.select()" ViewStateMode="Enabled">0.00</asp:TextBox>
											</div>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label14" runat="server" Text="Precio Contado:" Font-Bold="True"></asp:Label>
										</td>
										<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
											<div class="coldiv1">
												<asp:TextBox ID="TxtPrecioCont" runat="server" MaxLength="22" onfocus="this.select()" onblur="avisos1(this.id);" CssClass="coldiv1" ViewStateMode="Enabled">0.00</asp:TextBox>
											</div>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label13" runat="server" Text="Precio Minimo:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:TextBox ID="TxtPrecioMin" runat="server" MaxLength="22" onfocus="this.select()" onblur="avisos1(this.id);" CssClass="coldiv1" ViewStateMode="Enabled">0.00</asp:TextBox>
										</td>
									</tr>
									<tr>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label16" runat="server" Text="Costo Promedio:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<asp:Label ID="LblCostoProm" runat="server" Text="0.00" ForeColor="Black" Font-Size="Small" Font-Bold="False" ></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label18" runat="server" Text="Ultimo Costo:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<asp:Label ID="LblUltCosto" runat="server" Text="0.00" ForeColor="Black" Font-Size="Small" Font-Bold="False" ></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label20" runat="server" Text="Ultima Compra:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<asp:Label ID="LblUltCompra" runat="server" Text="dd/mm/aaaa" ForeColor="Black" Font-Size="Small" Font-Bold="False" ></asp:Label>
										</td>
									</tr>
									<tr>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
									</tr>
									<tr>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
									</tr>
								</table>
							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>
				<!-- ####################################################################################################### -->
				<ajaxToolkit:TabPanel ID="Pestana3" runat="server" Height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div3" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana3" runat="server" Text="Impuestos y Contable" Font-Bold="True" BackColor="Yellow" Font-Size="Small" CssClass="inActivo" Width="200px" ViewStateMode="Enabled"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate>
						<div class="coldiv1 org_col" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;">
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label17" runat="server" Text="Impuestos y Contable" Font-Size="Medium" Font-Bold="True"></asp:Label>
								</div>
							</div>
							<div class="coldiv1">
								<table width="100%">
									<tr>
										<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label19" runat="server" Text="Codigo CFDI:" Font-Bold="True"></asp:Label>
										</td>
										<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
											<div class="coldiv1">
												<asp:TextBox ID="TxtCodigoCFDI" runat="server" MaxLength="15" onblur="avisos1(this.id);" CssClass="coldiv1" onfocus="this.select()" ViewStateMode="Enabled">0.00</asp:TextBox>
											</div>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label21" runat="server" Text="Impuesto 1:" Font-Bold="True"></asp:Label>
										</td>
										<td class="campo_cen_izq" style="padding: 5px 0 5px 0;">
											<div class="coldiv1">
												<asp:DropDownList ID="DdlImpuesto1" runat="server" ViewStateMode="Enabled"></asp:DropDownList>
											</div>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label27" runat="server" Text="Impuesto 2:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:DropDownList ID="DdlImpuesto2" runat="server" ViewStateMode="Enabled"></asp:DropDownList>
										</td>
									</tr>
									<tr>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label28" runat="server" Text="Impuesto 3:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<asp:DropDownList ID="DdlImpuesto3" runat="server" ViewStateMode="Enabled"></asp:DropDownList>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label30" runat="server" Text="Cuenta Contable Debe:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<asp:TextBox ID="TxtCContable1" runat="server" MaxLength="30" onfocus="this.select()" onblur="avisos1(this.id);" CssClass="coldiv1" ViewStateMode="Enabled"></asp:TextBox>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label32" runat="server" Text="Cuenta Contable Haber:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;" class="org_col">
											<asp:TextBox ID="TxtCContable2" runat="server" MaxLength="30" onfocus="this.select()" onblur="avisos1(this.id);" CssClass="coldiv1" ViewStateMode="Enabled"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td style="padding: 5px 0 5px 0;">
											<asp:Label ID="Label34" runat="server" Text="Cuenta Contable 3:" Font-Bold="True"></asp:Label>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<asp:TextBox ID="TxtCContable3" runat="server" MaxLength="30" onfocus="this.select()" onblur="avisos1(this.id);" CssClass="coldiv1" ViewStateMode="Enabled"></asp:TextBox>
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
										<td style="padding: 5px 0 5px 0;">
											<br />
										</td>
									</tr>
								</table>
							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>


				<!-- ####################################################################################################### -->
				<ajaxToolkit:TabPanel ID="Pestana4" runat="server" Height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div4" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana4" runat="server" Text="Componentes" Font-Bold="True" Font-Size="Small" BackColor="Yellow" CssClass="inActivo" Width="110px" ViewStateMode="Enabled"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate>
						<div class="coldiv1 org_col" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;">
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label31" runat="server" Text="Componentes del Paquete" Font-Size="Medium" Font-Bold="True"></asp:Label>
								</div>
							</div>
							<div class="coldiv1">
								<br />
							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>
				<!-- ####################################################################################################### -->
				<ajaxToolkit:TabPanel ID="Pestana5" runat="server" Height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div5" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana5" runat="server" Text="Colores" Font-Bold="True" Font-Size="Small" BackColor="Yellow" CssClass="inActivo" ViewStateMode="Enabled"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate>
						<div class="coldiv1 org_col" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;">
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label35" runat="server" Text="Colores del Producto" Font-Size="Medium" Font-Bold="True"></asp:Label>
								</div>
							</div>
							<div class="coldiv1">
								<br />
							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>
				<!-- ####################################################################################################### -->
				<ajaxToolkit:TabPanel ID="Pestana6" runat="server" Height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div6" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana6" runat="server" Text="Tallas" Font-Bold="True" Font-Size="Small" BackColor="Yellow" CssClass="inActivo" ViewStateMode="Enabled"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate>
						<div class="coldiv1 org_col" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;">
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label37" runat="server" Text="Tallas del Producto" Font-Size="Medium" Font-Bold="True"></asp:Label>
								</div>
							</div>
							<div class="coldiv1">
								<br />
							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>
				<!-- ####################################################################################################### -->
				<ajaxToolkit:TabPanel ID="Pestana7" runat="server" Height="100%" Width="100%">
					<HeaderTemplate>
						<div runat="server" id="Div7" class="coldiv1 campo_cen_cen">
							<asp:Label ID="LblPestana7" runat="server" Text="Fotos" Font-Bold="True" Font-Size="Small" CssClass="inActivo" BackColor="Yellow" ViewStateMode="Enabled"></asp:Label>
						</div>
					</HeaderTemplate>
					<ContentTemplate>
						<div class="coldiv1 org_col" style="background-image: url(../../Resources/FondoGro2.jpg); background-size: 100%  auto; background-repeat: no-repeat; background-position: 50% 50%; height: 500px; width: 100%;">
							<div class="coldiv1 org_row campo_cen_izq" style="padding: 5px 5px 5px 5px">
								<div class="campo_cen_izq" style="padding: 5px 5px 5px 5px">
									<asp:Label ID="Label39" runat="server" Text="Fotos del Producto" Font-Size="Medium" Font-Bold="True"></asp:Label>
								</div>
							</div>
							<div class="coldiv1">
								<br />
							</div>

						</div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>

			</ajaxToolkit:TabContainer>
		</div>

	</div>

</asp:Content>
