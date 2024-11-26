<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admListaFlujos.aspx.cs" Inherits="wfip.administracion.admListaFlujos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblFlujos').DataTable({
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
    <asp:ScriptManager ID="SM1" runat ="server"></asp:ScriptManager>
    <fieldset>
        <legend>FLUJOS DE TRABAJO</legend>
        <div style ="">
            <table id="tblCajaBtnCerrar" style="width:100%">
                <tr>
                    <td style="width:50%; text-align:left;">
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtNuevo" runat="server" Width="300px" ValidationGroup="nuevo"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txtNuevo" runat="server" ControlToValidate="txtNuevo" ErrorMessage="*" ForeColor="Red" Font-Size="14px" SetFocusOnError="True" ValidationGroup="nuevo"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txtNuevo" runat="server" TargetControlID="txtNuevo" FilterMode="ValidChars" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnNuevo" runat ="server"  Text ="Nuevo" CssClass="boton" OnClick="btnNuevo_Click" ValidationGroup="nuevo" />
                    </td>
                    <td style="text-align:right;">
                        <asp:Button ID="btnCerrar" runat ="server"  Text ="Cerrar" CssClass="boton" OnClick="btnCerrar_Click" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </div><br />
        <div id="dvCaja" style="width: 100%;">
            <div id="dvLtsFlujos" runat ="server" style="width:96%; margin:auto;">
                <asp:Repeater ID="rpFlujos" runat="server" OnItemCommand="rpFlujos_ItemCommand" >
                    <HeaderTemplate>
                        <table id="tblFlujos" style="width:100%" class="display" >
                            <thead>
                                <th scope="col">Nombre</th>
                                <th scope="col">Documentos</th>
                                <th scope="col">Etapas</th>
                                <th scope="col">Mesas</th>
                                <th scope="col">Configuracion</th>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Nombre")%></td>
                            <td style="width: 35px; text-align:center">
                                <asp:ImageButton ID="imgbtnDoctos"  runat ="server"  ImageUrl="~/img/file.png" CommandName="Doctos" CommandArgument='<%# Eval("Id")%>' CausesValidation="false"/>
                            </td>
                            <td style="width: 35px; text-align:center">
                                <asp:ImageButton ID="imgbtnPasos"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="pasos" CommandArgument='<%# Eval("Id")%>' CausesValidation="false"/>
                            </td>
                            <td style="width: 35px; text-align:center">
                                <asp:ImageButton ID="imgbtnMesas"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="mesas" CommandArgument='<%# Eval("Id")%>' CausesValidation="false"/>
                            </td>
                            <td style="width: 100px; text-align:center">
                                <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="Ver" CommandArgument='<%# Eval("Id")%>' CausesValidation="false"/>
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
    </fieldset>
</asp:Content>
