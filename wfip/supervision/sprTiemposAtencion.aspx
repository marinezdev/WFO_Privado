<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprTiemposAtencion.aspx.cs" Inherits="wfip.supervision.sprTiemposAtencion" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>TIEMPOS DE ATENCIÓN</legend>
        <br />
            <table style ="width:65%">
                <tr>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"   Width="190" Caption="Desde:">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td>
                    <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td>    
                    <div style="vertical-align:bottom">
                    <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Filtrar" />
                    </div>
                </td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                 <td>
                    <asp:UpdateProgress ID="updProgress"  runat="server">
                        <ProgressTemplate>           
                         <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
                            <div style="padding: 10px;position:fixed;top:45%;left:50%;background-color:white;width:170px;height:45px " > 
                             <table style="background-size:0">
                                <tr>
                                 <td>
                                     <img alt=" " src="/img/spinner.gif" />
                                 </td>
                                <td>
                                    <span style="font-size:16px">&nbsp;Procesando...</span>
                                </td>
                                </tr>
                             </table>
                           </div> 
                         </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                 </td>
                </tr>
        </table>
        <table style="width:100%">
             <tr>
                 <td style="text-align:right">
                    <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                       <img src="../img/excel.png"/>
                    </asp:LinkButton>
                 </td>
                </tr>
        </table>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                 <div id="dvCajaGrilla">
                    <dx:ASPxGridView ID="dvgdMesa"  ClientInstanceName="dvgdMesa" runat="server" AutoGenerateColumns="False" Width="100%" EnableTheming="True" Theme="iOS" Font-Size="10px">
                    <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdMesa"></dx:ASPxGridViewExporter>
                <br />
                </div>
                
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
    </fieldset>
</asp:Content>

