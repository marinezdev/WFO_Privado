<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="SeleccionaTipoContratante.aspx.cs" Inherits="wfip.promotoria.SeleccionaTipoContratante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <div style="width: 60%; margin: auto;">
        <asp:Panel ID="pnlEmisionServicio" runat="server" CssClass="dropShadowPanel">
            <div style="width: 100%">
                ¿Tipo de Contratante?
                    <hr />
                <table id="tblEmisionServicio" style="width: 100%; text-align: center">
                    <tr>
                        <td style="width: 50%">
                            <asp:Button ID="btnFisica" runat="server" Text="Fisica" CssClass="capturaTipo" OnClick="btnFisica_Click"  />
                        </td>
                        <td>
                            <asp:Button ID="BtnMoral" runat="server" Text="Moral" CssClass="capturaTipo" OnClick="btnMoral_Click"  />
                        </td>
                    </tr>
                </table>
            </div><br />
        </asp:Panel>
    </div>
</asp:Content>
