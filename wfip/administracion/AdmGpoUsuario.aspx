<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmGpoUsuario.aspx.cs" Inherits="wfip.administracion.AdmGpoUsuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#TblCatalogo').DataTable({
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
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>ADMINISTRACION DE USUARIOS POLIZAS GRUPALES</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table id="tblCapturaDatos" border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                <tr>
                    <td>Nombre:</td>
                    <td>
                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="95%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txNombre" runat="server" ErrorMessage="*" ControlToValidate="txNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxtoolkit:filteredtextboxextender ID="ftb_txNombre" runat="server"
                            TargetControlID="txNombre"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ"></ajaxtoolkit:filteredtextboxextender>
                    </td>
                </tr>
                <tr>
                    <td>Usuario:</td>
                    <td>
                        <asp:TextBox ID="txUsuario" runat="server" MaxLength="32" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txUsuario" runat="server" ErrorMessage="*" ControlToValidate="txUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxtoolkit:filteredtextboxextender ID="ftb_txUsuario" runat="server"
                            TargetControlID="txUsuario"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.&/#$"></ajaxtoolkit:filteredtextboxextender>
                    </td>
                </tr>
                <tr>
                    <td>Clave:</td>
                    <td>
                        <asp:TextBox ID="txClave" runat="server" MaxLength="32" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txClave" runat="server" ErrorMessage="*" ControlToValidate="txClave" ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxtoolkit:filteredtextboxextender ID="ftb_txClave" runat="server"
                            TargetControlID="txClave"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-_!#$%&/()="></ajaxtoolkit:filteredtextboxextender>
                    </td>
                </tr>
                <tr>
                    <td>Grupo:</td>
                    <td>
                        <asp:DropDownList ID="ddlGrupo" runat="server" Font-Size="12px" Width="120px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_ddlGrupo" runat="server" ErrorMessage="*" ControlToValidate="ddlGrupo" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <asp:Button ID="BtnModifica" runat="server" Text="Modificar" CssClass="boton" OnClientClick="return onClickMod();" OnClick="BtnModifica_Click" Visible="False" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnModificaCancela" runat="server" Text="Cancelar" CssClass="boton" OnClientClick="return onClickCancela();" OnClick="BtnModificaCancela_Click" Visible="False" CausesValidation="false" />&nbsp;
                        <asp:Button ID="BtnCrear" runat="server" Text="Guardar" CssClass="boton" OnClientClick="return onClickCrear();" OnClick="BtnCrear_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Panel ID="PnlCatalogo" runat="server" Width="100%">
            <div id="CajaRptCatalogo" style="width: 95%; margin: auto;">
                <asp:Repeater ID="RptCatalogo" runat="server" OnItemCommand="RptCatalogo_ItemCommand" OnItemDataBound="RptCatalogo_ItemDataBound">
                    <HeaderTemplate>
                        <table id="TblCatalogo" style="width: 100%; font-size:10px; font-family:Arial" class="display">
                            <thead>
                                <tr>
                                    <th scope="col">USUARIO</th>
                                    <th scope="col">NOMBRE</th>
                                    <th scope="col">GRUPO</th>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="height:12px">
                            <td><%# Eval("Usuario")%></td>
                            <td><%# Eval("Nombre")%></td>
                            <td><%# Eval("Grupo")%></td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="ImgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CommandName="activo" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="ImgBtnModificar" runat="server" ImageUrl="~/img/edit.png" CommandName="modificar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
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
    <asp:HiddenField ID="Hf_Id" runat="server" />
    <asp:Literal ID="Lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
