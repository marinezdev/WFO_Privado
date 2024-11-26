<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteGrandesSumas.aspx.cs" Inherits="wfip.supervision.sprReporteGrandesSumas" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
        <script type="text/javascript">

        
        function ShowMesas(num) {
            $.ajax({
                type: "POST",
                url: "detalleMesaR.aspx/Busqueda",
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
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
    <link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

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


    <div class="row">
        <div class="x_panel">
            <br />
            <div class="container">
                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="card">
                        <div class="card-header">
                        <h3><small>Grandes sumas </small></h3>
                        </div>
                        <div class="card-body">
                            <p class="card-text">La consulta enlistara los trámites entre un rango de fechas a partir de la fecha de registro .</p>
                            <div class="row">
                                <div class="col-md-3 col-sm-3">
                                    <dx:aspxdateedit ID="CalDesde" runat="server" Theme="Material" EditFormat="Custom"   Width="190" Caption="Desde:">
                                        <TimeSectionProperties Adaptive="true">
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                        <CalendarProperties>
                                            <FastNavProperties DisplayMode="Inline" />
                                        </CalendarProperties> 
                                    </dx:aspxdateedit>
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    <dx:aspxdateedit ID="CalHasta" runat="server" Theme="Material"  EditFormat="Custom"  Width="190" Caption="Hasta">
                                        <TimeSectionProperties Adaptive="true">
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                        <CalendarProperties>
                                            <FastNavProperties DisplayMode="Inline" />
                                        </CalendarProperties>
                                     </dx:aspxdateedit>
                                </div>
                                <div class="col-md-1 col-sm-1">
                                    <p class="card-text">Prioridad:</p>
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    <asp:DropDownList ID="LisCat_Prioridad" runat="server" class="form-control">
                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LisCat_Prioridad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                              
                                </div>
                                <div class="col-md-2 col-sm-2">
                                    <asp:Button ID="btnFiltroMes"  CssClass="btn btn-primary" runat="server" Text="Buscar" OnClick="btnFiltroMes_Click" />
                                    
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
                                    
                                    <asp:Repeater ID="rptTramitesEspera" runat="server" OnItemCommand="rptTramitesEspera_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="tblTramitesEspera" class="table table-striped table-bordered table-hover" style='width:100%'>
                                                <thead>
                                                    <tr >
                                                        <th scope="col">Fecha de Ingreso</th>
                                                        <th scope="col">Fecha Solicitud</th>
                                                        <th scope="col">Folio</th>
                                                        <th scope="col">Producto</th>
                                                        <th scope="col">Suma Asegurada Básica</th>
                                                        <th scope="col">Suma Asegurada Póliza Vigentes</th>
                                                        <th scope="col">Prima </th>
                                                        <th scope="col">Moneda </th>
                                                        <th scope="col">Nombre Contratante </th>
                                                        <th scope="col">RFC Contratante</th>
                                                        <th scope="col">Estatus</th>
                                                        <th scope="col">Fecha estatus</th>
                                                        <th scope="col">Tiempo estatus actual (hrs)</th>
                                                        <th scope="col">Clave de promotoría</th>
                                                        <th scope="col">Promotoría</th>
                                                        <th scope="col">Clave de Agente</th>
                                                        <th scope="col">Póliza</th>
                                                        <th scope="col">OneShot</th>
                                                        <th scope="col">Mesas</th>
                                                        <th scope="col">Mostrar</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("FechaRegistro")%></td>
                                                <td><%# Eval("FechaSolicitud")%></td>
                                                <td><%# Eval("FolioCompuesto")%></td>
                                                <td><%# Eval("Producto")%></td>
                                                <td><%# Eval("SumaAsegurada")%></td>
                                                <td><%# Eval("SumaPolizas")%></td>
                                                <td><%# Eval("PrimaTotal")%></td>
                                                <td><%# Eval("MONEDA")%></td>
                                                <td><%# Eval("DatosHtml")%></td>
                                                <td><%# Eval("RFC")%></td>
                                                <td><%# Eval("StatusTramite")%></td>
                                                <td><%# Eval("FechaStatus")%></td>
                                                <td><%# Eval("Tiempo")%></td>
                                                <td><%# Eval("Clave")%></td>
                                                <td><%# Eval("NomPromotoria")%></td>
                                                <td><%# Eval("AgenteClave")%></td>
                                                <td><%# Eval("IdSisLegados")%></td>
                                                <td><%# Eval("OneShot")%></td>
                                                <td>
                                                    <button onclick="ShowMesas(<%# Eval("Id")%>); return false;"><img src="../img/folder.png"></button>
                                                </td>
                                                <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName='Consultar' CommandArgument='<%# Eval("Id")%>' /></td>
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
                dom: 'Bfrtip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ]
            });
            /*$('select').removeClass('custom-select custom-select-sm form-control form-control-sm');*/

        });
    </script>
</asp:Content>
