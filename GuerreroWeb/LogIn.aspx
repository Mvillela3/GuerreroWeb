<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/LogIn.aspx.cs" Inherits="GuerreroWeb.LogIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="~/Resources/iconoGroHD.ico" rel="shortcut icon" type="image/x-icon" />

<!--
	<link rel="preload" href="~/Content/jquery-ui.min.css" as="style" />
	<link href="~/Content/jquery-ui.css" rel="stylesheet" type="text/css" media="all" />
	<link rel="preload" href="~/Content/jquery-ui.css" as="style" />
	<link href="~/Content/jquery-ui.min.css" rel="stylesheet" type="text/css" media="all" />
	<link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css"/>
-->
<!--	<script src="~/Scripts/jquery-1.9.1.js" type="text/javascript"></script>
	<script src="~/Scripts/jquery-ui.js" type="text/javascript"></script>
	<script src="~/Scripts/jquery.ui.core.js" type="text/javascript"></script>
	<script src="~/Scripts/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="~/Scripts/jquery.ui.menu.js" type="text/javascript"></script>
	<script src="~/Scripts/jquery.ui.position.js" type="text/javascript"></script>
	<script src="~/Scripts/jquery.ui.autocomplete.js" type="text/javascript"></script>
-->
	<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>

<!--	
    <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script>
	<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js" type="text/javascript"></script>
-->
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
	<script type="text/javascript" language="javascript">
	    function avisos1(objeto) {
			var largo;
			var objeto2;

			//alert(objeto);
		    if (objeto == "TxtUsuario") {
				largo = document.getElementById(objeto).value;

				document.getElementById("LblMensaje1").innerHTML = "Escriba Su Usuario";
				if (largo.length == 0) {
					document.getElementById("LblMensaje1").style.display = "";
					//alert("si");
				}
				else {
					document.getElementById("LblMensaje1").style.display = "none";
					//alert("no");
				}
			}
    		if (objeto == "TxtPwd") {
	    		largo = document.getElementById(objeto).value;

    			document.getElementById("LblMensaje2").innerHTML = "Escriba Su Contraseña";
	    		if (largo.length == 0) {
		    		document.getElementById("LblMensaje2").style.display = "";
			    	//alert("si");
    			}
	    		else {
		    		document.getElementById("LblMensaje2").style.display = "none";
			    	//alert("no");
    			}
	    	}

        }
	</script>
	<script type="text/javascript">
		$(document).ready(function () {
			SearchText();
		});
		function SearchText() {
			$(".autosuggest").autocomplete({
				source: function (request, response) {
					$.ajax({
						type: "POST",
						contentType: "application/json; charset=utf-8",
						url: "LogIn.aspx/AutoCompleta",
						data: "{'prefixText':'" + document.getElementById('TxtUsuario').value + "'}",
						dataType: "json",
						success: function (data) {
							response(data.d);
						},
						error: function (result) {
							alert("Error");
						}
					});
				}
			});
		}
	</script>
</head>
<body style="background-image: url(../Resources/fondo_ingreso.jpg); background-repeat:no-repeat; background-position:top">
    <p>
        <br />
    </p>

	<form id="form1" runat="server" autocomplete="false">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
					<asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
					<asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
					<asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
					<asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
					<asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
					<asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
					<asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
					<asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
					<asp:ScriptReference Path="~/js/foundation.min.js" />
					<asp:ScriptReference Path="~/js/vendor/custom.modernizr.js" />
					<%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=272931&clcid=0x409 --%>
					<%--Framework Scripts--%>

					<%--<asp:ScriptReference Name="MsAjaxBundle" /> --%>
					<%--<asp:ScriptReference Name="jquery" />--%>
					<%--<asp:ScriptReference Name="jquery.ui.combined" /> --%>
					<%--<asp:ScriptReference Name="WebFormsBundle" /> --%>
					<%--Site Scripts--%>
					<%--<asp:ScriptReference Path="~/js/gauge.coffee" />--%>

            </Scripts>

        </asp:ScriptManager>
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
                    <asp:TextBox ID="TxtUsuario" runat="server" Style="margin-bottom: 7px" Width="271px" Font-Names="Century Gothic" onblur="avisos1(this.id);" placeholder="Escriba Su Usuario" class="autosuggest"></asp:TextBox>

					<asp:Label ID="LblMensaje1" runat="server" Text="Seleccione Un Usuario" Font-Bold="True" Font-Names="Century Gothic" ForeColor="Red" Visible="False" Style="display: none;"></asp:Label>

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
                    <asp:TextBox ID="TxtPwd" runat="server" Width="271px" Password="True" Caption="Contraseña" MaxLength="15" Theme="iOS" Font-Names="Century Gothic" OnTextChanged="TxtPwd_TextChanged" TextMode="Password" ValidateRequestMode="Enabled" onfocus="this.select()" placeholder="Escriba Su Contraseña" onblur="avisos1(this.id);"></asp:TextBox>
                    <asp:Label ID="LblMensaje2" runat="server" Text="Escriba Una Contraseña" Font-Bold="True" Font-Names="Century Gothic" ForeColor="Red" Visible="True" Style="display: none;"></asp:Label>
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
        </div>
    </form> 

</body>
</html>
