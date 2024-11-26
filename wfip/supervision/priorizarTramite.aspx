<%@ Page Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="priorizarTramite.aspx.cs" Inherits="wfip.supervision.priorizarTramite" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <script>
        $(document).keypress(
            function (event) {
                if (event.which == '13') {
                    event.preventDefault();
                }
            });
    </script>
    <fieldset>
        <legend>PRIORIZAR TRÁMITES </legend>
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
                        <asp:Button ID="btnFiltroMesa" CssClass="boton" runat="server" Text="Buscar" />
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
                            <td style="width: 100%; vertical-align: top">
                                <div style="max-height: 400px; overflow: auto">
                                    <dx:ASPxGridView ID="GridTramites" ClientInstanceName="dvgdTramites" runat="server" KeyFieldName="IdTramite" AutoGenerateColumns="False" Width="100%" Theme="Material" >
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="TRÁMITE" FieldName="FolioCompuesto" VisibleIndex="1" />
                                            <dx:GridViewDataTextColumn Caption="CONTRATANTE" FieldName="Contratante" VisibleIndex="2" />
                                            <dx:GridViewDataTextColumn Caption="TITULAR" FieldName="Titular" VisibleIndex="3" />
                                            <dx:GridViewDataTextColumn Caption="NUM. TRÁMITE" FieldName="IdTramite" Visible="false" VisibleIndex="4" />
                                            <dx:GridViewDataTextColumn Caption="PRIORIDAD ACTUAL" FieldName="prioridadDesc" VisibleIndex="5" />
                                            <dx:GridViewDataColumn VisibleIndex="0">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="btnPriorizar" runat="server" OnCommand="btnPriorizar_Command" CommandArgument='<%# Eval("IdTramite") %>' CommandName="Consultar" FieldName="IdTramite" Text="Priorizar" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsPager Mode="ShowPager" />
                                    </dx:ASPxGridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltroMesa" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </fieldset>
</asp:Content>

