<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="opProduccionR.aspx.cs" Inherits="wfip.supervision.opProduccionR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblProduccion').DataTable({
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
    <fieldset>
        <legend>CONSULTA DE PRODUCTIVIDAD</legend>
        <table id="tblDatosBusca" style="width:50%; margin:auto;">
            <tr>
                <td style="text-align:right; width:20%">Usuario:</td>
                <td>
                    <asp:DropDownList ID="ddlUsuario" runat="server" Width="100%"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">Año:</td>
                <td><asp:DropDownList ID="ddlAnio" runat="server">
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">Mes:</td>
                <td><asp:DropDownList ID="ddlMes" runat="server">
                        <asp:ListItem Value="01">ENERO</asp:ListItem>
                        <asp:ListItem Value="02">FEBRERO</asp:ListItem>
                        <asp:ListItem Value="03">MARZO</asp:ListItem>
                        <asp:ListItem Value="04">ABRIL</asp:ListItem>
                        <asp:ListItem Value="05">MAYO</asp:ListItem>
                        <asp:ListItem Value="06">JUNIO</asp:ListItem>
                        <asp:ListItem Value="07">JULIO</asp:ListItem>
                        <asp:ListItem Value="08">AGOSTO</asp:ListItem>
                        <asp:ListItem Value="09">SEPTIEMBRE</asp:ListItem>
                        <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                        <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                        <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="boton" OnClick="btnConsultar_Click" /></td>
            </tr>
        </table>
        <asp:Panel ID="pnProduccion" runat="server">
            <h4  style ="color:#187BB4">TRAMITES PROCESADOS</h4>
            <div style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                <asp:Repeater ID="rpProduccion" runat="server">
                    <HeaderTemplate>
                        <table id="tblProduccion" style="width:100%" class="display" >
                            <thead>
                                <th scope="col">Fecha inicio</th>
                                <th scope="col">Fecha termino</th>
                                <th scope="col">Mesa</th>
                                <th scope="col">Tipo de trámite</th>
                                <th scope="col">Número de trámite</th>
                                <th scope="col">Estado</th>
                                <th scope="col">Observación</th>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td style="text-align: center; width: 100px"><%# Eval("FechaInicio","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                            <td style="text-align: center; width: 100px"><%# Eval("FechaTermino","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                            <td style="width: 150px; text-align: center;"><%# Eval("NombreMesa")%></td>
                            <td><%# Eval("FlujoNombre")%> - <%# Eval("TramiteNombre")%></td>
                            <td style="width: 100px"><%# Eval("IdTramite","{0:0000#}")%></td>
                            <td style="width: 100px; text-align: center;"><%# Eval("EstadoNombre")%></td>
                            <td><%# Eval("Observacion")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </fieldset>
</asp:Content>

