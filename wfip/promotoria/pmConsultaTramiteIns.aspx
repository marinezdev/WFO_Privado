<%@ Page Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="pmConsultaTramiteIns.aspx.cs" Inherits="wfip.promotoria.pmConsultaTramiteIns" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>CONSULTA DETALLE INSTITUCIONAL SERVICIOS PRIVADO</legend>

        <table style="width:98%; margin:auto">
            <tr>
                <td style="width:100%;">
                    <asp:Panel ID="pnbtnMod" runat="server" Visible="false">
                        <fieldset style="text-align:left">
                            <legend>OBSERVACIONES</legend>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txObservaciones" runat="server" TextMode="MultiLine" Rows="5" Width="469%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_txComentarios" runat="server" ErrorMessage="LA OBSERVACION ES REQUERIDA" ControlToValidate="txObservaciones" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>                            
                        </fieldset>
                    </asp:Panel>
                </td>
                <td>
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="boton" OnClick="btnRegresar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td><asp:Label ID="lblEncabezado" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvArchivos" runat="server" EmptyDataText="No hay archivos." AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="TipoServicio" HeaderText="tipo de Servicio" />
                            <asp:BoundField DataField="Accion" HeaderText="Acción" />
                            <asp:BoundField DataField="Genero" HeaderText="Género" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="APaterno" HeaderText="Apellido Paterno" />
                            <asp:BoundField DataField="AMaterno" HeaderText="Apellido Materno" />

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>


    </fieldset>
</asp:Content>