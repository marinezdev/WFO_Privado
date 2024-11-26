using System;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class AdmSolicitudGrupal
    {
        public bool Nuevo(SolicitudGrupal pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into TRAM00005 (IdTramite,Asunto,Nombre,RFC,Calle,NoExterior,NoInterior,CP,Colonia,Municipio,Ciudad,Entidad,FechaRegistro,Estado) Values(");
            SqlCmd.Append(pDatos.IdTramite.ToString());
            SqlCmd.Append(",'" + pDatos.Asunto + "'");
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(",'" + pDatos.Rfc + "'");
            SqlCmd.Append(",'" + pDatos.Calle + "'");
            SqlCmd.Append(",'" + pDatos.NoExterior + "'");
            SqlCmd.Append(",'" + pDatos.NoInterior + "'");
            SqlCmd.Append(",'" + pDatos.CP + "'");
            SqlCmd.Append(",'" + pDatos.Colonia + "'");
            SqlCmd.Append(",'" + pDatos.Municipio + "'");
            SqlCmd.Append(",'" + pDatos.Ciudad + "'");
            SqlCmd.Append(",'" + pDatos.Entidad + "'");
            SqlCmd.Append(",getdate()");
            SqlCmd.Append(",1");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public SolicitudGrupal Carga(int pId)
        {
            SolicitudGrupal resultado = new SolicitudGrupal();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("Select * From TRAM00005 Where IdTramite=" + pId.ToString());
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private SolicitudGrupal arma(DataRow pRegistro)
        {
            SolicitudGrupal resultado = new SolicitudGrupal();
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("Asunto")) resultado.Asunto = Convert.ToString(pRegistro["Asunto"]);
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("RFC")) resultado.Rfc = Convert.ToString(pRegistro["RFC"]);
            if (!pRegistro.IsNull("Calle")) resultado.Calle = Convert.ToString(pRegistro["Calle"]);
            if (!pRegistro.IsNull("NoExterior")) resultado.NoExterior = Convert.ToString(pRegistro["NoExterior"]);
            if (!pRegistro.IsNull("NoInterior")) resultado.NoInterior = Convert.ToString(pRegistro["NoInterior"]);
            if (!pRegistro.IsNull("CP")) resultado.CP = Convert.ToString(pRegistro["CP"]);
            if (!pRegistro.IsNull("Colonia")) resultado.Colonia = Convert.ToString(pRegistro["Colonia"]);
            if (!pRegistro.IsNull("Municipio")) resultado.Municipio = Convert.ToString(pRegistro["Municipio"]);
            if (!pRegistro.IsNull("Ciudad")) resultado.Ciudad = Convert.ToString(pRegistro["Ciudad"]);
            if (!pRegistro.IsNull("Entidad")) resultado.Entidad = Convert.ToString(pRegistro["Entidad"]);
            if (!pRegistro.IsNull("FechaRegistro")) resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = (E_Estado)pRegistro["Estado"];
            return resultado;
        }
    }

    public class SolicitudGrupal
    {
        private int _IdTramite = 0;
        public int IdTramite { get => _IdTramite; set => _IdTramite = value; }
        public string Asunto { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Calle { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string CP { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get; set; }
        public string Entidad { get; set; }
        private DateTime _FechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return _FechaRegistro; } set { _FechaRegistro = value; } }
        public E_Estado Estado { get; set; }
    }
}
