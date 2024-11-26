using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class testEMail : System.Web.UI.Page
    {
        //ServiceReference1.ServicioCorreoClient enviarmail = new ServiceReference1.ServicioCorreoClient();
        //EnviaCorreos.ServicioCorreoClient EnviarMail = new EnviaCorreos.ServicioCorreoClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Text = "mail.asae.com.mx";
                TextBox2.Text = "25";
                TextBox3.Text = "certifiel@asae.com.mx";
                TextBox4.Text = "C3$ti2018_";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //wfiplib.Correo correo = new wfiplib.Correo();
            //Mensajes mensajes = new Mensajes();
            //mensajes.MostrarMensaje(this, correo.ProcesarCorreo("ruben.marines@asae.com.mx"));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //wfiplib.Correo correo = new wfiplib.Correo();
            Mensajes mensajes = new Mensajes();
            // string strResultado = "";
            //mensajes.MostrarMensaje(this, correo.TestCorreo(TextBox1.Text, Convert.ToInt16(TextBox2.Text), TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text));


            //enviarmail.ProcesaCorreo("ruben.marines@asae.com.mx");
            //EnviarMail.ProcesaCorreo("ruben.marines@asae.com.mx");

            try
            {
                //EnviarMail.ServicioCorreoClient EnviarMail = new EnviarMail.ServicioCorreoClient();
                //PruebaMail.ServicioCorreoClient EnviarMail = new PruebaMail.ServicioCorreoClient();


                //strResultado = EnviarMail.pruebaCorreo();
                ////strResultado = EnviarMail.TestCorreo(TextBox1.Text, Convert.ToInt16(TextBox2.Text), TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text);
                //// mensajes.MostrarMensaje(this, "Correo electrónico enviado correctamente.");
                //mensajes.MostrarMensaje(this, strResultado);

                //EnviarMail = null;
            }
            catch (Exception ex)
            {
                //mensajes.MostrarMensaje(this, "No se pudo enviar el correo elecntrónico.");
                mensajes.MostrarMensaje(this, "No se pudo enviar el correo elecntrónico. <<< " + ex.Message + " >>>");
            }
        }
    }
}