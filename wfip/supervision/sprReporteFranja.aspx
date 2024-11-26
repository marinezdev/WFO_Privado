<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteFranja.aspx.cs" Inherits="wfip.supervision.sprReporteFranja" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>FRANJA</legend>
        <table  style ="width:100%">
            <tr>
                <td style="width:50%">&nbsp;</td>
                <td style="text-align:right; width:50%">
                <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                   <img src="../img/excel.png"/>
                </asp:LinkButton>
             </td>
            </tr>
            <tr> 
                <td style="width:50%; vertical-align:top">
                    <asp:HiddenField ID="hfCambio" runat="server" />
                    <dx:ASPxCalendar ID="ASPxCalendar1" runat="server" Theme="iOS" AutoPostBack="True" OnSelectionChanged="ASPxCalendar1_SelectionChanged" ShowClearButton="False"></dx:ASPxCalendar>
                </td>
                <td style ="vertical-align:top">
                    <dx:ASPxGridView ID="dvgdFranja" ClientInstanceName="dvgdFranja" runat="server" AutoGenerateColumns ="False" Width ="400px" EnableTheming="True" Theme="iOS" Font-Size ="10px">
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
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdFranja"></dx:ASPxGridViewExporter>
                    <br /><br />
                    <label><strong>ACUMULADO MENSUAL:</strong></label>
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
