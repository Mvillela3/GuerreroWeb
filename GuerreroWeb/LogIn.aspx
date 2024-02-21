<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="GuerreroWeb.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="~/Resources/iconoGroHD.ico" rel="shortcut icon" type="image/x-icon" />

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Acceso al Sistema</title>
    <style type="text/css">

        .auto-style11 {
            width: 275px;
        }
        .auto-style21 {
            width: 50px;
        }
        .auto-style22 {
            width: 50px;
            height: 60px;
        }
        .auto-style23 {
            height: 60px;
            width: 275px;
        }
        .auto-style26 {
            width: 50px;
            height: 80px;
            vertical-align:bottom;
            padding-top:20px;
        }
        .auto-style27 {
            height: 80px;
            vertical-align:bottom;
            padding-top:20px;
        }
        .auto-style28 {
            width: 275px;
            height: 80px;
            vertical-align:bottom;
            padding-top:20px;
        }
        .auto-style29 {
            width: 50px;
            height: 30px;
        }
        .auto-style30 {
            width: 275px;
            height: 30px;
        }
    </style>

</head>
<body style="background-image: url(../Resources/fondo_ingreso.jpg); background-repeat:no-repeat; background-position:top">
    <p>
        <br />
    </p>
    <form id="form1" runat="server" autocomplete="off">
    <div style="padding-top:10%; ">    
        <table style="margin:0 auto ; padding-top :20px; padding-left:120px; padding-right:120px; padding-bottom :20px; background-color:white; border-radius: 10px 10px;" > 
            <tr>
                <td class="auto-style21"></td>
                <td class="auto-style11"></td>
                <td class="auto-style21"></td>
            </tr>
            <tr>
                <td style="text-align:center; vertical-align:bottom;" class="auto-style27" colspan="3" >
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Resources/ssc_logo.png" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:bottom;" class="auto-style27" >
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/usuario.png" />
                </td>
                <td style="vertical-align:bottom"; class="auto-style28" >
                    <asp:DropDownList ID="DdlUsuario" runat="server" style="margin-bottom: 7px" Width="271px" Font-Names="Century Gothic" DropDownRows="5" OnSelectedIndexChanged="DdlUsuario_SelectedIndexChanged" ValidateRequestMode="Enabled">
                    </asp:DropDownList>
                    <asp:Label ID="LblUsu" runat="server" Text="Seleccione Un Usuario" Font-Bold="True" Font-Names="Century Gothic" ForeColor="Red" Visible="False"></asp:Label>
                </td>
                <td class="auto-style26"></td>
            </tr>
            <tr>
                <td class="auto-style21"></td>
                <td class="auto-style11"></td>
                <td class="auto-style21"></td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:bottom;">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Resources/password.png" />
                </td>
                <td class="auto-style11">
                    <asp:TextBox ID="TxtPwd" runat="server" Width="271px" Password="True" Caption="Contraseña" MaxLength="15" Theme="iOS" Font-Names="Century Gothic" OnTextChanged="TxtPwd_TextChanged" TextMode="Password" ValidateRequestMode="Enabled"></asp:TextBox>
                    <asp:Label ID="LblPwd" runat="server" Text="Escriba Una Contraseña" Font-Bold="True" Font-Names="Century Gothic" ForeColor="Red" Visible="False"></asp:Label>
                </td>
                <td class="auto-style21"></td>
            </tr>
            <tr>
                <td class="auto-style29"></td>
                <td class="auto-style30">
                    <asp:Label ID="LblMensaje" runat="server" Text="Mensaje" Visible="False" ForeColor="#FF5050" Font-Bold="True" Font-Names="Century Gothic" Theme="iOS" ViewStateMode="Enabled"></asp:Label>
                    <br />
                </td>
                <td class="auto-style29"></td>
            </tr>
            <tr>
                <td class="auto-style22"></td>
                <td style="vertical-align:top" class="auto-style23">
                    <asp:Button ID="BtnEntrar" runat="server" Text="Ingresar" Width="271px" AutoPostBack="False" Font-Names="Century Gothic" Height="40px" Theme="Default" Font-Bold="True" Font-Overline="False" Font-Size="Medium" Font-Underline="False" OnCommand="BtnEntrar_Command" />
                </td>
                <td class="auto-style22"></td>
            </tr>
            </table>
    </form>
</body>
</html>
