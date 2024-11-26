using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace wfiplib
{
    public class admBuscar
    {
        /// <summary>
        /// Agrega un registro y obtiene su Id
        /// </summary>
        /// <returns></returns>
        public int siguienteId()
        {
            int resultado = 0;
            StringBuilder SqlCmd = new StringBuilder("Insert Into tramiteControl(Fecha) Values(GetDate()) SELECT SCOPE_IDENTITY()");
            bd BD = new bd();
            resultado = BD.ejecutaCmdScalar(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public string getFolio(int IdTramite)
        {
            string resultado = "";
            DataTable datos;
            try
            {
                string strSQL = "SELECT FolioCompuesto FROM folio WHERE IdTramite = " + IdTramite.ToString() + ";";
                bd BD = new bd();
                datos = BD.leeDatos(strSQL);
                if (datos.Rows.Count > 0)
                {
                    resultado = datos.Rows[0][0].ToString();
                }
                BD.cierraBD();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                resultado = "Folio: Error";
            }

            return resultado;
        }

        /// <summary>
        /// Agrega un nuevo trámite
        /// </summary>
        /// <param name="pTramite"></param>
        /// <returns></returns>
        public bool nuevo(tramiteP pTramite)
        {
            //TODO: Actualizar correctamente el proceso de la Sentencia SQL
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into tramite (Id,IdTipoTramite,FechaRegistro,Estado,DatosHtml,IdPromotoria,IdUsuario,AgenteClave,NumeroOrden,FechaSolicitud,TipoTramite, idRamo)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(pTramite.Id.ToString());
            SqlCmd.Append("," + pTramite.IdTipoTramite.ToString("d"));
            SqlCmd.Append(",GetDate()");
            SqlCmd.Append("," + E_EstadoTramite.Registro.ToString("d"));
            SqlCmd.Append(",'" + pTramite.DatosHtml.Replace("'", "''") + "'");
            SqlCmd.Append("," + pTramite.IdPromotoria.ToString());
            SqlCmd.Append("," + pTramite.IdUsuario.ToString());
            SqlCmd.Append(",'" + pTramite.AgenteClave.ToString() + "'");
            SqlCmd.Append(",'" + pTramite.NumeroOrden.ToString() + "'");
            SqlCmd.Append(",'" + pTramite.FechaSolicitud.ToString() + "'");
            SqlCmd.Append(",'" + pTramite.TipoTramite.ToString() + "'");
            SqlCmd.Append(",'0'");              // RMF Este campo ya no se utiliza
            //SqlCmd.Append(",'" + pTramite.CPDES.ToString() + "'");
            //SqlCmd.Append(",'" + pTramite.FolioCPDES.ToString() + "'");
            //SqlCmd.Append(",'" + pTramite.EstatusCPDES.ToString() + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Agrega un nuevo registro y obtiene su folio compuesto
        /// </summary>
        /// <param name="pTramite"></param>
        /// <returns></returns>
        /*public bool nuevoFolio(tramiteP pTramite)
        {
            bool resultado = true;
            String Abreviatura = "";

            // String IdTipoTramite = pTramite.IdTipoTramite.ToString("d");
            switch (pTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    Abreviatura = "NV";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    Abreviatura = "NG";
                    break;
            }

            StringBuilder SqlCmd = new StringBuilder("INSERT INTO dbo.Folio(IdTramite,TramitePP,TipoTramite,Year,Mes,Dia,IdPromotoria,Folio,FolioCompuesto)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(pTramite.Id.ToString());
            SqlCmd.Append(",'" + pTramite.TipoTramite.ToString() + "'");
            SqlCmd.Append(",'" + Abreviatura + "'");
            SqlCmd.Append(",FORMAT(GETDATE(), 'yy')");
            SqlCmd.Append(",FORMAT(GETDATE(), 'MM')");
            SqlCmd.Append(",FORMAT(GETDATE(), 'dd')");
            SqlCmd.Append("," + pTramite.IdPromotoria.ToString());
            SqlCmd.Append(",ISNULL((SELECT MAX(Folio) FROM dbo.Folio WHERE[TramitePP] = '" + pTramite.TipoTramite.ToString() + "' AND [TipoTramite] = '" + Abreviatura + "' ),0) + 1");
            SqlCmd.Append(",'" + Abreviatura + "'+FORMAT(GETDATE(),'yyMMdd')+RIGHT('000' + Ltrim(Rtrim('" + pTramite.IdPromotoria.ToString() + "')),4)+RIGHT('00000000' + Ltrim(Rtrim(ISNULL((SELECT MAX(Folio) FROM dbo.Folio WHERE[TramitePP] = '" + pTramite.TipoTramite.ToString() + "' AND [TipoTramite] = '" + Abreviatura + "' ),0) + 1)),8)");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }*/

        /// <summary>
        /// Regresa el Id del Status de la Mesa Indicado
        /// </summary>
        /// <param name="NombreMesa">Nombre de la Mesa</param>
        /// <returns></returns>
        public int getIdStatusMesa(string NombreMesa)
        {
            int intId = -1;
            string strSQL = "SELECT Id FROM statusMesa WHERE Nombre = '" + NombreMesa + "';";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                intId = Convert.ToInt32(datos.Rows[0]["Id"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return intId;
        }

        /// <summary>
        /// Valida si una mesa tiene permisos para establecer algún status del tramite
        /// </summary>
        /// <param name="IdFlujo"></param>
        /// <param name="IdTipoTramite"></param>
        /// <param name="IdMesa"></param>
        /// <param name="IdStatusMesa"></param>
        /// <returns></returns>
        public Boolean getPermisoMesaStatus(int IdFlujo, wfiplib.E_TipoTramite IdTipoTramite, int IdMesa, int IdStatusMesa)
        {
            Boolean blnPermiso = false;
            string strSQL = "SELECT Activo FROM statusMesaAsign WHERE IdFlujo = " + IdFlujo.ToString() + " AND IdTipoTramite = " + Convert.ToInt16(IdTipoTramite) + " AND IdMesa = " + IdMesa.ToString() + " AND IdStatusMesa = " + IdStatusMesa.ToString() + ";";
            Console.Write(strSQL);
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                blnPermiso = Convert.ToBoolean(datos.Rows[0]["Activo"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return blnPermiso;
        }

        /// <summary>
        /// Obtiene el detalle de un trámite
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public tramiteP carga(int pId)
        {
            tramiteP resultado = null;
            String SqlCmd = "SELECT * FROM vw_tramiteP WHERE Id = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private tramiteP arma(DataRow pRegistro)
        {
            tramiteP resultado = new tramiteP();
            if (!pRegistro.IsNull("Id")) resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdTipoTramite")) resultado.IdTipoTramite = (E_TipoTramite)(pRegistro["IdTipoTramite"]);
            // if (!pRegistro.IsNull("idRamo")) resultado.IdRamoTramite = (E_RamoTramite)(pRegistro["idRamo"]); //RMF -> Este código ya no se utiliza
            if (!pRegistro.IsNull("FechaRegistro")) resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("FechaTermino")) resultado.FechaTermino = Convert.ToDateTime(pRegistro["FechaTermino"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = (E_EstadoTramite)pRegistro["Estado"];
            if (!pRegistro.IsNull("EstadoNombre")) resultado.EstadoNombre = Convert.ToString(pRegistro["EstadoNombre"]);
            if (!pRegistro.IsNull("DatosHtml")) resultado.DatosHtml = Convert.ToString(pRegistro["DatosHtml"]);
            if (!pRegistro.IsNull("IdPromotoria")) resultado.IdPromotoria = Convert.ToInt32(pRegistro["IdPromotoria"]);
            if (!pRegistro.IsNull("IdUsuario")) resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("AgenteClave")) resultado.AgenteClave = Convert.ToString(pRegistro["AgenteClave"]);
            if (!pRegistro.IsNull("IdFlujo")) resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Flujo")) resultado.Flujo = Convert.ToString(pRegistro["Flujo"]);
            if (!pRegistro.IsNull("TramiteNombre")) resultado.TramiteNombre = Convert.ToString(pRegistro["TramiteNombre"]);
            if (!pRegistro.IsNull("Tabla")) resultado.Tabla = Convert.ToString(pRegistro["Tabla"]);
            if (!pRegistro.IsNull("PromotoriaNombre")) resultado.PromotoriaNombre = Convert.ToString(pRegistro["PromotoriaNombre"]);
            if (!pRegistro.IsNull("PromotoriaClave")) resultado.PromotoriaClave = Convert.ToString(pRegistro["PromotoriaClave"]);
            if (!pRegistro.IsNull("AgenteNombre")) resultado.AgenteNombre = Convert.ToString(pRegistro["AgenteNombre"]);
            if (!pRegistro.IsNull("UsuarioNombre")) resultado.UsuarioNombre = Convert.ToString(pRegistro["UsuarioNombre"]);
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista con todos los trámites que pertenecen a un usuario
        /// </summary>
        /// <param name="pIdUsuario"></param>
        /// <returns></returns>
        public List<tramiteP> daListaPorCreador(int pIdUsuario)
        {
            List<tramiteP> resultado = new List<tramiteP>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdUsuario=" + pIdUsuario.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los tramites que pertenecen a un usuario
        /// </summary>
        /// <param name="pIdUsuario"></param>
        /// <returns></returns>
        public DataTable daTablaTramitesCreador(int pIdUsuario)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdUsuario=" + pIdUsuario.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Obtiene todos los tramites que pertenecen a una promotoría  
        /// </summary>
        /// <param name="pIdPromotoria"></param>
        /// <returns></returns>
        public DataTable daTablaTramitesPromotoria(int pIdPromotoria)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdPromotoria = " + pIdPromotoria.ToString() + " ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Realiza filtros a partir del tipo de trámite
        /// </summary>
        /// <param name="pIdPromotoria"></param>
        /// <param name="TipoTramite"></param>
        /// <returns></returns>
        public DataTable daTablaTramitesPromotoriaTipoTramite(int pIdPromotoria, string TipoTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramitePTIPO WHERE IdPromotoria = " + pIdPromotoria.ToString() + " AND TipoTramite = '" + TipoTramite.ToString() + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesCreadorPendientes(int pIdUsuario)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdUsuario =" + pIdUsuario.ToString() + " AND (Estado=" + E_EstadoTramite.Hold.ToString("d") + " OR Estado=" + E_EstadoTramite.Suspendido.ToString("d") + ")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesPromotoriaPendientes(int pIdPromotoria)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdPromotoria = " + pIdPromotoria + " AND (Estado=" + E_EstadoTramite.Hold.ToString("d") + " OR Estado=" + E_EstadoTramite.Suspendido.ToString("d") + " OR Estado = " + E_EstadoTramite.PCI.ToString("d") + ")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesPromotoriaPendientesTipoTramite(int pIdPromotoria, string TipoTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramitePTIPO WHERE TipoTramite = '" + TipoTramite.ToString() + "' AND IdPromotoria = " + pIdPromotoria + " AND (Estado=" + E_EstadoTramite.Hold.ToString("d") + " OR Estado=" + E_EstadoTramite.Suspendido.ToString("d") + ")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesPromotoriaPorEstado(int pIdPromotoria, E_EstadoTramite pEstado)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdPromotoria = " + pIdPromotoria.ToString() + " AND Estado=" + pEstado.ToString("d"));
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTodosLosTramitesPendientes()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE (Estado=" + E_EstadoTramite.Hold.ToString("d") + " OR Estado=" + E_EstadoTramite.Suspendido.ToString("d") + ") ORDER BY Id");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Actualiza un tramite a un estado indicado
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pEstado"></param>
        /// <returns></returns>
        public Boolean cambiaEstado(int pId, E_EstadoTramite pEstado)
        {
            Boolean resultado = false;
            String SqlCmd = "UPDATE tramite SET Estado=" + pEstado.ToString("d") + " WHERE Id=" + pId.ToString();
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd);
            BD.cierraBD();
            return resultado;
        }

        public List<tramiteP> buscaEnDatosHtml(string pDatos)
        {
            List<tramiteP> resultado = new List<tramiteP>();
            string SqlCmd = "Select * From vw_tramiteP Where DatosHtml Like '%" + pDatos + "%' Order by Id";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro)); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public DataTable buscaEnDatosHtmlDataTable(string pDatos)
        {
            DataTable resultado = new DataTable();
            string SqlCmd = "Select * From vw_tramiteP Where DatosHtml Like '%" + pDatos + "%' Order by Id";
            bd BD = new bd();
            resultado = BD.leeDatos(SqlCmd);
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Elimina un tramite
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        /*public bool elimina(int pId)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("DELETE From tramite WHERE Id=" + pId.ToString());
            BD.cierraBD();
            return resultado;
        }*/

    }

    [Serializable]
    //public class tramite
    public class Buscar
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private E_TipoTramite mIdTipoTramite = E_TipoTramite.Ninguno;
        public E_TipoTramite IdTipoTramite { get { return mIdTipoTramite; } set { mIdTipoTramite = value; } }
        private DateTime mFechaRegistro = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private DateTime mFechaTermino = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime FechaTermino { get { return mFechaTermino; } set { mFechaTermino = value; } }
        public E_EstadoTramite Estado { get; set; }
        private string mEstadoNombre = "";
        public string EstadoNombre { get { return mEstadoNombre; } set { mEstadoNombre = value; } }
        private string mDatosHtml = "";
        public string DatosHtml { get { return mDatosHtml; } set { mDatosHtml = value; } }
        private int mIdPromotoria = 0;
        public int IdPromotoria { get { return mIdPromotoria; } set { mIdPromotoria = value; } }
        private int mIdUsuario = 0;
        public int IdUsuario { get { return mIdUsuario; } set { mIdUsuario = value; } }
        private int mAgenteClave = 0;
        public int AgenteClave { get { return mAgenteClave; } set { mAgenteClave = value; } }
        // NUEVOS DATOS 
        private string mNumeroOrden = "";
        public string NumeroOrden { get { return mNumeroOrden; } set { mNumeroOrden = value; } }
        private string mFechaSolicitud = "";
        public string FechaSolicitud { get { return mFechaSolicitud; } set { mFechaSolicitud = value; } }
        private string mFolioCPDES = "";
        public string FolioCPDES { get { return mFolioCPDES; } set { mFolioCPDES = value; } }
        private string mEstatusCPDES = "";
        public string EstatusCPDES { get { return mEstatusCPDES; } set { mEstatusCPDES = value; } }

        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private string mFlujo = "";
        public string Flujo { get { return mFlujo; } set { mFlujo = value; } }
        private string mTramiteNombre = "";
        public string TramiteNombre { get { return mTramiteNombre; } set { mTramiteNombre = value; } }
        private string mTabla = "";
        public string Tabla { get { return mTabla; } set { mTabla = value; } }
        private string mPromotoriaNombre = "";
        public string PromotoriaNombre { get { return mPromotoriaNombre; } set { mPromotoriaNombre = value; } }
        private int mPromotoriaClave = 0;
        public int PromotoriaClave { get { return mPromotoriaClave; } set { mPromotoriaClave = value; } }
        private string mAgenteNombre = "";
        public string AgenteNombre { get { return mAgenteNombre; } set { mAgenteNombre = value; } }
        private string mUsuarioNombre = "";
        public string UsuarioNombre { get { return mUsuarioNombre; } set { mUsuarioNombre = value; } }
    }
}
 

