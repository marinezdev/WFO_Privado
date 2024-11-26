<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprDetalleHoras.aspx.cs" Inherits="wfip.supervision.sprDetalleHoras" %>
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
        <legend>DETALLE DE HORAS</legend> 
        <br />
       
            <table style ="width:100%">
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
                <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="cmbUsuarios" Width="285px" runat="server" AnimationType="None" Theme="iOS" Caption="Usuario">
                <DropDownWindowStyle BackColor="#A5CE4E" />
                <DropDownWindowTemplate>
                <dx:ASPxListBox Width="100%" ID="listUsuario" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                    runat="server" Height="200" EnableSelectAll="true" OnInit="listUsuario_Init">
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
                </td>
                <td>    
                    <div style="vertical-align:bottom">
                    <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Filtrar" />
                    </div>
                </td>
                </tr>
                <tr>
                 <td colspan="3">
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
                <tr><td colspan="3">&nbsp;</td></tr>
        </table>
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
                 <div id="dvCajaGrilla">
                     <dx:ASPxGridView ID="dvgdProductividad" ClientInstanceName="dvgdProductividad" runat="server" AutoGenerateColumns ="False" Width ="100%" 
                             style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px">
                           <Styles Header-Wrap="True" />
                           <Columns>
                                <dx:GridViewDataTextColumn Caption= "ESTADO" FieldName="Estado" VisibleIndex="0" Visible="false" />
                                <dx:GridViewDataTextColumn Caption= "MESA" FieldName="MesaNombre" VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption= "# USUARIO" FieldName="IdUsuario" VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption ="USUARIO"  FieldName="UsuarioNombre"  VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption ="DIAS HÁBILES"  FieldName="diasHabiles"  VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption ="DIAS NO HÁBILES"  FieldName="diasNoHabiles"  VisibleIndex="5" />
                                <dx:GridViewDataTextColumn Caption ="HORAS HÁBILES"  FieldName="horasHabiles"  VisibleIndex="6" />
                                <dx:GridViewDataTextColumn Caption ="HORAS INHÁBILES"  FieldName="horasNoHabiles"  VisibleIndex="7" />
                                <dx:GridViewDataTextColumn Caption ="HORAS EFECTIVAS"  FieldName="horasEfectivas"  VisibleIndex="8" />
                                <dx:GridViewDataTextColumn Caption ="ESTATUS"  FieldName="EstadoTramite"  VisibleIndex="9" />
                                <dx:GridViewDataTextColumn Caption ="TRAMITES"  FieldName="tramites"  VisibleIndex="10" />
                            </Columns>
                            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <Settings  VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>                    
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdProductividad"></dx:ASPxGridViewExporter>
                <br />
                </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
    </fieldset>
</asp:Content>
