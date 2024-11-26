<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="EmisionVida_Fisica.aspx.cs" Inherits="wfip.promotoria.EmisionVida_Fisica" %>
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
    </script>      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <fieldset>
        <legend>SEGURO DE VIDA INDIVIDUAL PERSONA FISICA</legend>
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
        <asp:Panel ID="pnContenedor" runat="server">
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
                        <asp:RequiredFieldValidator ID="rfv_txIdAgente" runat="server" ErrorMessage="Agente" Text="*" ControlToValidate="txIdAgente" ForeColor="Red" Font-Size="16px" ></asp:RequiredFieldValidator>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbNombreAgente" runat="server" Text="N.D."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Número de Solicitud CP DES</td>
                    <td>
                        <asp:TextBox ID="txNumSolCpDes" runat="server" MaxLength="16"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxNumSolCpDes" runat="server" FilterMode="ValidChars" TargetControlID="txNumSolCpDes" ValidChars="ABCDEFGHIJKLMNOPQRSTWXYZabcdefghijklmnñopqrstuvwxyz1234567890-/" />
                        <asp:RequiredFieldValidator ID="rfvtxNumSolCpDes" runat="server" ErrorMessage="Numero de solicitud CP DES" Text="*" ControlToValidate="txNumSolCpDes" ForeColor="Red" Font-Size="16px"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>Número de Solicitud</td>
                    <td>
                        <asp:TextBox ID="txNumSolicitud" runat="server" MaxLength="16"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fte_txNumSolicitud" runat="server" FilterMode="ValidChars" TargetControlID="txNumSolicitud" ValidChars="ABCDEFGHIJKLMNOPQRSTWXYZabcdefghijklmnñopqrstuvwxyz1234567890-/" />
                        <asp:RequiredFieldValidator ID="rfvtxNumSolicitud" runat="server" ErrorMessage="Numero de solicitud" Text="*" ControlToValidate="txNumSolicitud" ForeColor="Red" Font-Size="16px"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style ="width :20%">Nombre(s) </td>
                    <td>
                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="300px" >
                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Nombre" Text="*" ControlToValidate="txNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Apellido paterno</td>
                    <td>
                        <asp:TextBox ID="txApPat" runat="server" MaxLength="64" Width="300px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                        <asp:RequiredFieldValidator ID="rfvApPat" runat="server" ErrorMessage="Apellido paterno" Text="*" ControlToValidate="txApPat" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Apellido materno</td>
                    <td>
                        <asp:TextBox ID="txApMat" runat="server" MaxLength="64" Width="300px">
                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                    </td>
                </tr>
                <tr>
                    <td  style ="width :15%">Fecha Nacimiento</td>
                    <td>
                        <asp:TextBox ID="txFechaNac" runat="server" MaxLength="10" Width="100px"  >
                        </asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="ftetxFechaNac" runat="server" FilterMode="ValidChars" TargetControlID="txFechaNac" ValidChars="1234567890/" />
                        <asp:RequiredFieldValidator ID="rfvtxFechaNac" runat="server" ErrorMessage="Fecha Nacimiento" Text="*" ControlToValidate="txFechaNac" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID ="revtxFechaNac" ValidationExpression="^([0-9]|0[1-9]|[12][0-9]|3[01])\/([0-9]|0[1-9]|1[012])\/(19|20)\d\d$" ControlToValidate="txFechaNac" ErrorMessage="Feha invalida usa DD/MM/YYYY" runat="server" Text="*"  ForeColor="Red" > </asp:RegularExpressionValidator>
                    </td>
                    <td  style ="width :15%">Edad</td>
                    <td>
                        <asp:TextBox ID="txEdad" runat="server" MaxLength="2" Width="100px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxEdad" runat="server" FilterMode="ValidChars" TargetControlID="txEdad" ValidChars="0123456789" />
                        <asp:RequiredFieldValidator ID="rfvtxEdad" runat="server" ErrorMessage="Edad" Text="*" ControlToValidate="txEdad" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>RFC</td>
                    <td>
                        <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="200px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                        <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC Incorrecto" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                    <td>CURP</td>
                    <td>
                        <asp:TextBox ID="txCurp" runat="server" MaxLength="18" Width="200px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txCurp" runat="server" FilterMode="ValidChars" TargetControlID="txCurp" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                        <asp:RegularExpressionValidator ID="rev_txCurp" runat="server" ControlToValidate="txCurp" ErrorMessage="El CURP no valido" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]"></asp:RegularExpressionValidator>                        <asp:RequiredFieldValidator ID="rfvtxCurp" runat="server" ErrorMessage="CURP" Text="*" ControlToValidate="txCurp" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Sexo</td>
                    <td>
                        <asp:DropDownList ID ="dpSexo" runat ="server" >
                            <asp:ListItem Value ="0" Text ="Seleccionar"></asp:ListItem>
                            <asp:ListItem Value ="1" Text ="Masculino"></asp:ListItem>
                            <asp:ListItem Value ="2" Text ="Femenino"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvdpSexo" runat="server" ErrorMessage="Sexo" Text="*" ControlToValidate="dpSexo" ForeColor="Red" InitialValue ="0" ></asp:RequiredFieldValidator>
                    </td>
                    <td>Estado Civil</td>
                    <td>
                        <asp:DropDownList ID ="dpEstadoCivil" runat ="server" >
                            <asp:ListItem Value ="0" Text ="Seleccionar"></asp:ListItem>
                            <asp:ListItem Value ="1" Text ="Soltero(a)"></asp:ListItem>
                            <asp:ListItem Value ="2" Text ="Casado(a)"></asp:ListItem>
                            <asp:ListItem Value ="3" Text ="Viudo(a)"></asp:ListItem>
                            <asp:ListItem Value ="4" Text ="Divorsiado(a)"></asp:ListItem>
                            <asp:ListItem Value ="5" Text ="Union Libre"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvdpEstadoCivil" runat="server" ErrorMessage="Estado Civil" Text="*" ControlToValidate="dpEstadoCivil" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table><br />
            <table style="width: 100%">
                <tr>
                    <td colspan ="4">
                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">LUGAR DE NACIMIENTO</span>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style ="width :15%">Pais</td>
                    <td>
                        <asp:TextBox ID="txLnPais" runat="server"  Width="90%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxLnPais" runat="server" FilterMode="ValidChars" TargetControlID="txLnPais"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ "/>
                        <asp:RequiredFieldValidator ID="rfvtxLnPais" runat="server" ErrorMessage="Pais nacimiento" Text="*" ControlToValidate="txLnPais" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                    <td  style ="width :15%">Estado/Provincia</td>
                    <td>
                        <asp:TextBox ID="txLnEstado" runat="server"  Width="90%" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxLnEstado" runat="server" FilterMode="ValidChars" TargetControlID="txLnEstado"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                        <asp:RequiredFieldValidator ID="rfvtxLnEstado" runat="server" ErrorMessage="Estado Nacimiento" Text="*" ControlToValidate="txLnEstado" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Ciudad/Población</td>
                    <td>
                        <asp:TextBox ID="txLnCiudad" runat="server"  Width="90%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxLnCiudad" runat="server" FilterMode="ValidChars" TargetControlID="txLnCiudad"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                        <asp:RequiredFieldValidator ID="rfvtxLnCiudad" runat="server" ErrorMessage="Ciudad Nacimiento" Text="*" ControlToValidate="txLnCiudad" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                    <td>Nacionalidades</td>
                    <td>
                       <asp:TextBox ID="txLnNacionalidad" runat="server" Width="90%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxLnNacionalidad" runat="server" FilterMode="ValidChars" TargetControlID="txLnNacionalidad"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ " />
                        <asp:RequiredFieldValidator ID="rfvtxLnNacionalidad" runat="server" ErrorMessage="Nacionalidad Nacimiento" Text="*" ControlToValidate="txLnNacionalidad" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table><br />
            <asp:UpdatePanel ID ="upDomicilio" runat ="server" >
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td colspan ="4">
                                <span style="font-size: 14px; font-weight: bold; color: #007CC3">DOMICILIO PARTICULAR</span>
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
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxNumExt" runat="server" FilterMode="ValidChars" TargetControlID="txNumExt"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ- "/>
                                <asp:RequiredFieldValidator ID="rfvtxNumExt" runat="server" ErrorMessage="Numero Exterior" Text="*" ControlToValidate="txNumExt" ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                            <td>Numero Interior</td>
                            <td>
                                <asp:TextBox ID="txNumInt" runat="server" MaxLength="18" Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxNumInt" runat="server" FilterMode="ValidChars" TargetControlID="txNumInt"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ -"/>
                            </td>
                        </tr>
                        <tr>
                            <td>Codigo Postal</td>
                            <td>
                                <asp:TextBox ID="txCP" runat="server" MaxLength="5" Width="150px" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftxtxCP" runat="server" FilterMode="ValidChars" TargetControlID="txCP" ValidChars="1234567890" />
                                <asp:RequiredFieldValidator ID="rfvtxCP" runat="server" ErrorMessage="Codigo Postal" Text="*" ControlToValidate="txCP" ForeColor="Red" ></asp:RequiredFieldValidator>
                                <asp:Button ID="btnBuscarCP" Text ="..." runat ="server" OnClick="btnBuscarCP_Click" Width="26px"   CausesValidation ="false" />
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
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftetxEstado" runat="server" FilterMode="ValidChars" TargetControlID="txEstado"  ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ "/>
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
                        </tr>
                    </table><br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <table style="width: 100%">
                <tr>
                    <td colspan ="4">
                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">TELEFONOS</span>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>Particular</td>
                    <td colspan ="3">
                        <asp:TextBox ID="txTelParticular" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtxTelParticular" runat="server" FilterMode="ValidChars" TargetControlID="txTelParticular" ValidChars="1234567890" />
                        <asp:RequiredFieldValidator ID="rfvtxTelParticular" runat="server" ErrorMessage="Telefono Particular" Text="*" ControlToValidate="txTelParticular" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Oficina/Laboral</td>
                    <td>
                        <asp:TextBox ID="txTelOficina" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxTelOficina" runat="server" FilterMode="ValidChars" TargetControlID="txTelOficina" ValidChars="1234567890" />
                    </td>
                    <td>Extencion</td>
                    <td>
                        <asp:TextBox ID="txExtencion" runat="server"  Width="200px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxExtencion" runat="server" FilterMode="ValidChars" TargetControlID="txExtencion" ValidChars="1234567890" />
                    </td>
                </tr>
                <tr>
                    <td>Movil</td>
                    <td>
                        <asp:TextBox ID="txMovil" runat="server"  Width="200px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtxMovil" runat="server" FilterMode="ValidChars" TargetControlID="txMovil" ValidChars="1234567890" />
                    </td>
                </tr>
            </table><br />
            <table style="width: 100%">
                <tr>
                    <td colspan ="4">
                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">CORREO ELECTRONICO</span>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>Correo Personal</td>
                    <td>
                        <asp:TextBox ID="txCorreoPersonal" runat="server" MaxLength="60" Width="300px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxCorreoPersonal" runat="server" FilterMode="ValidChars" TargetControlID="txCorreoPersonal" ValidChars="@abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890." />
                        <asp:RequiredFieldValidator ID="rfvtxCorreoPersonal" runat="server" ErrorMessage="Correo" Text="*" ControlToValidate="txCorreoPersonal" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                    <td>Correo Laboral</td>
                    <td>
                        <asp:TextBox ID="txCorreoLaboral" runat="server" MaxLength="60" Width="300px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxCorreoLaboral" runat="server" FilterMode="ValidChars" TargetControlID="txCorreoLaboral" ValidChars="@abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890." />
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