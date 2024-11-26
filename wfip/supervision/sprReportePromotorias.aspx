<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprReportePromotorias.aspx.cs" Inherits="wfip.supervision.sprReportePromotorias" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
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
    
    <div class="row">
        <div class="x_panel">
            <br />
            <div class="container">
                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="card">
                        <div class="card-header">
                        <h3><small>Tramites por promotoría </small></h3>
                        </div>
                        <div class="card-body">
                            <p class="card-text">La consulta enlistara los trámites sobre la fecha de registro y filtros por claves de promotorías </p>
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
                                    <dx:ASPxListBox runat="server" ID="listBoxPromotorias" Theme="Material" SelectionMode="CheckColumn" EnableSelectAll="true" Width="100%" Height="200"  CallbackPageSize="15" Caption="Promotorias" >
                                        <FilteringSettings ShowSearchUI="true" />
                                    </dx:ASPxListBox>
                                </div>
                                 <div class="col-md-2 col-sm-2">
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
                                    
                                    <asp:Repeater ID="rptTramites" runat="server" >
                                        <HeaderTemplate>
                                            <table id="tblTramitesEspera" class="table table-striped table-bordered table-hover" style='width:100%'>
                                                <thead>
                                                    <tr >
                                                        <th scope="col">NumPromotoria</th>
                                                        <th scope="col">PromotoriaNombre</th>
                                                        <th scope="col">FechaPromotoria</th>
                                                        <th scope="col">Folio</th>
                                                        <th scope="col">TipoProducto</th>
                                                        <th scope="col">NumPoliza</th>
                                                        <th scope="col">Contratante</th>
                                                        <th scope="col">EstatusActual</th>
                                                        <th scope="col">FechaEstatusActual</th>
                                                        <th scope="col">SATramite</th>
                                                        <th scope="col">Prima total de acuerdo a cotización</th>
                                                        <th scope="col">Moneda</th>
                                                        <th scope="col">RFCContratante</th>
                                                        <th scope="col">EstadoContratante</th>
                                                        <th scope="col">ClaveAgente</th>
                                                        <th scope="col">NombreAgente Solicitud / Número de Orden</th>
                                                        <th scope="col">Fecha solicitud</th>

                                                        
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("NumPromotoria")%></td>
                                                <td><%# Eval("PromotoriaNombre")%></td>
                                                <td><%# Eval("FechaPromotoria")%></td>
                                                <td><%# Eval("Folio")%></td>
                                                <td><%# Eval("TipoProducto")%></td>
                                                <td><%# Eval("NumPoliza")%></td>
                                                <td><%# Eval("Contratante")%></td>
                                                <td><%# Eval("EstatusActual")%></td>
                                                <td><%# Eval("FechaEstatusActual")%></td>
                                                <td><%# Eval("SATramite")%></td>
                                                <td><%# Eval("PrimaTotal")%></td>
                                                <td><%# Eval("Moneda")%></td>
                                                <td><%# Eval("RFCContratante")%></td>
                                                <td><%# Eval("EstadoContratante")%></td>
                                                <td><%# Eval("ClaveAgente")%></td>
                                                <td><%# Eval("NumeroOrden")%></td>
                                                <td><%# Eval("FechaSolicitud")%></td>
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
</asp:Content>
