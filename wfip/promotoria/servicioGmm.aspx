<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="servicioGmm.aspx.cs" Inherits="wfip.promotoria.servicioGmm" %>

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
                    var chk = $("input[type='checkbox']:checked").length;
                    if (chk != "") {
                        var rfc = "";
                        if (val == '1') { rfc = document.getElementById("<%=txRfc.ClientID%>").value; }
                        if (val == '2') { rfc = document.getElementById("<%=txRfcMoral.ClientID%>").value; }
                        $.ajax({
                            method: "POST",
                            async: false,
                            data: "{rfc:'" + rfc + "'}",
                            url: "servicioGmm.aspx/TieneTramitesanteriores",
                            contentType: "application/json; charset=utf-8",
                            dataType: "text",
                            success: function (Datos) {
                                var resultado = eval('(' + Datos + ')');
                                if (resultado.d == "1") { continuar = confirm("Ya existen tramites registrado par el RFC, desea continuar?"); }
                                else { continuar = confirm('Esta seguro que desea continuar con el trámite ?'); }
                            },
                            error: function (Datos) { alert("Se genero un problema intente nuevamente..." + Datos); }
                        });
                    } else { alert('Seleccione el tramite a efectuar.'); }
                }
            }
            return continuar;
        }

        //function muestra_caja() {
        //    document.getElementById('caja').style.visibility = 'visible'
        //}

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
        $(document).ready(function () {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            Region(idPromotoria);
            //alert($get('cph_areaTrabajo_lbNombreAgente').value);
            var valorTxt = document.getElementById("cph_areaTrabajo_texClavePromotoria").value = idPromotoria;
            //$("#cph_areaTrabajo_texClavePromotoria").val(total);
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
        <legend>INDIVIDUAL <asp:Label ID="Label2" runat="server"></asp:Label> SERVICIO GASTOS MEDICOS MAYORES </legend>
        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ValidationSummary ID="vsGral" runat="server"
                            HeaderText="Los siguientes datos son requeridos ..."
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
                            HeaderText="Los siguientes datos son requeridos ..."
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
                            HeaderText="Los siguientes datos son requeridos ..."
                            ShowMessageBox="false"
                            DisplayMode="BulletList"
                            ShowSummary="true"
                            BackColor="Snow"
                            Width="450"
                            ForeColor="Red"
                            Font-Size="Small"
                            Font-Italic="true"
                            ValidationGroup="vdMoral" />
                        <table id="tblInfTramite" style="width: 100%">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACION DE LA POLIZA</span>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">Clave promotoria</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:TextBox ID="texClavePromotoria" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 18%">Región </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:TextBox ID="texRegion" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">Agente</td>
                                <td>
                                    <asp:HiddenField ID="hf_IdPromotoria" runat="server" />
                                    <asp:TextBox ID="txIdAgente" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" onblur="buscaNombreAgente(this)"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txIdAgente" runat="server" FilterType="Numbers" TargetControlID="txIdAgente" />
                                    <asp:RequiredFieldValidator ID="rfv_txIdAgente" runat="server" ErrorMessage="Agente" Text="*" ControlToValidate="txIdAgente" ForeColor="Red" Font-Size="16px" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbNombreAgente" runat="server" Text="N.D."></asp:Label>
                                </td>
                                <td >Subdirección</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <asp:TextBox ID="texSubDireccion" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Número de poliza</td>
                                <td>
                                    <asp:TextBox ID="txPoliza" runat="server" MaxLength="16" Width="180px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txPoliza" runat="server" FilterMode="ValidChars" TargetControlID="txPoliza" ValidChars="ABCDEFGHIJKLMNOPQRSTWXYZabcdefghijklmnñopqrstuvwxyz1234567890-/" />
                                    <asp:RequiredFieldValidator ID="rfvPoliza" runat="server" ErrorMessage="Número de poliza" Text="*" ControlToValidate="txPoliza" ForeColor="Red" Font-Size="16px" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                </td>
                                <td >Gerente Comercial </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:TextBox ID="texGerenteComercial" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 18%">Fecha de firma solicitud</td>
                                <td>
                                    <asp:TextBox ID="txFechaSol" runat="server" MaxLength="10" Width="180px"  >
                                    </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftetxFechaSol" runat="server" FilterMode="ValidChars" TargetControlID="txFechaSol" ValidChars="1234567890/" />
                                    <asp:RequiredFieldValidator ID="rfvtxFechaSol" runat="server" ErrorMessage="Fecha de firma solicitud" Text="*" ControlToValidate="txFechaSol" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID ="revtxFechaSol" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaSol" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                                    
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="DD/MM/YYYY"></asp:Label>
                                </td>
                                <td >Ejecutivo comercial</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:TextBox ID="texEjecuticoComercial" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >Número de orden</td>
                                <td>
                                    <asp:TextBox ID="textNumeroOrden" runat="server" MaxLength="15" Width="180px" AutoPostBack="false" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ftb_textNumeroOrden" runat="server" ErrorMessage="Número de orden" Text="*" ControlToValidate="textNumeroOrden" ForeColor="Red" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Tipo de contratante</td>
                                <td>
                                    <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" Width="190px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="1">Persona fisica</asp:ListItem>
                                        <asp:ListItem Value="2">Persona moral</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipoContratante" runat="server" ErrorMessage="Tipo de contratante" Text="*" InitialValue="0" ControlToValidate="cboTipoContratante" ForeColor="Red" ValidationGroup="vdGral" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnPrsFisica" runat="server" Visible="false">
                            <table id="tblPrsFisica" style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <table id="tblNombre" style="width: 100%">
                                            <tr>
                                                <td>Nombre(s)</td>
                                                <td>
                                                    <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="200px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Nombre" Text="*" ControlToValidate="txNombre" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>Apellido paterno</td>
                                                <td>
                                                    <asp:TextBox ID="txApPat" runat="server" MaxLength="64" Width="200px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator ID="rfvApPat" runat="server" ErrorMessage="Apellido paterno" Text="*" ControlToValidate="txApPat" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>Apellido materno</td>
                                                <td>
                                                    <asp:TextBox ID="txApMat" runat="server" MaxLength="64" Width="200px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator ID="rfvApMat" runat="server" ErrorMessage="Apellido materno" Text="*" ControlToValidate="txApMat" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>RFC</td>
                                                <td>
                                                    <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="200px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                                    <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INCORRECTO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>Nacionalidad</td>
                                                <td>
                                                    <asp:DropDownList ID="txNacionalidad" runat="server" Width="210px">
                                                        <asp:ListItem Value="">Seleccionar</asp:ListItem>
                                                        <asp:ListItem Value="Mexicana">Mexicana</asp:ListItem>
                                                        <asp:ListItem Value="Extranjera">Extranjera</asp:ListItem>
                                                    </asp:DropDownList> 
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnPrsMoral" runat="server" Visible="false">
                            <table id="tblPrsMoral" style="width: 100%">
                                <tr>
                                    <td style="width: 15%">Nombre</td>
                                    <td>
                                        <asp:TextBox ID="txNomMoral" runat="server" MaxLength="64" Width="380px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNomMoral" runat="server" FilterMode="ValidChars" TargetControlID="txNomMoral" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ&,()" />
                                        <asp:RequiredFieldValidator ID="rfvNomMoral" runat="server" ErrorMessage="Nombre" Text="*" ControlToValidate="txNomMoral" ForeColor="Red" ValidationGroup="vdMoral"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 18%">RFC</td>
                                    <td>
                                        <asp:TextBox ID="txRfcMoral" runat="server" MaxLength="12" Width="180px"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="fteRfcMoral" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="revRfcMoral" runat="server" ControlToValidate="txRfcMoral" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{3}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvRfcMoral" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfcMoral" ForeColor="Red" ValidationGroup="vdMoral"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table id="tblObjetivoDelTramite" style="width: 100%">
                    <tr>
                        <td colspan="3" style="text-align: center"><span style="font-size: 14px; font-weight: bold; color: #007CC3">TRAMITE A EFECTUAR</span>
                            <hr />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 44%; vertical-align: top">
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">CAMBIOS SIN AFECTACIÓN A PRIMA</span><hr />
                            <asp:CheckBoxList ID="chkGpo1" runat="server" Font-Size="14px"></asp:CheckBoxList><br />
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">REHABILITACIÓN</span><hr />
                            <asp:CheckBoxList ID="chkGpo2" runat="server" Font-Size="14px"></asp:CheckBoxList><br />
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">CAMBIO DE CONDUCTO DE COBRO</span><hr />
                            <asp:CheckBoxList ID="chkGpo3" runat="server" Font-Size="14px"></asp:CheckBoxList><br />
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">ACTUALIZACIÓN DE INFORMACIÓN ARTÍCULO 492</span><hr />
                            <asp:CheckBoxList ID="chkGpo4" runat="server" Font-Size="14px"></asp:CheckBoxList>
                        </td>
                        <td style="width: 2%"></td>
                        <td style="width: 44%; vertical-align: top">
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">CAMBIOS CON AFECTACIÓN A PRIMA</span><hr />
                            <asp:CheckBoxList ID="chkGpo5" runat="server" Font-Size="14px"></asp:CheckBoxList><br />
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">RESCATES, RETIROS Y CANCELACIONES</span><hr />
                            <asp:CheckBoxList ID="chkGpo6" runat="server" Font-Size="14px"></asp:CheckBoxList><br />
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">ACLARACIÓN DE PAGOS</span><hr />
                            <asp:CheckBoxList ID="chkGpo7" runat="server" Font-Size="14px"></asp:CheckBoxList><br />
                            <%--<span style="font-size: 14px; font-weight: bold; color: #007CC3">DUPLICADO DE RECIBO</span><hr />
                            <asp:CheckBoxList ID="chkGpo8" runat="server" Font-Size="14px"></asp:CheckBoxList>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 25px"></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">DETALLE DE LA MODIFICACIÓN A EFECTUAR (INDICAR DATO NUEVO)</span><hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="txDetalle" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" TargetControlID="txDetalle" runat="server"
                                FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-" />
                            <asp:RequiredFieldValidator ID="rfvDetalle" runat="server" ErrorMessage="Detalle de la modificación " Text="*" ControlToValidate="txDetalle" ForeColor="Red" Font-Size="16px" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="dvBotones" style="text-align: right">
            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="boton" OnClick="BtnContinuar_Click" OnClientClick="return  Confirmar();" />
            &nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
    <div id="dvPop">
        <div id="dvpopContenido" style="margin: 0 auto; width: 95%"></div>
    </div>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
