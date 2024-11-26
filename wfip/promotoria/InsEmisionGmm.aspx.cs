using DevExpress.XtraSpellChecker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections;
using System.Text.RegularExpressions;

using System.ComponentModel;
using System.Drawing;
using System.Text;

using RFC;
using ConvierteRomanosaLetras;
using wfiplib;
using DevExpress.Web;
using System.Configuration;

namespace wfip.promotoria
{
    public partial class InsEmisionGmm : System.Web.UI.Page
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

                // IDENTIFICAR PROMOTORIA - NUNCA CAMBIA APARTIR DE LA AUTENTIFICACION
                identificaPromotoria();
                cargarMonedas(ref cboMoneda);
                //cboMoneda.SelectedValue = "1";
                cargarNacionalidadesCombo_db(ref txNacionalidad);
                cargarNacionalidadesCombo_db(ref txTiNacionalidad);

                // Inicio de datos y validación de fechas 
                DateTime validateFechaSolicitud = DateTime.Today;

                dtFechaSolicitud.MaxDate = validateFechaSolicitud;
                dtFechaSolicitud.MinDate = validateFechaSolicitud.AddDays(-30);
                dtFechaSolicitud.UseMaskBehavior = true;
                dtFechaSolicitud.EditFormatString = GetFormatString("dd/MM/yyyy");
                dtFechaSolicitud.Date = DateTime.Today;

                dtFechaConstitucion.MaxDate = DateTime.Today;
                dtFechaConstitucion.UseMaskBehavior = true;
                dtFechaConstitucion.EditFormatString = GetFormatString("dd/MM/yyyy");

                dtFechaNacimiento.MaxDate = DateTime.Today;
                dtFechaNacimiento.UseMaskBehavior = true;
                dtFechaConstitucion.EditFormatString = GetFormatString("dd/MM/yyyy");

                dtFechaNacimientoTitular.MaxDate = DateTime.Today;
                dtFechaNacimientoTitular.UseMaskBehavior = true;
                dtFechaNacimientoTitular.EditFormatString = GetFormatString("dd/MM/yyyy");

                dtFechaConstitucion.Date = DateTime.Today;
                dtFechaNacimiento.Date = DateTime.Today;
                dtFechaNacimientoTitular.Date = DateTime.Today;


                if (Session["tramite"] != null)
                {
                    pintLimpiar();
                    //wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                    //if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionVida)
                    //{
                    //    wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.emisionVidaIndividual.ToString()];
                    //    PostBack(oEmisionVida, "11");

                    //}
                    //else if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionGMM)
                    //{
                    //    wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.emisionGmmIndividual.ToString()];
                    //    PostBack(oEmisionGmm, "12");
                    //}
                }
                else
                {
                    Session.Remove("tramite");
                }
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

        private void PostBack(EmisionVG oEmisionVidaGMM, string TipoTramite)
        {
            /***********************************************************************/
            /*****************   PRIMER BLOQUE - POLIZA / SEGURO   *****************/
            /***********************************************************************/

            cboMoneda.SelectedValue = oEmisionVidaGMM.IdMoneda.ToString();
            txtSumaAseguradaBasica.Text = oEmisionVidaGMM.SumaAsegurada.ToString();
            txtPrimaTotal.Text = oEmisionVidaGMM.SumaPolizas.ToString();
            TramiteTipPoliza.SelectedValue = TipoTramite;

            /***********************************************************************/
            /********************   INFORMACION DE LA POLIZA    ********************/
            /***********************************************************************/

            dtFechaSolicitud.Text = oEmisionVidaGMM.FechaSolicitud.ToString();
            txIdAgente.Text = oEmisionVidaGMM.AgenteClave.ToString();
            textNumeroOrden.Text = oEmisionVidaGMM.NumeroOrden.ToString();

            cboTipoContratante.SelectedValue = oEmisionVidaGMM.TipoPersona.ToString();
            TipoContratante();

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                txNombre.Text = oEmisionVidaGMM.Nombre.ToString();
                txApPat.Text = oEmisionVidaGMM.ApMaterno.ToString();
                txApMat.Text = oEmisionVidaGMM.ApPaterno.ToString();
                //txNacionalidad.SelectedIndex = Int32.Parse(oEmisionVidaGMM.Nacionalidad.ToString());
                txNacionalidad.SelectedItem.Text = oEmisionVidaGMM.Nacionalidad.ToString();
                dtFechaNacimiento.Text = oEmisionVidaGMM.FechaNacimiento.ToString();
                txSexo.SelectedValue = oEmisionVidaGMM.Sexo.ToString();
                txRfc.Text = oEmisionVidaGMM.RFC.ToString();
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                txNomMoral.Text = oEmisionVidaGMM.Nombre.ToString();
                dtFechaConstitucion.Text = oEmisionVidaGMM.FechaConst.ToString();
                txRfcMoral.Text = oEmisionVidaGMM.RFC.ToString();
                txTiNacionalidad.SelectedItem.Text = oEmisionVidaGMM.Nacionalidad.ToString();
            }

            if (oEmisionVidaGMM.TitularNombre.ToString() != "")
            {
                CheckBox1.Checked = true;
                CheckB1();
                txTiNombre.Text = oEmisionVidaGMM.TitularNombre.ToString();
                txTiApPat.Text = oEmisionVidaGMM.TitularApPat.ToString();
                txTiApMat.Text = oEmisionVidaGMM.TitularApMat.ToString();
                txTiNacionalidad.SelectedItem.Text = oEmisionVidaGMM.TitularNacionalidad.ToString();
                txtSexoM.SelectedValue = oEmisionVidaGMM.TitularSexo.ToString();
                dtFechaNacimientoTitular.Text = oEmisionVidaGMM.TitularFechaNacimiento.ToString();
            }
        }
        
        private void pintLimpiar()
        {
            if (Session["URL"] != null)
            {
                Limpiar.Visible = true;
            }
        }
        protected void BtnLimpiar(object sender, EventArgs e)
        {
            Session.Remove("tramite");
            string url = Session["URL"].ToString();
            Response.Redirect(url);
        }

        protected string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        private void showMessage(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.mensajesInformativos, typeof(string), "Alert", "alert('" + Mensaje + "');", true);
            // Response.Write("<script language=javascript>alert('" + Mensaje + "')</script>");
        }

        protected void antecedentesRFC(object sender, EventArgs e)
        {
            TextantecedentesRFC.Text = "";
            textRFCFisica.Text = "";

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                string RFC = txRfc.Text.Trim();
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    textRFCFisica.Text = "Ya existen trámites registrados para el RFC";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                string RFC = txRfcMoral.Text.Trim();
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    TextantecedentesRFC.Text = "Ya existen trámites registrados para el RFC";
                }
            }
        }

        private void identificaPromotoria()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria) //1 Promotoría
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    hf_IdPromotoria.Value = manejo_sesion.Credencial.IdPromotoria.ToString();
                    texClave.Text = ClavePromotoria(manejo_sesion.Credencial.IdPromotoria.ToString());
                }
            }
        }

        /// <summary>
        /// Carga todas las Monedas definida en DB
        /// </summary>
        private void cargarMonedas(ref DropDownList objDDL)
        {
            DataTable dtMoneda = (new wfiplib.admEmisionVG()).cargaMonedas();
            objDDL.DataSource = dtMoneda;
            objDDL.DataTextField = "Nombre";
            objDDL.DataValueField = "IdMoneda";
            objDDL.DataBind();
            objDDL.SelectedIndex = 1;
        }

        private String validaPais(string nombre)
        {
            String respuesta = (new wfiplib.admEmisionVG()).validaPais(nombre);
            //String respuesta = id.ToString();
            return respuesta;
        }

        protected void LisNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textNacionalidad.Text = "jajaja";
            textNacionalidad.Text = "";
            textNacionalidad.Text = validaPais(txNacionalidad.Text.Trim());
        }

        protected void LisTitNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textNacionalidad.Text = "jajaja";
            textTitularNacionalidad.Text = "";
            textTitularNacionalidad.Text = validaPais(txTiNacionalidad.Text.Trim());
        }

        private void cargarNacionalidadesCombo_db(ref ASPxComboBox objDDL)
        {
            DataTable dtPaises = (new wfiplib.admEmisionVG()).cargaPaises();

            objDDL.DataSource = dtPaises;
            objDDL.TextField = "Nombre";
            //ddl_time.DataTextField = "Nombre";
            objDDL.ValueField = "Id";
            objDDL.DataBind();
            objDDL.SelectedIndex = 137;
        }
        /// <summary>
        /// Carga todas la nacionalidades definida en DB
        /// </summary>
        /// 

        private void cargarNacionalidades_db(ref DropDownList objDDL)
        {
            DataTable dtPaises = (new wfiplib.admEmisionVG()).cargaPaises();
            objDDL.DataSource = dtPaises;
            objDDL.DataTextField = "Nombre";
            objDDL.DataValueField = "Id";
            objDDL.DataBind();
            objDDL.SelectedIndex = 137;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("esperaPromotoria.aspx");
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            Continuar();
        }

        protected void Continuar()
        {
            /*
            try
            {
                // DateTime fechaSolicitud ;
                // fechaSolicitud = CalHasta.Date;
                // showMessage(CalHasta.Date.ToString());
                // showMessage(CalHasta.Value.ToString());

                // ALAMCENA LOS DATOS APARTIR DE LA FUNCION recuperaCaptura


                wfiplib.EmisionVG oDatos = recuperaCaptura();
                //wfiplib.serviciosVida oDatos = recuperaCaptura();

                int idTramite = armaTramiteYGuardaEnMemoria(oDatos.DatosHtml);
                if (idTramite > 0)
                {
                    oDatos.IdTramite = idTramite;
                    if (TramiteTipPoliza.SelectedValue.Equals("11"))
                    {
                        Session[wfiplib.E_TipoTramite.emisionVidaIndividual.ToString()] = oDatos;
                    }
                    else if (TramiteTipPoliza.SelectedValue.Equals("12"))
                    {
                        Session[wfiplib.E_TipoTramite.emisionGmmIndividual.ToString()] = oDatos;
                    }
                    //Session[wfiplib.E_TipoTramite.serviciosVida.ToString()] = oDatos;
                    Response.Redirect("anexaArchivos.aspx");
                }
            }
            catch (Exception ex)
            {
                enviaMsgCliente(ex.Message);
            }
            */
        }

        private int armaTramiteYGuardaEnMemoria(string DatosHtml)
        {
            int Id = (new wfiplib.admTramite()).siguienteId();

            // wfiplib.credencial oCredencial123 = (wfiplib.credencial)Session["credencial"];
            wfiplib.tramiteP oTramite = new wfiplib.tramiteP();

            oTramite.Id = Id;
            if (TramiteTipPoliza.SelectedValue.Equals("11"))
            {
                // oTramite.IdTipoTramite = wfiplib.E_TipoTramite.emisionVidaIndividual;
                oTramite.IdTipoTramite = wfiplib.E_TipoTramite.indPriEmisionVida;
            }
            else if (TramiteTipPoliza.SelectedValue.Equals("12"))
            {
                // oTramite.IdTipoTramite = wfiplib.E_TipoTramite.emisionGmmIndividual;
                oTramite.IdTipoTramite = wfiplib.E_TipoTramite.indPriEmisionGMM;
            }

            oTramite.IdPromotoria = (new wfiplib.admCredencial()).daPromotoria(manejo_sesion.Credencial.Id);
            oTramite.IdUsuario = manejo_sesion.Credencial.Id;
            oTramite.DatosHtml = DatosHtml;
            oTramite.AgenteClave = txIdAgente.Text.Trim();
            oTramite.NumeroOrden = textNumeroOrden.Text.Trim();
            oTramite.FechaSolicitud = dtFechaSolicitud.Text;
            oTramite.TipoTramite = Label2.Text.ToString();

            Session["tramite"] = oTramite;

            string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] Separado = cadena.Split('/');
            string Final = Separado[Separado.Length - 1];
            // Session["URL"] = HttpContext.Current.Request.Url.PathAndQuery;
            Session["URL"] = Final;
            return Id;
        }
        private wfiplib.EmisionVG recuperaCaptura()
        {
            // SE CREO LA CLASE serviciosVidaP para actualizar los nuevos datos
            wfiplib.EmisionVG resultado = new wfiplib.EmisionVG();
            //wfiplib.serviciosVida resultado = new wfiplib.serviciosVida();
            try
            {
                if (cboTipoContratante.SelectedValue.Equals("Fisica"))
                {
                    resultado.TipoPersona = wfiplib.E_TipoPersona.Fisica;
                    resultado.Nombre = txNombre.Text.Trim();
                    resultado.ApPaterno = txApPat.Text.Trim();
                    resultado.ApMaterno = txApMat.Text.Trim();
                    resultado.RFC = txRfc.Text.Trim().ToUpper();
                    //resultado.Nacionalidad = txNacionalidad.SelectedIndex.ToString();
                    resultado.Nacionalidad = txNacionalidad.Text.Trim();
                    resultado.FechaNacimiento = dtFechaNacimiento.Text.Trim();
                    resultado.Sexo = txSexo.Text.Trim();
                    //resultado.CURP = txCurp.Text.Trim().ToUpper();
                }
                else if (cboTipoContratante.SelectedValue.Equals("Moral"))
                {
                    resultado.TipoPersona = wfiplib.E_TipoPersona.Moral;
                    resultado.Nombre = txNomMoral.Text.Trim();
                    resultado.FechaConst = dtFechaConstitucion.Text.Trim();
                    resultado.RFC = txRfcMoral.Text.Trim().ToUpper();
                    resultado.Nacionalidad = txTiNacionalidad.Text.Trim();
                }

                if (CheckBox1.Checked.Equals(true))
                {
                    resultado.TitularNombre = txTiNombre.Text.Trim();
                    resultado.TitularApPat = txTiApPat.Text.Trim();
                    resultado.TitularApMat = txTiApMat.Text.Trim();
                    resultado.TitularNacionalidad = txTiNacionalidad.Text.Trim();
                    resultado.TitularFechaNacimiento = dtFechaNacimientoTitular.Text.Trim();
                    resultado.TitularSexo = txtSexoM.Text.Trim();
                }
                
                resultado.Detalle = txDetalle.Text.Trim();
                // NUEVOS DATOS DE AGREGACION
                resultado.NumeroOrden = textNumeroOrden.Text.Trim().ToUpper();
                resultado.AgenteClave = txIdAgente.Text.Trim();
                resultado.FechaSolicitud = dtFechaSolicitud.Text;
                ///
                //if (ConAfectacion) { resultado.ConAfectacion = 1; }

                resultado.SumaAsegurada = txtSumaAseguradaBasica.Text.Trim();
                resultado.SumaPolizas = txtPrimaTotal.Text.ToString();
                resultado.IdMoneda = cboMoneda.Text.Trim();
                
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return resultado;
        }
        
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckB1();
        }
        protected void CheckB1()
        {
            if (CheckBox1.Checked.Equals(true))
            {
                CheckBox2.Checked = false;
                DiferenteContratante.Visible = true;
            }
            else if (CheckBox1.Checked.Equals(false))
            {
                DiferenteContratante.Visible = false;
            }
            else
            {
                DiferenteContratante.Visible = false;
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckB2();
        }

        protected void CheckB2()
        {
            if (CheckBox2.Checked.Equals(true))
            {
                CheckBox1.Checked = false;
                CheckB1();
            }
        }

        public static bool IsDate(string inputDate, ref string strResultado, ref DateTime FechaValue)
        {
            bool isDate = true;
            try
            {
                FechaValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy", null);

                if (FechaValue > DateTime.Today)
                {
                    strResultado = "La fecha no puede ser mayor al día de hoy.";
                    isDate = false;
                }
                else if (FechaValue < FechaValue.AddDays(-60))
                {
                    strResultado = "La fecha no puede ser menor a 60 días a partir del día de hoy.";
                    isDate = false;
                }
                else
                {
                    isDate = true;
                }

            }
            catch (Exception ex)
            {
                isDate = false;
                strResultado = ex.Message;
                Console.Write("Error al Validar la Fecha: " + ex.Message);
            }
            return isDate;
        }

        protected void dtFechaNacimiento_OnChanged(object sender, EventArgs e)
        {
            string strValMsj = "";
            DateTime dtValor = DateTime.Today;
            if (dtFechaNacimiento.Text.Length > 0)
            {
                if (IsDate(dtFechaNacimiento.Text.Trim(), ref strValMsj, ref dtValor))
                {
                    try
                    {
                        ObtieneRFC rfc = new ObtieneRFC();
                        txRfc.Text = rfc.RFC13Pocisiones(txApPat.Text.ToUpper().Trim(), txApMat.Text.ToUpper().Trim(), txNombre.Text.ToUpper().Trim(), dtValor.ToString("yy/MM/dd"));
                    }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                    catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                    {
                        txRfc.Text = "ERR000000AAA".ToUpper();
                    }
                }
                else
                {
                    dtFechaNacimiento.Text = "";
                    dtFechaNacimiento.Focus();
                    txRfc.Text = "";
                    showMessage(strValMsj);
                }
            }
        }

        protected void dtFechaConstitucion_OnChanged(object sender, EventArgs e)
        {
            string strValMsj = "";
            DateTime dtValor = DateTime.Today;
            if (dtFechaConstitucion.Text.Length > 0)
            {
                if (IsDate(dtFechaConstitucion.Text.Trim(), ref strValMsj, ref dtValor))
                {
                    try
                    {
                        PersonaMoral moral = new PersonaMoral();
                        txRfcMoral.Text = moral.RetornaLetrasFinalesRFC(txNomMoral.Text.ToUpper().Trim(), dtValor.ToString("yy/MM/dd"));
                    }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                    catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                    {
                        txRfcMoral.Text = "ERR000000AAA".ToUpper();
                    }
                }
                else
                {
                    dtFechaConstitucion.Text = "";
                    dtFechaConstitucion.Focus();
                    txRfcMoral.Text = "";
                    showMessage(strValMsj);
                }
            }
        }

        protected void dtFechaSolicitud_OnChanged(object sender, EventArgs e)
        {
            string strValMsj = "";
            DateTime dtValor = DateTime.Today;
            if (dtFechaSolicitud.Text.Length > 0)
            {
                if (!IsDate(dtFechaSolicitud.Text.Trim(), ref strValMsj, ref dtValor))
                {
                    dtFechaSolicitud.Text = "";
                    dtFechaSolicitud.Focus();
                    showMessage(strValMsj);
                }
            }
        }
        
        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoContratante();
        }

        protected void TipoContratante()
        {
            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
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

        public static string ClavePromotoria(string pIdPromotoria)
        {
            string resultado = "NO ENCONTRADO";

            if (!string.IsNullOrEmpty(pIdPromotoria))
            {
                //resultado = pIdPromotoria;

                //wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria()).BuscaPromotoria(Convert.ToInt32(pIdPromotoria));
                wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(pIdPromotoria));
                //wfiplib.agentePromotoria agente = (new wfiplib.admAgentesPromotoria()).buscaAgenteEnPromotoria(promotoria.Clave, pClaveAgente);
                if (promotoria.Clave != "") resultado = promotoria.Clave;

            }

            return resultado;
        }

        [System.Web.Services.WebMethod()]
        public static string TieneTramitesanteriores(string rfc)
        {
            string dato = "0";
            bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedente(rfc);
            if (resultado) { dato = "1"; }
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
                if (agente.clave > 0) resultado = agente.descripcion;
            }
            return resultado;
        }

        [ScriptMethod(), WebMethod()]
        public static string[] regresaNombreRegion(string pIdPromotoria)
        {
            // Obtenemos la información Gerencial de la promotoría.
            wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(pIdPromotoria));
            wfiplib.comercialPromotoria comercial = (new wfiplib.admAgentesPromotoria()).getComercialInformation(promotoria.Clave);

            String[] array = new string[10];
            array[0] = string.Concat(comercial.ClaveRegion, " - " + comercial.Region);
            array[1] = string.Concat("", "");
            array[2] = string.Concat(comercial.ClaveGerente, " - " + comercial.Gerente);
            array[3] = string.Concat(comercial.ClaveEjecutivo, " - " + comercial.Ejecutivo);

            return array;
        }
    }
}