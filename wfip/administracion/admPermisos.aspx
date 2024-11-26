<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admPermisos.aspx.cs" Inherits="wfip.administracion.admPermisos" MasterPageFile="~/administracion/adminsysMaster.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>ADMINISTRACION DE PERMISOS</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="btnCerrar_Click" />
                </td>
            </tr>
        </table>
        <div id="dvCajaCapturaDatos" style="width: 100%">
            <table border="0" style="width: 800px; text-align: left; margin: 0 auto;">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rblPermisos" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblPermisos_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Mostrar Permisos"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Agregar Permiso"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>Roles:</td>
                    <td>
                        <asp:DropDownList ID="ddlRoles" runat="server" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="mostrar" runat="server" visible="false"><td colspan="2">

                    <asp:GridView ID="gvPermisos" runat="server"
                        AutoGenerateColumns="False" 
                        BackColor="White" 
                        BorderColor="Yellow" 
                        BorderStyle="None" 
                        BorderWidth="0" GridLines="Both" 
                        HeaderStyle-Font-Bold="true"
                        HeaderStyle-Font-Size="XX-Small" 
                        RowStyle-Font-Size="Small"
                        CellPadding="4" 
                        CellSpacing="1"  
                        RowStyle-Wrap="false" 
                        HeaderStyle-Wrap="true"  
                        ShowFooter="true" 
                        DataKeyNames="IdPermiso" 
                        SelectedRowStyle-BackColor="Green" 
                        OnRowCancelingEdit="gvPermisos_RowCancelingEdit"
                        OnRowEditing="gvPermisos_RowEditing"
                        OnRowUpdating="gvPermisos_RowUpdating"
                        OnRowCommand="gvPermisos_RowCommand"
                        >
                        <Columns>
                            <asp:CommandField ShowEditButton="true" ButtonType="Link" EditText="Modificar" CancelText="Cancelar" UpdateText="Actualizar" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActivoI" runat="server" Enabled="false" Checked='<%#Eval("Activo") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkActivoE" runat="server" Checked='<%#Eval("Activo") %>' />                                    
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" ReadOnly="true" />
                        </Columns>
                        <FooterStyle BackColor="#deedf7" ForeColor="#000066" />
                        <HeaderStyle BackColor="#deedf7" Font-Bold="True" ForeColor="#2779aa" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000000" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>

                    </td>
                </tr>
                <tr id="agregar1" runat="server" visible="false">
                    <td>Descripción:</td><td><asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>  </td>
                </tr>
                <tr id="agregar2" runat="server" visible="false">
                    <td>Activo:</td><td><asp:CheckBox ID="cbxActivo" runat="server" /></td>
                </tr>
                <tr id="agregar3" runat="server" visible="true">
                    <td colspan="2" align="center"><asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="boton" OnClick="btnAceptar_Click"/></td>
                </tr>

                </table>
        </div>
        <br />
    </fieldset>



</asp:Content>