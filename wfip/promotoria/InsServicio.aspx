<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="InsServicio.aspx.cs" Inherits="wfip.promotoria.InsServicio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var continuar = false;
            if (Page_ClientValidate() == true) {
                if (confirm('Esta seguro que desea continuar con el trámite ?')) {
                    continuar = true;
                }
                else {
                    alert('LAS OBSERVACIONES SON REQUERIDAS');
                }
            }
            return continuar;
        }
        
        function mostrar_detalle(user, Activo, clave) {
            if (Activo == '1') {
                var mousex = event.pageX;
                var mousey = event.pageY;

                user.style.left = mousex + 15 + 'px';
                user.style.top = mousey + 'px';
                user.style.visibility = 'visible';
                $('#dvpopContenido').load('Tramites/' + clave + '.html');
            }
            else {
                user.style.visibility = 'hidden';
            }
        }

        function validarFechaVigencia(txCtrl)
        {
            var FechaIn = document.getElementById("cph_areaTrabajo_txFechaInVi").value
            PageMethods.ValidarFechaVigencia(FechaIn,txCtrl.value, FechaOk, FechaError);
        }

        function FechaOk(resultado) {
            $get('cph_areaTrabajo_lbFechaSol').innerHTML = resultado;
        }

        function FechaError(error, userContext, methodName) {
            if (error != null) {
                alert(error.get_message());
            }
        }

        function buscaNombreAgente(txCtrl) {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            PageMethods.ObtenerNombreDeAgente(idPromotoria, txCtrl.value, regresaNombreAgente, siErrorNombreAgente);
        }

        function regresaNombreAgente(resultado) {
            $get('cph_areaTrabajo_lbNombreAgente').innerHTML = resultado;
        }

        function siErrorNombreAgente(error, userContext, methodName) {
            if (error != null) {
                alert(error.get_message());
            }
        }

        function regresaRegion() {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            alert(PageMethods.ObtenerNombreRegion(idPromotoria));
        }

        $(document).ready(function () {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            // MOSTRARA EL DATO DE LA REGION ///
            Region(idPromotoria);
            var valorTxt = document.getElementById("cph_areaTrabajo_texClavePromotoria").value = idPromotoria;
        });
       
        function Region(idPromotoria) {
            PageMethods.ObtenerNombreRegion(idPromotoria, RegionOk, Error);
        }

        function RegionOk(resultado) {
            var valorTxt = document.getElementById("cph_areaTrabajo_texRegion").value = resultado[0];
            var valorTxt = document.getElementById("cph_areaTrabajo_texSubDireccion").value = resultado[1];
            var valorTxt = document.getElementById("cph_areaTrabajo_texGerenteComercial").value = resultado[2];
            var valorTxt = document.getElementById("cph_areaTrabajo_texEjecuticoComercial").value = resultado[3];
        }

     function Validate(sender, args) {
            if (document.getElementById(sender.controltovalidate).value != "") {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>
    <style>
        #dvPop {
            position: absolute;
            border: 1px solid #007CC3;
            width: 400px;
            color: blue;
            background-color: #f2fafa;
            visibility: hidden;
            z-index: 10;
            font-size: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <fieldset>
        <legend>INSTITUCIONAL <asp:Label ID="Label3" runat="server"></asp:Label> SERVICIOS </legend>
        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ValidationSummary ID="vsGral" runat="server"
                            HeaderText="LOS SIGUENTES DATOS SON REQUERIDOS....."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            ShowSummary="true"
                            BackColor="Snow"
                            Width="450"
                            ForeColor="Red"
                            Font-Size="Small"
                            Font-Italic="true"
                            ValidationGroup="vdGral" />
                        

                        <table id="tbInfGralTramite" style="width: 100%">
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">PÓLIZA /SEGURO</span>
                                    <hr />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 18%">Producto </td>
                                <td>
                                    <asp:DropDownList ID="ddlTramiteTipo" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="TramiteTipPoliza_SelectedIndexChanged" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 18%">Ramo</td>
                                <td>
                                    <asp:DropDownList ID="ddlRamo" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>                        
                        
                        <table style="width: 100%">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN</span>
                                    <hr />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">Clave promotoria</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:TextBox ID="texClavePromotoria" runat="server" MaxLength="5" Width="50px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 18%">Región </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:TextBox ID="texRegion" runat="server" MaxLength="5" Width="150px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">Agente</td>
                                <td>
                                    <asp:HiddenField ID="hf_IdPromotoria" runat="server" />
                                    <asp:TextBox ID="txIdAgente" runat="server" MaxLength="5" Width="120px" AutoPostBack="false" onblur="buscaNombreAgente(this)"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txIdAgente" runat="server" FilterType="Numbers" TargetControlID="txIdAgente" />
                                    <asp:RequiredFieldValidator ID="rfv_txIdAgente" runat="server" ErrorMessage="AGENTE" Text="*" ControlToValidate="txIdAgente" ForeColor="Red" Font-Size="16px" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbNombreAgente" runat="server" Text="N.D."></asp:Label>
                                </td>
                                <td>Subdirección</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <asp:TextBox ID="texSubDireccion" runat="server" MaxLength="5" Width="150px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">Número de póliza</td>
                                <td>
                                    <asp:TextBox ID="txPoliza" runat="server" MaxLength="15"  Width="120px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txPoliza" runat="server" FilterMode="ValidChars" TargetControlID="txPoliza" ValidChars="ABCDEFGHIJKLMNOPQRSTWXYZabcdefghijklmnñopqrstuvwxyz1234567890-/" />
                                    <asp:RequiredFieldValidator ID="rfvPoliza" runat="server" ErrorMessage="Número de póliza" Text="*" ControlToValidate="txPoliza" ForeColor="Red" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                                <td >Gerente Comercial</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:TextBox ID="texGerenteComercial" runat="server" MaxLength="5" Width="150px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 18%">Número de orden</td>
                                <td>
                                    <asp:TextBox ID="textNumeroOrden" runat="server" AutoPostBack="false" MaxLength="15" Width="120px"></asp:TextBox>
                                </td>
                                <td>Ejecutivo Comercial</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:TextBox ID="texEjecuticoComercial" runat="server" AutoPostBack="false" Enabled="false" MaxLength="5" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Contratante </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="false" MaxLength="15" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textNumeroOrden" ErrorMessage="CONTRATANTE" ForeColor="Red" Text="*" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Fecha solicitud</td>
                                <td>
                                    <asp:TextBox ID="txtFechaSolicitud" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txtFechaSolicitud" ValidChars="1234567890/" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFechaSolicitud" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" ForeColor="Red" Text="*" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 18%">Inicio Poliza</td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txtFLMovimiento" ValidChars="1234567890/" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFLMovimiento" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" ForeColor="Red" Text="*" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label19" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                </td>
                                <td style="width: 18%">Fin Poliza</td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" TargetControlID="txtFLMovimiento" ValidChars="1234567890/" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtFLMovimiento" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" ForeColor="Red" Text="*" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label20" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <br />
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3"></span>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>Tipo de Servicio</td>
                                <td style="margin-left: 80px">
                                    <asp:DropDownList ID="ddlTipoServicio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                        
                        <asp:Panel ID="MovsAsegurados" runat="server" Visible="false">
                        
                            <table style="width: 100%">
                                <tr>
                                    <td><asp:Label ID="Label18" runat="server" Text="Acción"></asp:Label></td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlInsAccion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboInsAccion_SelectedIndexChanged"  Width="200px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Certificado</td>
                                    <td>
                                        <asp:TextBox ID="txtCertificado" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txtCertificado" ValidChars="ABCDEFGHIJKLMNOPQRSTWXYZabcdefghijklmnñopqrstuvwxyz1234567890-/" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label16" runat="server" Text="SubGrupo"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtSubGrupo" runat="server" MaxLength="15" Width="190px"></asp:TextBox>
                                    </td>
                                    <td><asp:Label ID="Label17" runat="server" Text="Categoría"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtCategoria" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table id="tblNombre1" style="width: 100%">
                                            <tr>
                                                <td>Nombre(s)</td>
                                                <td>
                                                    <asp:TextBox ID="txtNombres" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
                                                </td>
                                                <td>Apellido Paterno</td>
                                                <td>
                                                    <asp:TextBox ID="txtAPaterno" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
                                                </td>
                                                <td>Apellido Materno</td>
                                                <td>
                                                    <asp:TextBox ID="txtAMaterno" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Parentesco</td><td><asp:DropDownList ID="ddlParentesco" runat="server" Width="200px">
                                            </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFechaNac" runat="server">Fecha de Nacimiento</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaNac" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtFechaNac" runat="server" FilterMode="ValidChars" TargetControlID="txtFechaNac" ValidChars="1234567890/" />
                                        <asp:RegularExpressionValidator ID="revtxtFechaNac" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txtFechaNac" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red"></asp:RegularExpressionValidator>
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label8" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGenero" runat="server">Genero</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGenero" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSueldo" runat="server" Visible = "false">Sueldo</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSueldo" runat="server" MaxLength="20" Width="120px" Visible = "false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fecha de Movimiento</td>
                                    <td>
                                        <asp:TextBox ID="txtFechaMov" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtFechaMov" runat="server" FilterMode="ValidChars" TargetControlID="txtFechaMov" ValidChars="1234567890/" />
                                        <asp:RegularExpressionValidator ID="revtxtFechaMov" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txtFechaMov" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red"></asp:RegularExpressionValidator>
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkfechaAntiguedad"  runat="server" AutoPostBack="True" oncheckedchanged="chkfechaAntiguedad_CheckedChanged" Text="Fecha Antiguedad" Checked="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaAnt" runat="server" MaxLength="10" Width="120px" Visible = "false"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtFechaAnt" runat="server" FilterMode="ValidChars" TargetControlID="txtFechaAnt" ValidChars="1234567890/" />
                                        <asp:RegularExpressionValidator ID="revtxtFechaAnt" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txtFechaAnt" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red"></asp:RegularExpressionValidator>
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="DD/MM/YYYY" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="Label14" runat="server" Text="Soportes" Visible="false"></asp:Label></td>
                                    <td><asp:FileUpload ID="FileUpload1" runat="server" Visible="false" /></td>
                                    <td>
                                        <asp:Button ID="btnSubirSoporte" runat="server" Text="Subir Soportes" CssClass="boton" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label6" runat="server" Text="Poliza para Rec. Ant." Visible="false"></asp:Label></td>
                                    <td><asp:TextBox ID="txtPolizaRecAnt" runat="server" MaxLength="15" Width="120px" Visible="false"></asp:TextBox></td>
                                    <td><asp:Label ID="Label7" runat="server" Text="Cia. anterior para Rec. Ant." Visible="false"></asp:Label></td>
                                    <td><asp:TextBox ID="txtCiaAntRec" runat="server" MaxLength="15" Width="120px" Visible="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label15" runat="server" Text="Certificado Impreso"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtCertImp" runat="server" MaxLength="15" Width="190px" Visible = "false"></asp:TextBox>
                                        <asp:RadioButton id="rbCertImpSi" runat="server" GroupName="opCertImp" Text="SI"></asp:RadioButton>&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton id="rbCertImpNo" runat="server" GroupName="opCertImp" Text="NO" Checked="true"></asp:RadioButton>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>

                        </asp:Panel>
                        <asp:Panel ID="Cartas" runat="server" Visible="false">
                            <table id="tblCartas" style="width: 100%">
                                <tr>
                                    <td><asp:Label ID="Label9" runat="server" Text="Tipo Carta"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoCarta" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label10" runat="server" Text="Número de certificado"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtNoCertificado" runat="server" MaxLength="15" Width="190px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table id="tblNombre2" style="width: 100%">
                                            <tr>
                                                <td><asp:Label ID="Label11" runat="server" Text="Número de certificado"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtCNombres" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
                                                </td>
                                                <td><asp:Label ID="Label12" runat="server" Text="Número de certificado"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtCAPaterno" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
                                                </td>
                                                <td><asp:Label ID="Label13" runat="server" Text="Número de certificado"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtCAMaterno" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Listado" runat="server" Visible="false">
                            <table id="tblListado" style="width: 100%">
                                <tr>
                                    <td style="width: 18%;">SubGrupo</td>
                                    <td style="width: 42%;"><asp:TextBox ID="txtLSubGrupo" runat="server" MaxLength="15" Width="120px"></asp:TextBox></td>
                                    <td>Categoría</td>
                                    <td><asp:TextBox ID="txtLCategoria" runat="server" MaxLength="15" Width="120px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 18%">Nombre del contratante</td>
                                    <td><asp:TextBox ID="txtContratante" runat="server" MaxLength="150" Width="300px" AutoPostBack="false" ></asp:TextBox></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 18%">RFC</td>
                                    <td><asp:TextBox ID="txtRFC" runat="server" MaxLength="13" Width="120px"></asp:TextBox></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 18%">Domicilio Fiscal</td>
                                    <td><asp:TextBox ID="txtDomFiscal" runat="server" MaxLength="150" Width="300px"></asp:TextBox></td>
                                    <td>Forma de Pago</td>
                                    <td><asp:TextBox ID="txtFormaPago" runat="server" MaxLength="30" Width="120px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 18%">Servicio</td>
                                    <td>
                                        <asp:DropDownList ID="ddlInsServicioListados" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Comentarios</td>
                                    <td><asp:TextBox ID="txtLComentarios" runat="server" MaxLength="300" Width="120px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 18%">Fecha del movimiento</td>
                                    <td>
                                        <asp:TextBox ID="txtFLMovimiento" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtFLMovimiento" runat="server" FilterMode="ValidChars" TargetControlID="txtFLMovimiento" ValidChars="1234567890/" />
                                        <asp:RegularExpressionValidator ID="revtxtFLMovimiento" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txtFLMovimiento" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red"></asp:RegularExpressionValidator>
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>                        
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2" align="center"><asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="boton" OnClick="btnAgregar_Click" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">Agregar registros vía archivo de Excel</td>
                                <td align="left">
                                    <asp:UpdatePanel ID="upSubirExcel" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:FileUpload ID="fileUpDocumento" runat="server" />
                                            <asp:Button ID="btnSubirExcel" runat="server" Text="Procesar" CssClass="boton" OnClick="btnSubirExcel_Click" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubirExcel" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="gvAgregado" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" 
                                        BorderColor="Yellow" 
                                        BorderStyle="None" 
                                        BorderWidth="0" GridLines="Horizontal"
                                        HeaderStyle-Font-Size="XX-Small"
                                        CellPadding="4" CellSpacing="1" 
                                        OnRowCancelingEdit="gvAgregado_RowCancelingEdit" 
                                        OnRowDeleting="gvAgregado_RowDeleting" 
                                        OnRowEditing="gvAgregado_RowEditing" 
                                        OnRowUpdating="gvAgregado_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Tipo Servicio">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoServicio" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TipoServicio") %>'></asp:Label>
                                                    <asp:Label ID="lblIdTipoServicio" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdTipoServicio") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acción">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccion" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Accion") %>'></asp:Label>
                                                    <asp:Label ID="lblIdAccion" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdAccion") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Certificado" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCertificado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Certificado") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Grupo" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubGrupo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SubGrupo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Categoría" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoria" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Categoria") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres" Visible ="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombres" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nombres") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apellido Paterno" Visible ="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAPaterno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "APaterno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apellido Materno" Visible ="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAMaterno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AMaterno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parentesco" Visible ="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentesco" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Parentesco") %>'></asp:Label>
                                                    <asp:Label ID="lblIdParentesco" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdParentesco") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Nacimiento" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFechaNacimiento" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FNacimiento") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Género" Visible ="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGenero" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Genero") %>'></asp:Label>
                                                    <asp:Label ID="lblIdGenero" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdGenero") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sueldo" Visible ="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSueldo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Sueldo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Movimiento" Visible ="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFMovimiento" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FMovimiento") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFAntiguedad" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FAntiguedad") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPolizaReCant" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PolizaReCant") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCiaAnterior" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CiaAnterior") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCertificadoImpreso" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CertificadoImpreso") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoCarta" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TipoCarta") %>'></asp:Label>
                                                    <asp:Label ID="lblIdTipoCarta" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdTipoCarta") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoCertificado2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoCertificado2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreContratante" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NombreContratante") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRFC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RFC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDomFiscal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DomFiscal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFormaPago" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FormaPago") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServicio" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Servicio") %>'></asp:Label>
                                                    <asp:Label ID="lblIdServicio" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdServicio") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>        
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComentarios" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comentarios") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible ="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFLMovimiento" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FLMovimiento") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="true" ButtonType="Link" EditText="Cambiar" CancelText="Cancelar" UpdateText="Actualizar" Visible="false" />
                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Link" DeleteText="Quitar registro" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#deedf7" Font-Bold="True" ForeColor="#2779aa" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000000" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>

                        <br />

                        <table id="tblObjetivoDelTramite" style="width: 100%">
                            <tr>
                                <td>
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">OBSERVACIONES </span>
                                    <hr />
                                    <asp:TextBox ID="txDetalle" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" runat="server" FilterMode="ValidChars" TargetControlID="txDetalle" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-" />
                                    <asp:RequiredFieldValidator ID="rfvDetalle" runat="server" ControlToValidate="txDetalle" ErrorMessage="OBSERVACIONES " Font-Size="16px" ForeColor="Red" Text="*" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvBotones" style="text-align: right">
            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="boton" OnClientClick="return Confirmar();" OnClick="BtnContinuar_Click" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
