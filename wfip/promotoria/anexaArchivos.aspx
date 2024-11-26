<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="anexaArchivos.aspx.cs" Inherits="wfip.promotoria.anexaArchivos" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var continuar = false;
            if (confirm('Esta seguro que desea continuar con el trámite ?')) {
                $("#dvBotones").hide();
                pnlMsgProcesando.Show();
                continuar = true;
            }
            return continuar;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>ARCHIVOS ANEXOS <asp:Label ID="Label2" runat="server"></asp:Label></legend>
            <div style="width: 90%; margin: auto">
                <br />
                <table id="" style="width:100%; margin:auto;" >
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="border-bottom: 1px solid #ddd; background-color:#F7F7F7;">
                                        <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:UpdatePanel id="DatosTramiteInformacion" runat="server" UpdateMode="Conditional" Visible="true">
                                <ContentTemplate>
                                    <label>Agente</label>
                                    <asp:TextBox ID="txIdAgente" runat="server" MaxLength="10" Width="180px" AutoPostBack="true" OnTextChanged="daNombreDeAgente"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterMode="ValidChars" TargetControlID="txIdAgente" ValidChars="áéíóúabcdefghijklmnñopqrstuvwxyzÁÉÍÓÚABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789" />
                                    <asp:Label ID="Mensajes" runat="server" Text="   " ForeColor="Maroon"></asp:Label>
                                    <br /><br />
                                    <table style="width: 100%;">
                                        <tr>
                                            <th scope="col" style="background-color:#1572B7; color:white;">Nombre </th>
                                            <th scope="col" style="background-color:#1572B7; color:white;">Correo Electrónico </th>
                                            <th scope="col" style="background-color:#1572B7; color:white;">Correo Electrónico Alterno </th>
                                        </tr>
                                        <tr style="background-color: White; color: #333333; text-align:center">
                                            <td><asp:Label ID="lbNombreAgente" runat="server" Text=""></asp:Label></td>
                                            <td><asp:Label ID="lbEmailAgente" runat="server" Text=""></asp:Label></td>
                                            <td><asp:Label ID="lbEmailAlternoAgente" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                            
                                    
                                    </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width:50%">
                            <fieldset>
                                <legend >CHECKLIST  DE DOCUMENTOS</legend>
                                <asp:Label ID="TextIdTipoTramite" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="TextTipoPersona" runat="server" Visible="false"></asp:Label>
                                <asp:CheckBoxList ID="DocRequerid" runat="server" CssClass="cbl">
                                    
                                </asp:CheckBoxList>
                                <dx:ASPxFormLayout ID="FORMULAIRO" runat="server" Visible="false"></dx:ASPxFormLayout>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        <br />
        <asp:Panel ID="PnlArchivosAnexos" runat="server" Width="100%">
            <table id="tblArchivos" style="width:90%; margin:auto;">
                <tr>
                    <td style="width:50%">
                        <fieldset>
                            <legend>ARCHIVOS CON DOCUMENTOS REQUERIDOS</legend>
                            <asp:Label ID="lblDocumentosRequeridos" runat="server" Text="Archivos (*.PDF, *.JPG, *.PNG)"></asp:Label>
                            <br />
                            <asp:FileUpload ID="fileUpDocumento" runat="server" />
                            <%--<asp:RegularExpressionValidator ID="rev_fileUpDocumento" runat="server" ErrorMessage="Solo PDF o JPG" ControlToValidate="fileUpDocumento"  />--%>
                            <asp:Button ID="btnSubirDocumento" runat="server" Text="Subir" CssClass="boton" OnClick="btnSubirDocumento_Click" /><span style="font-size: 9px">Tamaño máximo de archivo: <%= ArchivoMaximo1 %> mb</span><br />
                            <asp:ListBox ID="lstDocumentos" runat="server" Height="150px" Width="95%" SelectionMode="Single" >
                            </asp:ListBox>
                            <br />
                            <asp:Button ID="btnEliminaDocumento" runat="server" Text="Eliminar" CssClass="boton" OnClick="btnEliminaDocumento_Click" />
                        </fieldset>
                    </td>
                    <td>
                        <!--
                        EVITAR SUBIR INSUMOS

                        <fieldset>
                            <legend>ARCHIVOS ADICIONALES</legend>
                            <asp:CheckBox ID="CheckBox1"  runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" Text="¿Desea agregar archivos adicionales?" />
                            <asp:CheckBox ID="chkArchPublicos"  runat="server" AutoPostBack="false" Text="Públicos" Visible="false" />
                            <asp:CheckBox ID="chkArchPrivados"  runat="server" AutoPostBack="false" Text="Privados" Visible="false" />

                            <br />
                            <asp:FileUpload ID="fileUpInsumo" runat="server" Visible="false"/>
                            <asp:Button ID="btnSubirInsumo" runat="server" Text="Subir" CssClass="boton" OnClick="btnSubirInsumo_Click"  Visible="false"/><span style="font-size: 9px">Tamaño máximo de archivo: <%= ArchivoMaximo2 %>mb</span><br />
                            <asp:ListBox ID="lstInsumos" runat="server" Height="150px" Width="95%" SelectionMode="Single"  Visible="false">
                            </asp:ListBox>
                            <br />
                            <asp:Button ID="btnEliminaInsumo" runat="server" Text="Eliminar" CssClass="boton" OnClick="btnEliminaInsumo_Click" Visible="false"/>
                        </fieldset>
                        -->
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div id="dvBotones" style="text-align: right">
            
            <asp:Panel ID="Regredar" runat="server" Visible="false">
                <asp:Button ID="btnRegresar" runat="server" CssClass="boton" OnClick="BtnposBack" Text="Regresar" Visible="true"/>&nbsp;&nbsp;
            </asp:Panel>
            <br />
            <asp:Label ID="SumaBasica" runat="server" Font-Size="12px" ForeColor="Crimson" Text="Al dar clic en el botón continuar, No cambies o cierres la pestaña"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="BtnContinuar" runat="server" Text="Continuar" CssClass="boton" OnClientClick="return Confirmar();" OnClick="BtnContinuar_Click" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="BtnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
    <dx:ASPxLoadingPanel ID="pnlMsgProcesando" runat="server" ClientInstanceName="pnlMsgProcesando" Modal="true" Text="Procesando...">
    </dx:ASPxLoadingPanel>
</asp:Content>
