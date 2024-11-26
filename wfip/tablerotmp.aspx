<%@ Page Title="" Language="C#" MasterPageFile="~/wfip.Master" AutoEventWireup="true" CodeBehind="tablerotmp.aspx.cs" Inherits="wfip.tablerotmp" %>

<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.State" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="4000" OnTick="Timer1_Tick"></asp:Timer>
            <div id="dvEjecutados" style="width: 100%">
                <fieldset>
                    <legend>EFECTIVIDAD</legend>
                    <div style="width: 100%; text-align: center">
                        <dx:ASPxGaugeControl ID="ASPxGaugeControl1" runat="server" Height="100px" Width="360px" BackColor="#F2F2F2" LayoutInterval="6" Value="75">
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
                            <LayoutPadding All="6" Left="6" Top="6" Right="6" Bottom="6"></LayoutPadding>
                        </dx:ASPxGaugeControl>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControl2" runat="server" BackColor="#F2F2F2" Height="100px" Value="2" Width="100px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="0, 0, 100, 100" Name="Gauge0">
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
                            <LayoutPadding All="0" Left="0" Top="0" Right="0" Bottom="0"></LayoutPadding>
                        </dx:ASPxGaugeControl>
                    </div>
                </fieldset>
            </div>
            <div id="dvPendientes" style="width: 100%">
                <fieldset>
                    <legend>PENDIENTES</legend>
                    <asp:Chart ID="grfPendientes" runat="server" Width="1140px" BackColor="211, 223, 240" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid"
                        BorderWidth="2px" Height="192px">
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series Name="seriePendientes" ChartType="Bar">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </fieldset>
            </div>
            <div id="dvAvance">
                <fieldset>
                    <legend>AVANCE</legend>
                    <table id="tblOrdenTablero" style="width: 100%; background-color:black;">
                        <tr>
                            <td style="width: 25%; text-align: center; color: white;">ADMISION</td>
                            <td style="width: 25%; text-align: center; color: white;">REVISION DOCUMENTAL</td>
                            <td style="width: 25%; text-align: center; color: white;">REVISION PLAD</td>
                            <td style="width: 25%; text-align: center; color: white;">SELECCION</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <dx:ASPxGaugeControl ID="tacAdmision" runat="server" Height="160px" Width="160px" BackColor="Black" LayoutInterval="6" Value="22">
                                    <Gauges>
                                        <dx:CircularGauge Bounds="6, 6, 148, 148" Name="circularGauge2">
                                            <scales>
                                <dx:ArcScaleComponent AcceptOrder="0" AppearanceTickmarkText-Font="Tahoma, 9.75pt" AppearanceTickmarkText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#00E1F3&quot;/&gt;" Center="125, 165" EndAngle="0" MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeOffset="-11" MajorTickmark-ShapeType="Circular_Style17_1" MajorTickmark-TextOrientation="LeftToRight" MaxValue="100" MinorTickCount="4" MinorTickmark-ShapeOffset="-7" MinorTickmark-ShapeType="Circular_Style17_2" Name="scale1" StartAngle="-180" Value="22" Appearance-Brush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#058897&quot;/&gt;" Appearance-Width="3" AppearanceScale-Brush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#058897&quot;/&gt;" AppearanceScale-Width="3" MajorTickCount="6" MajorTickmark-TextOffset="-22" RadiusX="95" RadiusY="95">
                                    <ranges>
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#09AC86&quot;/&gt;" EndValue="33" Name="Range0" ShapeOffset="0" />
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#CAB46E&quot;/&gt;" EndValue="66" Name="Range1" ShapeOffset="0" StartValue="33" />
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#C16268&quot;/&gt;" EndValue="100" Name="Range2" ShapeOffset="0" StartValue="66" />
                                    </ranges>
                                </dx:ArcScaleComponent>
                            </scales>
                                            <backgroundlayers>
                                <dx:ArcScaleBackgroundLayerComponent AcceptOrder="-1000" ArcScale="" Name="bg" ScaleID="scale1" ShapeType="CircularHalf_Style17" ZOrder="1000" ScaleCenterPos="0.5, 0.68" Size="250, 179" />
                            </backgroundlayers>
                                            <needles>
                                <dx:ArcScaleNeedleComponent AcceptOrder="50" ArcScale="" EndOffset="4" Name="needle" ScaleID="scale1" ShapeType="CircularFull_Style17" ZOrder="-50" StartOffset="-27" />
                            </needles>
                                        </dx:CircularGauge>
                                    </Gauges>
                                    <LayoutPadding All="6" Left="6" Top="6" Right="6" Bottom="6"></LayoutPadding>
                                </dx:ASPxGaugeControl>
                            </td>
                            <td style="text-align: center;">
                                <dx:ASPxGaugeControl ID="tacDocumental" runat="server" Height="160px" Width="160px" BackColor="Black" LayoutInterval="6" Value="20">
                                    <Gauges>
                                        <dx:CircularGauge Bounds="6, 6, 148, 148" Name="cGauge1">
                                            <scales>
                                <dx:ArcScaleComponent AcceptOrder="0" Center="125, 165" EndAngle="0" MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeType="Circular_Style7_2" MajorTickmark-TextOrientation="LeftToRight" MaxValue="80" MinorTickCount="4" MinorTickmark-ShapeType="Circular_Style7_1" Name="scale1" StartAngle="-180" Value="20" MajorTickCount="7" MajorTickmark-TextOffset="22" MinValue="20" RadiusX="83" RadiusY="83">
                                </dx:ArcScaleComponent>
                            </scales>
                                            <backgroundlayers>
                                <dx:ArcScaleBackgroundLayerComponent AcceptOrder="-1000" ArcScale="" Name="bg1" ScaleID="scale1" ShapeType="CircularHalf_Style7" ZOrder="1000" ScaleCenterPos="0.5, 0.815" Size="250, 154" />
                            </backgroundlayers>
                                            <effectlayers>
                                                <dx:ArcScaleEffectLayerComponent AcceptOrder="1000" ArcScale="" Name="effect1" ScaleID="scale1" Shader="&lt;ShaderObject Type=&quot;Opacity&quot; Data=&quot;Opacity[0.75]&quot;/&gt;" ShapeType="CircularFull_Style7" Size="230, 110" ZOrder="-1000" />
                                            </effectlayers>
                                            <needles>
                                <dx:ArcScaleNeedleComponent AcceptOrder="50" ArcScale="" EndOffset="-25" Name="needle1" ScaleID="scale1" ShapeType="CircularFull_Style7" ZOrder="-50" StartOffset="-21" />
                            </needles>
                                        </dx:CircularGauge>
                                    </Gauges>
                                    <LayoutPadding All="6" Left="6" Top="6" Right="6" Bottom="6"></LayoutPadding>
                                </dx:ASPxGaugeControl>
                            </td>
                            <td style="text-align: center;">
                                <dx:ASPxGaugeControl ID="tacPlad" runat="server" Height="160px" Width="160px" BackColor="Black" LayoutInterval="6" Value="22">
                                    <Gauges>
                                        <dx:CircularGauge Bounds="6, 6, 148, 148" Name="circularGauge1">
                                            <scales>
                                <dx:ArcScaleComponent AcceptOrder="0" AppearanceTickmarkText-Font="Tahoma, 9pt" AppearanceTickmarkText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#484E5A&quot;/&gt;" Center="125, 165" EndAngle="0" MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeOffset="-13" MajorTickmark-ShapeType="Circular_Style16_1" MajorTickmark-TextOrientation="LeftToRight" MaxValue="100" MinorTickCount="4" MinorTickmark-ShapeOffset="-9" MinorTickmark-ShapeType="Circular_Style16_2" Name="scale1" StartAngle="-180" Value="22" MajorTickCount="6" RadiusX="98" RadiusY="98">
                                    <ranges>
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#9EC968&quot;/&gt;" EndThickness="14" EndValue="33" Name="Range0" ShapeOffset="0" StartThickness="14" />
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#FED96D&quot;/&gt;" EndThickness="14" EndValue="66" Name="Range1" ShapeOffset="0" StartThickness="14" StartValue="33" />
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#EF8C75&quot;/&gt;" EndThickness="14" EndValue="100" Name="Range2" ShapeOffset="0" StartThickness="14" StartValue="66" />
                                    </ranges>
                                </dx:ArcScaleComponent>
                            </scales>
                                            <backgroundlayers>
                                <dx:ArcScaleBackgroundLayerComponent AcceptOrder="-1000" ArcScale="" Name="bg" ScaleID="scale1" ShapeType="CircularHalf_Style16" ZOrder="1000" ScaleCenterPos="0.5, 0.695" Size="250, 179" />
                            </backgroundlayers>
                                            <needles>
                                <dx:ArcScaleNeedleComponent AcceptOrder="50" ArcScale="" EndOffset="3" Name="needle" ScaleID="scale1" ShapeType="CircularFull_Style16" ZOrder="-50" />
                            </needles>
                                            <spindlecaps>
                                <dx:ArcScaleSpindleCapComponent AcceptOrder="100" ArcScale="" Name="circularGauge1_SpindleCap1" ScaleID="scale1" ShapeType="CircularFull_Style16" Size="25, 25" ZOrder="-100" />
                            </spindlecaps>
                                        </dx:CircularGauge>
                                    </Gauges>
                                    <LayoutPadding All="6" Left="6" Top="6" Right="6" Bottom="6"></LayoutPadding>
                                </dx:ASPxGaugeControl>
                            </td>
                            <td style="text-align: center;">
                                <dx:ASPxGaugeControl ID="tacSeleccion" runat="server" Height="160px" Width="160px" BackColor="Black" LayoutInterval="6" Value="50">
                                    <Gauges>
                                        <dx:CircularGauge Bounds="6, 6, 148, 148" Name="circularGauge10">
                                            <scales>
                                <dx:ArcScaleComponent AcceptOrder="0" AppearanceTickmarkText-Font="Tahoma, 9pt" Center="125, 170" EndAngle="0" MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeOffset="-6" MajorTickmark-ShapeType="Circular_Style25_1" MajorTickmark-TextOrientation="LeftToRight" MaxValue="100" MinorTickCount="4" MinorTickmark-ShapeOffset="-2" MinorTickmark-ShapeType="Circular_Style25_2" Name="scale1" StartAngle="-180" Value="50" MajorTickCount="6" MajorTickmark-TextOffset="-20" RadiusX="95" RadiusY="95">
                                    <ranges>
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#9EC968&quot;/&gt;" EndThickness="2" EndValue="33" Name="Range0" ShapeOffset="11.5" StartThickness="2" />
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#FFDA80&quot;/&gt;" EndThickness="2" EndValue="66" Name="Range1" ShapeOffset="11.5" StartThickness="2" StartValue="33" />
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#E99D9D&quot;/&gt;" EndThickness="2" EndValue="100" Name="Range2" ShapeOffset="11.5" StartThickness="2" StartValue="66" />
                                    </ranges>
                                </dx:ArcScaleComponent>
                            </scales>
                                            <backgroundlayers>
                                <dx:ArcScaleBackgroundLayerComponent AcceptOrder="-1000" ArcScale="" Name="bg" ScaleID="scale1" ShapeType="CircularHalf_Style25" ZOrder="1000" ScaleCenterPos="0.5, 0.695" Size="250, 179" />
                            </backgroundlayers>
                                            <needles>
                                <dx:ArcScaleNeedleComponent AcceptOrder="50" ArcScale="" EndOffset="13" Name="needle" ScaleID="scale1" ShapeType="CircularFull_Style25" ZOrder="-50" StartOffset="-16.5" />
                            </needles>
                                            <spindlecaps>
                                <dx:ArcScaleSpindleCapComponent AcceptOrder="100" ArcScale="" Name="circularGauge10_SpindleCap1" ScaleID="scale1" ShapeType="CircularFull_Style25" Size="15, 15" ZOrder="-100" />
                            </spindlecaps>
                                        </dx:CircularGauge>
                                    </Gauges>
                                    <LayoutPadding All="6" Left="6" Top="6" Right="6" Bottom="6"></LayoutPadding>
                                </dx:ASPxGaugeControl>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; color: white;">TECNICA</td>
                            <td style="text-align: center; color: white;">MEDICO</td>
                            <td style="text-align: center; color: white;">EJECUCION</td>
                            <td style="text-align: center; color: white;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <dx:ASPxGaugeControl ID="tacTecnica" runat="server" Height="160px" Width="160px" BackColor="Black" LayoutInterval="6" Value="22">
                                    <Gauges>
                                        <dx:CircularGauge Bounds="6, 6, 148, 148" Name="circularGauge5">
                                            <scales>
                                <dx:ArcScaleComponent AcceptOrder="0" AppearanceTickmarkText-Font="Tahoma, 9pt" AppearanceTickmarkText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#3A3832&quot;/&gt;" Center="125, 170" EndAngle="0" MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeOffset="-10" MajorTickmark-ShapeType="Circular_Style20_1" MajorTickmark-TextOrientation="LeftToRight" MaxValue="100" MinorTickCount="4" MinorTickmark-ShapeOffset="-6" MinorTickmark-ShapeType="Circular_Style20_2" Name="scale1" StartAngle="-180" Value="22" Appearance-Brush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#3A3832&quot;/&gt;" Appearance-Width="2" AppearanceScale-Brush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#3A3832&quot;/&gt;" AppearanceScale-Width="2" AppearanceTickmarkText-Spacing="2, 2, 2, 2" MajorTickCount="6" MajorTickmark-TextOffset="-18" RadiusY="98">
                                    <ranges>
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#96C562&quot;/&gt;" EndThickness="5" EndValue="33" Name="Range0" ShapeOffset="6" StartThickness="5" />
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#FCD66B&quot;/&gt;" EndThickness="5" EndValue="66" Name="Range1" ShapeOffset="6" StartThickness="5" StartValue="33" />
                                        <dx:ArcScaleRangeWeb AppearanceRange-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#EA836D&quot;/&gt;" EndThickness="5" EndValue="100" Name="Range2" ShapeOffset="6" StartThickness="5" StartValue="66" />
                                    </ranges>
                                </dx:ArcScaleComponent>
                            </scales>
                                            <backgroundlayers>
                                <dx:ArcScaleBackgroundLayerComponent AcceptOrder="-1000" ArcScale="" Name="bg" ScaleID="scale1" ShapeType="CircularHalf_Style20" ZOrder="1000" ScaleCenterPos="0.5, 0.685" Size="250, 179" />
                            </backgroundlayers>
                                            <needles>
                                <dx:ArcScaleNeedleComponent AcceptOrder="50" ArcScale="" EndOffset="2" Name="needle" ScaleID="scale1" ShapeType="CircularFull_Style20" ZOrder="-50" StartOffset="-39" />
                            </needles>
                                            <spindlecaps>
                                <dx:ArcScaleSpindleCapComponent AcceptOrder="100" ArcScale="" Name="circularGauge5_SpindleCap1" ScaleID="scale1" ShapeType="CircularFull_Style20" Size="10, 10" ZOrder="-100" />
                            </spindlecaps>
                                        </dx:CircularGauge>
                                    </Gauges>
                                    <LayoutPadding All="6" Left="6" Top="6" Right="6" Bottom="6"></LayoutPadding>
                                </dx:ASPxGaugeControl>
                            </td>
                            <td style="text-align: center;">
                                <dx:ASPxGaugeControl ID="tacMedico" runat="server" Height="160px" Width="160px" BackColor="Black" LayoutInterval="6" Value="40">
                                    <Gauges>
                                        <dx:CircularGauge Bounds="6, 6, 148, 148" Name="cGauge1">
                                            <scales>
                                <dx:ArcScaleComponent AcceptOrder="0" AppearanceTickmarkText-Font="Arial Narrow, 11pt, style=Bold" AppearanceTickmarkText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#C0C0FF&quot;/&gt;" Center="125, 165" EndAngle="0" MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeOffset="-9" MajorTickmark-ShapeType="Circular_Style2_2" MajorTickmark-TextOrientation="LeftToRight" MaxValue="100" MinorTickCount="4" MinorTickmark-ShapeType="Circular_Style2_1" Name="scale1" StartAngle="-180" Value="40" MajorTickCount="7" MajorTickmark-AllowTickOverlap="True" MajorTickmark-TextOffset="-22" MinValue="40" RadiusX="91" RadiusY="91">
                                </dx:ArcScaleComponent>
                            </scales>
                                            <backgroundlayers>
                                <dx:ArcScaleBackgroundLayerComponent AcceptOrder="-1000" ArcScale="" Name="bg1" ScaleID="scale1" ShapeType="CircularHalf_Style2" ZOrder="1000" ScaleCenterPos="0.5, 0.72" Size="244, 170" />
                            </backgroundlayers>
                                            <needles>
                                <dx:ArcScaleNeedleComponent AcceptOrder="50" ArcScale="" EndOffset="-6" Name="needle1" ScaleID="scale1" ShapeType="CircularFull_Style2" ZOrder="-50" StartOffset="9" />
                            </needles>
                                            <spindlecaps>
                                <dx:ArcScaleSpindleCapComponent AcceptOrder="100" ArcScale="" Name="cap1" ScaleID="scale1" ShapeType="CircularFull_Style2" Size="24, 24" ZOrder="-100" />
                            </spindlecaps>
                                        </dx:CircularGauge>
                                    </Gauges>
                                    <LayoutPadding All="6" Left="6" Top="6" Right="6" Bottom="6"></LayoutPadding>
                                </dx:ASPxGaugeControl>
                            </td>
                            <td style="text-align: center;">
                                <dx:ASPxGaugeControl ID="tacEjecucion" runat="server" Height="160px" Width="160px" BackColor="Black" LayoutInterval="6" Value="20">
                                    <Gauges>
                                        <dx:CircularGauge Bounds="6, 6, 148, 148" Name="cGauge1">
                                            <scales>
                                <dx:ArcScaleComponent AcceptOrder="0" AppearanceTickmarkText-Font="Tahoma, 12pt" AppearanceTickmarkText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#FF8000&quot;/&gt;" Center="125, 165" EndAngle="0" MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeType="Circular_Style3_4" MajorTickmark-TextOrientation="LeftToRight" MaxValue="80" MinorTickCount="4" MinorTickmark-ShapeType="Circular_Style3_3" Name="scale1" StartAngle="-180" Value="20" MajorTickCount="7" MajorTickmark-TextOffset="-18" MinValue="20" RadiusX="104" RadiusY="104">
                                </dx:ArcScaleComponent>
                            </scales>
                                            <backgroundlayers>
                                <dx:ArcScaleBackgroundLayerComponent AcceptOrder="-1000" ArcScale="" Name="bg1" ScaleID="scale1" ShapeType="CircularHalf_Style3" ZOrder="1000" ScaleCenterPos="0.5, 0.815" Size="244, 152" />
                            </backgroundlayers>
                                            <needles>
                                <dx:ArcScaleNeedleComponent AcceptOrder="50" ArcScale="" EndOffset="-8" Name="needle1" ScaleID="scale1" ShapeType="CircularFull_Style3" ZOrder="-50" />
                            </needles>
                                            <spindlecaps>
                                <dx:ArcScaleSpindleCapComponent AcceptOrder="100" ArcScale="" Name="cap1" ScaleID="scale1" ShapeType="CircularFull_Style3" ZOrder="-100" />
                            </spindlecaps>
                                        </dx:CircularGauge>
                                    </Gauges>
                                    <LayoutPadding All="6" Left="6" Top="6" Right="6" Bottom="6"></LayoutPadding>
                                </dx:ASPxGaugeControl>
                            </td>
                            <td style="text-align: center;">&nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
