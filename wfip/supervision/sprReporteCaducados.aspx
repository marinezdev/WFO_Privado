<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteCaducados.aspx.cs" Inherits="wfip.supervision.sprReporteCaducados" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
    <fieldset>
        <legend>TRÁMITES CADUCADOS</legend>
        <br /><br />
        <table style ="width:50%">
                <tr>
                <td>
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde">
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
                   <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Filtrar"/>
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
        <table style="width:100%;margin-left:auto;margin-right:auto">
            <tr>
             <td style="text-align:right">
                <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                   <img src="../img/excel.png"/>
                </asp:LinkButton>
             </td>
         </tr>
        </table>
        <table style="width:97%; margin-left:auto; margin-right:auto">
        <tr>
        <td>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" AutoGenerateColumns ="False" KeyFieldName="Id" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px">
            <Columns>
              <dx:GridViewDataTextColumn Caption="IdTramite" FieldName="Id"  VisibleIndex="0" Visible="false" />
              <dx:GridViewDataDateColumn Caption="FECHA INGRESO" FieldName="FechaIngreso"  VisibleIndex="1" >
                <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
              </dx:GridViewDataDateColumn>
              <dx:GridViewDataTextColumn Caption="FOLIO" FieldName="FolioTramite"  VisibleIndex="2" />
              <dx:GridViewDataTextColumn Caption="RAMO" FieldName="Ramo"  VisibleIndex="3" />
              <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="Producto"  VisibleIndex="4" />
              <dx:GridViewDataTextColumn Caption="ESTATUS" FieldName="Estatus"  VisibleIndex="5" visible="false"/>
              <dx:GridViewDataDateColumn Caption="FECHA ESTATUS" FieldName="FechaEstatus"  VisibleIndex="6">
                <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
              </dx:GridViewDataDateColumn>
              <dx:GridViewDataTextColumn Caption="TIEMPO (HORAS)" FieldName="Tiempo"  VisibleIndex="7" />
              <dx:GridViewDataTextColumn Caption="PROMOTORIA" FieldName="Promotoria"  VisibleIndex="8" />
              <dx:GridViewDataTextColumn Caption="CLAVE AGENTE" FieldName="ClaveAgente"  VisibleIndex="9" />
              <dx:GridViewDataTextColumn Caption ="POLIZA" FieldName="Poliza"  VisibleIndex="10" />
              <dx:GridViewDataTextColumn Caption ="IDENTIFICADOR" FieldName="prioridad"  VisibleIndex="11" />
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
                  <SettingsSearchPanel Visible="true" />
                  <Styles Header-Wrap="True"/>
                </dx:ASPxGridView>    
              </DetailRow>
            </Templates>
            <Settings  ShowFooter="True" ShowGroupFooter="VisibleAlways"  />
            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
            <SettingsPager  Mode="ShowAllRecords"/>
            <SettingsDetail ShowDetailRow="true" />
            <SettingsSearchPanel Visible="true" />
           </dx:ASPxGridView>
           <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdEstatusTramite"></dx:ASPxGridViewExporter>
        </ContentTemplate>
        <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
    </td>
    </tr>
    </table>
    </fieldset>
</asp:Content>