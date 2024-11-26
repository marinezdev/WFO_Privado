using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admTramiteDetatlle
    {
        /// <summary>
        /// Obtiene la Información del Detalle del Trámite
        /// </summary>
        /// <param name="pRegistro">Información del Detalle</param>
        /// <returns>La Información Tipeada del Detalle del Trámite</returns>
        private tramiteDetalle arma(DataRow pRegistro)
        {
            tramiteDetalle resultado = new tramiteDetalle();
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("CPDES")) resultado.CPDES = Convert.ToBoolean(pRegistro["CPDES"]);
            if (!pRegistro.IsNull("FolioCPDES")) resultado.FolioCPDES = Convert.ToString(pRegistro["FolioCPDES"]);
            if (!pRegistro.IsNull("EstatusCPDES")) resultado.EstatusCPDES = Convert.ToString(pRegistro["EstatusCPDES"]);
            if (!pRegistro.IsNull("TipoPersona")) resultado.TipoPersona = Convert.ToInt32(pRegistro["TipoPersona"]);
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("ApPaterno")) resultado.ApPaterno = Convert.ToString(pRegistro["ApPaterno"]);
            if (!pRegistro.IsNull("ApMaterno")) resultado.ApMaterno = Convert.ToString(pRegistro["ApMaterno"]);
            if (!pRegistro.IsNull("Sexo")) resultado.Sexo = Convert.ToString(pRegistro["Sexo"]);
            if (!pRegistro.IsNull("FechaNacimiento")) resultado.FechaNacimiento = Convert.ToString(pRegistro["FechaNacimiento"]);
            if (!pRegistro.IsNull("RFC")) resultado.RFC = Convert.ToString(pRegistro["RFC"]);
            if (!pRegistro.IsNull("FechaConst")) resultado.FechaConst = Convert.ToString(pRegistro["FechaConst"]);
            if (!pRegistro.IsNull("Nacionalidad")) resultado.Nacionalidad = Convert.ToString(pRegistro["Nacionalidad"]);
            if (!pRegistro.IsNull("TitularNombre")) resultado.TitularNombre = Convert.ToString(pRegistro["TitularNombre"]);
            if (!pRegistro.IsNull("TitularApPat")) resultado.TitularApPat = Convert.ToString(pRegistro["TitularApPat"]);
            if (!pRegistro.IsNull("TitularApMat")) resultado.TitularApMat = Convert.ToString(pRegistro["TitularApMat"]);
            if (!pRegistro.IsNull("TitularNacionalidad")) resultado.TitularNacionalidad = Convert.ToString(pRegistro["TitularNacionalidad"]);
            if (!pRegistro.IsNull("TitularSexo")) resultado.TitularSexo = Convert.ToString(pRegistro["TitularSexo"]);
            if (!pRegistro.IsNull("TitularFechaNacimiento")) resultado.TitularFechaNacimiento = Convert.ToString(pRegistro["TitularFechaNacimiento"]);
            if (!pRegistro.IsNull("SumaAsegurada")) resultado.SumaAsegurada = Convert.ToString(pRegistro["SumaAsegurada"]);
            if (!pRegistro.IsNull("SumaPolizas")) resultado.SumaPolizas = Convert.ToString(pRegistro["SumaPolizas"]);
            if (!pRegistro.IsNull("IdMoneda")) resultado.IdMoneda = Convert.ToString(pRegistro["IdMoneda"]);
            if (!pRegistro.IsNull("Detalle")) resultado.Detalle = Convert.ToString(pRegistro["Detalle"]);
            return resultado;
        }

        /// <summary>
        /// Obtiene la información del Detalle de un Trámite.
        /// </summary>
        /// <param name="pIdTramite">Id del Trámite</param>
        /// <returns>Regresa el Detalle del Trámite en un objeto tipado.</returns>
        public tramiteDetalle carga(int pIdTramite)
        {
            tramiteDetalle resultado = null;
            String SqlCmd = "SELECT * FROM TRAM00003P WHERE IdTramite = " + pIdTramite.ToString() + ";";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
                resultado = arma(datos.Rows[0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }
    }
}
