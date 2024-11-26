<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="opBuscaTramite.aspx.cs" Inherits="wfip.operacion.opBuscaTramite" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>BUSCA TRAMITE</legend>
        
        <table id="tblBuscar" style="width:100%">
            <tr>
                <td colspan="5">
                    &nbsp;
                </td>
            </tr>
            <tr>
              <td>
                <dx:ASPxTextBox ID="txtTramite" runat="server" Caption="Trámite" Theme="iOS">
                    <MaskSettings Mask="00000" ErrorText="Please input missing digits" />
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ErrorTextPosition="Bottom" />
                </dx:ASPxTextBox>
             </td>
            <td>
                <dx:ASPxTextBox ID="txtRFC" runat="server"  Caption="RFC" Theme="iOS" name="txtRFC">
                    <MaskSettings Mask="00000" ErrorText="Please input missing digits" />
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ErrorTextPosition="Bottom" />
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtContratante" runat="server" Caption="Contratante" Theme="iOS">
                    <MaskSettings Mask="00000" ErrorText="Please input missing digits" />
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ErrorTextPosition="Bottom" />
                </dx:ASPxTextBox>
             </td>
            <td>
                <dx:ASPxTextBox ID="txtAsegurado" runat="server" Caption="Asegurado titular" Theme="iOS">
                    <MaskSettings Mask="00000" ErrorText="Please input missing digits" />
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ErrorTextPosition="Bottom" />
                </dx:ASPxTextBox>
             </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar"  CssClass="boton" />
            </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br />
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
                        <div style="max-height:200px; overflow:auto">
                        <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server"
                            AutoGenerateColumns ="False" Width ="95%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  
                            Font-Size ="10px" KeyFieldName="IdTramite"   >
                           <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" />
                                <dx:GridViewDataTextColumn Caption ="N. TRÁMITE"  FieldName="tramite"  VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption ="CONTRATANTE"  FieldName="FechaRegistro"  VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption ="ASEGURADO TITULAR" FieldName="Id"  VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption= "MESA" FieldName="TramiteNombre" VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption= "USUARIO" FieldName="IdMesa"   VisibleIndex="5"/>
                                <dx:GridViewDataTextColumn Caption= "IDENTIFICADOR" FieldName="MesaNombre" VisibleIndex="6" />
                            </Columns>
                            <SettingsBehavior  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" EnableRowHotTrack="False"  />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>
                        </div>
                    </td>
                </tr>
                </table>
        </ContentTemplate>
            <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnBuscar"  EventName="Click" />
             </Triggers>
            </asp:UpdatePanel>
    </fieldset>
    <table>
        <tr>
            <td>
               <asp:Button ID="btnAsignar" runat="server" Text="Buscar"  CssClass="boton" OnClick="btnAsignar_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
