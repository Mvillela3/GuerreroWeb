<%@ Page Title="Catálogo de Empresas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/Empresas.aspx.cs" Inherits="GuerreroWeb.Views.Empresas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width:100%;height:75vh;" class="mh-100">
    <table width="100%">
        <tr>
            <td colspan="4" style="background-color:black;padding: 10px 10px 10px 10px">
                <asp:Label ID="LblTitulo" runat="server" Text="Catálogo de Empresas" ForeColor="White" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr style="background-color:orange;">
            <td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
                <asp:ImageButton ID="BtnNuevo" runat="server" ImageUrl="~/Resources/nuevo.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="False" ViewStateMode="Enabled" />
                <asp:Button ID="BtnVtMdl" runat="server" Text="MostrarModal" style="display:none;" ViewStateMode="Enabled" OnCommand="BtnVtMdl_Command" />
            </td>
            <td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
                <asp:ImageButton ID="BtnEditar" runat="server" ImageUrl="~/Resources/Editar2.png" CssClass="btn btnblock bg-light" Height="40px" OnCommand="BtnEditar_Command" Visible="False" ViewStateMode="Enabled" />
            </td>
            <td class="campo_cen_cen" width="40px" style="padding: 10px 10px 10px 10px">
                <asp:ImageButton ID="BtnBorrar" runat="server" ImageUrl="~/Resources/Borrar.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="False" Visible="False" ViewStateMode="Enabled" />
            </td>
            <td class="campo_cen_izq" style="padding: 10px 10px 10px 10px">
                <asp:ImageButton ID="BtnBuscar" runat="server" ImageUrl="~/Resources/Buscar.png" CssClass="btn btnblock bg-light" Height="40px" OnCommand="BtnBuscar_Command" ViewStateMode="Enabled" />
            </td>
        </tr>
        <tr id="DivBuscar" runat="server" style="background-color:orange;"> 
            <td colspan="4">
                <asp:TextBox ID="TxtBuscar" runat="server" onfocus="this.select()" MaxLength="50" CssClass="coldiv2"></asp:TextBox>
            </td>
        </tr>
        </table>

        <div style="overflow-x:scroll;overflow-y:hidden;width:100%;height:100%;">
            <div class="campo_top_izq" style="width:1400px;height:490px">

                <asp:GridView ID="GvConsulta" runat="server" width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowCustomPaging="False" ShowFooter="False" AllowPaging="True" OnSelectedIndexChanged="GvConsulta_SelectedIndexChanged" ShowHeaderWhenEmpty="True" OnRowDataBound="GvConsulta_RowDataBound" ViewStateMode="Enabled" OnPageIndexChanging="GvConsulta_PageIndexChanging">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="BtnEditar" runat="server" ImageUrl="~/Resources/eye-button.svg" Height="40" Width="40" CommandName="Select" ToolTip="Modificar"  />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Id" Visible="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblId" Text='<%# Bind("IdEmpresa") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="40px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Height="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Empresa">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblEmpresa" Text='<%# Bind("Empresa") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Width="5%" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Height="40px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" Height="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre Completo">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblNombre" Text='<%# Bind("NombreCom") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Width="20%" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Height="40px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" Height="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RFC">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblRFC" Text='<%# Bind("RFC") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="40px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dirección">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblDireccion" Text='<%# Bind("DireccionC") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" Height="40px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" Height="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ciudad">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblCiudad" Text='<%# Bind("Ciudad") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="40px" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" Height="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblEstado" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="40px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Regimen Fiscal">
                                <itemTemplate>
                                    <asp:Label runat="server" ID="LblRFical" Text='<%# Bind("ClvRfiscal") %>'></asp:Label>
                                </itemTemplate>
                                <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="40px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Height="30px" />
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
<!--
        <asp:Panel ID="VtMdl" runat="server" CssClass="campo_cen_cen" style="width:550px; height:400px" ViewStateMode="Enabled" OkControlID="BtnAceptar" CancelControlID="BtnAceptar" TargetControlID="BtnVtMdl" BackgroundCssClass="ImgMdl" ClientIDMode="AutoID" Visible="False" >
            <div class="campo_cen_cen alin-col FondoMsg VtnMsg" style="width:550px; height:400px">
            <table width="100%">
                <tr>
                    <td colspan="6"><br /></td>
                </tr>
                <tr>
                    <td colspan="2" class="campo_cen_cen">
                        <asp:Image ID="ImgModal" runat="server" CssClass="ImgModal" ImageUrl="~/Resources/Advertencia.png" width="85px" heigth="80px" ViewStateMode="Enabled" />
                    </td>
                    <td class="campo_cen_cen"><br /></td>
                    <td class="campo_cen_cen"><br /></td>
                    <td class="campo_cen_cen"><br /></td>
                    <td class="campo_cen_cen" style="width: 268435456px"><br /></td>
                </tr>
                <tr>
                    <td colspan="6"><br /></td>
                </tr>
                <tr>
                    <td colspan="6" class="campo_cen_izq" Height="209">
                        <asp:Label ID="LblMensaje" runat="server" Text="Label" Height="209px" Width="100%" BorderStyle="None" ForeColor="White" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px" ViewStateMode="Enabled"></asp:Label> 
                    </td>
                </tr>
                <tr >
                    <td class="campo_cen_cen" width="16.66%">
                        <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" style="display:none;" ViewStateMode="Enabled" OnCommand="BtnAceptar_Command" />
                    </td>
                    <td class="campo_cen_cen" width="16.66%">
                        <asp:Button ID="BtnSi" runat="server" CssClass="btn" Text="Aceptar" Width="103" Height="31" BackColor="Lime" ViewStateMode="Enabled" OnCommand="BtnSi_Command" OnClick="BtnSi_Click" />
                    </td>
                    <td colspan="2" class="campo_cen_cen" >
                        <asp:Button ID="BtnOk" runat="server" CssClass="btn" Text="Ok" Width="103" Height="31"  BackColor="#FF8000" ViewStateMode="Enabled" OnCommand="BtnOk_Command" OnClick="BtnOk_Click" />
                    </td>
                    <td class="campo_cen_cen" width="16.66%">
                        <asp:Button ID="BtnNo" runat="server" CssClass="btn" Text="Cancelar" Width="103" Height="31"  BackColor="Red" ViewStateMode="Enabled" OnCommand="BtnNo_Command" OnClick="BtnNo_Click" />
                    </td>
                    <td class="campo_cen_cen" width="16.66%" ><br /></td>

                </tr>
            </table>
            </div>
        </asp:Panel>


    <ajaxToolkit:ModalPopupExtender ID="VtMdl_ModalPopupExtender" runat="server" BehaviorID="VtMdl_ModalPopupExtender" DynamicServicePath="" TargetControlID="BtnVtMdl" CancelControlID="BtnAceptar" OkControlID="BtnAceptar" PopupControlID="VtMdl" BackgroundCssClass="FndContMdl" DropShadow="True">
    </ajaxToolkit:ModalPopupExtender>


 -->


<!--
    <div class="container">
        <div class="modal fade" id="mymodal" role="dialog">
        <div class="modal-dialog modal-dialog-centered modal-body">
            <div class="modal-content">
            <div class="campo_cen_cen alin-col  FondoMsg" style="width:550px; height:400px">
            <table width="100%">
                <tr>
                    <td colspan="6"><br /></td>
                </tr>
                <tr>
                    <td colspan="2" class="campo_cen_cen">
                        <asp:Image ID="ImgModal1" runat="server" CssClass="ImgModal" ImageUrl="~/Resources/Advertencia.png" width="85px" heigth="80px" ViewStateMode="Enabled" />
                    </td>
                    <td class="campo_cen_cen"><br /></td>
                    <td class="campo_cen_cen"><br /></td>
                    <td class="campo_cen_cen"><br /></td>
                    <td class="campo_cen_cen"><br /></td>
                </tr>
                <tr>
                    <td colspan="6"><br /></td>
                </tr>
                <tr>
                    <td colspan="7" class="campo_cen_izq" Height="209" width="100%">
                        <asp:Label ID="LblMensaje1" runat="server" Text="Label" Height="209px" Width="100%" BorderStyle="None" ForeColor="White" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px" ViewStateMode="Enabled"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td class="campo_cen_cen">
                        <asp:Button ID="BtnAceptar1" runat="server" Text="Aceptar" Visible="False" ViewStateMode="Enabled" />
                    </td>
                    <td>
                        <asp:Button ID="BtnSi1" runat="server" CssClass="btn btn-block" Text="Aceptar" Width="103" Height="31" BackColor="Lime" ViewStateMode="Enabled" />
                    </td>
                    <td colspan="2">
                        <asp:Button ID="BtnOk1" runat="server" CssClass="btn btn-block" Text="Ok" Width="103" Height="31"  BackColor="#FF8000" />
                    </td>
                    <td>
                        <asp:Button ID="BtnNo1" runat="server" CssClass="btn btn-block" Text="Cancelar" Width="103" Height="31"  BackColor="Red" ViewStateMode="Disabled" />
                    </td>
                    <td class="campo_cen_cen"><br /></td>

                </tr>
            </table>
            </div>

            </div>
        </div>
        </div>
    </div>
-->
 
<!--    
    <script type="text/javascript">
        var launch = false;
        function launchModal() {
            $find("VtMdl_ModalPopupExtender").show();
        }
    </script>
-->

</asp:Content>
