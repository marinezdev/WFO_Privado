<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="EmisionVidaP.aspx.cs" Inherits="wfip.promotoria.EmisionVidaP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var continuar = false;
            if (Page_ClientValidate("") === true) {
                var val = document.getElementById('<%=cboTipoContratante.ClientID%>').selectedIndex;
                if (val === '1') { continuar = (Page_ClientValidate("vdFisica") === true); }
                if (val === '2') { continuar = (Page_ClientValidate("vdMoral") === true); }
                if (val === '0') { alert('Datos incompletos.'); }
                if (continuar === true) {
                    continuar = false;
                    var rfc = "";
                    if (val === '1')
                    {
                        rfc = document.getElementById("<%=txRfc.ClientID%>").value;
                    }
                    if (val === '2')
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
                            if (resultado.d === "1")
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
            if (Activo === '1') {
                var mousex = event.pageX;
                var mousey = event.pageY;
                user.style.left = mousex + 15 + 'px';
                user.style.top = mousey + 'px';
                user.style.visibility = 'visible';
                $('#dvpopContenido').load('Tramites/' + clave + '.html');
            } else {
                user.style.visibility = 'hidden';
            }
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
            if (error !== null)
                alert(error.get_message());
        }

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
            // var valorTxt = document.getElementById("cph_areaTrabajo_texSubDireccion").value = resultado[1];
            var valorTxt = document.getElementById("cph_areaTrabajo_texGerenteComercial").value = resultado[2];
            var valorTxt = document.getElementById("cph_areaTrabajo_texEjecuticoComercial").value = resultado[3];
            var valorTxt = document.getElementById("cph_areaTrabajo_texEjecuticoFront").value = resultado[4];
        }
        function Error(error) {
            alert(error.get_message());
        }


        function calcula(idForm) {
            var valor = parseFloat($("#" + idForm).val());
            var n = new Intl.NumberFormat("es-MX").format(valor);
            var s = "0.00";
            /*
            if (!isNaN(n)) {
                n = Math.round(n * Math.pow(10, 2)) / Math.pow(10, 2);
                s = String(n);
                s += (s.indexOf(".") == -1 ? "." : "") + String(Math.pow(10, 2)).substr(1);
                s = s.substr(0, s.indexOf(".") + 2 + 1);
                
            }
            */
            $("#" + idForm).val(n);
            //$("#" + idForm).val(new Intl.NumberFormat("es-MX").format(s));

            //$("#" + idForm).val(s);
            //$("#" + idForm).val(s);
            
            //$("#" + idForm).val(new Intl.NumberFormat('es-MX').format(s));
            //alert($("#" + idForm).maskMoney({ thousands: '', decimal: '.', allowZero: true}));
            
            /*$()
            object = document.getElementById(idForm);
            

            var n = parseFloat(sVal);
            var s = "0.00";
            if (!isNaN(n)) {
                n = Math.round(n * Math.pow(10, nDec)) / Math.pow(10, nDec);
                s = String(n);
                s += (s.indexOf(".") == -1 ? "." : "") + String(Math.pow(10, nDec)).substr(1);
                s = s.substr(0, s.indexOf(".") + nDec + 1);
            }
            return s;
            */
        }

        function MASK(idForm, mask, format) {
            var n = $("#" + idForm).val();
            if (format === "undefined") format = false;
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
                        while (y && "#0.".indexOf(mask.charAt(y - 1)) === -1) {
                            if (n.charAt(x - 1) !== "-")
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
                if (".-+/*".indexOf(c) + 1 || c !== " " && !isNaN(c)) { num += c; }
            }
            if (isNaN(num)) { num = eval(num); }
            if (num === "") { num = 0; } else { num = parseFloat(num); }
            if (dec !== undefined) {
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
        .botonimagen{
          background-image:url(~/img/file.png);
          background-repeat:no-repeat;
          height:100px;
          width:100px;
          background-position:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="mensajesInformativos" runat="server"></asp:UpdatePanel>
    <fieldset>
        <legend>INDIVIDUAL <asp:Label ID="Label2" runat="server"></asp:Label> EMISIÓN</legend>
        <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                        <table id="tbInfGralTramite" style="width: 100%">
                            <tr style="width: 15%">
                                <td colspan="4" style="text-align: center">
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">PÓLIZA /SEGURO</span>
                                    <hr />
                                    <br /> &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" >
                                    <asp:Panel ID="Limpiar" runat="server" Visible="false">
                                        <asp:Button ID="btnlimpiar" runat="server" CssClass="boton" AutoPostBack="True" CausesValidation="false" OnClick="BtnLimpiar" Text="Limpiar" Visible="true"/>&nbsp;&nbsp;
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 18%; text-align:center;"><asp:Label runat="server" ID="lblRamo" Text="* Ramo" Font-Bold="True"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="TramiteTipPoliza" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TramiteTipPoliza_SelectedIndexChanged" Width="190px" TabIndex="1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="TramiteTipPoliza" ForeColor="Red" ErrorMessage="*" InitialValue="-1" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                                <td colspan="2">
                                    <table id="tbAsegurdas" style="width: 100%;">
                                        <tr>
                                            <td  style="width: 45%"><asp:Label runat="server" ID="Moneda" Text="* Moneda" Font-Bold="True"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="cboMoneda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CalculartSumaAsegurada" Width="210px" TabIndex="1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="cboMoneda" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="SumaAsegurada" Text="* Suma Asegurada Básica" Font-Bold="True"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtSumaAseguradaBasica" OnTextChanged="CalculartSumaAsegurada" onChange="MASK('cph_areaTrabajo_txtSumaAseguradaBasica','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="200px" AutoPostBack="true"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaBasica" ValidChars="0123456789.," />
                                                <asp:RequiredFieldValidator id="RequiredFieldValidator27" InitialValue="0.00"  ControlToValidate="txtSumaAseguradaBasica" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="SumaAseguradaPolizasVigentes" runat="server" Visible="false">
                                        <tr>
                                            <td><asp:Label runat="server" ID="labelSumaAseguradaPolizasVigentes" Text="Suma Asegurada de Pólizas Vigentes " Font-Bold="false"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtSumaAseguradaPolizasVigentes"  OnTextChanged="CalculartSumaAsegurada" onChange="MASK('cph_areaTrabajo_txtSumaAseguradaPolizasVigentes','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="200px" AutoPostBack="true"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaPolizasVigentes" ValidChars="0123456789.," />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="label9" Text="* Prima Total de Acuerdo a Cotización " Font-Bold="true"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtPrimaTotal" OnTextChanged="PrimaTotalGrandesSumas" onChange="MASK('cph_areaTrabajo_txtPrimaTotal','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="200px" AutoPostBack="true"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19"  runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotal" ValidChars="0123456789.," />
                                                <asp:RequiredFieldValidator id="RequiredFieldValidator32" InitialValue="0.00"  ControlToValidate="txtPrimaTotal" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align:center;">
                                                <asp:Label ID="PrimaTotalGrandeSumas" runat="server" Font-Bold="true" ForeColor="#5B8212"></asp:Label>
                                                <asp:Label ID="GrandeSumas" runat="server" Font-Bold="true" ForeColor="#5B8212"></asp:Label>
                                            </td>
                                        </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="SumaAseguradaPolizasVigentesGMM" runat="server" Visible="false">
                                            <tr>
                                                <td><asp:Label runat="server" ID="label10" Text="Prima Total de Acuerdo a Cotización "></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtPrimaTotalGMM" onChange="MASK('cph_areaTrabajo_txtPrimaTotalGMM','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" Width="200px" AutoPostBack="true"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"  runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotalGMM" ValidChars="0123456789.," />
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </td>
                            </tr>
                        </table>
                       <asp:Panel ID="Tramite" runat="server" Visible="false">
                           
                           <asp:DropDownList ID="NuProduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="NuProduct_SelectedIndexChanged" Width="100px" Visible ="false">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                            </asp:DropDownList>
                            
                           <table id="SeleccionTramiteVida" style="width: 100%">
                               <!-- 
                               <tr>
                                   <td style="width: 18%">Número de Movimientos</td>
                                   <td>
                                    aqui es la ubicación del combo de núemero de productos
                                   </td>
                               </tr>-->
                               <tr>
                                   <asp:Panel ID="producto1" runat="server" Visible="true">
                                   <td style="width: 18%"><asp:Label ID="lblProductoRamo" runat="server" Text="* Producto" Font-Bold="true"></asp:Label></td>
                                   <td>
                                        <asp:DropDownList ID="LisProducto1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisProducto1_SelectedIndexChanged" Width="200px">
                                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LisProducto1" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                   </td>
                                   </asp:Panel>
                                </tr>
                                <tr>
                                   <asp:Panel ID="subproducto1" runat="server" Visible="false">
                                   <td><asp:Label ID="lblSubProductoRamo" runat="server" Text="Plan" Font-Bold="true"></asp:Label></td>
                                   <td>
                                        <asp:DropDownList ID="LisSubproducto1" runat="server" AutoPostBack="false" Width="200px">
                                            <asp:ListItem Value=" ">SELECCIONAR</asp:ListItem>
                                        </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LisSubproducto1" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                   </td>
                                   </asp:Panel>
                               </tr>
                               <tr>
                                    <asp:Panel ID="producto2" runat="server" Visible="false">
                                    <td style="width: 18%">Producto</td>
                                    <td>
                                        <asp:DropDownList ID="LisProducto2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisProducto2_SelectedIndexChanged" Width="200px">
                                            <asp:ListItem Value=" ">SELECCIONAR</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    </asp:Panel>
                                   <asp:Panel ID="subproducto2" runat="server" Visible="false">
                                   <td>Plan</td>
                                   <td>
                                        <asp:DropDownList ID="LisSubproducto2" runat="server" AutoPostBack="True" Width="200px">
                                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                   </td>
                                   </asp:Panel>
                               </tr>
                               <tr>
                                   <asp:Panel ID="ActCPDES" runat="server" Visible="false">
                                    <td style="width: 18%" ><asp:Label runat="server" ID="lblCPDES" Text="* CPDES" Font-Bold="True"></asp:Label></td>
                                    <td><asp:DropDownList ID="ActividadCPDES" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActividadCPDES_SelectedIndexChanged" Width="100px">
                                            <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value="True">SI</asp:ListItem>
                                            <asp:ListItem Value="False">NO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" controltovalidate="ActividadCPDES" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </td>
                                    <td style="width: 18%" ></td>
                                    <td></td>
                                    </asp:Panel>
                                </tr>
                            </table>
                            <asp:Panel ID="CPDS" runat="server" Visible="false">
                                <table id="FormularioCPDS" style="width: 100%">
                                    <tr>
                                        <td style="width: 18%"><asp:Label runat="server" ID="lblFolioCPDES" Text="* Folio CPDES" Font-Bold="True"></asp:Label></td>
                                        <td>
                                            <asp:HiddenField ID="HiddenField6" runat="server" />
                                            <asp:TextBox ID="textFolioCPDES" runat="server" MaxLength="15" Width="190px" AutoPostBack="false" Wrap="False"  onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterMode="ValidChars" TargetControlID="textFolioCPDES" ValidChars="0123456789" />
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator12" controltovalidate="textFolioCPDES" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 18%"><asp:Label runat="server" ID="lblEstatusCPDES" Text="* Estatus" Font-Bold="True"></asp:Label></td>
                                        <td><asp:DropDownList ID="EstatusCPDES" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActividadCPDES_SelectedIndexChanged" Width="200px">
                                                <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                                <asp:ListItem Value="Sub-aceptado">SUB-ACEPTADO</asp:ListItem>
                                                <asp:ListItem Value="Manual">MANUAL</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" controltovalidate="EstatusCPDES" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                        </td>
                                        <td></td>
                                        <td></td>
                                   </tr>
                                </table>
                                <br />
                            </asp:Panel>
                        </asp:Panel>
                        <table id="" style="width: 100%">
                            <tr>
                                <td colspan="4" >
                                    Hombres Clave
                                    <asp:CheckBox ID="HombresClave"  runat="server" AutoPostBack="True" Text="Si"  />
                                </td>
                            </tr>
                        </table>
                        <table id="" style="width: 100%">
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN DE LA PÓLIZA</span>
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
                                    <asp:TextBox ID="texRegion" runat="server" MaxLength="5" Width="250px" AutoPostBack="false" Enabled="false" TextMode="MultiLine" Rows="1"></asp:TextBox>
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
                                    <asp:TextBox ID="texGerenteComercial" runat="server" Width="250px" TextMode="MultiLine" Rows="1" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >Solicitud / Número de Orden</td>
                                <td>
                                    <asp:TextBox ID="textNumeroOrden" runat="server" MaxLength="15" Width="180px" AutoPostBack="false" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                    
                                </td>
                                <td >Ejecutivo comercial  </td>
                                <td>
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:TextBox ID="texEjecuticoComercial" runat="server" Width="250px" TextMode="MultiLine" Rows="1"  AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lblTipoContratante" Text="* Tipo de Contratante" Font-Bold="True"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" Width="190px">
                                        <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                        <asp:ListItem Value="Fisica">PERSONA FÍSICA</asp:ListItem>
                                        <asp:ListItem Value="Moral">PERSONA MORAL</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipoContratante" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" Font-Size="16px"></asp:RequiredFieldValidator>
                                </td>
                                <td>Ejecutivo Front</td>
                                <td>
                                    <asp:TextBox ID="texEjecuticoFront" runat="server" Width="250px" TextMode="MultiLine" Rows="1"  AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnPrsFisica" runat="server" Visible="false">
                            <div style="text-align: center">
                                <br />
                                <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN CONTRATANTE</span>
                             </div>
                            <hr />
                            <br />
                            <table id="tblPrsFisica" style="width: 100%">
                                <tr>
                                    <td><asp:Label runat="server" ID="lblNombre" Text="* Nombre(s)" Font-Bold="True"></asp:Label></td>
                                    <td><asp:Label runat="server" ID="lblAPaterno" Text="* Apellido Paterno" Font-Bold="True"></asp:Label></td>
                                    <td><asp:Label runat="server" ID="lblAMaterno" Text="* Apellido Materno" Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="200px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
										<ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txApPat" runat="server" MaxLength="64" Width="200px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" controltovalidate="txApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txApMat" runat="server" MaxLength="64" Width="200px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="txApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label runat="server" ID="Label8" Text="* Sexo" Font-Bold="True"></asp:Label></td>
                                    <td><asp:Label runat="server" ID="lblRFCPFisica" Text="* RFC" Font-Bold="True"></asp:Label></td>
                                    <td>Nacionalidad</td>
                                </tr>
                                <tr>
                                    <td>

                                        <asp:DropDownList ID="txSexo" runat="server" Width="210px">
                                            <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value="Masculino">MASCULINO</asp:ListItem>
                                            <asp:ListItem Value="Femenino">FEMENINO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txSexo" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <asp:Label ID="Label1" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>

                                                    
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="200px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/reload.png" ImageAlign="AbsMiddle" CausesValidation="False" ToolTip="RFC" OnClick="dtFechaNacimiento_OnChanged" />
                                        <br />
                                        <asp:Label ID="textRFCFisica" runat="server" ForeColor="Crimson" ></asp:Label>
                                        
                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="txNacionalidad" runat="server" Width="210px"  AutoPostBack="true" OnSelectedIndexChanged="LisNacionalidad_SelectedIndexChanged">
                                        </dx:ASPxComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txNacionalidad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fecha Nacimiento</td>
                                    <td>Estado</td>
                                    <td style="text-align:center; width: 30%";"><asp:Label ID="textNacionalidad" runat="server" ForeColor="Crimson" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxDateEdit ID="dtFechaNacimiento" runat="server" Theme="Material" EditFormat="Custom" Width="210" AutoPostBack="true" OnDateChanged="dtFechaNacimiento_OnChanged">
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" controltovalidate="dtFechaNacimiento" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboEstado" runat="server" Width="210px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="cboEstado" ErrorMessage="*" Font-Size="16px" ForeColor="Red" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                        <asp:Label ID="Label11" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <hr />
                        </asp:Panel>
                        <asp:Panel ID="pnPrsMoral" runat="server" Visible="false">
                            <div style="text-align: center">
                                <br />
                                <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN CONTRATANTE </span>
                             </div>
                            <hr />
                            <br />
                            <table id="tblPrsMoral" style="width: 100%" >
                                <tr>
                                    <td style="width: 18%"><asp:Label runat="server" ID="lblNombrePMoral" Text="* Nombre" Font-Bold="True"></asp:Label></td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txNomMoral" runat="server" MaxLength="100" Width="380px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNomMoral" runat="server" FilterMode="ValidChars" TargetControlID="txNomMoral" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ&" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txNomMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fecha de Constitución</td>
                                    <td style="width: 20%">
                                        <dx:ASPxDateEdit ID="dtFechaConstitucion" runat="server" Theme="Material" EditFormat="Custom" Width="190" Caption="" AutoPostBack="true" OnDateChanged="dtFechaConstitucion_OnChanged">
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline"/>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td> <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="dtFechaConstitucion" ForeColor="Crimson" errormessage="*" Font-Size="16px"/></td>
                                    <td><asp:Label runat="server" ID="lblRFCPMoral" Text="* RFC" Font-Bold="True"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txRfcMoral" runat="server" MaxLength="12" Width="180px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteRfcMoral" runat="server" FilterMode="ValidChars" TargetControlID="txRfcMoral" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="revRfcMoral" runat="server" ControlToValidate="txRfcMoral" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txRfcMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                        <asp:ImageButton ID="btnCargaRFC" runat="server" ImageUrl="~/img/reload.png" ImageAlign="AbsMiddle" CausesValidation="False" ToolTip="RFC" OnClick="dtFechaConstitucion_OnChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td><asp:Label ID="TextantecedentesRFC" runat="server" ForeColor="Crimson" ></asp:Label></td>
                                </tr>
                            </table>
                            <hr />
                        </asp:Panel>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    ¿El solicitante es el mismo que el contratante?
                                    <asp:CheckBox ID="CheckBox2"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox2_CheckedChanged" Text="Si" Checked="true" />
                                    <asp:CheckBox ID="CheckBox1"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" Text="No" /> 
                                    <br />
                                </td>
                            </tr>
                            <asp:Panel ID="DiferenteContratante" runat="server" Visible="false">
                            <tr>
                                <td colspan="4">
                                    <div style="text-align: center">
                                        <br />
                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACIÓN TITULAR </span>
                                     </div>
                                    <hr />
                                   <table id="TableTitular" style="width: 100%">
                                       <tr>
                                            <td>Nombre(s)</td>
                                            <td>
                                                <asp:TextBox ID="txTiNombre" runat="server" MaxLength="64" Width="200px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txTiNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" controltovalidate="txTiNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                            <td>Apellido paterno</td>
                                            <td>
                                                <asp:TextBox ID="txTiApPat" runat="server" MaxLength="64" Width="200px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txTiApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator14" controltovalidate="txTiApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                            <td>Apellido materno</td>
                                            <td>
                                                <asp:TextBox ID="txTiApMat" runat="server" MaxLength="64" Width="200px" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txTiApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" controltovalidate="txTiApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                       </tr>
                                       <tr>
                                           <td>Nacionalidad</td>
                                            <td>
                                                <dx:ASPxComboBox ID="txTiNacionalidad" runat="server" Width="210px"  AutoPostBack="true" OnSelectedIndexChanged="LisTitNacionalidad_SelectedIndexChanged">
                                                </dx:ASPxComboBox>
                                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" controltovalidate="txTiNacionalidad" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            </td>
                                           <td align="center">Sexo</td>
                                           <td>
                                               <asp:DropDownList ID="txtSexoM" runat="server" Width="210px">
                                                    <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                                    <asp:ListItem Value="Masculino">MASCULINO</asp:ListItem>
                                                    <asp:ListItem Value="Femenino">FEMENINO</asp:ListItem>
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
                                       <tr>
                                           <td>Estado:</td>
                                           <td>
                                                <asp:DropDownList ID="cboEstado2" runat="server" Width="210px"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="cboEstado2" ErrorMessage="Estado" Font-Size="16px" ForeColor="Red" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                                <asp:Label ID="Label12" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                           </td>
                                           <td></td>
                                           <td></td>
                                           <td></td>
                                           <td></td>
                                       </tr>
                                    </table>
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hf_IdPromotoria" runat="server" />
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <label>Agente</label>&nbsp;&nbsp;
                                    
                                    <asp:TextBox ID="txIdAgente" runat="server" MaxLength="10" Width="180px" AutoPostBack="false" onblur="buscaNombreAgente(this)"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterMode="ValidChars" TargetControlID="txIdAgente" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbNombreAgente" runat="server" Text="N.D."></asp:Label>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">OBSERVACIONES</span>
                                    <hr />
                                    <asp:TextBox ID="txDetalle" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" runat="server" FilterMode="ValidChars" TargetControlID="txDetalle" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-,.:+*/?¿+¡\/][{};" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <div id="CitasMedicas">
                            <div style="text-align: right">
                                <asp:Label ID="MSresultado" runat="server" Font-Size="12" ForeColor="#80A721"></asp:Label>
                                <asp:Label ID="MSresultado2" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                            <asp:Button ID="BCitaMedica" runat="server" Text="Programar cita médica" CssClass="boton"  OnClick="CitasMedicas" Visible="false"/>
                            </div>
                            <br />
                            <br />
                            <asp:Panel ID="citamedica" runat="server" Visible="false">
                                <table style="width: 100%">
                                    <tr style="width: 15%">
                                        <td colspan="4" style="text-align: center">
                                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">PROGRAMAR CITA MÉDICA</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%"><asp:Label runat="server" ID="Label4" Text="* Combo" Font-Bold="True"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TextCombo" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td><asp:Label runat="server" ID="Label5" Text="* Cel" Font-Bold="True"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TextCel" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterMode="ValidChars" TargetControlID="TextCel" ValidChars="1234567890" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" controltovalidate="TextCel" errormessage="*" Font-Size="16px" ForeColor="Crimson" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Estado</td>
                                        <td>
                                            <asp:DropDownList ID="LisEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisEstado_SelectedIndexChanged" Width="210px">
                                                <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="LisEstado" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td><asp:Label runat="server" ID="Label6" Text="* Cel Agente / Promotor" Font-Bold="True"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TextCelAgentePromotor" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterMode="ValidChars" TargetControlID="TextCelAgentePromotor" ValidChars="1234567890" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" controltovalidate="TextCelAgentePromotor" errormessage="*" Font-Size="16px" ForeColor="Crimson" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Ciudad</td>
                                        <td>
                                            <asp:DropDownList ID="LisCiudad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisCiudad_SelectedIndexChanged" Width="210px">
                                                <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="LisCiudad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td><asp:Label runat="server" ID="Label7" Text="* Correo electrónico promotoria / agente" Font-Bold="True"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TextCorreo" runat="server" MaxLength="150" Width="200px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterMode="ValidChars" TargetControlID="TextCorreo" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" controltovalidate="TextCorreo" errormessage="*" Font-Size="16px" ForeColor="Crimson" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TextCorreo" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Laboratorio / hospital </td>
                                        <td>
                                            <asp:DropDownList ID="LisLabHospital" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisLabHospital_SelectedIndexChanged" Width="210px">
                                                <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="LisLabHospital" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        </td>
                                        <td>Dirección </td>
                                        <td>
                                            <asp:TextBox ID="TextDireccion" runat="server" Width="200px" TextMode="MultiLine" Rows="1" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><br />Opciones de cita </td>
                                        <td>dd/MM/yyyy Hora hh:mm A.M. / P.M.</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Fecha 1 </td>
                                        <td>
                                           <dx:ASPxDateEdit ID="TextFecha1" runat="server" EditFormat="Custom" Theme="Material" Width="210" AutoPostBack="True" OnDateChanged="fechas_Changed" OnDayCellPrepared="ASPxDateEdit1_CalendarDayCellPrepared">
                                                <TimeSectionProperties>
                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                </TimeSectionProperties>
                                                <CalendarProperties>
                                                    <FastNavProperties DisplayMode="Inline" />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator20" controltovalidate="TextFecha1" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            <asp:Label ID="texFecha1" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                                        </td>
                                        <td>
                                            <div>
                                                <asp:Label ID="lblFechaCitaMedica" runat="server" 
                                                    text="Horario de atención de laboratorio u hospitales: <br/>Lunes a viernes: 7:00 am a 12:00 pm <br/>Sábados: 7:00 a 11:00 am   (Únicamente área metropolitana de CDMX, Monterrey y Guadalajara)" 
                                                    Font-Bold="true"
                                                    ForeColor="red"
                                                    Font-Size="Small"
                                                    >
                                                </asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Fecha 2 </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="TextFecha2" runat="server" EditFormat="Custom" Theme="Material" Width="210" AutoPostBack="True" OnDateChanged="fechas_Changed">
                                                <TimeSectionProperties>
                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                </TimeSectionProperties>
                                                <CalendarProperties>
                                                    <FastNavProperties DisplayMode="Inline" />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator21" controltovalidate="TextFecha2" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                            <asp:Label ID="texFecha2" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Fecha 3 </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="TextFecha3" runat="server" EditFormat="Custom" Theme="Material" Width="210" AutoPostBack="True" OnDateChanged="fechas_Changed">
                                                <TimeSectionProperties>
                                                    <TimeEditProperties EditFormatString="hh:mm tt" />
                                                </TimeSectionProperties>
                                                <CalendarProperties>
                                                    <FastNavProperties DisplayMode="Inline" />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                            &nbsp;<asp:Label ID="texFecha3" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" >
                                            <span style="font-size: 14px; font-weight: bold; color: #007CC3">Notas</span>
                                            <hr />
                                            <asp:TextBox ID="notas" runat="server" Font-Size="14px" TextMode="MultiLine" Width="100%" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                            
                                            <br />
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <tb>
                                            <asp:Button ID="Button1" runat="server" Text="Cancelar Cita Medica" CssClass="boton" OnClick="BtnCancelarCita" CausesValidation="false" />
                                        </tb>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
            </div>
        </div>

        
        <div id="dvBotones" style="text-align: right">
            <asp:Label ID="Mensajes" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
            <asp:Label ID="SumaBasica" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Continuar" CssClass="boton" OnClick="BtnContinuar_Click" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
                </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>