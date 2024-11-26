<%@ Page Title="" Language="C#"  MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralMesaR.aspx.cs" Inherits="wfip.supervision.sprReporteGeneralMesaR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(Inicio);
        function Inicio() {
            $("#Ancho").val($(window).width() - 100)
            $("#btnFiltrar").click();
            $(window).resize(function () {
                $("#Ancho").val($(window).width() - 100)
                $("#btnFiltrar").click() 
            });
        
        }
    </script>
    <fieldset>
        <legend class="text-warning; text-center">REPORTE GENERAL POR MESA</legend>
        <br /> <br />
        <div class="container-fluid">
            <div class="row>">
                <div class="col-sm-4; text-right">
                  <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde:">
                     <TimeSectionProperties Adaptive="true">
                        <TimeEditProperties EditFormatString="hh:mm tt" />
                     </TimeSectionProperties>
                     <CalendarProperties>
                        <FastNavProperties DisplayMode="Inline" />
                     </CalendarProperties>
                  </dx:ASPxDateEdit>
                  <br />
                </div>
                <div class="col-sm-4; text-right">
                  <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                     <TimeSectionProperties Adaptive="true">
                         <TimeEditProperties EditFormatString="hh:mm tt" />
                     </TimeSectionProperties>
                     <CalendarProperties>
                        <FastNavProperties DisplayMode="Inline" />
                     </CalendarProperties>
                   </dx:ASPxDateEdit>
                </div>
                <div class="col-sm-4; text-right">
                     <asp:Button ID="btnFiltrar"  CssClass="btn btn-success" runat="server" Text="Filtrar" ClientIDMode="Static"/>
                     <br /><br />
                </div>
            </div>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <dx:ASPxGridView ID="dvgdMesa" ClientInstanceName="dvgdMesa" runat="server" AutoGenerateColumns="False" Width="100%" EnableTheming="True" Theme="iOS" Font-Size="10px">
                  <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                  <SettingsPager Mode="ShowAllRecords" />
                  <Settings HorizontalScrollBarMode="Auto" />
                   <Styles Header-Wrap="True"/>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdMesa"></dx:ASPxGridViewExporter>
                <br />
            </div>
        </div>
        <div class="row">
           <div class="col-sm-12">
             <div id="dvCajaGrafica" style="width: 100%; margin: auto;">
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
                </dx:WebChartControl>
               </div>
           </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:HiddenField ClientIDMode="Static" ID="Ancho" runat="server" Value="400"/>
            </div>
        </div>
        </ContentTemplate>
        <Triggers>
           <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
        </div>
    </fieldset>
</asp:Content>
