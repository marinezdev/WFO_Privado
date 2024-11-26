<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralTop10R.aspx.cs" Inherits="wfip.supervision.sprReporteGeneralTop10R" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<script type="text/javascript">
   $(document).ready(Inicio);
   function Inicio() {
     $("#Ancho").val($(window).width() - 100)
     $("#btnFiltroTP").click();
     $(window).resize(function () {
     $("#Ancho").val($(window).width() - 100)
     $("#btnFiltroTP").click()

     });
   }
</script>
<fieldset>
  <legend class="text-warning; text-center"> TOP 10 </legend>
   <br /><br />
   <div class="container-fluid">
     <div class="row">
         <div class="col-sm-4; text-right">
             <dx:aspxdateedit ID="CalDesdeTE" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde">
                <TimeSectionProperties Adaptive="true">
                    <TimeEditProperties EditFormatString="hh:mm tt" />
                </TimeSectionProperties>
                <CalendarProperties>
                    <FastNavProperties DisplayMode="Inline" />
                </CalendarProperties> 
             </dx:aspxdateedit>
             <br />
         </div>
         <div class="col-sm-4; text-right">
             <dx:aspxdateedit ID="CalHastaTE" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                <TimeSectionProperties Adaptive="true">
                    <TimeEditProperties EditFormatString="hh:mm tt" />
                </TimeSectionProperties>
                <CalendarProperties>
                    <FastNavProperties DisplayMode="Inline" />
                </CalendarProperties>
             </dx:aspxdateedit>
         </div>
         <div class="col-sm-4; text-right">
             <asp:Button ID="btnFiltroTP" runat="server" Text="Filtrar" CssClass="btn btn-success" ClientIDMode="Static"/>
         </div>
     </div>
     <asp:UpdatePanel ID="UPTopProm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <dx:ASPxGridView ID="dvgdPromotorias" ClientInstanceName="dvgdPromotorias" runat="server" AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption ="CVE PROMOTORIA"  FieldName="Promotoria"  VisibleIndex="1" Width="400" />
                            <dx:GridViewDataTextColumn Caption ="PROMOTORIA" FieldName="Nombre"  VisibleIndex="2" Width="400"/>
                            <dx:GridViewDataTextColumn Caption= "ZONA" FieldName="Zona" VisibleIndex="3" Width="400"/>
                            <dx:GridViewDataTextColumn Caption= "EJECUTADOS" FieldName="NumTramitesEje" VisibleIndex="4" Width="400" />
                            <dx:GridViewDataTextColumn Caption= "SUSPENDIDOS" FieldName="NumTramitesSus" VisibleIndex="5" Width="400"/>
                        </Columns>
                        <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" />
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <SettingsPager  Mode="ShowAllRecords"/>
                        <SettingsSearchPanel Visible="true" />
                     </dx:ASPxGridView>                    
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdPromotorias"></dx:ASPxGridViewExporter>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="600px" Theme ="SoftOrange">
                        <Titles>
                            <dx:ChartTitle Font="Arial,12pt" Text="TOP 10 EJECUTADOS" />
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
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnFiltroTP" EventName="Click" />
        </Triggers>
     </asp:UpdatePanel>
     <br />
     <div class="row">
         <div class="col-sm-4; text-right">
            <dx:aspxdateedit ID="CalDesdeTS" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde">
                <TimeSectionProperties Adaptive="true">
                    <TimeEditProperties EditFormatString="hh:mm tt" />
                </TimeSectionProperties>
                <CalendarProperties>
                    <FastNavProperties DisplayMode="Inline" />
                </CalendarProperties> 
            </dx:aspxdateedit>
            <br />
         </div>
         <div class="col-sm-4; text-right">
            <dx:aspxdateedit ID="CalHastaTS" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                <TimeSectionProperties Adaptive="true">
                    <TimeEditProperties EditFormatString="hh:mm tt" />
                </TimeSectionProperties>
                <CalendarProperties>
                    <FastNavProperties DisplayMode="Inline" />
                </CalendarProperties>
            </dx:aspxdateedit>
         </div>
         <div class="col-sm-4; text-right">
             <asp:Button ID="btnFiltroTR" runat="server" Text="Filtrar"  CssClass="btn btn-success" ClientIDMode="Static"   />
         </div>
     </div>
     <asp:UpdatePanel ID="UPTopSus" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <dx:ASPxGridView ID="dgvsuspendidos" ClientInstanceName="dgvsuspendidos" runat="server" AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"   >
                        <Columns>
                            <dx:GridViewDataTextColumn Caption ="CVE PROMOTORIA"  FieldName="Promotoria"  VisibleIndex="1" Width="400" />
                            <dx:GridViewDataTextColumn Caption ="PROMOTORIA" FieldName="Nombre"  VisibleIndex="2" Width="400" />
                            <dx:GridViewDataTextColumn Caption= "ZONA" FieldName="Zona" VisibleIndex="3" Width="400" />
                            <dx:GridViewDataTextColumn Caption= "SUSPENDIDOS" FieldName="NumTramitesSus" VisibleIndex="4" Width="400" />
                            <dx:GridViewDataTextColumn Caption= "EJECUTADOS" FieldName="NumTramitesEje" VisibleIndex="5" Width="400"/>
                        </Columns>
                        <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" />
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <SettingsPager  Mode="ShowAllRecords"/>
                        <SettingsSearchPanel Visible="true" />
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExportSuspendidos" runat="server" GridViewID="dgvsuspendidos"></dx:ASPxGridViewExporter>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                   <asp:HiddenField ClientIDMode="Static" ID="Ancho" runat="server" Value="400"/>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <dx:WebChartControl ID="dxChtSuspendidos" runat="server" CrosshairEnabled="True" Height="300px" Width="600px" Theme ="RedWine">
                        <Titles>
                            <dx:ChartTitle Font="Arial,12pt" Text="TOP 10 SUSPENDIDOS" />
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
                            <dx:Series Name="dxsreSuspendidos" ></dx:Series>
                        </SeriesSerializable>
                    </dx:WebChartControl>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnFiltroTR" EventName="Click" />
        </Triggers>
     </asp:UpdatePanel>
   </div>
</fieldset>
</asp:Content>
