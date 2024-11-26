<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFC.aspx.cs" Inherits="wfip.RFC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Implementación de RFC</h2>
            <table>
                <tr><td>Seleccione el tipo de RFC:</td>
                    <td><asp:RadioButtonList ID="rblTipo" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblTipo_SelectedIndexChanged">
                        <asp:ListItem Value="1">Persona</asp:ListItem>
                        <asp:ListItem Value="2">Empresa</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="rfcPersona1" runat="server" visible="false"><td>Nombres:</td><td><asp:TextBox ID="txtNombres" runat="server"></asp:TextBox></td></tr>
                <tr id="rfcPersona2" runat="server" visible="false"><td>Apellido Paterno:</td><td><asp:TextBox ID="txtAPaterno" runat="server"></asp:TextBox></td></tr>
                <tr id="rfcPersona3" runat="server" visible="false"><td>Apellido Materno:</td><td><asp:TextBox ID="txtAMaterno" runat="server"></asp:TextBox> </td></tr>
                <tr id="rfcPersona4" runat="server" visible="false"><td>Fecha de Nacimiento:</td><td><asp:TextBox ID="txtDia" runat="server" Width="28px"></asp:TextBox>Mes:<asp:TextBox ID="txtMes" runat="server" Width="28px"></asp:TextBox>Año:<asp:TextBox ID="txtAnn" runat="server" Width="50px"></asp:TextBox></td></tr>
                <tr id="rfcPersona5" runat="server" visible="false"><td colspan="2" align="center"><asp:Button ID="btnAceptar" runat="server" Text="Obtener" OnClick="btnAceptar_Click" /></td></tr>
                <tr id="rfcEmpresa1" runat="server" visible="false"><td>Nombre legal de la empresa:</td><td><asp:TextBox ID="txtEmpresa" runat="server"></asp:TextBox></td></tr>
                <tr id="rfcEmpresa2" runat="server" visible="false"><td>Fecha de creación:</td><td>Día:<asp:TextBox ID="txtEDia" runat="server" Width="28px" ></asp:TextBox>Mes:<asp:TextBox ID="txtEMes" runat="server" Width="28px"></asp:TextBox>año:<asp:TextBox ID="txtEAnn" runat="server" Width="50px"></asp:TextBox> </td></tr>
                <tr id="rfcEmpresa3" runat="server" visible="false"><td colspan="2" align="center"><asp:Button ID="btnaceptarE" runat="server" Text="Obtener" OnClick="btnaceptarE_Click" /></td></tr>
            </table>
             <br />
             <br />
             <br />
             <br />
            <br />
            <asp:Label ID="lblObtenido" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
