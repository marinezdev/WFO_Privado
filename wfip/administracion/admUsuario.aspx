<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admUsuario.aspx.cs" Inherits="wfip.administracion.admUsuario" %>
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
        <legend>ADMINISTRACION DE USUARIOS FTO</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="btnCerrar_Click" />
                </td>
            </tr>
        </table>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table id="tblCapturaDatos" border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                <tr>
                    <td style="width: 25%">Nombre:</td>
                    <td>
                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="95%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txNombre" runat="server" ErrorMessage="*" ControlToValidate="txNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server"
                            TargetControlID="txNombre"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ"></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>Usuario:</td>
                    <td>
                        <asp:TextBox ID="txUsuario" runat="server" MaxLength="32" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txUsuario" runat="server" ErrorMessage="*" ControlToValidate="txUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txUsuario" runat="server"
                            TargetControlID="txUsuario"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.&/#$"></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txClave" runat="server" MaxLength="50" Width="150px" Visible="false" Text="sin_clave"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txClave" runat="server" ErrorMessage="*" ControlToValidate="txClave" ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txClave" runat="server"
                            TargetControlID="txClave"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-_!#$%&/()="></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>Módulo</td>
                    <td>
                        <asp:DropDownList ID="ddlModulo" runat="server" Font-Size="12px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_ddlModulo" runat="server" ErrorMessage="*" ControlToValidate="ddlModulo" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Grupo:</td>
                    <td>
                        <asp:DropDownList ID="ddlGrupo" runat="server" Font-Size="12px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_ddlGrupo" runat="server" ErrorMessage="*" ControlToValidate="ddlGrupo" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>FTO:</td>
                    <td>
                        <asp:DropDownList ID="ddlFto" runat="server" Font-Size="12px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Correo:</td>
                    <td>
                        <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCorreo" ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            TargetControlID="txtCorreo"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-_@"></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td><!--Rol Asignado:--></td>
                    <td><asp:DropDownList ID="ddlRol" runat="server" Visible="false"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
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
                                    <th scope="col">USUARIO</th>
                                    <th scope="col">NOMBRE</th>
                                    <th scope="col">MODULO</th>
                                    <th scope="col">GRUPO</th>
                                    <th scope="col">FTO</th>
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
                            <td><%# Eval("Modulo")%></td>
                            <td><%# Eval("Grupo")%></td>
                            <td><%# Eval("FlujoNombre")%></td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CommandName="activo" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnModificar" runat="server" ImageUrl="~/img/edit.png" CommandName="modificar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnOperacion" runat="server" ImageUrl="~/img/mesa.png" CommandName="operacion" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
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
