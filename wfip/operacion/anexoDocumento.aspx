<%@ Page Title="" Language="C#" MasterPageFile="~/operacion/operacion.Master" AutoEventWireup="true" CodeBehind="anexoDocumento.aspx.cs" Inherits="wfip.operacion.anexoDocumento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Eliminar() { 
            var continuar = false;
            if (confirm('Esta seguro que desea eliminar el documento?')) {
                continuar = true;
            }
            return continuar;
        }
        function Confirmar() {
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>ARCHIVOS ANEXOS</legend>
        <div style="width: 90%; margin: auto">
            <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
            <br />
        </div>
        <asp:Panel ID="PnlArchivosAnexos" runat="server" Width="100%">
            <table id="tblArchivos" style="width:90%; margin:auto;">
                <tr>
                    <td colspan="2">
                        <b>LISTA DE OBSERVACIONES</b>
                        <asp:Repeater ID="rpObsrv" runat="server">
                            <ItemTemplate>
                                <li><%# Eval("Observacion")%></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td style="width:50%">
                        <fieldset>
                            <legend>ARCHIVOS CON DOCUMENTOS REQUERIDOS</legend>
                            <asp:FileUpload ID="fileUpDocumento" runat="server" />
                            <asp:RegularExpressionValidator ID="rev_fileUpDocumento" runat="server" ErrorMessage="Solo PDF" ControlToValidate="fileUpDocumento" ValidationExpression= "(.*).(.pdf|.PDF|.jpg|.png)$" />
                            <asp:Button ID="btnSubirDocumento" runat="server" Text="Subir" CssClass="boton" OnClick="btnSubirDocumento_Click" /><span style="font-size: 9px">Tamaño máximo de archivo: <%= ArchivoMaximo1 %> mb</span><br />
                            <asp:ListBox ID="lstDocumentos" runat="server" Height="150px" Width="95%" SelectionMode="Single" >
                            </asp:ListBox>
                            <br />
                            <asp:Button ID="btnEliminaDocumento" runat="server" Text="Eliminar" CssClass="boton" OnClick="btnEliminaDocumento_Click" />
                        </fieldset>
                    </td>
                    <td >
                        <fieldset id="divisionAdicionales" runat="server">
                            <legend>ARCHIVOS ADICIONALES</legend>
                            <asp:FileUpload ID="fileUpInsumo" runat="server" />
                            <asp:Button ID="btnSubirInsumo" runat="server" Text="Subir" CssClass="boton" OnClick="btnSubirInsumo_Click" /><span style="font-size: 9px">Tamaño máximo de archivo: <%= ArchivoMaximo2 %> mb</span><br />
                            <asp:ListBox ID="lstInsumos" runat="server" Height="150px" Width="95%" SelectionMode="Single" >
                            </asp:ListBox>
                            <br />
                            <asp:Button ID="btnEliminaInsumo" runat="server" Text="Eliminar" CssClass="boton" OnClick="btnEliminaInsumo_Click" />
                        </fieldset>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div id="dvBotones" style="text-align: right">
            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="boton" OnClientClick="return Confirmar();" OnClick="BtnContinuar_Click" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
