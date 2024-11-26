<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="EmisionGmmP.aspx.cs" Inherits="wfip.promotoria.EmisionGmmP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Confirmar() {

            var continuar = false;
            if (Page_ClientValidate("vdGral") == true) {
                var val = document.getElementById('<%=cboTipoContratante.ClientID%>').selectedIndex;
                if (val == '1') { continuar = (Page_ClientValidate("vdFisica") == true); }
                if (val == '2') { continuar = (Page_ClientValidate("vdMoral") == true); }
                if (val == '0') { alert('Datos incompletos.'); }
                if (continuar == true) {
                    continuar = false;
                        var rfc = "";
                        if (val == '1') { rfc = document.getElementById("<%=txRfc.ClientID%>").value; }
                        if (val == '2') { rfc = document.getElementById("<%=txRfcMoral.ClientID%>").value; }
                        $.ajax({
                            method: "POST",
                            async: false,
                            data: "{rfc:'" + rfc + "'}",
                            url: "servicioVida.aspx/TieneTramitesanteriores",
                            contentType: "application/json; charset=utf-8",
                            dataType: "text",
                            success: function (Datos) {
                                var resultado = eval('(' + Datos + ')');
                                if (resultado.d == "1") { continuar = confirm("Ya existen trámites registrados para el RFC, desea continuar?"); }
                                else { continuar = confirm('Esta seguro que desea continuar con el trámite ?'); }
                            },
                            error: function (Datos) { alert("Se genero un problema intente nuevamente..." + Datos); }
                        });

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
            } else { user.style.visibility = 'hidden'; }
        }

        function buscaNombreAgente(txCtrl) {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            PageMethods.daNombreDeAgente(idPromotoria, txCtrl.value, regresaNombreAgente, siErrorNombreAgente);
        }
        function regresaNombreAgente(resultado) { $get('cph_areaTrabajo_lbNombreAgente').innerHTML = resultado; }
        function siErrorNombreAgente(error, userContext, methodName) { if (error != null) { alert(error.get_message()); } }

        /*function regresaRegion() {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            alert(PageMethods.daNombreDeRegion(idPromotoria));
        }*/

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
        <legend>EMISIÓN GASTOS MÉDICOS INDIVIDUAL </legend>
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
                                <td>Tipo de contratante</td>
                                <td>
                                    <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="1">Persona fisica</asp:ListItem>
                                        <asp:ListItem Value="2">Persona moral</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipoContratante" runat="server" ErrorMessage="TIPO DE CONTRATANTE" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                        </table>
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
                            </table>
                        </asp:Panel>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvBotones" style="text-align: right">
            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="boton" OnClientClick="return  Confirmar();"  />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
