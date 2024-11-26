<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GpoHistorico.aspx.cs" Inherits="wfip.Grupal.GpoHistorico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#TblBuzonEntrada').DataTable({
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
        <legend>HISTORIAL</legend>
        <div id="DvBtnCerrar" style="width: 80%; margin: auto; text-align: right;">
            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="BtnCerrar_Click" />
        </div>
        <div id="DvCajRegistros" style="width: 80%; margin: auto;">
            <asp:Panel ID="PnlBuzonEntrada" runat="server" Width="100%">
                <asp:Repeater ID="RptBuzonEntrada" runat="server" OnItemCommand="RptBuzonEntrada_ItemCommand">
                    <HeaderTemplate>
                        <table id="TblBuzonEntrada" style="width: 100%; font-size:10px;" class="display">
                            <thead>
                                <tr>
                                    <th scope="col">NOMBRE</th>
                                    <th scope="col">ASUNTO</th>
                                    <th scope="col">FECHA</th>
                                    <th scope="col">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Tramite_Nombre")%></td>
                            <td><%# Eval("Tramite_Asunto")%></td>
                            <td><%# Eval("Tramite_Fecha")%></td>
                            <td style="width: 35px; text-align: center">
                                <asp:ImageButton ID="ImgBtnAbrir" runat="server" ImageUrl="~/img/foward.png" CommandName="Abrir" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" ToolTip="Abrir" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </div>
    </fieldset>
</asp:Content>
