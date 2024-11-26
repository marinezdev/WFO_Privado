<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TramitesPorMesa.aspx.cs" Inherits="wfip.supervision.TramitesPorMesa" MasterPageFile="~/supervision/supervision.Master" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <fieldset>
        <legend>Trámites por Mesa</legend>

    <div style="width: 100%; width:1150px; display:block;">
        <table>
            <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblFechaInicio" Text="Fecha Inicio:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtFechaInicio" runat="server" Theme="Material" EditFormat="Custom" Width="210" AutoPostBack="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                    </dx:ASPxDateEdit>
                    <asp:RequiredFieldValidator runat="server" id="reqValidaFehcaInicio" controltovalidate="dtFechaInicio" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblFechaFin" Text="Fecha Fin:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtFechaFin" runat="server" Theme="Material" EditFormat="Custom" Width="210" AutoPostBack="false" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                    </dx:ASPxDateEdit>
                    <asp:RequiredFieldValidator runat="server" id="reqValidaFehcaFin" controltovalidate="dtFechaFin" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td valign="top">
                    Status Trámite:<br /> 
                    <asp:DropDownList ID="DDLStatusTramite" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td valign="top">
                    Nombre Mesa:<br />
                    <asp:DropDownList ID="DDLMesa" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server"  AutoPostBack="True" Text="Buscar" CssClass="boton" OnClick="btnBuscar_Click"  />
                    <asp:Label runat="server" ID="lblMensaje" ForeColor="Red" Font-Bold="true" Font-Size="Large" Text=""></asp:Label>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:LinkButton ID="lnkExportarResumen" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                        <img src="../Img/excel.png"/>
                    </asp:LinkButton>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                </td>
            </tr>
        </table>
    </div>




        <dx:ASPxGridView ID="grid" 
            ClientInstanceName="grid" 
            runat="server" 
            Width="100%" 
            AutoGenerateColumns="false"
            KeyFieldName="ID" Visible="true" Styles-Header-HorizontalAlign="Center"
            >
                <Columns>
                    <dx:GridViewDataTextColumn                                   FieldName="Id"                    VisibleIndex="0"  Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Folio Compuesto"         FieldName="FolioCompuesto"        Width="120px" VisibleIndex="3"  Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Fecha Registro Tramite"  FieldName="FechaRegistroTramite"  Width="160px" VisibleIndex="4"  Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Estado"                  FieldName="Estado"                Width="50px" CellStyle-HorizontalAlign="Center" VisibleIndex="5"  Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Status Trámite"          FieldName="StatusTramite"         Width="200px" CellStyle-HorizontalAlign="Center" VisibleIndex="6"  Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Id Mesa"                 FieldName="IdMesa"                VisibleIndex="7"  Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Nombre Mesa"             FieldName="NombreMesa"            Width="200px" CellStyle-HorizontalAlign="Center" VisibleIndex="8"  Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Fecha Registro Mesa"     FieldName="FechaRegistroMesa"     Width="160px" VisibleIndex="9"  Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Fecha Inicio Mesa"       FieldName="FechaInicioMesa"       Width="160px" VisibleIndex="10" Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Fecha Fin Mesa"          FieldName="FechaFinMesa"          Width="160px" VisibleIndex="11" Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Estado"                  FieldName="Estado2"               Width="50px" CellStyle-HorizontalAlign="Center" VisibleIndex="12" Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Status Mesa"             FieldName="StatusMesa"            Width="200px" VisibleIndex="13" Visible="true" HeaderStyle-Wrap="false" CellStyle-Wrap="false"></dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <EmptyDataRow>
                        <div style="width: 300px;">
                            No hay datos para desplegar...
                        </div>
                    </EmptyDataRow>
                </Templates>            
            <SettingsBehavior AllowSelectByRowClick="true" />
            <SettingsPager  Mode="ShowAllRecords"/>
            <SettingsDetail ShowDetailRow="false" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" HorizontalScrollBarMode="Visible" />
            <SettingsSearchPanel Visible="false" />
        </dx:ASPxGridView>
        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdPrueba"></dx:ASPxGridViewExporter>

    </fieldset>
</asp:Content>