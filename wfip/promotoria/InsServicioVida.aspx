<%@ Page Title="" Language="C#" MasterPageFile="~/operacion/operacion.Master" AutoEventWireup="true" CodeBehind="InsServicioVida.aspx.cs" Inherits="wfip.promotoria.InsServicioVida" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">

        
        function mostrar_detalle(user, Activo, clave) {
            if (Activo == '1') {

                var mousex = event.pageX;
                var mousey = event.pageY;

                user.style.left = mousex + 15 + 'px';
                user.style.top = mousey + 'px';
                user.style.visibility = 'visible';
                $('#dvpopContenido').load('Tramites/' + clave + '.html');
            } else { user.style.visibility = 'hidden'; }
        }

        function buscaNombreAgente(txCtrl) {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            PageMethods.daNombreDeAgente(idPromotoria, txCtrl.value, regresaNombreAgente, siErrorNombreAgente);
        }
        function regresaNombreAgente(resultado) { $get('cph_areaTrabajo_lbNombreAgente').innerHTML = resultado; }
        function siErrorNombreAgente(error, userContext, methodName) { if (error != null) { alert(error.get_message()); } }

        function regresaRegion() {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            alert(PageMethods.daNombreDeRegion(idPromotoria));
        }

        $(document).ready(function () {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            // MOSTRARA EL DATO DE LA REGION ///
            Region(idPromotoria);
            var valorTxt = document.getElementById("cph_areaTrabajo_texClavePromotoria").value = idPromotoria;
        });
       
        function Region(idPromotoria) {
            PageMethods.regresaNombreRegion(idPromotoria, RegionOk, Error);
        }
        function RegionOk(resultado) {
            var valorTxt = document.getElementById("cph_areaTrabajo_texRegion").value = resultado[0];
            var valorTxt = document.getElementById("cph_areaTrabajo_texSubDireccion").value = resultado[1];
            var valorTxt = document.getElementById("cph_areaTrabajo_texGerenteComercial").value = resultado[2];
            var valorTxt = document.getElementById("cph_areaTrabajo_texEjecuticoComercial").value = resultado[3];
        }
        function Error(error) {
            alert(error.get_message());
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
        <legend>SERVICIO VIDA INSTITUCIONAL</legend>
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
                        <asp:ValidationSummary ID="vsPrsFisica" runat="server"
                            HeaderText="LOS SIGUENTES DATOS SON REQUERIDOS....."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            ShowSummary="true"
                            BackColor="Snow"
                            Width="450"
                            ForeColor="Red"
                            Font-Size="Small"
                            Font-Italic="true"
                            ValidationGroup="vdFisica" />
                        <asp:ValidationSummary ID="vsPrsMoral" runat="server"
                            HeaderText="LOS SIGUENTES DATOS SON REQUERIDOS....."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            ShowSummary="true"
                            BackColor="Snow"
                            Width="450"
                            ForeColor="Red"
                            Font-Size="Small"
                            Font-Italic="true"
                            ValidationGroup="vdMoral" />
                        <table id="tbInfGralTramite" style="width: 100%">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACION DE LA POLIZA</span>
                                    <hr />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">CLAVE PROMOTORIA</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:TextBox ID="texClavePromotoria" runat="server" MaxLength="5" Width="50px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 18%">REGIÓN </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:TextBox ID="texRegion" runat="server" MaxLength="5" Width="150px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">AGENTE</td>
                                <td>
                                    <asp:HiddenField ID="hf_IdPromotoria" runat="server" />
                                    <asp:TextBox ID="txIdAgente" runat="server" MaxLength="5" Width="120px" AutoPostBack="false" onblur="buscaNombreAgente(this)"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txIdAgente" runat="server" FilterType="Numbers" TargetControlID="txIdAgente" />
                                    <asp:RequiredFieldValidator ID="rfv_txIdAgente" runat="server" ErrorMessage="AGENTE" Text="*" ControlToValidate="txIdAgente" ForeColor="Red" Font-Size="16px" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbNombreAgente" runat="server" Text="N.D."></asp:Label>
                                </td>
                                <td >SUBDIRECCIÓN  </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <asp:TextBox ID="texSubDireccion" runat="server" MaxLength="5" Width="150px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">Número de POLIZA</td>
                                <td>
                                    <asp:TextBox ID="txPoliza" runat="server" MaxLength="15"  Width="120px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txPoliza" runat="server" FilterMode="ValidChars" TargetControlID="txPoliza" ValidChars="ABCDEFGHIJKLMNOPQRSTWXYZabcdefghijklmnñopqrstuvwxyz1234567890-/" />
                                    <asp:RequiredFieldValidator ID="rfvPoliza" runat="server" ErrorMessage="NUMERO DE POLIZA REQUERIDO" Text="*" ControlToValidate="txPoliza" ForeColor="Red" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                                <td >GERENTE COMERCIAL   </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:TextBox ID="texGerenteComercial" runat="server" MaxLength="5" Width="150px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 18%">FECHA DE FIRMA SOLICITUD</td>
                                <td>
                                    <asp:TextBox ID="txFechaSol" runat="server" MaxLength="10" Width="120px"  >
                                    </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftetxFechaSol" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                    <asp:RequiredFieldValidator ID="rfvtxFechaSol" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID ="revtxFechaSol" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                </td>
                                <td >EJECUTIVO COMERCIAL    </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:TextBox ID="texEjecuticoComercial" runat="server" MaxLength="5" Width="150px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >NÚMERO DE ORDEN </td>
                                <td>
                                    <asp:TextBox ID="textNumeroOrden" runat="server" MaxLength="15" Width="120px" AutoPostBack="false" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ftb_textNumeroOrden" runat="server" ErrorMessage="NÚMERO DE ORDEN" Text="*" ControlToValidate="textNumeroOrden" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <br />
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3"> </span>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td >CONTRATANTE  </td>
                                <td>
                                    <br />
                                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="15" Width="300px" AutoPostBack="false" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="NÚMERO DE ORDEN" Text="*" ControlToValidate="textNumeroOrden" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td >RFC  </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="15" Width="300px" AutoPostBack="false" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="NÚMERO DE ORDEN" Text="*" ControlToValidate="textNumeroOrden" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td >SUBGRUPO </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                         <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                                <td>TIPO DE PRODUCTO / CATEGORÍA </td>
                                <td><asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                         <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>

                                </td>
                            </tr>
                            <tr>
                                <td>TIPO DE MOVIMIENTO </td>
                                <td>
                                    <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="1">ALTAS</asp:ListItem>
                                        <asp:ListItem Value="2">BAJAS</asp:ListItem>
                                        <asp:ListItem Value="3">RECONOCIMIENTO DE ANTIGÜEDAD</asp:ListItem>
                                        <asp:ListItem Value="4">CARTAS</asp:ListItem>
                                        <asp:ListItem Value="5">LISTADOS</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipoContratante" runat="server" ErrorMessage="TIPO DE CONTRATANTE" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                        </table>
                        <br />
                        <asp:Panel ID="Altas" runat="server" Visible="false">
                            <table id="tblAltas" style="width: 100%">
                                <tr>
                                    <td>NUMERO DE CERTIFICADO </td>
                                    <td>
                                        <asp:TextBox ID="TextBox8" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>NOMBRE DEL ASEGURADO  </td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" MaxLength="13" Width="300px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>PARENTESCO</td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="Hijo">Hijo</asp:ListItem>
                                        <asp:ListItem Value="Conyugue">Conyugue </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>FECHA DE NACIMIENTO </td>
                                    <td><asp:TextBox ID="TextBox4" runat="server" MaxLength="10" Width="120px"  >
                                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID ="RegularExpressionValidator2" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="DD/MM/YYYY"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>GENERO</td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="masculino">MASCULINO </asp:ListItem>
                                        <asp:ListItem Value="femenino">FEMENINO </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>FECHA DE MOVIMIENTO </td>
                                    <td><asp:TextBox ID="TextBox5" runat="server" MaxLength="10" Width="120px"  >
                                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID ="RegularExpressionValidator4" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="DD/MM/YYYY"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>SUELDO</td>
                                    <td>
                                        <asp:TextBox ID="TextBox7" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>FECHA ANTIGÜEDAD</td>
                                    <td>
                                        <asp:TextBox ID="TextBox6" runat="server" MaxLength="10" Width="120px"  >
                                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID ="RegularExpressionValidator5" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Bajas" runat="server" Visible="false">
                            <table id="tblBajas" style="width: 100%">
                                <tr>
                                    <td>NUMERO DE CERTIFICADO </td>
                                    <td>
                                        <asp:TextBox ID="TextBox9" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>NOMBRE DEL ASEGURADO  </td>
                                    <td>
                                        <asp:TextBox ID="TextBox10" runat="server" MaxLength="13" Width="300px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>PARENTESCO</td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="Hijo">Hijo</asp:ListItem>
                                        <asp:ListItem Value="Conyugue">Conyugue </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>FECHA DE NACIMIENTO </td>
                                    <td><asp:TextBox ID="TextBox11" runat="server" MaxLength="10" Width="120px"  >
                                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID ="RegularExpressionValidator9" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="DD/MM/YYYY"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>GENERO</td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="masculino">MASCULINO </asp:ListItem>
                                        <asp:ListItem Value="femenino">FEMENINO </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>FECHA DE MOVIMIENTO </td>
                                    <td><asp:TextBox ID="TextBox12" runat="server" MaxLength="10" Width="120px"  >
                                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID ="RegularExpressionValidator10" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="DD/MM/YYYY"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>SUELDO</td>
                                    <td>
                                        <asp:TextBox ID="TextBox13" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>FECHA ANTIGÜEDAD</td>
                                    <td>
                                        <asp:TextBox ID="TextBox14" runat="server" MaxLength="10" Width="120px"  >
                                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID ="RegularExpressionValidator12" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="ReconAnt" runat="server" Visible="false">
                            <table id="tblReconAnt" style="width: 100%">
                                <tr>
                                    <td>NUMERO DE CERTIFICADO </td>
                                    <td>
                                        <asp:TextBox ID="TextBox15" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>NOMBRE DEL ASEGURADO  </td>
                                    <td>
                                        <asp:TextBox ID="TextBox16" runat="server" MaxLength="13" Width="300px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>PARENTESCO</td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="Hijo">Hijo</asp:ListItem>
                                        <asp:ListItem Value="Conyugue">Conyugue </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>FECHA DE NACIMIENTO </td>
                                    <td><asp:TextBox ID="TextBox17" runat="server" MaxLength="10" Width="120px"  >
                                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID ="RegularExpressionValidator15" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label8" runat="server" Text="DD/MM/YYYY"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>GENERO</td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="masculino">MASCULINO </asp:ListItem>
                                        <asp:ListItem Value="femenino">FEMENINO </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>FECHA DE MOVIMIENTO </td>
                                    <td><asp:TextBox ID="TextBox18" runat="server" MaxLength="10" Width="120px"  >
                                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="FECHA DE FIRMA SOLICITUD" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID ="RegularExpressionValidator16" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label9" runat="server" Text="DD/MM/YYYY"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>SUELDO</td>
                                    <td>
                                        <asp:TextBox ID="TextBox19" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>DOCUMENTOS DE ANTIGUEDAD</td>
                                    <td>
                                        <asp:TextBox ID="TextBox20" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Cartas" runat="server" Visible="false">
                            <table id="tblCartas" style="width: 100%">
                                <tr>
                                    <td>TIPO CARTA </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList9" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="Reconocimiento">Reconocimiento de antigüedad</asp:ListItem>
                                        <asp:ListItem Value="Constancia">Constancia de antigüedad</asp:ListItem>
                                        <asp:ListItem Value="Asegurabilidad">Constancia de asegurabilidad</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>NUMERO DE CERTIFICADO </td>
                                    <td>
                                        <asp:TextBox ID="TextBox21" runat="server" MaxLength="13" Width="190px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>NOMBRE DEL ASEGURADO  </td>
                                    <td>
                                        <asp:TextBox ID="TextBox22" runat="server" MaxLength="13" Width="300px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Listado" runat="server" Visible="false">
                            <table id="tblListado" style="width: 100%">
                                <tr>
                                    <td>TIPO LISTADO </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList10" runat="server" AutoPostBack="True"  Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="EMISION">EMISION</asp:ListItem>
                                        <asp:ListItem Value="VIGOR">EN VIGOR</asp:ListItem>
                                        <asp:ListItem Value="HISTORICO">HISTORICO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="TIPO SUBGRUPO" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnPrsFisica" runat="server" Visible="false">
                            <table id="tblPrsFisica" style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <table id="tblNombre" style="width: 100%">
                                            <tr>
                                                <td style="width: 33%">
                                                    <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="200px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="NOMBRE" Text="*" ControlToValidate="txNombre" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator></td>
                                                <td style="width: 33%">
                                                    <asp:TextBox ID="txApPat" runat="server" MaxLength="64" Width="250px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator ID="rfvApPat" runat="server" ErrorMessage="APELLIDO PATERNO" Text="*" ControlToValidate="txApPat" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator></td>
                                                <td>
                                                    <asp:TextBox ID="txApMat" runat="server" MaxLength="64" Width="250px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nombre(s)</td>
                                                <td>Apellido paterno</td>
                                                <td>Apellido materno</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>RFC</td>
                                    <td>
                                        <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="130px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>CURP</td>
                                    <td>
                                        <asp:TextBox ID="txCurp" runat="server" MaxLength="18" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txCurp" runat="server" FilterMode="ValidChars" TargetControlID="txCurp" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="rev_txCurp" runat="server" ControlToValidate="txCurp" ErrorMessage="EL RFC NO VALIDO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="pnPrsMoral" runat="server" Visible="false">
                            <table id="tblPrsMoral" style="width: 100%">
                                <tr>
                                    <td style="width: 20%">Nombre</td>
                                    <td>
                                        <asp:TextBox ID="txNomMoral" runat="server" MaxLength="64" Width="600px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNomMoral" runat="server" FilterMode="ValidChars" TargetControlID="txNomMoral" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ&,()" />
                                        <asp:RequiredFieldValidator ID="rfvNomMoral" runat="server" ErrorMessage="NOMBRE" Text="*" ControlToValidate="txNomMoral" ForeColor="Red" ValidationGroup="vdMoral"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>RFC</td>
                                    <td>
                                        <asp:TextBox ID="txRfcMoral" runat="server" MaxLength="12" Width="130px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="fteRfcMoral" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="revRfcMoral" runat="server" ControlToValidate="txRfcMoral" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{3}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvRfcMoral" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfcMoral" ForeColor="Red" ValidationGroup="vdMoral"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span style="font-size: 14px; font-weight: bold; color: #007CC3">DETALLE DE LA MODIFICACION A EFECTUAR (INDICAR DATO NUEVO)</span>
                                    <hr /></td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table id="tblObjetivoDelTramite" style="width: 100%">
                            <tr>
                                <td>
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">OBSERVACIONES </span>
                                    <hr />
                                    <asp:TextBox ID="txDetalle" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" runat="server" FilterMode="ValidChars" TargetControlID="txDetalle" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-" />
                                    <asp:RequiredFieldValidator ID="rfvDetalle" runat="server" ControlToValidate="txDetalle" ErrorMessage="DETALLE DE LA MODIFICACIÓN " Font-Size="16px" ForeColor="Red" Text="*" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvBotones" style="text-align: right">
            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="boton" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
