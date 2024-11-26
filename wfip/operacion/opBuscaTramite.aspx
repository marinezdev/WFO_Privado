<%@ Page Title="" Language="C#" MasterPageFile="~/operacion/operacion.Master" AutoEventWireup="true" CodeBehind="opBuscaTramite.aspx.cs" Inherits="wfip.operacion.opBuscaTramite" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Descarga(url) {
            window.open(url, 'Download');
        }
        function Continuar() {
            cierraTodo();
            return true;
        }
        function cierraTodo() {
            popBitacora.Hide();
            PopInsumos.Hide();
            $("#dvBotones").hide();
            //pnlMsgProcesando.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>BUSCA TRAMITE</legend>
        <asp:Panel ID="pnlSeleccionaFlujo" runat="server" Width="100%" HorizontalAlign="Center" >
            <asp:Label ID="lbLstFlujos" runat="server" Text="FLUJO DE TRABAJO:" Font-Size="18px"></asp:Label>&nbsp;&nbsp; <asp:DropDownList ID="ddlLstFlujos" runat="server" Width="520px" AutoPostBack="True" Font-Size="18px" OnSelectedIndexChanged="ddlLstFlujos_SelectedIndexChanged"></asp:DropDownList>
        </asp:Panel>
        <table id="tblBuscar" style="width:100%">
            <tr>
                <td style="width:50%; vertical-align:top;">
                    <table id="tblDatosBusca" style="width:90%; margin:auto;">
                        <tr>
                            <td style="width:33%; text-align:left">
                                <asp:Label ID="lbFolioBuscar" runat="server" Text="Folio"></asp:Label>
                           </td>
                            <td style="width:33%; text-align:left">
                                <asp:TextBox ID="txFolioBuscar" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txFolioBuscar" runat="server" TargetControlID="txFolioBuscar" FilterType="Numbers" />
                            </td>
                            <td style="text-align:center;">
                                <asp:Button ID="btnBuscaFilio" runat="server" Text="Buscar" OnClick="btnBuscaFilio_Click" CssClass="boton" OnClientClick="return Continuar();" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                <asp:Label ID="lbNombreBuscar" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txNombreBuscar" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombreBuscar" runat="server" TargetControlID="txNombreBuscar" FilterType="Custom" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ123456789áéíóú" />
                            </td>
                            <td style="text-align:center;">
                                <asp:Button ID="btnBuscaNombre" runat="server" Text="Buscar" OnClick="btnBuscaNombre_Click" CssClass="boton" OnClientClick="return Continuar();" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align:center;">
                                <asp:Label ID="lbNoExiste" runat="server" Text="NO EXISTE" Font-Size="14px" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Panel ID ="pnlResultadoNombre" runat="server" Height ="200px" ScrollBars="Auto">
                        <asp:Repeater ID="rptResultadoNombre" runat="server" OnItemCommand="rptResultadoNombre_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblResultadoNombre" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Número de trámite</th>
                                        <th scope="col">Tipo de trámite</th>
                                        <th scope="col">Información del contratante</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td style="width: 100px"><%# Eval("IdTramite","{0:0000#}")%></td>
                                    <td style="width: 100px"><%# Eval("FlujoNombre")%> - <%# Eval("TramiteNombre")%></td>
                                    <td><%# Eval("DatosHtml")%></td>
                                    <td style="width: 40px; text-align:left; vertical-align:middle"><%# Eval("EstadoNombre")%></td>
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/foward.png" CommandName ="Consultar" CommandArgument='<%# Eval("IdTramite")%>' /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>                    
                    </asp:Panel>
                </td>
            </tr>
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
                <td>
                    <asp:Panel ID="pnlDatosContratante" runat="server" Width="100%">
                        <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                        <br />
                        <dx:ASPxButton ID="btnMuestraBitacora" runat="server" Text="Mostrar Bitacora" AutoPostBack="false" Width="180px"></dx:ASPxButton>
                        <dx:ASPxButton ID="btnMostrarInsumos" runat="server" Text="Mostrar Insumos" AutoPostBack="false" Width="180px"></dx:ASPxButton>
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
                                        <%--<asp:Label ID="lbMensaje" runat="server" Text="Label"></asp:Label>--%>
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
        HeaderText="BITACORA"
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
    <dx:ASPxPopupControl ID="PopInsumos" runat="server" 
        RenderIFrameForPopupElements ="True"
        LoadContentViaCallback="OnFirstShow" 
        CloseAction="CloseButton" 
        PopupVerticalAlign="Below" 
        PopupHorizontalAlign="RightSides"
        AllowDragging="true"
        ShowFooter="false"
        ShowHeader="true"
        HeaderText="INSUMOS"
        ClientInstanceName="PopInsumos"
        Width="350px"
        Height="150px"
        PopupElementID="btnMostrarInsumos" >
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <asp:Panel ID="pnInsumos" runat="server" Visible="true" ScrollBars="Auto">
                    <fieldset>
                        <legend>ARCHIVOS DE INSUMOS</legend>
                        <asp:Repeater ID="rptInsumos" runat="server" OnItemDataBound="rptInsumos_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblInsumos" style="width:100%" class="display">
                                    <thead>
                                        <th scope="col"></th>
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="color: #333333">
                                    <td style="text-align: center">
                                        <asp:ImageButton ID="ImgExp" runat="server" ImageUrl="~/img/download.png" />
                                    </td>
                                    <td><%# Eval("NmOriginal")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </fieldset>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <asp:HiddenField ID="hdIdTramite" runat="server" />
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
