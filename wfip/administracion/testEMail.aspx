<%@ Page Title="" Language="C#" MasterPageFile="~/wfip.Master" AutoEventWireup="true" CodeBehind="testEMail.aspx.cs" Inherits="wfip.administracion.testEMail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <table>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Servidor"></asp:Label></td>
            <td><asp:TextBox ID="TextBox1" runat="server" Width="600px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Puerto"></asp:Label></td>
            <td><asp:TextBox ID="TextBox2" runat="server" Width="600px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Usuario"></asp:Label></td>
            <td><asp:TextBox ID="TextBox3" runat="server" Width="600px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="Contraseña"></asp:Label></td>
            <td><asp:TextBox ID="TextBox4" runat="server" Width="600px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="Destinatario"></asp:Label></td>
            <td><asp:TextBox ID="TextBox5" runat="server" Width="600px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label6" runat="server" Text="Asunto"></asp:Label></td>
            <td><asp:TextBox ID="TextBox6" runat="server" Width="600px"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label7" runat="server" Text="Contenido"></asp:Label></td>
            <td><asp:TextBox ID="TextBox7" runat="server" Width="600px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <!--<asp:Button ID="Button1" runat="server" Text="Enviar Correo de Prueba" OnClick="Button1_Click" />-->
                <asp:Button ID="Button2" runat="server" Text="Enviar Correo de Prueba" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>
    <br />&nbsp;
    <br />&nbsp;
</asp:Content>
