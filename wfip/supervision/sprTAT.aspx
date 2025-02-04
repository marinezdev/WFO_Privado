﻿<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprTAT.aspx.cs" Inherits="wfip.supervision.sprTAT" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend> TAT </legend> 
        <br />
       
            <table style ="width:65%">
                <tr>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde:">
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
                <tr><td colspan="5">&nbsp;</td></tr>
                <tr>
                 <td colspan="5">
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
        <table style="margin-left:auto; margin-right:auto; width:100%">
         <tr>
             <td style="text-align:right">
                <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                   <img src="../img/excel.png"/>
                </asp:LinkButton>
             </td>
         </tr>
          <tr><td>
            <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                 <div id="dvCajaGrilla">
                     <dx:ASPxGridView ID="dvgdReporteTAT" ClientInstanceName="dvgdReporteTAT" runat="server" AutoGenerateColumns ="False" Width ="100%" 
                             style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px">
                           <Columns>
                                <dx:GridViewDataTextColumn Caption= "TRAMITE" FieldName="IdTramite" VisibleIndex="0" Visible="false" />
                                <dx:GridViewDataTextColumn Caption= "TIPO DE PROCESO" FieldName="TipoProceso" VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption= "RAMO" FieldName="Ramo" VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption= "FOLIO TRÁMITE" FieldName="Folio" VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption= "CVE PROMOTORÍA" FieldName="PromotoriaClave" VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption= "PROMOTORÍA" FieldName="PromotoriaNombre" VisibleIndex="5" />
                                <dx:GridViewDataTextColumn Caption= "ESTATUS TRÁMITE" FieldName="Estatus" VisibleIndex="6" />
                                <dx:GridViewDataTextColumn Caption ="RCM TIEMPO TOTAL"  FieldName="RCM_TiempoTotal"  VisibleIndex="7" />
                                <dx:GridViewDataTextColumn Caption ="RCM ULTIMO USUARIO"  FieldName="RCM_UltimoUsuario"  VisibleIndex="8" />
                                <dx:GridViewDataTextColumn Caption ="RCM MOTIVO SUSPENSION"  FieldName="RCM_MotivoSuspension"  VisibleIndex="9" />
                                <dx:GridViewDataTextColumn Caption ="ADM TIEMPO TOTAL"  FieldName="ADM_TiempoTotal"  VisibleIndex="10" />
                                <dx:GridViewDataTextColumn Caption ="ADM ULTIMO USUARIO"  FieldName="ADM_UltimoUsuario"  VisibleIndex="11" />
                                <dx:GridViewDataTextColumn Caption ="ADM MOTIVO SUSPENSION"  FieldName="ADM_MotivoSuspension"  VisibleIndex="12" />
                                <dx:GridViewDataTextColumn Caption ="RD TIEMPO TOTAL"  FieldName="RD_TiempoTotal"  VisibleIndex="13" />
                                <dx:GridViewDataTextColumn Caption ="RD ULTIMO USUARIO"  FieldName="RD_UltimoUsuario"  VisibleIndex="14" />
                                <dx:GridViewDataTextColumn Caption ="RD MOTIVO SUSPENSION"  FieldName="RD_MotivoSuspension"  VisibleIndex="15" />
                                <dx:GridViewDataTextColumn Caption ="PLAD TIEMPO TOTAL"  FieldName="RCM_TiempoTotal"  VisibleIndex="16" />
                                <dx:GridViewDataTextColumn Caption ="PLAD ULTIMO USUARIO"  FieldName="RCM_UltimoUsuario"  VisibleIndex="17" />
                                <dx:GridViewDataTextColumn Caption ="PLAD MOTIVO SUSPENSION"  FieldName="RCM_MotivoSuspension"  VisibleIndex="18" />
                                <dx:GridViewDataTextColumn Caption ="SELEC TIEMPO TOTAL"  FieldName="SELEC_TiempoTotal"  VisibleIndex="19" />
                                <dx:GridViewDataTextColumn Caption ="SELEC ULTIMO USUARIO"  FieldName="SELEC_UltimoUsuario"  VisibleIndex="20" />
                                <dx:GridViewDataTextColumn Caption ="SELEC MOTIVO SUSPENSION"  FieldName="SELEC_MotivoSuspension"  VisibleIndex="21" />
                                <dx:GridViewDataTextColumn Caption ="RT TIEMPO TOTAL"  FieldName="RT_TiempoTotal"  VisibleIndex="22" />
                                <dx:GridViewDataTextColumn Caption ="RT ULTIMO USUARIO"  FieldName="RT_UltimoUsuario"  VisibleIndex="23" />
                                <dx:GridViewDataTextColumn Caption ="RT MOTIVO SUSPENSION"  FieldName="RT_MotivoSuspension"  VisibleIndex="24" />
                                <dx:GridViewDataTextColumn Caption ="LATAM TIEMPO TOTAL"  FieldName="LATAM_TiempoTotal"  VisibleIndex="25" />
                                <dx:GridViewDataTextColumn Caption ="LATAM ULTIMO USUARIO"  FieldName="LATAM_UltimoUsuario"  VisibleIndex="26" />
                                <dx:GridViewDataTextColumn Caption ="LATAM MOTIVO SUSPENSION"  FieldName="LATAM_MotivoSuspension"  VisibleIndex="27" />
                                <dx:GridViewDataTextColumn Caption ="RMED TIEMPO TOTAL"  FieldName="RMED_TiempoTotal"  VisibleIndex="28" />
                                <dx:GridViewDataTextColumn Caption ="RMED ULTIMO USUARIO"  FieldName="RMED_UltimoUsuario"  VisibleIndex="29" />
                                <dx:GridViewDataTextColumn Caption ="RMED MOTIVO SUSPENSION"  FieldName="RMED_MotivoSuspension"  VisibleIndex="30" />
                                <dx:GridViewDataTextColumn Caption ="CMED TIEMPO TOTAL"  FieldName="CMED_TiempoTotal"  VisibleIndex="31" />
                                <dx:GridViewDataTextColumn Caption ="CMED ULTIMO USUARIO"  FieldName="CMED_UltimoUsuario"  VisibleIndex="32" />
                                <dx:GridViewDataTextColumn Caption ="CMED MOTIVO SUSPENSION"  FieldName="CMED_MotivoSuspension"  VisibleIndex="33" />
                                <dx:GridViewDataTextColumn Caption ="CAP TIEMPO TOTAL"  FieldName="CAP_TiempoTotal"  VisibleIndex="34" />
                                <dx:GridViewDataTextColumn Caption ="CAP ULTIMO USUARIO"  FieldName="CAP_UltimoUsuario"  VisibleIndex="35" />
                                <dx:GridViewDataTextColumn Caption ="CAP MOTIVO SUSPENSION"  FieldName="CAP_MotivoSuspension"  VisibleIndex="36" />
                                <dx:GridViewDataTextColumn Caption ="CTRL TIEMPO TOTAL"  FieldName="CTRL_TiempoTotal"  VisibleIndex="37" />
                                <dx:GridViewDataTextColumn Caption ="CTRL ULTIMO USUARIO"  FieldName="CTRL_UltimoUsuario"  VisibleIndex="38" />
                                <dx:GridViewDataTextColumn Caption ="CTRL MOTIVO SUSPENSION"  FieldName="CTRL_MotivoSuspension"  VisibleIndex="39" />
                                <dx:GridViewDataTextColumn Caption ="EJEC TIEMPO TOTAL"  FieldName="EJEC_TiempoTotal"  VisibleIndex="40" />
                                <dx:GridViewDataTextColumn Caption ="EJEC ULTIMO USUARIO"  FieldName="EJEC_UltimoUsuario"  VisibleIndex="41" />
                                <dx:GridViewDataTextColumn Caption ="EJEC MOTIVO SUSPENSION"  FieldName="EJEC_MotivoSuspension"  VisibleIndex="42" />
                                <dx:GridViewDataTextColumn Caption= "TAT INICIAL" FieldName="TATInicial" VisibleIndex="43" />
                                <dx:GridViewDataTextColumn Caption ="TAT PROMOTORÍA"  FieldName="TATPromotoria"  VisibleIndex="44" />
                                <dx:GridViewDataTextColumn Caption= "END TO END 1" FieldName="ENDToEND1" VisibleIndex="45" />
                                <dx:GridViewDataTextColumn Caption ="END TO END 2"  FieldName="ENDToEND2"  VisibleIndex="46" />
                                <dx:GridViewDataTextColumn Caption ="TOTAL SUSPENSIÓN"  FieldName="TotalSuspendido"  VisibleIndex="47" />
                                <dx:GridViewDataTextColumn Caption ="OBSERVACIONES PÚBLICAS"  FieldName="ObservacionesPublicas"  VisibleIndex="48" />
                                <dx:GridViewDataTextColumn Caption ="OBSERVACIONES PRIVADAS"  FieldName="ObservacionesPrivadas"  VisibleIndex="49" />
                                <dx:GridViewDataTextColumn Caption ="CAL TIEMPO TOTAL"  FieldName="CAL_TiempoTotal"  VisibleIndex="50" />
                                <dx:GridViewDataTextColumn Caption ="CAL ULTIMO USUARIO"  FieldName="CAL_UltimoUsuario"  VisibleIndex="51" />
                                <dx:GridViewDataTextColumn Caption ="CAL MOTIVO SUSPENSION"  FieldName="CAL_MotivoSuspension"  VisibleIndex="52" />
                                <dx:GridViewDataTextColumn Caption ="KWIK TIEMPO TOTAL"  FieldName="KWIK_TiempoTotal"  VisibleIndex="53" />
                                <dx:GridViewDataTextColumn Caption ="KWIK ULTIMO USUARIO"  FieldName="KWIK_UltimoUsuario"  VisibleIndex="54" />
                                <dx:GridViewDataTextColumn Caption ="KWIK MOTIVO SUSPENSION"  FieldName="KWIK_MotivoSuspension"  VisibleIndex="55" />
                                
                               </Columns>
                            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <Settings  VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" HorizontalScrollBarMode="Visible" />
                            <SettingsSearchPanel Visible="true" />
                            <Styles Header-Wrap="True"/>
                        </dx:ASPxGridView>                    
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdReporteTAT"></dx:ASPxGridViewExporter>
                <br />
                </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </td></tr>
      </table>
    </fieldset>
</asp:Content>



