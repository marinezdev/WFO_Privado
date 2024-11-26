<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprReporteTramitesAnuales.aspx.cs" Inherits="wfip.supervision.sprReporteTramitesAnuales" MasterPageFile="~/supervision/supervision.Master" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
    <fieldset>
        <legend> TRAMITES ANUALES </legend>
        <br /><br />
        <div>

            <dx:ASPxGridView ID="dvgdEstatusTramite" ClientInstanceName="dvgdEstatusTramite" runat="server" 
                AutoGenerateColumns ="True" 
                style="margin-top: 0px" 
                EnableTheming="false" 
                Font-Size ="10px" 
                Width="100%" SettingsDetail-ExportMode="All" SettingsExport-ExcelExportMode="Default">           
            <Settings HorizontalScrollBarMode="Hidden" VerticalScrollBarMode="Hidden" ShowFooter="false" ShowGroupFooter="Hidden" />
            <SettingsBehavior AllowSelectByRowClick="false" AllowSelectSingleRowOnly="true" EnableRowHotTrack="True" />
            <SettingsPager  Mode="ShowAllRecords"/>
            <SettingsSearchPanel Visible="false" />
            <Styles Header-Wrap="false" Cell-Wrap="true" />
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="dvgdEstatusTramite"></dx:ASPxGridViewExporter>        

            <br />

                <asp:Chart ID="Chart1" runat="server" BackGradientStyle="LeftRight" Height="350px" Palette="None" Width="1100px">   
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="true" Name="Default" LegendStyle="Row" AutoFitMinFontSize="5"></asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Name="Titulo1" Text="Gráfico de Trámites por Año"></asp:Title>
                    </Titles>
                    <ChartAreas>  
                        <asp:ChartArea Name="ChartArea1">
                            <AxisX Title="Meses"></AxisX>
                            <AxisY Title="Trámites"></AxisY>
                        </asp:ChartArea>  
                        <%--<asp:ChartArea Name="ChartArea2"></asp:ChartArea> --%> 
                    </ChartAreas>
                    
                    <BorderSkin BackColor=""  />  
                </asp:Chart>   
 




        </div>
    </fieldset>
</asp:Content>
