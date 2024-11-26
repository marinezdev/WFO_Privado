using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wfiplib;

namespace wfip.laboratorios
{
    public partial class OpConsultaTramite : System.Web.UI.Page
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
                if (Request.Params["Id"] != null)
                {
                    pintaTramite(Request.Params["Id"]);
                }
                else
                {
                    Response.Redirect("MisTramites.aspx");
                }
            }
        }

        protected void pintaTramite(string Id)
        {
            int pIdTramite = Convert.ToInt32(Id);
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            if (oTramite != null)
            {
                string IdTipoTramite = oTramite.IdTipoTramite.ToString();
                switch (oTramite.IdTipoTramite)
                {
                    case wfiplib.E_TipoTramite.serviciosVida:
                        break;
                    case wfiplib.E_TipoTramite.ServicioGmm:
                        break;

                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        wfiplib.EmisionVG oEmisionGmm = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                        llenaDatos(pIdTramite);
                        CargarInformacionTramite(pIdTramite, oEmisionGmm, oTramite.IdTipoTramite);
                        break;

                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        break;

                    default:
                        break;
                }
            }
            else
            {
                Response.Redirect("MisTramites.aspx");
            }
        }

        private void llenaDatos(int pIdTramite)
        {
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            ltInfTipoTramite.Text = oTramite.Flujo + "<br />" + oTramite.TramiteNombre;

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:

                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    ltInfTipoTramite.Text = oTramite.Flujo.ToUpper() + "<br />";
                    ltInfTipoTramite.Text += "VIDA";
                    wfiplib.EmisionVG oEmisionGmm = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                    ltInfContratante.Text = oEmisionGmm.DatosHtml;
                    wfiplib.EmisionVG oEmisionGmm2 = (new wfiplib.admEmisionVG()).cargaFolio(pIdTramite);
                    ltInfFolio.Text = oEmisionGmm2.FolioHtml;
                    //DataTable lstProductos2 = (new wfiplib.admEmisionVG()).cargaProdructos(pIdTramite);
                    //rptTramite.DataSource = lstProductos2;
                    //rptTramite.DataBind();
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Realiza la carga de información del Trámite
        /// </summary>
        /// <param name="pIdTramite">Id del Trámite.</param>
        /// <param name="oEmisionGmm">Infomación del Trámite para mostrar</param>
        private void CargarInformacionTramite(int pIdTramite, wfiplib.EmisionVG oEmisionGmm, E_TipoTramite idTipoTramite)
        {
            string strNacionalidad = "";
            IdTramite.Text = pIdTramite.ToString();
            int IdMoneda = Convert.ToInt32(oEmisionGmm.IdMoneda.ToString());
            DatosPromotoria(oEmisionGmm.IdPromotoria.ToString());
            
            DataTable ValorMoneda = (new wfiplib.admEmisionVG()).ValorMoneda(IdMoneda);
            DataRow row = ValorMoneda.Rows[0];
            //InfoMoneda.Text = row["Nombre"].ToString().ToUpper();

            /* APARTIR DEL TIPO DE TRAMITE MOESTRARA LAS CANTIDADES APLIDAS*/
            switch (idTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    //SumaAseguradaPólizasVigentes.Visible = true;
                    // CALCULA GRANDES SUMAS;
                    //if (GrandesSumas(oEmisionGmm.SumaAsegurada.ToString(), oEmisionGmm.SumaPolizas.ToString(), oEmisionGmm.IdMoneda.ToString()))
                    //{
                    //    InfoGrandeSumas.Text = "GRANDES SUMAS";
                    //}
                    //// CONTENIDO CPDES
                    //if (oEmisionGmm.CPDES)
                    //{
                    //    TramiteInformacionCPDES.Visible = true;
                    //    InfoFolioCPDES.Text = oEmisionGmm.FolioCPDES.ToString();
                    //    InfoEstatusCPDES.Text = oEmisionGmm.EstatusCPDES;
                    //}
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    break;

                default:
                    break;
            }

            //InfoFechaRegistro.Text = oEmisionGmm.FechaRegistro.ToString();
            //InfoSumaAseguradaBasica.Text = oEmisionGmm.SumaAsegurada.ToString();
            //InfoSumaAseguradaPolizasVigentes.Text = oEmisionGmm.SumaPolizas.ToString();
            //InfoPrimaTotal.Text = oEmisionGmm.PrimaTotal.ToString();
            //InfoNumero.Text = oEmisionGmm.NumeroOrden.ToString();
            //InfoFechaSolicitud.Text = oEmisionGmm.FechaSolicitud.ToString();

            switch (oEmisionGmm.TipoPersona)
            {
                case wfiplib.E_TipoPersona.Fisica:
                    //InfoContratante.Text = "FISICA";

                    //InfoPrsFisica.Visible = true;
                    //InfoPrsMoral.Visible = false;

                    //InfoFNombre.Text = oEmisionGmm.Nombre.ToString();
                    //InfoFApellidoP.Text = oEmisionGmm.ApPaterno.ToString();
                    //InfoFApellidoM.Text = oEmisionGmm.ApMaterno.ToString();
                    //InfoFSexo.Text = oEmisionGmm.Sexo.ToString().ToUpper();
                    //InfoFRFC.Text = oEmisionGmm.RFC.ToString();
                    //InfoFNacionalidad.Text = oEmisionGmm.Nacionalidad.Trim().ToString();
                    //InfoFFechaNa.Text = oEmisionGmm.FechaNacimiento.ToString();

                    break;
                case wfiplib.E_TipoPersona.Moral:
                    //InfoContratante.Text = "MORAL";
                    //InfoPrsMoral.Visible = true;
                    //InfoPrsFisica.Visible = false;

                    //InfoMNombre.Text = oEmisionGmm.Nombre.ToString();
                    //InfoMFechaConsti.Text = oEmisionGmm.FechaConst.ToString();
                    //InfoMRFC.Text = oEmisionGmm.RFC.ToString();
                    break;

                default:
                    //InfoPrsFisica.Visible = false;
                    //InfoPrsMoral.Visible = false;
                    break;
            }

            if (oEmisionGmm.TitularNombre.ToString() != "")
            {
                //InfoDiContratante.Visible = true;
                //InfoFContratante.Text = "NO";
                //InfoTNombre.Text = oEmisionGmm.TitularNombre.ToString();
                //InfoTApellidoP.Text = oEmisionGmm.TitularApPat.ToString();
                //InfoTApellidoM.Text = oEmisionGmm.TitularApMat.ToString();
                //InfoTNacionalidad.Text = oEmisionGmm.TitularNacionalidad.Trim().ToString();
                //InfoTSexo.Text = oEmisionGmm.TitularSexo.ToString().ToUpper();
                //InfoTNacimiento.Text = oEmisionGmm.TitularFechaNacimiento.ToString();
            }
            TextFecha4.TimeSectionProperties.Visible = true;
            TextFecha4.UseMaskBehavior = true;
            TextFecha4.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha4.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

            wfiplib.EmisionVG oCitamedica = (new wfiplib.admEmisionVG()).cargaCitaMedicaLaboratorio(pIdTramite);
            if (oCitamedica != null)
            {
                InfoCombo.Text = oCitamedica.Combo.ToString();
                InfoSexo.Text = oCitamedica.Sexo.ToString();
                InfoEdad.Text = oCitamedica.Edad.ToString();
                //InfoCelular.Text = oCitamedica.Cel.ToString();
                //InfoCelularAgentePromotor.Text = oCitamedica.CelAgentePromotor.ToString();
                //InfoCorreo.Text = oCitamedica.Correo.ToString();
                InfoEstado.Text = oCitamedica.Estado.ToString();
                InfoCiudad.Text = oCitamedica.Ciudad.ToString();
                InfoSucursal.Text = oCitamedica.LaboratorioHospital.ToString();
                InfoDireccion.Text = oCitamedica.Direccion.ToString();
                InfoFecha1.Text = oCitamedica.Fecha1.ToString();
                InfoFecha2.Text = oCitamedica.Fecha2.ToString();
                InfoFecha3.Text = oCitamedica.Fecha3.ToString();
                TextFecha4.Text = oCitamedica.Fecha4.ToString();
                FechaSelecion(oCitamedica.FechaSeleccionada);
            }
            
            strNacionalidad = (new wfiplib.admEmisionVG()).NacionalidadSancionada(oEmisionGmm.Nacionalidad.Trim());
            if (strNacionalidad == "NA")
            {
                lblAdvertencia.Visible = false;
                lblAdvertencia.Text = "";
            }
            else
            {
                lblAdvertencia.Visible = true;
                lblAdvertencia.Text = " * El Contratante es de Nacionalidad " + strNacionalidad;
            }
        }

        protected bool GrandesSumas(string SumaBasica, string SumaPolizasVigentes, string cboMoneda)
        {
            bool resultado = false;

            // Monto total de la suma asegura y prima total, apartir del tipo de moneda seleccionada
            Double MontoTotal = Total(SumaBasica, SumaPolizasVigentes, cboMoneda);
            // Validacion del monto "MAL PROGRAMADO" el id del dolar es = 2, el regitro esta en SQL cat_moneda 
            double ValidacionMonto = double.Parse("1500000");
            ValidacionMonto = convertir(ValidacionMonto, 2);
            if (MontoTotal >= ValidacionMonto)
            {
                resultado = true;
            }
            return resultado;
        }

        protected double Total(String SumaAsegurda, String TotalCotiacion, String Moneda)
        {
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
            String valor = row["valor"].ToString();
            Double Moneda = Convert.ToDouble(valor);

            total = numero * Moneda;
            return total;
        }

        private void DatosPromotoria(String IdPromotoria)
        {
            wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(IdPromotoria));
            wfiplib.comercialPromotoria comercial = (new wfiplib.admAgentesPromotoria()).getComercialInformation(promotoria.Clave);

            //InfoClave.Text = promotoria.Clave.ToString();
            //InfoRegion.Text = string.Concat(comercial.ClaveRegion, " - " + comercial.Region);
            //InfoGerente.Text = string.Concat(comercial.ClaveGerente, " - " + comercial.Gerente);
            //InfoEjecutivo.Text = string.Concat(comercial.ClaveEjecutivo, " - " + comercial.Ejecutivo);
        }

        protected void FechaSelecion(int FechaSeleccionada)
        {
            switch (FechaSeleccionada)
            {
                case 1:
                    Radio1.Checked = true;
                    break;
                case 2:
                    Radio2.Checked = true;
                    break;
                case 3:
                    Radio3.Checked = true;
                    break;
                case 4:
                    Radio4.Checked = true;
                    break;
            }
        }

        protected string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            wfiplib.EmisionVG oDatos = recuperaCaptura();
            wfiplib.admTramite oAdmTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG oAdmEmisionVG = new wfiplib.admEmisionVG();

            if (oAdmEmisionVG.AlteraCitaMedicaLaboratorio(oDatos))
            {
                if (oAdmTramite.ActualiaEstadoLaboratorio(oDatos.IdTramite))
                {
                    if (oAdmTramite.ActualiaEstadoLaboratorioMesa(oDatos.IdTramite))
                    {
                        registraBitacoraLaboratorio(oDatos.IdTramite, E_EstadoMesa.Procesado);
                        Response.Redirect("MisTramites.aspx");
                    }
                }
            }
        }

        private wfiplib.EmisionVG recuperaCaptura()
        {
            wfiplib.EmisionVG resultado = new wfiplib.EmisionVG();
            try
            {
                int idTramite = Convert.ToInt32(IdTramite.Text.Trim());

                resultado.IdTramite = Convert.ToInt32(IdTramite.Text.Trim());
                resultado.Fecha4 = TextFecha4.Text.Trim();
                
                if (Radio1.Checked)
                {
                    resultado.FechaSeleccionada = 1;
                }
                else if (Radio2.Checked)
                {
                    resultado.FechaSeleccionada = 2;
                }
                else if (Radio3.Checked)
                {
                    resultado.FechaSeleccionada = 3;
                }
                else if (Radio4.Checked)
                {
                    resultado.FechaSeleccionada = 4;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        private void registraBitacoraLaboratorio(int pIdTramite, wfiplib.E_EstadoMesa pEstado)
        {
            wfiplib.bitacora oBitacora = new wfiplib.bitacora();
            oBitacora.IdFlujo = -2;
            oBitacora.IdTipoTramite = 0;
            oBitacora.IdTramite = pIdTramite;
            oBitacora.IdMesa = -2;
            oBitacora.Usuario = manejo_sesion.Credencial.Nombre;
            oBitacora.IdUsuario = manejo_sesion.Credencial.Id;
            oBitacora.FechaInicio = DateTime.Now;
            oBitacora.Estado = pEstado;
            oBitacora.Observacion = "Laboratorio.";
            oBitacora.ObservacionPrivada = "Laboratorio";
            (new wfiplib.admBitacora()).Nuevo(oBitacora);
        }
    }
}