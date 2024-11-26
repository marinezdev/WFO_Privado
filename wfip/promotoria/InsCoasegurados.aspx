<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsCoasegurados.aspx.cs" Inherits="wfip.promotoria.Coasegurados" MasterPageFile="~/promotoria/promotoria.Master" %>


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
        <legend>CO-ASEGURADOS</legend>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table id="tblCapturaDatos" border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                <tr>
                    <td>Apellido Paterno:</td>
                    <td><asp:TextBox ID="txtAPaterno" runat="server"></asp:TextBox></td>
                    <td>Apellido Materno:</td>
                    <td><asp:TextBox ID="txtAMaterno" runat="server"></asp:TextBox></td>
                    <td>Nombres:</td>
                    <td><asp:TextBox ID="txtNombres" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Fecha Nacimiento:</td>
                    <td><asp:TextBox ID="txtFNacimiento" runat="server"></asp:TextBox></td>
                    <td>CURP:</td>
                    <td><asp:TextBox ID="txtCURP" runat="server"></asp:TextBox></td>
                    <td>Sexo:</td>
                    <td>
                        <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="H" Text="H"></asp:ListItem>
                        <asp:ListItem Value="M" Text="M"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Fecha Afiliación:</td>
                    <td><asp:TextBox ID="txtFAfiliacion" runat="server"></asp:TextBox></td>
                    <td>Tipo:</td>
                    <td>
                        <asp:DropDownList ID="ddlTipoAsegurado" runat="server">
                            <asp:ListItem Text="Seleccione"></asp:ListItem>
                            <asp:ListItem Value="T" Text="Titular"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Cónyuge"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Concubina/Concubino"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Pareja del mismo sexo"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Hijo"></asp:ListItem>
                            <asp:ListItem Value="A" Text="Ascendiente"></asp:ListItem>
                            <asp:ListItem Value="CS" Text="Cónyuge Supérsite"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td>Ingreso a la colectividad:</td>
                    <td><asp:TextBox ID="txtFIColectividad" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                        </asp:RadioButtonList>  

                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center;">
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
                                    <th scope="col">Titular</th>
                                    <th scope="col">Ap. Paterno</th>
                                    <th scope="col">Ap. Materno</th>
                                    <th scope="col">Nombres</th>
                                    <th scope="col">F. Nacimiento</th>
                                    <th scope="col">Tipo</th>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("NombreTitular")%></td>
                            <td><%# Eval("ApellidoPaterno")%></td>
                            <td><%# Eval("ApellidoMaterno")%></td>
                            <td><%# Eval("Nombres")%></td>
                            <td align="center"><%# Eval("FechaNacimiento")%></td>
                            <td align="center"><%# Eval("Tipo")%></td>
                            <td style="width: 30px; text-align: center;">
                                <%--<asp:ImageButton ID="imgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CausesValidation="false"  />--%>
                                <asp:Image ID="imgActivo" runat="server" ImageUrl="~/img/activo.png" />
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnModificar" runat="server" ImageUrl="~/img/edit.png" CommandName="modificar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" ToolTip="Editar Registro" />
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

