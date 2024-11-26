<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admDxFlujo.aspx.cs" Inherits="wfip.administracion.admDxFlujo" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link type="text/css" rel="stylesheet" href="../css/jQuerySortable.css" />
    <script type="text/javascript" src="../js/jquery-sortable.js"></script>
    <script type="text/javascript">
        var mIndexIdFlujo = 0;
        var mEstaActualizandoCfgMesa = 0;
        function pintaTipoTramite(pRowIndex) {
            mIndexIdFlujo = pRowIndex;
            grdTipoTramite.PerformCallback(mIndexIdFlujo);
        }
        function pintaEtapas() { grdEtapas.PerformCallback(mIndexIdFlujo); }
        function pintaMesas() { grdMesas.PerformCallback(mIndexIdFlujo); }
        function pintaCfgMesas() { if (mEstaActualizandoCfgMesa == 0) pnlCfgMesas.PerformCallback(); else mEstaActualizandoCfgMesa = 0; }

        function guardarCfgMesas() {
            if (confirm('Esta seguro que desea continuar ?')) {
                var data = $("ol.listaOrdenada").sortable("serialize").get();
                mEstaActualizandoCfgMesa = 1;
                dxPnlCargandoCfgMesas.Show();
                callBackCfgMesas.PerformCallback(JSON.stringify(data));
            }
        }

        function regresoCallBackCfgMesas(s, e) {
            dxPnlCargandoCfgMesas.Hide();
            alert(e.result);
            pintaMesas();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <fieldset>
        <legend>FLUJOS DE TRABAJO</legend>
        <table id="tblCajaPrincipal" style="width: 100%">
            <tr>
                <td style="width: 35%; vertical-align: top;">
                    <dx:ASPxRoundPanel ID="dxPnlFlujos" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" EnableAnimation="False" HeaderText="FLUJOS" Theme="Aqua" Font-Size="10px">
                        <ContentPaddings Padding="5px" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:ASPxGridView ID="grdFlujos" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdFlujos" Font-Size="10px" Theme="Aqua" Width="100%" DataSourceID="objDsFlujoTrabajo" KeyFieldName="Id" OnRowInserting="grdFlujos_RowInserting">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) { pintaTipoTramite(s.GetFocusedRowIndex()); }" />
                                    <SettingsPager PageSize="4">
                                    </SettingsPager>
                                    <Settings ShowTitlePanel="False" />
                                    <SettingsEditing Mode="EditForm">
                                    </SettingsEditing>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSort="False" />
                                    <SettingsDataSecurity AllowDelete="False" />
                                    <SettingsText Title="FLUJOS" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Id" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Nombre" ShowInCustomizationForm="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataCheckColumn FieldName="IdEstado" ShowInCustomizationForm="True" VisibleIndex="2" Caption="Activo">
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataTextColumn FieldName="Estado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdUsuario" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Usuario" ShowInCustomizationForm="True" VisibleIndex="5" ReadOnly="True">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="FechaRegistro" ShowInCustomizationForm="True" ReadOnly="True" VisibleIndex="6">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="New">
                                                </dx:GridViewToolbarItem>
                                                <dx:GridViewToolbarItem Command="Edit">
                                                </dx:GridViewToolbarItem>
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Styles>
                                        <TitlePanel BackColor="#94C37E" Border-BorderColor="#1386C7" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Bold="False" ForeColor="White">
                                        </TitlePanel>
                                    </Styles>
                                </dx:ASPxGridView>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                    <asp:ObjectDataSource ID="objDsFlujoTrabajo" runat="server"
                        DataObjectTypeName="wfiplib.flujo"
                        TypeName="wfiplib.admFlujo"
                        InsertMethod="nuevo"
                        SelectMethod="Lista"
                        UpdateMethod="modifica"></asp:ObjectDataSource>
                    <dx:ASPxRoundPanel ID="dxPnlCfgMesa" runat="server" Font-Size="10px" HeaderText="CONFIGURACION DE MESAS" Theme="Aqua" Width="100%" AllowCollapsingByHeaderClick="True">
                        <ContentPaddings Padding="5px" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:ASPxCallbackPanel ID="pnlCfgMesas" runat="server" ClientInstanceName="pnlCfgMesas" Width="100%" OnCallback="pnlCfgMesas_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) { $('ol.listaOrdenada').sortable(); }" />
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <div id="dvCjaBtnLstOrdena" style="width: 100%; text-align: right;">
                                                <dx:ASPxButton ID="btnActlizaCfgMesas" runat="server" Text="Guardar" Font-Size="10px" Theme="Aqua" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) { guardarCfgMesas(); }" />
                                                </dx:ASPxButton>
                                            </div>
                                            <ol class='listaOrdenada'>
                                                <asp:Literal ID="ltFlujo" runat="server"></asp:Literal>
                                            </ol>
                                            <dx:ASPxLoadingPanel ID="dxPnlCargandoCfgMesas" runat="server" ClientInstanceName="dxPnlCargandoCfgMesas" ContainerElementID="pnlCfgMesas" Text="Procesando&amp;hellip;" Theme="Default"></dx:ASPxLoadingPanel>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                    <dx:ASPxCallback ID="callBackCfgMesas" runat="server" ClientInstanceName="callBackCfgMesas" OnCallback="callBackCfgMesas_Callback">
                        <ClientSideEvents CallbackComplete="regresoCallBackCfgMesas" />
                    </dx:ASPxCallback>
                </td>
                <td style="vertical-align: top;">
                    <dx:ASPxRoundPanel ID="PnlTramites" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" EnableAnimation="False" HeaderText="TRAMITES" Theme="Aqua" Font-Size="10px">
                        <ContentPaddings Padding="5px" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:ASPxGridView ID="grdTipoTramite" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdTipoTramite" Font-Size="10px" OnCustomCallback="grdTipoTramite_CustomCallback" OnRowInserting="grdTipoTramite_RowInserting" Theme="Aqua" Width="100%" OnPageIndexChanged="grdTipoTramite_PageIndexChanged" KeyFieldName="Id" OnRowUpdating="grdTipoTramite_RowUpdating">
                                    <ClientSideEvents EndCallback="function(s, e) { pintaEtapas(); }" />
                                    <SettingsPager PageSize="3">
                                    </SettingsPager>
                                    <Settings ShowTitlePanel="False" />
                                    <SettingsEditing Mode="EditForm">
                                    </SettingsEditing>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSort="False" />
                                    <SettingsDataSecurity AllowDelete="False" />
                                    <SettingsText Title="TIPOS DE TRAMITE" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Id" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdFlujo" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Flujo" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Nombre" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdUsuario" ShowInCustomizationForm="True" VisibleIndex="6" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Usuario" ShowInCustomizationForm="True" VisibleIndex="7" ReadOnly="True">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Estado" ShowInCustomizationForm="True" VisibleIndex="8" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Tabla" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="FechaRegistro" ShowInCustomizationForm="True" VisibleIndex="9" ReadOnly="True">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataCheckColumn Caption="Activo" FieldName="IdEstado" ShowInCustomizationForm="True" VisibleIndex="5">
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="New">
                                                </dx:GridViewToolbarItem>
                                                <dx:GridViewToolbarItem Command="Edit">
                                                </dx:GridViewToolbarItem>
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Styles>
                                        <TitlePanel BackColor="#94C37E" Border-BorderColor="#1386C7" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Bold="False" ForeColor="White">
                                        </TitlePanel>
                                    </Styles>
                                </dx:ASPxGridView>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                    <asp:ObjectDataSource ID="objDsTiposTramite" runat="server"
                        DataObjectTypeName="wfiplib.TipoTramite"
                        TypeName="wfiplib.admTipoTramite"
                        SelectMethod="Lista">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="pIdFlujo" SessionField="pIdFlujo" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <dx:ASPxRoundPanel ID="pnlEtapas" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" EnableAnimation="False" HeaderText="ETAPAS" Theme="Aqua" Font-Size="10px">
                        <ContentPaddings Padding="5px" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:ASPxGridView ID="grdEtapas" runat="server" Font-Size="10px" Theme="Aqua" Width="100%" AutoGenerateColumns="False" OnCustomCallback="grdEtapas_CustomCallback" ClientInstanceName="grdEtapas" OnPageSizeChanged="grdEtapas_PageSizeChanged" KeyFieldName="Id" OnRowInserting="grdEtapas_RowInserting" OnRowUpdating="grdEtapas_RowUpdating">
                                    <ClientSideEvents EndCallback="function(s, e) { pintaMesas(); }" />
                                    <SettingsPager PageSize="6">
                                    </SettingsPager>
                                    <SettingsEditing Mode="EditForm">
                                    </SettingsEditing>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Id" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdFlujo" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Flujo" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Nombre" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdEtapa" ShowInCustomizationForm="True" VisibleIndex="4" Caption="Etapa">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataCheckColumn FieldName="IdEstado" ShowInCustomizationForm="True" VisibleIndex="5" Caption="Activo">
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdUsuario" ShowInCustomizationForm="True" VisibleIndex="6" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Usuario" ShowInCustomizationForm="True" VisibleIndex="7" ReadOnly="True">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Estado" ShowInCustomizationForm="True" VisibleIndex="8" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="FechaRegistro" ShowInCustomizationForm="True" VisibleIndex="9" ReadOnly="True">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="New">
                                                </dx:GridViewToolbarItem>
                                                <dx:GridViewToolbarItem Command="Edit">
                                                </dx:GridViewToolbarItem>
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                </dx:ASPxGridView>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                    <asp:ObjectDataSource ID="objDsEtapas" runat="server"
                        TypeName="wfiplib.admCatEtapas"
                        SelectMethod="Lista">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="pIdFlujo" SessionField="pIdFlujo" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <dx:ASPxRoundPanel ID="dxPnlMesas" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" EnableAnimation="False" HeaderText="MESAS" Theme="Aqua" Font-Size="10px">
                        <ContentPaddings Padding="5px" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:ASPxGridView ID="grdMesas" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdMesas" Font-Size="10px" OnCustomCallback="grdMesas_CustomCallback" OnRowInserting="grdMesas_RowInserting" Theme="Aqua" Width="100%" OnPageIndexChanged="grdMesas_PageIndexChanged" KeyFieldName="Id" OnRowUpdating="grdMesas_RowUpdating" OnInitNewRow="grdMesas_InitNewRow">
                                    <ClientSideEvents EndCallback="function(s, e) { pintaCfgMesas(); }" />
                                    <Settings ShowTitlePanel="False" />
                                    <SettingsEditing Mode="EditForm">
                                    </SettingsEditing>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSort="False" />
                                    <SettingsDataSecurity AllowDelete="False" />
                                    <SettingsText Title="MESAS" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Id" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdFlujo" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Flujo" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Nombre" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdTipo" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="Tipo" ShowInCustomizationForm="True" VisibleIndex="5">
                                            <PropertiesComboBox>
                                                <Items>
                                                    <dx:ListEditItem Text="GENERAL" Value="1" />
                                                    <dx:ListEditItem Text="ESPECIALIZADA" Value="2" />
                                                </Items>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdEtapa" ShowInCustomizationForm="True" VisibleIndex="6" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Etapa" ShowInCustomizationForm="True" VisibleIndex="7">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataCheckColumn FieldName="IdEstado" ShowInCustomizationForm="True" VisibleIndex="8" Caption="Activo">
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataTextColumn FieldName="Estado" ShowInCustomizationForm="True" VisibleIndex="9" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdMesaPadre" ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="MesaPadre" ShowInCustomizationForm="True" VisibleIndex="11" ReadOnly="true">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IdUsuario" ShowInCustomizationForm="True" VisibleIndex="12" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Usuario" ShowInCustomizationForm="True" VisibleIndex="13" ReadOnly="True">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="FechaRegistro" ShowInCustomizationForm="True" VisibleIndex="14" ReadOnly="True">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="New">
                                                </dx:GridViewToolbarItem>
                                                <dx:GridViewToolbarItem Command="Edit">
                                                </dx:GridViewToolbarItem>
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Styles>
                                        <TitlePanel BackColor="#94C37E" Border-BorderColor="#1386C7" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Bold="False" ForeColor="White">
                                        </TitlePanel>
                                    </Styles>
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="objDsMesas" runat="server" SelectMethod="Lista" TypeName="wfiplib.admMesa">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="0" Name="pIdFlujo" SessionField="pIdFlujo" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
