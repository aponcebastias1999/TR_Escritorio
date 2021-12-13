using Aplicacion.Metodos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Aplicacion.Biblioteca_de_clases
{
    public class EnvioReporte
    {
        static string de = "agenciaturismoreal@gmail.com";
        static string usuario = "agenciaturismoreal@gmail.com";
        static string contraseña = "Turismo2021";
        static string cola = "<h5 id='h4'>Acta check-in </h5></center><br><table>";

        public static bool EnviarMail( string para, string asunto, string contenido)
        {
            //string rutaAdjunto = "C:/Users/Administrador/Desktop/4.SW TR/TR Aplicacion/reportes/reporte.pdf"; 
            string rutaAdjunto = Path.GetFullPath(@"..\..\..\..\reportes\reporte.pdf");

            MailMessage _Correo = new MailMessage();

            if (string.IsNullOrEmpty(de))

                MessageBox.Show("Tronó");
            else
            {
                _Correo.From = new MailAddress(de);

                _Correo.To.Add(para);
                _Correo.Subject = asunto;
                _Correo.Body = contenido;
   
                _Correo.Body = CorreoHtml.GetString() + htmlCreator.GetHtml();
                _Correo.BodyEncoding = System.Text.Encoding.UTF8;
                _Correo.Priority = MailPriority.Normal;
                _Correo.IsBodyHtml = true;

            }

            
           Attachment _attachment = new Attachment(rutaAdjunto);
           _Correo.Attachments.Add(_attachment);
           //Adj = false;
            

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(usuario, contraseña);

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(_Correo);
                return true;
            }
            catch
            {
                return false;
            }
            _Correo.Dispose();
        }
        public static bool EnviarCHECKIN(string para, string asunto, string contenido)
        {


            MailMessage _Correo = new MailMessage();

            if (string.IsNullOrEmpty(de))

                MessageBox.Show("Tronó");
            else
            {
                _Correo.From = new MailAddress(de);

                _Correo.To.Add(para);
                _Correo.Subject = asunto;
                _Correo.Body = CorreoHtml.GetString() + cola + contenido  +"</table></center> ";
                _Correo.BodyEncoding = System.Text.Encoding.UTF8;
                _Correo.Priority = MailPriority.Normal;
                _Correo.IsBodyHtml = true;

            }


            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(usuario, contraseña);

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(_Correo);
                return true;
            }
            catch
            {
                return false;
            }
            _Correo.Dispose();
        }

    }
}
