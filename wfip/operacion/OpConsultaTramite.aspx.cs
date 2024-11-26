using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wfiplib;

namespace wfip.operacion
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
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    hdIdTramite.Value = urlCifrardo.IdTramite;
                    pintaTramite(urlCifrardo.IdTramite);

                    if ((new wfiplib.admEmisionVG()).CitaMedicaGenerada(Convert.ToInt32(urlCifrardo.IdTramite)))
                    {
                        btnCitasMedicas.Visible = true;
                    }
                    else
                    {
                        btnCitasMedicas.Visible = false;

                    }

                    if ((new wfiplib.admEmisionVG()).EsperaResultadoLab(Convert.ToInt32(urlCifrardo.IdTramite)))
                    {
                        btnUpResultados.Visible = true;
                    }
                    else
                    {
                        btnUpResultados.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("MisTramites.aspx");
                }
            }
        }

        protected void pintaTramite(string Id)
        {
            ltStatusTramite.Text = "";
            btnCarta.Visible = false;

            int pIdTramite = Convert.ToInt32(Id);
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            if (oTramite!= null)
            {
                string  IdTipoTramite = oTramite.IdTipoTramite.ToString();
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
                        CargarInformacionTramite(pIdTramite, oEmisionGmm , oTramite.IdTipoTramite);
                        break;

                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                        wfiplib.EmisionVG oEmisionVida = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                        llenaDatos(pIdTramite);
                        CargarInformacionTramite(pIdTramite, oEmisionVida, oTramite.IdTipoTramite);
                        break;

                    default:
                    break;
                }

                // Verificamo status del trámite
                string strStatus = "";
                string strImagen = "";
                switch (oTramite.Estado)
                {
                    case E_EstadoTramite.Caducado:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaRoja.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Caducado";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.Cancelado:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaRoja.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Cancelado";
                        btnCarta.Visible = true;
                        break;

                    case E_EstadoTramite.CMConfirmacionPendiente:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaAmarilla.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Confirmación Pendiente.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.CMRevProspecto:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaAmarilla.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Revisión con Prospecto.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.Ejecucion:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaVerde.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Ejecutado.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.EsperaResultados:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaAmarilla.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Espera de Resultados.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.Hold:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaAmarilla.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Hold.";
                        btnCarta.Visible = true;
                        break;

                    case E_EstadoTramite.InfoCitaMedica:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaAmarilla.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Información Citas Médicas.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.PCI:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaMorada.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "PCI.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.Proceso:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaAzul.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Proceso.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.PromotoriaCancela:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaRoja.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Promotoría Cancela.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.PromotoriaReconsidera:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaAzul.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Promotoría Reconsidera.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.Rechazo:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaRoja.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Rechazado.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.Registro:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaGris.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Registro.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.RevPromotoria:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaNaranja.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Revisión Promotoría.";
                        btnCarta.Visible = false;
                        break;

                    case E_EstadoTramite.Suspendido:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaNaranja.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "Suspendido.";
                        btnCarta.Visible = true;
                        break;

                    default:
                        strImagen = "<td style='text-align: center; background-color: White; color: #333333;'><img src = '../img/bolaGrisObscuro.png' alt = '' height = '24px' width = '24px' />";
                        strStatus += "";
                        btnCarta.Visible = false;
                        break;
                }

                ltStatusTramite.Text = "<br/><table width='100%'><tr><td style='text-align: right; background-color:#1572B7; color:white; font-size:15px;'>Status Trámite: <strong>&nbsp;&nbsp;&nbsp;" +  strStatus + "&nbsp;&nbsp;&nbsp;</strong></td>" + strImagen + "</td></tr></table>";
            }
            else
            {
                Response.Redirect("MisTramites.aspx");
            }
        }

        protected void btnGeneraCarta_Click(object sender, EventArgs e)
        {
            int idTramite = Convert.ToInt32(hdIdTramite.Value);
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt("Id=" + urlCifrardo.IdTramite);

            DataTable datos = new DataTable();
            string carta = string.Empty;
            string cartaRequisitada = string.Empty;
            datos = (new wfiplib.Reportes()).DatosCarta(idTramite);
            wfiplib.E_EstadoTramite statusTramite = (wfiplib.E_EstadoTramite)Enum.Parse(typeof(wfiplib.E_EstadoTramite), datos.Rows[0]["Estado"].ToString(), true);
            switch (statusTramite)
            {
                case E_EstadoTramite.Hold:
                    Response.Write("<script type = 'text/javascript' > window.open('../promotoria/CartaPDFHold.aspx?data=" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;

                case E_EstadoTramite.Suspendido:
                    Response.Write("<script type = 'text/javascript' > window.open('../promotoria/CartaPDFSuspencion.aspx?data=" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;

                case E_EstadoTramite.Rechazo:
                    Response.Write("<script type = 'text/javascript' > window.open('../promotoria/CartaPDFRechazo.aspx?data=" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;

                case E_EstadoTramite.Ejecucion:
                    Response.Write("<script type = 'text/javascript' > window.open('../promotoria/CartaPDFAceptado.aspx?data=" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;
                case E_EstadoTramite.Cancelado:
                    Response.Write("<script type = 'text/javascript' > window.open('../promotoria/CartaPDFCancelar.aspx?data=" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;
            }
        }

        private void llenaDatos(int pIdTramite)
        {
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            ltInfTipoTramite.Text = oTramite.Flujo + "<br />" + oTramite.TramiteNombre;
            
            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    //wfiplib.serviciosVida oServiciosVida = (new wfiplib.admServiciosVida()).carga(pIdTramite);
                    wfiplib.serviciosVidaP oServiciosVida = (new wfiplib.admServiciosVida()).carga(pIdTramite);
                    ltInfContratante.Text = oServiciosVida.CabeceraHtml;
                    wfiplib.serviciosVidaP oServiciosVida2 = (new wfiplib.admServiciosVida()).cargaFolio(pIdTramite);
                    ltInfFolio.Text = oServiciosVida2.FolioHtml;
                    wfiplib.serviciosVidaP oServiciosVida3 = (new wfiplib.admServiciosVida()).carga(pIdTramite);
                    ltInfProducto.Text = oServiciosVida3.ProductoHtml;

                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    wfiplib.ServicioGmm oServiciosGmm = (new wfiplib.admServicioGmm()).carga(pIdTramite);
                    ltInfContratante.Text = oServiciosGmm.CabeceraHtml;
                    wfiplib.ServicioGmmP oServiciosGmm2 = (new wfiplib.admServicioGmm()).cargaFolio(pIdTramite);
                    ltInfFolio.Text = oServiciosGmm2.FolioHtml;
                    wfiplib.ServicioGmmP oServiciosGmm3 = (new wfiplib.admServicioGmm()).CargaP(pIdTramite);
                    ltInfProducto.Text = oServiciosGmm3.ProductosHtml;
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    ltInfTipoTramite.Text = oTramite.Flujo.ToUpper() + "<br />";
                    ltInfTipoTramite.Text += "VIDA";
                    wfiplib.EmisionVG oEmisionGmm = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                    ltInfContratante.Text = oEmisionGmm.DatosHtml;
                    wfiplib.EmisionVG oEmisionGmm2 = (new wfiplib.admEmisionVG()).cargaFolio(pIdTramite);
                    ltInfFolio.Text = oEmisionGmm2.FolioHtml;
                    DataTable lstProductos2 = (new wfiplib.admEmisionVG()).cargaProdructos(pIdTramite);
                    rptTramite.DataSource = lstProductos2;
                    rptTramite.DataBind();
                    MuestraSleccionVida(pIdTramite);
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    ltInfTipoTramite.Text = oTramite.Flujo.ToUpper() + "<br />";
                    ltInfTipoTramite.Text += "GASTOS MÉDICOS MAYORES";
                    wfiplib.EmisionVG oEmisionVida = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                    ltInfContratante.Text = oEmisionVida.DatosHtml;
                    wfiplib.EmisionVG oEmisionVida2 = (new wfiplib.admEmisionVG()).cargaFolio(pIdTramite);
                    ltInfFolio.Text = oEmisionVida2.FolioHtml;
                    DataTable lstProductos = (new wfiplib.admEmisionVG()).cargaProdructos(pIdTramite);
                    MuestraSleccion(pIdTramite);
                    rptTramite.DataSource = lstProductos;
                    rptTramite.DataBind();
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
            int IdMoneda = Convert.ToInt32(oEmisionGmm.IdMoneda.ToString());
            DatosPromotoria(oEmisionGmm.IdPromotoria.ToString());

            DataTable ValorMoneda = (new wfiplib.admEmisionVG()).ValorMoneda(IdMoneda);
            DataRow row = ValorMoneda.Rows[0];
            InfoMoneda.Text = row["Nombre"].ToString().ToUpper();

            if (oEmisionGmm.HombreClave)
            {
                InfoHobreClave.Text = "SI";
            }
            else
            {
                InfoHobreClave.Text = "NO";
            }

            // PRIORIDAD DE TRAMITE
            InfoPrioridad.Text = "";
            switch (oEmisionGmm.Prioridad)
            {
                case wfiplib.E_PrioridadTramite.GrandesSumas:
                    InfoPrioridad.Text = "GRANDES SUMAS";
                    break;
                case wfiplib.E_PrioridadTramite.HombreClave:
                    InfoPrioridad.Text = "HOMBRES CLABE";
                    break;
                case wfiplib.E_PrioridadTramite.Tramite:
                    InfoPrioridad.Text = "";
                    break;
                default:
                    InfoPrioridad.Text = "";
                    break;
            }

            /* APARTIR DEL TIPO DE TRAMITE MOESTRARA LAS CANTIDADES APLIDAS*/
            switch (idTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    SumaAseguradaPólizasVigentes.Visible = true;
                    
                    if (oEmisionGmm.CPDES)
                    {
                        TramiteInformacionCPDES.Visible = true;
                        InfoFolioCPDES.Text = oEmisionGmm.FolioCPDES.ToString();
                        InfoEstatusCPDES.Text = oEmisionGmm.EstatusCPDES;
                    }
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    break;

                default:
                    break;
            }

            InfoFechaRegistro.Text = oEmisionGmm.FechaRegistro.ToString();
            InfoSumaAseguradaBasica.Text = oEmisionGmm.SumaAsegurada.ToString();
            InfoSumaAseguradaPolizasVigentes.Text = oEmisionGmm.SumaPolizas.ToString();
            InfoPrimaTotal.Text = oEmisionGmm.PrimaTotal.ToString();
            InfoNumero.Text = oEmisionGmm.NumeroOrden.ToString();
            InfoFechaSolicitud.Text = oEmisionGmm.FechaSolicitud.ToString();

            switch (oEmisionGmm.TipoPersona)
            {
                case wfiplib.E_TipoPersona.Fisica:
                    InfoContratante.Text = "FISICA";

                    InfoPrsFisica.Visible = true;
                    InfoPrsMoral.Visible = false;

                    InfoFNombre.Text = oEmisionGmm.Nombre.ToString();
                    InfoFApellidoP.Text = oEmisionGmm.ApPaterno.ToString();
                    InfoFApellidoM.Text = oEmisionGmm.ApMaterno.ToString();
                    InfoFSexo.Text = oEmisionGmm.Sexo.ToString().ToUpper();
                    InfoFRFC.Text = oEmisionGmm.RFC.ToString();
                    InfoFNacionalidad.Text = oEmisionGmm.Nacionalidad.Trim().ToString();
                    InfoFFechaNa.Text = oEmisionGmm.FechaNacimiento.ToString();

                    break;
                case wfiplib.E_TipoPersona.Moral:
                    InfoContratante.Text = "MORAL";
                    InfoPrsMoral.Visible = true;
                    InfoPrsFisica.Visible = false;

                    InfoMNombre.Text = oEmisionGmm.Nombre.ToString();
                    InfoMFechaConsti.Text = oEmisionGmm.FechaConst.ToString();
                    InfoMRFC.Text = oEmisionGmm.RFC.ToString();
                    break;

                default:
                    InfoPrsFisica.Visible = false;
                    InfoPrsMoral.Visible = false;
                    break;
            }

            if (oEmisionGmm.TitularNombre.ToString() != "")
            {
                InfoDiContratante.Visible = true;
                InfoFContratante.Text = "NO";
                InfoTNombre.Text = oEmisionGmm.TitularNombre.ToString();
                InfoTApellidoP.Text = oEmisionGmm.TitularApPat.ToString();
                InfoTApellidoM.Text = oEmisionGmm.TitularApMat.ToString();
                InfoTNacionalidad.Text = oEmisionGmm.TitularNacionalidad.Trim().ToString();
                InfoTSexo.Text = oEmisionGmm.TitularSexo.ToString().ToUpper();
                InfoTNacimiento.Text = oEmisionGmm.TitularFechaNacimiento.ToString();
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
            
            wfiplib.admTramiteMesa adm = new wfiplib.admTramiteMesa();
            rptTrmHistorial.DataSource = adm.daHistorialTramite(pIdTramite);
            rptTrmHistorial.DataBind();

            MuestraPDF(pIdTramite);
        }

        private void DatosPromotoria(String IdPromotoria)
        {
            wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(IdPromotoria));
            wfiplib.comercialPromotoria comercial = (new wfiplib.admAgentesPromotoria()).getComercialInformation(promotoria.Clave);

            InfoClave.Text = promotoria.Clave.ToString();
            InfoRegion.Text = string.Concat(comercial.ClaveRegion, " - " + comercial.Region);
            InfoGerente.Text = string.Concat(comercial.ClaveGerente, " - " + comercial.Gerente);
            InfoEjecutivo.Text = string.Concat(comercial.ClaveEjecutivo, " - " + comercial.Ejecutivo);
            InfoEjecutivoFront.Text = string.Concat(comercial.ClaveFront, " - " + comercial.Front);
        }

        protected void rptTrmHistorial_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString() + ",tp=" + e.CommandName.ToString(), "consultaTramite2.aspx").URL, true);
                //Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandName.ToString() + "&id=" + e.CommandArgument.ToString());
            }
        }

        private void MuestraPDF(int pIdTramite)
        {
            int IdTramite = int.Parse(hdIdTramite.Value.ToString());
            ltMuestraPdf.Text = "";
            ltMuestraPdf.Text = "<iframe src='" + EncripParametros("IdTramite=" + IdTramite, "../promotoria/Displaypdf.aspx").URL + "' style='width:100%; height:540px' style='border: none;'></iframe>";


            //wfiplib.admExpediente admExpediente = new wfiplib.admExpediente();
            ////admExpediente = new wfiplib.admExpediente();

            //string strDoctoWeb = "";
            //string strDoctoServer = "";
            //try
            //{
            //    wfiplib.expediente ArchivoFusion = admExpediente.daFusion(pIdTramite);
            //    if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
            //    {
            //        strDoctoWeb = "..\\DocsUp\\" + ArchivoFusion.NmArchivo;
            //        // strDoctoServer = Server.MapPath("~") + "DocsUp\\" + ArchivoFusion.NmArchivo;
            //        strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + ArchivoFusion.NmArchivo;
            //        if (File.Exists(strDoctoServer))
            //        {
            //            Session["consulta_docPop"] = strDoctoWeb;
            //        }
            //        else
            //        {
            //            strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
            //            Session["consulta_docPop"] = strDoctoWeb;
            //        }
            //    }
            //    else
            //    {
            //        //TODO: Crear un documento que indique que el archivo no se fuciono o no se cargo desde el inicio del tramite
            //        strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
            //        Session["consulta_docPop"] = strDoctoWeb;
            //    }
            //    ltMuestraPdf.Text = "<embed src='" + strDoctoWeb + "' style='width:100%; height:100%' type='application/pdf'>";
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        private void MuestraSleccionVida(int idTramite)
        {
            TablaBeneficiarios.Visible = false;
            DataTable lstRiesgos = (new wfiplib.admSeleccion()).CargaRiesgoFactoresVida(idTramite);
            if (lstRiesgos.Rows.Count > 0)
            {
                rptRiesgosInfoVida.DataSource = lstRiesgos;
                rptRiesgosInfoVida.DataBind();
                TablaBeneficiarios.Visible = true;
            }
        }

        private void MuestraSleccion(int idTramite)
        {
            TablaBeneficiarios.Visible = false;
            DataTable lstRiesgos = (new wfiplib.admSeleccion()).CargaRiesgoFactores(idTramite);
            if (lstRiesgos.Rows.Count > 0)
            {
                rptRiesgosInfo.DataSource = lstRiesgos;
                rptRiesgosInfo.DataBind();
                TablaBeneficiarios.Visible = true;
            }

            /*
            DataTable lstMesasTramite = (new wfiplib.admSeleccion()).VerificaMesas(idTramite);
            if (lstMesasTramite.Rows.Count > 0)
            {
                wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
                if (oAdmSeleccion.InactivoTodoSeleccion(idTramite))
                {
                    //Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                DataTable lstRiesgos = (new wfiplib.admSeleccion()).CargaRiesgoFactores(idTramite);
                if (lstRiesgos.Rows.Count > 0)
                {
                    rptRiesgos.DataSource = lstRiesgos;
                    rptRiesgos.DataBind();
                    rptRiesgosInfo.DataSource = lstRiesgos;
                    rptRiesgosInfo.DataBind();
                    TabllaSeleccion.Visible = true;
                    TablaBeneficiarios.Visible = true;
                }
            }
            */
        }

        protected void BtnCitaMedica(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt("Id=" + urlCifrardo.IdTramite);

            if ((new wfiplib.admEmisionVG()).CitaMedicaGenerada(Convert.ToInt32(urlCifrardo.IdTramite)))
            {
                Response.Write("<script type = 'text/javascript' > window.open('../promotoria/CitaPDF.aspx?data=" + Encrypt + "', '_blank', 'PDF MetLife', 'width=400,height=500')</script >;");
            }
        }

        protected void btnUpResultados_Click(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            int idTramite = Convert.ToInt32(urlCifrardo.IdTramite);
            int idMesa = -1;
            idMesa = (new wfiplib.admEmisionVG()).getIdMesaCitaMedica(Convert.ToInt32(urlCifrardo.IdTramite));
            Response.Redirect(EncripParametros("Id=" + idTramite.ToString() + ",tp=" + idMesa.ToString(), "consultaTramite2.aspx").URL, true);

            //int idTramite = Convert.ToInt32(Request.Params["Id"].ToString());
            //int idMesa = -1;
            //idMesa = (new wfiplib.admEmisionVG()).getIdMesaCitaMedica(Convert.ToInt32(Request.Params["Id"]));
            //Response.Redirect("consultaTramite2.aspx?tp=" + idMesa.ToString() + "&id=" + idTramite.ToString());
        }

        protected void BtnVerPDF_Click(object sender, EventArgs e)
        {
            PnlPdf.Visible = true;
        }

        protected void BtnCerrarPdf_Click(object sender, EventArgs e)
        {
            PnlPdf.Visible = false;
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            urlCifrardo.Result = false;
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string IdTramite = "";
                string Mesa = "";
                string Regreso = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusquedaIdTramite = stringBetween(s + ".", "Id=", ".");
                    if (BusquedaIdTramite.Length > 0)
                    {
                        IdTramite = BusquedaIdTramite;
                    }
                }

                if (IdTramite.Length > 0)
                {
                    urlCifrardo.IdTramite = IdTramite.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdTramite = "";
                }
            }
            catch (Exception)
            {
                urlCifrardo.Result = false;
            }

            return urlCifrardo;
        }
        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }
        public static string stringBetween(string Source, string Start, string End)
        {
            string result = "";
            if (Source.Contains(Start) && Source.Contains(End))
            {
                int StartIndex = Source.IndexOf(Start, 0) + Start.Length;
                int EndIndex = Source.IndexOf(End, StartIndex);
                result = Source.Substring(StartIndex, EndIndex - StartIndex);
                return result;
            }

            return result;
        }

    }
}