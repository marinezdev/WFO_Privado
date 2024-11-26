<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprDetallePromotoria.aspx.cs" Inherits="wfip.supervision.sprDetallePromotoria" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend> DETALLE PROMOTORIA </legend>
             <br /><br />
        <div>
            <table style="width:50%">
                <tr>
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
                    <td >
                    <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" CssClass="boton"  />
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
        </div>
        <asp:UpdatePanel ID="DetallePromotorias" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table  style ="width:100%">
                <tr>
                    <td style ="vertical-align:top" colspan="2">
                        <dx:ASPxGridView ID="dvgdResumenPromotoria" KeyFieldName="idPromotoria"  
                            ClientInstanceName ="dvgdResumenPromotoria" runat="server" AutoGenerateColumns ="False" Width ="100%" EnableTheming="True" Theme="iOS" Font-Size ="10px">
                           <Columns>
                                <dx:GridViewDataTextColumn Caption ="NUM. PROMOTORÍA"  FieldName="idPromotoria"  VisibleIndex="0" Width="150" Visible="false"/>
                                <dx:GridViewDataTextColumn Caption ="CVE. PROMOTORÍA"  FieldName="PromotoriaClave"  VisibleIndex="0" Width="150"/>
                                <dx:GridViewDataTextColumn Caption= "PROMOTORÍA" FieldName="Promotoria" VisibleIndex="1" Width="150">
                                  <CellStyle Wrap="True"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption= "EN TRÁMITE" FieldName="Registro" VisibleIndex="2" Width="150" />
                                <dx:GridViewDataTextColumn Caption ="PROCESO"  FieldName="Proceso"  VisibleIndex="3" Width="150" />
                                <dx:GridViewDataTextColumn Caption= "HOLD" FieldName="Hold" VisibleIndex="4" Width="150" />
                                <dx:GridViewDataTextColumn Caption= "RECHAZO" FieldName="Rechazo" VisibleIndex="5" Width="150" />
                                <dx:GridViewDataTextColumn Caption ="SUSPENDIDO"  FieldName="Suspendido"  VisibleIndex="6" Width="150" />
                                <dx:GridViewDataTextColumn Caption ="EJECUCIÓN"  FieldName="Ejecucion"  VisibleIndex="7" Width="150" />
                                <dx:GridViewDataTextColumn Caption ="REV PROMOTORÍA"  FieldName="RevPromotoria"  VisibleIndex="8" Width="150" />
                                <dx:GridViewDataTextColumn Caption ="CANCELADO"  FieldName="Cancelado"  VisibleIndex="9" Width="150" />
                                <dx:GridViewDataTextColumn Caption= "TOTAL" FieldName="Total" VisibleIndex="10" Width="150" />
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxLabel runat="server" Text='<%# Eval("Promotoria") %>' Font-Bold="true"/>
                                    <br /><br />
                                    <dx:ASPxGridView ID="dvgdDetallePromotoria" runat="server" ClientInstanceName="dvgdResumenPromotoria" OnBeforePerformDataSelect="dvgdDetallePromotoria_BeforePerformDataSelect"  OnInit="dvgdDetallePromotoria_Init" KeyFieldName="idTramite" width="100%" EnablePagingGestures="False" AutoGenerateColumns ="False" >
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="idTramite" Caption="TRAMITE" VisibleIndex="1" Visible="false"/>
                                            <dx:GridViewDataColumn FieldName="FolioCompuesto" Caption="FOLIO" VisibleIndex="2" />
                                            <dx:GridViewDataDateColumn FieldName="FechaRegistro" Caption="FECHA REGISTRO" VisibleIndex="3" PropertiesDateEdit-DisplayFormatString="G"/>
                                            <dx:GridViewDataDateColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="4" PropertiesDateEdit-DisplayFormatString="G"/>
                                            <dx:GridViewDataColumn FieldName="UsuarioNombre" Caption="USUARIO" VisibleIndex="5"/>
                                            <dx:GridViewDataColumn FieldName="MesaNombre" Caption="MESA" VisibleIndex="6"/>
                                            <dx:GridViewDataColumn FieldName="EstadoTramite" Caption="ESTADO" VisibleIndex="7"/>
                                        </Columns>
                                        <Settings ShowFooter="True" />
                                        <SettingsPager EnableAdaptivity="true" />
                                        <SettingsExport ExcelExportMode ="WYSIWYG"></SettingsExport>
                                        <Styles Header-Wrap="True"/>
                                        <SettingsSearchPanel Visible="true" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true"  />
                            <SettingsPager  mode="ShowPager"/>
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" HorizontalScrollBarMode="Auto" />
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>                    
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdDetallePromotoria"></dx:ASPxGridViewExporter>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltro" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>