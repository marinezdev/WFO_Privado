using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace wfiplib
{
    public class admWFOSiguienteTramite
    {
        private string mCadenaConexionDB = "";

        public admWFOSiguienteTramite(string CadenaConexionDB)
        {
            mCadenaConexionDB = CadenaConexionDB;
        }

        public int daSiguienteTramite(credencial pUsuario, int pIdMesa, ref bool pMostrarMensaje, ref string pMensaje)
        {
            IDbConnection _conn = null;
            IDbTransaction _trans = null;
            DataTable dtTramites = null;
            wfiplib.dbSQLServer cDatos = new wfiplib.dbSQLServer();
            wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
            wfiplib.tramiteMesa oTramiteMesa = new wfiplib.tramiteMesa();
            int intIdTramiteAsignado = -1;
            int intIdTipoTramite = -1;
            string strSQL = "";
            bool blnAtrapado = false;
            bool blnDisponibles = false;
            bool blnTramiteAsignadoSuper = false;

            // Inicialización de Variables
            pMostrarMensaje = false;
            pMensaje = "";

            try
            {
                // Establcemos la conexion.
                _conn = cDatos.EstablecerConexion(mCadenaConexionDB);
                _trans = cDatos.getTransaccion(_conn);

                // Aseguramos transacción
                //cDatos.EjecutarComando(ref _conn, ref _trans, "UPDATE tramiteMesa SET IdTramite = IdTramite WHERE IdTramite = -1; ");

                // Obtenemos los tipos de Trámites.
                strSQL = "SELECT DISTINCT IdTipoTramite FROM usuariosMesa WHERE IdUsuario = " + pUsuario.Id.ToString() + "; ";
                dtTramites = cDatos.ObtenerDatos(ref _conn, ref _trans, strSQL);
                if (dtTramites.Rows.Count > 0)
                {
                    intIdTipoTramite = int.Parse(dtTramites.Rows[0]["IdTipoTramite"].ToString());
                    oTramiteMesa = oAdmTramiteMesa.daAtrapado(pIdMesa, pUsuario.Id, ref _conn, ref _trans, ref cDatos);
                    if (oTramiteMesa.IdTramite > 0)
                    {
                        intIdTramiteAsignado = oTramiteMesa.IdTramite;
                    }
                    else
                    {
                        do
                        {
                            blnTramiteAsignadoSuper = false;
                            oTramiteMesa = oAdmTramiteMesa.daAsignado(pIdMesa, pUsuario.Id, ref _conn, ref _trans, ref cDatos);
                            if (oTramiteMesa.IdTramite > 0)
                            {
                                blnTramiteAsignadoSuper = true;
                                intIdTramiteAsignado = oTramiteMesa.IdTramite;
                                string strFolioTramite = oAdmTramiteMesa.getFolio(intIdTramiteAsignado);
                                blnAtrapado = oAdmTramiteMesa.atrapa(oTramiteMesa.IdTramite, pIdMesa, pUsuario.Id, ref _conn, ref _trans, ref cDatos);
                                pMostrarMensaje = true;
                                pMensaje = "El támite con folio: " + strFolioTramite + " ha sido asignado por el Supervisor.";
                            }
                        }
                        while (blnTramiteAsignadoSuper == true && blnAtrapado == false);

                        if (!blnTramiteAsignadoSuper)
                        {
                            pMostrarMensaje = false;
                            pMensaje = "";

                            do
                            {
                                if (intIdTipoTramite > 0)
                                    oTramiteMesa = oAdmTramiteMesa.daSiguienteXTipoTramite(pIdMesa, intIdTipoTramite, ref _conn, ref _trans, ref cDatos);
                                else
                                    oTramiteMesa = oAdmTramiteMesa.daSiguiente(pIdMesa, pUsuario.Id, ref _conn, ref _trans, ref cDatos);

                                if (oTramiteMesa.IdTramite > 0)
                                {
                                    blnDisponibles = true;
                                    blnAtrapado = oAdmTramiteMesa.atrapa(oTramiteMesa.IdTramite, pIdMesa, pUsuario.Id, ref _conn, ref _trans, ref cDatos);
                                    if (blnAtrapado)
                                        intIdTramiteAsignado = oTramiteMesa.IdTramite;
                                }
                            } while (blnAtrapado == false && blnDisponibles == true);

                            if (blnDisponibles == false && blnAtrapado == false)
                            {
                                intIdTramiteAsignado = 0;
                            }
                        }
                    }
                }

                cDatos.TransaccionCommit(ref _trans);
            }
            catch (Exception ex)
            {
                cDatos.TransaccionRollback(ref _trans);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (cDatos != null)
                {
                    if (_conn != null)
                    {
                        cDatos.CerrarConexion(ref _conn);
                    }
                }

                _conn.Dispose();
                _trans.Dispose();
                _conn = null;
                _trans = null;


                oAdmTramiteMesa = null;
                oTramiteMesa = null;
            }

            return intIdTramiteAsignado;
        }
    }
}
