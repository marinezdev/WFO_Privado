<%@ Page Title="" Language="C#" MasterPageFile="~/operacionResp/operacion.Master" AutoEventWireup="true" CodeBehind="SeleccionarFlujo.aspx.cs" Inherits="wfip.operacionResp.SeleccionarFlujo" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="x_panel">
                    <br />
                    <div class="container">
                        <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                            <div class="card">
                               
                                <dx:ASPxPageControl ID="carTabPage" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True" >
                                   <TabPages>
                                        <dx:TabPage Text="SELECCIONAR FLUJO PARA GESTIONAR">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server">
                                                <fieldset>
                                                    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" HeaderText="MESAS DE ATENCIÓN" Theme="Aqua">
                                                        <ContentPaddings Padding="25px" />
                                                        <PanelCollection>
                                                            <dx:PanelContent runat="server">
                                                            <div class="row">
                                                                <div class="col-md-6 col-sm-6 col-xs-12">
                                                                    <asp:Label ID="lblFlujos" runat="server" Text="Flujos " Font-Bold="true" class="control-label col-md-4 col-sm-4 col-xs-12 "></asp:Label>
                                                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                                        <asp:DropDownList ID="ListFlujos" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListFlujos_SelectedIndexChanged" class="form-control">
                                                                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <hr />
                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                                    <asp:Literal id="MesasLiteral" runat=server  text=""/>
                                                                </div>
                                                            </div>
                                                            <hr />

                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxRoundPanel>
                                                </fieldset>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
