<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteTramiteSemanaR.aspx.cs" Inherits="wfip.supervision.sprReporteTramiteSemanaR" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
    <fieldset>
        <legend>RESUMEN SEMANAL</legend>
        <br /><br />
    <div>
        <table style ="width:50%">
                <tr>
                <td>
                 <div class="form-inline form-group">
                    <label>Año: &nbsp;</label> 
                    <asp:DropDownList runat="server" ID="Annio" CssClass="form-control">
                       <asp:ListItem Text="2016" Value="2016" />
                       <asp:ListItem Text="2017" Value="2017" />
                       <asp:ListItem Text="2018" Value="2018" />
                    </asp:DropDownList>
                    &nbsp;&nbsp; 
                    <label>Mes: &nbsp;</label> 
                    <asp:DropDownList runat="server" ID="Mes" CssClass="form-control">
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
                    <asp:Button ID="btnFiltrar"  CssClass="btn btn-success" runat="server" Text="Filtrar"/>
                 </div>
                </td>
                </tr>
        </table>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table  style ="width:100%">
            <tr>
                <td style ="width:50%">
                    &nbsp;<dx:ASPxGridView ID="dvgdTramiteSemana" ClientInstanceName="dvgdTramiteSemana" runat="server" AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"    >
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
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdTramiteSemana"></dx:ASPxGridViewExporter>
                </td>
                <td>
                    <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="550px" Theme ="SoftOrange">
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
                            <dx:Series Name="dxSreTotales" ></dx:Series>
                        </SeriesSerializable>
                    </dx:WebChartControl>
                </td>
            </tr>
            <tr>
                <td> &nbsp;</td>
            </tr>
        </table>
        </ContentTemplate>
        <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
    </div>
         <asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click">Exportar a Excel</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
    </fieldset>
</asp:Content>
