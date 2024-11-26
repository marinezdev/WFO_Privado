using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip
{
    public partial class bu : System.Web.UI.Page
    {
        ProcesosLocales locales = new ProcesosLocales();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                locales.Reiniciar();
                Mensajes mensajes = new Mensajes();
                mensajes.MostrarMensaje(this, "Ya estuvo, se reinició tu contraseña.", "Default.aspx");
            }
        }

        internal class ProcesosLocales
        {
            //Reiniciar

            public void Reiniciar()
            {
                wfiplib.admCredencial cred = new wfiplib.admCredencial();
                cred.ReiniciarSupervisor();
            }
        }
    }
}