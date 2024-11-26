using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfip.Application.Operacion
{
    public class Mesa
    {
        Data.Mesa _mesa = new Data.Mesa();
        public List<Propiedades.Mesa> UsuariosMesa_Seleccionar_IdFlujo_IdUsuario(int IdFlujo, int IdUsuario)
        {
            return _mesa.UsuariosMesa_Seleccionar_IdFlujo_IdUsuario(IdFlujo, IdUsuario);
        }
    }
}
