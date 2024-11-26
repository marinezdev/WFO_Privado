using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.administracion
{
    public partial class admCatMensajes : System.Web.UI.Page
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
                ListaMensajes();
            }

        }

        private void ListaMensajes()
        {
            List<wfiplib.Mensajes> lista = (new wfiplib.admCatMensajes()).ListaMensajes();
            rptMensajes.DataSource = lista;
            rptMensajes.DataBind();
        }

        protected void rptMensajes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdMensajes.Value = e.CommandArgument.ToString();
                wfiplib.admCatMensajes adm = new wfiplib.admCatMensajes();
                wfiplib.Mensajes oMnsje = adm.carga(Convert.ToInt32(hdIdMensajes.Value));
                txMensaje.Text = oMnsje.Mensaje;
                txDescripcion.Text = oMnsje.Descripcion;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            wfiplib.admCatMensajes adm = new wfiplib.admCatMensajes();
            wfiplib.Mensajes oMnsje = RecuperaDatos();
            oMnsje.Id_Control = Convert.ToInt32(hdIdMensajes.Value);
            adm.modifica(oMnsje);
            this.Limpiar();
            this.ListaMensajes();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            wfiplib.admCatMensajes adm = new wfiplib.admCatMensajes();
            wfiplib.Mensajes oMnsje = RecuperaDatos();

            bool Resultado = adm.nuevo(oMnsje);
            this.Limpiar();
            this.ListaMensajes();

        }

        private void Limpiar()
        {
            txMensaje.Text = String.Empty;
            txDescripcion.Text = String.Empty;

        }

        private wfiplib.Mensajes RecuperaDatos()
        {
            wfiplib.Mensajes oMnsje = new wfiplib.Mensajes();
            oMnsje.Mensaje = txMensaje.Text.ToLower();
            oMnsje.Descripcion = txDescripcion.Text.ToLower();
            return oMnsje;
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admsysEspera.aspx"); }

    }
}

    
