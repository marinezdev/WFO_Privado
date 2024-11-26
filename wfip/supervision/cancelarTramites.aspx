<%@ Page Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="cancelarTramites.aspx.cs" Inherits="wfip.supervision.cancelarTramites" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v17.2" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="mensajesInformativos" runat="server"></asp:UpdatePanel>


    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        function treeList_CustomDataCallbackCancelar(s, e) {
                document.getElementById('treeListCountCell').innerHTML = e.result;
            }
        function treeList_SelectionChangedCancelar(s, e) {
                window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
        }

        function cierraTodo() {
            pnlPopMotivosCancelar.Hide();
        }

        function CancelaTramite()
        {
            alert(1);
            cierraTodo();
            pnlPopMotivosCancelar.Show();
        }

        function fnAplicaCancelacion()
        {
            cierraTodo();
            fxPintaProcesando();
            btnCtrlAplicaCancelacion.DoClick();
        }

    </script>
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
                    //"oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
                    "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
                }
            });
        });
    </script>
    <script>
        $(document).keypress(
            function (event) {
                if (event.which == '13') {
                    event.preventDefault();
                }
            }); 
    </script>
    <fieldset>
        <legend>CANCELACIÓN DE TRÁMITES </legend>
        <asp:HiddenField ID="hdIdTramite" runat="server" />
         <table  style ="width:100%">
            <tr>
                <td colspan="4">
                    <label>Búsqueda por rangos de fecha de envió </label>
                    <br />
                    <asp:Label ID="MSresultado2" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                </td>
            </tr>
            <tr>
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
                <td>
                    <div style="vertical-align:bottom">
                        <asp:Button ID="Button2"  CssClass="boton" runat="server" Text="Limpiar" OnClick="LimpiaFormulario"  CausesValidation="false"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                    <p class="text-muted font-13 m-b-30">
                        Consulta por filtro.
                    </p>
                </td>
            </tr>
            <tr>
                <td style="width:18%">
                    <asp:Label runat="server" ID="Label2" Text="Folio: " Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                    <div class="col-md-5 col-sm-5 col-xs-12 form-group has-feedback">
                        <asp:TextBox ID="TextFolio" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" Width="200px" ></asp:TextBox>
                    </div>
                </td>
                <td style="width:18%">
                    <asp:Label runat="server" ID="Label3" Text="RFC: " Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                        <asp:TextBox ID="TextRFC" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" Width="200px" ></asp:TextBox>
                    </div>
                </td>
                <td colspan="2"style="width:30%">

                </td>
            </tr>
            <tr>
                <td colspan ="4">
                    <p class="text-muted font-13 m-b-30">
                        Contratante
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblNombre" Text="Nombre(s)" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" Width="200px"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblAPaterno" Text="Apellido Paterno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                        <asp:TextBox ID="txApPat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" Width="200px"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblAMaterno" Text="Apellido Materno" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                    <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                        <asp:TextBox ID="txApMat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" Width="200px"></asp:TextBox>
                    </div>
                </td>
                <td>    
                    <div style="vertical-align:bottom">
                        <asp:Button ID="Button1"  CssClass="boton" runat="server" Text="Filtrar" OnClick="ConsultaFiltros" CausesValidation="false"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style ="width:100%; vertical-align:top" colspan="4">
                    <asp:Panel ID="pnTrmPausa" runat="server" Width="100%">
                        <div id="dvTrmPausa" style="width: 100%; margin: auto; font-family: Arial;">
                            <asp:Repeater ID="rptTramitesEspera" runat="server">
                                <HeaderTemplate>
                                    <table id="tblTramitesEspera" style="width:100%" class="display" >
                                        <thead>
                                            <th scope="col">Fecha envió</th>
                                            <th scope="col">Número de trámite</th>
                                            <th scope="col">Información de contratante</th>
                                            <th scope="col">Estado</th>
                                            <th scope="col">Fecha Firma de Solicitud</th>
                                            <th scope="col">Número Póliza</th>
                                            <th scope="col">Identificador</th>
                                            <th scope="col">Cancelar</th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("FechaRegistro")%></td>
                                        <td><%# Eval("FolioCompuesto")%></td>
                                        <td><%# Eval("DatosHtml")%></td>
                                        <td><%# Eval("Nombre")%></td>
                                        <td><%# Eval("FechaSolicitud")%></td>
                                        <td><%# Eval("IdSisLegados")%></td>
                                        <td><%# Eval("Prioridad")%></td>
                                        <td style="width: 20px; text-align:center"><asp:ImageButton ID="imbtnConsultar" runat="server" ImageUrl="~/img/rechazado.jpg" CommandName='Consultar' CommandArgument='<%# Eval("Id")%>' OnCommand="CargaExpedienteID" /></td>
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

    <dx:ASPxButton ID="btnCtrlAplicaCancelacion" 
                    runat="server" 
                    Text="Aplica Cancelación" 
                    ClientVisible="False" 
                    OnClick="btnCtrlAplicaCancelacion_Click" 
                    ClientInstanceName="btnCtrlAplicaCancelacion" 
                    CausesValidation="False">
        <ClientSideEvents Click="function(s, e) 
            { 
                pnlMsgProcesando.Show(); 
            }" />
    </dx:ASPxButton>

    <asp:UpdatePanel id="DatosTramiteInformacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
        <dx:ASPxPopupControl ID="pnlPopMotivosCancelar" runat="server" CloseAction="CloseButton"  HeaderText="Motivos Suspender" ShowFooter="True"  Theme="Aqua"  Width="350px" ClientInstanceName="pnlPopMotivosCancelar"  Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" FooterText="">
	        <ContentStyle>
		        <Paddings Padding="5px" />
	        </ContentStyle>
	        <ContentCollection>
		        <dx:PopupControlContentControl runat="server">
			        <dx:ASPxCallbackPanel ID="pnlCallbackMotCancelar" runat="server" ClientInstanceName="pnlCallbackMotSuspender" Width="100%" OnCallback="pnlCallbackMotCancelar_Callback">
				        <PanelCollection>
					        <dx:PanelContent runat="server">
						        <dx:ASPxTreeList ID="treeListCancelar" runat="server" EnableCallbacks="false" AutoGenerateColumns="False" KeyFieldName="Id" OnCustomDataCallback="treeList_CustomDataCallbackCancelar" OnDataBound="treeList_DataBoundCancelar" ParentFieldName="IdParent" Width="100%">
                                    <Columns>
                                        <dx:TreeListDataColumn AutoFilterCondition="Default" Caption="Motivos de cancelación" FieldName="motivoRechazo" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="1"></dx:TreeListDataColumn>
                                    </Columns>
                                    <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                    <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                    <settingsselection enabled="True"></settingsselection>
                                    <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                    <settingspopup>
                                        <editform verticaloffset="-1"></editform>
                                    </settingspopup>
                                    <clientsideevents customdatacallback="treeList_CustomDataCallbackCancelar" selectionchanged="treeList_SelectionChangedCancelar"></clientsideevents>
                                </dx:ASPxTreeList>
					        </dx:PanelContent>
				        </PanelCollection>
			        </dx:ASPxCallbackPanel>
		        </dx:PopupControlContentControl>
	        </ContentCollection>
	        <FooterTemplate>
		        <div style="text-align:right;">
                    <br />&nbsp;
			        <dx:ASPxButton ID="btnAplicaHold" runat="server" AutoPostBack="False" EnableTheming="True" Text=" Aplicar Cancelación " Theme="Aqua">
				        <ClientSideEvents Click="function(s, e) { fnAplicaCancelacion(); }" />
			        </dx:ASPxButton>&nbsp;&nbsp;&nbsp;<br />&nbsp;
		        </div>
	        </FooterTemplate>
        </dx:ASPxPopupControl>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
