using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    /// <summary>
    /// Información del Detalle del Trámite
    /// </summary>
    public class tramiteDetalle
    {
        public int IdTramite { get; set; }
        public bool CPDES { get; set; }
        public string FolioCPDES { get; set; }
        public string EstatusCPDES { get; set; }
        public int TipoPersona { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string RFC { get; set; }
        public string FechaConst { get; set; }
        public string Nacionalidad { get; set; }
        public string TitularNombre { get; set; }
        public string TitularApPat { get; set; }
        public string TitularApMat { get; set; }
        public string TitularNacionalidad { get; set; }
        public string TitularSexo { get; set; }
        public string TitularFechaNacimiento { get; set; }
        public string SumaAsegurada { get; set; }
        public string SumaPolizas { get; set; }
        public string IdMoneda { get; set; }
        public string Detalle { get; set; }
    }
}
