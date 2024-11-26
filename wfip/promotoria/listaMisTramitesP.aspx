<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="listaMisTramitesP.aspx.cs" Inherits="wfip.promotoria.listaMisTramitesP" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramite').DataTable({
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
                <legend >MIS TRÁMITES <asp:Label ID="Label2" runat="server"></asp:Label> </legend>
                <br />
                <table style="width:100%">
                        <tr>
                            <td style="text-align:right;width:100%">
                               <asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click">
                                   <img src="../img/excel.png"/>
                               </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                <br /><br />
                <div id="dvCajaTramite" style="width: 100%;">
                    <div id="cajaRptTramite" style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                        <asp:Repeater ID="rptTramite" runat="server" OnItemDataBound="rptTramite_ItemDataBound" OnItemCommand="rptTramite_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTramite" style="width:100%" class="display" >
                                    <thead>
                                        <th scope="col">Fecha envío</th>
                                        <th scope="col">Número de trámite</th>
                                        <th scope="col">Orden de Trabajo</th>
                                        <th scope="col">Operación</th>
                                        <th scope="col">Producto</th>
                                        <th scope="col">Información del Contratante</th>
                                        <th scope="col">Fecha Firma de Solicitud </th>
                                        <th scope="col">Estado</th>
                                        <th scope="col">Número De Póliza De Los Sistemas Legados</th>
                                        <th scope="col">KWIK</th>
                                        <!-- <th scope="col"></th>-->
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td style="text-align: center;"><%# Eval("FechaRegistro","{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                    <td><%#Eval("FolioCompuesto")%></td>
                                    <td><%#Eval("NumeroOrden")%></td>
                                    <td><%#Eval("Flujo")%></td>
                                    <td><%#Eval("Producto")%></td>
                                    <td><%#Eval("DatosHtml")%></td>
                                    <td style="text-align: center;"><%# Eval("FechaSolicitud","{0:dd/MM/yyyy }")%></td>
                                    <td style="width: 40px; text-align:left; vertical-align:middle"><%# Eval("EstadoNombre")%></td>
                                    <td><%#Eval("IdSisLegados")%></td>
                                    <td><%#Eval("kwik")%></td>
                                    <!-- <td style="width: 20px; text-align:center"><asp:Image ID="imgEstado" runat="server" ImageUrl="~/img/bolaGris.png"  /></td>-->
                                    <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName ="Consultar" CommandArgument='<%# Eval("Id")%>' /></td>
                                    <!--<td style="width: 20px; text-align:center"><asp:ImageButton ID="imgBtnEliminar" runat="server" ImageUrl="~/img/eliminar.png" CommandName ="Eliminar" CommandArgument='<%# Eval("Id")%>'  OnClientClick="return confirm('¿Seguro de que desea realizazr la cancelación del Trámite?');" /></td>-->
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <dx:ASPxGridView ID="dvgdListaTramites" ClientInstanceName="dvgdListaTramites" runat="server" AutoGenerateColumns ="False" Width ="100%" 
                             style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px" Visible="false">
                           <Styles Header-Wrap="True" />
                           <Columns>
                                <dx:GridViewDataTextColumn Caption= "FECHA ENVÍO" FieldName="FechaRegistro" VisibleIndex="0" />
                                <dx:GridViewDataTextColumn Caption= "NUM TRÁMITE" FieldName="FolioCompuesto" VisibleIndex="1" />
                                <dx:GridViewDataTextColumn Caption= "ORDEN DE TRABAJO" FieldName="NumeroOrden" VisibleIndex="2" />
                                <dx:GridViewDataTextColumn Caption ="OPERACIÓN"  FieldName="Flujo"  VisibleIndex="3" />
                                <dx:GridViewDataTextColumn Caption ="TIPO"  FieldName="Producto"  VisibleIndex="4" />
                                <dx:GridViewDataTextColumn Caption ="INFORMACIÓN CONTRATANTE"  FieldName="DatosHtml"  VisibleIndex="5" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn Caption ="FECHA FIRMA DE SOLICITUD"  FieldName="FechaSolicitud"  VisibleIndex="6" />
                                <dx:GridViewDataTextColumn Caption ="ESTADO"  FieldName="EstadoNombre"  VisibleIndex="7" />
                                <dx:GridViewDataTextColumn Caption ="NUM PÓLIZA SIS LEGADOS"  FieldName="IdSisLegados"  VisibleIndex="8" />
                                <dx:GridViewDataTextColumn Caption ="KWIK"  FieldName="kwik"  VisibleIndex="9" />
                            </Columns>
                            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                            <SettingsPager  Mode="ShowAllRecords"/>
                            <Settings  VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                            <SettingsSearchPanel Visible="true" />
                        </dx:ASPxGridView>                    
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdListaTramites"></dx:ASPxGridViewExporter>

                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
