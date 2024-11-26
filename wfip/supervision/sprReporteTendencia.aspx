<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteTendencia.aspx.cs" Inherits="wfip.supervision.sprReporteTendencia" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>TENDENCIA POR AÑO</legend>
        <table  style ="width:100%">
            <tr>
             <td style="text-align:right">
                <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                   <img src="../img/excel.png"/>
                </asp:LinkButton>
             </td>
            </tr>
            <tr>
                <td style ="vertical-align:top" colspan="2">
                    <dx:ASPxGridView ID="dvgdTendencia" ClientInstanceName="dvgdTendencia" runat="server" AutoGenerateColumns ="False" Width ="100%" EnableTheming="True" Theme="iOS" Font-Size ="10px">
                       <Columns>
                            <dx:GridViewDataTextColumn Caption ="Año"  FieldName="Annio"  VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption= "Enero" FieldName="Enero" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption ="Febrero"  FieldName="Febrero"  VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption= "Marzo" FieldName="Marzo" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption ="Abril"  FieldName="Abril"  VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption= "Mayo" FieldName="Mayo" VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption ="Junio"  FieldName="Junio"  VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption= "Julio" FieldName="Julio" VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption ="Agosto"  FieldName="Agosto"  VisibleIndex="9" />
                            <dx:GridViewDataTextColumn Caption= "Septiembre" FieldName="Septiembre" VisibleIndex="10" />
                            <dx:GridViewDataTextColumn Caption ="Octubre"  FieldName="Octubre"  VisibleIndex="11" />
                            <dx:GridViewDataTextColumn Caption= "Noviembre" FieldName="Noviembre" VisibleIndex="12" />
                            <dx:GridViewDataTextColumn Caption= "Diciembre" FieldName="Diciembre" VisibleIndex="12" />

                        </Columns>
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <SettingsPager  Mode="ShowAllRecords"/>
                    </dx:ASPxGridView>                    
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdTendencia"></dx:ASPxGridViewExporter>
                </td>
            </tr>
            <tr>
                <td colspan ="2">
                    <dx:WebChartControl ID="dvchtTendencia" runat="server" CrosshairEnabled="True" Height="300px" Width="1150px" PaletteBaseColorNumber="5">
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
                </td>
            </tr>
        </table>







    </fieldset>
</asp:Content>
