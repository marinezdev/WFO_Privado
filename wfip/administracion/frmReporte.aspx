<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporte.aspx.cs" Inherits="wfip.administracion.frmReporte"  MasterPageFile="~/Administracion/adminsysMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Reporte</h1>

    <table align="center">
        <tr>
            <td>Fecha Inicio:</td>
            <td>
                <asp:TextBox ID="txtInicio" runat="server" Width="70px"></asp:TextBox> 
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" Format="dd/MM/yyyy" />
            </td>
            <td>Fecha Final:</td>
            <td>
                <asp:TextBox ID="txtFinal" runat="server" Width="70px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFinal" Format="dd/MM/yyyy" />
            </td>
            <td><asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" OnClick="BtnAceptar_Click" CssClass="boton" /> </td>
        </tr>
    </table>

    <center>
    <asp:Chart ID="chrReporte" runat="server" Width="1078px" BackColor="211, 223, 240" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="NotSet" BorderWidth="2px" OnClick="chrReporte_Click" Height="322px" Visible="false" >
        <Titles>
            <asp:Title Text="TOTAL DE CAPTURA"></asp:Title>
        </Titles>
        <Legends>
            <asp:Legend Docking="Bottom" Alignment="Center" LegendStyle="Row"></asp:Legend>
        </Legends>
        <Series>
            <asp:Series Name="Series2" XValueMember="Mesa" YValueMembers="ProcesosRealizados" LegendText="Procesos Realizados" Color="Green" IsValueShownAsLabel="true"></asp:Series>
            <asp:Series Name="Series1" XValueMember="Mesa" YValueMembers="TramiteProcesados" LegendText="Trámites Procesados" Color="Red" IsValueShownAsLabel="true"></asp:Series>
            <asp:Series Name="Series3" XValueMember="Mesa" YValueMembers="Reingresos" LegendText="Reingresos" Color="DarkSlateBlue" IsValueShownAsLabel="true"></asp:Series>
            
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="GrupoUno"  BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="NotSet" ShadowColor="Transparent">
                <AxisX Title="Mesas" ></AxisX>
                <AxisY Title="Trámites" ></AxisY>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    

    <asp:Chart ID="chrTiempos1" runat="server" Width="500px" BackColor="211, 223, 240" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="4px" Visible="false">
        <Titles>
            <asp:Title Name="Titulo1" Text="Tiempos Totales"></asp:Title>
        </Titles>
        <Legends>
            <asp:Legend Alignment="Center" Docking="Right" IsTextAutoFit="False" Name="Default" LegendStyle="Table" ShadowColor="DarkGray" />
        </Legends>
        <ChartAreas>
            <asp:ChartArea Name="Dato1"  BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"> 
                <AxisX Title="Mesa"></AxisX>
                <AxisY Title="TiempoTal"></AxisY>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

    <asp:Chart ID="chrTiempos2" runat="server" Width="500px" BackColor="211, 223, 240" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="4px" Visible="false">
        <Titles>
            <asp:Title Name="Titulo1" Text="Tiempos Promedio"></asp:Title>
        </Titles>
        <Legends>
            <asp:Legend Alignment="Center" Docking="Right" IsTextAutoFit="False" Name="Default" LegendStyle="Table" ShadowColor="DarkGray" />
        </Legends>
        <ChartAreas>
            <asp:ChartArea Name="Dato1"  BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"> 
                <AxisX Title="Mesa"></AxisX>
                <AxisY Title="TiempoPromedio"></AxisY>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

    </center>

</asp:Content>