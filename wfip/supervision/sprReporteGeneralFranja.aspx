<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralFranja.aspx.cs" Inherits="wfip.supervision.sprReporteGeneralFranja" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        function onShowPopup(x, y, text) {
            texto = ''
            switch (text) {
                case 'EN ATENCIÓN':
                    texto = 'MESAS CON TRÁMITES EN REVISIÓN HOLD,REINGRESO HOLD,SOLICITUD APOYO, REINGRESO APOYO,PAUSA,REVISIÓN SUSPENSIÓN,REINGRESO SUSPENSIÓN, ATENCIÓN'
                    break;
                case 'SUSPENDIDOS':
                    texto = 'MESAS CON TRÁMITES EN SUSPENSIÓN'
                    break;
                case 'HOLD':
                    texto = 'MESAS CON TRÁMITES EN HOLD'
                    break;
                case 'RECHAZADOS':
                    texto = 'MESAS CON TRÁMITES RECHAZADOS'
                    break;
                case 'PROCESADOS':
                    texto = 'MESAS CON TRÁMITES EN PROCESO '
                    break;
            }
            lbl.SetText(texto);
            popup.ShowAtPos(x, y);
        }
    </script>
    <fieldset>
        <legend>FRANJA</legend>
        <div>
            <!-- Reporte Franja -->
            <table style="width: 95%; margin-left: auto; margin-right: auto">
                <tr>
                    <td style="width: 2%; vertical-align: top">
                        <asp:HiddenField ID="hfCambio" runat="server" />
                        <dx:ASPxDateEdit ID="CalFranja" runat="server" Theme="iOS" EditFormat="Custom" Width="190" Caption="Fecha:">
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
                    <td style="width: 30%">
                        <asp:Button ID="btnFiltroFranja" runat="server" Text="Filtrar" CssClass="boton" OnClick="BtnCosnultarFranja_Click" />
                    </td>
                </tr>
            </table>
            <table style="width: 95%; margin-left: auto; margin-right: auto">
                <tr>
                    <td style="text-align: right; width: 100%">
                        <asp:LinkButton ID="lnkExportarFranja" runat="server" CausesValidation="False" OnClick="lnkExportarFranja_Click">
                 <img src="../img/excel.png"/>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="Franja" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 95%; margin-left: auto; margin-right: auto">
                        <tr>
                            <td style="vertical-align: top; width: 55%">
                                <dx:ASPxGridView ID="dvgdFranja" ClientInstanceName="dvgdFranja" runat="server" AutoGenerateColumns="False" Width="95%" EnableTheming="True" Theme="iOS" Font-Size="10px">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="FRANJA" FieldName="Franja" VisibleIndex="0" />
                                        <dx:GridViewDataTextColumn Caption="INGRESADOS" FieldName="ingresados" VisibleIndex="1" />
                                        <dx:GridViewDataTextColumn Caption="PENDIENTES DE ATENCIÓN" FieldName="tocados" VisibleIndex="2">
                                            <CellStyle Wrap="True"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="PROCESADOS" FieldName="ejecutados" VisibleIndex="3" />
                                    </Columns>
                                    <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <Styles Header-Wrap="True" />
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExportFranja" runat="server" GridViewID="dvgdFranja"></dx:ASPxGridViewExporter>
                                <br />
                                <br />
                                <label><strong>ACUMULADO MENSUAL:</strong></label>
                                <asp:Label ID="lblAcumulado" runat="server"></asp:Label>
                            </td>
                            <td style="width: 45%">
                                <dx:WebChartControl ID="dvchtFranja" runat="server" CrosshairEnabled="True" Height="300px" Width="500px" PaletteBaseColorNumber="5">
                                    <BorderOptions Visibility="False" />
                                    <Titles>
                                        <dx:ChartTitle Font="Arial,12pt" Text="FRANJA" TextColor="Tomato" />
                                    </Titles>
                                    <DiagramSerializable>
                                        <dx:XYDiagram>
                                            <AxisX VisibleInPanesSerializable="-1">
                                            </AxisX>
                                            <AxisY VisibleInPanesSerializable="-1">
                                            </AxisY>
                                            <DefaultPane BackColor="219, 229, 241" BorderColor="79, 97, 40">
                                            </DefaultPane>
                                        </dx:XYDiagram>
                                    </DiagramSerializable>
                                    <Legend Name="Default Legend" TextVisible="False" Visibility="False"></Legend>
                                </dx:WebChartControl>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <br />
                                <table style="border-collapse: collapse" border="1">
                                    <tr>
                                        <td>INGRESADOS
                             
                                        </td>
                                        <td>TRÁMITES REGISTRADOS
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PENDIENTES DE ATENCIÓN
                                        </td>
                                        <td>TRÁMITES EN ATENCIÓN,HOLD,REVISIÓN HOLD, REINGRESO HOLD,
                              SOLICITUD APOYO,REINGRESO APOYO,PAUSA,RECHAZADOS,SUSPENDIDOS,REVISIÓN SUSPENSIÓN,
                              REINGRESO SUSPENSIÓN
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PROCESADOS 
                                        </td>
                                        <td>TRÁMITES PROCESADOS
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltroFranja" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <br />
        </div>
        <!--Fin reporte Franja -->
        <!-- Reporte Trámites por semana-->
        <div>
            <br />
            <br />
            <table style="width: 95%; margin-left: auto; margin-right: auto">
                <tr>
                    <td>
                        <label style="font-weight: bold">TRÁMITES POR SEMANA </label>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <label>Año: &nbsp;</label>
                        <asp:DropDownList runat="server" ID="AnnioTS">
                        </asp:DropDownList>
                        &nbsp;&nbsp; 
            <label>Mes: &nbsp;</label>
                        <asp:DropDownList runat="server" ID="MesTS">
                            <asp:ListItem Text="Enero" Value="1" />
                            <asp:ListItem Text="Febrero" Value="2" />
                            <asp:ListItem Text="Marzo" Value="3" />
                            <asp:ListItem Text="Abril" Value="4" />
                            <asp:ListItem Text="Mayo" Value="5" />
                            <asp:ListItem Text="Junio" Value="6" />
                            <asp:ListItem Text="Julio" Value="7" />
                            <asp:ListItem Text="Agosto" Value="8" />
                            <asp:ListItem Text="Septiembre" Value="9" />
                            <asp:ListItem Text="Octubre" Value="10" />
                            <asp:ListItem Text="Noviembre" Value="11" />
                            <asp:ListItem Text="Diciembre" Value="12" />
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                    <asp:Button ID="btnFiltrarTS" CssClass="boton" runat="server" Text="Filtrar" OnClick="BtnCosnultarSemana_Click" />
                    </td>
                </tr>
            </table>
            <table style="width: 95%; margin-left: auto; margin-right: auto">
                <tr>
                    <td style="text-align: right">
                        <asp:LinkButton ID="lnkExportarTS" runat="server" CausesValidation="False" OnClick="lnkExportarTS_Click">
               <img src="../img/excel.png"/>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="DetalleReporteTS" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 95%; margin-left: auto; margin-right: auto">
                        <tr>
                            <td style="width: 50%">&nbsp;
             <dx:ASPxGridView ID="dvgdTramiteSemana" ClientInstanceName="dvgdTramiteSemana" runat="server" AutoGenerateColumns="False" Width="95%" Style="margin-top: 0px" EnableTheming="True" Theme="iOS" Font-Size="10px">
                 <Columns>
                     <dx:GridViewDataTextColumn Caption="SEMANA" FieldName="SEMANA" VisibleIndex="1" GroupIndex="0" />
                     <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA" VisibleIndex="2" />
                     <dx:GridViewDataTextColumn Caption="DIA" FieldName="DIA" VisibleIndex="3" />
                     <dx:GridViewDataTextColumn Caption="TRAMITES" FieldName="TRAMITES" VisibleIndex="4" />
                 </Columns>
                 <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True" ShowGroupPanel="True" />
                 <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                 <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                 <SettingsPager Mode="ShowAllRecords" />
             </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="grdExportTS" runat="server" GridViewID="dvgdTramiteSemana"></dx:ASPxGridViewExporter>
                            </td>
                            <td>
                                <dx:WebChartControl ID="dxChtTotalesTS" runat="server" CrosshairEnabled="True" Height="300px" Width="550px" Theme="SoftOrange">
                                    <Titles>
                                        <dx:ChartTitle Font="Arial,12pt" Text="TRÁMITES POR SEMANA" />
                                    </Titles>
                                    <DiagramSerializable>
                                        <dx:XYDiagram>
                                            <AxisX VisibleInPanesSerializable="-1">
                                            </AxisX>
                                            <AxisY VisibleInPanesSerializable="-1">
                                            </AxisY>
                                        </dx:XYDiagram>
                                    </DiagramSerializable>
                                    <Legend Name="Default Legend" Visibility="False"></Legend>
                                    <SeriesSerializable>
                                        <dx:Series Name="dxSreTotalesTS"></dx:Series>
                                    </SeriesSerializable>
                                </dx:WebChartControl>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltrarTS" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!-- Fin reporte Trámites por semana -->
        <!-- Reporte de Tendencia por año -->
        <div>
            <br />
            <br />

            <table style="width: 95%; margin-left: auto; margin-right: auto">
                <tr>
                    <td>
                        <label><strong>TENDENCIA POR AÑO</strong></label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:LinkButton ID="lnkExportarT" runat="server" CausesValidation="False" OnClick="lnkExportarTendencia_Click">
                            <img src="../img/excel.png"/>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>


                    <table style="width: 95%; margin-left: auto; margin-right: auto">
                <tr>
                    <td style="vertical-align: top; width: 100%">
                        <dx:ASPxGridView ID="dvgdTendencia" ClientInstanceName="dvgdTendencia" runat="server" AutoGenerateColumns="False" Width="100%" EnableTheming="True" Theme="iOS" Font-Size="10px">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="TIPO" FieldName="Tipo" VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption="AÑO" FieldName="Annio" VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption="ENERO" FieldName="Enero" VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption="FEBRERO" FieldName="Febrero" VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption="MARZO" FieldName="Marzo" VisibleIndex="5" />
                                <dx:GridViewDataTextColumn Caption="ABRIL" FieldName="Abril" VisibleIndex="6" />
                                <dx:GridViewDataTextColumn Caption="MAYO" FieldName="Mayo" VisibleIndex="7" />
                                <dx:GridViewDataTextColumn Caption="JUNIO" FieldName="Junio" VisibleIndex="8" />
                                <dx:GridViewDataTextColumn Caption="JULIO" FieldName="Julio" VisibleIndex="9" />
                                <dx:GridViewDataTextColumn Caption="AGOSTO" FieldName="Agosto" VisibleIndex="10" />
                                <dx:GridViewDataTextColumn Caption="SEPTIEMBRE" FieldName="Septiembre" VisibleIndex="11" />
                                <dx:GridViewDataTextColumn Caption="OCTUBRE" FieldName="Octubre" VisibleIndex="12" />
                                <dx:GridViewDataTextColumn Caption="NOVIEMBRE" FieldName="Noviembre" VisibleIndex="13" />
                                <dx:GridViewDataTextColumn Caption="DICIEMBRE" FieldName="Diciembre" VisibleIndex="14" />
                            </Columns>
                            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <Settings HorizontalScrollBarMode="Visible" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportT" runat="server" GridViewID="dvgdTendencia"></dx:ASPxGridViewExporter>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <dx:WebChartControl ID="dvchtTendencia" runat="server" CrosshairEnabled="True" Height="300px" Width="1100px" PaletteBaseColorNumber="5">
                                    <BorderOptions Visibility="False" />
                                    <Titles>
                                        <dx:ChartTitle Font="Arial,12pt" Text="TENDENCIA" TextColor="Tomato" />
                                    </Titles>
                                    <DiagramSerializable>
                                        <dx:XYDiagram>
                                            <AxisX VisibleInPanesSerializable="-1">
                                            </AxisX>
                                            <AxisY VisibleInPanesSerializable="-1">
                                            </AxisY>
                                            <DefaultPane BackColor="219, 229, 241" BorderColor="79, 97, 40">
                                            </DefaultPane>
                                        </dx:XYDiagram>
                                    </DiagramSerializable>
                                    <Legend Name="Default Legend" TextVisible="False" Visibility="False"></Legend>
                                </dx:WebChartControl>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnFiltroFranja" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <table style="border-collapse: collapse" border="1">
                            <tr>
                                <td>INGRESADOS</td>
                                <td>TRÁMITES REGISTRADOS (CONTABILIZADOS POR FECHA DE REGISTRO)</td>
                            </tr>
                            <tr>
                                <td>EN ATENCIÓN </td>
                                <td>TRÁMITES EN PROCESO,HOLD,EJECUCIÓN,RECHAZADOS,SUSPENDIDOS,PCI,
                                    CANCELADOS,ESPERA RESULTADOS,INFORMACIÓN CITA MÉDICA,REVISIÓN MÉDICA,REVISIÓN PROMOTORÍA
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            
        </div>
        <!-- Fin reporte de Tendencia por año -->
    </fieldset>
</asp:Content>
