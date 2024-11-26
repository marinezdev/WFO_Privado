<%@ Page Title="" Language="C#" MasterPageFile="~/operacion/operacion.Master" AutoEventWireup="true" CodeBehind="consultaTramite2.aspx.cs" Inherits="wfip.operacion.consultaTramite2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v17.2" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramitesEspera').DataTable({
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
                    "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
                }
            });
        });

        // Evitar doble click
        var isSubmitted = false;
        function preventMultipleSubmissions(itemHtmlClientId) {
            if (!isSubmitted) {
                $(itemHtmlClientId).val('procesando...');
                isSubmitted = true;
                return true;
            }
            else {
                return false;
            }
        }

        function treeList_CustomDataCallbackHold(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedHold(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function treeList_CustomDataCallbackCM(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedCM(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function treeList_CustomDataCallbackSuspender(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedSuspender(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function treeList_SelectionChangedCancelar(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function treeList_CustomDataCallbackRechazar(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedRechazar(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function treeList_CustomDataCallbackCMRevProspecto(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedCMRevProspecto(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function treeList_CustomDataCallbackCMCitaReprogramada(s, e) {
            document.getElementById('treeListCountCell').innerHTML = e.result;
        }
        function treeList_SelectionChangedCMCitaReprogramada(s, e) {
            window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function ConfirmarProceso(mensaje) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(mensaje)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function CitaReprogramada(pParametros, pTipo) {
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            if (Page_ClientValidate() == true)
            {
                $("#<%=hfModoRechazo.ClientID%>").val(pTipo);
                $("#dvEspacioPDF").hide();
                pnlPopMotivosCMCitaReprogramada.Show();
                pnlPopMotivosCMCitaReprogramada.PerformCallback(pParametros);
            }
            else
            {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
        }

        function RevProspecto(pParametros, pTipo)
        {
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            if (Page_ClientValidate() == true) {
                $("#<%=hfModoRechazo.ClientID%>").val(pTipo);
                $("#dvEspacioPDF").hide();
                pnlPopMotivosCMRevProspecto.Show();
                pnlPopMotivosCMRevProspecto.PerformCallback(pParametros);
            }
            else
            {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
        }

        function showConfirmTramite(pParametros, pTipo) {
            pnlPopProcesarTramite.Show();
            pnlPopProcesarTramite.PerformCallback(pParametros);
        }

        function querySeguimientoTramite(pParametros, pTipo) {
            pnlPopSigueTramiteStatus.Show();
            pnlPopSigueTramiteStatus.PerformCallback(pParametros);
        }

        function Hold(pParametros, pTipo)
        {
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            var observaciones = document.getElementById('<%=txComentariosPrv.ClientID%>').value;
            if (observaciones.length > 0) {
                $("#<%=hfModoRechazo.ClientID%>").val(pTipo);
                $("#dvEspacioPDF").hide();
                pnlPopMotivosHold.Show();
                pnlCallbackMotHold.PerformCallback(pParametros);
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
        }

        function CitaMedica(pParametros, pTipo)
        {
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            var observaciones = document.getElementById('<%=txComentariosPrv.ClientID%>').value;
            if (observaciones.length > 0) {
                $("#<%=hfModoRechazo.ClientID%>").val(pTipo);
                $("#dvEspacioPDF").hide();
                pnlPopMotivosCM.Show();
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
        }

        function fnAplicaCM() {
             var ObservacionPublicaCM = document.getElementById('<%=txtObservacionesCM.ClientID%>').value;
            if (ObservacionPublicaCM.length > 0)
            {
                pnlPopMotivosCM.Hide();
                cierraTodo();
                btnCtrlAplicaCM.DoClick();
            }
            else
            {
                document.getElementById('<%=txtObservacionesCM.ClientID%>').focus();
                alert("LAS OBSERVACIONES SON REQUERIDAS.");
            }
        }

        function fnAplicaCMCitaReprogramada() {
            pnlPopMotivosCMCitaReprogramada.Hide();
            cierraTodo();
            btnCtrlAplicaCMCitaReprogramada.DoClick();
        }

        function fnAplicaCMRevProspecto() {
            pnlPopMotivosCMRevProspecto.Hide();
            cierraTodo();
            btnCtrlAplicaCMRevProspecto.DoClick();
        }

        function fnAceptarSeleccion() {
            pnlPopProcesarTramite.Hide();
            cierraTodo();
            btnCtrlAceptaSelccion.DoClick();
        }

        

        function fnAplicaHold() {
            pnlPopMotivosHold.Hide();
            cierraTodo();
            btnCtrlAplicaHold.DoClick();
        }

        function HoldAsigna() {
            $("#dvEspacioPDF").hide();
        }

        function Suspender(pParametros, pTipo) {
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            var observaciones = document.getElementById('<%=txComentariosPrv.ClientID%>').value;

            if (observaciones.length > 0)
            {
                pnlPopMotivosSuspender.Show();
                pnlCallbackMotSuspender.PerformCallback(pParametros);
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
        }

        function fnAplicaSuspender() {
            cierraTodo();
            btnSuspenderDir.DoClick();
        }


        function Cancelacion(pParametros, pTipo) {
            cierraTodo();
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            var observaciones = document.getElementById('<%=txComentariosPrv.ClientID%>').value;
            if (observaciones.length > 0)
            {
                pnlPopCancelar.Show();
                pnlCallbackCancelar.PerformCallback(pParametros);
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
        }

        function fnAplicaCancelacion()
        {
            
            var ObservacionPublicaRechazo = document.getElementById('<%=txObservacionesCancelacion.ClientID%>').value;
            if (ObservacionPublicaRechazo.length > 0)
            {
                btnCtrlCancelacion.DoClick();
            }
            else
            {
                document.getElementById('<%=txObservacionesCancelacion.ClientID%>').focus();
                alert("LAS OBSERVACIONES SON REQUERIDAS.");
            }
        }

        function PCI(pParametros, pTipo) {
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            if (Page_ClientValidate() == true) {
                $("#<%=hfModoRechazo.ClientID%>").val(pTipo);
                $("#dvEspacioPDF").hide();
                pnlPopMotivosPCI.Show();
                pnlCallbackMotPCI.PerformCallback(pParametros);
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
        }

        function fnAplicaPCI() {
            cierraTodo();
        }

        function PCIAsigna(pParametros, pTipo) {
            cierraTodo();
            btnCtrlAplicaHoldDir.DoClick();
        }

        function RechazarAsigna(pParametros, pTipo) {
            cierraTodo();
            pnlPopMovitosRechazo.Show();
            pnlCallbackMotRechazo.PerformCallback(pParametros);
        }

        function Rechazar(pParametros, pTipo) {
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            if (Page_ClientValidate() == true) {
                $("#<%=hfModoRechazo.ClientID%>").val(pTipo);
                $("#dvEspacioPDF").hide();
                pnlPopMovitosRechazo.Show();
                pnlCallbackMotRechazo.PerformCallback(pParametros);
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
        }

        function fnAplicaRechazo2()
        {
            var ObservacionPublicaRechazo = document.getElementById('<%=txObservacionesRechazo.ClientID%>').value;
            if (ObservacionPublicaRechazo.length > 0)
            {
                btnCtrlAplicaRechazodDir.DoClick();
            }
            else
            {
                document.getElementById('<%=txObservacionesRechazo.ClientID%>').focus();
                alert("LAS OBSERVACIONES SON REQUERIDAS.");
            }
        }

        function fnAplicaRechazo() {
            var strCadena = "";
            var items = lstMotivosRechazo.GetSelectedItems();

            for (var i = items.length - 1; i >= 0; i = i - 1) {
                if (strCadena.length > 0)
                    strCadena = strCadena + ";" + items[i].value;
                else
                    strCadena = items[i].value;
            }

            if (strCadena.length > 0) {
                $("#<%=hfCadenaMotivosRechazo.ClientID%>").val(strCadena);
                pnlPopMovitosRechazo.Hide();
                cierraTodo();
                pnlMsgProcesando.Show();
                btnCtrlAplicaRechazo.DoClick();
            }
            else {
                alert('No seleccionó ningún motivo de Rechazo');
            }

        }

        function Continuar() {
            var continuar = false;
            if (Page_ClientValidate('Observaciones') == true) {
                if (confirm('Esta seguro que desea aceptar el Trámite ?')) {
                    cierraTodo();
                    continuar = true;
                }
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
            return continuar;
        }

        function CMCartaPrevia() {
            // var idTramite = document.getElementById('<%= hdIdTramite.ClientID %>').value
            // window.open('../promotoria/CitaPDF.aspx?Id=' + idTramite, '_blank', 'PDF MetLife', 'width=400,height=500')
        }

        function ContinuarTramite() {
            var continuar = false;
            if (Page_ClientValidate('Observaciones') == true) {
                if (confirm('Esta seguro que desea procesar el Trámite ?')) {
                    cierraTodo();
                    continuar = true;
                }
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
            return continuar;
        }

        function ContinuarNoAplica() {
            var continuar = false;
            if (Page_ClientValidate('Observaciones') == true) {
                pnlPopNoAplica.Show();
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
            return continuar;
        }

        


        function fnNoAplica() {
            var ctrObservaciones = document.getElementById('<%= txtObservacionNoAplica.ClientID %>').value
            if (ctrObservaciones.length == 0) {
                alert('Debe indicar las observaciones');
                ctrObservaciones.setfocus();
            }
            else {
                pnlPopNoAplica.Hide();
                cierraTodo();
                btnCtrlNoAplica.DoClick();
            }
        }


        function ContinuarProcesable() {
            var continuar = false;
            if (Page_ClientValidate() == true) {
                if (confirm('Esta seguro que desea establecer como Procesable el Trámite ?')) {
                    cierraTodo();
                    continuar = true;
                }
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
            return continuar;
        }

        function fnPausarTramite() {
            var ctrObservaciones = document.getElementById('<%= txtObservacionPausarTramite.ClientID %>').value
            if (ctrObservaciones.length == 0) {
                alert('Debe indicar las observaciones');
                ctrObservaciones.setfocus();
            }
            else {
                pnlPopPausaTramite.Hide();
                cierraTodo();
                btnCtrlPausarTramite.DoClick();
            }
        }

        function PausarTramite(pParametros, pTipo) {
            $("#<%=hfCadenaMotivosRechazo.ClientID%>").val("");
            $("#<%=hfModoRechazo.ClientID%>").val(pTipo);
            $("#dvEspacioPDF").hide();
            pnlPopPausaTramite.Show();
        }

        function Pausa() {
            var continuar = false;
            if (Page_ClientValidate() == true) {
                if (confirm('Esta seguro que desea poner en Pausa el Trámite ?')) {
                    cierraTodo();
                    continuar = true;
                }
            }
            else {
                alert('LAS OBSERVACIONES SON REQUERIDAS');
            }
            return continuar;
        }

        function changeImage(btnImg) {
            if (btnImg.src.match("stop")) {
                btnImg.src = "../img/play.png";
                $("#cph_areaTrabajo_hdEnLinea").val("1");
            } else {
                btnImg.src = "../img/stop.png";
                $("#cph_areaTrabajo_hdEnLinea").val("0");
            }
        }

        function Descarga(url) {
            console.log(url);
            window.open('DescargarInsumos.aspx?tipo=insumo&file=' + url, 'MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=450,height=300');
        }

        function cierraTodo() {
            popDocumento.Hide();
            popBitacora.Hide();
            popBitacoraPrivada.Hide();
            PopInsumos.Hide();
            pnlPopSigueTramiteStatus.Hide();
            pnlPopMotivosCMCitaReprogramada.Hide();
            pnlPopMotivosCMRevProspecto.Hide();
            pnlPopProcesarTramite.Hide();
            pnlPopMotivosHold.Hide();
            pnlPopMotivosSuspender.Hide();
            pnlPopMotivosPCI.Hide();
            pnlPopMovitosRechazo.Hide();
            pnlPopPausaTramite.Hide();
            pnlPopNoAplica.Hide();
            pnlPopMotivosCM.Hide();
            pnlPopCancelar.Hide();

            $("#dvBotones").hide();
        }

        function MASK(idForm, mask, format) {
            var n = $("#" + idForm).val();
            if (format == "undefined") format = false;
            if (format || NUM(n)) {
                dec = 0, point = 0;
                x = mask.indexOf(".") + 1;
                if (x) { dec = mask.length - x; }

                if (dec) {
                    n = NUM(n, dec) + "";
                    x = n.indexOf(".") + 1;
                    if (x) { point = n.length - x; } else { n += "."; }
                } else {
                    n = NUM(n, 0) + "";
                }
                for (var x = point; x < dec; x++) {
                    n += "0";
                }
                x = n.length, y = mask.length, XMASK = "";
                while (x || y) {
                    if (x) {
                        while (y && "#0.".indexOf(mask.charAt(y - 1)) == -1) {
                            if (n.charAt(x - 1) != "-")
                                XMASK = mask.charAt(y - 1) + XMASK;
                            y--;
                        }
                        XMASK = n.charAt(x - 1) + XMASK, x--;
                    } else if (y && "$0".indexOf(mask.charAt(y - 1)) + 1) {
                        XMASK = mask.charAt(y - 1) + XMASK;
                    }
                    if (y) { y-- }
                }
            } else {
                XMASK = "";
            }
            $("#" + idForm).val(XMASK);
            return XMASK;
        }

        function NUM(s, dec) {
            for (var s = s + "", num = "", x = 0; x < s.length; x++) {
                c = s.charAt(x);
                if (".-+/*".indexOf(c) + 1 || c != " " && !isNaN(c)) { num += c; }
            }
            if (isNaN(num)) { num = eval(num); }
            if (num == "") { num = 0; } else { num = parseFloat(num); }
            if (dec != undefined) {
                r = .5; if (num < 0) r = -r;
                e = Math.pow(10, (dec > 0) ? dec : 0);
                return parseInt(num * e + r) / e;
            } else {
                return num;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="mensajesInformativos" runat="server"></asp:UpdatePanel>
    
    <fieldset>
        <legend><asp:Label ID="lbNmMesa" runat="server" Text="MESA"></asp:Label><asp:Label ID="IdTramiteSe" runat="server" Visible="false"></asp:Label></legend>
        <ajaxToolkit:TabContainer ID="tabGeneral" runat="server" Width="100%" ActiveTabIndex="0">
            <ajaxToolkit:TabPanel runat="server" ID="pnlTablTramiteTrabajo" HeaderText="TRAMITE">
                <ContentTemplate>
                    <div style="width: 98%; margin: auto">
                        <table id="tblDatos" style="width: 100%;">
                            <tr>
                                    
                                <td style="width:60%; vertical-align: top; font-size:14px; "; >
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3"><asp:Literal ID="ltInfTipoTramite"  runat="server"></asp:Literal></span>
                                    <hr />
                                    <asp:UpdatePanel id="DatosTramiteInformacion" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table border="0" style="width: 100%;">
                                                
                                                <tr>
                                                    <td style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"><asp:Label ID="Label55" runat="server" Font-Names="Britannic Bold" Font-Size="12px" > Fecha de Registro: </asp:Label></td>
                                                    <td colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;">
                                                        <asp:Label ID="InfoFechaRegistro" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="TramiteInformacionCPDES" runat="server" Visible="false" >
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Folio CPDES</td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Estatus CPDES</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFolioCPDES" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoEstatusCPDES" runat="server" ></asp:Label>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                    <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                       
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="CantidadesVida" runat="server" Visible="false" >
                                                    <tr>
                                                        <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Moneda
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Suma Asegurada Básica
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Suma Asegurada de Pólizas Vigentes
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoMoneda" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoSumaAseguradaBasica" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoSumaAseguradaPolizasVigentes" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Prima Total de Acuerdo a Cotización
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoPrimaTotal" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="CantidadesGastosMedicos" runat="server" Visible="false" >
                                                    <tr>
                                                        <td style="width:35%; background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Moneda
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Suma Asegurada Básica
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Prima Total de Acuerdo a Cotización
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoMonedaGM" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoSumaAseguradaBasicaGM" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoPrimaTotalGM" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Hombre Clave
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoOneShotTexto" Text="NO" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoMetaLifeTexto" Text="NO" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoHobreClave" Text="NO" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoOneShotValue" Text="NO" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoMetaLifeValue" Text="NO" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoGrandeSumas" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"> 
                                                        INFORMACIÓN DE PÓLIZA
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Clave Promotoria
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Región
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Gerente Comercial 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoClave" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoRegion" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoGerente" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Ejecutivo Comercial 
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Ejecutivo Front 
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Solicitud / Número de orden 
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoEjecutivo" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoEjecutivoFront" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoNumero" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Fecha solicitud 
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Tipo de contratante 
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoFechaSolicitud" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoContratante" runat="server" ></asp:Label>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        INFORMACIÓN CONTRATANTE 
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="InfoPrsFisica" runat="server" Visible="true" >
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nombre(s) 
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Apellido Paterno
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Apellido Materno
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFNombre" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFApellidoP" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFApellidoM" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Sexo
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            RFC
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nacionalidad
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFSexo" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoFRFC" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoFNacionalidad" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Fecha Nacimiento
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFFechaNa" runat="server" ></asp:Label>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="InfoPrsMoral" runat="server" Visible="true" >
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nombre
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Fecha de Constitución
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            RFC
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoMNombre" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoMFechaConsti" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoMRFC" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="InfoDiContratante" runat="server" Visible="false" >
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        ¿El solicitante es el <br />mismo que el contratante?
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoFContratante" runat="server" ></asp:Label>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            INFORMACIÓN TITULAR 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nombre(s) 
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Apellido Paterno
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Apellido Materno
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTNombre" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTApellidoP" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTApellidoM" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nacionalidad
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Sexo
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Fecha Nacimiento
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTNacionalidad" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTSexo" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTNacimiento" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td colspan="3" style="font-size: 25px; color:#007CC3; font-weight: bold; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        
                                                        <asp:Label ID="LabelCapturaExcel" Font-Size="Large" runat="server" Visible="false" Text="CAPTURA POR EXCEL" ></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Panel ID="TablaBeneficiarios" runat="server" >
                                        <table style="width: 100%;" border="0">
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">Tabla Beneficiarios</span>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rptRiesgosInfo" runat="server" >
                                                            <HeaderTemplate>
                                                                <table id="rptRiesgosTabla" style="width:100%; font-size:9px" class="display" >
                                                                    <thead>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ramo</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">ID</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Vigencia</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Parentesco</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Zona</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Plan</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Factor</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Riesgo Ocupacional</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Extraprima Ocupacional</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Endosos</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ocupación</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Estatus</th>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="background-color: White; color: #333333; text-align:center">
                                                                    <td><%#Eval("[Ramo]")%></td>
                                                                    <td><%#Eval("[ID]")%></td>
                                                                    <td><%#Eval("[Vigencia]")%></td>
                                                                    <td><%#Eval("[Parentesco]")%></td>
                                                                    <td><%#Eval("[Zona]")%></td>
                                                                    <td><%#Eval("[Plan]")%></td>
                                                                    <td><%#Eval("[Factor]")%></td>
                                                                    <td><%#Eval("[Riesgo]")%></td>
                                                                    <td><%#Eval("[RiesgoFactor]")%></td>
                                                                    <td><%#Eval("[Endosos]")%></td>
                                                                    <td><%#Eval("[Ocupacion]")%></td>
                                                                    <td><%#Eval("[Estatus]")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rptRiesgosInfoVida" runat="server" >
                                                            <HeaderTemplate>
                                                                <table id="rptRiesgosTablaVida" style="width:100%; font-size:9px" class="display" >
                                                                    <thead>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Producto</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">SubProducto</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Vigencia</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">ExtraPrima</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Habito</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Parentesco</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Endosos</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ocupación</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Estatus</th>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="background-color: White; color: #333333; text-align:center">
                                                                    <td><%#Eval("[Producto]")%></td>
                                                                    <td><%#Eval("[SubProducto]")%></td>
                                                                    <td><%#Eval("[Vigencia]")%></td>
                                                                    <td><%#Eval("[ExtraPrima]")%></td>
                                                                    <td><%#Eval("[Habito]")%></td>
                                                                    <td><%#Eval("[Parentesco]")%></td>
                                                                    <td><%#Eval("[Endosos]")%></td>
                                                                    <td><%#Eval("[Ocupacion]")%></td>
                                                                    <td><%#Eval("[Estatus]")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </table>
                                    </asp:Panel>
                                    <asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional" Visible="False">
                                    <ContentTemplate>
                                    <table style="width: 100%;">
                                        <asp:Label ID="IdTramite" runat="server" Font-Names="Britannic Bold" Font-Size="12px" Visible="false" ></asp:Label>
                                        <asp:Label ID="Label8" runat="server" Font-Names="Britannic Bold" Font-Size="12px" > Fecha de Registro: </asp:Label>
                                        <asp:Label ID="FechaRegistro" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                        <asp:Panel ID="PanelEtitar" runat="server" Visible="true" Enabled="false">
                                        <tr>
                                            <td style="width:35%;">
                                                <asp:Label ID="lblProductoRamo" runat="server" Text="* Producto" Font-Bold="true" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label>
                                                <asp:DropDownList ID="LisProducto1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisProducto1_SelectedIndexChanged" Width="200px">
                                                    <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                                </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="LisProducto1" ValidationGroup="DatoTramiteCitamedica" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:33%;">
                                                <br />
                                                <asp:Label ID="lblSubProductoRamo" runat="server" Text="* Plan" Font-Bold="true" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label>
                                                <asp:DropDownList ID="LisSubproducto1" runat="server" AutoPostBack="false" Width="200px">
                                                    <asp:ListItem Value="-1">SELECCIONAR</asp:ListItem>
                                                </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="LisSubproducto1" ValidationGroup="DatoTramiteCitamedica"  ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <asp:Panel ID="InfCPDES" runat="server" Visible="false" >
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Folio CPDES</asp:Label><br />
                                                <asp:TextBox ID="lblFolioCPDES" runat="server"  Width="180px" AutoPostBack="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="lblFolioCPDES" ValidationGroup="DatoTramiteCitamedica" ForeColor="Red" ErrorMessage="*" Font-Size="16px"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Estatus CPDES</asp:Label><br />
                                                <asp:DropDownList ID="EstatusCPDES" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ActividadCPDES_SelectedIndexChanged" Width="150px">
                                                    <asp:ListItem Value="">SELECCIONE</asp:ListItem>
                                                    <asp:ListItem Value="Sub-aceptado">SUB-ACEPTADO</asp:ListItem>
                                                    <asp:ListItem Value="Manual">MANUAL</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="EstatusCPDES" ValidationGroup="DatoTramiteCitamedica" ForeColor="Red" ErrorMessage="*" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox3"  runat="server"  Text="Elimina CPDES" Font-Names="Britannic Bold" Font-Size="12px" AutoPostBack="True" oncheckedchanged="CheckBox3_CheckedChanged"/>
                                            </td>
                                        </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td style="width:35%;">
                                                <asp:Label runat="server" ID="Moneda" Text="* Moneda" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:DropDownList ID="cboMoneda" runat="server" Width="190px" AutoPostBack="True" OnSelectedIndexChanged="CalculartSumaAsegurada"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="cboMoneda" ValidationGroup="DatoTramiteCitamedica" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:33%;"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;">
                                                <asp:Label runat="server" ID="SumaAsegurada" Text="* Suma Asegurada Básica" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:TextBox ID="txtSumaAseguradaBasica" OnTextChanged="CalculartSumaAsegurada" onChange="MASK('cph_areaTrabajo_tabGeneral_pnlTablTramiteTrabajo_txtSumaAseguradaBasica','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="180px" AutoPostBack="true"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaBasica" ValidChars="0123456789.," />
                                                <asp:RequiredFieldValidator id="RequiredFieldValidator27" InitialValue="0.00"  ControlToValidate="txtSumaAseguradaBasica" ErrorMessage="*" ValidationGroup="DatoTramiteCitamedica" runat="server" ForeColor="Red"/>
                                            </td>
                                            <asp:Panel ID="SumaAseguradaPolizasVigentes" runat="server" Visible="false">
                                            <td>
                                                <asp:Label runat="server" ID="labelSumaAseguradaPolizasVigentes" Text="* Suma Asegurada de Pólizas Vigentes " Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:TextBox ID="txtSumaAseguradaPolizasVigentes"  OnTextChanged="CalculartSumaAsegurada" onChange="MASK('cph_areaTrabajo_tabGeneral_pnlTablTramiteTrabajo_txtSumaAseguradaPolizasVigentes','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="200px" AutoPostBack="true"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaPolizasVigentes" ValidChars="0123456789.," />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="Label32" Text="* Prima Total de Acuerdo a Cotización" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:TextBox ID="txtPrimaTotal" OnTextChanged="PrimaTotalGrandesSumas" onChange="MASK('cph_areaTrabajo_tabGeneral_pnlTablTramiteTrabajo_txtPrimaTotal','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="180px" AutoPostBack="true"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19"  runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotal" ValidChars="0123456789.," />
                                            </td>
                                            </asp:Panel>
                                            <asp:Panel ID="SumaAseguradaPolizasVigentesGMM" runat="server" Visible="false">
                                            <td>
                                                <asp:Label runat="server" ID="label61" Text="Prima Total de Acuerdo a Cotización " Font-Names="Britannic Bold" Font-Size="12px"></asp:Label>
                                                <asp:TextBox ID="txtPrimaTotalGMM" onChange="MASK('cph_areaTrabajo_tabGeneral_pnlTablTramiteTrabajo_txtPrimaTotalGMM','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="200px" AutoPostBack="true"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"  runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotalGMM" ValidChars="0123456789.," />
                                            </td>
                                        </asp:Panel>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="3" >
                                                <asp:Label runat="server" ID="label63" Text="Hombre Clave" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label>
                                                <asp:CheckBox ID="CheckHombreClave"  runat="server" AutoPostBack="True" Text="Si" Font-Names="Britannic Bold" Font-Size="12px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" >
                                                <asp:Label runat="server" ID="label83" Text="OneShot" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label>
                                                <asp:CheckBox ID="CheckOneShot"  runat="server" AutoPostBack="True" Text="Si" Font-Names="Britannic Bold" Font-Size="12px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align:center;">
                                                <asp:Label ID="PrimaTotalGrandeSumas" runat="server" Font-Bold="true" ForeColor="#5B8212" Font-Size="11px"></asp:Label>
                                                <asp:Label ID="GrandeSumas" runat="server" Font-Bold="true" ForeColor="#5B8212" Font-Size="11px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" >
                                                <asp:Label runat="server" ID="label84" Text="Metalife Especial" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label>
                                                <asp:CheckBox ID="CheckMetalifeEspecial"  runat="server" AutoPostBack="True" Text="Si" Font-Names="Britannic Bold" Font-Size="12px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center" colspan="3">
                                                <br />
                                                <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN DE PÓLIZA</span>
                                                 <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Clave Promotoria</asp:Label><br />
                                                <asp:TextBox ID="texClave" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Región </asp:Label><br />
                                                <asp:TextBox ID="texRegion" runat="server" MaxLength="50" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Gerente Comercial  </asp:Label><br />
                                                <asp:TextBox ID="texGerente" runat="server" MaxLength="50" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Ejecutivo Comercial </asp:Label><br />
                                                <asp:TextBox ID="texEjecutivo" runat="server" MaxLength="50" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label64" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Ejecutivo Front </asp:Label><br />
                                                <asp:TextBox ID="texEjecutivoFront" runat="server" MaxLength="50" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Solicitud / Número de orden </asp:Label><br />
                                                <asp:TextBox ID="texNumeroOrden" runat="server" MaxLength="15" Width="180px" AutoPostBack="false" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Fecha solicitud  </asp:Label><br />
                                                <dx:ASPxDateEdit ID="dtFechaSolicitud" runat="server" Theme="Material" EditFormat="Custom" Width="190" Caption="" >
                                                    <TimeSectionProperties>
                                                        <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                    </TimeSectionProperties>
                                                    <CalendarProperties>
                                                        <FastNavProperties DisplayMode="Inline" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="dtFechaSolicitud" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="Label17" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Tipo de contratante </asp:Label><br />
                                                <asp:DropDownList ID="cboTipoContratante" runat="server" Width="190px" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" >
                                                    <asp:ListItem Value="0">SELECCIONE</asp:ListItem>
                                                    <asp:ListItem Value="Fisica">PERSONA FÍSICA</asp:ListItem>
                                                    <asp:ListItem Value="Moral">PERSONA MORAL</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="pnPrsFisica" runat="server" Visible="false">
                                            <tr>
                                                <td colspan="3" style="text-align:center"><span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN CONTRATANTE </span><hr /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label24" Text="* Nombre(s)" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAPaterno" Text="* Apellido Paterno" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="txApPat" runat="server" MaxLength="64" Width="180px"  onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" controltovalidate="txApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAMaterno" Text="* Apellido Materno" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="txApMat" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" ></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="txApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label18" Text="* Sexo" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:DropDownList ID="txSexo" runat="server" Width="190px" >
                                                        <asp:ListItem Value="">SELECCIONE</asp:ListItem>
                                                        <asp:ListItem Value="Masculino">MASCULINO</asp:ListItem>
                                                        <asp:ListItem Value="Femenino">FEMENINO</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txSexo" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                                    &nbsp;
                                                    <asp:Label ID="Label7" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRFCPFisica" Text="* RFC" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="180px" ></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                                    <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                                    
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label19" Text="Nacionalidad" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <dx:ASPxComboBox ID="txNacionalidad" runat="server" Width="190px" AutoPostBack="true"  OnSelectedIndexChanged="LisNacionalidad_SelectedIndexChanged" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txNacionalidad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label20" Text="Fecha Nacimiento" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <dx:ASPxDateEdit ID="dtFechaNacimiento" runat="server" AutoPostBack="true"  Theme="Material" EditFormat="Custom" Width="190px"  OnDateChanged="dtFechaNacimiento_OnChanged" >
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                        </TimeSectionProperties>
                                                        <CalendarProperties>
                                                            <FastNavProperties DisplayMode="Inline" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" controltovalidate="dtFechaNacimiento" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                                <td VALIGN="TOP">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/reload.png" ImageAlign="AbsMiddle" CausesValidation="False" ToolTip="RFC" OnClick="dtFechaNacimiento_OnChanged" /><br />
                                                    <asp:Label ID="textRFCFisica" runat="server" Font-Size="10px" ForeColor="Crimson" ></asp:Label>
                                                </td>
                                                <td style="text-align:center;">
                                                    <asp:Label ID="textNacionalidad" runat="server" Font-Size="10px" ForeColor="Crimson" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label79" Text="Estado" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:DropDownList ID="cboEstado" runat="server" Width="210px"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="cboEstado" ErrorMessage="*" Font-Size="16px" ForeColor="Red" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="Label78" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="pnPrsMoral" runat="server" Visible="false">
                                            <tr>
                                                <td colspan="3" style="text-align:center"><span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN CONTRATANTE </span><hr /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label runat="server" ID="lblNombrePMoral" Text="* Nombre" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="txNomMoral" runat="server" MaxLength="64" Width="380px" AutoPostBack="true" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" ></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteNomMoral" runat="server" FilterMode="ValidChars" TargetControlID="txNomMoral" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ&,()" />
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txNomMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td> 
                                                    <asp:Label runat="server" ID="Label21" Text="Fecha de Constitución" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <dx:ASPxDateEdit ID="dtFechaConstitucion" runat="server" Theme="Material" EditFormat="Custom" Width="190" Caption="" AutoPostBack="true" OnDateChanged="dtFechaConstitucion_OnChanged" >
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                        </TimeSectionProperties>
                                                        <CalendarProperties>
                                                            <FastNavProperties DisplayMode="Inline"/>
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="dtFechaConstitucion" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRFCPMoral" Text="* RFC" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="txRfcMoral" runat="server" MaxLength="12" Width="180px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteRfcMoral" runat="server" FilterMode="ValidChars" TargetControlID="txRfcMoral" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                                    <asp:RegularExpressionValidator ID="revRfcMoral" runat="server" ControlToValidate="txRfcMoral" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txRfcMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnCargaRFC" runat="server" ImageUrl="~/img/reload.png" ImageAlign="AbsMiddle" CausesValidation="False" ToolTip="RFC" OnClick="dtFechaConstitucion_OnChanged" />
                                                    <asp:Label ID="TextantecedentesRFC" runat="server" Font-Size="12px" ForeColor="Crimson" ></asp:Label>

                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td colspan="3"><asp:Label runat="server" ID="Label22" Text="¿El solicitante es el mismo que el contratante?" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label>
                                                <asp:CheckBox ID="CheckBox2"  runat="server"  Text="Si" Checked="true" Font-Names="Britannic Bold" Font-Size="12px" AutoPostBack="True" oncheckedchanged="CheckBox2_CheckedChanged"/>
                                                <asp:CheckBox ID="CheckBox1"  runat="server"  Text="No" Font-Names="Britannic Bold" Font-Size="12px" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged"/> 
                                                <br />
                                            </td>
                                        </tr>
                                        <asp:Panel ID="DiferenteContratante" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="3" style="text-align:center"><span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN TITULAR </span><hr /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label23" Text="* Nombre(s)" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:TextBox ID="txTiNombre" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txTiNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator32" controltovalidate="txTiNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px" ValidationGroup="DatoTramiteCitamedica"/>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="Label25" Text="* Apellido paterno" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:TextBox ID="txTiApPat" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterMode="ValidChars" TargetControlID="txTiApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator33" controltovalidate="txTiApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px" ValidationGroup="DatoTramiteCitamedica"/>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="Label26" Text="* Apellido materno" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:TextBox ID="txTiApMat" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterMode="ValidChars" TargetControlID="txTiApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator34" controltovalidate="txTiApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px" ValidationGroup="DatoTramiteCitamedica"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label27" Text="Nacionalidad" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <dx:ASPxComboBox ID="txTiNacionalidad" runat="server" Width="190px" AutoPostBack="true" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" OnSelectedIndexChanged="LisTitNacionalidad_SelectedIndexChanged">
                                                </dx:ASPxComboBox>
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator35" controltovalidate="txTiNacionalidad" ForeColor="Crimson" errormessage="*" Font-Size="16px" ValidationGroup="DatoTramiteCitamedica"/>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="Label28" Text="Sexo" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:DropDownList ID="txtSexoM" runat="server" AutoPostBack="True" Width="190px">
                                                    <asp:ListItem Value="">SELECCIONA</asp:ListItem>
                                                    <asp:ListItem Value="Masculino">MASCULINO</asp:ListItem>
                                                    <asp:ListItem Value="Femenino">FEMENINO</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txtSexoM" ForeColor="Red" InitialValue="" Font-Size="16px" ValidationGroup="DatoTramiteCitamedica"></asp:RequiredFieldValidator>
                                                &nbsp;
                                                <asp:Label ID="Label62" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="Label29" Text="Fecha Nacimiento" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <dx:ASPxDateEdit ID="dtFechaNacimientoTitular" runat="server" Theme="Material" EditFormat="Custom" Width="190" Caption="">
                                                    <TimeSectionProperties>
                                                        <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                    </TimeSectionProperties>
                                                    <CalendarProperties>
                                                        <FastNavProperties DisplayMode="Inline"/>
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator37" controltovalidate="dtFechaNacimientoTitular" ForeColor="Crimson" errormessage="*" Font-Size="16px" ValidationGroup="DatoTramiteCitamedica"/>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td>
                                               <asp:Label runat="server" ID="Label77" Text="Estado " Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                <asp:DropDownList ID="cboEstado2" runat="server" Width="190px"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="cboEstado2" ErrorMessage="Estado" Font-Size="16px" ForeColor="Red" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label76" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                           </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="textTitularNacionalidad" runat="server" ForeColor="Crimson" ></asp:Label>
                                            </td>
                                        </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td colspan="3" style="text-align:center"><span runat="server" id="capturaExcel" style="font-size: 14px; font-weight: bold; color: #007CC3">CAPTURA POR EXCEL </span><hr /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3"><asp:Label runat="server" ID="Label82" Text="¿Se realizara captura por Excel? " Font-Names="Britannic Bold" Font-Size="12px"></asp:Label>
                                                <asp:CheckBox ID="CheckBoxExcel"  runat="server"  Text="Si" Checked="False" Font-Names="Britannic Bold" Font-Size="12px" AutoPostBack="True" />
                                                <br />
                                            </td>
                                        </tr>
                                        </asp:Panel>
                                        
                                        <tr>
                                            <td colspan="3"><br /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="CancelarCita" runat="server" Text="Cancelar Cita Medica" CssClass="boton" CausesValidation="false"  Visible="false" OnClick="BtnCancelarCita" Enabled="false"/>
                                                <asp:Button ID="ProgramarCita" runat="server" Text="Programar Cita Medica " CssClass="boton" ValidationGroup="DatoTramiteCitamedica" Visible="false" OnClick="CitasMedicas"  Enabled="false"/>
                                            </td>
                                            <td><asp:Label ID="MSresultado2" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label></td>
                                            <td style="text-align:center;">
                                                <asp:Label ID="MensajesRFC" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>&nbsp;
                                                <asp:Label ID="SumaBasica" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="GuardarCambios" runat="server" Text="Guardar Cambios" CssClass="boton" ValidationGroup="DatoTramiteCitamedica" Visible="false" OnClick="BtnContinuar_Click"  Enabled="false"/>
                                                <asp:Button ID="Editar" runat="server" Text="Editar" CssClass="boton" CausesValidation="false"  Visible="true" OnClick="BtnEditar_Click"/>

                                                <asp:Button ID="GuardarCambiosAdmision" runat="server" Text="Guardar Cambios" CssClass="boton" ValidationGroup="DatoTramiteCitamedica" Visible="false" OnClick="BtnContinuarAdmision_Click"  Enabled="false"/>
                                                <asp:Button ID="EditarAdmision" runat="server" Text="Editar" CssClass="boton" CausesValidation="false"  Visible="true" OnClick="BtnEditarAdmision_Click"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button 
                                                            ID="btnGenerarCartaPrevia"  
                                                            runat="server" 
                                                            Text="Mostrar Carta Previa" 
                                                            CssClass="boton" 
                                                            CausesValidation="false" 
                                                            OnClientClick="return CMCartaPrevia();"
                                                            OnClick="btnMostrarCartarPrevia_clic"
                                                            Visible="false"  
                                                            Enabled="true"
                                                            />
                                            </td>
                                        </tr>
                                    </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                <asp:UpdatePanel id="SeleccionDatos" runat="server" UpdateMode="Conditional" Visible="False">
                                    <ContentTemplate>
                                        <asp:Panel ID="Seleccion" runat="server" Visible="true" >
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td colspan="3" style="text-align:center">
                                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3; align-content:center">DATOS SELECCIÓN</span>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label46" Text="Ramo" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:DropDownList ID="RamoSelecion" runat="server" AutoPostBack="True" Width="190px" OnSelectedIndexChanged="RamoSelecion_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="reqRamo" ControlToValidate="RamoSelecion" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label47" Text="ID" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:TextBox ID="TextID" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="reqID" ControlToValidate="TextID" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label48" Text="Vigencia" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <dx:ASPxDateEdit ID="FechaVigencia" runat="server" Theme="Material" EditFormat="Custom" Width="190" Caption="">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                            </TimeSectionProperties>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline"/>
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                        <asp:RequiredFieldValidator runat="server" ID="reqFechaVigencia" ControlToValidate="FechaVigencia" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <asp:Panel ID="CatRiesgo" runat="server" Visible="false">
                                                        <td>
                                                            <asp:Label runat="server" ID="Label51" Text="Riesgo ocupacional" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                            <asp:DropDownList ID="RiesgoCatalogo" runat="server" AutoPostBack="True" Width="190px"  OnSelectedIndexChanged="RiesgoCatalogo_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="reqRiesgoCatalogo" ControlToValidate="RiesgoCatalogo" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label50" Text="Extraprima Ocupacional" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                            <asp:TextBox ID="TextFactor" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" Enabled="false"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="FechaVigencia" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="CatRiesgoCaptura" runat="server" Visible="true">
                                                        <td>
                                                            <asp:Label runat="server" ID="Label52" Text="Riesgo ocupacional" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                            <asp:TextBox ID="TextRiesgo" runat="server" MaxLength="150" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="reqTextRiesgo" ControlToValidate="TextRiesgo" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label53" Text="Extraprima Ocupacional" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                            <asp:TextBox ID="TextRiesgoFactor" runat="server" MaxLength="150" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="requRiesgoFacto" ControlToValidate="TextRiesgoFactor" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </asp:Panel>
                                                    <td>
                                                        <asp:Panel ID="PanelZonas" runat="server" Visible="true">
                                                            <asp:Label runat="server" ID="LabeZona" Text="Zona" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                            <asp:DropDownList ID="CatalogoZonas" runat="server" AutoPostBack="True" Width="190px" Visible="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="reqCatZonas" ControlToValidate="CatalogoZonas" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PanelPlanes" runat="server" Visible="true">
                                                            <asp:Label runat="server" ID="Label49" Text="Plan" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                            <asp:DropDownList ID="CatalogoPlanes" runat="server" AutoPostBack="True" Width="190px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="reqCatPlanes" ControlToValidate="CatalogoPlanes" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                        </asp:Panel>
                                                    </td>
                                                    <asp:Panel ID="CatFactor" runat="server" >
                                                        <td>
                                                            <asp:Label runat="server" ID="Label54" Text="Factor" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                            <asp:DropDownList ID="CatalogoFactores" runat="server" AutoPostBack="True" Width="190px" >
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="reqCatFactores" ControlToValidate="CatalogoFactores" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </asp:Panel>
                                                    <asp:Panel ID="CatFactorCaptura" runat="server">
                                                        <td>
                                                            <asp:Label runat="server" ID="Label56" Text="Factor" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                            <asp:TextBox ID="TexFactor"  onChange="MASK('cph_areaTrabajo_tabGeneral_pnlTablTramiteTrabajo_TexFactor','##,##0.0000',1)" onfocus="if(this.value == '0.0') {this.value=''}" onblur="if(this.value == ''){this.value ='0.0'}" value="0.0" runat="server" MaxLength="10" Width="180px" AutoPostBack="true"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="TexFactor" ValidChars="0123456789.," />
                                                            <asp:RequiredFieldValidator id="RequiredFieldValidator14" InitialValue="0.00"  ControlToValidate="TexFactor" ErrorMessage="*" runat="server" ForeColor="Red" ValidationGroup="Seleccion"/>
                                                        </td>
                                                    </asp:Panel>
                                                    <td>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label60" Text="Parentesco" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:DropDownList ID="CatParentesco" runat="server" AutoPostBack="True" Width="190px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator26" ControlToValidate="CatParentesco" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label72" Text="Endosos" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <dx:ASPxComboBox ID="EndososGM" runat="server" Width="190px" SelectedIndex="0"  AutoPostBack="true"  onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()">
                                                        </dx:ASPxComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="EndososGM" ForeColor="Red" ErrorMessage="*" InitialValue="" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label81" Text="* Ocupación " Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:TextBox ID="TextBoxOcupacionGM" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterMode="ValidChars" TargetControlID="TextBoxOcupacionGM" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ 123456789//*-+#$%&/()=?¡¿!" />
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator53" controltovalidate="TextBoxOcupacionGM" ForeColor="Crimson" errormessage="*" Font-Size="16px" ValidationGroup="Seleccion"/>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="ReSeleccion" runat="server" Text="Registrar" CssClass="boton" ValidationGroup="Seleccion" OnClientClick="return preventMultipleSubmissions('#cph_areaTrabajo_tabGeneral_pnlTablTramiteTrabajo_ReSeleccion');" OnClick="RegistrarSeleccion" Visible="true"/>
                                                        <asp:Button ID="GardarSeleccion" runat="server" Text="Guardar" CssClass="boton" ValidationGroup="Seleccion" OnClick="GuardarSeleccion" Visible="false"/>
                                                        <asp:Button ID="EditarSeleccion" runat="server" Text="Editar" CssClass="boton" CausesValidation="false"  OnClick="EditarSeleccionCampos" Visible="false"/>
                                                        
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="ElimaSeleccion" runat="server" Text="Eliminar" CssClass="boton-rojo" CausesValidation="false"  Visible="false" OnClick="ElimarSeleccion"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        <asp:Panel ID="TabllaSeleccion" runat="server" Visible="false" >
                                            <table style="width: 100%;" border="0">
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rptRiesgos" runat="server" >
                                                            <HeaderTemplate>
                                                                <table id="rptRiesgosTabla" style="width:100%; font-size:9px" class="display" >
                                                                    <thead>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ramo</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">ID</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Vigencia</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Parentesco</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Zona</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Plan</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Factor</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Riesgo Ocupacional</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Extraprima Ocupacional</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Endosos</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ocupación</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Eliminar</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Estatus</th>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="background-color: White; color: #333333; text-align:center">
                                                                    <td><%#Eval("[Ramo]")%></td>
                                                                    <td><%#Eval("[ID]")%></td>
                                                                    <td><%#Eval("[Vigencia]")%></td>
                                                                    <td><%#Eval("[Parentesco]")%></td>
                                                                    <td><%#Eval("[Zona]")%></td>
                                                                    <td><%#Eval("[Plan]")%></td>
                                                                    <td><%#Eval("[Factor]")%></td>
                                                                    <td><%#Eval("[Riesgo]")%></td>
                                                                    <td><%#Eval("[RiesgoFactor]")%></td>
                                                                    <td><%#Eval("[Endosos]")%></td>
                                                                    <td><%#Eval("[Ocupacion]")%></td>
                                                                    <td><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/eliminar.png" CommandName ="Consultar" CommandArgument='<%# Eval("[IdDatosSeleccion]")%>' OnCommand="ElimaSelecciones" /></td>
                                                                    <td><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl='<%# ((string)Eval("[Estatus]") == "ACEPTADO" ? "~/img/acpetado.jpg":"~/img/rechazado.jpg")%>' CommandName ="Consultar" CommandArgument='<%# Eval("[IdDatosSeleccion]")%>' OnCommand='ModificacionDatoSeleccionesGMM' /></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </table>
                                            </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                <asp:UpdatePanel id="SelccionDatosVida" runat="server" UpdateMode="Conditional" Visible="False">
                                    <ContentTemplate>
                                        <asp:Panel ID="SeleccionVida" runat="server" Visible="true" >
                                            <br />
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td colspan="3" style="text-align:center">
                                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3; align-content:center">DATOS SELECCIÓN</span>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label65" Text="Producto" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:DropDownList ID="RamoSelecionVida" runat="server" AutoPostBack="True" Width="190px" OnSelectedIndexChanged="RamoSelecionVida_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator41" ControlToValidate="RamoSelecionVida" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label66" Text="Subproducto " Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:DropDownList ID="SelecionSubProductos" runat="server" AutoPostBack="True" Width="190px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator42" ControlToValidate="SelecionSubProductos" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td><br />
                                                        <asp:Label runat="server" ID="Label68" Text="Vigencia" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <dx:ASPxDateEdit ID="FechaVigenciaVida" runat="server" Theme="Material" EditFormat="Custom" Width="190" Caption="">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                            </TimeSectionProperties>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline"/>
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator44" ControlToValidate="FechaVigenciaVida" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td>
                                                        <asp:Label runat="server" ID="Label69" Text="ExtraPrima" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:DropDownList ID="Extraprima" runat="server"  AutoPostBack="True" Width="190px" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator45" ControlToValidate="Extraprima" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label70" Text="Hábito" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:DropDownList ID="Habito" runat="server" AutoPostBack="True" Width="190px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator46" ControlToValidate="Habito" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label73" Text="Parentesco" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:DropDownList ID="CatParentescoGMM" runat="server" AutoPostBack="True" Width="190px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator49" ControlToValidate="CatParentescoGMM" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label71" Text="Endosos" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <dx:ASPxComboBox ID="Endosos" runat="server" Width="190px" SelectedIndex="0"  AutoPostBack="true"  onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()">
                                                        </dx:ASPxComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ControlToValidate="Endosos" ForeColor="Red" ErrorMessage="*" InitialValue="" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label80" Text="* Ocupación " Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <asp:TextBox ID="TextOcupacion" runat="server" MaxLength="64" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterMode="ValidChars" TargetControlID="TextOcupacion" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ 123456789//*-+#$%&/()=?¡¿!" />
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator52" controltovalidate="TextOcupacion" ForeColor="Crimson" errormessage="*" Font-Size="16px" ValidationGroup="Seleccion"/>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button1" runat="server" Text="Registrar" CssClass="boton" ValidationGroup="Seleccion" OnClick="RegistrarSeleccionVida" Visible="true"/>
                                                    </td>
                                                    <td>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label67" Text="No. De Captura" Font-Names="Britannic Bold" Font-Size="12px" Visible="false"></asp:Label><br />
                                                        <asp:TextBox ID="Nocaptura" runat="server" MaxLength="10" Width="180px" Visible="false" ></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterMode="ValidChars" TargetControlID="Nocaptura" ValidChars="1234567890abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator43" ControlToValidate="Nocaptura" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="Seleccion"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="TablaSelccionDatosVida" runat="server" Visible="true" >
                                            <table style="width: 100%;" border="0">
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rptRiesgosVida" runat="server" >
                                                            <HeaderTemplate>
                                                                <table id="rptRiesgosVidaTabla" style="width:100%; font-size:9px" class="display" >
                                                                    <thead>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Producto</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">SubProducto</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Vigencia</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">ExtraPrima</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Habito</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Parentesco</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Endosos</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ocupación</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Eliminar</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Estatus</th>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="background-color: White; color: #333333; text-align:center">
                                                                    <td><%#Eval("[Producto]")%></td>
                                                                    <td><%#Eval("[SubProducto]")%></td>
                                                                    <td><%#Eval("[Vigencia]")%></td>
                                                                    <td><%#Eval("[ExtraPrima]")%></td>
                                                                    <td><%#Eval("[Habito]")%></td>
                                                                    <td><%#Eval("[Parentesco]")%></td>
                                                                    <td><%#Eval("[Endosos]")%></td>
                                                                    <td><%#Eval("[Ocupacion]")%></td>
                                                                    <td><asp:ImageButton ID="imbtnConsultar4" runat="server" ImageUrl="~/img/eliminar.png" CommandName ="Consultar" CommandArgument='<%# Eval("[IdDatosVidaSeleccion]")%>' OnCommand="ElimaSeleccionesVida" /></td>
                                                                    <td><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl='<%# ((string)Eval("[Estatus]") == "ACEPTADO" ? "~/img/acpetado.jpg":"~/img/rechazado.jpg")%>' CommandName ="Consultar" CommandArgument='<%# Eval("[IdDatosVidaSeleccion]")%>' OnCommand='ModificacionDatoSeleccionesVida' /></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    
                                    <asp:Label ID="ContenidoSeleccion" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>

                                </td>
                                <td style="vertical-align: top" >
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align:center; border-bottom: 1px solid #ddd; background-color:#8EBB53;">
                                                <asp:Literal ID="ltInfFolio" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-bottom: 1px solid #ddd; background-color:#F7F7F7;">
                                                <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-bottom: 1px solid #ddd;">
                                                <asp:Literal ID="ltInfProducto" runat="server"></asp:Literal>
                                                <asp:Repeater ID="rptTramite" runat="server" >
                                                    <HeaderTemplate>
                                                        <table id="tblTramite" style="width:100%" class="display" >
                                                            <thead>
                                                                <th scope="col" style="background-color:#1572B7; color:white;">Producto</th>
                                                                <th scope="col" style="background-color:#1572B7; color:white;">Plan</th>
                                                            </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr style="background-color: White; color: #333333; text-align:center">
                                                            <td><%#Eval("[PRODUCTO]")%></td>
                                                            <td><%#Eval("[SUBPRODUCTO]")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                 <asp:Label runat="server" ID="SubProducto" Text="Parentesco" Visible="False"></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-bottom: 1px solid #ddd;">
                                                <asp:Literal ID="lblStatusMesas" runat="server"></asp:Literal>
                                                <br />
                                                <asp:Button ID="btnSeguimientoTramitePopUp" runat="server" Text="Ver Información Reingresos" CssClass="boton" CausesValidation="False" OnClick="btnSeguimientoTramitePopUp_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:UpdatePanel id="DatosPanelKWIK" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="DatosKWIK" runat="server" Visible="false">
                                                <br />
                                                <table border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="text-align:center;" colspan="2"><span style="font-size: 14px; font-weight: bold; color: #007CC3">KWIK</span>
                                                             <hr />
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LabelKwik" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Número KWIK</asp:Label><br />
                                                            <asp:TextBox ID="TextNumKwik" runat="server" MaxLength="30"  Width="220px" AutoPostBack="false"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterMode="ValidChars" TargetControlID="TextNumKwik" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789[]" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" controltovalidate="TextNumKwik" errormessage="*" ValidationGroup="DatoKwik" Font-Size="16px" ForeColor="Crimson" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="GuardarKwik" runat="server" Text="Guardar Cambios" CssClass="boton" ValidationGroup="DatoKwik" Visible="false" OnClick="BtnContinuarKwik_Click" Enabled="false"/>
                                                            <asp:Button ID="EditarKwik" runat="server" Text="Editar" CssClass="boton" CausesValidation="false"  Visible="true" OnClick="BtnEditarKwik_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel id="DatosEjecuacion" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="DatosEjecucion" runat="server" Visible="false">
                                                <br />
                                                <table border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="text-align:center;" colspan="2"><span style="font-size: 14px; font-weight: bold; color: #007CC3">EJECUCIÓN</span>
                                                             <hr />
                                                            <asp:Label ID="Label58" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label59" runat="server" Font-Names="Britannic Bold" Font-Size="12px" >Número De Póliza De Los Sistemas Legados</asp:Label><br />
                                                            <asp:TextBox ID="TextNumPolizaSisLegado" runat="server" MaxLength="20"  Width="220px" AutoPostBack="false"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="TextNumPolizaSisLegado" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" controltovalidate="TextNumPolizaSisLegado" errormessage="*" ValidationGroup="DatoEjecucion" Font-Size="16px" ForeColor="Crimson" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="GuardarEjecucion" runat="server" Text="Guardar Cambios" CssClass="boton" ValidationGroup="DatoEjecucion" Visible="false" OnClick="BtnContinuarEjecucion_Click"  Enabled="false"/>
                                                            <asp:Button ID="EditarEjecucion" runat="server" Text="Editar" CssClass="boton" CausesValidation="false"  Visible="true" OnClick="BtnEditarEjecucion_Click"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                    <asp:Panel ID="CitaMedica" runat="server" Visible="false" Enabled="false">
                                        <br />
                                        <table style="width: 100%;" >
                                            <tr>
                                                <td style="text-align:center" colspan="2"><span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN DE CITA DE MÉDICA</span>
                                                     <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;">
                                                    <asp:Label runat="server" ID="Label31" Text="Edad" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="texEdad" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" controltovalidate="texEdad" ValidationGroup="DatoTramiteCitamedica" errormessage="*" Font-Size="16px" ForeColor="Crimson" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="texCombo" runat="server" MaxLength="5" Width="200px" AutoPostBack="false" Enabled="false" Visible="false"></asp:TextBox>
                                                    <asp:Label runat="server" ID="Label33" Text="Combo" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:DropDownList ID="listCombosCitaMed" runat="server" AutoPostBack="True"  Width="190px">
                                                        <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="listCombosCitaMed" ValidationGroup="DatoTramiteCitamedica" ErrorMessage="*" InitialValue="SELECCIONAR" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label34" Text="* Cel" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="texCel" runat="server" MaxLength="15" Width="180px" AutoPostBack="false"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterMode="ValidChars" TargetControlID="texCel" ValidChars="1234567890" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" controltovalidate="texCel" ValidationGroup="DatoTramiteCitamedica" errormessage="*" Font-Size="16px" ForeColor="Crimson" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label35" Text="* Cel Agente / Promotor" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="texCelAg" runat="server" MaxLength="15" Width="180px" AutoPostBack="false" ></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterMode="ValidChars" TargetControlID="texCelAg" ValidChars="1234567890" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" controltovalidate="texCelAg" errormessage="*" ValidationGroup="DatoTramiteCitamedica" Font-Size="16px" ForeColor="Crimson" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label36" Text="* Correo electrónico promotoria / agente" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="texCorreo" runat="server" MaxLength="150" Width="180px" AutoPostBack="false"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterMode="ValidChars" TargetControlID="texCorreo" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" controltovalidate="texCorreo" errormessage="*" ValidationGroup="DatoTramiteCitamedica"  Font-Size="16px" ForeColor="Crimson" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="texCorreo" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label37" Text="* Estado" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:DropDownList ID="LisEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisEstado_SelectedIndexChanged" Width="190px" >
                                                        <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="DatoTramiteCitamedica" ControlToValidate="LisEstado" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label38" Text="* Ciudad" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:DropDownList ID="LisCiudad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisCiudad_SelectedIndexChanged" Width="190px" >
                                                        <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ValidationGroup="DatoTramiteCitamedica" ControlToValidate="LisCiudad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label39" Text="* Laboratorio / hospital" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:DropDownList ID="LisLabHospital" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisLabHospital_SelectedIndexChanged" Width="190px" >
                                                        <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ValidationGroup="DatoTramiteCitamedica" ControlToValidate="LisLabHospital" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label40" Text="Dirección" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="TextDireccion" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label1" Text="* Fecha Hora 1" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <dx:ASPxDateEdit ID="TextFecha1" runat="server" EditFormat="Custom" Theme="Material" Width="190" AutoPostBack="True" >
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                                        </TimeSectionProperties>
                                                        <CalendarProperties>
                                                            <FastNavProperties DisplayMode="Inline" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator20" controltovalidate="TextFecha1" ValidationGroup="DatoTramiteCitamedica" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                    <asp:Label ID="texFecha1" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton id="Radio1" Value="1" GroupName="fechas" Text="Fecha Hora 1"  runat="server" Font-Names="Britannic Bold" Font-Size="12px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label2" Text="* Fecha Hora 2" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <dx:ASPxDateEdit ID="TextFecha2" runat="server" EditFormat="Custom" Theme="Material" Width="190" AutoPostBack="True" >
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                                        </TimeSectionProperties>
                                                        <CalendarProperties>
                                                            <FastNavProperties DisplayMode="Inline" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator21" controltovalidate="TextFecha2" ValidationGroup="DatoTramiteCitamedica" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                    <asp:Label ID="texFecha2" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton id="Radio2" Value="2" GroupName="fechas" Text="Fecha Hora 2"  runat="server" Font-Names="Britannic Bold" Font-Size="12px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label3" Text="* Fecha Hora 3" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <dx:ASPxDateEdit ID="TextFecha3" runat="server" EditFormat="Custom" Theme="Material" Width="190" AutoPostBack="True"  >
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                                        </TimeSectionProperties>
                                                        <CalendarProperties>
                                                            <FastNavProperties DisplayMode="Inline" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                    &nbsp;<asp:Label ID="texFecha3" Value="3" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton id="Radio3" Value="3" GroupName="fechas" Text="Fecha Hora 3"  runat="server" Font-Names="Britannic Bold" Font-Size="12px"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: 1px solid #ddd;">
                                                    <asp:Label runat="server" ID="Label4" Text="* Fecha Hora 4" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <dx:ASPxDateEdit ID="TextFecha4" runat="server" EditFormat="Custom" Theme="Material" Width="190" >
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                                        </TimeSectionProperties>
                                                        <CalendarProperties>
                                                            <FastNavProperties DisplayMode="Inline" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td style="border-bottom: 1px solid #ddd;">
                                                    <asp:RadioButton id="Radio4" Value="4" GroupName="fechas" Text="Fecha Hora 4"  runat="server" Font-Names="Britannic Bold" Font-Size="12px" />
                                                </td>
                                            </tr>
                                            <asp:Panel ID="DatosFechaRecepcion" runat="server" Visible="false">
                                                <tr>
                                                    <td style="text-align:center" colspan="2"><span style="font-size: 14px; font-weight: bold; color: #007CC3"><br />FECHA DE RECEPCIÓN DE ESTUDIOS MÉDICOS</span>
                                                         <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label57" Text="* Fecha De Recepción" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                        <dx:ASPxDateEdit ID="TextFechaRecepcion" runat="server" EditFormat="Custom" Theme="Material" Width="190" >
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties EditFormatString="hh:mm tt" />
                                                            </TimeSectionProperties>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </asp:Panel>
                                        </table>
                                        <br />
                                    </asp:Panel>
                                    
                                    <asp:Panel ID="DatosPlad" runat="server" Visible="false">
                                        <table style="width: 100%;">
                                            <asp:Panel ID="PLAD" runat="server">
                                            <tr>
                                                <td style="text-align:center;" colspan="2"><span style="font-size: 14px; font-weight: bold; color: #007CC3">PLAD</span>
                                                     <hr />
                                                    <asp:Label ID="MensajesPLAD" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label runat="server" ID="Label9" Text="* Razón Social" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="TextRazonSocial" runat="server" MaxLength="50" Width="400px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  style="width: 50%;">
                                                    <asp:Label runat="server" ID="Label10" Text="* RFC" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="TextRFC" runat="server" MaxLength="50" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td >
                                                    <asp:Label runat="server" ID="Label30" Text="* Fecha de Identificación" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <dx:ASPxDateEdit ID="TextFechaIdentificacion" runat="server" EditFormat="Custom" Theme="Material" Width="190" >
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                                        </TimeSectionProperties>
                                                        <CalendarProperties>
                                                            <FastNavProperties DisplayMode="Inline" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator3"
                                                      controltovalidate="TextFechaIdentificacion"
                                                      validationgroup="DatosPLAD"
                                                      errormessage="*"
                                                      runat="Server"
                                                       ForeColor="Red">
                                                    </asp:requiredfieldvalidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label41" Text="* Duración de la sociedad" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="TextDuracionSociedad" runat="server" MaxLength="50" Width="180px" AutoPostBack="false"></asp:TextBox>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator1"
                                                      controltovalidate="TextDuracionSociedad"
                                                      validationgroup="DatosPLAD"
                                                      errormessage="*"
                                                        ForeColor="Red"
                                                      runat="Server">
                                                    </asp:requiredfieldvalidator>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label42" Text="Nombre del representante legal" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="TextNombreRepresentante" runat="server" MaxLength="50" Width="180px" AutoPostBack="false"></asp:TextBox>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator6"
                                                      controltovalidate="TextNombreRepresentante"
                                                      validationgroup="DatosPLAD"
                                                      errormessage="*"
                                                      ForeColor="Red"
                                                      runat="Server">
                                                    </asp:requiredfieldvalidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label43" Text="Folio mercantil" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="TextFolioMercantil" runat="server" MaxLength="50" Width="180px" AutoPostBack="false"></asp:TextBox>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator7"
                                                      controltovalidate="TextFolioMercantil"
                                                      validationgroup="DatosPLAD"
                                                      errormessage="*"
                                                      ForeColor="Red"
                                                      runat="Server">
                                                    </asp:requiredfieldvalidator>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="Label44" Text="Giro mercantil" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="TextGiroMercantil" runat="server" MaxLength="50" Width="180px" AutoPostBack="false"></asp:TextBox>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator12"
                                                      controltovalidate="TextGiroMercantil"
                                                      validationgroup="DatosPLAD"
                                                      errormessage="*"
                                                        ForeColor="Red"
                                                      runat="Server">
                                                    </asp:requiredfieldvalidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="Label45" Text="Vigencia del comprobante de domicilio" Font-Names="Britannic Bold" Font-Size="12px"></asp:Label><br />
                                                    <asp:TextBox ID="TextVigencia" runat="server" MaxLength="50" Width="180px" AutoPostBack="false"></asp:TextBox>
                                                    <asp:requiredfieldvalidator id="RequiredFieldValidator13"
                                                      controltovalidate="TextVigencia"
                                                      validationgroup="DatosPLAD"
                                                      errormessage="*"
                                                      ForeColor="Red"
                                                      runat="Server">
                                                    </asp:requiredfieldvalidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td colspan="2" style="text-align:left; border-bottom: 1px solid #ddd;">
                                                    <br />
                                                    <asp:Button ID="EditarPLAD" runat="server" Text="Ediatr PLAD" CssClass="boton" CausesValidation="false" Visible="false" OnClick="Editar_PLAD"/>
                                                    <asp:Button ID="GuardaPLAD" runat="server" Text="Guardar PLAD" CssClass="boton" CausesValidation="true"  OnClick="Guardar_PLAD" validationgroup="DatosPLAD"/>
                                                    <asp:Button ID="ActualizaPLAD" runat="server" Text="Actualizar" CssClass="boton" CausesValidation="true"  Visible="false" OnClick="Actualiza_PLAD" validationgroup="DatosPLAD"/>
                                                    <asp:Button ID="EliminarPLAD" runat="server" Text="Eliminar" CssClass="boton-rojo" CausesValidation="false"  Visible="false" OnClick="Eliminar_PLAD"/>
                                                    <br /><br />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <!--<strong>OBSERVACIONES PUBLICAS</strong>-->
                                    <asp:TextBox ID="txComentarios" runat="server" TextMode="MultiLine" Width="95%" Height="50px" Visible="False"></asp:TextBox>
                                </td>

                                <td rowspan="2" style="vertical-align: central; padding-left: 10px; width: 50%">
                                    <table style="width:100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnConApoyo" runat="server" Width="100%">
                                                    <div style="width: 100%; text-align: left;">
                                                        <asp:CheckBoxList ID="lsChApoyo" runat="server" Font-Size="Smaller" Width="100%" OnSelectedIndexChanged="lsChApoyo_Changed" AutoPostBack = "True">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div id="dvCajaBtnsApoyo" style="width: 100%; text-align: center" runat="server">
                                                        <asp:Button ID="btnReenviar" runat="server" Text="Enviar" CssClass="btnVerde" OnClick="btnReenviar_Click" OnClientClick="return Reenviar();" validationgroup="Observaciones" Width="80px" />
                                                    </div>
                                                    <script type="text/javascript">
                                                        $(document).ready(function () {
                                                            $("#dvCajaBtnsApoyo").hide();
                                                            $("input[type=checkbox]").click(function () {
                                                                var chk = $("input[type='checkbox']:checked").length;
                                                                if (chk != "") {
                                                                    $("#dvCajaBtnsRegular").hide();
                                                                    $("#dvCajaBtnsApoyo").show();
                                                                }
                                                                else {
                                                                    $("#dvCajaBtnsRegular").show();
                                                                    $("#dvCajaBtnsApoyo").hide();
                                                                }
                                                            });
                                                        });
                                                        function Reenviar() {
                                                            var continuar = false;
                                                            // if (Page_ClientValidate() == true) {
                                                            //     var chk = $("input[type='checkbox']:checked").length;
                                                            //     if (chk != "") {
                                                            //         if (confirm('Esta seguro que desea Enviar el trámite ?')) { cierraTodo(); continuar = true; }
                                                            //     } else { alert('Marque la(s) mesa(s) a reenvíar.'); }
                                                            // } else { alert('LAS OBSERVACIONES SON REQUERIDAS'); }
                                                            // return continuar;


                                                            var txtObservaciones = document.getElementById('<%=txComentariosPrv.ClientID%>').value;
                                                            if (txtObservaciones.length > 0) {
                                                                if (confirm('Esta seguro que desea Enviar el trámite ?')) {
                                                                    cierraTodo();
                                                                    continuar = true;
                                                                }
                                                            }
                                                            else {
                                                                alert("Las obaservaciones son requeridas.");
                                                            }
                                                            return continuar;
                                                        }
                                                    </script>
                                                </asp:Panel>
                                            </td>
                                            <td style="vertical-align: central; padding-left: 10px; width: 50%">
                                                <asp:Panel ID="pnSendToMesa" runat="server" Width="100%">
                                                    <div style="width: 100%; text-align: left;">
                                                        <asp:RadioButtonList ID="lsOpSendToMesa" runat="server" Font-Size="Smaller" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div id="divSendToMesa" style="width: 100%; text-align: center" runat="server">
                                                        <asp:Button ID="btnSendToMesa" runat="server" Text="Enviar a Mesa" CssClass="btnVerde" OnClick="btnSendToMesa_Click"  OnClientClick="return fSendToMesa();" Width="75%" />
                                                    </div> 
                                                    <script type="text/javascript">
                                                        function fSendToMesa() {
                                                            var continuar = false;
                                                            var txtObservaciones = document.getElementById('<%=txComentariosPrv.ClientID%>').value;
                                                            if (txtObservaciones.length > 0) {
                                                                if (confirm('Esta seguro que desea Enviar el trámite ?')) {
                                                                    cierraTodo();
                                                                    continuar = true;
                                                                }
                                                            }
                                                            else {
                                                                alert("Las obaservaciones son requeridas.");
                                                            }
                                                            return continuar;

                                                            // if (Page_ClientValidate() == true) {
                                                            //     // var chk = $("input[type='checkbox']:checked").length;
                                                            //     var chk = $("input[type='radio']:checked").length;
                                                            //     if (chk != "") {
                                                            //         if (confirm('Esta seguro que desea Enviar el trámite a la mesea seleccionada?')) { cierraTodo(); continuar = true; }
                                                            //     }
                                                            //     else
                                                            //     {
                                                            //         alert('Seleccione la mesa a la cual desea enviar el trámite.');
                                                            //     }
                                                            // } else
                                                            // {
                                                            //     alert('LAS OBSERVACIONES SON REQUERIDAS');
                                                            // }
                                                            return continuar;
                                                        }
                                                    </script>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>OBSERVACIONES PRIVADAS</strong>
                                    <asp:TextBox ID="txComentariosPrv" runat="server" TextMode="MultiLine" Width="95%" Height="50px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txComentariosPrv" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ =$&%*_0123456789-,.:+*/?¿+¡\/][{}();" BehaviorID="_content_FilteredTextBoxExtender15" />
                                    <asp:RequiredFieldValidator ID="rfv_txComentariosPrv" runat="server" ErrorMessage="*" ControlToValidate="txComentariosPrv" ForeColor="Red" validationgroup="Observaciones"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table id="tblBotonesControl" style="width: 100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnMuestraPop1" runat="server" Text="Abrir Documento" RenderMode="Link" CausesValidation="False" OnClick="btnMuestraPop1_Click">
                                                </dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <dx:ASPxButton ID="btnMuestraBitacora" runat="server" Text="Bitácora públicas" AutoPostBack="False" RenderMode="Link" CausesValidation="False">
                                                </dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <dx:ASPxButton ID="btnBitacoraPrivada" runat="server" Text="Bitácora privada" AutoPostBack="False" RenderMode="Link" CausesValidation="False">
                                                </dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <dx:ASPxButton ID="btnMuestraInsumos" runat="server" Text="Insumos" AutoPostBack="False" RenderMode="Link" CausesValidation="False">
                                                </dx:ASPxButton>
                                                <br />
                                                <asp:LinkButton ID="lnkModificar" runat="server" CausesValidation="False" OnClick="lnkModificar_Click" Font-Size="12px">Agregar Documento</asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="lnkOcultaDocumento" runat="server" CausesValidation="False" Font-Size="12px" OnClientClick="$('#dvEspacioPDF').toggle(); return false;">Ocultar Documento</asp:LinkButton>
                                            </td>
                                            <td style="width: 50px; text-align: center">
                                                <!-- 20180403_RMF <img id="btnEnLinea" onclick="changeImage(this)" src="../img/play.png" />&nbsp;&nbsp; -->
                                            </td>
                                            <td style="width: 50%; text-align: right; vertical-align: top;">
                                                <div id="dvCajaBtnsRegular" style="width:100%; text-align: right; line-height:30px;" runat="server">
                                                    <asp:Label runat="server" ID="lblReingresos" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Visible="False"></asp:Label><br />
                                                    <asp:Label runat="server" ID="lblAdvertencia" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Visible="False"></asp:Label><br />
                                                    <table runat="server" style="width:100%; border:1px; text-align:center; " id="tblBotonesPrincipales">
                                                        <tr runat="server">
                                                            <td runat="server" id="tdNoAplicaMesaCaptura"><asp:Button ID="btnNoAplica" runat="server" Text="No Aplica" CssClass="btnVerde" validationgroup="Observaciones" OnClientClick="return ContinuarNoAplica();" Width="100%" /></td>
                                                            <td runat="server" id="tdAceptar"><asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btnVerde" validationgroup="Observaciones" OnClick="btnAceptar_Click" OnClientClick="return Continuar();" Width="100%" /></td>
                                                            <td runat="server" id="tdAceptarSeleccion"><asp:Button ID="btnAcepatarSeleccion" runat="server" Text="Aceptar" CssClass="btnVerde" validationgroup="Observaciones" OnClientClick="return Continuar();" Width="100%" /></td>
                                                            <td runat="server" id="tdCitaMedica"><asp:Button ID="btnCitaMedica" runat="server" Text="Cita Medica" CssClass="btnAmarillo" Width="100%"/></td>
                                                            <td runat="server" id="tdHold"><asp:Button ID="btnHold" runat="server" Text="Hold" CssClass="btnAmarillo" Width="100%"/></td>
                                                            <td runat="server" id="tdSuspender"><asp:Button ID="btnSuspender" runat="server" Text="Suspender" CssClass="btnRojo" Width="100%" CausesValidation="False"/></td>
                                                            <td runat="server" id="tdRechazar"><asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CssClass="btnRojo" Width="100%"/></td>
                                                            <td runat="server" id="tdPCI"><asp:Button ID="btnPCI" runat="server" Text="P C I" CssClass="btnRojo" Width="100%"/></td>
                                                            <td runat="server" id="tdPausa"><asp:Button ID="btnPausa" runat="server" Text="Pausa de Trámite" CssClass="btnRojo" Width="100%"/></td>
                                                            <td runat="server" id="tdStopAsig"><asp:Button ID="btnStopAsig" runat="server" Text="Pausa de Usuario" CssClass="btnAmarillo" OnClick="btnStopAsig_Click" Width="100%"/></td>
                                                            <td runat="server" id="tdCancelacion"><asp:Button ID="btnCancelacion" runat="server" Text="Cancelación" CssClass="btnRojo" Width="100%"/></td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" style="width:100%; border:1px; text-align:center; " id="tblBotonsCM1">
                                                        <tr runat="server">
                                                            <td runat="server" id="tdProcesable"><asp:Button ID="btnProcesable" runat="server" Text="Procesable" CssClass="btnVerde" OnClick="btnProcesable_Click" OnClientClick="return ContinuarProcesable();" Width="100%"/></td>
                                                            <td runat="server" id="tdNoProcesable"><asp:Button ID="btnNoProcesable" runat="server" Text="No Procesable" CssClass="btnRojo" Width="100%" OnClick="btnNoProcesable_Click"/></td>
                                                            <td runat="server" id="tdComplementario"><asp:Button ID="btnComplementario" runat="server" Text="Complementario" CssClass="btnAmarillo" OnClick="btnComplementario_Click" Width="100%"/></td>
                                                            <td runat="server" id="tdSuspencionCM">   <asp:Button ID="btnSuspencionCM"    runat="server" Text="Suspención Cita Médica"  CssClass="btnRojo"     OnClick="btnSuspencionCitaMedica_Click" OnClientClick="return ContinuarTramite();"  Width="100%"/></td>
                                                            <td runat="server" id="tdEsperaResultado"><asp:Button ID="btnEsperaResultado" runat="server" Text="En Espera de Resultados" CssClass="btnAmarillo" OnClick="btnEsperaResultado_Click"      OnClientClick="return ContinuarTramite();"  Width="100%"/></td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" style="width:100%; border:1px; text-align:center; " id="tblBotonsCM2">
                                                        <tr runat="server">
                                                            <td runat="server" id="tdCitaProgramada"><asp:Button ID="btnCitaProgramada" runat="server" Text="Cita Programada" CssClass="btnAmarillo" OnClick="btnCitaProgramada_Click" Width="100%"/></td>
                                                            <td runat="server" id="tdConPendiente"><asp:Button ID="btnConPendiente" runat="server" Text="Confirmación Pendiente" CssClass="btnAmarillo" OnClick="btnComplementario_Click" Width="100%"/></td>
                                                            <td runat="server" id="tdRevProspecto"><asp:Button ID="btnRevProspecto" runat="server" Text="Revisión Prospecto" CssClass="btnAmarillo" OnClick="btnConPendiente_Click" Width="100%"/></td>
                                                            <td runat="server" id="tdCitaReProgramada"><asp:Button ID="btnCitaReProgramada" runat="server" Text="Cita Re-Programada" CssClass="btnAmarillo" OnClick="btnCitaReProgramada_Click" Width="100%"/></td>
                                                        </tr>
                                                    </table>
                                                </div>


                                                <div id="dvCajaBtnCtrlRechazos" style="width:100%;">
                                                    <dx:ASPxButton ID="btnSuspenderDir" 
                                                                    runat="server" 
                                                                    Text="Aplica Rechazo" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaSuspenderDir_Click" 
                                                                    ClientInstanceName="btnSuspenderDir" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMovitosRechazo.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlAplicaRechazodDir" 
                                                                    runat="server" 
                                                                    Text="Aplica Rechazo" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaRechazodDir_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaRechazodDir" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMovitosRechazo.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlAplicaHoldDir" 
                                                                    runat="server" 
                                                                    Text="Aplica Rechazo" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaHoldDir_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaHoldDir" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMovitosRechazo.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlAplicaCMCitaReprogramada" 
                                                                    runat="server" 
                                                                    Text="Revisión con Prospecto" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaCMCitaReprogramada_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaCMCitaReprogramada" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMotivosCMCitaReprogramada.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>
                                                    
                                                    <dx:ASPxButton ID="btnCtrlAplicaCMRevProspecto" 
                                                                    runat="server" 
                                                                    Text="Revisión con Prospecto" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaCMRevProspecto_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaCMRevProspecto" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMotivosCMRevProspecto.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlAplicaHold"
                                                                    runat="server" 
                                                                    Text="Aplica Hold" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaHold_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaHold" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMovitosHold.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlAplicaCM"
                                                                    runat="server" 
                                                                    Text="Aplica CM" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaCM_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaCM" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMovitosCM.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlPausarTramite"
                                                                    runat="server" 
                                                                    Text="Pausar Trámite" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlPausarTramite_Click" 
                                                                    ClientInstanceName="btnCtrlPausarTramite" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopPausaTramite.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    
                                                    <dx:ASPxButton ID="btnCtrlAceptaSelccion"
                                                                    runat="server" 
                                                                    Text="Aplica Hold" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAceptaSelccion_Click" 
                                                                    ClientInstanceName="btnCtrlAceptaSelccion" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopProcesarTramite.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlAplicaHoldD"
                                                                    runat="server" 
                                                                    Text="Aplica Rechazo" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaHoldD_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaHoldD" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMovitosHold.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlAplicaSuspencion" 
                                                                    runat="server" 
                                                                    Text="Aplica Rechazo" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaSuspencion_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaSuspencion" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlAplicaRechazo" 
                                                                    runat="server" 
                                                                    Text="Aplica Rechazo" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaRechazo_Click" 
                                                                    ClientInstanceName="btnCtrlAplicaRechazo" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopMovitosRechazo.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>


                                                    <dx:ASPxButton ID="btnCtrlNoAplica"
                                                                    runat="server" 
                                                                    Text="Pausar Trámite" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnAceptar_Click" 
                                                                    ClientInstanceName="btnCtrlNoAplica" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopNoAplica.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="btnCtrlCancelacion" 
                                                                    runat="server" 
                                                                    Text="Aplica cancelación" 
                                                                    ClientVisible="False" 
                                                                    OnClick="btnCtrlAplicaCancelardDir_Click" 
                                                                    ClientInstanceName="btnCtrlCancelacion" 
                                                                    CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) 
                                                            { 
                                                                pnlPopCancelar.Hide(); 
                                                                pnlMsgProcesando.Show(); 
                                                            }" />
                                                    </dx:ASPxButton>

                                                    <asp:HiddenField ID="hfModoRechazo" runat="server" />
                                                    <asp:HiddenField ID="hfCadenaMotivosRechazo" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>
                            </tr>
                            
                        </table>
                        <asp:Literal ID="litEncabezado" runat="server"></asp:Literal>
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <!-- 
                                    <asp:LinkButton ID="lnkExportSuspend" runat="server" CausesValidation="False">Exportar a Excel</asp:LinkButton>
                                    -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                    <asp:Repeater ID="Repeater1" runat="server" >
                                        <HeaderTemplate>
                                            <table id="tblInstitucional" style="width:100%" class="display" >
                                                <thead>
                                                    <th scope="col">Tipo Servicio</th>
                                                    <th scope="col">Acción</th>
                                                    <th scope="col">Género</th>
                                                    <th scope="col">Nombres</th>
                                                    <th scope="col">Apellido Paterno</th>
                                                    <th scope="col">Apellido Materno</th>
                                                    <th scope="col">Verficado</th>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: White; color: #333333">
                                                <td><%#Eval("tipoServicio")%></td>
                                                <td><%#Eval("Accion")%></td>
                                                <td><%#Eval("Genero")%></td>
                                                <td><%#Eval("Nombres")%></td>
                                                <td><%#Eval("APaterno")%></td>
                                                <td><%#Eval("AMaterno")%></td>
                                                <td><asp:CheckBox ID="chkVerificado" runat="server" /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>                        

                        <div id="dvEspacioPDF" style="width: 100%; height: 550px; vertical-align: top" tabindex="0">
                            <asp:HiddenField ID="hfIdArchivo" runat="server" Value="9999" />
                            <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                        </div>
                    </div>

                    <dx:ASPxPopupControl ID="popDocumento" runat="server"
                        RenderIFrameForPopupElements="True"
                        LoadContentViaCallback="OnFirstShow"
                        CloseAction="CloseButton"
                        PopupVerticalAlign="Below"
                        PopupHorizontalAlign="LeftSides"
                        AllowDragging="True"
                        HeaderText="DOCUMENTO POP"
                        ClientInstanceName="popDocumento"
                        Width="500px"
                        Height="400px"
                        PopupElementID="btnMuestraPop" Theme="Aqua">
                        <ContentStyle>
                            <Paddings Padding="5px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <asp:Literal ID="ltPdfPop" runat="server"></asp:Literal>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="popBitacora" runat="server"
                        RenderIFrameForPopupElements="True"
                        LoadContentViaCallback="OnFirstShow"
                        CloseAction="CloseButton"
                        PopupVerticalAlign="Below"
                        PopupHorizontalAlign="LeftSides"
                        AllowDragging="True"
                        HeaderText="BITÁCORA PÚBLICA"
                        ClientInstanceName="popBitacora"
                        Width="500px"
                        Height="400px"
                        PopupElementID="btnMuestraBitacora" Theme="Aqua">
                        <ContentStyle>
                            <Paddings Padding="5px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <asp:Literal ID="ltBitacora" runat="server"></asp:Literal>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="popBitacoraPrivada" runat="server"
                        RenderIFrameForPopupElements="True"
                        LoadContentViaCallback="OnFirstShow"
                        CloseAction="CloseButton"
                        PopupVerticalAlign="Below"
                        PopupHorizontalAlign="LeftSides"
                        AllowDragging="True"
                        HeaderText="BITÁCORA PRIVADA"
                        ClientInstanceName="popBitacoraPrivada"
                        Width="500px"
                        Height="400px"
                        PopupElementID="btnBitacoraPrivada" Theme="Aqua">
                        <ContentStyle>
                            <Paddings Padding="5px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <asp:Literal ID="ltBitacoraPrivada" runat="server"></asp:Literal>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="PopInsumos" runat="server"
                        RenderIFrameForPopupElements="True"
                        LoadContentViaCallback="OnFirstShow"
                        CloseAction="CloseButton"
                        PopupVerticalAlign="Below"
                        PopupHorizontalAlign="RightSides"
                        AllowDragging="True"
                        HeaderText="INSUMOS"
                        ClientInstanceName="PopInsumos"
                        Width="350px"
                        Height="150px"
                        PopupElementID="btnMuestraInsumos"
                        Theme="Aqua">
                        <ContentStyle>
                            <Paddings Padding="5px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <asp:Panel ID="pnInsumos" runat="server" ScrollBars="Auto">
                                    <fieldset>
                                        <legend>ARCHIVOS DE INSUMOS</legend>
                                        <asp:Repeater ID="rptInsumos" runat="server" OnItemDataBound="rptInsumos_ItemDataBound">
                                            <HeaderTemplate>
                                                <table id="tblInsumos" style="width: 100%" class="display">
                                                    <thead>
                                                        <th scope="col"></th>
                                                        <th scope="col"></th>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="color: #333333">
                                                    <td style="text-align: center">
                                                        <asp:ImageButton ID="ImgExp" runat="server" ImageUrl="~/img/download.png" Visible="false"/>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <button onclick="Descarga('<%# Eval("NmArchivo")%>'); return false;">
                                                            <img src="../img/download.png" /></button>
                                                    </td>
                                                    <td><%# Eval("NmOriginal")%></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </fieldset>
                                </asp:Panel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopMotivosPCI" 
					                    runat="server" 
					                    CloseAction="CloseButton" 
					                    HeaderText="Motivos PCI" 
					                    ShowFooter="True" 
					                    Theme="Aqua" 
					                    Width="350px" 
					                    ClientInstanceName="pnlPopMotivosPCI" 
					                    Modal="True" 
					                    PopupHorizontalAlign="WindowCenter" 
					                    PopupVerticalAlign="WindowCenter" 
					                    FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
                                <dx:ASPxCallbackPanel ID="pnlCallbackMotPCIAsigna" 
								                    runat="server" 
								                    ClientInstanceName="pnlCallbackMotPCIAsigna" 
								                    Width="100%" 
								                    OnCallback="pnlCallbackMotPCIAsigna_Callback">
			                        <PanelCollection>
                                        <dx:PanelContent runat="server">
                                        </dx:PanelContent>
                                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>

			                    <dx:ASPxCallbackPanel ID="pnlCallbackMotPCI" 
								                    runat="server" 
								                    ClientInstanceName="pnlCallbackMotPCI" 
								                    Width="100%" 
								                    OnCallback="pnlCallbackMotPCI_Callback">
				                    <PanelCollection>
					                    <dx:PanelContent runat="server">
						                    <dx:ASPxListBox ID="lstMotivosPCI" 
										                    runat="server" 
										                    ClientInstanceName="lstMotivosPCI" 
										                    SelectionMode="CheckColumn" 
										                    Theme="Aqua" 
										                    Width="100%">
						                    </dx:ASPxListBox>
					                    </dx:PanelContent>
				                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
			                    <dx:ASPxButton ID="btnAplicaPCI" runat="server" AutoPostBack="False" EnableTheming="True" Text="Aplicar" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnAplicaPCI(); 
					                    }" />
			                    </dx:ASPxButton>
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopProcesarTramite" 
					                    runat="server" 

					                    CloseAction="CloseButton" 
					                    HeaderText="" 

                        ShowCloseButton="False"
                        ShowHeader="False"

					                    ShowFooter="True" 
					                    Theme="iOS" 
					                    Width="450px" 
					                    ClientInstanceName="pnlPopProcesarTramite" 
					                    Modal="True" 
					                    PopupHorizontalAlign="WindowCenter" 
					                    PopupVerticalAlign="WindowCenter" 
					                    FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="1px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
			                    <dx:ASPxCallbackPanel ID="pnlCallbackProcesaTramite" 
								                    runat="server" 
								                    ClientInstanceName="pnlCallbackProcesaTramite" 
								                    Width="100%" 
								                    OnCallback="pnlCallbackProcesaTramite_Callback">
				                    <PanelCollection>
					                    <dx:PanelContent runat="server">
                                            <br />
                                            <table>
                                                <tr>
                                                    <!--<td style="width:35%; text-align:center;"> <img src = '../img/helping.png' alt='' />  </td>-->
                                                    <td style="width:100%"> <asp:Label runat="server" ID="lblAceptarSeleccion" Text="<strong>El trámite tiene estatus NO PROCESABLE por mesa Técnico/Médico</strong> <br/><br/> ¿desea continuar con la operación? <br/>&nbsp;"></asp:Label> </td>
                                                </tr>
                                            </table>
					                    </dx:PanelContent>
				                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
                                <br />
			                    <dx:ASPxButton ID="btnProcesarTramiteOK" runat="server" AutoPostBack="False" EnableTheming="True" Text="Aceptar" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnAceptarSeleccion();
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
								<dx:ASPxButton ID="btnProcesarTramiteKO" runat="server" AutoPostBack="False" EnableTheming="True" Text="Cancelar" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    pnlPopProcesarTramite.Hide();
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>


                    <!-- Modal:::: No Aplica -->
                    <dx:ASPxPopupControl ID="pnlPopNoAplica" 
					                    runat="server" 
					                    CloseAction="CloseButton" 
					                    HeaderText="Aceptar"  
					                    ShowFooter="True" 
					                    Theme="iOS" 
					                    Width="350px" 
					                    ClientInstanceName="pnlPopNoAplica" 
					                    Modal="True" 
					                    PopupHorizontalAlign="WindowCenter" 
					                    PopupVerticalAlign="WindowCenter" 
					                    FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
                                <p>
                                    <strong>OBSERVACIONES PUBLICAS</strong>
                                </p>
                                <asp:TextBox ID="txtObservacionNoAplica" runat="server" TextMode="MultiLine" Width="95%" Height="50px"></asp:TextBox>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
                                <br />
			                    <dx:ASPxButton ID="btnPausaTramite" runat="server" AutoPostBack="False" EnableTheming="True" Text="Aceptar" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnNoAplica();
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopPausaTramite" 
					                    runat="server" 
					                    CloseAction="CloseButton" 
					                    HeaderText="Pausar Trámite" 
					                    ShowFooter="True" 
					                    Theme="iOS" 
					                    Width="350px" 
					                    ClientInstanceName="pnlPopPausaTramite" 
					                    Modal="True" 
					                    PopupHorizontalAlign="WindowCenter" 
					                    PopupVerticalAlign="WindowCenter" 
					                    FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
                                <p>
                                    <strong>OBSERVACIONES PÚBLICAS</strong>
                                </p>
                                <asp:TextBox ID="txtObservacionPausarTramite" runat="server" TextMode="MultiLine" Width="95%" Height="50px"></asp:TextBox>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
                                <br />
			                    <dx:ASPxButton ID="btnPausaTramite" runat="server" AutoPostBack="False" EnableTheming="True" Text="Pausar Trámite" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnPausarTramite();
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopSigueTramiteStatus" 
					                    runat="server" 
					                    CloseAction="CloseButton" 
					                    HeaderText="Seguimiento del Trámite." 
					                    ShowFooter="True" 
					                    Theme="iOS" 
					                    Width="800px" 
					                    ClientInstanceName="pnlPopSigueTramiteStatus" 
					                    Modal="True" 
					                    PopupHorizontalAlign="WindowCenter" 
					                    PopupVerticalAlign="WindowCenter" 
					                    FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
			                    <dx:ASPxCallbackPanel ID="pnlCallbackPopSigueTramiteStatus" 
								                    runat="server" 
								                    ClientInstanceName="pnlCallbackPopSigueTramiteStatus" 
								                    Width="100%" 
								                    OnCallback="pnlCallbackPopSigueTramiteStatus_Callback">
				                    <PanelCollection>
					                    <dx:PanelContent runat="server">

                                            <asp:Panel ID="pnTrmPausa" runat="server" Width="100%">
                                                <div id="dvTrmPausa" style="width: 100%; margin: auto; font-family: Arial;">
                                                    <asp:Repeater ID="rptTramitesEspera" runat="server" OnItemCommand="rptTramitesEspera_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table id="tblTramitesEspera" style="width:100%; text-align:center; border=1;" class="display" >
                                                                <thead>
                                                                    <th scope="col">Mesa</th>
                                                                    <th scope="col">Estado</th>
                                                                    <th scope="col">Obs. Pública</th>
                                                                    <th scope="col">Obs. Privada</th>
                                                                    <th scope="col">Fecha</th>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr style="background-color: White; color: #333333">
                                                                <td><%# Eval("Mesa")%></td>
                                                                <td><%# Eval("Estado")%></td>
                                                                <td><%# Eval("Observacion")%></td>
                                                                <td><%# Eval("ObservacionPrivada")%></td>
                                                                <td><%# Eval("FechaTermino")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                        </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </asp:Panel>
											
					                    </dx:PanelContent>
				                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
                                <br />
			                    <dx:ASPxButton ID="btnSinUsar" runat="server" AutoPostBack="False" EnableTheming="True" Text="Cerrar" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e)
					                    { 
						                    pnlPopSigueTramiteStatus.Hide();
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopMotivosCM" 
					                    runat="server" 
					                    CloseAction="CloseButton" 
					                    HeaderText="Motivos Citas Medicas" 
					                    ShowFooter="True" 
					                    Theme="iOS" 
					                    Width="350px" 
					                    ClientInstanceName="pnlPopMotivosCM" 
					                    Modal="True" 
					                    PopupHorizontalAlign="WindowCenter" 
					                    PopupVerticalAlign="WindowCenter" 
					                    FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
			                    <dx:ASPxCallbackPanel ID="pnlCallbackMotCM" 
								                    runat="server" 
								                    ClientInstanceName="pnlCallbackMotCM" 
								                    Width="100%" 
								                    OnCallback="pnlCallbackMotCM_Callback">
				                    <PanelCollection>
					                    <dx:PanelContent runat="server">
						                    <!--
                                            <dx:ASPxTreeList ID="treeListCM" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackCM" OnDataBound="treeList_DataBoundCM" ParentFieldName="idParent" Width="100%">
                                                <Columns>
                                                    <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos de Citas Medicas" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                                </Columns>
                                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior> 
                                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                                <settingsselection enabled="True"></settingsselection>
                                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                                <settingspopup>
                                                    <editform verticaloffset="-1"></editform>
                                                </settingspopup>
                                                <clientsideevents customdatacallback="treeList_CustomDataCallbackCM" selectionchanged="treeList_SelectionChangedCM"></clientsideevents>
                                            </dx:ASPxTreeList>
                                            -->

                                            <asp:Label ID="lblObservacionesCM" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                                            <asp:TextBox ID="txtObservacionesCM" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
					                    </dx:PanelContent>
				                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
                                <br />
			                    <dx:ASPxButton ID="btnAplicaCM" runat="server" AutoPostBack="False" EnableTheming="True" Text="Aplicar Cita Medica" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnAplicaCM(); 
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopMotivosHold" runat="server" CloseAction="CloseButton" HeaderText="Motivos HOLD" ShowFooter="True" Theme="iOS"  Width="350px"  ClientInstanceName="pnlPopMotivosHold" Modal="True" PopupHorizontalAlign="WindowCenter"  PopupVerticalAlign="WindowCenter" FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
			                    <dx:ASPxCallbackPanel ID="pnlCallbackMotHold" runat="server" ClientInstanceName="pnlCallbackMotHold" Width="100%" OnCallback="pnlCallbackMotHold_Callback">
				                    <PanelCollection>
					                    <dx:PanelContent runat="server">
						                    <dx:ASPxTreeList ID="treeListHold" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackHold" OnDataBound="treeList_DataBoundHold" ParentFieldName="idParent" Width="100%">
                                                <Columns>
                                                    <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos de Hold" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                                </Columns>
                                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                                <settingsselection enabled="True"></settingsselection>
                                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                                <settingspopup>
                                                    <editform verticaloffset="-1"></editform>
                                                </settingspopup>
                                                <clientsideevents customdatacallback="treeList_CustomDataCallbackHold" selectionchanged="treeList_SelectionChangedHold"></clientsideevents>
                                            </dx:ASPxTreeList>
                                            <br />
                                            <asp:Label ID="lblObservacionesHold" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                                            <asp:TextBox ID="txtObservacionesHold" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
					                    </dx:PanelContent>
				                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
                                <br />
			                    <dx:ASPxButton ID="btnAplicaHold" runat="server" AutoPostBack="False" EnableTheming="True" Text="Aplicar HOLD" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnAplicaHold(); 
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopMotivosSuspender" runat="server" CloseAction="CloseButton"  HeaderText="Motivos Suspender" ShowFooter="True"  Theme="Aqua"  Width="350px" ClientInstanceName="pnlPopMotivosSuspender"  Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
			                    <dx:ASPxCallbackPanel ID="pnlCallbackMotSuspender" runat="server" ClientInstanceName="pnlCallbackMotSuspender" Width="100%" OnCallback="pnlCallbackMotSuspender_Callback">
				                    <PanelCollection>
					                    <dx:PanelContent runat="server">
						                    <dx:ASPxTreeList ID="treeListSuspender" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackSuspender" OnDataBound="treeList_DataBoundSuspender" ParentFieldName="IdParent" Width="100%">
                                                <Columns>
                                                    <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos de Suspensión" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                                </Columns>
                                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                                <settingsselection enabled="True"></settingsselection>
                                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                                <settingspopup>
                                                    <editform verticaloffset="-1"></editform>
                                                </settingspopup>
                                                <clientsideevents customdatacallback="treeList_CustomDataCallbackSuspender" selectionchanged="treeList_SelectionChangedSuspender"></clientsideevents>
                                            </dx:ASPxTreeList>
                                            <br />
                                            <asp:Label ID="lblObservacionesSuspencion" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                                            <asp:TextBox ID="txtObservacionesSuspencion" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
					                    </dx:PanelContent>
				                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
			                    <dx:ASPxButton ID="btnAplicaSuspender" runat="server" AutoPostBack="False" EnableTheming="True" Text="Aplicar" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnAplicaSuspender();
					                    }" />
			                    </dx:ASPxButton>
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopCancelar" runat="server" CloseAction="CloseButton" HeaderText="Motivos cancelación" ShowFooter="True" Theme="Aqua"  Width="350px" ClientInstanceName="pnlPopCancelar" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" FooterText="">
                        <ContentStyle>
                            <Paddings Padding="5px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server" >
                                <dx:ASPxCallbackPanel ID="pnlCallbackCancelar" runat="server" ClientInstanceName="pnlCallbackCancelar" Width="100%" OnCallback="pnlCallbackCancelar_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxTreeList ID="treeListCancelar" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackCancelar" OnDataBound="treeList_DataBoundCancelar" ParentFieldName="IdParent" Width="100%">
                                            <Columns>
                                                <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos de cancelación" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                            </Columns>
                                            <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                            <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                            <settingsselection enabled="True"></settingsselection>
                                            <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                            <settingspopup>
                                                <editform verticaloffset="-1"></editform>
                                            </settingspopup>
                                            <clientsideevents customdatacallback="treeList_CustomDataCallbackSuspender" selectionchanged="treeList_SelectionChangedSuspender"></clientsideevents>
                                            </dx:ASPxTreeList>
                                            <br />
                                            <asp:Label ID="Label75" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                                            <asp:TextBox ID="txObservacionesCancelacion" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                        <FooterTemplate>
                            <div style="text-align:right;">
                                <br />&nbsp;&nbsp;&nbsp;
                                <dx:ASPxButton ID="btnAplicaCancelar" runat="server" AutoPostBack="False" EnableTheming="True" Text=" Aplicar " Theme="Aqua">
                                    <ClientSideEvents Click="function(s, e) 
                                        { 
                                            fnAplicaCancelacion(); 
                                        }" />
                                </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
                            </div>
                        </FooterTemplate>
                    </dx:ASPxPopupControl>

                    
                    <dx:ASPxPopupControl ID="pnlPopMovitosRechazo" runat="server" CloseAction="CloseButton" HeaderText="Motivos Rechazo" ShowFooter="True" Theme="Aqua"  Width="350px" ClientInstanceName="pnlPopMovitosRechazo" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" FooterText="">
                        <ContentStyle>
                            <Paddings Padding="5px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server" >
                                <dx:ASPxCallbackPanel ID="pnlCallbackMotRechazo" runat="server" ClientInstanceName="pnlCallbackMotRechazo" Width="100%" OnCallback="pnlCallbackMotRechazo_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxTreeList ID="treeListRechazo" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackSuspender" OnDataBound="treeList_DataBoundSuspender" ParentFieldName="IdParent" Width="100%">
                                            <Columns>
                                                <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos de Rechazo" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                            </Columns>
                                            <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                            <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                            <settingsselection enabled="True"></settingsselection>
                                            <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                            <settingspopup>
                                                <editform verticaloffset="-1"></editform>
                                            </settingspopup>
                                            <clientsideevents customdatacallback="treeList_CustomDataCallbackSuspender" selectionchanged="treeList_SelectionChangedSuspender"></clientsideevents>
                                            </dx:ASPxTreeList>
                                            <br />
                                            <asp:Label ID="Label74" runat="server" Text="OBSERVACIONES PÚBLICAS" Font-Bold="True"></asp:Label>
                                            <asp:TextBox ID="txObservacionesRechazo" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                        <FooterTemplate>
                            <div style="text-align:right;">
                                <br />&nbsp;&nbsp;&nbsp;
                                <dx:ASPxButton ID="btnAplicaRechazo" runat="server" AutoPostBack="False" EnableTheming="True" Text=" Aplicar " Theme="Aqua">
                                    <ClientSideEvents Click="function(s, e) 
                                        { 
                                            fnAplicaRechazo2(); 
                                        }" />
                                </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
                            </div>
                        </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopMotivosCMRevProspecto" 
					                    runat="server" 
					                    CloseAction="CloseButton" 
					                    HeaderText="Citas Médicas" 
					                    ShowFooter="True" 
					                    Theme="iOS" 
					                    Width="350px" 
					                    ClientInstanceName="pnlPopMotivosCMRevProspecto" 
					                    Modal="True" 
					                    PopupHorizontalAlign="WindowCenter" 
					                    PopupVerticalAlign="WindowCenter" 
					                    FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
			                    <dx:ASPxCallbackPanel ID="pnlCallbackMotCMRevProspecto" 
								                    runat="server" 
								                    ClientInstanceName="pnlCallbackMotCMRevProspecto" 
								                    Width="100%" 
								                    OnCallback="pnlCallbackMotCMRevProspecto_Callback">
				                    <PanelCollection>
					                    <dx:PanelContent runat="server">
						                    <dx:ASPxTreeList ID="treeListCMRevProspecto" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackCMRevProspecto" OnDataBound="treeList_DataBoundCMRevProspecto" ParentFieldName="idParent" Width="100%">
                                                <Columns>
                                                    <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Revisión con Prospecto" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                                </Columns>
                                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                                <settingsselection enabled="True"></settingsselection>
                                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                                <settingspopup>
                                                    <editform verticaloffset="-1"></editform>
                                                </settingspopup>
                                                <clientsideevents customdatacallback="treeList_CustomDataCallbackCMRevProspecto" selectionchanged="treeList_SelectionChangedCMRevProspecto"></clientsideevents>
                                            </dx:ASPxTreeList>
					                    </dx:PanelContent>
				                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
                                <br />
			                    <dx:ASPxButton ID="btnAplicaCMRevProspecto" runat="server" AutoPostBack="False" EnableTheming="True" Text="Revisión con Prospecto" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnAplicaCMRevProspecto(); 
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl ID="pnlPopMotivosCMCitaReprogramada" 
					                    runat="server" 
					                    CloseAction="CloseButton" 
					                    HeaderText="Citas Médicas" 
					                    ShowFooter="True" 
					                    Theme="iOS" 
					                    Width="350px" 
					                    ClientInstanceName="pnlPopMotivosCMCitaReprogramada" 
					                    Modal="True" 
					                    PopupHorizontalAlign="WindowCenter" 
					                    PopupVerticalAlign="WindowCenter" 
					                    FooterText="">
	                    <ContentStyle>
		                    <Paddings Padding="5px" />
	                    </ContentStyle>
	                    <ContentCollection>
		                    <dx:PopupControlContentControl runat="server">
			                    <dx:ASPxCallbackPanel ID="pnlCallbackMotCMCitaReprogramada" 
								                    runat="server" 
								                    ClientInstanceName="pnlCallbackMotCMCitaReprogramada" 
								                    Width="100%" 
								                    OnCallback="pnlCallbackMotCMCitaReprogramada_Callback">
				                    <PanelCollection>
					                    <dx:PanelContent runat="server">
						                    <dx:ASPxTreeList ID="treeListCMCitaReprogramada" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackCMCitaReprogramada" OnDataBound="treeList_DataBoundCMCitaReprogramada" ParentFieldName="idParent" Width="100%">
                                                <Columns>
                                                    <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Cita Reprogramada" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="0"></dx:TreeListDataColumn>
                                                </Columns>
                                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                                <settingsselection enabled="True"></settingsselection>
                                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                                <settingspopup>
                                                    <editform verticaloffset="-1"></editform>
                                                </settingspopup>
                                                <clientsideevents customdatacallback="treeList_CustomDataCallbackCMCitaReprogramada" selectionchanged="treeList_SelectionChangedCMCitaReprogramada"></clientsideevents>
                                            </dx:ASPxTreeList>
					                    </dx:PanelContent>
				                    </PanelCollection>
			                    </dx:ASPxCallbackPanel>
		                    </dx:PopupControlContentControl>
	                    </ContentCollection>
	                    <FooterTemplate>
		                    <div style="text-align:right;">
                                <br />
			                    <dx:ASPxButton ID="btnAplicaCMCitaReprogramada" runat="server" AutoPostBack="False" EnableTheming="True" Text="Cita Reprogramada 2" Theme="Aqua">
				                    <ClientSideEvents Click="function(s, e) 
					                    { 
						                    fnAplicaCMCitaReprogramada(); 
					                    }" />
			                    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;
                                <br />&nbsp;
		                    </div>
	                    </FooterTemplate>
                    </dx:ASPxPopupControl>

                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel ID="pnlTabAntecedentes" runat="server" HeaderText="ANTECEDENTES">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upPnlAntecedente" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlAntecedentes" runat="server" Width="100%" Height="150px" ScrollBars="Vertical">
                                <asp:Repeater ID="rpt_Antecedentes" runat="server" OnItemCommand="rpt_Antecedentes_ItemCommand">
                                    <FooterTemplate>
                                        </tbody>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <table id="tblAntecedentes" style="width: 100%; font-size: 12px">
                                            <thead>
                                                <th scope="col">Folio</th>
                                                <th scope="col">Poliza</th>
                                                <th scope="col">Nombre</th>
                                                <th scope="col">RFC</th>
                                                <th scope="col"></th>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: White; color: #333333">
                                            <td><%# Eval("IdTramite","{0:0000#}")%></td>
                                            <td><%# Eval("NumPoliza")%></td>
                                            <td><%# Eval("Nombre")%></td>
                                            <td><%# Eval("RFC")%></td>
                                            <td>
                                                <asp:ImageButton ID="imbtnAbreAntecedente" runat="server" CausesValidation="false" CommandArgument='<%# Eval("IdTramite")%>' CommandName="abreAntecedente" ImageUrl="~/img/Folder.png" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <asp:Panel ID="pnlDatosAntededente" runat="server" Width="100%">
                                <hr />
                                <table id="tblDatosntededente" style="width: 95%; margin: auto;">
                                    <tr>
                                        <td style="width: 33%; vertical-align: top;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 33%">Folio</td>
                                                    <td>
                                                        <asp:Label ID="lbFolioAntecedente" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Fecha de registro</td>
                                                    <td>
                                                        <asp:Label ID="lbFechaRegistroAntecedente" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Trámite</td>
                                                    <td>
                                                        <asp:Label ID="lbTramiteNombreAntecedente" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 33%; vertical-align: top;">
                                            <asp:Literal ID="ltDatosContratanteAntecedente" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <div style="width: 100%; height: 150px; overflow: auto;">
                                                <asp:Literal ID="ltBitacoraAntecedente" runat="server"></asp:Literal>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlDocAntecedente" runat="server" Width="100%" Height="500px">
                                    <hr />
                                    <asp:Literal ID="ltDocumentoAntecedente" runat="server"></asp:Literal>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel ID="pnlTabBuscar" runat="server" HeaderText="BUSCAR">
                <ContentTemplate>

                    <asp:UpdatePanel ID="pnlUpBuscaTrmt" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table id="tblBuscar" border="0" style="width: 100%">
                                <tr style="width: 15%">
                                    <td colspan="2" style="text-align: center">
                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">DATOS DE BÚSQUEDA</span>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%; text-align:right;">
                                        <asp:Label ID="lbNombreABuscar" runat="server" Text="Nombre:"></asp:Label>
                                    </td>
                                    <td style="width: 70%;">
                                        <asp:TextBox ID="txNombreABuscar" runat="server" Width="40%"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ft_txNombreABuscar" runat="server" TargetControlID="txNombreABuscar" FilterMode="ValidChars" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ.," />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                       <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%; text-align:right;">
                                        <asp:Label ID="lblRFC" runat="server" Text="RFC:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBuscarRFC" runat="server" Width="40%"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtBuscarRFC" FilterMode="ValidChars" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                       <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%; text-align:right;">
                                        <asp:Label ID="lblFolio" runat="server" Text="Folio: "></asp:Label>
                                    </td>
                                    <td style="width: 70%;">
                                        <asp:TextBox ID="txtBuscarFolio" runat="server" Width="40%"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtBuscarFolio" FilterMode="ValidChars" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                        <asp:Button ID="btnBuscaFilio" runat="server" Text=" Buscar " OnClick="btnBuscaFilio_Click" CssClass="boton" CausesValidation="false" Width="150px" Height="40px" />
                                        <br /><br />
                                        <asp:Label ID="lbNoExiste" runat="server" Text="NO EXISTE" Font-Size="12px" ForeColor="Red" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <asp:Panel ID="pnlGrdResultadosBusca" runat="server" Width="100%">
                            <table id="" border="0" style="width: 100%">
                                <tr style="width: 15%">
                                    <td colspan="2" style="text-align: center">
                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">Información de trámites</span>
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                                <asp:Repeater ID="rptTrmResBusca" runat="server" OnItemCommand="rptTrmResBusca_ItemCommand">
                                    <HeaderTemplate>
                                        <table id="tblTrmResBusca" style="width: 100%; font-size: 12px">
                                            <thead>
                                                <th scope="col">Folio</th>
                                                <th scope="col">Fecha envío</th>
                                                <th scope="col">Datos</th>
                                                <th scope="col">Tipo</th>
                                                <th scope="col">Estado</th>
                                                <th scope="col"></th>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: White; color: #333333">
                                            <td><%# Eval("FolioCompuesto")%></td>
                                            <td><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                            <td><%# Eval("DatosHtml")%></td>
                                            <td><%# Eval("IdTipoTramite")%></td>
                                            <td><%# Eval("Estado")%></td>
                                            <td>
                                                <asp:ImageButton ID="imbtnAbreResBusca" runat="server" ImageUrl="~/img/Folder.png" CommandName='abreResBusca' CommandArgument='<%# Eval("Id")%>' CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <asp:Panel ID="pnlDatosTramiteBusca" runat="server" Width="100%">
                                <hr />
                                <table id="tblDatosTramiteBusca" border="0" style="width: 100%; margin: auto;">
                                    <tr>
                                        <td style="width: 35%; vertical-align: top;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; width: 25%">Folio</td>
                                                    <td>
                                                        <asp:Label ID="lbFolioBusca" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; width: 25%"> Fecha de registro</td>
                                                    <td>
                                                        <asp:Label ID="lbFechaRegistroBusca" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; width: 25%">Trámite</td>
                                                    <td>
                                                        <asp:Label ID="lbTramiteNombreBusca" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <div style="width: 100%; height: 200px; overflow: auto;">
                                                <asp:Literal ID="ltBitacoraBusca" runat="server"></asp:Literal>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlDocumentoBusca" runat="server" Width="100%" Height="500px">
                                    <hr />
                                    <asp:Literal ID="ltDocumentoBusca" runat="server"></asp:Literal>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </fieldset>
    <dx:ASPxLoadingPanel ID="pnlMsgProcesando" runat="server" ClientInstanceName="pnlMsgProcesando" Modal="true" Text="Procesando...">
    </dx:ASPxLoadingPanel>
    <asp:HiddenField ID="hdIdTramite" runat="server" />
    <asp:HiddenField ID="hdArchivo" runat="server" />
    <asp:HiddenField ID="hdEnLinea" runat="server" />
    <asp:HiddenField ID="hdIdMesa" runat="server" />
    <asp:HiddenField ID="hdPrioridadTramite" runat="server" />
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>