<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admFlujo.aspx.cs" Inherits="wfip.administracion.admFlujo" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/jQuerySortable.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery-sortable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () { $("ol.listaOrdenada").sortable(); });
        });

        function Guardar() {
            var continuar = false;
            if (confirm('Esta seguro que desea continuar ?')) {
                var data = $("ol.listaOrdenada").sortable("serialize").get();
                $('#cph_areaTrabajo_hdFlujoTrabajo').val(JSON.stringify(data));
                continuar = true;
                pnlMsgProcesando.Show();
            }
            return continuar;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>
            <asp:Label ID="lbNomFlujo" runat="server"></asp:Label></legend>
        <div style="text-align: right">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="boton" OnClientClick=" return Guardar();" OnClick="btnAceptar_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="boton" OnClick="btnCerrar_Click" />
        </div>
        <br />
        <div id="dvCajaCfgFlojo" style="width: 400px; margin: 0 auto;">
            <asp:Literal ID="ltFlujo" runat="server"></asp:Literal>
        </div>
    </fieldset>
    <asp:HiddenField ID="hdIdFlujo" runat="server" />
    <asp:HiddenField ID="hdFlujoTrabajo" runat="server" />
    <dx:ASPxLoadingPanel ID="pnlMsgProcesando" runat="server" ClientInstanceName="pnlMsgProcesando" Modal="true" Text="Procesando...">
    </dx:ASPxLoadingPanel>
</asp:Content>
