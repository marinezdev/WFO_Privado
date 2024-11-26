<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteLaboratorios.aspx.cs" Inherits="wfip.supervision.sprReporteLaboratorios" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script>
        function ShowMesas(num,com) {
            $.ajax({
                type: "POST",
                url: "sprReporteLaboratorios.aspx/consultaLaboratorios",
                data: '{Id: ' + num + ', Combo:' + com + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultado,
                error: errores
            });
        }

        function resultado(data) {
            console.log(data);
            
            $("#Bitacora").modal("show");
            
            $('#DatosConsulta').html("");


            var tabla = "<table id='InfomacionMesas' class='table table-striped table-bordered table-hover' style='width:100%'>" +
                "<thead>" +
                "<tr>" +
                        "<td>Folio</td>" +
                        "<td>Estatus Tramite</td>" +
                        "<td>Datos</td>" +
                        "<td>Sexo</td>" +
                        "<td>Edad</td>" +
                        "<td>Suma Asegurada Básica</td>" +
                        "<td>Prima Total </td>" +
                        "<td>Notas</td>" +
                    "</tr>" +
                "</thead>" +
                "<tbody>";
            for (var b = 0; b < data.d.citaMedicas.length; b++) {
                tabla += "<tr>" +
                            "<td>" + data.d.citaMedicas[b].FolioCompuesto + "</td>" +
                            "<td>" + data.d.citaMedicas[b].Estatus + "</td>" +
                            "<td>" + data.d.citaMedicas[b].DatosHtml + "</td>" +
                            "<td>" + data.d.citaMedicas[b].Sexo + "</td>" +
                            "<td>" + data.d.citaMedicas[b].Edad + "</td>" +
                            "<td>" + data.d.citaMedicas[b].SumaPolizas + "</td>" +
                            "<td>" + data.d.citaMedicas[b].PrimaTotal + "</td>" +
                            "<td>" + data.d.citaMedicas[b].Notas + "</td>" +
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
                <h5 class="modal-title" id="NumeroFolio">Información de citas medicas</h5>
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
                        <h3><small>Reporte laboratorios (proveedores) </small></h3>
                        </div>
                        <div class="card-body">
                            <p class="card-text">El reporte muestra el total de laboratorios utilizados y el combo requerido.</p>
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
                                                        <th scope="col">Sucursal</th>
                                                        <th scope="col">Ciudad</th>
                                                        <th scope="col">Combo 1</th>
                                                        <th scope="col">Combo 2</th>
                                                        <th scope="col">Combo 3</th>
                                                        <th scope="col">Total </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("SUCURSAL")%></td>
                                                <td><%# Eval("CIUDAD")%></td>
                                                <td><button  class="btn btn-primary" <%#Eval("COMBO1").ToString() == "" ? "disabled" : "" %> onclick="ShowMesas(<%# Eval("IDPROVEEDOR")%>,1); return false;"><%#Eval("COMBO1").ToString() == "" ? '0' : Eval("COMBO1") %></button></td>
                                                <td><button  class="btn btn-primary" <%#Eval("COMBO2").ToString() == "" ? "disabled" : "" %> onclick="ShowMesas(<%# Eval("IDPROVEEDOR")%>,2); return false;"><%#Eval("COMBO2").ToString() == "" ? '0' : Eval("COMBO2") %></button></td>
                                                <td><button  class="btn btn-primary" <%#Eval("COMBO3").ToString() == "" ? "disabled" : "" %> onclick="ShowMesas(<%# Eval("IDPROVEEDOR")%>,3); return false;"><%#Eval("COMBO3").ToString() == "" ? '0' : Eval("COMBO3") %></button></td>
                                                <td><%# Eval("TOTALES")%></td>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramitesEspera').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                scrollY: "410px",
                scrollX: true,
                scrollCollapse: true,
                fixedColumns: true,
                
            });
            /*$('select').removeClass('custom-select custom-select-sm form-control form-control-sm');*/
            
        });
    </script>
</asp:Content>
