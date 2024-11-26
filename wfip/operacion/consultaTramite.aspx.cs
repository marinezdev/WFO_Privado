using DevExpress.Web;
using RFC;
using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using wfiplib;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;


namespace wfip.operacion
{
    public partial class consultaTramite : System.Web.UI.Page
    {
        public string Archivo = "";
        public Mensajes mensajes = new Mensajes();
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        admEntidadFederativa edos = new admEntidadFederativa();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx", true);
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            hfCadenaMotivosRechazo.Value = "0";
            wfiplib.mesa oMesa = (new wfiplib.admMesa()).carga(Convert.ToInt32(Request.QueryString["tp"]));
            DataTable dtH = new DataTable();
            DataTable dtS = new DataTable();
            DataTable dtR = new DataTable();
            DataTable dtC = new DataTable();
            DataTable dtCM = new DataTable();
            DataTable dtCMRevProspecto = new DataTable();
            DataTable dtCMCitaReprogram = new DataTable();

            switch (oMesa.Nombre)
            {
                case "ADMISIÓN": //Admisión
                    dtH = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 1);
                    dtC = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 9);
                    // MOTIVOS DE RECHAZO MEDA ADMISION - CON CITA MEDICA
                    dtCM = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 10);
                    break;
                case "REVISIÓN DOCUMENTAL": //Rev. Documental
                    dtH = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 1);
                    dtS = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 2);
                    break;
                case "REVISIÓN PLAD": //PLAD
                    dtH = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 1);
                    dtS = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 2);
                    break;
                case "SELECCIÓN": //Selección
                    dtS = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 2);
                    dtR = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 3);
                    break;
                case "CAPTURA": // CAPTURA
                    dtS = (new wfiplib.Reportes()).ListaMotivosRechazo(oMesa.Id.ToString(), 2);
                    break;

                case "CITAS MÉDICAS": //Citas Médicas
                case "RECEPCIÓN CITAS MEDICAS": //Recepción de Citas Médicas
                    dtCMRevProspecto = (new wfiplib.Reportes()).ListaMotivosRechazo(35.ToString(), 7);
                    dtCMCitaReprogram = (new wfiplib.Reportes()).ListaMotivosRechazo(35.ToString(), 8);
                    break;
            }
            treeListCancelar.ClearNodes();
            treeListCancelar.DataSource = dtC;
            treeListCancelar.DataBind();
            treeListCancelar.ExpandToLevel(0);

            treeListSuspender.ClearNodes();
            treeListSuspender.DataSource = dtS;
            treeListSuspender.DataBind();
            treeListSuspender.ExpandToLevel(2);

            treeListRechazo.ClearNodes();
            treeListRechazo.DataSource = dtR;
            treeListRechazo.DataBind();
            treeListRechazo.ExpandToLevel(2);

            treeListHold.ClearNodes();
            treeListHold.DataSource = dtH;
            treeListHold.DataBind();
            treeListHold.ExpandToLevel(3);

            treeListCMRevProspecto.ClearNodes();
            treeListCMRevProspecto.DataSource = dtCMRevProspecto;
            treeListCMRevProspecto.DataBind();
            treeListCMRevProspecto.ExpandToLevel(1);

            treeListCMCitaReprogramada.ClearNodes();
            treeListCMCitaReprogramada.DataSource = dtCMCitaReprogram;
            treeListCMCitaReprogramada.DataBind();
            treeListCMCitaReprogramada.ExpandToLevel(1);

            // ELIMINAR FOREACH CUANDO SE TENGA EL CATALOGO COMPLETO DE CITAS MEDICAS
            if (dtCM.Rows.Count > 0)
            {
                foreach (DataRow row in dtCM.Rows)
                {
                    hfCadenaMotivosRechazo.Value = row["id"].ToString();
                }
            }


            //================================================

            if (!IsPostBack)
            {
                tabGeneral.ActiveTabIndex = 0;
                pnlTabAntecedentes.Visible = false;
                pnlGrdResultadosBusca.Visible = false;
                pnlDatosTramiteBusca.Visible = false;
                dvCajaBtnsRegular.Visible = true;
                dvCajaBtnsApoyo.Visible = false;

                treeListCM.UnselectAll();
                treeListCMRevProspecto.UnselectAll();
                treeListCancelar.UnselectAll();
                treeListSuspender.UnselectAll();
                treeListRechazo.UnselectAll();
                treeListHold.UnselectAll();
                treeListCMCitaReprogramada.UnselectAll();

                cargarMonedas(ref cboMoneda);
                cargarNacionalidadesCombo_db(ref txNacionalidad);
                cargarNacionalidadesCombo_db(ref txTiNacionalidad);

                edos.SeleccionarDependencias_DropDrownList(ref cboEstado);
                edos.SeleccionarDependencias_DropDrownList(ref cboEstado2);


                int idMesa = Convert.ToInt32(Request.QueryString["tp"]);
                int idTramite = Convert.ToInt32(Request.QueryString["id"]);

                ReSeleccion.Enabled = true;             // Corrección a error reportado de que botón se deshabilita y nos los deja procesar trámite.

                Session["TramiteAutomatico"] = true;
                Session["MesaApoyo"] = "";

                if (!inicializaPantalla(idMesa, idTramite))
                {
                    Session["tramiteActual"] = null;
                    
                    int intIdFlujo = Convert.ToInt32(Request.QueryString["flujo"]);
                    Response.Redirect("esperaOperacion.aspx?flujo=" + intIdFlujo.ToString() + "&res=1", true);
                    //Response.Redirect(Request.RawUrl);
                }

                if (Session["nota"] != null)
                {
                    txComentarios.Text = Session["nota"].ToString();
                }
            }

            if (popDocumento.IsCallback)
            {
                ltPdfPop.Text = "<embed src='" + Session["consulta_docPop"].ToString() + "' style='width:100%; height:100%' type='application/pdf'>";
            }
        }

        private bool inicializaPantalla(int pIdMesa, int pIdTramiteXParametro)
        {
            if (Convert.ToBoolean(Session["TramiteAutomatico"]) == false)
            {
                int intIdFlujo = Convert.ToInt32(Request.QueryString["flujo"]);
                Response.Redirect("esperaOperacion.aspx?flujo=" + intIdFlujo.ToString(), true);
                return false;
            }

            bool resultado = false;
            int idTramite = 0;
            limpiaPantalla();
            wfiplib.mesa oMesa = (new wfiplib.admMesa()).carga(pIdMesa);
            lbNmMesa.Text = "MESA " + oMesa.Nombre;

            Radio1.Checked = false;
            Radio2.Checked = false;
            Radio3.Checked = false;
            Radio4.Checked = false;

            txObservacionesRechazo.Text = "";
            txObservacionesCancelacion.Text = "";
            txtObservacionPausarTramite.Text = "";
            txtObservacionesSuspencion.Text = "";
            txtObservacionesHold.Text = "";
            txtObservacionesCM.Text = "";
            ltBitacora.Text = "";
            ltBitacoraPrivada.Text = "";
            lblAdvertencia.Text = "";
            lblAdvertencia.Visible = false;

            if (pIdTramiteXParametro > 0)
            {
                if ((new wfiplib.admTramiteMesa()).atrapa(pIdTramiteXParametro, oMesa.Id, manejo_sesion.Credencial.Id))
                {
                    idTramite = pIdTramiteXParametro;
                }
            }
            else
            {
                idTramite = daSiguienteTramite(oMesa.Id, oMesa.eTipo);
                //////////////////////////////
                /*
                int idFlujo = obtenerIdFlujo(idTramite, Convert.ToInt16(idTramite.ToString("d")));
                Response.Redirect("consultaTramite2.aspx?tp=" + oMesa.Id.ToString() + "&id=" + idTramite.ToString(), true);
                */
            }

            if (idTramite > 0)
            {
                // Verificamos si puede enviar a otras mesas
                int idFlujo = 0;
                wfiplib.E_TipoTramite idTipoTramite = obtenerTipoTramite(idTramite);
                bool blnTramiteCancelado = VerificaTramiteCancelado(idTramite);
                
                IdTramiteSe.Text = idTramite.ToString();
                if (oMesa.SendToMesa)
                    pintaMesasToEnvio(oMesa.Id, idTramite);
                else
                {
                    pnSendToMesa.Visible = false;
                    btnSendToMesa.Visible = false;
                }

                hdIdTramite.Value = idTramite.ToString();
                hdIdMesa.Value = oMesa.Id.ToString();
                if (oMesa.ConApoyo == wfiplib.E_Estado.Activo)
                {
                    pnConApoyo.Visible = true;
                    btnReenviar.Visible = true;
                    pintaMesasDeApoyo(oMesa.IdFlujo, oMesa.Id, idTipoTramite);
                }
                else
                {
                    pnConApoyo.Visible = false;
                    btnReenviar.Visible = false;
                    if (oMesa.Apoyo == wfiplib.E_Estado.Activo)
                    {
                        btnRechazar.Visible = false;
                        btnHold.Visible = false;
                        btnSuspender.Visible = false;
                    }
                }

                idFlujo = obtenerIdFlujo(idTramite, Convert.ToInt16(idTipoTramite.ToString("d")));

                if (!blnTramiteCancelado)
                {
                    Session["tramiteActual"] = idTramite.ToString() + ";" + idFlujo.ToString() + ";" + idTipoTramite.ToString("d");
                    Session["tramiteActualId"] = idTramite;
                    Session["mesaActual"] = oMesa.Id;
                    llenaDatosYBitacora(idTramite, oMesa);
                    MuestraPDF(idTipoTramite, idTramite, oMesa.Nombre);
                    pintaInsumos();
                    resultado = true;
                    llenaAntecedentes(idTramite);
                    llenaFormularios(idTramite);
                    formulariosMesas(oMesa.Nombre, idTipoTramite.ToString());

                    int intTotalReIngresos = (new wfiplib.admEmisionVG()).countReingresos(idTramite, oMesa.Id);
                    if (intTotalReIngresos > 0)
                    {
                        btnSeguimientoTramitePopUp.Visible = true;
                        MuestraSeguimiento(idTramite, oMesa.Id);
                    }
                    else
                    {
                        btnSeguimientoTramitePopUp.Visible = false;
                    }
                }
                else
                {
                    txComentariosPrv.Text = "El trámite no se encuentra disponible.";
                    ProcesarTramite(wfiplib.E_EstadoTramite.Proceso, wfiplib.E_EstadoMesa.NoProcesable);
                    // Response.Redirect("consultaTramite2.aspx?flujo=" + idFlujo.ToString() + "&tp=" + hdIdMesa.Value.ToString(), true);
                    return false;
                }
            }
            return resultado;
        }

        protected void btnSeguimientoTramitePopUp_Click(object sender, EventArgs e)
        {
            // mensajes.MostrarMensaje(this, "Mostrar Mensaje de popUp");
        }

        /*********************************************************************************/
        /*************** MUESTRA FORMULARIO SELECCION, CITA MEDICA, PLAD *****************/
        /*********************************************************************************/
        private void formulariosMesas(String NombreMesa, String TipoTramite)
        {
            UpdatePanel1.Visible = false;
            ProgramarCita.Visible = false;
            CancelarCita.Visible = false;
            CitaMedica.Visible = false;
            DatosEjecucion.Visible = false;
            DatosTramiteInformacion.Visible = true;
            TablaBeneficiarios.Visible = false;
            EditarAdmision.Visible = false;
            SeleccionDatos.Visible = false;
            SelccionDatosVida.Visible = false;

            //if (NombreMesa == "SELECCIÓN")//
            if (NombreMesa == "SELECCIÓN" && (TipoTramite == "indPriEmisionGMM" || TipoTramite == "indPriEmisionConversiones"))
            {
                SeleccionDatos.Visible = true;
                //TextRiesgoFactor.Text = TipoTramite.ToString();
            }
            else if (NombreMesa == "SELECCIÓN" && TipoTramite == "indPriEmisionVida")
            {
                SelccionDatosVida.Visible = true;
            }
            else if (NombreMesa == "SELECCIÓN" && TipoTramite == "indPriEmisionVidaCM")
            {
                SelccionDatosVida.Visible = true;
            }

            if (NombreMesa == "REVISIÓN PLAD")
                DatosPlad.Visible = true;

            if (NombreMesa == "ADMISIÓN")
            {
                UpdatePanel1.Visible = true;
                EditarAdmision.Visible = true;
                GuardarCambiosAdmision.Enabled = true;

                Editar.Visible = false;
                DatosTramiteInformacion.Visible = false;
                ProgramarCita.Visible = false;
                CancelarCita.Visible = false;
                CitaMedica.Visible = false;
            }

            if (NombreMesa == "CITAS MÉDICAS")
            {
                DatosTramiteInformacion.Visible = false;
                UpdatePanel1.Visible = true;
                ProgramarCita.Visible = false;
                CancelarCita.Visible = true;
                CitaMedica.Visible = true;
                DatosFechaRecepcion.Visible = true;
                btnGenerarCartaPrevia.Visible = false;
            }

            if (NombreMesa == "RECEPCIÓN CITAS MEDICAS")
            {
                DatosTramiteInformacion.Visible = false;
                UpdatePanel1.Visible = true;
                ProgramarCita.Visible = false;
                CancelarCita.Visible = true;
                CitaMedica.Visible = true;
                EditarAdmision.Visible = false;
            }

            if (NombreMesa == "EJECUCIÓN")
            {

                DatosTramiteInformacion.Visible = true;
                TextNumPolizaSisLegado.Enabled = false;
                DatosEjecucion.Visible = true;
            }
            if (NombreMesa == "KWIK")
            {
                DatosTramiteInformacion.Visible = true;
                TextNumKwik.Enabled = false;
                DatosKWIK.Visible = true;
            }

            if (NombreMesa == "CAPTURA" || NombreMesa == "EJECUCIÓN" || NombreMesa == "CONTROL")
            {
                TablaBeneficiarios.Visible = true;
            }
        }

        private void llenaFormularios(int pIdTramite)
        {
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    wfiplib.EmisionVG oEmisionGmm = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                    MuetraPLAD(pIdTramite, oEmisionGmm);
                    CargarInformacionTramite(pIdTramite, oEmisionGmm, oTramite.IdTipoTramite);
                    SeleccionDatos.Visible = false;
                    MuestraSleccionVida(pIdTramite, oEmisionGmm);
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    wfiplib.EmisionVG oEmisionVida = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                    MuetraPLAD(pIdTramite, oEmisionVida);
                    SelccionDatosVida.Visible = false;
                    MuestraSleccion(pIdTramite, oEmisionVida);
                    CargarInformacionTramite(pIdTramite, oEmisionVida, oTramite.IdTipoTramite);
                    cboMoneda.SelectedValue = "1";
                    cboMoneda.Enabled = false;
                    break;

                default:
                    break;
            }

            if (oTramite.Estado == E_EstadoTramite.PromotoriaReconsidera)
            {
                lblAdvertencia.Visible = true;
                lblAdvertencia.Text += "<br/> Promotoría solicita reconsideración.";
            }

        }

        private int obtenerIdFlujo(int idTramite, int idTipoTramite)
        {
            int resultado;
            resultado = (new wfiplib.admUsuarioMesa()).DaIdFlujo(idTramite, idTipoTramite);
            return resultado;
        }

        private wfiplib.E_TipoTramite obtenerTipoTramite(int idTramite)
        {
            wfiplib.E_TipoTramite resultado;
            int idTipoTramite = -1;
            idTipoTramite = (new wfiplib.admUsuarioMesa()).DaIdTipoTramite(idTramite);
            resultado = (wfiplib.E_TipoTramite)Enum.Parse(typeof(wfiplib.E_TipoTramite), idTipoTramite.ToString());
            return resultado;
        }

        private bool VerificaTramiteCancelado(int IdTramite)
        {
            bool blnTramiteCancelado = false;
            blnTramiteCancelado = (new wfiplib.admUsuarioMesa()).VerificaTramiteCancelado(IdTramite);
            return blnTramiteCancelado;
        }

        private void limpiaPantalla()
        {
            ltPdfPop.Text = "";
            ltMuestraPdf.Text = "";
            hdArchivo.Value = "";
            hfIdArchivo.Value = "9999";
            hdIdTramite.Value = "";
            hdEnLinea.Value = "1";
            ltInfContratante.Text = "";
            ltBitacora.Text = "";
            txComentarios.Text = "";
            txComentariosPrv.Text = "";
            txtObservacionNoAplica.Text = "";
            Session.Contents.Remove("consulta_docPop");
        }

        protected void lsChApoyo_Changed(object sender, EventArgs e)
        {
            bool bnlMesaActiva = false;
            wfiplib.admTramiteMesa tramiteMesa = new wfiplib.admTramiteMesa();
            DataTable data = tramiteMesa.ConsultaMesaCitaMedica(Convert.ToInt32(hdIdTramite.Value));

            foreach (ListItem item in lsChApoyo.Items)
            {
                if (item.Selected)
                    bnlMesaActiva = true;

                if (item.Selected && item.Text == "CITAS MÉDICAS")
                {
                    if (data.Rows.Count > 0)
                    {
                        mensajes.MostrarMensaje(this, "El trámite ya ha estado en citas médicas.");
                        foreach (ListItem itemDisabled in lsChApoyo.Items)
                        {
                            if (itemDisabled.Text == "CITAS MÉDICAS")
                            {
                                itemDisabled.Enabled = false;
                                itemDisabled.Selected = false;

                                bnlMesaActiva = false;
                            }
                        }
                    }
                    else
                    {
                        
                        Session["MesaApoyo"] = "CITAS MÉDICAS";
                        foreach (ListItem itemDisabled in lsChApoyo.Items)
                        {
                            if (itemDisabled.Text != "CITAS MÉDICAS")
                            {
                                itemDisabled.Enabled = false;
                                itemDisabled.Selected = false;
                            }

                        }

                        
                    } 
                }

                if (!item.Selected && item.Text == "CITAS MÉDICAS")
                {
                    string strMesaApoyoActual = Session["MesaApoyo"].ToString();
                    if (strMesaApoyoActual == "CITAS MÉDICAS")
                    {
                        Session["MesaApoyo"] = "";
                        foreach (ListItem itemDisabled in lsChApoyo.Items)
                        {
                            if (itemDisabled.Text != "CITAS MÉDICAS")
                            {
                                itemDisabled.Enabled = true;
                                itemDisabled.Selected = false;
                            }
                        }
                    }
                }
            }

            if (bnlMesaActiva)
            {
                dvCajaBtnsRegular.Visible = false;
                dvCajaBtnsApoyo.Visible = true;
            }
            else
            {
                dvCajaBtnsRegular.Visible = true;
                dvCajaBtnsApoyo.Visible = false;
            }
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            PanelEtitar.Enabled = true;
            CitaMedica.Enabled = true;
            GuardarCambios.Visible = true;
            Editar.Visible = false;
            CancelarCita.Enabled = true;
            ProgramarCita.Enabled = true;
            GuardarCambios.Enabled = true;

            /*****************************************/
            /************ HABILITA DATOS *************/
            /*****************************************
            pnPrsFisica.Enabled = true;
            pnPrsMoral.Enabled = true;
            InfCPDES.Enabled = true;
            */
        }

        protected void BtnEditarEjecucion_Click(object sender, EventArgs e)
        {
            GuardarEjecucion.Visible = true;
            GuardarEjecucion.Enabled = true;
            TextNumPolizaSisLegado.Enabled = true;
            EditarEjecucion.Visible = false;
        }

        protected void BtnEditarKwik_Click(object sender, EventArgs e)
        {
            GuardarKwik.Visible = true;
            GuardarKwik.Enabled = true;
            TextNumKwik.Enabled = true;
            EditarKwik.Visible = false;
        }

        protected void BtnEditarAdmision_Click(object sender, EventArgs e)
        {
            PanelEtitar.Enabled = true;
            //CitaMedica.Enabled = true;

            GuardarCambiosAdmision.Visible = true;

            EditarAdmision.Visible = false;
            //CancelarCita.Enabled = true;
            //ProgramarCita.Enabled = true;
            GuardarCambios.Enabled = true;

            /*****************************************/
            /************ HABILITA DATOS *************/
            /*****************************************
            pnPrsFisica.Enabled = true;
            pnPrsMoral.Enabled = true;
            InfCPDES.Enabled = true;
            */
        }

        protected void Index_Changed(object sender, EventArgs e)
        {
            bool bnlMesaActiva = false;

            if (!string.IsNullOrEmpty(lsChApoyo.SelectedValue.ToString()))
            {
                for (int i = 0; i < lsChApoyo.Items.Count; i++)
                    foreach (ListItem item in lsChApoyo.Items)
                    {
                        if (item.Selected)
                            bnlMesaActiva = true;

                        if (item.Selected && item.Text == "CITAS MÉDICAS")
                        {
                            Session["MesaApoyo"] = "CITAS MÉDICAS";
                            foreach (ListItem itemDisabled in lsChApoyo.Items)
                            {
                                if (itemDisabled.Text != "CITAS MÉDICAS")
                                {
                                    itemDisabled.Enabled = false;
                                    itemDisabled.Selected = false;
                                }
                            }
                        }

                        if (!item.Selected && item.Text == "CITAS MÉDICAS")
                        {
                            string strMesaApoyoActual = Session["MesaApoyo"].ToString();
                            if (strMesaApoyoActual == "CITAS MÉDICAS")
                            {
                                Session["MesaApoyo"] = "";
                                foreach (ListItem itemDisabled in lsChApoyo.Items)
                                {
                                    if (itemDisabled.Text != "CITAS MÉDICAS")
                                    {
                                        itemDisabled.Enabled = true;
                                        itemDisabled.Selected = false;
                                    }
                                }
                            }
                        }
                    }

                if (bnlMesaActiva)
                {
                    dvCajaBtnsRegular.Visible = false;
                    dvCajaBtnsApoyo.Visible = true;
                }
                else
                {
                    dvCajaBtnsRegular.Visible = true;
                    dvCajaBtnsApoyo.Visible = false;
                }
            }
        }

        /// <summary>
        /// Obtiene las mesas a las cuales se les puede regresar el flujo del tramite
        /// </summary>
        /// <param name="pIdMesa"></param>
        /// <param name="pIdTramite"></param>
        public void pintaMesasToEnvio(int pIdMesa, int pIdTramite)
        {
            // TODO: ### Pendiente: Lo ideal sería que se hiciara desde la consulta SQL pero existe el error conceptual sobre como enviar a una mesaje el trámite si en el flujo todavía no es prcesado
#pragma warning disable CS0219 // La variable 'drPlad' está asignada pero su valor nunca se usa
            DataRow drPlad = null;
#pragma warning restore CS0219 // La variable 'drPlad' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'blnDeleteRow' está asignada pero su valor nunca se usa
            bool blnDeleteRow = false;
#pragma warning restore CS0219 // La variable 'blnDeleteRow' está asignada pero su valor nunca se usa
            int IdMesaPlad = -1;

            IdMesaPlad = (new wfiplib.admMesa()).getTieneIdMesaPlad(pIdTramite);
            if (IdMesaPlad != -1)
            {
                lsOpSendToMesa.DataSource = (new wfiplib.admMesa()).ListaMesasEnviar(pIdMesa, pIdTramite);
                lsOpSendToMesa.DataTextField = "Nombre";
                lsOpSendToMesa.DataValueField = "Id";
                lsOpSendToMesa.DataBind();
            }
            else
            {
                DataTable dtMesas = (new wfiplib.admMesa()).getMesasEnviar(pIdMesa, pIdTramite);
                /*
                foreach (DataRow row in dtMesas.Rows)
                {
                    if (row["Nombre"].ToString() == "REVISIÓN PLAD")
                    {
                        blnDeleteRow = true;
                        drPlad = row;
                    }
                }

                if (blnDeleteRow)
                    dtMesas.Rows.Remove(drPlad);
                */

                lsOpSendToMesa.DataSource = dtMesas;
                lsOpSendToMesa.DataTextField = "Nombre";
                lsOpSendToMesa.DataValueField = "Id";
                lsOpSendToMesa.DataBind();
            }

            if (lsOpSendToMesa.Items.Count == 0)
            {
                pnSendToMesa.Visible = false;
                btnSendToMesa.Visible = false;
            }
            else
            {
                pnSendToMesa.Visible = true;
                btnSendToMesa.Visible = true;
            }
        }

        private void pintaMesasDeApoyo(int pIdFlujo, int pIdMesa, wfiplib.E_TipoTramite IdTipoTramite)
        {
            lsChApoyo.DataSource = (new wfiplib.admMesa()).ListaMesasHijas2(pIdMesa, IdTipoTramite);
            lsChApoyo.DataTextField = "Nombre";
            lsChApoyo.DataValueField = "Id";
            lsChApoyo.DataBind();
        }

        private int daSiguienteTramite(int pIdMesa, wfiplib.E_TipoMesa pTipo)
        {
            int idTramite = 0;
            bool blnMostrarMensaje = false;
            string strMenesaje = "";
            //bool atrapa = false;
            //bool disponibles = false;

            //wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
            //wfiplib.tramiteMesa oTramiteMesa = new wfiplib.tramiteMesa();
            //int idTipoTramite = (new wfiplib.admUsuarioMesa()).DaIdTipoTramite(manejo_sesion.Credencial.Id, pIdMesa);
            try
            {
                wfiplib.admWFOSiguienteTramite WFO = new wfiplib.admWFOSiguienteTramite(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString);
                idTramite = WFO.daSiguienteTramite(manejo_sesion.Credencial, pIdMesa, ref blnMostrarMensaje, ref strMenesaje);

                if (blnMostrarMensaje)
                {
                    mensajes.MostrarMensaje(this, strMenesaje);
                }

                //oTramiteMesa = oAdmTramiteMesa.daAtrapado(pIdMesa, manejo_sesion.Credencial.Id);
                //if (oTramiteMesa.IdTramite > 0)
                //{
                //    idTramite = oTramiteMesa.IdTramite;
                //}
                //else
                //{
                //    oTramiteMesa = oAdmTramiteMesa.daAsignado(pIdMesa, manejo_sesion.Credencial.Id);
                //    if (oTramiteMesa.IdTramite > 0)
                //    {
                //        idTramite = oTramiteMesa.IdTramite;
                //        string strFolioTramite = oAdmTramiteMesa.getFolio(idTramite);
                //        atrapa = oAdmTramiteMesa.atrapa(oTramiteMesa.IdTramite, pIdMesa, manejo_sesion.Credencial.Id);
                //        mensajes.MostrarMensaje(this, "El támite con folio: " + strFolioTramite + " ha sido asignado por el Supervisor.");
                //    }
                //    else
                //    {
                //        do
                //        {
                //            if (idTipoTramite > 0)
                //                oTramiteMesa = oAdmTramiteMesa.daSiguienteXTipoTramite(pIdMesa, idTipoTramite);
                //            else
                //                oTramiteMesa = oAdmTramiteMesa.daSiguiente(pIdMesa, manejo_sesion.Credencial.Id);

                //            if (oTramiteMesa.IdTramite > 0)
                //            {
                //                disponibles = true;
                //                atrapa = oAdmTramiteMesa.atrapa(oTramiteMesa.IdTramite, pIdMesa, manejo_sesion.Credencial.Id);
                //                if (atrapa)
                //                    idTramite = oTramiteMesa.IdTramite;
                //            }
                //        } while (atrapa == false && disponibles == true);

                //        if (disponibles == false && atrapa == false)
                //        {
                //            idTramite = 0;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                // mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
                Console.WriteLine(ex.Message);
            }
            return idTramite;
        }

        private void llenaDatosYBitacora(int pIdTramite, wfiplib.mesa oMesa)
        {
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            ltInfTipoTramite.Text = oTramite.Flujo + "<br />" + oTramite.TramiteNombre;

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
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
                    DataRow row = lstProductos2.Rows[0];
                    if (row["PRODUCTO"].ToString() == "TempoLife")
                    {
                        /* COLOCA VALIDACION DE CITA MEDICA */
                        SubProducto.Text = "TempoLife";
                    }
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    ltInfTipoTramite.Text = oTramite.Flujo.ToUpper() + "<br />";
                    ltInfTipoTramite.Text += "GASTOS MEDICOS MAYORES";
                    wfiplib.EmisionVG oEmisionVida = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                    ltInfContratante.Text = oEmisionVida.DatosHtml;
                    wfiplib.EmisionVG oEmisionVida2 = (new wfiplib.admEmisionVG()).cargaFolio(pIdTramite);
                    ltInfFolio.Text = oEmisionVida2.FolioHtml;
                    DataTable lstProductos = (new wfiplib.admEmisionVG()).cargaProdructos(pIdTramite);
                    rptTramite.DataSource = lstProductos;
                    rptTramite.DataBind();
                    break;

                default:
                    break;
            }

            if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionVida
                || oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionVidaCM
                || oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionGMM)
            {
                if ((new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Procesado")))
                {
                    tdAceptarSeleccion.Visible = false;
                    tdAceptar.Visible = true;
                    if (oMesa.Nombre == "SELECCIÓN")
                    {
                        bool blnProcesable = (new wfiplib.admTramite()).getProcesableTramite(oTramite.Id, oTramite.IdFlujo);
                        if (!blnProcesable)
                        {
                            tdAceptar.Visible = false;
                            tdAceptarSeleccion.Visible = true;
                        }
                        else
                        {
                            tdAceptar.Visible = true;
                            tdAceptarSeleccion.Visible = false;
                        }
                    }
                }
                else
                {
                    tdAceptar.Visible = false;
                    tdAceptarSeleccion.Visible = false;
                }

                tdCitaMedica.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Suspensión Cita Médica"));
                tdCancelacion.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Cancelado"));
                tdNoAplicaMesaCaptura.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("No aplica"));
                tdHold.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Hold"));
                tdSuspender.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Suspendido"));
                tdRechazar.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Rechazo"));
                tdPCI.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("PCI"));
                tdPausa.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Pausa"));
                tdStopAsig.Visible = true;              // TODO: ### Pendiente: Validación desdes permisos
                tdProcesable.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Procesable"));
                tdNoProcesable.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("No Procesable"));
                tdComplementario.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Complementario"));
                tdRevProspecto.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Revisión con Prospecto"));
                tdConPendiente.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Confirmación Pendiente"));
                tdCitaProgramada.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Cita Programada"));
                tdCitaReProgramada.Visible = false; // (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("Cita Reprogramada"));
                tdEsperaResultado.Visible = (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("En espera de resultados"));
                tdSuspencionCM.Visible = false; // (new wfiplib.admTramite()).getPermisoMesaStatus(oTramite.IdFlujo, oTramite.IdTipoTramite, oMesa.Id, (new wfiplib.admTramite()).getIdStatusMesa("En espera de resultados"));

                if (tdSuspencionCM.Visible)
                {
                    if ((new wfiplib.admEmisionVG()).CitaMedicaGenerada(Convert.ToInt32(Request.Params["Id"])))
                    {
                        btnSuspencionCM.Enabled = false;
                    }
                    else 
                    {
                        btnSuspencionCM.Enabled = false;
                    }
                }

                if (oTramite.Estado == wfiplib.E_EstadoTramite.PromotoriaReconsidera && oMesa.Nombre == "CALIDAD")
                {
                    tdRechazar.Visible = true;
                    tdAceptar.Visible = false;
                }

                // NUEVO REQUERIMIENTO MESA DE CALIDAD CAMBIAR BOTON POR NO APLICA
                if (oMesa.Nombre == "CALIDAD")
                {
                    tdAceptar.Visible = false;
                }

                // VALIDA SI EXISTE EL TRAMITE EN LAMESA DE CITAS MEDICAS
                if ((new wfiplib.admTramite()).ExisteTramiteMesaCitasMedicas(pIdTramite))
                {
                    tdCitaMedica.Visible = false;
                }
            }

            btnCancelacion.OnClientClick = "Cancelacion('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.Cancelado.ToString("d") + "'," + wfiplib.E_EstadoMesa.Cancelado.ToString("d") + ");" + " return false;";
            btnAcepatarSeleccion.OnClientClick = "showConfirmTramite('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.Procesado.ToString("d") + "'," + wfiplib.E_EstadoMesa.Procesado.ToString("d") + ");" + " return false;";
            btnCitaMedica.OnClientClick = "CitaMedica('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.SuspensionCitaMedica.ToString("d") + "'," + wfiplib.E_EstadoMesa.SuspensionCitaMedica.ToString("d") + ");" + " return false;";
            btnHold.OnClientClick = "Hold('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.Hold.ToString("d") + "'," + wfiplib.E_EstadoMesa.Hold.ToString("d") + ");" + " return false;";
            btnSuspender.OnClientClick = "Suspender('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.Suspendido.ToString("d") + "'," + wfiplib.E_EstadoMesa.Suspendido.ToString("d") + ");" + " return false;";
            btnRechazar.OnClientClick = "RechazarAsigna('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.Rechazo.ToString("d") + "'," + wfiplib.E_EstadoMesa.Rechazo.ToString("d") + ");" + " return false;";
            btnPCI.OnClientClick = "PCIAsigna('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.PCI.ToString("d") + "'," + wfiplib.E_EstadoMesa.PCI.ToString("d") + ");" + " return false;";
            btnRevProspecto.OnClientClick = "RevProspecto('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.CMRevisionProspecto.ToString("d") + "'," + wfiplib.E_EstadoMesa.CMRevisionProspecto.ToString("d") + ");" + " return false;";
            btnCitaReProgramada.OnClientClick = "CitaReprogramada('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.CMCitaReProgramada.ToString("d") + "'," + wfiplib.E_EstadoMesa.CMCitaReProgramada.ToString("d") + ");" + " return false;";
            btnPausa.OnClientClick = "PausarTramite('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.Pausa.ToString("d") + "'," + wfiplib.E_EstadoMesa.Pausa.ToString("d") + ");" + " return false;";
            btnSeguimientoTramitePopUp.OnClientClick = "querySeguimientoTramite('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.Hold.ToString("d") + "'," + wfiplib.E_EstadoMesa.Hold.ToString("d") + ");" + " return false;";

            List<wfiplib.bitacora> lsBitacora = (new wfiplib.admBitacora()).daLista(pIdTramite);
            foreach (wfiplib.bitacora oBitacora in lsBitacora)
            {
                ltBitacora.Text = ltBitacora.Text + oBitacora.TextoHtml;
                ltBitacoraPrivada.Text = ltBitacoraPrivada.Text + oBitacora.TextoHtmlPrivado;
            }
        }

        private void MuestraSleccionVida(int idTramite, wfiplib.EmisionVG oEmisionVG)
        {
            Nocaptura.Text = "";
            /*
            RamoSelecionVida.Text = "";
            SelecionSubProductos.Text = "";
            FechaVigenciaVida.Text = "";
            Extraprima.Text = "";
            Habito.Text = "";
            Endosos.Text = ""
            */

            DateTime validateFechaSolicitud = DateTime.Today;

            FechaVigenciaVida.MaxDate = validateFechaSolicitud.AddDays(+60);
            FechaVigenciaVida.MinDate = validateFechaSolicitud.AddDays(-60);
            FechaVigenciaVida.UseMaskBehavior = true;
            FechaVigenciaVida.EditFormatString = GetFormatString("dd/MM/yyyy");
            FechaVigenciaVida.Date = DateTime.Today;
            TablaSelccionDatosVida.Visible = false;

            DataTable dtRamos = (new wfiplib.admSeleccion()).RamosVida();
            RamoSelecionVida.DataSource = dtRamos;
            RamoSelecionVida.Items.Add("Seleccione");
            RamoSelecionVida.DataBind();
            RamoSelecionVida.DataTextField = "combo";
            RamoSelecionVida.DataValueField = "IdCatProducto";
            RamoSelecionVida.DataBind();

            DataTable dtExtraprima = (new wfiplib.admSeleccion()).ExtraPrima();
            Extraprima.DataSource = dtExtraprima;
            Extraprima.Items.Add("Seleccione");
            Extraprima.DataBind();
            Extraprima.DataTextField = "extraprima";
            Extraprima.DataValueField = "Id_extraprima";
            Extraprima.DataBind();
            Extraprima.SelectedIndex = 0;

            DataTable dtHabitos = (new wfiplib.admSeleccion()).Habitos();
            Habito.DataSource = dtHabitos;
            Habito.Items.Add("Seleccione");
            Habito.DataBind();
            Habito.DataTextField = "habito";
            Habito.DataValueField = "Id_habito";
            Habito.DataBind();

            DataTable dtEndosos = (new wfiplib.admSeleccion()).Endosos();
            Endosos.DataSource = dtEndosos;
            Endosos.TextField = "Nombre";
            Endosos.ValueField = "Id_endoso";
            Endosos.DataBind();
            Endosos.SelectedIndex = 0;

            RamoSelecionVida.Enabled = true;
            FechaVigenciaVida.Enabled = true;
            //Endosos.Enabled = true;
            SeleccionVida.Enabled = false;
            // VALIDA EL CONTNIDO DEL PRODRUCTO
            
            DataTable lstRiesgos = (new wfiplib.admSeleccion()).CargaRiesgoFactoresVida(idTramite);
            if (lstRiesgos.Rows.Count > 0)
            {
                SeleccionVida.Enabled = false;
                DataRow row = lstRiesgos.Rows[0];

                if (row["Producto"].ToString() == "EDUCALIFE" || row["Producto"].ToString() == "VIDAS CONJUNTAS")
                {
                    SeleccionVida.Enabled = true;
                    // OCULTA ELEMENTOS DEL FORMULARIO, APARTIR DEL PRODUCTO SELECIONADO
                    DataTable TBID = (new wfiplib.admSeleccion()).RamosVidaID(row["Producto"].ToString());
                    DataRow rowid = TBID.Rows[0];

                    //RamoSelecionVida.SelectedValue = rowid["IdCatProducto"].ToString();
                    RamoSelecionVida.SelectedValue = rowid["IdCatProducto"].ToString();
                    DatosRamoVida();

                    //DatosRamoVida();
                    RamoSelecionVida.Enabled = false;
                    FechaVigenciaVida.Text = row["Vigencia"].ToString();
                    FechaVigenciaVida.Enabled = false;
                    //Endosos.SelectedItem.Text = row["Endosos"].ToString();
                    //Endosos.Enabled = false;

                    if (lstRiesgos.Rows[0]["Parentesco"].ToString() == "TODOS LOS SOLICITANTES")
                    {
                        SeleccionVida.Enabled = false;
                    }

                    foreach (DataRow renglon in lstRiesgos.Rows)
                    {
                        if (renglon["Parentesco"].ToString() == "RESTO DE SOLICITANTES")
                        {
                            SeleccionVida.Enabled = false;
                            break;
                        }
                    }
                }

                DataTable dtParentesco = (new wfiplib.admSeleccion()).ParentescoConRegistros();
                CatParentescoGMM.DataSource = dtParentesco;
                CatParentescoGMM.Items.Add("Seleccione");
                CatParentescoGMM.DataBind();
                CatParentescoGMM.DataTextField = "parentesco";
                CatParentescoGMM.DataValueField = "Id_parentesco";
                CatParentescoGMM.DataBind();

                rptRiesgosVida.DataSource = lstRiesgos;
                rptRiesgosVida.DataBind();
                rptRiesgosInfoVida.DataSource = lstRiesgos;
                rptRiesgosInfoVida.DataBind();
                TablaSelccionDatosVida.Visible = true;
                TablaBeneficiarios.Visible = true;
            }
            else
            {
                DataTable lstProducto = (new wfiplib.admSeleccion()).BuscaProductos(idTramite);
                if (lstProducto.Rows.Count > 0)
                {
                    DataRow row = lstProducto.Rows[0];
                    DataTable lstSeleccionProducto = (new wfiplib.admSeleccion()).BuscaProductosSeleccion(row["IdCatProducto"].ToString());
                    if (lstSeleccionProducto.Rows.Count > 0)
                    {
                        RamoSelecionVida.SelectedValue = row["IdCatProducto"].ToString();
                        DatosRamoVida();
                        SelecionSubProductos.SelectedValue = row["IdCatSubProducto"].ToString();
                    }
                }
                DataTable dtParentesco = (new wfiplib.admSeleccion()).Parentesco();
                CatParentescoGMM.DataSource = dtParentesco;
                CatParentescoGMM.Items.Add("Seleccione");
                CatParentescoGMM.DataBind();
                CatParentescoGMM.DataTextField = "parentesco";
                CatParentescoGMM.DataValueField = "Id_parentesco";
                CatParentescoGMM.DataBind();

                FechaVigenciaVida.Date = DateTime.Today;
                SeleccionVida.Enabled = true;
            }
        }

        private void MuestraSleccion(int idTramite, wfiplib.EmisionVG oEmisionVG)
        {
            DateTime validateFechaSolicitud = DateTime.Today;
            PanelZonas.Visible = false;
            PanelPlanes.Visible = false;
            CatFactor.Visible = false;
            CatFactorCaptura.Visible = false;
            TabllaSeleccion.Visible = false;
            TablaBeneficiarios.Visible = false;

            string folio = (new wfiplib.admTramite()).getFolio(idTramite);
            folio = folio.Substring(2, (folio.Length-1) - 1);

            FechaVigencia.MaxDate = validateFechaSolicitud.AddDays(+60);
            FechaVigencia.MinDate = validateFechaSolicitud.AddDays(-60);
            FechaVigencia.UseMaskBehavior = true;
            FechaVigencia.EditFormatString = GetFormatString("dd/MM/yyyy");
            FechaVigencia.Date = DateTime.Today;

            DataTable dtRamos = (new wfiplib.admSeleccion()).Ramos(oEmisionVG.TipoTramite);
            RamoSelecion.DataSource = dtRamos;
            RamoSelecion.Items.Add("Seleccione");
            RamoSelecion.DataBind();
            RamoSelecion.DataTextField = "combo";
            RamoSelecion.DataValueField = "Id_seleccion";
            RamoSelecion.DataBind();

            DataTable dtEndososGM = (new wfiplib.admSeleccion()).EndososGM();
            EndososGM.DataSource = dtEndososGM;
            EndososGM.TextField = "Nombre";
            EndososGM.ValueField = "Id_endosoGM";
            EndososGM.DataBind();
            EndososGM.SelectedIndex = 0;

            RamoSelecion.Enabled = true;
            FechaVigencia.Enabled = true;
            
            //EndososGM.Enabled = true;
            TextID.Enabled = true;
            /*
            TextID.Text = "";
            TexFactor.Text = "";
            FechaVigencia.Text = "";
            */
            CatalogoPlanes.Enabled = true;
            CatalogoZonas.Enabled = true;
            CatalogoFactores.Enabled = true;
            TexFactor.Enabled = true;
            
            Seleccion.Enabled = false;

            DataTable lstProducto = (new wfiplib.admSeleccion()).BuscaProductos(idTramite);
            if (lstProducto.Rows.Count > 0)
            {
                DataRow row = lstProducto.Rows[0];
                DataTable lstSeleccionProducto = (new wfiplib.admSeleccion()).BuscaProductosSeleccion(row["IdCatProducto"].ToString());
                if (lstSeleccionProducto.Rows.Count > 0)
                {
                    DataRow row2 = lstSeleccionProducto.Rows[0];
                    RamoSelecion.SelectedValue = row2["Id_seleccion"].ToString();
                    DatosRamo();

                    DataTable lstSeleccionSubProducto = (new wfiplib.admSeleccion()).BuscaSubProductosSeleccion(row["IdCatSubProducto"].ToString());
                    if (lstSeleccionSubProducto.Rows.Count > 0)
                    {
                        DataRow row3 = lstSeleccionSubProducto.Rows[0];
                        DataTable SeleccionSubProducto = (new wfiplib.admSeleccion()).BuscaSubProductosSeleccion2(row2["Id_seleccion"].ToString(), row3["Nombre"].ToString());
                        if (SeleccionSubProducto.Rows.Count > 0)
                        {
                            DataRow row4 = SeleccionSubProducto.Rows[0];
                            CatalogoPlanes.SelectedValue = row4["Id_CatPlan"].ToString();
                        }
                    }
                }
            }

            DataTable lstRiesgos = (new wfiplib.admSeleccion()).CargaRiesgoFactores(idTramite);
            if (lstRiesgos.Rows.Count > 0)
            {
                Seleccion.Enabled = true;

                DataRow row = lstRiesgos.Rows[0];
                // LLENAR FORMULARIO CON DATOS DEL REGISTRO ANTERIOR 
                DataTable TBID = (new wfiplib.admSeleccion()).RamosGmmID(row["Ramo"].ToString(), oEmisionVG.TipoTramite);
                DataRow rowid = TBID.Rows[0];
                RamoSelecion.SelectedValue = rowid["Id_seleccion"].ToString();
                DatosRamo();
                RamoSelecion.Enabled = false;

                FechaVigencia.Text = row["Vigencia"].ToString();
                FechaVigencia.Enabled = false;
                //EndososGM.SelectedItem.Text = row["Endosos"].ToString();
                //EndososGM.Enabled = false;
                TextID.Text = row["ID"].ToString();
                TextID.Enabled = false;

                DataTable dtPlanes = (new wfiplib.admSeleccion()).BuscaPlan(rowid["Id_seleccion"].ToString(), row["Plan"].ToString());
                if (dtPlanes.Rows.Count > 0)
                {
                    DataRow rowPlan = dtPlanes.Rows[0];
                    CatalogoPlanes.SelectedValue = rowPlan["Id_CatPlan"].ToString();
                    CatalogoPlanes.Enabled = false;
                }

                DataTable dtZonas = (new wfiplib.admSeleccion()).BuscaZona(rowid["Id_seleccion"].ToString(), row["Zona"].ToString());
                if (dtZonas.Rows.Count > 0)
                {
                    DataRow rowZona = dtZonas.Rows[0];

                    CatalogoZonas.SelectedValue = rowZona["Id_CatZona"].ToString();
                    CatalogoZonas.Enabled = false;
                }


                DataTable dtFactor = (new wfiplib.admSeleccion()).Factores(RamoSelecion.SelectedValue.Trim());
                {
                    if (dtFactor.Rows.Count > 0)
                    {
                        //CatFactorCaptura.Visible = true;
                        DataTable dtCatFactor = (new wfiplib.admSeleccion()).BuscaFactor(RamoSelecion.SelectedValue.Trim(), row["Factor"].ToString());
                        if (dtCatFactor.Rows.Count > 0)
                        {
                            DataRow rowFactor = dtCatFactor.Rows[0];
                            CatalogoFactores.SelectedValue = rowFactor["Id_CatFactor"].ToString();
                            CatalogoFactores.Enabled = false;
                        }
                        else
                        {
                            TexFactor.Text = row["Factor"].ToString();
                            TexFactor.Enabled = false;
                        }
                    }
                }
                
                DataTable dtParentesco = (new wfiplib.admSeleccion()).ParentescoConRegistros();
                CatParentesco.DataSource = dtParentesco;
                CatParentesco.Items.Add("Seleccione");
                CatParentesco.DataBind();
                CatParentesco.DataTextField = "parentesco";
                CatParentesco.DataValueField = "Id_parentesco";
                CatParentesco.DataBind();


                rptRiesgos.DataSource = lstRiesgos;
                rptRiesgos.DataBind();
                rptRiesgosInfo.DataSource = lstRiesgos;
                rptRiesgosInfo.DataBind();
                TabllaSeleccion.Visible = true;
                TablaBeneficiarios.Visible = true;

                Seleccion.Enabled = true;
                ReSeleccion.Enabled = true;
                if (lstRiesgos.Rows[0]["Parentesco"].ToString() == "TODOS LOS SOLICITANTES")
                {
                    Seleccion.Enabled = false;
                    ReSeleccion.Enabled = false;
                }

                foreach (DataRow renglon in lstRiesgos.Rows)
                {
                    if (renglon["Parentesco"].ToString() == "RESTO DE SOLICITANTES")
                    {
                        Seleccion.Enabled = false;
                        ReSeleccion.Enabled = false;
                        break;
                    }
                }

            }
            else
            {
                DataTable dtParentesco = (new wfiplib.admSeleccion()).Parentesco();
                CatParentesco.DataSource = dtParentesco;
                CatParentesco.Items.Add("Seleccione");
                CatParentesco.DataBind();
                CatParentesco.DataTextField = "parentesco";
                CatParentesco.DataValueField = "Id_parentesco";
                CatParentesco.DataBind();

                TextID.Text = folio;
                //TextID.Text = "";
                TextID.Enabled = false;
                TexFactor.Text = "";
                FechaVigencia.Date = DateTime.Today;

                Seleccion.Enabled = true;
            }
        }

        private bool AceptarSeleccion()
        {
            bool respuesta = false;

            int idMesa = Convert.ToInt32(Request.QueryString["tp"]);
            int idTramite = Convert.ToInt32(IdTramiteSe.Text.ToString().Trim());

            wfiplib.mesa oMesa = (new wfiplib.admMesa()).carga(idMesa);
            if (oMesa.Nombre == "SELECCIÓN")
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(idTramite);
                switch (oTramite.IdTipoTramite)
                {
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        DataTable lstRiesgos = (new wfiplib.admSeleccion()).CargaRiesgoFactoresVida(idTramite);
                        if (lstRiesgos.Rows.Count > 0)
                        {
                            respuesta = true;
                        }
                        break;
                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        DataTable lstRiesgosGMM = (new wfiplib.admSeleccion()).CargaRiesgoFactores(idTramite);
                        if (lstRiesgosGMM.Rows.Count > 0)
                        {
                            respuesta = true;
                        }
                        break;
                    default:
                        respuesta = true;
                        break;
                }
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        protected void GuardarSeleccion(object sender, EventArgs e)
        {
            wfiplib.Seleccion oDatos = CapturaSeleccion();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.Actualizar(oDatos))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ElimaSelecciones(object sender, CommandEventArgs e)
        {
            string IdDatosSeleccion = e.CommandArgument.ToString();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.InactivoSeleccion(IdDatosSeleccion))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ElimaSeleccionesVida(object sender, CommandEventArgs e)
        {
            string IdDatosSeleccion = e.CommandArgument.ToString();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.InactivoSeleccionVida(IdDatosSeleccion))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ModificacionDatoSeleccionesVida(object sender, CommandEventArgs e)
        {
            string IdDatosSeleccion = e.CommandArgument.ToString();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.ModificacionDatoSeleccionVida(IdDatosSeleccion))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ModificacionDatoSeleccionesGMM(object sender, CommandEventArgs e)
        {
            string IdDatosSeleccion = e.CommandArgument.ToString();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.ModificacionDatoSeleccionGMM(IdDatosSeleccion))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ElimarSeleccion(object sender, EventArgs e)
        {
            /*/wfiplib.Seleccion oDatos = CapturaSeleccion();
            string IdDatosSeleccion = e.ToString();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.InactivoSeleccion(IdDatosSeleccion))
            {
                Response.Redirect(Request.RawUrl);
            }

            /*
            wfiplib.Seleccion oDatos = CapturaSeleccion();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.InactivoSeleccion(oDatos))
            {
                Response.Redirect(Request.RawUrl);
            }
            */
        }

        protected void EditarSeleccionCampos(object sender, EventArgs e)
        {
            Seleccion.Enabled = true;
            GardarSeleccion.Visible = true;
            ElimaSeleccion.Visible = true;
            EditarSeleccion.Visible = false;
        }

        protected void RamoSelecionVida_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatosRamoVida();
        }

        protected void DatosRamoVida()
        {
            DataTable listProductos = (new wfiplib.admEmisionVG()).cartgaCatSupProducto(RamoSelecionVida.SelectedValue.ToString());
            SelecionSubProductos.DataSource = listProductos;
            SelecionSubProductos.Items.Add("Seleccione");
            SelecionSubProductos.DataBind();
            SelecionSubProductos.DataTextField = "Nombre";
            SelecionSubProductos.DataValueField = "IdCatSubProducto";
            SelecionSubProductos.DataBind();
        }

        protected void RamoSelecion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatosRamo();
        }

        protected void DatosRamo()
        {
            CatRiesgo.Visible = false;
            CatRiesgoCaptura.Visible = true;
            TextFactor.Text = "";
            PanelZonas.Visible = false;
            PanelPlanes.Visible = false;
            CatFactor.Visible = false;
            CatFactorCaptura.Visible = false;

            DataTable dtRiesgos = (new wfiplib.admSeleccion()).CargaRiesgos(RamoSelecion.SelectedValue.Trim());
            if (dtRiesgos.Rows.Count > 0)
            {
                DataRow row = dtRiesgos.Rows[0];
                DataTable dtRiesgoFactor = (new wfiplib.admSeleccion()).CargaRiesgoFactor(row["Id_Riesgo"].ToString());
                RiesgoCatalogo.DataSource = dtRiesgoFactor;
                RiesgoCatalogo.DataBind();
                RiesgoCatalogo.DataTextField = "riesgo";
                RiesgoCatalogo.DataValueField = "Id_CatRiesgo";
                RiesgoCatalogo.DataBind();
                CatRiesgo.Visible = true;
                CatRiesgoCaptura.Visible = false;
            }

            DataTable dtZonas = (new wfiplib.admSeleccion()).CargaZonas(RamoSelecion.SelectedValue.Trim());
            if (dtZonas.Rows.Count > 1)
            {
                CatalogoZonas.DataSource = dtZonas;
                CatalogoZonas.DataBind();
                CatalogoZonas.DataTextField = "dato";
                CatalogoZonas.DataValueField = "Id_CatZona";
                CatalogoZonas.DataBind();
                PanelZonas.Visible = true;
            }

            DataTable dtPlanes = (new wfiplib.admSeleccion()).CargaPlanes(RamoSelecion.SelectedValue.Trim());
            if (dtPlanes.Rows.Count > 1)
            {
                CatalogoPlanes.DataSource = dtPlanes;
                CatalogoPlanes.DataBind();
                CatalogoPlanes.DataTextField = "dato";
                CatalogoPlanes.DataValueField = "Id_CatPlan";
                CatalogoPlanes.DataBind();
                PanelPlanes.Visible = true;
            }

            DataTable dtFactor = (new wfiplib.admSeleccion()).Factores(RamoSelecion.SelectedValue.Trim());
            {
                if (dtFactor.Rows.Count > 0)
                {
                    CatFactorCaptura.Visible = true;
                    DataTable dtCatFactor = (new wfiplib.admSeleccion()).CargaFactores(RamoSelecion.SelectedValue.Trim());
                    if (dtCatFactor.Rows.Count > 1)
                    {
                        CatalogoFactores.DataSource = dtCatFactor;
                        CatalogoFactores.DataBind();
                        CatalogoFactores.DataTextField = "dato";
                        CatalogoFactores.DataValueField = "Id_CatFactor";
                        CatalogoFactores.DataBind();

                        CatFactor.Visible = true;
                        CatFactorCaptura.Visible = false;
                    }
                }
            }
        }

        protected void RegistrarSeleccion(object sender, EventArgs e)
        {
            wfiplib.Seleccion oDatos = CapturaSeleccion();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.nuevo(oDatos))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void RegistrarSeleccionVida(object sender, EventArgs e)
        {
            wfiplib.Seleccion oDatos = CapturaSeleccionVida();
            wfiplib.admSeleccion oAdmSeleccion = new wfiplib.admSeleccion();
            if (oAdmSeleccion.NuevoVida(oDatos))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        private wfiplib.Seleccion CapturaSeleccionVida()
        {
            wfiplib.Seleccion resultado = new wfiplib.Seleccion();
            try
            {
                resultado.IdTramite = Convert.ToInt32(IdTramite.Text.Trim());
                resultado.Producto = RamoSelecionVida.SelectedItem.Text.Trim();
                resultado.SubProducto = SelecionSubProductos.SelectedItem.Text.Trim();
                resultado.NumCaptura = Nocaptura.Text.Trim();
                resultado.Vigencia = FechaVigenciaVida.Text.Trim();
                resultado.ExtraPrima = Extraprima.SelectedItem.Text.Trim();
                resultado.Habito = Habito.SelectedItem.Text.Trim();
                resultado.Endosos = Endosos.SelectedItem.Text.Trim();
                resultado.Parentesco = CatParentescoGMM.SelectedItem.Text.Trim();
                resultado.Ocupacion = TextOcupacion.Text.Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        private wfiplib.Seleccion CapturaSeleccion()
        {
            wfiplib.Seleccion resultado = new wfiplib.Seleccion();
            try
            {
                resultado.IdTramite = Convert.ToInt32(IdTramite.Text.Trim());
                resultado.Ramo = RamoSelecion.SelectedItem.Text.Trim();
                resultado.ID = TextID.Text.Trim();
                resultado.FechaVigencia = FechaVigencia.Text.Trim();
                resultado.Parentesco = CatParentesco.SelectedItem.Text.Trim();
                resultado.Endosos = EndososGM.SelectedItem.Text.Trim();
                resultado.Ocupacion = TextBoxOcupacionGM.Text.Trim();

                if (RiesgoCatalogo.SelectedValue.Trim() == "-1" || RiesgoCatalogo.SelectedValue.Trim() == "" || RiesgoCatalogo.SelectedValue.Trim() == null)
                {
                    resultado.Riesgo = TextRiesgo.Text.Trim();
                    resultado.RiesgoFactor = TextRiesgoFactor.Text.Trim();
                }
                else
                {
                    resultado.Riesgo = RiesgoCatalogo.SelectedItem.Text.Trim();
                    resultado.RiesgoFactor = TextFactor.Text.Trim();
                }

                if (CatalogoZonas.SelectedValue.Trim() == "-1" || CatalogoZonas.SelectedValue.Trim() == "" || CatalogoZonas.SelectedValue.Trim() == null)
                    resultado.Zona = "";
                else
                    resultado.Zona = CatalogoZonas.SelectedItem.Text.Trim();

                if (CatalogoPlanes.SelectedValue.Trim() == "-1" || CatalogoPlanes.SelectedValue.Trim() == "" || CatalogoPlanes.SelectedValue.Trim() == null)
                    resultado.Plan = "";
                else
                    resultado.Plan = CatalogoPlanes.SelectedItem.Text.Trim();

                if (CatalogoFactores.SelectedValue.Trim() == "-1" || CatalogoFactores.SelectedValue.Trim() == "" || CatalogoFactores.SelectedValue.Trim() == null)
                    resultado.Factor = TexFactor.Text.Trim();
                else
                    resultado.Factor = CatalogoFactores.SelectedItem.Text.Trim();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        protected void RiesgoCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RiesgosCatalogo();
        }

        private void RiesgosCatalogo()
        {
            TextFactor.Text = "";
            DataTable dtRiesgoFactor = (new wfiplib.admSeleccion()).CargaFactor(RiesgoCatalogo.SelectedValue.Trim());
            if (dtRiesgoFactor.Rows.Count > 0)
            {
                DataRow row = dtRiesgoFactor.Rows[0];
                TextFactor.Text = row["factor"].ToString();
            }
        }

        private void MuetraPLAD(int idTramite, wfiplib.EmisionVG oEmisionVG)
        {
            /***********************************************************/
            /*********************** Datos PLAD ************************/
            /***********************************************************/
            TextFechaIdentificacion.UseMaskBehavior = true;
            TextFechaIdentificacion.EditFormatString = GetFormatString("dd/MM/yyyy");
            TextFechaIdentificacion.Date = DateTime.Today;
            TextRazonSocial.Text = oEmisionVG.Nombre.ToString();
            TextRFC.Text = oEmisionVG.RFC.ToString();

            wfiplib.PLAD oPLAD = (new wfiplib.admPLAD()).carga(idTramite);
            if (oPLAD != null)
            {
                //MensajesPLAD.Text = "CON DATOS PLAD";
                TextFechaIdentificacion.Text = oPLAD.FechaIdentificacion.ToString();
                TextNombreRepresentante.Text = oPLAD.NombreRepreentante.ToString();
                TextFolioMercantil.Text = oPLAD.FolioMercantil.ToString();
                TextGiroMercantil.Text = oPLAD.GiroMercantil.ToString();
                TextVigencia.Text = oPLAD.VigenciaComprovante.ToString();
                TextDuracionSociedad.Text = oPLAD.DuracionSociedad.ToString();
                PLAD.Enabled = false;
                EditarPLAD.Visible = true;
                EditarPLAD.Enabled = true;
                GuardaPLAD.Visible = false;
            }
            else
            {
                if (wfiplib.E_TipoPersona.Moral == oEmisionVG.TipoPersona)
                {
                    MensajesPLAD.Text = "REQUIERE DATOS PLAD";
                    GuardaPLAD.Visible = true;
                }
                else
                {
                    DatosPlad.Visible = false;
                    MensajesPLAD.Text = "NO REQUIERE DATOS PLAD";

                    TextFechaIdentificacion.Text = "";
                    TextNombreRepresentante.Text = "";
                    TextFolioMercantil.Text = "";
                    TextGiroMercantil.Text = "";
                    TextVigencia.Text = "";
                    TextDuracionSociedad.Text = "";
                    PLAD.Enabled = true;
                    EditarPLAD.Visible = false;
                    EditarPLAD.Enabled = false;
                    GuardaPLAD.Visible = true;

                    TextFechaIdentificacion.UseMaskBehavior = true;
                    TextFechaIdentificacion.EditFormatString = GetFormatString("dd/MM/yyyy");
                    TextFechaIdentificacion.Date = DateTime.Today;
                }
            }

        }

        protected void Eliminar_PLAD(object sender, EventArgs e)
        {
            wfiplib.PLAD oDatos = CapturaPlad();
            wfiplib.admPLAD oAdmPlad = new wfiplib.admPLAD();
            if (oAdmPlad.InactivoPlad(oDatos))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void Editar_PLAD(object sender, EventArgs e)
        {
            PLAD.Enabled = true;
            EditarPLAD.Visible = false;
            ActualizaPLAD.Visible = true;
            EliminarPLAD.Visible = true;
        }

        protected void Actualiza_PLAD(object sender, EventArgs e)
        {
            wfiplib.PLAD oDatos = CapturaPlad();
            wfiplib.admPLAD oAdmPlad = new wfiplib.admPLAD();
            if (oAdmPlad.Actualizar(oDatos))
                Response.Redirect(Request.RawUrl);
        }

        protected void Guardar_PLAD(object sender, EventArgs e)
        {
            wfiplib.PLAD oDatos = CapturaPlad();
            wfiplib.admPLAD oAdmPlad = new wfiplib.admPLAD();
            if (oAdmPlad.nuevo(oDatos))
                Response.Redirect(Request.RawUrl);
        }

        private wfiplib.PLAD CapturaPlad()
        {
            wfiplib.PLAD resultado = new wfiplib.PLAD();
            try
            {
                resultado.IdTramite = Convert.ToInt32(IdTramite.Text.Trim());
                resultado.FechaIdentificacion = TextFechaIdentificacion.Text.ToString();
                resultado.NombreRepreentante = TextNombreRepresentante.Text.ToString();
                resultado.FolioMercantil = TextFolioMercantil.Text.ToString();
                resultado.GiroMercantil = TextGiroMercantil.Text.ToString();
                resultado.VigenciaComprovante = TextVigencia.Text.ToString();
                resultado.DuracionSociedad = TextDuracionSociedad.Text.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        private void LimpiarInformacionTramite()
        {
            try
            {
                //RamoSelecionVida.Text = "";
                //SelecionSubProductos.Text = "";
                FechaVigenciaVida.Text = "";
                ContenidoSeleccion.Text = "";
                //Extraprima.Text = "";
                //Habito.Text = "";
                Endosos.Text = "";
                TextOcupacion.Text = "";
                DateTime validateFechaSolicitud = DateTime.Today;
                DatosPromotoriaLimpiar();
                cboMoneda.SelectedValue = null;
                IdTramite.Text = "";
                txtSumaAseguradaBasica.Text = "";
                dtFechaSolicitud.Text = "";
                InfoClave.Text = "";
                InfoRegion.Text = "";
                InfoGerente.Text = "";
                InfoEjecutivo.Text = "";
                InfoEjecutivoFront.Text = "";
                texNumeroOrden.Text = "";
                InfoNumero.Text = "";
                dtFechaSolicitud.MaxDate = validateFechaSolicitud;
                dtFechaSolicitud.MinDate = validateFechaSolicitud.AddDays(-30);
                dtFechaSolicitud.UseMaskBehavior = true;
                dtFechaSolicitud.Date = DateTime.Today;
                dtFechaConstitucion.MaxDate = DateTime.Today;
                dtFechaConstitucion.UseMaskBehavior = true;
                dtFechaNacimiento.MaxDate = validateFechaSolicitud.AddYears(-18);
                dtFechaNacimiento.UseMaskBehavior = true;
                dtFechaNacimientoTitular.UseMaskBehavior = true;
                dtFechaConstitucion.Date = DateTime.Today;
                dtFechaNacimiento.Date = DateTime.Today.AddYears(-18);
                dtFechaNacimientoTitular.Date = DateTime.Today;
                SumaAseguradaPolizasVigentes.Visible = false;
                SumaAseguradaPolizasVigentesGMM.Visible = false;
                CantidadesVida.Visible = false;
                CantidadesGastosMedicos.Visible = false;
                InfoMoneda.Text = cboMoneda.SelectedItem.ToString();
                InfoSumaAseguradaBasica.Text = "";
                InfoSumaAseguradaPolizasVigentes.Text = "";
                InfoPrimaTotal.Text = "";
                txtSumaAseguradaPolizasVigentes.Text = "";
                txtPrimaTotal.Text = "";
                TextFecha1.Date = DateTime.Today;
                TextFecha1.TimeSectionProperties.Visible = false;
                TextFecha1.UseMaskBehavior = false;
                TextFecha2.Date = DateTime.Today;
                TextFecha2.TimeSectionProperties.Visible = false;
                TextFecha2.UseMaskBehavior = false;
                TextFecha3.TimeSectionProperties.Visible = false;
                TextFecha3.UseMaskBehavior = false;
                TextFecha4.TimeSectionProperties.Visible = false;
                TextFecha4.UseMaskBehavior = false;
                InfCPDES.Visible = false;
                InfCPDES.Visible = false;
                lblFolioCPDES.Text = "";
                EstatusCPDES.SelectedValue = "";
                TramiteInformacionCPDES.Visible = false;
                InfoFolioCPDES.Text = "";
                InfoEstatusCPDES.Text = "";
                GrandeSumas.Text = "";
                InfoGrandeSumas.Text = "";
                ProgramarCita.Visible = false;
                texEdad.Text = "";
                texCombo.Text = "";
                texCel.Text = "";
                texCelAg.Text = "";
                texCorreo.Text = "";
                LisEstado.SelectedValue = null;
                LisCiudad.SelectedValue = null;
                LisLabHospital.SelectedValue = null;
                TextFecha1.Text = "";
                TextFecha2.Text = "";
                TextFecha3.Text = "";
                TextFecha4.Text = "";
                Radio1.Checked = false;
                Radio2.Checked = false;
                Radio3.Checked = false;
                Radio4.Checked = false;
                TextFechaRecepcion.Text = "";
                CitaMedica.Visible = false;
                ProgramarCita.Visible = false;
                CancelarCita.Visible = false;
                SumaAseguradaPolizasVigentes.Visible = false;
                SumaAseguradaPolizasVigentesGMM.Visible = false;
                GrandeSumas.Visible = false;
                CantidadesVida.Visible = false;
                CantidadesGastosMedicos.Visible = true;
                InfoMonedaGM.Text = "";
                InfoSumaAseguradaBasicaGM.Text = "";
                InfoPrimaTotalGM.Text = "";
                txtPrimaTotalGMM.Text = "";
                CheckHombreClave.Checked = false;
                InfoHobreClave.Text = "";
                dtFechaSolicitud.Text = "";
                InfoFechaSolicitud.Text = "";
                FechaRegistro.Text = "";
                InfoFechaRegistro.Text = "";
                cboTipoContratante.SelectedValue = null;
                InfoContratante.Text = "";
                txNombre.Text = "";
                txApPat.Text = "";
                txApMat.Text = "";
                //txNacionalidad.SelectedItem.Text = "";
                dtFechaNacimiento.Text = "";
                txSexo.SelectedValue = "";
                txRfc.Text = "";
                InfoFNombre.Text = "";
                InfoFApellidoP.Text = "";
                InfoFApellidoM.Text = "";
                InfoFSexo.Text = "";
                InfoFRFC.Text = "";
                InfoFNacionalidad.Text = "";
                InfoFFechaNa.Text = "";
                txNomMoral.Text = "";
                dtFechaConstitucion.Text = "";
                txRfcMoral.Text = "";
                //txTiNacionalidad.SelectedItem.Text = "";
                InfoMNombre.Text = "";
                InfoMFechaConsti.Text = "";
                InfoMRFC.Text = "";
                CheckBox1.Checked = false;
                CheckBox2.Checked = false;
                DiferenteContratante.Visible = false;
                txTiNombre.Text = "";
                txTiApPat.Text = "";
                txTiApMat.Text = "";
                //txTiNacionalidad.SelectedItem.Text = "";
                txtSexoM.SelectedValue = "";
                dtFechaNacimientoTitular.Text = "";
                InfoDiContratante.Visible = false;
                InfoFContratante.Text = "";
                InfoTNombre.Text = "";
                InfoTApellidoP.Text = "";
                InfoTApellidoM.Text = "";
                InfoTNacionalidad.Text = "";
                InfoTSexo.Text = "";
                InfoTNacimiento.Text = "";
                TextNumPolizaSisLegado.Text = "";
                TextNumKwik.Text = "";
                lblAdvertencia.Visible = false;
                lblAdvertencia.Text = "";
                lblStatusMesas.Text = "";
                TextantecedentesRFC.Text = "";
                textRFCFisica.Text = "";
                pnPrsFisica.Visible = false;
                pnPrsMoral.Visible = false;
                InfoPrsFisica.Visible = false;
                InfoPrsMoral.Visible = false;
                CheckBox1.Enabled = false;
                CheckBox2.Enabled = false;
                CheckBoxExcel.Checked = false;
                LabelCapturaExcel.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Realiza la carga de información del Trámite
        /// </summary>
        /// <param name="pIdTramite">Id del Trámite.</param>
        /// <param name="oEmisionGmm">Infomación del Trámite para mostrar</param>
        private void CargarInformacionTramite(int pIdTramite, wfiplib.EmisionVG oEmisionGmm, E_TipoTramite idTipoTramite)
        {
            LimpiarInformacionTramite();
            string strNacionalidad = "";

            cboMoneda.SelectedValue = oEmisionGmm.IdMoneda.ToString();

            IdTramite.Text = pIdTramite.ToString();

            /* TODOS LOS TRAMITES TIENE SUMA ASEGURADA BASICA */
            txtSumaAseguradaBasica.Text = oEmisionGmm.SumaAsegurada.ToString();


            // txtPrimaTotal.Text = oEmisionGmm.SumaPolizas.ToString();


            dtFechaSolicitud.Text = oEmisionGmm.FechaSolicitud.ToString();

            DatosPromotoria(oEmisionGmm.IdPromotoria.ToString());
            InfoClave.Text = texClave.Text.ToString();
            InfoRegion.Text = texRegion.Text.ToString();
            InfoGerente.Text = texGerente.Text.ToString();
            InfoEjecutivo.Text = texEjecutivo.Text.ToString();
            InfoEjecutivoFront.Text = texEjecutivoFront.Text.ToString();

            texNumeroOrden.Text = oEmisionGmm.NumeroOrden.ToString();
            InfoNumero.Text = oEmisionGmm.NumeroOrden.ToString();

            //INICIO DE FECHAS, FECHA VALIDA A PARTIR DEL DÍA EN CURSO  --  Inicio de datos y validación de fechas 
            DateTime validateFechaSolicitud = DateTime.Today;

            dtFechaSolicitud.MaxDate = validateFechaSolicitud;
            dtFechaSolicitud.MinDate = validateFechaSolicitud.AddDays(-30);
            dtFechaSolicitud.UseMaskBehavior = true;
            dtFechaSolicitud.EditFormatString = GetFormatString("dd/MM/yyyy");
            dtFechaSolicitud.Date = DateTime.Today;

            dtFechaConstitucion.MaxDate = DateTime.Today;
            dtFechaConstitucion.UseMaskBehavior = true;
            dtFechaConstitucion.EditFormatString = GetFormatString("dd/MM/yyyy");

            dtFechaNacimiento.MaxDate = validateFechaSolicitud.AddYears(-18);
            dtFechaNacimiento.UseMaskBehavior = true;
            dtFechaNacimiento.EditFormatString = GetFormatString("dd/MM/yyyy");

            //dtFechaNacimientoTitular.MaxDate = validateFechaSolicitud.AddYears(-18);
            dtFechaNacimientoTitular.UseMaskBehavior = true;
            dtFechaNacimientoTitular.EditFormatString = GetFormatString("dd/MM/yyyy");

            dtFechaConstitucion.Date = DateTime.Today;
            dtFechaNacimiento.Date = DateTime.Today.AddYears(-18);
            dtFechaNacimientoTitular.Date = DateTime.Today;

            /* APARTIR DEL TIPO DE TRAMITE MOESTRARA LAS CANTIDADES APLIDAS*/
            switch (idTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    SumaAseguradaPolizasVigentes.Visible = true;
                    SumaAseguradaPolizasVigentesGMM.Visible = false;
                    // CANTIDADES POR TABLE DE INFORMACION

                    CantidadesVida.Visible = true;
                    CantidadesGastosMedicos.Visible = false;
                    InfoMoneda.Text = cboMoneda.SelectedItem.ToString();
                    InfoSumaAseguradaBasica.Text = oEmisionGmm.SumaAsegurada.ToString();
                    InfoSumaAseguradaPolizasVigentes.Text = oEmisionGmm.SumaPolizas.ToString();
                    InfoPrimaTotal.Text = oEmisionGmm.PrimaTotal.ToString();


                    txtSumaAseguradaPolizasVigentes.Text = oEmisionGmm.SumaPolizas.ToString();
                    txtPrimaTotal.Text = oEmisionGmm.PrimaTotal.ToString();

                    TextFecha1.Date = DateTime.Today;
                    TextFecha1.TimeSectionProperties.Visible = true;
                    TextFecha1.UseMaskBehavior = true;
                    TextFecha1.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
                    TextFecha1.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

                    TextFecha2.Date = DateTime.Today;
                    TextFecha2.TimeSectionProperties.Visible = true;
                    TextFecha2.UseMaskBehavior = true;
                    TextFecha2.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
                    TextFecha2.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

                    TextFecha3.TimeSectionProperties.Visible = true;
                    TextFecha3.UseMaskBehavior = true;
                    TextFecha3.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
                    TextFecha3.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

                    TextFecha4.TimeSectionProperties.Visible = true;
                    TextFecha4.UseMaskBehavior = true;
                    TextFecha4.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
                    TextFecha4.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

                    InfCPDES.Visible = false;
                    if (oEmisionGmm.CPDES)
                    {
                        InfCPDES.Visible = true;
                        lblFolioCPDES.Text = oEmisionGmm.FolioCPDES.ToString();
                        EstatusCPDES.SelectedValue = oEmisionGmm.EstatusCPDES;

                        TramiteInformacionCPDES.Visible = true;
                        InfoFolioCPDES.Text = oEmisionGmm.FolioCPDES.ToString();
                        InfoEstatusCPDES.Text = oEmisionGmm.EstatusCPDES;
                    }
                    if (GrandesSumas())
                    {
                        GrandeSumas.Text = "GRANDES SUMAS";
                        InfoGrandeSumas.Text = "GRANDES SUMAS";
                    }
                    else if (GrandesSumasPrimaTotal())
                    {
                        PrimaTotalGrandeSumas.Text = "GRANDES SUMAS";
                        InfoGrandeSumas.Text = "GRANDES SUMAS";
                    }


                    ProgramarCita.Visible = true;

                    wfiplib.EmisionVG DatosCitaMedica = (new wfiplib.admEmisionVG()).cargaCitaMedica(pIdTramite);
                    if (DatosCitaMedica != null)
                    {
                        //CitasMedicasEvalucacion();

                        texEdad.Text = DatosCitaMedica.Edad.ToString();
                        if (texEdad.Text == "0")
                        {
                            texEdad.Text = (new wfiplib.admEmisionVG()).getEdad(pIdTramite).ToString();
                        }

                        //texCombo.Text = DatosCitaMedica.Combo.ToString();

                        listCombos();
                        listCombosCitaMed.SelectedValue = DatosCitaMedica.Combo.ToString();
                        texCel.Text = DatosCitaMedica.Cel.ToString();
                        texCelAg.Text = DatosCitaMedica.CelAgentePromotor.ToString();
                        texCorreo.Text = DatosCitaMedica.Correo.ToString();
                        listEstados();
                        LisEstado.SelectedValue = DatosCitaMedica.Estado.ToString();
                        listCiudad();
                        LisCiudad.SelectedValue = DatosCitaMedica.Ciudad.ToString();
                        lisLabHospital();
                        LisLabHospital.SelectedValue = DatosCitaMedica.LaboratorioHospital.ToString();
                        cargaDireccion();

                        TextFecha1.Text = DatosCitaMedica.Fecha1.ToString();
                        TextFecha2.Text = DatosCitaMedica.Fecha2.ToString();
                        TextFecha3.Text = DatosCitaMedica.Fecha3.ToString();
                        TextFecha4.Text = DatosCitaMedica.Fecha4.ToString();

                        FechaSelecion(DatosCitaMedica.FechaSeleccionada);
                        TextFechaRecepcion.Text = DatosCitaMedica.FechaRecepcion.ToString();

                        CitaMedica.Visible = true;
                        ProgramarCita.Visible = false;
                        CancelarCita.Visible = true;
                    }

                    break;
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    SumaAseguradaPolizasVigentes.Visible = false;
                    SumaAseguradaPolizasVigentesGMM.Visible = true;
                    GrandeSumas.Visible = false;
                    CantidadesVida.Visible = false;
                    CantidadesGastosMedicos.Visible = true;

                    InfoMonedaGM.Text = cboMoneda.SelectedItem.ToString();
                    InfoSumaAseguradaBasicaGM.Text = oEmisionGmm.SumaAsegurada.ToString();
                    InfoPrimaTotalGM.Text = oEmisionGmm.PrimaTotal.ToString();

                    txtPrimaTotalGMM.Text = oEmisionGmm.PrimaTotal.ToString();
                    break;

                default:
                    break;
            }

            if (oEmisionGmm.HombreClave)
            {
                CheckHombreClave.Checked = true;
                InfoHobreClave.Text = "SI";
            }
            else
            {
                CheckHombreClave.Checked = false;
                InfoHobreClave.Text = "NO";
            }

            dtFechaSolicitud.Text = oEmisionGmm.FechaSolicitud.ToString();
            InfoFechaSolicitud.Text = oEmisionGmm.FechaSolicitud.ToString();

            FechaRegistro.Text = oEmisionGmm.FechaRegistro.ToString();
            InfoFechaRegistro.Text = oEmisionGmm.FechaRegistro.ToString();

            cboTipoContratante.SelectedValue = oEmisionGmm.TipoPersona.ToString();
            InfoContratante.Text = cboTipoContratante.SelectedItem.ToString();

            TipoContratante();

            /* CARGA EL CATALOGO DE PRODUCTOS */
            switch (idTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    ListaProductos("2");
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    ListaProductos("3");
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    ListaProductos("1");
                    break;

                default:
                    break;
            }

            DataTable lstProductos2 = (new wfiplib.admEmisionVG()).cargaProdructos(pIdTramite);
            foreach (DataRow row in lstProductos2.Rows)
            {
                LisProducto1.SelectedValue = row["IdCatProducto"].ToString();
                LisSbproductos();
                LisSubproducto1.SelectedValue = row["IdCatSubProducto"].ToString();
            }

            

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                txNombre.Text = oEmisionGmm.Nombre.ToString();
                txApPat.Text = oEmisionGmm.ApPaterno.ToString();
                txApMat.Text = oEmisionGmm.ApMaterno.ToString();
                // txNacionalidad.SelectedItem.Text = oEmisionGmm.Nacionalidad.Trim().ToString();
                txNacionalidad.Value = oEmisionGmm.Nacionalidad.Trim().ToString();
                dtFechaNacimiento.Text = oEmisionGmm.FechaNacimiento.ToString();
                txSexo.SelectedValue = oEmisionGmm.Sexo.ToString();
                txRfc.Text = oEmisionGmm.RFC.ToString();
                cboEstado.SelectedValue = oEmisionGmm.EntidadFederativa.ToString();

                InfoFNombre.Text = oEmisionGmm.Nombre.ToString();
                InfoFApellidoP.Text = oEmisionGmm.ApPaterno.ToString();
                InfoFApellidoM.Text = oEmisionGmm.ApMaterno.ToString();
                InfoFSexo.Text = oEmisionGmm.Sexo.ToString();
                InfoFRFC.Text = oEmisionGmm.RFC.ToString();
                InfoFNacionalidad.Text = oEmisionGmm.Nacionalidad.Trim().ToString();
                InfoFFechaNa.Text = oEmisionGmm.FechaNacimiento.ToString();
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                txNomMoral.Text = oEmisionGmm.Nombre.ToString();
                dtFechaConstitucion.Text = oEmisionGmm.FechaConst.ToString();
                txRfcMoral.Text = oEmisionGmm.RFC.ToString();
                // txTiNacionalidad.SelectedItem.Text = oEmisionGmm.Nacionalidad.Trim().ToString();
                txTiNacionalidad.Value = oEmisionGmm.Nacionalidad.Trim().ToString();
                InfoMNombre.Text = oEmisionGmm.Nombre.ToString();
                InfoMFechaConsti.Text = oEmisionGmm.FechaConst.ToString();
                InfoMRFC.Text = oEmisionGmm.RFC.ToString();
            }

            if (oEmisionGmm.Excel.ToString() == "True")
            {
                CheckBoxExcel.Checked = true;
                LabelCapturaExcel.Visible = true;
            }
            
            /************************************************************/

            if (oEmisionGmm.TitularNombre.ToString() != "")
            {
                CheckBox1.Checked = true;

                CheckB1();

                txTiNombre.Text = oEmisionGmm.TitularNombre.ToString();
                txTiApPat.Text = oEmisionGmm.TitularApPat.ToString();
                txTiApMat.Text = oEmisionGmm.TitularApMat.ToString();
                // txTiNacionalidad.SelectedItem.Text = oEmisionGmm.TitularNacionalidad.Trim().ToString();
                txTiNacionalidad.Value = oEmisionGmm.TitularNacionalidad.Trim().ToString();
                txtSexoM.SelectedValue = oEmisionGmm.TitularSexo.ToString();
                dtFechaNacimientoTitular.Text = oEmisionGmm.TitularFechaNacimiento.ToString();
                cboEstado2.SelectedValue = oEmisionGmm.TitularEntidad.ToString();

                InfoDiContratante.Visible = true;
                InfoFContratante.Text = "No";
                InfoTNombre.Text = oEmisionGmm.TitularNombre.ToString();
                InfoTApellidoP.Text = oEmisionGmm.TitularApPat.ToString();
                InfoTApellidoM.Text = oEmisionGmm.TitularApMat.ToString();
                InfoTNacionalidad.Text = oEmisionGmm.TitularNacionalidad.Trim().ToString();
                InfoTSexo.Text = oEmisionGmm.TitularSexo.ToString();
                InfoTNacimiento.Text = oEmisionGmm.TitularFechaNacimiento.ToString();
            }
            else
            {
                /* mostra la informacion la funcion limpia formulario no limpia esta parte del formulario*/
                CheckBox2.Checked = true;
                CheckB2();

                InfoDiContratante.Visible = false;
                InfoFContratante.Text = "Si";

                txTiNombre.Text = "";
                txTiApPat.Text = "";
                txTiApMat.Text = "";
                // txTiNacionalidad.SelectedItem.Text = oEmisionGmm.TitularNacionalidad.Trim().ToString();
                txTiNacionalidad.Value = "";
                txtSexoM.SelectedValue = "";
                dtFechaNacimientoTitular.Text = "";
                cboEstado2.SelectedIndex = 0;
            }

            TextNumPolizaSisLegado.Text = oEmisionGmm.IdSisLegados.ToString();
            TextNumKwik.Text = oEmisionGmm.kwik.ToString();
            antecedentesRFC();

            //TODO: Revisar automaticamente la tabla de Paises para verificar Paises Sancionados
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

            lblStatusMesas.Text = "<br/>" +
                "<table>" +
                     getInfoMesasStatus(pIdTramite) +
                "</table>";
        }

        /*******************************************************************/
        /*******************************************************************/
        /*******************************************************************/
        protected void Continuar()
        {
            wfiplib.EmisionVG oDatos = recuperaCaptura();
            wfiplib.admTramite oAdmTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG oAdmEmisionVG = new wfiplib.admEmisionVG();
            if (oAdmTramite.ActualiaOrdenFecha(oDatos))
            {
                if (oAdmEmisionVG.AlteraTRAM00003P(oDatos))
                {
                    if (oDatos.Combo != "" && oDatos.Combo != null)
                    {
                        wfiplib.EmisionVG DatosCitaMedica = (new wfiplib.admEmisionVG()).cargaCitaMedica(oDatos.IdTramite);
                        if (DatosCitaMedica != null)
                        {
                            // DESACTIVA TODAS LAS CITAS MEDICAS REGISTRADAS AL TRAMITE
                            oAdmEmisionVG.AlteraCitaMedicaDesactiva(oDatos);
                            // ACTIVA LA CITA MEDICA A ALTERAR PERTENECIENTE AL TRAMITE, SOLO TOMARA EL ULTIMO REGISTRO
                            if (oAdmEmisionVG.AlteraCitaMedica(oDatos))
                            {
                                Response.Redirect(Request.RawUrl);
                            }
                        }
                        else
                        {
                            if (oAdmEmisionVG.NuevaCitaMedica(oDatos))
                            {
                                Response.Redirect(Request.RawUrl);
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect(Request.RawUrl);
                    }
                }
                else
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        private wfiplib.EmisionVG recuperaCaptura()
        {
            // SE CREO LA CLASE serviciosVidaP para actualizar los nuevos datos
            wfiplib.EmisionVG resultado = new wfiplib.EmisionVG();
            try
            {
                resultado.IdTramite = Convert.ToInt32(IdTramite.Text.Trim());

                resultado.IdMoneda = cboMoneda.Text.Trim();
                resultado.SumaAsegurada = txtSumaAseguradaBasica.Text.Trim();
                //resultado.SumaPolizas = txtPrimaTotal.Text.ToString();

                // DATO DE TABLA TRAMITE
                resultado.NumeroOrden = texNumeroOrden.Text.Trim().ToUpper();
                resultado.FechaSolicitud = dtFechaSolicitud.Text;

                resultado.Producto = LisProducto1.SelectedValue.ToString();
                resultado.Plan1 = LisSubproducto1.SelectedValue.ToString();

                int idTramite = Convert.ToInt32(IdTramite.Text.Trim());
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(idTramite);

                switch (oTramite.IdTipoTramite)
                {
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        resultado.SumaPolizas = txtSumaAseguradaPolizasVigentes.Text.ToString();
                        resultado.PrimaTotal = txtPrimaTotal.Text.ToString();
                        break;

                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        resultado.PrimaTotal = txtPrimaTotalGMM.Text.ToString();
                        break;

                    default:
                        break;
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
                    resultado.EntidadFederativa = cboEstado.SelectedValue;
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
                    resultado.TitularEntidad = cboEstado2.SelectedValue;
                }

                if (CheckBoxExcel.Checked.Equals(true))
                {
                    resultado.Excel = "1";
                }


                resultado.IdSisLegados = TextNumPolizaSisLegado.Text.Trim();
                resultado.kwik = TextNumKwik.Text.Trim();

                if (CheckHombreClave.Checked)
                {
                    resultado.HombreClave = true;
                    resultado.Prioridad = wfiplib.E_PrioridadTramite.HombreClave;
                }
                else
                {
                    resultado.Prioridad = wfiplib.E_PrioridadTramite.Tramite;
                }

                switch (oTramite.IdTipoTramite)
                {
                    case wfiplib.E_TipoTramite.serviciosVida:
                        break;
                    case wfiplib.E_TipoTramite.ServicioGmm:
                        break;
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        if (CheckBox3.Checked)
                        {
                            resultado.CPDES = false;
                            resultado.FolioCPDES = "";
                            resultado.EstatusCPDES = "";
                        }
                        else
                        {
                            resultado.FolioCPDES = lblFolioCPDES.Text.Trim();
                            resultado.EstatusCPDES = EstatusCPDES.SelectedValue.ToString();
                        }
                        if (GrandesSumas())
                        {
                            resultado.Prioridad = wfiplib.E_PrioridadTramite.GrandesSumas;

                        }
                        else if (GrandesSumasPrimaTotal())
                        {
                            resultado.Prioridad = wfiplib.E_PrioridadTramite.GrandesSumasPrimas;
                        }

                        /// String combo = texCombo.Text.Trim();
                        String combo = listCombosCitaMed.Text.Trim();
                        if (combo != "" && combo != null)
                        {
                            resultado.CitaMedica = true;
                            resultado.Combo = listCombosCitaMed.Text.Trim(); //texCombo.Text.Trim();

                            CitasMedicasEvalucacion();

                            resultado.Edad = texEdad.Text.Trim();
                            resultado.Cel = texCel.Text.Trim();
                            resultado.Estado = LisEstado.Text.Trim();
                            resultado.CelAgentePromotor = texCelAg.Text.Trim();
                            resultado.Ciudad = LisCiudad.Text.Trim();
                            resultado.Correo = texCorreo.Text.Trim();
                            resultado.LaboratorioHospital = LisLabHospital.Text.Trim();
                            resultado.Direccion = TextDireccion.Text.Trim();
                            resultado.Fecha1 = TextFecha1.Text.Trim();
                            resultado.Fecha2 = TextFecha2.Text.Trim();
                            resultado.Fecha3 = TextFecha3.Text.Trim();
                            resultado.Fecha4 = TextFecha4.Text.Trim();
                            resultado.FechaRecepcion = TextFechaRecepcion.Text.Trim();
                            resultado.Activo = 1;

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
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            double SumaAseguradaBasica = double.Parse(txtSumaAseguradaBasica.Text.ToString());
            int IdMoneda = int.Parse(cboMoneda.Text.ToString());
            Double SumaAseguradaBasicaConvertida = convertir(SumaAseguradaBasica, IdMoneda);

            SumaBasica.Text = "";
            if (SumaAseguradaBasicaConvertida < 10000.00)
            {
                SumaBasica.Text = "la suma asegura no es mayor a 10,000.00 Pesos";
            }
            else
            {
                if (ValidantinuidadRFC())
                {
                    Continuar();
                    /*
                    MSresultado2.Text = "";
                    String combo = texCombo.Text.Trim();
                    if (combo != "" && combo != null)
                    {
                        if (ContinuarFechas())
                        {
                            Continuar();
                        }
                        else
                        {
                            // MSresultado2.Text = "no hara nada";
                        }
                    }
                    else
                    {
                        Continuar();
                    }
                    */
                }
            }
        }

        protected void BtnContinuarEjecucion_Click(object sender, EventArgs e)
        {
            wfiplib.EmisionVG oDatos = recuperaCaptura();
            wfiplib.admTramite oAdmTramite = new wfiplib.admTramite();
            if (oAdmTramite.AlteraEjecucion(oDatos))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void BtnContinuarKwik_Click(object sender, EventArgs e)
        {
            wfiplib.EmisionVG oDatos = recuperaCaptura();
            wfiplib.admTramite oAdmTramite = new wfiplib.admTramite();
            if (oAdmTramite.AlteraKwik(oDatos))
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void BtnContinuarAdmision_Click(object sender, EventArgs e)
        {
            double SumaAseguradaBasica = double.Parse(txtSumaAseguradaBasica.Text.ToString());
            int IdMoneda = int.Parse(cboMoneda.Text.ToString());
            Double SumaAseguradaBasicaConvertida = convertir(SumaAseguradaBasica, IdMoneda);

            SumaBasica.Text = "";
            if (SumaAseguradaBasicaConvertida < 10000.00)
            {
                SumaBasica.Text = "la suma asegura no es mayor a 10,000.00 Pesos";
            }
            else
            {
                if (ValidantinuidadRFC())
                {
                    
                    wfiplib.EmisionVG oDatos = recuperaCaptura();
                    wfiplib.admTramite oAdmTramite = new wfiplib.admTramite();
                    wfiplib.admEmisionVG oAdmEmisionVG = new wfiplib.admEmisionVG();
                    if (oAdmTramite.ActualiaOrdenFecha(oDatos))
                    {
                        if (oAdmEmisionVG.AlteraTRAM00003P(oDatos))
                        {
                            if (oAdmEmisionVG.ActualiaProductoSubproducto(oDatos))
                            {
                                Response.Redirect(Request.RawUrl);
                            }
                            else
                            {
                                Response.Redirect(Request.RawUrl);
                            }
                        }
                        else
                        {
                            Response.Redirect(Request.RawUrl);
                        }
                    }
                }
            }
        }

        protected bool ValidantinuidadRFC()
        {
            bool resultado = false;
            MensajesRFC.Text = "";

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
                            MensajesRFC.Text = "RFC Persona Física Inválido ";
                        }
                    }
                    else
                    {
                        MensajesRFC.Text = "El RFC No Contiene 13 Caracteres ";
                    }
                }
                else
                {
                    MensajesRFC.Text = "Coloca el RFC de la Persona Física";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                string MoralRFC = txRfcMoral.Text.ToString().Trim();
                if (MoralRFC != "" && MoralRFC != null)
                {
                    if (MoralRFC.Length == 12)
                    {
                        Regex Val = new Regex(@"^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$");
                        if (Val.IsMatch(MoralRFC))
                        {
                            resultado = true;
                        }
                        else
                        {
                            MensajesRFC.Text = "RFC Persona Moral Inválido ";
                        }
                    }
                    else
                    {
                        MensajesRFC.Text = "El RFC No Contiene 12 Caracteres ";
                    }
                }
                else
                {
                    MensajesRFC.Text = "Coloca el RFC Moral ";
                }
            }
            return resultado;
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

        protected void CitasMedicas(object sender, EventArgs e)
        {
            CitasMedicasEvalucacion();
        }

        protected void CitasMedicasEvalucacion()
        {
            CitaMedica.Visible = true;
            texCombo.Enabled = false;
            TextDireccion.Enabled = false;
            CancelarCita.Visible = true;
            ProgramarCita.Visible = false;
            /*
            listCombos();
            listEstados();
            listCiudad();
            lisLabHospital();
            */

            if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                if (CheckBox1.Checked.Equals(true))
                {
                    int Edad = CalcularEdad(dtFechaNacimientoTitular.Text.Trim());
                    texEdad.Text = Edad.ToString();
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                if (CheckBox1.Checked.Equals(true))
                {
                    int Edad = CalcularEdad(dtFechaNacimientoTitular.Text.Trim());
                    texEdad.Text = Edad.ToString();
                }
                else
                {
                    int Edad = CalcularEdad(dtFechaNacimiento.Text.Trim());
                    texEdad.Text = Edad.ToString();
                }
            }

            /*
            MSresultado2.Text = "";
            texCombo.Text = "";
            string DescripCombo = "";
            CitaMedica.Visible = false;

            Double MontoTotal = Total(txtSumaAseguradaBasica.Text.ToString(), txtPrimaTotal.Text.ToString(), cboMoneda.Text.ToString());

            if (SubProducto.Text == "TempoLife")
            {
                DescripCombo = "Tempo life riesgos preferentes";
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
                }
                else
                {
                    int Edad = CalcularEdad(dtFechaNacimiento.Text.Trim());
                    Evaluacion(MontoTotal, Edad, DescripCombo);
                }
            }
            */
        }
        /*
        protected void Evaluacion(double Total, int Edad, string DescripCombo)
        {
            texEdad.Text = Edad.ToString();
            DataTable combo = (new wfiplib.admEmisionVG()).validaCombo(Total, Edad, DescripCombo);
            if (combo.Rows.Count > 0)
            {
                DataRow row = combo.Rows[0];
                if (row["combo"].ToString() == null || row["combo"].ToString() == "")
                {
                    MSresultado2.Text = "La solicitud no necesita cita médica";
                    //FormConfirmacion.Visible = false;
                }
                else
                {
                    texCombo.Text = row["combo"].ToString();

                    DateTime Fecha22 = Convert.ToDateTime(FechaRegistro.Text.ToString());

                    DateTime validateFechaSolicitud = Convert.ToDateTime(Fecha22.ToShortDateString());
                    //DateTime validateFechaSolicitud = FechaRegistro.Text.ToString();
                    //DateTime validateFechaSolicitud = DateTime.Now;

                    //DateTime hora1 = Convert.ToDateTime("16:02:00");
                    //DateTime fechaConvertida = validateFechaSolicitud.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);
                    //DateTime HC1 = validateFechaSolicitud.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);


                    ValidaFecha(validateFechaSolicitud, row["combo"].ToString());
                    //MSresultado.Text = "La solicitud necesita cita médica ";
                    CitaMedica.Visible = true;
                    texCombo.Enabled = false;
                    TextDireccion.Enabled = false;
                    CancelarCita.Visible = true;
                    ProgramarCita.Visible = false;
                    listEstados();
                    listCiudad();
                    lisLabHospital();
                }
            }
            else
            {
                MSresultado2.Text = "La solicitud no necesita cita médica";
            }
        }
        */
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
                FechasCombo(FechaSolicitud, dias);
            }
            else if (HC3 <= FechaSolicitud && FechaSolicitud <= HC4)
            {
                int dias = AumentaDias(combo, 2);
                FechasCombo(FechaSolicitud, dias);
            }
        }
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

            TextFecha4.TimeSectionProperties.Visible = true;
            TextFecha4.UseMaskBehavior = true;
            TextFecha4.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha4.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
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
                else if (combo == "2")
                {
                    dias = 2;
                }
                else if (combo == "3")
                {
                    dias = 2;
                }
            }
            else if (horario == 2)
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

        protected int CalcularEdad(String Fecha)
        {
            int edad = 0;
            string fecha = Fecha;
            DateTime nacimiento = DateTime.Parse(fecha);
            edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            return edad;
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

        protected void BtnCancelarCita(object sender, EventArgs e)
        {
            MSresultado2.Text = "";
            CitaMedica.Visible = false;
            ProgramarCita.Visible = true;
            CancelarCita.Visible = false;
            texEdad.Text = "";
            texCombo.Text = "";
            texCel.Text = "";
            texCelAg.Text = "";
            texCorreo.Text = "";
            TextFecha3.Text = "";
            TextFecha4.Text = "";
            Radio1.Checked = false;
            Radio2.Checked = false;
            Radio3.Checked = false;
            Radio4.Checked = false;

            int idTramite = Convert.ToInt32(IdTramite.Text);
            if ((new wfiplib.admEmisionVG()).InactivoCitaMedica(idTramite))
            {
                if ((new wfiplib.admTramite()).CambiaTramite(idTramite))
                {
                    ProcesarTramite(wfiplib.E_EstadoTramite.Proceso, wfiplib.E_EstadoMesa.CMCancelada);
                }
                MSresultado2.Text = "Cita Medica Eliminada";
            }
            /*
            TextCombo.Text = "";
            MSresultado.Text = "";
            MSresultado2.Text = "";
            citamedica.Visible = false;
            */
        }

        protected void fechas_Changed(object sender, EventArgs e)
        {
            string Nfecha = TextFecha1.Text.Trim();
            string Nfech2 = TextFecha2.Text.Trim();
            if (TextFecha1.Text.Trim().Length == 0)
            {
                DateTime dtValor = DateTime.Today;
                Nfecha = dtValor.ToString();
            }

            if (TextFecha2.Text.Trim().Length == 0)
            {
                DateTime dtValor = DateTime.Today;
                Nfech2 = dtValor.ToString();
            }

            DateTime Fecha1 = Convert.ToDateTime(Nfecha);
            DateTime Fecha2 = Convert.ToDateTime(Nfech2);
            texFecha1.Text = "";
            texFecha2.Text = "";
            texFecha3.Text = "";

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
                }
            }

            if (!ValdaComparaFechas(Fecha1, Fecha2))
                texFecha1.Text = "Fecha no valida";

            if (!ValdaComparaFechas(Fecha2, Fecha1))
                texFecha2.Text = "Fecha no valida";
        }

        public bool ValdaComparaFechas(DateTime Fecha, DateTime Fecha2)
        {
            bool resultado = true;
            DateTime FechaHora = Convert.ToDateTime(Fecha.ToShortTimeString());
            //DateTime FechaHora = Convert.ToDateTime("00:00:00");
            DateTime hora1 = Convert.ToDateTime("08:00:00");
            DateTime hora2 = Convert.ToDateTime("23:59:59");

            if (FechaHora >= hora1 && FechaHora <= hora2)
            {
                if (Fecha.Equals(Fecha2))
                    resultado = false;
            }
            else
                resultado = false;

            return resultado;
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
                    resultado = false;
                else if (Fecha3.Equals(Fecha2))
                    resultado = false;
            }
            else
                resultado = false;

            return resultado;
        }

        protected void antecedentesRFC()
        {
            TextantecedentesRFC.Text = "";
            textRFCFisica.Text = "";

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                string RFC = txRfc.Text.Trim().Replace("-", "");
                if ((new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision2(RFC))
                {
                    if ((new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC))
                        textRFCFisica.Text = "Ya existen trámites registrados para el RFC";
                }
                /*
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    textRFCFisica.Text = "Ya existen trámites registrados para el RFC";
                }
                */
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                string RFC = txRfcMoral.Text.Trim().Replace("-", "");
                if ((new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision2(RFC))
                {
                    if ((new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC))
                        TextantecedentesRFC.Text = "Ya existen trámites registrados para el RFC";
                }
                /*
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                    TextantecedentesRFC.Text = "Ya existen trámites registrados para el RFC";
                */
            }
        }

        protected void PrimaTotalGrandesSumas(object sender, EventArgs e)
        {
            if (GrandesSumasPrimaTotal())
            {
                PrimaTotalGrandeSumas.Text = "GRANDES SUMAS";
            }

        }

        protected bool GrandesSumasPrimaTotal()
        {
            bool resultado = false;
            PrimaTotalGrandeSumas.Text = "";
            if (cboMoneda.SelectedValue != "-1")
            {
                int IdMoneda = int.Parse(cboMoneda.Text.ToString());
                string primatotal = "";

                if (txtPrimaTotal.Text.ToString().Length == 0)
                {
                    primatotal = "0.0";
                }
                else
                {
                    primatotal = txtPrimaTotal.Text.ToString();
                }
                
                double Monto = double.Parse(primatotal);
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
                GrandeSumas.Text = "GRANDES SUMAS";

            }
            else if (GrandesSumasPrimaTotal())
            {
                PrimaTotalGrandeSumas.Text = "GRANDES SUMAS";
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
                // Monto total de la suma asegura y prima total, apartir del tipo de moneda seleccionada
                Double MontoTotal = Total(txtSumaAseguradaBasica.Text.ToString(), txtSumaAseguradaPolizasVigentes.Text.ToString(), cboMoneda.Text.ToString());
                // Validacion del monto "MAL PROGRAMADO" el id del dolar es = 2, el regitro esta en SQL cat_moneda 
                //double ValidacionMonto = double.Parse("1500000");
                double ValidacionMonto = double.Parse("6000000.00");
                ValidacionMonto = convertir(ValidacionMonto, 2);
                if (MontoTotal >= ValidacionMonto)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public void BuscaAntecedentesRFC()
        {
            TextantecedentesRFC.Text = "";
            textRFCFisica.Text = "";
            string FisicaRFC = txRfc.Text.Trim();
            string MoralRFC = txRfcMoral.Text.Trim();
            wfiplib.admServiciosUtiler oAdmServicio = new wfiplib.admServiciosUtiler();

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                if (oAdmServicio.buscaRFCAntecedenteEmision(FisicaRFC))
                    textRFCFisica.Text = "Ya existen trámites registrados para el RFC";
                else
                    textRFCFisica.Text = "";
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {

                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(MoralRFC);
                if (resultado)
                    TextantecedentesRFC.Text = "Ya existen trámites registrados para el RFC";
                else
                    TextantecedentesRFC.Text = "";
            }
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
                        txRfcMoral.Text = moral.RetornaLetrasFinalesRFC(removerAcentos(txNomMoral.Text.ToUpper().Trim()), dtValor.ToString("yy/MM/dd"));
                        //TextantecedentesRFC.Text = "Ya CALCULO RFC MORAL";
                        antecedentesRFC();
                        //BuscaAntecedentesRFC();
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
                        txRfc.Text = rfc.RFC13Pocisiones(removerAcentos(txApPat.Text.ToUpper().Trim()), removerAcentos(txApMat.Text.ToUpper().Trim()), removerAcentos(txNombre.Text.ToUpper().Trim()), dtValor.ToString("yy/MM/dd"));
                        antecedentesRFC();
                        //BuscaAntecedentesRFC();
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

        private void showMessage(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.mensajesInformativos, typeof(string), "Alert", "alert('" + Mensaje + "');", true);
            // Response.Write("<script language=javascript>alert('" + Mensaje + "')</script>");
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

        protected void LisLabHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        protected void LisCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            lisLabHospital();
        }
        protected void LisEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            listCiudad();
            lisLabHospital();
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

        protected void listCombos()
        {
            DataTable listCombos = (new wfiplib.admEmisionVG()).cargaCombosCitasMedicas();
            listCombosCitaMed.DataSource = listCombos;
            listCombosCitaMed.Items.Add("Seleccione");
            listCombosCitaMed.DataBind();
            listCombosCitaMed.DataTextField = "combo";
            listCombosCitaMed.DataValueField = "combo";
            listCombosCitaMed.DataBind();
        }

        private String validaPais(string nombre)
        {
            String respuesta = (new wfiplib.admEmisionVG()).validaPais(nombre);
            return respuesta;
        }
        protected void ActividadCPDES_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cargarNacionalidadesCombo_db(ref ASPxComboBox objDDL)
        {
            DataTable dtPaises = (new wfiplib.admEmisionVG()).cargaPaises();
            objDDL.DataSource = dtPaises;
            objDDL.TextField = "Nombre";
            objDDL.ValueField = "Id";
            objDDL.DataBind();
        }

        protected string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        private void DatosPromotoriaLimpiar()
        {
            texClave.Text = "";
            texRegion.Text = "";
            texGerente.Text = "";
            texEjecutivo.Text = "";
            texEjecutivoFront.Text = "";
        }

        private void DatosPromotoria(String IdPromotoria)
        {
            wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(IdPromotoria));
            wfiplib.comercialPromotoria comercial = (new wfiplib.admAgentesPromotoria()).getComercialInformation(promotoria.Clave);

            texClave.Text = promotoria.Clave.ToString();
            texRegion.Text = string.Concat(comercial.ClaveRegion, " - " + comercial.Region);
            texGerente.Text = string.Concat(comercial.ClaveGerente, " - " + comercial.Gerente);
            texEjecutivo.Text = string.Concat(comercial.ClaveEjecutivo, " - " + comercial.Ejecutivo);
            texEjecutivoFront.Text = string.Concat(comercial.ClaveFront, " - " + comercial.Front);
            /*
            array[0] = string.Concat(comercial.ClaveRegion, " - " + comercial.Region);
            array[1] = string.Concat("", "");
            array[2] = string.Concat(comercial.ClaveGerente, " - " + comercial.Gerente);
            array[3] = string.Concat(comercial.ClaveEjecutivo, " - " + comercial.Ejecutivo);
            */

        }

        private string getInfoMesasStatus(int pIdTramite)
        {
            string strRespuesta = "";
            string strStatus = "";
            try
            {
                DataTable data = (new wfiplib.admMesa()).getStatusMesas(pIdTramite);
                foreach (DataRow row in data.Rows)
                {
                    switch ((wfiplib.E_EstadoMesa)Enum.ToObject(typeof(wfiplib.E_EstadoMesa), (int)row["Estado"]))
                    {
                        case wfiplib.E_EstadoMesa.Registro:
                            strStatus = "<img src = '../img/bolaGris.png' alt = '' height = '24px' width = '24px' />";
                            break;

                        case wfiplib.E_EstadoMesa.Hold:
                        case wfiplib.E_EstadoMesa.CMConfirmacionPendiente:
                        case wfiplib.E_EstadoMesa.CMRevisionProspecto:
                            strStatus = "<img src = '../img/bolaAmarilla.png' alt = '' height = '24px' width = '24px' />";
                            break;

                        case wfiplib.E_EstadoMesa.Suspendido:
                            strStatus = "<img src = '../img/bolaNaranja.png' alt = '' height = '24px' width = '24px' />";
                            break;

                        case wfiplib.E_EstadoMesa.NoProcesable:
                            strStatus = "<img src = '../img/bolaNaranja.png' alt = '' height = '24px' width = '24px' /><b style='color:red;'>NO PROCESABLE</b>";
                            break;
                        
                        case wfiplib.E_EstadoMesa.Atrapado:
                            strStatus = "<img src = '../img/bolaAzul.png' alt = '' height = '24px' width = '24px' />";
                            break;

                        case wfiplib.E_EstadoMesa.Procesado:
                        case wfiplib.E_EstadoMesa.Procesable:
                        case wfiplib.E_EstadoMesa.CMCitaProgramada:
                            strStatus = "<img src = '../img/bolaVerde.png' alt = '' height = '24px' width = '24px' />";
                            break;

                        case wfiplib.E_EstadoMesa.CMCancelada:
                            strStatus = "<img src = '../img/bolaRoja.png' alt = '' height = '24px' width = '24px' /><b style='color:red;'>Cita Cancelada</b>";
                            break;

                        case wfiplib.E_EstadoMesa.Rechazo:
                            strStatus = "<img src = '../img/bolaRoja.png' alt = '' height = '24px' width = '24px' />";
                            break;

                        case wfiplib.E_EstadoMesa.PCI:
                            strStatus = "<img src = '../img/bolaMorada.png' alt = '' height = '24px' width = '24px' />";
                            break;

                        default:
                            strStatus = "<img src = '../img/bolaGrisObscuro.png' alt = '' height = '24px' width = '24px' />";
                            break;
                    }

                    strRespuesta += "<tr><td>" + row["Mesa"].ToString().ToUpper().Trim() + "</td><td text-align='center'>" + strStatus + "</td></tr>";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                strRespuesta = "";
            }
            return strRespuesta;
        }

        private void limpiaDirectorioUsr(string Ruta, string CodigoUsr)
        {
            string NomArchivo = CodigoUsr + "*.*";
            string[] archivos = System.IO.Directory.GetFiles(Ruta, NomArchivo);
            foreach (string arch in archivos)
            {
                try
                {
                    System.IO.File.Delete(arch);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }
        }

        private wfiplib.tramiteMesa recuperaDatos()
        {
            wfiplib.tramiteMesa resultado = (new wfiplib.admTramiteMesa()).carga(Convert.ToInt32(hdIdTramite.Value), Convert.ToInt32(Request.QueryString["tp"]));
           
            //NUEVO REQUERIMIENTO
            if (resultado.MesaNombre == "CALIDAD")
            {
                resultado.ObservacionPublica = txtObservacionNoAplica.Text.Trim().ToUpper();
            }
            else
            {
                resultado.ObservacionPublica = txComentarios.Text.Trim().ToUpper();
            }

            resultado.ObservacionPrivada = txComentariosPrv.Text.Trim().ToUpper();
            return resultado;
        }

        private string registraDocExp(int pIdTramite, string directorioTemporal)
        {
            string msgError = "";
            try
            {
                wfiplib.admExpediente oAdmExp = new wfiplib.admExpediente();
                List<string> lstArchivos = new List<string>();

                lstArchivos.Add(directorioTemporal);

                string DirectoriSeparador = Server.MapPath("~");// + "\\DocsUp\\";

                // CONULTA EL ULTIMO ARCHIVO FUSION.
                wfiplib.expediente oFusionAnt = oAdmExp.daFusion(pIdTramite);
                string ArchFusionAnt = "";
                if (oFusionAnt.Id > 0)
                {
                    ArchFusionAnt = DirectoriSeparador + oFusionAnt.NmArchivo;
                }

                int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                string nombreFusion = DirectoriSeparador + IdArchivo.ToString().PadLeft(8, '0') + ".pdf";
                if (File.Exists(nombreFusion))
                {
                    File.Delete(nombreFusion);
                }

                
                string nmSeparador = DirectoriSeparador + manejo_sesion.Credencial.Usuario + ".pdf";
                string nmLogo = Server.MapPath("~\\img") + @"\logo_sep.png";
                //msgError = (new wfiplib.admPdfFusion()).Adiciona(lstArchivos, ArchFusionAnt, nombreFusion, oCredencial.Nombre, nmSeparador, nmLogo);
                msgError = (new wfiplib.admPdfFusion()).Adiciona(lstArchivos, ArchFusionAnt, nombreFusion, manejo_sesion.Credencial.Nombre, nmSeparador, nmLogo);
                if (string.IsNullOrEmpty(msgError))
                {
                    wfiplib.expediente oFusion = new wfiplib.expediente();
                    oFusion.IdTramite = pIdTramite;
                    oFusion.Id = IdArchivo;
                    oFusion.NmArchivo = IdArchivo.ToString().PadLeft(8, '0') + ".pdf";
                    oFusion.NmOriginal = "";
                    oFusion.Activo = wfiplib.E_SiNo.Si;
                    oFusion.Fusion = wfiplib.E_SiNo.Si;
                    oAdmExp.eliminaFusion(pIdTramite);
                    oAdmExp.Nuevo(oFusion);
                    msgError = "";
                }
            }
            catch (Exception ex)
            {
                msgError = ex.Message;
            }
            return msgError;
        }

        public void GeneraCartaEstatusTramite(int IdTramite, wfiplib.E_EstadoTramite EstadoTramite)
        {
            string folio = (new wfiplib.admTramite()).getFolio(IdTramite);
            wfiplib.admCartas cartas = new wfiplib.admCartas();
            string directorioTemporal = "";

            switch (EstadoTramite)
            {
                case wfiplib.E_EstadoTramite.Rechazo:
                    directorioTemporal = Server.MapPath("~") + "\\Cartas\\CartaRechazo_" + manejo_sesion.Credencial.Id + "_" + folio + ".pdf";
                    if (System.IO.File.Exists(directorioTemporal))
                    {
                        File.Delete(directorioTemporal);
                        cartas.CartaRechazoPDF(IdTramite, Response, 1, manejo_sesion.Credencial.Id);
                    }
                    else
                    {
                        cartas.CartaRechazoPDF(IdTramite, Response, 1, manejo_sesion.Credencial.Id);
                    }
                    break;

                case wfiplib.E_EstadoTramite.Suspendido:
                    directorioTemporal = Server.MapPath("~") + "\\Cartas\\CartaSuspendido_" + manejo_sesion.Credencial.Id + "_" + folio + ".pdf";
                    if (System.IO.File.Exists(directorioTemporal))
                    {
                        File.Delete(directorioTemporal);
                        cartas.CartaSuspendidoPDF(IdTramite, Response, 1, manejo_sesion.Credencial.Id);
                    }
                    else
                    {
                        cartas.CartaSuspendidoPDF(IdTramite, Response, 1, manejo_sesion.Credencial.Id);
                    }
                    break;

                case wfiplib.E_EstadoTramite.Hold:
                    directorioTemporal = Server.MapPath("~") + "\\Cartas\\CartaHold_" + manejo_sesion.Credencial.Id + "_" + folio + ".pdf";
                    if (System.IO.File.Exists(directorioTemporal))
                    {
                        File.Delete(directorioTemporal);
                        cartas.CartaHoldPDF(IdTramite, Response, 1, manejo_sesion.Credencial.Id);
                    }
                    else
                    {
                        cartas.CartaHoldPDF(IdTramite, Response, 1, manejo_sesion.Credencial.Id);
                    }
                    break;
            }
            
            registraDocExp(IdTramite, directorioTemporal);

        }

        private void actualizaEstadoTramite(int pIdTramite, int pIdMesa)
        {
            wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
            wfiplib.mesa oMesa = (new wfiplib.admMesa()).carga(pIdMesa);
            bool blnTienePendietes = false;
            bool conRechazo = false;
            bool conHold = false;
            bool conSuspencion = false;
            bool conPCI = false;
            bool conCMRevProspecto = false;
            bool conCMConfirmacionPendiente = false;
            bool conSuspensionCitaMedica = false;

            if (
                (oMesa.Id == 2) || (oMesa.Id == 15) || (oMesa.Id == 29) ||
                (oMesa.Id == 3) || (oMesa.Id == 16) || (oMesa.Id == 30) 
            )
            {
                blnTienePendietes = oAdmTramiteMesa.tienePendientes_gpoPladDoc(pIdTramite, pIdMesa);
            }
            else 
            {
                if (
                    (oMesa.Id == 4) || (oMesa.Id == 17) || (oMesa.Id == 31)
                )
                {
                    blnTienePendietes = oAdmTramiteMesa.tienePendientes_gpoSeleccion(pIdTramite, pIdMesa);
                }
                else 
                {
                    blnTienePendietes = oAdmTramiteMesa.tienePendientes(pIdTramite, pIdMesa);
                }
            }


            if (!blnTienePendietes)
            {
                if (
                    (oMesa.Id == 2) || (oMesa.Id == 15) || (oMesa.Id == 29) ||
                    (oMesa.Id == 3) || (oMesa.Id == 16) || (oMesa.Id == 30)
                )
                {
                    bool existeMesaSeleccion = oAdmTramiteMesa.tienePendientes_existeSeleccion(pIdTramite, pIdMesa);

                    if (!existeMesaSeleccion)
                    {
                        registraSigMesaFlujoManual(pIdTramite, oMesa.Id);
                    }
                    else
                    {
                        List<wfiplib.E_EstadoMesa> Lista = oAdmTramiteMesa.DaEstadosMismoNivel_manualSeleccion(pIdTramite, oMesa.IdFlujo, oMesa.IdEtapa);
                        foreach (wfiplib.E_EstadoMesa var in Lista)
                        {
                            if (var == wfiplib.E_EstadoMesa.Rechazo) { conRechazo = true; }
                            if (var == wfiplib.E_EstadoMesa.Suspendido) { conSuspencion = true; }
                            if (var == wfiplib.E_EstadoMesa.Hold) { conHold = true; }
                            if (var == wfiplib.E_EstadoMesa.PCI) { conPCI = true; }
                            if (var == wfiplib.E_EstadoMesa.CMRevisionProspecto) { conCMRevProspecto = true; }
                            if (var == wfiplib.E_EstadoMesa.CMConfirmacionPendiente) { conCMConfirmacionPendiente = true; }
                            if (var == wfiplib.E_EstadoMesa.SuspensionCitaMedica) { conSuspensionCitaMedica = true; }
                        }
                    }
                }
                else
                {
                    if ((oMesa.Id == 4) || (oMesa.Id == 17) || (oMesa.Id == 31))
                    {
                        List<wfiplib.E_EstadoMesa> Lista = oAdmTramiteMesa.DaEstadosMismoNivel_manualSeleccion(pIdTramite, oMesa.IdFlujo, oMesa.IdEtapa);
                        foreach (wfiplib.E_EstadoMesa var in Lista)
                        {
                            if (var == wfiplib.E_EstadoMesa.Rechazo) { conRechazo = true; }
                            if (var == wfiplib.E_EstadoMesa.Suspendido) { conSuspencion = true; }
                            if (var == wfiplib.E_EstadoMesa.Hold) { conHold = true; }
                            if (var == wfiplib.E_EstadoMesa.PCI) { conPCI = true; }
                            if (var == wfiplib.E_EstadoMesa.CMRevisionProspecto) { conCMRevProspecto = true; }
                            if (var == wfiplib.E_EstadoMesa.CMConfirmacionPendiente) { conCMConfirmacionPendiente = true; }
                            if (var == wfiplib.E_EstadoMesa.SuspensionCitaMedica) { conSuspensionCitaMedica = true; }
                        }
                    }
                    else
                    {
                        List<wfiplib.E_EstadoMesa> Lista = oAdmTramiteMesa.DaEstadosMismoNivel(pIdTramite, oMesa.IdFlujo, oMesa.IdEtapa);
                        foreach (wfiplib.E_EstadoMesa var in Lista)
                        {
                            if (var == wfiplib.E_EstadoMesa.Rechazo) { conRechazo = true; }
                            if (var == wfiplib.E_EstadoMesa.Suspendido) { conSuspencion = true; }
                            if (var == wfiplib.E_EstadoMesa.Hold) { conHold = true; }
                            if (var == wfiplib.E_EstadoMesa.PCI) { conPCI = true; }
                            if (var == wfiplib.E_EstadoMesa.CMRevisionProspecto) { conCMRevProspecto = true; }
                            if (var == wfiplib.E_EstadoMesa.CMConfirmacionPendiente) { conCMConfirmacionPendiente = true; }
                            if (var == wfiplib.E_EstadoMesa.SuspensionCitaMedica) { conSuspensionCitaMedica = true; }
                        }
                    }
                }

                //### Liberación Diciembre 2020
                //para que funcione el cambio de flujo de citas médicas, se debe cambioar los triggers de la tabla de tramitemesa
                //recordar que están solicitando la actualización del reporte de sabana - observación privada
                //hace falta probar la carta de grupo los angeles

                if (conRechazo)
                {
                    (new wfiplib.admTramite()).cambiaEstado(pIdTramite, wfiplib.E_EstadoTramite.Rechazo);
                    GeneraCartaEstatusTramite(pIdTramite, wfiplib.E_EstadoTramite.Rechazo);
                }
                else
                {
                    if (conSuspencion)
                    {
                        (new wfiplib.admTramite()).cambiaEstado(pIdTramite, wfiplib.E_EstadoTramite.Suspendido);
                        GeneraCartaEstatusTramite(pIdTramite, wfiplib.E_EstadoTramite.Suspendido);
                    }
                    else
                    {
                        if (conHold)
                        {
                            (new wfiplib.admTramite()).cambiaEstado(pIdTramite, wfiplib.E_EstadoTramite.Hold);
                            GeneraCartaEstatusTramite(pIdTramite, wfiplib.E_EstadoTramite.Hold);
                        }
                        else
                        {
                            if (conPCI)
                            {
                                (new wfiplib.admTramite()).cambiaEstado(pIdTramite, wfiplib.E_EstadoTramite.PCI);
                                //GeneraCartaEstatusTramite(pIdTramite, wfiplib.E_EstadoTramite.PCI);
                            }
                            else
                            {
                                if (conCMRevProspecto)
                                {
                                    (new wfiplib.admTramite()).cambiaEstado(pIdTramite, wfiplib.E_EstadoTramite.CMRevProspecto);
                                }
                                else
                                {
                                    if (conCMConfirmacionPendiente)
                                    {
                                        (new wfiplib.admTramite()).cambiaEstado(pIdTramite, wfiplib.E_EstadoTramite.CMConfirmacionPendiente);
                                    }
                                    else
                                    {
                                        if (conSuspensionCitaMedica)
                                        {
                                            (new wfiplib.admTramite()).cambiaEstado(pIdTramite, wfiplib.E_EstadoTramite.SuspensionCitaMedica);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else 
            {
                // Pendiente
            }
        }

        protected void btnStopAsig_Click(object sender, EventArgs e)
        {
            Session["TramiteAutomatico"] = false;
            btnStopAsig.Enabled = false;
            btnStopAsig.CssClass = "btnGris";
            mensajes.MostrarMensaje(this, "Se ha detenido la asignación de tramites para la mesa actual.");
        }

        protected void btnNoProcesable_Click(object sender, EventArgs e)
        {
            if (txComentariosPrv.Text.Trim().Length > 3)
            {
                ProcesarTramite(wfiplib.E_EstadoTramite.Proceso, wfiplib.E_EstadoMesa.NoProcesable);
            }
            else
            {
                mensajes.MostrarMensaje(this, "Las observaciones son requeridas.");
            }
        }

        protected void btnProcesable_Click(object sender, EventArgs e)
        {
            if (txComentariosPrv.Text.Trim().Length > 3)
            {
                ProcesarTramite(wfiplib.E_EstadoTramite.Proceso, wfiplib.E_EstadoMesa.Procesable);
            }
            else
            {
                mensajes.MostrarMensaje(this, "Las observaciones son requeridas.");
            }
                
        }

        protected void btnConPendiente_Click(object sender, EventArgs e)
        {
            //mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
        }

        protected void btnCitaProgramada_Click(object sender, EventArgs e)
        {
            if ((new wfiplib.admTramite()).cambiaEstado(int.Parse(hdIdTramite.Value.ToString()), wfiplib.E_EstadoTramite.Proceso))
            {
                ProcesarTramite(wfiplib.E_EstadoTramite.Proceso, wfiplib.E_EstadoMesa.CMCitaProgramada);
            }
        }

        protected void btnCitaReProgramada_Click(object sender, EventArgs e)
        {
            //mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
        }

        protected void btnSuspencionCitaMedica_Click(object sender, EventArgs e)
        {
            wfiplib.admTramiteMesa tramiteMesa = new wfiplib.admTramiteMesa();
            DataTable data = tramiteMesa.ConsultaMesaCitaMedica(Convert.ToInt32(hdIdTramite.Value));
            wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
            wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();

            if (data.Rows.Count > 0)
            {
                if (txComentariosPrv.Text.Length > 0)
                {
                    if ((new wfiplib.admTramite()).cambiaEstado(int.Parse(hdIdTramite.Value.ToString()), wfiplib.E_EstadoTramite.InfoCitaMedica))
                    {
                        // CAMBIA EL STATUS DEL TRAMITE 
                        if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.InfoCitasMedicas, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                        {
                            // REGISTRA EN BITACORA EL MOVIEMINTO
                            registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                            // ACTUALIZA LA PESTAÑA 
                            validaEnLinea();
                        }
                    }
                }
                else
                {
                    mensajes.MostrarMensaje(this, "OBSERVACIONES PRIVADAS REQUERIDAS.");
                }
            }
            else
            {
                mensajes.MostrarMensaje(this, "NO EXISTE CITA MÉDICA PREVIA.");
            }
        }

        protected void btnEsperaResultado_Click(object sender, EventArgs e)
        {
            wfiplib.admTramiteMesa tramiteMesa = new wfiplib.admTramiteMesa();
            DataTable data = tramiteMesa.ConsultaMesaCitaMedica(Convert.ToInt32(hdIdTramite.Value));
            wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
            wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();

            if (data.Rows.Count > 0)
            {
                if (txComentariosPrv.Text.Length > 0)
                {
                    // CAMBIA EL STATUS DEL TRAMITE 
                    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.CMEnEsperaResult, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                    {
                        // REGISTRA EN BITACORA EL MOVIEMINTO
                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                        // ACTUALIZA EL ESTADO DE LA MESA DE CITAS MEDICAS EN REGISTRO OBTIENE EL ID DE LA MESA CITAS MEDICAS            
                        int IdMesa = 0;
                        foreach (DataRow row in data.Rows)
                        {
                            IdMesa = Convert.ToInt32(row["Id"].ToString());
                        }
                        int idMesaRequeste = Convert.ToInt32(Request.QueryString["tp"]);

                        if (IdMesa == idMesaRequeste)
                        {
                            // NO ACTUALIZAR LA LA MESA DE CITAS MEDICAS.
                        }
                        else
                        {
                            oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, IdMesa, wfiplib.E_EstadoMesa.Registro, "", "");
                        }
                        
                        // ACTUALIZA LA PESTAÑA 
                        validaEnLinea();
                    }
                }
                else
                {
                    mensajes.MostrarMensaje(this, "OBSERVACIONES PRIVADAS REQUERIDAS.");
                }
                
            }
            else
            {
                mensajes.MostrarMensaje(this, "NO EXISTE CITA MÉDICA PREVIA.");
            }
            
        }

        protected void ProcesarTramite(wfiplib.E_EstadoTramite statusTramite, wfiplib.E_EstadoMesa statusMesa)
        {
            wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
            wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
            try
            {
                if (oTramiteMesa.MesaNombre == "EJECUCIÓN" && statusMesa == E_EstadoMesa.Procesado)
                {
                    string strPoliza = oAdmTramiteMesa.getPoliza(oTramiteMesa.IdTramite);
                    if (strPoliza.Length == 0 )
                    {
                        mensajes.MostrarMensaje(this, "Debe guardar un número de Póliza.");
                        return;
                    }
                }

                if (statusMesa == E_EstadoMesa.CMCitaProgramada || statusMesa == E_EstadoMesa.CMCitaReProgramada || statusMesa == E_EstadoMesa.CMConfirmacionPendiente || statusMesa == E_EstadoMesa.CMRevisionProspecto || statusMesa == E_EstadoMesa.CMEnEsperaResult)
                {
                    //if (!(new wfiplib.admTramite()).CitaMedicaCheckFechaSelected(oTramiteMesa.IdTramite))
                    if (!Radio1.Checked && !Radio2.Checked && !Radio3.Checked && !Radio4.Checked)
                    {
                        mensajes.MostrarMensaje(this, "No se ha seleccionado una fecha para la cita médica.");
                        return;
                    }
                }

                if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, statusMesa, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                {
                    if (oAdmTramiteMesa.getStatusTramite(oTramiteMesa.Id) != Convert.ToInt32(wfiplib.E_EstadoTramite.PromotoriaCancela))
                    {
                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                        if (statusMesa == E_EstadoMesa.CMCitaProgramada)
                        {
                            (new wfiplib.admTramite()).GeneraCitaMedica(oTramiteMesa.IdTramite);
                        }

                        wfiplib.mesa oMesa = (new wfiplib.admMesa()).carga(oTramiteMesa.IdMesa);
                        int ultimaEtapa = (new wfiplib.admMesa()).UltimaEtapaTramite(oTramiteMesa.IdFlujo);
                        if (oMesa.EtapaOrden == 1)
                        {
                            (new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, statusTramite);
                        }
                        else
                        {
                            if (oTramiteMesa.MesaNombre == "CALIDAD" && statusMesa == E_EstadoMesa.Procesado)
                            {
                                (new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, E_EstadoTramite.Ejecucion);
                            }
                        }

                        //if (oMesa.EtapaOrden == ultimaEtapa && oTramiteMesa.Procede == -100)
                        if (oMesa.EtapaOrden == (new wfiplib.admTramite()).getEtapaMesaCalidad(oTramiteMesa.IdFlujo) && oTramiteMesa.Procede == -100)
                        {
                            oTramiteMesa.Procede = -1;
                        }

                        if (oTramiteMesa.Procede != -1)
                        {
                            if (oTramiteMesa.AnswerTo != -1)
                            {
                                //if (!oAdmTramiteMesa.tieneApoyosPendientes(oTramiteMesa.IdTramite, oTramiteMesa.Procede))

                                // Corrección de envío entre mesas...
                                int idProcede = oAdmTramiteMesa.getMesaFromProcede(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, oTramiteMesa.Procede);
                                int idMesaToAnswer = oAdmTramiteMesa.getMesaFromToAnswer(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, oTramiteMesa.Procede);
                                //oAdmTramiteMesa.reinicia(oTramiteMesa.IdTramite, oTramiteMesa.Procede, wfiplib.E_EstadoMesa.ResponseFromMesa, -1, -1);
                                oAdmTramiteMesa.reinicia(oTramiteMesa.IdTramite, oTramiteMesa.Procede, wfiplib.E_EstadoMesa.ResponseFromMesa);
                            }
                            else
                            {
                                int idProcede = oAdmTramiteMesa.getMesaFromProcede(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, oTramiteMesa.Procede);
                                int idMesaToAnswer = oAdmTramiteMesa.getMesaFromToAnswer(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, oTramiteMesa.Procede);
                                if (!oAdmTramiteMesa.tieneApoyosPendientes(oTramiteMesa.IdTramite, oMesa.IdMesaPadre))
                                {
                                    if (statusMesa != E_EstadoMesa.CMCitaProgramada && statusMesa != E_EstadoMesa.CMEnEsperaResult)
                                    {
                                        oAdmTramiteMesa.reinicia(oTramiteMesa.IdTramite, oMesa.IdMesaPadre, wfiplib.E_EstadoMesa.ReingresoApoyo, idProcede, idMesaToAnswer);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Verificamos si la etapa esta finalizada....
                            if (!oAdmTramiteMesa.VerificaEtapa(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa))
                            {
                                // Verificamos el status de las mesas en la etapa
                                bool blnRegistrarPaso = true;
                                bool blnRechazo = false;
                                bool blnSuspencion = false;
                                bool blnHold = false;
                                bool blnPCI = false;

                                // procesar selección al verificar hold
                                if (
                                    (oMesa.Id == 2) || (oMesa.Id == 15) || (oMesa.Id == 29) ||
                                    (oMesa.Id == 3) || (oMesa.Id == 16) || (oMesa.Id == 30)
                                )
                                {
                                    // No realizamos ninguna acción
                                    registraSigMesaFlujoManual(oTramiteMesa.IdTramite, oMesa.Id);

                                }
                                else 
                                {
                                    if ((oMesa.Id == 4) || (oMesa.Id == 17) || (oMesa.Id == 31))
                                    {
                                        List<wfiplib.E_EstadoMesa> Lista = oAdmTramiteMesa.DaEstadosMismoNivel_manualSeleccion(oTramiteMesa.IdTramite, oMesa.IdFlujo, oMesa.IdEtapa);
                                        foreach (wfiplib.E_EstadoMesa var in Lista)
                                        {
                                            if (var == wfiplib.E_EstadoMesa.Rechazo) { blnRechazo = true; blnRegistrarPaso = false; }
                                            if (var == wfiplib.E_EstadoMesa.Suspendido) { blnSuspencion = true; blnRegistrarPaso = false; }
                                            if (var == wfiplib.E_EstadoMesa.Hold) { blnHold = true; blnRegistrarPaso = false; }
                                            if (var == wfiplib.E_EstadoMesa.PCI) { blnPCI = true; blnRegistrarPaso = false; }
                                            //if (var == wfiplib.E_EstadoMesa.CMRevisionProspecto) { conCMRevProspecto = true; blnRegistrarPaso = false; }
                                            //if (var == wfiplib.E_EstadoMesa.CMConfirmacionPendiente) { conCMConfirmacionPendiente = true; blnRegistrarPaso = false; }
                                            //if (var == wfiplib.E_EstadoMesa.SuspensionCitaMedica) { conSuspensionCitaMedica = true; blnRegistrarPaso = false; }
                                        }
                                    }
                                    else
                                    {
                                        List<wfiplib.E_EstadoMesa> ListaMesas = oAdmTramiteMesa.checkStatusEtapa(oTramiteMesa.IdTramite, oMesa.IdFlujo, oMesa.IdEtapa);
                                        foreach (wfiplib.E_EstadoMesa EstadoMesa in ListaMesas)
                                        {
                                            switch (EstadoMesa)
                                            {
                                                case wfiplib.E_EstadoMesa.Rechazo:
                                                    blnRegistrarPaso = false;
                                                    blnRechazo = true;
                                                    //(new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Rechazo);
                                                    break;

                                                case wfiplib.E_EstadoMesa.Suspendido:
                                                    blnRegistrarPaso = false;
                                                    blnSuspencion = true;
                                                    //(new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Suspendido);
                                                    break;

                                                case wfiplib.E_EstadoMesa.Hold:
                                                    blnRegistrarPaso = false;
                                                    blnHold = true;
                                                    //(new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Hold); ;
                                                    break;

                                                case wfiplib.E_EstadoMesa.PCI:
                                                    blnPCI = true;
                                                    blnRegistrarPaso = false;
                                                    //(new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.PCI);
                                                    break;

                                                    //default:
                                                    //    registraSigMesaFlujo(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                                                    //    break;
                                            }
                                        }
                                    }
                                    
                                    if (blnRechazo)
                                    {
                                        (new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Rechazo);
                                        GeneraCartaEstatusTramite(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Rechazo);
                                    }
                                    else
                                    {
                                        if (blnSuspencion)
                                        {
                                            (new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Suspendido);
                                            GeneraCartaEstatusTramite(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Suspendido);
                                        }
                                        else
                                        {
                                            if (blnHold)
                                            {
                                                (new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Hold);
                                                GeneraCartaEstatusTramite(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.Hold);
                                            }
                                            else
                                            {
                                                if (blnPCI)
                                                {
                                                    (new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.PCI);
                                                    //GeneraCartaEstatusTramite(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.PCI);
                                                }
                                            }
                                        }
                                    }

                                    if (blnRegistrarPaso)
                                    {
                                        registraSigMesaFlujo(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, statusMesa);
                                    }
                                }
                            }
                        }
                    }

                    validaEnLinea();
                }
            }
            catch (Exception ex)
            {
                // mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
                Console.WriteLine(ex.Message);
            }
        }

        protected bool regresaTramiteMesa(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            wfiplib.mesa oMesaActual = (new wfiplib.admMesa()).carga(pIdMesa);

            wfiplib.tramiteMesa siguiente = new wfiplib.tramiteMesa();
            siguiente.IdTramite = oTramite.Id;
            siguiente.IdFlujo = oTramite.IdFlujo;
            siguiente.IdTipoTramite = oTramite.IdTipoTramite;

            // Verificamos si el apoyo solicitado ya fue resuelto.
            List<wfiplib.mesa> ListMesasApoyo = (new wfiplib.admMesa()).getMesasApoyo(oTramite.IdFlujo, Convert.ToInt32(oTramite.IdTipoTramite), oMesaActual.Id);
            if (ListMesasApoyo.Count > 0)
            {
                wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                //foreach (wfiplib.mesa MesaApoyo in ListMesasApoyo)
                //{
                //    xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                //}
            }
            return resultado;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ContenidoSeleccion.Text = "";
            if (AceptarSeleccion())
            {
                ProcesarTramite(wfiplib.E_EstadoTramite.Proceso, wfiplib.E_EstadoMesa.Procesado);
            }
            else
            {
                ContenidoSeleccion.Text = "Se requiere al menos un registro en la tabla de beneficiarios";
            }    
        }

        private bool registraSigMesaFlujoManual(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
            bool blnTramiteCancelado = VerificaTramiteCancelado(pIdTramite);

            if (!blnTramiteCancelado)
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
                wfiplib.mesa oMesaActual = (new wfiplib.admMesa()).carga(pIdMesa);

                wfiplib.tramiteMesa siguiente = new wfiplib.tramiteMesa();
                siguiente.IdTramite = oTramite.Id;
                siguiente.IdFlujo = oTramite.IdFlujo;
                siguiente.IdTipoTramite = oTramite.IdTipoTramite;

                List<wfiplib.mesa> lstSigMesa = (new wfiplib.admMesa()).daSiguienteMesa(oTramite.IdFlujo, oMesaActual.EtapaOrden, oTramite.Id);
                if (lstSigMesa.Count > 0)
                {
                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                    foreach (wfiplib.mesa SiguienteMesa in lstSigMesa)
                    {
                        bool blnRegistrarMesa = true;
                        siguiente.IdMesa = SiguienteMesa.Id;
                        if (SiguienteMesa.ConCondicion == wfiplib.E_Estado.Activo)
                        {
                            blnRegistrarMesa = ActivarMesaCondicionada(oTramite, SiguienteMesa.Id, SiguienteMesa.Nombre);
                            if (SiguienteMesa.Nombre == "SELECCIÓN" && oTramite.CPDES == "True" && oTramite.EstatusCPDES == "Sub-aceptado")
                            {
                                oAdmTramiteMesa.cambiaEstado(pIdTramite, siguiente.IdMesa, wfiplib.E_EstadoMesa.CPDES, "Trámite CPDES", "Trámite CPDES");
                            }
                        }

                        if (blnRegistrarMesa)
                        {
                            if (
                                (siguiente.IdMesa == 4) || (siguiente.IdMesa == 17) || (siguiente.IdMesa == 31)
                            )
                            {
                                int statusMesaSeleccion = (new wfiplib.admMesa()).getStatusMesaSeleccion(oTramite.Id, siguiente.IdMesa);

                                //verificamos si la mesa de selección ya se encuentra procesada...
                                //si es así , deberemos crear la siguiente mesa...
                                if (statusMesaSeleccion == 8)
                                {
                                    oMesaActual = (new wfiplib.admMesa()).carga(siguiente.IdMesa);
                                    List<wfiplib.mesa> lstSigMesaSeleccion = (new wfiplib.admMesa()).daSiguienteMesa(oTramite.IdFlujo, oMesaActual.EtapaOrden, oTramite.Id);
                                    foreach (wfiplib.mesa SiguienteMesa_seleccion in lstSigMesaSeleccion)
                                    {
                                        wfiplib.tramiteMesa siguienteseleccion = new wfiplib.tramiteMesa();
                                        siguienteseleccion.IdTramite = oTramite.Id;
                                        siguienteseleccion.IdFlujo = oTramite.IdFlujo;
                                        siguienteseleccion.IdTipoTramite = oTramite.IdTipoTramite;
                                        siguienteseleccion.IdMesa = SiguienteMesa_seleccion.Id;
                                        resultado = oAdmTramiteMesa.registra(siguienteseleccion);
                                    }
                                }
                                else
                                {
                                    resultado = oAdmTramiteMesa.registra(siguiente);
                                }
                            }
                            else
                            {
                                resultado = oAdmTramiteMesa.registra(siguiente);
                                if (SiguienteMesa.Nombre == "SELECCIÓN" && oTramite.CPDES == "True" && oTramite.EstatusCPDES == "Sub-aceptado")
                                {
                                    oAdmTramiteMesa.cambiaEstado(pIdTramite, siguiente.IdMesa, wfiplib.E_EstadoMesa.CPDES, "Trámite CPDES", "Trámite CPDES");
                                }
                            }
                        }
                    }
                }
                else
                {
                    //Última Mesa actualiza el estado al Tramite
                    if (oMesaActual.Id != 21)
                    {
                        (new wfiplib.admTramite()).cambiaEstado(oTramite.Id, wfiplib.E_EstadoTramite.Ejecucion);
                    }
                }
            }
            return resultado;
        }

        private bool registraSigMesaFlujo(int pIdTramite, int pIdMesa, wfiplib.E_EstadoMesa statusMesa)
        {
            bool resultado = false;
            bool blnTramiteCancelado = VerificaTramiteCancelado(pIdTramite);

            if (!blnTramiteCancelado)
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
                wfiplib.mesa oMesaActual = (new wfiplib.admMesa()).carga(pIdMesa);

                wfiplib.tramiteMesa siguiente = new wfiplib.tramiteMesa();
                siguiente.IdTramite = oTramite.Id;
                siguiente.IdFlujo = oTramite.IdFlujo;
                siguiente.IdTipoTramite = oTramite.IdTipoTramite;

                if (statusMesa == E_EstadoMesa.CMCancelada && oMesaActual.Nombre == "RECEPCIÓN CITAS MEDICAS")
                {
                    oMesaActual.EtapaOrden = 0;
                }

                List<wfiplib.mesa> lstSigMesa = (new wfiplib.admMesa()).daSiguienteMesa(oTramite.IdFlujo, oMesaActual.EtapaOrden, oTramite.Id);
                if (lstSigMesa.Count > 0)
                {
                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                    foreach (wfiplib.mesa SiguienteMesa in lstSigMesa)
                    {
                        bool blnRegistrarMesa = true;
                        siguiente.IdMesa = SiguienteMesa.Id;
                        if (SiguienteMesa.ConCondicion == wfiplib.E_Estado.Activo)
                        {
                            blnRegistrarMesa = ActivarMesaCondicionada(oTramite, SiguienteMesa.Id, SiguienteMesa.Nombre);
                            if (SiguienteMesa.Nombre == "SELECCIÓN" && oTramite.CPDES == "True" && oTramite.EstatusCPDES == "Sub-aceptado")
                            {
                                oAdmTramiteMesa.cambiaEstado(pIdTramite, siguiente.IdMesa, wfiplib.E_EstadoMesa.CPDES, "Trámite CPDES", "Trámite CPDES");
                            }
                        }

                        if (blnRegistrarMesa)
                        {
                            resultado = oAdmTramiteMesa.registra(siguiente);
                            if (SiguienteMesa.Nombre == "SELECCIÓN" && oTramite.CPDES == "True" && oTramite.EstatusCPDES == "Sub-aceptado")
                            {
                                oAdmTramiteMesa.cambiaEstado(pIdTramite, siguiente.IdMesa, wfiplib.E_EstadoMesa.CPDES, "Trámite CPDES", "Trámite CPDES");
                            }
                        }
                    }
                }
                else
                {
                    //Última Mesa actualiza el estado al Tramite
                    if (oMesaActual.Id != 21)
                    {
                        (new wfiplib.admTramite()).cambiaEstado(oTramite.Id, wfiplib.E_EstadoTramite.Ejecucion);
                    }
                }
            }
            return resultado;
        }

        /// <summary>
        ///  Procedimiento para validar si se procesará la mesa de manera automática o de manera especifíca.
        /// </summary>
        /// <param name="Tramite">Trámite</param>
        /// <param name="IdMesa">Id de la Mesa.</param>
        /// <param name="MesaNombre">Nombre de la mesa.</param>
        /// <returns></returns>
        private bool ActivarMesaCondicionada(wfiplib.tramiteP Tramite, int IdMesa, string MesaNombre)
        {
            bool blnResultado = false;

            switch (MesaNombre.ToUpper())
            {
                case "REVISIÓN PLAD":
                    if (Tramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionVidaCM || Tramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionVida || Tramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionGMM)
                    {
                        wfiplib.EmisionVG oEmisionGmm = (new wfiplib.admEmisionVG()).cargaCompleto(Tramite.Id);

                        // Verificamos si es una personal moral
                        if (oEmisionGmm.TipoPersona == wfiplib.E_TipoPersona.Moral)
                        {
                            // Activamos la mesa
                            blnResultado = true;
                        }
                        else
                        {
                            string strNacionalidad = "";
                            if (oEmisionGmm.Nacionalidad.Length > 0)
                                strNacionalidad = oEmisionGmm.Nacionalidad;
                            else if (oEmisionGmm.TitularNacionalidad.Length > 0)
                                strNacionalidad = oEmisionGmm.TitularNacionalidad;

                            if ((new wfiplib.admEmisionVG()).NacionalidadSancionada(strNacionalidad) != "NA")
                                blnResultado = true;
                        }
                    }
                    break;

                default:
                    blnResultado = false;
                    break;
            }
            return blnResultado;
        }

        protected void btnSendToMesa_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> lstSendToMesa = new List<int>();
                for (int i = 0; i <= (lsOpSendToMesa.Items.Count - 1); i++)
                {
                    if (lsOpSendToMesa.Items[i].Selected)
                    {
                        lstSendToMesa.Add(Convert.ToInt32(lsOpSendToMesa.Items[i].Value));
                        break;
                    }
                }

                if (lstSendToMesa.Count > 0)
                {
                    bool resultado = false;
                    wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();

                    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.SendToMesa, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                    {
                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                        wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(oTramiteMesa.IdTramite);
                        wfiplib.tramiteMesa siguiente = new wfiplib.tramiteMesa();
                        siguiente.IdTramite = oTramite.Id;
                        siguiente.IdFlujo = oTramite.IdFlujo;
                        siguiente.IdTipoTramite = oTramite.IdTipoTramite;

                        foreach (int idSendTo in lstSendToMesa)
                        {
                            siguiente.IdMesa = idSendTo;
                            //resultado = oAdmTramiteMesa.EnviaMesa(oTramiteMesa, oTramiteMesa.IdMesa, siguiente.IdMesa, manejo_sesion.Credencial.Id);
                            resultado = oAdmTramiteMesa.EnviaMesa(oTramiteMesa, oTramiteMesa.IdMesa, siguiente.IdMesa, -2);
                        }
                    }
                    if (resultado)
                    {
                        dvCajaBtnsRegular.Visible = true;
                        dvCajaBtnsApoyo.Visible = false;
                        validaEnLinea();
                    }
                }
            }
            catch (Exception ex)
            {
                // mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnReenviar_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> lstApoyo = new List<int>();
                for (int i = 0; i <= (lsChApoyo.Items.Count - 1); i++)
                {
                    if (lsChApoyo.Items[i].Selected)
                        lstApoyo.Add(Convert.ToInt32(lsChApoyo.Items[i].Value));
                }

                if (lstApoyo.Count > 0)
                {
                    bool resultado = false;
                    wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();

                    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.SolicitudApoyo, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                    {
                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                        wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(oTramiteMesa.IdTramite);
                        wfiplib.tramiteMesa siguiente = new wfiplib.tramiteMesa();
                        siguiente.IdTramite = oTramite.Id;
                        siguiente.IdFlujo = oTramite.IdFlujo;
                        siguiente.IdTipoTramite = oTramite.IdTipoTramite;

                        foreach (int idApoyo in lstApoyo)
                        {
                            siguiente.IdMesa = idApoyo;
                            resultado = oAdmTramiteMesa.registra(siguiente, oTramiteMesa.IdMesa, -1);
                        }
                    }
                    if (resultado)
                    {
                        dvCajaBtnsRegular.Visible = true;
                        dvCajaBtnsApoyo.Visible = false;
                        validaEnLinea();
                    }
                }
            }
            catch (Exception ex)
            {
                // mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnPausa_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
            //    if ((new wfiplib.admTramiteMesa()).cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.Pausa, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
            //    {
            //        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
            //        validaEnLinea();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
            //    Console.WriteLine(ex.Message);
            //}
        }

        public void registraBitacora(int pIdTramite, int pIdMesa)
        {
            wfiplib.tramiteMesa oTramiteMesa = (new wfiplib.admTramiteMesa()).carga(pIdTramite, pIdMesa);

            wfiplib.bitacora oBitacora = new wfiplib.bitacora();
            oBitacora.IdFlujo = oTramiteMesa.IdFlujo;
            oBitacora.IdTipoTramite = oTramiteMesa.IdTipoTramite;
            oBitacora.IdTramite = oTramiteMesa.IdTramite;
            oBitacora.IdMesa = oTramiteMesa.IdMesa;
            oBitacora.Usuario = oTramiteMesa.UsuarioNombre;
            oBitacora.IdUsuario = oTramiteMesa.IdUsuario;
            oBitacora.FechaInicio = oTramiteMesa.FechaInicio;
            oBitacora.Estado = oTramiteMesa.Estado;
            oBitacora.Observacion = oTramiteMesa.ObservacionPublica;
            oBitacora.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;

            (new wfiplib.admBitacora()).Nuevo(oBitacora);
        }

        private void validaEnLinea()
        {
            Session.Contents.Remove("nota");

            int IdMesa = Convert.ToInt32(Request.QueryString["tp"]);

            if (hdEnLinea.Value.Equals("1"))
            {
                if (!inicializaPantalla(IdMesa, 0))
                {
                    Session["tramiteActual"] = null;
                    if (Request.QueryString["flujo"] == null)
                    {
                        Response.Redirect("esperaOperacion.aspx?res=1", false);
                    }
                    else
                    {
                        Response.Redirect("esperaOperacion.aspx?flujo=" + Request.QueryString["flujo"] + "&res=1", false);
                    }
                }
                else /////  NUVA CONTINUIDAD 
                {
                    if (Convert.ToInt32(hdIdTramite.Value) > 0)
                    {
                        int IdTramite = Convert.ToInt32(hdIdTramite.Value);
                        int idTipoTramite = (new wfiplib.admUsuarioMesa()).DaIdTipoTramite(IdTramite);
                        Response.Redirect("consultaTramite2.aspx?flujo=" + idTipoTramite + "&tp=" + hdIdMesa.Value.ToString(), true);
                    }
                    else
                    {
                        Session["tramiteActual"] = null;
                        Response.Redirect("esperaOperacion?flujo=" + Request.QueryString["flujo"] + ".aspx", true);
                    }
                }
            }
            else
            {
                Session["tramiteActual"] = null;
                Response.Redirect("esperaOperacion?flujo=" + Request.QueryString["flujo"] + ".aspx", true);
            }
        }

        protected void lnkModificar_Click(object sender, EventArgs e)
        {
            int idMesa = Convert.ToInt32(Request.QueryString["tp"]);
            Session["nota"] = txComentarios.Text.Trim();
            Response.Redirect("anexoDocumento.aspx?tp=" + idMesa.ToString() + "&id=" + hdIdTramite.Value);
        }

        protected void btnBuscaFilio_Click(object sender, EventArgs e)
        {
            lbNoExiste.Visible = false;
            lbNoExiste.Text = "";
            pnlGrdResultadosBusca.Visible = false;
            pnlDatosTramiteBusca.Visible = false;
            if (!string.IsNullOrEmpty(txNombreABuscar.Text) ||
                !string.IsNullOrEmpty(txtBuscarFolio.Text) ||
                !string.IsNullOrEmpty(txtBuscarRFC.Text)
               )
            {
                List<wfiplib.tramiteP> lstTramites = (new wfiplib.admTramite()).buscaEnDatosHtml(txNombreABuscar.Text.Trim(), txtBuscarRFC.Text.Trim(), txtBuscarFolio.Text.Trim());
                if (lstTramites.Count > 0)
                {
                    rptTrmResBusca.DataSource = lstTramites;
                    rptTrmResBusca.DataBind();
                    pnlGrdResultadosBusca.Visible = true;



                    string script = "";
                    script = "$('#tblTrmResBusca').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                }
                else
                {
                    lbNoExiste.Visible = true;
                    lbNoExiste.Text = "Inexistente";
                }
            }
            else
            {
                Mensajes mensajes = new Mensajes();
                mensajes.MostrarMensaje(this, "Debe establecer al menos un críterio de búsqueda.");
                lbNoExiste.Visible = true;
                lbNoExiste.Text = "Debe establecer al menos un críterio de búsqueda.";
            }
        }

        private void pintaDatosBusca(string pIdTramite)
        {
            int idTramite = Convert.ToInt32(pIdTramite);

            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(idTramite);
            lbFolioBusca.Text = oTramite.FolioCompuesto;  //oTramite.Id.ToString().PadLeft(5, '0');
            lbFechaRegistroBusca.Text = oTramite.FechaRegistro.ToString();
            lbTramiteNombreBusca.Text = oTramite.TramiteNombre;
            
            List<wfiplib.bitacora> lsBitacora = (new wfiplib.admBitacora()).daLista(idTramite);
            ltBitacoraBusca.Text = "";
            foreach (wfiplib.bitacora oBitacora in lsBitacora)
            {
                ltBitacoraBusca.Text = ltBitacoraBusca.Text + oBitacora.TextoHtmlPrivado2;
            }

            MuestraPDFAdicional(oTramite.Id, ltDocumentoBusca);
            pnlDatosTramiteBusca.Visible = true;

            string script = "";
            script = "$('#tblTrmResBusca').DataTable({'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'},scrollY: '400px',scrollX: true,scrollCollapse: true, fixedColumns: true,dom: 'Blfrtip', buttons: [{ extend: 'copy', className: 'btn-sm'}, {extend: 'csv', className: 'btn-sm'}, {extend: 'excel', className: 'btn-sm'}, {extend: 'pdfHtml5', className: 'btn-sm'}, {extend: 'print', className: 'btn-sm'}]}); ";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

        }

        private void MuestraPDF(wfiplib.E_TipoTramite IdTipoTramite, int pIdTramite, string mesaNombre)
        {

            if (IdTipoTramite == wfiplib.E_TipoTramite.InsPrivadoServicios)
            {
                //Obtener los datos del registro
                wfiplib.Tablas.Institucional.Privado.InsServicios insservicios = new wfiplib.Tablas.Institucional.Privado.InsServicios();
                wfiplib.Tablas.Institucional.Privado.InsServicioDetalle insserviciodetalle = new wfiplib.Tablas.Institucional.Privado.InsServicioDetalle();

                var fila = insservicios.ObtenerServicio(pIdTramite);

                litEncabezado.Text = "<b>Producto:</b> " + fila[1].ToString() + "<br />" +
                    "<b>Ramo:</b> " + fila[2].ToString() + "<br />" +
                    "<b>Clave Promotoría:</b> " + fila[3].ToString() + "<br />" +
                    "<b>Región:</b> " + fila[4].ToString() + "<br />" +
                    "<b>Agente:</b> " + fila[5].ToString() + "<br />" +
                    "<b>Subdirección:</b> " + fila[6].ToString() + "<br />" +
                    "<b>No. Poliza:</b> " + fila[7].ToString() + "<br />" +
                    "<b>Gerente Comercial:</b> " + fila[8].ToString() + "<br />" +
                    "<b>No. Orden:</b> " + fila[9].ToString() + "<br />" +
                    "<b>Ejecutivo Comercial:</b> " + fila[10].ToString() + "<br />" +
                    "<b>Contratante:</b> " + fila[11].ToString() + "<br />" +
                    "<b>Fecha de Solicitud:</b> " + fila[12].ToString() + "<br />" +
                    "<b>Observaciones:</b> " + fila[13].ToString();

                Repeater1.DataSource = insserviciodetalle.ObtenerServiciosDetalle(int.Parse(fila[1].ToString()));
                Repeater1.DataBind();
            }
            else
            {
                string strDoctoWeb = "";
                string strDoctoServer = "";
                wfiplib.expediente ArchivoFusion = null;
                try
                {
                    if (mesaNombre == "KWIK")
                    {
                        ArchivoFusion = (new wfiplib.admExpediente()).daFusion_sinTiempo(Convert.ToInt32(hdIdTramite.Value));
                    }
                    else
                    { 
                        ArchivoFusion = (new wfiplib.admExpediente()).daFusion(Convert.ToInt32(hdIdTramite.Value));
                    }

                    if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
                    {
                        strDoctoWeb = ""; // "..\\DocsUp\\" + ArchivoFusion.NmArchivo;
                        strDoctoServer = Server.MapPath("~"); // + "\\DocsUp\\" + ArchivoFusion.NmArchivo;
                        if (File.Exists(strDoctoServer))
                        {
                            Session["consulta_docPop"] = strDoctoWeb;
                        }
                        else
                        {
                            strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
                            Session["consulta_docPop"] = strDoctoWeb;
                        }
                    }
                    else
                    {
                        //TODO: Crear un documento que indique que el archivo no se fuciono o no se cargo desde el inicio del tramite
                        strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
                        Session["consulta_docPop"] = strDoctoWeb;
                    }
                    ltMuestraPdf.Text = "";
                    ltMuestraPdf.Text = "<embed type='application/pdf' height='100%' width='100%' src='" + strDoctoWeb + "'></embed>";
                    // ltMuestraPdf.Text = "<iframe src='" + strDoctoWeb + "' width='100%' height='100%' />";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void MuestraPDFAdicional(int pIdTramite, Literal pLtDocumentoPintar)
        {
            string strDoctoWeb = "";
            string strDoctoServer = "";
            try
            {
                wfiplib.expediente ArchivoFusion = (new wfiplib.admExpediente()).daFusion(Convert.ToInt32(pIdTramite));
                if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
                {

                    strDoctoWeb = ""; // "..\\DocsUp\\" + ArchivoFusion.NmArchivo;
                    strDoctoServer = Server.MapPath("~"); // + "\\DocsUp\\" + ArchivoFusion.NmArchivo;
                    if (File.Exists(strDoctoServer))
                    {
                        Session["consulta_docPop"] = strDoctoWeb;
                    }
                    else
                    {
                        strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
                        Session["consulta_docPop"] = strDoctoWeb;
                    }
                }
                else
                {
                    strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
                    Session["consulta_docPop"] = strDoctoWeb;
                }
                pLtDocumentoPintar.Text = "";
                pLtDocumentoPintar.Text = "<embed type='application/pdf' height='100%' width='100%' src='" + strDoctoWeb + "'></embed>";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void rptTrmResBusca_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                if (e.CommandName.Equals("abreResBusca"))
                {
                    pintaDatosBusca(e.CommandArgument.ToString());
                }
            }
        }

        private void pintaInsumos()
        {
            int pIdTramite = Convert.ToInt32(hdIdTramite.Value);
            List<wfiplib.insumos> LstArchInsumos = new List<wfiplib.insumos>();
            LstArchInsumos = (new wfiplib.admInsumos()).daLista(pIdTramite);
            if (LstArchInsumos.Count > 0)
            {
                rptInsumos.DataSource = LstArchInsumos;
                rptInsumos.DataBind();
                btnMuestraInsumos.Visible = true;
            }
            else
            {
                rptInsumos.DataSource = null;
                rptInsumos.DataBind();
                btnMuestraInsumos.Visible = false;
            }
        }

        protected void rptInsumos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    wfiplib.insumos oInsumo = (wfiplib.insumos)(e.Item.DataItem);
                    // string archOrigen = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, oInsumo.Id) + oInsumo.NmArchivo;
                    // string archDestino = Properties.Settings.Default.dirDoctosWeb + oInsumo.NmArchivo;
                    string archDestino = Server.MapPath("~") + (new wfiplib.insumos()).CarpetaInicial + "\\" + oInsumo.NmArchivo;
                    if (File.Exists(archDestino))
                    {
                        //    File.Copy(archOrigen, archDestino);
                        ImageButton btnExp = (ImageButton)(e.Item.FindControl("ImgExp"));
                        string strLinkDownload = "../" + (new wfiplib.insumos()).CarpetaInicial + "/" + oInsumo.NmArchivo;
                        // btnExp.OnClientClick = "Descarga('" + strLinkDownload + "'); return false;";
                        //btnExp.OnClientClick = "Descarga('" + oInsumo.NmArchivo + "'); return false;";

                    }
                    // ImageButton btnExp = (ImageButton)(e.Item.FindControl("ImgExp"));
                    // btnExp.OnClientClick = "Descarga('" + Properties.Settings.Default.urlDoctosWeb + oInsumo.NmArchivo + "'); return false;";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void llenaAntecedentes(int pIdTramite)
        {
            string RfcABuscar = "";
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    wfiplib.serviciosVidaP oServiciosVida = (new wfiplib.admServiciosVida()).carga(pIdTramite);
                    RfcABuscar = oServiciosVida.RFC;
                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    wfiplib.ServicioGmm oServiciosGmm = (new wfiplib.admServicioGmm()).carga(pIdTramite);
                    RfcABuscar = oServiciosGmm.RFC;
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(RfcABuscar))
            {
                DataTable rfcEncontrados = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedente(RfcABuscar, pIdTramite);
                if (rfcEncontrados.Rows.Count > 0)
                {
                    rpt_Antecedentes.DataSource = rfcEncontrados;
                    rpt_Antecedentes.DataBind();
                    pnlTabAntecedentes.Enabled = true;
                }
                else pnlTabAntecedentes.Enabled = false;
            }
        }

        protected void rpt_Antecedentes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                if (e.CommandName.Equals("abreAntecedente"))
                {
                    pintaDatosAntecedente(e.CommandArgument.ToString());
                }
            }
        }

        private void pintaDatosAntecedente(string pIdTramite)
        {
            int idTramite = Convert.ToInt32(pIdTramite);

            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(idTramite);
            lbFolioAntecedente.Text = oTramite.Id.ToString();
            lbFechaRegistroAntecedente.Text = oTramite.FechaRegistro.ToString();
            lbTramiteNombreAntecedente.Text = oTramite.TramiteNombre;

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    wfiplib.serviciosVidaP oServiciosVida = (new wfiplib.admServiciosVida()).carga(idTramite);
                    ltDatosContratanteAntecedente.Text = oServiciosVida.TextoHtml;
                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    wfiplib.ServicioGmm oServiciosGmm = (new wfiplib.admServicioGmm()).carga(idTramite);
                    ltDatosContratanteAntecedente.Text = oServiciosGmm.TextoHtml;
                    break;
                default:
                    break;
            }

            List<wfiplib.bitacora> lsBitacora = (new wfiplib.admBitacora()).daLista(idTramite);
            ltBitacoraAntecedente.Text = "";
            foreach (wfiplib.bitacora oBitacora in lsBitacora)
            {
                ltBitacoraAntecedente.Text = ltBitacoraAntecedente.Text + oBitacora.TextoHtml;
            }
            MuestraPDFAdicional(oTramite.Id, ltDocumentoAntecedente);
            pnlDatosAntededente.Visible = true;
        }

        protected void btnCtrlAceptaSelccion_Click(object sender, EventArgs e)
        {
            ContenidoSeleccion.Text = "";
            if (AceptarSeleccion())
            {
                ProcesarTramite(wfiplib.E_EstadoTramite.Proceso, wfiplib.E_EstadoMesa.Procesado);
            }
            else
            {
                ContenidoSeleccion.Text = "Se requiere al menos un registro en la tabla de beneficiarios";
            }
        }

        protected void btnCtrlPausarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                if ((new wfiplib.admTramiteMesa()).cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.Pausa, txtObservacionPausarTramite.Text, oTramiteMesa.ObservacionPrivada))
                {
                    registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                    validaEnLinea();
                }
            }
            catch (Exception ex)
            {
                // mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnCtrlAplicaHold_Click(object sender, EventArgs e)
        {
            // SE PONE EL TRÁMITE EN HOLD
            Mensajes mensajes = new Mensajes();
            int intMotivo = -1;
            var nodos = treeListHold.GetSelectedNodes();

            if (nodos.Count > 0)
            {
                wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                if (txtObservacionesHold.Visible)
                {
                    txComentarios.Text = txtObservacionesHold.Text.ToString();
                    oTramiteMesa.ObservacionPublica = txtObservacionesHold.Text.ToString();
                }
                else
                {
                    txComentarios.Text = "";
                    oTramiteMesa.ObservacionPublica = "Trámite puesto en HOLD.";
                }
                if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.Hold, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                {
                    wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                    wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();

                    oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                    oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                    oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                    oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                    oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                    oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.Hold;
                    oAdmTramiteRechazo.nuevo(oTramiteRechazo);

                    foreach (TreeListNode node in nodos)
                    {
                        intMotivo = Convert.ToInt32(node.GetValue("id"));
                        oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                        oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                    }

                    registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                    actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                    treeListCM.UnselectAll();
                    treeListCMRevProspecto.UnselectAll();
                    treeListCancelar.UnselectAll();
                    treeListSuspender.UnselectAll();
                    treeListRechazo.UnselectAll();
                    treeListHold.UnselectAll();
                    treeListCMCitaReprogramada.UnselectAll();

                    validaEnLinea();
                }
            }
            else
                mensajes.MostrarMensaje(this, "Debe Seleccionar Motivos de HOLD.");
        }

        protected void btnCtrlAplicaCM_Click(object sender, EventArgs e)
        {
            // SE ASIGNA EL TRÁMITE A STATUS DE CITA MÉDICA.
            Mensajes mensajes = new Mensajes();
            int intMotivo = -1;
            // RECUPERA OBSERVACIONES PUBLICAS Y PRIVADAS DEL MODAL
            wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
            wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
            if (txtObservacionesCM.Visible)
            {
                txComentarios.Text = txtObservacionesCM.Text.ToString();
                oTramiteMesa.ObservacionPublica = txtObservacionesCM.Text.ToString();
            }
            else
            {
                txComentarios.Text = "";
                oTramiteMesa.ObservacionPublica = "Trámite puesto en Cita Medica Adelantada.";
            }

            if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.SuspensionCitaMedica, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
            {
                registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();

                oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.Hold;
                oAdmTramiteRechazo.nuevo(oTramiteRechazo);

                // CREAR RECORRIDO DE NODOS CUANDO SE TENGA EL LISTADO DE MOTIVOS DE RECHAZO EN CITAS MEDICAS
                intMotivo = Convert.ToInt32(hfCadenaMotivosRechazo.Value);
                oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);

                
                // NUEVO REQUERIMIENTO 27-04-2019 
                // SE INGRESA A LA MESA DE CITAS MEDICAS EN PARALELO
                wfiplib.admTramiteMesa oAdmTramiteMesaCitaMedica = new wfiplib.admTramiteMesa();
                wfiplib.tramiteMesa tramiteMesaCitaMedica = new wfiplib.tramiteMesa();
                wfiplib.admTramite admTramiteCitamedica = new wfiplib.admTramite();
                int idMesa = 0;

                tramiteMesaCitaMedica.IdTramite = oTramiteMesa.IdTramite;
                // BUSCA EL ID DE LA MESA DE CITAS MEDICAS PERTENECIENTE AL TRAMITE
                DataTable datos = admTramiteCitamedica.ExisteTramiteMesaCitasMedicasFlujoTramite(oTramiteMesa.IdTramite);
                if (datos.Rows.Count > 0)
                {
                    foreach (DataRow row in datos.Rows)
                    {
                        idMesa = Convert.ToInt32(row["id"].ToString());
                        tramiteMesaCitaMedica.IdMesa = Convert.ToInt32(row["id"].ToString());
                    }
                }
                tramiteMesaCitaMedica.Estado = E_EstadoMesa.Registro;
                if (oAdmTramiteMesaCitaMedica.nuevo(tramiteMesaCitaMedica))
                {
                    // registraBitacora(oTramiteMesa.IdTramite, idMesa);
                }
                else
                {
                    mensajes.MostrarMensaje(this, "Error al enviar a citas medicas.");
                }


                treeListCM.UnselectAll();
                treeListCMRevProspecto.UnselectAll();
                treeListSuspender.UnselectAll();
                treeListHold.UnselectAll();
                treeListCMCitaReprogramada.UnselectAll();

                validaEnLinea();
            }
        }

        protected void BorrarExpediente(wfiplib.E_TipoTramite IdTipoTramite, int pIdTramite)
        {
            string strDoctoWeb = "";
            string strDoctoServer = "";
            try
            {
                wfiplib.expediente ArchivoFusion = (new wfiplib.admExpediente()).daFusion(Convert.ToInt32(hdIdTramite.Value));
                if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
                {
                    strDoctoWeb = ""; // "..\\DocsUp\\" + ArchivoFusion.NmArchivo;
                    strDoctoServer = Server.MapPath("~"); // + "\\DocsUp\\" + ArchivoFusion.NmArchivo;
                    if (File.Exists(strDoctoServer))
                    {
                        File.Delete(strDoctoServer);
                        File.Copy(Server.MapPath("~") + "\\DocsTemplate\\PCI.pdf", strDoctoServer);
                    }
                    else
                    {
                        strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
                        Session["consulta_docPop"] = strDoctoWeb;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnCtrlAplicaHoldD_Click(object sender, EventArgs e)
        {
            //mensajes.MostrarMensaje(this, "Aplicando HOLD DIRECTO.");
        }

        protected void btnCtrlAplicaPCI_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(hfCadenaMotivosRechazo.Value) && !string.IsNullOrEmpty(hfModoRechazo.Value))
            //    mensajes.MostrarMensaje(this, "Aplicando PCI C#");
            //else
            //    mensajes.MostrarMensaje(this, "Debes especificar los motivos del PCI");
        }

        protected void btnCtrlAplicaSuspencion_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfCadenaMotivosRechazo.Value) && !string.IsNullOrEmpty(hfModoRechazo.Value))
            {
                String[] motivos = hfCadenaMotivosRechazo.Value.Split(';');
                wfiplib.E_EstadoMesa estado = (wfiplib.E_EstadoMesa)Convert.ToInt32(hfModoRechazo.Value);
                try
                {
                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                    wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, estado, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                    {
                        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
                        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                        oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                        oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                        oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                        oTramiteRechazo.Estado = estado;
                        oAdmTramiteRechazo.nuevo(oTramiteRechazo);
                        oAdmTramiteRechazo.nuevoMotivos(oTramiteRechazo.Id, motivos);
                        oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);

                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                        actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                        validaEnLinea();
                    }
                }
                catch (Exception ex)
                {
                    //mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
                    Console.WriteLine(ex.Message);
                }
            }
            else
                mensajes.MostrarMensaje(this, "Debes especificar los motivos del Rechazo.");
        }

        protected void btnCtrlAplicaRechazo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfCadenaMotivosRechazo.Value) && !string.IsNullOrEmpty(hfModoRechazo.Value))
            {
                String[] motivos = hfCadenaMotivosRechazo.Value.Split(';');
                wfiplib.E_EstadoMesa estado = (wfiplib.E_EstadoMesa)Convert.ToInt32(hfModoRechazo.Value);
                try
                {
                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                    wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, estado, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                    {
                        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
                        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                        oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                        oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                        oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                        oTramiteRechazo.Estado = estado;
                        oAdmTramiteRechazo.nuevo(oTramiteRechazo);
                        oAdmTramiteRechazo.nuevoMotivos(oTramiteRechazo.Id, motivos);
                        oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);

                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                        actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                        validaEnLinea();
                    }
                }
                catch (Exception ex)
                {
                    //mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
                    Console.WriteLine(ex.Message);
                }
            }
            else
                mensajes.MostrarMensaje(this, "Debes especificar los motivos del Rechaxo");
        }


        public void btnCtrlAplicaCancelardDir_Click(object sender, EventArgs e)
        {
            // SE PONE EL TRÁMITE EN SUSPENCIÓN
            Mensajes mensajes = new Mensajes();
            int intMotivo = -1;
            var nodos = treeListCancelar.GetSelectedNodes();

            if (nodos.Count > 0)
            {
                try
                {
                    // OBJETOS DE MESA Y TRAMITE MESA
                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                    wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();

                    // REGISTRA OBSERVACIONES PUBLICAS Y PRIVADAS 
                    oTramiteMesa.ObservacionPrivada = txComentariosPrv.Text.Trim();
                    oTramiteMesa.ObservacionPublica = txObservacionesCancelacion.Text.Trim();

                    wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(hdIdTramite.Value));
                    if ((new wfiplib.admTramiteMesa()).CancelarTramite(oTramite, manejo_sesion.Credencial, E_EstadoTramite.Cancelado, oTramiteMesa))
                    {
                        
                        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();

                        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                        oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                        oTramiteRechazo.ObservacionPublica = txObservacionesCancelacion.Text.Trim();
                        oTramiteRechazo.ObservacionPrivada = txComentariosPrv.Text.Trim();
                        oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.PromotoriaCancela;
                        oAdmTramiteRechazo.nuevo(oTramiteRechazo);

                        // AGREGA MOTIVOS DE RECHAZO
                        foreach (TreeListNode node in nodos)
                        {
                            intMotivo = Convert.ToInt32(node.GetValue("id"));
                            oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                            oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                        }
                        
                        // MENSAJE DE EJECUAICON 
                        mensajes.MostrarMensaje(this, "El Trámite " + oTramite.FolioCompuesto + " se Canceló Correctamente.");
                        
                        // LIBERA DATOS SELECIONADOS
                        treeListCMRevProspecto.UnselectAll();
                        treeListCancelar.UnselectAll();
                        treeListSuspender.UnselectAll();
                        treeListRechazo.UnselectAll();
                        treeListHold.UnselectAll();
                        treeListCMCitaReprogramada.UnselectAll();

                        validaEnLinea();
                    }
                    else
                    {
                        mensajes.MostrarMensaje(this, "No se pudó cancelar el Trámite " + oTramite.FolioCompuesto + ".");
                    }
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }

            /*
            wfiplib.E_EstadoMesa estado = wfiplib.E_EstadoMesa.Rechazo;
            try
            {
                // OBJETOS DE MESA Y TRAMITE MESA
                wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();

                // REGISTRA OBSERVACIONES PUBLICAS Y PRIVADAS 
                oTramiteMesa.ObservacionPrivada = txComentariosPrv.Text.Trim();
                oTramiteMesa.ObservacionPublica = txObservacionesRechazo.Text.Trim();

                if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, estado, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                {
                    registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                    actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                    wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                    wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
                    oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                    oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                    oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                    oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                    oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                    oTramiteRechazo.Estado = estado;
                    oAdmTramiteRechazo.nuevo(oTramiteRechazo);
                    // oAdmTramiteRechazo.nuevoMotivos(oTramiteRechazo.Id, motivos);        NO HAY MOTIVOS DE RECHAZO
                    //oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);

                    // AGREGA MOTIVOS DE RECHAZO
                    foreach (TreeListNode node in nodos)
                    {
                        intMotivo = Convert.ToInt32(node.GetValue("id"));
                        oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                        oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                    }

                    // LIBERA DATOS SELECIONADOS
                    treeListCMRevProspecto.UnselectAll();
                    treeListCancelar.UnselectAll();
                    treeListSuspender.UnselectAll();
                    treeListRechazo.UnselectAll();
                    treeListHold.UnselectAll();
                    treeListCMCitaReprogramada.UnselectAll();

                    validaEnLinea();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            */
        }
            else
                mensajes.MostrarMensaje(this, "Debe Seleccionar Motivos de Cancelación.");
        }

        public void btnCtrlAplicaRechazodDir_Click(object sender, EventArgs e)
        {
            // SE PONE EL TRÁMITE EN SUSPENCIÓN
            Mensajes mensajes = new Mensajes();
            int intMotivo = -1;
            var nodos = treeListRechazo.GetSelectedNodes();

            if (nodos.Count > 0)
            {
                wfiplib.E_EstadoMesa estado = wfiplib.E_EstadoMesa.Rechazo;
                try
                {
                    // OBJETOS DE MESA Y TRAMITE MESA
                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                    wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                    
                    // REGISTRA OBSERVACIONES PUBLICAS Y PRIVADAS 
                    oTramiteMesa.ObservacionPrivada = txComentariosPrv.Text.Trim();
                    oTramiteMesa.ObservacionPublica = txObservacionesRechazo.Text.Trim();

                    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, estado, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                    {
                        

                        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
                        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                        oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                        oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                        oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                        oTramiteRechazo.Estado = estado;
                        oAdmTramiteRechazo.nuevo(oTramiteRechazo);
                        // oAdmTramiteRechazo.nuevoMotivos(oTramiteRechazo.Id, motivos);        NO HAY MOTIVOS DE RECHAZO
                        //oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);

                        // AGREGA MOTIVOS DE RECHAZO
                        foreach (TreeListNode node in nodos)
                        {
                            intMotivo = Convert.ToInt32(node.GetValue("id"));
                            oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                            oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                        }

                        // LIBERA DATOS SELECIONADOS
                        treeListCMRevProspecto.UnselectAll();
                        treeListCancelar.UnselectAll();
                        treeListSuspender.UnselectAll();
                        treeListRechazo.UnselectAll();
                        treeListHold.UnselectAll();
                        treeListCMCitaReprogramada.UnselectAll();

                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                        actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                        validaEnLinea();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            else
                mensajes.MostrarMensaje(this, "Debe Seleccionar Motivos de Suspención.");
            
        }

        protected void btnCtrlAplicaSuspenderDir_Click(object sender, EventArgs e)
        {
            // SE PONE EL TRÁMITE EN SUSPENCIÓN
            Mensajes mensajes = new Mensajes();
            int intMotivo = -1;
            var nodos = treeListSuspender.GetSelectedNodes();

            if (nodos.Count > 0)
            {
                wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();

                if (txtObservacionesSuspencion.Visible)
                {
                    txComentarios.Text = txtObservacionesHold.Text.ToString();
                    oTramiteMesa.ObservacionPublica = txtObservacionesSuspencion.Text.ToString();
                }
                else
                {
                    txComentarios.Text = "";
                    oTramiteMesa.ObservacionPublica = "Trámite puesto en Suspención.";
                }

                if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.Suspendido, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                {
                    wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                    wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();

                    oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                    oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                    oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                    oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                    oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                    oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.Hold;
                    oAdmTramiteRechazo.nuevo(oTramiteRechazo);

                    foreach (TreeListNode node in nodos)
                    {
                        intMotivo = Convert.ToInt32(node.GetValue("id"));
                        oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                        oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                    }

                    treeListCM.UnselectAll();
                    treeListCMRevProspecto.UnselectAll();
                    treeListCancelar.UnselectAll();
                    treeListSuspender.UnselectAll();
                    treeListRechazo.UnselectAll();
                    treeListHold.UnselectAll();
                    treeListCMCitaReprogramada.UnselectAll();

                    registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                    actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                    validaEnLinea();
                }
            }
            else
                mensajes.MostrarMensaje(this, "Debe Seleccionar Motivos de Suspención.");
        }

        /// <summary>
        /// Establece el status del Tramite en PCI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCtrlAplicaHoldDir_Click(object sender, EventArgs e)
        {
            try
            {
                String[] parametros = Session["tramiteActual"].ToString().Split(';');
                wfiplib.E_EstadoMesa estado = wfiplib.E_EstadoMesa.PCI;

                wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();

                // Obtenemos los Motivos PCI
                string strMotivos = (new wfiplib.admTramiteMesa()).getMotivosPCI(parametros[1], parametros[2]);
                if (strMotivos.Length == 0)
                {
                    strMotivos = "0";
                }
                String[] motivos = strMotivos.Split(';');

                if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, estado, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                {
                    wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                    wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
                    oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                    oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                    oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                    oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                    oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                    oTramiteRechazo.Estado = estado;
                    oAdmTramiteRechazo.nuevo(oTramiteRechazo);
                    oAdmTramiteRechazo.nuevoMotivos(oTramiteRechazo.Id, motivos);
                    oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                    BorrarExpediente(oTramiteMesa.IdTipoTramite, oTramiteMesa.IdTramite);

                    treeListCM.UnselectAll();
                    treeListCMRevProspecto.UnselectAll();
                    treeListCancelar.UnselectAll();
                    treeListSuspender.UnselectAll();
                    treeListRechazo.UnselectAll();
                    treeListHold.UnselectAll();
                    treeListCMCitaReprogramada.UnselectAll();

                    registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                    actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                    validaEnLinea();
                }
            }
            catch (Exception ex)
            {
                //mensajes.MostrarMensaje(this, "Ocurrio un error... Por favor contacte a soporte.");
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnCtrlAplicaCMRevProspecto_Click(object sender, EventArgs e)
        {
            Mensajes mensajes = new Mensajes();
            int intMotivo = -1;
            var nodos = treeListCMRevProspecto.GetSelectedNodes();

            if (nodos.Count > 0)
            {
                wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                txComentarios.Text = "";
                oTramiteMesa.ObservacionPublica = "Revisión con Prospecto.";


                if ((new wfiplib.admTramite()).cambiaEstado(int.Parse(hdIdTramite.Value.ToString()), wfiplib.E_EstadoTramite.CMRevProspecto))
                {
                    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.CMRevisionProspecto, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                    {
                        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();

                        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                        oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                        oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                        oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                        oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.CMRevisionProspecto;
                        oAdmTramiteRechazo.nuevo(oTramiteRechazo);

                        foreach (TreeListNode node in nodos)
                        {
                            intMotivo = Convert.ToInt32(node.GetValue("id"));
                            oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                            oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                        }

                        treeListCM.UnselectAll();
                        treeListCMRevProspecto.UnselectAll();
                        treeListCancelar.UnselectAll();
                        treeListSuspender.UnselectAll();
                        treeListRechazo.UnselectAll();
                        treeListHold.UnselectAll();
                        treeListCMCitaReprogramada.UnselectAll();

                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
                        //actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                        validaEnLinea();
                    }
                }
            }
            else
            { 
                mensajes.MostrarMensaje(this, "Debe Seleccionar Motivos de Revisión con Prospecto.");
            }
        }

        protected void btnCtrlAplicaCMCitaReprogramada_Click(object sender, EventArgs e)
        {
            treeListCM.UnselectAll();
            treeListCMRevProspecto.UnselectAll();
            treeListCancelar.UnselectAll();
            treeListSuspender.UnselectAll();
            treeListRechazo.UnselectAll();
            treeListHold.UnselectAll();
            treeListCMCitaReprogramada.UnselectAll();
        }

        protected void btnComplementario_Click(object sender, EventArgs e)
        {
            Mensajes mensajes = new Mensajes();
            try
            {
                wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();
                txComentarios.Text = "";
                oTramiteMesa.ObservacionPublica = "Confirmación Pendiente.";

                if ((new wfiplib.admTramite()).cambiaEstado(oTramiteMesa.IdTramite, wfiplib.E_EstadoTramite.CMConfirmacionPendiente))
                {
                    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, wfiplib.E_EstadoMesa.CMConfirmacionPendiente, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
                    {
                        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
                        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                        oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
                        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                        oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
                        oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                        oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.CMConfirmacionPendiente;
                        oAdmTramiteRechazo.nuevo(oTramiteRechazo);
                        enviarCorreoProveedor(oTramiteMesa.IdTramite);

                        treeListCM.UnselectAll();
                        treeListCMRevProspecto.UnselectAll();
                        treeListCancelar.UnselectAll();
                        treeListSuspender.UnselectAll();
                        treeListRechazo.UnselectAll();
                        treeListHold.UnselectAll();
                        treeListCMCitaReprogramada.UnselectAll();

                        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

                        validaEnLinea();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void enviarCorreoProveedor(int pIdTramite)
        {
            try
            {
                string strCorreoElectronico = (new wfiplib.admTramiteMesa()).GetTramiteCitaProveedorEMailContent(pIdTramite);
                string strEmailProveedor = (new wfiplib.admTramiteMesa()).GetTramiteCitaProveedorEMail(pIdTramite);
                wfiplib.Correo correo = new wfiplib.Correo();
                correo.ProcesarCorreo(strEmailProveedor, "wfo@asae.com.mx", "Confirmación de Citas", strCorreoElectronico);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void pnlCallbackMotPCIAsigna_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //String[] parametros = e.Parameter.Split(';');
            //wfiplib.E_EstadoMesa estado = wfiplib.E_EstadoMesa.PCI;
            //try
            //{
            //    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
            //    wfiplib.tramiteMesa oTramiteMesa = recuperaDatos();

            //    // Obtenemos los Motivos PCI
            //    String[] motivos = (new wfiplib.admTramiteMesa()).getMotivosPCI(parametros[0], parametros[1]).Split(';');

            //    if (oAdmTramiteMesa.cambiaEstado(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa, estado, oTramiteMesa.ObservacionPublica, oTramiteMesa.ObservacionPrivada))
            //    {
            //        registraBitacora(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);
            //        actualizaEstadoTramite(oTramiteMesa.IdTramite, oTramiteMesa.IdMesa);

            //        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
            //        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
            //        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
            //        oTramiteRechazo.IdTramiteMesa = oTramiteMesa.Id;
            //        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
            //        oTramiteRechazo.ObservacionPublica = oTramiteMesa.ObservacionPublica;
            //        oTramiteRechazo.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
            //        oTramiteRechazo.Estado = estado;
            //        oAdmTramiteRechazo.nuevo(oTramiteRechazo);
            //        oAdmTramiteRechazo.nuevoMotivos(oTramiteRechazo.Id, motivos);
            //        oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);

            //treeListCM.UnselectAll();
            //treeListCMRevProspecto.UnselectAll();
            //treeListSuspender.UnselectAll();
            //treeListHold.UnselectAll();
            //treeListCMCitaReprogramada.UnselectAll();

            //        validaEnLinea();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    enviaMsgCliente(ex.Message);
            //}
        }

        /*****************************************************************/
        /*****************************************************************/
        /************** Nuevos datos y validación a citas médicas ********/
        /*****************************************************************/
        /*****************************************************************/

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoContratante();
        }

        protected void TipoContratante()
        {
            pnPrsFisica.Visible = false;
            pnPrsMoral.Visible = false;
            InfoPrsFisica.Visible = false;
            InfoPrsMoral.Visible = false;

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                InfoPrsFisica.Visible = true;
                InfoPrsMoral.Visible = false;
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
                CheckBox1.Enabled = true;
                CheckBox2.Enabled = true;
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                pnPrsMoral.Visible = true;
                pnPrsFisica.Visible = false;
                CheckBox1.Checked = true;
                CheckB1();
                CheckBox1.Enabled = false;
                CheckBox2.Enabled = false;
                InfoPrsMoral.Visible = true;
                InfoPrsFisica.Visible = false;
            }
            else
            {
                pnPrsFisica.Visible = false;
                pnPrsMoral.Visible = false;
                InfoPrsFisica.Visible = false;
                InfoPrsMoral.Visible = false;
            }

            /*// ANTERIOR VALIDACION
            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                InfoPrsFisica.Visible = true;
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
                InfoPrsMoral.Visible = false;
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                pnPrsMoral.Visible = true;
                InfoPrsMoral.Visible = true;
                pnPrsFisica.Visible = false;
                InfoPrsFisica.Visible = false;
            }
            else
            {
                pnPrsFisica.Visible = false;
                pnPrsMoral.Visible = false;
                InfoPrsFisica.Visible = false;
                InfoPrsMoral.Visible = false;
            }
            */
            //lbNombreAgente.Text = daNombreDeAgente(hf_IdPromotoria.Value, txIdAgente.Text);
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked)
            {
                lblFolioCPDES.Enabled = false;
                EstatusCPDES.Enabled = false;
            }
            else
            {
                lblFolioCPDES.Enabled = true;
                EstatusCPDES.Enabled = true;
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

        protected void LisProducto1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LisSbproductos();
        }

        private void ListaProductos(string TramiteTipo)
        {
            DataTable listProductos = (new wfiplib.admEmisionVG()).cartgaCatProducto(TramiteTipo);
            LisProducto1.DataSource = listProductos;
            LisProducto1.DataBind();
            LisProducto1.DataTextField = "Nombre";
            LisProducto1.DataValueField = "IdCatProducto";
            LisProducto1.DataBind();
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
        }

        private void cargarMonedas(ref DropDownList objDDL)
        {
            DataTable dtMoneda = (new wfiplib.admEmisionVG()).cargaMonedas();
            objDDL.DataSource = dtMoneda;
            objDDL.DataTextField = "Nombre";
            objDDL.DataValueField = "IdMoneda";
            objDDL.DataBind();
            objDDL.SelectedIndex = 137;
        }

        protected void pnlCallbackMotRechazo_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            String[] parametros = e.Parameter.Split(';');
            List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
            //lstMotivosRechazo.DataSource = datos;
            //lstMotivosRechazo.ValueField = "Id";
            //lstMotivosRechazo.TextField = "Nombre";
            //lstMotivosRechazo.DataBind();
        }

        protected void pnlCallbackMotCMRevProspecto_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //String[] parametros = e.Parameter.Split(';');
            //List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
            //lstMotivosHold.DataSource = datos;
            //lstMotivosHold.ValueField = "Id";
            // lstMotivosHold.TextField = "Nombre";
            // lstMotivosHold.DataBind();
        }

        protected void pnlCallbackMotCMCitaReprogramada_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //String[] parametros = e.Parameter.Split(';');
            //List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
            //lstMotivosHold.DataSource = datos;
            //lstMotivosHold.ValueField = "Id";
            // lstMotivosHold.TextField = "Nombre";
            // lstMotivosHold.DataBind();
        }

        protected void pnlCallbackMotHold_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            String[] parametros = e.Parameter.Split(';');
            List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
            //lstMotivosHold.DataSource = datos;
            //lstMotivosHold.ValueField = "Id";
            // lstMotivosHold.TextField = "Nombre";
            // lstMotivosHold.DataBind();
        }

        protected void pnlCallbackMotCM_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            String[] parametros = e.Parameter.Split(';');
            List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
        }

        protected void pnlCallbackPopSigueTramiteStatus_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            String[] parametros = e.Parameter.Split(';');
            List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
            //lstMotivosHold.DataSource = datos;
            //lstMotivosHold.ValueField = "Id";
            // lstMotivosHold.TextField = "Nombre";
            // lstMotivosHold.DataBind();
        }

        protected void pnlCallbackPausaTramite(object sender, DevExpress.Web.CallbackEventArgs e)
        {
            String[] parametros = e.Parameter.Split(';');
            List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
        }

        protected void pnlCallbackMotSuspender_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            String[] parametros = e.Parameter.Split(';');
            List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
            //lstMotivosSuspender.DataSource = datos;
            // lstMotivosSuspender.ValueField = "Id";
            // lstMotivosSuspender.TextField = "Nombre";
            // lstMotivosSuspender.DataBind();
        }

        protected void pnlCallbackCancelar_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            String[] parametros = e.Parameter.Split(';');
            List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
            //lstMotivosSuspender.DataSource = datos;
            // lstMotivosSuspender.ValueField = "Id";
            // lstMotivosSuspender.TextField = "Nombre";
            // lstMotivosSuspender.DataBind();
        }

        protected void pnlCallbackMotPCI_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            String[] parametros = e.Parameter.Split(';');
            List<wfiplib.catMotivoRechazo> datos = (new wfiplib.admCatMotivoRechazo()).ListaSoloActivos(parametros[0], parametros[1], parametros[2]);
            //lstMotivosSuspender.DataSource = datos;
            //lstMotivosSuspender.ValueField = "Id";
            //lstMotivosSuspender.TextField = "Nombre";
            //lstMotivosSuspender.DataBind();
        }

        protected void treeList_CustomDataCallbackHold(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            e.Result = treeListHold.SelectionCount.ToString();
        }

        protected void treeList_CustomDataCallbackCM(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            e.Result = treeListCM.SelectionCount.ToString();
        }

        protected void treeList_CustomDataCallbackCMRevProspecto(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            e.Result = treeListCMRevProspecto.SelectionCount.ToString();
        }

        protected void treeList_CustomDataCallbackCMCitaReprogramada(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            e.Result = treeListCMCitaReprogramada.SelectionCount.ToString();
        }

        protected void treeList_DataBoundHold(object sender, EventArgs e)
        {
            SetNodeSelectionSettings();
        }

        protected void treeList_DataBoundCM(object sender, EventArgs e)
        {
            SetNodeSelectionSettings();
        }

        protected void treeList_DataBoundCMRevProspecto(object sender, EventArgs e)
        {
            SetNodeSelectionSettings();
        }

        protected void treeList_DataBoundCMCitaReprogramada(object sender, EventArgs e)
        {
            SetNodeSelectionSettings();
        }

        protected void treeList_CustomDataCallbackCancelar(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            e.Result = treeListCancelar.SelectionCount.ToString();
        }

        protected void treeList_DataBoundCancelar(object sender, EventArgs e)
        {
            SetNodeSelectionSettingsCancelar();
        }

        protected void treeList_CustomDataCallbackSuspender(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            e.Result = treeListSuspender.SelectionCount.ToString();
        }

        protected void treeList_DataBoundSuspender(object sender, EventArgs e)
        {
            SetNodeSelectionSettingsSus();
        }

        protected void treeList_CustomDataCallbackRechazar(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
            e.Result = treeListRechazo.SelectionCount.ToString();
        }

        protected void treeList_DataBoundRechazar(object sender, EventArgs e)
        {
            SetNodeSelectionSettingsRechazar();
        }

        private void SetNodeSelectionSettings()
        {
            TreeListNodeIterator iterator = treeListHold.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;
            }

        }

        private void SetNodeSelectionSettingsCancelar()
        {
            TreeListNodeIterator iterator = treeListCancelar.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;
            }

        }

        private void SetNodeSelectionSettingsSus()
        {
            TreeListNodeIterator iterator = treeListSuspender.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;
            }

        }

        private void SetNodeSelectionSettingsRechazar()
        {
            TreeListNodeIterator iterator = treeListRechazo.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;
            }

        }
        //===============================================================

        protected void pnlCallbackProcesaTramite_Callback(object sender, EventArgs e)
        {
#pragma warning disable CS0219 // La variable 'x' está asignada pero su valor nunca se usa
            var x = "x";
#pragma warning restore CS0219 // La variable 'x' está asignada pero su valor nunca se usa
        }

        protected void rptTramitesEspera_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (!string.IsNullOrEmpty(e.CommandName))
            //{
            //    Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
            //}
        }

        private void MuestraSeguimiento(int pIdTramite, int pIdMesa)
        {
            try
            {
                DataTable Datos = null;
                Datos = (new wfiplib.admTramite()).seguimientoTramiteBitacora(pIdTramite, pIdMesa);
                rptTramitesEspera.DataSource = Datos;
                rptTramitesEspera.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnMuestraPop1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath(Session["consulta_docPop"].ToString())))
            {
                Response.Write("<script>window.open('" + Session["consulta_docPop"].ToString().Replace("\\", "/") + "','_blank', 'width = 600, height = 400');</script>");
            }
        }
    }
}