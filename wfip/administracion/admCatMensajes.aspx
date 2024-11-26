<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admCatMensajes.aspx.cs" Inherits="wfip.administracion.admCatMensajes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblMensajes').DataTable({
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
    <asp:HiddenField ID ="hdIdMensajes" runat ="server" />
      <div style ="width:80%; margin:0 auto">
    <fieldset>
        <legend>CATALOGO DE MENSAJES</legend>
        <div style ="text-align :right">
                <asp:Button ID="btnCerrar" runat ="server"  Text ="Cerrar" CssClass="boton" OnClick="btnCerrar_Click" CausesValidation="false" />
            </div><br />
          <div id="dvDatos" runat ="server">
                <table style ="width :80%; margin: 0 auto">
                    <tr>
                        <td style ="width:15%"><b>Mensaje:</b></td>
                        <td class="auto-style1"></td>
                        <td>
                            <asp:TextBox  ID="txMensaje" runat="server" Width ="50%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMensaje" runat="server" ControlToValidate="txMensaje" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteMensaje" runat="server" TargetControlID="txMensaje" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>Descripción:</b></td>  
                        <td></td>
                        <td>
                            <asp:TextBox ID="txDescripcion" runat ="server" Width ="50%" MaxLength="60"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteDescripcion" runat="server" TargetControlID="txDescripcion" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
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
        </div>
           <asp:Panel  ID="pnMensajes" runat="server" Height="350px"  ScrollBars ="Auto" >
               <div id="cajaRptMensajes" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                <asp:Repeater ID="rptMensajes" runat="server" OnItemCommand="rptMensajes_ItemCommand" >
                    <HeaderTemplate>
                        <table id="tblMensajes" style="width:100%" class="display" >
                            <thead>
                                <th scope="col">Mensaje</th>
                                <th scope="col">Descripcion</th>
                                <th scope="col">&nbsp</th>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Mensaje")%></td>
                            <td style="width: 550px"><%# Eval("Descripcion")%></td 
                            </td>
                             <td style="width: 35px; text-align:center">
                                <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Id_Control")%>'  CausesValidation="false"  ToolTip ="Editar"/>
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