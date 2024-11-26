<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsReporteColectividad.aspx.cs" Inherits="wfip.promotoria.ReporteColectividad" MasterPageFile="~/promotoria/promotoria.Master" EnableEventValidation="false" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
<script>
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

    function Confirmar() {
            pnlProcesando.Show();
        return continuar;
    }

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>REPORTE DE COLECTIVIDAD</legend>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="BtnGenerarExcel" runat="server" Text="Generar Reporte" CssClass="boton" ClientIDMode="Static" OnClientClick="return Confirmar();" OnClick="BtnGenerarExcel_Click" />&nbsp;
        <asp:Button ID="BtnExportar" runat="server" Text="Exportar a Excel" CssClass="boton" ClientIDMode="Static" OnClick="BtnExportar_Click"  Visible="false" />
        <br /><br />    
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
                            ShowFooter="true" PageSize="25" AllowPaging="true" 
                            Width="100%" OnRowCreated="gvAgregado_RowCreated" OnPageIndexChanging="gvAgregado_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Poliza" HeaderText="Poliza" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Dependencia" HeaderText="Dependencia" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Certificado" HeaderText="No. Certificado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="APaterno" HeaderText="Apellido Paterno" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="AMaterno" HeaderText="Apellido Materno" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Nombres" HeaderText="Nombre(s)" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FNacimiento" HeaderText="Fecha de Nacimiento" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="RFC" HeaderText="RFC" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="CURP" HeaderText="CURP" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Sexo" HeaderText="Sexo" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="CEntidadFederativa" HeaderText="Código Entidad Federativa" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="CMunicipio" HeaderText="Código Municipio" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="NivelTabular" HeaderText="Nivel Tabular" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="MPercepcionOBM" HeaderText="Monto Percepción Ordinaria Bruta" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Eventual" HeaderText="Eventual" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="APAsegurado" HeaderText="Apellido Paterno Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="AMAsegurado" HeaderText="Apellido Materno Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="NAsegurado" HeaderText="Nombre(s) Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FNAsegurado" HeaderText="Fecha Nacimiento Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="CURPAsegurado" HeaderText="CURP Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="SAsegurado" HeaderText="Sexo Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FAAsegurado" HeaderText="Fecha Afliación Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="TAsegurado" HeaderText="Tipo Asegurado" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FIColectividad" HeaderText="Fecha Ingreso Colectividad" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" HeaderStyle-BackColor="Aqua" />
                                <asp:BoundField DataField="FechaBaja" HeaderText="Fecha de Baja" HeaderStyle-BackColor="Aqua" />

                                <asp:BoundField DataField="SAB1PV" HeaderText="Suma Asegurada Básica del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP1PV" HeaderText="Suma Asegurada Potenciada del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT1PV" HeaderText="Suma Asegurada Total del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB1T" HeaderText="Suma Asegurada Básica del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP6Q" HeaderText="Suma Asegurada Potenciada del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT1T" HeaderText="Suma Asegurada Total del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB2T" HeaderText="Suma Asegurada Básica del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP12Q" HeaderText="Suma Asegurada Potenciada del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT2T" HeaderText="Suma Asegurada Total del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB3T" HeaderText="Suma Asegurada Básica del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP18Q" HeaderText="Suma Asegurada Potenciada del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT3T" HeaderText="Suma Asegurada Total del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB4T" HeaderText="Suma Asegurada Básica del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP24Q" HeaderText="Suma Asegurada Potenciada del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT3T" HeaderText="Suma Asegurada Total del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB1T19" HeaderText="Suma Asegurada Básica del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP6Q19" HeaderText="Suma Asegurada Potenciada del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT1T19" HeaderText="Suma Asegurada Total del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAB2T19" HeaderText="Suma Asegurada Básica del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAP12Q19" HeaderText="Suma Asegurada Potenciada del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="SAT2T19" HeaderText="Suma Asegurada Total del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#C0C0C0"  ItemStyle-HorizontalAlign="Right"/>
                                                      
                                <asp:BoundField DataField="PAB1PV" HeaderText="Prima Básica del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}"  ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP1PV" HeaderText="Prima Potenciada del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT1PV" HeaderText="Prima Total del 16 de noviembre al 31 de diciembre de 2017" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB1T" HeaderText="Prima Básica del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP1T" HeaderText="Prima Potenciada del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT1T" HeaderText="Prima Total del 1° de enero al 31 de marzo del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB2T" HeaderText="Prima Básica del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP2T" HeaderText="Prima Potenciada del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT2T" HeaderText="Prima Total del 1° de abril al 30 de junio del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB3T" HeaderText="Prima Básica del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP3T" HeaderText="Prima Potenciada del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT3T" HeaderText="Prima Total del 1° de julio al 30 de septiembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB4T" HeaderText="Prima Básica del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP4T" HeaderText="Prima Potenciada del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT4T" HeaderText="Prima Total del 1° de octubre al 31 de diciembre del 2018" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB1T19" HeaderText="Prima Básica del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP6Q19" HeaderText="Prima Potenciada del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT1T19" HeaderText="Prima Total del 1° de enero al 31 de marzo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAB2T19" HeaderText="Prima Básica del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PAP12Q19" HeaderText="Prima Potenciada del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT2T19" HeaderText="Prima Total del 1° de abril al 15 de mayo del 2019" HeaderStyle-BackColor="#808080" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>

                                <asp:BoundField DataField="PABT" HeaderText="Prima Básica Total del 16 de noviembre 2017 al 15 de mayo del 2019" HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PPT" HeaderText="Prima Potenciada total del 16 de noviembre 2017 al 15 de mayo del 2019" HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="PT" HeaderText="Prima Potenciada total del 16 de noviembre 2017 al 15 de mayo del 2019" HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"/>
                            </Columns>
                            <HeaderStyle ForeColor="Black" />
                            <RowStyle Font-Size="XX-Small" />
                        </asp:GridView>                
                            
                            </ContentTemplate>
                        </asp:UpdatePanel>
        
                    </div>

                    <div id="DivFooterRow" style="overflow:hidden"></div>

            </div>

            <dx:ASPxLoadingPanel ID="pnlProcesando" runat="server" ClientInstanceName="pnlProcesando" Modal="true" Text="Procesando..."></dx:ASPxLoadingPanel>
        
        </fieldset>

</asp:Content>