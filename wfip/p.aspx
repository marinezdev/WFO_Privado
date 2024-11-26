<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p.aspx.cs" Inherits="wfip.p" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Area de pruebas diversas</title>
<style>
    .ModalPopupBG
{
    background-color: #666699;
    filter: alpha(opacity=50);
    opacity: 0.7;
}
</style>
</head>
<body style="background-color: cornflowerblue">
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
        <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
        <div>
            <h1>Exclusivamente para pruebas</h1>
            <h4>Seleccione un archivo para subirlo</h4>

            <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Mode="Auto" MaximumNumberOfFiles="5" Width="500px" OnUploadComplete="AjaxFileUpload1_UploadComplete" OnUploadCompleteAll="AjaxFileUpload1_UploadCompleteAll" />

            <asp:GridView ID="gvArchivos" runat="server" Caption="Archivos" CellSpacing="2" CellPadding="2" BorderWidth="0" Font-Names="Tahoma" Font-Size="Small"
                AutoGenerateColumns="false" OnRowDataBound="gvArchivos_RowDataBound" ShowHeader="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="hlkNombresarchivos" runat="server" Text='<%# Eval("Nombre") %>' Target="_blank"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>                            
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkQuitarArchivo" runat="server" Text="Quitar" CommandArgument='<%# Eval("Nombre") %>' OnClick="lnkQuitarArchivo_Click" ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>

            <asp:Button ID="BtnGuardar" runat="server" Text="Mostrar los archivos cargados" OnClick="BtnGuardar_Click" />

            <ajaxToolkit:AsyncFileUpload ID="AsyncFileUpload1" runat="server" />


            <br />Seleccione el rango de fechas:
            <table>
                <tr>
                    <td><asp:TextBox ID="txtFechaInicial" runat="server" Width="75"></asp:TextBox></td>
                    <td valign="bottom"><asp:Image ID="ImgFecha" runat="server" ImageUrl="img/Calendar.png" /></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="txtFechaFinal" runat="server" Width="75"></asp:TextBox></td>
                    <td valign="bottom"><asp:Image ID="ImgFecha2" runat="server" ImageUrl="img/Calendar.png" /></td>
                </tr>
            </table>
            
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImgFecha" TargetControlID="txtFechaInicial" Format="dd/MM/yyyy" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImgFecha2" TargetControlID="txtFechaFinal" Format="dd/MM/yyyy" />
        
            <br />
            <br />


  
                   

            <asp:TextBox ID="txtBody" runat="server" Columns="80" Rows="20" TextMode="MultiLine"></asp:TextBox>
            <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtBody" EnableSanitization="false"></ajaxToolkit:HtmlEditorExtender>
            <asp:Button ID="BtnHTMLEditorGuardar" runat="server" Text="Guardar los datos presentados" OnClick="BtnHTMLEditorGuardar_Click" /><br />
            <asp:Label ID="lblTextoEditor" runat="server"></asp:Label>
            <br />
            <br />
            
            <asp:Button ID="BtnMostratpopUp" runat="server" Text="Ver" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="UPPop" TargetControlID="BtnMostratpopUp" CancelControlID="BtnCerrar" BackgroundCssClass="ModalPopupBG"></ajaxToolkit:ModalPopupExtender>
            <asp:UpdatePanel ID="UPPop" runat="server" style="display: none; background-color: white; border: 1px solid black;">
                <ContentTemplate>
                        Chart<br />
                        <ajaxToolkit:PieChart ID="PieChart1" runat="server"></ajaxToolkit:PieChart><br />
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" />
                </ContentTemplate>                
            </asp:UpdatePanel>
        
            <asp:Chart ID="ChartUsuarios" runat="server" Width="1100" Palette="Chocolate" OnClick="ChartUsuarios_Click">
                <ChartAreas>
                    <asp:ChartArea Name="Roles">
                        <AxisY LineColor="64, 64, 64, 64" IsMarginVisible="false" Title="Usuarios Por Rol">
                            <LabelStyle Font="Tahoma, 8pt" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisY>
                        <AxisX LineColor="64, 64, 64, 64" IsMarginVisible="false" Title="Roles">
                            <LabelStyle Font="Tahoma, 8pt" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisX>
                        <Area3DStyle Enable3D="true" />
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>


            <asp:UpdatePanel ID="upReportes" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="60%" Height="60%" ShowPrintButton="true"></rsweb:ReportViewer>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>


            
        
        
        </div>
    </form>
</body>
</html>
