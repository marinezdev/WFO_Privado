using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class testEMailg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            wfiplib.CorreoGoogle correo = new wfiplib.CorreoGoogle();
            Mensajes mensajes = new Mensajes();
            mensajes.MostrarMensaje(this, correo.SendEmail(TextBox1.Text.ToString().Trim(), "Prueba WFO", "<br/><br/><br/>Este es una <b>prueba</b> enviado por <b>WFO Applications</b>"));
        }
    }
}