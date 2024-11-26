<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="wfip.Mtto.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Mantenimiento</title>
    <link href="Estilos.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center><h1>Mantenimiento del Sistema</h1></center>

            <table align="center">
                <tr><td>Clave:</td><td><asp:TextBox ID="txtClave" runat="server"></asp:TextBox></td></tr>
                <tr><td>Contraseña:</td><td><asp:TextBox ID="txtContra" runat="server" TextMode="Password"></asp:TextBox></td></tr>
                <tr><td colspan="2" align="center"><asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" OnClick="BtnAceptar_Click" /></td></tr>
                <tr><td colspan="2" align="center"><asp:Label ID="lblMensajes" runat="server"></asp:Label></td></tr>
            </table>
        </div>
    </form>
</body>
</html>
