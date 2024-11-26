<%@ Page Title="" Language="C#" MasterPageFile="~/wfip.Master" AutoEventWireup="true" CodeBehind="actualizaClaveConfirma.aspx.cs" Inherits="wfip.actualizaClaveConfirma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <fieldset>
        <legend>ACTUALIZACION DE CONTRASEÑA</legend>
        <div id="dvAviso" style="width:60%; margin:auto;">
            <div id="dvTitulo" style="width:100%; text-align:center;">
                <h4>La actualización se completó con éxito</h4><br />
            </div>
            <table id="tblMensaje" style="width:500px; margin: 0 auto; text-align: center">
                <tr>
                    <td>
                        Por favor ingrese nuevamente al sistema, ahora con su nueva contraseña.
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
           </table>
            <div id="dvBtns" style="width:100%; text-align:right">
                <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="boton" OnClick="btnContinuar_Click" />
            </div>
        </div>
    </fieldset>
</asp:Content>
