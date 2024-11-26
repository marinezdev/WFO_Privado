using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfip.Propiedades
{
    public class UrlCifrardo
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public bool Result { get; set; }
        public string IdMesa { get; set; }
        public string Mesa { get; set; }
        public string Regreso { get; set; }
        public string Respuesta { get; set; }
        public string Mensaje { get; set; }
        public string IdTramite { get; set; }
        public string IdTipoTramite { get; set; }
        public string IdProceso { get; set; }
        public string Procesable { get; set; }
        public string Estado { get; set; }
        public string PDF { get; set; }
        public string Flujo { get; set; }
        public string IdUsuario { get; set; }


        public string CalDesde { get; set; }
        public string CalHasta { get; set; }
        public string Numero { get; set; }

        public string Action { get; set; }
        public string Folio { get; set; }
    }
}
