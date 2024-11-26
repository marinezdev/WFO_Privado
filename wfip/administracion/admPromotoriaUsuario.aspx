<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admPromotoriaUsuario.aspx.cs" Inherits="wfip.administracion.admPromotoriaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblUsuariosAsignandos').DataTable({
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
            $('#tblUsrSinAsignar').DataTable({
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
    <div style="width: 80%; margin: 0 auto">
        <fieldset>
            <legend>USUARIOS DE PROMOTORIAS</legend>
            <div style="width:60%; margin:auto; text-align:center;">
                <asp:Label ID="lbSepPromotoria" runat="server" Text="PROMOTORIA:"></asp:Label>
                <asp:DropDownList ID="ddlSelPromotoria" runat="server" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlSelPromotoria_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <asp:Panel ID="pnlCajaPromUsr" runat="server" Width="100%">
                <h3><asp:Literal ID="ltNomPromotoria" runat="server"></asp:Literal></h3>
                <table id="tblCajaPromUssr" style="width:100%">
                    <tr>
                        <td style="width:50%; vertical-align:top">
                            <h4>USUARIOS ASIGNADOS</h4>
                            <asp:Repeater ID="rptUsuariosAsignandos" runat="server" OnItemCommand="rptUsuariosAsignandos_ItemCommand" >
                                <HeaderTemplate>
                                    <table id="tblUsuariosAsignandos" style="width:95%; margin:auto; font-size:12px;" class="display" >
                                        <thead>
                                            <th scope="col">Nombre</th>
                                            <th scope="col"></th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("Texto")%></td>
                                        <td style="width: 30px; text-align: center;">
                                            <asp:Button ID="btnQuitar" runat="server" Text="Quitar" CssClass="btnGris" CommandName="quitar" CommandArgument='<%# Eval("valor")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="vertical-align:top">
                            <h4>USUARIOS SIN ASIGNAR</h4>
                            <asp:Repeater ID="rptUsrSinAsignar" runat="server" OnItemCommand="rptUsrSinAsignar_ItemCommand" >
                                <HeaderTemplate>
                                    <table id="tblUsrSinAsignar" style="width:95%; margin:auto; font-size:12px;" class="display" >
                                        <thead>
                                            <th scope="col">Nombre</th>
                                            <th scope="col"></th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("texto")%></td>
                                        <td style="width: 30px; text-align: center;">
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btnGris" CommandName="agregar" CommandArgument='<%# Eval("valor")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
