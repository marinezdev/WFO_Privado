<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admDoctosFlujo.aspx.cs" Inherits="wfip.administracion.admDoctosFlujo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramites').DataTable({
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
            resultado = confirm('¿Desea continuar?'); 
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdFlujo"  runat ="server"/>
    <asp:HiddenField ID="hdIdTramite"  runat ="server"/>
    <fieldset>
        <legend><asp:Label ID="lbFlujo" runat ="server" Font-Size="13px"></asp:Label></legend>
        <asp:MultiView ID="mvContenedor" runat ="server" ActiveViewIndex="0" >
            <asp:View ID="vwTramites" runat ="server">
                <div style ="text-align :right">
                    <asp:Button ID="btnCerrar" runat ="server"  Text ="Cerrar" CssClass="boton" OnClick="btnCerrar_Click" />
                </div><br />
                <div id="dvTramites" runat ="server" style ="width:60%; margin: 0 auto " >
                    <asp:Repeater ID="rpTramites" runat="server" OnItemCommand="rpTramites_ItemCommand" >
                        <HeaderTemplate>
                            <table id="tblTramites" class="display" >
                                <thead>
                                    <th scope="col">TRAMITE</th>
                                    <th scope="col"></th>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White; color: #333333">
                                <td><asp:Label ID="lbTramite" runat ="server" Text ='<%# Eval("Nombre")%>' ></asp:Label></td>
                                <td style="width: 35px; text-align:center">
                                    <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="VerDoctos" CommandArgument='<%# Eval("IdTipoTramite")%>'  />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </asp:View>
            <asp:View ID="vwDoctos" runat ="server">
                <div style ="text-align :right">
                    <asp:Button ID="btnAceptar" runat ="server"  Text ="Aceptar" CssClass="boton"   OnClientClick ="return Confirmar();" OnClick="btnAceptar_Click"/>&nbsp;&nbsp;
                    <asp:Button ID="btnRegresar" runat ="server"  Text ="Regresar" CssClass="boton" OnClick="btnRegresar_Click" />
                </div><br />
                <div style="font-size: 14px; font-weight: bold; color: #007CC3; width:90%; margin:0 auto">
                    <b><asp:Label ID="lbTipoTramite" runat ="server"></asp:Label> </b>
                </div><br />
                <asp:UpdatePanel ID ="upDoctos" runat ="server" >
                   <ContentTemplate>
                       <table style ="width:80%; margin:0 auto">
                           <tr>
                                <td >DISPONIBLES</td>
                                <td style ="width :110px"></td>
                                <td >AGREGADOS</td>
                            </tr>
                            <tr style ="vertical-align :top">
                                <td>
                                    <asp:ListBox  Width="450px" ID="lstDisponibles" runat="server" Rows="20"  DataValueField="IdTipoDocto" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                                <td >
                                    <asp:Button Width="110px" runat="server" ID="btnAgregar" Text="Agregar" CssClass="boton" OnClick="btnAgregar_Click" /><br /><br />
                                    <asp:Button Width="110px" runat="server" ID="btnAgegarTodos" Text="Aregar todos" CssClass="boton" OnClick="btnAgegarTodos_Click"  /><br /><br />
                                    <asp:Button Width="110px" runat="server" ID="btnRemove" Text="Remover"   CssClass="boton" OnClick="btnRemove_Click" /><br /><br />
                                    <asp:Button Width="110px" runat="server" ID="btnRemoveTodo" Text="Remover todos" CssClass="boton" OnClick="btnRemoveTodo_Click"  />
                                </td>
                                <td style ="vertical-align :top">
                                    <asp:ListBox  Width="450px" ID="lstAgregados" runat="server" Rows="20" DataValueField="IdTipoDocto" SelectionMode="Multiple"></asp:ListBox></td>
                            </tr>
                        </table>
                    </ContentTemplate>
               </asp:UpdatePanel>
            </asp:View>
        </asp:MultiView>
        
    </fieldset>
    
</asp:Content>
