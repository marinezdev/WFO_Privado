using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admMesaCondicion
    {
        public bool Guardar(List<mesaCondicion> lista)
        {
            bool resultado = false;
            Eliminar(lista[0].IdFlujo);
            foreach (mesaCondicion odt in lista){resultado = Registrar(odt);}
            return resultado;
        }

        private bool Registrar(mesaCondicion pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO mesaCondicion (");
            SqlCmd.Append("IdFlujo");
            SqlCmd.Append(",IdTipoTramite");
            SqlCmd.Append(",IdMesa");
            SqlCmd.Append(",IdCondicion");
            SqlCmd.Append(",Condicion");
            SqlCmd.Append(",Nivel");
            SqlCmd.Append(")");
            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("," + pDatos.IdFlujo.ToString());
            SqlCmd.Append("," + pDatos.IdTipoTramite.ToString());
            SqlCmd.Append("," + pDatos.IdMesa.ToString());
            SqlCmd.Append("," + pDatos.IdCondicion.ToString());
            SqlCmd.Append(",'" + pDatos.Condicion + "'");
            SqlCmd.Append("," + pDatos.NivelDestino.ToString());
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        public List<mesaCondicion> Lista(int pIdFlujo, int pIdMesa)
        {
            List<mesaCondicion> respuesta = new List<mesaCondicion>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM mesaCondicion where IdFlujo=" + pIdFlujo.ToString() + " and IdMesa=" + pIdMesa.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private mesaCondicion arma(DataRow pRegistro)
        {
            mesaCondicion respuesta = new mesaCondicion();
            if (!pRegistro.IsNull("IdFlujo")) respuesta.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("IdTipoTramite")) respuesta.IdTipoTramite = Convert.ToInt32(pRegistro["IdTipoTramite"]);
            if (!pRegistro.IsNull("IdMesa")) respuesta.IdMesa = Convert.ToInt32(pRegistro["IdMesa"]);
            if (!pRegistro.IsNull("IdCondicion")) respuesta.IdCondicion = Convert.ToInt32(pRegistro["IdCondicion"]);
            if (!pRegistro.IsNull("Condicion")) respuesta.Condicion = Convert.ToString(pRegistro["Condicion"]);
            if (!pRegistro.IsNull("NivelDestino")) respuesta.NivelDestino = Convert.ToInt32(pRegistro["NivelDestino"]);
            return respuesta;
        }

        private void Eliminar(int idFlujo)
        {
            string SqlCmd = "DELETE mesaCondicion WHERE IdFlujo=" + idFlujo.ToString();
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd);
            BD.cierraBD();
        }
    }

    public class mesaCondicion
    {
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private int mIdTipoTramite = 0;
        public int IdTipoTramite { get { return mIdTipoTramite; } set { mIdTipoTramite = value; } }
        private int mIdMesa = 0;
        public int IdMesa { get { return mIdMesa; } set { mIdMesa = value; } }
        private int mIdCondicion = 0;
        public int IdCondicion { get { return mIdCondicion; } set { mIdCondicion = value; } }
        private string mCondicion = string.Empty;
        public string Condicion { get { return mCondicion; } set { mCondicion = value; } }
        private int mNivelDestino = 0;
        public int NivelDestino { get { return mNivelDestino; } set { mNivelDestino = value; } }
     }
}
