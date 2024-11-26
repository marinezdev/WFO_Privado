using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace wfiplib
{
    public class admMesaApoyo
    {
        public bool Guardar(List<mesaApoyo> lista) {
            bool resultado = false;

            Eliminar(lista[0].IdFlujo);
            foreach (mesaApoyo odt in lista) {resultado=Registrar(odt);}
            
            return resultado;
        }

        
        public bool Registrar(mesaApoyo pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO mesaApoyo (");
            SqlCmd.Append("IdFlujo");
            SqlCmd.Append(",IdMesa");
            SqlCmd.Append(",IdMesaApoyo");
            
            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append(pDatos.IdFlujo );
            SqlCmd.Append("," + pDatos.IdMesaPrincipal);
            SqlCmd.Append("," + pDatos.IdMesaApoyo);
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        public List<mesaApoyo> Lista(int IdFlujo)
        {
            List<mesaApoyo> respuesta = new List<mesaApoyo>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM mesaApoyo where IdFlujo=" + IdFlujo);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<mesaApoyo> Lista(int IdFlujo, int IdMesaPrincipal)
        {
            List<mesaApoyo> respuesta = new List<mesaApoyo>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT mesaApoyo.IdFlujo,mesaApoyo.IdMesa,mesaApoyo.IdMesaApoyo,mesa.Nombre FROM mesaApoyo");
            SqlCmd.Append(" INNER JOIN mesa on mesaApoyo.IdMesaApoyo=mesa.IdMesa");
            SqlCmd.Append(" WHERE mesaApoyo.IdFlujo=" + IdFlujo.ToString());
            SqlCmd.Append(" AND mesaApoyo.IdMesa=" + IdMesaPrincipal.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public int daMesaSolApoyo(int IdFlujo, int pIdMesaApoyo)
        {
            int respuesta = 0;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT IdMesa FROM mesaApoyo WHERE IdFlujo=" + IdFlujo + " and IdMesaApoyo=" + pIdMesaApoyo.ToString());
            if (datos.Rows.Count > 0) { if (!datos.Rows[0].IsNull("IdMesa")) respuesta = Convert.ToInt32(datos.Rows[0]["IdMesa"]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private mesaApoyo arma(DataRow pRegistro)
        {
            mesaApoyo respuesta = new mesaApoyo();
            if (!pRegistro.IsNull("IdFlujo")) respuesta.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("IdMesa")) respuesta.IdMesaPrincipal = Convert.ToInt32(pRegistro["IdMesa"]);
            if (!pRegistro.IsNull("IdMesaApoyo")) respuesta.IdMesaApoyo = Convert.ToInt32(pRegistro["IdMesaApoyo"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            return respuesta;
        }

        public void Eliminar(int idFlujo)
        {
            StringBuilder SqlCmd = new StringBuilder("delete mesaApoyo ");
            SqlCmd.Append(" WHERE IdFlujo=" + idFlujo);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

    }

    public class mesaApoyo {
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private int mIdMesaPrincipal = 0;
        public int IdMesaPrincipal { get { return mIdMesaPrincipal; } set { mIdMesaPrincipal = value; } }
        private int mIdMesaApoyo = 0;
        public int IdMesaApoyo { get { return mIdMesaApoyo; } set { mIdMesaApoyo = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
    }
}
