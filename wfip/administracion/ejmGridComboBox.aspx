<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="ejmGridComboBox.aspx.cs" Inherits="wfip.administracion.ejmGridComboBox" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function terminaCallback()
        {
            var dt = new Date();
            lbMsg.SetText(dt.toUTCString());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <div id="dvCajGrdCboBox" style="width: 50%; margin: auto;">
        <dx:ASPxButton ID="btnLlenaGrid" runat="server" Text="Llena grid" AutoPostBack="False" Theme="Aqua">
            <ClientSideEvents Click="function(s, e) { grdMesas.PerformCallback('1');}" />
        </dx:ASPxButton>
        <dx:ASPxLabel ID="lbMsg" ClientInstanceName="lbMsg" runat="server"></dx:ASPxLabel>
        <dx:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="Id" ClientInstanceName="grdMesas" Theme="Aqua" Width="100%" Font-Size="10px" ID="grdMesas" OnCustomCallback="grdMesas_CustomCallback" OnRowInserting="grdMesas_RowInserting" OnPageIndexChanged="grdMesas_PageIndexChanged" OnRowUpdating="grdMesas_RowUpdating">
            <ClientSideEvents EndCallback="function(s, e) { terminaCallback(); }"></ClientSideEvents>
            <SettingsPager PageSize="5">
            </SettingsPager>
            <SettingsEditing Mode="EditForm"></SettingsEditing>
            <Settings ShowTitlePanel="True" />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSort="False"></SettingsBehavior>
            <SettingsDataSecurity AllowDelete="False"></SettingsDataSecurity>
            <SettingsText Title="MESAS"></SettingsText>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Id" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IdFlujo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Flujo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Nombre" ShowInCustomizationForm="True" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IdTipo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="Tipo" ShowInCustomizationForm="True" VisibleIndex="5">
                    <PropertiesComboBox>
                        <Items>
                            <dx:ListEditItem Text="GENERAL" Value="1" />
                            <dx:ListEditItem Text="ESPECIALIZADA" Value="2" />
                        </Items>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="IdEtapa" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="Etapa" ShowInCustomizationForm="True" VisibleIndex="7">
                    <PropertiesComboBox DropDownStyle="DropDown" ValueType="System.String" DataSourceID="objDsEtapas" TextField="Nombre" ValueField="Id"></PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataCheckColumn FieldName="IdEstado" ShowInCustomizationForm="True" Caption="Activo" VisibleIndex="8"></dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="Estado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IdMesaPadre" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10"></dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="MesaPadre" ShowInCustomizationForm="True" VisibleIndex="11">
                    <PropertiesComboBox DropDownStyle="DropDown" ValueType="System.String" DataSourceID="objDsMesas" TextField="Nombre" ValueField="Id"></PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="IdUsuario" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Usuario" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="13">
                    <EditFormSettings Visible="False"></EditFormSettings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FechaRegistro" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="14">
                    <EditFormSettings Visible="False"></EditFormSettings>
                </dx:GridViewDataDateColumn>
            </Columns>
            <Toolbars>
                <dx:GridViewToolbar ItemAlign="Right">
                    <Items>
                        <dx:GridViewToolbarItem Command="New"></dx:GridViewToolbarItem>
                        <dx:GridViewToolbarItem Command="Edit"></dx:GridViewToolbarItem>
                    </Items>
                </dx:GridViewToolbar>
            </Toolbars>
            <Styles>
                <TitlePanel Border-BorderColor="#1386C7" Border-BorderStyle="Solid" Border-BorderWidth="1px" BackColor="#94C37E" Font-Bold="False" ForeColor="White"></TitlePanel>
            </Styles>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="objDsEtapas" runat="server" TypeName="wfiplib.admCatEtapas" SelectMethod="Lista">
            <SelectParameters>
                <asp:SessionParameter SessionField="pIdFlujo" DefaultValue="0" Name="pIdFlujo" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objDsMesas" runat="server" TypeName="wfiplib.admMesa" SelectMethod="Lista" >
            <SelectParameters>
                <asp:SessionParameter SessionField="pIdFlujo" DefaultValue="0" Name="pIdFlujo" Type="Int32"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
