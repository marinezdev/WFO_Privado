<%@ Page Title="" Language="C#" MasterPageFile="~/laboratorios/laboratorios.master" AutoEventWireup="true" CodeBehind="MisTramites.aspx.cs" Inherits="wfip.laboratorios.MisTramites" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.State" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="mensajesInformativos" runat="server"></asp:UpdatePanel>
    <fieldset>       
    <legend>MIS TRÁMITES – BÚSQUEDAS</legend>
        <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
    <table  style ="width:100%">
        <tr>
            <td colspan="3">
                <label>Búsqueda por rangos de fecha de envió </label>
                <br />
                <asp:Label ID="MSresultado2" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde:">
                    <TimeSectionProperties Adaptive="true">
                        <TimeEditProperties EditFormatString="hh:mm tt" />
                    </TimeSectionProperties>
                    <CalendarProperties>
                        <FastNavProperties DisplayMode="Inline" />
                    </CalendarProperties>
                </dx:ASPxDateEdit>
                <asp:RequiredFieldValidator runat="server" ID="reqFechaDesde" ControlToValidate="CalDesde" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="ConsultaFechas"></asp:RequiredFieldValidator>
            </td>
            <td>
                <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom" Width="190" Caption="Hasta">
                    <TimeSectionProperties Adaptive="true">
                        <TimeEditProperties EditFormatString="hh:mm tt" />
                    </TimeSectionProperties>
                    <CalendarProperties>
                        <FastNavProperties DisplayMode="Inline" />
                    </CalendarProperties>
                </dx:ASPxDateEdit>
                <asp:RequiredFieldValidator runat="server" ID="reqFechaHasta" ControlToValidate="CalHasta" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="ConsultaFechas"></asp:RequiredFieldValidator>
            </td>
            <td>    
                <div style="vertical-align:bottom">
                    <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Filtrar" OnClick="ConsultaFechasBD" ValidationGroup="ConsultaFechas"/>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
                <hr />
                <br />
            </td>
        </tr>
        <tr>
            <td style ="width:100%; vertical-align:top" colspan="3">
                <dx:ASPxGridView ID="dvgdTramitesEspera" ClientInstanceName="dvgdTramitesEspera" runat="server" AutoGenerateColumns ="False" Width ="100%" EnableTheming="True" Theme=""   >
                    <Columns>
                        <dx:GridViewDataDateColumn Caption ="Fecha envió"  FieldName="FechaRegistro" /> 
                        <dx:GridViewDataTextColumn Caption ="Número de trámite "  FieldName="FolioCompuesto"  />
                        <dx:GridViewDataTextColumn Caption ="Información de contratante "  FieldName="DatosHtml" PropertiesTextEdit-EncodeHtml="false" />
                        <dx:GridViewDataDateColumn Caption="Fecha Firma de Solicitud " FieldName="FechaSolicitud" />
                        <dx:GridViewDataColumn Caption="Details">
                            <DataItemTemplate>
                                <asp:ImageButton ID="imbtnConsultar" runat="server" OnCommand="MuestraTramiteOnclick"  FieldName="Id" CommandArgument='<%# Eval("Id")%>' CommandName ="Consultar" ImageUrl="~/img/Folder.png" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsPager Mode="ShowAllRecords" />
                        <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="true" ShowFooter="true" />
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
