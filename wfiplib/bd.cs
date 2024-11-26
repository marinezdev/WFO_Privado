using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace wfiplib
{
    class bd
    {
        private SqlConnection mBDConect = null;
        private String mStrConecta = String.Empty;

        public bd()
        {
            if (leeCadenaConeccion())
            {
                mBDConect = new SqlConnection(mStrConecta);
                mBDConect.Open();
            }
        }

        private Boolean leeCadenaConeccion()
        {
            mStrConecta = ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString;
            return (mStrConecta.Length > 0);
        }

        public string strConecta { get { return mStrConecta; } }

        private void generationLog(string fileContent) 
        {
            string fileName = "";
            string filePath = "C:\\metlife\\wfip\\log";

            try 
            {
                if (!File.Exists(filePath + "\\sqlTracerLog.rmf"))
                    return;

                fileName = filePath + "\\SQLTracer" + "Log_" + DateTime.Now.ToString("yyMMdd") + ".wfolog";

                //creamos el archivo y guardamos
                StreamWriter sw = new StreamWriter(fileName, true);
                sw.Write(DateTime.Now.ToString("yyMMddHHmmss") + "          " + fileContent + Environment.NewLine);
                sw.Close();
            }
            catch (Exception ex)
            {
                string errormessage = ex.Message.ToString();
            }
        }

        public Boolean ejecutaCmd(string pSqlCmd)
        {
            generationLog(pSqlCmd);

            int resultado = 0;
            try
            {
                SqlCommand procSqlCmd = new SqlCommand(pSqlCmd, mBDConect);
                resultado = procSqlCmd.ExecuteNonQuery();
                procSqlCmd.Dispose();
            }
            catch (Exception ex)
            {
                string _error = ex.Message.ToString();
            }
            return (resultado > 0);
        }

        public int ejecutaCmdScalar(String pSqlCmd)
        {
            generationLog(pSqlCmd);

            int resultado = 0;
            SqlCommand procSqlCmd = new SqlCommand(pSqlCmd, mBDConect);
            resultado = Convert.ToInt32(procSqlCmd.ExecuteScalar());
            procSqlCmd.Dispose();
            return resultado;
        }

        public DataTable leeDatos(String pSqlCmd)
        {
            generationLog(pSqlCmd);

            DataTable resultado = new DataTable();
            SqlDataAdapter Adaptador = new SqlDataAdapter();
            SqlCommand procSqlCmd = new SqlCommand(pSqlCmd, mBDConect);
            Adaptador.SelectCommand = procSqlCmd;
            procSqlCmd.CommandTimeout = 0;   

            Adaptador.Fill(resultado);
            Adaptador.Dispose();
            procSqlCmd.Dispose();
            return resultado;
        }

        public DataSet leeDatosDataSet(String pSqlCmd)
        {
            generationLog(pSqlCmd);

            DataSet resultado = new DataSet();
            SqlDataAdapter Adaptador = new SqlDataAdapter();
            SqlCommand procSqlCmd = new SqlCommand(pSqlCmd, mBDConect);
            Adaptador.SelectCommand = procSqlCmd;
            procSqlCmd.CommandTimeout = 0;

            Adaptador.Fill(resultado);
            Adaptador.Dispose();
            procSqlCmd.Dispose();
            return resultado;
        }

        public void cierraBD()
        {
            if (mBDConect != null && mBDConect.State == System.Data.ConnectionState.Open)
                mBDConect.Close();
        }
    }
}
