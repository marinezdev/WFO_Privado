﻿<%@ Page Title="" Language="C#" MasterPageFile="~/operacion/operacion.Master" AutoEventWireup="true" CodeBehind="esperaOperacion.aspx.cs" Inherits="wfip.operacion.esperaOperacion" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function valida() {
            $("#dvBotones").hide();
            pnlMsgProcesando.Show();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <div style="width:100%">
        <div style="width: 50%; margin:auto;">
            <asp:Panel ID="pnlMesas" runat="server" Width="100%" CssClass="dropShadowPanel" >
                <div style="padding: 20px; text-align: right;">
                    <asp:Label ID="lblMessageBySystem" runat="server" Font-Bold="false" ForeColor="Yellow" Text ="No asignado" Font-Size="Large"> </asp:Label>
                </div>
                <div style="padding: 20px">
                    <div style="width:50%; margin:auto; text-align:center">
                        Selecciona la Mesa para Gestionar
                        <hr />
                        <asp:Repeater ID="rpt_Mesas" runat="server" OnItemCommand="rpt_Mesas_ItemCommand">
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                            <HeaderTemplate>
                                    <table id="tblMesas" style="width:100%; font-size:12px" >
                                        <tbody>
                                </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="height:45px">
                                        <asp:Button ID="btnMesa" runat="server" Text='<%# Eval("Nombre")%>' CssClass="boton" Width="80%" CommandArgument='<%# Eval("Id")%>' CommandName="abreMesa" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <ajaxToolkit:DropShadowExtender ID="DropShadowExtender2" runat="server" TargetControlID="pnlMesas" Rounded="true" Radius="6" />
    <dx:ASPxLoadingPanel ID="pnlMsgProcesando" runat="server" ClientInstanceName="pnlMsgProcesando" Modal="true" Text="Procesando...">
    </dx:ASPxLoadingPanel>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>