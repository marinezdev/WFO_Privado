using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace wfiplib
{
    public class admTipoTramiteMov
    {
        /// <summary>
        /// Arma la estructura de los Movimientos Tipo de Trámite
        /// </summary>
        /// <param name="pRegistro">Información proveniente de la base de datos</param>
        /// <returns>Objeto con la información del Tipo de Servicios</returns>
        private TipoServicio arma(DataRow pRegistro)
        {
            TipoServicio Resultado = new TipoServicio();
            if (!pRegistro.IsNull("Id")) Resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("Nombre")) Resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Descripcion")) Resultado.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("Activo")) Resultado.Activo = Convert.ToBoolean(pRegistro["Activo"]);
            return Resultado;
        }
    }

    [Serializable]
    public class TipoTramiteMov
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Boolean Activo { get; set; }
    }
}
