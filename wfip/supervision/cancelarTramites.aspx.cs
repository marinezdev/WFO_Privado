using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.IO;
using Microsoft.Reporting.WebForms;
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

namespace wfip.supervision
{
    public partial class cancelarTramites : System.Web.UI.Page
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
            DataTable dtMotivosCancelacion = new DataTable();
            dtMotivosCancelacion = (new wfiplib.Reportes()).ListaMotivosRechazo(-1, "Cancelación Promotoría");
            treeListCancelar.DataSource = dtMotivosCancelacion;
            treeListCancelar.DataBind();
            treeListCancelar.ExpandToLevel(1);

            if (!IsPostBack)
            {
                // btnCancelar.OnClientClick = "CancelaTramite();" + " return false;";
                // REALIZA NINGUNA CARGA DE INFORMACION.
            }
            
        }

        protected void ConsultaFechasBD(object sender, EventArgs e)
        {
            MSresultado2.Text = "";
            DateTime Fecha1 = Convert.ToDateTime(CalDesde.Text.ToString());
            DateTime Fecha2 = Convert.ToDateTime(CalHasta.Text.ToString());

            DateTime hora1 = Convert.ToDateTime("00:00:00");
            DateTime hora2 = Convert.ToDateTime("23:59:59");

            DateTime F1 = Fecha1.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);
            DateTime F2 = Fecha2.AddHours(hora2.Hour).AddMinutes(hora2.Minute).AddSeconds(hora2.Second);

            if (Fecha1 > Fecha2)
            {
                MSresultado2.Text = "La fecha inicial no puede ser mayor a la fecha final ";
            }
            else
            {
                DataTable Datos = null;
                Datos = (new wfiplib.admTramite()).daTablaTramitesParaCancelacionFechas(F1, F2, manejo_sesion.Credencial.Id);

                rptTramitesEspera.DataSource = Datos;
                rptTramitesEspera.DataBind();
            }
        }

        protected void ConsultaFiltros(object sender, EventArgs e)
        {
            MSresultado2.Text = "";
            string Folio = TextFolio.Text.ToString().Trim();
            string RFC = TextRFC.Text.ToString().Trim();
            string Nombre = txNombre.Text.ToString().Trim();
            string ApPaterno = txApPat.Text.ToString().Trim();
            string ApMaterno = txApMat.Text.ToString().Trim();

            DataTable Datos = null;
            Datos = (new wfiplib.admTramite()).daTablaTramitesParaCancelacionFiltro(Folio, RFC, Nombre, ApPaterno, ApMaterno, manejo_sesion.Credencial.Id);

            rptTramitesEspera.DataSource = Datos;
            rptTramitesEspera.DataBind();
        }

        protected void LimpiaFormulario(object sender, EventArgs e)
        {
            MSresultado2.Text = "";
            TextFolio.Text = "";
            TextRFC.Text = "";
            txNombre.Text = "";
            txApPat.Text = "";
            txApMat.Text = "";
            CalDesde.Text = "";
            CalHasta.Text = "";

            DataTable Datos = (new wfiplib.admTramite()).daTablaTramitesParaCancelacionFiltro("NULL", "NULL", "NULL", "NULL", "NULL", manejo_sesion.Credencial.Id);

            rptTramitesEspera.DataSource = Datos;
            rptTramitesEspera.DataBind();
        }

        protected void CargaExpedienteID(object sender, CommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                hdIdTramite.Value = "";
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                string folio = (new wfiplib.admTramite()).getFolio(id);
                string poliza = (new wfiplib.admTramite()).getPolizaTramite(id);

                if (poliza.Length > 0)
                {
                    showMessage("No se puede realizar la cancelación del trámite debido a que ya se encuentra con un número de póliza.");
                }
                else
                {
                    pnlPopMotivosCancelar.ShowOnPageLoad = true;
                    pnlPopMotivosCancelar.HeaderText = "Motivos de Cancelación - " + folio;
                    hdIdTramite.Value = e.CommandArgument.ToString();
                }
            }
        }
        
        protected void pnlCallbackMotCancelar_Callback(object sender, EventArgs e)
        {
            //Mensajes message = new Mensajes();
            //message.MostrarMensaje(this, "prueba");
        }

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
        
        protected void btnCtrlAplicaCancelacion_Click(object sender, EventArgs e)
        {
            try
            {
                Mensajes mensajes = new Mensajes();
                int intMotivo = -1;
                var nodos = treeListCancelar.GetSelectedNodes();
#pragma warning disable CS0219 // La variable 'rogerPrueba' está asignada pero su valor nunca se usa
                string rogerPrueba = "";
#pragma warning restore CS0219 // La variable 'rogerPrueba' está asignada pero su valor nunca se usa
                if (nodos.Count > 0)
                {
                    wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(hdIdTramite.Value.ToString()));
                    if ((new wfiplib.admTramite()).CancelarTramiteServisor(oTramite, manejo_sesion.Credencial, E_EstadoTramite.Cancelado))
                    {
                        wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                        wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();

                        oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                        oTramiteRechazo.IdTramiteMesa = -1;
                        oTramiteRechazo.IdUsuario = manejo_sesion.Credencial.Id;
                        oTramiteRechazo.ObservacionPublica = "Cancelación desde Promotoría";
                        oTramiteRechazo.ObservacionPrivada = "Cancelación desde Promotoría";
                        oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.Cancelado;
                        oAdmTramiteRechazo.nuevo(oTramiteRechazo);

                        foreach (TreeListNode node in nodos)
                        {
                            intMotivo = Convert.ToInt32(node.GetValue("id"));
                            oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, intMotivo);
                            // oAdmTramiteMesa.registraIdRechazo(oTramiteMesa.Id, oTramiteRechazo.Id);
                        }
                        mensajes.MostrarMensaje(this, "El Trámite " + oTramite.FolioCompuesto + " se Canceló Correctamente.");
                        Response.Redirect("cancelarTramites.aspx", true);
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

        /*
        private void Muestradatos()
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            string tramite = "' '";
            string rfc = "' '";
            string contratante = "' '";
            string asegurado = "' '";
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(txtTramite.Text)) tramite = "'%" + txtTramite.Text.Trim() + "%'";
            if (!string.IsNullOrEmpty(txtRFC.Text)) rfc = "'%" + txtRFC.Text.Trim() + "%'";
            if (!string.IsNullOrEmpty(txtContratante.Text)) contratante = "'%" + txtContratante.Text.Trim() + "%'";
            if (!string.IsNullOrEmpty(txtAsegurado.Text)) asegurado = "'%" + txtAsegurado.Text.Trim() + "%'";
            dt = (new wfiplib.admMesa()).TramitesEnOperacion(tramite, rfc, contratante, asegurado,3);
            dvgdTramites.DataSource = dt;
            dvgdTramites.DataBind();
           

        }
        private void MuestraMotivos()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.admMesa()).motivosCancelacion();
            dvgdMotivosCancelacion.DataSource= dt;
            dvgdMotivosCancelacion.DataBind();

          

        }
        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                var tramites = dvgdTramites.GetFilteredSelectedValues("IdTramite");
                var cancelaciones = dvgdMotivosCancelacion.GetFilteredSelectedValues("idMotivoCancelacion");
                string idTramite = tramites[0].ToString();
                string idCancelacion = cancelaciones[0].ToString();
                string motivoCancelacion = dvgdMotivosCancelacion.GetFilteredSelectedValues("MotivoCancelacion")[0].ToString(); 
                bool resultado = (new wfiplib.admMesa()).CancelarTramites(idTramite, idCancelacion, manejo_sesion.Credencial, motivoCancelacion);
                this.Muestradatos();
                this.MuestraMotivos();

                // Mensaje.Text = "Asignacion Existosa";
                if (resultado)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cancelación Exitosa');", true);
                    Response.Redirect("esperaSupervisorP.aspx?action=1");
                }
            }
            catch  (Exception ex)
            {
                string Mensaje = ex.Message.ToString();
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Seleccione algún trámite y el motivo de cancelación');", true);
            }
        }

        protected void btnFiltroMesa_Click(object sender, EventArgs e)
        {

        }
        */

        private void showMessage(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.mensajesInformativos, typeof(string), "Alert", "alert('" + Mensaje + "');", true);
            // Response.Write("<script language=javascript>alert('" + Mensaje + "')</script>");
        }
    }
}