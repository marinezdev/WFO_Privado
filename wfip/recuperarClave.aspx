<%@ Page Language="C#" MasterPageFile="~/wfip.Master" AutoEventWireup="true" CodeBehind="recuperarClave.aspx.cs" Inherits="wfip.recuperarClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
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
            width:180px;
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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Label ID="lblMenu" runat="server"></asp:Label>

    <br />

    <asp:Menu ID="MainMenu" runat="server" 
        Orientation="Horizontal" 
        StaticDisplayLevels="1"  
        MaximumDynamicDisplayLevels="5" 
        CssClass="Menu">
    </asp:Menu>     

    <asp:Label ID="lblMenuDinamico" runat="server"></asp:Label>

    <br />

    <fieldset>
        <legend>RECUPERACIÓN DE CONTRASEÑA</legend>
        
        <table style="width:80%; align-content:center;" border="0">
            <tr>
                <td colspan="2" style="text-align:right">Ingresa tu nombre de usuario:&nbsp;</td>
                <td colspan="2"><asp:TextBox ID="txtUsuario" runat="server" MaxLength="150" Width="98%"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right">Ingresa tu dirección de correo electrónico:&nbsp;</td>
                <td colspan="2"><asp:TextBox ID="txtEMail" runat="server" MaxLength="150" AutoCompleteType="Email" Width="98%"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td style="text-align:center;">
                    <asp:Button ID="btnAceptar" runat="server" Text="Recuperar" OnClick="btnAceptar_Click" />
                </td>
            </tr>
        </table>
         
        
        <%--Ingresa la clave a cifrar: <asp:TextBox ID="txtClaveACifrar" runat="server"></asp:TextBox><asp:Button ID="btnAceptar2" runat="server" Text="Cifrar" OnClick="btnAceptar2_Click" style="height: 26px" /><br />--%>
        <br />
        <asp:Label ID="lblMensajes" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
        <asp:TreeView ID="tvwOpciones" runat="server" ShowCheckBoxes="All" ShowLines="True" ></asp:TreeView>

        <asp:TreeView ID="tvwMenu" runat="server" ShowLines="true" ShowCheckBoxes="All"></asp:TreeView>    

    
    </fieldset>






</asp:Content>