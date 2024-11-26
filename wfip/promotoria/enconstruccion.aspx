<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="enconstruccion.aspx.cs" Inherits="wfip.promotoria.enconstruccion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <div style="width:100%; text-align:center;">
        <img alt="" src="../img/enconstruccion.jpg" />
    </div>
    <div  style="width:100%; text-align:center;"">
        <asp:Button ID="brnCerrar" runat="server" Text="Cerrar" CssClass="boton" OnClick="brnCerrar_Click" />
    </div>
</asp:Content>
