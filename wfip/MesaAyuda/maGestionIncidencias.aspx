<%@ Page Title="" Language="C#" MasterPageFile="~/MesaAyuda/MesaAyuda.Master" AutoEventWireup="true" CodeBehind="maGestionIncidencias.aspx.cs" Inherits="wfip.MesaAyuda.maGestionIncidencias" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramite').DataTable({
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>GESTION DE INCIDENCIAS</legend>
        <table id="tblCajaGraficas" style="width: 94%; margin:auto;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Theme="Aqua" Width="530px">
                        <DiagramSerializable>
                            <dx:XYDiagram>
                                <AxisX VisibleInPanesSerializable="-1">
                                </AxisX>
                                <AxisY VisibleInPanesSerializable="-1">
                                </AxisY>
                            </dx:XYDiagram>
                        </DiagramSerializable>
                        <Legend Name="Default Legend" Visibility="False"></Legend>
                        <SeriesSerializable>
                            <dx:Series Name="dxSreTotales">
                            </dx:Series>
                        </SeriesSerializable>
                        <Titles>
                            <dx:ChartTitle Font="Arial, 12pt" Text="TOP 10 INGRESADOS" />
                        </Titles>
                    </dx:WebChartControl>
                </td>
                <td style="vertical-align: top">
                    <dx:WebChartControl ID="dxChtSuspendidos" runat="server" CrosshairEnabled="True" Height="300px" Theme="Aqua" Width="530px">
                        <DiagramSerializable>
                            <dx:XYDiagram>
                                <AxisX VisibleInPanesSerializable="-1">
                                </AxisX>
                                <AxisY VisibleInPanesSerializable="-1">
                                </AxisY>
                            </dx:XYDiagram>
                        </DiagramSerializable>
                        <Legend Name="Default Legend" Visibility="False"></Legend>
                        <SeriesSerializable>
                            <dx:Series Name="dxsreSuspendidos">
                            </dx:Series>
                        </SeriesSerializable>
                        <Titles>
                            <dx:ChartTitle Font="Arial, 12pt" Text="TOP 10 SUSPENDIDOS" />
                        </Titles>
                    </dx:WebChartControl>
                </td>
            </tr>
        </table>
        <div id="dvCajaTramite" style="width: 100%;">
            <div id="cajaRptTramite" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                <asp:Repeater ID="rptTramite" runat="server" OnItemDataBound="rptTramite_ItemDataBound" OnItemCommand="rptTramite_ItemCommand">
                    <HeaderTemplate>
                        <table id="tblTramite" style="width: 100%" class="display">
                            <thead>
                                <th scope="col">TRÁMITE</th>
                                <th scope="col">PROMOTORIA</th>
                                <th scope="col">TIPO DE TRÁMITE</th>
                                <th scope="col">INFORMACIÓN DEL CONTRATANTE</th>
                                <th scope="col">FECHA ENVÍO</th>
                                <th scope="col">ESTADO</th>
                                <th scope="col"></th>
                                <th scope="col"></th>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color:white; color: #333333">
                            <td><%# Eval("Id","{0:0000#}")%></td>
                            <td><%# Eval("PromotoriaNombre")%></td>
                            <td><%# Eval("Flujo")%> - <%# Eval("TramiteNombre")%></td>
                            <td><%# Eval("DatosHtml")%></td>
                            <td><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                            <td><%# Eval("EstadoNombre")%></td>
                            <td style="width: 20px; text-align: center">
                                <asp:Image ID="imgEstado" runat="server" ImageUrl="~/img/bolaGris.png" /></td>
                            <td style="width: 20px; text-align: center">
                                <asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName="Consultar" CommandArgument='<%# Eval("Id")%>' /></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </fieldset>
</asp:Content>
