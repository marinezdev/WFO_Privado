using System;
using System.Collections.Generic;
using System.Data;

namespace wfiplib
{
    public class admCatCodigoPostal
    {
        public List<valorTexto> llenarCombo(string pCodigo)
        {
            List<valorTexto> resultado = new List<valorTexto>();
            resultado.Add(new valorTexto("0", "SELECCIONAR"));
            string SqlCmd = "SELECT asentamiento AS colonia, id_asenta_cpcons FROM cat_codigopostal WHERE codigo = '" + pCodigo + "' ORDER BY codigo, asentamiento, tipo_asentamiento";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { foreach (DataRow registro in datos.Rows) { resultado.Add(armaValorTexto(registro)); } }
            return resultado;
        }

        private valorTexto armaValorTexto(DataRow pRegistro)
        {
            valorTexto resultado = new valorTexto();
            if (!pRegistro.IsNull("id_asenta_cpcons")) { resultado.Valor = (string)pRegistro["id_asenta_cpcons"]; }
            if (!pRegistro.IsNull("colonia")) { resultado.Texto = ((string)pRegistro["colonia"]).ToUpper(); }
            return resultado;
        }

        public string daColonia(string pCodigo, string pIdAsentamiento)
        {
            string resultado = "";
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT CONCAT(asentamiento,', ',municipio,', ',estado) AS colonia FROM cat_codigopostal WHERE codigo = '" + pCodigo + "' AND id_asenta_cpcons='" + pIdAsentamiento + "'");
            if (datos.Rows.Count > 0) { resultado = Convert.ToString(datos.Rows[0][0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public CodigoPostal Carga(string pCodigo, string pIdAsentamiento)
        {
            CodigoPostal respuesta = new CodigoPostal();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT Top 1 * FROM cat_codigopostal WHERE codigo = '" + pCodigo + "' AND id_asenta_cpcons='" + pIdAsentamiento + "'");
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private CodigoPostal arma(DataRow pRegistro)
        {
            CodigoPostal respuesta = new CodigoPostal();
            if (!pRegistro.IsNull("codigo")) respuesta.Codigo = Convert.ToString(pRegistro["codigo"]);
            if (!pRegistro.IsNull("asentamiento")) respuesta.Asentamiento = Convert.ToString(pRegistro["asentamiento"]);
            if (!pRegistro.IsNull("tipo_asentamiento")) respuesta.TipoAsentamiento = Convert.ToString(pRegistro["tipo_asentamiento"]);
            if (!pRegistro.IsNull("municipio")) respuesta.Municipio = Convert.ToString(pRegistro["municipio"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("ciudad")) respuesta.Ciudad = Convert.ToString(pRegistro["ciudad"]);
            if (!pRegistro.IsNull("id_asenta_cpcons")) respuesta.id_asenta_cpcons = Convert.ToString(pRegistro["id_asenta_cpcons"]);
            
            return respuesta;
        }
    }

    public class valorTexto
    {
        public string Valor { get; set; }
        public string Texto { get; set; }
        public valorTexto() { }
        public valorTexto(string pValor, string pTexto) { Valor = pValor; Texto = pTexto; }
    }

    public class CodigoPostal
    {
        public string Codigo { get; set; }
        public string Asentamiento { get; set; }
        public string TipoAsentamiento { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string id_asenta_cpcons { get; set; }
    }

}
