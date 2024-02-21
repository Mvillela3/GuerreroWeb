<%@ Page Title="Detalle de Almacen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Views/AlmacenesDet.aspx.cs" Inherits="GuerreroWeb.Views.AlmacenesDet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td colspan="6" style="background-color:black;" style="padding: 10px 10px 10px 10px">
                <asp:Label ID="LblTitulo" runat="server" Text="Detalle de Sucursal" Font-Bold="True" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr style="background-color:orange;">
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
    <div class="conteiner" style="background-image: url(../Resources/FondoGro2.jpg);background-size: 100%  auto; background-repeat:no-repeat; background-position: 50% 50%; height:500px;width: 100%;" width="100%">
    <table  width="100%" >
        <tr>
            <td class="campo_cen_izq">
                <asp:Label ID="LblAlm" runat="server" Text="Almacen:" Font-Bold="True"></asp:Label>
            </td>
            <td class="campo_cen_izq">
                <asp:TextBox ID="TxtAlmacen" runat="server" MaxLength="50" CssClass="coldiv1"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Nombre:" Font-Bold="True"></asp:Label>
            </td>
            <td class="campo_cen_izq" colspan="2">
                <asp:TextBox ID="TxtNombre" runat="server" MaxLength="50" CssClass="coldiv1"></asp:TextBox>
            </td>
            <td>
                <br />
            </td>
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
                <asp:DropDownList ID="DdlCol" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlCol_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="CP:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtCP" runat="server" MaxLength="5" AutoPostBack="True" OnTextChanged="TxtCP_TextChanged"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Sucursal:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DdlSucursal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlSucursal_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Dirección:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtDir" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="No. Ext.:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtNoExt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="No. Int" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtNoInt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Encargado:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DdlEncargado" runat="server" AutoPostBack="True" ViewStateMode="Enabled"></asp:DropDownList>
            </td>
            <td>
                <br />
            </td>
            <td>
                <br />
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
