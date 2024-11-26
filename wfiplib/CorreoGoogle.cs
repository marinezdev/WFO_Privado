using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace wfiplib
{
    public class CorreoGoogle
    {
        public string SendEmail(string To, string Subject, string Body)
        {
            string strResultado = "";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient smtp = new SmtpClient();
            try
            {
                mail.From = new MailAddress("rmf.marinez.2018@gmail.com");
                mail.To.Add(To);
                mail.IsBodyHtml = true;
                mail.Subject = Subject;
                mail.Body = Body;
                
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("rmf.marinez.2018@gmail.com", "**Kizraj123**");
                
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                strResultado = "No se ha podido enviar el email: " + ex.Message;
            }
            finally
            {
                smtp.Dispose();
            }

            return strResultado;
        }
    }
}
