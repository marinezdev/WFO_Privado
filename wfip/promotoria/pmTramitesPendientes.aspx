<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="pmTramitesPendientes.aspx.cs" Inherits="wfip.promotoria.pmTramitesPendientes" %>
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
    <br />
    <div style="width:100%">
        <div style="width: 90%; margin:auto;">
            <fieldset>
                <legend>TRÁMITES PENDIENTES</legend>
                <div id="dvCajaTramite" style="width: 100%;">
                    <div id="cajaRptTramite" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                        <asp:Repeater ID="rptTramite" runat="server" OnItemDataBound="rptTramite_ItemDataBound" OnItemCommand="rptTramite_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTramite" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Fecha envío</th>
                                        <th scope="col">Número de trámite</th>
                                        <th scope="col">Orden de Trabajo</th>
                                        <th scope="col">Operación</th>
                                        <th scope="col">Producto</th>
                                        <th scope="col">Información del Contratante</th>
                                        <th scope="col">Fecha Firma de Solicitud </th>
                                        <th scope="col">Estado</th>
                                        <!--<th scope="col"></th>-->
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td style="text-align: center;"><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                    <td><%#Eval("FolioCompuesto")%></td>
                                    <td><%#Eval("NumeroOrden")%></td>
                                    <td><%#Eval("Flujo")%></td>
                                    <td><%#Eval("Producto")%></td>
                                    <td><%#Eval("DatosHtml")%></td>
                                    <td style="text-align: center;"><%# Eval("FechaSolicitud","{0:dd/MM/yyyy }")%></td>
                                    <td style="width: 40px; text-align:left; vertical-align:middle"><%# Eval("EstadoNombre")%></td>
                                    <!--<td style="width: 20px; text-align:center"><asp:Image ID="imgEstado" runat="server" ImageUrl="~/img/bolaGris.png"  /></td>-->
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName ="Consultar" CommandArgument='<%# Eval("Id")%>' /></td>
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
        </div>
    </div>
</asp:Content>
