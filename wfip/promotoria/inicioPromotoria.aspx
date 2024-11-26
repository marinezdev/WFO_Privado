<%@ Page Title="" Language="C#"  MasterPageFile="~/promotoria/iniPromotoria.Master" AutoEventWireup="true" CodeBehind="inicioPromotoria.aspx.cs" Inherits="wfip.promotoria.inicioPromotoria" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick" Enabled="False"></asp:Timer>
            <fieldset>
            <div class="row">
                <div class="col-md-12 col-sm-12"></div>
                    <legend>INDICADOR GENERAL</legend>
                </div>
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <h3 class="text-center"><asp:Literal ID="ltTituloPromotoria" runat="server"></asp:Literal></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 text-center" id="grafica">
                <asp:Chart CanResize="true" ID="grfGrupoUno" ClientIDMode="Static" Height="300px" Width="700px" runat="server" BackColor="211, 223, 240" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="#1A3B69" BorderlineDashStyle="Solid" BorderWidth="2px" OnClick="grfGrupoUno_Click"  >
                 <ChartAreas>
                            <asp:ChartArea Name="GrupoUno" BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" ShadowColor="Transparent">
                               <AxisY LineColor="64, 64, 64, 64" IsMarginVisible="false">
                                  <LabelStyle Font="Trebuchet MS, 8pt" />
                                  <MajorGrid LineColor="64, 64, 64, 64" />
                               </AxisY>
                               <AxisX LineColor="64, 64, 64, 64" IsMarginVisible="false">
                                  <LabelStyle Font="Trebuchet MS, 8pt" />
                                  <MajorGrid LineColor="64, 64, 64, 64" />
                               </AxisX>
                            </asp:ChartArea>
                         </ChartAreas>
                </asp:Chart>
                <asp:Literal ID="ltTemp" runat="server"></asp:Literal>
             </div>
             <input type="button" style="display:none" id="button" />
             </div>                       
             <div class="row">
                 <div class="col-md-12 col-sm-12">&nbsp;<br />&nbsp;</div>
             </div>
            </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    

</asp:Content>

