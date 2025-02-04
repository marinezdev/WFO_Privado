﻿<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprRelojChecador.aspx.cs" Inherits="wfip.supervision.sprRelojChecador" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server" EnablePartialRendering="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>RELOJ CHECADOR</legend>
        <br />
        
            <table style ="width:100%">
                <tr>
                <td>
                    <dx:ASPxDateEdit ID="CalDesde" runat="server" Theme="iOS"  EditFormat="Custom" Caption="Desde:">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td>
                    <dx:ASPxDateEdit ID="CalHasta" runat="server" Theme="iOS"  EditFormat="Custom" Width="190" Caption="Hasta">
                        <TimeSectionProperties Adaptive="true">
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                        <CalendarProperties>
                            <FastNavProperties DisplayMode="Inline" />
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td>    
                    <div style="vertical-align:bottom">
                    <asp:Button ID="btnFiltrar"  CssClass="boton" runat="server" Text="Filtrar" />
                    </div>
                </td>
                </tr>
                <tr>
                 <td>
                    <asp:UpdateProgress ID="updProgress"  runat="server">
                        <ProgressTemplate>           
                         <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
                            <div style="padding: 10px;position:fixed;top:45%;left:50%;background-color:white;width:170px;height:45px " > 
                             <table style="background-size:0">
                                <tr>
                                 <td>
                                     <img alt=" " src="/img/spinner.gif" />
                                 </td>
                                <td>
                                    <span style="font-size:16px">&nbsp;Procesando...</span>
                                </td>
                                </tr>
                             </table>
                                
                           </div> 
                         </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                 </td>
                </tr>
        </table>
        <table style="width:100%">
             <tr>
             <td style="text-align:right">
               <asp:LinkButton ID="lnkExportar" runat="server"  CausesValidation="False" OnClick="lnkExportar_Click">
                  <img src="../img/excel.png"/>
               </asp:LinkButton>
             </td>
             </tr>
        </table>
        <br />
            <asp:UpdatePanel ID="DetalleReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="dvCajaGrilla" style="width: 1000px; margin: auto;">
                    <dx:ASPxGridView ID="dvgdRelojChecador" ClientInstanceName="dvgdRelojChecador" Width="100%" runat ="server" AutoGenerateColumns="False"  EnableTheming="True" Theme="iOS" Font-Size="10px">
                    <Columns>
                            <dx:GridViewDataTextColumn Caption="NUM USUARIO" FieldName="idUsuario"  VisibleIndex="1" width="100"/>
                            <dx:GridViewDataTextColumn Caption ="USUARIO" FieldName="Nombre"  VisibleIndex="2" width="350"/>
                            <dx:GridViewDataDateColumn Caption ="FECHA" FieldName="Fecha"  VisibleIndex="3" Width="250" >
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" />
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption ="TIEMPO" FieldName="TiempoTotal"  VisibleIndex="4" width="250"/>
                    </Columns>
                    <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" />
                    </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdRelojChecador"></dx:ASPxGridViewExporter>
                </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltrar"  EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
    </fieldset>
</asp:Content>

