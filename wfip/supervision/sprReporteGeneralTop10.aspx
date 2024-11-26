<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteGeneralTop10.aspx.cs" Inherits="wfip.sprReporteGeneralTop10" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend> TOP 10 </legend>
         <br /><br />
        <div>
            <table style="width:50%">
                <tr>
                    <td>                    
                     <dx:aspxdateedit ID="CalDesdeTE" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties> 
                      </dx:aspxdateedit>
                    </td>
                    <td>
                     <dx:aspxdateedit ID="CalHastaTE" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                     </dx:aspxdateedit>
                    </td>
                    <td>
                        <asp:Button ID="btnFiltroTP" runat="server" Text="Filtrar" CssClass="boton"   />
                    </td>
                </tr>
         </table>
         <table style="width:100%">
           <tr>
             <td style="text-align:right;width:48%">
               <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                  <img src="../img/excel.png"/>
               </asp:LinkButton>
             </td>
            <td style="width:52%">
                 &nbsp;
            </td>
          </tr>
         </table>
         <asp:UpdatePanel ID="UPTopProm" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
             <table  style ="width:100%">
                <!--<tr><td colspan ="2" style ="font-size:14px""><br />Top 10 Ejecutados</td></tr>-->
                <tr>
                    <td style ="width:50%; vertical-align:top">
                        <dx:ASPxGridView ID="dvgdPromotorias" ClientInstanceName="dvgdPromotorias" runat="server" AutoGenerateColumns ="False" Width ="95%" 
                            style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px">
                           <Columns>
                                <dx:GridViewDataTextColumn Caption ="CVE PROMOTORIA"  FieldName="Promotoria"  VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption ="PROMOTORIA" FieldName="Nombre"  VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption= "ZONA" FieldName="Zona" VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption= "EJECUTADOS" FieldName="NumTramitesEje" VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption= "SUSPENDIDOS" FieldName="NumTramitesSus" VisibleIndex="5" />
                            </Columns>
                            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>                    
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdPromotorias"></dx:ASPxGridViewExporter>
                    </td>
                    <td>
                        <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="600px" Theme ="SoftOrange">
                            <Titles>
                                <dx:ChartTitle Font="Arial,12pt" Text="TOP 10 EJECUTADOS" />
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
                </table>
             </ContentTemplate>
             <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltroTP" EventName="Click" />
             </Triggers>
            </asp:UpdatePanel> 
            <table style="width:50%">
                <tr>
                    <td>                    
                     <dx:aspxdateedit ID="CalDesdeTS" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties> 
                      </dx:aspxdateedit>
                    </td>
                    <td>
                     <dx:aspxdateedit ID="CalHastaTS" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                     </dx:aspxdateedit>
                    </td>
                    <td>
                        <asp:Button ID="btnFiltroTR" runat="server" Text="Filtrar"  CssClass="boton"   />
                    </td>
                </tr>
                
            </table>
            <table style="width:100%">
              <tr>
                 <td style="text-align:right;width:48%">
                   <asp:LinkButton ID="lnkExportSuspend" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                      <img src="../img/excel.png"/>
                   </asp:LinkButton>
                 </td>
                 <td style="width:52%">&nbsp;</td>
              </tr>
            </table>
            <asp:UpdatePanel ID="UPTopSus" runat="server"
             UpdateMode="Conditional">
             <ContentTemplate>
                <table>
                    <tr>
                        <td style ="width:50%; vertical-align:top">
                            <dx:ASPxGridView ID="dgvsuspendidos" ClientInstanceName="dgvsuspendidos" runat="server" AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"   >
                               <Columns>
                                    <dx:GridViewDataTextColumn Caption ="CVE PROMOTORIA"  FieldName="Promotoria"  VisibleIndex="1" />
                                    <dx:GridViewDataTextColumn Caption ="PROMOTORIA" FieldName="Nombre"  VisibleIndex="2" />
                                    <dx:GridViewDataTextColumn Caption= "ZONA" FieldName="Zona" VisibleIndex="3" />
                                    <dx:GridViewDataTextColumn Caption= "SUSPENDIDOS" FieldName="NumTramitesSus" VisibleIndex="4" />
                                    <dx:GridViewDataTextColumn Caption= "EJECUTADOS" FieldName="NumTramitesEje" VisibleIndex="5" />
                               </Columns>
                                <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                                <SettingsPager  Mode="ShowAllRecords"/>
                                <SettingsSearchPanel Visible="true" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportSuspendidos" runat="server" GridViewID="dgvsuspendidos"></dx:ASPxGridViewExporter>
                        </td>
                        <td>
                            <dx:WebChartControl ID="dxChtSuspendidos" runat="server" CrosshairEnabled="True" Height="300px" Width="600px" Theme ="RedWine">
                                <Titles>
                                    <dx:ChartTitle Font="Arial,12pt" Text="TOP 10 SUSPENDIDOS" />
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
                                    <dx:Series Name="dxsreSuspendidos" ></dx:Series>
                                </SeriesSerializable>
                            </dx:WebChartControl>
                        </td>
                    </tr>
                </table>
             </ContentTemplate>
             <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltroTR" EventName="Click" />
             </Triggers>
             </asp:UpdatePanel>
            </div>
            
        <div>
            
        </div>
    </fieldset>
</asp:Content>
