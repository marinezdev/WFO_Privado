<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="usuariosV2_cat.aspx.cs" Inherits="wfip.administracion.usuariosV2_cat" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblCatalogo').DataTable({
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
        <legend>ADMINISTRACIÓN DE USUARIOS</legend>
        <br />
        <asp:Panel ID="pnlCatalogo" runat="server" Width="100%">
            <div id="cajaRptCatalogo" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                
                <table style="width:100%; margin:auto; text-align:right; ">
                    <tr>
                        <td>
                            <asp:Button ID="btnNewUser" runat="server" Text="Nuevo Usuario" CausesValidation="false" CssClass="btnVerde" OnClick="btnNewUser_Click" Height="30px" Width="150px" />
                            <br />&nbsp;
                        </td>
                    </tr>
                </table>
                
                <asp:Repeater ID="rptCatalogo" runat="server" OnItemCommand="rptCatalogo_ItemCommand" OnItemDataBound="rptCatalogo_ItemDataBound">
                    <HeaderTemplate>
                        <table id="tblCatalogo" style="width: 100%" class="display">
                            <thead>
                                <tr>
                                    <th scope="col">USUARIO</th>
                                    <th scope="col">NOMBRE</th>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Usuario")%></td>
                            <td><%# Eval("Nombre")%></td>
                            <td style="width: 30px; height: auto; text-align: center;">
                                <a><asp:ImageButton ID="imgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CommandName="activo" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" ToolTip="Activar / Desactivar Usuario..." /></a>
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <a><asp:ImageButton ID="imgBtnModificar" runat="server" ImageUrl="~/img/edit.png" CommandName="modificar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" ToolTip="Modifcar Información del Usuario..." /></a>
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <a><asp:ImageButton ID="imgBtnOperacion" runat="server" ImageUrl="~/img/mesa.png" CommandName="operacion" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" ToolTip="Permisos del Ususuario..." /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <br />
        </asp:Panel>
    </fieldset>
</asp:Content>
