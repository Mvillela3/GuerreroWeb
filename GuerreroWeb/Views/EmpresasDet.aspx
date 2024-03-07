<%@ Page Title="Detalle de Empresa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/EmpresasDet.aspx.cs" Inherits="GuerreroWeb.Views.EmpresasDet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td colspan="6" style="background-color:black;padding: 10px 10px 10px 10px">
                <asp:Label ID="LblTitulo" runat="server" Text="Detalle de Empresa" ForeColor="White" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr style="background-color:orange;">
            <td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
                <asp:ImageButton ID="BtnEditar" runat="server" ImageUrl="~/Resources/Editar2.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="True" ToolTip="Editar" OnCommand="BtnEditar_Command" />
            </td>
            <td class="campo_cen_cen" width="50px" style="padding: 10px 10px 10px 10px">
                <asp:ImageButton ID="BtnGuardar" runat="server" ImageUrl="~/Resources/Guardar.png" CssClass="btn btnblock bg-light" Height="40px" Enabled="False" ToolTip="Guardar" OnCommand="BtnGuardar_Command" OnClientClick="return confirm('¿Están Correctos los Datos?');" />
                <asp:Button ID="BtnVtMdl" runat="server" Text="MostrarModal" style="display:none;" />
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
    <div class="conteiner" style="background-image: url(../Resources/FondoGro2.jpg);background-size: 100%  auto; background-repeat:no-repeat; background-position: 50% 50%; height:500px;width: 100%;" width="100%">
    <table  width="100%" >

        <tr>
            <td class="campo_cen_izq">
                <asp:Label ID="LblEmp" runat="server" Text="Empresa:" Font-Bold="True"></asp:Label>
            </td>
            <td class="campo_cen_izq" colspan="2">
                <asp:Label ID="LblEmpresa" runat="server" Text="" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Nombre Comercial:" Font-Bold="True"></asp:Label>
            </td>
            <td class="campo_cen_izq" colspan="2">
                <asp:Label ID="LblNombreCom" runat="server" Text="" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="RFC:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LblRFC" runat="server" Text="" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Regimen Fiscal:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="DdlRFiscal" runat="server" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td><br /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Pais:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DdlPais" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlPais_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Estado:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DdlEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlEstado_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Ciudad:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DdlCiu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlCiu_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Colonia:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DdlCol" runat="server" OnSelectedIndexChanged="DdlCol_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="CP:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtCP" runat="server" MaxLength="5" onfocus="this.select()" OnTextChanged="TxtCP_TextChanged"></asp:TextBox>
            </td>
            <td><br /></td>
            <td><br /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Dirección:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtDir" runat="server" onfocus="this.select()"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="No. Ext.:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtNoExt" runat="server" onfocus="this.select()"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="No. Int" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtNoInt" runat="server" onfocus="this.select()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Telefono 1;" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtTel1" runat="server" MaxLength="15" onfocus="this.select()"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label13" runat="server" Text="Telefono 2;" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtTel2" runat="server" MaxLength="15" onfocus="this.select()"></asp:TextBox>
            </td>
            <td><br /></td>
            <td><br /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Email:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtEmail" runat="server" MaxLength="100" onfocus="this.select()" Font-Bold="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label15" runat="server" Text="Página Web:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtWeb" runat="server" MaxLength="100" onfocus="this.select()"></asp:TextBox>
            </td>
            <td><br /></td>
            <td><br /></td>
        </tr>

    </table>
    </div>
        <asp:Panel ID="VtMdl" runat="server" CssClass="coldiv2 campo_cen_cen" style="width:550px; height:400px" Visible="False" >
            <div class="campo_cen_cen alin-col  FondoMsg" style="width:550px; height:400px">
            <table width="100%">
                <tr>
                    <td colspan="6"><br /></td>
                </tr>
                <tr>
                    <td colspan="2" class="campo_cen_cen">
                        <asp:Image ID="ImgModal" runat="server" CssClass="ImgModal" ImageUrl="~/Resources/Advertencia.png" width="85px" heigth="80px" />
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
                        <asp:Label ID="LblMensaje" runat="server" Text="Label" Height="209px" Width="100%" BorderStyle="None" ForeColor="White" Font-Size="Medium" style="text-align: justify;padding: 0  10px  0  10px"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td class="campo_cen_cen">
                        <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" Visible="False" OnCommand="BtnAceptar_Command" />
                    </td>
                    <td>
                        <asp:Button ID="BtnSi" runat="server" CssClass="btn btn-block" Text="Aceptar" Width="103" Height="31" BackColor="Lime" OnCommand="BtnSi_Command" />
                    </td>
                    <td colspan="2">
                        <asp:Button ID="BtnOk" runat="server" CssClass="btn btn-block" Text="Ok" Width="103" Height="31"  BackColor="#FF8000" OnCommand="BtnOk_Command" />
                    </td>
                    <td>
                        <asp:Button ID="BtnNo" runat="server" CssClass="btn btn-block" Text="Cancelar" Width="103" Height="31"  BackColor="Red" OnCommand="BtnNo_Command" />
                    </td>
                    <td class="campo_cen_cen"><br /></td>

                </tr>
            </table>
            </div>
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="AjVtModal" runat="server" OkControlID="BtnAceptar" CancelControlID="BtnAceptar" TargetControlID="BtnVtMdl" PopupControlID="VtMdl" BackgroundCssClass="ImgMdl"></ajaxToolkit:ModalPopupExtender>

</asp:Content>
