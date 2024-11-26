<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reseteo.aspx.cs" Inherits="wfip.Mtto.Reseteo" %>

<%@ Register Src="~/Mtto/Menu1.ascx" TagPrefix="uc1" TagName="Menu1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Mantenimiento</title>
    <link href="Estilos.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <uc1:Menu1 runat="server" id="Menu1" />
            
            <h1>Reseteo de Contraseña</h1>

            Clave de Usuario: <asp:TextBox ID="txtClave" runat="server"></asp:TextBox><asp:Button ID="Btnbuscar" runat="server" Text="Buscar" OnClick="Btnbuscar_Click" />

            <asp:GridView ID="GVUsuarios" runat="server" CellPadding="5" CellSpacing="5" HeaderStyle-Wrap="false" HeaderStyle-Font-Bold="false" RowStyle-Wrap="false" HeaderStyle-BackColor="#808080" AutoGenerateColumns="false" OnRowCommand="GVUsuarios_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:TemplateField HeaderText="Usuario">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkIdUsuario" runat="server" Text='<%# Eval("Usuario") %>' CommandArgument='<%# Bind("Usuario") %>' CommandName="Accion"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Correo" HeaderText="Correo" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    <asp:BoundField DataField="Conectado" HeaderText="Conectado" />
                    <asp:BoundField DataField="ConectadoFecha" HeaderText="Fecha Conectado" />

                </Columns>
            </asp:GridView>

            <asp:GridView ID="GVResultado" runat="server"  CellPadding="5" CellSpacing="5" HeaderStyle-Wrap="false" HeaderStyle-Font-Bold="false" RowStyle-Wrap="false" HeaderStyle-BackColor="#808080"></asp:GridView>

        </div>
    </form>
</body>
</html>
