<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprAlmacenamientoTramites.aspx.cs" Inherits="wfip.supervision.sprAlmacenamientoTramites" MasterPageFile="~/supervision/supervision.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">

    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTramitesEspera').DataTable({
                "paging": true,
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
                    "oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
                    "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
                }
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <fieldset>
        <legend>REPORTE DE ALMACENAMIENTO DE TRAMITES</legend> 
        <br />
        <asp:GridView ID="GVPromedio" runat="server" AutoGenerateColumns="false" CellPadding="15" CellSpacing="10" HeaderStyle-BackColor="#deedf7" HeaderStyle-ForeColor="#2779aa" BorderColor="#aed0ea">
            <Columns>
                <asp:HyperLinkField DataTextField="A" HeaderText="0 a 30" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=1" DataNavigateUrlFields="A" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#33cc33" ItemStyle-ForeColor="White" />
                <asp:HyperLinkField DataTextField="B" HeaderText="31 a 60" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=2" DataNavigateUrlFields="B" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#33cc33" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="C" HeaderText="61 a 90" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=3" DataNavigateUrlFields="C" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="D" HeaderText="91 a 120" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=4" DataNavigateUrlFields="D" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="E" HeaderText="121 a 150" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=5" DataNavigateUrlFields="E" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="F" HeaderText="151 a 180" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=6" DataNavigateUrlFields="F" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="G" HeaderText="181 a 210" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=7" DataNavigateUrlFields="G" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="H" HeaderText="211 a 240" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=8" DataNavigateUrlFields="H" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="I" HeaderText="241 a 270" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=9" DataNavigateUrlFields="I" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="J" HeaderText="271 a 300" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=10" DataNavigateUrlFields="J" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="K" HeaderText="301 a 330" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=11" DataNavigateUrlFields="K" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="L" HeaderText="331 a 360" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=12" DataNavigateUrlFields="L" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:HyperLinkField DataTextField="M" HeaderText="más de 361" DataNavigateUrlFormatString="sprAlmacenamientoTramites.aspx?a=13" DataNavigateUrlFields="M" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White"/>
                <asp:BoundField DataField="Acumulado" HeaderText="Acumulado" ItemStyle-BackColor="#ff6600" ItemStyle-ForeColor="White" />
            </Columns>
        </asp:GridView>
        <br /><br />

        <asp:Chart ID="Chart1" runat="server" BackGradientStyle="LeftRight" Height="350px" Palette="None" Width="1100px">   
            <Legends>
                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="true" Name="Default" LegendStyle="Row" AutoFitMinFontSize="5"></asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Name="Titulo1" Text="Almacenamiento de Trámites"></asp:Title>
            </Titles>
            <Series>  
                <asp:Series Name="Series1" Color="Red" BorderWidth="5" ChartArea="ChartArea1" IsValueShownAsLabel="true" MarkerSize="2" Font="Arial"></asp:Series>  
            </Series>  
            <ChartAreas>  
                <asp:ChartArea Name="ChartArea1">
                    <AxisX></AxisX>
                    <AxisY Title="Trámites"></AxisY>
                </asp:ChartArea>  
            </ChartAreas>
                    
            <BorderSkin BackColor=""  />  
        </asp:Chart>
        <asp:GridView ID="GVDetalle" runat="server" CellPadding="5" CellSpacing="5"></asp:GridView>

        <asp:Panel ID="pnTrmPausa" runat="server" Width="100%">
            <div id="dvTrmPausa" style="width: 100%; margin: auto; font-family: Arial;">

                <asp:Repeater ID="rptTramitesPromedio" runat="server" OnItemCommand="rptTramitesEspera_ItemCommand">
                    <HeaderTemplate>
                        <table id="tblTramitesEspera" style="width:100%" class="display" >
                            <thead>
                                <th scope="col">Fecha envío</th>
                                <th scope="col">Número de trámite</th>
                                <th scope="col">Información de contratante</th>
                                <th scope="col">Estado</th>
                                <th scope="col">Fecha Firma de Solicitud</th>
                                <th scope="col">Número Póliza</th>
                                <th scope="col">Identificador</th>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("FechaRegistro")%></td>
                            <td><%# Eval("FolioCompuesto")%></td>
                            <td><%# Eval("DatosHtml")%></td>
                            <td><%# Eval("EstadoNombre")%></td>
                            <td><%# Eval("FechaSolicitud")%></td>
                            <td><%# Eval("IdSisLegados")%></td>
                            <td><%# Eval("Prioridad")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>

            </div>
        </asp:Panel>




    </fieldset>
</asp:Content>  