<%@ Page Title="" Language="C#" MasterPageFile="~/wfip.Master" AutoEventWireup="true" CodeBehind="actualizaClave.aspx.cs" Inherits="wfip.actualizaClave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function lenNoMenos(oSrc, args) {
            args.IsValid = (args.Value.length > 7);
        }
        function onClickMod() {
            var resultado = false;
            if (Page_ClientValidate()) {
                if (confirm("Desea actualizar la contraseña?")) {
                    fxPintaProcesando();
                    resultado = true;
                }
            }
            return resultado;
        }

        function ValidarNuevaClave(txNuevaClave){
            //alert(txNuevaClave.value);
            PageMethods.VerificarClave(txNuevaClave.value, regresarValidacion);
        }

        function regresarValidacion(resultado){
            if (resultado) {
                $get('cph_areaTrabajo_lblMensajeValidacion').innerHTML = "Pruebe otra contraseña!!!";
                $get('cph_areaTrabajo_txNuevaClave').innerHTML = "";
                $get('cph_areaTrabajo_txNuevaClave').select();
            }
            else 
                $get('cph_areaTrabajo_lblMensajeValidacion').innerHTML = "";
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <fieldset>
        <legend>ACTUALIZACION DE CONTRASEÑA</legend>
        <table style="width: 772px; margin: 0 auto; text-align: left;">
            <tr>
                <td colspan="2" style="text-align:left;">
                    <span style="color:#93ACCF; font-style: italic; font-size: medium;">
                        1) Escribe la contraseña actual.<br />
                        2) Escribe una contraseña nueva y vuelve a escribirla para confirmarla. 
                    Debe reunir los siguientes requerimientos: mayor de 8 caracteres, debe incluir por lo menos una letra mayúscula, un número y un caracter especial .-_!#$%&/()=.<br />
                        Después de guardar se requiere volver a iniciar tu sesión.
                    </span>
                    <hr />
                </td>
            </tr>
            <tr>
                <td><b>Usuario:</b></td>
                <td>
                    <asp:Label ID="lbUsrActual" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Clave actual:</b></td>
                <td>
                    <asp:TextBox ID="txClaveActual" runat="server" MaxLength="32" Width="200px" TextMode="Password" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txClaveActual" runat="server"
                        TargetControlID="txClaveActual"
                        FilterMode="ValidChars"
                        ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-_!#$%&/()=">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfv_txClaveActual" runat="server" ErrorMessage="*" ControlToValidate="txClaveActual" Font-Size="Large" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><b>Nueva clave:</b></td>
                <td>

                    <asp:TextBox ID="txNuevaClave" runat="server" MaxLength="32" Width="200px" TextMode="Password" autocomplete="off" AutoCompleteType="Disabled" onblur="ValidarNuevaClave(this)"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNuevaClave" runat="server"
                        TargetControlID="txNuevaClave"
                        FilterMode="ValidChars"
                        ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-_!#$%&/()=+">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfv_txNuevaClave" runat="server" ErrorMessage="*" ControlToValidate="txNuevaClave" Font-Size="Large" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cv_txNuevaClave" runat="server" ErrorMessage="No menos de 8 caracteres" ControlToValidate="txNuevaClave" ClientValidationFunction="lenNoMenos" ForeColor="Red" Display="Dynamic"></asp:CustomValidator>
                    &nbsp;<asp:Label id="lblMensajeValidacion" runat="server"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td><b>Confirma nueva clave:</b></td>
                <td>
                    <asp:TextBox ID="txConfimaClave" runat="server" MaxLength="32" Width="200px" autocomplete="off" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txConfimaClave" runat="server"
                        TargetControlID="txConfimaClave"
                        FilterMode="ValidChars"
                        ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-_!#$%&/()=">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfv_txConfimaClave" runat="server" ErrorMessage="*" ControlToValidate="txConfimaClave" Font-Size="Large" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cv_txConfimaClave" runat="server" ErrorMessage="La confirmación no coincide" ControlToValidate="txConfimaClave" ControlToCompare="txNuevaClave" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="color:red; height:20px; text-align:center;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <asp:Button ID="btnContinuar" runat="server" Text="Guardar" CssClass="boton" OnClientClick="return onClickMod();" OnClick="btnContinuar_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="False" OnClick="btnCancelar_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
