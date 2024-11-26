<%@ Page Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="detalleMesa.aspx.cs" Inherits="wfip.supervision.detalleMesa" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        var start;
        function grid_Init(s, e) {
            grid.Refresh();
        }
        function grid_BeginCallback(s, e) {
            start = new Date();
            ClientCommandLabel.SetText(e.command);
            ClientTimeLabel.SetText("working...");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />

    <asp:UpdatePanel ID="mensajesInformativos" runat="server"></asp:UpdatePanel>
    <fieldset>
        <legend>DETALLE POR MESA (SÁBANA) </legend>
         <br /><br />
        <span> LA GENERACIÓN DE ESTE REPORTE PUEDE DURAR MÁS DE UN MINUTO</span>
        <br /><br />
        <div>
            <table style ="width:50%">
                <tr>
                    <td>                    
                     <dx:aspxdateedit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"   Width="190" Caption="Desde:">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties> 
                      </dx:aspxdateedit>
                    </td>
                    <td>
                     <dx:aspxdateedit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                     </dx:aspxdateedit>
                    </td>
                    <td>
                        <asp:Button ID="btnFiltroMes"  CssClass="boton" runat="server" Text="Filtrar" OnClick="btnFiltroMes_Click"/>
                    </td>
                    
                </tr>
            </table>
            <br />
            <hr />
            <asp:UpdatePanel ID="DetalleTramites" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="ID"
                            Width="100%" AutoGenerateColumns="False" OnCustomColumnDisplayText="grid_CustomColumnDisplayText"
                            OnSummaryDisplayText="grid_SummaryDisplayText">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="FolioCompuesto" Width="200px" />
                            </Columns>
                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="true" ShowFooter="true" />
                            <SettingsPager>
                                <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                            </SettingsPager>
                            <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                            <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Size" SummaryType="Sum" />
                            </TotalSummary>
                            <GroupSummary>
                                <dx:ASPxSummaryItem SummaryType="Count" />
                            </GroupSummary>
                        </dx:ASPxGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </fieldset>
</asp:Content>
  