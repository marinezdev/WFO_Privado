<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsPendientesImss.aspx.cs" Inherits="wfip.promotoria.PendientesImss" MasterPageFile="~/promotoria/promotoria.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>Movimientos Ingresados por Quincena<asp:Label ID="Label2" runat="server"></asp:Label></legend>
        <div style="width: 90%; margin: auto">
                        <table style="width:100%; margin:auto;">
                <tr><td></td><td></td><td>Fecha de Registro:<%=DateTime.Now %></td><td>&nbsp;</td></tr>
                <tr><td></td><td></td><td align="Right">Buscar:</td><td align="Left"><asp:TextBox ID="txtBuscar" runat="server"></asp:TextBox></td></tr>
                <tr>
                    <td style="font-weight: bold">Año/Quincena:</td>
                    <td>
                        <asp:DropDownList ID="ddlAnnQuincena" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAnnQuincena_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                            <asp:ListItem Value="1" Text="2018/01"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2018/02"></asp:ListItem>
                            <asp:ListItem Value="3" Text="2018/03"></asp:ListItem>
                            <asp:ListItem Value="4" Text="2018/04"></asp:ListItem>
                            <asp:ListItem Value="5" Text="2018/05"></asp:ListItem>
                            <asp:ListItem Value="6" Text="2018/06"></asp:ListItem>
                            <asp:ListItem Value="7" Text="2018/07"></asp:ListItem>
                            <asp:ListItem Value="8" Text="2018/08"></asp:ListItem>
                            <asp:ListItem Value="9" Text="2018/09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="2018/10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="2018/11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="2018/12"></asp:ListItem>
                            <asp:ListItem Value="13" Text="2018/13"></asp:ListItem>
                            <asp:ListItem Value="14" Text="2018/14"></asp:ListItem>
                            <asp:ListItem Value="15" Text="2018/15"></asp:ListItem>
                            <asp:ListItem Value="16" Text="2018/16"></asp:ListItem>
                            <asp:ListItem Value="17" Text="2018/17"></asp:ListItem>
                            <asp:ListItem Value="18" Text="2018/18"></asp:ListItem>
                            <asp:ListItem Value="19" Text="2018/19"></asp:ListItem>
                            <asp:ListItem Value="20" Text="2018/20"></asp:ListItem>
                            <asp:ListItem Value="21" Text="2018/21"></asp:ListItem>
                            <asp:ListItem Value="22" Text="2018/22"></asp:ListItem>
                            <asp:ListItem Value="23" Text="2018/23"></asp:ListItem>
                            <asp:ListItem Value="24" Text="2018/24"></asp:ListItem>
                        </asp:DropDownList>
                    </td><td></td><td>&nbsp;</td></tr>
                </table>

            <asp:GridView ID="GVDetalle" runat="server" CellPadding="2" CellSpacing="2">

            </asp:GridView>

        </div>
    </fieldset>
</asp:Content>