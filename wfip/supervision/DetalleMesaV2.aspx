<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/Supervision.Master" AutoEventWireup="true" CodeBehind="DetalleMesaV2.aspx.cs" Inherits="wfip.supervision.DetalleMesaV2" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <fieldset>
        <legend>DETALLE POR MESA (SÁBANA) </legend>
         <br /><br />
        <span> LA GENERACIÓN DE ESTE REPORTE PUEDE DURAR MÁS DE UN MINUTO</span>
        <br /><br />
        <div>
            <table style ="width:50%">
                <tr>
                    <td>                    
                     <dx:aspxdateedit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"   Width="190" Caption="Desde:">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties> 
                      </dx:aspxdateedit>
                    </td>
                    <td>
                     <dx:aspxdateedit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                     </dx:aspxdateedit>
                    </td>
                    <td>
                        <asp:Button ID="btnFiltroMes"  CssClass="boton" runat="server" Text="Filtrar"/>
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
         <asp:UpdatePanel ID="DetalleTramites" runat="server" UpdateMode="Conditional"> 
             <ContentTemplate>
             <table  style ="width:100%">
                <tr>
                    <td style ="width:100%; vertical-align:top">
                        <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server"
                            AutoGenerateColumns ="False" Width ="100%"  EnableTheming="True" Theme="iOS"  
                            Font-Size ="10px" KeyFieldName="IdTramite">
                           <Columns>
                                <dx:GridViewDataTextColumn Caption ="Tramite"  FieldName="IdTramite"  VisibleIndex="1" Visible="false" />
                                <dx:GridViewDataTextColumn Caption ="FOLIO" FieldName="NumPoliza"  VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption ="CVE PROMOTORIA"  FieldName="PromotoriaClave"  VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption ="PROMOTORIA"  FieldName="PromotoriaNombre"  VisibleIndex="4">
                                  <CellStyle Wrap="True"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption ="CONTRATANTE" FieldName="Contratante"  VisibleIndex="5">
                                  <CellStyle Wrap="True"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption= "ESTATUS" FieldName="EstatusActual"   VisibleIndex="6"/>
                                <dx:GridViewDataTextColumn Caption= "FECHA ULTIMO MOV" FieldName="FechaEstatusActual" VisibleIndex="7" />
                           </Columns>
                           <Templates>
                               <DetailRow>
                                   <dx:ASPxLabel runat="server" Font-Bold="true" Text="Trámite: " />
                                   <dx:ASPxLabel runat="server" Text='<%#Eval("NumPoliza") %>' Font-Bold="true" />
                                    <br /><br />
                                   <dx:ASPxGridView ID="dvgdDetalleTramite" runat="server" ClientInstanceName="dvgdDetalleTramite"   OnInit="dvgdDetalleTramite_Init" KeyFieldName="idTramite" width="100%" EnablePagingGestures="False" AutoGenerateColumns ="False"  >
                                       <Columns>
                                            <dx:GridViewDataColumn FieldName="idTramite" Caption="TRAMITE" VisibleIndex="1" Visible="false"/>
                                            <dx:GridViewDataColumn FieldName="idMesa" Caption="NUM MESA" VisibleIndex="2" Visible="false"/>
                                            <dx:GridViewDataColumn FieldName="MesaNombre" Caption="MESA" VisibleIndex="3"/>
                                            <dx:GridViewDataColumn FieldName="usuario" Caption="USUARIO" VisibleIndex="4"/>
                                            <dx:GridViewDataTextColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="5"/>
                                            <dx:GridViewDataTextColumn FieldName="FechaTermino" Caption="FECHA TERMINO" VisibleIndex="6"/>
                                            <dx:GridViewDataColumn FieldName="EstadoNombre" Caption="ESTATUS MESA" VisibleIndex="7"/>
                                            <dx:GridViewDataColumn FieldName="Observacion" Caption="COMENTARIO PÚBLICO" VisibleIndex="8" Width="200"/>
                                            <dx:GridViewDataColumn FieldName="ObservacionPrivada" Caption="COMENTARIO PRIVADO" VisibleIndex="9" Width="200"/>
                                        </Columns>
                                        <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True" ShowGroupPanel="True"/>
                                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" HorizontalScrollBarMode="Visible" />
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager"/>
                                        <SettingsSearchPanel Visible="true" />
                                    </dx:ASPxGridView>
                               </DetailRow>
                           </Templates>
                            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                            <SettingsSearchPanel Visible="true" />
                            <Styles Header-Wrap="True"/>
                        </dx:ASPxGridView>

                        <dx:ASPxGridView  ID="dvgdMesa" ClientInstanceName="dvgdMesa" runat="server" AutoGenerateColumns="False" Width="100%" EnableTheming="True" Theme="iOS" Font-Size="10px" Visible="false">
                        <Styles Header-Wrap="True" />   
                        <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <Settings HorizontalScrollBarMode="Auto" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdMesa"></dx:ASPxGridViewExporter>
                    </td>
                </tr>
                 <tr>
                     <td>&nbsp;</td>
                 </tr>
                </table>
             </ContentTemplate>
            <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltroMes"  EventName="Click" />
             </Triggers>
            </asp:UpdatePanel> 
        </div>
        </fieldset>
</asp:Content>
