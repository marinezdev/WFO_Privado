<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/iniPromotoria.Master" AutoEventWireup="true" CodeBehind="anexaArchivosRes.aspx.cs" Inherits="wfip.promotoria.anexaArchivosRes" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script>

        function EliminaExpediente(num) {
            new PNotify({
                title: 'Archivo eliminado !',
                text: 'Ha eliminado un archivo de la lista de expedientes.',
                type: 'error',
                styling: 'bootstrap3'
            });
            $('#cph_areaTrabajo_LabelExpedienteRestante').text(num);
            $('#cph_areaTrabajo_LabelRestantes').text(num);
            $("#btnAdd").trigger("click");
        }

        function ExpedinteIncompleto() {
            new PNotify({
                title: 'Formulario incompleto !',
                text: 'No se han subido archivos al expediente.',
                type: 'error',
                styling: 'bootstrap3'
            });
            $("#btnAdd").trigger("click");
        }


        

    </script>

    <script>
        var bPreguntar = false;

        window.onbeforeunload = preguntarAntesDeSalir;

        function preguntarAntesDeSalir() {
            if (bPreguntar)
                return "¿Seguro que quieres salir?";
        }
    </script>
    <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#btnAdd").trigger("click"); // ejecutas el evento click del boton
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:HiddenField ID="hfArchivos" runat="server" Value="10" />

     <!-- MODAL CARGA DE EXPEDIENTE -->
	    <div class="modal fade CargadeArchivosExpediente"  tabindex="-1" role="dialog" aria-hidden="true">
		    <div class="modal-dialog modal-sm">
			    <div class="modal-content">
				    <div class="modal-header">
                        <h4 class="modal-title">Carga Archivo Expediente</h4>
                    </div>
				    <div class="modal-body ">
					    <asp:Label ID="lblDocumentosRequeridos" runat="server" Text="Archivos (*.PDF, *.JPG, *.PNG)"></asp:Label>
                        <br />
                        <asp:FileUpload ID="fileUpDocumento" AutoPostBack="True" ClientIDMode="Static" multiple="multiple" runat="server"></asp:FileUpload>
					    <br />
					    <span style="font-size: 12px">
                            <p>Tamaño máximo por expediente <asp:Label ID="LabelExpedienteMax" runat="server" Text="10"></asp:Label> MG
                                <br />
                            Restantes <asp:Label ID="LabelExpedienteRestante" runat="server"></asp:Label> MG</p>
					    </span>
                        <br />
					    <asp:LinkButton ID="btnSubirDocumento" CssClass="btn btn-default" EnableTheming="True" runat="server" CausesValidation="False" OnClientClick="bPreguntar = false;" OnClick="btnSubirDocumento_Click">
                            <span class="btn-label">
							    <i class="fa fa-paper-plane"> </i>
						    </span>
						    Subir
                        </asp:LinkButton>
				    </div>
				    <div class="modal-footer text-center">
                        <div class="col-md-6 col-lg-6">
                        </div>
					    <div class="col-md-6 col-lg-6">
						    <button type="button" class="btn btn-default col-lg-12" data-dismiss="modal">Cancelar</button>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>

    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional" Visible="True">
        <ContentTemplate>
            
        
        <div class="row">
            <div class="x_panel">
                <br />
                <div class="container">

                    <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                        <div class="card">
                            <div class="card-header">
                            <h3><small>ARCHIVOS ANEXOS </small></h3>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <br />
                                    <div class="row">
                                        <div class="container">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="container">
                                        <asp:UpdatePanel id="DatosTramiteInformacion" runat="server" UpdateMode="Conditional" Visible="true">
                                            <ContentTemplate>
                                            <asp:Label runat="server" ID="LabelAgente" Text="Agente" Font-Bold="True" class="control-label col-md-2 col-sm-2 col-xs-12 "></asp:Label>
                                            <div class="col-md-7 col-sm-7 col-xs-12 form-group has-feedback">
                                                <asp:TextBox ID="txIdAgente" runat="server" MaxLength="10" class="form-control " AutoPostBack="true" OnTextChanged="daNombreDeAgente"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterMode="ValidChars" TargetControlID="txIdAgente" ValidChars="áéíóúabcdefghijklmnñopqrstuvwxyzÁÉÍÓÚABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789" />
                                            </div>
                                            
                                            <asp:Label ID="Mensajes" runat="server" Text="   " ForeColor="Maroon"></asp:Label>
                                            <div class="col-md-11 col-sm-11 col-xs-12">
                                            <table class="table">
                                                <thead class="thead-dark">
                                                    <tr>
                                                        <th scope="col">Nombre</th>
                                                        <th scope="col">Correo Electrónico</th>
                                                        <th scope="col">Correo Electrónico Alterno</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th  scope="row"><asp:Label ID="lbNombreAgente" runat="server" Text=""></asp:Label></th>
                                                        <td><asp:Label ID="lbEmailAgente" runat="server" Text=""></asp:Label></td>
                                                        <td><asp:Label ID="lbEmailAlternoAgente" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <br />
                                    <div class="container">
                                        <div class="card">
                                            <div class="card-body">
                                                <asp:Label runat="server" ID="Label1" Text="CHECKLIST  DE DOCUMENTOS" Font-Bold="True" class="control-label col-md-12 col-sm-12 col-xs-12 "></asp:Label>
                                                <hr />
                                                <asp:CheckBoxList ID="DocRequerid" runat="server" CssClass="cbl">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div class="container">
                                    <asp:Panel ID="PanelArchivosTiket" runat="server"  Visible="true">
                                        <div class="card">
                                            <div class="card-header">
                                                <h4 class="text-white">Archivos agregados (Expediente).</h4>
                                                <input type="button" value="Preguntar" id="btnAdd" style="visibility:hidden" onclick="bPreguntar = true;" >
								            </div>
                                            <div class="card-body">
                                                <asp:LinkButton ID="LinkButton2" runat="server" EnableTheming="True" ValidationGroup="Bloque1" Visible="true" Class="btn btn-default" data-toggle="modal" data-target=".CargadeArchivosExpediente">
											        <span class="btn-label">
												        <i class="fa fa-file" aria-hidden="true"></i>
											        </span>
											        Cargar Expediente
										        </asp:LinkButton>
                                                <asp:UpdatePanel ID="PnlArchivosAnexos" runat="server">
                                                    <ContentTemplate>
                                                        <fieldset>
                                                            <code><asp:Label ID="LabRespuestaArchivosCarga" runat="server" Text =""></asp:Label><br />
                                                                <p> <asp:Label ID="LabelRestantes" runat="server"></asp:Label> MG Disponibles.</p>
                                                            </code>
                                                            <asp:ListBox ID="lstDocumentos" runat="server" Height="100px" Width="100%" SelectionMode="Single" class="select2_multiple form-control">
                                                            </asp:ListBox>
                                                            <br />
                                                            <asp:Button ID="btnEliminaDocumento" runat="server" Text="Eliminar" class="btn btn-danger" CausesValidation="False"  OnClick="btnEliminaDocumento_Click" />
                                                        </fieldset>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSubirDocumento" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 text-justify">
                                                <asp:Label ID="LabelDescripcion" runat="server" Font-Size="14px"  >Antes de subir los archivos, verifica que no estén deñados y pertenezcan al formato permitido <strong>(*.PDF, *.JPG, *.PNG)</strong>. Al dar clic en el botón <strong>continuar</strong>, No cambies o cierres la pestaña.</asp:Label>
                                                <p>Tamaño máximo por expediente <strong><asp:Label ID="LabelTamExpediente" runat="server" Text="10"></asp:Label> MG</strong>.
                                                
                                           </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <asp:Panel ID="Regresar" runat="server" Visible="false">
                                                <asp:Button ID="btnRegresar" runat="server"  CssClass=" btn btn-info col-md-5 col-sm-5 col-xs-12" OnClientClick="bPreguntar = false;" OnClick="BtnposBack" Text="Regresar" Visible="true"/>
                                            </asp:Panel>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="btn btn-success col-md-5 col-sm-5 col-xs-12" OnClientClick="bPreguntar = false;" OnClick="BtnContinuar_Click"/>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger  col-md-5 col-sm-5 col-xs-12" OnClientClick="bPreguntar = false;" OnClick="BtnCancelar_Click" CausesValidation="false" />
                                        </div>
                                        <asp:Button ID="Button1" style="visibility:hidden" runat="server" Text="Aceptar" class="btn btn-primary btn-lg"  OnClick="Button_Continuar" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                         </div>
                    </div>

                </div>
            </div>
        </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

