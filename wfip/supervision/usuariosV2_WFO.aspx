<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="usuariosV2_WFO.aspx.cs" Inherits="wfip.supervision.usuariosV2_WFO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/cupertino.css" rel="stylesheet" />
    <link href="../css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="../js/jquery.dataTables.min.js"></script>
    <script src="../js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        function onSiguiente() {
            fxPintaProcesando();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend><asp:Label ID="lblTitulo" Text="Permisos" runat="server"></asp:Label></legend>

        <asp:Panel ID="pnlInformacionGeneral" runat="server" >
            <div style="width:50%; margin:auto; text-align:center">
                <br />
                <span style="font-size: 14px; font-weight: bold; color: #007CC3">Información del Usuario.</span>
                <hr />
                <table style="width:100%;" border="0">
                    <tr>
                        <td style="text-align:right;"><asp:Label runat="server" ID="lblTituloUsuario" Text="Usuario:   "></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblUsuario" Text="Nombre del Usaurio"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align:right;"><asp:Label runat="server" ID="lblTituloRol" Text="Rol:   "></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblRol" Text="Nombre del Rol"></asp:Label></td>
                    </tr>
                </table>
                <br /><br /><br />&nbsp;
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlTiposServicios" runat="server" >
            <div style="width:50%; margin:auto; text-align:center">
                <br />
                <span style="font-size: 14px; font-weight: bold; color: #007CC3">Tipos de Servicios</span>
                <hr />
                <div style="margin:auto; text-align:left; s">
                    <asp:CheckBoxList ID="lsTiposServicios" 
                            runat="server" 
                            Font-Size="Smaller" 
                            Width="100%" >
                        <asp:ListItem></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <br />&nbsp;
                <asp:Button ID="btnSigTipoServicio" runat="server" Text="Siguiente" CssClass="boton" OnClientClick="return onSiguiente();" Visible="true" Height="35px" Width="150px" OnClick="btnSigTipoServicio_Click" />&nbsp;&nbsp;&nbsp;
                <br /><br /><br />&nbsp;
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlTiposTramite" runat="server" visible="false">
            <div style="width:50%; margin:auto; text-align:center">
                <br />
                <span style="font-size: 14px; font-weight: bold; color: #007CC3">
                    <asp:Label ID="lblTituloTiposTramite" runat="server" Text=""></asp:Label>
                </span>
                <hr />
                <div style="margin:auto; text-align:left; s">
                    <asp:CheckBoxList ID="lsTiposTramites" 
                            runat="server" 
                            Font-Size="Smaller" 
                            Width="100%" >
                        <asp:ListItem></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <br />&nbsp;
                <asp:Button ID="btnSigTipoTramite" runat="server" Text="Siguiente" CssClass="boton" OnClientClick="return onSiguiente();" Visible="true" Height="35px" Width="150px" OnClick="btnSigTipoTramite_Click" />&nbsp;&nbsp;&nbsp;
                <br /><br /><br />&nbsp;
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlTiposTramiteMov" runat="server" visible="false">
            <div style="width:50%; margin:auto; text-align:center">
                <br />
                <span style="font-size: 14px; font-weight: bold; color: #007CC3">
                    <asp:Label runat="server" ID="lblTiposMovimientos" Text="">

                    </asp:Label>
                </span>
                <hr />
                <div style="margin:auto; text-align:left; ">
                    <asp:CheckBoxList ID="lsTiposTramitesMov" 
                            runat="server" 
                            Font-Size="Smaller" 
                            Width="100%" >
                        <asp:ListItem></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <br />&nbsp;
                <span style="font-size: 14px; font-weight: bold; color: #007CC3">Mesas</span>
                <hr />
                <div style="margin:auto; text-align:left; ">
                    <asp:CheckBoxList ID="lsMesas" 
                            runat="server" 
                            Font-Size="Smaller" 
                            Width="100%" >
                        <asp:ListItem></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <br />&nbsp;
                <asp:Button ID="btnGuardarPermisos" runat="server" Text="Guardar Permisos" CssClass="boton" OnClientClick="return onSiguiente();" Visible="true" Height="35px" Width="150px" OnClick="btnGuardarPermisos_Click" />&nbsp;&nbsp;&nbsp;
                <br /><br /><br />&nbsp;
            </div>
        </asp:Panel>
    </fieldset>
</asp:Content>

