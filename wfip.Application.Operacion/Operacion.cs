using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfip.Application.Operacion
{
    public class Operacion
    {
        public Flujo flujo;
        public Mesa mesa;
        public UrlCifrardo urlCifrardo;
        public Operacion()
        {
            flujo = new Flujo();
            mesa = new Mesa();
            urlCifrardo = new UrlCifrardo();
        }
    }
}
