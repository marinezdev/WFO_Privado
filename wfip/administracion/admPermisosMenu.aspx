<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admPermisosMenu.aspx.cs" Inherits="wfip.administracion.admPermisosMenu" MasterPageFile="~/administracion/adminsysMaster.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="upGeneral" runat="server">
        <ContentTemplate>

            <fieldset>
                <legend>ADMINISTRACION DE MENU</legend>
                <table id="tblBtns" style="width: 100%">
                    <tr>
                        <td style="text-align: right;">
                            <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="btnCerrar_Click" />
                        </td>
                    </tr>
                </table>
                <div id="dvCajaCapturaDatos" style="width: 100%">
                    <table border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                        <tr>
                            <td >Roles:</td>
                            <td>
                                <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <tr><td colspan="2">
                                <asp:Label ID="lblMensajePermisosMenu" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tr>
                    </table>
                    <table id="tablaPermisosMenu" runat="server" visible="false" border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                        <tr><td colspan="2">Accesos al Menú por Rol seleccionado:</td></tr>
                        <tr>
                            <td>                           

                                <asp:TreeView ID="tvwMenu" runat="server" ShowLines="true" ShowCheckBoxes="All" OnTreeNodeDataBound="tvwMenu_TreeNodeDataBound" OnTreeNodePopulate="tvwMenu_TreeNodePopulate">
                                    <HoverNodeStyle BackColor="#0099CC" Font-Bold="True" />
                                    <SelectedNodeStyle BackColor="#009933" />
                                </asp:TreeView>

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblVerMenu" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr><td colspan="2" align="center"><asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="boton" OnClick="btnAceptar_Click"/></td></tr>
                    </table>
                </div>
                <br />
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
