<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admUsuarioMesa.aspx.cs" Inherits="wfip.administracion.admUsuarioMesa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function onClickGuarda() {
            var resultado = false;
            if (confirm("Se actualiza el usuario?")) {
                fxPintaProcesando();
                resultado = true;
            }
            return resultado;
        }

        function onClickCancela() {
            fxPintaProcesando();
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>ADMINISTRACION DE USUARIOS</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="btnCerrar_Click" />
                </td>
            </tr>
        </table>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table id="tblCapturaDatos" border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                <tr>
                    <td style="width: 25%">Nombre:</td>
                    <td><asp:Label ID="lbNombre" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Usuario:</td>
                    <td><asp:Label ID="lbUsuario" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Módulo</td>
                    <td><asp:Label ID="lbModulo" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Grupo:</td>
                    <td><asp:Label ID="lbGrupo" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr><td colspan="2"><hr /></td></tr>
                <tr>
                    <td>Flujo:</td>
                    <td>
                        <asp:DropDownList ID="ddlFlujo" runat="server" OnSelectedIndexChanged="ddlFlujo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Mesa:</td>
                    <td>
                        <asp:DropDownList ID="ddlMesa" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Trámite:</td>
                    <td>
                        <asp:DropDownList ID="ddlTipoTrámite" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="boton" OnClick="btnAgregar_Click" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <div style="width: 95%; margin: auto; font-size: 10px; font-family: Arial;">
                            <asp:Repeater ID="rptMesas" runat="server" OnItemCommand="rptMesas_ItemCommand">
                                <HeaderTemplate>
                                    <table id="tblMesas" style="width:100%" class="display">
                                        <thead>
                                            <th scope="col">Flujo</th>
                                            <th scope="col">Mesa</th>
                                            <th scope="col">Trámite</th>
                                            <th scope="col"></th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="color: #333333">
                                        <td><%# Eval("FlujoNombre")%></td>
                                        <td><%# Eval("MesaNombre")%></td>
                                        <td><%# Eval("TramiteNombre")%></td>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/img/eliminar.png" CommandName="eliminar" CommandArgument='<%# Eval("IdMesa")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="boton" OnClientClick="return onClickGuarda();" OnClick="btnGuardar_Click" />&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <asp:HiddenField ID="hf_Id" runat="server" />
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
