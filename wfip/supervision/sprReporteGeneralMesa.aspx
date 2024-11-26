<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralMesa.aspx.cs" Inherits="wfip.supervision.sprReporteGeneralMesa" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        function onShowPopup(x, y, text) {
            texto=''
            switch (text) {
                case 'EN ATENCIÓN':
                    texto='MESAS CON TRÁMITES EN REVISIÓN HOLD,REINGRESO HOLD,SOLICITUD APOYO, REINGRESO APOYO,PAUSA,REVISIÓN SUSPENSIÓN,REINGRESO SUSPENSIÓN, ATENCIÓN'
                    break;
                case 'SUSPENDIDOS':
                    texto='MESAS CON TRÁMITES EN SUSPENSIÓN'
                    break;
                case 'HOLD':
                    texto='MESAS CON TRÁMITES EN HOLD'
                    break;
                case 'RECHAZADOS':
                    texto='MESAS CON TRÁMITES RECHAZADOS'
                    break;
                case 'PROCESADOS':
                    texto='MESAS CON TRÁMITES EN PROCESO '
                    break;
            }
            lbl.SetText(texto);
            popup.ShowAtPos(x, y);
        }
    </script>
    <fieldset>
        <legend> MESA </legend>
        <br /> 
            <table style ="width:60%">
                <tr>
                 <td>
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"   Width="190" Caption="Desde:">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td>
                    <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td>
                    <dx:ASPxComboBox ID="ListaFlujo" runat="server" ClientInstanceName="ListaFlujoComboBox" EnableClientSideAPI="True" Theme="iOS" Width="300"
                        ValueType="System.String" Caption="Flujo de trabajo">
                        <Items>
                        </Items>                        
                    </dx:ASPxComboBox>
                </td>
                <td>
                    <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Filtrar" OnClick="BtnConsultar_Click" />
                </td>
                </tr>
            </table>
            <table style="width:100%">
                <tr>
                  <td style="text-align:right">
                     <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                        <img src="../img/excel.png"/>
                     </asp:LinkButton>
                 </td>
                </tr>
                <tr>
                 <td>
                    <asp:UpdateProgress ID="updProgress"  runat="server">
                        <ProgressTemplate>           
                         <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
                            <div style="padding: 10px;position:fixed;top:45%;left:50%;background-color:white;width:170px;height:45px " > 
                             <table style="background-size:0">
                                <tr>
                                 <td>
                                     <img alt=" " src="/img/spinner.gif" />
                                 </td>
                                <td>
                                    <span style="font-size:16px">&nbsp;Procesando...</span>
                                </td>
                                </tr>
                             </table>
                           </div> 
                         </div>
                        
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                 </td>
            </tr>
            <tr>
            <td>
            <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                 <div id="dvCajaGrilla">
                <dx:ASPxGridView  ID="dvgdMesa" ClientInstanceName="dvgdMesa" runat="server" AutoGenerateColumns="False" Width="100%" EnableTheming="True" Theme="iOS" Font-Size="10px" OnHtmlDataCellPrepared="dvgdMesa_HtmlDataCellPrepared">
                    <Styles Header-Wrap="True" />   
                    <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Settings HorizontalScrollBarMode="Auto" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdMesa"></dx:ASPxGridViewExporter>
                <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="popup" ShowHeader="false">
                   <ContentCollection>
                       <dx:PopupControlContentControl runat="server">
                          <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ASPxLabel" ClientInstanceName="lbl" Width="350">
                          </dx:ASPxLabel>
                       </dx:PopupControlContentControl>
                   </ContentCollection>
                 </dx:ASPxPopupControl>
                <br />
                </div>
                <div id="dvCajaGrafica" style="width: 1000px; margin: auto;">
                    <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="1000px" Theme="SoftOrange">
                        <Titles>
                            <dx:ChartTitle Font="Arial,12pt" Text="GENERAL POR MESA" />
                        </Titles>
                        <DiagramSerializable>
                            <dx:XYDiagram>
                                <AxisX VisibleInPanesSerializable="-1">
                                </AxisX>
                                <AxisY VisibleInPanesSerializable="-1">
                                </AxisY>
                            </dx:XYDiagram>
                        </DiagramSerializable>
                        <Legend Name="Default Legend"></Legend>
                        <%--                <SeriesSerializable>
                            <dx:Series Name="dxSreTotales"></dx:Series>
                        </SeriesSerializable>--%>
                    </dx:WebChartControl>
                </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
    </td>
    </tr>
    </table>
    </fieldset>
</asp:Content>