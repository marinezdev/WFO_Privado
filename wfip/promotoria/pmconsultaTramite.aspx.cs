using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Data;
using iTextSharp.text.pdf;
using System.Globalization;
using DevExpress.Web.ASPxTreeList;
using wfiplib;
#pragma warning disable CS0105 // La directiva using para 'System.IO' aparece previamente en este espacio de nombres
using System.IO;
#pragma warning restore CS0105 // La directiva using para 'System.IO' aparece previamente en este espacio de nombres
using static iTextSharp.text.Font;
using iTextSharp.text;
using iTextSharp.text.html;
#pragma warning disable CS0105 // La directiva using para 'iTextSharp.text.pdf' aparece previamente en este espacio de nombres
using iTextSharp.text.pdf;
#pragma warning restore CS0105 // La directiva using para 'iTextSharp.text.pdf' aparece previamente en este espacio de nombres
using System.Configuration;

namespace wfip.promotoria
{
    public partial class pmconsultaTramite : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admTramiteMesa admTramiteMesa;
        wfiplib.admTramite admTramite;
        wfiplib.admMesa admMesa;
        wfiplib.admBitacora admBitacora;
        wfiplib.admServiciosVida admServiciosVida;
        wfiplib.admServicioGmm admServicioGmm;
        wfiplib.admEmisionVG admEmisionVG;
        wfiplib.admExpediente admExpediente;
        wfiplib.admTipoTramite admTipoTramite;
        wfiplib.admInsumos admInsumos;
        wfiplib.admDirectorio admDirectorio;

        /// <summary>
        /// Muestra un mensaje informativo al Usuario
        /// </summary>
        /// <param name="Mensaje">Información que se mostrará el Usuario.</param>
        private void showMessage(string Mensaje)
        {
            try
            {
                Mensajes message = new Mensajes();
                message.MostrarMensaje(this, Mensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int IdTramite = 0;
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

            if (urlCifrardo.Result)
            {
                DataTable dtMotivosCancelacion = new DataTable();
                dtMotivosCancelacion = (new wfiplib.Reportes()).ListaMotivosRechazo(-1, "Cancelación Promotoría");
                treeListCancelar.DataSource = dtMotivosCancelacion;
                treeListCancelar.DataBind();
                treeListCancelar.ExpandToLevel(1);

                if (!IsPostBack)
                {
                    //hdIdTramite.Value = Request.Params["Id"].ToString();
                    hdIdTramite.Value = urlCifrardo.IdTramite;
                    pintaDatos(Convert.ToInt32(hdIdTramite.Value));
                    MuestraPDF();
                    if (Session["nota"] != null)
                        txObservaciones.Text = Session["nota"].ToString();

                    txObservacionesReconsidera.Text = "";
                    btnReconsideracion.Visible = false;
                    btnRechazaPoliza.Visible = false;
                    btnAceptarPoliza.Visible = false;
                    btnAceptar.Visible = false;
                    btnModificar.Visible = false;
                    btnCancelar.Visible = false;

                    pnSuspensionCitaMedica.Visible = false;
                    btnModificarSNCitaMedica.Visible = false;
                    btnAceptarSNCitaMedica.Visible = false;

                    admTramite = new wfiplib.admTramite();
                    wfiplib.tramiteP oTramite = admTramite.carga(Convert.ToInt32(hdIdTramite.Value));

                    switch (oTramite.Estado)
                    {
                        case wfiplib.E_EstadoTramite.RevPromotoria:

                            if (admTramiteMesa.ReviewCalidad(Convert.ToInt32(hdIdTramite.Value)))
                            {
                                btnRechazaPoliza.Visible = true;
                            }
                            else
                            {
                                btnRechazaPoliza.Visible = false;
                            }
                            btnAceptarPoliza.Visible = true;

                            //btnCarta1.Visible = true;
                            //btnGeneraCarta.Visible = true;
                            btnAceptar.Visible = false;
                            btnModificar.Visible = false;
                            btnCancelar.Visible = false;
                            break;

                        case wfiplib.E_EstadoTramite.CMRevProspecto:
                            citamedica.Visible = true;
                            UpdatePanel1.Visible = false;
                            TextDireccion.Enabled = false;
                            CitaMedicaProspecto.Visible = true;
                            wfiplib.EmisionVG oEmisionPros = (new wfiplib.admEmisionVG()).cargaCompleto(Convert.ToInt32(hdIdTramite.Value));
                            CargarInformacionTramite(Convert.ToInt32(hdIdTramite.Value), oEmisionPros, oTramite.IdTipoTramite);
                            llenaDatos(Convert.ToInt32(hdIdTramite.Value));
                            /*
                            listCombos();
                            listEstados();
                            listCiudad();
                            lisLabHospital();
                            */
                            //CitasMedicasEvalucacion();
                            btnAceptar.CausesValidation = false;
                            btnAceptar.Visible = true;
                            btnCancelar.Visible = true;
                            break;

                        case wfiplib.E_EstadoTramite.InfoCitaMedica:
                            btnSuspencionCM.Visible = true;
                            citamedica.Visible = true;
                            CitaMedicaProspecto.Visible = false;
                            TextDireccion.Enabled = false;
                            wfiplib.EmisionVG oEmisionGmm = (new wfiplib.admEmisionVG()).cargaCompleto(Convert.ToInt32(hdIdTramite.Value));
                            CargarInformacionTramite(Convert.ToInt32(hdIdTramite.Value), oEmisionGmm, oTramite.IdTipoTramite);
                            llenaDatos(Convert.ToInt32(hdIdTramite.Value));

                            listCombos();
                            listEstados();
                            listCiudad();
                            lisLabHospital();

                            //CitasMedicasEvalucacion();
                            btnAceptar.CausesValidation = false;
                            btnAceptar.Visible = true;
                            btnCancelar.Visible = true;
                            break;

                        case wfiplib.E_EstadoTramite.PCI:
                            btnRechazaPoliza.Visible = false;
                            btnAceptarPoliza.Visible = false;
                            btnAceptar.Visible = true;
                            btnModificar.Visible = true;
                            btnCancelar.Visible = true;
                            lblPCI.Visible = true;
                            break;

                        case wfiplib.E_EstadoTramite.PromotoriaCancela:
                        case wfiplib.E_EstadoTramite.Caducado:
                            btnRechazaPoliza.Visible = false;
                            btnAceptarPoliza.Visible = false;
                            btnAceptar.Visible = true;
                            btnModificar.Visible = false;
                            btnCancelar.Visible = false;
                            break;
                        case wfiplib.E_EstadoTramite.Cancelado:
                            btnRechazaPoliza.Visible = false;
                            btnAceptarPoliza.Visible = false;
                            btnAceptar.Visible = true;
                            btnModificar.Visible = false;
                            btnCancelar.Visible = false;
                            btnGeneraCarta.Visible = true;
                            break;

                        case wfiplib.E_EstadoTramite.SuspensionCitaMedica:
                            pnSuspensionCitaMedica.Visible = true;
                            btnModificarSNCitaMedica.Visible = true;
                            btnAceptarSNCitaMedica.Visible = true;
                            btnCancelar.Visible = true;
                            btnGeneraCarta.Visible = false;
                            /*
                            btnRechazaPoliza.Visible = false;
                            btnAceptarPoliza.Visible = false;
                            btnAceptar.Visible = true;
                            btnModificar.Visible = true;
                            btnCancelar.Visible = true;
                            */
                            break;
                        case wfiplib.E_EstadoTramite.Rechazo:
                            btnRechazaPoliza.Visible = false;
                            btnAceptarPoliza.Visible = false;
                            btnAceptar.Visible = true;
                            btnModificar.Visible = false;
                            btnCancelar.Visible = false;
                            pnObsrMod.Visible = false;
                            if (!admTramite.existeTramiteMesaCancelado(Convert.ToInt32(hdIdTramite.Value), "CALIDAD"))
                            {
                                btnReconsideracion.Visible = true;
                            }
                            else
                            {
                                btnReconsideracion.Visible = false;
                            }
                            break;

                        default:
                            btnRechazaPoliza.Visible = false;
                            btnAceptarPoliza.Visible = false;
                            btnAceptar.Visible = true;
                            btnModificar.Visible = true;
                            btnCancelar.Visible = true;
                            break;
                    }

                    // El trámite no se puede cancelar si ya cuenta con un número de póliza.
                    string numPoliza = "";
                    if (oTramite.IdSisLegados != null)
                    {
                        numPoliza = oTramite.IdSisLegados.ToString();
                    }
                    if (numPoliza.Length > 0)
                    {
                        btnCancelar.OnClientClick = "";
                        btnNoCancelar.Visible = btnCancelar.Visible;
                        btnCancelar.Visible = false;
                    }
                    else
                    {
                        btnCancelar.OnClientClick = "CancelaTramite('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.PromotoriaCancela.ToString("d") + "'," + wfiplib.E_EstadoMesa.PromotoriaCancela.ToString("d") + ");" + " return false;";
                    }

                    btnReconsideracion.OnClientClick = "ReconsideraTramite('" + oTramite.IdFlujo.ToString() + ";" + oTramite.IdTipoTramite.ToString("d") + ";" + wfiplib.E_EstadoMesa.PromotoriaCancela.ToString("d") + "'," + wfiplib.E_EstadoMesa.PromotoriaCancela.ToString("d") + ");" + " return false;";
                }
                if (popAceptacion.IsCallback)
                    this.imprimeCartaAceptacion();

                if (popCartaObv.IsCallback)
                    this.imprimeCartaHold();

                if (popCartaRechazo.IsCallback)
                    this.imprimeCartaRechazo();

                if (PopSuspension.IsCallback)
                    this.imprimeCartaSuspension();

                if (PopInsumos.IsCallback)
                    pintaInsumos();

                if (popDocumento.IsCallback)
                {
                    string ruta = Server.MapPath("..");
                    int idTramite = Convert.ToInt32(hdIdTramite.Value);
                    DataTable datos = new DataTable();
                    string carta = string.Empty;
                    string cartaRequisitada = string.Empty;
                    datos = (new wfiplib.Reportes()).DatosCarta(idTramite);
                    string estado = datos.Rows[0]["Estado"].ToString();
                    switch (estado)
                    {
                        case "2": //Hold
                                  // Response.Write("<script type = 'text/javascript' > window.open('CartaPDFHold.aspx?Id=" + Request.Params["Id"].ToString() + "', '_blank', 'PDF MetLife', 'width=400,height=500')</script >;");

                            carta = ruta + "\\DocsTemplate\\Carta_HoldE.pdf";
                            cartaRequisitada = ruta + "\\DocsTemplate\\cartaRequisitadaH_" + idTramite + ".pdf";
                            Stream fileH = new FileStream(cartaRequisitada, FileMode.Create);
                            llenarPDF(carta, fileH, datos, estado);
                            Literal1.Text = "<embed src='..\\DocsTemplate\\cartaRequisitadaH_" + idTramite + ".pdf' style='width:100%; height:100%' type='application/pdf'>";
                            break;

                        case "4": //rechazo
                            carta = ruta + "\\DocsTemplate\\Carta_Rechazo_Emision_Final_8.pdf";
                            cartaRequisitada = ruta + "\\DocsTemplate\\cartaRequisitadaR_" + idTramite + ".pdf";
                            Stream fileR = new FileStream(cartaRequisitada, FileMode.Create);
                            llenarPDF(carta, fileR, datos, estado);
                            Literal1.Text = "<embed src='..\\DocsTemplate\\cartaRequisitadaR_" + idTramite + ".pdf' style='width:100%; height:100%' type='application/pdf'>";
                            break;

                        case "5": //Suspendido
                            carta = ruta + "\\DocsTemplate\\Carta_Suspension_Final_8.pdf";
                            cartaRequisitada = ruta + "\\DocsTemplate\\cartaRequisitadaS_" + idTramite + ".pdf";
                            Stream fileS = new FileStream(cartaRequisitada, FileMode.Create);
                            llenarPDF(carta, fileS, datos, estado);
                            Literal1.Text = "<embed src='..\\DocsTemplate\\cartaRequisitadaS_" + idTramite + ".pdf' style='width:100%; height:100%' type='application/pdf'>";
                            break;

                        case "12": //Carta Aceptacion
                            carta = ruta + "\\DocsTemplate\\carta_aceptacion.pdf";
                            cartaRequisitada = ruta + "\\DocsTemplate\\cartaRequisitadaA_" + idTramite + ".pdf";
                            Stream fileA = new FileStream(cartaRequisitada, FileMode.Create);
                            llenarPDF(carta, fileA, datos, estado);
                            Literal1.Text = "<embed src='..\\DocsTemplate\\cartaRequisitadaA_" + idTramite + ".pdf' style='width:100%; height:100%' type='application/pdf'>";
                            break;
                    }
                }






            }
            else
            {
                string script = "";
                script = "window.location.href='esperaPromotoria.aspx'; ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnGeneraCarta_Click(object sender, EventArgs e)
        {
            int idTramite = Convert.ToInt32(hdIdTramite.Value);
            DataTable datos = new DataTable();
            string carta = string.Empty;
            string cartaRequisitada = string.Empty;
            datos = (new wfiplib.Reportes()).DatosCarta(idTramite);
            wfiplib.E_EstadoTramite statusTramite = (wfiplib.E_EstadoTramite)Enum.Parse(typeof(wfiplib.E_EstadoTramite), datos.Rows[0]["Estado"].ToString(), true);

            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            string Encrypt = "?data=" + (new Application.Operacion.UrlCifrardo()).Encrypt("Id=" + urlCifrardo.IdTramite);

            switch (statusTramite)
            {
                case E_EstadoTramite.Hold:
                    Response.Write("<script type = 'text/javascript' > window.open('CartaPDFHold.aspx" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    //Response.Write("<script type = 'text/javascript' > window.open('CartaPDFHold.aspx?Id=" + Request.Params["Id"].ToString() + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;

                case E_EstadoTramite.Suspendido:
                    Response.Write("<script type = 'text/javascript' > window.open('CartaPDFSuspencion.aspx" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    //Response.Write("<script type = 'text/javascript' > window.open('CartaPDFSuspencion.aspx?Id=" + Request.Params["Id"].ToString() + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;

                case E_EstadoTramite.Rechazo:
                    Response.Write("<script type = 'text/javascript' > window.open('CartaPDFRechazo.aspx" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    //Response.Write("<script type = 'text/javascript' > window.open('CartaPDFRechazo.aspx?Id=" + Request.Params["Id"].ToString() + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;

                case E_EstadoTramite.Ejecucion:
                    Response.Write("<script type = 'text/javascript' > window.open('CartaPDFAceptado.aspx" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    //Response.Write("<script type = 'text/javascript' > window.open('CartaPDFAceptado.aspx?Id=" + Request.Params["Id"].ToString() + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;
                case E_EstadoTramite.Cancelado:
                    Response.Write("<script type = 'text/javascript' > window.open('CartaPDFCancelar.aspx" + Encrypt + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    //Response.Write("<script type = 'text/javascript' > window.open('CartaPDFCancelar.aspx?Id=" + Request.Params["Id"].ToString() + "', 'PDF MetLife', 'toolbar=no,scrollbars=yes,resizable=yes,width=600,height=800')</script >;");
                    break;
            }
        }

        private void llenarPDF(string templateFile, Stream stream,DataTable datos,string estado)
        {
            PdfReader reader = new PdfReader(templateFile);
            PdfStamper stamp = new PdfStamper(reader, stream);

            int idTramite = Convert.ToInt32(hdIdTramite.Value);

            //Introducimos el valor en los campos del formulario...
            CultureInfo culture = new CultureInfo("es-MX");
            string fechaCarta = "Ciudad de México, "; //+ DateTime.Today.ToString(culture.DateTimeFormat.LongDatePattern, culture);
            DataTable motivos = new DataTable();

            string tipoTramite = datos.Rows[0]["tramiteNombre"].ToString();
            string ramo = datos.Rows[0]["Nombre"].ToString();
            string agente = datos.Rows[0]["AgenteNombre"].ToString();
            string promotoria = datos.Rows[0]["PromotoriaNombre"].ToString();
            string Contratante= datos.Rows[0]["Contratante"].ToString();
            string Poliza= datos.Rows[0]["Poliza"].ToString();
            string Producto= datos.Rows[0]["Producto"].ToString();
            string MotivoUno = string.Empty;
            string MotivoDos = string.Empty;
            string MotivoTres = string.Empty;
            string MotivoCuatro = string.Empty;
            string MotivoCinco = string.Empty;
            string MotivoSeis = string.Empty;
            string MotivoSiete = string.Empty;
            string MotivoOcho = string.Empty;

            fechaCarta = (new wfiplib.Reportes()).MotivosRechazoFecha(idTramite);
            fechaCarta = "Ciudad de México, " + (Convert.ToDateTime(fechaCarta)).ToString(culture.DateTimeFormat.LongDatePattern, culture);

            if (string.Equals(estado,"2") || string.Equals(estado, "5"))
            {
                motivos = (new wfiplib.Reportes()).MotivosRechazo(idTramite);

                try
                {
                    if (!string.IsNullOrEmpty(motivos.Rows[0]["motivoRechazo"].ToString()))
                        MotivoUno = "   *  " + motivos.Rows[0]["motivoRechazo"].ToString();

                    if (!string.IsNullOrEmpty(motivos.Rows[1]["motivoRechazo"].ToString()))
                        MotivoDos = "   *  " + motivos.Rows[1]["motivoRechazo"].ToString();

                    if (!string.IsNullOrEmpty(motivos.Rows[2]["motivoRechazo"].ToString()))
                        MotivoTres = "   *  " + motivos.Rows[2]["motivoRechazo"].ToString();

                    if (!string.IsNullOrEmpty(motivos.Rows[3]["motivoRechazo"].ToString()))
                        MotivoCuatro = "   *  " + motivos.Rows[3]["motivoRechazo"].ToString();

                    if (!string.IsNullOrEmpty(motivos.Rows[4]["motivoRechazo"].ToString()))
                        MotivoCinco = "   *  " + motivos.Rows[4]["motivoRechazo"].ToString();

                    if (!string.IsNullOrEmpty(motivos.Rows[5]["motivoRechazo"].ToString()))
                        MotivoSeis = "   *  " + motivos.Rows[5]["motivoRechazo"].ToString();

                    if (!string.IsNullOrEmpty(motivos.Rows[6]["motivoRechazo"].ToString()))
                        MotivoSiete = "   *  " + motivos.Rows[6]["motivoRechazo"].ToString();

                    if (!string.IsNullOrEmpty(motivos.Rows[7]["motivoRechazo"].ToString()))
                        MotivoOcho = "   *  " + motivos.Rows[7]["motivoRechazo"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            switch (estado)
            {
                case "2": //Hold
                    stamp.AcroFields.SetField("Fecha", fechaCarta);
                    stamp.AcroFields.SetField("TipoTramite", tipoTramite);
                    stamp.AcroFields.SetField("Ramo", ramo);
                    stamp.AcroFields.SetField("MotivoUno", MotivoUno);
                    stamp.AcroFields.SetField("MotivoDos", MotivoDos);
                    stamp.AcroFields.SetField("MotivoTres", MotivoTres);
                    stamp.AcroFields.SetField("MotivoCuatro", MotivoCuatro);
                    stamp.AcroFields.SetField("MotivoCinco", MotivoCinco);
                    stamp.AcroFields.SetField("MotivoSeis", MotivoSeis);
                    stamp.AcroFields.SetField("MotivoSiete", MotivoSiete);
                    stamp.AcroFields.SetField("MotivoCinco", MotivoOcho);
                    stamp.AcroFields.SetField("Agente", agente);
                    stamp.AcroFields.SetField("Promotoria", promotoria);
                    break;

                case "5":// Suspendido

                    stamp.AcroFields.SetField("Fecha", fechaCarta);
                    stamp.AcroFields.SetField("Contratante", Contratante);
                    stamp.AcroFields.SetField("Poliza", Poliza);
                    stamp.AcroFields.SetField("Ramo", ramo);
                    stamp.AcroFields.SetField("MotivoUno", MotivoUno);
                    stamp.AcroFields.SetField("MotivoDos", MotivoDos);
                    stamp.AcroFields.SetField("MotivoTres", MotivoTres);
                    stamp.AcroFields.SetField("MotivoCuatro", MotivoCuatro);
                    stamp.AcroFields.SetField("MotivoCinco", MotivoCinco);
                    stamp.AcroFields.SetField("MotivoSeis", MotivoSeis);
                    stamp.AcroFields.SetField("MotivoSiete", MotivoSiete);
                    stamp.AcroFields.SetField("MotivoCinco", MotivoOcho);
                    stamp.AcroFields.SetField("Agente", agente);
                    stamp.AcroFields.SetField("Promotoria", promotoria);
                    break;

                case "4"://Rechazado
                    stamp.AcroFields.SetField("Fecha", fechaCarta);
                    stamp.AcroFields.SetField("TipoTramite", tipoTramite);
                    stamp.AcroFields.SetField("Contratante", Contratante);
                    stamp.AcroFields.SetField("Ramo", ramo);
                    stamp.AcroFields.SetField("Agente", agente);
                    stamp.AcroFields.SetField("Promotoria", promotoria);
                    break;

                case "12":// Carta de Aceptación
                    DateTime Vigencia = DateTime.Today;
                    string formato = "yyyy-MM-dd";

                    stamp.AcroFields.SetField("Fecha", fechaCarta);
                    stamp.AcroFields.SetField("Ramo", ramo);
                    stamp.AcroFields.SetField("Poliza", Poliza);
                    stamp.AcroFields.SetField("Contratante", Contratante);
                    stamp.AcroFields.SetField("Producto", Producto);
                    stamp.AcroFields.SetField("Vigencia", Vigencia.ToString(formato));
                    stamp.AcroFields.SetField("Agente", agente);
                    stamp.AcroFields.SetField("Promotoria", promotoria);
                    break;
            }
            

            // Fijamos los valores y enviamos el resultado al stream...
            stamp.FormFlattening = true;
            stamp.Close();
        }

        protected void btnSuspencionCM_Click(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            string Encrypt = "?data=" + (new Application.Operacion.UrlCifrardo()).Encrypt("Id=" + urlCifrardo.IdTramite);
            Response.Write("<script type = 'text/javascript' > window.open('CartaPDFSuspencionCM.aspx" + Encrypt + "', '_blank', 'PDF MetLife', 'width=400,height=500')</script >;");
            //Response.Write("<script type = 'text/javascript' > window.open('CartaPDFSuspencionCM.aspx?Id=" + Request.Params["Id"].ToString() + "', '_blank', 'PDF MetLife', 'width=400,height=500')</script >;");
        }

        protected void BtnCitaMedica(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            string Encrypt = "?data=" + (new Application.Operacion.UrlCifrardo()).Encrypt("Id=" + urlCifrardo.IdTramite);

            if ((new wfiplib.admEmisionVG()).CitaMedicaGenerada(Convert.ToInt32(urlCifrardo.IdTramite)))
            {
                Response.Write("<script type = 'text/javascript' > window.open('CitaPDF.aspx" + Encrypt + "', '_blank', 'PDF MetLife', 'width=400,height=500')</script >;");
            }
            else
            {
                showMessage("Aún no se ha confirmado la cita médica...");
            }

            //if ((new wfiplib.admEmisionVG()).CitaMedicaGenerada(Convert.ToInt32(Request.Params["Id"])))
            //{
            //    Response.Write("<script type = 'text/javascript' > window.open('CitaPDF.aspx?Id=" + Request.Params["Id"].ToString() + "', '_blank', 'PDF MetLife', 'width=400,height=500')</script >;");
            //}
            //else
            //{
            //    showMessage("Aún no se ha confirmado la cita médica...");
            //}
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("nota");
            Response.Redirect("listaMisTramites.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Session["nota"] = txObservaciones.Text.Trim();
            Response.Redirect(EncripParametros("Id=" + hdIdTramite.Value.ToString(), "anexoDocumento.aspx").URL, true);
            //Response.Redirect("anexoDocumento.aspx?Id=" + hdIdTramite.Value);
        }

        /// <summary>
        /// Regresa el Trámite a MetLife para su Revisión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Lo envía a la mesa de Calidad</remarks>
        protected void btnRechazaPoliza_Click(object sender, EventArgs e)
        {
            if (txObservaciones.Text.Trim().Length > 0)
            {
                admTramiteMesa = new wfiplib.admTramiteMesa();
                admTramite = new wfiplib.admTramite();
                wfiplib.tramiteP oTramite = admTramite.carga(Convert.ToInt32(hdIdTramite.Value));
                List<wfiplib.tramiteMesa> Lista = new List<wfiplib.tramiteMesa>();
                wfiplib.E_EstadoMesa EstadoMesa = new wfiplib.E_EstadoMesa();
                if (oTramite.Estado.Equals(wfiplib.E_EstadoTramite.RevPromotoria))
                {
                    Lista = admTramiteMesa.DaMesasTramitesPromotoriaKO(Convert.ToInt32(hdIdTramite.Value));
                    EstadoMesa = wfiplib.E_EstadoMesa.RevPromotoriaKO;
                }

                DateTime FechaInicio = Lista[0].FechaInicio;
                foreach (wfiplib.tramiteMesa oTram in Lista)
                {
                    admTramiteMesa.reinicia(oTram.IdTramite, oTram.IdMesa, EstadoMesa, -100, -1, txObservaciones.Text.Trim().ToUpper(), manejo_sesion.Credencial.Id);
                }

                bool Resultado = admTramite.cambiaEstado(Convert.ToInt32(hdIdTramite.Value), wfiplib.E_EstadoTramite.Proceso);
                registraBitacora(Convert.ToInt32(hdIdTramite.Value), FechaInicio, EstadoMesa);
                Session.Contents.Remove("nota");
                Response.Redirect("listaMisTramites.aspx");
            }
            else
            {
                showMessage("Debe indicar los motivos por los cuales no esta de acuerdo con la Póliza...");
            }
        }

        /// <summary>
        /// Acepta la póliza generada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Fin del Trámite.</remarks>
        protected void btnAceptarPoliza_Click(object sender, EventArgs e)
        {
            admTramiteMesa = new wfiplib.admTramiteMesa();
            admTramite = new wfiplib.admTramite();
            wfiplib.tramiteP oTramite = admTramite.carga(Convert.ToInt32(hdIdTramite.Value));
            List<wfiplib.tramiteMesa> Lista = new List<wfiplib.tramiteMesa>();
            wfiplib.E_EstadoMesa EstadoMesa = new wfiplib.E_EstadoMesa();
            if (oTramite.Estado.Equals(wfiplib.E_EstadoTramite.RevPromotoria))
            {
                Lista = admTramiteMesa.DaMesasTramitesPromotoriaKO(Convert.ToInt32(hdIdTramite.Value));
                EstadoMesa = wfiplib.E_EstadoMesa.RevPromotoriaOK;         // Pone status a la mesa de CALIDAD para que ya no sea tomada en cuenta y se ejecute la poliza
            }

            if (Lista.Count > 0)
            {
                DateTime FechaInicio = Lista[0].FechaInicio;
                foreach (wfiplib.tramiteMesa oTram in Lista)
                {
                    admTramiteMesa.reinicia(oTram.IdTramite, oTram.IdMesa, EstadoMesa, -100, -1, txObservaciones.Text.Trim().ToUpper(), manejo_sesion.Credencial.Id);
                    registraSigMesaFlujo(oTram.IdTramite, oTram.IdMesa);
                }

                FechaInicio = admTramite.getEjecutaTramite(Convert.ToInt32(hdIdTramite.Value));
                bool Resultado = admTramite.cambiaEstado(Convert.ToInt32(hdIdTramite.Value), wfiplib.E_EstadoTramite.Ejecucion);
                registraBitacora(Convert.ToInt32(hdIdTramite.Value), FechaInicio, EstadoMesa);
                Session.Contents.Remove("nota");
                Response.Redirect("listaMisTramites.aspx");
            }
        }

        private bool registraSigMesaFlujo(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
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
                        //blnRegistrarMesa = ActivarMesaCondicionada(oTramite, SiguienteMesa.Id, SiguienteMesa.Nombre);
                    }

                    if (blnRegistrarMesa)
                        resultado = oAdmTramiteMesa.registra(siguiente);

                }
            }
            else
            {
                //Última Mesa actualiza el estado al Tramite
                //(new wfiplib.admTramite()).cambiaEstado(oTramite.Id, wfiplib.E_EstadoTramite.Ejecucion);
            }
            return resultado;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Session["expediente"] == null)
                showMessage("Debe de Anexar un Archivo para continuar con el proceso de su Solicitud...");
            else
            {
                admTramiteMesa = new wfiplib.admTramiteMesa();
                admTramite = new wfiplib.admTramite();
                wfiplib.tramiteP oTramite = admTramite.carga(Convert.ToInt32(hdIdTramite.Value));
                List<wfiplib.tramiteMesa> Lista = new List<wfiplib.tramiteMesa>();
                wfiplib.E_EstadoMesa EstadoMesa = new wfiplib.E_EstadoMesa();

                switch (oTramite.Estado)
                {
                    case wfiplib.E_EstadoTramite.Hold:
                        Lista = admTramiteMesa.DaMesasTramitesRevisionHold(Convert.ToInt32(hdIdTramite.Value));
                        EstadoMesa = wfiplib.E_EstadoMesa.ReingresoHold;
                        ReingresarTramitesOperacion(Lista, EstadoMesa);
                        admTramiteMesa.resetMotivosRechazp(Convert.ToInt32(hdIdTramite.Value));
                        break;

                    case wfiplib.E_EstadoTramite.Suspendido:
                        Lista = admTramiteMesa.DaMesasTramitesRevisionHoldSuspencion(Convert.ToInt32(hdIdTramite.Value));
                        EstadoMesa = wfiplib.E_EstadoMesa.ReingresoHold;
                        ReingresarTramitesOperacion(Lista, EstadoMesa);

                        Lista = admTramiteMesa.DaMesasTramitesRevisonSupension(Convert.ToInt32(hdIdTramite.Value));
                        EstadoMesa = wfiplib.E_EstadoMesa.ReingresoSuspencion;
                        ReingresarTramitesOperacion(Lista, EstadoMesa);
                        admTramiteMesa.resetMotivosRechazp(Convert.ToInt32(hdIdTramite.Value));
                        break;

                    case wfiplib.E_EstadoTramite.PCI:
                        Lista = admTramiteMesa.DaMesasTramitesRevisonPCI(Convert.ToInt32(hdIdTramite.Value));
                        EstadoMesa = wfiplib.E_EstadoMesa.ReingresoPCI;
                        ReingresarTramitesOperacion(Lista, EstadoMesa);
                        break;

                    case wfiplib.E_EstadoTramite.SuspensionCitaMedica:
                        Lista = admTramiteMesa.DaMesasTramitesRevisionCitaMedica(Convert.ToInt32(hdIdTramite.Value));
                        EstadoMesa = wfiplib.E_EstadoMesa.ReingresoCitaMedica;
                        ReingresarTramitesOperacion(Lista, EstadoMesa);
                        break;
                }

                Session.Contents.Remove("expediente");
                Response.Redirect("listaMisTramites.aspx");
            }
        }

        private void ReingresarTramitesOperacion(List<wfiplib.tramiteMesa> Lista, wfiplib.E_EstadoMesa EstadoMesa)
        {
            DateTime FechaInicio = DateTime.Now;
            if (Lista.Count > 0)
                FechaInicio = Lista[0].FechaInicio;
            foreach (wfiplib.tramiteMesa oTram in Lista)
            {
                admTramiteMesa.reiniciaDesdePromotoria(oTram.IdTramite, oTram.IdMesa, EstadoMesa, true);
            }
            bool Resultado = admTramite.cambiaEstado(Convert.ToInt32(hdIdTramite.Value), wfiplib.E_EstadoTramite.Proceso);
            registraBitacora(Convert.ToInt32(hdIdTramite.Value), FechaInicio, EstadoMesa);
            Session.Contents.Remove("nota");
        }

        private void registraBitacora(int pIdTramite, DateTime pFechaInicio, wfiplib.E_EstadoMesa pEstadoMesa)
        {
            admTramite = new wfiplib.admTramite();
            admMesa = new wfiplib.admMesa();
            admBitacora = new wfiplib.admBitacora();

            //wfiplib.credencial ocrd = (wfiplib.credencial)Session["credencial"];
            wfiplib.tramiteP oTramite = admTramite.carga(pIdTramite);

            wfiplib.bitacora oBitacora = new wfiplib.bitacora
            {
                IdFlujo = oTramite.IdFlujo,
                IdTipoTramite = oTramite.IdTipoTramite,
                IdTramite = oTramite.Id,
                IdMesa = -1,            // admMesa.daPrimerMesa(oTramite.IdFlujo).Id,
                Usuario = manejo_sesion.Credencial.Usuario,
                IdUsuario = manejo_sesion.Credencial.Id,
                FechaInicio = pFechaInicio,
                Estado = pEstadoMesa,
                Observacion = txObservaciones.Text
            };
            admBitacora.Nuevo(oBitacora);
        }

        private void registraBitacora(int pIdTramite, DateTime pFechaInicio, wfiplib.E_EstadoMesa pEstadoMesa, string pObservaciones)
        {
            admTramite = new wfiplib.admTramite();
            admMesa = new wfiplib.admMesa();
            admBitacora = new wfiplib.admBitacora();

            //wfiplib.credencial ocrd = (wfiplib.credencial)Session["credencial"];
            wfiplib.tramiteP oTramite = admTramite.carga(pIdTramite);

            wfiplib.bitacora oBitacora = new wfiplib.bitacora
            {
                IdFlujo = oTramite.IdFlujo,
                IdTipoTramite = oTramite.IdTipoTramite,
                IdTramite = oTramite.Id,
                IdMesa = -1,            // admMesa.daPrimerMesa(oTramite.IdFlujo).Id,
                Usuario = manejo_sesion.Credencial.Usuario,
                IdUsuario = manejo_sesion.Credencial.Id,
                FechaInicio = pFechaInicio,
                Estado = pEstadoMesa,
                Observacion = pObservaciones
            };
            admBitacora.Nuevo(oBitacora);
        }

        private void pintaDatos(int pIdTramite)
        {
            lblCitasMedicas.Visible = false;
            btnCitasMedicas.Visible = false;
            btnSuspencionCM.Visible = false;
            lblCarta.Visible = false;
            //btnCarta1.Visible = false;
            btnGeneraCarta.Visible = false;

            admTramite = new wfiplib.admTramite();
            admServiciosVida = new wfiplib.admServiciosVida();
            admServicioGmm = new wfiplib.admServicioGmm();
            admEmisionVG = new wfiplib.admEmisionVG();
            
            wfiplib.tramiteP oTramite = admTramite.carga(pIdTramite);

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    wfiplib.serviciosVidaP oServiciosVida = admServiciosVida.carga(pIdTramite);
                    ltInfContratante.Text = oServiciosVida.CabeceraHtml;
                    break;

                case wfiplib.E_TipoTramite.ServicioGmm:
                    wfiplib.ServicioGmm oServiciosGmm = admServicioGmm.carga(pIdTramite);
                    ltInfContratante.Text = oServiciosGmm.CabeceraHtml;
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    wfiplib.EmisionVG oEmisionVida = admEmisionVG.carga(pIdTramite);
                    ltInfContratante.Text = oEmisionVida.DatosHtml;
                    DataTable listEstados = (new wfiplib.admEmisionVG()).CitaMedica(pIdTramite);
                    if (listEstados.Rows.Count > 0)
                    {
                        DataRow row = listEstados.Rows[0];
                        if (row["Activo"].ToString() == "1")
                        {
                            Cita.Visible = true;
                            lblCitasMedicas.Visible = true;
                            btnCitasMedicas.Visible = true;
                        }
                        else
                        {
                                Cita.Visible = true;
                                MsCitaMedica.Visible = true;
                                MsCitaMedica.Text = "Cita Medica Inactiva";
                        }
                    }
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    wfiplib.EmisionVG oEmisionGmm = admEmisionVG.carga(pIdTramite);
                    ltInfContratante.Text = oEmisionGmm.DatosHtml;
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    wfiplib.EmisionVG oEmisionConversiones = admEmisionVG.carga(pIdTramite);
                    ltInfContratante.Text = oEmisionConversiones.DatosHtml;
                    break;
                default:
                    break;
            }

            ValidaObservaciones(oTramite.Estado, pIdTramite);
            ValidaInsumos(pIdTramite);
        }

        // Habilita panel observaciones de acuerdo al estado
        private void ValidaObservaciones(wfiplib.E_EstadoTramite Estado, int pIdTramite)
        {
            bool blnShowCartas = true;
            admTramiteMesa = new wfiplib.admTramiteMesa();
            List<wfiplib.tramiteMesa> Lista = new List<wfiplib.tramiteMesa>();

            if (Estado.Equals(wfiplib.E_EstadoTramite.Rechazo) ||
                Estado.Equals(wfiplib.E_EstadoTramite.Hold) ||
                Estado.Equals(wfiplib.E_EstadoTramite.PCI) ||
                Estado.Equals(wfiplib.E_EstadoTramite.Suspendido) ||
                Estado.Equals(wfiplib.E_EstadoTramite.RevPromotoria) ||
                Estado.Equals(wfiplib.E_EstadoTramite.SuspensionCitaMedica))
            {
                switch (Estado)
                {
                    case wfiplib.E_EstadoTramite.Hold:
                        lbEtiquetaObsv.Text = "OBSERVACIONES";
                        admTramiteMesa.AtrapaMesasenHoldTramite(pIdTramite);
                        Lista = admTramiteMesa.DaMesasTramitesRevisionHold(pIdTramite);
                        pnbtnMod.Visible = true;
                        //btnObsv.Visible = false;
                        break;

                    case wfiplib.E_EstadoTramite.Suspendido:
                        lbEtiquetaObsv.Text = "OBSERVACIONES";
                        admTramiteMesa.AtrapaMesasenHoldTramite(pIdTramite);
                        admTramiteMesa.AtrapaMesasSuspension(pIdTramite);
                        Lista = admTramiteMesa.DaMesasTramitesRevisonSupension(pIdTramite);
                        //btnImpPendientes.Visible = true;
                        pnbtnMod.Visible = true;
                        break;

                    case wfiplib.E_EstadoTramite.PCI:
                        lbEtiquetaObsv.Text = "OBSERVACIONES";
                        admTramiteMesa.AtrapaMesasSuspension(pIdTramite);
                        Lista = admTramiteMesa.DaMesasTramitesRevisonSupension(pIdTramite);
                        //btnImpPendientes.Visible = true;
                        pnbtnMod.Visible = true;
                        blnShowCartas = false;
                        break;

                    case wfiplib.E_EstadoTramite.Rechazo:
                        lbEtiquetaObsv.Text = "LISTA DE RECHAZOS";
                        Lista = admTramiteMesa.DaMesasTramiteRechazo(pIdTramite);
                        //btnimpRechazo.Visible = false;
                        break;

                    case wfiplib.E_EstadoTramite.RevPromotoria:

                        lbEtiquetaObsv.Text = "OBSERVACIONES:<hr/><br/>&nbsp;<strong style='color:blue;'>" + admTramiteMesa.ReviewCalidadObs(Convert.ToInt32(hdIdTramite.Value)) + "</strong>";
                        admTramiteMesa.AtrapaMesasTramiteRevision(pIdTramite);
                        Lista = admTramiteMesa.DaMesasTramitesRevisionPromo(pIdTramite);
                        pnbtnMod.Visible = true;
                        //btnObsv.Visible = false;
                        blnShowCartas = false;
                        break;

                    case wfiplib.E_EstadoTramite.SuspensionCitaMedica:
                        //lbEtiquetaObsv.Text = "OBSERVACIONES";
                        admTramiteMesa.AtrapaMesasTramiteRevisionCitaMedica(pIdTramite);
                        Lista = admTramiteMesa.DaMesasTramitesRevisionPromoCitaMedica(pIdTramite);
                        //pnSuspensionCitaMedica.Visible = true;
                        //btnObsv.Visible = false;
                        //blnShowCartas = false;
                        break;
                }

                rpObsrv.DataSource = Lista;
                rpObsrv.DataBind();
                pnObsrMod.Visible = true;
                //btnCarta1.Visible = blnShowCartas;
                btnGeneraCarta.Visible = blnShowCartas;
            }
            else
            {
                if (Estado.Equals(wfiplib.E_EstadoTramite.Proceso))
                {
                    List<wfiplib.bitacora> lsBitacora = (new wfiplib.admBitacora()).daLista(pIdTramite);
                    foreach (wfiplib.bitacora oBitacora in lsBitacora)
                    {
                        ltBitacora.Text = ltBitacora.Text + oBitacora.TextoHtml;
                    }
                    btnMuestraBitacora.Visible = true;
                }
            }
        }

        private void ValidaInsumos(int pIdTramite)
        {
            //admInsumos = new wfiplib.admInsumos();
            //if (admInsumos.tieneInsumos(pIdTramite))
            //    btnMuestraInsumos.Visible = true;
            //else
            //    btnMuestraInsumos.Visible = false;
        }

        private void MuestraPDF()
        {
            int IdTramite = int.Parse(hdIdTramite.Value.ToString());
            ltMuestraPdf.Text = "";
            ltMuestraPdf.Text = "<iframe src='" + EncripParametros("IdTramite=" + IdTramite, "Displaypdf.aspx").URL + "' style='width:100%; height:540px' style='border: none;'></iframe>";


            //admExpediente = new wfiplib.admExpediente();

            //string strDoctoWeb = "";
            //string strDoctoServer = "";
            //try
            //{
            //    wfiplib.expediente ArchivoFusion = admExpediente.daFusion(Convert.ToInt32(hdIdTramite.Value));
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

        private void imprimeCartaHold()
        {
            admTramiteMesa = new wfiplib.admTramiteMesa();

            List<wfiplib.tramiteMesa> Lista = admTramiteMesa.DaMesasTramitesRevisionHold(Convert.ToInt32(hdIdTramite.Value));

            ReportViewer Plantilla = llenaPlantillaHold_O_Suspension(Convert.ToInt32(hdIdTramite.Value), Lista, wfiplib.E_EstadoTramite.Hold);

            this.MuestraDocumento(Plantilla, ltPdfPop);
        }

        private void imprimeCartaSuspension()
        {
            admTramiteMesa = new wfiplib.admTramiteMesa();

            List<wfiplib.tramiteMesa> Lista = admTramiteMesa.DaMesasTramitesRevisonSupension(Convert.ToInt32(hdIdTramite.Value));

            ReportViewer Plantilla = llenaPlantillaHold_O_Suspension(Convert.ToInt32(hdIdTramite.Value), Lista, wfiplib.E_EstadoTramite.Suspendido);

            this.MuestraDocumento(Plantilla, ltCartaSuspension);
        }

        private void imprimeCartaRechazo()
        {
            ReportViewer Plantilla = llenaPlantillaRechazo(Convert.ToInt32(hdIdTramite.Value));

            MuestraDocumento(Plantilla, ltCartaRechazo);
        }

        private void imprimeCartaAceptacion()
        {

        }
        private void MuestraDocumento(ReportViewer Plantilla, Literal ltdocumento)
        {
            //wfiplib.credencial oCrd = (wfiplib.credencial)Session["credencial"];
            string Archivo = manejo_sesion.Credencial.Id.ToString().PadLeft(3, '0') + "CartaTramite.pdf";
            string Destino = Properties.Settings.Default.dirDoctosWeb + Archivo;

            if (File.Exists(Destino))
            {
                File.Delete(Destino);
            }

            if (creaPDF(Plantilla, Destino))
            {
                if (File.Exists(Destino))
                {
                    Archivo = Properties.Settings.Default.urlDoctosWeb + Archivo;
                    ltdocumento.Text = "<embed src='" + Archivo + "' style='width:100%; height:100%' type='application/pdf'>";
                }
            }
        }

        private ReportParameterCollection IncializaParametroReporte(int pIdTramite)
        {
            string Nombre = string.Empty;
            string FechaTramite = string.Empty;
            string TipoTramite = string.Empty;

            DaDatosBasicosTramite(pIdTramite, ref Nombre, ref FechaTramite, ref TipoTramite);
            wfiplib.credencial oCrd = (wfiplib.credencial)Session["credencial"];

            ReportParameterCollection Parametros = new ReportParameterCollection();
            Parametros.Add(new ReportParameter("IdTramite", pIdTramite.ToString().PadLeft(5, '0')));
            Parametros.Add(new ReportParameter("Nombre", Nombre));
            Parametros.Add(new ReportParameter("FechaTramite", FechaTramite));
            Parametros.Add(new ReportParameter("TipoTramite", TipoTramite));
            Parametros.Add(new ReportParameter("cvePromotoria", oCrd.IdPromotoria.ToString()));
            Parametros.Add(new ReportParameter("cveAgente", oCrd.Id.ToString()));
            //Parametros.Add(new ReportParameter("imgFirma", Server.MapPath("~\\img") + @"\frm.png"));
            Parametros.Add(new ReportParameter("imgFirma", Server.MapPath("~\\img") + @"\edit.png"));
            Parametros.Add(new ReportParameter("imgLogo", Server.MapPath("~\\img") + @"\logo.gif"));

            return Parametros;
        }

        private ReportViewer llenaPlantillaHold_O_Suspension(int pIdTramite, List<wfiplib.tramiteMesa> Lista, wfiplib.E_EstadoTramite Estado)
        {
            ReportParameterCollection parametros = IncializaParametroReporte(pIdTramite);

            ReportDataSource rdsDetallado = new ReportDataSource("dtObservaciones");
            rdsDetallado.Value = Lista;

            ReportViewer Reporte = new ReportViewer();
            Reporte.Reset();
            Reporte.ProcessingMode = ProcessingMode.Local;
            Reporte.LocalReport.EnableExternalImages = true;
            if (Estado.Equals(wfiplib.E_EstadoTramite.Hold))
            {
                Reporte.LocalReport.ReportPath = "promotoria/rvCarta.rdlc";
            }
            else
            {
                Reporte.LocalReport.ReportPath = "promotoria/rvSuspesion.rdlc";
            }
            Reporte.LocalReport.SetParameters(parametros);
            Reporte.LocalReport.DataSources.Add(rdsDetallado);
            Reporte.LocalReport.Refresh();

            return Reporte;
        }

        private ReportViewer llenaPlantillaRechazo(int pIdTramite)
        {
            ReportParameterCollection parametros = IncializaParametroReporte(pIdTramite);

            ReportViewer Reporte = new ReportViewer();
            Reporte.Reset();
            Reporte.ProcessingMode = ProcessingMode.Local;
            Reporte.LocalReport.EnableExternalImages = true;
            Reporte.LocalReport.ReportPath = "promotoria/rvRechazo.rdlc";
            Reporte.LocalReport.SetParameters(parametros);
            Reporte.LocalReport.Refresh();

            return Reporte;
        }

        private void DaDatosBasicosTramite(int pIdTramite, ref string Nombre, ref string FechaTramite, ref string TipoTramite)
        {
            admTramite = new wfiplib.admTramite();
            admServiciosVida = new wfiplib.admServiciosVida();
            admServicioGmm = new wfiplib.admServicioGmm();
            admEmisionVG = new wfiplib.admEmisionVG();
            admTipoTramite = new wfiplib.admTipoTramite();

            wfiplib.tramiteP oTramite = admTramite.carga(pIdTramite);

            Nombre = string.Empty;
            FechaTramite = string.Empty;
            TipoTramite = admTipoTramite.DaNombre(oTramite.IdTipoTramite);

            FechaTramite = oTramite.FechaRegistro.ToString("dd/MM/yyyy");
            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    wfiplib.serviciosVidaP oServiciosVida = admServiciosVida.carga(pIdTramite);
                    Nombre = oServiciosVida.Nombre + " " + oServiciosVida.ApPaterno + " " + oServiciosVida.ApMaterno; ; ;
                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    wfiplib.ServicioGmm oServiciosGmm = admServicioGmm.carga(pIdTramite);
                    Nombre = oServiciosGmm.Nombre + " " + oServiciosGmm.ApPaterno + " " + oServiciosGmm.ApMaterno;
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    wfiplib.EmisionVG oEmisionVida = admEmisionVG.carga(pIdTramite);
                    //wfiplib.EmisionVida oEmisionVida = (new wfiplib.admEmisionVida()).carga(pIdTramite);
                    ltInfContratante.Text = oEmisionVida.DatosHtml;
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    wfiplib.EmisionVG oEmisionGmm = admEmisionVG.carga(pIdTramite);
                    //wfiplib.EmisionGmm oEmisionGmm = (new wfiplib.admEmisionGmm()).carga(pIdTramite);
                    ltInfContratante.Text = oEmisionGmm.DatosHtml;
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    wfiplib.EmisionVG oEmisionConversiones = admEmisionVG.carga(pIdTramite);
                    //wfiplib.EmisionGmm oEmisionGmm = (new wfiplib.admEmisionGmm()).carga(pIdTramite);
                    ltInfContratante.Text = oEmisionConversiones.DatosHtml;
                    break;
            }
        }

        public bool creaPDF(ReportViewer reporte, string pArchivo)
        {
            bool resultado = false;
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            if (File.Exists(pArchivo))
                File.Delete(pArchivo);

            byte[] bytes = reporte.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(pArchivo, System.IO.FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            resultado = true;
            return resultado;
        }

        private void pintaInsumos()
        {
            admInsumos = new wfiplib.admInsumos();

            int pIdTramite = Convert.ToInt32(hdIdTramite.Value);
            List<wfiplib.insumos> LstArchInsumos = new List<wfiplib.insumos>();
            LstArchInsumos = admInsumos.daLista(pIdTramite);
            if (LstArchInsumos.Count > 0)
            {
                rptInsumos.DataSource = LstArchInsumos;
                rptInsumos.DataBind();
            }
        }

        protected void rptInsumos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            admDirectorio = new wfiplib.admDirectorio();

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    wfiplib.insumos oInsumo = (wfiplib.insumos)(e.Item.DataItem);
                    string archOrigen = admDirectorio.daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, oInsumo.Id) + oInsumo.NmArchivo;
                    string archDestino = Properties.Settings.Default.dirDoctosWeb + oInsumo.NmArchivo;
                    if (!File.Exists(archDestino))
                    {
                        File.Copy(archOrigen, archDestino);
                    }
                    ImageButton btnExp = (ImageButton)(e.Item.FindControl("ImgExp"));
                    btnExp.OnClientClick = "Descarga('" + Properties.Settings.Default.urlDoctosWeb + oInsumo.NmArchivo + "'); return false;";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected void listCombos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime validateFechaSolicitud = DateTime.Now;
            ValidaFecha(validateFechaSolicitud, listCombosCitaMed.Text.ToString());
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
                    MuestraProducto(lstProductos2);
                    rptTramite.DataSource = lstProductos2;
                    rptTramite.DataBind();
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

                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    ltInfTipoTramite.Text = oTramite.Flujo.ToUpper() + "<br />";
                    ltInfTipoTramite.Text += "CONVERSIONES";
                    wfiplib.EmisionVG oEmisionConversiones = (new wfiplib.admEmisionVG()).cargaCompleto(pIdTramite);
                    ltInfContratante.Text = oEmisionConversiones.DatosHtml;
                    wfiplib.EmisionVG oEmisionConversiones2 = (new wfiplib.admEmisionVG()).cargaFolio(pIdTramite);
                    ltInfFolio.Text = oEmisionConversiones2.FolioHtml;
                    DataTable lstProductosConversiones = (new wfiplib.admEmisionVG()).cargaProdructos(pIdTramite);
                    rptTramite.DataSource = lstProductosConversiones;
                    rptTramite.DataBind();
                    break;
                default:
                    break;
            }
        }

        protected void MuestraProducto(DataTable tabla)
        {
            DataRow row = tabla.Rows[0];
            Infosubproduto.Text = row["SUBPRODUCTO"].ToString();
        }
        protected void CitasMedicasEvalucacion()
        {
            MSresultado2.Text = "";
            TextCombo.Text = "";
            string DescripCombo = "";
            citamedica.Visible = false;


            Double MontoTotal = Total(InfoSumaAseguradaBasica.Text.ToString(), InfoSumaAseguradaPolizasVigentes.Text.ToString(), IdInfoMoneda.Text.ToString());

            
            if (Infosubproduto.Text == "Riesgo Preferente")
            {
                DescripCombo = "Tempo life riesgos preferentes";
            }

            if (InfoContratante.Text.Equals("MORAL"))
            {
                int Edad = CalcularEdad(InfoTNacimiento.Text.Trim());
                textEdadCombo.Text = Edad.ToString();
                Evaluacion(MontoTotal, Edad, DescripCombo);
                
            }
            else if (InfoContratante.Text.Equals("FISICA"))
            {
                if (InfoTNombre.Text.ToString() != "")
                {
                    int Edad = CalcularEdad(InfoTNacimiento.Text.Trim());
                    textEdadCombo.Text = Edad.ToString();
                    Evaluacion(MontoTotal, Edad, DescripCombo);
                    //MSresultado2.Text = "se reariliza con solicitante";
                }
                else
                {
                    int Edad = CalcularEdad(InfoFFechaNa.Text.Trim());
                    textEdadCombo.Text = Edad.ToString();
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
                    MSresultado2.Text = "La solicitud no necesita cita médica";
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
                MSresultado2.Text = "La solicitud no necesita cita médica";
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
            String valor = row["valor"].ToString();
            Double Moneda = Convert.ToDouble(valor);

            total = numero * Moneda;
            return total;
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
            IdInfoMoneda.Text = IdMoneda.ToString();
            DatosPromotoria(oEmisionGmm.IdPromotoria.ToString());



            DataTable ValorMoneda = (new wfiplib.admEmisionVG()).ValorMoneda(IdMoneda);
            DataRow row = ValorMoneda.Rows[0];
            InfoMoneda.Text = row["Nombre"].ToString().ToUpper();

            if (oEmisionGmm.HombreClave)
            {
                InfoHobreClave.Text = "SI";
            }

            // PRIORIDAD DE TRAMITE
            InfoPrioridad.Text = "";
            switch (oEmisionGmm.Prioridad)
            {
                case wfiplib.E_PrioridadTramite.Supervisor:
                    InfoPrioridad.Text = "SUPERVISOR";
                    break;
                case wfiplib.E_PrioridadTramite.GrandesSumas:
                    InfoPrioridad.Text = "GRANDES SUMAS";
                    break;
                case wfiplib.E_PrioridadTramite.GrandesSumasPrimas:
                    InfoPrioridad.Text = "GRANDES SUMAS PRIMAS";
                    break;
                case wfiplib.E_PrioridadTramite.HombreClave:
                    InfoPrioridad.Text = "HOMBRES CLAVE";
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
                InfoCelular.Text = oCitamedica.Cel.ToString();
                InfoCelularAgentePromotor.Text = oCitamedica.CelAgentePromotor.ToString();
                InfoCorreo.Text = oCitamedica.Correo.ToString();
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

            TextFecha1.TimeSectionProperties.Visible = true;
            TextFecha1.UseMaskBehavior = true;
            TextFecha1.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha1.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

            TextFecha2.TimeSectionProperties.Visible = true;
            TextFecha2.UseMaskBehavior = true;
            TextFecha2.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha2.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

            TextFecha3.TimeSectionProperties.Visible = true;
            TextFecha3.UseMaskBehavior = true;
            TextFecha3.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha3.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

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
                        Response.Redirect("listaMisTramites.aspx");
                    }
                }
            }
        }

        private wfiplib.EmisionVG recuperaCaptura()
        {
            wfiplib.EmisionVG resultado = new wfiplib.EmisionVG();
            try
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
                
                //int idTramite = Convert.ToInt32(Request.Params["Id"].ToString());

                resultado.IdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
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

        protected void LisLabHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaDireccion();
        }

        protected void LisEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            listCiudad();
            lisLabHospital();
        }

        protected void LisCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            lisLabHospital();
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
                FechasCombo(FechaSolicitud, dias);
            }
            else if (HC3 <= FechaSolicitud && FechaSolicitud <= HC4)
            {
                int dias = AumentaDias(combo, 2);
                FechasCombo(FechaSolicitud, dias);
            }
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

        private void FechasCombo(DateTime FechaSolicitud, int dias)
        {
            DateTime Fecha = Convert.ToDateTime(FechaSolicitud.AddDays(+dias).ToShortDateString());
            DateTime hora1 = Convert.ToDateTime("08:00:00");
            DateTime hora2 = Convert.ToDateTime("23:59:59");
            DateTime HC1 = Fecha.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);
            DateTime HC2 = Fecha.AddHours(hora2.Hour).AddMinutes(hora2.Minute).AddSeconds(hora2.Second);

            TextFecha1.MinDate = HC1;
            TextFecha1.MaxDate = HC2.AddYears(+1);
            TextFecha1.Date = FechaSolicitud.AddDays(+dias);
            TextFecha1.TimeSectionProperties.Visible = true;
            TextFecha1.UseMaskBehavior = true;
            TextFecha1.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha1.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

            TextFecha2.MinDate = HC1;
            TextFecha2.MaxDate = HC2.AddYears(+1);
            TextFecha2.Date = FechaSolicitud.AddDays(+dias);
            TextFecha2.TimeSectionProperties.Visible = true;
            TextFecha2.UseMaskBehavior = true;
            TextFecha2.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha2.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");

            TextFecha3.MinDate = HC1;
            TextFecha3.MaxDate = HC2.AddYears(+1);
            TextFecha3.TimeSectionProperties.Visible = true;
            TextFecha3.UseMaskBehavior = true;
            TextFecha3.EditFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
            TextFecha3.DisplayFormatString = GetFormatString("dd/MM/yyyy hh:mm tt");
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
            else
            {
                DateTime Fecha3 = Convert.ToDateTime(TextFecha3.Text.Trim());
                if (!ValdaFecha3(Fecha1, Fecha2, Fecha3))
                {
                    texFecha3.Text = "Fecha no valida";
                }
            }
            if (!ValdaComparaFechas(Fecha1, Fecha2))
            {
                texFecha1.Text = "Fecha no valida";
            }
            if (!ValdaComparaFechas(Fecha2, Fecha1))
            {
                texFecha2.Text = "Fecha no valida";
            }

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

        public bool ValdaComparaFechas(DateTime Fecha, DateTime Fecha2)
        {
            bool resultado = true;
            DateTime FechaHora = Convert.ToDateTime(Fecha.ToShortTimeString());
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
                {
                    resultado = false;
                }
                else if (Fecha3.Equals(Fecha2))
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

        protected void btnGenerarCita_Click(object sender, EventArgs e)
        {
            if (ContinuarFechas())
            {
                // admTramiteMesa = new wfiplib.admTramiteMesa();
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
                int idTramite = Convert.ToInt32(urlCifrardo.IdTramite);
                //int idTramite = Convert.ToInt32(Request.Params["Id"].ToString());
                wfiplib.admTramite admTramite = new wfiplib.admTramite();
                wfiplib.admTramiteDetatlle admTramiteDet = new wfiplib.admTramiteDetatlle();
                wfiplib.tramiteP oTramite = admTramite.carga(idTramite);
                wfiplib.tramiteDetalle oTramiteDetalle = admTramiteDet.carga(oTramite.Id);

                wfiplib.EmisionVG citaMedica = new wfiplib.EmisionVG
                {
                    IdTramite = oTramite.Id,
                    Sexo = oTramiteDetalle.Sexo,
                    Edad = textEdadCombo.Text.ToString().Trim(),
                    //Edad = CalcularEdad(oTramiteDetalle.FechaNacimiento).ToString(),
                    SumaAsegurada = oTramiteDetalle.SumaAsegurada,
                    SumaPolizas = oTramiteDetalle.SumaPolizas,
                    CitaMedica = true,
                    //Combo = TextCombo.Text.Trim(),
                    Combo = listCombosCitaMed.Text.Trim(),
                    Cel = TextCel.Text.Trim(),
                    Estado = LisEstado.Text.Trim(),
                    CelAgentePromotor = TextCelAgentePromotor.Text.Trim(),
                    Ciudad = LisCiudad.Text.Trim(),
                    Correo = TextCorreo.Text.Trim(),
                    LaboratorioHospital = LisLabHospital.Text.Trim(),
                    Direccion = TextDireccion.Text.Trim(),
                    Fecha1 = TextFecha1.Text.Trim(),
                    Fecha2 = TextFecha2.Text.Trim(),
                    Fecha3 = TextFecha3.Text.Trim(),
                    Notas = notas.Text.Trim()
                };

                // Actualizamos la información de la cita Médica
                wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                
                // DESACTVA TODAS LAS CITAS QUE SE TENGAN DEL TRAMITE CON ANTERIORIDAD.
                (new wfiplib.admEmisionVG()).InactivoCitaMedica(idTramite);

                if ((new wfiplib.admEmisionVG()).newCitaMedica(citaMedica))
                {
                    admTramiteMesa = new wfiplib.admTramiteMesa();
                    admTramite = new wfiplib.admTramite();
                    List<wfiplib.tramiteMesa> Lista = new List<wfiplib.tramiteMesa>();
                    wfiplib.E_EstadoMesa EstadoMesa = new wfiplib.E_EstadoMesa();
                    if (oTramite.Estado.Equals(wfiplib.E_EstadoTramite.InfoCitaMedica))
                    {
                        Lista = admTramiteMesa.DaMesasTramitesCitaMedicaReactiva(oTramite.Id);
                        EstadoMesa = wfiplib.E_EstadoMesa.Registro;
                    }

                    DateTime FechaInicio = Lista[0].FechaInicio;
                    foreach (wfiplib.tramiteMesa oTram in Lista)
                    {
                        admTramiteMesa.reinicia(oTram.IdTramite, oTram.IdMesa, EstadoMesa, -100, -1);
                    }

                    FechaInicio = admTramite.getEjecutaTramite(Convert.ToInt32(hdIdTramite.Value));
                    bool Resultado = admTramite.cambiaEstado(Convert.ToInt32(hdIdTramite.Value), wfiplib.E_EstadoTramite.Proceso);
                    registraBitacora(Convert.ToInt32(hdIdTramite.Value), FechaInicio, EstadoMesa);
                    Session.Contents.Remove("nota");

                    wfiplib.Correo correo = new wfiplib.Correo();
                    string strSendMail = correo.ProcesarCorreo(TextCorreo.Text.Trim(), "wfo@asae.com.mx", "Cita Médica", "Se generó una Cita Médica para el Solicitante. <br/><br/><br/>" + oTramite.DatosHtml + "");

                    Response.Redirect("listaMisTramites.aspx");
                }
                else
                {
                    showMessage("No se pudó crear la cita médica.");
                }
            }
            
        }

        protected string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
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

        protected void pnlCallbackMotCancelar_Callback(object sender, EventArgs e)
        {
            //Mensajes message = new Mensajes();
            //message.MostrarMensaje(this, "prueba");
        }

        protected void pnlCallbackMotReconsidera_Callback(object sender, EventArgs e)
        {
            //Mensajes message = new Mensajes();
            //message.MostrarMensaje(this, "prueba");
        }
        
        //protected void treeList_CustomDataCallbackCancelar(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        //{
        //    // e.Result = treeListCancelar.SelectionCount.ToString();
        //}
        protected void treeList_CustomDataCallbackCancelar(object sender, EventArgs e)
        {
            Mensajes mensajes = new Mensajes();
            //var nodos = treeListCancelar.GetSelectedNodes();
            //List<string> motivos = new List<string>();
            //foreach (TreeListNode node in nodos)
            //{
            //    motivos.Add(node.GetValue("motivoRechazo").ToString() + "|" + node.GetValue("id").ToString() + "\n") ;
            //}
            //mensajes.MostrarMensaje(this, string.Join(",", motivos));

            mensajes.MostrarMensaje(this, "Ocurrio un error al intentar realizar la cancelación del trámite.");
        }

        protected void treeList_DataBoundCancelar(object sender, EventArgs e)
        {
            SetNodeSelectionSettings();
        }

        private void SetNodeSelectionSettings()
        {
            TreeListNodeIterator iterator = treeListCancelar.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;
            }
        }

        protected void btnCtrlAplicaReconsideracion_Click(object sender, EventArgs e)
        {
            if (txObservacionesReconsidera.Text.Trim().Length > 0)
            {
                DataTable dtMesaCalidad = null;
                admTramiteMesa = new wfiplib.admTramiteMesa();
                admTramite = new wfiplib.admTramite();
                wfiplib.tramiteP oTramite = admTramite.carga(Convert.ToInt32(hdIdTramite.Value));
                List<wfiplib.tramiteMesa> Lista = new List<wfiplib.tramiteMesa>();
                wfiplib.E_EstadoMesa EstadoMesa = new wfiplib.E_EstadoMesa();
                if (oTramite.Estado.Equals(wfiplib.E_EstadoTramite.Rechazo))
                {
                    dtMesaCalidad = admTramiteMesa.DaMesasCliadad(Convert.ToInt32(hdIdTramite.Value));
                    EstadoMesa = wfiplib.E_EstadoMesa.PromotoriaReconsidera;
                }

                //DateTime FechaInicio = Lista[0].FechaInicio;
                DateTime FechaInicio = DateTime.Now;
                foreach (DataRow row in dtMesaCalidad.Rows)
                {
                    // admTramiteMesa.reinicia(oTram.IdTramite, oTram.IdMesa, EstadoMesa, -100, -1, txObservaciones.Text.Trim().ToUpper(), manejo_sesion.Credencial.Id);
                    if (!admTramite.existeTramiteMesa(Convert.ToInt32(hdIdTramite.Value), Convert.ToInt32(row["Id"])))
                    {
                        admTramiteMesa.sendToCalidad(Convert.ToInt32(hdIdTramite.Value), Convert.ToInt32(row["Id"]), EstadoMesa, -100, -1, txObservacionesReconsidera.Text.Trim().ToUpper(), manejo_sesion.Credencial.Id);
                    }
                    else
                    {
                        admTramiteMesa.reinicia(Convert.ToInt32(hdIdTramite.Value), Convert.ToInt32(row["Id"]), EstadoMesa, -100, -1, txObservacionesReconsidera.Text.Trim().ToUpper(), manejo_sesion.Credencial.Id);
                    }
                }

                bool Resultado = admTramite.cambiaEstado(Convert.ToInt32(hdIdTramite.Value), wfiplib.E_EstadoTramite.PromotoriaReconsidera);
                registraBitacora(Convert.ToInt32(hdIdTramite.Value), FechaInicio, EstadoMesa, txObservacionesReconsidera.Text.Trim());
                Session.Contents.Remove("nota");
                Response.Redirect("listaMisTramites.aspx");
            }
            else
            {
                showMessage("Debe indicar los motivos por los cuales solicita la reconsideración del trámite...");
            }
        }

        protected void btnCtrlAplicaCancelacion_Click(object sender, EventArgs e)
        {
            try
            {
                Mensajes mensajes = new Mensajes();
                int intMotivo = -1;
                var nodos = treeListCancelar.GetSelectedNodes();

                if (nodos.Count > 0)
                {
                    wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(hdIdTramite.Value));
                    if ((new wfiplib.admTramite()).CancelarTramite(oTramite, manejo_sesion.Credencial, E_EstadoTramite.PromotoriaCancela))
                    {
                        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();

                        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                        oTramiteRechazo.IdTramiteMesa = -1;
                        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                        oTramiteRechazo.ObservacionPublica = "Cancelación desde Promotoría";
                        oTramiteRechazo.ObservacionPrivada = "Cancelación desde Promotoría";
                        oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.PromotoriaCancela;
                        oAdmTramiteRechazo.nuevo(oTramiteRechazo);

                        foreach (TreeListNode node in nodos)
                        {
                            intMotivo = Convert.ToInt32(node.GetValue("id"));
                            oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                            // oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                        }
                        mensajes.MostrarMensaje(this, "El Trámite " + oTramite.FolioCompuesto + " se Canceló Correctamente.");
                        Response.Redirect("esperaPromotoria.aspx", true);
                    }
                    else
                    {
                        mensajes.MostrarMensaje(this, "No se pudó cancelar el Trámite " + oTramite.FolioCompuesto + ".");
                    }
                }
                else
                    mensajes.MostrarMensaje(this, "Debe seleccionar al menos un motivo para al cancelación");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }


        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString()); 
                string IdTramite = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaIdTramite = stringBetween(s + ".", "Id=", ".");
                    if (BusqeudaIdTramite.Length > 0)
                    {
                        IdTramite = BusqeudaIdTramite;
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
                    urlCifrardo.Result = false;
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