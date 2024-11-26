<%@ Page Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="buscarTramites.aspx.cs" Inherits="wfip.supervision.buscarTramites" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
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
        <legend>BÚSQUEDA DE TRÁMITES </legend>
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
                         <asp:Button ID="btnFiltroMesa"  CssClass="boton" runat="server" Text="Buscar" />
                    </td>
                </tr>
             </table>
             <table style="width:100%">
                <tr>
                    <td style="text-align:right">
                    <br />
                    <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                        <img src="../img/excel.png"/>
                    </asp:LinkButton>
                    </td>
                </tr>
           </table>
         <asp:UpdatePanel ID="DetalleTramites" runat="server" UpdateMode="Conditional"> 
             <ContentTemplate>
             <table  style ="width:100%">
                <tr>
                    <td style ="width:100%; vertical-align:top">
                        <div style="max-height:400px; overflow:auto">
                        <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server"
                            AutoGenerateColumns ="False" width="100%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  
                            Font-Size ="10px" KeyFieldName="IdTramite">
                           <Columns>
                                <dx:GridViewDataTextColumn Caption ="TRÁMITE"  FieldName="FolioCompuesto"  VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption ="CONTRATANTE"  FieldName="Contratante"  VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption ="TITULAR" FieldName="Titular"  VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption= "NUM. TRÁMITE" FieldName="IdTramite"  Visible="false"  VisibleIndex="6"/>
                                <dx:GridViewDataTextColumn Caption= "MESA" FieldName="MesaNombre"   VisibleIndex="7"/>
                                <dx:GridViewDataTextColumn Caption= "USUARIO" FieldName="UsuarioNombre"   VisibleIndex="8"/>
                                <dx:GridViewDataTextColumn Caption= "IDENTIFICADOR" FieldName="prioridad"   VisibleIndex="9"/>

                            </Columns>
                            <SettingsBehavior  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" EnableRowHotTrack="False"  />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>
                        </div>
                    </td>
                </tr>
                 <tr>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         <dx:ASPxGridViewExporter ID="grdExportMotivos" runat="server" GridViewID="dvgdMotivosSuspension"></dx:ASPxGridViewExporter>
                     </td>
                 </tr>
             </table>
             </ContentTemplate>
            <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnFiltroMesa" EventName="Click"/>
             </Triggers>
            </asp:UpdatePanel>
        </div>
   </fieldset>
</asp:Content>

