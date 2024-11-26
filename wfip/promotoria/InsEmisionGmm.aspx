<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="InsEmisionGmm.aspx.cs" Inherits="wfip.promotoria.InsEmisionGmm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var continuar = false;
            if (Page_ClientValidate("") == true) {
                var val = document.getElementById('<%=cboTipoContratante.ClientID%>').selectedIndex;
                if (val == '1') { continuar = (Page_ClientValidate("vdFisica") == true); }
                if (val == '2') { continuar = (Page_ClientValidate("vdMoral") == true); }
                if (val == '0') { alert('Datos incompletos.'); }
                if (continuar == true) {
                    continuar = false;
                    var rfc = "";
                    if (val == '1')
                    {
                        rfc = document.getElementById("<%=txRfc.ClientID%>").value;
                    }
                    if (val == '2')
                    {
                        rfc = document.getElementById("<%=txRfcMoral.ClientID%>").value;
                    }
                    $.ajax({
                        method: "POST",
                        async: false,
                        data: "{rfc:'" + rfc + "'}",
                        url: "servicioVida.aspx/TieneTramitesanteriores",
                        contentType: "application/json; charset=utf-8",
                        dataType: "text",
                        success: function (Datos)
                        {
                            var resultado = eval('(' + Datos + ')');
                            if (resultado.d == "1")
                            {
                                continuar = confirm("Ya existen trámites registrados para el RFC, desea continuar?");
                            }
                            else
                            {
                                continuar = confirm('Esta seguro que desea continuar con el trámite ?');
                            }
                        },
                        error: function (Datos)
                        {
                            alert("Se genero un problema intente nuevamente..." + Datos);
                        }
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

        function regresaNombreAgente(resultado)
        {
            $get('cph_areaTrabajo_lbNombreAgente').innerHTML = resultado;
        }

        function siErrorNombreAgente(error, userContext, methodName)
        {
            if (error != null)
            {
                alert(error.get_message());
            }
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
            // var valorTxt = document.getElementById("cph_areaTrabajo_texSubDireccion").value = resultado[1];
            var valorTxt = document.getElementById("cph_areaTrabajo_texGerenteComercial").value = resultado[2];
            var valorTxt = document.getElementById("cph_areaTrabajo_texEjecuticoComercial").value = resultado[3];
        }
        function Error(error) {
            alert(error.get_message());
        }


        function calcula(idForm) {
            var valor = parseFloat($("#" + idForm).val());
            var n = new Intl.NumberFormat("es-MX").format(valor);
            var s = "0.00";

            $("#" + idForm).val(n);
            
        }



        function MASK(idForm, mask, format) {
            var n = $("#" + idForm).val();
            if (format == "undefined") format = false;
            if (format || NUM(n)) {
                dec = 0, point = 0;
                x = mask.indexOf(".") + 1;
                if (x) { dec = mask.length - x; }

                if (dec) {
                    n = NUM(n, dec) + "";
                    x = n.indexOf(".") + 1;
                    if (x) { point = n.length - x; } else { n += "."; }
                } else {
                    n = NUM(n, 0) + "";
                }
                for (var x = point; x < dec; x++) {
                    n += "0";
                }
                x = n.length, y = mask.length, XMASK = "";
                while (x || y) {
                    if (x) {
                        while (y && "#0.".indexOf(mask.charAt(y - 1)) == -1) {
                            if (n.charAt(x - 1) != "-")
                                XMASK = mask.charAt(y - 1) + XMASK;
                            y--;
                        }
                        XMASK = n.charAt(x - 1) + XMASK, x--;
                    } else if (y && "$0".indexOf(mask.charAt(y - 1)) + 1) {
                        XMASK = mask.charAt(y - 1) + XMASK;
                    }
                    if (y) { y-- }
                }
            } else {
                XMASK = "";
            }
            $("#" + idForm).val(XMASK);
            return XMASK;
        }

        // Convierte una cadena alfanumérica a numérica (incluyendo formulas aritméticas)
        //
        // s   = cadena a ser convertida a numérica
        // dec = numero de decimales a redondear
        //
        // La función devuelve el numero redondeado

        function NUM(s, dec) {
            for (var s = s + "", num = "", x = 0; x < s.length; x++) {
                c = s.charAt(x);
                if (".-+/*".indexOf(c) + 1 || c != " " && !isNaN(c)) { num += c; }
            }
            if (isNaN(num)) { num = eval(num); }
            if (num == "") { num = 0; } else { num = parseFloat(num); }
            if (dec != undefined) {
                r = .5; if (num < 0) r = -r;
                e = Math.pow(10, (dec > 0) ? dec : 0);
                return parseInt(num * e + r) / e;
            } else {
                return num;
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
    <asp:UpdatePanel ID="mensajesInformativos" runat="server"></asp:UpdatePanel>
    <fieldset>
        <legend>INSTITUCIONAL <asp:Label ID="Label2" runat="server"></asp:Label> EMISIÓN GASTOS MÉDICOS CONVERSIONES</legend>
        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="tbInfGralTramite" style="width: 100%">
                            <tr style="width: 15%">
                                <td colspan="4" style="text-align: center">
                                    
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">PÓLIZA /SEGURO</span>
                                    <hr />
                                    <br /> &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Limpiar" runat="server" Visible="false">
                                        <asp:Button ID="btnlimpiar" runat="server" CssClass="boton" AutoPostBack="True" OnClick="BtnLimpiar" Text="Limpiar" Visible="true"/>&nbsp;&nbsp;
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="Label8" Text="Ramo 2006" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 18%"><asp:Label runat="server" ID="lblRamo" Text="* Producto" Font-Bold="True"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="TramiteTipPoliza" runat="server" Width="190px">
                                        <asp:ListItem Value="">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="Conversion">Conversión</asp:ListItem>
                                        <asp:ListItem Value="Proteccion">Protección Garantizada</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" id="reqName" controltovalidate="TramiteTipPoliza" ForeColor="Crimson" errormessage="*" Font-Size="16px" />
                                </td>
                                <td colspan="2">
                                    <table id="tbAsegurdas" style="width: 100%">
                                        <tr>
                                            <td><asp:Label runat="server" ID="Moneda" Text="* Moneda" Font-Bold="True"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="cboMoneda" runat="server" Width="210px" Enabled="false" ></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="SumaAsegurada" Text="* Suma Asegurada Básica" Font-Bold="True"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtSumaAseguradaBasica" onChange="MASK('cph_areaTrabajo_txtSumaAseguradaBasica','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="70%" AutoPostBack="false"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaBasica" ValidChars="0123456789.," />
                                                <asp:RequiredFieldValidator id="RequiredFieldValidator27" InitialValue="0.00"  ControlToValidate="txtSumaAseguradaBasica" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Prima Total de Acuerdo a Cotización</td>
                                            <td>
                                                <asp:TextBox ID="txtPrimaTotal" onChange="MASK('cph_areaTrabajo_txtPrimaTotal','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="70%" AutoPostBack="false"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19"  runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotal" ValidChars="0123456789.," />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="" style="width: 100%">
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
                                    <asp:TextBox ID="texClavePromotoria" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="texClave" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 18%">Región</td>
                                <td>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:TextBox ID="texRegion" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 18%"><asp:Label runat="server" ID="lblFechaSolicitud" Text="* Fecha Solicitud" Font-Bold="True"></asp:Label></td>
                                <td>
                                    <dx:ASPxDateEdit ID="dtFechaSolicitud" runat="server" Theme="Material" EditFormat="Custom" Width="190" Caption="" AutoPostBack="true" OnDateChanged="dtFechaSolicitud_OnChanged">
                                        <TimeSectionProperties>
                                            <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                        </TimeSectionProperties>
                                        <CalendarProperties>
                                            <FastNavProperties DisplayMode="Inline" />
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="dtFechaSolicitud" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </td>
                                <td >
                                    
                                    Gerente comercial </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:TextBox ID="texGerenteComercial" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >Solicitud / Número de Orden</td>
                                <td>
                                    <asp:TextBox ID="textNumeroOrden" runat="server" MaxLength="15" Width="180px" AutoPostBack="false" ></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ0123456789" />
                                </td>
                                <td >Ejecutivo comercial  </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:TextBox ID="texEjecuticoComercial" runat="server" MaxLength="5" Width="180px" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lblTipoContratante" Text="* Tipo de Contratante" Font-Bold="True"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" Width="190px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="Fisica">Persona fisica</asp:ListItem>
                                        <asp:ListItem Value="Moral">Persona moral</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipoContratante" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnPrsFisica" runat="server" Visible="false">
                            <hr />
                            <table id="tblPrsFisica" style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        
                                        <table id="tblNombre" style="width: 100%">
                                            <tr>
                                                <td><asp:Label runat="server" ID="lblNombre" Text="* Nombre(s)" Font-Bold="True"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="lblAPaterno" Text="* Apellido Paterno" Font-Bold="True"></asp:Label></td>
                                                <td><asp:Label runat="server" ID="lblAMaterno" Text="* Apellido Materno" Font-Bold="True"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="200px" AutoPostBack="True" OnTextChanged="dtFechaNacimiento_OnChanged"></asp:TextBox>
													<ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txApPat" runat="server" MaxLength="64" Width="200px" AutoPostBack="True" OnTextChanged="dtFechaNacimiento_OnChanged"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" controltovalidate="txApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txApMat" runat="server" MaxLength="64" Width="200px" AutoPostBack="True" OnTextChanged="dtFechaNacimiento_OnChanged"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="txApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Sexo</td>
                                                <td><asp:Label runat="server" ID="lblRFCPFisica" Text="* RFC" Font-Bold="True"></asp:Label></td>
                                                <td>Nacionalidad</td>
                                            </tr>
                                            <tr>
                                                <td>

                                                    <asp:DropDownList ID="txSexo" runat="server" AutoPostBack="True" Width="210px">
                                                        <asp:ListItem Value="">Selecionar</asp:ListItem>
                                                        <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                                                        <asp:ListItem Value="Femenino">Femenino</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txSexo" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                                    &nbsp;
                                                    <asp:Label ID="Label1" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>

                                                    
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txRfc" OnTextChanged="antecedentesRFC" AutoPostBack="True" runat="server" MaxLength="13" Width="200px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                                    <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:Label ID="textRFCFisica" runat="server" ForeColor="Crimson" ></asp:Label>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="txNacionalidad" runat="server" Width="210px" AutoPostBack="true" SelectedIndex="137" OnSelectedIndexChanged="LisNacionalidad_SelectedIndexChanged">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txNacionalidad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Fecha Nacimiento</td>
                                                <td></td>
                                                <td style="text-align:center; width: 30%";"><asp:Label ID="textNacionalidad" runat="server" ForeColor="Crimson" ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxDateEdit ID="dtFechaNacimiento" runat="server" Theme="Material" EditFormat="Custom" Width="210" Caption="" AutoPostBack="true" OnDateChanged="dtFechaNacimiento_OnChanged">
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                        </TimeSectionProperties>
                                                        <CalendarProperties>
                                                            <FastNavProperties DisplayMode="Inline" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" controltovalidate="dtFechaNacimiento" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                        </asp:Panel>
                        <asp:Panel ID="pnPrsMoral" runat="server" Visible="false">
                            <hr />
                            <table id="tblPrsMoral" style="width: 100%">
                                <tr>
                                    <td style="width: 18%"><asp:Label runat="server" ID="lblNombrePMoral" Text="* Nombre" Font-Bold="True"></asp:Label></td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txNomMoral" runat="server" MaxLength="64" Width="380px" AutoPostBack="true" OnTextChanged="dtFechaConstitucion_OnChanged"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNomMoral" runat="server" FilterMode="ValidChars" TargetControlID="txNomMoral" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ&,()" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txNomMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fecha de Constitución</td>
                                    <td>
                                        <dx:ASPxDateEdit ID="dtFechaConstitucion" runat="server" Theme="Material" EditFormat="Custom" Width="190" Caption="" AutoPostBack="true" OnDateChanged="dtFechaConstitucion_OnChanged">
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline"/>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="dtFechaConstitucion" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </td>
                                    <td><asp:Label runat="server" ID="lblRFCPMoral" Text="* RFC" Font-Bold="True"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txRfcMoral" OnTextChanged="antecedentesRFC" AutoPostBack="True" runat="server" MaxLength="12" Width="180px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteRfcMoral" runat="server" FilterMode="ValidChars" TargetControlID="txRfcMoral" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="revRfcMoral" runat="server" ControlToValidate="txRfcMoral" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txRfcMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                        <asp:Label ID="TextantecedentesRFC" runat="server" ForeColor="Crimson" ></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                        </asp:Panel>
                        <table style="width: 100%">
                            <tr>
                                <td colspan="4">
                                    ¿El solicitante es el mismo que el contratante?
                                    <asp:CheckBox ID="CheckBox2"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox2_CheckedChanged" Text="Si" Checked="true" />
                                    <asp:CheckBox ID="CheckBox1"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" Text="No" /> 
                                    <br />
                                </td>
                            </tr>
                            <asp:Panel ID="DiferenteContratante" runat="server" Visible="false">
                            <tr>
                                <td colspan="4">
                                   <table id="TableTitular" style="width: 100%">
                                       <tr>
                                            <td>Nombre(s)</td>
                                            <td>
                                                <asp:TextBox ID="txTiNombre" runat="server" MaxLength="64" Width="200px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txTiNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" controltovalidate="txTiNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                            <td>Apellido paterno</td>
                                            <td>
                                                <asp:TextBox ID="txTiApPat" runat="server" MaxLength="64" Width="200px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txTiApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator14" controltovalidate="txTiApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                            <td>Apellido materno</td>
                                            <td>
                                                <asp:TextBox ID="txTiApMat" runat="server" MaxLength="64" Width="200px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txTiApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" controltovalidate="txTiApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                       </tr>
                                       <tr>
                                           <td>Nacionalidad</td>
                                            <td>
                                                <dx:ASPxComboBox ID="txTiNacionalidad" runat="server" Width="210px" AutoPostBack="true" SelectedIndex="137" OnSelectedIndexChanged="LisTitNacionalidad_SelectedIndexChanged">
                                                </dx:ASPxComboBox>
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" controltovalidate="txTiNacionalidad" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                           <td align="center">Sexo</td>
                                           <td>
                                               <asp:DropDownList ID="txtSexoM" runat="server" AutoPostBack="True" Width="210px">
                                                    <asp:ListItem Value="">Selecionar</asp:ListItem>
                                                    <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                                                    <asp:ListItem Value="Femenino">Femenino</asp:ListItem>
                                                </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txtSexoM" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                                &nbsp;
                                                <asp:Label ID="Label3" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                           </td>
                                           <td>Fecha Nacimiento </td>
                                           <td>
                                               <dx:ASPxDateEdit ID="dtFechaNacimientoTitular" runat="server" Theme="Material" EditFormat="Custom" Width="210" Caption="" >
                                                    <TimeSectionProperties>
                                                        <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                                    </TimeSectionProperties>
                                                    <CalendarProperties>
                                                        <FastNavProperties DisplayMode="Inline"/>
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator26" controltovalidate="dtFechaNacimientoTitular" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="6">
                                               <asp:Label ID="textTitularNacionalidad" runat="server" ForeColor="Crimson" ></asp:Label>
                                           </td>
                                       </tr>
                                    </table>
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr>
                                <td colspan="4">
                                    
                                    <label>Agente</label>&nbsp;&nbsp;
                                    <asp:HiddenField ID="hf_IdPromotoria" runat="server" />
                                    <asp:TextBox ID="txIdAgente" runat="server" MaxLength="10" Width="180px" AutoPostBack="false" onblur="buscaNombreAgente(this)"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterMode="ValidChars" TargetControlID="txIdAgente" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbNombreAgente" runat="server" Text="N.D."></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">OBSERVACIONES</span>
                                    <hr />
                                    <asp:TextBox ID="txDetalle" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" runat="server" FilterMode="ValidChars" TargetControlID="txDetalle" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-" />
                                    <br />
                                </td>
                            </tr>
                        </table>
					</ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvBotones" style="text-align: right">
            <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Continuar" CssClass="boton" OnClick="BtnContinuar_Click" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
