<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralFranjaR.aspx.cs" Inherits="wfip.supervision.sprReporteGeneralFranjaR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <script type="text/javascript">
        $(document).ready(Inicio);
        function Inicio() {
            $("#Ancho").val($(window).width() - 100)
            $("#btnFiltroFranja").click();
            $(window).resize(function () {
                $("#Ancho").val($(window).width() - 100)
                $("#btnFiltroFranja").click() 
            });
        
        }
    </script>
<fieldset>
   <legend class="text-center">FRANJA</legend>
   <div class="container-fluid">
      <!-- Reporte Franja -->
      <div class="form-horizontal">
         <div class="form-group">
            <div class="col-md-3 col-sm-4 col-xs-9">
               <asp:HiddenField ID="hfCambio" runat="server" />
               <dx:ASPxDateEdit ID="CalFranja" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Fecha:">
                  <TimeSectionProperties Adaptive="true">
                     <TimeEditProperties EditFormatString="hh:mm tt" />
                  </TimeSectionProperties>
                  <CalendarProperties>
                     <FastNavProperties DisplayMode="Inline" />
                  </CalendarProperties>
               </dx:ASPxDateEdit>
            </div>
            <div class="col md-9 col-sm-8 col-xs-3">
               <asp:Button ID="btnFiltroFranja" runat="server" Text="Filtrar" CssClass="btn btn-success" ClientIDMode="Static" />
            </div>
         </div>
      </div>
      <asp:UpdatePanel ID="Franja" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                   <dx:ASPxGridView ID="dvgdFranja" ClientInstanceName="dvgdFranja" runat="server" AutoGenerateColumns ="False" Width ="95%" EnableTheming="True" Theme="iOS" Font-Size ="10px">
                      <Columns>
                         <dx:GridViewDataTextColumn Caption ="FRANJA"  FieldName="Franja"  VisibleIndex="0" />
                         <dx:GridViewDataTextColumn Caption= "INGRESADOS" FieldName="ingresados" VisibleIndex="1" />
                         <dx:GridViewDataTextColumn Caption= "PENDIENTES DE ATENCIÓN" FieldName="tocados" VisibleIndex="2" >
                            <CellStyle Wrap="True"></CellStyle> 
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption= "PROCESADOS" FieldName="ejecutados" VisibleIndex="3" />
                      </Columns>
                      <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                      <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
                      <SettingsPager  Mode="ShowAllRecords"/>
                      <Styles Header-Wrap="True"/>
                   </dx:ASPxGridView>                    
                   <dx:ASPxGridViewExporter ID="grdExportFranja" runat="server" GridViewID="dvgdFranja"></dx:ASPxGridViewExporter>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                   <label"><strong>ACUMULADO MENSUAL:</strong></label>
                   <asp:Label ID="lblAcumulado" runat="server"></asp:Label>
                   <br />
                   <dx:WebChartControl ID="dvchtFranja" runat="server" CrosshairEnabled="True" Height="300px" Width="500px" PaletteBaseColorNumber="5">
                      <BorderOptions Visibility="False" />
                      <Titles>
                         <dx:ChartTitle Font="Arial,12pt" Text="FRANJA" TextColor ="Tomato" />
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
                      <Legend Name="Default Legend" TextVisible="False" Visibility ="False" ></Legend>
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
            <asp:AsyncPostBackTrigger ControlID="btnFiltroFranja" EventName="Click" />
         </Triggers>
      </asp:UpdatePanel>
      <br /><br /> 
      <!--Fin reporte Franja -->
      <!-- Reporte Trámites por semana-->
      <div class="row">
         <div class="col-md-12 col-sm-12">
            <label style="font-weight:bold"> TRÁMITES POR SEMANA </label>
            <br /><br />
         </div>
      </div>
      <div class="form-horizontal">
         <div class="form-group">
            <label for="AnnioTS" class="control-label col-md-1 col-sm-1 col-xs-1">Año: &nbsp;</label>
            <div class="col-md-3 col-sm-3 col-xs-3">
               <asp:DropDownList runat="server" ID="AnnioTS" CssClass="form-control">
               </asp:DropDownList>
            </div>
            <label for="MesTS" class="col-md-1 col-sm-1 col-xs-1">Mes: &nbsp;</label>
            <div class="col-md-3 col-sm-3 col-xs-3">
               <asp:DropDownList runat="server" ID="MesTS" CssClass="form-control">
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
            </div>
            <div class="col-md-4 col-sm-4 col-xs-3">
               <asp:Button ID="btnFiltrarTS"  CssClass="btn btn-success" runat="server" Text="Filtrar"/>
            </div>
         </div>
      </div>
      <asp:UpdatePanel ID="DetalleReporteTS" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
             <div class="row">
                 <div class="col-md-12">
                    <dx:ASPxGridView ID="dvgdTramiteSemana" ClientInstanceName="dvgdTramiteSemana" runat="server" AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"    >
                       <Columns>
                          <dx:GridViewDataTextColumn Caption="SEMANA" FieldName="SEMANA"  VisibleIndex="1" GroupIndex="0"/>
                          <dx:GridViewDataTextColumn Caption ="FECHA" FieldName="FECHA"  VisibleIndex="2" />
                          <dx:GridViewDataTextColumn Caption ="DIA" FieldName="DIA" VisibleIndex="3" />
                          <dx:GridViewDataTextColumn Caption ="TRAMITES" FieldName="TRAMITES" VisibleIndex="4" />
                       </Columns>
                       <Settings  ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True" ShowGroupPanel="True" />
                       <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                       <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                       <SettingsPager  Mode="ShowAllRecords"/>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExportTS" runat="server" GridViewID="dvgdTramiteSemana"></dx:ASPxGridViewExporter>
                 </div>
             </div>
             <div class="row">
                <div class="col-md-12">
                   <dx:WebChartControl ID="dxChtTotalesTS" runat="server" CrosshairEnabled="True" Height="300px" Width="550px" Theme ="SoftOrange">
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
                      <Legend Name="Default Legend" Visibility ="False" ></Legend>
                      <SeriesSerializable>
                         <dx:Series Name="dxSreTotalesTS" ></dx:Series>
                      </SeriesSerializable>
                   </dx:WebChartControl>
                </div>
             </div>
          </ContentTemplate>
          <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltrarTS"  EventName="Click" />
          </Triggers>
      </asp:UpdatePanel>
      <!-- Fin reporte Trámites por semana -->
      <!-- Reporte de Tendencia por año -->
      <br /><br/>
      <div class="row">
         <div class="col-md-12">
            <label><strong>TENDENCIA POR AÑO</strong></label>
         </div>
      </div>
      <div class="row">
         <div class="col-md-12">
            <dx:ASPxGridView ID="dvgdTendencia" ClientInstanceName="dvgdTendencia" runat="server" AutoGenerateColumns ="False" Width ="100%" EnableTheming="True" Theme="iOS" Font-Size ="10px">
               <Columns>
                  <dx:GridViewDataTextColumn Caption ="TIPO"  FieldName="Tipo"  VisibleIndex="1" />
                  <dx:GridViewDataTextColumn Caption ="AÑO"  FieldName="Annio"  VisibleIndex="2" />
                  <dx:GridViewDataTextColumn Caption= "ENERO" FieldName="Enero" VisibleIndex="3" />
                  <dx:GridViewDataTextColumn Caption ="FEBRERO"  FieldName="Febrero"  VisibleIndex="4" />
                  <dx:GridViewDataTextColumn Caption= "MARZO" FieldName="Marzo" VisibleIndex="5" />
                  <dx:GridViewDataTextColumn Caption ="ABRIL"  FieldName="Abril"  VisibleIndex="6" />
                  <dx:GridViewDataTextColumn Caption= "MAYO" FieldName="Mayo" VisibleIndex="7" />
                  <dx:GridViewDataTextColumn Caption ="JUNIO"  FieldName="Junio"  VisibleIndex="8" />
                  <dx:GridViewDataTextColumn Caption= "JULIO" FieldName="Julio" VisibleIndex="9" />
                  <dx:GridViewDataTextColumn Caption ="AGOSTO"  FieldName="Agosto"  VisibleIndex="10" />
                  <dx:GridViewDataTextColumn Caption= "SEPTIEMBRE" FieldName="Septiembre" VisibleIndex="11" />
                  <dx:GridViewDataTextColumn Caption ="OCTUBRE"  FieldName="Octubre"  VisibleIndex="12" />
                  <dx:GridViewDataTextColumn Caption= "NOVIEMBRE" FieldName="Noviembre" VisibleIndex="13" />
                  <dx:GridViewDataTextColumn Caption= "DICIEMBRE" FieldName="Diciembre" VisibleIndex="14" />
               </Columns>
               <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
               <SettingsPager  Mode="ShowAllRecords"/>
               <Settings HorizontalScrollBarMode="Visible" />
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="grdExportT" runat="server" GridViewID="dvgdTendencia"></dx:ASPxGridViewExporter>
         </div>
      </div>
      <asp:UpdatePanel runat="server" ID="TendenciaGlobal" UpdateMode="Always">
          <ContentTemplate>
             <div class="row">
                <div class="col-md-12">
                   <dx:WebChartControl ID="dvchtTendencia" runat="server" CrosshairEnabled="True" Height="300px" Width="1100px" PaletteBaseColorNumber="5">
                      <BorderOptions Visibility="False" />
                      <Titles>
                         <dx:ChartTitle Font="Arial,12pt" Text="TENDENCIA" TextColor ="Tomato" />
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
                      <Legend Name="Default Legend" TextVisible="False" Visibility ="False" ></Legend>
                   </dx:WebChartControl>
                </div>
             </div>
          </ContentTemplate>
      </asp:UpdatePanel>
      <!-- Fin reporte de Tendencia por año -->
   </div>
</fieldset>
</asp:Content>

