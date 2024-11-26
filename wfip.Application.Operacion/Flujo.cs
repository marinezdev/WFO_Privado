using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfip.Application.Operacion
{
    public class Flujo
    {
        Data.Flujo _flujo = new Data.Flujo();

        public List<Propiedades.Flujo> Flujo_Selecionar_IdUsuario(int IdUsuario)
        {
            return _flujo.Flujo_Selecionar_IdUsuario(IdUsuario);
        }
    }
}
