<%@ Page Title="" Language="C#" MasterPageFile="~/tablero/tablero.Master" AutoEventWireup="true" CodeBehind="generalPorFlujo.aspx.cs" Inherits="wfip.tablero.generalPorFlujo" %>

<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick"></asp:Timer>
            <div id="dvEjecutados" style="width: 100%;">
                <fieldset>
                    <legend>INDICADOR GENERAL</legend>
                    <table id="tblGraficaUno" style="width: 100%;">
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:DropDownList ID="ddlFlujo" runat="server" AutoPostBack="True" Font-Size="14px" OnSelectedIndexChanged="ddlFlujo_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <div id="dvGrafUno" style="width: 550px; margin: auto;">
                                    <asp:Chart ID="grfGrupoUno" runat="server" Width="500px" Height="300px" BackColor="211, 223, 240" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="2px">
                                        <BorderSkin SkinStyle="Raised" />
                                        <ChartAreas>
                                            <asp:ChartArea Name="GrupoUno" BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" ShadowColor="Transparent">
                                                <%--<Area3DStyle Enable3D="True" IsRightAngleAxes="False" LightStyle="Realistic" Inclination="20" Rotation="15"/>--%>
                                                <AxisY LineColor="64, 64, 64, 64" IsMarginVisible="false">
                                                    <LabelStyle Font="Trebuchet MS, 8pt" />
                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                </AxisY>
                                                <AxisX LineColor="64, 64, 64, 64" IsMarginVisible="false">
                                                    <LabelStyle Font="Trebuchet MS, 8pt" />
                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </td>
                            <td style="vertical-align: top">
                                <div id="dvTacometro" style="width: 550px; margin: auto; text-align:center;">
                                    <dx:ASPxGaugeControl ID="deMedidorEfectrividad" runat="server" Height="100px" Width="450px" BackColor="#F2F2F2" Value="75" LayoutInterval="6">
                                        <Gauges>
                                            <dx:LinearGauge Bounds="6, 6, 348, 88" Name="linearGauge11" Orientation="Horizontal">
                                                <scales>
                                                    <dx:LinearScaleComponent AcceptOrder="0" Appearance-Brush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#4D4D4D&quot;/&gt;" Appearance-Width="4" AppearanceScale-Brush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#4D4D4D&quot;/&gt;" AppearanceScale-Width="4" AppearanceTickmarkText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#4D4D4D&quot;/&gt;" CustomLogarithmicBase="2" EndPoint="62.5, 20" MajorTickCount="6" MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeOffset="-7" MajorTickmark-ShapeScale="1.1, 1" MajorTickmark-ShapeType="Circular_Style28_1" MajorTickmark-TextOffset="-20" MajorTickmark-TextOrientation="BottomToTop" MaxValue="100" MinorTickCount="4" MinorTickmark-ShapeOffset="-14" MinorTickmark-ShapeType="Circular_Style28_1" MinorTickmark-ShowTick="False" Name="scale2" StartPoint="62.5, 230" Value="75">
                                                        <ranges>
                                                            <dx:LinearScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#D54367&quot;/&gt;" EndThickness="11" EndValue="50" Name="Range0" ShapeOffset="10" StartThickness="11" />
                                                            <dx:LinearScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#F5E16B&quot;/&gt;" EndThickness="11" EndValue="80" Name="Range1" ShapeOffset="10" StartThickness="11" StartValue="50" />
                                                            <dx:LinearScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#59BB71&quot;/&gt;" EndThickness="11" EndValue="100" Name="Range2" ShapeOffset="10" StartThickness="11" StartValue="80" />
                                                        </ranges>
                                                    </dx:LinearScaleComponent>
                                                </scales>
                                                <rangebars>
                                                    <dx:LinearScaleRangeBarComponent AcceptOrder="100" AppearanceRangeBar-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#E73141&quot;/&gt;" EndOffset="8" LinearScale="" Name="linearGauge2_RangeBar1" ScaleID="scale2" StartOffset="4" ZOrder="-100" />
                                                </rangebars>
                                            </dx:LinearGauge>
                                        </Gauges>
                                        <LayoutPadding All="6" Bottom="6" Left="6" Right="6" Top="6" />
                                    </dx:ASPxGaugeControl>
                                    <dx:ASPxGaugeControl ID="ASPxGaugeControl2" runat="server" BackColor="#F2F2F2" Height="90px" Value="2" Width="90px">
                                        <Gauges>
                                            <dx:StateIndicatorGauge Bounds="0, 0, 90, 90" Name="Gauge0">
                                                <indicators>
                                                    <dx:StateIndicatorComponent AcceptOrder="0" Center="124, 124" Name="stateIndicatorComponent1" Size="200, 200" StateIndex="2">
                                                        <states>
                                                            <dx:IndicatorStateWeb Name="State1" ShapeType="ElectricLight1" />
                                                            <dx:IndicatorStateWeb Name="State2" ShapeType="ElectricLight2" />
                                                            <dx:IndicatorStateWeb Name="State3" ShapeType="ElectricLight3" />
                                                            <dx:IndicatorStateWeb Name="State4" ShapeType="ElectricLight4" />
                                                        </states>
                                                    </dx:StateIndicatorComponent>
                                                </indicators>
                                            </dx:StateIndicatorGauge>
                                        </Gauges>
                                        <LayoutPadding All="0" Bottom="0" Left="0" Right="0" Top="0" />
                                    </dx:ASPxGaugeControl>
                                </div>
                                <div id="dvGridUno" style="width: 550px; margin: auto;">
                                    <asp:GridView ID="grdDatosTotales" runat="server" Width="100%"
                                        CellPadding="4" GridLines="None" Font-Size="10px"
                                        AutoGenerateColumns="False" HorizontalAlign="Center" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        <Columns>
                                            <asp:BoundField DataField="Nombre" HeaderText="MESA" ItemStyle-HorizontalAlign="Left" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Totales" HeaderText="TOTAL" HeaderStyle-ForeColor="Blue" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle ForeColor="Blue" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Procesados" HeaderText="PROCESADOS" HeaderStyle-ForeColor="LightGreen" ItemStyle-HorizontalAlign="Center" >
                                            <HeaderStyle ForeColor="LightGreen" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Pendientes" HeaderText="PENDIENTES" HeaderStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center" >
                                            <HeaderStyle ForeColor="Red" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
