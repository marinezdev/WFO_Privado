using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace wfiplib
{
    public class dbSQLServer
    {
        private string mConnectionString = "";

        public IDbConnection Conexion(string CadenaConexion)
        {
            mConnectionString = CadenaConexion;
            return new SqlConnection(mConnectionString);
        }

        public IDbConnection EstablecerConexion(string CadenaConexion)
        {
            IDbConnection conn = null;
            try
            {
                mConnectionString = CadenaConexion;
                conn = new SqlConnection(mConnectionString);
                conn.Open();
            }
            catch (Exception)
            {
                conn = null;
            }

            return conn;
        }

        public bool CerrarConexion(ref IDbConnection pConn)
        {
            bool blnResultado = false;

            try
            {
                if (pConn != null && pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                    pConn.Dispose();
                }
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                blnResultado = false;
                //TODO: Registrar LOG de ERROR.
            }

            return blnResultado;
        }

        public IDbTransaction getTransaccion(IDbConnection pConn)
        {
            IDbTransaction _trans = null;

            try
            {
                _trans = pConn.BeginTransaction();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _trans = null;
                //TODO: Registrar LOG de ERROR.
            }

            return _trans;
        }

        public bool TransaccionCommit(ref IDbTransaction pTrans)
        {
            bool blnResultado = false;

            try
            {
                pTrans.Commit();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                blnResultado = false;
                //TODO: Registrar LOG de ERROR.
            }

            return blnResultado;
        }

        public bool TransaccionRollback(ref IDbTransaction pTrans)
        {
            bool blnResultado = false;

            try
            {
                pTrans.Rollback();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                blnResultado = false;
                //TODO: Registrar LOG de ERROR.
            }

            return blnResultado;
        }

        public int EjecutarComando(ref IDbConnection pConn, ref IDbTransaction pTrans, string pSentenciaSQL)
        {
            int intResultado = -1;

            try
            {
                IDbCommand cmd = new SqlCommand()
                {
                    Connection = pConn as SqlConnection,
                    Transaction = pTrans as SqlTransaction,
                    CommandType = CommandType.Text,
                    CommandText = pSentenciaSQL
                };

                intResultado = cmd.ExecuteNonQuery();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                intResultado = -1;
                //TODO: Registrar LOG de ERROR (Verificar si muestra sentencia SQL).
            }

            return intResultado;
        }

        public DataTable ObtenerDatos(ref IDbConnection pConn, ref IDbTransaction pTrans, string pSentenciaSQL)
        {
            DataTable dtResultado = new DataTable();
            IDbCommand cmdComando = null;

            try
            {
                cmdComando = new SqlCommand()
                {
#if DEBUG
                    CommandTimeout = 1000,
#endif
                    Connection = pConn as SqlConnection,
                    Transaction = pTrans as SqlTransaction,
                    CommandType = CommandType.Text,
                    CommandText = pSentenciaSQL
                };
                dtResultado.Load(cmdComando.ExecuteReader());
                cmdComando.Dispose();

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                dtResultado = null;
                //TODO: Registrar LOG de ERROR
            }
            finally
            {
                cmdComando = null;
            }

            return dtResultado;
        }
    }
}
