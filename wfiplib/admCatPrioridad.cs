using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admCatPrioridad
    {
        BaseDeDatos b = new BaseDeDatos();
        public DataTable ListaPrioridades()
        {
            DataTable dt = new DataTable();
            bd data = new bd();
            string q = "EXEC Seleccionar_Prioridades ";
            dt = data.leeDatos(q);
            data.cierraBD();
            return dt;
        }

        public DataTable ListaFlujos(int usuarioId)
        {
            DataTable dt = new DataTable();
            bd data = new bd();
            string q = "EXEC Seleccionar_flujos " + usuarioId.ToString() + ";";
            dt = data.leeDatos(q);
            data.cierraBD();
            return dt;
        }

        public DataTable CapturaPromotoriaAdm(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            DataTable dt = new DataTable();
            bd data = new bd();
            string q = "DECLARE @fechaF  DATETIME; " +
                "DECLARE @fechaI  DATETIME; " +
                "SET @fechaI = TRY_CONVERT(DATETIME, '" + fechaD + "',121); " +
                "SET @fechaF = TRY_CONVERT(DATETIME, '" + fechaH + "',121); " +
                "EXECUTE Reporte_PromotoriaCaptura @fechaI, @fechaF; ";
            dt = data.leeDatos(q);
            data.cierraBD();
            return dt;
        }

        public DataTable TramitesPrioridad(DateTime fechaDesde, DateTime fechaHasta, int Idprioridad)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            DataTable dt = new DataTable();
            bd data = new bd();
            string q = "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "EXECUTE Prioridad_Reporte @FECHAINI, @FECHAFIN," + Idprioridad;
            dt = data.leeDatos(q);
            data.cierraBD();
            return dt;
        }

        public DataTable OneShot(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            DataTable dt = new DataTable();
            b.ExecuteCommandSP("OneShot_Reporte");
            b.AddParameter("@Inicio", fechaD, SqlDbType.DateTime);
            b.AddParameter("@Fin", fechaH, SqlDbType.DateTime);
            return b.Select();

        }

        public DataTable TramitesFactuacionKWIK(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            DataTable dt = new DataTable();
            b.ExecuteCommandSP("Tramites_FacturacionKWIK");
            b.AddParameter("@fechaI", fechaD, SqlDbType.DateTime);
            b.AddParameter("@fechaF", fechaH, SqlDbType.DateTime);
            return b.Select();

        }

    }
}
