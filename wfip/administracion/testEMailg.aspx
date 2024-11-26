<%@ Page Title="" Language="C#" MasterPageFile="~/wfip.Master" AutoEventWireup="true" CodeBehind="testEMailg.aspx.cs" Inherits="wfip.administracion.testEMailg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:TextBox ID="TextBox1" runat="server" Text="rmf100382@hotmail.com"></asp:TextBox>
    <br />&nbsp;
    <br />&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Enviar Correo de Prueba" OnClick="Button1_Click" />
    <br />&nbsp;
    <br />&nbsp;
</asp:Content>
