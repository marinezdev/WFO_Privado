<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="nuevoDocumento.aspx.cs" Inherits="wfip.promotoria.nuevoDocumento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>DOCUMENTOS DEL EXPEDIENTE</legend>
        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                <br />
            </div>
            <div style="width: 90%; margin: auto">
                <table id="tblInfTramite" style="width: 100%">
                    <tr>
                        <td>DOCUMENTOS DISPONIBLES</td>
                        <td>DOCUMENTOS INTEGRADOS</td>
                    </tr>
                    <tr>
                        <td style="height:280px; width:50%; vertical-align:top">
                            <asp:ListBox ID="lstCatalogo" runat="server" Height="100%" Width="95%" >
                            </asp:ListBox>
                        </td>
                        <td style="height:280px; vertical-align:top">
                            <asp:ListBox ID="lstTramite" runat="server" Height="100%" Width="95%">
                            </asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:FileUpload ID="FileUpload1" runat="server" Width="100%" /></td>
                        <td style="text-align:left"><asp:Button ID="btnSubir" runat="server" Text="Subir" CssClass="boton" OnClick="btnSubir_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2"><hr /></td>
                    </tr>
				    <tr>
					    <td>OBSERVACIONES</td>
                    </tr>
                    <tr>
					    <td>
                            <asp:TextBox ID="txComentarios" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
					    </td>
				    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center"><asp:Button ID="btnContinuar" runat="server" Text="Finalizar" CssClass="boton" OnClick="btnContinuar_Click" /></td>
                    </tr>
                </table>
            </div>
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
