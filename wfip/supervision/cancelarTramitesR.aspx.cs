using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class cancelarTramitesR : System.Web.UI.Page
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

                Mensaje.Text = string.Empty;
            }
            else
            {
                this.Muestradatos();
                this.MuestraMotivos();
                Usuarios.Update();
            }
        }

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
            dt = (new wfiplib.admMesa()).TramitesEnOperacion(tramite, rfc, contratante, asegurado,1, manejo_sesion.Credencial.Id);
            dvgdTramites.DataSource = dt;
            dvgdTramites.DataBind();


        }
        private void MuestraMotivos()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.admMesa()).motivosCancelacion();
            dvgdMotivosCancelacion.DataSource = dt;
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
                bool resultado = (new wfiplib.admMesa()).CancelarTramites(idTramite, idCancelacion, manejo_sesion.Credencial);
                this.Muestradatos();
                this.MuestraMotivos();

                // Mensaje.Text = "Asignacion Existosa";
                if (resultado)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cancelación Existosa');", true);
                    Response.Redirect("supervision/esperaSupervisor.aspx");
                }
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message.ToString();
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Seleccione algún trámite y el motivo de cancelación');", true);
            }
        }

        protected void btnFiltroMesa_Click(object sender, EventArgs e)
        {

        }
    }
}