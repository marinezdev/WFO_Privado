<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsCobranza.aspx.cs" Inherits="wfip.promotoria.Cobranza" MasterPageFile="~/promotoria/promotoria.Master" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v17.2" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.2" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <script>        
        function NegritaToggle(item) {
            if (item === 1) {
                $('#tdNombre').css('font-weight', 'bold');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
            else if (item === 2) {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'bold');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
            else if (item === 3) {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'bold');
            }
            else {
                $('#tdNombre').css('font-weight', 'normal');
                $('#tdAPaterno').css('font-weight', 'normal');
                $('#tdAMaterno').css('font-weight', 'normal');
            }
        }

        function Confirmar() {
                pnlProcesando.Show();
            return continuar;
        }

        function HacerEncabezadoEstatico(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
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
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     
        .auto-style1 {
            width: 16px;
            height: 16px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    
    <fieldset>
        <legend>COBRANZA</legend>

        <div style="padding: 20px">
            <div style="width: 90%; margin: auto">
                <asp:UpdatePanel ID="upCaptura" runat="server">
                    <ContentTemplate>

                            <table align="center">
                                <tr style="visibility:hidden">
                                    <td></td><td></td><td align="right">VIP:</td><td><asp:RadioButtonList ID="rblVIP" runat="server" RepeatDirection="Horizontal"><asp:ListItem Value="1">Sí</asp:ListItem><asp:ListItem Value="2">No</asp:ListItem></asp:RadioButtonList></td>
                                </tr>
                                <tr>
                                    <td colspan="4"><asp:Label ID="lblFolio" runat="server"></asp:Label></td>
                                </tr>
                                <tr id="trNoFolioExiste" runat="server" visible="false">
                                    <td>Ingrese su no. de folio: </td>
                                    <td colspan="3"><asp:TextBox ID="txtFoliovalidar" runat="server" AutoPostBack="True" OnTextChanged="txtFoliovalidar_TextChanged"></asp:TextBox></td>
                                </tr>
                                <tr><td>Número de Póliza:</td>
                                    <td>
                                        <asp:TextBox ID="txtNoPoliza" runat="server" AutoPostBack="True" OnTextChanged="txtNoPoliza_TextChanged"></asp:TextBox>
                                    </td>
                                    <td align="right">Fecha:</td><td align="left"><asp:TextBox ID="txtFecha" runat="server" Width="70px"></asp:TextBox><asp:Image ID="ImgCalendar" runat="server" ImageUrl="../img/Calendar.png" />
                                    <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha" PopupButtonID="ImgCalendar"  Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr><td>Nombre del Contratante:</td><td colspan="3"><asp:TextBox ID="txtCliente" runat="server" Width="330px"></asp:TextBox></td></tr>
                                <tr>
                                    <td>Cobertura:</td>
                                    <td colspan="2" align="center">
                                        <asp:RadioButtonList ID="rdbCobertura" runat="server" TextAlign="Left" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbCobertura_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="1" Text="Básica"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Potenciación"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr id="trTrimestre" runat="server" visible="false">
                                    <td><asp:Label ID="lblTrimestreQuincena" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlTrimestreQuincena" runat="server">
                                            <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">Año:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlAnn" runat="server">
                                            <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trAnn2" runat="server" visible="false">
                                    <td>Folio:</td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodoReporte" runat="server"></asp:TextBox> 
                                    </td>
                                    <td align="right">Movimiento:</td>
                                    <td><asp:TextBox ID="txtMovimiento" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Nombre:</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNombreSolicitante" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right">Correo:</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSolicitanteCorreo" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSolicitanteCorreo" ErrorMessage="*" Font-Size="12pt"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSolicitanteCorreo" ErrorMessage="E-mail inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Width="131px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr id="trErrores" runat="server" visible="false">
                                    <td valign="top">Observaciones:</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtErrores" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor2" runat="server" Width="100%" Height="200px">
                                            <Toolbars>
                                                <dx:HtmlEditorToolbar>
                                                    <Items>
                                                        <dx:ToolbarUndoButton></dx:ToolbarUndoButton>
                                                        <dx:ToolbarRedoButton></dx:ToolbarRedoButton>
                                                        <dx:ToolbarBoldButton BeginGroup="true"></dx:ToolbarBoldButton>
                                                        <dx:ToolbarUnderlineButton></dx:ToolbarUnderlineButton>
                                                        <dx:ToolbarItalicButton></dx:ToolbarItalicButton>
                                                        <dx:ToolbarStrikethroughButton></dx:ToolbarStrikethroughButton>
                                                        <dx:ToolbarInsertLinkDialogButton BeginGroup="true"></dx:ToolbarInsertLinkDialogButton>
                                                    </Items>
                                                </dx:HtmlEditorToolbar>
                                            </Toolbars>
                                        </dx:ASPxHtmlEditor>
                                    </td>
                                </tr>
                                <tr id="trErrores2" runat="server" visible="false">
                                    <td valign="top">Comentarios:</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtErrores2" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trArchivoExcel" runat="server" visible ="false">
                                    <td valign="top">Archivo adjunto:</td>
                                    <td colspan="3" align="left">
                                        <asp:HyperLink ID="hlkExcel" runat="server"></asp:HyperLink>
                                        <asp:HyperLink ID="PDF" runat="server"></asp:HyperLink>
                                        <asp:HyperLink ID="XML" runat="server"></asp:HyperLink>
                                        <asp:HyperLink ID="CienPosPagos" runat="server" Target="_blank"></asp:HyperLink>
                                        <asp:HyperLink ID="CienPosCance" runat="server" Target="_blank"></asp:HyperLink>
                                        <asp:HyperLink ID="Documento" runat="server" Target="_blank"></asp:HyperLink>
                                        <asp:HyperLink ID="CartaPDF" runat="server" Target="_blank"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top"><asp:Panel ID="Panel2" runat="server"></asp:Panel></td>
                                    <td colspan="3"><asp:Panel ID="Panel1" runat="server"></asp:Panel></td>
                                </tr>

                                <tr>
                                    <td valign="top">Asunto:</td>
                                    <td colspan="3">
                                        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" Width="100%" Height="200px">
                                            <Toolbars>
                                                <dx:HtmlEditorToolbar>
                                                    <Items>
                                                        <dx:ToolbarUndoButton></dx:ToolbarUndoButton>
                                                        <dx:ToolbarRedoButton></dx:ToolbarRedoButton>
                                                        <dx:ToolbarBoldButton BeginGroup="true"></dx:ToolbarBoldButton>
                                                        <dx:ToolbarUnderlineButton></dx:ToolbarUnderlineButton>
                                                        <dx:ToolbarItalicButton></dx:ToolbarItalicButton>
                                                        <dx:ToolbarStrikethroughButton></dx:ToolbarStrikethroughButton>
                                                        <dx:ToolbarInsertLinkDialogButton BeginGroup="true"></dx:ToolbarInsertLinkDialogButton>
                                                    </Items>
                                                </dx:HtmlEditorToolbar>
                                            </Toolbars>
                                        </dx:ASPxHtmlEditor>

                                    </td>
                                </tr>

                            </table>
                            

                            <div id="dvEspacioPDF" runat="server" visible="false" style="width: 100%; height: 550px; vertical-align: top" tabindex="0">
                                <asp:HiddenField ID="hfIdArchivo" runat="server" Value="9999" />
                                <asp:Literal ID="ltMuestraPdf" runat="server"></asp:Literal>
                            </div>                            

                            <table align="center">
                                <tr>
                                    <td colspan="4">

                                        <asp:UpdatePanel ID="upSubirExcel" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <table id="tblSubirArchivo" runat="server">

                                                    <tr  id="trSubirExcel" runat="server">
                                                        <td>Capturar archivo:</td>
                                                        <td>
                                                            <ajaxToolkit:AsyncFileUpload id="AsyncFileUpload1" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSubirExcel" runat="server" Text="Cargar" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="btnSubirExcel_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trSubirExcelParaValidar" runat="server" visible="false">
                                                        <td>Capturar archivo:</td>
                                                        <td>
                                                            <ajaxToolkit:AsyncFileUpload id="AsyncFileUpload2" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="BtnSubirExcelParaValidar" runat="server" Text="Cargar" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="BtnSubirExcelParaValidar_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trDocumento1" runat="server" visible="false">
                                                        <td>Documento:</td>
                                                        <td><ajaxToolkit:AsyncFileUpload id="AsyncFileUpload3" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/></td>
                                                        <td><asp:Button ID="BtnDocumento" runat="server" Text="Subir Documento" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="BtnDocumento_Click" /></td>
                                                    </tr>
                                                    <tr id="trCartaPDF1" runat="server" visible="false">
                                                        <td>Documento:</td>
                                                        <td><ajaxToolkit:AsyncFileUpload id="AsyncFileUpload4" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/></td>
                                                        <td><asp:Button ID="BtnCartaPDF" runat="server" Text="Subir" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="BtnCartaPDF_Click" /></td>
                                                    </tr>
                                                    <tr id="trCartaPDF2" runat="server" visible="false">
                                                        <td></td>
                                                        <td>
                                                            <asp:GridView ID="gvArchivos" runat="server" Caption="Archivos" CellSpacing="2" CellPadding="2" BorderWidth="0" Font-Names="Tahoma" Font-Size="Small"
                                                                AutoGenerateColumns="false" OnRowDataBound="gvArchivos_RowDataBound" ShowHeader="false">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="hlkNombresarchivos" runat="server" Text='<%# Eval("Archivo") %>' Target="_blank"></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                            
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkQuitarArchivo" runat="server" Text="Quitar" CommandArgument='<%# Eval("Archivo") %>' OnClick="lnkQuitarArchivo_Click" ></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                        <td valign="bottom">&nbsp;</td>
                                                    </tr>
                                                    <tr id="trSubirPDFXLS" runat="server" visible="false">
                                                        <td colspan="3" align="center">Subir archivos:</td>
                                                    </tr>
                                                    <tr id="trsubirPDF" runat="server" visible="false">
                                                        <td>PDF:</td>
                                                        <td><ajaxToolkit:AsyncFileUpload id="AsyncFileUpload5" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/></td>
                                                        <td rowspan="2">
                                                            <asp:Button ID="BtnSubirPDFXLS" runat="server" ClientIDMode="Static" CssClass="boton" OnClick="BtnSubirPDFXLS_Click" OnClientClick="return Confirmar();" Text="Subir Archivos" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trsubirXLS" runat="server" visible="false">
                                                        <td>XML:</td>
                                                        <td><ajaxToolkit:AsyncFileUpload id="AsyncFileUpload6" runat="server" PersistFile="true" ClientIDMode="AutoID" UploaderStyle="Modern"/></td>
                                                    </tr>

                                                </table>

                                                <span id="Span1" runat="server" style="color:Red; font-size:X-large; font-style:italic; font-weight:bold;"></span>
                                                                                        
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubirExcel" />
                                                <asp:PostBackTrigger ControlID="BtnSubirExcelParaValidar" />
                                                <asp:PostBackTrigger ControlID="BtnDocumento" />
                                                <asp:PostBackTrigger ControlID="BtnCartaPDF" />
                                                <asp:PostBackTrigger ControlID="BtnSubirPDFXLS" />
                                                <asp:AsyncPostBackTrigger ControlID="gvArchivos" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    <dx:ASPxLoadingPanel ID="pnlProcesando" runat="server" ClientInstanceName="pnlProcesando" Modal="true" Text="Procesando...">
                                    </dx:ASPxLoadingPanel>
                                                
                                </td>
                            </tr>
                            </table>                       

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div id="dvBotones" style="text-align: right">            
            <asp:Button ID="BtnValidacion"  runat="server" Text="Generar Archivo 100 Posiciones"    CssClass="boton" OnClick="BtnValidacion_Click"  OnClientClick="ConfirmarProceso('Hay muchos errores para la generación de este archivo ¿Desea generar de todas formas?'); return Confirmar();" Visible="false" />&nbsp;&nbsp;
            <asp:Button ID="BtnContinuar"   runat="server" Text="Solicitar Recibo Fiscal"           CssClass="boton" OnClick="BtnContinuar_Click"   OnClientClick="return Confirmar();" Visible="false" />&nbsp;&nbsp;
            <asp:Button ID="BtnCancelar"    runat="server" Text="Suspender"                         CssClass="boton" OnClick="BtnCancelar_Click"    OnClientClick="ConfirmarProceso('¿Desea eliminar los datos? Se borrarán permamentemente'); return Confirmar();" Visible="false" />
            <asp:Button ID="BtnTerminar"    runat="server" Text="Enviar a Dependencia"              CssClass="boton" OnClick="BtnTerminar_Click"    OnClientClick="return Confirmar();" Visible="false" />
            <asp:Button ID="BtnContinuar2"  runat="server" Text="Aceptar"                           CssClass="boton" OnClick="BtnContinuar2_Click"  OnClientClick="return Confirmar();" Visible="false" />
            <asp:Button ID="BtnContinuar3"  runat="server" Text="Aceptar"                           CssClass="boton" OnClick="BtnContinuar3_Click"  OnClientClick="return Confirmar();" Visible="false" />
            <asp:Button ID="BtnContinuar4"  runat="server" Text="Solicitd Incompleta"               CssClass="boton" OnClick="BtnContinuar4_Click"  OnClientClick="return Confirmar();" Visible="false" />
            <asp:Button ID="BtnContinuar5"  runat="server" Text="Enviar a Dependencia"              CssClass="boton" OnClick="BtnContinuar5_Click"  OnClientClick="return Confirmar();" Visible="false" />
        </div>


    </fieldset>

    <br />

    <div id="DivRoot" style="margin:auto">

            <div style="overflow: hidden;" id="DivHeaderRow"></div>

            <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">

            <asp:UpdatePanel ID="upGVAgregado" runat="server">
                <ContentTemplate>

                        <asp:GridView ID="gvAgregado" runat="server" 
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
                                    Width="100%" 
                                    SelectedRowStyle-BackColor="Green" 
                                    OnRowDataBound="gvAgregado_RowDataBound"
                                    OnRowCancelingEdit="gvAgregado_RowCancelingEdit" 
                                    OnRowEditing="gvAgregado_RowEditing" 
                                    OnRowUpdating="gvAgregado_RowUpdating" 
                                    OnRowCommand="gvAgregado_RowCommand"
                                    >
                                    <Columns>
                                        <asp:BoundField DataField="Dependencia" HeaderText="Dependencia" />
                                        <asp:BoundField DataField="APaterno" HeaderText="Apellido Paterno" />
                                        <asp:BoundField DataField="AMaterno" HeaderText="Apellido Materno" />
                                        <asp:BoundField DataField="Nombres" HeaderText="Nombre(s)" />
                                        <asp:BoundField DataField="FNacimiento" HeaderText="Fecha Nacimiento" />
                                        <asp:BoundField DataField="RFC" HeaderText="RFC" />
                                        <asp:BoundField DataField="CURP" HeaderText="CURP" />
                                        <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                        <asp:BoundField DataField="CEntidadFederativa" HeaderText="Entidad Federativa" />
                                        <asp:BoundField DataField="CMunicipio" HeaderText="Ciudad/Municipio" />
                                        <asp:BoundField DataField="NivelTabular" HeaderText="Nivel Tabular" />
                                        <asp:BoundField DataField="MPercepcionOBM" HeaderText="Monto Percepción Ordinaria Bruta Mensual" />
                                        <asp:BoundField DataField="Eventual" HeaderText="Eventual" />
                                        <asp:BoundField DataField="APAsegurado" HeaderText="Apellido Paterno Asegurado" />
                                        <asp:BoundField DataField="AMAsegurado" HeaderText="Apellido Materno Asegurado" />
                                        <asp:BoundField DataField="NAsegurado" HeaderText="Nombre(s) Asegurado" />
                                        <asp:BoundField DataField="FNAsegurado" HeaderText="Fecha Nacimiento Asegurado" />
                                        <asp:BoundField DataField="CURPAsegurado" HeaderText="CURP Asegurado" />
                                        <asp:BoundField DataField="SAsegurado" HeaderText="Sexo Asegurado" />
                                        <asp:BoundField DataField="FAAsegurado" HeaderText="Fecha Afiliación Asegurado" />
                                        <asp:BoundField DataField="TAsegurado" HeaderText="Tipo de Asegurado" />
                                        <asp:BoundField DataField="FIColectividad" HeaderText="Fecha Ingreso Colectividad" />
                                        <asp:BoundField DataField="SABasica" HeaderText="Suma Asegurada Básica" />
                                        <asp:BoundField DataField="PBTReportar" HeaderText="Prima Básica Trimestre a Reportar" />
                                        <asp:BoundField DataField="MAPBasica" HeaderText="Monto de Ajuste en Prima Básica" />
                                        <asp:BoundField DataField="ITPDependencia" HeaderText="Importe total a Pagar por la Dependencia" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#deedf7" Font-Bold="True" ForeColor="#2779aa" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000000" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>

                        <asp:GridView ID="gvPotenciacion" runat="server" 
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
                            Width="100%" 
                            SelectedRowStyle-BackColor="Green" 
                            OnRowDataBound="gvPotenciacion_RowDataBound"
                            OnRowCancelingEdit="gvPotenciacion_RowCancelingEdit" 
                            OnRowEditing="gvPotenciacion_RowEditing" 
                            OnRowUpdating="gvPotenciacion_RowUpdating" 
                            OnRowCommand="gvPotenciacion_RowCommand"
                            >
                            <Columns>
                                <asp:BoundField DataField="Dependencia" HeaderText="Dependencia" />
                                <asp:BoundField DataField="APaterno" HeaderText="Apellido Paterno" />
                                <asp:BoundField DataField="AMaterno" HeaderText="Apellido Materno" />
                                <asp:BoundField DataField="Nombres" HeaderText="Nombre(s)" />
                                <asp:BoundField DataField="FNacimiento" HeaderText="Fecha Nacimiento" />
                                <asp:BoundField DataField="RFC" HeaderText="RFC" />
                                <asp:BoundField DataField="CURP" HeaderText="CURP" />
                                <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                <asp:BoundField DataField="CEntidadFederativa" HeaderText="Entidad Federativa" />
                                <asp:BoundField DataField="CMunicipio" HeaderText="Ciudad/Municipio" />
                                <asp:BoundField DataField="NivelTabular" HeaderText="Nivel Tabular" />
                                <asp:BoundField DataField="MPercepcionOBM" HeaderText="Monto Percepción Ordinaria Bruta Mensual" />
                                <asp:BoundField DataField="Eventual" HeaderText="Eventual" />
                                <asp:BoundField DataField="APAsegurado" HeaderText="Apellido Paterno Asegurado" />
                                <asp:BoundField DataField="AMAsegurado" HeaderText="Apellido Materno Asegurado" />
                                <asp:BoundField DataField="NAsegurado" HeaderText="Nombre(s) Asegurado" />
                                <asp:BoundField DataField="FNAsegurado" HeaderText="Fecha Nacimiento Asegurado" />
                                <asp:BoundField DataField="CURPAsegurado" HeaderText="CURP Asegurado" />
                                <asp:BoundField DataField="SAsegurado" HeaderText="Sexo Asegurado" />
                                <asp:BoundField DataField="FAAsegurado" HeaderText="Fecha Afiliación Asegurado" />
                                <asp:BoundField DataField="TAsegurado" HeaderText="Tipo de Asegurado" />
                                <asp:BoundField DataField="FIColectividad" HeaderText="Fecha Ingreso Colectividad" />
                                <asp:BoundField DataField="SABasica" HeaderText="Suma Asegurada Básica" />
                                <asp:BoundField DataField="SAPotenciada" HeaderText="Suma Asegurada Potenciada" />
                                <asp:BoundField DataField="SAT" HeaderText="Suma Asegurada Total" />
                                <asp:BoundField DataField="PrimaPotenciadaQR" HeaderText="Prima Potenciada Quincenal a Reportar" />
                                <asp:BoundField DataField="FormaPago" HeaderText="Forma de Pago" />
                                <asp:BoundField DataField="MontoAjustePrimaP" HeaderText="Monto de Ajuste en Prima Potenciada" />
                                <asp:BoundField DataField="ImporteTotalPagar" HeaderText="Importe Total a Pagar de la Prima Potenciada" />
                                <asp:BoundField DataField="FechaAASA" HeaderText="Fecha de Antigüedad del Asegurado en la Suma Asegurada" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#deedf7" Font-Bold="True" ForeColor="#2779aa" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000000" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        
                </ContentTemplate>
            </asp:UpdatePanel>

            </div>

            <div id="DivFooterRow" style="overflow:hidden"></div>

    </div>


</asp:Content>