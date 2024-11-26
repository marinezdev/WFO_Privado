<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsCorreoTramites.aspx.cs" Inherits="wfip.promotoria.CorreoTramites" MasterPageFile="~/promotoria/promotoria.Master" %>

<%@ Register Assembly="DevExpress.Web.v17.2" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script>        
        function NegritaToggle(item) {
            if (item == 1) {
                $('#tdNombre').css('font-weight', 'bold');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
            else if (item == 2) {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'bold');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
            else if (item == 3) {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'bold');
            }
            else {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
        }

        function Confirmar() {
                pnlProcesando.Show();
            return continuar;
        }

        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }

    </script>
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     
    </style>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <fieldset>
        <legend>PANTALLA DE CAPTURA DE CORREO PARA TRAMITES</legend>

        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                <asp:UpdatePanel ID="upCaptura" runat="server">
                    <ContentTemplate>

                        <table align="center">
                            <tr><td></td><td></td><td align="right">VIP:</td><td><asp:RadioButtonList ID="rblVIP" runat="server" RepeatDirection="Horizontal"><asp:ListItem Value="1">Sí</asp:ListItem><asp:ListItem Value="2">No</asp:ListItem></asp:RadioButtonList></td></tr>
                            <tr><td>Número de Póliza:</td><td><asp:TextBox ID="txtNoPoliza" runat="server" AutoPostBack="True" OnTextChanged="txtNoPoliza_TextChanged"></asp:TextBox></td><td align="right">Número de Oficio:</td><td><asp:TextBox ID="txtNoOficio" runat="server"></asp:TextBox></td></tr>
                            <tr><td>Nombre del Cliente:</td><td colspan="3"><asp:TextBox ID="txtCliente" runat="server" Width="100%"></asp:TextBox></td></tr>
                            <tr><td>Persona que solicita el trámite:</td>
                                <td><asp:TextBox ID="txtNombre" runat="server" onfocus="NegritaToggle(1);" onblur="NegritaToggle();"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtAPaterno" runat="server" onfocus="NegritaToggle(2);" onblur="NegritaToggle();"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtAMaterno" runat="server" onfocus="NegritaToggle(3);" onblur="NegritaToggle();"></asp:TextBox></td></tr>
                            <tr><td></td>
                                <td id="tdNombre" align="center">Nombre</td>
                                <td id="tdAPaterno" align="center">Apellido Paterno</td>
                                <td id="tdAMaterno" align="center">Apellido Materno</td>
                            </tr>
                            <tr><td>Clave de Correo:</td><td><asp:TextBox ID="txtClaveCorreo" runat="server"></asp:TextBox></td><td align="right">Fecha de solicitud:</td><td><asp:TextBox ID="txtFechaSolicitud" runat="server"></asp:TextBox>
                            </td></tr>
                            <tr><td></td><td></td><td></td><td></td></tr>
                            <tr><td>Asunto:</td><td colspan="3"><asp:TextBox ID="txtAsunto" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox></td><td></td><td></td></tr>
                            <tr><td>Capturar archivo:</td>
                                <td><asp:RadioButtonList ID="rblCaptura" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblCaptura_SelectedIndexChanged"><asp:ListItem Value="1">Sí</asp:ListItem><asp:ListItem Value="2">No</asp:ListItem></asp:RadioButtonList></td>
                                <td></td> 
                                <td></td>
                            </tr>
                            <tr id="trCargaArchivo" runat="server" visible="false">
                                <td colspan="4" align="right">

                                    <asp:Wizard ID="AsistenteCargaArchivo" runat="server" BackColor="#FFFFFF" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Width="100%">
                                        <SideBarButtonStyle ForeColor="White" />  
                                        <SideBarStyle BackColor="SteelBlue" Font-Size="0.9em" VerticalAlign="Top" /> 
                                        <WizardSteps>

                                            <asp:WizardStep ID="Paso01" runat="server" Title="Importación de archivo de Excel">
                                                <table style="width: 100%;">
                                                    <tr><td>Requsitos para importar un archivo de tipo excel con información institucional y guardar en la base de datos para posterior manipulación.</td><td></td></tr>
                                                    <tr><td>El archivo debe estar libre de encabezados par apoderse agregar los datos que contiene.</td><td></td></tr>
                                                </table>                                                
                                            </asp:WizardStep> 
                                            
                                            <asp:WizardStep ID="Paso02" runat="server" Title="Revisión del archivo">
                                                <table style="width: 100%;">
                                                    <tr><td>Vista del archivo antes de ser importado y requisitos que debe cumplir para poder ser importado</td><td></td></tr>
                                                    <tr><td colspan="2">
                                                        <img src="../img/Archivo1.png" /></td></tr>
                                                    <tr><td>vista del archivo como debe quedar para poder ser importado</td><td></td></tr>
                                                    <tr><td colspan="2">
                                                        <img src="../img/Archivo2.png" /></td></tr>
                                                </table>
                                            </asp:WizardStep>
                                            
                                            <asp:WizardStep ID="Paso03" runat="server" Title="Revisión de los datos importados">
                                                <table style="width: 100%;">
                                                    <tr><td>Proceso exitoso o fallido.</td><td></td></tr>
                                                </table>

                                                <asp:UpdatePanel ID="upSubirExcel" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--<asp:FileUpload ID="fileUpDocumento" runat="server" />--%>
                                                        <ajaxToolkit:AsyncFileUpload id="AsyncFileUpload1" runat="server" PersistFile="true" ClientIDMode="AutoID" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" UploaderStyle="Modern" />
                                                        <asp:Button ID="btnSubirExcel" runat="server" Text="Procesar" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="btnSubirExcel_Click" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSubirExcel" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                                <dx:ASPxLoadingPanel ID="pnlProcesando" runat="server" ClientInstanceName="pnlProcesando" Modal="true" Text="Procesando...">
                                                </dx:ASPxLoadingPanel>
                                                
                                            </asp:WizardStep>
                                        </WizardSteps>
                                    </asp:Wizard>


                                </td>
                            </tr>
                            <tr><td colspan="4"></td></tr>
                        </table>
                        <hr />
                        <table id="tbTipoServicio" runat="server" visible="false" style="width: 100%">
                            <tr>
                                <td style="width: 163px">Tipo de Servicio:</td>
                                <td><asp:DropDownList ID="ddlTipoServicio" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoServicio_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                        
                        <table id="MovsAsegurados" runat="server" visible="false" style="width: 100%">
                            <tr>
                                <td><asp:Label ID="Label18" runat="server" Text="Acción"></asp:Label></td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlInsAccion" runat="server" Width="200px"></asp:DropDownList>
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
                                                <asp:TextBox ID="txtNombresAsegurados" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
                                            </td>
                                            <td>Apellido Paterno</td>
                                            <td>
                                                <asp:TextBox ID="txtAPaternoAsegurados" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
                                            </td>
                                            <td>Apellido Materno</td>
                                            <td>
                                                <asp:TextBox ID="txtAMaternoAsegurados" runat="server" MaxLength="70" Width="190px"></asp:TextBox>
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
                                    <asp:Label ID="lblFechaNacimiento" runat="server">Fecha de Nacimiento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaNacimiento" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtFechaNacimienti" runat="server" FilterMode="ValidChars" TargetControlID="txtFechaNacimiento" ValidChars="1234567890/" />
                                    <asp:RegularExpressionValidator ID="revtxtFechaNac" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txtFechaNacimiento" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red"></asp:RegularExpressionValidator>
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
                                    <asp:TextBox ID="txtFechaMovimiento" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtFechaMov" runat="server" FilterMode="ValidChars" TargetControlID="txtFechaMovimiento" ValidChars="1234567890/" />
                                    <asp:RegularExpressionValidator ID="revtxtFechaMov" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txtFechaMovimiento" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red"></asp:RegularExpressionValidator>
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkFechaAntiguedad"  runat="server" Text="Fecha Antiguedad" Checked="false" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaAntiguedad" runat="server" MaxLength="10" Width="120px" Visible = "false"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtFechaAnt" runat="server" FilterMode="ValidChars" TargetControlID="txtFechaAntiguedad" ValidChars="1234567890/" />
                                    <asp:RegularExpressionValidator ID="revtxtFechaAnt" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txtFechaAntiguedad" ErrorMessage="Fecha inválida, usar DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red"></asp:RegularExpressionValidator>
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="DD/MM/YYYY" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trSoportes" runat="server" visible="false">
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
                        <table id="tblCartas" runat="server" visible="false" style="width: 100%">
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
                        <table id="tblListado" runat="server" visible="false" style="width: 100%">
                            <tr>
                                <td style="width: 18%;">SubGrupo</td>
                                <td style="width: 42%;"><asp:TextBox ID="txtLSubGrupo" runat="server" MaxLength="15" Width="120px"></asp:TextBox></td>
                                <td>Categoría</td>
                                <td><asp:TextBox ID="txtLCategoria" runat="server" MaxLength="15" Width="120px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 18%">Nombre del contratante</td>
                                <td><asp:TextBox ID="txtContratante" runat="server" MaxLength="150" Width="300px"></asp:TextBox></td>
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

                        <table id="tbBtnAgregar" runat="server" visible="false" style="width: 100%">
                            <tr><td colspan="4" align="center"><asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="boton" OnClick="btnAgregar_Click" /></td></tr>
                        </table>                   

                        


                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div id="dvBotones" style="text-align: right">
            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="boton" OnClick="BtnContinuar_Click" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" />
        </div>
        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
    </fieldset>

    <br />

    <!--div style="width:99%; height: 300px; overflow: scroll; margin: auto"-->

    <div id="DivRoot" style="margin:auto">

        <div style="overflow: hidden;" id="DivHeaderRow"></div>

        <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">

        <asp:UpdatePanel ID="upGVAgregado" runat="server">
            <ContentTemplate>

                <asp:GridView ID="gvAgregado" runat="server" 
                    AutoGenerateColumns="False" 
                    BackColor="White" 
                    BorderColor="Yellow" 
                    BorderStyle="None" 
                    BorderWidth="0" GridLines="Both" 
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-Font-Size="XX-Small" 
                    RowStyle-Font-Size="Small"
                    CellPadding="4" 
                    CellSpacing="1"  
                    RowStyle-Wrap="false" 
                    HeaderStyle-Wrap="true"  
                    ShowFooter="true" 
                    Width="100%" 
                    SelectedRowStyle-BackColor="Green" 
                    OnRowDataBound="gvAgregado_RowDataBound"
                    OnRowCancelingEdit="gvAgregado_RowCancelingEdit" 
                    OnRowEditing="gvAgregado_RowEditing" 
                    OnRowUpdating="gvAgregado_RowUpdating" OnRowCommand="gvAgregado_RowCommand"
                    >
                    <Columns>
                        <asp:CommandField ShowEditButton="true" ButtonType="Link" EditText="Modificar" CancelText="Cancelar" UpdateText="Actualizar" />
                        <asp:TemplateField HeaderText="Dependencia">
                            <ItemTemplate>
                                <asp:Label ID="lblDependencia" runat="server" Text='<%# contarCaracteres(Eval("Dependencia").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlDependencia" runat="server" Font-Size="Small"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Apellido Paterno">
                            <ItemTemplate>
                                <asp:Label ID="lblAPaterno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "APaterno") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAPaterno" runat="server" Width="150px" MaxLength="20" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "APaterno") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Apellido Materno">
                            <ItemTemplate>
                                <asp:Label ID="lblAMaterno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AMaterno") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAMaterno" runat="server" Width="150px" MaxLength="20" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "AMaterno") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre(s)">
                            <ItemTemplate>
                                <asp:Label ID="lblNombres" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nombres") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombres" runat="server" Width="150px" MaxLength="20" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "Nombres") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Nacimiento" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFNacimiento" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FNacimiento") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFNacimiento" runat="server" Width="150px" MaxLength="8" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "FNacimiento") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RFC">
                            <ItemTemplate>
                                <asp:Label ID="lblRFC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RFC") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRFC" runat="server" Width="150px" MaxLength="13" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "RFC") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CURP">
                            <ItemTemplate>
                                <asp:Label ID="lblCURP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CURP") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCURP" runat="server" Width="150px" MaxLength="18" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "CURP") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sexo">
                            <ItemTemplate>
                                <asp:Label ID="lblSexo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Sexo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSexo" runat="server" Width="150px" MaxLength="1" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "Sexo") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Entidad Federativa" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblCEntidadFederativa" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CEntidadFederativa") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCEntidadFederativa" runat="server" Width="50px" MaxLength="2" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "CEntidadFederativa") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ciudad/Municipio" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblCMunicipio" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CMunicipio") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCMunicipio" runat="server" Width="50px" MaxLength="3" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "CMunicipio") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nivel Tabular" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblNivelTabular" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NivelTabular") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNivelTabular" runat="server" Width="50px" MaxLength="2" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "NivelTabular") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nivel Tabular Anterior" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblNivelTabularAnterior" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NTabularAnterior") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNTabularAnterior" runat="server" Width="50px" MaxLength="2" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "NTabularAnterior") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nivel Tabular Nuevo" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblNivelTabularNuevo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NTabularNuevo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNTabularNuevo" runat="server" Width="50px" MaxLength="2" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "NTabularNuevo") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monto Percepción Ordinaria" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblMPercepcionOrdinaria" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MPercepcionOrdinaria") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMPercepcionOrdinaria" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "MPercepcionOrdinaria") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monto Percepcion Ordinaria Bruta Anterior" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblMPercepcionOrdinariaBrutaAnterior" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MpercepcionOrdinariaBrutaAnterior") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMpercepcionOrdinariaBrutaAnterior" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "MpercepcionOrdinariaBrutaAnterior") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monto Percepcion Ordinaria Bruta Nuevo" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblMPercepcionOrdinariaBrutaNuevo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MPercepcionOrdinariaBrutaNuevo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMPercepcionOrdinariaBrutaNuevo" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "MPercepcionOrdinariaBrutaNuevo") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eventual" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblEventual" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Eventual") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEventual" runat="server" Width="50px" MaxLength="2" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "Eventual") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Movimiento" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFMovimiento" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FMovimiento") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFMovimiento" runat="server" Width="50px" MaxLength="8" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "FMovimiento") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Apellido Paterno Asegurado">
                            <ItemTemplate>
                                <asp:Label ID="lblAPAsegurado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "APAsegurado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAPAsegurado" runat="server" Width="50px" MaxLength="20" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "APAsegurado") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Apellido Materno Asegurado">
                            <ItemTemplate>
                                <asp:Label ID="lblAMAsegurado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AMAsegurado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAMAsegurado" runat="server" Width="50px" MaxLength="20" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "AMAsegurado") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre(s) Asegurado">
                            <ItemTemplate>
                                <asp:Label ID="lblNAsegurado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NAsegurado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNAsegurado" runat="server" Width="50px" MaxLength="20" Font-Size="XX-Small" Text='<%#DataBinder.Eval(Container.DataItem, "NAsegurado") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Nacimiento Asegurado" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFNAsegurado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FNAsegurado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFNAsegurado" runat="server" Width="50px" MaxLength="8" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "FNAsegurado") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CURP Asegurado">
                            <ItemTemplate>
                                <asp:Label ID="lblCURPAsegurado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CURPAsegurado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCURPAsegurado" runat="server" Width="50px" MaxLength="18" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "CURPAsegurado") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sexo Asegurado" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSAsegurado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SAsegurado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSAsegurado" runat="server" Width="50px" MaxLength="1" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SAsegurado") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>        
                        <asp:TemplateField HeaderText="Fecha Antigüedad Asegurado" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFAAsegurado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FAAsegurado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFAAsegurado" runat="server" Width="50px" MaxLength="8" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "FAAsegurado") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo de Asegurado" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTAsegurado" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TAsegurado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTAsegurado" runat="server" Width="50px" MaxLength="2" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "TAsegurado") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Ingreso Colectividad" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFIColectividad" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FIColectividad") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFIColectividad" runat="server" Width="50px" MaxLength="8" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "FIColectividad") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Campo Corregido">
                            <ItemTemplate>
                                <asp:Label ID="lblCampoCorregido" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CampoCorregido") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCampoCorregido" runat="server" Width="50px" MaxLength="77" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "CampoCorregido") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alta o Baja">
                            <ItemTemplate>
                                <asp:Label ID="lblAltaBaja" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AltaBaja") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAltaBaja" runat="server" Width="50px" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "AltaBaja") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Efecto Movimiento" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFEMovimiento" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FEMovimiento") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFEMovimiento" runat="server" Width="50px" MaxLength="8" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "FEMovimiento") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suma Asegurada Básica" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSABasica" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SABasica") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSABasica" runat="server" Width="50px" MaxLength="3" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SABasica") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suma Asegurada Básica Anterior" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSABAnterior" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SABAnterior") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSABAnterior" runat="server" Width="50px" MaxLength="3" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SABAnterior") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suma Asegurada Básica Nueva" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSABNueva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SABNueva") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSABNueva" runat="server" Width="50px" MaxLength="3" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SABNueva") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suma Asegurada Básica Incorrecta" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSABIncorrecta" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SABIncorrecta") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSABIncorrecta" runat="server" Width="50px" MaxLength="3" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SABIncorrecta") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suma Asegurada Básica Correcta" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSABCorrecta" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SABCorrecta") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSABCorrecta" runat="server" Width="50px" MaxLength="3" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SABCorrecta") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suma Asegurada del Trimestre a Reportar" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSABTrimestreReportar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SABTrimestreReportar") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSABTrimestreReportar" runat="server" Width="50px" MaxLength="3" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SABTrimestreReportar") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prima Básica Total Anterior" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPBTAnterior" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PBTAnterior") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPBTAnterior" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PBTAnterior") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Prima Básica Total Nueva" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPBTNueva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PBTNueva") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPBTNueva" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PBTNueva") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prima Básica Total Quincenas Cubiertas" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPBTQCubiertas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PBTQCubiertas") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPBTQCubiertas" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PBTQCubiertas") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prima Básica Total Incorrecta" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPBTIncorrecta" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PBTIncorrecta") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPBTIncorrecta" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PBTIncorrecta") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prima Básica Timestre a Reportar" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPBTReportar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PBTReportar") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPBTReportar" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PBTReportar") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Importe en la Prima Básica" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblIPBasica" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IPBasica") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIPBasica" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "IPBasica") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Importe a Pagar por la Dependencia" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblIPDependencia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IPDependencia") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIPDependencia" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "IPDependencia") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monto de Ajuste en Prima Básica" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblMAPBasica" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MAPBasica") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMAPBasica" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "MAPBasica") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suma Asegurada Potenciada" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSAPotenciada" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SAPotenciada") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSAPotenciada" runat="server" Width="50px" MaxLength="5" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SAPotenciada") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suma Asegurada Potenciada Anterior" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSAPAnterior" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SAPAnterior") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSAPAnterior" runat="server" Width="50px" MaxLength="5" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SAPAnterior") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Suma Asegurada Potenciada Nueva" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSAPNueva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SAPNueva") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSAPNueva" runat="server" Width="50px" MaxLength="5" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SAPNueva") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Suma Asegurada Potenciada Incorrecta" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSAPIncorrecta" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SAPIncorrecta") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSAPIncorrecta" runat="server" Width="50px" MaxLength="5" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SAPIncorrecta") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Suma Asegurada Potenciada Corecta" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSAPCorrecta" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SAPCorrecta") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSAPCorrecta" runat="server" Width="50px" MaxLength="5" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SAPCorrecta") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prima Potenciada Total Incorrecta" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPPTIncorrecta" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PPTIncorrecta") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPPTIncorrecta" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PPTIncorrecta") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Prima Potenciada Total Anterior" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPPTAnterior" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PPTAnterior") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPPTAnterior" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PPTAnterior") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Prima Potenciada Total Nueva" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPPTNueva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PPTNueva") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPPTNueva" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PPTNueva") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Prima Potenciada Total" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPPTotal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PPTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPPTotal" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PPTotal") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Importe Prima Potenciada Faltante" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblIPPFaltante" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IPPFaltante") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIPPFaltante" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "IPPFaltante") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Fecha Antigüedad Asegurado en Suma Asegurada" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFAASAsegurada" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FAASAsegurada") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFAASAsegurada" runat="server" Width="50px" MaxLength="8" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "FAASAsegurada") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Suma Asegurada Total" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSumaAseguradaTotal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SumaAseguradaTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSumaAseguradaTotal" runat="server" Width="50px" MaxLength="5" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "SumaAseguradaTotal") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Prima Potenciada Quincenal a Reportar" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPPQReportar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PPQReportar") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPPQReportar" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "PPQReportar") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Forma de Pago" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFormaPago" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FormaPago") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFormaPago" runat="server" Width="50px" MaxLength="3" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "FormaPago") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Monto de Ajuste en Prima Potenciada" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblMAPPotenciada" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MAPPotenciada") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMAPPotenciada" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "MAPPotenciada") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                        <asp:TemplateField HeaderText="Importe Total a Pagar Prima Potenciada" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblITPPotenciada" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ITPPPotenciada") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtITPPPotenciada" runat="server" Width="50px" MaxLength="6" Font-Size="Small" Text='<%#DataBinder.Eval(Container.DataItem, "ITPPPotenciada") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                        
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#deedf7" Font-Bold="True" ForeColor="#2779aa" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000000" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>

        </div>

        <div id="DivFooterRow" style="overflow:hidden"></div>

    </div>

                                
    <!--/div-->

</asp:Content>