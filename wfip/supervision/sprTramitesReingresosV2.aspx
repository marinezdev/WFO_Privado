<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprTramitesReingresosV2.aspx.cs" Inherits="wfip.supervision.sprTramitesReingresosV2" MasterPageFile="~/supervision/supervision.Master"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>REPORTE DE TRAMITES REINGRESOS</legend> 
        <br />

        <table>
            <tr>
                <td>Fecha Inicio:</td>
                <td>
                    <asp:TextBox ID="txtFechaInicio" runat="server" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaInicio" Format="dd/MM/yyyy" />
                </td>
                <td>Fecha Término:</td>
                <td>
                    <asp:TextBox ID="txtFechaTermino" runat="server" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFechaTermino" Format="dd/MM/yyyy" />
                </td>
                <td>Tipo Trámite:</td>
                <td>
                    <asp:DropDownList ID="DDLTipoTramite" runat="server">
                        <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Emisión GMM"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Emisión Vida"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Emisión Vida CM"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><asp:Button ID="BtnAceptar" runat="server" Text="Buscar" CssClass="boton" OnClick="BtnAceptar_Click" EnableViewState="true" /></td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td valign="top">
                    <asp:GridView ID="GV01" runat="server" CellPadding="7" CellSpacing="5" HeaderStyle-BackColor="#deedf7" HeaderStyle-ForeColor="#2779aa" BorderColor="#aed0ea" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="ProcesosRealizados" HeaderText="Procesos Realizados" />
                            <asp:BoundField DataField="TotaldeReingresos" HeaderText="Total de Reingresos" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td>
                    <asp:Chart ID="Chart1" runat="server" Width="600px" Height="500px" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="4px" OnClick="Chart1_Click">
                        <Titles>
                            <asp:Title Name="Titulo1" Text=""></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend Alignment="Center" IsTextAutoFit="False" Name="Default" ShadowColor="DarkGray" />
                        </Legends>
                        <ChartAreas>
                            <asp:ChartArea Name="GrupoUno" BorderColor="64, 64, 64, 64" > 
                                <AxisX IsReversed="true"><LabelStyle Interval="1" /></AxisX>
                                <AxisY Title="ProcesosRealizados"></AxisY>
                                <AxisY></AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
            </tr>
        </table>

        

        <br />
        
        

        <br />

        <center>
            <asp:GridView ID="GV02" runat="server" CellPadding="15" CellSpacing="10" HeaderStyle-BackColor="#deedf7" HeaderStyle-ForeColor="#2779aa" BorderColor="#aed0ea" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="IdTRamite" HeaderText="IdTrámite" />
                    <asp:BoundField DataField="Mesa" HeaderText="Mesa" />
                    <asp:BoundField DataField="TotalProcesos" HeaderText="Procesos Totales" />
                </Columns>
            </asp:GridView>
        </center>

        <asp:GridView ID="GV03" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" CellPadding="7" CellSpacing="5" HeaderStyle-BackColor="#deedf7" HeaderStyle-ForeColor="#2779aa" BorderColor="#aed0ea">
            <Columns>
                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Envío" />
                <asp:BoundField DataField="FolioCompuesto" HeaderText="Número de Trámite" />
                <asp:BoundField DataField="DatosHtml" HeaderText="Información del Contratante" />
                <asp:BoundField DataField="EstadoNombre" HeaderText="Estado" />
                <asp:BoundField DataField="Fechasolicitud" HeaderText="Fecha Firma de Solicitud" />
                <asp:BoundField DataField="IdsisLegados" HeaderText="Número de Póliza" />
                <asp:BoundField DataField="Prioridad" HeaderText="Identificador" />
                <asp:TemplateField HeaderText="Mostrar">
                    <ItemTemplate>
                        <asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/Folder.png" CommandName='Consultar' CommandArgument='<%# Eval("Id")%>' OnClick="imbtnConsultar_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </fieldset>
</asp:Content>  
