<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admConfiguracionGeneral.aspx.cs" Inherits="wfip.administracion.admConfiguracionGeneral" MasterPageFile="~/administracion/adminsysMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 309px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <asp:ScriptManager ID="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdProm" runat ="server" />
    <div style ="width:80%; margin:0 auto">
        <fieldset>
            <legend>CONFIGURACION DE PARAMETROS DEL SISTEMA</legend>
            <div id="dvDatos" runat ="server">
                <table style="width:100%; margin: 0 auto">
                    <tr>
                        <td class="auto-style2"><b>Días para caducidad de clave:</b></td>
                        <td>                        
                            <asp:TextBox ID="txtDiasCaducidad" runat="server" Width="25px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><b>Dias previos de aviso de proxima caducidad de clave:</b></td>  
                        <td><asp:TextBox ID="txtDiasPreviosAviso" runat="server" Width="25px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><b>Primera vez de clave aleatoria:</b></td>  
                        <td><asp:DropDownList ID="ddlClaveAleatoria" runat="server">
                            <asp:ListItem Value="1">Aleatorio</asp:ListItem>
                            <asp:ListItem Value="2">Fijo</asp:ListItem>
                            <asp:ListItem Value="3">Usuario</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><b>Clave Alta:</b></td>
                        <td><asp:RadioButtonList ID="rblClaveAlta" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Sí"></asp:ListItem>
                            <asp:ListItem Value="2" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>    
                        <td class="auto-style2"><b>Contraseña:</b></td>
                        <td>
                            <asp:RadioButtonList ID="rblTipoGeneracionClave" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Generarla automáticamente</asp:ListItem>
                                <asp:ListItem Value="2">Dejar que el usuario la cree</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><b>Hacer que el usuario cambie la contraseña cuando inicie sesión por primera vez</b></td>
                        <td>
                            <asp:CheckBox ID="ckhInicioSesionPrimeraVez" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><b>Intentos para bloquear clave</b></td>
                        <td>
                            <asp:TextBox ID="txtIntentos" runat="server" Width="29px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="2" style ="height :25px;color:red;text-align :center">
                            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;">
                            <asp:Button ID="btnCerrar" runat ="server"  Text ="Cerrar" CssClass="boton" OnClick="btnCerrar_Click" CausesValidation="false"/>&nbsp;
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="boton" OnClientClick="return Confirmar();" OnClick="btnGuardar_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            
            
        </fieldset>
    </div>



</asp:Content>
