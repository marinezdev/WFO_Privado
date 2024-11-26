using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admArchivosDependenciasEstados
    {
        BaseDeDatos b = new BaseDeDatos();


        public string SeleccionarEstadoArchivo(string folio)
        {
            string consulta = "SELECT MAX(Estado) FROM archivosdependenciasestados WHERE Folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.SelectString();
        }


        /// <summary>
        /// Agrega un estado a un proceso por numero de folio
        /// </summary>
        /// <param name="folio">Folio al que pertenece el estado</param>
        /// <param name="estado">Estado númerico de la operación</param>
        /// <returns></returns>
        public int Agregar(string folio, int estado)
        {
            string consulta = "INSERT INTO archivosdependenciasestados VALUES(@folio, @estado, getdate())";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            return b.InsertUpdateDeleteWithTransaction();
        }


    }
}
