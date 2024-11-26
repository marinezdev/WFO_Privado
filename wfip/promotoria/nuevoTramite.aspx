<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="nuevoTramite.aspx.cs" Inherits="wfip.promotoria.nuevoTramite" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
    <br />
    <div style="width: 100%">
        <div style="width: 60%; margin: auto;">
            <div style="width: 100%; text-align:center;">
                <asp:Label ID="lbTipoProducto" runat="server" Text="VidaGmm"></asp:Label>
            </div>
            <asp:Panel ID="pnlOpcionesTramite" runat="server" Width="100%" CssClass="dropShadowPanel">
                <asp:Panel ID="pnlEmisionServicio" runat="server" Visible="false">
                    <div style="width: 100%">
                        ¿Que tipo de trámite deseas realizar?
                            <hr />
                        <table id="tblEmisionServicio" style="width: 100%; text-align: center">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Button ID="btnEmision" runat="server" Text="Emision" CssClass="capturaTipo" CommandName="emision" OnCommand="BtnEmisionServicio_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnServicio" runat="server" Text="Servicios" CssClass="capturaTipo" CommandName="servicio" OnCommand="BtnEmisionServicio_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlVidaGastosMedicos" runat="server" Visible="false">
                    <div style="width: 100%">
                        <hr />
                        <table id="tblVidaGmm" style="width: 100%; text-align: center">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Button ID="BtnVida" runat="server" Text="Vida" CssClass="capturaTipo" CommandName="vida" OnCommand="BtnVidaGmm_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnGmm" runat="server" Text="Gmm" CssClass="capturaTipo" CommandName="gmm" OnCommand="BtnVidaGmm_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEmisionNuevoRenovacion" runat="server" Style="padding: 20px" Visible="false">
                    <div style="width: 50%; margin: auto; text-align: center">
                        ¿Que trámite deseas realizar?
                            <hr />
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo negocio" CssClass="boton" OnClick="btnNuevo_Click" />
                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnRenovacion" runat="server" Text="Renovación" CssClass="boton" OnClick="BtnRenovacion_Click" />
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
    <ajaxToolkit:DropShadowExtender ID="DropShadowExtender1" runat="server" TargetControlID="pnlOpcionesTramite" Rounded="true" Radius="6" />
    <br />
    <asp:HiddenField ID="hfTipoTramite" runat="server" />
    <asp:HiddenField ID="hfProducto" runat="server" />
    <asp:HiddenField ID="hfSubProducto" runat="server" />
</asp:Content>
