using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.Grupal
{
    public partial class GpoHistoricoTramite : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        private void EnviaMsgCliente(string pMensaje)
        {
            Lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GpoEspera.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Lt_jsMsg.Text = "";
            if (!IsPostBack)
            {
                if (Request.Params["id"] != null)
                {
                    Session.Remove("documentos");
                    PintaDatos(Request.Params["id"]);
                }
                else { Response.Redirect("GpoEspera.aspx"); }
            }
        }

        private void PintaDatos(string pId)
        {
            wfiplib.AdmBuzonTramite _AdmBuzonTramite = new wfiplib.AdmBuzonTramite();

            wfiplib.BuzonTramite tramite = _AdmBuzonTramite.Carga(pId);
            wfiplib.SolicitudGrupal solicitud = (new wfiplib.AdmSolicitudGrupal()).Carga(tramite.Tramite_Id);

            TxAsunto.Text = solicitud.Asunto;
            txNombre.Text = solicitud.Nombre;
            txRfc.Text = solicitud.Rfc;
            txCalle.Text = solicitud.Calle;
            txNumExt.Text = solicitud.NoExterior;
            txNumInt.Text = solicitud.NoInterior;
            txCP.Text = solicitud.CP;
            TxColonia.Text = solicitud.Colonia;
            txMpio.Text = solicitud.Municipio;
            txCiudad.Text = solicitud.Ciudad;
            txEstado.Text = solicitud.Entidad;

            RptDocAnexados.DataSource = (new wfiplib.admExpediente()).DaLista(tramite.Tramite_Id);
            RptDocAnexados.DataBind();

            RptBitacora.DataSource = _AdmBuzonTramite.Bitacora(solicitud.IdTramite);
            RptBitacora.DataBind();
        }

        protected void RptDocAnexados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Descargar"))
            {
                wfiplib.expediente archivo = (new wfiplib.admExpediente()).carga(Convert.ToInt32(e.CommandArgument));
                string archOrigen = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, archivo.Id) + archivo.NmArchivo;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + archivo.NmArchivo);
                Response.WriteFile(archOrigen);
                Response.End();
            }
        }
    }
}