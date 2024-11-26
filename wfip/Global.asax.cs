using System;
using System.IO;
using System.Text;

namespace wfip
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            StringBuilder mensajeErr = new StringBuilder(DateTime.Now.ToString());
            mensajeErr.AppendLine("Usuario : " + Context.User.Identity.Name.ToString());
            mensajeErr.AppendLine("Página : " + Request.Url.AbsoluteUri);
            mensajeErr.AppendLine("Message : " + Server.GetLastError().InnerException.ToString());
            mensajeErr.AppendLine("InnerException : " + Server.GetLastError().Message.ToString());
            mensajeErr.AppendLine("Source : " + Server.GetLastError().Source.ToString());
            mensajeErr.AppendLine("StackTrace : " + Server.GetLastError().StackTrace.ToString());
            mensajeErr.AppendLine("TargetSite : " + Server.GetLastError().TargetSite.ToString());
            mensajeErr.AppendLine("-------------------------------------------------------------------------------------");
            Server.ClearError();

            string nombreArchivoErr = Properties.Settings.Default.errLog + DateTime.Now.ToString("yyyyMMdd") + ".asae";
            using (StreamWriter archivoErr = new StreamWriter(nombreArchivoErr, true))
            {
                archivoErr.WriteLine(mensajeErr.ToString());
            }

            Response.Redirect("https://www.cloud-asae.com.mx/MetLife/Default.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                //Cerrar la sesión del usuario cuando termine sin cerrar el navegador
                wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
                manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
                if (manejo_sesion != null)
                    (new wfiplib.admCredencial()).desconecta(manejo_sesion.Credencial.Id, Session.SessionID);
            }
            catch(Exception)
            {
                // Ignoramos errores. En caso de que se no haya iniciado sesión...
            }

            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Server.ClearError();
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}