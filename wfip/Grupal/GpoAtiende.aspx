<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GpoAtiende.aspx.cs" Inherits="wfip.Grupal.GpoAtiende" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <fieldset>
        <legend>CAPTURA OPERACION</legend>
        <div id="DvBtnCerrar" style="width: 80%; margin: auto; text-align: right;">
            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="BtnCerrar_Click" />
        </div>
        <table id="TblCaptura" style="width: 80%; margin: auto;">
            <tbody>
                <tr>
                    <td style="width: 49%; vertical-align: top">
                        <dx:aspxroundpanel id="PnlAsunto" runat="server" headertext="ASUNTO" theme="Aqua" width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:TextBox ID="TxAsunto" runat="server" Width="95%" Rows="3" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:aspxroundpanel>
                        <br />
                        <dx:aspxroundpanel id="PnlDatos" runat="server" headertext="DATOS" theme="Aqua" width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table id="TblCapturaDatosGenerales" style="width: 100%">
                                        <tr>
                                            <td colspan="4">
                                                <span style="color: #007CC3">GENERALES</span>
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">Razon Social</td>
                                            <td>
                                                <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="80%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RFC</td>
                                            <td>
                                                <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="200px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="TblCapturaDatosDireccion" style="width: 100%">
                                        <tr>
                                            <td colspan="4">
                                                <span style="color: #007CC3">DOMICILIO</span>
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Calle</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txCalle" runat="server" MaxLength="128" Width="295px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>No. Ext.</td>
                                            <td>
                                                <asp:TextBox ID="txNumExt" runat="server" MaxLength="18" Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>No. Int.</td>
                                            <td>
                                                <asp:TextBox ID="txNumInt" runat="server" MaxLength="18" Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>CP</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txCP" runat="server" MaxLength="5" Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Col./Barrio</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="TxColonia" runat="server" Width="290px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Mpio/Del.</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txMpio" runat="server" Width="290px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cd/Pob.</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txCiudad" runat="server" Width="290px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Estado</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txEstado" runat="server" Width="290px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:aspxroundpanel>
                        <br />
                        <dx:aspxroundpanel id="PnlDocAnexados" runat="server" width="100%" headertext="ANEXOS" theme="Aqua">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:Repeater ID="RptDocAnexados" runat="server" OnItemCommand="RptDocAnexados_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="TblDocAnexados" style="width: 100%; font-size:10px;" class="display">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">Descripcion</th>
                                                        <th scope="col"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: White; color: #333333">
                                                <td><%# Eval("Descripcion")%></td>
                                                <td style="width: 35px; text-align: center">
                                                    <asp:ImageButton ID="ImgBtnDescargar" runat="server" ImageUrl="~/img/download.png" CommandName="Descargar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" ToolTip="Descargar" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:aspxroundpanel>
                    </td>
                    <td style="width: 2%"></td>
                    <td style="width: 49%; vertical-align: top;">
                        <dx:aspxroundpanel id="PnlNuevoDocumento" runat="server" headertext="ANEXAR DOCUMENTO" theme="Aqua" width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table id="TblNuevoDocumento" style="width: 100%">
                                        <tr>
                                            <td style="width: 20%">Nombre</td>
                                            <td>
                                                <asp:TextBox ID="txDescripcionDocto" runat="server" Width="290px" MaxLength="150"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Archivo</td>
                                            <td>
                                                <asp:FileUpload ID="FupArchivo" runat="server" Width="330px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Button ID="BtnAgregaDocumento" runat="server" Text="Anexar" CssClass="boton" OnClick="BtnAgregaDocumento_Click" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:aspxroundpanel>
                        <br />
                        <dx:aspxroundpanel id="PnlDocumentos" runat="server" width="100%" headertext="DOCUMENTOS" theme="Aqua">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:Repeater ID="RptDocumentos" runat="server" OnItemCommand="RptDocumentos_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="tblDocumentos" style="width: 100%; font-size:10px;" class="display">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">Descripcion</th>
                                                        <th scope="col">Archivo</th>
                                                        <th scope="col"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: White; color: #333333">
                                                <td><%# Eval("Descripcion")%></td>
                                                <td><%# Eval("NmOriginal")%></td>
                                                <td style="width: 35px; text-align: center">
                                                    <asp:ImageButton ID="imgbtnEditar" runat="server" ImageUrl="~/img/eliminar.png" CommandName="Eliminar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" ToolTip="Eliminar" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:aspxroundpanel>
                        <br />

                        <dx:aspxroundpanel id="Pnl_Obs" runat="server" headertext="OBSERVACIONES" theme="Aqua" width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:TextBox ID="TxObs" runat="server" Rows="2" TextMode="MultiLine" Width="95%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Rfv_TxObs" runat="server" ControlToValidate="TxObs" ErrorMessage="Observaciones" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:aspxroundpanel>

                        <br />
                        <asp:Panel ID="PnlEnvio" runat="server" Width="100%" Style="text-align: right;">
                            <asp:HiddenField ID="Hf_TramiteId" runat="server" />
                            <asp:HiddenField ID="Hf_BuzonEntradaId" runat="server" />
                            Enviar a: &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="CboBuzon" runat="server" Width="130px"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="Rfv_CboBuzon" runat="server" ControlToValidate="CboBuzon" InitialValue="0" ErrorMessage="Destino" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" CausesValidation="true" CssClass="boton" OnClick="BtnEnviar_Click" />
                        </asp:Panel>
                    </td>
                </tr>
            </tbody>
        </table>
    </fieldset>
    <asp:Literal ID="Lt_jsMsg" runat="server"></asp:Literal>
</asp:Content>
