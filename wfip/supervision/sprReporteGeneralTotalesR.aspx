<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralTotalesR.aspx.cs" Inherits="wfip.supervision.sprReporteGeneralTotalesR" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
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
        <legend class="text-warning; text-center">REPORTE GENERAL DE TOTALES</legend>
        <br /><br />
    <div class="container-fluid">
        <div class="row>">
            <div class="col-sm-4; text-right">
              <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom" EditFormatString="f"  Width="190" Caption="Desde:">
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
              <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom" EditFormatString="f" Width="190" Caption="Hasta">
                <TimeSectionProperties Adaptive="true">
                <TimeEditProperties EditFormatString="hh:mm tt" />
                </TimeSectionProperties>
                <CalendarProperties>
                   <FastNavProperties DisplayMode="Inline" />
                </CalendarProperties>
              </dx:ASPxDateEdit>
            </div>
            <div class="col-sm-4; text-right">
                <asp:Button ID="btnFiltrar" CssClass="btn btn-success" runat="server" Text="Filtrar" ClientIDMode="Static"/>
            </div>
        </div>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <dx:ASPxGridView ID="dvgdTotales" ClientInstanceName="dvgdTotales" runat="server" AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"    >
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
                 <br />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <dx:WebChartControl ID="dxChtTotales" ClientInstanceName="chart" runat="server" CrosshairEnabled="True" Height="300px" Width="800px" Theme ="SoftOrange">
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
         <!--<asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click">Exportar a Excel</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:LinkButton ID="lnkControl" runat="server" CausesValidation="False" OnClick="lnkControl_Click">Operativo</asp:LinkButton>-->
    </fieldset>
</asp:Content>
