using System;
using System.Data;

namespace wfiplib
{
    public class admEmisionGmm
    {
        public bool nuevo(EmisionGmm pDatos)
        {
            bool resultado = false;
            string SqlCmd = "INSERT INTO TRAM00004 (";
            SqlCmd += "IdTramite";
            SqlCmd += ",TipoPersona";
            SqlCmd += ",NumFolio";
            SqlCmd += ",Nombre";
            SqlCmd += ",ApPaterno";
            SqlCmd += ",ApMaterno";
            SqlCmd += ",FechaNacimiento";
            SqlCmd += ",RFC";
            SqlCmd += ",CURP";
            SqlCmd += ",Sexo";
            SqlCmd += ",EstadoCivil";
            SqlCmd += ",LugarNacimPais";
            SqlCmd += ",LugarNacimEstado";
            SqlCmd += ",LugarNacimCiudad";
            SqlCmd += ",Nacionalidad";
            SqlCmd += ",Calle";
            SqlCmd += ",NoExterior";
            SqlCmd += ",NoInterior";
            SqlCmd += ",CP";
            SqlCmd += ",Colonia";
            SqlCmd += ",Municipio";
            SqlCmd += ",Ciudad";
            SqlCmd += ",Estado";
            SqlCmd += ",Pais";
            SqlCmd += ",Telefono1";
            SqlCmd += ",Telefono2";
            SqlCmd += ",Correo";
            SqlCmd += ",Idetificacion";
            SqlCmd += ",NoIdentificacion";
            SqlCmd += ",FolioMercantil";
            SqlCmd += ",DetalleGiroMercantil";
            SqlCmd += ",Representante1";
            SqlCmd += ",RepresentanteNacionalidad";
            SqlCmd += ",Representante2";
            SqlCmd += ",Representante3";
            SqlCmd += ",Representante4";
            SqlCmd += ",ZonaFronteriza";


            SqlCmd += ")";

            SqlCmd += " VALUES (";
            SqlCmd += pDatos.IdTramite;
            SqlCmd += "," + pDatos.TipoPersona.ToString("d");
            SqlCmd += ",'" + pDatos.NumFolio + "'";
            SqlCmd += ",'" + pDatos.Nombre + "'";
            SqlCmd += ",'" + pDatos.ApPaterno + "'";
            SqlCmd += ",'" + pDatos.ApMaterno + "'";
            SqlCmd += ",'" + pDatos.FechaNacimiento.ToString("dd/MM/yyyy") + "'";
            SqlCmd += ",'" + pDatos.RFC + "'";
            SqlCmd += ",'" + pDatos.CURP + "'";
            SqlCmd += "," + pDatos.Sexo.ToString();
            SqlCmd += "," + pDatos.EstadoCivil.ToString();
            SqlCmd += ",'" + pDatos.LugarNacimPais + "'";
            SqlCmd += ",'" + pDatos.LugarNacimEstado + "'";
            SqlCmd += ",'" + pDatos.LugarNacimCiudad + "'";
            SqlCmd += ",'" + pDatos.Nacionalidad + "'";
            SqlCmd += ",'" + pDatos.Calle + "'";
            SqlCmd += ",'" + pDatos.NoExterior + "'";
            SqlCmd += ",'" + pDatos.NoInterior + "'";
            SqlCmd += ",'" + pDatos.CP + "'";
            SqlCmd += ",'" + pDatos.Colonia + "'";
            SqlCmd += ",'" + pDatos.Municipio + "'";
            SqlCmd += ",'" + pDatos.Ciudad + "'";
            SqlCmd += ",'" + pDatos.Estado + "'";
            SqlCmd += ",'" + pDatos.Pais + "'";
            SqlCmd += ",'" + pDatos.Telefono1 + "'";
            SqlCmd += ",'" + pDatos.Telefono2 + "'";
            SqlCmd += ",'" + pDatos.Correo + "'";
            SqlCmd += "," + pDatos.Idetificacion.ToString () ;
            SqlCmd += ",'" + pDatos.NoIdentificacion + "'";
            SqlCmd += ",'" + pDatos.FolioMercantil + "'";
            SqlCmd += ",'" + pDatos.DetalleGiroMercantil + "'";
            SqlCmd += ",'" + pDatos.Representante1 + "'";
            SqlCmd += ",'" + pDatos.RepresentanteNacionalidad + "'";
            SqlCmd += ",'" + pDatos.Representante2 + "'";
            SqlCmd += ",'" + pDatos.Representante3 + "'";
            SqlCmd += ",'" + pDatos.Representante4 + "'";
            SqlCmd += "," + pDatos.ZonaFronteriza.ToString () ;
            SqlCmd += ");";
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd);

            BD.cierraBD();

            return resultado;
        }

        public EmisionGmm carga(int pId)
        {
            EmisionGmm respuesta = new EmisionGmm();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM TRAM00004 WHERE IdTramite=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private EmisionGmm arma(DataRow pRegistro)
        {
            EmisionGmm respuesta = new EmisionGmm();
            if (!pRegistro.IsNull("IdTramite")) respuesta.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("TipoPersona")) respuesta.TipoPersona = (E_TipoPersona)(pRegistro["TipoPersona"]);
            if (!pRegistro.IsNull("NumFolio")) respuesta.NumFolio = Convert.ToString(pRegistro["NumFolio"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("ApPaterno")) respuesta.ApPaterno = Convert.ToString(pRegistro["ApPaterno"]);
            if (!pRegistro.IsNull("ApMaterno")) respuesta.ApMaterno = Convert.ToString(pRegistro["ApMaterno"]);
            if (!pRegistro.IsNull("FechaNacimiento")) respuesta.FechaNacimiento = Convert.ToDateTime(pRegistro["FechaNacimiento"]);
            if (!pRegistro.IsNull("RFC")) respuesta.RFC = Convert.ToString(pRegistro["RFC"]);
            if (!pRegistro.IsNull("CURP")) respuesta.CURP = Convert.ToString(pRegistro["CURP"]);
            if (!pRegistro.IsNull("Sexo")) respuesta.Sexo = Convert.ToInt32(pRegistro["Sexo"]);
            if (!pRegistro.IsNull("EstadoCivil")) respuesta.EstadoCivil = Convert.ToInt32(pRegistro["EstadoCivil"]);
            if (!pRegistro.IsNull("LugarNacimPais")) respuesta.LugarNacimPais = Convert.ToString(pRegistro["LugarNacimPais"]);
            if (!pRegistro.IsNull("LugarNacimEstado")) respuesta.LugarNacimEstado = Convert.ToString(pRegistro["LugarNacimEstado"]);
            if (!pRegistro.IsNull("LugarNacimCiudad")) respuesta.LugarNacimCiudad = Convert.ToString(pRegistro["LugarNacimCiudad"]);
            if (!pRegistro.IsNull("[Nacionalidad")) respuesta.Nacionalidad = Convert.ToString(pRegistro["[Nacionalidad"]);
            if (!pRegistro.IsNull("Calle")) respuesta.Calle = Convert.ToString(pRegistro["Calle"]);
            if (!pRegistro.IsNull("NoExterior")) respuesta.NoExterior = Convert.ToString(pRegistro["NoExterior"]);
            if (!pRegistro.IsNull("NoInterior")) respuesta.NoInterior = Convert.ToString(pRegistro["NoInterior"]);
            if (!pRegistro.IsNull("CP")) respuesta.CP = Convert.ToString(pRegistro["CP"]);
            if (!pRegistro.IsNull("Colonia")) respuesta.Colonia = Convert.ToString(pRegistro["Colonia"]);
            if (!pRegistro.IsNull("Municipio")) respuesta.Municipio = Convert.ToString(pRegistro["Municipio"]);
            if (!pRegistro.IsNull("Ciudad")) respuesta.Ciudad = Convert.ToString(pRegistro["Ciudad"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("Pais")) respuesta.Pais = Convert.ToString(pRegistro["Pais"]);
            if (!pRegistro.IsNull("Telefono1")) respuesta.Telefono1 = Convert.ToString(pRegistro["Telefono1"]);
            if (!pRegistro.IsNull("Telefono2")) respuesta.Telefono2 = Convert.ToString(pRegistro["Telefono2"]);
            if (!pRegistro.IsNull("Correo")) respuesta.Correo = Convert.ToString(pRegistro["Correo"]);
            if (!pRegistro.IsNull("Idetificacion")) respuesta.Idetificacion = Convert.ToInt32(pRegistro["Idetificacion"]);
            if (!pRegistro.IsNull("NoIdentificacion")) respuesta.NoIdentificacion = Convert.ToString(pRegistro["NoIdentificacion"]);
            if (!pRegistro.IsNull("FolioMercantil")) respuesta.FolioMercantil = Convert.ToString(pRegistro["FolioMercantil"]);
            if (!pRegistro.IsNull("DetalleGiroMercantil")) respuesta.DetalleGiroMercantil = Convert.ToString(pRegistro["DetalleGiroMercantil"]);
            if (!pRegistro.IsNull("Representante1")) respuesta.Representante1 = Convert.ToString(pRegistro["Representante1"]);
            if (!pRegistro.IsNull("RepresentanteNacionalidad")) respuesta.RepresentanteNacionalidad = Convert.ToString(pRegistro["RepresentanteNacionalidad"]);
            if (!pRegistro.IsNull("Representante2")) respuesta.Representante2 = Convert.ToString(pRegistro["Representante2"]);
            if (!pRegistro.IsNull("Representante3")) respuesta.Representante3 = Convert.ToString(pRegistro["Representante3"]);
            if (!pRegistro.IsNull("Representante4")) respuesta.Representante4 = Convert.ToString(pRegistro["Representante4"]);
            if (!pRegistro.IsNull("ZonaFronteriza")) respuesta.ZonaFronteriza = Convert.ToInt32(pRegistro["ZonaFronteriza"]);

            return respuesta;
        }

        public bool elimina(int pIdTramite)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("DELETE From TRAM00004 WHERE IdTramite=" + pIdTramite.ToString());
            BD.cierraBD();
            return resultado;
        }
    }

    [Serializable]
    public class EmisionGmm
    {
        private int mIdTramite = 0;
        public int IdTramite { get { return mIdTramite; } set { mIdTramite = value; } }
        private E_TipoPersona mTipoPersona = E_TipoPersona.Fisica;
        public E_TipoPersona TipoPersona { get { return mTipoPersona; } set { mTipoPersona = value; } }

        private string mNumFolio = "";
        public string NumFolio { get { return mNumFolio; } set { mNumFolio = value; } }
        private string mNombre = "";
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mApPaterno = "";
        public string ApPaterno { get { return mApPaterno; } set { mApPaterno = value; } }
        private string mApMaterno = "";
        public string ApMaterno { get { return mApMaterno; } set { mApMaterno = value; } }
        private DateTime mFechaNacimiento = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaNacimiento { get { return mFechaNacimiento; } set { mFechaNacimiento = value; } }
        private string mRFC = "";
        public string RFC { get { return mRFC; } set { mRFC = value; } }
        private string mCURP = "";
        public string CURP { get { return mCURP; } set { mCURP = value; } }
        private int mSexo = 0;
        public int Sexo { get { return mSexo; } set { mSexo = value; } }
        private int mEstadoCivil = 0;
        public int EstadoCivil { get { return mEstadoCivil; } set { mEstadoCivil = value; } }
        private string mLugarNacimPais= "";
        public string LugarNacimPais { get { return mLugarNacimPais; } set { mLugarNacimPais = value; } }
        private string mLugarNacimEstado = "";
        public string LugarNacimEstado { get { return mLugarNacimEstado; } set { mLugarNacimEstado = value; } }
        private string mLugarNacimCiudad = "";
        public string LugarNacimCiudad { get { return mLugarNacimCiudad; } set { mLugarNacimCiudad = value; } }
        private string mNacionalidad = "";
        public string Nacionalidad { get { return mNacionalidad; } set { mNacionalidad = value; } }
        private string mCalle = "";
        public string Calle { get { return mCalle; } set { mCalle = value; } }
        private string mNoExterior = "";
        public string NoExterior { get { return mNoExterior; } set { mNoExterior = value; } }
        private string mNoInterior = "";
        public string NoInterior { get { return mNoInterior; } set { mNoInterior = value; } }
        private string mCP = "";
        public string CP { get { return mCP; } set { mCP = value; } }
        private string mColonia = "";
        public string Colonia { get { return mColonia; } set { mColonia = value; } }
        private string mMunicipio = "";
        public string Municipio { get { return mMunicipio; } set { mMunicipio = value; } }
        private string mCiudad = "";
        public string Ciudad { get { return mCiudad; } set { mCiudad = value; } }
        private string mEstado = "";
        public string Estado { get { return mEstado; } set { mEstado = value; } }
        private string mPais = "";
        public string Pais { get { return mPais; } set { mPais = value; } }
        private string mTelefono1 = "";
        public string Telefono1 { get { return mTelefono1; } set { mTelefono1 = value; } }
        private string mTelefono2 = "";
        public string Telefono2 { get { return mTelefono2; } set { mTelefono2 = value; } }
        private string mCorreo = "";
        public string Correo { get { return mCorreo; } set { mCorreo = value; } }
        private int mIdetificacion = 0;
        public int Idetificacion { get { return mIdetificacion; } set { mIdetificacion = value; } }
        private string mNoIdentificacion = "";
        public string NoIdentificacion { get { return mNoIdentificacion; } set { mNoIdentificacion = value; } }

        private string mFolioMercantil = "";
        public string FolioMercantil { get { return mFolioMercantil; } set { mFolioMercantil = value; } }
        private string mDetalleGiroMercantil = "";
        public string DetalleGiroMercantil { get { return mDetalleGiroMercantil; } set { mDetalleGiroMercantil = value; } }
        private string mRepresentante1 = "";
        public string Representante1 { get { return mRepresentante1; } set { mRepresentante1 = value; } }
        private string mRepresentanteNacionalidad = "";
        public string RepresentanteNacionalidad { get { return mRepresentanteNacionalidad; } set { mRepresentanteNacionalidad = value; } }
        private string mRepresentante2 = "";
        public string Representante2 { get { return mRepresentante2; } set { mRepresentante2 = value; } }
        private string mRepresentante3 = "";
        public string Representante3 { get { return mRepresentante3; } set { mRepresentante3 = value; } }
        private string mRepresentante4 = "";
        public string Representante4 { get { return mRepresentante4; } set { mRepresentante4 = value; } }
        private int mZonaFronteriza = 0;
        public int ZonaFronteriza { get { return mZonaFronteriza; } set { mZonaFronteriza = value; } }

        public string DatosHtml
        {
            get
            {
                string cadena = "<p>";
                cadena += "Nombre(s):<b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></br>";
                cadena += "RFC:<b>" + mRFC + "</b></br>";
                cadena += "</p>";
                return cadena;
            }
        }

        public string CabeceraHtml
        {
            get
            {
                string cadena = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                cadena += "<tr><td style ='width:150px'>Número trámite:</td><td><b>" + mIdTramite.ToString().PadLeft(5, '0') + "</b></td></tr>";
                cadena += "<tr><td>Nombre(s):</td><td><b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></td></tr>";
                cadena += "</table>";
                return cadena;
            }
        }
    }
}
