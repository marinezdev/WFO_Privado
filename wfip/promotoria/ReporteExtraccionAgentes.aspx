<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/iniPromotoria.Master" AutoEventWireup="true" CodeBehind="ReporteExtraccionAgentes.aspx.cs" Inherits="wfip.promotoria.ReporteExtraccionAgentes" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
    <link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" rel="stylesheet" />

    <!--
<script src="https://code.jquery.com/jquery-3.3.1.js"></script>
<script src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    -->
<script src="https://cdn.datatables.net/buttons/1.6.0/js/dataTables.buttons.min.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.flash.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.html5.min.js"></script>
<script src="https://cdn.datatables.net/buttons/1.6.0/js/buttons.print.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <div class="row">
        <div class="x_panel">
            <br />
            <div class="container">
                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="card">
                        <div class="card-header">
                        <h3><small>Reporte promotoría </small></h3>
                        </div>
                        <div class="card-body">
                            <p class="card-text">La consulta enlistara los trámites sobre la fecha de registro y filtros por estatus o agentes. </p>
                            <div class="row">
                                <div class="col-md-6 col-sm-6">
                                    <dx:aspxdateedit ID="CalDesde" runat="server" Theme="Material" EditFormat="Custom"   Width="190" Caption="Desde:">
                                        <TimeSectionProperties Adaptive="true">
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                        <CalendarProperties>
                                            <FastNavProperties DisplayMode="Inline" />
                                        </CalendarProperties> 
                                    </dx:aspxdateedit>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <dx:aspxdateedit ID="CalHasta" runat="server" Theme="Material"  EditFormat="Custom"  Width="190" Caption="Hasta">
                                        <TimeSectionProperties Adaptive="true">
                                            <TimeEditProperties EditFormatString="hh:mm tt" />
                                        </TimeSectionProperties>
                                        <CalendarProperties>
                                            <FastNavProperties DisplayMode="Inline" />
                                        </CalendarProperties>
                                     </dx:aspxdateedit>
                                </div>
                            </div>
                            <div class="row">
                                <hr />
                                <div class="col-md-6 col-sm-6">
                                    <dx:ASPxListBox runat="server" ID="listBoxAgentes" Theme="Material" SelectionMode="CheckColumn" EnableSelectAll="true" Width="100%" Height="200"  CallbackPageSize="15" Caption="Agente" >
                                        <FilteringSettings ShowSearchUI="true" />
                                    </dx:ASPxListBox>
                                </div>
                                <div class="col-md-4 col-sm-4">
                                    <dx:ASPxListBox runat="server" ID="listBoxEstatus" Theme="Material" SelectionMode="CheckColumn" EnableSelectAll="true" Width="80%" Height="200"  CallbackPageSize="15" Caption="Estatus">
                                        <FilteringSettings ShowSearchUI="true" />
                                    </dx:ASPxListBox>
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
                            <div class="row">
                                <hr />
                                <div class="col-md-12 col-sm-12">
                                    <asp:Repeater ID="rptTramites" runat="server">
                                        <HeaderTemplate>
                                            <table id="tblTramites" class="table table-striped table-bordered table-hover" style='width:100%'>
                                                <thead>
                                                    <tr >
                                                        <th scope="col">ClaveAgente</th>
                                                        <th scope="col">NombreAgente</th>
                                                        <th scope="col">Fecha Solicitud</th>
                                                        <th scope="col">Fecha Ingreso Promotoria</th>
                                                        <th scope="col">Folio</th>
                                                        <th scope="col">Operación</th>
                                                        <th scope="col">RFCContratante</th>
                                                        <th scope="col">Tipo de Contratante</th>
                                                        <th scope="col">TipoProducto</th>
                                                        <th scope="col">SATramite</th>
                                                        <th scope="col">Prima Total de Acuerdo a Cotización</th>
                                                        <th scope="col">EstatusActual</th>
                                                        <th scope="col">NumPoliza</th>
                                                        <th scope="col">FechaEstatusActual</th>
                                                        <th scope="col">Motivo de suspensión</th>
                                                        <th scope="col">No. De Ringreso</th>
                                                        <th scope="col">Comentario último estatus</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("ClaveAgente")%></td>
                                                <td><%# Eval("NombreAgente")%></td>
                                                <td><%# Eval("Fecha Solicitud")%></td>
                                                <td><%# Eval("Fecha Ingreso Promotoria")%></td>
                                                <td><%# Eval("Folio")%></td>
                                                <td><%# Eval("Operación")%></td>
                                                <td><%# Eval("RFCContratante")%></td>
                                                <td><%# Eval("Tipo de Contratante")%></td>
                                                <td><%# Eval("TipoProducto")%></td>
                                                <td><%# Eval("SATramite")%></td>
                                                <td><%# Eval("Prima Total de Acuerdo a Cotización")%></td>
                                                <td><%# Eval("EstatusActual")%></td>
                                                <td><%# Eval("NumPoliza")%></td>
                                                <td><%# Eval("FechaEstatusActual")%></td>
                                                <td><%# Eval("Motivo de suspensión")%></td>
                                                <td><%# Eval("No De Ringreso")%></td>
                                                <td><%# Eval("Comentario último estatus")%></td>
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
            $('#tblTramites').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },

                scrollY: "400px",
                scrollX: true,
                scrollCollapse: true,
                fixedColumns: true,
                dom: "Blfrtip", buttons: [{
                    extend: "copy", className: "btn-sm"
                }, {
                    extend: "csv", className: "btn-sm", title: 'Reporte Agentes'
                }, {
                    extend: "excel", className: "btn-sm", title: 'Reporte Agentes'
                }, {
                    extend: "pdfHtml5", className: "btn-sm", title: 'Reporte Agentes'
                }, {
                    extend: "print", className: "btn-sm"
                    }]
                /*
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
                /*
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                scrollY: "410px",
                scrollX: true,
                scrollCollapse: true,
                fixedColumns: true,
                */
            });
            /*$('select').removeClass('custom-select custom-select-sm form-control form-control-sm');*/
            
        });
    </script>
</asp:Content>
