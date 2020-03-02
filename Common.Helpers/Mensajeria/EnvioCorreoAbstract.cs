using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Common.Mensajeria
{
    public  abstract class EnvioCorreoAbstract
    {

        public abstract void Enviar();

        protected void PreparandoEnvioCorreo(CorreoEntidad correo, string asunto, string cuerpo)
        {

            try
            {

                var correoEntidad = correo;

                MailMessage mail = new MailMessage();

                SmtpClient smtp = new SmtpClient(correoEntidad.Servidor);
                smtp.Port = correoEntidad.Puerto;
                smtp.EnableSsl = correoEntidad.Ssl ?? false;
                smtp.Credentials = new NetworkCredential(correoEntidad.De, correoEntidad.Password);

                mail.From = new MailAddress(correoEntidad.De);
                mail.To.Add(correoEntidad.Para);
                mail.Subject = asunto;

                ContentType contentType = new ContentType("text/html");

                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(cuerpo, contentType);

                mail.AlternateViews.Add(alternateView);


                smtp.Send(mail);

                smtp.Dispose();


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

            }

        }


    }
}
