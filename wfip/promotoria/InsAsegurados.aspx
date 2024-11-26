<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsAsegurados.aspx.cs" Inherits="wfip.promotoria.Asegurados" MasterPageFile="~/promotoria/promotoria.Master"%>

<%@ Register Assembly="DevExpress.Web.v17.2" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
        <legend>ADMINISTRACION DE ASEGURADOS</legend>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table id="tblCapturaDatos" border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                <tr>
                    <td>Dependencia:</td>
                    <td colspan="3"><asp:DropDownList ID="ddlDependencias" runat="server" Width="430px"></asp:DropDownList></td>
                    <td>Poliza:</td>
                    <td><asp:TextBox ID="txtPoliza" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Apellido Paterno:</td>
                    <td><asp:TextBox ID="txtAPaterno" runat="server"></asp:TextBox></td>
                    <td>Apellido Materno:</td>
                    <td><asp:TextBox ID="txtAMaterno" runat="server"></asp:TextBox></td>
                    <td>Nombres:</td>
                    <td><asp:TextBox ID="txtNombres" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Fecha Nacimiento: (aaaammdd)</td>
                    <td><asp:TextBox ID="txtFNacimiento" runat="server"></asp:TextBox></td>
                    <td>RFC:</td>
                    <td><asp:TextBox ID="txtRFC" runat="server"></asp:TextBox></td>
                    <td>CURP:</td>
                    <td><asp:TextBox ID="txtCURP" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Sexo:</td>
                    <td>
                        <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="H" Text="H"></asp:ListItem>
                        <asp:ListItem Value="M" Text="M"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>Entidad Federativa:</td>
                    <td><asp:DropDownList ID="ddlEntidadFederativa" runat="server" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ddlEntidadFederativa_SelectedIndexChanged"></asp:DropDownList> </td>
                    <td>Municipio:</td>
                    <td><asp:DropDownList ID="ddlMunicipio" runat="server" Visible="false" Width="160px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Nivel Tabular:</td>
                    <td>
                        <asp:DropDownList ID="ddlNivelTabular" runat="server" Width="180px">
                            <asp:ListItem Value="" Text="Seleccionar" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="CS" Text="Conyuge Supérstite"></asp:ListItem>
                            <asp:ListItem Value="G" Text="Presidente de la República y Secretario de Estado"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Subsecretario de Estado"></asp:ListItem>
                            <asp:ListItem Value="I" Text="Oficial Mayor"></asp:ListItem>
                            <asp:ListItem Value="J" Text="Jefe de Unidad"></asp:ListItem>
                            <asp:ListItem Value="K" Text="Director General"></asp:ListItem>
                            <asp:ListItem Value="L" Text="Director General Adjunto"></asp:ListItem>
                            <asp:ListItem Value="M" Text="Director de Área"></asp:ListItem>
                            <asp:ListItem Value="N" Text="Subdirector de Área"></asp:ListItem>
                            <asp:ListItem Value="O" Text="Jefe de Departamento"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Personal de Enlace"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td>Percepción OBM:</td>
                    <td><asp:TextBox ID="txtPercepcion" runat="server"></asp:TextBox></td>
                    <td>Eventual:</td>
                    <td>
                        <asp:RadioButtonList ID="rblEventual" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="SI" Text="Si"></asp:ListItem>
                        <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal" >
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
                                    <th scope="col">Poliza</th>
                                    <th scope="col">Dependencia</th>
                                    <th scope="col">Ap. Paterno</th>
                                    <th scope="col">ap. Materno</th>
                                    <th scope="col">Nombres</th>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Poliza")%></td>
                            <td><%# Eval("Dependencia")%></td>
                            <td><%# Eval("ApellidoPaterno")%></td>
                            <td><%# Eval("ApellidoMaterno")%></td>
                            <td><%# Eval("Nombres")%></td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CommandName="activo" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" />
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnModificar" runat="server" ImageUrl="~/img/edit.png" CommandName="modificar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" ToolTip="Editar Registro" />
                            </td>
                            <td style="width: 30px; text-align: center;">
                                <asp:ImageButton ID="imgBtnOperacion" runat="server" ImageUrl="~/img/mesa.png" CommandName="operacion" CommandArgument='<%# Eval("CURP")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" ToolTip="CoAsegurados" />
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

