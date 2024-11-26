<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReportePCI.aspx.cs" Inherits="wfip.supervision.sprReportePCI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.2.1/Chart.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
    <link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <div class="row">
        <div class="x_panel">
            <br />
            <div class="container">
                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="card">
                        <div class="card-header">
                        <h3><small>Trámites estatus PCI  </small></h3>
                        </div>
                        <div class="card-body">
                            <hr />
                            <div class="row">
                                <div class="col-md-8 col-sm-8">
                                    <div class="card">
                                      <div class="card-body">
                                        <div class="container">
	                                    <h2 class="text-center">Estatus PCI</h2>
                                        <canvas id="barChart" width="100%" height="250"></canvas>
	
                                      </div>
                                      </div>
                                    </div>
                                     
                                </div>
                                <div class="col-md-2 col-sm-2">
                                    <p>Mostrar detalle de gráfica </p>
                                    <asp:Button ID="btnFiltroMes"  CssClass="btn btn-primary" runat="server" Text="Mostrar" OnClick="btnConsulta_Click"/>
                                </div>
                            </div>

                   
                            <hr />
                            <div class="row">
                                <div class="col-md-12 col-sm-12">
                                    <asp:Repeater ID="rptTramitesPCI" runat="server">
                                        <HeaderTemplate>
                                            <table id="tblTramitesPCI" class="table table-striped table-bordered table-hover" style='width:100%'>
                                                <thead>
                                                    <tr >
                                                        <th scope="col">FECHA INGRESO</th>
                                                        <th scope="col">FOLIO</th>
                                                        <th scope="col">RAMO</th>
                                                        <th scope="col">ULTIMO STATUS TRAMITE</th>
                                                        <th scope="col">FECHA STATUS PCI</th>
                                                        <th scope="col">CLAVE PROMOTORIA </th>
                                                        <th scope="col">PROMOTORIA </th>
                                                        <th scope="col">CLAVE AGENTE </th>
                                                        <th scope="col">POLIZA </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("FECHA INGRESO")%></td>
                                                <td><%# Eval("FOLIO")%></td>
                                                <td><%# Eval("RAMO")%></td>
                                                <td><%# Eval("ULTIMO STATUS TRAMITE")%></td>
                                                <td><%# Eval("FECHA STATUS PCI")%></td>
                                                <td><%# Eval("CLAVE PROMOTORIA")%></td>
                                                <td><%# Eval("PROMOTORIA")%></td>
                                                <td><%# Eval("CLAVE AGENTE")%></td>
                                                <td><%# Eval("POLIZA")%></td>
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
            $('#tblTramitesPCI').DataTable({
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
    <script>
        function getRandomColorHex() {
            var hex = "0123456789ABCDEF",
                color = "#";
            for (var i = 1; i <= 6; i++) {
                color += hex[Math.floor(Math.random() * 16)];
            }
            return color;
        }

        jQuery(document).ready(function () {

             $.ajax({
                type: "POST",
                url: "sprReportePCI.aspx/consultaLaboratorios",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultado,
                error: errores
            });

           

            function resultado(data) {
                console.log(data);

                var ddDataLabel = [];
                var ddDataValue = [];
                var ddDatabackgroundColor = [];

                for (var b = 0; b < data.d.statusPCI.length; b++) {
                     
                    ddDataLabel.push(data.d.statusPCI[b].labels);
                    ddDataValue.push(data.d.statusPCI[b].data);
                    ddDatabackgroundColor.push(getRandomColorHex());
                }

                console.log(ddDataLabel);
                console.log(ddDataValue);
                console.log(ddDatabackgroundColor);

                var chartDiv = $("#barChart");
                var myChart = new Chart(chartDiv, {
                    type: 'pie',
                    data: {
                        labels: ddDataLabel,
                        datasets: [
                        {
                            data: ddDataValue,
                            backgroundColor: ddDatabackgroundColor
                        }]
                    },
                    options: {
                        title: {
                            display: true,
                            text: 'Tramites acumulados por promotorias (clave)'
                        },
		                responsive: true,
                        maintainAspectRatio: false,
                    }
                });

            }

            function errores(data) {
                //msg.responseText tiene el mensaje de error enviado por el servidor
                alert('Error: ' + msg.responseText);
            }

            
        });
    </script>
</asp:Content>
