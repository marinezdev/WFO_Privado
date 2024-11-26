<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="sprMapaSupervisorR.aspx.cs" Inherits="wfip.supervision.sprMapaSupervisorR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
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
                        <h3><small>DETALLE DE MOVIMIENTOS </small></h3>
                        </div>
                        <div class="card-body">
                            <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" HeaderText="MESA: " Theme="Aqua">
                                <ContentPaddings Padding="25px" />
                                <PanelCollection>
                                    <dx:PanelContent runat="server">
                                        
                                        <div class="row">
                                            <hr />
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <asp:Repeater ID="rptMesaDetalle" runat="server" OnItemCommand="rptImgListadoTramites_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table id="tblTramitesEspera" class="table table-striped table-bordered table-hover" style='width:100%'>
                                                            <thead>
                                                                <tr >
                                                                    <th scope="col">FLUJO</th>
                                                                    <th scope="col">USUARIOS CONECTADOS</th>
                                                                    <th scope="col">TRAMITES NUEVOS</th>
                                                                    <th scope="col">TRAMITES CON REINGRESO </th>
                                                                    <th scope="col">TOTAL</th>
                                                                    <th scope="col">MOSTRAR </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("FLUJO")%></td>
                                                            <td><%# Eval("USUARIOS")%></td>
                                                            <td><%# Eval("NUEVOS")%></td>
                                                            <td><%# Eval("REINGRESOS")%></td>
                                                            <td><%# Eval("TOTALES")%></td>
                                                            <td><asp:ImageButton ID="ListadoTramites" runat="server" ImageUrl="~/img/folder.png" CommandName='<%# Eval("FLUJO1")%>' CommandArgument='<%# Eval("FLUJO2")%>' /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                            <div class="row">
                                            <hr />
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="rptImgDetalleTramite_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table id="tblDetallleMesa" class="table table-striped table-bordered table-hover" style='width:100%'>
                                                            <thead>
                                                                <tr >
                                                                    <th scope="col">FOLIO</th>
                                                                    <th scope="col">ESTATUS MESA</th>
                                                                    <th scope="col">PRIORIDAD</th>
                                                                    <th scope="col">REINGRESOS</th>
                                                                    <th scope="col">INGRESO SISTEMA</th>
                                                                    <th scope="col">INGRESO EN MESA</th>
                                                                    <th scope="col">ULTIMO REGISTRO</th>
                                                                    <th scope="col">USUARIO</th>
                                                                    <th scope="col">TIEMPO ATENCION</th>
                                                                    <th scope="col">TIEMPO EN MESA</th>
                                                                    <th scope="col">INFORMACIÓN DEL CONTRATANTE</th>
                                                                    <th scope="col">MOSTRAR</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("FOLIO")%></td>
                                                            <td><%# Eval("ESTATUS MESA")%></td>
                                                            <td><%# Eval("PRIORIDAD")%></td>
                                                            <td><%# Eval("REINGRESOS")%></td>
                                                            <td><%# Eval("INGRESO SISTEMA")%></td>
                                                            <td><%# Eval("INGRESO EN MESA")%></td>
                                                            <td><%# Eval("ULTIMO REGISTRO")%></td>
                                                            <td><%# Eval("USUARIO")%></td>
                                                            <td><%# Eval("TIEMPO ATENCION")%></td>
                                                            <td><%# Eval("TIEMPO EN MESA")%></td>
                                                            <td><%# Eval("INFORMACIÓN DEL CONTRATANTE")%></td>
                                                            <td>
                                                                <asp:ImageButton ID="ListadoTramites" runat="server" ImageUrl="~/img/folder.png" CommandName='<%# Eval("Id")%>' />
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
                        <br />
                    </div>
                    <br /><br />
                </div>
                
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblDetallleMesa').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                scrollY: "400px",
                scrollX: true,
                scrollCollapse: true,
                fixedColumns: true,

                /*
                scrollY: "400px",
                                fixedColumns: true,
                scrollX: true,
                scrollCollapse: true,
                
                */
                
            });
            /*$('select').removeClass('custom-select custom-select-sm form-control form-control-sm');*/
            
        });
    </script>
</asp:Content>
