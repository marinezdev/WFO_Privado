<%@ Page Title="" Language="C#"  MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteProductividadR.aspx.cs" Inherits="wfip.supervision.sprReporteProductividadR" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<script type="text/javascript">
  var textSeparator = ";";
  function updateText() {
    var selectedItems = checkListBox.GetSelectedItems();
    checkComboBox.SetText(getSelectedItemsText(selectedItems));
  }
  function synchronizeListBoxValues(dropDown, args) {
    checkListBox.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = getValuesByTexts(texts);
    checkListBox.SelectValues(values);
    updateText(); // for remove non-existing texts
  }
  function getSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
         texts.push(items[i].text);
    return texts.join(textSeparator);
  }
  function getValuesByTexts(texts) {
    var actualValues = [];
    var item;
    for(var i = 0; i < texts.length; i++) {
    item = checkListBox.FindItemByText(texts[i]);
    if(item != null)
       actualValues.push(item.value);
  }
  return actualValues;
  }
</script>
<fieldset>
   <legend class="text-center"> PRODUCTIVIDAD </legend> 
   <br />
   <div class="container-fluid">
      <div class="row>">
         <div class="col-sm-3; text-right">
            <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde:">
               <TimeSectionProperties Adaptive="true">
                  <TimeEditProperties EditFormatString="hh:mm tt" />
               </TimeSectionProperties>
               <CalendarProperties>
                  <FastNavProperties DisplayMode="Inline" />
               </CalendarProperties>
            </dx:ASPxDateEdit>
            <br />
         </div>
         <div class="col-sm-3; text-right">
            <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
               <TimeSectionProperties Adaptive="true">
                   <TimeEditProperties EditFormatString="hh:mm tt" />
               </TimeSectionProperties>
               <CalendarProperties>
                  <FastNavProperties DisplayMode="Inline" />
               </CalendarProperties>
            </dx:ASPxDateEdit>
            <br />
         </div>
         <div class="col-sm-3; text-right">
            <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="cmbUsuarios" Width="285px" runat="server" AnimationType="None" Theme="iOS" Caption="Usuario">
               <DropDownWindowStyle BackColor="#A5CE4E" />
               <DropDownWindowTemplate>
                  <dx:ASPxListBox Width="100%" ID="listUsuario" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" runat="server" Height="200" EnableSelectAll="true" OnInit="listUsuario_Init">
                     <FilteringSettings ShowSearchUI="true"/>
                     <Border BorderStyle="None" />
                     <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                     <ClientSideEvents SelectedIndexChanged="updateText" />
                  </dx:ASPxListBox>
                  <table style="width: 100%">
                    <tr>
                       <td style="padding: 4px">
                          <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Cerrar" style="float: right">
                             <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                          </dx:ASPxButton>
                       </td>
                    </tr>
                  </table>
               </DropDownWindowTemplate>
               <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
            </dx:ASPxDropDownEdit>
            <br />
         </div>
         <div class="col-sm-3; text-right">
            <asp:Button ID="btnFiltrar" CssClass="btn btn-success" runat="server" Text="Filtrar" />
         </div>
      </div>
      <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
            <div class="row">
               <div class="col-sm-12">
                  <dx:ASPxGridView ID="dvgdProductividad" ClientInstanceName="dvgdProductividad" runat="server" AutoGenerateColumns ="False" Width ="100%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"  KeyFieldName="mesaNombre">
                     <Columns>
                        <dx:GridViewDataTextColumn Caption= "OPERADOR" FieldName="operador" VisibleIndex="0" Width="150" />
                        <dx:GridViewDataTextColumn Caption= "MESA" FieldName="mesaNombre" VisibleIndex="1" Width="120"/>
                        <dx:GridViewDataTextColumn Caption= "ACEPTADOS" FieldName="aceptado" VisibleIndex="2" Width="125" />
                        <dx:GridViewDataTextColumn Caption= "RECHAZADOS" FieldName="rechazo" VisibleIndex="4" Width="125" />
                        <dx:GridViewDataTextColumn Caption= "EN TRÁMITE" FieldName="tramite" VisibleIndex="5" Width="125" />
                        <dx:GridViewDataTextColumn Caption= "EN PROCESO" FieldName="proceso" VisibleIndex="6" Width="125" />
                        <dx:GridViewDataTextColumn Caption= "EN PAUSA" FieldName="pausa" VisibleIndex="7" Width="150" />
                        <dx:GridViewDataTextColumn Caption ="TOTAL TRÁMITES"  FieldName="totalTramites"  VisibleIndex="8" Width="150" />
                        <dx:GridViewDataTextColumn Caption ="TIEMPO TOTAL"  FieldName="tiempo"  VisibleIndex="9" Width="150"/>
                     </Columns>
                     <Templates>
                         <DetailRow>
                            <dx:ASPxLabel runat="server" Text='<%# Eval("mesaNombre") %>' Font-Bold="true"/>
                            <br /><br />
                            <dx:ASPxGridView ID="dvgdDetalleProducividad" runat="server" ClientInstanceName="dvgdProductividad" OnInit="dvgdDetalleProducividad_Init" KeyFieldName="mesaNombre" width="100%" EnablePagingGestures="False" AutoGenerateColumns ="False" >
                               <Columns>
                                  <dx:GridViewDataColumn FieldName="IdTramite" Caption="TRAMITE" VisibleIndex="0" Visible="false"/>
                                  <dx:GridViewDataColumn FieldName="FolioCompuesto" Caption="FOLIO" VisibleIndex="1" />
                                  <dx:GridViewDataColumn FieldName="EstadoNombre" Caption="ESTATUS" VisibleIndex="2" />
                                  <dx:GridViewDataDateColumn FieldName="FechaInicio" Caption="FECHA INICIO" VisibleIndex="3" PropertiesDateEdit-DisplayFormatString="G"/>
                                  <dx:GridViewDataDateColumn FieldName="FechaTermino" Caption="FECHA TERMINO" VisibleIndex="4" PropertiesDateEdit-DisplayFormatString="G"/>
                                  <dx:GridViewDataColumn FieldName="tiempo" Caption="TIEMPO" VisibleIndex="5" />
                               </Columns>
                               <Settings ShowFooter="True" />
                               <SettingsPager EnableAdaptivity="true" />
                               <SettingsExport ExcelExportMode ="WYSIWYG"></SettingsExport>
                               <Styles Header-Wrap="True"/>
                            </dx:ASPxGridView>
                         </DetailRow>
                      </Templates>
                     <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                     <SettingsPager  Mode="ShowAllRecords"/>
                     <Settings  VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" HorizontalScrollBarMode="Visible" />
                     <SettingsSearchPanel Visible="true" />
                     <SettingsDetail ShowDetailRow="true" />
                  </dx:ASPxGridView>                    
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

