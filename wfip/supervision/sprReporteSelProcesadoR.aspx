<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master"  AutoEventWireup="true" CodeBehind="sprReporteSelProcesadoR.aspx.cs" Inherits="wfip.supervision.sprReporteSelProcesadoR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<fieldset>
   <legend class="text-center"> PROCESABLE </legend>
   <br />
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
             <asp:Button ID="btnFiltrar"  CssClass="btn btn-success" runat="server" Text="Filtrar" />
         </div>
      </div>
      <br />
          <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
                 <div class="row">
                    <div class="col-sm-12">
                       <dx:ASPxGridView  ID="rptProcesable" ClientInstanceName="rptProcesable" runat="server" AutoGenerateColumns="True" Width="100%" EnableTheming="True" Theme="iOS" Font-Size="10px">
                          <Styles Header-Wrap="True" />   
                          <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                          <SettingsPager Mode="ShowAllRecords" />
                          <Settings HorizontalScrollBarMode="Auto" />
                       </dx:ASPxGridView>
                       <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="rptProcesable"></dx:ASPxGridViewExporter>
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