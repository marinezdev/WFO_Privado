<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReporteProductividadOperacion.aspx.cs" Inherits="wfip.supervision.sprReporteProductividadOperacion" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
<script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
<script src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
<link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" rel="stylesheet" />

<script src="https://cdn.datatables.net/buttons/1.6.0/js/dataTables.buttons.min.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.flash.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.html5.min.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.print.min.js"></script>
 <script>
     function DetalleMesaGraf(IdOrden, IdFlujo) {
         var recipient = $("#<%=CalDesde.ClientID%>").val();
         var FachaIn = $("#cph_areaTrabajo_carTabPage_ASPxRoundPanel1_CalDesde_I").val();
         var FechaFin = $("#cph_areaTrabajo_carTabPage_ASPxRoundPanel1_CalHasta_I").val(); 

         $.ajax({
                type: "POST",
                url: "sprReporteProductividadOperacion.aspx/DetalleMesaGraf",
                data: '{IdOrden: ' + IdOrden + ', IdFlujo: ' + IdFlujo + ', FechaIn: "'+FachaIn+'", FechaFin: "'+FechaFin+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultadoGraf,
                error: errores
         });
     }

     function resultadoGraf(data) {
         //console.log(data);
         $("#DetalleMesaGraf").modal("show");
         pintaGrafica(data);
         
     }

     function DetalleMesa(IdOrden, IdFlujo) {

         var recipient = $("#<%=CalDesde.ClientID%>").val();
         var FachaIn = $("#cph_areaTrabajo_carTabPage_ASPxRoundPanel1_CalDesde_I").val();
         var FechaFin = $("#cph_areaTrabajo_carTabPage_ASPxRoundPanel1_CalHasta_I").val(); 

         $.ajax({
                type: "POST",
                url: "sprReporteProductividadOperacion.aspx/DetalleMesa",
                data: '{IdOrden: ' + IdOrden + ', IdFlujo: ' + IdFlujo + ', FechaIn: "'+FachaIn+'", FechaFin: "'+FechaFin+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resultado,
                error: errores
         });

         //$("#Titulo").text('Productividad por mesa: '+ Mesa);
     }

     function resultado(data) {
        console.log(data);
         $("#DetalleMesa").modal("show");
         $('#DatosConsulta').html("");
         var tabla = "<table id='InfomacionMesas' class='table table-striped table-bordered table-hover' style='width:100%'>" +
                "<thead>" +
                "<tr>" +
                        "<th scope='col'>Operador</th>" +
                        "<th scope='col'>Total</th>" +
                        "<th scope='col'>Reingresos</th>" +
                        "<th scope='col'>Productividad</th>" +
                        "<th scope='col'>Calidad</th>" +
                        "<th scope='col'></th>" +
                    "</tr>" +
                "</thead>" +
                "<tbody>";
            for (var b = 0; b < data.d.consulta.length; b++) {
                var img = "";
                if (Total = 0) {
                    img = "<img src='../img/bolaGrisObscuro.png' />";
                } else {
                    var pro = (((data.d.consulta[b].Reingreso * 100) / (data.d.consulta[b].Total)) - 100) * -1
                    if (pro >= 90) {
                        img = "<img src='../img/bolaVerde.png' /> ";
                    } else if (pro >= 80) {
                        img = "<img src='../img/bolaAzul.png' />";
                    }else if (pro >= 60) {
                        img = "<img src='../img/bolaAmarilla.png' />";
                    }else if (pro >= 50) {
                        img = "<img src='../img/bolaNaranja.png' />";
                    }else {
                        img = "<img src='../img/bolaRoja.png' />";
                    }
                }

                tabla += "<tr>" +
                            "<td>" + data.d.consulta[b].Nombre + "</td>" +
                            "<td>" + data.d.consulta[b].Total + "</td>" +
                            "<td>" + data.d.consulta[b].Reingreso + "</td>" +
                            "<td>" + data.d.consulta[b].Productividad + "</td>" +
                            "<td>" + data.d.consulta[b].Calidad + "</td>" +
                            "<td>" + img + "</td>" +

                        "</tr>";
            }
            tabla += "</tbody>" +
                "</table>";

            $('#DatosConsulta').html(tabla);

            $("#InfomacionMesas").dataTable().fnDestroy();
            $('#InfomacionMesas').DataTable({
                "order": [[1, "DESC"]],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                dom: "Blfrtip", buttons: [{
                    extend: "copy", className: "btn-sm"
                }, {
                    extend: "csv", className: "btn-sm", title: 'Reporte Productividad Mesa'
                }, {
                    extend: "excel", className: "btn-sm", title: 'Reporte Productividad Mesa'
                }, {
                    extend: "pdfHtml5", className: "btn-sm", title: 'Reporte Productividad Mesa'
                }, {
                    extend: "print", className: "btn-sm"
                    }]
            });
     }

     function errores(data) {
        //msg.responseText tiene el mensaje de error enviado por el servidor
        alert('Error: ' + msg.responseText);
     }
 </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

<div class="modal fade " id="DetalleMesa" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Productividad por mesa:</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body text-justify">
                <p id ="Titulo"></p>
                
                <div  id="DatosConsulta" class="text-center">

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" id="CloseModal" class="btn btn-secondary" data-dismiss="modal">CERRAR</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade " id="DetalleMesaGraf" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalTitle">Se recomiendo visualizar la gráfica en periodos de 30  a 31 días</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body text-justify">
                <p id ="TituloGraf"></p>
                
                 <div id="pieChartContent">
                    <h2>Sin Datos </h2>
                    <canvas id="pieChart" width="300" height="1"></canvas>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" id="CloseModal" class="btn btn-secondary" data-dismiss="modal">CERRAR</button>
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
                        <dx:ASPxPageControl ID="carTabPage" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True" >
                        <TabPages>
                            <dx:TabPage Text="PRODUCTIVIDAD DE OPERACIÓN">
                                <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <fieldset>
                                        <p class="card-text">Tramites finalizados en mesas con estatus: PCI, Hold, Suspendido, Procesable, No Procesable, Procesado, Rechazo, Cancelado, Caducad y  EnviaMesa.</p>
                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" HeaderText="MESAS " Theme="Aqua">
                                        <ContentPaddings Padding="25px" />
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <div class="row">
                                                        <label for="cmbFlujo" class="col-md-1 col-sm-1 control-label">Desde:</label>
                                                        <div class="col-md-3 col-sm-3">
                                                            <dx:aspxdateedit ID="CalDesde" runat="server" Theme="Material" EditFormat="Custom"   Width="190" Caption="">
                                                                <TimeSectionProperties Adaptive="true">
                                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                                </TimeSectionProperties>
                                                                <CalendarProperties>
                                                                    <FastNavProperties DisplayMode="Inline" />
                                                                </CalendarProperties> 
                                                            </dx:aspxdateedit>
                                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ValidationGroup="Consulta" controltovalidate="CalDesde" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                        </div>
                                                        <label for="cmbFlujo" class="col-md-1 col-sm-1 control-label">Hasta:</label>
                                                        <div class="col-md-3 col-sm-3">
                                                            <dx:aspxdateedit ID="CalHasta" runat="server" Theme="Material"  EditFormat="Custom"  Width="190" Caption="">
                                                                <TimeSectionProperties Adaptive="true">
                                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                                </TimeSectionProperties>
                                                                <CalendarProperties>
                                                                    <FastNavProperties DisplayMode="Inline" />
                                                                </CalendarProperties>
                                                             </dx:aspxdateedit>
                                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" ValidationGroup="Consulta" controltovalidate="CalHasta" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                        </div>
                                                        <label for="cmbFlujo" class="col-md-1 col-sm-1 control-label">Flujo:</label>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:DropDownList runat="server" ID="cmbFlujo" CssClass="form-control">
                                                                 <asp:ListItem Value="-1">SELECCIONAR </asp:ListItem>
                                                                 <asp:ListItem Value="1">INDIVIDUAL PRIVADO EMISIÓN GMM</asp:ListItem>
                                                                 <asp:ListItem Value="2">INDIVIDUAL PRIVADO EMISIÓN VIDA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="cmbFlujo" ValidationGroup="Consulta" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                              
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2 col-sm-2">
                                                            <asp:Button ID="btnConsultar"  ValidationGroup="Consulta" CssClass="btn btn-primary btn-block" runat="server" Text="Buscar"  OnClick="BtnConsultar_Click"/>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                         <div class="col-md-6 col-sm-6">
                                                            <asp:Label runat="server" ForeColor="Red" ID="Mensaje" Text =""></asp:Label>
                                                         </div>
                                                    </div>
                                                    <div class="row">
                                                        <hr />
                                                        <asp:Literal id="MesasLiteral" runat=server  text=""/>
                                                    </div>
                                                    <asp:Panel ID= "Panel1"  runat = "server" Visible="false">
                                                        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                                                    </asp:Panel>
                                                    <div class="row">
                                                        <hr />
                                                    </div>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxRoundPanel>
                                    </fieldset>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </div>
            </div>
        </div>
    </div>
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.js"></script>
<script>
    function pintaGrafica(data) {

        var pieChartContent = document.getElementById('pieChartContent');
        pieChartContent.innerHTML = '&nbsp;';
        $('#pieChartContent').append('<canvas id="pieChart" width="300" height="150"><canvas>');
        ctx = $("#pieChart").get(0).getContext("2d");


        var tiempos = new Array();
        for (var b = 1; b < data.d.tiempos.length; b++) {
            tiempos.push(data.d.tiempos[b].tiempo);
        }
        console.log(tiempos);
        var datasets = [];

        for (var a = 0; a < data.d.tablaModels.length; a++) {
            var obj = {};
            obj['label'] = data.d.tablaModels[a].label;

            var cantidades = new Array();
            for (var c = 1; c < data.d.tablaModels[a].tablaDatos.length; c++) {
                var re = /,/g;
                cantidades.push(data.d.tablaModels[a].tablaDatos[c].cantidad.replace(re, ''));
            };

            //console.log(cantidades);

            //cantidades = [10, 20, 30, 40, 50, 60];
            obj['data'] = cantidades;
            //obj['lineTension'] =  0,
            obj['fill'] = false,
            obj['borderColor'] =   getRandomColorHex(),
            obj['backgroundColor'] = 'transparent',
            //obj['borderDash'] = [5, 5],
             
            obj['pointBorderColor'] = 'rgba(0, 153, 194 ,0.5)',
                obj['pointBackgroundColor'] = 'rgba(0, 153, 194 ,0.5)',

            /*
            obj['pointRadius'] = 5,
            obj['pointHoverRadius'] = 10,
            obj['pointHitRadius'] = 30,
            obj['pointBorderWidth'] = 2,
            */
            //obj['pointStyle'] = 'rectRounded'
            //obj[' backgroundColor'] = 'transparent',
            /*
            obj['backgroundColor'] = getRandomColorHex();
            */
            datasets.push(obj);
        }
        
        var TiempoGraf = tiempos;
        
        var config = {
            type: 'line',
            data: {
                labels: TiempoGraf,
                datasets: datasets
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Productividad'
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                },
                hover: {
                    mode: 'nearest',
                    intersect: true
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Periodos de tiempo'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                        },
                    }]
                }
            }
        };
        var myLine = new Chart(ctx, config);
    }

    function shuffle(array) {
      var m = array.length, t, i;

      // While there remain elements to shuffle…
      while (m) {

        // Pick a remaining element…
        i = Math.floor(Math.random() * m--);

        // And swap it with the current element.
        t = array[m];
        array[m] = array[i];
        array[i] = t;
      }

      return array;
    }

    function getRandomColorHex() {
        var colours = shuffle(["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)", "rgb(153, 102, 255)", "rgb(231,233,237)","rgb(4, 118, 127)","rgb(50, 4, 127 )","rgb(149, 9, 88 )","rgb(182, 57, 83 )"]);

        color = "";
        
        for (var n = 1; n < colours.length-1; n++) {
            color =  colours[n];
        }
       
        return color;
    }
    
</script>
</asp:Content>
