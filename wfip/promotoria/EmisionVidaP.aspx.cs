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
#pragma warning disable CS0105 // La directiva using para 'System.Text.RegularExpressions' aparece previamente en este espacio de nombres
using System.Text.RegularExpressions;
#pragma warning restore CS0105 // La directiva using para 'System.Text.RegularExpressions' aparece previamente en este espacio de nombres
using System.Configuration;

namespace wfip.promotoria
{
    public partial class EmisionVidaP : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        admEntidadFederativa edos = new admEntidadFederativa();


        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                // IDENTIFICAR PROMOTORIA - NUNCA CAMBIA APARTIR DE LA AUTENTIFICACION
                identificaPromotoria();
                cargarRamos(ref TramiteTipPoliza);
                cargarMonedas(ref cboMoneda);
                //cboMoneda.SelectedValue = "1";
                cargarNacionalidadesCombo_db(ref txNacionalidad);
                cargarNacionalidadesCombo_db(ref txTiNacionalidad);

                edos.SeleccionarDependencias_DropDrownList(ref cboEstado);
                edos.SeleccionarDependencias_DropDrownList(ref cboEstado2);

                // Inicio de datos y validación de fechas 
                DateTime validateFechaSolicitud = DateTime.Today;

                dtFechaSolicitud.MaxDate = validateFechaSolicitud;
                dtFechaSolicitud.MinDate = validateFechaSolicitud.AddDays(-60);
                dtFechaSolicitud.UseMaskBehavior = true;
                dtFechaSolicitud.EditFormatString = GetFormatString("dd/MM/yyyy");
                dtFechaSolicitud.Date = DateTime.Today;

                dtFechaConstitucion.MaxDate = DateTime.Today;
                dtFechaConstitucion.UseMaskBehavior = true;
                dtFechaConstitucion.EditFormatString = GetFormatString("dd/MM/yyyy");

                //dtFechaNacimiento.MaxDate = DateTime.Today;
                dtFechaNacimiento.MaxDate = validateFechaSolicitud.AddYears(-18);
                dtFechaNacimiento.UseMaskBehavior = true;
                dtFechaNacimiento.EditFormatString = GetFormatString("dd/MM/yyyy");
                //
                //dtFechaNacimientoTitular.MaxDate = DateTime.Today;
                //dtFechaNacimientoTitular.MaxDate = validateFechaSolicitud.AddYears(-18);
                dtFechaNacimientoTitular.UseMaskBehavior = true;
                dtFechaNacimientoTitular.EditFormatString = GetFormatString("dd/MM/yyyy");

                dtFechaConstitucion.Date = DateTime.Today;
                dtFechaNacimiento.Date = DateTime.Today.AddYears(-18);
                dtFechaNacimientoTitular.Date = DateTime.Today;


                if (Session["tramite"] != null)
                {
                    /*****************************************************************/
                    /************ NUEVO TRÁMITE VIDA CON CITAS MÉDICAS ***************/
                    /************** RECONOCE APARTIR DE E_TIPOTRAMITE ****************/

                    pintLimpiar();
                    wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                    switch ((wfiplib.E_TipoTramite)Convert.ToInt32((oTramite.IdTipoTramite)))
                    {
                        // VIDA
                        case wfiplib.E_TipoTramite.indPriEmisionVida:
                        // VIDA CITA MEDICA
                        case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                            wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                            PostBack(oEmisionVida, wfiplib.E_TipoTramite.indPriEmisionVida.ToString("d"));
                            break;
                        case wfiplib.E_TipoTramite.indPriEmisionGMM:
                            wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()];
                            PostBack(oEmisionGmm, wfiplib.E_TipoTramite.indPriEmisionGMM.ToString("d"));
                            break;
                    }
                    /*pintLimpiar();
                    wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                    
                    
                    if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionVida)
                    {
                        wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                        
                        PostBack(oEmisionVida, wfiplib.E_TipoTramite.indPriEmisionVida.ToString("d"));
                        
                    }
                    else if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionGMM)
                    {
                        wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()];
                        PostBack(oEmisionGmm, wfiplib.E_TipoTramite.indPriEmisionGMM.ToString("d"));
                    }*/
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
                if (TipoTramite == "privado")
                {
                    Label2.Text = TipoTramite.ToUpper();
                }
                else if (TipoTramite == "publico")
                {
                    //Label2.Text = TipoTramite.ToUpper();
                    Label2.Text = "PÚBLICO";
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
            //txtPrimaTotal.Text = oEmisionVidaGMM.SumaPolizas.ToString();
            if (GrandesSumas())
            {
                PrimaTotalGrandeSumas.Text = "";
                GrandeSumas.Text = "Grandes sumas";
            }
            TramiteTipPoliza.SelectedValue = TipoTramite;
            TramiteTipoPoliza();

            switch ((wfiplib.E_TipoTramite)Convert.ToInt32(TramiteTipPoliza.SelectedValue))
            {
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    txtSumaAseguradaPolizasVigentes.Text = oEmisionVidaGMM.SumaPolizas.ToString();
                    txtPrimaTotal.Text = oEmisionVidaGMM.PrimaTotal.ToString();
                    //resultado.SumaPolizas = txtSumaAseguradaPolizasVigentes.Text.ToString();
                    //resultado.PrimaTotal = txtPrimaTotal.Text.ToString();
                    if (GrandesSumasPrimaTotal())
                    {
                        GrandeSumas.Text = "";
                        PrimaTotalGrandeSumas.Text = "Grandes sumas";
                    }
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    txtPrimaTotalGMM.Text = oEmisionVidaGMM.PrimaTotal.ToString();
                    //resultado.PrimaTotal = txtPrimaTotalGMM.Text.ToString();
                    break;

                default:
                    break;
            }



            LisProducto1.SelectedValue = oEmisionVidaGMM.Producto1.ToString();
            LisSbproductos();
            LisSubproducto1.SelectedValue = oEmisionVidaGMM.Plan1.ToString();
            ActividadCPDES.SelectedValue = oEmisionVidaGMM.CPDES.ToString();
            CPDES();
            textFolioCPDES.Text = oEmisionVidaGMM.FolioCPDES.ToString();
            EstatusCPDES.SelectedValue = oEmisionVidaGMM.EstatusCPDES.ToString();

            HombresClave.Checked = oEmisionVidaGMM.HombreClave;

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
                txApPat.Text = oEmisionVidaGMM.ApPaterno.ToString();
                txApMat.Text = oEmisionVidaGMM.ApMaterno.ToString();
                //txNacionalidad.SelectedIndex = Int32.Parse(oEmisionVidaGMM.Nacionalidad.ToString());
                // txNacionalidad.SelectedItem.Text = oEmisionVidaGMM.Nacionalidad.ToString();
                txNacionalidad.Value = oEmisionVidaGMM.Nacionalidad.ToString();
                dtFechaNacimiento.Text = oEmisionVidaGMM.FechaNacimiento.ToString();
                txSexo.SelectedValue = oEmisionVidaGMM.Sexo.ToString();
                txRfc.Text = oEmisionVidaGMM.RFC.ToString();
                cboEstado.SelectedValue = oEmisionVidaGMM.EntidadFederativa.ToString();
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                txNomMoral.Text = oEmisionVidaGMM.Nombre.ToString();
                dtFechaConstitucion.Text = oEmisionVidaGMM.FechaConst.ToString();
                txRfcMoral.Text = oEmisionVidaGMM.RFC.ToString();
                //txTiNacionalidad.SelectedItem.Text = oEmisionVidaGMM.Nacionalidad.ToString();
                txTiNacionalidad.Value = oEmisionVidaGMM.Nacionalidad.ToString();
            }

            if (oEmisionVidaGMM.TitularNombre.ToString() != "")
            {
                CheckBox1.Checked = true;
                CheckB1();
                txTiNombre.Text = oEmisionVidaGMM.TitularNombre.ToString();
                txTiApPat.Text = oEmisionVidaGMM.TitularApPat.ToString();
                txTiApMat.Text = oEmisionVidaGMM.TitularApMat.ToString();
                //txTiNacionalidad.SelectedItem.Text = oEmisionVidaGMM.TitularNacionalidad.ToString();
                txTiNacionalidad.Value = oEmisionVidaGMM.TitularNacionalidad.ToString();
                txtSexoM.SelectedValue = oEmisionVidaGMM.TitularSexo.ToString();
                dtFechaNacimientoTitular.Text = oEmisionVidaGMM.TitularFechaNacimiento.ToString();
                cboEstado2.SelectedValue = oEmisionVidaGMM.TitularEntidad.ToString();
            }
            txDetalle.Text = oEmisionVidaGMM.Detalle.ToString();
            antecedentesRFC();

            textNacionalidad.Text = "";
            textNacionalidad.Text = validaPais(txNacionalidad.Text.Trim());
            textTitularNacionalidad.Text = "";
            textTitularNacionalidad.Text = validaPais(txTiNacionalidad.Text.Trim());

            //CheckBox3.Checked = oEmisionVidaGMM.TempoLife;
            CitasMedicasEvalucacion();
            citamedica.Visible = false;

            MSresultado2.Text = "";
            if (TipoTramite == wfiplib.E_TipoTramite.indPriEmisionVida.ToString("d"))
            {
                if (oEmisionVidaGMM.Combo.ToString() != "" && oEmisionVidaGMM.Cel.ToString() != "")
                {
                    citamedica.Visible = true;
                    
                    //TextCombo.Text = oEmisionVidaGMM.Combo.ToString();
                    TextCel.Text = oEmisionVidaGMM.Cel.ToString();
                    TextCelAgentePromotor.Text = oEmisionVidaGMM.CelAgentePromotor.ToString();
                    TextCorreo.Text = oEmisionVidaGMM.Correo.ToString();
                    listEstados();
                    LisEstado.SelectedValue = oEmisionVidaGMM.Estado.ToString();
                    listCiudad();
                    LisCiudad.SelectedValue = oEmisionVidaGMM.Ciudad.ToString();
                    lisLabHospital();
                    LisLabHospital.SelectedValue = oEmisionVidaGMM.LaboratorioHospital.ToString();
                    cargaDireccion();

                    TextFecha1.Text = oEmisionVidaGMM.Fecha1.ToString();
                    TextFecha2.Text = oEmisionVidaGMM.Fecha2.ToString();
                    TextFecha3.Text = oEmisionVidaGMM.Fecha3.ToString();
                    notas.Text = oEmisionVidaGMM.Notas.ToString();
                }
                else
                {
                    citamedica.Visible = false;
                    TextCombo.Text = "";
                }
            }
            else
            {
                citamedica.Visible = false;
            }
        }
        
        protected bool ContinuarFechas()
        {
            bool resultado = true;
            texFecha1.Text = "";
            texFecha2.Text = "";
            texFecha3.Text = "";
            DateTime Fecha1 = Convert.ToDateTime(TextFecha1.Text.Trim());
            DateTime Fecha2 = Convert.ToDateTime(TextFecha2.Text.Trim());

            if (String.IsNullOrEmpty(TextFecha3.Text.Trim()))
#pragma warning disable CS0168 // La variable 'Fecha3' se ha declarado pero nunca se usa
            { DateTime Fecha3; }
#pragma warning restore CS0168 // La variable 'Fecha3' se ha declarado pero nunca se usa
            else
            {
                DateTime Fecha3 = Convert.ToDateTime(TextFecha3.Text.Trim());
                if (!ValdaFecha3(Fecha1, Fecha2, Fecha3))
                {
                    texFecha3.Text = "Fecha no valida";
                    resultado = false;
                }
            }
            if (!ValdaComparaFechas(Fecha1, Fecha2))
            {
                texFecha1.Text = "Fecha no valida";
                resultado = false;
            }
            if (!ValdaComparaFechas(Fecha2, Fecha1))
            {
                texFecha2.Text = "Fecha no valida";
                resultado = false;
            }
            return resultado;
        }

        protected void fechas_Changed(object sender, EventArgs e)
        {
            DateTime Fecha1 = Convert.ToDateTime(TextFecha1.Text.Trim());
            DateTime Fecha2 = Convert.ToDateTime(TextFecha2.Text.Trim());
            texFecha1.Text = "";
            texFecha2.Text = "";
            texFecha3.Text = "";

            if (String.IsNullOrEmpty(TextFecha3.Text.Trim()))
#pragma warning disable CS0168 // La variable 'Fecha3' se ha declarado pero nunca se usa
            { DateTime Fecha3; }
#pragma warning restore CS0168 // La variable 'Fecha3' se ha declarado pero nunca se usa
            else {
                DateTime Fecha3 = Convert.ToDateTime(TextFecha3.Text.Trim());
                if (!ValdaFecha3(Fecha1, Fecha2, Fecha3))
                {
                    texFecha3.Text = "Fecha no valida";
                }
            }
            if(!ValdaComparaFechas(Fecha1, Fecha2))
            {
                texFecha1.Text = "Fecha no valida";
            }
            if(!ValdaComparaFechas(Fecha2, Fecha1))
            {
                texFecha2.Text = "Fecha no valida";
            }
        }

        public bool ValdaFecha3(DateTime Fecha, DateTime Fecha2, DateTime Fecha3)
        {
            bool resultado = true;
            DateTime FechaHora = Convert.ToDateTime(Fecha3.ToShortTimeString());
            //DateTime FechaHora = Convert.ToDateTime("00:00:00");
            DateTime hora1 = Convert.ToDateTime("08:00:00");
            DateTime hora2 = Convert.ToDateTime("23:59:59");
            if (FechaHora >= hora1 && FechaHora <= hora2)
            {
                if (Fecha3.Equals(Fecha))
                {
                    resultado = false;
                }else if(Fecha3.Equals(Fecha2))
                {
                    resultado = false;
                }
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        public bool ValdaComparaFechas(DateTime Fecha, DateTime Fecha2)
        {
            bool resultado = true;
            DateTime FechaHora = Convert.ToDateTime(Fecha.ToShortTimeString());
            //DateTime FechaHora = Convert.ToDateTime("00:00:00");
            DateTime hora1 = Convert.ToDateTime("08:00:00");
            DateTime hora2 = Convert.ToDateTime("23:59:59");

            if(FechaHora >= hora1 && FechaHora <= hora2)
            {
                if (Fecha.Equals(Fecha2))
                {
                    resultado = false;
                }
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        public void ValidaFecha(DateTime FechaSolicitud, String combo)
        {
            DateTime Fecha = Convert.ToDateTime(FechaSolicitud.ToShortDateString());

            DateTime hora1 = Convert.ToDateTime("00:00:00");
            DateTime hora2 = Convert.ToDateTime("16:00:59");
            DateTime hora3 = Convert.ToDateTime("16:01:00");
            DateTime hora4 = Convert.ToDateTime("23:59:59");

            DateTime HC1 = Fecha.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);
            DateTime HC2 = Fecha.AddHours(hora2.Hour).AddMinutes(hora2.Minute).AddSeconds(hora2.Second);
            DateTime HC3 = Fecha.AddHours(hora3.Hour).AddMinutes(hora3.Minute).AddSeconds(hora3.Second);
            DateTime HC4 = Fecha.AddHours(hora4.Hour).AddMinutes(hora4.Minute).AddSeconds(hora4.Second);
            
            if (HC1 <= FechaSolicitud && FechaSolicitud <= HC2)
            {
                int dias = AumentaDias(combo, 1);
                FechasCombo(FechaSolicitud,dias);
            }
            else if(HC3 <= FechaSolicitud && FechaSolicitud <= HC4)
            {
                int dias = AumentaDias(combo, 2);
                FechasCombo(FechaSolicitud,dias);
            }
        }

        protected void ASPxCalendar_DayCellPrepared(object sender, DevExpress.Web.CalendarDayCellPreparedEventArgs e)
        {
            if (e.Date.DayOfWeek == DayOfWeek.Sunday || e.Date.DayOfWeek == DayOfWeek.Saturday)
            {
                e.Cell.Attributes["disabled"] = "disabled";
                e.Cell.Attributes["style"] = "pointer-events:none";
            }
        }
        protected void ASPxDateEdit1_CalendarDayCellPrepared(object sender, DevExpress.Web.CalendarDayCellPreparedEventArgs e)
        {
            if (e.Date.DayOfWeek != DayOfWeek.Tuesday && e.Date.DayOfWeek != DayOfWeek.Thursday)
            {
                e.TextControl.Visible = false;
                e.Cell.Attributes["disabled"] = "disable";
                e.Cell.Attributes["style"] = "pointer-events:none";
            }
        }

        /*
        protected void ASPxCalendar_DayCellPrepared(object sender, DevExpress.Web.CalendarDayCellPreparedEventArgs e)
        {

            if (e.Date.DayOfWeek == DayOfWeek.Sunday || e.Date.DayOfWeek == DayOfWeek.Saturday)
            {
                e.Cell.Attributes["disabled"] = "disabled";
                e.Cell.Attributes["style"] = "pointer-events:none";
            }
            /*
            e.TextControl.ForeColor = Color.Black;
                e.TextControl.Font.Bold = true;
            *
        }
        */

        private void FechasCombo(DateTime FechaSolicitud, int dias)
        {
            DateTime Fecha = Convert.ToDateTime(FechaSolicitud.AddDays(+dias).ToShortDateString());
            DateTime hora1 = Convert.ToDateTime("08:00:00");
            DateTime hora2 = Convert.ToDateTime("23:59:59");
            DateTime HC1 = Fecha.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);
            DateTime HC2 = Fecha.AddHours(hora2.Hour).AddMinutes(hora2.Minute).AddSeconds(hora2.Second);

            TextFecha1.MinDate = HC1;
            //TextFecha1.MaxDate = HC2.AddDays(+2);
            TextFecha1.MaxDate = HC2.AddYears(+1);
            TextFecha1.Date = FechaSolicitud.AddDays(+dias);
            TextFecha1.TimeSectionProperties.Visible = true;
            TextFecha1.UseMaskBehavior = true;
            TextFecha1.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha1.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

            TextFecha2.MinDate = HC1;
            //TextFecha2.MaxDate = HC2.AddDays(+2);
            TextFecha2.MaxDate = HC2.AddYears(+1);
            TextFecha2.Date = FechaSolicitud.AddDays(+dias);
            TextFecha2.TimeSectionProperties.Visible = true;
            TextFecha2.UseMaskBehavior = true;
            TextFecha2.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha2.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

            TextFecha3.MinDate = HC1;
            //TextFecha3.MaxDate = HC2.AddDays(+2);
            TextFecha3.MaxDate = HC2.AddYears(+1);
            TextFecha3.TimeSectionProperties.Visible = true;
            TextFecha3.UseMaskBehavior = true;
            TextFecha3.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha3.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
        }

        // ** MAL PROGRAMADO **/
        // NO ES PARAMETRIZABLE Y LA VALIDACIÓN ES ESTÁTICA ** RECTIFICAR PARÁMETROS DE VALIDACIÓN 
        // SOLO RETORNO EL VALOR DEL NUERO DE DÍAS A PARTIR DEL MODELO DE NEGOCIO DEL ANEXO 7
        private int AumentaDias(String combo, int horario)
        {
            int dias = 0;
            if (horario == 1)
            {
                if (combo == "1")
                {
                    dias = 1;
                }
                else if(combo == "2")
                {
                    dias = 2;
                }
                else if(combo == "3")
                {
                    dias = 2;
                }
            }
            else if(horario == 2)
            {
                if (combo == "1")
                {
                    dias = 2;
                }
                else if (combo == "2")
                {
                    dias = 3;
                }
                else if (combo == "3")
                {
                    dias = 3;
                }
            }
            return dias;
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
        /// Carga los Ramos Disponibles para la captura de tramites (Tipos de Tramites...)
        /// </summary>
        /// <param name="objDDL"></param>
        private void cargarRamos(ref DropDownList objDDL)
        {
            DataTable dtRamos = (new wfiplib.admEmisionVG()).cargaRamos("INDIVIDUAL", "PRIVADO");
            objDDL.DataSource = dtRamos;
            objDDL.DataTextField = "Nombre";
            objDDL.DataValueField = "IdRamo";
            objDDL.DataBind();
            objDDL.SelectedIndex = 0;
            objDDL.Focus();
            
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
            //objDDL.SelectedIndex = 137;
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
            //objDDL.TextField = "Nombre";
            //ddl_time.DataTextField = "Nombre";
            objDDL.ValueField = "Id";
            objDDL.DataBind();
            objDDL.Value= "MÉXICO";
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
            objDDL.SelectedIndex = 136;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("esperaPromotoria.aspx");
        }
        
        /// <summary>
        /// /////////////////////////////////////////////////
        /// /////////////////// CITAS MEDICAS ///////////////
        /// /////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void CitasMedicas(object sender, EventArgs e)
        {
            CitasMedicasEvalucacion();
        }

        protected void CitasMedicasEvalucacion()
        {
            MSresultado2.Text = "";
            TextCombo.Text = "";
            string DescripCombo = "";
            citamedica.Visible = false;
            Double MontoTotal = Total(txtSumaAseguradaBasica.Text.ToString(), txtSumaAseguradaPolizasVigentes.Text.ToString(), cboMoneda.Text.ToString());

            if (LisSubproducto1.SelectedItem.Text == "RIESGO PREFERENTE")
            {
                // SE COMENTA LINEA YA NO APLICA VALIDACION POR TEMPO LIFE RIESGOS PREFERENTES
                //DescripCombo = "Tempo life riesgos preferentes";
            }
            
            if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                if (CheckBox1.Checked.Equals(true))
                {
                    int Edad = CalcularEdad(dtFechaNacimientoTitular.Text.Trim());
                    Evaluacion(MontoTotal, Edad, DescripCombo);
                }
                else
                {
                    MSresultado2.Text = "No se pude elaborar una cita médica";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                if (CheckBox1.Checked.Equals(true))
                {
                    int Edad = CalcularEdad(dtFechaNacimientoTitular.Text.Trim());
                    Evaluacion(MontoTotal, Edad, DescripCombo);
                    //MSresultado2.Text = "se reariliza con solicitante";
                }
                else
                {
                    int Edad = CalcularEdad(dtFechaNacimiento.Text.Trim());
                    Evaluacion(MontoTotal, Edad, DescripCombo);
                    //MSresultado2.Text = "se realiza con persona fisica";
                }
            }
        }

        protected void Evaluacion(double Total, int Edad, string DescripCombo)
        {
            DataTable combo = (new wfiplib.admEmisionVG()).validaCombo(Total, Edad, DescripCombo);
            if (combo.Rows.Count > 0)
            {
                DataRow row = combo.Rows[0];
                if (row["combo"].ToString() == null || row["combo"].ToString() == "")
                {
                    MSresultado2.Text = (new wfiplib.admCatMensajes().getMensaje(1)) + " - " + Total + "-" + Edad + " -" + DescripCombo;
                    //MSresultado2.Text = "La solicitud no necesita cita médica";
                    //FormConfirmacion.Visible = false;
                }
                else
                {
                    TextCombo.Text = row["combo"].ToString();

                    DateTime validateFechaSolicitud = DateTime.Now;
                    
                    //DateTime hora1 = Convert.ToDateTime("16:02:00");
                    //DateTime fechaConvertida = validateFechaSolicitud.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);
                    //DateTime HC1 = validateFechaSolicitud.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);


                    ValidaFecha(validateFechaSolicitud, row["combo"].ToString());
                    //MSresultado.Text = "La solicitud necesita cita médica ";
                    citamedica.Visible = true;
                    TextCombo.Enabled = false;
                    TextDireccion.Enabled = false;
                    listEstados();
                    listCiudad();
                    lisLabHospital();
                }
            }
            else
            {
                MSresultado2.Text = (new wfiplib.admCatMensajes().getMensaje(1)) +" - "+ Total+ "-" + Edad +" -"+ DescripCombo;
                //MSresultado2.Text = "La solicitud no necesita cita médica";
            }
        }

        protected int CalcularEdad(String Fecha)
        {
            int edad = 0;
            string fecha = Fecha;
            DateTime nacimiento = DateTime.Parse(fecha);
            edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            return edad;
        }

        protected double Total(String SumaAsegurda, String TotalCotiacion, String Moneda)
        {
            if (TotalCotiacion.Length == 0)
                TotalCotiacion = "0";

            double Total = 0;
            double SumaAsegurada = double.Parse(SumaAsegurda);
            double TotalCotizacion = double.Parse(TotalCotiacion);
            int IdMoneda = int.Parse(Moneda);
            SumaAsegurada = convertir(SumaAsegurada, IdMoneda);
            TotalCotizacion = convertir(TotalCotizacion, IdMoneda);

            Total = SumaAsegurada + TotalCotizacion;

            return Total;
        }

        private double convertir(double numero, int IdMoneda)
        {
            double total = 0;
            DataTable ValorMoneda = (new wfiplib.admEmisionVG()).ValorMoneda(IdMoneda);
            DataRow row = ValorMoneda.Rows[0];
            String valor  = row["valor"].ToString();
            Double Moneda = Convert.ToDouble(valor);

            total = numero * Moneda;
            return total;
        }

        protected void BtnCancelarCita(object sender, EventArgs e)
        {
            TextCombo.Text = "";
            MSresultado.Text = "";
            MSresultado2.Text = "";
            citamedica.Visible = false;
        }

        protected void LisEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            listCiudad();
            lisLabHospital();
        }
        
        protected void PrimaTotalGrandesSumas(object sender, EventArgs e)
        {
            if (GrandesSumasPrimaTotal())
            {
                PrimaTotalGrandeSumas.Text = "Grandes sumas";
                GrandeSumas.Text = "";
            }
                
        }

        protected bool GrandesSumasPrimaTotal()
        {
            bool resultado = false;
            PrimaTotalGrandeSumas.Text = "";
            if (cboMoneda.SelectedValue != "-1")
            {
                int IdMoneda = int.Parse(cboMoneda.Text.ToString());
                string Primatotal = txtPrimaTotal.Text.ToString();
                double Monto;
                if (Primatotal != null && Primatotal.Length == 0)
                {
                    Monto = 0.00;
                }
                else
                {
                    Monto = double.Parse(Primatotal);
                }
                
                Monto = convertir(Monto, IdMoneda);
                
                double ValidacionMonto = double.Parse("200000.00");
                if (Monto >= ValidacionMonto)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        protected void CalculartSumaAsegurada(object sender, EventArgs e)
        {
            if (GrandesSumas())
            {
                GrandeSumas.Text = "Grandes sumas";
                PrimaTotalGrandeSumas.Text = "";
            }
            else if (GrandesSumasPrimaTotal())
            {
                GrandeSumas.Text = "Grandes sumas";
                PrimaTotalGrandeSumas.Text = "";
            }
            else
            {
                GrandeSumas.Text = "";
                PrimaTotalGrandeSumas.Text = "";
            }
        }

        protected bool GrandesSumas()
        {
            bool resultado = false;
            // Limpia los mensajes 
            GrandeSumas.Text = "";
            // Compara el valor de la seleccion
            if (cboMoneda.SelectedValue != "-1" && txtSumaAseguradaBasica.Text.ToString() != "0.00")
            {
                // Monto total de la suma aseguada y prima total, apartir del tipo de moneda seleccionada
                Double MontoTotal = Total(txtSumaAseguradaBasica.Text.ToString(), txtSumaAseguradaPolizasVigentes.Text.ToString(), cboMoneda.Text.ToString());
                // Validacion del monto "MAL PROGRAMADO" el id del dolar es = 2, el regitro esta en SQL cat_moneda, 1500000 dolares
                // double ValidacionMonto = double.Parse("1500000");
                double ValidacionMonto = double.Parse("6000000.00");
                ValidacionMonto = convertir(ValidacionMonto, 1);
                if (MontoTotal >= ValidacionMonto)
                {
                    resultado = true;
                }

            }
            return resultado;
        }

        protected void LisCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            lisLabHospital();
        }
        protected void LisLabHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaDireccion();
        }
        private void listEstados()
        {
            DataTable listEstados = (new wfiplib.admEmisionVG()).cargaCatEstados();
            
            LisEstado.DataSource = listEstados;
            LisEstado.Items.Add("Seleccione");
            LisEstado.DataBind();
            LisEstado.DataTextField = "estado";
            LisEstado.DataValueField = "Id_estado";
            LisEstado.DataBind();
        }

        private void listCiudad()
        {
            DataTable listCiudad = (new wfiplib.admEmisionVG()).cargaCatCiudad(LisEstado.SelectedValue.ToString());

            LisCiudad.DataSource = listCiudad;
            LisCiudad.Items.Add("Seleccione");
            LisCiudad.DataBind();
            LisCiudad.DataTextField = "ciudad";
            LisCiudad.DataValueField = "Id_ciudad";
            LisCiudad.DataBind();
        }

        private void lisLabHospital()
        {
           
            DataTable listLabHospital = (new wfiplib.admEmisionVG()).cargaCatProveedor(LisCiudad.SelectedValue.ToString());

            LisLabHospital.DataSource = listLabHospital;
            LisLabHospital.Items.Add("Seleccione");
            LisLabHospital.DataBind();
            LisLabHospital.DataTextField = "proveedor";
            LisLabHospital.DataValueField = "Id_proveedor";
            LisLabHospital.DataBind();
            cargaDireccion();
            
        }

        private void cargaDireccion()
        {
            TextDireccion.Text = "";
            DataTable listLabHospital = (new wfiplib.admEmisionVG()).cargaDireccionProveedor(LisLabHospital.SelectedValue.ToString());
            if (listLabHospital.Rows.Count > 0)
            {
                DataRow row = listLabHospital.Rows[0];
                TextDireccion.Text = row["direccion"].ToString().ToUpper();
            }
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            double SumaAseguradaBasica = double.Parse(txtSumaAseguradaBasica.Text.ToString());
            int IdMoneda = int.Parse(cboMoneda.Text.ToString());
            Double SumaAseguradaBasicaConvertida = convertir(SumaAseguradaBasica, IdMoneda);

            SumaBasica.Text = "";
            if (SumaAseguradaBasicaConvertida<10000.00)
            {
                SumaBasica.Text = "la suma asegurada no es mayor a 10,000.00 Pesos";
            }
            else
            {
                if (ValidantinuidadRFC())
                {
                    String combo = TextCombo.Text.Trim();
                    if (combo != "" && combo != null)
                    {
                        if (ContinuarFechas())
                        {
                            Continuar();
                        }
                        else
                        {
                            // MSresultado2.Text = combo;
                        }
                    }
                    else
                    {
                        Continuar();
                    }
                }
            }
        }

        protected bool ValidantinuidadRFC()
        {
            bool resultado = false;
            Mensajes.Text = "";
            
            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                string FiscoRFC = txRfc.Text.ToString().Trim();
                if (FiscoRFC != "" && FiscoRFC != null)
                {
                    if (FiscoRFC.Length == 13)
                    {
                        Regex Val = new Regex(@"[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?");
                        if (Val.IsMatch(FiscoRFC))
                        {
                            resultado = true;
                        }
                        else
                        {
                            Mensajes.Text = "RFC Persona Física Inválido ";
                        }
                    }
                    else
                    {
                        Mensajes.Text = "El RFC No Contiene 13 Caracteres ";
                    }
                }
                else
                {
                    Mensajes.Text = "Coloca el RFC de la Persona Física";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                string MoralRFC = txRfcMoral.Text.ToString().Trim();
                if (MoralRFC != "" && MoralRFC != null)
                {
                    if (MoralRFC.Length == 12 )
                    {
                        Regex Val = new Regex(@"^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$");
                        if (Val.IsMatch(MoralRFC))
                        {
                            resultado = true;
                        }
                        else
                        {
                            Mensajes.Text = "RFC Persona Moral Inválido ";
                        }
                    }
                    else
                    {
                        Mensajes.Text = "El RFC No Contiene 12 Caracteres ";
                    }
                }
                else
                {
                    Mensajes.Text = "Coloca el RFC Moral ";
                }
            }
            return resultado;
        }

        protected void Continuar()
        {
            try
            {
                // ALAMCENA LOS DATOS APARTIR DE LA FUNCION recuperaCaptura
                

                wfiplib.EmisionVG oDatos = recuperaCaptura();

                int idTramite = armaTramiteYGuardaEnMemoria(oDatos.DatosHtml, oDatos.CitaMedica);
                if (idTramite > 0)
                {
                    oDatos.IdTramite = idTramite;
                    switch ((wfiplib.E_TipoTramite)Convert.ToInt32(TramiteTipPoliza.SelectedValue))
                    {
                        case wfiplib.E_TipoTramite.indPriEmisionVida:
                            Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()] = oDatos;
                            break;

                        case wfiplib.E_TipoTramite.indPriEmisionGMM:
                            Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()] = oDatos;
                            break;

                        default:
                            break;
                    }
                    Response.Redirect("anexaArchivos.aspx");
                }
            }
            catch (Exception ex)
            {
                enviaMsgCliente(ex.Message);
            }
        }

        private int armaTramiteYGuardaEnMemoria(string DatosHtml, bool blnCitaMedica)
        {
            int Id = (new wfiplib.admTramite()).siguienteId();
            wfiplib.tramiteP oTramite = new wfiplib.tramiteP();

            if (HombresClave.Checked.Equals(true))
            {
                oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.HombreClave);
            }
            else
            {
                oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.Tramite);
            }

            oTramite.Id = Id;

            switch ((wfiplib.E_TipoTramite)Convert.ToInt32((TramiteTipPoliza.SelectedValue)))
            {
                // VIDA
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    if (blnCitaMedica)
                    {
                        // DESHABILITAR CITA MEDICA FLUJO
                        oTramite.IdTipoTramite = wfiplib.E_TipoTramite.indPriEmisionVidaCM;
                        oTramite.IdTipoTramite = wfiplib.E_TipoTramite.indPriEmisionVida;
                    }
                    else
                    {
                        oTramite.IdTipoTramite = wfiplib.E_TipoTramite.indPriEmisionVida;
                    }
                    // PRIORIDAD DE TRAMITE
                    if (GrandesSumas())
                    {
                        oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.GrandesSumas);
                    }
                    else if (GrandesSumasPrimaTotal())
                    {
                        oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.GrandesSumasPrimas);
                    }

                    break;

                // GMM
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    oTramite.IdTipoTramite = wfiplib.E_TipoTramite.indPriEmisionGMM;
                    break;

                // Defaul
                default:
                    break;
            }

            //TODO: ###Pendiente: Prioridad de Tramites   5=>Normal, 4=>Alta, 3=>VIP
            oTramite.IdRamo = Convert.ToInt32(TramiteTipPoliza.SelectedValue);
            
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
            Session["URL"] = Final;

            return Id;
        }

        private wfiplib.EmisionVG recuperaCaptura()
        {
            // SE CREO LA CLASE serviciosVidaP para actualizar los nuevos datos
            wfiplib.EmisionVG resultado = new wfiplib.EmisionVG();
            try
            {
                if (ActividadCPDES.SelectedValue.Equals("True"))
                {
                    resultado.CPDES = true;
                    resultado.FolioCPDES = textFolioCPDES.Text.Trim();
                    resultado.EstatusCPDES = EstatusCPDES.Text.Trim();
                }
                if (HombresClave.Checked.Equals(true))
                {
                    resultado.HombreClave = true;
                }

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
                    resultado.EntidadFederativa = cboEstado.SelectedValue;
                }
                else if (cboTipoContratante.SelectedValue.Equals("Moral"))
                {
                    resultado.TipoPersona = wfiplib.E_TipoPersona.Moral;
                    resultado.Nombre = txNomMoral.Text.Trim();
                    resultado.FechaConst = dtFechaConstitucion.Text.Trim();
                    resultado.RFC = txRfcMoral.Text.Trim().ToUpper();
                    resultado.Nacionalidad = txTiNacionalidad.Text.Trim();
                    //resultado.EntidadFederativa = cboEstado2.SelectedValue;
                }

                if (CheckBox1.Checked.Equals(true))
                {
                    resultado.TitularNombre = txTiNombre.Text.Trim();
                    resultado.TitularApPat = txTiApPat.Text.Trim();
                    resultado.TitularApMat = txTiApMat.Text.Trim();
                    resultado.TitularNacionalidad = txTiNacionalidad.Text.Trim();
                    resultado.TitularFechaNacimiento = dtFechaNacimientoTitular.Text.Trim();
                    resultado.TitularSexo = txtSexoM.Text.Trim();
                    resultado.TitularEntidad = cboEstado2.SelectedValue;
                }
                // DATOS DE CITA MEDICA  RECONOCIMIENTO DE ASEGURADO TITULAR O PERSONA FISICA ///
                if (cboTipoContratante.SelectedValue.Equals("Moral"))
                {
                    if (CheckBox1.Checked.Equals(true))
                    {
                        int Edad = CalcularEdad(dtFechaNacimientoTitular.Text.Trim());
                        resultado.Edad = Edad.ToString();
                        resultado.SexoCitaMedica = txtSexoM.Text.Trim();
                    }
                }
                else if (cboTipoContratante.SelectedValue.Equals("Fisica"))
                {
                    if (CheckBox1.Checked.Equals(true))
                    {
                        int Edad = CalcularEdad(dtFechaNacimientoTitular.Text.Trim());
                        resultado.Edad = Edad.ToString();
                        resultado.SexoCitaMedica = txtSexoM.Text.Trim();
                    }
                    else
                    {
                        int Edad = CalcularEdad(dtFechaNacimiento.Text.Trim());
                        resultado.Edad = Edad.ToString();
                        resultado.SexoCitaMedica = txSexo.Text.Trim();
                    }
                }

                resultado.Producto1 = LisProducto1.Text.Trim();
                resultado.Plan1 = LisSubproducto1.Text.Trim();
                resultado.Producto2 = LisProducto2.Text.Trim();
                resultado.Plan2 = LisSubproducto2.Text.Trim();
                resultado.Detalle = txDetalle.Text.Trim();
                
                // NUEVOS DATOS DE AGREGACION
                resultado.NumeroOrden = textNumeroOrden.Text.Trim().ToUpper();
                resultado.AgenteClave = txIdAgente.Text.Trim();
                resultado.FechaSolicitud = dtFechaSolicitud.Text;
                resultado.SumaAsegurada = txtSumaAseguradaBasica.Text.Trim();

                switch ((wfiplib.E_TipoTramite)Convert.ToInt32(TramiteTipPoliza.SelectedValue))
                {
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                        resultado.SumaPolizas = txtSumaAseguradaPolizasVigentes.Text.ToString();
                        resultado.PrimaTotal = txtPrimaTotal.Text.ToString();
                        break;

                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        resultado.PrimaTotal = txtPrimaTotalGMM.Text.ToString();
                        break;

                    default:
                        break;
                }
                resultado.IdMoneda = cboMoneda.Text.Trim();
                
                ///////////////// CITA MEDICA /////////////
                String combo = TextCombo.Text.Trim();
                if (combo != "" && combo != null)
                {
                    resultado.CitaMedica = true;
                    //resultado.TempoLife = CheckBox3.Checked;
                    resultado.Combo = TextCombo.Text.Trim();
                    resultado.Cel = TextCel.Text.Trim();
                    resultado.Estado = LisEstado.Text.Trim();
                    resultado.CelAgentePromotor = TextCelAgentePromotor.Text.Trim();
                    resultado.Ciudad = LisCiudad.Text.Trim();
                    resultado.Correo = TextCorreo.Text.Trim();
                    resultado.LaboratorioHospital = LisLabHospital.Text.Trim();
                    resultado.Direccion = TextDireccion.Text.Trim();
                    resultado.Fecha1 = TextFecha1.Text.Trim();
                    resultado.Fecha2 = TextFecha2.Text.Trim();
                    resultado.Fecha3 = TextFecha3.Text.Trim();
                    resultado.Notas = notas.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        protected void TramiteTipPoliza_SelectedIndexChanged(object sender, EventArgs e)
        {
            TramiteTipoPoliza();
            // COLOCA TABULADOR EN PRIMERO LUGAR
            TramiteTipPoliza.Focus();
        }

        protected void TramiteTipoPoliza()
        {
            Tramite.Visible = false;
            subproducto1.Visible = false;
            subproducto2.Visible = false;
            ActCPDES.Visible = false;
            CPDS.Visible = false;
            BCitaMedica.Visible = false;
            ActividadCPDES.SelectedIndex = -1;
            EstatusCPDES.SelectedIndex = -1;

            switch ((wfiplib.E_TipoTramite)Convert.ToInt32(TramiteTipPoliza.SelectedValue))
            {
                // PRIVADO INDIVIDUAL VIDA
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    lblProductoRamo.Text = "* Producto";
                    lblSubProductoRamo.Text = "* Sub Producto";
                    Tramite.Visible = true;
                    ActCPDES.Visible = true;
                    subproducto1.Visible = true;
                    subproducto2.Visible = false;
                    BCitaMedica.Visible = false;
                    //BCitaMedica.Visible = true;
                    //CheckBox3.Visible = false;
                    ListaProductos();
                    LisSbproductos();

                    SumaAseguradaPolizasVigentes.Visible = true;
                    SumaAseguradaPolizasVigentesGMM.Visible = false;
                    cboMoneda.Enabled = true;
                    cargarMonedas(ref cboMoneda);
                    break;

                // PRIVADO INDIVIDUAL GMM
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    lblProductoRamo.Text = "* Producto";
                    lblSubProductoRamo.Text = "* Plan";
                    Tramite.Visible = true;
                    ActCPDES.Visible = false;
                    subproducto1.Visible = true;
                    subproducto2.Visible = false;
                    //CheckBox3.Visible = false;
                    ListaProductos();
                    LisSbproductos();
                    citamedica.Visible = false;

                    SumaAseguradaPolizasVigentes.Visible = false;
                    SumaAseguradaPolizasVigentesGMM.Visible = true;
                    cboMoneda.SelectedValue = "5";
                    cboMoneda.Enabled = false;
                    break;

                // DEFAULT
                default:
                    lblProductoRamo.Text = "NA";
                    lblSubProductoRamo.Text = "NA";
                    Tramite.Visible = false;
                    ActCPDES.Visible = false;
                    subproducto1.Visible = false;
                    subproducto2.Visible = false;
                    citamedica.Visible = false;
                    //CheckBox3.Visible = false;
                    // Nuevo requerimeinto de validacion 
                    SumaAseguradaPolizasVigentesGMM.Visible = false;
                    SumaAseguradaPolizasVigentes.Visible = false;
                    break;
            }
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
            catch(Exception ex)
            {
                isDate = false;
                strResultado = ex.Message;
                Console.Write("Error al Validar la Fecha: " + ex.Message);
            }
            return isDate;
        }

        protected void antecedentesRFC()
        {
            TextantecedentesRFC.Text = "";
            textRFCFisica.Text = "";

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                string RFC = txRfc.Text.Trim().Replace("-","");
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    textRFCFisica.Text = "Ya existen trámites registrados para el RFC";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                string RFC = txRfcMoral.Text.Trim().Replace("-", "");
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    TextantecedentesRFC.Text = "Ya existen trámites registrados para el RFC";
                }
            }
           
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
                        string strNombre = "";
                        strNombre = removerAcentos(txNombre.Text.ToUpper().Trim());

                        if (strNombre.Equals("MARIA") || strNombre.Equals("JOSE"))
                        {
                            strNombre += "A";
                        }

                        ObtieneRFC rfc = new ObtieneRFC();
                        txRfc.Text = rfc.RFC13Pocisiones(removerAcentos(txApPat.Text.ToUpper().Trim()), removerAcentos(txApMat.Text.ToUpper().Trim()), removerAcentos(strNombre), dtValor.ToString("yy/MM/dd"));
                        antecedentesRFC();
                    }
                    catch 
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
                        string strMoral = removerAcentos(txNomMoral.Text.ToUpper().Trim());
                        String[] arrPalabrasNo = { " EL ", " S DE RL ", " DE ", " LAS ", " DEL ", " COMPAÑÍA ", " SOCIEDAD ", " COOPERATIVA ", " S EN C POR A ", " S EN NC ", " PARA ", " POR ", " AL ", " E ", " SCL ", " SNC ", " OF ", " COMPANY ", " MC ", " VON ", " MI ", " SRL CV ", " SA MI ", " LA ", " SA DE CV ", " LOS ", " Y ", " SA ", " CIA ", " SOC ", " COOP ", " A EN P ", " S EN C ", " EN ", " CON ", " SUS ", " SC ", " SCS ", " THE ", " AND ", " CO ", " MAC ", " VAN ", " A ", " SA DE CV ", " COMPAÑÍA ", " COMPANÍA ", " DE ", " LA ", " LAS ", " MC ", " VON ", " DEL ", " LOS ", " Y ", " MAC ", " VAN ", " MI ", " SRL CV MI ", " SRL MI" };
                        foreach (string strPalabra in arrPalabrasNo)
                        {
                            strMoral = strMoral.Replace(strPalabra, " ");
                        }

                        String[] arrPalabras = strMoral.Split(' ');
                        if (arrPalabras.Length > 3)
                        {
                            strMoral = "";
                            strMoral += arrPalabras[0].ToString() + " ";
                            strMoral += arrPalabras[1].ToString() + " ";
                            strMoral += arrPalabras[2].ToString() + " ";
                        }

                        PersonaMoral moral = new PersonaMoral();
                        txRfcMoral.Text = moral.RetornaLetrasFinalesRFC(strMoral, dtValor.ToString("yy/MM/dd"));
                        antecedentesRFC();
                    }
                    catch 
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

        private static string removerAcentos(String texto)
        {
            string consignos = "áàäéèëíìïóòöúùuÁÀÄÉÈËÍÌÏÓÒÖÚÙÜçÇñÑ";
            string sinsignos = "aaaeeeiiiooouuuAAAEEEIIIOOOUUUcCnN";

            StringBuilder textoSinAcentos = new StringBuilder(texto.Length);
            int indexConAcento;
            foreach (char caracter in texto)
            {
                indexConAcento = consignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos.Append(sinsignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos.Append(caracter);
            }
            return textoSinAcentos.ToString();
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
            //////////////// GrandeSumas.Text = "Grandes sumas";
            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
                CheckBox1.Enabled = true;
                CheckBox2.Enabled = true;
                // edos.SeleccionarDependencias_DropDrownList(ref cboEstado);
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                pnPrsMoral.Visible = true;
                pnPrsFisica.Visible = false;
                CheckBox1.Checked = true;
                CheckB1();
                CheckBox1.Enabled = false;
                CheckBox2.Enabled = false;
                // edos.SeleccionarDependencias_DropDrownList(ref cboEstado2);
            }
            else
            {
                pnPrsFisica.Visible = false;
                pnPrsMoral.Visible = false;
            }

            lbNombreAgente.Text = daNombreDeAgente(hf_IdPromotoria.Value, txIdAgente.Text);
        }

        protected void NuProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NuProduct.SelectedValue.Equals("1"))
            {
                producto1.Visible = true;
                producto2.Visible = false;
                subproducto1.Visible = true;
                subproducto2.Visible = false;
            }
            else if (NuProduct.SelectedValue.Equals("2"))
            {
                producto1.Visible = true;
                producto2.Visible = true;
                subproducto2.Visible = true;
                subproducto2.Visible = true;
            }
        }

        protected void LisProducto1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LisSbproductos();
        }
        protected void LisProducto2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LisSbproductos();
        }

        private void ListaProductos()
        {
            DataTable listProductos = (new wfiplib.admEmisionVG()).cartgaCatProducto(TramiteTipPoliza.SelectedValue.ToString());
            LisProducto1.DataSource = listProductos;
            LisProducto1.DataBind();
            LisProducto1.DataTextField = "Nombre";
            LisProducto1.DataValueField = "IdCatProducto";
            LisProducto1.DataBind();
            //LisProducto1.Focus();

            LisProducto2.DataSource = listProductos;
            LisProducto2.DataBind();
            LisProducto2.DataTextField = "Nombre";
            LisProducto2.DataValueField = "IdCatProducto";
            LisProducto2.DataBind();
        }

        protected void LisSbproductos()
        {
            DataTable listProductos = (new wfiplib.admEmisionVG()).cartgaCatSupProducto(LisProducto1.SelectedValue.ToString());
            LisSubproducto1.DataSource = listProductos;
            LisSubproducto1.Items.Add("Seleccione");
            LisSubproducto1.DataBind();
            LisSubproducto1.DataTextField = "Nombre";
            LisSubproducto1.DataValueField = "IdCatSubProducto";
            LisSubproducto1.DataBind();
            //LisSubproducto1.Focus();

            DataTable listProductos2 = (new wfiplib.admEmisionVG()).cartgaCatSupProducto(LisProducto2.SelectedValue.ToString());
            LisSubproducto2.DataSource = listProductos2;
            LisSubproducto2.Items.Add("Seleccione");
            LisSubproducto2.DataBind();
            LisSubproducto2.DataTextField = "Nombre";
            LisSubproducto2.DataValueField = "IdCatSubProducto";
            LisSubproducto2.DataBind();
        }

        protected void ActividadCPDES_SelectedIndexChanged(object sender, EventArgs e)
        {
            CPDES();
        }
        
        protected void CPDES()
        {
            if (ActividadCPDES.SelectedValue.Equals("True"))
            {
                CPDS.Visible = true;
            }
            else if (ActividadCPDES.SelectedValue.Equals("False"))
            {
                CPDS.Visible = false;
            }
            else
            {
                CPDS.Visible = false;
            }
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
            array[4] = string.Concat(comercial.ClaveFront, " - " + comercial.Front);

            return array;
        }

    }
}