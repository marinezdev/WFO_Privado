using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admIndicadores
    {
        public DataTable IndicadoresGeneralesMesas(int pIdFlujo)
        {
            DataTable resultado = new DataTable();
            StringBuilder SqlCmd = new StringBuilder("select Nombre, Totales, Procesados, Pendientes from fn_IndicadoresGeneralesMesas (" + pIdFlujo.ToString() + ")");
            bd BD = new bd();
            resultado = BD.leeDatos(SqlCmd.ToString());
            return resultado;
        }

        public DataTable IndicadoresGeneralesMesasApoyo(int pIdFlujo)
        {
            DataTable resultado = new DataTable();
            StringBuilder SqlCmd = new StringBuilder("select Nombre, Totales, Procesados, Pendientes from fn_IndicadoresGeneralesMesasApoyo(" + pIdFlujo.ToString() + ")");
            bd BD = new bd();
            resultado = BD.leeDatos(SqlCmd.ToString());
            return resultado;
        }

        public List<indicadorMesaApoyo> LstIndicadoresGeneralesMesasApoyo(int pIdFlujo)
        {
            List<indicadorMesaApoyo> resultado = new List<indicadorMesaApoyo>();
            StringBuilder SqlCmd = new StringBuilder("select Nombre, Totales, Procesados, Pendientes from fn_IndicadoresGeneralesMesasApoyo(" + pIdFlujo.ToString() + ")");
            //StringBuilder SqlCmd = new StringBuilder("select Nombre, Totales, Procesados, Pendientes from fn_IndicadoresGeneralesMesasApoyo(1)");
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma_indicadorMesaApoyo(registro));
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private indicadorMesaApoyo arma_indicadorMesaApoyo(DataRow pRegistro)
        {
            indicadorMesaApoyo resultado = new indicadorMesaApoyo();
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Pendientes")) resultado.Valor = Convert.ToInt32(pRegistro["Pendientes"]);
            return resultado;
        }

        public int IndicadorGeneralEfectividadFlujo(int pIdflujo, int pIdMesaControl, int pIdMesSalida)
        {
            int resultado = 0;
            bd BD = new bd();
            resultado = BD.ejecutaCmdScalar("select dbo.fn_IndicadorGeneralEfectividadFlujo (" + pIdflujo.ToString() + "," + pIdMesaControl.ToString() + "," + pIdMesSalida.ToString() + ")");
            BD.cierraBD();
            return resultado;
        }

        public DataTable IndicadoresGeneralesPromotoria(int pIdPromotoria)
        {
            DataTable resultado = new DataTable();
            StringBuilder SqlCmd = new StringBuilder("select * from fn_IndicadoresGeneralesPromotoria (" + pIdPromotoria.ToString() + ")");
            bd BD = new bd();
            resultado = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public int IndicadorGeneralEfectividadFlujoPromotoria(int pIdflujo, int pIdPromotoria,  int pIdMesaControl, int pIdMesSalida)
        {
            int resultado = 0;
            bd BD = new bd();
            resultado = BD.ejecutaCmdScalar("select dbo.fn_IndicadorGeneralEfectividadFlujo (" + pIdflujo.ToString() + "," + pIdMesaControl.ToString() + "," + pIdMesSalida.ToString() + ")");
            BD.cierraBD();
            return resultado;
        }
    }

    public class indicadorMesaApoyo
    {
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mValor = 0;
        public int Valor { get { return mValor; } set { mValor = value; } }
    }
}
