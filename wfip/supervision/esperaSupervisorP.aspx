<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="esperaSupervisorP.aspx.cs" Inherits="wfip.supervision.esperaSupervisorP" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <dx:ASPxPageControl ID="carTabPage" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True" >
        <TabPages>
            <dx:TabPage Text="INDICADORES">
                <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server">
                <fieldset>
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" AllowCollapsingByHeaderClick="True" HeaderText="MAPA GENERAL" Theme="Aqua">
                    <ContentPaddings Padding="5px" />
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div style="width:70%; margin:0 auto"">
                                <table style ="width:80%">
                                    <tr>
                                        <td style="text-align:right">
                                            <asp:LinkButton ID="LinkButton1" runat="server"  CausesValidation="False" OnClick="LinkUsuarios_Click">
                                                <img src="../img/usuario2.png"/>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="LinkExportarMapa" runat="server"  CausesValidation="False" OnClick="LinkExportarMapa_Click">
                                                <img src="../img/excel.png"/>
                                            </asp:LinkButton>
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="dvgdIndicadores"  ClientInstanceName="dvgdIndicadores" runat="server" AutoGenerateColumns ="False" Width ="95%" 
                                             style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px">
                                            <Columns>
                                                <dx:GridViewDataColumn>
                                                    <DataItemTemplate>
                                                        <asp:ImageButton ID="imbtnConsultar" runat="server" OnCommand="MuestraTramiteOnclick" CommandArgument='<%# Eval("mesa_uno")%>' CommandName ="Consultar" FieldName="Grupo_uno" ImageUrl='<%# Eval("Grupo_uno")%>' Visible='<%# Eval("mesa_uno").ToString().Equals(" ")?false:true%>'/>
                                                        <%#(Eval("mesa_uno").ToString().Equals(" "))?
                                                        "<label></label>" :       
                                                        "<table style='width:100%; border-collapse: separate; border-spacing: 0px;' border='0'> "+
                                                           "<tr><td style='text-align:center'><label><b>" +Eval("mesa_uno") +"</b></label></td></tr>" +
                                                           "<tr><td><label>Usuarios conectados: </label><label>"+ Eval("usuarios_uno")+ "</label></td></tr> "+
                                                           "<tr><td><label>Tramites: </label><label>"+ Eval("tramites_uno") +"</label></td></tr>" +
                                                        "</table>"
                                                        %>
                                                        
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn>
                                                    <DataItemTemplate>
                                                        <asp:ImageButton ID="imbtnConsultar" runat="server" OnCommand="MuestraTramiteOnclick" CommandArgument='<%# Eval("mesa_dos")%>' CommandName ="Consultar" FieldName="Grupo_dos" ImageUrl='<%# Eval("Grupo_dos")%>' Visible='<%# Eval("mesa_dos").ToString().Equals(" ")?false:true%>'/>
                                                        <%#(Eval("mesa_dos").ToString().Equals(" "))?
                                                        "<label></label>" :       
                                                        "<table style='width:100%; border-collapse: separate; border-spacing: 0px;' border='0'> "+
                                                           "<tr><td style='text-align:center'><label><b>" +Eval("mesa_dos") +"</b></label></td></tr>" +
                                                           "<tr><td><label>Usuarios conectados: </label><label>"+ Eval("usuarios_dos")+ "</label></td></tr> "+
                                                           "<tr><td><label>Tramites: </label><label>"+ Eval("tramites_dos") +"</label></td></tr>" +
                                                        "</table>"
                                                        %>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn>
                                                    <DataItemTemplate>
                                                        <asp:ImageButton ID="imbtnConsultar" runat="server" OnCommand="MuestraTramiteOnclick" CommandArgument='<%# Eval("mesa_tres")%>' CommandName ="Consultar" FieldName="Grupo_tres" ImageUrl='<%# Eval("Grupo_tres")%>' Visible='<%# Eval("mesa_tres").ToString().Equals(" ")?false:true%>'/>
                                                        <%#(Eval("mesa_tres").ToString().Equals(" "))?
                                                        "<label></label>" :       
                                                        "<table style='width:100%; border-collapse: separate; border-spacing: 0px;' border='0'> "+
                                                           "<tr><td style='text-align:center'><label><b>" +Eval("mesa_tres") +"</b></label></td></tr>" +
                                                           "<tr><td><label>Usuarios conectados: </label><label>"+ Eval("usuarios_tres")+ "</label></td></tr> "+
                                                           "<tr><td><label>Tramites: </label><label>"+ Eval("tramites_tres") +"</label></td></tr>" +
                                                        "</table>"
                                                        %>
                                                        </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn>
                                                    <DataItemTemplate>
                                                        <asp:ImageButton ID="imbtnConsultar" runat="server" OnCommand="MuestraTramiteOnclick" CommandArgument='<%# Eval("mesa_cuatro")%>' CommandName ="Consultar" FieldName="Grupo_cuatro" ImageUrl='<%# Eval("Grupo_cuatro")%>' Visible='<%# Eval("mesa_cuatro").ToString().Equals(" ")?false:true%>'/>
                                                        <%#(Eval("mesa_cuatro").ToString().Equals(" "))?
                                                        "<label></label>" :       
                                                        "<table style='width:100%; border-collapse: separate; border-spacing: 0px;' border='0'> "+
                                                           "<tr><td style='text-align:center'><label><b>" +Eval("mesa_cuatro") +"</b></label></td></tr>" +
                                                           "<tr><td><label>Usuarios conectados: </label><label>"+ Eval("usuarios_cuatro")+ "</label></td></tr> "+
                                                           "<tr><td><label>Tramites: </label><label>"+ Eval("tramites_cuatro") +"</label></td></tr>" +
                                                        "</table>"
                                                        %>
                                                        </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true"  />
                                            <SettingsPager  Mode="ShowAllRecords"/>
                                            <Settings ShowColumnHeaders="false" />
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                           <dx:ASPxGridView ID="dvgdTramites" ClientInstanceName="dvgdTramites" runat="server"
                                            AutoGenerateColumns ="False" Width ="100%"  EnableTheming="True" Theme="iOS"  
                                            Font-Size ="10px" KeyFieldName="idMesa" Visible="false">
                                           <Columns>
                                                <dx:GridViewDataTextColumn Caption ="FLUJO"  FieldName="flujo"  VisibleIndex="1" Width="250" />
                                                <dx:GridViewDataTextColumn Caption ="NUM MESA"  FieldName="idMesa"  VisibleIndex="2" Visible="false" />
                                                <dx:GridViewDataTextColumn Caption ="MESA"  FieldName="Mesa"  VisibleIndex="3" />
                                                <dx:GridViewDataTextColumn Caption ="USUARIOS CONECTADOS" FieldName="Usuarios"  VisibleIndex="4" />
                                                <dx:GridViewDataTextColumn Caption ="TRAMITES NUEVOS" FieldName="TramitesNuevos"  VisibleIndex="5" />
                                                <dx:GridViewDataTextColumn Caption= "# REINGRESOS" FieldName="Reingresos"   VisibleIndex="6"/>
                                                <dx:GridViewDataTextColumn Caption ="TOTAL TRAMITES"  FieldName="TramitesTotal"  VisibleIndex="7" />
                                           </Columns>
                                           <SettingsDetail ShowDetailRow="true" />
                                           <SettingsExport ExcelExportMode ="WYSIWYG"></SettingsExport>
                                           <Templates>
                                               <DetailRow> 
                                                   <dx:ASPxGridView ID="dvgdDetalleTramite"  runat="server" ClientInstanceName="dvgdDetalleTramite"   OnInit="dvgdDetalleTramite_Init" KeyFieldName="idTramite" width="100%" EnablePagingGestures="False" AutoGenerateColumns ="False" >
                                                       <Columns>
                                                            <dx:GridViewDataColumn FieldName="FolioCompuesto" Caption="FOLIO" VisibleIndex="1" Width="180px" />
                                                            <dx:GridViewDataColumn FieldName="Reingreso" Caption="REINGRESOS" VisibleIndex="2"/>
                                                            <dx:GridViewDataColumn FieldName="FechaIngreso" Caption="INGRESO SISTEMA" VisibleIndex="3"/>
                                                            <dx:GridViewDataColumn FieldName="Fecha" Caption="INGRESO EN MESA" VisibleIndex="4"/>
                                                            <dx:GridViewDataColumn FieldName="FechaFin" Caption="ULTIMO REGISTRO" VisibleIndex="5"/>
                                                            <dx:GridViewDataColumn FieldName="Usuario" Caption="USUARIO" VisibleIndex="6"/>
                                                            <dx:GridViewDataColumn FieldName="tAtencion" Caption="TIEMPO ATENCION" VisibleIndex="7"/>
                                                            <dx:GridViewDataColumn FieldName="tMesa" Caption="TIEMPO MESA" VisibleIndex="8"/>
                                                            <dx:GridViewDataColumn FieldName="Contratante" Caption="CONTRATANTE" VisibleIndex="9"/>
                                                            <dx:GridViewDataColumn FieldName="Solicitante" Caption="SOLICITANTE" VisibleIndex="10"/>
                                                       </Columns>
                                                        <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupedColumns="True" ShowGroupPanel="True"/>
                                                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True"  />
                                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="600" />
                                                        <SettingsPager EnableAdaptivity="true" Mode="ShowAllRecords" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Styles Header-Wrap="True"/>
                                                    </dx:ASPxGridView>
                                               </DetailRow>
                                           </Templates>
                                           <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" AllowEllipsisInText="true" />
                                           <SettingsPager  Mode="ShowAllRecords"/>
                                           <SettingsDetail ShowDetailRow="true" />
                                           <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                                           <SettingsSearchPanel Visible="true" />
                                           </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> &nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
            </fieldset>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <TabPages>
            <dx:TabPage Text="RESUMEN PROMOTORÍAS">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                    <fieldset>
                        <table  style ="width:100%">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkExportar" runat="server" CausesValidation="False" OnClick="lnkExportar_Click" Visible="false">Exportar a Excel</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td style ="width:75%">
                                    &nbsp;
                                    <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" AutoGenerateColumns ="False" KeyFieldName="ESTADO"
                                        Width ="100%" style="margin-top: 0px" EnableTheming="True" Theme="iOS"  Font-Size ="10px"    >
                                       <Columns>
                                            <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO"  VisibleIndex="0" Visible="false" />
                                            <dx:GridViewDataTextColumn Caption="ESTATUS" FieldName="ESTATUS"  VisibleIndex="1" />
                                            <dx:GridViewDataTextColumn Caption ="TOTAL" FieldName="TOTAL"  VisibleIndex="2" />
                                        </Columns>
                                        <Templates>
                                        <DetailRow>
                                            <dx:ASPxLabel runat="server" Text='<%# Eval("ESTATUS") %>' Font-Bold="true" />
                                            <br /><br />
                                            <dx:ASPxGridView ID="dvgdDetallePromotoria" runat="server" ClientInstanceName="dvgdDetallePromotoria" OnBeforePerformDataSelect="dvgdDetallePromotoria_BeforePerformDataSelect"  OnInit="dvgdDetallePromotoria_Init" KeyFieldName="TRAMITE" width="100%" EnablePagingGestures="False" AutoGenerateColumns ="False" >
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="RAMO" Caption="CVERAMO" VisibleIndex="0" Visible="false"/>
                                                    <dx:GridViewDataColumn FieldName="NRAMO" Caption="RAMO" VisibleIndex="1"/>
                                                    <dx:GridViewDataColumn FieldName="TRAMITE" Caption="TRAMITE" VisibleIndex="2" Width="180"/>
                                                    <dx:GridViewDataColumn FieldName="CVEPROMOTORIA" Caption="NUM PROMOTORIA" VisibleIndex="3"/>
                                                    <dx:GridViewDataColumn FieldName="PROMOTORIA" Caption="PROMOTORIA" VisibleIndex="4"/>
                                                    <dx:GridViewDataColumn FieldName="CLAVEAGENTE" Caption="NUM AGENTE" VisibleIndex="5"/>
                                                    <dx:GridViewDataColumn FieldName="AGENTE" Caption="AGENTE" VisibleIndex="6"/>
                                                    <dx:GridViewDataColumn FieldName="DIAS" Caption="DIAS" VisibleIndex="7"/>
                                                </Columns>
                                                <Settings ShowFooter="True" HorizontalScrollBarMode="Visible" />
                                                <SettingsPager EnableAdaptivity="true" />
                                                <Styles Header-Wrap="True"/>
                                                <SettingsSearchPanel Visible="true" />
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                        </Templates>
                                        <Settings  ShowFooter="True" ShowGroupFooter="VisibleAlways"  />
                                        <SettingsBehavior  AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true"  EnableRowHotTrack="True" AllowEllipsisInText="true"  />
                                        <SettingsPager  Mode="ShowAllRecords"/>
                                        <SettingsDetail ShowDetailRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="200"  />
                                        <SettingsSearchPanel Visible="true" />
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdEstatusTramite"></dx:ASPxGridViewExporter>
                                </td>
                                <td>
                                    <dx:WebChartControl ID="dxChtTotales" runat="server" CrosshairEnabled="True" Height="300px" Width="550px" Theme ="SoftOrange">
                                        <Titles>
                                            <dx:ChartTitle Font="Arial,12pt" Text="ESTATUS" />
                                        </Titles>
                                        <DiagramSerializable>
                                            <dx:XYDiagram>
                                                <AxisX VisibleInPanesSerializable="-1">
                                                </AxisX>
                                                <AxisY VisibleInPanesSerializable="-1">
                                                </AxisY>
                                            </dx:XYDiagram>
                                        </DiagramSerializable>
                                        <Legend Name="Default Legend" Visibility ="False" ></Legend>
                                        <SeriesSerializable>
                                            <dx:Series Name="dxSreTotales" ></dx:Series>
                                        </SeriesSerializable>
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td> &nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                   </dx:ContentControl>
                </ContentCollection>
                </dx:TabPage>
        </TabPages>
        </dx:ASPxPageControl>
</asp:Content>
