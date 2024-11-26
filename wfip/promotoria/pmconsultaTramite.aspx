<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="pmconsultaTramite.aspx.cs" Inherits="wfip.promotoria.pmconsultaTramite" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v17.2" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v17.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function treeList_CustomDataCallbackCancelar(s, e) {
                document.getElementById('treeListCountCell').innerHTML = e.result;
            }
        function treeList_SelectionChangedCancelar(s, e) {
                window.setTimeout(function () { s.PerformCustomDataCallback(''); }, 0)
            }

        function ventanaSecundaria(URL) {
            // window.open(URL, "_blank", "PDF MetLife", "width=400,height=500");
            // window.open("https://www.w3schools.com", "_blank", "toolbar=yes,scrollbars=yes,resizable=yes,top=500,left=500,width=400,height=400");
            // alert("rmf");
        }

        /*
        function Continuar() {
            var resultado = false;
            if (Page_ClientValidate() == true) {
                //if (confirm('Esta seguro que desea REENVIAR el tramite?')) {
                    resultado = true;
                //}
            } else { alert('LAS OBSERVACIONES SON REQUERIDAS'); }
            return resultado;
        }
        */
        function Descarga(url) {
            window.open(url, 'Download');
        }

        function cierraTodo() {
            popBitacora.Hide();
            pnlPopMotivosCancelar.Hide();
            pnlPopMotivosReconsidera.Hide();
        }

        function CancelaTramite(pParametros, pTipo)
        {
            cierraTodo();
            pnlPopMotivosCancelar.Show();
        }

        function ReconsideraTramite(pParametros, pTipo)
        {
            cierraTodo();
            pnlPopMotivosReconsidera.Show();
        }

        function fnAplicaReconsideracion()
        {
            cierraTodo();
            fxPintaProcesando();
            btnCtrlAplicaReconsideracion.DoClick();
        }

        function fnAplicaCancelacion()
        {
            cierraTodo();
            fxPintaProcesando();
            btnCtrlAplicaCancelacion.DoClick();

            //  var strCadena = "";
            //  var items = lstMotivosHold.GetSelectedItems();
            //  for (var i = items.length - 1; i >= 0; i = i - 1)
            //  {
            //      if (strCadena.length > 0)
            //          strCadena = strCadena + ";" + items[i].value;
            //      else
            //          strCadena = items[i].value;
            //  }
            //  
            //  if (strCadena.length > 0) {
            //      $("#<%=hfCadenaMotivosRechazo.ClientID%>").val(strCadena);
            //      pnlPopMotivosHold.Hide();
            //      cierraTodo();
            //      pnlMsgProcesando.Show();
            //      btnCtrlAplicaHold.DoClick();
            //  }
            //  else {
            //      // pnlPopMovitosRechazo.Hide();
            //      alert('No seleccionó ningún motivo de Hold');
            //  }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID = "mensajesInformativos" runat = "server" ></asp:UpdatePanel>
    <fieldset>
        <legend>CONSULTA TRÁMITE</legend>

        <asp:HiddenField ID="hfModoRechazo" runat="server" />
        <asp:HiddenField ID="hfCadenaMotivosRechazo" runat="server" />

            <div style="width:98%; margin:auto">
                <asp:Label ID="MSresultado2" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                    <asp:Panel ID="citamedica" runat="server" Visible="false">
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
                                                                <asp:Label ID="IdInfoMoneda" runat="server" Visible="false" ></asp:Label>
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
                                                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-bottom: 1px solid #ddd;">
                                                        <asp:Label ID="Infosubproduto" runat="server" Visible="false" ></asp:Label>
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
                                                                    <td ><%#Eval("[PRODUCTO]")%></td>
                                                                    <td ><%#Eval("[SUBPRODUCTO]")%></td>
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
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                    
                <asp:Panel ID="CitaMedicaProspecto" runat="server" Visible="false" >
                    <br /><hr /><br />
                    <table style="width: 100%;">
                    <tr>
                        <td colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"> 
                            INFORMACIÓN DE CITA MÉDICA 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <th colspan="2" scope="col" style="background-color:#1572B7; color:white;">Combo</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td colspan="2"><asp:Label ID="InfoCombo" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Sexo</th>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Edad</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td><asp:Label ID="InfoSexo" runat="server" ></asp:Label></td>
                                    <td><asp:Label ID="InfoEdad" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Celular</th>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Celular Agente Promotor</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td><asp:Label ID="InfoCelular" runat="server" ></asp:Label></td>
                                    <td><asp:Label ID="InfoCelularAgentePromotor" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Correo</th>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Estado</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td><asp:Label ID="InfoCorreo" runat="server" ></asp:Label></td>
                                    <td><asp:Label ID="InfoEstado" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Ciudad</th>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Sucursal</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td><asp:Label ID="InfoCiudad" runat="server" ></asp:Label></td>
                                    <td><asp:Label ID="InfoSucursal" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th colspan="2" scope="col" style="background-color:#1572B7; color:white;">Dirección </th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td colspan="2"><asp:Label ID="InfoDireccion" runat="server" ></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"> 
                                        Fechas
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Fecha 1 </th>
                                    <td style="background-color: White; color: #333333; text-align:center"> <asp:Label ID="InfoFecha1" runat="server" ></asp:Label></td>
                                    <td><asp:RadioButton id="Radio1" Value="1" GroupName="fechas" Text=""  runat="server" Font-Names="Britannic Bold" Font-Size="12px" /></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Fecha 2 </th>
                                    <td style="background-color: White; color: #333333; text-align:center"><asp:Label ID="InfoFecha2" runat="server" ></asp:Label> </td>
                                    <td><asp:RadioButton id="Radio2" Value="2" GroupName="fechas" Text=""  runat="server" Font-Names="Britannic Bold" Font-Size="12px" /></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Fecha 3 </th>
                                    <td style="background-color: White; color: #333333; text-align:center"><asp:Label ID="InfoFecha3" runat="server" ></asp:Label></td>
                                    <td><asp:RadioButton id="Radio3" Value="3" GroupName="fechas" Text=""  runat="server" Font-Names="Britannic Bold" Font-Size="12px"/></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Fecha 4 </th>
                                    <td style="background-color: White; color: #333333; text-align:center"> 
                                        <dx:ASPxDateEdit ID="TextFecha4" runat="server" EditFormat="Custom" Theme="Material" Width="190" >
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="hh:mm tt" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td><asp:RadioButton id="Radio4" Value="4" GroupName="fechas" Text=""  runat="server" Font-Names="Britannic Bold" Font-Size="12px" /></td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align:center;"> 
                                        <br />
                                        <asp:Button ID="GuardarFecha" runat="server" Text="Guardar Cambios" CssClass="boton" OnClick="BtnContinuar_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                
                        <table style="width:100%">
                            <tr>
                                <td style="text-align:right">
                                    <asp:Button ID="btnSuspencionCM" runat="server" CssClass="boton" OnClick="btnSuspencionCM_Click" Text="Carta Suspención" Visible="false" CausesValidation="false" />
                                    <br />&nbsp;
                                </td>
                            </tr>

                        </table>

                    <br /><hr /><br />
                        
                                 <asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>

                                     
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
                                        <td style="width: 20%">
                                            <asp:Label runat="server" ID="Label4" Text="* Combo" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCombo" runat="server" Width="200px" Visible="false"></asp:TextBox>
                                            <asp:Label ID="textEdadCombo" runat="server" Visible="false" ></asp:Label>
                                            <asp:DropDownList ID="listCombosCitaMed" runat="server" AutoPostBack="True" Visible="true" Width="210px">
                                                <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="listCombosCitaMed" ErrorMessage="*" InitialValue="SELECCIONAR" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="Label5" Text="* Cel" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCel" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterMode="ValidChars" TargetControlID="TextCel" ValidChars="1234567890" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" controltovalidate="TextCel" errormessage="*" Font-Size="16px" ForeColor="Crimson" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Estado
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="LisEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisEstado_SelectedIndexChanged" Width="210px">
                                                <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="LisEstado" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="Label6" Text="* Cel Agente / Promotor" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCelAgentePromotor" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterMode="ValidChars" TargetControlID="TextCelAgentePromotor" ValidChars="1234567890" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" controltovalidate="TextCelAgentePromotor" errormessage="*" Font-Size="16px" ForeColor="Crimson" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Ciudad
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="LisCiudad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisCiudad_SelectedIndexChanged" Width="210px">
                                                <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="LisCiudad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="Label7" Text="* Correo electrónico promotoria / agente" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextCorreo" runat="server" MaxLength="150" Width="200px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterMode="ValidChars" TargetControlID="TextCorreo" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ@. = $%*_0123456789-" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" controltovalidate="TextCorreo" errormessage="*" Font-Size="16px" ForeColor="Crimson" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TextCorreo" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Laboratorio / hospital
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="LisLabHospital" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisLabHospital_SelectedIndexChanged" Width="210px">
                                                <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="LisLabHospital" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Dirección
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextDireccion" runat="server" Width="200px" TextMode="MultiLine" Rows="1" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Fecha 1</td>
                                        <td>
                                           <dx:ASPxDateEdit ID="TextFecha1" runat="server" EditFormat="Custom" Theme="Material" Width="210" AutoPostBack="True" >
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
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Fecha 2 </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="TextFecha2" runat="server" EditFormat="Custom" Theme="Material" Width="210" AutoPostBack="True">
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
                                            <dx:ASPxDateEdit ID="TextFecha3" runat="server" EditFormat="Custom" Theme="Material" Width="210" AutoPostBack="True">
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
                                        <td colspan="4" style="text-align:center;">
                                            <asp:Button ID="btnGenerarCita" runat="server" Text="Generar Cita Medica" CssClass="btnVerde" OnClick="btnGenerarCita_Click" />
                                        </td>
                                    </tr>
                                </table>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                   </asp:Panel>
                
            </div>
            <table style="width:98%; margin:auto">
                <tr>
                    <td style="width:75%; text-align:right">
                        <asp:Panel ID="pnbtnMod" runat="server" Visible="false">
                            <fieldset style="text-align:left">
                                <legend>OBSERVACIONES</legend>
                                <asp:Label ID="lblPCI" runat="server" Visible="false" ForeColor="red" Font-Bold="true" Font-Size="Large" Text="El tramite solicitado se suspende, ya que por temas normativos no es posibles contar con información financiera de los clientes"></asp:Label>
                                <asp:TextBox ID="txObservaciones" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                            </fieldset>
                            <asp:Button ID="btnRechazaPoliza" runat="server" Text="Rechazar" CssClass="btnRojo" Height="27px" OnClick="btnRechazaPoliza_Click"  />&nbsp;&nbsp;
                            <asp:Button ID="btnAceptarPoliza" runat="server" Text="Aceptar Poliza" CssClass="btnVerde" Height="27px" OnClick="btnAceptarPoliza_Click"  />&nbsp;&nbsp;
                            <asp:Button ID="btnAceptar" runat="server" Text="Enviar" CssClass="btnVerde" Height="27px" OnClick="btnAceptar_Click"  />&nbsp;&nbsp;
                            <asp:Button ID="btnModificar" runat="server" CssClass="boton" Text="Agregar Documento" OnClick="btnModificar_Click" CausesValidation="false" />
                        </asp:Panel>
                        <asp:Panel ID="pnSuspensionCitaMedica" runat="server" Visible="false">
                            <asp:Button ID="btnAceptarSNCitaMedica" runat="server" Text="Enviar" CssClass="btnVerde" Height="27px" OnClick="btnAceptar_Click"  />&nbsp;&nbsp;
                            <asp:Button ID="btnModificarSNCitaMedica" runat="server" CssClass="boton" Text="Agregar Documento" OnClick="btnModificar_Click" CausesValidation="false" />
                        </asp:Panel>
                    </td>
                    <td>
                        <div style="width:100%; height:100%; text-align:right; vertical-align:bottom">
                            <asp:Button ID="btnReconsideracion" runat="server" Text="Reconsideración" CssClass="btnRojo" Height="27px" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelar" runat="server" Text=" Cancelar " CssClass="btnRojo" Height="27px" />&nbsp;&nbsp;
                            <asp:Button ID="btnNoCancelar" runat="server" Text=" Cancelar " Enabled="false" Visible="false" Height="27px" />&nbsp;&nbsp;
                            <asp:Button ID="btnMuestraInsumos" runat="server" Text="Muestra Insumos" CssClass="boton" CausesValidation="false" Visible="false"/>&nbsp;&nbsp;
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="boton" OnClick="btnRegresar_Click" CausesValidation="false" />
                        </div>
                    </td>
                </tr>
            </table>
            <div style="width:98%; margin:auto">
                <table style ="width:100%" >
                    <tr>
                        <td>
                            <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                        </td>
                        <td  style="text-align: right;">
                            <asp:Label ID ="lblCitasMedicas" runat ="server" Visible="false" >Carta pase Médico</asp:Label>&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Panel ID="Cita" runat="server" Visible="false">
                                <asp:Label ID ="MsCitaMedica" runat="server" ForeColor="Crimson" Visible="false" ></asp:Label>
                                <asp:Button ID="btnCitasMedicas" runat="server" CssClass="boton" OnClick="BtnCitaMedica" Text="Cita Medica" Visible="false"/>
                            </asp:Panel>    
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td  style="text-align: right;">
                            <asp:Label ID ="lblCarta" runat ="server" Visible="false" >Carta</asp:Label>&nbsp;&nbsp;
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxButton ID="btnGeneraCarta"  runat="server" Text="Mostrar Carta" AutoPostBack="true" Visible="false"  CssClass="btnRojo" CausesValidation="False" OnClick="btnGeneraCarta_Click">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td style ="vertical-align:top; background-color:#f0faf7;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;">
                            <asp:Panel ID ="pnObsrMod" runat ="server" Visible="false" >
                                <table style ="width:90%; margin:0 auto">
                                   <tr>
                                       <td><b><asp:Label ID ="lbEtiquetaObsv" runat ="server" ></asp:Label></b></td>
                                       <td style ="text-align :right">
                                           </td>
                                    </tr>
                                    <tr><td></td></tr>
                                    <tr>
                                        <td colspan ="2">
                                            <asp:Repeater ID="rpObsrv" runat="server" >
                                                <ItemTemplate>
                                                    <li><%# Eval("ObservacionPublica")%></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                
                <dx:ASPxButton ID="btnMuestraBitacora" runat="server" Text="Bitácora" AutoPostBack="False" RenderMode="Link" CausesValidation="False" Visible="false"></dx:ASPxButton><br />&nbsp;

                <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="tblDoctos" style="width:100%;">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
    </fieldset>
    
    <dx:ASPxPopupControl ID="popBitacora" runat="server"
                        RenderIFrampopBitacoraeForPopupElements="True"
                        LoadContentViaCallback="OnFirstShow"
                        CloseAction="CloseButton"
                        PopupVerticalAlign="Below"
                        PopupHorizontalAlign="LeftSides"
                        AllowDragging="True"
                        HeaderText="BITÁCORA PÚBLICA"
                        ClientInstanceName="popBitacora"
                        Width="500px"
                        Height="400px"
                        PopupElementID="btnMuestraBitacora" Theme="Aqua">
                        <ContentStyle>
                            <Paddings Padding="5px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <asp:Literal ID="ltBitacora" runat="server"></asp:Literal>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popDocumento" runat="server"
                                RenderIFrameForPopupElements="True"
                                LoadContentViaCallback="OnFirstShow"
                                CloseAction="CloseButton"
                                PopupVerticalAlign="Below"
                                PopupHorizontalAlign="LeftSides"
                                AllowDragging="True"
                                HeaderText="Carta"
                                ClientInstanceName="popDocumento"
                                Width="900px"
                                Height="700px"
                                PopupElementID="btnCarta1" Theme="Aqua">
                                <ContentStyle>
                                    <Paddings Padding="5px" />
                                </ContentStyle>
                                <ContentCollection>
                                    <dx:PopupControlContentControl runat="server">
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popCartaObv" runat="server" 
        RenderIFrameForPopupElements ="True"
        LoadContentViaCallback="OnFirstShow" 
        CloseAction="CloseButton" 
        PopupVerticalAlign="Below" 
        PopupHorizontalAlign="WindowCenter"
        AllowDragging="true"
        ShowFooter="false"
        ShowHeader="true"
        HeaderText="CARTA OBSERVACIONES"
        ClientInstanceName="popCartaObv"
        Width="650px"
        Height="450px"
        PopupElementID="btnObsv" >
        <ContentCollection>
            <dx:PopupControlContentControl ID="popupCartaObv" runat="server">
                <asp:Literal ID="ltPdfPop" runat="server"></asp:Literal>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popCartaRechazo" runat="server" 
        RenderIFrameForPopupElements ="True"
        LoadContentViaCallback="OnFirstShow" 
        CloseAction="CloseButton" 
        PopupVerticalAlign="Below" 
        PopupHorizontalAlign="WindowCenter"
        AllowDragging="true"
        ShowFooter="false"
        ShowHeader="true"
        HeaderText="CARTA DE RECHAZO"
        ClientInstanceName="popCartaRechazo"
        Width="500px"
        Height="500px"
        PopupElementID="btnimpRechazo" >
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupCartaRechazo" runat="server">
                <asp:Literal ID="ltCartaRechazo" runat="server"></asp:Literal>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="PopSuspension" runat="server" 
        RenderIFrameForPopupElements ="True"
        LoadContentViaCallback="OnFirstShow" 
        CloseAction="CloseButton" 
        PopupVerticalAlign="Below" 
        PopupHorizontalAlign="WindowCenter"
        AllowDragging="true"
        ShowFooter="false"
        ShowHeader="true"
        HeaderText="CARTA SUSPENSION"
        ClientInstanceName="PopSuspension"
        Width="500px"
        Height="500px"
        PopupElementID="btnImpPendientes" >
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupCArtaSuspension" runat="server">
                <asp:Literal ID="ltCartaSuspension" runat="server"></asp:Literal>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="popAceptacion" runat="server" 
        RenderIFrameForPopupElements ="True"
        LoadContentViaCallback="OnFirstShow" 
        CloseAction="CloseButton" 
        PopupVerticalAlign="Below" 
        PopupHorizontalAlign="WindowCenter"
        AllowDragging="true"
        ShowFooter="false"
        ShowHeader="true"
        HeaderText="CARTA DE ACEPTACION"
        ClientInstanceName="PopAceptacion"
        Width="500px"
        Height="500px"
        PopupElementID="btnImpAceptacion" >
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupCartaAceptacion" runat="server">
                <asp:Literal ID="ltCartaAceptacion" runat="server"></asp:Literal>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="PopInsumos" runat="server" 
        RenderIFrameForPopupElements ="True"
        LoadContentViaCallback="OnFirstShow" 
        CloseAction="CloseButton" 
        PopupVerticalAlign="Below" 
        PopupHorizontalAlign="RightSides"
        AllowDragging="true"
        ShowFooter="false"
        ShowHeader="true"
        HeaderText="INSUMOS"
        ClientInstanceName="PopInsumos"
        Width="350px"
        Height="150px"
        PopupElementID="btnMuestraInsumos" >
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <asp:Panel ID="pnInsumos" runat="server" Visible="true" ScrollBars="Auto">
                    <fieldset>
                        <legend>ARCHIVOS DE INSUMOS</legend>
                        <asp:Repeater ID="rptInsumos" runat="server" OnItemDataBound="rptInsumos_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblInsumos" style="width:100%" class="display">
                                    <thead>
                                        <th scope="col"></th>
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="color: #333333">
                                    <td style="text-align: center">
                                        <asp:ImageButton ID="ImgExp" runat="server" ImageUrl="~/img/download.png" />
                                    </td>
                                    <td><%# Eval("NmOriginal")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </fieldset>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <asp:HiddenField ID="hdIdTramite" runat="server" />
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>

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

    
    <dx:ASPxButton ID="btnCtrlAplicaReconsideracion" 
                    runat="server" 
                    Text="Aplica Cancelación" 
                    ClientVisible="False" 
                    OnClick="btnCtrlAplicaReconsideracion_Click" 
                    ClientInstanceName="btnCtrlAplicaReconsideracion" 
                    CausesValidation="False">
        <ClientSideEvents Click="function(s, e) 
            { 
                pnlMsgProcesando.Show(); 
            }" />
    </dx:ASPxButton>

    <dx:ASPxPopupControl ID="pnlPopMotivosCancelar" 
					runat="server" 
					CloseAction="CloseButton" 
					HeaderText="Motivos de Cancelación" 
					ShowFooter="True" 
					Theme="iOS" 
					Width="350px" 
					ClientInstanceName="pnlPopMotivosCancelar" 
					Modal="True" 
					PopupHorizontalAlign="WindowCenter" 
					PopupVerticalAlign="WindowCenter" 
					FooterText="">
        <ContentStyle>
		    <Paddings Padding="5px" />
	    </ContentStyle>

        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel ID="pnlCallbackMotCancelar" 
								runat="server" 
								ClientInstanceName="pnlCallbackMotCancelar" 
								Width="100%" 
								OnCallback="pnlCallbackMotCancelar_Callback">

                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxTreeList ID="treeListCancelar" 
                                runat="server" 
                                AutoGenerateColumns="False" 
                                KeyFieldName="Id" 
                                OnCustomDataCallback="treeList_CustomDataCallbackCancelar" 
                                OnDataBound="treeList_DataBoundCancelar" 
                                ParentFieldName="idParent" Width="100%" >

                                <Columns>
                                    <dx:TreeListDataColumn AutoFilterCondition="Default" 
                                        Caption="Motivos de Cancelación" 
                                        FieldName="motivoRechazo" 
                                        ShowInCustomizationForm="True" 
                                        ShowInFilterControl="Default" 
                                        VisibleIndex="0">
                                    </dx:TreeListDataColumn>
                                </Columns>
                                <settingsbehavior allowautofilter="True" expandcollapseaction="NodeDblClick"></settingsbehavior>
                                <settingscustomizationwindow caption="" popuphorizontalalign="RightSides" popupverticalalign="BottomSides"></settingscustomizationwindow>
                                <settingsselection enabled="True"></settingsselection>
                                <settingspopupeditform verticaloffset="-1"></settingspopupeditform>
                                
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
				    <ClientSideEvents Click="function(s, e) { fnAplicaCancelacion()
                        ; }" />
			    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;<br />&nbsp;
		    </div>
	    </FooterTemplate>

    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="pnlPopMotivosReconsidera" 
					runat="server" 
					CloseAction="CloseButton" 
					HeaderText="Reconsideración" 
					ShowFooter="True" 
					Theme="iOS" 
					Width="350px" 
					ClientInstanceName="pnlPopMotivosReconsidera" 
					Modal="True" 
					PopupHorizontalAlign="WindowCenter" 
					PopupVerticalAlign="WindowCenter" 
					FooterText="">
        <ContentStyle>
		    <Paddings Padding="5px" />
	    </ContentStyle>

        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel ID="pnlCallbackMotReconsidera" 
								runat="server" 
								ClientInstanceName="pnlCallbackMotReconsidera" 
								Width="100%" 
								OnCallback="pnlCallbackMotReconsidera_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <asp:Label ID="Label74" runat="server" Text="Motivos para Reconsideración:" Font-Bold="True"></asp:Label>
                            <asp:TextBox ID="txObservacionesReconsidera" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
		    <div style="text-align:right;">
                <br />&nbsp;
			    <dx:ASPxButton ID="btnAplicaReconsideracion" runat="server" AutoPostBack="False" EnableTheming="True" Text=" Aplicar Reconsideración " Theme="Aqua">
				    <ClientSideEvents Click="function(s, e) 
					{ 
						fnAplicaReconsideracion(); 
					}" />
			    </dx:ASPxButton>&nbsp;&nbsp;&nbsp;<br />&nbsp;
		    </div>
	    </FooterTemplate>

    </dx:ASPxPopupControl>

</asp:Content>
