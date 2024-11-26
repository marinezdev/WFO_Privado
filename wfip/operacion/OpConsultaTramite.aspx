<%@ Page Title="" Language="C#" MasterPageFile="~/operacion/operacion.Master" AutoEventWireup="true" CodeBehind="OpConsultaTramite.aspx.cs" Inherits="wfip.operacion.OpConsultaTramite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblTrmHistorial').DataTable({
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>INFORMACIÓN DEL TRÁMITE <asp:Label ID="Label2" runat="server"></asp:Label></legend>
        
        <asp:HiddenField ID="hdIdTramite" runat="server" />

            <div style="width: 90%; margin: auto">
                <table id="tblDatos" style="width: 100%;">
                    <tr>     
                        <td style="width:60%; vertical-align: top; font-size:14px; "; >
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3"><asp:Literal ID="ltInfTipoTramite"  runat="server"></asp:Literal></span>
                            <hr />
                            <asp:UpdatePanel id="DatosTramiteInformacion" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table border="0" style="width: 100%;">
                                        <tr>
                                            <td colspan="3" style="align-content:center;">
                                                  <asp:Label runat="server" ID="lblAdvertencia" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53; border: 1px solid #8EBB53;">
                                                <asp:Label ID="Label55" runat="server" Font-Names="Britannic Bold" Font-Size="12px" Font-Bold="true"> Fecha de Registro: </asp:Label>
                                            </td>
                                            <td colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53; border: 1px solid #8EBB53;">
                                                <asp:Label ID="InfoFechaRegistro" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="TramiteInformacionCPDES" runat="server" Visible="false" >
                                            <tr>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Folio CPDES</td>
                                                <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Estatus CPDES</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoFolioCPDES" runat="server" ></asp:Label>
                                                </td>
                                                <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                    <asp:Label ID="InfoEstatusCPDES" runat="server" ></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                            <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                       
                                            </td>
                                        </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td style="background-color:#1572B7; color:#F0F0F0; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Moneda
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoMoneda" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#1572B7; color:#F0F0F0; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Suma Asegurada Básica
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoSumaAseguradaBasica" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="SumaAseguradaPólizasVigentes" runat="server" Visible="false" >
                                        <tr>
                                            <td style="background-color:#1572B7; color:#F0F0F0; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Suma Asegurada de Pólizas Vigentes 
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoSumaAseguradaPolizasVigentes" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                            </td>
                                        </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td style="background-color:#1572B7; color:#F0F0F0; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">Prima Total de Acuerdo a Cotización
                                            </td>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoPrimaTotal" runat="server" Font-Names="Britannic Bold" Font-Size="12px"  Visible="True" Font-Bold="true" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                Hombre Clave
                                            </td>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoHobreClave" Text="NO" runat="server" ></asp:Label>
                                            </td>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                <asp:Label ID="InfoPrioridad" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"> 
                                                INFORMACIÓN DE PÓLIZA
                                            </td>
                                        </tr>
                                        <tr>
                                                    <td style="width: 38%; background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Clave Promotoria
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Región
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Gerente Comercial 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoClave" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoRegion" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoGerente" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Ejecutivo Comercial 
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Ejecutivo Front 
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Solicitud / Número de orden 
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoEjecutivo" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoEjecutivoFront" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoNumero" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Fecha solicitud 
                                                    </td>
                                                    <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        Tipo de contratante 
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoFechaSolicitud" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        <asp:Label ID="InfoContratante" runat="server" ></asp:Label>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        INFORMACIÓN CONTRATANTE 
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="InfoPrsFisica" runat="server" Visible="true" >
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nombre(s) 
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Apellido Paterno
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Apellido Materno
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFNombre" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFApellidoP" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFApellidoM" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Sexo
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            RFC
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nacionalidad
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFSexo" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoFRFC" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoFNacionalidad" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Fecha Nacimiento
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoFFechaNa" runat="server" ></asp:Label>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="InfoPrsMoral" runat="server" Visible="true" >
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nombre
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Fecha de Constitución
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            RFC
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoMNombre" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoMFechaConsti" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoMRFC" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="InfoDiContratante" runat="server" Visible="false" >
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                        ¿El solicitante es el <br />mismo que el contratante?
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                           <asp:Label ID="InfoFContratante" runat="server" ></asp:Label>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="color:#244f02; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            INFORMACIÓN TITULAR 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nombre(s)
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Apellido Paterno
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Apellido Materno
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTNombre" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTApellidoP" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTApellidoM" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Nacionalidad
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Sexo
                                                        </td>
                                                        <td style="background-color:#1572B7; color:white; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            Fecha Nacimiento
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTNacionalidad" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTSexo" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="background-color:#ddd; color:black; font-size:smaller; text-align:center; font-family:'Arial Rounded MT'">
                                                            <asp:Label ID="InfoTNacimiento" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                    </table>
                                    <asp:Panel ID="TablaBeneficiarios" runat="server" Visible="false">
                                        <table style="width: 100%;" border="0">
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">Tabla Beneficiarios </span>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rptRiesgosInfo" runat="server" >
                                                            <HeaderTemplate>
                                                                <table id="rptRiesgosTabla" style="width:100%; font-size:9px" class="display" >
                                                                    <thead>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ramo</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">ID</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Vigencia</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Parentesco</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Zona</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Plan</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Factor</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Riesgo Ocupacional</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Extraprima Ocupacional</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Endosos</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ocupación</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Estatus</th>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="background-color: White; color: #333333; text-align:center">
                                                                    <td><%#Eval("[Ramo]")%></td>
                                                                    <td><%#Eval("[ID]")%></td>
                                                                    <td><%#Eval("[Vigencia]")%></td>
                                                                    <td><%#Eval("[Parentesco]")%></td>
                                                                    <td><%#Eval("[Zona]")%></td>
                                                                    <td><%#Eval("[Plan]")%></td>
                                                                    <td><%#Eval("[Factor]")%></td>
                                                                    <td><%#Eval("[Riesgo]")%></td>
                                                                    <td><%#Eval("[RiesgoFactor]")%></td>
                                                                    <td><%#Eval("[Endosos]")%></td>
                                                                    <td><%#Eval("[Ocupacion]")%></td>
                                                                    <td><%#Eval("[Estatus]")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rptRiesgosInfoVida" runat="server" >
                                                            <HeaderTemplate>
                                                                <table id="rptRiesgosTablaVida" style="width:100%; font-size:9px" class="display" >
                                                                    <thead>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Producto</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">SubProducto</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Vigencia</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">ExtraPrima</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Habito</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Parentesco</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Endosos</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Ocupación</th>
                                                                        <th scope="col" style="background-color:#1572B7; color:white;">Estatus</th>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style="background-color: White; color: #333333; text-align:center">
                                                                    <td><%#Eval("[Producto]")%></td>
                                                                    <td><%#Eval("[SubProducto]")%></td>
                                                                    <td><%#Eval("[Vigencia]")%></td>
                                                                    <td><%#Eval("[ExtraPrima]")%></td>
                                                                    <td><%#Eval("[Habito]")%></td>
                                                                    <td><%#Eval("[Parentesco]")%></td>
                                                                    <td><%#Eval("[Endosos]")%></td>
                                                                    <td><%#Eval("[Ocupacion]")%></td>
                                                                    <td><%#Eval("[Estatus]")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="vertical-align: top" >
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align:center; border-bottom: 1px solid #ddd; background-color:#8EBB53;">
                                        <asp:Literal ID="ltInfFolio" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #ddd; background-color:#F7F7F7;">
                                        <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #ddd;">
                                        <asp:Literal ID="ltInfProducto" runat="server"></asp:Literal>
                                        <asp:Repeater ID="rptTramite" runat="server" >
                                            <HeaderTemplate>
                                                <table id="tblTramite" style="width:100%" class="display" >
                                                    <thead>
                                                        <th scope="col" style="background-color:#1572B7; color:white;">Producto</th>
                                                        <th scope="col" style="background-color:#1572B7; color:white;">Plan</th>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="background-color: White; color: #333333; text-align:center">
                                                    <td><%#Eval("[PRODUCTO]")%></td>
                                                    <td><%#Eval("[SUBPRODUCTO]")%></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #ddd;">
                                        <asp:Literal ID="lblStatusMesas" runat="server"></asp:Literal>
                                    </td>
                                </tr>

                                <tr><td>&nbsp;</td></tr>
                                <tr><td>&nbsp;</td></tr>
                                <tr><td>&nbsp;</td></tr>

                                <tr>
                                    <td colspan ="4" style="text-align:right;">
                                        <asp:Literal ID="ltStatusTramite" runat="server"></asp:Literal>
                                        <br /><asp:Button ID="btnCarta" runat="server" Text="   Visualizar Carta   " CssClass="boton" CausesValidation="False" Visible="false" OnClick="btnGeneraCarta_Click"/>
                                        
                                        <!--
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="background-color:#1572B7; color:white;">Status del Trámite:</td>
                                                <td style="background-color: White; color: #333333; text-align:center; vertical-align:middle;">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                        -->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                

                <br />
                <table style="width:100%;">
                    <tr>
                        <td style="text-align:right">
                            <asp:Button ID="btnCitasMedicas" runat="server" CssClass="boton" OnClick="BtnCitaMedica" Text="Carta Pase" Visible="false"/>
                            <br />&nbsp;
                            <br />&nbsp;
                            <asp:Button ID="btnUpResultados" runat="server" CssClass="boton" OnClick="btnUpResultados_Click" Text="Subir Resultados" Visible="false"/>
                        </td>
                    </tr>
                </table>
                <br/>

                <asp:Panel ID="pnTrmHistorial" runat="server" Width="100%">
                    <table style="width: 100%; border-bottom: 2px solid gray">
                        <tr>
                            <td style="width: 80%"><h4 style ="color:#187BB4">HISTORIAL TRÁMITE</h4></td>
                            <td>

                                <asp:UpdatePanel ID="UPAbrirPdf" runat="server">
                                    <ContentTemplate>

                                        <asp:Button ID="BtnVerPDF" runat="server" Text="Ver Archivo" CssClass="boton" OnClick="BtnVerPDF_Click" />

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="BtnVerPDF" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                    </table>
                        <asp:Repeater ID="rptTrmHistorial" runat="server" OnItemCommand="rptTrmHistorial_ItemCommand">
                            <HeaderTemplate>
                                <table id="tblTrmHistorial" style="width:100%" class="display" >
                                    <thead>
                                        <th style="display:none;" scope="col">#</th>
                                        <th scope="col">USUARIO</th>
                                        <th scope="col">MESA</th>
                                        <th scope="col">ESTADO</th>
                                        <th scope="col">OBSERVACIONES </th>
                                        <th scope="col">OBSERVACIONES PRIVADAS</th>
                                        <th scope="col">FECHA INICIO </th>
                                        <th scope="col">FECHA TÉRMINO</th>
                                        <th scope="col">TIEMPO </th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td style="display:none;"><%# Eval("Id")%></td>
                                    <td><%# Eval("usuario")%></td>
                                    <td><%# Eval("MesaNombre")%></td>
                                    <td><%# Eval("Nombre")%></td>
                                    <td><%# Eval("Observacion")%></td>
                                    <td><%# Eval("ObservacionPrivada")%></td>
                                    <td><%# Eval("FechaInicio")%></td>
                                    <td><%# Eval("FechaTermino")%></td>
                                    <td><%# Eval("Tiempo")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                </asp:Panel>
                
                <asp:UpdatePanel ID="upPnlCaptura" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PnlPdf" runat="server" Visible=" false" style="width:80%; z-index: 200; position: absolute;top: 35%; left: 100px; border: 2px solid black; background-color: white;">
                            <table id="tblDoctos" style="width:100%;" >
                                <tr>
                                    <td><asp:Button ID="BtnCerrarPdf" runat="server" Text="Cerrar" CssClass="boton-rojo" OnClick="BtnCerrarPdf_Click" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="dvBtnFlotar" style="width:100%; text-align:center;" >
                                            <asp:HiddenField ID="hfIdArchivo" runat="server" Value="9999" />
                                        </div>
                                        <div id="EspacioPDF" style="width:100%; height:550px; vertical-align:top" tabindex="0" >
                                            <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
   </fieldset>
</asp:Content>