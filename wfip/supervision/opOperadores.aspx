<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="opOperadores.aspx.cs" Inherits="wfip.operacion.opOperadores" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTrmProceso').DataTable({
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

            $('#tblTrmPausa').DataTable({
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
        function Continuar() {
            var continuar = false;
            if (confirm('Desea liberar el tramite ?')) { pnlMsgProcesando.Show(); continuar = true; }
            return continuar;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <fieldset>
        <legend>CONSULTA DE OPERADORES</legend>
        <table id="tblDatosBusca" style="width: 100%;">
            <tr>
                <td style="text-align: center; width: 300px">OPERADORES</td>
                <td>OPERACION</td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    <asp:ListBox ID="lstBxOperadores" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstBxOperadores_SelectedIndexChanged" Width="100%"></asp:ListBox>
                </td>
                <td>
                    <asp:Panel ID="pnTrmPausa" runat="server" Width="100%">
                        <h4 style="color: #187BB4">TRAMITES EN PAUSA</h4>
                        <div id="dvTrmPausa" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                            <asp:Repeater ID="rptTrmPausa" runat="server" OnItemCommand="rptTrmPausa_ItemCommand">
                                <HeaderTemplate>
                                    <table id="tblTrmPausa" style="width: 100%" class="display">
                                        <thead>
                                            <th scope="col">Fecha envío</th>
                                            <th scope="col">Trámite</th>
                                            <th scope="col">Tipo de trámite</th>
                                            <th scope="col">Información del contratante</th>
                                            <th scope="col">Mesa</th>
                                            <th scope="col">Fecha Inicio en Mesa</th>
                                            <th scope="col">Observaciones</th>
                                            <th scope="col"></th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td style="text-align: center; width: 100px"><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                        <td style="width: 80px"><%# Eval("Id","{0:0000#}")%></td>
                                        <td><%# Eval("Flujo")%> - <%# Eval("TramiteNombre")%></td>
                                        <td><%# Eval("DatosHtml")%></td>
                                        <td style="width: 150px; text-align: center;"><%# Eval("MesaNombre")%></td>
                                        <td style="text-align: center; width: 100px"><%# Eval("FechaInicio","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                        <td style="width: 100px; text-align: center;"><%# Eval("ObservacionPublica")%></td>
                                        <td style="width: 20px; text-align: center">
                                            <asp:ImageButton ID="imbtnQuitarPausa" runat="server" ImageUrl="~/img/foward.png" CommandName='<%# Eval("IdMesa")%>' CommandArgument='<%# Eval("Id")%>' OnClientClick="return Continuar();" /></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>
                    <br />
                    <br />
                    <asp:Panel ID="pnTrmProceso" runat="server" Width="100%">
                        <h4 style="color: #187BB4">TRAMITES EN PROCESO</h4>
                        <div style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                            <asp:Repeater ID="rpTrmProceso" runat="server">
                                <HeaderTemplate>
                                    <table id="tblTrmProceso" style="width: 100%" class="display">
                                        <thead>
                                            <th scope="col">Fecha envío promotor</th>
                                            <th scope="col">Número de trámite</th>
                                            <th scope="col">Tipo de trámite</th>
                                            <th scope="col">Información del contratante</th>
                                            <th scope="col">Mesa</th>
                                            <th scope="col">Fecha Inicio en Mesa</th>
                                            <th scope="col">Estado</th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td style="text-align: center; width: 150px"><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                        <td style="width: 100px"><%# Eval("Id","{0:0000#}")%></td>
                                        <td><%# Eval("Flujo")%> - <%# Eval("TramiteNombre")%></td>
                                        <td><%# Eval("DatosHtml")%></td>
                                        <td style="width: 150px; text-align: center;"><%# Eval("MesaNombre")%></td>
                                        <td style="text-align: center; width: 150px"><%# Eval("FechaInicio","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                        <td style="width: 100px; text-align: center;"><%# Eval("EstadoNombre")%></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>

                </td>
            </tr>
        </table>
    </fieldset>
    <dx:ASPxLoadingPanel ID="pnlMsgProcesando" runat="server" ClientInstanceName="pnlMsgProcesando" Modal="true" Text="Procesando...">
    </dx:ASPxLoadingPanel>
</asp:Content>
