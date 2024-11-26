<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteCaducadosR.aspx.cs" Inherits="wfip.supervision.sprReporteCaducadosR" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
<fieldset>
  <legend class="text-center">TRÁMITES CADUCADOS</legend>
  <br /><br />
   <div class="container-fluid">
      <div class="row>">
         <div class="col-sm-4; text-right">
            <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde">
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
             <asp:Button ID="btnFiltrar"   CssClass="btn btn-success" runat="server" Text="Filtrar"/>
         </div>
         <br />
      </div>
      <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
            <div class="row">
               <div class="col-sm-12">
                  <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" AutoGenerateColumns ="False" KeyFieldName="Id" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px">
                     <Columns>
                        <dx:GridViewDataTextColumn Caption="IdTramite" FieldName="Id"  VisibleIndex="0" Visible="false" />
                        <dx:GridViewDataDateColumn Caption="FECHA INGRESO" FieldName="FechaIngreso"  VisibleIndex="1" Width="150" >
                           <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="FOLIO" FieldName="FolioTramite"  VisibleIndex="2" Width="150"/>
                        <dx:GridViewDataTextColumn Caption="RAMO" FieldName="Ramo"  VisibleIndex="3" Width="150" />
                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="Producto"  VisibleIndex="4" Width="150"/>
                        <dx:GridViewDataTextColumn Caption="ESTATUS" FieldName="Estatus"  VisibleIndex="5" visible="false" Width="150"/>
                        <dx:GridViewDataDateColumn Caption="FECHA ESTATUS" FieldName="FechaEstatus"  VisibleIndex="6" Width="150">
                           <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="TIEMPO (HORAS)" FieldName="Tiempo"  VisibleIndex="7" Width="150" />
                        <dx:GridViewDataTextColumn Caption="PROMOTORIA" FieldName="Promotoria"  VisibleIndex="8" Width="150" />
                        <dx:GridViewDataTextColumn Caption="CLAVE AGENTE" FieldName="ClaveAgente"  VisibleIndex="9" Width="150" />
                        <dx:GridViewDataTextColumn Caption ="POLIZA" FieldName="Poliza"  VisibleIndex="10" Width="150"/>
                        <dx:GridViewDataTextColumn Caption ="IDENTIFICADOR" FieldName="prioridad"  VisibleIndex="11" Width="150" />
                     </Columns>
                     <Templates>
                        <DetailRow>
                           <dx:ASPxLabel runat="server" Text='<%# Eval("FolioTramite") %>' Font-Bold="true"/>
                           <br /><br />
                           <dx:ASPxGridView ID="dvgdDetalleCaducados" runat="server" ClientInstanceName="dvgdEstatusTramite" OnInit="dvgdDetalleCaducados_Init" KeyFieldName="Id" width="100%" EnablePagingGestures="False" AutoGenerateColumns ="False" >
                              <Columns>
                                 <dx:GridViewDataColumn FieldName="IdTramite" Caption="TRAMITE" VisibleIndex="1" Visible="false"/>
                                 <dx:GridViewDataColumn FieldName="MesaNombre" Caption="MESA" VisibleIndex="3" />
                                 <dx:GridViewDataColumn FieldName="UsuarioNombre" Caption="USUARIO" VisibleIndex="4" />
                                 <dx:GridViewDataDateColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="5" PropertiesDateEdit-DisplayFormatString="G"/>
                                 <dx:GridViewDataDateColumn FieldName="FechaTermino" Caption="FECHA TERMINO" VisibleIndex="6" PropertiesDateEdit-DisplayFormatString="G"/>
                              </Columns>
                              <Settings ShowFooter="True" />
                              <SettingsPager EnableAdaptivity="true" />
                              <SettingsExport ExcelExportMode ="WYSIWYG"></SettingsExport>
                              <Styles Header-Wrap="True"/>
                           </dx:ASPxGridView>    
                        </DetailRow>
                     </Templates>
                     <Settings  ShowFooter="True" VerticalScrollableHeight="400" ShowGroupFooter="VisibleAlways" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                     <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                     <SettingsPager  Mode="ShowAllRecords"/>
                     <SettingsDetail ShowDetailRow="true" />
                  </dx:ASPxGridView>
                  <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdEstatusTramite"></dx:ASPxGridViewExporter>
               </div>
            </div>
         </ContentTemplate>
         <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
         </Triggers>
      </asp:UpdatePanel>
   </div>
</fieldset>
</asp:Content>
