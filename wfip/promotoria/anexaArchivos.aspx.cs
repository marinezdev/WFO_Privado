using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Activities.Expressions;
using System.Data;
using System.Configuration;

namespace wfip.promotoria
{
    public partial class anexaArchivos : System.Web.UI.Page
    {
        public string ArchivoMaximo1 {get; set;}
        public string ArchivoMaximo2 {get; set;}

        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];

            ArchivoMaximo1 = System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximo1"].ToString();
            ArchivoMaximo2 = System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximo2"].ToString();
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        private void showMessage(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.pnlMsgProcesando, typeof(string), "Alert", "alert('" + Mensaje + "');", true);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("documentos");
            Session.Remove("insumos");
            Session.Remove("AnexoArchivos");
            Session.Remove("tramite");
            Response.Redirect("esperaPromotoria.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lt_jsMsg.Text = "";

            pintaChecks();
            pintRegreso();

            if (!IsPostBack)
            {
                pintaCabeceraHtml();
                MuestraDocumentos();
                MuestraInsumos();
                MuestraDatos();
                //Session.Remove("documentos");
                //Session.Remove("insumos");
            }
            

            
            
        }

        private void MuestraDatos()
        {

            if (Session["AnexoArchivos"] != null)
            {
                wfiplib.AnexoArchivos oAnexoArchivos = (wfiplib.AnexoArchivos)Session["AnexoArchivos"];

                txIdAgente.Text = oAnexoArchivos.AgenteClave.ToString();
                NombreAgente();

                CheckBox1.Checked = oAnexoArchivos.CheckInsumos;
                ArchivosAdicionesles();

                if (oAnexoArchivos.IdTipoTramite != TextIdTipoTramite.Text.ToString() || oAnexoArchivos.TipoPersona != TextTipoPersona.Text.ToString())
                {

                }
                else
                {
                    

                    //string texto = "";
                    for (int i = 0; i < DocRequerid.Items.Count; i++)
                    {
                        if (DocRequerid.Items[i].Text == oAnexoArchivos.CheckDocumento[i].ToString())
                        {
                            DocRequerid.Items[i].Selected = true;
                        }
                        else
                        {
                            DocRequerid.Items[i].Selected = false;
                        }
                    }
                }
            }
        }

        private void GuardaDatos()
        {
            wfiplib.AnexoArchivos oAnexoArchivos = new wfiplib.AnexoArchivos();
            oAnexoArchivos.AgenteClave = txIdAgente.Text.ToString();
            if(CheckBox1.Checked == true)
            {
                oAnexoArchivos.CheckInsumos = true;
            }
            oAnexoArchivos.IdTipoTramite = TextIdTipoTramite.Text.ToString();
            oAnexoArchivos.TipoPersona = TextTipoPersona.Text.ToString();

            for (int i = 0; i < DocRequerid.Items.Count; i++)
            {
                if (DocRequerid.Items[i].Selected)
                {
                    oAnexoArchivos.CheckDocumento.Add(DocRequerid.Items[i].Text.ToString());
                }
                else
                {
                    oAnexoArchivos.CheckDocumento.Add("");
                }
            }
            Session["AnexoArchivos"] = oAnexoArchivos;
        }

        private void pintRegreso()
        {
            if (Session["URL"] != null)
            {
                //Label2.Text = Session["URL"].ToString();
                Regredar.Visible = true;
            }
        }

        protected void BtnposBack(object sender, EventArgs e)
        {
            string url = Session["URL"].ToString();
            GuardaDatos();
            Response.Redirect(url);
        }

        private void pintaChecks()
        {
            if (Session["tramite"] != null)
            {
                wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                TextIdTipoTramite.Text = oTramite.IdTipoTramite.ToString();
                
                switch ((wfiplib.E_TipoTramite)Convert.ToInt32((oTramite.IdTipoTramite)))
                {
                    // VIDA
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                        wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                        DataTable tableVida = new wfiplib.admEmisionVG().Checks2(oEmisionVida.TipoPersona.ToString("d"), wfiplib.E_TipoTramite.indPriEmisionVida);
                        Checks(tableVida);
                        TextTipoPersona.Text = oEmisionVida.TipoPersona.ToString("d");
                        
                        break;
                    // VIDA CITA MEDICA
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        wfiplib.EmisionVG oEmisionVidaCM = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                        DataTable tableVidaCM = new wfiplib.admEmisionVG().Checks2(oEmisionVidaCM.TipoPersona.ToString("d"), wfiplib.E_TipoTramite.indPriEmisionVida);
                        Checks(tableVidaCM);
                        TextTipoPersona.Text = oEmisionVidaCM.TipoPersona.ToString("d");
                        break;
                    // GMM
                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()];
                        DataTable tableGMM = new wfiplib.admEmisionVG().Checks2(oEmisionGmm.TipoPersona.ToString("d"), wfiplib.E_TipoTramite.indPriEmisionGMM);
                        Checks(tableGMM);
                        TextTipoPersona.Text = oEmisionGmm.TipoPersona.ToString("d");
                        break;
                }
                /*
                if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionVida)
                {
                    wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                    // DataTable table = new wfiplib.admEmisionVG().Checks(oEmisionVida.TipoPersona.ToString("d"), oTramite.IdTipoTramite.ToString("d"));
                    DataTable table = new wfiplib.admEmisionVG().Checks2(oEmisionVida.TipoPersona.ToString("d"), oTramite.IdTipoTramite);
                    Checks(table);
                }
                else if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.indPriEmisionGMM)
                {
                    wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()];
                    // DataTable table = new wfiplib.admEmisionVG().Checks(oEmisionGmm.TipoPersona.ToString("d"), oTramite.IdTipoTramite.ToString("d"));
                    DataTable table = new wfiplib.admEmisionVG().Checks2(oEmisionGmm.TipoPersona.ToString("d"), oTramite.IdTipoTramite);
                    Checks(table);
                }

                /*
                if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.emisionVidaIndividual)
                {
                    wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.emisionVidaIndividual.ToString()];
                    DataTable table = new wfiplib.admEmisionVG().Checks(oEmisionVida.TipoPersona.ToString("d"), oTramite.IdTipoTramite.ToString("d"));
                    Checks(table);
                }

                if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.emisionGmmIndividual)
                {
                    wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.emisionGmmIndividual.ToString()];
                    DataTable table = new wfiplib.admEmisionVG().Checks(oEmisionGmm.TipoPersona.ToString("d"), oTramite.IdTipoTramite.ToString("d"));
                    Checks(table);
                }
                */

                if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.ServicioGmm)
                {
                    wfiplib.ServicioGmmP oServicioGmmP = (wfiplib.ServicioGmmP)Session[wfiplib.E_TipoTramite.ServicioGmm.ToString()];
                    String P1 = "Cambios sin afectación a prima";
                    String SP1 = "";
                    String SP2 = "";
                    String SP3 = "";
                    String SP4 = "";
                    String SP5 = "";
                    String SP6 = "";
                    String SP7 = "";
                    String SP8 = "";
                    String SP9 = "";
                    String P2 = "Rehabilitación";
                    String SP10 = "";
                    String P3 = "Cambio de conducto de cobro";
                    String SP11 = "";
                    String SP12 = "";
                    String SP13 = "";
                    String P4 = "Actualización de información artículo 492";
                    String SP14 = "";
                    String P5 = "Cambios con afectación a prima";
                    String SP15 = "";
                    String SP16 = "";
                    String SP17 = "";
                    String SP18 = "";
                    String SP19 = "";
                    String SP20 = ""; 
                    String P6 = "Rescates, retiros y cancelaciones";
                    String SP21 = "";
                    String P7 = "Aclaración de pagos";
                    String SP22 = "";
                    String SP23 = "";

                    if (oServicioGmmP.C128 == 1)
                    {
                        SP1 = "Modificación de nombre y apellidos";
                    }
                    if (oServicioGmmP.C129 == 1)
                    {
                        SP2 = "Cambio de contratante";
                    }
                    if (oServicioGmmP.C1210 == 1)
                    {
                        SP3 = "Cambio de domicilio";
                    }
                    if (oServicioGmmP.C1211 == 1)
                    {
                        SP4 = "Corrección registro federal de contribuyentes";
                    }
                    if (oServicioGmmP.C1212 == 1)
                    {
                        SP5 = "Cambio de beneficiario cobertura muerte accidental y últimos gastos";
                    }
                    if (oServicioGmmP.C1213 == 1)
                    {
                        SP6 = "Duplicado de póliza";
                    }
                    if (oServicioGmmP.C1214 == 1)
                    {
                        SP7 = "Duplicado de endoso";
                    }
                    if (oServicioGmmP.C1215 == 1)
                    {
                        SP8 = "Cambio clave de agente";
                    }
                    if (oServicioGmmP.C1216 == 1)
                    {
                        SP9 = "Reconocimiento de antigüedad gastos médicos mayores";
                    }
                    if (oServicioGmmP.C1217 == 1)
                    {
                        SP10 = "Rehabilitación";
                    }
                    ///////////////////////////////
                    if (oServicioGmmP.C1218 == 1)
                    {
                        SP11 = "Cambio de conducto de cobro a tarjeta de crédito y débito";
                    }
                    if (oServicioGmmP.C1219 == 1)
                    {
                        SP12 = "Cambio de conducto de cobro a Clave Bancaria";
                    }
                    if (oServicioGmmP.C1220 == 1)
                    {
                        SP13 = "Cambio de conducto de cobro a agente";
                    }
                    //////////////////////////////////////
                    if (oServicioGmmP.C1221 == 1)
                    {
                        SP14 = "Actualización de información artículo 492 de la ley de instituciones de seguros y de fianzas";
                    }
                    //////////////////////////////
                    if (oServicioGmmP.C1222 == 1)
                    {
                        SP15 = "Cambio de forma de pago";
                    }
                    if (oServicioGmmP.C1223 == 1)
                    {
                        SP16 = "Reconsideración de dictamen";
                    }
                    if (oServicioGmmP.C1224 == 1)
                    {
                        SP17 = "Inclusión o exclusión de extra primas";
                    }
                    if (oServicioGmmP.C1225 == 1)
                    {
                        SP18 = "Inclusión o exclusión de coberturas";
                    }
                    if (oServicioGmmP.C1226 == 1)
                    {
                        SP19 = "Inclusión y exclusión de dependientes";
                    }
                    if (oServicioGmmP.C1227 == 1)
                    {
                        SP20 = "Inclusión o exclusión de asegurados";
                    }
                    //// P6
                    if (oServicioGmmP.C1229 == 1)
                    {
                        SP21 = "Cancelación de póliza";
                    }
                    //// P7
                    if (oServicioGmmP.C1231 == 1)
                    {
                        SP22 = "Devolución de primas";
                    }
                    if (oServicioGmmP.C1232 == 1)
                    {
                        SP23 = "Aclaración de pagos";
                    }
                    DataTable table = new wfiplib.admServicioGmm().Checks(P1, SP1, P1, SP2, P1, SP3, P1, SP4, P1, SP5, P1, SP6, P1, SP7, P1, SP8, P1, SP9, P2, SP10, P3, SP11, P3, SP12, P3, SP13, P4, SP14, P5, SP15, P5, SP16, P5, SP17, P5, SP18, P5, SP19, P5, SP20, P6, SP21, P7, SP22, P7, SP23, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                    Checks2(table);
                    /*
                    String P2 = "Rehabilitación";
                    String P3 = "Cambio de conducto de cobro";
                    String P4 = "Actualización de información artículo 492";
                    String P5 = "Cambios con afectación a prima";
                    String P6 = "Rescates, retiros y cancelaciones";
                    String P7 = "Aclaración de pagos";
                    String P8 = "";
                    String SP1 = ""; String SP2 = ""; String SP3 = "";  String SP5 = ""; String SP6 = ""; String SP7 = ""; String SP8 = ""; String SP9 = ""; String SP10 = ""; String SP11 = ""; String SP12 = ""; String SP13 = ""; String SP14 = ""; String SP15 = ""; String SP16 = ""; String SP17 = ""; String SP18 = "";
                    String SP19 = ""; String SP20 = ""; String SP21 = ""; String SP22 = ""; String SP23 = ""; String SP24 = ""; String SP25 = ""; String SP26 = ""; String SP27 = ""; String SP28 = ""; String SP29 = ""; String SP30 = ""; String SP31 = ""; String SP32 = ""; String SP33 = ""; String SP34 = ""; String SP35 = "";
                    String SP36 = ""; String SP37 = ""; String SP38 = ""; String SP39 = "";

                    /////// P1
                    if (oServicioGmmP.C128 == 1)
                    {
                        SP1 = "Modificación de nombre y apellidos";
                    }
                    if (oServicioGmmP.C129 == 1)
                    {
                        SP2 = "Cambio de contratante";
                    }
                    if (oServicioGmmP.C1210 == 1)
                    {
                        SP3 = "Cambio de domicilio";
                    }
                    if (oServicioGmmP.C1211 == 0)
                    {
                        SP4 = "Corrección de registro federal de contribuyentes";
                    }/*
                    if (oServicioGmmP.C1212 == 1)
                    {
                        SP5 = "Cambio de beneficiario cobertura muerte accidental y últimos gastos";
                    }
                    if (oServicioGmmP.C1213 == 1)
                    {
                        SP6 = "Duplicado de póliza";
                    }
                    if (oServicioGmmP.C1214 == 1)
                    {
                        SP7 = "Duplicado de endoso";
                    }
                    if (oServicioGmmP.C1215 == 1)
                    {
                        SP8 = "Cambio clave de agente";
                    }
                    if (oServicioGmmP.C1216 == 1)
                    {
                        SP9 = "Reconocimiento de antigüedad gastos médicos mayores";
                    }
                    ///// P2
                    if (oServicioGmmP.C1217 == 1)
                    {
                        SP10 = "Rehabilitación";
                    }
                    ///// P3
                    if (oServicioGmmP.C1218 == 1)
                    {
                        SP11 = "Cambio de conducto de cobro a tarjeta de crédito y débito";
                    }
                    if (oServicioGmmP.C1219 == 1)
                    {
                        SP12 = "Cambio de conducto de cobro a Clave Bancaria";
                    }
                    if (oServicioGmmP.C1220 == 1)
                    {
                        SP13 = "Cambio de conducto de cobro a agente";
                    }
                    ////// P4
                    if (oServicioGmmP.C1221 == 1)
                    {
                        SP14 = "Actualización de información artículo 492 de la ley de instituciones de seguros y de fianzas";
                    }
                    ////// P5
                    if (oServicioGmmP.C1222 == 1)
                    {
                        SP15 = "Cambio de forma de pago";
                    }
                    if (oServicioGmmP.C1223 == 1)
                    {
                        SP16 = "Reconsideración de dictamen";
                    }
                    if (oServicioGmmP.C1224 == 1)
                    {
                        SP17 = "Inclusión o exclusión de extra primas";
                    }
                    if (oServicioGmmP.C1225 == 1)
                    {
                        SP18 = "Inclusión o exclusión de coberturas";
                    }
                    if (oServicioGmmP.C1226 == 1)
                    {
                        SP19 = "Inclusión y exclusión de dependientes";
                    }
                    if (oServicioGmmP.C1227 == 1)
                    {
                        SP20 = "Inclusión o exclusión de asegurados";
                    }
                    //// P6
                    if (oServicioGmmP.C1229 == 1)
                    {
                        SP21 = "Cancelación de póliza";
                    }
                    //// P7
                    if (oServicioGmmP.C1231 == 1)
                    {
                        SP22 = "Devolución de primas";
                    }
                    if (oServicioGmmP.C1232 == 1)
                    {
                        SP23 = "Aclaración de pagos";
                    }
                    */
                    //DataTable table = new wfiplib.admServicioGmm().Checks(P1, SP1, P1, SP2, P1, SP3, P1, , P1, SP5, P1, SP6, P1, SP7, P2, SP8, P2, SP9, P2, SP10, P2, SP11, P2, SP12, P2, SP13, P2, SP14, P2, SP15, P3, SP16, P4, SP17, P4, SP18, P4, SP19, P5, SP20, P6, SP21, P6, SP22, P6, SP23, P6, SP24, P6, SP25, P6, SP26, P6, SP27, P6, SP28, P6, SP29, P6, SP30, P6, SP31, P6, SP32, P6, SP33, P7, SP34, P7, SP35, P7, SP36, P7, SP37, P8, SP38, P8, SP39);

                }
                if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.serviciosVida)
                {
                    //wfiplib.serviciosVidaP oserviciosVidaP = (wfiplib.serviciosVidaP)Session[wfiplib.E_TipoTramite.emisionGmmIndividual.ToString()];
                    wfiplib.serviciosVidaP oServiciosVidaP = (wfiplib.serviciosVidaP)Session[wfiplib.E_TipoTramite.serviciosVida.ToString()];
                    String P1 = "Cambios sin afectación vida - (anexar formato 5)";
                    String P2 = "Cambios sin afectación vida";
                    String P3 = "Rehabilitación";
                    String P4 = "Cambio de conducto de cobro";
                    String P5 = "Duplicado de recibo";
                    String P6 = "Cambios con afectación vida";
                    String P7 = "Rescates, retiros y cancelaciones";
                    String P8 = "Aclaración de pagos";
                    String SP1 = "";String SP2 = "";String SP3 = "";String SP4 = "";String SP5 = "";String SP6 = "";String SP7 = "";String SP8 = "";String SP9 = "";String SP10 = "";String SP11 = ""; String SP12 = "";String SP13 = "";String SP14 = "";String SP15 = ""; String SP16 = "";String SP17 = "";String SP18 = "";
                    String SP19 = "";String SP20 = "";String SP21 = "";String SP22 = "";String SP23 = "";String SP24 = "";String SP25 = "";String SP26 = "";String SP27 = "";String SP28 = "";String SP29 = "";String SP30 = "";String SP31 = "";String SP32 = ""; String SP33 = "";String SP34 = "";String SP35 = "";
                    String SP36 = "";String SP37 = "";String SP38 = ""; String SP39 = "";

                    /////// P1/
                    if (oServiciosVidaP.C119 == 1)
                    {
                        SP1 = "Corrección de nombre y apellidos";
                    }
                    if(oServiciosVidaP.C1110 == 1)
                    {
                        SP2 = "Cambio de contratante";
                    }
                    if (oServiciosVidaP.C1111 == 1)
                    {
                        SP3 = "Cambio de domicilio";
                    }
                    if (oServiciosVidaP.C1112 == 1)
                    {
                        SP4 = "Corrección de registro federal de contribuyentes";
                    }
                    if (oServiciosVidaP.C1113 == 1)
                    {
                        SP5 = "Corrección de género";
                    }
                    if (oServiciosVidaP.C1114 == 1)
                    {
                        SP6 = "Corrección de estado civil";
                    }
                    if (oServiciosVidaP.C1115 == 1)
                    {
                        SP7 = "Actualización de información artículo 492 ley de instituciones de seguro y de fianzas";
                    }
                    //////////////////////////// P2
                    if (oServiciosVidaP.C1116 == 1)
                    {
                        SP8 = "Cambio de beneficiario (anexar formato de identificación de beneficiarios)";
                    }
                    if (oServiciosVidaP.C1117 == 1)
                    {
                        SP9 = "Aclaración de estado de cuenta";
                    }
                    if (oServiciosVidaP.C1118 == 1)
                    {
                        SP10 = "Carta estatus";
                    }
                    if (oServiciosVidaP.C1119 == 1)
                    {
                        SP11 = "Duplicado de póliza";
                    }
                    if (oServiciosVidaP.C1120 == 1)
                    {
                        SP12 = "Duplicado de endoso";
                    }
                    if (oServiciosVidaP.C1121 == 1)
                    {
                        SP13 = "Cambio clave de agente";
                    }
                    if (oServiciosVidaP.C1122 == 1)
                    {
                        SP14 = "Cambio de fecha de emisión";
                    }
                    if (oServiciosVidaP.C1123 == 1)
                    {
                        SP15 = "Estado de cuenta por jubilación y capitalizable a corto plazo";
                    }
                    /////////////////// P3
                    if (oServiciosVidaP.C1124 == 1)
                    {
                        SP16 = "Rehabilitación";
                    }
                    /////////////////// P4
                    if (oServiciosVidaP.C1125 == 1)
                    {
                        SP17 = "Cambio de conducto de cobro a tarjeta de crédito y débito";
                    }
                    if (oServiciosVidaP.C1126 == 1)
                    {
                        SP18 = "Cambio de conducto de cobro a CLABE Bancaria";
                    }
                    if (oServiciosVidaP.C1127 == 1)
                    {
                        SP19 = "Cambio de conducto de cobro a agente";
                    }
                    //////////////////// P5
                    if (oServiciosVidaP.C1128 == 1)
                    {
                        SP20 = "Duplicado de recibo";
                    }
                    //////////////////// P6
                    if (oServiciosVidaP.C1129 == 1)
                    {
                        SP21 = "Cambio de moneda";
                    }
                    if (oServiciosVidaP.C1130 == 1)
                    {
                        SP22 = "Cambio de suma asegurada";
                    }
                    if (oServiciosVidaP.C1131 == 1)
                    {
                        SP23 = "Cambio de forma de pago";
                    }
                    if (oServiciosVidaP.C1132== 1)
                    {
                        SP24 = "Cambio de plan";
                    }
                    if (oServiciosVidaP.C1133 == 1)
                    {
                        SP25 = "Corrección de edad / corrección de fecha de nacimiento";
                    }
                    if (oServiciosVidaP.C1134 == 1)
                    {
                        SP26 = "Reconsideración de dictamen";
                    }
                    if (oServiciosVidaP.C1135 == 1)
                    {
                        SP27 = "Inclusión o exclusión de extra primas";
                    }
                    if (oServiciosVidaP.C1136 == 1)
                    {
                        SP28 = "Inclusión o exclusión de beneficios adicionales";
                    }
                    if (oServiciosVidaP.C1137 == 1)
                    {
                        SP29 = "Inclusión o exclusión del beneficio de no fumador";
                    }
                    if (oServiciosVidaP.C1138 == 1)
                    {
                        SP30 = "Inclusión de plan capitalizable corto plazo";
                    }
                    if (oServiciosVidaP.C1139 == 1)
                    {
                        SP31 = "Cambio de seguro prorrogado";
                    }
                    if (oServiciosVidaP.C1140 == 1)
                    {
                        SP32 = "Cambio de seguro saldado";
                    }
                    if (oServiciosVidaP.C1141 == 1)
                    {
                        SP33 = "Otros";
                    }
                    /////////////////////// P7
                    if (oServiciosVidaP.C1142 == 1)
                    {
                        SP34 = "Cancelación de póliza";
                    }
                    if (oServiciosVidaP.C1143 == 1)
                    {
                        SP35 = "Rescate de fondo de pólizas de jubilación y capitalizable a corto plazo para pago de prima-póliza de vida mismo Asegurado";
                    }
                    if (oServiciosVidaP.C1144 == 1)
                    {
                        SP36 = "Rescate total";
                    }
                    if (oServiciosVidaP.C1145 == 1)
                    {
                        SP37 = "Rescate parcial";
                    }
                    /////////////////////// P8
                    if (oServiciosVidaP.C1146 == 1)
                    {
                        SP38 = "Devolución de primas";
                    }
                    if (oServiciosVidaP.C1147 == 1)
                    {
                        SP39 = "Aclaración de pagos";
                    }
                    DataTable table = new wfiplib.admServiciosVida().Checks(
                        oTramite.IdTipoTramite.ToString("d"),P1, SP1,P1, SP2,P1, SP3, P1, SP4,P1, SP5,P1, SP6, P1, SP7,P2, SP8,P2, SP9,P2, SP10,P2, SP11,P2, SP12, P2, SP13,P2, SP14,P2, SP15,P3, SP16,P4, SP17,P4, SP18,P4, SP19,P5, SP20,P6, SP21,P6, SP22,P6, SP23,P6, SP24,P6, SP25,P6, SP26,P6, SP27, P6, SP28,P6, SP29,P6, SP30,P6, SP31,P6, SP32,P6, SP33,P7, SP34,P7, SP35,P7, SP36,P7, SP37,P8, SP38,P8, SP39);
                    Checks2(table);
                    //DataTable table = new wfiplib.admServiciosVida().Checks(oserviciosVidaP.TipoPersona.ToString("d"), oTramite.IdTipoTramite.ToString("d"));
                }
            }
        }

        private void Checks2(DataTable table)
        {
            DocRequerid.DataSource = table;
            DocRequerid.ID = "IdDocRecEmicion";
            DocRequerid.DataTextField = "Documentos";
            DocRequerid.DataValueField = "IdDocRecEmicion";
            DocRequerid.DataBind();
            /*
            foreach (DataRow row in table.Rows)
            {
                CheckBox chkList1 = new CheckBox();
                chkList1.Text = row[0].ToString();
                chkList1.ID = "DocRequerido_" + row[0].ToString();
                chkList1.Font.Name = row[0].ToString();
                chkList1.Font.Size = 9;
                FORMULAIRO.Controls.Add(chkList1);
                FORMULAIRO.Controls.Add(new LiteralControl("<br>"));
            }
            */
        }

        private void Checks(DataTable table)
        {
            DocRequerid.DataSource = table;
            DocRequerid.ID = "IdDocRecEmicion";
            DocRequerid.DataTextField = "Documentos";
            DocRequerid.DataValueField = "IdDocRecEmicion";
            DocRequerid.DataBind();
        }
        
        private void pintaCabeceraHtml()
        {
            if(Session["tramite"] != null){
                wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];

                string CabeceraHtml = "";
                switch (oTramite.IdTipoTramite)
                {
                    // VIDA
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                        CabeceraHtml = ((wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()]).CabeceraHtml;
                        break;
                    // VIDA CON CITA MEDICA 
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        CabeceraHtml = ((wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()]).CabeceraHtml;
                        break;
                    // GASTOS MEDICOS
                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        CabeceraHtml = ((wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()]).CabeceraHtml;
                        break;

                }
                ltInfContratante.Text = CabeceraHtml;
            }
        }

        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            string script = "";

            /* LISTA LOS ARCHIVOS DEL DOCUMENTO */
            List<wfiplib.expediente> LstArchivosDocumento = new List<wfiplib.expediente>();
            /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
            if (Session["documentos"] != null)
            {
                LstArchivosDocumento = (List<wfiplib.expediente>)Session["documentos"];
            }

            if (fileUpDocumento.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpDocumento.FileName).ToLower();
                string fileExtension2 = "";
                if (".pdf".Contains(fileExtension) ^ ".jpg".Contains(fileExtension) ^ ".png".Contains(fileExtension))
                {
                    try
                    {
                        wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                        wfiplib.expediente oDocumento = new wfiplib.expediente();
                        int fileSize = fileUpDocumento.PostedFile.ContentLength;
                        //if (fileSize < 41943040)
                        if (fileSize < int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximoByte1"].ToString()))
                        {
                            int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                            string nombreArchivo = IdArchivo.ToString().PadLeft(8, '0');
                            string directorioTemporal = Server.MapPath("~"); //+ "\\DocsUp\\";

                            // string directorioTemporal = "C:\\inetpub\\wwwroot\\fto\\DocsUp\\";
                            // script = "alert('" + directorioTemporal + nombreArchivo + fileExtension  + "');";
                            // ScriptManager.RegisterStartupScript(this, GetType(),"ServerControlScript", script, true);

                            fileUpDocumento.PostedFile.SaveAs(directorioTemporal + nombreArchivo + fileExtension);

                            /* Convertir EL ARCHIVO A PDF ANTES DE SER ENVIADO A LA RUTA EN ESPECIFICO */
                            wfiplib.admPdfFusion ConJPGaPDF = new wfiplib.admPdfFusion();
                            if (!fileExtension.Equals(".pdf"))
                            {
                                if (ConJPGaPDF.ConviertePDF(directorioTemporal + nombreArchivo + fileExtension, directorioTemporal + nombreArchivo + ".pdf"))
                                {
                                    fileExtension2 = ".pdf";
                                }
                            }
                            
                            fileExtension2 = ".pdf";
                            
                            bool archivoEnPdf = false;
                            if (!fileExtension2.Equals(".pdf"))
                            {
                                
                                //string origen = directorioTemporal + nombreArchivo + fileExtension;
                                //string destino = directorioTemporal + nombreArchivo + ".pdf";
                                //Jpgpdf obj = new Jpgpdf();
                                //archivoEnPdf = obj.convierte(origen, destino);
                                //if (archivoEnPdf) nombreArchivo = nombreArchivo + ".pdf";
                                archivoEnPdf = false;
                            }
                            else
                            {
                                nombreArchivo = nombreArchivo + fileExtension2;
                                archivoEnPdf = true;
                            }

                            if (archivoEnPdf)
                            {

                                oDocumento.IdTramite = oTramite.Id;
                                oDocumento.Id = IdArchivo;
                                oDocumento.NmArchivo = nombreArchivo;
                                oDocumento.NmOriginal = fileUpDocumento.FileName;
                                //oDocumento.NmOriginal = nombreArchivo;
                                oDocumento.Activo = wfiplib.E_SiNo.Si;
                                oDocumento.Fusion = wfiplib.E_SiNo.No;
                                oDocumento.RutaTemporal = directorioTemporal;

                                LstArchivosDocumento.Add(oDocumento);
                                lstDocumentos.DataSource = LstArchivosDocumento;
                                lstDocumentos.DataValueField = "Id";
                                lstDocumentos.DataTextField = "NmOriginal";
                                lstDocumentos.DataBind();

                                Session["documentos"] = LstArchivosDocumento;
                            }
                            else
                            {
                                enviaMsgCliente("El archivo no se puede convertir a PDF.");
                            }
                        }
                        else
                        {
                            enviaMsgCliente("El archivo excede el límite de "+ ArchivoMaximo1 + "MB.");
                        }
                    }
                    catch (Exception ex)
                    {
                        enviaMsgCliente("Problemas con el archivo " + ex.Message);

                        script = "alert('" + ex.Message + "');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);


                        //enviaMsgCliente("Problemas con el archivo " + srcFilename);
                    }
                }
                else { enviaMsgCliente("El archivo no es un PDF o JPG."); }
            }
            else
            {
                /* No a cargado ningun tipo de archivo */
                enviaMsgCliente("No has seleccionado un archivo ");
            }
        }

        protected void btnSubirInsumo_Click(object sender, EventArgs e)
        {
            List<wfiplib.insumos> LstArchivosInsumo = new List<wfiplib.insumos>();
            if (Session["insumos"] != null) { LstArchivosInsumo = (List<wfiplib.insumos>)Session["insumos"]; }

            if (fileUpInsumo.HasFile)
            {
                try
                {
                    String fileExtension = System.IO.Path.GetExtension(fileUpInsumo.FileName).ToLower();
                    wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                    wfiplib.insumos oInsumo = new wfiplib.insumos();
                    int fileSize = fileUpInsumo.PostedFile.ContentLength;
                    //if (fileSize < 41943040)
                    if (fileSize < int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximoByte2"].ToString()))
                    {
                        int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                        string nombreArchivo = IdArchivo.ToString().PadLeft(8, '0') + fileExtension;
                        //string directorioTemporal = Server.MapPath("~") + Properties.Settings.Default.dirCargaArchivos;
                        //string directorioTemporal = Server.MapPath("~") + "\\DocsInsumos\\";
                        string directorioTemporal = Server.MapPath("~") + (new wfiplib.insumos()).CarpetaInicial;
                        fileUpInsumo.PostedFile.SaveAs(directorioTemporal + nombreArchivo);

                        oInsumo.IdTramite = oTramite.Id;
                        oInsumo.Id = IdArchivo;
                        oInsumo.NmArchivo = nombreArchivo;
                        oInsumo.NmOriginal = fileUpInsumo.FileName;
                        oInsumo.Activo = wfiplib.E_SiNo.Si;
                        oInsumo.RutaTemporal = directorioTemporal;

                        LstArchivosInsumo.Add(oInsumo);
                        lstInsumos.DataSource = LstArchivosInsumo;
                        lstInsumos.DataValueField = "Id";
                        lstInsumos.DataTextField = "NmOriginal";
                        lstInsumos.DataBind();

                        Session["insumos"] = LstArchivosInsumo;
                    }
                    else
                    {
                        enviaMsgCliente("El archivo excede el límite de "+ ArchivoMaximo2 + "MB.");
                    }
                }
                catch (Exception ex)
                {
                    enviaMsgCliente("Problemas con el archivo " + ex.Message);
                }
            }
            else
            {
                /* No a cargado ningun tipo de archivo */
                enviaMsgCliente("No has seleccionado un archivo ");
            }
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (Session["tramite"] != null)
            {
                List<wfiplib.expediente> LstExpediente = new List<wfiplib.expediente>();

                if (Session["documentos"] != null)
                    LstExpediente = (List<wfiplib.expediente>)Session["documentos"];

                if (LstExpediente.Count > 0)
                {
                    List<wfiplib.insumos> LstInsumos = new List<wfiplib.insumos>();
                    if (Session["insumos"] != null)
                        LstInsumos = (List<wfiplib.insumos>)Session["insumos"];

                    wfiplib.admTramite oAdmTramite = new wfiplib.admTramite();
                    wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];

                    oTramite.AgenteClave = txIdAgente.Text.Trim();

                    // registraChecks();
                    if (oAdmTramite.nuevo(oTramite))
                    {
                        if (oAdmTramite.nuevoFolio(oTramite))
                        {
                            if (registraDatosPorTipoDeTramite(oTramite.IdTipoTramite, oTramite))
                            {
                                string msgError = registraDocumentos(oTramite.Id, LstExpediente);
                                if (string.IsNullOrEmpty(msgError))
                                {
                                    registraInsumos(oTramite.Id, LstInsumos);
                                    if (registraPaso(oTramite.Id, wfiplib.E_EstadoMesa.Registro))
                                    {
                                        string strFolio = oAdmTramite.getFolio(oTramite.Id);
                                        Session.Remove("documentos");
                                        Session.Remove("insumos");
                                        Session.Remove("AnexoArchivos");
                                        Response.Redirect("listaMisTramites.aspx?msg=1&folio=" + strFolio);
                                    }
                                    else
                                    {
                                        oAdmTramite.elimina(oTramite.Id);
                                        eliminaDatosPorTipoDeTramite(oTramite);
                                        (new wfiplib.admExpediente()).eliminaTodos(oTramite.Id);
                                        enviaMsgCliente("Problemas al iniciar el flujo del trámite, intente nuevamente.");
                                    }
                                }
                                else
                                {
                                    oAdmTramite.elimina(oTramite.Id);
                                    eliminaDatosPorTipoDeTramite(oTramite);
                                    enviaMsgCliente(msgError);
                                }
                            }
                            else
                            {
                                oAdmTramite.elimina(oTramite.Id);
                                enviaMsgCliente("Problemas al registrar los datos del trámite, intente nuevamente.");
                            }
                        }
                    }
                }
                else
                {
                    enviaMsgCliente("Es necesario que ingrese los archivos para los documentos requeridos.");
                }
            } 
            else
            {
                enviaMsgCliente("Problemas al registrar el trámite, intente nuevamente.");
            }
        }

        private bool registraDatosPorTipoDeTramite(wfiplib.E_TipoTramite pIdTipoTramite, wfiplib.tramiteP oTramite)
        {
            bool resultado = false;
            switch (pIdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                    (new wfiplib.admTramite()).registraBitacora(oTramite, manejo_sesion.Credencial, oEmisionVida.Detalle);
                    resultado = (new wfiplib.admEmisionVG()).nuevo(oEmisionVida);
                    Session["tramite"] = null;
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()];
                    (new wfiplib.admTramite()).registraBitacora(oTramite, manejo_sesion.Credencial, oEmisionGmm.Detalle);
                    resultado = (new wfiplib.admEmisionVG()).nuevo(oEmisionGmm);
                    Session["tramite"] = null;
                    break;

                case wfiplib.E_TipoTramite.serviciosVida:
                    wfiplib.serviciosVidaP oServiciosVida = (wfiplib.serviciosVidaP)Session[wfiplib.E_TipoTramite.serviciosVida.ToString()];
                    resultado = (new wfiplib.admServiciosVida()).nuevo(oServiciosVida);
                    Session["tramite"] = null;
                    break;

                case wfiplib.E_TipoTramite.ServicioGmm:
                    wfiplib.ServicioGmmP oServiciosGmm = (wfiplib.ServicioGmmP)Session[wfiplib.E_TipoTramite.ServicioGmm.ToString()];
                    resultado = (new wfiplib.admServicioGmm()).nuevo(oServiciosGmm);
                    Session["tramite"] = null;
                    break;
            }
            return resultado;
        }

        private bool eliminaDatosPorTipoDeTramite(wfiplib.tramiteP pTramite)
        {
            bool resultado = false;
            //switch (pTramite.IdTipoTramite)
            //{
            //    case wfiplib.E_TipoTramite.serviciosVida:
            //        resultado = (new wfiplib.admServiciosVida()).elimina(pTramite.Id);
            //        break;
            //    case wfiplib.E_TipoTramite.ServicioGmm:
            //        resultado = (new wfiplib.admServicioGmm()).elimina(pTramite.Id);
            //        break;
            //    case wfiplib.E_TipoTramite.emisionVidaIndividual:
            //        resultado = (new wfiplib.admEmisionVida()).elimina(pTramite.Id);
            //        break;
            //    case wfiplib.E_TipoTramite.emisionGmmIndividual:
            //        resultado = (new wfiplib.admEmisionGmm()).elimina(pTramite.Id);
            //        break;
            //    default:
            //        break;
            //}
            return resultado;
        }

        private string registraDocumentos(int pIdTramite, List<wfiplib.expediente> pLstDocumentos)
        {
            string msgError = "";

            string strRutaServidor = "";
            string strArchivoOrigen = "";

            try
            {
                strRutaServidor = Server.MapPath("~");// + "\\DocsUp\\";


                wfiplib.admExpediente oAdmExp = new wfiplib.admExpediente();
                List<string> lstArchivos = new List<string>();
                foreach (wfiplib.expediente oDocumento in pLstDocumentos)
                {
                    /*
                    string Origen = oDocumento.RutaTemporal + oDocumento.NmArchivo;
                    string Destino = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, oDocumento.Id) + oDocumento.NmArchivo;
                    File.Copy(Origen, Destino, true);
                    if (File.Exists(Destino))
                    {
                        oAdmExp.Nuevo(oDocumento);
                        lstArchivos.Add(Origen);
                    }
                    */

                    strArchivoOrigen = Server.MapPath("~"); // + "\\DocsUp\\" + oDocumento.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        oAdmExp.Nuevo(oDocumento);
                        lstArchivos.Add(strArchivoOrigen);
                    }
                }

                int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                string nombreFusion = IdArchivo.ToString().PadLeft(8, '0') + ".pdf";
                // string rutaTemporal = Server.MapPath("~") + Properties.Settings.Default.dirCargaArchivos;
                /* FUCIONA LOS ARCHIVOS*/


                // msgError = (new wfiplib.admPdfFusion()).Fusiona(lstArchivos, rutaTemporal + nombreFusion);
                msgError = (new wfiplib.admPdfFusion()).Fusiona(lstArchivos, strRutaServidor + nombreFusion);
                if (string.IsNullOrEmpty(msgError))
                {
                    // string Destino = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, IdArchivo) + nombreFusion;
                    // File.Copy(rutaTemporal + nombreFusion, Destino, true);

                    wfiplib.expediente oFusion = new wfiplib.expediente();
                    oFusion.Id = IdArchivo;
                    oFusion.IdTramite = pIdTramite;
                    oFusion.NmArchivo = nombreFusion;
                    oFusion.NmOriginal = "";
                    oFusion.Activo = wfiplib.E_SiNo.Si;
                    oFusion.Fusion = wfiplib.E_SiNo.Si;
                    oAdmExp.Nuevo(oFusion);

                    msgError = "";
                }
                              

                ;

            }

            catch (Exception ex) { msgError = ex.Message; }
            return "";
        }

        private bool registraInsumos(int pIdTramite, List<wfiplib.insumos> pLstInsumos)
        {
            bool resultado = false;
            try
                {
                    wfiplib.admInsumos oAdmInsumos = new wfiplib.admInsumos();
                    foreach (wfiplib.insumos oInsumo in pLstInsumos)
                    {
                        string Destino = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, oInsumo.Id) + oInsumo.NmArchivo;
                        File.Copy(oInsumo.RutaTemporal + oInsumo.NmArchivo, Destino, true);
                        if (File.Exists(Destino)) 
                        { 
                            oAdmInsumos.nuevo(oInsumo); 
                        }
                    }
                resultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            return resultado;
        }

        private bool registraPaso(int pIdTramite, wfiplib.E_EstadoMesa statusMesa)
        {
            bool resultado = false;
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);

            List<wfiplib.mesa> lstSiguientesMesas = (new wfiplib.admMesa()).daSiguienteMesa(oTramite.IdFlujo, 0, 0);
            if (lstSiguientesMesas.Count > 0)
            {
                wfiplib.tramiteMesa siguienteMesa = new wfiplib.tramiteMesa();
                siguienteMesa.IdTramite = oTramite.Id;
                siguienteMesa.Estado = wfiplib.E_EstadoMesa.Registro;

                wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                foreach (wfiplib.mesa oMesa in lstSiguientesMesas)
                {
                    siguienteMesa.IdMesa = oMesa.Id;
                    resultado = oAdmTramiteMesa.registra(siguienteMesa);
                }
            }
            else
                enviaMsgCliente("Flujo incompleto.");
            return resultado;
        }

        protected void btnEliminaDocumento_Click(object sender, EventArgs e)
        {
            if (lstDocumentos.Items.Count > 0 && lstDocumentos.SelectedIndex > -1)
            {
                List<wfiplib.expediente> LstArchExpediente = new List<wfiplib.expediente>();
                List<wfiplib.expediente> LstArchExpedienteTmp = new List<wfiplib.expediente>();
                if (Session["documentos"] != null) { LstArchExpediente = (List<wfiplib.expediente>)Session["documentos"]; }
                int contador = 0;
                foreach (wfiplib.expediente oArchivo in LstArchExpediente)
                {
                    if (contador != lstDocumentos.SelectedIndex) { LstArchExpedienteTmp.Add(oArchivo); }
                    contador += 1;
                }
                lstDocumentos.DataSource = LstArchExpedienteTmp;
                lstDocumentos.DataValueField = "Id";
                lstDocumentos.DataTextField = "NmOriginal";
                lstDocumentos.DataBind();
                Session["documentos"] = LstArchExpedienteTmp;
            }
        }

        protected void btnEliminaInsumo_Click(object sender, EventArgs e)
        {
            if (lstInsumos.Items.Count > 0 && lstInsumos.SelectedIndex > -1)
            {
                List<wfiplib.insumos> LstArchInsumos = new List<wfiplib.insumos>();
                List<wfiplib.insumos> LstArchInsumosTmp = new List<wfiplib.insumos>();
                if (Session["insumos"] != null) { LstArchInsumos = (List<wfiplib.insumos>)Session["insumos"]; }
                int contador = 0;
                foreach (wfiplib.insumos oInsumo in LstArchInsumos)
                {
                    if (contador != lstInsumos.SelectedIndex) { LstArchInsumosTmp.Add(oInsumo); }
                    contador += 1;
                }
                lstInsumos.DataSource = LstArchInsumosTmp;
                lstInsumos.DataValueField = "Id";
                lstInsumos.DataTextField = "NmOriginal";
                lstInsumos.DataBind();
                Session["insumos"] = LstArchInsumosTmp;
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ArchivosAdicionesles();
        }

        protected void ArchivosAdicionesles()
        {
            if (CheckBox1.Checked.Equals(true))
            {
                fileUpInsumo.Visible = true;
                btnSubirInsumo.Visible = true;
                lstInsumos.Visible = true;
                btnEliminaInsumo.Visible = true;
                //chkArchPrivados.Visible = true;
                //chkArchPublicos.Visible = true;
            }
            else
            {
                fileUpInsumo.Visible = false;
                btnSubirInsumo.Visible = false;
                lstInsumos.Visible = false;
                btnEliminaInsumo.Visible = false;
                //chkArchPrivados.Visible = false;
                //chkArchPublicos.Visible = false;
            }
        }

        protected void MuestraDocumentos()
        {
            /* LISTA LOS ARCHIVOS DEL DOCUMENTO */
            List<wfiplib.expediente> LstArchivosDocumento = new List<wfiplib.expediente>();
            /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
            if (Session["documentos"] != null)
            {
                LstArchivosDocumento = (List<wfiplib.expediente>)Session["documentos"];
            }

            lstDocumentos.DataSource = LstArchivosDocumento;
            lstDocumentos.DataValueField = "Id";
            lstDocumentos.DataTextField = "NmOriginal";
            lstDocumentos.DataBind();
        }

        protected void MuestraInsumos()
        {
            List<wfiplib.insumos> LstArchivosInsumo = new List<wfiplib.insumos>();
            if (Session["insumos"] != null) { LstArchivosInsumo = (List<wfiplib.insumos>)Session["insumos"]; }

            lstInsumos.DataSource = LstArchivosInsumo;
            lstInsumos.DataValueField = "Id";
            lstInsumos.DataTextField = "NmOriginal";
            lstInsumos.DataBind();
        }
        protected void daNombreDeAgente(object sender, EventArgs e)
        {
            NombreAgente();
        }

        protected void NombreAgente()
        {
            wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
            //lbNombreAgente.Text = "NO EXISTE";
            Mensajes.Text = "";
            lbNombreAgente.Text = "";
            lbEmailAgente.Text = "";
            lbEmailAlternoAgente.Text = "";
            if (!string.IsNullOrEmpty(oTramite.IdPromotoria.ToString()) && !string.IsNullOrEmpty(txIdAgente.Text.ToString()))
            {
                wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(oTramite.IdPromotoria.ToString()));
                wfiplib.agentePromotoria agente = (new wfiplib.admAgentesPromotoria()).buscaAgenteEnPromotoria(promotoria.Clave, txIdAgente.Text.ToString());
                if (agente.clave > 0)
                {
                    lbNombreAgente.Text = agente.descripcion;
                    lbEmailAgente.Text = agente.Email;
                    lbEmailAlternoAgente.Text = agente.EmailAlterno;
                }
                else
                {
                    Mensajes.Text = "Agente No Encotrado";
                }
                
            }
            else
            {
                Mensajes.Text = "Coloca la clave del Agente";
            }
        }
    }
}