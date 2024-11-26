<%@ Page Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprMapaSupervisor.aspx.cs" Inherits="wfip.supervision.sprMapaSupervisor" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <fieldset>
        <legend>DETALLE DE MOVIMIENTOS </legend>
         <br /><br />
        <div>
            <table style ="width:100%">
                <tr>
                    <td>
                       <br /><br /><dx:ASPxLabel runat="server" ID="lblMesa" Font-Bold="true" />
                    </td>
                </tr>
            </table>
             <table  style ="width:100%">
                 <tr>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                    <td style="text-align:right">
                        <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                          <img src="../img/excel.png"/>
                        </asp:LinkButton>
                    </td>
                 </tr>
                <tr>
                    <td style ="width:100%; vertical-align:top">
                    <div style="max-height:400px; overflow:auto">
                        <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server"
                            AutoGenerateColumns ="False" Width ="100%"  EnableTheming="True" Theme="iOS"  
                            Font-Size ="10px" KeyFieldName="idMesa">
                           <Columns>
                                <dx:GridViewDataTextColumn Caption ="FLUJO"  FieldName="flujo"  VisibleIndex="1" Width="250" />
                                <dx:GridViewDataTextColumn Caption ="NUM MESA"  FieldName="idMesa"  VisibleIndex="2" Visible="false" />
                                <dx:GridViewDataTextColumn Caption ="USUARIOS CONECTADOS" FieldName="Usuarios"  VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption ="TRAMITES NUEVOS" FieldName="TramitesNuevos"  VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption= "# REINGRESOS" FieldName="Reingresos"   VisibleIndex="5"/>
                                <dx:GridViewDataTextColumn Caption ="TOTAL TRAMITES"  FieldName="TramitesTotal"  VisibleIndex="6" />
                           </Columns>
                            <SettingsDetail ShowDetailRow="true" />
                            <SettingsExport ExcelExportMode ="WYSIWYG"></SettingsExport>
                           <Templates>
                               <DetailRow> 
                                   <dx:ASPxGridView ID="dvgdDetalleTramite"  runat="server" ClientInstanceName="dvgdDetalleTramite"   OnInit="dvgdDetalleTramite_Init" KeyFieldName="idTramite" width="100%" EnablePagingGestures="False" AutoGenerateColumns ="False" >
                                       <Columns>
                                            <dx:GridViewDataColumn FieldName="FolioCompuesto" Caption="FOLIO" VisibleIndex="0" Width="180px" />
                                            <dx:GridViewDataColumn FieldName="EstadoMesa" Caption="ESTADO EN MESA" VisibleIndex="1" Width="180px" />
                                            <dx:GridViewDataColumn FieldName="prioridadDesc" Caption="PRIORIDAD" VisibleIndex="2" Width="180px" />
                                            <dx:GridViewDataColumn FieldName="Reingreso" Caption="REINGRESOS" VisibleIndex="3"/>
                                            <dx:GridViewDataColumn FieldName="FechaIngreso" Caption="INGRESO SISTEMA" VisibleIndex="4"/>
                                            <dx:GridViewDataColumn FieldName="Fecha" Caption="INGRESO EN MESA" VisibleIndex="5"/>
                                            <dx:GridViewDataColumn FieldName="FechaFin" Caption="ULTIMO REGISTRO" VisibleIndex="6"/>
                                            <dx:GridViewDataColumn FieldName="Usuario" Caption="USUARIO" VisibleIndex="7"/>
                                            <dx:GridViewDataColumn FieldName="tAtencion" Caption="TIEMPO ATENCION" VisibleIndex="8"/>
                                            <dx:GridViewDataColumn FieldName="tMesa" Caption="TIEMPO MESA" VisibleIndex="9"/>
                                            <dx:GridViewDataColumn FieldName="Contratante" Caption="CONTRATANTE" VisibleIndex="10"/>
                                            <dx:GridViewDataColumn FieldName="Solicitante" Caption="SOLICITANTE" VisibleIndex="11"/>
                                       </Columns>
                                        <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True" ShowGroupPanel="True"/>
                                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                                        <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto"/>
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager" />
                                        <SettingsSearchPanel Visible="true" />
                                        <Styles Header-Wrap="True"/>
                                    </dx:ASPxGridView>
                               </DetailRow>
                           </Templates>
                            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdTramites"></dx:ASPxGridViewExporter>
                    </div>
                    </td>
                </tr>
                 <tr>
                     <td>&nbsp;</td>
                 </tr>
                </table>
        </div>
        </fieldset>
</asp:Content>
