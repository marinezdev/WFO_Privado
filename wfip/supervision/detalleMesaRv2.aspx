<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="detalleMesaRv2.aspx.cs" Inherits="wfip.supervision.detalleMesaRv2" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
     
    <script type="text/javascript">

        function Carga() {
            $('#DatosConsultaBitacora').html("");
            $('#DatosConsultaBitacora').html("<h3> Cargando ... </h3><p>Al finalizar cierra esta ventana, para realizar otra operación. </p>");

            //boton..disabled = true;

            //document.getElementById("<%=DescargaExcel.ClientID%>").enabled = false;
            //document.getElementById("<%=DescargaExcel.ClientID%>").disabled=true;
            //$('#boton_enviar').attr('disabled', false);
            //setTimeout('CloseModal()',10000);
            //$("#BitacoraDescarga").modal("hide");

            //$('#CloseModal').click();
            //$('#Excel').attr('disabled', true);

            //$("#exampleModalCenter").modal("show");
            
        }
        function CloseModal() {
            $('#CloseModal').click();
        }

        function BitacoraSabana() {
            $.ajax({
                type: "POST",
                url: "detalleMesaRv2.aspx/BusquedaBitacoraDescraga",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultadoBitacora,
                error: errores
            });
        }
        
        function resultadoBitacora(data) {
            console.log(data);
            $("#BitacoraDescarga").modal("show");
            
            $('#DatosConsultaBitacora').html("");

            var tabla = "<table id='InfomacionBitacora' class='table  table-responsive table-striped table-bordered table-hover' style='width:100%'>" +
                "<thead>" +
                "<tr>" +
                        "<td>Fecha descarga</td>" +
                        "<td>Rango inicial</td>" +
                        "<td>Rango final</td>" +
                        "<td>Número de registros incluidos en el reporte</td>" +
                        "<td>Usuario Solicitante </td>" +
                        "<td>Total de descargas acumuladas</td>" +
                    "</tr>" +
                "</thead>" +
                "<tbody>";
            for (var b = 0; b < data.d.bitacoraSabanas.length; b++) {
                tabla += "<tr>" +
                            "<td>" + data.d.bitacoraSabanas[b].FechaRegistro + "</td>" +
                            "<td>" + data.d.bitacoraSabanas[b].FechaInicio + "</td>" +
                            "<td>" + data.d.bitacoraSabanas[b].FechaFin + "</td>" +
                            "<td>" + data.d.bitacoraSabanas[b].NumRegistros + "</td>" +
                            "<td>" + data.d.bitacoraSabanas[b].Usuario + "</td>" +
                            "<td>" + data.d.bitacoraSabanas[b].NumSolicitudes + "</td>" +
                        "</tr>";
            }
            tabla += "</tbody>" +
                "</table>";

            $('#DatosConsultaBitacora').html(tabla);
        }

        function ShowMesas(num) {
            $.ajax({
                type: "POST",
                url: "detalleMesaRv2.aspx/Busqueda",
                data: '{Id: ' + num + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultado,
                error: errores
            });
        }

        function resultado(data) {
            console.log(data);
            $("#Bitacora").modal("show");
            $("#NumeroFolio").text('Información del trámite');
            
            $('#DatosConsulta').html("");

            var tabla = "<table id='InfomacionMesas' class='table table-striped table-bordered table-hover' style='width:100%'>" +
                "<thead>" +
                "<tr>" +
                        "<td></td>" +
                        "<td>Fecha envió</td>" +
                        "<td>Mesa</td>" +
                        "<td>Fecha Inicio</td>" +
                        "<td>Fecha Termino</td>" +
                        "<td>Estado Mesa</td>" +
                        "<td>Observación</td>" +
                        "<td>Observación Privada</td>" +
                        "<td>Ususario</td>" +
                    "</tr>" +
                "</thead>" +
                "<tbody>";
            for (var b = 0; b < data.d.consulta.length; b++) {
                tabla += "<tr>" +
                            "<td>" + data.d.consulta[b].Orden + "</td>" +
                            "<td>" + data.d.consulta[b].FechaRegistro + "</td>" +
                            "<td>" + data.d.consulta[b].NMESA + "</td>" +
                            "<td>" + data.d.consulta[b].FechaInicio + "</td>" +
                            "<td>" + data.d.consulta[b].FechaTermino + "</td>" +
                            "<td>" + data.d.consulta[b].EstadoMesa + "</td>" +
                            "<td>" + data.d.consulta[b].Observacion + "</td>" +
                            "<td>" + data.d.consulta[b].ObservacionPrivada + "</td>" +
                            "<td>" + data.d.consulta[b].NombreUsuario + "</td>" +
                        "</tr>";
            }
            tabla += "</tbody>" +
                "</table>";

            $('#DatosConsulta').html(tabla);

            $("#InfomacionMesas").dataTable().fnDestroy();
            $('#InfomacionMesas').DataTable({
                "order": [[0, "asc"]],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
            });
            
        }
        function errores(data) {
            //msg.responseText tiene el mensaje de error enviado por el servidor
            alert('Error: ' + msg.responseText);
        }
    </script>
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
    <link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="modal fade bd-example-modal-lg" id="Bitacora" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="NumeroFolio">Tramite</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="table-responsive" id="DatosConsulta">
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>


<div class="modal fade " id="BitacoraDescarga" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document" >
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Bitácora de descargas</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body text-justify">
                <p>La descargar puede demorar un par de minutos, esto puede variar a partir del número de registros solicitados y la velocidad de navegación de su internet.</p>
                <p>Descargas anteriores:</p>
                <div  id="DatosConsultaBitacora" class="text-center">
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" ID="DescargaExcel"  class="btn btn-primary" text="Descargar Excel" OnClientClick="Carga();" onclick="btnExportar_Click2"/>
                <br /><br />
                <button type="button" id="CloseModal" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
            </div>
        </div>
    </div>
</div>

    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
    <div class="row">
        <div class="x_panel">
            <br />
            <div class="container">
                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="card">
                        <div class="card-header">
                        <h3><small>Detalle por mesa </small></h3>
                        </div>
                        <div class="card-body">
                            <p class="card-text">La consulta enlistara los trámites finalizados en alguna mesa.</p>
                            <div class="row">
                                <div class="col-md-4 col-sm-4">
                                    <dx:aspxdateedit ID="CalDesde" runat="server" Theme="Material" EditFormat="Custom"   Width="190" Caption="Desde:">
                                        <TimeSectionProperties Adaptive="true">
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                        <CalendarProperties>
                                            <FastNavProperties DisplayMode="Inline" />
                                        </CalendarProperties> 
                                    </dx:aspxdateedit>
                                </div>
                                <div class="col-md-4 col-sm-4">
                                    <dx:aspxdateedit ID="CalHasta" runat="server" Theme="Material"  EditFormat="Custom"  Width="190" Caption="Hasta">
                                        <TimeSectionProperties Adaptive="true">
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                        <CalendarProperties>
                                            <FastNavProperties DisplayMode="Inline" />
                                        </CalendarProperties>
                                     </dx:aspxdateedit>
                                </div>
                                <div class="col-md-2 col-sm-2">
                                    <asp:Button ID="btnFiltroMes"  CssClass="btn btn-primary" runat="server" Text="Buscar" OnClick="btnFiltroMes_Click"/>
                                    
                                </div>
                                <div class="col-md-2 col-sm-2">
                                    <button type="button" id="Excel" onclick="BitacoraSabana();"><img src="../img/logo-ms-excel-png-7.png"></button>
                               </div>
                            </div>
                            <div class="row">
                                 <div class="col-md-6 col-sm-6">
                                    <asp:Label runat="server" ForeColor="Red" ID="Mensaje" Text =""></asp:Label>
                                 </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12 col-sm-12">
                                    
                                    <asp:Repeater ID="rptTramitesEspera" runat="server">
                                        <HeaderTemplate>
                                            <table id="tblTramitesEspera" class="table table-striped table-bordered table-hover" style='width:100%'>
                                                <thead>
                                                    <tr >
                                                        <th scope="col">Fecha envió</th>
                                                        <th scope="col">Número de trámite</th>
                                                        <th scope="col">Flujo</th>
                                                        <th scope="col">Información de contratante</th>
                                                        <th scope="col">Fecha solicitud</th>
                                                        <th scope="col">Numero de póliza </th>
                                                        <th scope="col">KWIK </th>
                                                        <th scope="col">Mustra Mesas</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("FechaRegistro")%></td>
                                                <td><%# Eval("FolioCompuesto")%></td>
                                                <td><%# Eval("flujo")%></td>
                                                <td><%# Eval("DatosHtml")%></td>
                                                <td><%# Eval("FechaSolicitud")%></td>
                                                <td><%# Eval("IdSisLegados")%></td>
                                                <td><%# Eval("kwik")%></td>
                                                <td>
                                                    <button onclick="ShowMesas(<%# Eval("Id")%>); return false;"><img src="../img/folder.png"></button>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramitesEspera').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                scrollY: "400px",
                scrollX: true,
                scrollCollapse: true,
                fixedColumns: true,
                
            });
            /*$('select').removeClass('custom-select custom-select-sm form-control form-control-sm');*/
            
        });
    </script>
</asp:Content>