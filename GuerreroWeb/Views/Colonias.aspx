<%@ Page Title="Catálogo de Colonias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/Colonias.aspx.cs" Inherits="GuerreroWeb.Views.Colonias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div style="width:100%;height:75vh;" class="mh-100">
    <table width="100%">
    <tr>
        <td colspan="4" style="background-color:black;padding: 10px 10px 10px 10px">
            <asp:Label ID="LblTitulo" runat="server" Text="Catálogo de Colonias" ForeColor="White" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr style="background-color:orange;">
        <td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
            <asp:ImageButton ID="BtnNuevo" runat="server" ImageUrl="~/Resources/nuevo.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="True" ViewStateMode="Enabled" OnCommand="BtnNuevo_Command" />
            <asp:Button ID="BtnVtMdl" runat="server" Text="MostrarModal" style="display:none;" ViewStateMode="Enabled" />
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
    <tr id="DivBuscar" runat="server" style="background-color:orange;"> 
        <td colspan="4">
            <asp:Panel ID="PnlConsulta" runat="server" DefaultButton="BtnConslta" CssClass="campo_cen_izq org-row">
                <table>
                    <tr>
                        <td class="w-25 campo_cen_der">
                            <asp:TextBox ID="TxtBuscar" runat="server" onfocus="this.select()" MaxLength="50" CssClass="coldiv1"></asp:TextBox>
                        </td>
                        <td class="campo_cen_der">
                            <asp:Label ID="LblBusca1" runat="server" Text="País:" Font-Bold="True" ></asp:Label>
                        </td>
                        <td class="w-25 campo_cen_cen">
                            <asp:DropDownList ID="DdlPaisB" runat="server" AutoPostBack="True" width="100%" OnSelectedIndexChanged="DdlPaisB_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
                        </td>
                        <td class="campo_cen_der">
                            <asp:Label ID="LblBusca2" runat="server" Text="Estado:" Font-Bold="True" ></asp:Label>
                        </td>
                        <td class="w-25 campo_cen_cen">
                            <asp:DropDownList ID="DdlEstadoB" runat="server" AutoPostBack="True" width="100%" OnSelectedIndexChanged="DdlEstadoB_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
                        </td>
                        <td class="campo_cen_der">
                            <asp:Label ID="LblBusca3" runat="server" Text="Ciudad:" Font-Bold="True" ></asp:Label>
                        </td>
                        <td class="w-25 campo_cen_cen">
                            <asp:DropDownList ID="DdlCiudadB" runat="server" AutoPostBack="True" width="100%" OnSelectedIndexChanged="DdlCiudadB_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
                        </td>
                        <td class="w-25">
                            <asp:ImageButton ID="BtnConslta" runat="server"  ImageUrl="~/Resources/IcoBuscar.png" Height="30px" Width="30px" OnCommand="BtnConslta_Command"/>
                        </td>
                    </tr>
                </table>                    

            </asp:Panel>
        </td>
    </tr>
    </table>
    <div style="overflow-x:scroll;overflow-y:scroll;width:100%;height:100%;">
        <div class="campo_top_izq" style="width:100%; height:490px">

            <asp:GridView ID="GvConsulta" runat="server" width="100%" height="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowPaging="True" ShowHeaderWhenEmpty="True" ViewStateMode="Enabled" OnRowDataBound="GvConsulta_RowDataBound" OnRowDeleting="GvConsulta_RowDeleting" OnRowEditing="GvConsulta_RowEditing" OnPageIndexChanging="GvConsulta_PageIndexChanging">
            <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnEditar" runat="server" ImageUrl="~/Resources/IcoEditar.png" Height="40" Width="40" CommandName="Edit" ToolTip="Modificar"  />
                            <asp:ImageButton ID="BtnDel" runat="server" ImageUrl="~/Resources/IcoEliminar.png" Height="40" Width="40" CommandName="Delete" ToolTip="Eliminar" OnClientClick="return confirm('¿Está Seguro de Eliminar el Registro?');" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id" Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblId" Text='<%# Bind("IdCol") %>'></asp:Label>
                        </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="40px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Colonia">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblColonia" Text='<%# Bind("Colonia") %>'></asp:Label>
                        </ItemTemplate>
                    <FooterStyle Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CP">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblCP" Text='<%# Bind("CP") %>'></asp:Label>
                        </ItemTemplate>
                    <FooterStyle Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" Height="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Es Default">
                        <ItemTemplate>
                            <asp:Image ID="ImgDefault" runat="server"  Height="40" Width="40" ImageUrl="~/Resources/Check2.png" />
                            <asp:HiddenField ID="HfDefault" runat="server" value='<%# Bind("EsDefault") %>' />
                        </ItemTemplate>
                    <FooterStyle Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="País">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblPais" Text='<%# Bind("Pais") %>'></asp:Label>
                        </ItemTemplate>
                    <FooterStyle Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblEstado" Text='<%# Bind("Estado") %>'></asp:Label>
                        </ItemTemplate>
                    <FooterStyle Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ciudad">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblCiudad" Text='<%# Bind("Ciudad") %>'></asp:Label>
                        </ItemTemplate>
                    <FooterStyle Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="Orange" Font-Bold="True" ForeColor="Black" />
				<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Font-Size="Larger" HorizontalAlign="Center" VerticalAlign="Middle" />
				<PagerSettings mode="Numeric" position="Bottom" pagebuttoncount="10" />
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
 
    <asp:Panel ID="VtMdl" runat="server" CssClass="campo_cen_cen" style="width:550px; height:400px" ViewStateMode="Enabled" Visible="False" >
        <div class="campo_cen_cen alin-col FondoMsg VtnMsg" style="width:550px; height:400px">
        <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label1" runat="server" Text="Detalle de la Colonia" Font-Bold="True" Font-Size="Larger"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4"><br /></td>
            </tr>
            <tr>
                <td colspan="4"><br /></td>
            </tr>
            <tr>
                <td  class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:Label ID="Label2" runat="server" Text="Colonia:" BorderStyle="None" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px" ViewStateMode="Enabled" Font-Bold="True"></asp:Label> 
                </td>
                <td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:TextBox ID="TxtColonia" runat="server" width="100%" onfocus="this.select()" MaxLength="60" ViewStateMode="Enabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:Label ID="Label7" runat="server" Text="CP:" BorderStyle="None" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px" ViewStateMode="Enabled" Font-Bold="True"></asp:Label> 
                </td>
                <td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:TextBox ID="TxtCP" runat="server" width="100%" onfocus="this.select()" MaxLength="5" ViewStateMode="Enabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:Label ID="Label4" runat="server" Text="Es Default:" BorderStyle="None" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px" ViewStateMode="Enabled" Font-Bold="True"></asp:Label> 
                </td>
                <td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
					<asp:CheckBox ID="ChkDefault" runat="server" Height="20px" ViewStateMode="Enabled" Width="20px" CssClass="chkbox1" />
				</td>
            </tr>
            <tr>
                <td  class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:Label ID="Label5" runat="server" Text="País:" BorderStyle="None" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px" ViewStateMode="Enabled" Font-Bold="True"></asp:Label> 
                </td>
                <td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:DropDownList ID="DdlPais" runat="server" AutoPostBack="True" EnableTheming="True" OnSelectedIndexChanged="DdlPais_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:Label ID="Label3" runat="server" Text="Estado:" BorderStyle="None" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px" ViewStateMode="Enabled" Font-Bold="True"></asp:Label> 
                </td>
                <td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:DropDownList ID="DdlEstado" runat="server" AutoPostBack="True" EnableTheming="True" OnSelectedIndexChanged="DdlEstado_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:Label ID="Label6" runat="server" Text="Ciudad:" BorderStyle="None" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px" ViewStateMode="Enabled" Font-Bold="True"></asp:Label> 
                </td>
                <td class="campo_cen_izq" colspan="2" style="padding: 2px 10px  2px  10px">
                    <asp:DropDownList ID="DdlCiudad" runat="server" AutoPostBack="True" EnableTheming="True" OnSelectedIndexChanged="DdlCiudad_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4"><br /></td>
            </tr>
            <tr>
                <td colspan="4"><br /></td>
            </tr>
            <tr >
                <td class="campo_cen_cen w-25">
                    <br />
                </td>
                <td class="campo_cen_cen w-25" style="padding: 10px 0  10px  0">
                    <asp:Button ID="BtnGuardar" runat="server" CssClass="btn" Text="Guardar" Width="103" Height="31" BackColor="Lime" ViewStateMode="Enabled" Font-Bold="True" OnClientClick="return confirm('¿Están Correctos los Datos?');" OnClick="BtnGuardar_Click" />
                </td>
                <td class="campo_cen_cen w-25" style="padding: 10px 0  10px  0" >
                    <asp:Button ID="BtnCancelar" runat="server" CssClass="btn" Text="Cancelar" Width="103" Height="31"  BackColor="#FF5050" ViewStateMode="Enabled" Font-Bold="True" OnClientClick="return confirm('¿Está seguro de no Guardar los Datos?');" OnClick="BtnCancelar_Click" />
                </td>
                <td class="campo_cen_cen w-25">
                    <br />
                </td>
            </tr>
        </table>
            <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" style="display:none;" OnCommand="BtnAceptar_Command" />
        </div>
    </asp:Panel>



    <ajaxToolkit:ModalPopupExtender ID="MpeVtMdl" runat="server" BehaviorID="MpeVtMdl" DynamicServicePath="" OkControlID="BtnAceptar" TargetControlID="BtnVtMdl" CancelControlID="BtnAceptar" PopupControlID="VtMdl" BackgroundCssClass="FndContMdl">
    </ajaxToolkit:ModalPopupExtender>

    <asp:HiddenField ID="HfId" runat="server" ViewStateMode="Enabled" />

</asp:Content>
