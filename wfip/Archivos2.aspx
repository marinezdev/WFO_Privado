<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Archivos2.aspx.cs" Inherits="wfip.Archivos2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="GVArchivos" runat="server" AutoGenerateColumns="false" OnRowCommand="GVArchivos_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" Text="Ver" CommandArgument='<%# Eval("Nombre") + "," + Eval("IdArchivo") %>' CommandName="Descargar" runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Entrega" HeaderText="Entrega" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
