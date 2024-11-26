<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="pmTramiteEjctd.aspx.cs" Inherits="wfip.promotoria.pmTramiteEjctd" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>TRÁMITES EJECUTADOS</legend>
        <div id="dvCajaMsgNoexiste" style="width:100%; text-align:center;">
            <asp:Label ID="lbNoExiste" runat="server" Text="NO EXISTE" Font-Size="14px" ForeColor="Red" Visible="False"></asp:Label>
        </div>
        <table id="tblBuscar" style="width:100%">
            <tr>
                <td style="vertical-align:top">
                    <asp:Panel ID="pnlDatosTramite" runat="server" Width="100%">
                        <table id="tblDatosTramite" style="width:90%; margin:auto;">
                            <tr>
                                <td style="width:35%" >Folio</td>
                                <td><asp:Label ID="lbFolio" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Fecha de registro</td>
                                <td><asp:Label ID="lbFechaRegistro" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Flujo</td>
                                <td><asp:Label ID="lbFlujoNomnre" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Trámite</td>
                                <td><asp:Label ID="lbTramiteNombre" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="vertical-align:top;">
                    <asp:Panel ID="pnlDatosContratante" runat="server" Width="100%">
                        <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                        <br />
                        <dx:ASPxButton ID="btnMuestraBitacora" runat="server" Text="Mostrar Bitácora" AutoPostBack="false" Width="100px" RenderMode="Link"></dx:ASPxButton>
                        <dx:ASPxButton ID="btnImprimeCartaAceptacion" runat="server" Text="Carta aceptación" AutoPostBack="true" Width="120px" OnClick="btnImprimeCartaAceptacion_Click" RenderMode="Link"></dx:ASPxButton>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table id="tblDoctos" style="width:100%;">
                                <tr>
                                    <td>
                                        <div id="EspacioPDF" style="width:100%; height:550px; vertical-align:top" tabindex="0" >
                                            <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </fieldset>
    <dx:ASPxPopupControl ID="popBitacora" runat="server" 
        RenderIFrameForPopupElements ="True"
        LoadContentViaCallback="OnFirstShow" 
        CloseAction="CloseButton" 
        PopupVerticalAlign="Below" 
        PopupHorizontalAlign="LeftSides"
        AllowDragging="true"
        ShowFooter="false"
        ShowHeader="true"
        HeaderText="BITÁCORA"
        ClientInstanceName="popBitacora"
        Width="500px"
        Height="400px"
        PopupElementID="btnMuestraBitacora" >
        <ContentCollection>
            <dx:PopupControlContentControl ID="popupContenedorBitacora" runat="server">
                <asp:Literal ID="ltBitacora" runat="server"></asp:Literal>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <!-- <dx:ASPxPopupControl ID="popCartaAceptado" runat="server" 
        RenderIFrameForPopupElements ="True"
        LoadContentViaCallback="OnFirstShow" 
        CloseAction="CloseButton" 
        PopupVerticalAlign="WindowCenter" 
        PopupHorizontalAlign="WindowCenter"
        AllowDragging="true"
        ShowFooter="false"
        ShowHeader="true"
        HeaderText="CARTA DE ACEPTADO"
        ClientInstanceName="popCartaAceptado"
        Width="500px"
        Height="500px"
        PopupElementID="btnImprimeCartaAceptacion" Theme="Aqua" >
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupCarta" runat="server">
                <asp:Literal ID="ltCarta" runat="server"></asp:Literal>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>-->
    <asp:HiddenField ID="hdIdTramite" runat="server" />
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
