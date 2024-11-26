<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admRoles.aspx.cs" Inherits="wfip.administracion.admRoles" MasterPageFile="~/administracion/adminsysMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>ADMINISTRACION DE ROLES</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="btnCerrar_Click" />
                </td>
            </tr>
        </table>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                <tr><td colspan="2">

                    <asp:RadioButtonList ID="rblOpciones" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblOpciones_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="1" Text="Agregar nuevo Rol"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Editar Rol"></asp:ListItem>
                        <%--<asp:ListItem Value="3" Text="Asociar un menú a un Rol"></asp:ListItem>--%>
                    </asp:RadioButtonList>

                    </td>
                </tr>
                <tr id="nuevo" runat="server" visible="false">
                    <td>Nombre del Rol:</td>
                    <td><asp:TextBox ID="txtNombre" runat="server"></asp:TextBox></td>
                </tr>
                <tr id="rol" runat="server" visible="false">
                    <td>Seleccione Rol:</td>
                    <td>
                        <asp:DropDownList ID="ddlRoles" runat="server" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="nombreEditado" runat="server" visible="false">
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="txtNombreEditado" runat="server" Visible="false"></asp:TextBox></td>
                </tr>
                <tr id="menuAsociado" runat="server" visible="false">
                    <td>Menu:</td>
                    <td>

                        <asp:DropDownList ID="ddlMenu" runat="server" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            <asp:ListItem Value="99">Administración</asp:ListItem>
                            <asp:ListItem Value="1">Promotoría</asp:ListItem>
                            <asp:ListItem Value="3">Operación</asp:ListItem>
                            <asp:ListItem Value="5">Mesa Ayuda</asp:ListItem>
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlSub" runat="server" Visible="false">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            <asp:ListItem Value="10">Operador</asp:ListItem>
                            <asp:ListItem Value="20">SuperVisor</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td>
                    </td>
                </tr>
                <tr><td colspan="2" align="center"><asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="boton" OnClick="btnAceptar_Click" Visible="false"/></td></tr>
                </table>
        </div>
        <br />
    </fieldset>



</asp:Content>