<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtrasDependencias.aspx.cs" Inherits="wfip.promotoria.OtrasDependencias" MasterPageFile="~/promotoria/promotoria.Master" %>

<%@ Register Assembly="DevExpress.Web.v17.2" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script>        
        function Confirmar() {
                pnlProcesando.Show();
            return continuar;
        }

        function ConfirmarProceso(mensaje) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(mensaje)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

 
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    
    <fieldset>
        <legend>OTRAS DEPENDENCIAS</legend>

        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
        
                <table>
                    <tr>
                        <td>Seleccionar Archivo:</td>
                        <td>
                            <ajaxToolkit:AsyncFileUpload id="AsyncFileUpload1" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/>
                        </td>
                        <td>
                            <asp:Button ID="BtnSubirArchivo" runat="server" Text="Subir" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="BtnSubirArchivo_Click" />
                        </td>
                    </tr>
                </table>

                <div style="overflow:scroll; width: 900px; height: 500px">

                <asp:GridView ID="gvDatos" runat="server" Font-Names="Courier" Width="100%"></asp:GridView>


                </div>

                <asp:Button ID="BtnGuardar" runat="server" Text="Renombrar Archivo" CssClass="boton" OnClick="BtnGuardar_Click" Visible="false" />

            </div>        
        </div>

    </fieldset>
</asp:Content>