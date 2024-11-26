<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sprReingresos.aspx.cs" Inherits="wfip.promotoria.sprReingresos" MasterPageFile="~/promotoria/promotoria.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <fieldset>
        <legend>REINGRESOS</legend>


        <asp:Chart ID="Chart1" runat="server" Width="1078px" BackColor="211, 223, 240" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="4px" OnClick="CapturaDiariaPorAnalista_Click">
            <Legends>
                <asp:Legend Alignment="Center" Docking="Right" IsTextAutoFit="False" Name="Default" LegendStyle="Table" ShadowColor="DarkGray" />
            </Legends>
            <ChartAreas>
                <asp:ChartArea Name="GrupoUno" BackColor="64, 165, 191, 198" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"> 
                    <AxisX Title="Analista"></AxisX>
                    <AxisY Title="CapturadosPorAnalista"></AxisY>                                
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>


    </fieldset>
</asp:Content>