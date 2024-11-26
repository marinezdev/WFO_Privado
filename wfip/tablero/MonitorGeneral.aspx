<%@ Page Title="" Language="C#" MasterPageFile="~/tablero/tablero.Master" AutoEventWireup="true" CodeBehind="MonitorGeneral.aspx.cs" Inherits="wfip.tablero.MonitorGeneral" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Timer ID="ActualizaInfo" runat="server" Interval="20000" OnTick="ActualizaInfo_Tick">
     </asp:Timer>

    <fieldset>
        <legend>INDICADORES GENERALES</legend>
        <br />
       
            <table style ="width:100%">
                <tr>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlFlujo" runat="server" 
                        AutoPostBack="True" Font-Size="14px">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 
                    <br /><br />
                </td>
                </tr>
        </table>
            <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                 <div id="dvCajaGrilla" style="width: 1000px; margin: auto;">
                    <dx:ASPxGridView ID="dvgdMesa" ClientInstanceName="dvgdMesa" runat="server" AutoGenerateColumns="False" Width="100%" EnableTheming="True" Theme="iOS" Font-Size="10px" KeyFieldName="ESTATUS">
                        <Templates>
                               <DetailRow>
                                   <dx:ASPxLabel runat="server" Font-Bold="true" Text="ESTATUS: " />
                                   <dx:ASPxLabel runat="server" Text='<%#Eval("ESTATUS") %>' Font-Bold="true" />
                                    <br /><br />
                                   <dx:ASPxGridView ID="dvgdDetalleTramite" runat="server" ClientInstanceName="dvgdDetalleTramite"   OnInit="dvgdDetalleTramite_Init" KeyFieldName="ESTATUS" width="100%" EnablePagingGestures="False" AutoGenerateColumns ="False" >
                                       <Columns>
                                            <dx:GridViewDataColumn FieldName="MesaNombre" Caption="MESA" VisibleIndex="1"/>
                                            <dx:GridViewDataColumn FieldName="UsuarioNombre" Caption="USUARIO" VisibleIndex="2"/>
                                            <dx:GridViewDataColumn FieldName="FechaRegistro" Caption="FECHA REGISTRO" VisibleIndex="3"/>
                                            <dx:GridViewDataColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="4"/>
                                            <dx:GridViewDataColumn FieldName="PromotoriaNombre" Caption="PROMOTORIA" VisibleIndex="5"/>
                                        </Columns>
                                        <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True" ShowGroupPanel="True"/>
                                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="100" />
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowAllRecords" />
                                        <Styles Header-Wrap="True"/>
                                    </dx:ASPxGridView>
                               </DetailRow>
                           </Templates>

                    <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True"  HorizontalScrollBarMode="Auto" />
                    <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdMesa"></dx:ASPxGridViewExporter>
                <br />
                </div>
                <div id="dvCajaGrafica" style="width: 1000px; margin: auto;">
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
                        <%--                <SeriesSerializable>
                            <dx:Series Name="dxSreTotales"></dx:Series>
                        </SeriesSerializable>--%>
                    </dx:WebChartControl>
                </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ActualizaInfo" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
    </fieldset>
</asp:Content>
