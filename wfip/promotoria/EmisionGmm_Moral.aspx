<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="EmisionGmm_Moral.aspx.cs" Inherits="wfip.promotoria.EmisionGmm_Moral" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var continuar = false;
            if (Page_ClientValidate() == true) {
                if (confirm('Esta seguro que desea continuar con el trámite ?')) {
                    continuar = true;
                }
            }
            return continuar;
        }

        function buscaNombreAgente(txCtrl) {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            PageMethods.daNombreDeAgente(idPromotoria, txCtrl.value, regresaNombreAgente, siErrorNombreAgente);
        }
        function regresaNombreAgente(resultado) {
            $get('cph_areaTrabajo_lbNombreAgente').innerHTML = resultado;
            if (resultado == 'NO EXISTE') { $('#<%=txIdAgente.ClientID%>').val(''); }
        }
        function siErrorNombreAgente(error, userContext, methodName) { if (error != null) { alert(error.get_message()); } }
        $(document).ready(function () {
            //idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;

            alert($get('cph_areaTrabajo_hf_IdPromotoria').value);
            var valorTxt = document.getElementById("cph_areaTrabajo_texClavePromotoria").value = idPromotoria;
            //$("#cph_areaTrabajo_texClavePromotoria").val(total);
        });

        </script>      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <fieldset>
        <legend>SEGURO DE GASTOS MEDICOS MAYORES PERSONA MORAL</legend>
        <asp:ValidationSummary ID="vsDatos" runat="server"
            HeaderText="LOS SIGUENTES DATOS SON REQUERIDOS....."
            ShowMessageBox="false"
            DisplayMode="BulletList"
            ShowSummary="true"
            BackColor="Snow"
            Width="450"
            ForeColor="Red"
            Font-Size="Small"
            Font-Italic="true"/>
        <br />
        <asp:Panel ID="pnContenedor" runat="server" >
            <table style="width: 100%">
                <tr>
                    <td colspan ="4">
                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">DATOS GENERALES </span>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%">AGENTE</td>
                    <td>
                        <asp:HiddenField ID="hf_IdPromotoria" runat="server" />
                        <asp:TextBox ID="txIdAgente" runat="server" MaxLength="5" Width="50px" AutoPostBack="false" onblur="buscaNombreAgente(this)"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txIdAgente" runat="server" FilterType="Numbers" TargetControlID="txIdAgente" />
                        <asp:RequiredFieldValidator ID="rfv_txIdAgente" runat="server" ErrorMessage="Agente" Text="*" ControlToValidate="txIdAgente" ForeColor="Red" Font-Size="16px" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbNombreAgente" runat="server" Text="N.D."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Número de Folio</td>
                    <td>
                        <asp:TextBox ID="txNumFolio" runat="server" MaxLength="16"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fte_txNumFolio" runat="server" FilterMode="ValidChars" TargetControlID="txNumFolio" ValidChars="ABCDEFGHIJKLMNOPQRSTWXYZabcdefghijklmnñopqrstuvwxyz1234567890-/" />
                        <asp:RequiredFieldValidator ID="rfvtxNumFolio" runat="server" ErrorMessage="Numero de Folio" Text="*" ControlToValidate="txNumFolio" ForeColor="Red" Font-Size="16px"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style ="width :20%">Denominación o Razon Social</td>
                    <td>
                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="80%" >
                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Razón social" Text="*" ControlToValidate="txNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>RFC</td>
                    <td>
                        <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="200px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                        <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INCORRECTO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Folio Mercantil</td>
                    <td>
                       <asp:TextBox ID="txFolioMerantil" runat="server" Width="250px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxFolioMerantil" runat="server" FilterMode="ValidChars" TargetControlID="txFolioMerantil" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                    </td>
                </tr>
                <tr>
                    <td>Detalle giro Mercantil, Actividad u objeto Social</td>
                    <td>
                       <asp:TextBox ID="txDetalleGiro" runat="server" Width="90%" MaxLength ="255"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxDetalleGiro" runat="server" FilterMode="ValidChars" TargetControlID="txDetalleGiro" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                        <asp:RequiredFieldValidator ID="rfvtxDetalleGiro" runat="server" ErrorMessage="Detales giro" Text="*" ControlToValidate="txDetalleGiro" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table><br />
            <table style="width: 100%">
                <tr>
                    <td colspan ="4">
                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">NOMBRE COMPLETO DEL APODERADO O REPRESENTANTE LEGAL, ADMINISTRADORES, DIRECTOR, GERENTE GENERAL CON FACULTADES PARA CELEBRAR OPERACIONES DE SEGUROS PARA EMPRESAS NACIONALES.</span>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txRepresentante1" runat="server" MaxLength="18" Width="600px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxRepresentante1" runat="server" FilterMode="ValidChars" TargetControlID="txRepresentante1"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ "/>
                        <asp:RequiredFieldValidator ID="rfvtxRepresentante1" runat="server" ErrorMessage="Representante" Text="*" ControlToValidate="txRepresentante1" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                    <td>
                       <asp:TextBox ID="txNacionalidad" runat="server" Width="250px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxNacionalidad" runat="server" FilterMode="ValidChars" TargetControlID="txNacionalidad"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                        <asp:RequiredFieldValidator ID="rfvtxNacionalidad" runat="server" ErrorMessage="Nacionalidad" Text="*" ControlToValidate="txNacionalidad" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>1. Apellido Paterno Materno Nombre(s)</td>
                    <td>Nacionalidad</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txRepresentante2" runat="server" MaxLength="18" Width="600px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxRepresentante2" runat="server" FilterMode="ValidChars" TargetControlID="txRepresentante2"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                    </td>
                </tr>
                <tr>
                    <td>2. Apellido Paterno Materno Nombre(s)</td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txRepresentante3" runat="server" MaxLength="18" Width="600px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxRepresentante3" runat="server" FilterMode="ValidChars" TargetControlID="txRepresentante3"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                    </td>
                </tr>
                <tr>
                    <td>3. Apellido Paterno Materno Nombre(s)</td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txRepresentante4" runat="server" MaxLength="18" Width="600px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxRepresentante4" runat="server" FilterMode="ValidChars" TargetControlID="txRepresentante4" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ "/>
                    </td>
                </tr>
                <tr>
                    <td>4. Apellido Paterno Materno Nombre(s)</td>
                    <td></td>
                </tr>
            </table><br />
            <asp:UpdatePanel ID ="upDomicilio" runat ="server" >
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td colspan ="4">
                                <span style="font-size: 14px; font-weight: bold; color: #007CC3">DOMICILIO</span>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>Calle/Avenida</td>
                            <td colspan ="3">
                                <asp:TextBox ID="txCalle" runat="server" MaxLength="128" Width="600px" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxCalle" runat="server" FilterMode="ValidChars" TargetControlID="txCalle"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                                <asp:RequiredFieldValidator ID="rfvtxCalle" runat="server" ErrorMessage="calle" Text="*" ControlToValidate="txCalle" ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Numero Exterior</td>
                            <td>
                                <asp:TextBox ID="txNumExt" runat="server" MaxLength="18" Width="200px" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxNumExt" runat="server" FilterMode="ValidChars" TargetControlID="txNumExt"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ -"/>
                                <asp:RequiredFieldValidator ID="rfvtxNumExt" runat="server" ErrorMessage="Numero Exterior" Text="*" ControlToValidate="txNumExt" ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                            <td>Numero Interior</td>
                            <td>
                                <asp:TextBox ID="txNumInt" runat="server" MaxLength="18" Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxNumInt" runat="server" FilterMode="ValidChars" TargetControlID="txNumInt"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ -" />
                            </td>
                        </tr>
                        <tr>
                            <td>Codigo Postal</td>
                            <td>
                                <asp:TextBox ID="txCP" runat="server" MaxLength="5" Width="150px" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftxtxCP" runat="server" FilterMode="ValidChars" TargetControlID="txCP" ValidChars="1234567890" />
                                <asp:RequiredFieldValidator ID="rfvtxCP" runat="server" ErrorMessage="Codigo Postal" Text="*" ControlToValidate="txCP" ForeColor="Red" ></asp:RequiredFieldValidator>
                                <asp:Button ID="btnBuscarCP" Text ="..." runat ="server" OnClick="btnBuscarCP_Click" Width="26px"   CausesValidation ="false" style="height: 26px" />
                            </td>
                        </tr>
                        <tr>
                            <td>Colonia/Barrio</td>
                            <td>
                                <asp:DropDownList ID="dpColonia"  runat ="server" AutoPostBack ="true" OnSelectedIndexChanged="dpColonia_SelectedIndexChanged" Width="95%" ></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvdpColonia" runat="server" ErrorMessage="Colonia" Text="*" ControlToValidate="dpColonia" ForeColor="Red" InitialValue ="0" ></asp:RequiredFieldValidator>
                            </td>
                            <td>Municipio/Delegacion</td>
                            <td>
                                <asp:TextBox ID="txMpio" runat="server"  Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxMpio" runat="server" FilterMode="ValidChars" TargetControlID="txMpio"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ "/>
                                <asp:RequiredFieldValidator ID="rfvtxMpio" runat="server" ErrorMessage="Municipio" Text="*" ControlToValidate="txMpio" ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Ciudad/Poblacion</td>
                            <td>
                                <asp:TextBox ID="txCiudad" runat="server"  Width="200px" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxCiudad" runat="server" FilterMode="ValidChars" TargetControlID="txCiudad"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ "/>
                                <asp:RequiredFieldValidator ID="rfvtxCiudad" runat="server" ErrorMessage="Ciudad" Text="*" ControlToValidate="txCiudad" ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                            <td>Estado</td>
                            <td>
                                <asp:TextBox ID="txEstado" runat="server" Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxEstado" runat="server" FilterMode="ValidChars" TargetControlID="txEstado"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                                <asp:RequiredFieldValidator ID="rfvtxEstado" runat="server" ErrorMessage="Estado" Text="*" ControlToValidate="txEstado" ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Pais</td>
                            <td>
                                <asp:TextBox ID="txPais" runat="server" Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxPais" runat="server" FilterMode="ValidChars" TargetControlID="txPais"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                                <asp:RequiredFieldValidator ID="rfvtxPais" runat="server" ErrorMessage="Pais" Text="*" ControlToValidate="txPais" ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                            <td>Zona Fronteriza?</td>
                            <td>
                                <asp:DropDownList ID ="dpZonaFonteriza" runat ="server" >
                                    <asp:ListItem Value ="3" Text ="Seleccionar"></asp:ListItem>
                                    <asp:ListItem Value ="0" Text ="NO"></asp:ListItem>
                                    <asp:ListItem Value ="1" Text ="SI"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvdpZonaFonteriza" runat="server" ErrorMessage="Zona fronteriza" Text="*" ControlToValidate="dpZonaFonteriza" ForeColor="Red"  InitialValue ="3"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table style="width: 100%">
                <tr>
                    <td colspan ="4">
                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">TELEFONOS DE CONTACTO</span>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>Telefono 1</td>
                    <td>
                        <asp:TextBox ID="txTelUno" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtxTelUno" runat="server" FilterMode="ValidChars" TargetControlID="txTelUno" ValidChars="1234567890" />
                        <asp:RequiredFieldValidator ID="rfvtxTelUno" runat="server" ErrorMessage="Telefono Uno" Text="*" ControlToValidate="txTelUno" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                    <td>Telefono 2</td>
                    <td>
                        <asp:TextBox ID="txTelefonoDos" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxTelefonoDos" runat="server" FilterMode="ValidChars" TargetControlID="txTelefonoDos" ValidChars="1234567890" />
                    </td>
                </tr>
                <tr>
                    <td>Correo electronico</td>
                    <td>
                        <asp:TextBox ID="txCorreo" runat="server" MaxLength="60" Width="300px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxCorreo" runat="server" FilterMode="ValidChars" TargetControlID="txCorreo" ValidChars="@abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890." />
                        <asp:RequiredFieldValidator ID="rfvtxCorreo" runat="server" ErrorMessage="Correo" Text="*" ControlToValidate="txCorreo" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:Panel><br />

        <div id="dvBotones" style="text-align: right">
            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="boton" OnClick="BtnContinuar_Click" OnClientClick="return  Confirmar();" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
