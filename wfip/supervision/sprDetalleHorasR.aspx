<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprDetalleHorasR.aspx.cs" Inherits="wfip.supervision.sprDetalleHorasR" %>
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
   <legend class="text-center">DETALLE DE HORAS</legend>
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
             <asp:Button ID="btnFiltrar"  CssClass="btn btn-success" runat="server" Text="Filtrar" />
         </div>
      </div>
      <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
            <div class="row">
               <div class="col-sm-12">
                  <dx:ASPxGridView ID="dvgdProductividad" ClientInstanceName="dvgdProductividad" runat="server" AutoGenerateColumns ="False" Width ="100%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px">
                     <Styles Header-Wrap="True" />
                     <Columns>
                        <dx:GridViewDataTextColumn Caption= "ESTADO" FieldName="Estado" VisibleIndex="0" Visible="false" />
                        <dx:GridViewDataTextColumn Caption= "MESA" FieldName="MesaNombre" VisibleIndex="1" Width="150" />
                        <dx:GridViewDataTextColumn Caption= "# USUARIO" FieldName="IdUsuario" VisibleIndex="2" Width="150"/>
                        <dx:GridViewDataTextColumn Caption ="USUARIO"  FieldName="UsuarioNombre"  VisibleIndex="3" Width="150" />
                        <dx:GridViewDataTextColumn Caption ="DIAS HÁBILES"  FieldName="diasHabiles"  VisibleIndex="4" Width="120" />
                        <dx:GridViewDataTextColumn Caption ="DIAS NO HÁBILES"  FieldName="diasNoHabiles"  VisibleIndex="5" Width="120" />
                        <dx:GridViewDataTextColumn Caption ="HORAS HÁBILES"  FieldName="horasHabiles"  VisibleIndex="6" Width="120" />
                        <dx:GridViewDataTextColumn Caption ="HORAS INHÁBILES"  FieldName="horasNoHabiles"  VisibleIndex="7" Width="120" />
                        <dx:GridViewDataTextColumn Caption ="HORAS EFECTIVAS"  FieldName="horasEfectivas"  VisibleIndex="8" Width="120"/>
                        <dx:GridViewDataTextColumn Caption ="ESTATUS"  FieldName="EstadoTramite"  VisibleIndex="9" Width="120" />
                        <dx:GridViewDataTextColumn Caption ="TRAMITES"  FieldName="tramites"  VisibleIndex="10" Width="120" />
                     </Columns>
                     <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                     <SettingsPager  Mode="ShowAllRecords"/>
                     <Settings  VerticalScrollBarMode="Visible" VerticalScrollableHeight="400"  HorizontalScrollBarMode="Visible"/>
                     <SettingsSearchPanel Visible="true" />
                  </dx:ASPxGridView>                    
                  <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdProductividad"></dx:ASPxGridViewExporter>
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
