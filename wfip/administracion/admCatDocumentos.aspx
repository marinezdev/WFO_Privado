<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admCatDocumentos.aspx.cs" Inherits="wfip.administracion.admCatDocumentos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblDoctos').DataTable({
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

        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Desea continuar?'); }
            return resultado;
        }
    </script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdTipoDocto" runat ="server" />
    <div style ="width:80%; margin:0 auto">
        <fieldset>
            <legend>CATALOGO DE DOCUMENTOS</legend>
            <div style ="text-align :right">
                <asp:Button ID="btnCerrar" runat ="server"  Text ="Cerrar" CssClass="boton" OnClick="btnCerrar_Click" CausesValidation="false" />
            </div><br />
            <div id="dvDatos" runat ="server">
                <table style ="width :80%; margin: 0 auto">
                    <tr>
                        <td style ="width:10%"><b>Nombre:</b></td>
                        <td style ="width:7px"></td>
                        <td>
                            <asp:TextBox  ID="txNombre" runat="server" Width ="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txNombre" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteNombre" runat="server" TargetControlID="txNombre" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="3" style ="height :25px;color:red;text-align :center">
                            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:right;">
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="boton" OnClientClick="return Confirmar();" OnClick="btnModificar_Click" Visible="False" />&nbsp;&nbsp
                            <asp:Button ID="btnModCancela" runat="server" Text="Cancelar" CssClass="boton" OnClick="btnModCancela_Click" Visible="False" CausesValidation ="false" />&nbsp;&nbsp
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="boton" OnClientClick="return Confirmar();"  OnClick="btnGuardar_Click" />
                        </td>
                    </tr>
                </table>
            </div><br />
            <asp:Panel  ID="pnDoctos" runat="server" Width="100%">
                <asp:Repeater ID="rptDoctos" runat="server" OnItemCommand="rptDoctos_ItemCommand" >
                    <HeaderTemplate>
                        <table id="tblDoctos" style="width:100%" class="display" >
                            <thead>
                                <th scope="col">Nombre</th>
                                <th scope="col"></th>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Nombre")%></td>
                            <td style="width: 35px; text-align:center">
                                <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("IdTipoDocto")%>'  CausesValidation="false"  ToolTip ="Editar"/>
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
