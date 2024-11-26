<%@ Page Title="" Language="C#" MasterPageFile="~/operacion/operacion.Master" AutoEventWireup="true" CodeBehind="citasMedicas.aspx.cs" Inherits="wfip.operacion.citasMedicas" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="DevExpress.Web.v17.2" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<fieldset>
    <legend>TRÁMITES CON ESTADO EN ESPERA DE RESULTADOS</legend>
    <table  style ="width:100%">
            <tr>
                <td style ="width:50%; vertical-align:top">
                    <dx:ASPxGridView ID="dvgdTramitesEspera" ClientInstanceName="dvgdTramitesEspera" runat="server" AutoGenerateColumns ="False" Width ="95%" EnableTheming="True" Theme="iOS"  Font-Size ="10px"   >
                       <Columns> 
                           <dx:GridViewCommandColumn ShowSelectCheckbox="true" />
                           <dx:GridViewDataTextColumn Caption ="FOLIO"  FieldName="Folio"  VisibleIndex="1" /> 
                           <dx:GridViewDataTextColumn Caption ="PROMOTORIA"  FieldName="Promotoria"  VisibleIndex="1" />
                        </Columns>
                        <SettingsBehavior  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                        <SettingsPager  Mode="ShowAllRecords"/>
                    </dx:ASPxGridView>                    
                </td>
            </tr>
            <tr>
                <td style ="vertical-align:top">
                    <br /><br />
                    <asp:FileUpload ID="FileUploadControl" runat="server"  />
                    <asp:Button runat="server" ID="UploadButton" text="Subir Archivo" onclick="UploadButton_Click" />
                    <br /><br /><br />
                    <asp:Label runat="server" id="StatusLabel" />
                </td>
            </tr>
    </table>
</fieldset>
</asp:Content>

