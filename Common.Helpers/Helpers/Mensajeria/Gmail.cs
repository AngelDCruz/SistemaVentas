using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Common.Mensajeria
{
    public class Gmail : EnvioCorreoAbstract
    {


        private string _de { get; }
        private string _para { get; set; }
        private string _password { get; set; }
        private string _asunto { get; set; }
        private string _cuerpo { get; set; }

        public Gmail(string para, string asunto, string cuerpo, string de = "AngelDevMaster94@gmail.com", string password = "reycabra45")
        {
            _de = de;
            _para = para;
            _password = password;
            _asunto = asunto;
            _cuerpo = cuerpo;
        }

        public override void Enviar()
        {
            var gmail = new CorreoEntidad();
            gmail.Password = _password;
            gmail.De = _de;
            gmail.Para = _para;
            gmail.Puerto = 587;
            gmail.Ssl = true;
            gmail.Servidor = "smtp.gmail.com";

            this.PreparandoEnvioCorreo(gmail, _asunto, _cuerpo);

        }
      
    }
}
