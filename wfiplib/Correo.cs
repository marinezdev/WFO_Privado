using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;

namespace wfiplib
{
    public class Correo
    {
        //Datos generales a aplicar
        string host = "mail.asae.com.mx";
        int puerto = 25;
        string usuario = "certifiel@asae.com.mx";
        string contra = "C3$ti2018_";
        //string contra = "C4#$tf28_";

        ///// <summary>
        ///// Correo para pruebas
        ///// </summary>
        ///// <returns>Cadena de texto con el resultado del envío</returns>
        //public string TestCorreo(string pHost, int pPuerto, string pUsuario, string pPassword, string pDestino, string pAsusnto, string pContenido)
        //{
        //    MailMessage correo = new MailMessage();
        //    foreach (var direccion in pDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        correo.To.Add(direccion);
        //    }
        //    //correo.To.Add(new MailAddress(pDestino));
        //    correo.From = new MailAddress(pUsuario);
        //    correo.Subject = pAsusnto;
        //    correo.Body = pContenido;
        //    correo.IsBodyHtml = true;
        //    correo.Priority = MailPriority.Normal;

        //    SmtpClient smtp = new SmtpClient(pHost, pPuerto);
        //    smtp.EnableSsl = false;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new NetworkCredential(pUsuario, pPassword);

        //    ServicePointManager.ServerCertificateValidationCallback =
        //       delegate (object s
        //           , System.Security.Cryptography.X509Certificates.X509Certificate certificate
        //           , System.Security.Cryptography.X509Certificates.X509Chain chai
        //           , SslPolicyErrors sslPolicyErrors)
        //       {
        //           return true;
        //       };

        //    try
        //    {
        //        smtp.Send(correo);
        //        correo.Dispose();
        //        return "Envío éxitoso.";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}


        ///// <summary>
        ///// Correo para pruebas
        ///// </summary>
        ///// <returns>Cadena de texto</returns>
        //public string ProcesarCorreo(string pCorreo)
        //{
        //    MailMessage correo = new MailMessage();
        //    correo.To.Add(new MailAddress(pCorreo));
        //    correo.From = new MailAddress(usuario, "Met Woork Tool");
        //    correo.Subject = "Mensaje de Prueba del administrador para verificar envío de correo";
        //    correo.Body = "Cualquier contenido en <b>HTML</b> para enviarlo por correo electrónico.<br />";
        //    correo.IsBodyHtml = true;
        //    correo.Priority = MailPriority.Normal;

        //    SmtpClient smtp = new SmtpClient(host, puerto);
        //    smtp.EnableSsl = false;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new NetworkCredential(usuario, contra);

        //    ServicePointManager.ServerCertificateValidationCallback =
        //       delegate (object s
        //           , System.Security.Cryptography.X509Certificates.X509Certificate certificate
        //           , System.Security.Cryptography.X509Certificates.X509Chain chai
        //           , SslPolicyErrors sslPolicyErrors)
        //       {
        //           return true;
        //       };

        //    try
        //    {
        //        smtp.Send(correo);
        //        correo.Dispose();
        //        return "Envío éxitoso.";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        ///// <summary>
        ///// Procesar correo
        ///// </summary>
        ///// <param name="para">A quién se envía el correo</param>
        ///// <param name="de">Quién lo envía</param>
        ///// <param name="titulo">Título del mensaje</param>
        ///// <param name="mensaje">Cuerpo del mensaje</param>
        ///// <returns>Verdadero si se envío, falso si no</returns>
        public string ProcesarCorreo(string para, string de, string titulo, string mensaje)
        {
            string strRespuesta = "No se pudó enviar el correo.";
            //if (System.Web.Configuration.WebConfigurationManager.AppSettings["EnviarMail"].ToString() != "0")
            //{
            //    //ServiceReference1.ServicioCorreoClient enviarmail = new ServiceReference1.ServicioCorreoClient();
            //    EnviaCorreos.ServicioCorreoClient EnviarMail = new EnviaCorreos.ServicioCorreoClient();

            //    try
            //    {
            //        EnviarMail.TestCorreo(host, puerto, usuario, contra, para, titulo, mensaje);
            //        strRespuesta = "Envío éxitoso.";
            //    }
            //    catch (Exception ex)
            //    {
            //        strRespuesta = "Error: " + ex.Message;
            //    }

            //    //MailMessage correo = new MailMessage();
            //    //correo.To.Add(new MailAddress(para));
            //    //correo.From = new MailAddress(de);
            //    //correo.Subject = titulo;
            //    //correo.Body = mensaje + "<br /><br /><br />";
            //    //correo.IsBodyHtml = true;
            //    //correo.Priority = MailPriority.Normal;

            //    //SmtpClient smtp = new SmtpClient(host, puerto);
            //    //smtp.EnableSsl = false;
            //    //smtp.UseDefaultCredentials = false;
            //    //smtp.Credentials = new NetworkCredential(usuario, contra);

            //    //ServicePointManager.ServerCertificateValidationCallback =
            //    //   delegate (object s
            //    //       , System.Security.Cryptography.X509Certificates.X509Certificate certificate
            //    //       , System.Security.Cryptography.X509Certificates.X509Chain chai
            //    //       , SslPolicyErrors sslPolicyErrors)
            //    //   {
            //    //       return true;
            //    //   };

            //    //try
            //    //{
            //    //    smtp.Send(correo);
            //    //    correo.Dispose();
            //    //    strRespuesta = "Envío éxitoso.";
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    strRespuesta = "Error: " + ex.Message;
            //    //}
            //}
            //else
            //{
            //    strRespuesta = "";
            //}

            return strRespuesta;
        }
    }
}