<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="subirLaboratorios.aspx.cs" Inherits="wfip.supervision.subirLaboratorios" MasterPageFile="~/supervision/supervision.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>Catálogo de Laboratorios</legend>
             <br /><br />
        <div>
            <table style="width:100%">
                <tr>
                    <td>
                       <label>
                           El archivo a procesar deberá estar en formato CSV, ningún campo podrá contener comas
                       </label>
                        <br /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="DetallePromotorias" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table  style ="width:100%">
                <tr>
                    <td style ="vertical-align:top">
                        <asp:FileUpload ID="FileUploadControl" runat="server" accept=".csv"  />
                        <asp:Button runat="server" ID="UploadButton" text="Procesar" onclick="UploadButton_Click" OnClientClick="pnlMsgProcesando.Show();" />
                        <br /><br />
                        <asp:Label runat="server" id="StatusLabel" text=" " />
                    </td>
                </tr>
                <tr>
                    <asp:TextBox id="Info" style="height:100px; width:800px" runat="server" TextMode="MultiLine"></asp:TextBox>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="UploadButton" /> 
        </Triggers>
        </asp:UpdatePanel>

        <dx:ASPxLoadingPanel ID="pnlMsgProcesando" runat="server" ClientInstanceName="pnlMsgProcesando" Modal="true" Text="Procesando...">
        </dx:ASPxLoadingPanel>


    </fieldset>
</asp:Content>
