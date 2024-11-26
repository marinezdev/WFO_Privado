<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admCfgMesas.aspx.cs" Inherits="wfip.administracion.admCfgMesas" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <dx:ASPxTreeList ID="dxCfgMesas" runat="server" Theme="Office2003Blue" AutoGenerateColumns="False" KeyFieldName="IdMesa" ParentFieldName="MesaPadre" Width="100%">
        <Columns>
            <dx:TreeListDataColumn FieldName="Nombre" Caption="Nombre" VisibleIndex="0" />
            <dx:TreeListDataColumn FieldName="Nivel" Caption="Etapa" VisibleIndex="1" />
            <dx:TreeListDataColumn FieldName="Tipo" Caption="Tipo" VisibleIndex="2" />
        </Columns>
        <Settings GridLines="None" ShowTreeLines="False" />
        <SettingsBehavior ExpandCollapseAction="Button" AllowFocusedNode="True" />
        <SettingsEditing AllowNodeDragDrop="True" />
        <Styles>
            <AlternatingNode Enabled="False">
            </AlternatingNode>
        </Styles>
    </dx:ASPxTreeList>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%">
        
    </dx:ASPxGridView>
</asp:Content>
