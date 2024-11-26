<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="subirAgentesR.aspx.cs" Inherits="wfip.supervision.subirAgentesR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>Catálogo de agentes </legend>
             <br /><br />
        <div>
            <table style="width:100%">
                <tr>
                    <td>
                       <label>
                           El archivo a procesar deberá estar en formato CSV, Ningún campo podrá contener comas
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
                      <div class="form-group form-inline">
                        <asp:FileUpload ID="FileUploadControl" runat="server" accept=".csv"  />
                        <asp:Button  CssClass="btn btn-success" runat="server" ID="UploadButton" text="Procesar" OnClick="UploadButton_Click" />
                        <br /><br />
                        <asp:Label runat="server" id="StatusLabel" text=" " />
                     </div>
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
    </fieldset>
</asp:Content>
