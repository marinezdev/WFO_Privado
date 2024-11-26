<%@ Page Title="" Language="C#" MasterPageFile="~/MesaAyuda/MesaAyuda.Master" AutoEventWireup="true" CodeBehind="buscaTramite.aspx.cs" Inherits="wfip.MesaAyuda.buscaTramite" EnableEventValidation="false" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#EspacioPDF').hide();
        });
        function Descarga(url) { window.open(url, 'Download'); }
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
        <asp:Panel ID="pnlSeleccionaFlujo" runat="server" Width="100%" HorizontalAlign="Center">
            <asp:Label ID="lbLstFlujos" runat="server" Text="FLUJO DE TRABAJO:" Font-Size="18px"></asp:Label>&nbsp;&nbsp;
            <asp:DropDownList ID="ddlLstFlujos" runat="server" Width="520px" AutoPostBack="True" Font-Size="18px" OnSelectedIndexChanged="ddlLstFlujos_SelectedIndexChanged"></asp:DropDownList>
        </asp:Panel>
        <table id="tblBuscar" style="width: 100%">
            <tr>
                <td colspan="2">
                    <div id="dvCajaTblLogin" style="border: 1px solid #999999; width: 50%; margin: auto; background-color: #F2F2F2; border-radius: 8px 8px 8px 8px;">
                        <table id="tblDatosBusca" style="width: 100%; margin: auto;">
                            <tr>
                                <td style="width: 20%; text-align: left">
                                    <asp:Label ID="lbFolioBuscar" runat="server" Text="Folio"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txFolioBuscar" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txFolioBuscar" runat="server" TargetControlID="txFolioBuscar" FilterType="Numbers" />
                                </td>
                                <td style="width: 20%; text-align: center;">
                                    <asp:Button ID="btnBuscaFilio" runat="server" Text="Buscar" OnClick="btnBuscaFilio_Click" CssClass="boton" OnClientClick="return Continuar();" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:Label ID="lbNombreBuscar" runat="server" Text="Nombre"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txNombreBuscar" runat="server" Width="320px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombreBuscar" runat="server" TargetControlID="txNombreBuscar" FilterType="Custom" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ123456789áéíóú" />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Button ID="btnBuscaNombre" runat="server" Text="Buscar" OnClick="btnBuscaNombre_Click" CssClass="boton" OnClientClick="return Continuar();" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <asp:Label ID="lbNoExiste" runat="server" Text="NO EXISTE" Font-Size="14px" ForeColor="Red" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlResultadoNombre" runat="server">
                        <hr />
                        <asp:Repeater ID="rptResultadoNombre" runat="server" OnItemCommand="rptResultadoNombre_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblResultadoNombre" style="width: 90%; margin: auto;" class="display">
                                    <thead>
                                        <th scope="col">Trámite</th>
                                        <th scope="col">Tipo de trámite</th>
                                        <th scope="col">Información del contratante</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td style="width: 100px"><%# Eval("Id","{0:0000#}")%></td>
                                    <td style="width: 100px"><%# Eval("Flujo")%> - <%# Eval("TramiteNombre")%></td>
                                    <td><%# Eval("DatosHtml")%></td>
                                    <td style="width: 40px; text-align: left; vertical-align: middle"><%# Eval("EstadoNombre")%></td>
                                    <td style="width: 20px; text-align: center">
                                        <asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/foward.png" CommandName="Consultar" CommandArgument='<%# Eval("Id")%>' /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <hr />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; vertical-align: top">
                    <asp:Panel ID="pnlDatosTramite" runat="server" Width="90%" Style="margin: auto; border: 1px solid #999999; border-radius: 8px 8px 8px 8px;">
                        <table id="tblDatosTramite" style="width: 90%; margin: auto;">
                            <tr>
                                <td style="width: 35%">Folio</td>
                                <td>
                                    <asp:Label ID="lbFolio" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Fecha de registro</td>
                                <td>
                                    <asp:Label ID="lbFechaRegistro" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Flujo</td>
                                <td>
                                    <asp:Label ID="lbFlujoNomnre" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Trámite</td>
                                <td>
                                    <asp:Label ID="lbTramiteNombre" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    <asp:Panel ID="pnlDatosContratante" runat="server" Width="100%">
                        <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                        <br />
                        <dx:ASPxButton ID="btnMuestraBitacora" runat="server" Text="Mostrar Bitacora" AutoPostBack="false" Width="180px" RenderMode="Link" ClientVisible="False"></dx:ASPxButton>
                        <dx:ASPxButton ID="btnMostrarInsumos" runat="server" Text="Mostrar Insumos" AutoPostBack="false" Width="180px" RenderMode="Link"></dx:ASPxButton>
                        <asp:LinkButton ID="lnkOcultaDocumento" runat="server" CausesValidation="False" Font-Size="12px" OnClientClick="$('#EspacioPDF').toggle(); return false;">Mostrar/Ocultar Documento</asp:LinkButton>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlDatosBitacora" runat="server" Width="100%">
                        <asp:Repeater ID="RptDatosBitacora" runat="server" OnItemDataBound="RptDatosBitacora_ItemDataBound">
                            <HeaderTemplate>
                                <table id="TblDatosBitacora" style="width: 90%; margin: auto; border-color:silver; border-style:solid; border-width:1px; font-size:10px;">
                                    <thead style="background-color:#a6baf3">
                                        <th scope="col">MESA</th>
                                        <th scope="col">INICIA</th>
                                        <th scope="col">TERMINA</th>
                                        <th scope="col">TIEMPO</th>
                                        <th scope="col"></th>
                                        <th scope="col">USUARIO</th>
                                        <th scope="col">OBS</th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("MesaNombre")%></td>
                                    <td><%# Eval("FechaInicio","{0:dd/MM/yy H:mm:ss}")%></td>
                                    <td><%# Eval("FechaTermino","{0:dd/MM/yy H:mm:ss}")%></td>
                                    <td><%# Eval("Tiempo")%></td>
                                    <td style="width: 20px; text-align: center">
                                        <asp:Image ID="imgEstado" runat="server" ImageUrl="~/img/estrellaVerde.png" />
                                    </td>
                                    <td><%# Eval("UsuarioNombre")%></td>
                                    <td><%# Eval("ObservacionPrivada")%></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #e0e0e0; color: #333333">
                                    <td><%# Eval("MesaNombre")%></td>
                                    <td><%# Eval("FechaInicio","{0:dd/MM/yy H:mm:ss}")%></td>
                                    <td><%# Eval("FechaTermino","{0:dd/MM/yy H:mm:ss}")%></td>
                                    <td><%# Eval("Tiempo")%></td>
                                    <td style="width: 20px; text-align: center">
                                        <asp:Image ID="imgEstado" runat="server" ImageUrl="~/img/estrellaVerde.png" />
                                    </td>
                                    <td><%# Eval("UsuarioNombre")%></td>
                                    <td><%# Eval("ObservacionPrivada")%></td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="EspacioPDF" style="width: 100%; height: 550px; vertical-align: top" tabindex="0">
                        <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <dx:ASPxPopupControl ID="popBitacora" runat="server"
        RenderIFrameForPopupElements="True"
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
        PopupElementID="btnMuestraBitacora">
        <ContentCollection>
            <dx:PopupControlContentControl ID="popupContenedorBitacora" runat="server">
                <asp:Literal ID="ltBitacora" runat="server"></asp:Literal>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="PopInsumos" runat="server"
        RenderIFrameForPopupElements="True"
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
        PopupElementID="btnMostrarInsumos">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <asp:Panel ID="pnInsumos" runat="server" Visible="true" ScrollBars="Auto">
                    <fieldset>
                        <legend>ARCHIVOS DE INSUMOS</legend>
                        <asp:Repeater ID="rptInsumos" runat="server" OnItemDataBound="rptInsumos_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblInsumos" style="width: 100%" class="display">
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
