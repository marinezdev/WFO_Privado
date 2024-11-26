<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteMesa.aspx.cs" Inherits="wfip.supervision.sprReporteMesa" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>MESA</legend>
        <table  style ="width:100%">
            <tr>
                <td style ="width:50%; vertical-align:top">
                    <asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click">Exportar a Excel</asp:LinkButton>
                    <dx:ASPxGridView ID="dvgdSuspendidos" ClientInstanceName="dvgdFranja" runat="server" AutoGenerateColumns ="False" Width ="95%" EnableTheming="True" Theme="iOS"  Font-Size ="10px"   >
                       <Columns>
                           <dx:GridViewDataTextColumn Caption ="Tipo"  FieldName="Tipo"  VisibleIndex="1" /> 
                           <dx:GridViewDataTextColumn Caption ="Admision"  FieldName="Admision"  VisibleIndex="1" />
                           <dx:GridViewDataTextColumn Caption ="Revision Documental"  FieldName="RevisionDocumental"  VisibleIndex="2" />
                           <dx:GridViewDataTextColumn Caption ="Plad"  FieldName="Plad"  VisibleIndex="3" />
                           <dx:GridViewDataTextColumn Caption ="Ejecucion"  FieldName="Ejecucion"  VisibleIndex="4" />
                        </Columns>
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <SettingsPager  Mode="ShowAllRecords"/>
                    </dx:ASPxGridView>                    
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdSuspendidos"></dx:ASPxGridViewExporter>
                </td>
            </tr>
           <%-- <tr>
                <td>
                    <dx:WebChartControl ID="dvchtSuspendidos" runat="server" CrosshairEnabled="True" Height="300px" Width="900px" PaletteBaseColorNumber="5">
                        <BorderOptions Visibility="False" />
                        <Titles>
                            <dx:ChartTitle Font="Arial,12pt" Text="Suspendidos" TextColor ="Tomato" />
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
            </tr>--%>
        </table>
    </fieldset>
</asp:Content>
