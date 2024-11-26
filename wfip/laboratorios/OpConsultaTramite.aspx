<%@ Page Title="" Language="C#" MasterPageFile="~/laboratorios/laboratorios.master" AutoEventWireup="true" CodeBehind="OpConsultaTramite.aspx.cs" Inherits="wfip.laboratorios.OpConsultaTramite" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v17.2" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>INFORMACIÓN DE TRÁMITE <asp:Label ID="Label2" runat="server"></asp:Label></legend>
            <div style="width: 90%; margin: auto">
                <asp:Label ID="IdTramite" runat="server" Font-Names="Britannic Bold" Font-Size="12px" Visible="false" ></asp:Label>
                <table id="tblDatos" style="width: 100%;">
                    <tr>     
                        <td style="width:60%; vertical-align: top; font-size:14px; "; >
                            <span style="font-size: 14px; font-weight: bold; color: #007CC3"><asp:Literal ID="ltInfTipoTramite"  runat="server"></asp:Literal></span>
                            <hr />
                            <asp:UpdatePanel id="DatosTramiteInformacion" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table border="0" style="width: 100%;">
                                        <tr>
                                            <td colspan="3" style="align-content:center;">
                                                  <asp:Label runat="server" ID="lblAdvertencia" Font-Bold="True" Font-Size="Larger" ForeColor="Red" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="vertical-align: top" >
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align:center; border-bottom: 1px solid #ddd; background-color:#8EBB53;">
                                        <asp:Literal ID="ltInfFolio" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #ddd; background-color:#F7F7F7;">
                                        <asp:Literal ID="ltInfContratante" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="border-bottom: 1px solid #ddd;">
                                        <asp:Literal ID="lblStatusMesas" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br /><hr /><br />
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"> 
                            INFORMACIÓN DE CITA MÉDICA 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <th colspan="2" scope="col" style="background-color:#1572B7; color:white;">Combo</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td colspan="2"><asp:Label ID="InfoCombo" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Sexo</th>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Edad</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td><asp:Label ID="InfoSexo" runat="server" ></asp:Label></td>
                                    <td><asp:Label ID="InfoEdad" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Ciudad</th>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Estado</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td><asp:Label ID="InfoCiudad" runat="server" ></asp:Label></td>
                                    <td><asp:Label ID="InfoEstado" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th colspan="2" scope="col" style="background-color:#1572B7; color:white;">Sucursal</th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td colspan="2"><asp:Label ID="InfoSucursal" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <th colspan="2" scope="col" style="background-color:#1572B7; color:white;">Dirección </th>
                                </tr>
                                <tr style="background-color: White; color: #333333; text-align:center">
                                    <td colspan="2"><asp:Label ID="InfoDireccion" runat="server" ></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3" style="text-align:center; border-bottom: 1px solid #ddd; color:black; background-color:#8EBB53;"> 
                                        Fechas
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Fecha 1 </th>
                                    <td style="background-color: White; color: #333333; text-align:center"> <asp:Label ID="InfoFecha1" runat="server" ></asp:Label></td>
                                    <td><asp:RadioButton id="Radio1" Value="1" GroupName="fechas" Text=""  runat="server" Font-Names="Britannic Bold" Font-Size="12px" /></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Fecha 2 </th>
                                    <td style="background-color: White; color: #333333; text-align:center"><asp:Label ID="InfoFecha2" runat="server" ></asp:Label> </td>
                                    <td><asp:RadioButton id="Radio2" Value="2" GroupName="fechas" Text=""  runat="server" Font-Names="Britannic Bold" Font-Size="12px" /></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Fecha 3 </th>
                                    <td style="background-color: White; color: #333333; text-align:center"><asp:Label ID="InfoFecha3" runat="server" ></asp:Label></td>
                                    <td><asp:RadioButton id="Radio3" Value="3" GroupName="fechas" Text=""  runat="server" Font-Names="Britannic Bold" Font-Size="12px"/></td>
                                </tr>
                                <tr>
                                    <th scope="col" style="background-color:#1572B7; color:white;">Fecha 4 </th>
                                    <td style="background-color: White; color: #333333; text-align:center"> 
                                        <dx:ASPxDateEdit ID="TextFecha4" runat="server" EditFormat="Custom" Theme="Material" Width="190" >
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="hh:mm tt" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td><asp:RadioButton id="Radio4" Value="4" GroupName="fechas" Text=""  runat="server" Font-Names="Britannic Bold" Font-Size="12px" /></td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align:center;"> 
                                        <br />
                                        <asp:Button ID="GuardarFecha" runat="server" Text="Guardar Cambios" CssClass="boton" OnClick="BtnContinuar_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
   </fieldset>
</asp:Content>

