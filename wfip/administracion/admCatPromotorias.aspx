<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admCatPromotorias.aspx.cs" Inherits="wfip.administracion.admCatPromotorias" %>
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

        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Desea continuar?'); }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdProm" runat ="server" />
    <div style ="width:80%; margin:0 auto">
        <fieldset>
            <legend>CATALOGO DE PROMOTORIAS</legend>
            <div style ="text-align :right">
                <asp:Button ID="btnCerrar" runat ="server"  Text ="Cerrar" CssClass="boton" OnClick="btnCerrar_Click" CausesValidation="false"/>
            </div><br />
            <div id="dvDatos" runat ="server">
                <table style ="width :80%; margin: 0 auto">
                    <tr>
                        <td style ="width:15%"><b>Nombre:</b></td>
                        <td style ="width:15px"></td>
                        <td>
                            <asp:TextBox  ID="txNombre" runat="server" Width ="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txNombre" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteNombre" runat="server" TargetControlID="txNombre" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>RFC:</b></td>  
                        <td></td>
                        <td>
                            <asp:TextBox ID="txRfc" runat ="server" Width ="50%" MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteRfc" runat="server" TargetControlID="txRfc" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>Clave:</b></td>  
                        <td></td>
                        <td>
                            <asp:TextBox ID="txClave" runat ="server" Width ="50%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txClave" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteClave" runat="server" TargetControlID="txClave" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>Direccion:</b></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID ="txDireccion" runat ="server" Width ="95%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txDireccion" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteDireccion" runat="server" TargetControlID="txDireccion" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                        </td>
                    </tr>
                    <tr>    
                        <td><b>Ciudad:</b></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txCiudad" runat ="server"  MaxLength ="128" Width ="90%" Style="text-transform: uppercase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" ControlToValidate="txCiudad" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteCiudad" runat="server" TargetControlID="txCiudad" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>C.P.</b></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txCp" runat ="server"  MaxLength ="5" Width ="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCp" runat="server" ControlToValidate="txCp" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCp" runat="server" TargetControlID="txCp" FilterMode="ValidChars" ValidChars="0123456789" />    
                        </td>
                    </tr>
                    <tr>
                        <td><b>Correo:</b></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txCorreo" runat ="server"  MaxLength ="64" Width ="90%"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteCorreo" runat="server" TargetControlID="txCorreo" FilterMode="ValidChars" ValidChars="!#$%&*+-./0123456789=?@ABCDEFGHIJKLMNOPQRSTUVWXYZ^_abcdefghijklmnopqrstuvwxyz" />
                            <asp:RequiredFieldValidator ID="rtvCorreo" runat="server" ControlToValidate="txCorreo" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revCorreo" runat="server" 
                                ErrorMessage="*"
                                ControlToValidate="txCorreo"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ForeColor="Red"> </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Teléfono:</b></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txTelefono" runat ="server" MaxLength ="15" Width ="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txTelefono" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" Width ="35px" >EXT:</asp:Label>
                            <asp:TextBox ID="txExtencion" runat ="server"  MaxLength ="6" Width ="100px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteTelefono" runat="server" TargetControlID="txTelefono" FilterMode="ValidChars" ValidChars="0123456789"  />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteExtencion" runat="server" TargetControlID="TxExtencion" FilterType="Numbers"   />
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
            <asp:Panel ID ="pnPromotor" runat ="server" Width="100%">
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
                                <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Id")%>'  CausesValidation="false"  ToolTip ="Editar"/>
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
