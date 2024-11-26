<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsDependencias.aspx.cs" Inherits="wfip.promotoria.Dependencias" MasterPageFile="~/promotoria/promotoria.Master" %>

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

        function onClickCrear() {
            var resultado = false;
            if (Page_ClientValidate()) {
                if (confirm("Se crea el usuario?")) {
                    fxPintaProcesando();
                    resultado = true;
                }
            }
            return resultado;
        }

        function onClickMod() {
            var resultado = false;
            if (Page_ClientValidate()) {
                if (confirm("Se actualiza el usuario?")) {
                    fxPintaProcesando();
                    resultado = true;
                }
            }
            return resultado;
        }

        function onClickCancela() {
            fxPintaProcesando();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>DEPENDENCIAS</legend>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table id="tblCapturaDatos" border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                <tr>
                    <td>Nombre:</td>
                    <td><asp:TextBox ID="txtNombre" runat="server" Width="760px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Retenenedor:</td>
                    <td><asp:TextBox ID="txtRetenedor" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center;">
                        <asp:Button ID="btnModifica" runat="server" Text="Modificar" CssClass="boton" OnClientClick="return onClickMod();" OnClick="btnModifica_Click" Visible="False" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnModificaCancela" runat="server" Text="Cancelar" CssClass="boton" OnClientClick="return onClickCancela();" OnClick="btnModificaCancela_Click" Visible="False" CausesValidation="false" />&nbsp;
                        <asp:Button ID="btnCrear" runat="server" Text="Guardar" CssClass="boton" OnClientClick="return onClickCrear();" OnClick="btnCrear_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Panel ID="pnlCatalogo" runat="server" Width="100%">
            <div id="cajaRptCatalogo" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                <asp:Repeater ID="rptCatalogo" runat="server" OnItemCommand="rptCatalogo_ItemCommand" OnItemDataBound="rptCatalogo_ItemDataBound">
                    <HeaderTemplate>
                        <table id="tblCatalogo" style="width: 100%" class="display">
                            <thead>
                                <tr>
                                    <th scope="col">Dependenca</th>
                                    <th scope="col">Retenedor</th>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Nombre")%></td>
                            <td><%# Eval("Retenedor")%></td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CommandName="activo" CommandArgument='<%# Eval("IdDependencia")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnModificar" runat="server" ImageUrl="~/img/edit.png" CommandName="modificar" CommandArgument='<%# Eval("IdDependencia")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" ToolTip="Editar Registro" />
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
    <asp:HiddenField ID="hf_Id" runat="server" />
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>

