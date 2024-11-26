<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteOperativo.aspx.cs" Inherits="wfip.supervision.sprReporteOperativo" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <fieldset>
        <legend>OPERATIVO</legend>
        <div style="width:100%; background-color:#0094ff; font-size:14px; text-align:center; color:white;">
            <asp:Literal ID="ltTituloGrafica" runat="server"></asp:Literal>
        </div>
        <asp:Panel ID="pnlCajaGrid" runat="server" Width="100%" ScrollBars="Horizontal" >
            <asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click">Exportar a Excel</asp:LinkButton>
            <dx:ASPxGridView ID="grdOperativo" runat="server" AutoGenerateColumns="False" EnableTheming="True" Font-Size="10px" Theme="Aqua" Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn Caption ="Folio" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Fecha de ingreso" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Hora de ingreso" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Clave de Agente" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Nombre del Agente" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="No. De Promotoría" VisibleIndex="5">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Nombre de la Promotoría" VisibleIndex="6">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Línea de Negocio" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Tipo de Trámite" VisibleIndex="8">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Tipo de Producto" VisibleIndex="9">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Ramo" VisibleIndex="10">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Centro de Costos" VisibleIndex="11">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Tipo de solicitud" VisibleIndex="12">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Contratante" VisibleIndex="13">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Estatus" VisibleIndex="14">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Fecha de Estatus" VisibleIndex="15">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Motivo" VisibleIndex="16">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Operador" VisibleIndex="17">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption ="Póliza" VisibleIndex="18">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdOperativo"></dx:ASPxGridViewExporter>
        </asp:Panel>
    </fieldset>
</asp:Content>
