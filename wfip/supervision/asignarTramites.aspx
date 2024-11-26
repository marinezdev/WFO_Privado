<%@ Page Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="asignarTramites.aspx.cs" Inherits="wfip.supervision.asignarTramites" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <script>
        var command = "";
        function OnBeginCallback(s, e) {
            s.isError = false;
            command = e.command;
        } 
        function OnEndCallback(s, e) {
            if ((command == "ADDNEWROW" || command == "UPDATEEDIT") && !s.isError) {
                GridTramites.Refresh();
            }
        }
        function OnCallbackError(s, e) {
            s.isError = true;
        }
    </script>
    <fieldset>
        <legend>ASIGNACIÓN DE TRÁMITES </legend>
        <br />
        <br />
        <div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="txtTramite" runat="server" Caption="Trámite" Theme="iOS" />
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtRFC" runat="server" Caption="RFC" Theme="iOS" name="txtRFC" />
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtContratante" runat="server" Caption="Contratante" Theme="iOS" />
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtAsegurado" runat="server" Caption="Asegurado titular" Theme="iOS" />
                    </td>
                    <td>
                        <asp:Button ID="btnFiltroTramites" CssClass="boton" runat="server" Text="Buscar" OnClick="btnFiltroTramites_Click" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="DetalleTramites" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Detalle de trámites por mesa</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="GridTramite" ClientInstanceName="GridTramites" runat="server" KeyFieldName="IdTramite" AutoGenerateColumns="False" Width="100%" Theme="Material">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="FolioCompuesto" VisibleIndex="1" Caption="FOLIO" />
                                        <dx:GridViewDataColumn FieldName="Contratante" VisibleIndex="2" Caption="CONTRATANTE" />
                                        <dx:GridViewDataColumn FieldName="Titular" VisibleIndex="3" Caption="TITULAR" />
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxGridView ID="detailGrid" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnBeforePerformDataSelect="detailGrid_BeforePerformDataSelect" OnRowUpdating="detailGrid_RowUpdating" Theme="iOS" OnInit="detailGrid_Init" OnCellEditorInitialize="detailGrid_CellEditorInitialize">
                                                <ClientSideEvents EndCallback="OnEndCallback" BeginCallback="OnBeginCallback" CallbackError="OnCallbackError" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowEditButton="true" VisibleIndex="0">
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn FieldName="IdTramite" ReadOnly="True" VisibleIndex="1" Visible="false">
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataComboBoxColumn Width="150" FieldName="Usuario" VisibleIndex="2" Caption="USUARIO">
                                                        <PropertiesComboBox DropDownStyle="DropDownList" TextField="usuario" ValueField="numUsuario" ValueType="System.Int32" IncrementalFilteringMode="StartsWith">
                                                        </PropertiesComboBox>
                                                    </dx:GridViewDataComboBoxColumn>
                                                    <dx:GridViewDataTextColumn FieldName="statusTramite" VisibleIndex="3" ReadOnly="true" Caption="ESTATUS">
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="IdMesa" VisibleIndex="4" ReadOnly="true" Visible="false">
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Mesa" VisibleIndex="5" ReadOnly="true" Caption="MESA">
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsCommandButton EditButton-Text="Asignar" UpdateButton-Text="Aplicar" CancelButton-Text="Cancelar"></SettingsCommandButton>
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                    </Templates>
                                    <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <SettingsDetail ShowDetailRow="true" />
                                    <Settings VerticalScrollBarMode="Auto" />
                                    <SettingsSearchPanel Visible="true" />
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltroTramites" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </fieldset>
</asp:Content>
