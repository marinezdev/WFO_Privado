<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="UsuariosAtencion.aspx.cs" Inherits="wfip.supervision.UsuariosAtencion" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.State" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
        <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramitesAtencion').DataTable({
                "paging": false,
                "language": {                    
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "All",
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
                    //"oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
                    "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
                }
            });
            $('#tblTramitesPausa').DataTable({
                "paging": false,
                "language": {                    
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "All",
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
                    //"oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
                    "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="mensajesInformativos" runat="server"></asp:UpdatePanel>
    <fieldset>       
    <legend>MIS TRÁMITES – BÚSQUEDAS</legend>

    <table  style ="width:100%">
        <tr>
            <td style ="width:90%; vertical-align:top; text-align:right" colspan="2">
                <asp:LinkButton ID="btnExportarPausados" runat="server"  CausesValidation="False" OnClick="btnExportarPausados_Click" >
                    <img src="../img/excel.png"/>
                </asp:LinkButton>
                
            </td>
            <td style ="width:10%; vertical-align:top; text-align:right" colspan="2">
                <asp:Button ID="btnRegresar"  CssClass="boton" runat="server" Text="Regresar" OnClick="btnRegresar_Clic" />
            </td>
        </tr>


        <tr>
            <td colspan="3">
                <label>Listado de trámites pausados </label>
                <br />
            </td>
        </tr>
        <tr style="display:none; ">
            <td colspan="3">
                <label>Búsqueda por rangos de fecha de envió </label>
                <br />
                <asp:Label ID="MSresultado2" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
            </td>
        </tr>
        <tr style="display:none;">
            <td>
                <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom"  Width="190" Caption="Desde:">
                    <TimeSectionProperties Adaptive="true">
                        <TimeEditProperties EditFormatString="hh:mm tt" />
                    </TimeSectionProperties>
                    <CalendarProperties>
                        <FastNavProperties DisplayMode="Inline" />
                    </CalendarProperties>
                </dx:ASPxDateEdit>
                <asp:RequiredFieldValidator runat="server" ID="reqFechaDesde" ControlToValidate="CalDesde" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="ConsultaFechas"></asp:RequiredFieldValidator>
            </td>
            <td>
                <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom" Width="190" Caption="Hasta">
                    <TimeSectionProperties Adaptive="true">
                        <TimeEditProperties EditFormatString="hh:mm tt" />
                    </TimeSectionProperties>
                    <CalendarProperties>
                        <FastNavProperties DisplayMode="Inline" />
                    </CalendarProperties>
                </dx:ASPxDateEdit>
                <asp:RequiredFieldValidator runat="server" ID="reqFechaHasta" ControlToValidate="CalHasta" ForeColor="Red" ErrorMessage="*" Font-Size="16px" ValidationGroup="ConsultaFechas"></asp:RequiredFieldValidator>
            </td>
            <td>    
                <div style="vertical-align:bottom">
                    <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Filtrar" OnClick="ConsultaFechasBD" ValidationGroup="ConsultaFechas"/>
                </div>
            </td>
        </tr>

        
        
        <tr>
            <td style ="width:100%; vertical-align:top" colspan="3">
                <asp:Panel ID="pnTrmPausa" runat="server" Width="100%">
                    <div id="dvTrmPausa" style="width: 100%; margin: auto; font-family: Arial;">
                        <asp:Repeater ID="rptTramitesPausa" runat="server" OnItemCommand="rptTramitesPausa_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTramitesPausa" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Folio</th>
                                        <th scope="col">Fecha Envio</th>
                                        <th scope="col">Fecha Firma de Solicitud</th>
                                        <th scope="col">Información de contratante</th>
                                        <th scope="col">Mesa</th>
                                        <th scope="col">Usuario</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col">Observaciones públicas</th>
                                        <th scope="col">Observaciones privadas </th>
                                        <th scope="col">Mostrar</th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("Folio")%></td>
                                    <td><%# Eval("FechaRegistro")%></td>
                                    <td><%# Eval("FechaSolicitud")%></td>
                                    <td><%# Eval("Contratante")%></td>
                                    <td><%# Eval("Mesa")%></td>
                                    <td><%# Eval("Nombre")%></td>
                                    <td><%# Eval("StatusMesa")%></td>
                                    <td><%# Eval("ObservacionPublica")%></td>
                                    <td><%# Eval("ObservacionPrivada")%></td>
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName='Consultar' CommandArgument='<%# Eval("Id")%>' /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel><br /><br />
            </td>
        </tr>

        <tr>
            <td style ="width:100%; vertical-align:top; text-align:right" colspan="3">
                <asp:LinkButton ID="btnExportarAtencion" runat="server"  CausesValidation="False" OnClick="btnExportarAtencion_Click" >
                    <img src="../img/excel.png"/>
                </asp:LinkButton>
            </td>
        </tr>


        <tr>
            <td colspan="3">
                <br />
                <label>Usuarios en operación </label>
                <br />
            </td>
        </tr>
        <tr>
            <td style ="width:100%; vertical-align:top" colspan="3">
                <asp:Panel ID="Panel1" runat="server" Width="100%">
                    <div id="dvTrmAtencion" style="width: 100%; margin: auto; font-family: Arial;">
                        <asp:Repeater ID="rptTramitesAtencion" runat="server" OnItemCommand="rptTramitesAtencion_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTramitesAtencion" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Folio</th>
                                        <th scope="col">Fecha Envio</th>
                                        <th scope="col">Fecha Firma de Solicitud</th>
                                        <th scope="col">Información de contratante</th>
                                        <th scope="col">Mesa</th>
                                        <th scope="col">Usuario</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col">Tiempo de atención</th>
                                        <th scope="col">Mostrar</th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("Folio")%></td>
                                    <td><%# Eval("FechaRegistro")%></td>
                                    <td><%# Eval("FechaSolicitud")%></td>
                                    <td><%# Eval("Contratante")%></td>
                                    <td><%# Eval("Mesa")%></td>
                                    <td><%# Eval("Nombre")%></td>
                                    <td><%# Eval("StatusMesa")%></td>
                                    <td><%# Eval("TiempoAtencion")%></td>
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName='Consultar' CommandArgument='<%# Eval("Id")%>' /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel><br /><br />
            </td>
        </tr>
    </table>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
