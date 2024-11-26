<%@ Page Title="" Language="C#" MasterPageFile="~/operacion/operacion.Master" AutoEventWireup="true" CodeBehind="listaTramite.aspx.cs" Inherits="wfip.operacion.listaTramite" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTrmProceso').DataTable({
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

            $('#tblCitasMedicas').DataTable({
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

            $('#tblTrmPausa').DataTable({
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

            $('#tblTrmCancelado').DataTable({
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <div style="width:100%">
        <div style="width: 90%; margin:auto;">
            <fieldset>
                <legend>TRAMITES</legend><br />
                <asp:Panel ID="pnTrmPausa" runat="server" Width="100%">
                    <h4 style ="color:#187BB4">EN PAUSA</h4>
                    <div id="dvTrmPausa" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                        <asp:Repeater ID="rptTrmPausa" runat="server" OnItemCommand="rptTrmPausa_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTrmPausa" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Trámite</th>
                                        <th scope="col">Tipo de trámite</th>
                                        <th scope="col">Información del contratante</th>
                                        <th scope="col">Mesa</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col">Fecha envío</th>
                                        <th scope="col">&nbsp;</th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("FolioCompuesto")%></td>
                                    <td><%# Eval("Flujo")%> - <%# Eval("TramiteNombre")%></td>
                                    <td><%# Eval("DatosHtml")%></td>
                                    <td><%# Eval("MesaNombre")%></td>
                                    <td><%# Eval("EstadoNombre")%></td>
                                    <td><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName='<%# Eval("IdMesa")%>' CommandArgument='<%# Eval("IdTramite")%>' /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel><br /><br />
                <div id="dvTrmProceso" style="width: 100%;">
                    <h4  style ="color:#187BB4">EN PROCESO</h4>
                    <div style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                        <asp:Repeater ID="rpTrmProceso" runat="server" OnItemCommand="rpTrmProceso_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTrmProceso" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Trámite</th>
                                        <th scope="col">Tipo de trámite</th>
                                        <th scope="col">Información del contratante</th>
                                        <th scope="col">Mesa</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col">Fecha envío</th>
                                        <th scope="col">&nbsp;</th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("FolioCompuesto")%></td>
                                    <td><%# Eval("Flujo")%> - <%# Eval("TramiteNombre")%></td>
                                    <td><%# Eval("DatosHtml")%></td>
                                    <td><%# Eval("MesaNombre")%></td>
                                    <td><%# Eval("EstadoNombre")%></td>
                                    <td><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnAbrir" runat="server" ImageUrl="~/img/Folder.png" CommandName='<%# Eval("IdMesa")%>' CommandArgument='<%# Eval("IdTramite")%>' /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div><br /><br />
                <div id="dvTrmCitasMedicas" style="width: 100%;">
                    <h4  style ="color:#187BB4">CITAS MEDICAS</h4>
                    <div style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                        <asp:Repeater ID="rpCitaMedica" runat="server" OnItemCommand="rpCitaMedica_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblCitasMedicas" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Trámite</th>
                                        <th scope="col">Tipo de trámite</th>
                                        <th scope="col">Información del contratante</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col">Fecha envío</th>
                                        <th scope="col">&nbsp;</th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("FolioCompuesto")%></td>
                                    <td><%# Eval("Nombre")%></td>
                                    <td><%# Eval("DatosHtml")%></td>
                                    <td><%# Eval("EstadoNombre")%></td>
                                    <td><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName='<%# Eval("Id")%>' CommandArgument='<%# Eval("Id")%>' /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div><br /><br />

                <div id="dvTrmCancelados" style="width: 100%;">
                    <h4  style ="color:#187BB4">CANCELADOS</h4>
                    <div style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                        <asp:Repeater ID="rpTrmCancelado" runat="server" OnItemCommand="rptTramitesCancelado_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTrmCancelado" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Trámite</th>
                                        <th scope="col">Tipo de trámite</th>
                                        <th scope="col">Información del contratante</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col">Fecha envío</th>
                                        <th scope="col">&nbsp;</th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("FolioCompuesto")%></td>
                                    <td><%# Eval("Nombre")%></td>
                                    <td><%# Eval("DatosHtml")%></td>
                                    <td>CANCELADO</td>
                                    <td><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName='<%# Eval("Id")%>' CommandArgument='<%# Eval("Id")%>' /></td>
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
        </div>
    </div>
</asp:Content>
