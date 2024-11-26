using System;
using System.Collections.Generic;
using System.Data;

namespace wfiplib
{
    public class admConfiguracion
    {
        public List<configuracionVar> Lista(int pIdFlujo)
        {
            List<configuracionVar> respuesta = new List<configuracionVar>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM configuracion");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public configuracionVar asignacionAutomaticaTramite()
        {
            configuracionVar respuesta = new configuracionVar();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM configuracion where Nombre='asignacionAutomaticaTramite'");
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private configuracionVar arma(DataRow pRegistro)
        {
            configuracionVar respuesta = new configuracionVar();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Valor")) respuesta.Valor = Convert.ToString(pRegistro["Valor"]);
            return respuesta;
        }

        public void actualiza(configuracionVar pConfiguracionVar)
        {
            bd BD = new bd();
            BD.ejecutaCmd("Update configuracion Set Valor='" + pConfiguracionVar.Valor + "' Where Nombre='" + pConfiguracionVar.Nombre + "'");
            BD.cierraBD();
        }
    }

    public class configuracionVar
    {
        private int mId = 0;
        public int Id
        {
            get { return mId; }
            set { mId = value; }
        }
        private string mNombre = String.Empty;
        public string Nombre
        {
            get { return mNombre; }
            set { mNombre = value; }
        }
        private string mValor = String.Empty;
        public string Valor
        {
            get { return mValor; }
            set { mValor = value; }
        }
    }
}
