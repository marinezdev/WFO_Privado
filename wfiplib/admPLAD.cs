using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admPLAD
    {
        public bool nuevo(PLAD pPLAD)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO [DatosPlad]([IdTramite],[FechaIdentificacion],[NombreRepreentante],[FolioMercantil],[GiroMercantil],[VigenciaComprovante],[DuracionSociedad],[Activo])");
            SqlCmd.Append(" VALUES(");
            SqlCmd.Append(pPLAD.IdTramite);
            SqlCmd.Append(",'" + pPLAD.FechaIdentificacion + "'");
            SqlCmd.Append(",'" + pPLAD.NombreRepreentante + "'");
            SqlCmd.Append(",'" + pPLAD.FolioMercantil + "'");
            SqlCmd.Append(",'" + pPLAD.GiroMercantil + "'");
            SqlCmd.Append(",'" + pPLAD.VigenciaComprovante + "'");
            SqlCmd.Append(",'" + pPLAD.DuracionSociedad + "'");
            SqlCmd.Append(",1");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool Actualizar(PLAD pPLAD)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [DatosPlad] SET ");
            SqlCmd.Append("FechaIdentificacion ='" + pPLAD.FechaIdentificacion + "'");
            SqlCmd.Append(",NombreRepreentante ='" + pPLAD.NombreRepreentante + "'");
            SqlCmd.Append(",FolioMercantil = '" + pPLAD.FolioMercantil + "'");
            SqlCmd.Append(",GiroMercantil ='" + pPLAD.GiroMercantil + "'");
            SqlCmd.Append(",VigenciaComprovante = '" + pPLAD.VigenciaComprovante + "'");
            SqlCmd.Append(",DuracionSociedad = '" + pPLAD.DuracionSociedad + "'");
            SqlCmd.Append(",Activo = 1 ");
            SqlCmd.Append("WHERE [IdDatosPlad]= (SELECT TOP 1 IdDatosPlad FROM [DatosPlad] WHERE Activo = 1 ORDER BY IdDatosPlad DESC) AND IdTramite ='" + pPLAD.IdTramite + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool InactivoPlad(PLAD pPLAD)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [DatosPlad] SET Activo = 0 WHERE [IdDatosPlad]= (SELECT TOP 1 IdDatosPlad FROM [DatosPlad] WHERE Activo = 1 ORDER BY IdDatosPlad DESC) AND ");
            SqlCmd.Append("IdTramite ='" + pPLAD.IdTramite + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public PLAD carga(int IdTramite)
        {
            PLAD resultado = null;
            String SqlCmd = " SELECT * FROM DatosPlad WHERE IdTramite = " + IdTramite + " AND Activo = 1 ";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private PLAD arma(DataRow pRegistro)
        {
            PLAD resultado = new PLAD(); 
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("FechaIdentificacion")) resultado.FechaIdentificacion = Convert.ToString(pRegistro["FechaIdentificacion"]);
            if (!pRegistro.IsNull("NombreRepreentante")) resultado.NombreRepreentante = Convert.ToString(pRegistro["NombreRepreentante"]);
            if (!pRegistro.IsNull("FolioMercantil")) resultado.FolioMercantil = Convert.ToString(pRegistro["FolioMercantil"]);
            if (!pRegistro.IsNull("GiroMercantil")) resultado.GiroMercantil = Convert.ToString(pRegistro["GiroMercantil"]);
            if (!pRegistro.IsNull("VigenciaComprovante")) resultado.VigenciaComprovante = Convert.ToString(pRegistro["VigenciaComprovante"]);
            if (!pRegistro.IsNull("DuracionSociedad")) resultado.DuracionSociedad = Convert.ToString(pRegistro["DuracionSociedad"]);
            if (!pRegistro.IsNull("Activo")) resultado.Activo = Convert.ToString(pRegistro["Activo"]);
            return resultado;
        }

    }
}