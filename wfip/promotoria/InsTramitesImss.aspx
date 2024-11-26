<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsTramitesImss.aspx.cs" Inherits="wfip.promotoria.TramitesImss" MasterPageFile="~/promotoria/promotoria.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>IMSS Portal<asp:Label ID="Label2" runat="server"></asp:Label></legend>
        <div style="width: 90%; margin: auto">
            <table style="width:100%; margin:auto;">
                <tr><td></td><td></td><td>Fecha de Registro:<%=DateTime.Now %></td></tr>
                <tr style="font-weight: bold"><td>Tipo de Nómina</td><td>Tipo de Movimiento</td><td></td></tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rblTipoNomina" runat="server">
                            <asp:ListItem Text="Activos" Value="activos"></asp:ListItem>
                            <asp:ListItem Text="Estatuto A" Value="estatutoa"></asp:ListItem>
                            <asp:ListItem Text="Jubilados" Value="jubilados"></asp:ListItem>
                            <asp:ListItem Text="Mandos" Value="mandos"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td valign="top">
                        <asp:RadioButtonList ID="rblTipoMovimiento" runat="server">
                            <asp:ListItem Text="Alta" Value="Alta"></asp:ListItem>
                            <asp:ListItem Text="Modificación" Value="Modificación"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td></td>
                </tr>
                <tr><td colspan="3" align="center"><asp:Button ID="BtnContinuar" runat="server" Text="Aceptar" CssClass="boton" OnClick="BtnContinuar_Click" /></td></tr>
                <tr><td></td><td></td><td></td></tr>
            </table>
        </div>
    </fieldset>
</asp:Content>

