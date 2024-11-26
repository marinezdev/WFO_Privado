<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GpoCaptura.aspx.cs" Inherits="wfip.Grupal.GpoCaptura" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>CAPTURA OPERACION</legend>
        <div id="DvBtnCerrar" style="width: 80%; margin: auto; text-align: right;">
            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="boton" OnClick="BtnCerrar_Click" />
        </div>
        <table id="TblCaptura" style="width: 80%; margin: auto;">
            <tbody>
                <tr>
                    <td style="width: 49%; vertical-align: top">
                        <dx:ASPxRoundPanel ID="PnlAsunto" runat="server" HeaderText="ASUNTO" Theme="Aqua" Width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <asp:TextBox ID="TxAsunto" runat="server" Width="95%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Rfv_TxAsunto" runat="server" ErrorMessage="Asunto" Text="*" ControlToValidate="TxAsunto" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="Ftb_TxAsunto" runat="server" FilterMode="ValidChars" TargetControlID="TxAsunto" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" BehaviorID="PnlAsunto_Ftb_TxAsunto" />
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                        <br />
                        <dx:ASPxRoundPanel ID="PnlDatos" runat="server" HeaderText="DATOS" Theme="Aqua" Width="100%">
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
                                                <asp:TextBox ID="txNombre" runat="server" MaxLength="64" Width="80%"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Razón social" Text="*" ControlToValidate="txNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RFC</td>
                                            <td>
                                                <asp:TextBox ID="txRfc" runat="server" MaxLength="13" Width="200px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
<%--                                                <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="*" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>--%>
                                                <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:UpdatePanel ID="upDomicilio" runat="server">
                                        <ContentTemplate>
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
                                                        <asp:TextBox ID="txCalle" runat="server" MaxLength="128" Width="295px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxCalle" runat="server" FilterMode="ValidChars" TargetControlID="txCalle" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ1234567890" />
                                                        <asp:RequiredFieldValidator ID="rfvtxCalle" runat="server" ErrorMessage="calle" Text="*" ControlToValidate="txCalle" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>No. Ext.</td>
                                                    <td>
                                                        <asp:TextBox ID="txNumExt" runat="server" MaxLength="18" Width="50px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxNumExt" runat="server" FilterMode="ValidChars" TargetControlID="txNumExt" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                                        <asp:RequiredFieldValidator ID="rfvtxNumExt" runat="server" ErrorMessage="Numero Exterior" Text="*" ControlToValidate="txNumExt" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>No. Int.</td>
                                                    <td>
                                                        <asp:TextBox ID="txNumInt" runat="server" MaxLength="18" Width="50px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxNumInt" runat="server" FilterMode="ValidChars" TargetControlID="txNumInt" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>CP</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txCP" runat="server" MaxLength="5" Width="50px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtxCP" runat="server" FilterMode="ValidChars" TargetControlID="txCP" ValidChars="1234567890" />
                                                        <asp:RequiredFieldValidator ID="rfvtxCP" runat="server" ErrorMessage="Codigo Postal" Text="*" ControlToValidate="txCP" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:Button ID="BtnBuscarCP" Text="..." runat="server" OnClick="BtnBuscarCP_Click" Width="26px" CausesValidation="false" Style="height: 26px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Col./Barrio</td>
                                                    <td>
                                                        <asp:DropDownList ID="DpColonia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DpColonia_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvdpColonia" runat="server" ErrorMessage="Colonia" Text="*" ControlToValidate="dpColonia" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
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
                                                        <asp:TextBox ID="txCiudad" runat="server" ReadOnly="True" Width="290px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Estado</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txEstado" runat="server" ReadOnly="True" Width="290px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                    </td>
                    <td style="width: 2%"></td>
                    <td style="width: 49%; vertical-align: top;">
                        <dx:ASPxRoundPanel ID="PnlNuevoDocumento" runat="server" HeaderText="ANEXAR DOCUMENTO" Theme="Aqua" Width="100%">
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
                        </dx:ASPxRoundPanel>
                        <br />
                        <dx:ASPxRoundPanel ID="PnlDocumentos" runat="server" Width="100%" HeaderText="DOCUMENTOS" Theme="Aqua">
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
                        </dx:ASPxRoundPanel>
                        <br />

                        <dx:ASPxRoundPanel ID="Pnl_Obs" runat="server" HeaderText="OBSERVACIONES" Theme="Aqua" Width="100%">
                            <ContentPaddings Padding="5px" />
                            <PanelCollection>
<dx:PanelContent runat="server">
    <asp:TextBox ID="TxObs" runat="server" Rows="2" TextMode="MultiLine" Width="95%"></asp:TextBox>
                                </dx:PanelContent>
</PanelCollection>
                        </dx:ASPxRoundPanel>

                        <br />
                        <asp:Panel ID="PnlEnvio" runat="server" Width="100%" style="text-align:right;">
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
