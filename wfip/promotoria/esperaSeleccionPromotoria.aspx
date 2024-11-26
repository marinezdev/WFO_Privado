<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="esperaSeleccionPromotoria.aspx.cs" Inherits="wfip.promotoria.esperaSeleccionPromotoria" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblPromo').DataTable({
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
    <asp:HiddenField ID ="hdIdProm" runat ="server" />


    <div style ="width:80%; margin:0 auto">
        <fieldset>
            <legend>SELECCIONAR PROMOTORÍA</legend>
            <br />
            <asp:Panel ID ="pnPromotor" runat ="server" Width="100%">
                <asp:Label ID="lblMessageBySystem" runat="server" Font-Bold="false" ForeColor="Orange" Text ="No asignado" Font-Size="Large"> </asp:Label>
                <asp:Repeater ID="rptPromo" runat="server" OnItemCommand="rptPromo_ItemCommand" >
                    <HeaderTemplate>
                        <table id="tblPromo" style="width:100%" class="display" >
                            <thead>
                                <th scope="col">Nombre</th>
                                <th scope="col">Clave</th>
                                <th scope="col">Direccion</th>
                                <th scope="col">Correo</th>
                                <th scope="col">Telefono</th>
                                <th scope="col"></th>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Nombre")%></td>
                            <td style="width: 100px"><%# Eval("clave")%></td>
                            <td><%# Eval("Direccion")%></td>
                            <td style="width: 100px"><%# Eval("Correo")%></td>
                            <td style="width: 150px"><%# Eval("telefono")%></td>
                            <td style="width: 35px; text-align:center">
                                <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/seleccion2.png" CommandName="Editar" CommandArgument='<%# Eval("Id")%>'  CausesValidation="false"  ToolTip ="Editar"/>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>