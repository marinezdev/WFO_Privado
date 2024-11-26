<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="nuevoVida.aspx.cs" Inherits="wfip.promotoria.nuevoVida" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function buscaNombreAgente(txCtrl) {
            idPromotoria = $get('cph_areaTrabajo_hf_IdPromotoria').value;
            PageMethods.daNombreDeAgente(idPromotoria, txCtrl.value, regresaNombreAgente, siErrorNombreAgente);
        }
        function regresaNombreAgente(resultado) { $get('cph_areaTrabajo_lbNombreAgente').innerHTML = resultado; }
        function siErrorNombreAgente(error, userContext, methodName) { if (error != null) { alert(error.get_message()); } }
        
        function confirmaContinuar() { pnlMsgProcesando.Show(); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <fieldset>
        <legend>TRAMITE NUEVO VIDA</legend>
        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="tblInfTramite" style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACION DEL TRAMITE</span>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>Agente:</td>
                                <td>
                                    <asp:HiddenField ID="hf_IdPromotoria" runat="server" />
                                    <asp:TextBox ID="txIdAgente" runat="server" MaxLength="5" Width="50px" AutoPostBack="false" onblur="buscaNombreAgente(this)"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txIdAgente" runat="server" FilterType="Numbers" TargetControlID="txIdAgente" />
                                    <asp:RequiredFieldValidator ID="rfv_txIdAgente" runat="server" ErrorMessage="AGENTE" Text="*" ControlToValidate="txIdAgente" ForeColor="Red" Font-Size="16px" ValidationGroup="vdGral"></asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbNombreAgente" runat="server" Text="N.D."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Producto:</td>
                                <td>
                                    <asp:DropDownList ID="cboProducto" runat="server" Width="300px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="1">TempoLIfe</asp:ListItem>
                                        <asp:ListItem Value="2">TotalFile</asp:ListItem>
                                        <asp:ListItem Value="3">PerfectLife</asp:ListItem>
                                        <asp:ListItem Value="4">Horizonte Metlife Retiro</asp:ListItem>
                                        <asp:ListItem Value="5">Horizonte Metlife CPEA</asp:ListItem>
                                        <asp:ListItem Value="6">FexiLife Protección</asp:ListItem>
                                        <asp:ListItem Value="7">FexiLife Sueños</asp:ListItem>
                                        <asp:ListItem Value="8">Educalife</asp:ListItem>
                                        <asp:ListItem Value="9">FexiLife</asp:ListItem>
                                        <asp:ListItem Value="10">Cáncer</asp:ListItem>
                                        <asp:ListItem Value="11">MetaLife</asp:ListItem>
                                        <asp:ListItem Value="12">Otros especificar</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Número de solicitud CP DES/WEB</td>
                                <td>
                                    <asp:TextBox ID="txSolicitud_cpdesweb" runat="server" MaxLength="16"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txSolicitud_cpdesweb" runat="server" TargetControlID="txSolicitud_cpdesweb" FilterMode="ValidChars" 
                                        ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ1234567890" />
                                </td>
                            </tr>
                            <tr>
                                <td>Número de solicitud</td>
                                <td>
                                    <asp:TextBox ID="txSolicitud" runat="server" MaxLength="16"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txSolicitud" runat="server" TargetControlID="txSolicitud" FilterMode="ValidChars" 
                                        ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ1234567890" />
                                </td>
                            </tr>
                            <tr>
                                <td>Estatus CPDES</td>
                                <td>
                                    <asp:DropDownList ID="cbopEstatus_cpdes" runat="server">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="1">Otros</asp:ListItem>
                                        <asp:ListItem Value="2">Extra prima</asp:ListItem>
                                        <asp:ListItem Value="3">Sub-aceptado artículo 140</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Tipo de contratante</td>
                                <td>
                                    <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" Width="200px">
                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Value="1">Persona fisica</asp:ListItem>
                                        <asp:ListItem Value="2">Persona moral</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlInfContratanteFisico" runat="server" Width="100%">
                            <table id="tblInfContratanteFisico" style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACION DEL CONTRATANTE PERSONA FISICA</span>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">Apellido paterno</td>
                                    <td>
                                        <asp:TextBox ID="txApPat" runat="server" MaxLength="64"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" TargetControlID="txApPat" FilterMode="ValidChars" 
                                            ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Apellido materno</td>
                                    <td>
                                        <asp:TextBox ID="txApMat" runat="server" MaxLength="64"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" TargetControlID="txApMat" FilterMode="ValidChars" 
                                            ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nombre(s)</td>
                                    <td>
                                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" TargetControlID="txNombre" FilterMode="ValidChars" 
                                            ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fecha de nacimiento</td>
                                    <td>
                                        <asp:TextBox ID="txFechaNacimiento" runat="server" MaxLength="10"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txFechaNacimiento" runat="server" TargetControlID="txFechaNacimiento" FilterMode="ValidChars" 
                                            ValidChars="1234567890/-" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>RFC</td>
                                    <td>
                                        <asp:TextBox ID="txRfc" runat="server" MaxLength="13"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" TargetControlID="txRfc" FilterMode="ValidChars" 
                                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>CURP</td>
                                    <td>
                                        <asp:TextBox ID="txCurp" runat="server" MaxLength="18"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txCurp" runat="server" TargetControlID="txCurp" FilterMode="ValidChars" 
                                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align:right">
                                        <asp:Button ID="btnSigFisica" runat="server" Text="Siguiente" CssClass="boton" OnClientClick="confirmaContinuar();" OnClick="btnSigFisica_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlInfContratanteMoral" runat="server" Width="100%">
                            <table id="tblInfContratanteMoral" style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <span style="font-size: 14px; font-weight: bold; color: #007CC3">INFORMACION DEL CONTRATANTE PERSONA MORAL</span>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">Nombre</td>
                                    <td>
                                        <asp:TextBox ID="txNombreMoral" runat="server" MaxLength="100"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombreMoral" runat="server" TargetControlID="txNombreMoral" FilterMode="ValidChars" 
                                            ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ1234567890&()/-," />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fecha de constitución</td>
                                    <td>
                                        <asp:TextBox ID="txFechaConstitucion" runat="server" MaxLength="10"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txFechaConstitucion" runat="server" TargetControlID="txFechaConstitucion" FilterMode="ValidChars" 
                                            ValidChars="1234567890/-" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>RFC</td>
                                    <td>
                                        <asp:TextBox ID="txRfcMoral" runat="server" MaxLength="13"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfcMoral" runat="server" TargetControlID="txRfcMoral" FilterMode="ValidChars" 
                                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align:right">
                                        <asp:Button ID="btnSigMoral" runat="server" Text="Siguiente" CssClass="boton" OnClientClick="confirmaContinuar();" OnClick="btnSigMoral_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
    <dx:ASPxLoadingPanel ID="pnlMsgProcesando" runat="server" ClientInstanceName="pnlMsgProcesando" Modal="true" Text="Procesando..." xmlns:dx="devexpress.web">
      </dx:ASPxLoadingPanel>
</asp:Content>
