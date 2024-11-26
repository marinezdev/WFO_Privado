<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="admCatMotivosRechazo.aspx.cs" Inherits="wfip.administracion.admCatObservaciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            return resultado;
        }
        function fxPintaProcesando() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdId" runat="server" />
    <fieldset>
        <legend>ADMINISTRACION DE CATALOGO DE OBSERVACIONES</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="btnCerrar_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%">
            <tr style="text-align: center; height: 40px">
                <td>GRUPO</td>
                <td rowspan="2" style="background: #e8e8ec; width: 2px"></td>
                <td>REGISTRO</td>
            </tr>
            <tr>
                <td style="width: 40%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 150px">FTO:</td>
                            <td>
                                <asp:DropDownList ID="ddlFlujo" runat="server" OnSelectedIndexChanged="ddlFlujo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFlujo" runat="server" ControlToValidate="ddlFlujo" ErrorMessage="*" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>TRAMITE:</td>
                            <td>
                                <asp:DropDownList ID="ddlTipoTramite" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoTramite_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoTramite" runat="server" ControlToValidate="ddlTipoTramite" ErrorMessage="*" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="width: 85%; margin: auto;">
                        <tr>
                            <td>TIPO RECHAZO:</td>
                            <td>
                                <asp:DropDownList ID="ddlTipoRechazo" runat="server">
                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                    <asp:ListItem Value="2">Hold</asp:ListItem>
                                    <asp:ListItem Value="9">Rechazo</asp:ListItem>
                                    <asp:ListItem Value="10">Suspendido</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoRechazo" runat="server" ControlToValidate="ddlTipoRechazo" ErrorMessage="*" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>DESCRIPCION:</td>
                            <td>
                                <asp:TextBox ID="txDescripcion" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteDescripcion" runat="server" TargetControlID="txDescripcion" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                                <asp:RequiredFieldValidator ID="rfv_txDescripcion" runat="server" ControlToValidate="txDescripcion" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: red; text-align: center">
                                <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="boton" Visible="false" CausesValidation="false" OnClientClick="return Confirmar();" OnClick="btnModificar_Click" />&nbsp;
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="boton" Visible="false" CausesValidation="false" OnClick="btnCancelar_Click" />&nbsp;
                                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="boton" OnClick="btnRegistrar_Click" OnClientClick="return Confirmar();" CausesValidation="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <br />
        <asp:Panel ID="pnlCajaRegistros" Width="100%" runat="server">
            <div id="dvMotivosRechazo" style="width: 90%; margin: auto; font-size: 10px; font-family: Arial;">
                <table id="tblCajaRegistros" style="width: 100%;">
                    <tr>
                        <td style="width: 33%; vertical-align:top;">
                            <asp:Repeater ID="rptMotHold" runat="server" OnItemCommand="rptMotHold_ItemCommand" OnItemDataBound="rptMotHold_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="tblrptMotHold" style="width: 100%; margin: 0 auto" class="display">
                                        <caption>HOLD</caption>
                                        <thead>
                                            <th scope="col">Nombre</th>
                                            <th scope="col">Activo</th>
                                            <th scope="col">Editar</th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("Nombre")%></td>
                                        <td style="width: 40px; text-align: center">
                                            <asp:ImageButton ID="imgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CommandName="activo" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                                        </td>
                                        <td style="width: 40px; text-align: center">
                                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="width: 33%; vertical-align:top;">
                            <asp:Repeater ID="rptMotSuspendido" runat="server" OnItemCommand="rptMotSuspendido_ItemCommand" OnItemDataBound="rptMotSuspendido_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="tblMotSuspendido" style="width: 100%; margin: 0 auto" class="display">
                                        <caption>SUPENDIDO</caption>
                                        <thead>
                                            <th scope="col">Nombre</th>
                                            <th scope="col">Activo</th>
                                            <th scope="col">Editar</th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("Nombre")%></td>
                                        <td style="width: 40px; text-align: center">
                                            <asp:ImageButton ID="imgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CommandName="activo" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                                        </td>
                                        <td style="width: 40px; text-align: center">
                                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="vertical-align:top;">
                            <asp:Repeater ID="rptMotRechazo" runat="server" OnItemCommand="rptMotRechazo_ItemCommand" OnItemDataBound="rptMotRechazo_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="tblMotRechazo" style="width: 100%; margin: 0 auto" class="display">
                                        <caption>RECHAZO</caption>
                                        <thead>
                                            <th scope="col">Nombre</th>
                                            <th scope="col">Activo</th>
                                            <th scope="col">Editar</th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("Nombre")%></td>
                                        <td style="width: 40px; text-align: center">
                                            <asp:ImageButton ID="imgBtnActivo" runat="server" ImageUrl="~/img/activo.png" CommandName="activo" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                                        </td>
                                        <td style="width: 40px; text-align: center">
                                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="fxPintaProcesando();" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </fieldset>
</asp:Content>
