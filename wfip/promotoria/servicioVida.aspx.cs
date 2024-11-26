using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class servicioVida : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("tramite");
                this.llenaListaTramites();
                identificaPromotoria();
                cargarNacionalidades_db(ref txNacionalidad);
                
            }

            // VALIDA EL TIPO DE TRAMITE TANTO PUBLICO COMO PRIVADO
            if (!String.IsNullOrEmpty(Request.QueryString["t"]))
            {
                // Query string value is there so now use it
                String TipoTramite = Request.QueryString["t"];
                if (TipoTramite == "privado" || TipoTramite == "publico")
                {
                    Label2.Text = TipoTramite.ToUpper();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        private void identificaPromotoria()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    hf_IdPromotoria.Value = manejo_sesion.Credencial.IdPromotoria.ToString();
                }
            }
        }
        /// <summary>
        /// Carga todas la nacionalidades definida en DB
        /// </summary>
        private void cargarNacionalidades_db(ref DropDownList objDDL)
        {
            DataTable dtPaises = (new wfiplib.admEmisionVG()).cargaPaises();
            objDDL.DataSource = dtPaises;
            objDDL.DataTextField = "Nombre";
            objDDL.DataValueField = "Id";
            objDDL.DataBind();
            objDDL.SelectedIndex = 136;
        }

        /// <summary>
        /// Carga todas la nacionalidades definida en Microsoft.Framework
        /// </summary>
        private void cargarNacionalidades(ref DropDownList objDDL)
        {
            RegionInfo reginfo;                            //Definiendo un objeto RegionInfo    
            //Creando una lista de todas las culturas.....
            CultureInfo[] cultInfoList = CultureInfo.GetCultures(CultureTypes.AllCultures);
            //Explorando todas las culturas (no todas retornan paises que se encuentran en RegionInfo            
            foreach (CultureInfo cultInfo in cultInfoList)
            {
                //Se puede generar una excepción por no corresponder un culture info LCID con 
                //un un código existente en RegInfo (por ejemplo Cuba) en ese caso se captura 
                //la excepción y continua el lazo
                try
                {
                    //Crear una clase reginfo para traer los nombres del país
                    reginfo = new RegionInfo(cultInfo.LCID);                //Se crea una reg info del pais
                    //Crear un ListItem para almacenar el nombre del país y el código de dos letras ISO 
                    ListItem li = new ListItem(reginfo.DisplayName, reginfo.TwoLetterISORegionName);
                    //Debido a que diferentes culture info pueden generar diferentes varias veces el
                    //mismo pais, verificar que el pais ya no se encuentre.
                    if (objDDL.Items.IndexOf(li) < 1)
                    {
                        objDDL.Items.Add(li);
                    }
                }
                catch //Captura de la excepción por falta de correspondencia de código
                {
                }
            }
            // Sort
            sortNacionalidad(ref objDDL);
        }

        private void sortNacionalidad(ref DropDownList objDDL)
        {
            ArrayList textList = new ArrayList();
            ArrayList valueList = new ArrayList();
            int itemDefault = 0;
            foreach (ListItem li in objDDL.Items)
            {
                textList.Add(li.Text);
            }
            textList.Sort();
            foreach (object item in textList)
            {
                string value = objDDL.Items.FindByText(item.ToString()).Value;
                valueList.Add(value);
            }
            objDDL.Items.Clear();
            for (int i = 0; i < textList.Count; i++)
            {
                if (textList[i].ToString() == "México")
                {
                    itemDefault = i;
                }
                ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
                objDDL.Items.Add(objItem);
            }
            objDDL.SelectedIndex = itemDefault;
        }
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("esperaPromotoria.aspx");
        }

        private void llenaListaTramites()
        {
            List<wfiplib.tipoTramiteCampo> lstGrupo1 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.serviciosVida, 1);
            List<wfiplib.tipoTramiteCampo> lstGrupo2 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.serviciosVida, 2);
            List<wfiplib.tipoTramiteCampo> lstGrupo3 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.serviciosVida, 3);
            List<wfiplib.tipoTramiteCampo> lstGrupo4 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.serviciosVida, 4);
            List<wfiplib.tipoTramiteCampo> lstGrupo5 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.serviciosVida, 5);
            List<wfiplib.tipoTramiteCampo> lstGrupo6 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.serviciosVida, 6);
            List<wfiplib.tipoTramiteCampo> lstGrupo7 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.serviciosVida, 7);
            List<wfiplib.tipoTramiteCampo> lstGrupo8 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.serviciosVida, 8);

            foreach (wfiplib.tipoTramiteCampo reg in lstGrupo1)
            {
                ListItem itm = new ListItem();
                switch (reg.Campo)
                {
                    case "C1114":
                    case "C1115":
                        break;
                    default:
                        itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                        itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                        break;
                }
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGrupo1.Items.Add(itm);
            }


            foreach (wfiplib.tipoTramiteCampo reg in lstGrupo2)
            {
                ListItem itm = new ListItem();
                switch (reg.Campo)
                {
                    case "C1117":
                    case "C1122":
                        break;
                    default:
                        itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                        itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                        break;
                }
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGrupo2.Items.Add(itm);
            }

            foreach (wfiplib.tipoTramiteCampo reg in lstGrupo3)
            {
                ListItem itm = new ListItem();
                itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGrupo3.Items.Add(itm);
            }

            foreach (wfiplib.tipoTramiteCampo reg in lstGrupo4)
            {
                ListItem itm = new ListItem();
                itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGrupo4.Items.Add(itm);
            }

            foreach (wfiplib.tipoTramiteCampo reg in lstGrupo5)
            {
                chkGrupo5.Items.Add(new ListItem(reg.Descripcion, reg.Campo));
            }

            foreach (wfiplib.tipoTramiteCampo reg in lstGrupo6)
            {
                ListItem itm = new ListItem();
                switch (reg.Campo)
                {
                    case "C1138":
                    case "C1141":
                        break;
                    default:
                        itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                        itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                        break;
                }
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGrupo6.Items.Add(itm);
            }

            foreach (wfiplib.tipoTramiteCampo reg in lstGrupo7)
            {
                ListItem itm = new ListItem();
                itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGrupo7.Items.Add(itm);

            }
            foreach (wfiplib.tipoTramiteCampo reg in lstGrupo8)
            {
                chkGrupo8.Items.Add(new ListItem(reg.Descripcion, reg.Campo));
            }
        }
        /*
        protected void antecedentesRFC(object sender, EventArgs e)
        {
            TextantecedentesRFC.Text = "";
            textRFCFisica.Text = "";

            if (cboTipoContratante.SelectedValue.Equals("1"))
            {
                string RFC = txRfc.Text.Trim();
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    textRFCFisica.Text = "Ya existen trámites registrados para el RFC";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("2"))
            {
                string RFC = txRfcMoral.Text.Trim();
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    TextantecedentesRFC.Text = "Ya existen trámites registrados para el RFC";
                }
            }
        }
        */
        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            
            try
            {
                // ALAMCENA LOS DATOS APARTIR DE LA FUNCION recuperaCaptura
                wfiplib.serviciosVidaP oDatos = recuperaCaptura();
                //wfiplib.serviciosVida oDatos = recuperaCaptura();

                int idTramite = armaTramiteYGuardaEnMemoria(oDatos.DatosHtml);
                if (idTramite > 0)
                {
                    oDatos.IdTramite = idTramite;
                    Session[wfiplib.E_TipoTramite.serviciosVida.ToString()] = oDatos;
                    Response.Redirect("anexaArchivos.aspx");
                }
            }
            catch (Exception ex)
            {
                enviaMsgCliente(ex.Message);
            }
        }

        private int armaTramiteYGuardaEnMemoria(string DatosHtml)
        {
            int Id = (new wfiplib.admTramite()).siguienteId();

            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            wfiplib.tramiteP oTramite = new wfiplib.tramiteP();

            oTramite.Id = Id;
            oTramite.IdTipoTramite = wfiplib.E_TipoTramite.serviciosVida;
            oTramite.IdPromotoria = (new wfiplib.admCredencial()).daPromotoria(manejo_sesion.Credencial.Id);
            oTramite.IdUsuario = manejo_sesion.Credencial.Id;
            oTramite.DatosHtml = DatosHtml;
            oTramite.AgenteClave = Convert.ToString(txIdAgente.Text.Trim().ToUpper());
            oTramite.NumeroOrden = textNumeroOrden.Text.Trim().ToUpper();
            oTramite.FechaSolicitud = txFechaSol.Text.Trim().ToUpper();
            oTramite.TipoTramite = Label2.Text.ToString();
            /*
            oTramite.CPDES = ActividadCPDES.Text.Trim().ToUpper();
            if (ActividadCPDES.SelectedValue.Equals("SI"))
            {
                oTramite.FolioCPDES = textFolioCPDES.Text.Trim().ToUpper();
                oTramite.EstatusCPDES = EstatusCPDES.Text.Trim().ToUpper();
            }
            */
            Session["tramite"] = oTramite;
            return Id;
        }

        private wfiplib.serviciosVidaP recuperaCaptura()
        {
            // SE CREO LA CLASE serviciosVidaP para actualizar los nuevos datos
            wfiplib.serviciosVidaP resultado = new wfiplib.serviciosVidaP();
            //wfiplib.serviciosVida resultado = new wfiplib.serviciosVida();

            try
            {
                resultado.CPDES = ActividadCPDES.Text.Trim().ToUpper();
                if (ActividadCPDES.SelectedValue.Equals("Si"))
                {
                    resultado.FolioCPDES = textFolioCPDES.Text.Trim().ToUpper();
                    resultado.EstatusCPDES = EstatusCPDES.Text.Trim().ToUpper();
                }
                resultado.NumPoliza = txPoliza.Text.Trim().ToUpper();
                if (cboTipoContratante.SelectedValue.Equals("1"))
                {
                    resultado.TipoPersona = wfiplib.E_TipoPersona.Fisica;
                    resultado.Nombre = txNombre.Text.Trim().ToUpper();
                    resultado.ApPaterno = txApPat.Text.Trim().ToUpper();
                    resultado.ApMaterno = txApMat.Text.Trim().ToUpper();
                    resultado.RFC = txRfc.Text.Trim().ToUpper();
                    resultado.Nacionalidad = txNacionalidad.Text.Trim();
                    //resultado.CURP = txCurp.Text.Trim().ToUpper();
                }
                else if (cboTipoContratante.SelectedValue.Equals("2"))
                {
                    resultado.TipoPersona = wfiplib.E_TipoPersona.Moral;
                    resultado.Nombre = txNomMoral.Text.Trim().ToUpper();
                    resultado.RFC = txRfcMoral.Text.Trim().ToUpper();
                }

                resultado.Detalle = txDetalle.Text.Trim();

                //bool ConAfectacion = false;
                foreach (ListItem reg in chkGrupo1.Items)
                {
                    if (reg.Selected)
                    {
                        if (reg.Value.Equals("C119"))
                        {
                            resultado.C119 = 1;
                        }
                        if (reg.Value.Equals("C1110"))
                        {
                            resultado.C1110 = 1;
                        }
                        if (reg.Value.Equals("C1111"))
                        {
                            resultado.C1111 = 1;
                        }
                        if (reg.Value.Equals("C1112"))
                        {
                            resultado.C1112 = 1;
                        }
                        if (reg.Value.Equals("C1113"))
                        {
                            resultado.C1113 = 1;
                        }
                        if (reg.Value.Equals("C1114"))
                        {
                            resultado.C1114 = 1;
                        }
                        if (reg.Value.Equals("C1115"))
                        {
                            resultado.C1115 = 1;
                        }
                    }
                }

                foreach (ListItem reg in chkGrupo2.Items)
                {
                    if (reg.Selected)
                    {
                        if (reg.Value.Equals("C1116"))
                        {
                            resultado.C1116 = 1;
                        }
                        if (reg.Value.Equals("C1117"))
                        {
                            resultado.C1117 = 1;
                        }
                        if (reg.Value.Equals("C1118"))
                        {
                            resultado.C1118 = 1;
                        }
                        if (reg.Value.Equals("C1119"))
                        {
                            resultado.C1119 = 1;
                        }
                        if (reg.Value.Equals("C1120"))
                        {
                            resultado.C1120 = 1;
                        }
                        if (reg.Value.Equals("C1121"))
                        {
                            resultado.C1121 = 1;
                        }
                        if (reg.Value.Equals("C1122"))
                        {
                            resultado.C1122 = 1;
                        }
                        if (reg.Value.Equals("C1123"))
                        {
                            resultado.C1123 = 1;
                        }
                    }
                }

                foreach (ListItem reg in chkGrupo3.Items)
                {
                    if (reg.Selected)
                    {
                        if (reg.Value.Equals("C1124"))
                        {
                            resultado.C1124 = 1;
                        }
                    }
                }

                foreach (ListItem reg in chkGrupo4.Items)
                {
                    if (reg.Selected)
                    {
                        if (reg.Value.Equals("C1125"))
                        {
                            resultado.C1125 = 1;
                        }
                        if (reg.Value.Equals("C1126"))
                        {
                            resultado.C1126 = 1;
                        }
                        if (reg.Value.Equals("C1127"))
                        {
                            resultado.C1127 = 1;
                        }
                    }
                }

                foreach (ListItem reg in chkGrupo5.Items)
                {
                    if (reg.Selected)
                    {
                        if (reg.Value.Equals("C1128"))
                        {
                            resultado.C1128 = 1;
                        }
                    }
                }

                foreach (ListItem reg in chkGrupo6.Items)
                {
                    if (reg.Selected)
                    {
                        if (reg.Value.Equals("C1129"))
                        {
                            resultado.C1129 = 1;
                        }
                        if (reg.Value.Equals("C1130"))
                        {
                            resultado.C1130 = 1;
                        }
                        if (reg.Value.Equals("C1131"))
                        {
                            resultado.C1131 = 1;
                        }
                        if (reg.Value.Equals("C1132"))
                        {
                            resultado.C1132 = 1;
                        }
                        if (reg.Value.Equals("C1133"))
                        {
                            resultado.C1133 = 1;
                        }
                        if (reg.Value.Equals("C1134"))
                        {
                            resultado.C1134 = 1;
                        }
                        if (reg.Value.Equals("C1135"))
                        {
                            resultado.C1135 = 1;
                        }
                        if (reg.Value.Equals("C1136"))
                        {
                            resultado.C1136 = 1;
                        }
                        if (reg.Value.Equals("C1137"))
                        {
                            resultado.C1137 = 1;
                        }
                        if (reg.Value.Equals("C1138"))
                        {
                            resultado.C1138 = 1;
                        }
                        if (reg.Value.Equals("C1139"))
                        {
                            resultado.C1139 = 1;
                        }
                        if (reg.Value.Equals("C1140"))
                        {
                            resultado.C1140 = 1;
                        }
                        if (reg.Value.Equals("C1141"))
                        {
                            resultado.C1141 = 1;
                        }
                    }
                }

                foreach (ListItem reg in chkGrupo7.Items)
                {
                    if (reg.Selected)
                    {
                        if (reg.Value.Equals("C1142"))
                        {
                            resultado.C1142 = 1;
                        }
                        if (reg.Value.Equals("C1143"))
                        {
                            resultado.C1143 = 1;
                        }
                        if (reg.Value.Equals("C1144"))
                        {
                            resultado.C1144 = 1;
                        }
                        if (reg.Value.Equals("C1145"))
                        {
                            resultado.C1145 = 1;
                        }
                    }
                }

                foreach (ListItem reg in chkGrupo8.Items)
                {
                    if (reg.Selected)
                    {
                        if (reg.Value.Equals("C1146"))
                        {
                            resultado.C1146 = 1;
                        }
                        if (reg.Value.Equals("C1147"))
                        {
                            resultado.C1147 = 1;
                        }
                    }
                }
                // NUEVOS DATOS DE AGREGACION
                resultado.TipoTramite = Label2.Text.ToString();
                resultado.NumeroOrden = textNumeroOrden.Text.Trim().ToUpper();
                resultado.FechaSolicitud = txFechaSol.Text.Trim().ToUpper();
                
                ///
                //if (ConAfectacion) { resultado.ConAfectacion = 1; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoContratante.SelectedValue.Equals("1"))
            {
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
            }
            else if (cboTipoContratante.SelectedValue.Equals("2"))
            {
                pnPrsMoral.Visible = true;
                pnPrsFisica.Visible = false;
            }
            else
            {
                pnPrsFisica.Visible = false;
                pnPrsMoral.Visible = false;
            }

            lbNombreAgente.Text = daNombreDeAgente(hf_IdPromotoria.Value, txIdAgente.Text);
        }

        protected void ActividadCPDES_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActividadCPDES.SelectedValue.Equals("Si"))
            {
                CPDS.Visible = true;
            }
            else if (ActividadCPDES.SelectedValue.Equals("No"))
            {
                CPDS.Visible = false;
            }
            else
            {
                CPDS.Visible = false;
            }
        }

        [System.Web.Services.WebMethod()]
        public static string TieneTramitesanteriores(string rfc)
        {
            string dato = "0";
            bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedente(rfc);
            if (resultado)
            {
                dato = "1";
            }
            return dato;
        }

        [System.Web.Services.WebMethod()]
        public static string daNombreDeAgente(string pIdPromotoria, string pClaveAgente)
        {
            string resultado = "NO EXISTE";
            if (!string.IsNullOrEmpty(pIdPromotoria) && !string.IsNullOrEmpty(pClaveAgente))
            {
                wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(pIdPromotoria));
                wfiplib.agentePromotoria agente = (new wfiplib.admAgentesPromotoria()).buscaAgenteEnPromotoria(promotoria.Clave, pClaveAgente);
                if (agente.clave > 0)
                    resultado = agente.descripcion;
            }
            return resultado;
        }

        [ScriptMethod(), WebMethod()]
        public static string[] regresaNombreRegion(string pIdPromotoria)
        {
            String[] array = new string[10];
            array[0] = string.Concat(pIdPromotoria, " Región");
            array[1] = string.Concat(pIdPromotoria, " Subdirección");
            array[2] = string.Concat(pIdPromotoria, " Gerente Comercial");
            array[3] = string.Concat(pIdPromotoria, " Ejecutivo Comercial");

            return array;
        }
    }
}