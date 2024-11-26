<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="cancelarTramitesR.aspx.cs" Inherits="wfip.supervision.cancelarTramitesR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <fieldset>
        <legend>CANCELACIÓN DE TRÁMITES </legend>
         <br /><br />
        <div>
            <table style ="width:100%">
                 <tr>
                    <td>
                        <dx:ASPxTextBox ID="txtTramite" runat="server" Caption="Trámite" Theme="iOS"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtRFC" runat="server"  Caption="RFC" Theme="iOS" name="txtRFC"/>
                    </td>
                    <td>
                         <dx:ASPxTextBox ID="txtContratante" runat="server" Caption="Contratante" Theme="iOS"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtAsegurado" runat="server" Caption="Asegurado titular" Theme="iOS"/>
                    </td>
                    <td>
                         <asp:Button ID="btnFiltroMesa"  CssClass="btn btn-success" runat="server" Text="Buscar" OnClick="btnFiltroMesa_Click" />
                    </td>
                </tr>
            </table>
         <asp:UpdatePanel ID="DetalleTramites" runat="server" UpdateMode="Conditional"> 
             <ContentTemplate>
             <table  style ="width:100%">
                 <tr>
                     <td>&nbsp;</td>
                 </tr>
                 <tr><td>Detalle de trámites por mesa</td></tr>
                <tr>
                    <td style ="width:100%; vertical-align:top">
                        <div style="max-height:200px; overflow:auto";>
                        <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server"
                            AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  
                            Font-Size ="10px" KeyFieldName="IdTramite"   >
                           <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" />
                                <dx:GridViewDataTextColumn Caption ="TRÁMITE"  FieldName="FolioCompuesto"  VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption ="CONTRATANTE"  FieldName="Contratante"  VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption ="TITULAR" FieldName="Titular"  VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption= "IdMesa" FieldName="IdMesa"  Visible="false"  VisibleIndex="4"/>
                                <dx:GridViewDataTextColumn Caption= "MESA" FieldName="MesaNombre" VisibleIndex="5" />
                                <dx:GridViewDataTextColumn Caption= "IdTramite" FieldName="IdTramite"  Visible="false"  VisibleIndex="6"/>
                            </Columns>
                            <SettingsSearchPanel Visible="true" />
                            <SettingsBehavior  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="True" EnableRowHotTrack="False"  />
                            <SettingsPager  Mode="ShowAllRecords"/>
                        </dx:ASPxGridView> 
                        </div>
                    </td>
                </tr>
                 <tr>
                     <td>&nbsp;</td>
                 </tr>
                 <tr><td>Motivos de Cancelacion</td></tr>
                <tr>
                    <td style ="width:100%; vertical-align:top">
                    <div style="max-height:250px; overflow:auto">
                        <dx:ASPxGridView ID="dvgdMotivosCancelacion" ClientInstanceName="dvgdMotivosCancelacion" runat="server"
                            AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  
                            Font-Size ="10px" KeyFieldName="idMotivoCancelacion"   >
                           <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" />
                                <dx:GridViewDataTextColumn Caption ="id"  FieldName="idMotivoCancelacion"  VisibleIndex="1" Visible="false" />
                                <dx:GridViewDataTextColumn Caption ="Motivo" FieldName="MotivoCancelacion"  VisibleIndex="2" />
                            </Columns>
                            <SettingsBehavior  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="True" EnableRowHotTrack="False" />
                            <SettingsSearchPanel Visible="true" />
                            <SettingsPager  Mode="ShowAllRecords"/>
                        </dx:ASPxGridView>
                    </div>
                    </td>
                </tr>
                </table>
             </ContentTemplate>
            <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltroMesa"  EventName="Click" />
             </Triggers>
            </asp:UpdatePanel> 
        </div>
        <div class="d-table w-100">
            <div class="d-table-cell tar">
                <br />
                <asp:Button runat="server" ID="btnAsignar" Text="Cancelar"  CssClass="btn btn-success" OnClick="btnAsignar_Click" />
                <br /><br />
            </div>
            <asp:UpdatePanel ID="Usuarios" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
            <div class="d-table-cell tar">
              <table  style ="width:100%">
                <tr><td>&nbsp;</td></tr>
                <tr><td><asp:label runat="server" ID="Mensaje"></asp:label></td></tr>
              </table>
            </div>
             </ContentTemplate>
            <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnAsignar" EventName="Click"  />
             </Triggers>
            </asp:UpdatePanel>
       </div>
    </fieldset>
</asp:Content>
