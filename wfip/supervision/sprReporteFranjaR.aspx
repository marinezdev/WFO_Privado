<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteFranjaR.aspx.cs" Inherits="wfip.supervision.sprReporteFranjaR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>FRANJA</legend>
        <table  style ="width:100%"> 
            <tr> 
                <td style="width:50%; vertical-align:top">
                    <asp:HiddenField ID="hfCambio" runat="server" />
                    <dx:ASPxCalendar ID="ASPxCalendar1" runat="server" Width="560px" Theme="iOS" AutoPostBack="True" OnSelectionChanged="ASPxCalendar1_SelectionChanged" ShowClearButton="False"></dx:ASPxCalendar>
                </td>
                <td style ="vertical-align:top">
                    <asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click">Exportar a Excel</asp:LinkButton>
                    <dx:ASPxGridView ID="dvgdFranja" ClientInstanceName="dvgdFranja" runat="server" AutoGenerateColumns ="False" Width ="300px" EnableTheming="True" Theme="iOS" Font-Size ="10px">
                       <Columns>
                            <dx:GridViewDataTextColumn Caption ="Franja"  FieldName="Franja"  VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption= "Total" FieldName="Total" VisibleIndex="2" />
                        </Columns>
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
                        <SettingsPager  Mode="ShowAllRecords"/>
                    </dx:ASPxGridView>                    
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdFranja"></dx:ASPxGridViewExporter>
                    <br /><br />
                    <label><strong>Acumulado mensual:</strong></label>
                    <asp:Label ID="lblAcumulado" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan ="2">
                    <dx:WebChartControl ID="dvchtFranja" runat="server" CrosshairEnabled="True" Height="300px" Width="1150px" PaletteBaseColorNumber="5">
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
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

