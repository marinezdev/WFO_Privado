<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recuperarClaveOlvidada.aspx.cs" Inherits="wfip.recuperarClaveOlvidada" MasterPageFile="~/wfip.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
<script>


</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <style>
        .Menu {  }
        .Menu ul { background:#7795BD; }
        .Menu ul li
        {
            background: -moz-linear-gradient(top, #015891 0%, #023456 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #015891), color-stop(100%, #023456));
            background: -webkit-linear-gradient(top, #015891 0%, #023456 100%);
            background: -o-linear-gradient(top, #015891 0%, #023456 100%);
            background: -ms-linear-gradient(top, #015891 0%, #023456 100%);
            background: linear-gradient(to bottom, #015891 0%, #023456 100%);
            text-align:center;
            /* set width if needed.*/
            width:200px;
        }
        .Menu ul li a
        {
            color: black;
            padding: 14px;
            padding-bottom:8px !important;
            border-bottom: 2px solid #648ABD;            
        }
        .Menu ul li a:hover { color: Gray; background-color: #023456; }
        .Menu ul li a { color: white; }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

    <asp:Label ID="lblMenu" runat="server"></asp:Label>

    <br />

    <asp:Menu ID="MainMenu" runat="server" 
        Orientation="Horizontal" 
        StaticDisplayLevels="1"  
        MaximumDynamicDisplayLevels="5" 
        CssClass="Menu">
    </asp:Menu>     

    <br />

    <fieldset>
        <legend>RECUPERACIÓN DE CONTRASEÑA OLVIDADA</legend>
        
        <asp:UpdatePanel ID="upClaveRecuperar" runat="server">
            <ContentTemplate>

                <table>
                    <tr><td>Ingresa tu usuario:</td><td><asp:TextBox ID="txtUsuario" runat="server" AutoPostBack="True" OnTextChanged="txtUsuario_TextChanged"></asp:TextBox></td><td><asp:Label ID="lblUsuarioVal" runat="server"></asp:Label></td></tr>
                    <tr><td>Ingresa tu dirección de correo electrónico:</td><td><asp:TextBox ID="txtCorreo" runat="server" AutoPostBack="True" OnTextChanged="txtCorreo_TextChanged"></asp:TextBox></td><td><asp:Label ID="lblCorreoVal" runat="server"></asp:Label></td></tr>
                    <tr><td colspan="2" align="center"><asp:Button ID="btnAceptar" runat="server" Text="Recuperar" OnClick="btnAceptar_Click" /></td></tr>
                    <tr><td colspan="2"><asp:Label ID="lblMensajes" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr>
                </table>               

            </ContentTemplate>
        </asp:UpdatePanel>

    </fieldset>
</asp:Content>
