<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/iniPromotoria.Master" AutoEventWireup="true" CodeBehind="EmisionVidaRes.aspx.cs" Inherits="wfip.promotoria.EmisionVidaRes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="upPnlCaptura" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
       
     <!-- -->
    <div class="modal fade bd-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModal" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLongTitle">Confirmación</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                ¿Deseas continuar con el trámite?
              </div>
              <div class="modal-footer">
                  <asp:Button ID="Button1" runat="server"  AutoPostBack="True" Text="Continuar" CssClass="btn btn-info" OnClick="Continuar" />&nbsp;&nbsp;
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
              </div>
            </div>
        </div>
    </div>

     <div class="row">
        <div class="x_panel">
            <br />
            <div class="container">
                <div class="col-md-12 col-sm-12 col-xs-12 text-left">
                    <div class="card">
                        <div class="card-header">
                        <h3><small>INDIVIDUAL PRIVADO EMISIÓN </small></h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <h2><small style="color:#007CC3">PÓLIZA /SEGURO</small></h2>
                            </div>
                            <asp:Panel ID="Limpiar" runat="server" Visible="false">
                                <asp:Button ID="btnlimpiar" runat="server" CssClass="btn btn-info col-md-2 col-sm-12" AutoPostBack="True" CausesValidation="false" OnClick="BtnLimpiar" Text="Limpiar" Visible="true"/>&nbsp;&nbsp;
                            </asp:Panel>
                            <hr />
                            <!-- RAMO, PRODUCTO MONEDA ETC. -->
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <asp:Label ID="lblRamo" runat="server" Text="* Ramo" Font-Bold="true" class="control-label col-md-4 col-sm-4 col-xs-12 "></asp:Label>
                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                        <asp:DropDownList ID="TramiteTipPoliza" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TramiteTipPoliza_SelectedIndexChanged" class="form-control">
                                            <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="TramiteTipPoliza" ForeColor="Red" ErrorMessage="*" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Panel ID="Tramite" runat="server" Visible="false">
                                        <asp:Panel ID="producto1" runat="server" Visible="true">
                                            <asp:Label ID="lblProductoRamo" runat="server" Text="* Producto" Font-Bold="true" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:DropDownList ID="LisProducto1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisProducto1_SelectedIndexChanged" class="form-control">
                                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                                </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LisProducto1" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                           </div>
                                        </asp:Panel>
                                        <asp:Panel ID="subproducto1" runat="server" Visible="false">
                                            <asp:Label ID="lblSubProductoRamo" runat="server" Text="Plan" Font-Bold="true" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:DropDownList ID="LisSubproducto1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisSubproducto1_SelectedIndexChanged" class="form-control">
                                                    <asp:ListItem Value=" ">SELECCIONAR</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LisSubproducto1" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                       </asp:Panel>
                                        <asp:Panel ID="producto2" runat="server" Visible="false">
                                            <asp:Label ID="Label2" runat="server" Text="Producto" Font-Bold="true" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:DropDownList ID="LisProducto2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LisProducto1_SelectedIndexChanged" class="form-control">
                                                    <asp:ListItem Value=" ">SELECCIONAR</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="subproducto2" runat="server" Visible="false">
                                            <asp:Label ID="Label3" runat="server" Text="Plan" Font-Bold="true" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:DropDownList ID="LisSubproducto2" runat="server" AutoPostBack="True" class="form-control">
                                                    <asp:ListItem Value=" ">Seleccionar</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                       </asp:Panel>
                                        <asp:Panel ID="ActCPDES" runat="server" Visible="false">
                                            <asp:Label runat="server" ID="lblCPDES" Text="* CPDES" Font-Bold="True" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                            <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                                <asp:DropDownList ID="ActividadCPDES" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActividadCPDES_SelectedIndexChanged" class="form-control">
                                                    <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                                    <asp:ListItem Value="True">SI</asp:ListItem>
                                                    <asp:ListItem Value="False">NO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" controltovalidate="ActividadCPDES" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <asp:Label runat="server" ID="Moneda" Text="* Moneda" Font-Bold="True" class="control-label col-md-5 col-sm-5 col-xs-12 "></asp:Label>
                                    <div class="col-md-7 col-sm-7 col-xs-12 form-group has-feedback">
                                        <asp:DropDownList ID="cboMoneda" runat="server" AutoPostBack="True"  class="form-control" OnSelectedIndexChanged="cboMoneda_SelectedIndexChanged"  ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="cboMoneda" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Label runat="server" ID="SumaAsegurada" Text="* Suma Asegurada Básica" Font-Bold="True" class="control-label col-md-5 col-sm-5 col-xs-12 "></asp:Label>
                                    <div class="col-md-7 col-sm-7 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="txtSumaAseguradaBasica" OnTextChanged="CalculartSumaAsegurada" onChange="MASK('cph_areaTrabajo_txtSumaAseguradaBasica','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control "></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaBasica" ValidChars="0123456789.," />
                                        <asp:RequiredFieldValidator id="RequiredFieldValidator27" InitialValue="0.00"  ControlToValidate="txtSumaAseguradaBasica" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                    </div>

                                    <!--  DATOS MOSTRADOS APARTIR DE LO SELECIONADO -->
                                    <asp:Panel ID="SumaAseguradaPolizasVigentes" runat="server" Visible="False">
                                        <asp:Label runat="server" ID="labelSumaAseguradaPolizasVigentes" Text="Suma Asegurada de Pólizas Vigentes " Font-Bold="false" class="control-label col-md-5 col-sm-5 col-xs-12 "></asp:Label>
                                        <div class="col-md-7 col-sm-7 col-xs-12 form-group has-feedback">
                                            <asp:TextBox ID="txtSumaAseguradaPolizasVigentes" OnTextChanged="CalculartSumaAsegurada"  onChange="MASK('cph_areaTrabajo_txtSumaAseguradaPolizasVigentes','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15"  AutoPostBack="true" class="form-control "></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars" TargetControlID="txtSumaAseguradaPolizasVigentes" ValidChars="0123456789.," />
                                        </div>

                                        <asp:Label runat="server" ID="label9" Text="* Prima Total de Acuerdo a Cotización " Font-Bold="true" class="control-label col-md-5 col-sm-5 col-xs-12 "></asp:Label>
                                        <div class="col-md-7 col-sm-7 col-xs-12 form-group has-feedback">
                                            <asp:TextBox ID="txtPrimaTotal" OnTextChanged="PrimaTotalGrandesSumas" onChange="MASK('cph_areaTrabajo_txtPrimaTotal','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control "></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19"  runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotal" ValidChars="0123456789.," />
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator32" InitialValue="0.00"  ControlToValidate="txtPrimaTotal" ErrorMessage="*" runat="server" ForeColor="Red"/>
                                         </div>

                                        <asp:Label ID="PrimaTotalGrandeSumas" runat="server" Font-Bold="true" ForeColor="#5B8212" class="control-label col-md-4 col-sm-4 col-xs-12 "></asp:Label>
                                        <asp:Label ID="GrandeSumas" runat="server" Font-Bold="true" ForeColor="#5B8212" class="control-label col-md-4 col-sm-4 col-xs-12 "></asp:Label>
                                    </asp:Panel>

                                    <asp:Panel ID="SumaAseguradaPolizasVigentesGMM" runat="server" Visible="false">
                                        <asp:Label runat="server" ID="label10" Text="Prima Total de Acuerdo a Cotización " class="control-label col-md-5 col-sm-5 col-xs-12 "></asp:Label>
                                        <div class="col-md-7 col-sm-7 col-xs-12 form-group has-feedback">
                                            <asp:TextBox ID="txtPrimaTotalGMM" onChange="MASK('cph_areaTrabajo_txtPrimaTotalGMM','###,###,###,###,##0.00',1)" onfocus="if(this.value == '0.00') {this.value=''}" onblur="if(this.value == ''){this.value ='0.00'}" value="0.00" runat="server" MaxLength="15" AutoPostBack="true" class="form-control "></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"  runat="server" FilterMode="ValidChars" TargetControlID="txtPrimaTotalGMM" ValidChars="0123456789.," />
                                        </div>  
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <asp:Panel ID="CPDS" runat="server" Visible="false">
                                        <asp:Label runat="server" ID="lblFolioCPDES" Text="* Folio CPDES" Font-Bold="True" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                        <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">    
                                            <asp:TextBox ID="textFolioCPDES" runat="server" MaxLength="15" AutoPostBack="false" Wrap="False"  onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" class="form-control"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterMode="ValidChars" TargetControlID="textFolioCPDES" ValidChars="0123456789" />
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator12" controltovalidate="textFolioCPDES" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                        </div>
                                        <asp:Label runat="server" ID="lblEstatusCPDES" Text="* Estatus" Font-Bold="True" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                        <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">    
                                            <asp:DropDownList ID="EstatusCPDES" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActividadCPDES_SelectedIndexChanged" class="form-control">
                                                <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                                <asp:ListItem Value="Sub-aceptado">SUB-ACEPTADO</asp:ListItem>
                                                <asp:ListItem Value="Manual">MANUAL</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" controltovalidate="EstatusCPDES" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                         </div>  
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label runat="server" ID="Label1" Text="Hombres Clave" class="control-label col-md-1 col-sm-1 col-xs-12 text-center"></asp:Label>
                                <asp:CheckBox ID="HombresClave"  runat="server" AutoPostBack="True" Text=" Si"  /><br />
                            </div>
                            <div class="row">
                                <asp:Label runat="server" ID="LblOneShot" Text="OneShot" CssClass="control-label col-md-1 col-sm-1 col-xs-12 text-center" Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkOneShot" runat="server" AutoPostBack="True" Text="Si" Visible="false" /><br />
                            </div>
                            <div class="row">
                                <asp:Label runat="server" ID="lblMetaLifeEspecial" Text="Metalife Especial" CssClass="control-label col-md-1 col-sm-1 col-xs-12 text-center" Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkMetaLifeEspecial" runat="server" AutoPostBack="True" Text="Si" Visible="false" /><br />
                            </div>
                            <div class="form-group">
                                <h2><small style="color:#007CC3">INFORMACIÓN DE LA PÓLIZA</small></h2>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <asp:Label runat="server" ID="LabelClave" Text="Clave promotoria" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="texClave" runat="server" MaxLength="5" AutoPostBack="false" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:Label runat="server" ID="lblFechaSolicitud" Text="* Fecha Solicitud" Font-Bold="True" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                        <dx:ASPxDateEdit ID="dtFechaSolicitud" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" AutoPostBack="true">
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="dtFechaSolicitud" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                    <asp:Label runat="server" ID="Label6" Text="Solicitud / Número de Orden" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="textNumeroOrden" runat="server" MaxLength="15" AutoPostBack="false" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"  class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:Label runat="server" ID="lblTipoContratante" Text="* Tipo de Contratante" Font-Bold="True"  class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                        <asp:DropDownList ID="cboTipoContratante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboTipoContratante_SelectedIndexChanged" class="form-control">
                                            <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value="Fisica">PERSONA FÍSICA</asp:ListItem>
                                            <asp:ListItem Value="Moral">PERSONA MORAL</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTipoContratante" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="cboTipoContratante" ForeColor="Red" InitialValue="0" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                     <asp:Label runat="server" ID="Label4" Text="Región" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                     <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                         <asp:TextBox ID="texRegion" runat="server" MaxLength="5" AutoPostBack="false" TextMode="MultiLine" Rows="1" class="form-control"></asp:TextBox>
                                     </div>
                                    <asp:Label runat="server" ID="Label5" Text="Gerente comercial" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="texGerenteComercial" runat="server" TextMode="MultiLine" Rows="1" AutoPostBack="false"  class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:Label runat="server" ID="Label7" Text="Ejecutivo comercial" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="texEjecuticoComercial" runat="server" TextMode="MultiLine" Rows="1"  AutoPostBack="false" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:Label runat="server" ID="Label8" Text="Ejecutivo Front" class="control-label col-md-4 col-sm-4 col-xs-12"></asp:Label>
                                    <div class="col-md-8 col-sm-8 col-xs-12 form-group has-feedback">
                                        <asp:TextBox ID="texEjecuticoFront" runat="server" TextMode="MultiLine" Rows="1"  AutoPostBack="false" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnPrsFisica" runat="server" Visible="false">
                            <div class="form-group">
                                <h2><small style="color:#007CC3">INFORMACIÓN CONTRATANTE</small></h2>
                            </div>
                            <hr />   
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="lblNombre" Text="*Nombre(s)" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <asp:TextBox ID="txNombre" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
								        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNombre" runat="server" FilterMode="ValidChars" TargetControlID="txNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="lblAPaterno" Text="*Apellido Paterno" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <asp:TextBox ID="txApPat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApPat" runat="server" FilterMode="ValidChars" TargetControlID="txApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" controltovalidate="txApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="lblAMaterno" Text="*Apellido Materno" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <asp:TextBox ID="txApMat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txApMat" runat="server" FilterMode="ValidChars" TargetControlID="txApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" controltovalidate="txApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="Label12" Text="* Sexo" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class=" form-group has-feedback">
                                        <asp:DropDownList ID="txSexo" runat="server" class="form-control" >
                                            <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value="Masculino">MASCULINO</asp:ListItem>
                                            <asp:ListItem Value="Femenino">FEMENINO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txSexo" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="lblRFCPFisica" Text="* RFC" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class=" form-group has-feedback">
                                        <div class="input-group col-xs-12">
                                            <asp:TextBox ID="txRfc" runat="server" MaxLength="13" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button  runat="server" CausesValidation="False" Text="Calcular" class="btn btn-primary" ToolTip="RFC" OnClick="dtFechaNacimiento_OnChanged" />
                                            </span>
                                        </div>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txRfc" runat="server" FilterMode="ValidChars" TargetControlID="txRfc" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                        <asp:RegularExpressionValidator ID="rev_txRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="RFC INVALIDO" Text="*" Font-Size="16px" ForeColor="Red" ValidationExpression="[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ErrorMessage="RFC" Text="*" ControlToValidate="txRfc" ForeColor="Red" ValidationGroup="vdFisica"></asp:RequiredFieldValidator>
                                    <code><asp:Label ID="textRFCFisica" runat="server" ForeColor="Crimson" ></asp:Label></code>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="LabelNacionalidad" Text="* Nacionalidad" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback"> 
                                        <dx:ASPxComboBox ID="txNacionalidad" runat="server" Theme="Material" Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="LisNacionalidad_SelectedIndexChanged" class="form-control">
                                        </dx:ASPxComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txNacionalidad" ErrorMessage="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <code style="text-align:center;"><asp:Label ID="textNacionalidad" runat="server" ForeColor="Crimson" class="control-label" ></asp:Label></code>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                     <asp:Label runat="server" ID="Label13" Text="* Fecha Nacimiento" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <dx:ASPxDateEdit ID="dtFechaNacimiento" runat="server" Theme="Material" EditFormat="Custom" Width="100%" AutoPostBack="true" OnDateChanged="dtFechaNacimiento_OnChanged">
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" controltovalidate="dtFechaNacimiento" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>  
                                </div>
                               <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                   <asp:Label runat="server" ID="Label14" Text="* Estado" Font-Bold="True" class="control-label"></asp:Label>
                                   <div class="form-group has-feedback">
                                        <asp:DropDownList ID="cboEstado" runat="server"  class="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="cboEstado" ErrorMessage="*" Font-Size="16px" ForeColor="Red" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                        <asp:Label ID="Label15" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                    </div>
                               </div>
                                
                                
                                
                            </div>
                            </asp:Panel>
                            <asp:Panel ID="pnPrsMoral" runat="server" Visible="false">
                                <div class="form-group">
                                <h2><small style="color:#007CC3">INFORMACIÓN CONTRATANTE</small></h2>
                            </div>
                            <hr /> 
                            <div class="row"> 
                                <asp:Label runat="server" ID="lblNombrePMoral" Text="*Nombre" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"/>
                                <div class="col-md-7 col-sm-7 col-xs-12 form-group has-feedback">
                                    <asp:TextBox ID="txNomMoral" runat="server" MaxLength="100" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteNomMoral" runat="server" FilterMode="ValidChars" TargetControlID="txNomMoral" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ&" />
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="txNomMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                            </div>
                            <div class="row"> 
                                <asp:Label runat="server" ID="LabelMoralFechaConstitucion" Text="* Fecha de Constitución" Font-Bold="True" class="control-label col-md-2 col-sm-2 col-xs-6"></asp:Label>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                    <dx:ASPxDateEdit ID="dtFechaConstitucion" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" AutoPostBack="true" OnDateChanged="dtFechaConstitucion_OnChanged">
                                        <TimeSectionProperties>
                                            <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                        </TimeSectionProperties>
                                        <CalendarProperties>
                                            <FastNavProperties DisplayMode="Inline"/>
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="dtFechaConstitucion" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <asp:Label runat="server" ID="lblRFCPMoral" Text="*RFC" Font-Bold="True" class="control-label col-md-1 col-sm-1 col-xs-6"></asp:Label>
                                <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                    <div class="input-group col-xs-11">
                                    <asp:TextBox ID="txRfcMoral" runat="server" MaxLength="12" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button  runat="server" CausesValidation="False" Text="Calcular" class="btn btn-primary" ToolTip="RFC" OnClick="dtFechaConstitucion_OnChanged" />
                                    </span>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteRfcMoral" runat="server" FilterMode="ValidChars" TargetControlID="txRfcMoral" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890" />
                                    <asp:RegularExpressionValidator ID="revRfcMoral" runat="server" ControlToValidate="txRfcMoral" ErrorMessage="*" Font-Size="16px" ForeColor="Red" ValidationExpression="^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txRfcMoral" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 text-center">
                                    <code><asp:Label runat="server" ID="TextantecedentesRFC" Text=""></asp:Label></code>
                                </div>
                            </div>
                            </asp:Panel>
                            <div class="row">
                                <asp:Label runat="server" ID="Label11" Text="¿El solicitante es el mismo que el contratante?" class="control-label col-md-3 col-sm-3 col-xs-12 text-center"></asp:Label>
                                <asp:CheckBox ID="CheckBox2"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox2_CheckedChanged" Text="Si" Checked="true" />
                                <asp:CheckBox ID="CheckBox1"  runat="server" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" Text="No" />
                            </div>
                            <asp:Panel ID="DiferenteContratante" runat="server" Visible="false">
                            <div class="form-group">
                                <h2><small style="color:#007CC3">INFORMACIÓN TITULAR</small></h2>
                            </div>
                            <hr /> 
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="LabelTitularNombre" Text="*Nombre(s)" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <asp:TextBox ID="txTiNombre" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txTiNombre" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" controltovalidate="txTiNombre" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="LabelTitularApellidoPaterno" Text="*Apellido paterno" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <asp:TextBox ID="txTiApPat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txTiApPat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator14" controltovalidate="txTiApPat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="LabelApellidoMaterno" Text="*Apellido materno" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <asp:TextBox ID="txTiApMat" runat="server" MaxLength="64" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txTiApMat" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.áéíóúÁÉÍÓÚ" />
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" controltovalidate="txTiApMat" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="LabelTitularNacionalidad" Text="*Nacionalidad" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <dx:ASPxComboBox ID="txTiNacionalidad" runat="server" Theme="Material" EditFormat="Custom" Width="100%" SelectedIndex="136" AutoPostBack="true" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()" OnSelectedIndexChanged="LisTitNacionalidad_SelectedIndexChanged">
                                        </dx:ASPxComboBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" controltovalidate="txTiNacionalidad" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                        <code><asp:Label ID="textTitularNacionalidad" runat="server" ForeColor="Crimson" class="control-label" ></asp:Label></code>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="LabelTitularSexo" Text="*Sexo" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <asp:DropDownList ID="txtSexoM" runat="server" class="form-control">
                                            <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                            <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Tipo de contratante" Text="*" ControlToValidate="txtSexoM" ForeColor="Red" InitialValue="" Font-Size="16px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="LabelTitularFechaNacimiento" Text="*Fecha Nacimiento" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <dx:ASPxDateEdit ID="dtFechaNacimientoTitular" runat="server" Theme="Material" EditFormat="Custom" Width="100%" Caption="" >
                                            <TimeSectionProperties>
                                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                                            </TimeSectionProperties>
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline"/>
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator26" controltovalidate="dtFechaNacimientoTitular" ForeColor="Crimson" errormessage="*" Font-Size="16px"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                    <asp:Label runat="server" ID="Label16" Text="* Estado" Font-Bold="True" class="control-label"></asp:Label>
                                    <div class="form-group has-feedback">
                                        <asp:DropDownList ID="cboEstado2" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="cboEstado2" ErrorMessage="Estado" Font-Size="16px" ForeColor="Red" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                        <asp:Label ID="Label17" runat="server" Font-Size="16px" ForeColor="Crimson"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            </asp:Panel>
                            <div class="form-group">
                                <h2><small style="color:#007CC3">OBSERVACIONES</small></h2>
                            </div>
                            <hr /> 
                            <div class="col-md-9 col-sm-9 col-xs-12 form-group has-feedback">
                                <asp:TextBox ID="txDetalle" runat="server" Font-Size="14px" TextMode="MultiLine" class="form-control" onKeyUp="document.getElementById(this.id).value=document.getElementById(this.id).value.toUpperCase()"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteDetalle" runat="server" FilterMode="ValidChars" TargetControlID="txDetalle" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzáéíóúÁÉÍÓÚ = $%*_0123456789-,.:+*/?¿+¡\/][{};" />
                                <br />
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-12 form-group has-feedback">
                                <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Continuar" CssClass="btn btn-info col-md-12 col-sm-12" OnClick="BtnContinuar_Click" />&nbsp;&nbsp;
                                <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger col-md-12 col-sm-12" OnClick="BtnCancelar_Click" CausesValidation="false" />
                                <asp:Label ID="Mensajes" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>
                                <asp:Label ID="SumaBasica" runat="server" Font-Size="12px" ForeColor="Crimson"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function calcula(idForm) {

            var valor = parseFloat($("#" + idForm).val());
            var n = new Intl.NumberFormat("es-MX").format(valor);
            var s = "0.00";
            
            $("#" + idForm).val(n);
        }

        function MASK(idForm, mask, format) {
            var n = $("#" + idForm).val();
            if (format === "undefined") format = false;
            if (format || NUM(n)) {
                dec = 0, point = 0;
                x = mask.indexOf(".") + 1;
                if (x) { dec = mask.length - x; }

                if (dec) {
                    n = NUM(n, dec) + "";
                    x = n.indexOf(".") + 1;
                    if (x) { point = n.length - x; } else { n += "."; }
                } else {
                    n = NUM(n, 0) + "";
                }
                for (var x = point; x < dec; x++) {
                    n += "0";
                }
                x = n.length, y = mask.length, XMASK = "";
                while (x || y) {
                    if (x) {
                        while (y && "#0.".indexOf(mask.charAt(y - 1)) === -1) {
                            if (n.charAt(x - 1) !== "-")
                                XMASK = mask.charAt(y - 1) + XMASK;
                            y--;
                        }
                        XMASK = n.charAt(x - 1) + XMASK, x--;
                    } else if (y && "$0".indexOf(mask.charAt(y - 1)) + 1) {
                        XMASK = mask.charAt(y - 1) + XMASK;
                    }
                    if (y) { y-- }
                }
            } else {
                XMASK = "";
            }
            $("#" + idForm).val(XMASK);
            return XMASK;
        }

        // Convierte una cadena alfanumérica a numérica (incluyendo formulas aritméticas)
        //
        // s   = cadena a ser convertida a numérica
        // dec = numero de decimales a redondear
        //
        // La función devuelve el numero redondeado

        function NUM(s, dec) {
            for (var s = s + "", num = "", x = 0; x < s.length; x++) {
                c = s.charAt(x);
                if (".-+/*".indexOf(c) + 1 || c !== " " && !isNaN(c)) { num += c; }
            }
            if (isNaN(num)) { num = eval(num); }
            if (num === "") { num = 0; } else { num = parseFloat(num); }
            if (dec !== undefined) {
                r = .5; if (num < 0) r = -r;
                e = Math.pow(10, (dec > 0) ? dec : 0);
                return parseInt(num * e + r) / e;
            } else {
                return num;
            }
        }

    </script>
    <script type="text/javascript"> 
        function WebForm_OnSubmit() {
            if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
                for (var i in Page_Validators) {
                    try {
                        var control = document.getElementById(Page_Validators[i].controltovalidate);
                        if (!Page_Validators[i].isvalid) {
                            control.className = "form-control-error";
                        } else {
                            control.className = "form-control";
                        }
                    } catch (e) { }
                }
                return false;
            }
            return true;
        }
    </script> 
</asp:Content>
