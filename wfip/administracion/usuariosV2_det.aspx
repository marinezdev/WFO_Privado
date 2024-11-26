<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="usuariosV2_det.aspx.cs" Inherits="wfip.administracion.usuariosV2_det" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        function onCrear() {
            fxPintaProcesando();
            return true;
        }

        function onClickMod() {
            var resultado = false;
            if (Page_ClientValidate()) {
                if (confirm("Se actualiza el usuario?")) {
                    fxPintaProcesando();
                    resultado = true;
                }
            }
            return resultado;
        }

        function onCancelar() {
            fxPintaProcesando();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend><asp:Label ID="lblTitulo" Text="Detalle del Usuario" runat="server"></asp:Label></legend>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table id="tblCapturaDatos" border="0" style="width: 80%; text-align: left; margin: 0 auto;">
                <tr>
                    <td style="width: 25%; text-align:right;">Nombre:&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="80%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server"
                            TargetControlID="txNombre"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ"></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align:right;">Nombre de Usuario:&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txUsuario" runat="server" MaxLength="32" Width="80%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txUsuario" runat="server"
                            TargetControlID="txUsuario"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-_!#$%&/()=+"></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align:right;">Correo Electrónico:&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" Width="80%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            TargetControlID="txtCorreo"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-_@"></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align:right;">Perfil:&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:DropDownList ID="cboRol" runat="server" Visible="true" Width="80%"></asp:DropDownList></td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td></td>
                    <td style="text-align: right;">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btnRojo" OnClientClick="return onCancelar();" Visible="true" CausesValidation="false" Height="35px" Width="85px" OnClick="btnCancelar_Click" />&nbsp;
                        <asp:Button ID="btnEditar" runat="server" Text="Modificar" CssClass="btnAzul" OnClientClick="return onClickMod();" Visible="false" Height="35px" Width="85px" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCrear" runat="server" Text="Guardar" CssClass="btnAzul" OnClientClick="return onCrear();" Visible="false" Height="35px" Width="85px" OnClick="btnCrear_Click" />&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
