using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatMoneda
    {
        public bool nuevo(Moneda pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_monedas (Nombre, Valor) VALUES (");
      
            SqlCmd.Append("'" + pDatos.Nombre + "'");
            SqlCmd.Append(",'" + pDatos.Valor + "'");
            //SqlCmd.Append("'" + pDatos.activo + "'");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        public Moneda carga(int pIdMoneda)
        {
            Moneda respuesta = new Moneda();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_monedas WHERE IdMoneda=" + pIdMoneda.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<Moneda> ListaMoneda()
        {
            List<Moneda> respuesta = new List<Moneda>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_monedas order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private Moneda arma(DataRow pRegistro)
        {
            Moneda respuesta = new Moneda();
            if (!pRegistro.IsNull("IdMoneda")) respuesta.IdMoneda = Convert.ToInt32(pRegistro["IdMoneda"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Valor")) respuesta.Valor = Convert.ToString(pRegistro["Valor"]);
            if (!pRegistro.IsNull("activo")) respuesta.activo = Convert.ToInt32(pRegistro["activo"]);
            return respuesta;
        }

        public bool Existe(string pValor)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_monedas Where Nombre= '" + pValor + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica(Moneda oDt)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_monedas SET");
            SqlCmd.Append(" Nombre='" + oDt.Nombre + "'");
            SqlCmd.Append(",Valor='" + oDt.Valor + "'");
            SqlCmd.Append(" WHERE IdMoneda=" + oDt.IdMoneda);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    public class Moneda
    {
        private int mIdMoneda = 0;
        public int IdMoneda { get { return mIdMoneda; } set { mIdMoneda = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mValor = String.Empty;
        public string Valor { get { return mValor; } set { mValor = value; } }
        private int mactivo = 1;
        public int activo { get { return mactivo; } set { mactivo = value; } }


    }
}



