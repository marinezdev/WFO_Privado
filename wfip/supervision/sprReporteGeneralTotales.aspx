<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralTotales.aspx.cs" Inherits="wfip.supervision.sprReporteGeneralTotales" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
    <fieldset>
        <legend> TOTALES </legend>
        <br /><br />
        <table style ="width:50%">
                <tr>
                    <td>
                        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True" EnableViewState="true" Text="Cargando.." Border-BorderWidth="0">
                        </dx:ASPxLoadingPanel>
                    </td>
                </tr>
                <tr>
                <td>
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"  EditFormatString="f" Width="190" Caption="Desde:">
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
                     <asp:Button ID="btnFiltrar" CssClass="boton" runat="server" Text="Filtrar" ClientIDMode="Static"/>
                </td>
                </tr>
        </table>
        <table style="width:100%">
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
        </table>
        <table style="width:100%">
          <tr style="width:48%;text-align:right">
             <td >
                <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                   <img src="../img/excel.png"/>
                </asp:LinkButton>
             </td>
             <td style="width:52%">
                &nbsp;
             </td>
          </tr>
        </table>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table  style ="width:100%; margin-left:auto; margin-right:auto">
            <tr>
                <td style ="width:50%; vertical-align:top">
                    &nbsp;<dx:ASPxGridView ID="dvgdTotales" ClientInstanceName="dvgdTotales" runat="server" AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"    >
                       <Columns>
                            <dx:GridViewDataTextColumn Caption="DESCRIPCIÓN" FieldName="Descripcion"  VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption ="TOTALES" FieldName="Totales"  VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption ="%" FieldName="Porcentaje" VisibleIndex="3" />
                        </Columns>
                        <Settings  ShowFooter="True" ShowGroupFooter="VisibleAlways"  />
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <SettingsPager  Mode="ShowAllRecords"/>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdTotales"></dx:ASPxGridViewExporter>
                </td>
                <td style="vertical-align:top">
                    <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="550px" Theme ="SoftOrange">
                        <Titles>
                            <dx:ChartTitle Font="Arial,12pt" Text="PORCENTAJES OPERACION" />
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
        </table>
        </ContentTemplate>
        <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
      <asp:LinkButton ID="lnkControl" runat="server" CausesValidation="False" OnClick="lnkControl_Click" Visible="false">Operativo</asp:LinkButton>
    </fieldset>
</asp:Content>
