using System;
using System.Data;

namespace wfiplib
{
    public class admAgentesPromotoria
    {
        /// <summary>
        /// Obtiene la información del área comercial de la promotoría.
        /// </summary>
        /// <param name="pIdPromotoria">Identificacdor de la Promotoría</param>
        /// <returns></returns>
        public comercialPromotoria getComercialInformation(string pIdPromotoria)
        {
            comercialPromotoria resultado = new comercialPromotoria();
            string SqlCmd = "SELECT [clave_region],[region],[clave_ejecutivo],[ejecutivo],[clave_gerente],[gerente],[clave_front],[front] FROM [cat_promotorias] WHERE [Clave] ='" + pIdPromotoria + "';";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) resultado = getDataComercial(datos.Rows[0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public comercialPromotoria ConsultaInformacionPromotoria(int pIdPromotoria)
        {
            comercialPromotoria resultado = new comercialPromotoria();
            string SqlCmd = "SELECT [clave_region],[region],[clave_ejecutivo],[ejecutivo],[clave_gerente],[gerente],[clave_front],[front],[Clave] FROM [cat_promotorias] WHERE [Id] ='" + pIdPromotoria + "';";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) resultado = DataPromotoria(datos.Rows[0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public agentePromotoria buscaAgenteEnPromotoria(string pIdPromotoria, string pIdAgente)
        {
            agentePromotoria resultado = new agentePromotoria();
            string SqlCmd = "Select * from AgentesPromotoria Where PROMOTORIA='" + pIdPromotoria + "' And CLAVE='" + pIdAgente + "'";
            // string SqlCmd = "SELECT * FROM cat_agentes WHERE ClavePromotoria = '" + pIdPromotoria + "' AND CLAVE = '" + pIdAgente + "';" ;
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) resultado = arma(datos.Rows[0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public DataTable AgentesPromotoria_Selecionar(int IdPromotria, string AgenteClave)
        {
            DataTable dt = new DataTable();
            bd Data = new bd();
            string qData = "EXEC AgentesPromotoria_Selecionar "+ IdPromotria.ToString() + ", '"+ AgenteClave+"'";
            dt = Data.leeDatos(qData);
            Data.cierraBD();
            return dt;
        }

        private comercialPromotoria getDataComercial(DataRow pRegistro)
        {
            comercialPromotoria resultado = new comercialPromotoria();
            if (!pRegistro.IsNull("clave_region")) resultado.ClaveRegion = pRegistro["clave_region"].ToString();
            if (!pRegistro.IsNull("region")) resultado.Region = pRegistro["region"].ToString();
            if (!pRegistro.IsNull("clave_ejecutivo")) resultado.ClaveEjecutivo = pRegistro["clave_ejecutivo"].ToString();
            if (!pRegistro.IsNull("ejecutivo")) resultado.Ejecutivo = pRegistro["ejecutivo"].ToString();
            if (!pRegistro.IsNull("clave_gerente")) resultado.ClaveGerente = pRegistro["clave_gerente"].ToString();
            if (!pRegistro.IsNull("clave_region")) resultado.Gerente = pRegistro["gerente"].ToString();
            
            return resultado;
        }

        private comercialPromotoria DataPromotoria(DataRow pRegistro)
        {
            comercialPromotoria resultado = new comercialPromotoria();
            if (!pRegistro.IsNull("clave_region")) resultado.ClaveRegion = pRegistro["clave_region"].ToString();
            if (!pRegistro.IsNull("region")) resultado.Region = pRegistro["region"].ToString();
            if (!pRegistro.IsNull("clave_ejecutivo")) resultado.ClaveEjecutivo = pRegistro["clave_ejecutivo"].ToString();
            if (!pRegistro.IsNull("ejecutivo")) resultado.Ejecutivo = pRegistro["ejecutivo"].ToString();
            if (!pRegistro.IsNull("clave_gerente")) resultado.ClaveGerente = pRegistro["clave_gerente"].ToString();
            if (!pRegistro.IsNull("clave_region")) resultado.Gerente = pRegistro["gerente"].ToString();
            if (!pRegistro.IsNull("Clave")) resultado.Clave = pRegistro["Clave"].ToString();
            return resultado;
        }
        private agentePromotoria arma(DataRow pRegistro)
        {
            agentePromotoria resultado = new agentePromotoria();
            // if (!pRegistro.IsNull("CLAVE")) resultado.clave = Convert.ToInt32(pRegistro["CLAVE"]);
            // if (!pRegistro.IsNull("DESCRIPCION")) resultado.descripcion = Convert.ToString(pRegistro["DESCRIPCION"]);
            // if (!pRegistro.IsNull("TIPO_PERSONA")) resultado.tipoPersona = Convert.ToInt32(pRegistro["TIPO_PERSONA"]);
            // if (!pRegistro.IsNull("PROMOTORIA")) resultado.promotoria = Convert.ToInt32(pRegistro["PROMOTORIA"]);
            // if (!pRegistro.IsNull("DESC_PROMOTORIA")) resultado.descPromotoria = Convert.ToString(pRegistro["DESC_PROMOTORIA"]);
            if (!pRegistro.IsNull("Clave")) resultado.clave = Convert.ToInt32(pRegistro["Clave"]);
            if (!pRegistro.IsNull("DESCRIPCION")) resultado.descripcion = Convert.ToString(pRegistro["DESCRIPCION"]);
            if (!pRegistro.IsNull("EMAIL")) resultado.Email = Convert.ToString(pRegistro["EMAIL"]);
            if (!pRegistro.IsNull("EMAIL_ALTERNO")) resultado.EmailAlterno = Convert.ToString(pRegistro["EMAIL_ALTERNO"]);

            return resultado;
        }

        public DataTable AgentesPromotoria_Seleccionar_PorIdPromotoria(int IdPromotoria)
        {
            DataTable dt = new DataTable();
            bd ExportarExcel = new bd();
            string qExportarExcel = "EXEC AgentesPromotoria_Seleccionar_PorIdPromotoria " + IdPromotoria;
            dt = ExportarExcel.leeDatos(qExportarExcel);
            ExportarExcel.cierraBD();
            return dt;
        }

        public DataTable ConsultaMegasPromotoria(int IdPromotoria)
        {
            DataTable dt = new DataTable();
            bd ExportarExcel = new bd();
            string qExportarExcel = "EXEC ConsultaMegasPromotoria " + IdPromotoria;
            dt = ExportarExcel.leeDatos(qExportarExcel);
            ExportarExcel.cierraBD();
            return dt;
        }

    }

    public class agentePromotoria
    {
        private int mClave = 0;
        public int clave { get { return mClave; } set { mClave = value; } }
        private string mDescripcion = "";
        public string descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
        private int mTipoPersona = 0;
        public int tipoPersona { get { return mTipoPersona; } set { mTipoPersona = value; } }
        private int mPromotoria = 0;
        public int promotoria { get { return mPromotoria; } set { mPromotoria = value; } }
        private string mDescPromotoria = "";
        public string descPromotoria { get { return mDescPromotoria; } set { mDescPromotoria = value; } }
        private string mEmail = "";
        public string Email { get { return mEmail; } set { mEmail = value; } }
        private string mEmailAlterno;
        public string EmailAlterno { get { return mEmailAlterno; } set { mEmailAlterno = value; } }
    }

    /// <summary>
    /// Clase. Área Comercial de Promotorias. 
    /// </summary>
    public class comercialPromotoria
    {
        private string mClaveRegion;
        private string mClaveEjecutivo;
        private string mClaveGerente;
        private string mClaveFront;
        private string mRegion;
        private string mEjecutivo;
        private string mGerente;
        private string mFront;
        private string mClave;

        public string Clave { get { return mClave; } set { mClave = value; } }
        public string ClaveRegion { get { return mClaveRegion; } set { mClaveRegion = value; } }
        public string ClaveEjecutivo { get { return mClaveEjecutivo; } set { mClaveEjecutivo = value; } }
        public string ClaveGerente { get { return mClaveGerente; } set { mClaveGerente = value; } }
        public string ClaveFront { get { return mClaveFront; } set { mClaveFront = value; } }
        public string Region { get { return mRegion; } set { mRegion = value; } }
        public string Ejecutivo { get { return mEjecutivo; } set { mEjecutivo = value; } }
        public string Gerente { get { return mGerente; } set { mGerente = value; } }
        public string Front { get { return mFront; } set { mFront = value; } }
    }
}
