<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReportePorcientoSuspension.aspx.cs" Inherits="wfip.supervision.sprReportePorcientoSuspension" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
    <fieldset>
        <legend>SUSPENDIDOS</legend>
        <br /><br />
    <div>
        <table style ="width:50%">
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
                <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Filtrar"/>
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
        </table>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table  style ="width:100%">
            <tr>
                <td style ="width:52%">
                    &nbsp;
                    <dx:ASPxGridView ID="dvgdPorcientoSuspension" ClientInstanceName="dvgdPorcientoSuspension" runat="server" AutoGenerateColumns ="False" Width ="100%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"    >
                       <Columns>
                            <dx:GridViewDataTextColumn Caption="NUM. PROMOTORÍA" FieldName="IdPromotoria"  VisibleIndex="0" Visible="false"/>
                            <dx:GridViewDataTextColumn Caption="CVE. PROMOTORÍA" FieldName="PromotoriaClave"  VisibleIndex="1" Width="95" />
                            <dx:GridViewDataTextColumn Caption="PROMOTORÍA" FieldName="PromotoriaNombre"  VisibleIndex="2" Width="95" />
                            <dx:GridViewDataTextColumn Caption ="INGRESADOS" FieldName="ingresados" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption ="SUSPENDIDOS" FieldName="suspendidos"  VisibleIndex="4"  />
                            <dx:GridViewDataTextColumn Caption ="% PARCIAL" FieldName="porcientoSuspendidos"  VisibleIndex="5" PropertiesTextEdit-DisplayFormatString="N" />
                            <dx:GridViewDataTextColumn Caption ="% TOTAL" FieldName="porcientoTotal"  VisibleIndex="6" PropertiesTextEdit-DisplayFormatString="N" />
                       </Columns>
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
                        <SettingsPager  Mode="ShowAllRecords"/>
                        <SettingsSearchPanel Visible="true" />
                        <Styles Header-Wrap="True"/>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdTotales"></dx:ASPxGridViewExporter>
                </td>
                <td>
                    <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="550px" Theme ="SoftOrange">
                        <Titles>
                            <dx:ChartTitle Font="Arial,12pt" Text="TRÁMITES SUSPENDIDOS" />
                        </Titles>
                        <DiagramSerializable>
                            <dx:XYDiagram>
                                <AxisX VisibleInPanesSerializable="-1">
                                </AxisX>
                                <AxisY VisibleInPanesSerializable="-1">
                                </AxisY>
                            </dx:XYDiagram>
                        </DiagramSerializable>
                        <Legend Name="Default Legend" Visibility ="False" ></Legend>
                        <SeriesSerializable>
                            <dx:Series Name="dxSreTotales" ></dx:Series>
                        </SeriesSerializable>
                    </dx:WebChartControl>
                </td>
            </tr>
            <tr>
                <td> &nbsp;</td>
            </tr>
            <td colspan="2" style="width:100%">
                 &nbsp;<dx:ASPxGridView ID="dvgdMotivosSuspension" ClientInstanceName="dvgdMotivosSuspension" runat="server" AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"    >
                       <Columns>
                            <dx:GridViewDataTextColumn Caption="% MOTIVOS" FieldName="MOTIVO"  VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption ="ENERO" FieldName="ENERO"  VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption ="FEBRERO" FieldName="FEBRERO"  VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption ="MARZO" FieldName="MARZO"  VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption ="ABRIL" FieldName="ABRIL"  VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption ="MAYO" FieldName="MAYO"  VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption ="JUNIO" FieldName="JUNIO"  VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption ="JULIO" FieldName="JULIO"  VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption ="AGOSTO" FieldName="AGOSTO"  VisibleIndex="9" />
                            <dx:GridViewDataTextColumn Caption ="SEPTIEMBRE" FieldName="SEPTIEMBRE"  VisibleIndex="10" />
                            <dx:GridViewDataTextColumn Caption ="OCTUBRE" FieldName="OCTUBRE"  VisibleIndex="11" />
                            <dx:GridViewDataTextColumn Caption ="NOVIEMBRE" FieldName="NOVIEMBRE"  VisibleIndex="12" />
                            <dx:GridViewDataTextColumn Caption ="DICIEMBRE" FieldName="DICIEMBRE"  VisibleIndex="13" />
                        </Columns>
                        <Settings  ShowFooter="True" ShowGroupFooter="VisibleAlways"  />
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <SettingsPager  Mode="ShowAllRecords"/>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExportMotivos" runat="server" GridViewID="dvgdMotivosSuspension"></dx:ASPxGridViewExporter>
            </td>
        </table>
        </ContentTemplate>
        <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
    </div>
    </fieldset>
</asp:Content>
