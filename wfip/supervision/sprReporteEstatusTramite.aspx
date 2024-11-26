<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteEstatusTramite.aspx.cs" Inherits="wfip.supervision.sprReporteEstatusTramite" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
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
        <legend> ESTATUS TRÁMITE </legend>
        <br /><br />
    <div>
        <table style ="width:75%">
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
               <td>
                <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="cmbEstatus" Width="285px" runat="server" AnimationType="None" Theme="iOS" Caption="Estatus">
                <DropDownWindowStyle BackColor="#A5CE4E" />
                <DropDownWindowTemplate>
                <dx:ASPxListBox Width="100%" ID="listaEstatus" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                    runat="server" Height="200" EnableSelectAll="true">
                    <FilteringSettings ShowSearchUI="true"/>
                    <Border BorderStyle="None" />
                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                    <Items>
                        <dx:ListEditItem Text="En Tramite" Value="0" Selected="true"/>
                        <dx:ListEditItem Text="Proceso" Value="1" />
                        <dx:ListEditItem Text="Hold" Value="2"/>
                        <dx:ListEditItem Text="Ejecucion" Value="3"/>
                        <dx:ListEditItem Text="Rechazo" Value="4" />
                        <dx:ListEditItem Text="Suspendido" Value="5" />
                        <dx:ListEditItem Text="PCI" Value="6" />
                        <dx:ListEditItem Text="Cancelado" Value="7" />
                        <dx:ListEditItem Text="Promotoría Cancela" Value="15" />

                        
                    </Items>

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
        <br /><br />
        <table style="width:100%">
            <tr>
             <td style="text-align:right">
                <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                   <img src="../img/excel.png"/>
                </asp:LinkButton>
             </td>
         </tr>
        </table>
        <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table style="width:100%">
        <tr><td style="width:100%;vertical-align:top">
           <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" AutoGenerateColumns ="False" 
                          style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px" Width="100%">
           <Columns>
             <dx:GridViewDataDateColumn Caption="FECHA INGRESO" FieldName="FechaIngreso"  VisibleIndex="1" >
                <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataTextColumn Caption="FOLIO" FieldName="FolioTramite"  VisibleIndex="2" Width="110px"/>
             <dx:GridViewDataTextColumn Caption="RAMO" FieldName="Ramo"  VisibleIndex="3" />
             <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="Producto"  VisibleIndex="4" />
             <dx:GridViewDataTextColumn Caption="ESTATUS" FieldName="Estatus"  VisibleIndex="5" />
             <dx:GridViewDataDateColumn Caption="FECHA ESTATUS" FieldName="FechaEstatus"  VisibleIndex="6">
               <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" EditFormatString="dd-MM-yyyy"></PropertiesDateEdit>
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataTextColumn Caption="TIEMPO ESTATUS ACTUAL (HORAS)" FieldName="Tiempo"  VisibleIndex="7" />
             <dx:GridViewDataTextColumn Caption="NUM. PROMOTORIA" FieldName="IdPromotoria"  VisibleIndex="8" Visible="false" />
             <dx:GridViewDataTextColumn Caption="CVE. PROMOTORIA" FieldName="PromotoriaClave"  VisibleIndex="9" />
             <dx:GridViewDataTextColumn Caption="PROMOTORIA" FieldName="Promotoria"  VisibleIndex="10" />
             <dx:GridViewDataTextColumn Caption="CLAVE AGENTE" FieldName="ClaveAgente"  VisibleIndex="11" />
             <dx:GridViewDataTextColumn Caption ="POLIZA" FieldName="Poliza"  VisibleIndex="12" />
             <dx:GridViewDataTextColumn Caption ="IDENTIFICADOR" FieldName="prioridad"  VisibleIndex="13" />
           </Columns>
           <Settings  VerticalScrollBarMode="Auto"  ShowFooter="True" ShowGroupFooter="VisibleAlways" />
           <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
           <SettingsPager  Mode="ShowAllRecords"/>
           <SettingsSearchPanel Visible="true" />
           <Styles Header-Wrap="True"/>
           </dx:ASPxGridView>
           <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdEstatusTramite"></dx:ASPxGridViewExporter>
        </td></tr>
        </table>
        </ContentTemplate>
        <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
    </div>
    </fieldset>
</asp:Content>
