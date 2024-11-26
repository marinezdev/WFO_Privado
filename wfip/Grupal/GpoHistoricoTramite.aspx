<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GpoHistoricoTramite.aspx.cs" Inherits="wfip.Grupal.GpoHistoricoTramite" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#TblBitacora').DataTable({
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
    <fieldset>
        <legend>BITACORA</legend>
        <div id="DvBtnCerrar" style="width: 80%; margin: auto; text-align: right;">
            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="BtnCerrar_Click" />
        </div>
        <table id="TblCaptura" style="width: 80%; margin: auto;">
            <tbody>
                <tr>
                    <td style="width: 49%; vertical-align: top">
                        <dx:ASPxRoundPanel ID="PnlAsunto" runat="server" HeaderText="ASUNTO" Theme="Aqua" Width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:TextBox ID="TxAsunto" runat="server" Width="95%" Rows="3" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                        <br />
                        <dx:ASPxRoundPanel ID="PnlDatos" runat="server" HeaderText="DATOS" Theme="Aqua" Width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table id="TblCapturaDatosGenerales" style="width: 100%">
                                        <tr>
                                            <td colspan="4">
                                                <span style="color: #007CC3">GENERALES</span>
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">Razon Social</td>
                                            <td>
                                                <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="80%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RFC</td>
                                            <td>
                                                <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="200px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="TblCapturaDatosDireccion" style="width: 100%">
                                        <tr>
                                            <td colspan="4">
                                                <span style="color: #007CC3">DOMICILIO</span>
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Calle</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txCalle" runat="server" MaxLength="128" Width="295px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>No. Ext.</td>
                                            <td>
                                                <asp:TextBox ID="txNumExt" runat="server" MaxLength="18" Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>No. Int.</td>
                                            <td>
                                                <asp:TextBox ID="txNumInt" runat="server" MaxLength="18" Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>CP</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txCP" runat="server" MaxLength="5" Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Col./Barrio</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="TxColonia" runat="server" Width="290px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Mpio/Del.</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txMpio" runat="server" Width="290px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cd/Pob.</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txCiudad" runat="server" Width="290px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Estado</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txEstado" runat="server" Width="290px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                        <br />
                        <dx:ASPxRoundPanel ID="PnlDocAnexados" runat="server" Width="100%" HeaderText="ANEXOS" Theme="Aqua">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:Repeater ID="RptDocAnexados" runat="server" OnItemCommand="RptDocAnexados_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="TblDocAnexados" style="width: 100%; font-size: 10px;" class="display">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">Descripcion</th>
                                                        <th scope="col"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: White; color: #333333">
                                                <td><%# Eval("Descripcion")%></td>
                                                <td style="width: 35px; text-align: center">
                                                    <asp:ImageButton ID="ImgBtnDescargar" runat="server" ImageUrl="~/img/download.png" CommandName="Descargar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" ToolTip="Descargar" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                    </td>
                    <td style="width: 2%"></td>
                    <td style="width: 49%; vertical-align: top;">
                        <dx:ASPxRoundPanel ID="PnlBitacora" runat="server" HeaderText="BITACORA" Theme="Aqua" Width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:Repeater ID="RptBitacora" runat="server">
                                        <HeaderTemplate>
                                            <table id="TblBitacora" style="width: 100%; font-size:10px;" class="display">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">BUZON</th>
                                                        <th scope="col">INICIO</th>
                                                        <th scope="col">TERMINO</th>
                                                        <th scope="col">OPERADOR</th>
                                                        <th scope="col">OBS. ENTRADA</th>
                                                        <th scope="col">OBS. SALIDA</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: White; color: #333333">
                                                <td><%# Eval("Buzon_Nombre")%></td>
                                                <td><%# Eval("FechaInicio")%></td>
                                                <td><%# Eval("FechaTermina")%></td>
                                                <td><%# Eval("UsuarioAtiende_Nombre")%></td>
                                                <td><%# Eval("ObsEntrada")%></td>
                                                <td><%# Eval("ObsSalida")%></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>


                    </td>
                </tr>
            </tbody>
        </table>
    </fieldset>
    <asp:Literal ID="Lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
