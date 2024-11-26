<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprDetallePromotoriaR.aspx.cs" Inherits="wfip.supervision.sprDetallePromotoriaR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<fieldset>
   <legend class="text-center"> DETALLE PROMOTORIA </legend>
   <div class="container-fluid">
      <div class="row>">
         <div class="col-sm-4; text-right">
            <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"   Width="190" Caption="Desde:">
               <TimeSectionProperties Adaptive="true">
                   <TimeEditProperties EditFormatString="hh:mm tt" />
               </TimeSectionProperties>
               <CalendarProperties>
                   <FastNavProperties DisplayMode="Inline" />
               </CalendarProperties>
            </dx:ASPxDateEdit>
            <br />
         </div>
         <div class="col-sm-4; text-right">
            <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
               <TimeSectionProperties Adaptive="true">
                   <TimeEditProperties EditFormatString="hh:mm tt" />
               </TimeSectionProperties>
               <CalendarProperties>
                   <FastNavProperties DisplayMode="Inline" />
               </CalendarProperties>
            </dx:ASPxDateEdit>
         </div>
         <div class="col-sm-4; text-right">
             <asp:Button ID="btnFiltro" runat="server" Text="Filtrar"  CssClass="btn btn-success" />
         </div>
      </div>
      <asp:UpdatePanel ID="DetallePromotorias" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
             <div class="row">
                <div class="col-sm-12">
                   <dx:ASPxGridView ID="dvgdResumenPromotoria" KeyFieldName="idPromotoria" ClientInstanceName ="dvgdResumenPromotoria" runat="server" AutoGenerateColumns ="False" Width ="95%" EnableTheming="True" Theme="iOS" Font-Size ="10px">
                       <Columns>
                          <dx:GridViewDataTextColumn Caption ="NUM. PROMOTORÍA"  FieldName="idPromotoria"  VisibleIndex="0" Width="150" Visible="false"/>
                          <dx:GridViewDataTextColumn Caption ="CVE. PROMOTORÍA"  FieldName="PromotoriaClave"  VisibleIndex="1" Width="150"/>
                          <dx:GridViewDataTextColumn Caption= "PROMOTORÍA" FieldName="Promotoria" VisibleIndex="2" Width="200">
                             <CellStyle Wrap="True"></CellStyle>
                          </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption= "EN TRÁMITE" FieldName="Registro" VisibleIndex="3" Width="150" />
                          <dx:GridViewDataTextColumn Caption ="PROCESO"  FieldName="Proceso"  VisibleIndex="4" />
                          <dx:GridViewDataTextColumn Caption= "HOLD" FieldName="Hold" VisibleIndex="5" />
                          <dx:GridViewDataTextColumn Caption= "RECHAZO" FieldName="Rechazo" VisibleIndex="6" />
                          <dx:GridViewDataTextColumn Caption ="SUSPENDIDO"  FieldName="Suspendido"  VisibleIndex="7" Width="150" />
                          <dx:GridViewDataTextColumn Caption ="EJECUCIÓN"  FieldName="Ejecucion"  VisibleIndex="8" />
                          <dx:GridViewDataTextColumn Caption= "TOTAL" FieldName="Total" VisibleIndex="9" />
                        </Columns>
                        <Templates>
                            <DetailRow>
                               <dx:ASPxLabel runat="server" Text='<%# Eval("Promotoria") %>' Font-Bold="true"/>
                               <br /><br />
                               <dx:ASPxGridView ID="dvgdDetallePromotoria" runat="server" ClientInstanceName="dvgdResumenPromotoria" OnBeforePerformDataSelect="dvgdDetallePromotoria_BeforePerformDataSelect"  OnInit="dvgdDetallePromotoria_Init" KeyFieldName="idTramite" width="95%" EnablePagingGestures="False" AutoGenerateColumns ="False" >
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
                               </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true"  />
                        <SettingsPager  Mode="ShowAllRecords"/>
                        <SettingsDetail ShowDetailRow="true" />
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                        <SettingsSearchPanel Visible="true" />
                        <Settings HorizontalScrollBarMode="Visible" />
                   </dx:ASPxGridView>                    
                   <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdDetallePromotoria"></dx:ASPxGridViewExporter>
                </div>
             </div>
          </ContentTemplate>
          <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltro" EventName="Click" />
          </Triggers>
      </asp:UpdatePanel>
    </div>
</fieldset>
</asp:Content>