<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/inicioSupervisor.Master" AutoEventWireup="true" CodeBehind="esperaSupervisorPR.aspx.cs" Inherits="wfip.supervision.esperaSupervisorPR" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v17.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

     <div class="row">
        <div class="x_panel">
            <br />
            <div class="container">
                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="card">
                        <dx:ASPxPageControl ID="carTabPage" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True" >
                        <TabPages>
                            <dx:TabPage Text="INDICADORES">
                                <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <fieldset>
                                        
                                        <p class="card-text">Total de trámites nuevo y reingresos por mesa.</p>
                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" HeaderText="MAPA GENERAL 'MESAS' " Theme="Aqua">
                                        <ContentPaddings Padding="25px" />
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                <div class="row right">
                                                    <div class="col-md-11 col-sm-11 col-xs-6 text-left">
                                                        <asp:Label ID="lblMessageBySystem" runat="server" Font-Bold="false" ForeColor="Orange" Text ="No asignado" Font-Size="Large"> </asp:Label>
                                                    </div>
                                                    <!--
                                                    <div class="col-md-2 col-sm-2 col-xs-12 text-center">
                                                    <p>Tramites con estatus pausa y en proceso</p>
                                                    </div>
                                                    -->
                                                    <div class="col-md-1 col-sm-1 col-xs-4 text-center">
                                                        <asp:LinkButton ID="LinkButton1" runat="server"  CausesValidation="False" OnClick="LinkUsuarios_Click">
                                                            <img src="../img/UsuariosRes.png"  class="img-thumbnail"/>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <hr />
                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                        <asp:Literal id="MesasLiteral" runat=server  text=""/>
                                                    </div>
                                                </div>
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
</asp:Content>



