﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admAutentificar
    {
        public CredencialesWS Autentificar(int IdUsuario, int IdAplicacion, string token)
        {
            CredencialesWS credencialesWS = new CredencialesWS();
            WSautentificar.AutentificarSoapClient autentificar = new WSautentificar.AutentificarSoapClient();
            
            var sal = autentificar.AccesoAP(IdUsuario, IdAplicacion, token);
            
            foreach (var datos in sal.ConsultaAcceso)
            {
                credencialesWS.Id = datos.usuario;
                credencialesWS.Token = datos.token;
            }

            return credencialesWS;
        }

        public CredencialesWS CierreSesion(string token)
        {
            CredencialesWS credencialesWS = new CredencialesWS();
            WSautentificar.AutentificarSoapClient autentificar = new WSautentificar.AutentificarSoapClient();

            var sal = autentificar.CerrarSesionAP(token);

            foreach (var datos in sal.ConsultaAcceso)
            {
                credencialesWS.Token = datos.token;
            }

            return credencialesWS;

        }
    }

    public class CredencialesWS
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}