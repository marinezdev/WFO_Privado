<%@ Page Title="" Language="C#" MasterPageFile="~/operacionResp/operacion.Master" AutoEventWireup="true" CodeBehind="TramiteProcesar.aspx.cs" Inherits="wfip.operacionResp.TramiteProcesar" %>
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
                                <fieldset>
                                <p class="text-center"><asp:Label ID="lbNmMesa" runat="server" Text="MESA"></asp:Label><asp:Label ID="IdTramiteSe" runat="server" Visible="false"></asp:Label></p>
                                </fieldset>
                                <dx:ASPxPageControl ID="carTabPage" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True" >
                                   <TabPages>
                                        <dx:TabPage Text="TRAMITE">
                                            <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server">
                                                <fieldset>
                                                    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" HeaderText="INFORMACIÓN DEL TRAMITE" Theme="Aqua">
                                                        <ContentPaddings Padding="25px" />
                                                        <PanelCollection>
                                                            <dx:PanelContent runat="server">
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxRoundPanel>
                                                </fieldset>
                                            </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                       <dx:TabPage Text="BUSCAR">
                                            <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server">
                                                <fieldset>
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
