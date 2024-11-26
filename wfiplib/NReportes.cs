using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class NReportes
    {
        public DataTable UltimoEstatusTramite(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00,00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;
            bd uEstatusTramite = new bd();
            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            string qUEstatus = "SELECT  tramite.Id,tramite.FechaRegistro,Folio.FolioCompuesto,tramite.NumeroOrden,tipoTramite.Nombre AS Flujo,"+
                               " cat_producto.Nombre,tramite.DatosHtml,tramite.FechaSolicitud,statusTramite.Nombre as EstadoNombre,tramite.IdSisLegados,tramite.kwik" +
                               " FROM bitacora" +
                               " INNER JOIN statusMesa ON bitacora.Estado = statusMesa.Id" +
                               " INNER JOIN tramite ON bitacora.IdTramite = tramite.Id" +
                               " INNER JOIN Folio ON Folio.IdTramite = tramite.Id" +
                               " INNER JOIN Producto ON Producto.IdTramite = tramite.Id" +
                               " INNER JOIN cat_producto ON cat_producto.IdCatProducto = Producto.IdCatProducto" +
                               " INNER JOIN statusTramite ON statusTramite.Id = tramite.Estado" +
                               " INNER JOIN tipoTramite ON tipoTramite.Id = tramite.IdTipoTramite" +
                               " WHERE statusMesa.Nombre NOT IN('Pausa', 'Send To Mesa')" +
                               " AND bitacora.IdMesa NOT IN(-1)" +
                               " AND bitacora.FechaTermino >= CONVERT(DATETIME,'" + fechaD + "',102)" +
                               " AND bitacora.FechaTermino <= CONVERT(DATETIME,'" + fechaH + "',102)" +
                               " GROUP BY tramite.Id,tramite.FechaRegistro,Folio.FolioCompuesto, tramite.NumeroOrden,tipoTramite.Nombre,cat_producto.Nombre," +
                               " tramite.DatosHtml, tramite.FechaSolicitud,statusTramite.Nombre,tramite.IdSisLegados,tramite.kwik";

            dt = uEstatusTramite.leeDatos(qUEstatus);
            uEstatusTramite.cierraBD();
            return dt;
        }

        public DataTable InformacionTramiteBitacora(int IdTramite)
        {
            DataTable dt = new DataTable();
            bd uEstatusTramite = new bd();
            string qUEstatus = "SELECT * FROM ( " +
                                    "SELECT bitacora.IdTramite, bitacora.IdMesa,mesa.Nombre, tramiteMesa.FechaRegistro, " +
                                    "bitacora.FechaInicio, bitacora.FechaTermino,statusMesa.Nombre AS EstadoMesa, bitacora.Observacion, usuarios.Nombre AS NombreUsuario " +
                                    "FROM mesa " +
                                    "INNER JOIN bitacora ON bitacora.IdMesa = mesa.Id " +
                                    "INNER JOIN statusMesa ON statusMesa.Id = bitacora.Estado " +
                                    "INNER JOIN usuarios ON usuarios.id = bitacora.IdUsuario " +
                                    "INNER JOIN tramiteMesa ON bitacora.IdMesa = tramiteMesa.IdMesa AND bitacora.IdTramite = tramiteMesa.IdTramite " +
                                    "WHERE bitacora.IdTramite = " + IdTramite  +
                                    "AND statusMesa.Nombre NOT IN('Pausa', 'Send To Mesa') " +
                               ") AS MesaBitacora " +
                               "FULL OUTER JOIN( " +
                                   "SELECT distinct mesa.Nombre AS NMESA, mesa.OrdenReportes AS NORDENREPORTE FROM mesa " +
                               ") AS NombreMesa ON NombreMesa.NMESA = MesaBitacora.Nombre " +
                               "WHERE NMESA NOT IN('COBRANZA')  ORDER BY NORDENREPORTE ASC";

            dt = uEstatusTramite.leeDatos(qUEstatus);
            uEstatusTramite.cierraBD();
            return dt;
        }

        public DataTable InformacionTramiteBitacoraPrivada(int IdTramite)
        {
            DataTable dt = new DataTable();
            bd uEstatusTramite = new bd();
            string qUEstatus = "SELECT * FROM ( " +
                                    "SELECT bitacora.IdTramite, bitacora.IdMesa,mesa.Nombre, tramiteMesa.FechaRegistro, " +
                                    "bitacora.FechaInicio, bitacora.FechaTermino,statusMesa.Nombre AS EstadoMesa,bitacora.Observacion, bitacora.ObservacionPrivada, usuarios.Nombre AS NombreUsuario " +
                                    "FROM mesa " +
                                    "INNER JOIN bitacora ON bitacora.IdMesa = mesa.Id " +
                                    "INNER JOIN statusMesa ON statusMesa.Id = bitacora.Estado " +
                                    "INNER JOIN usuarios ON usuarios.id = bitacora.IdUsuario " +
                                    "INNER JOIN tramiteMesa ON bitacora.IdMesa = tramiteMesa.IdMesa AND bitacora.IdTramite = tramiteMesa.IdTramite " +
                                    "WHERE bitacora.IdTramite = " + IdTramite +
                                    "AND statusMesa.Nombre NOT IN('Pausa', 'Send To Mesa') " +
                               ") AS MesaBitacora " +
                               "FULL OUTER JOIN( " +
                                   "SELECT distinct mesa.Nombre AS NMESA, mesa.OrdenReportes AS NORDENREPORTE FROM mesa " +
                               ") AS NombreMesa ON NombreMesa.NMESA = MesaBitacora.Nombre " +
                               "WHERE NMESA NOT IN('COBRANZA')  ORDER BY NORDENREPORTE ASC";

            dt = uEstatusTramite.leeDatos(qUEstatus);
            uEstatusTramite.cierraBD();
            return dt;
        }


        public DataSet Sabana(DateTime fechaDesde, DateTime fechaHasta, int IdUsuario)
        {
            DataSet dt = new DataSet();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;
            bd uEstatusTramite = new bd();
            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);


            string qExportarExcel = "DECLARE @FECHAINI  DATETIME;" +
               "DECLARE @FECHAFIN  DATETIME;" +
               "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
               "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
               "EXECUTE SabanaReporte  @FECHAINI, @FECHAFIN," + IdUsuario.ToString();

            dt = uEstatusTramite.leeDatosDataSet(qExportarExcel);
            uEstatusTramite.cierraBD();
            return dt;
        }

        public DataSet SabanaPrivada(DateTime fechaDesde, DateTime fechaHasta, int IdUsuario)
        {
            DataSet dt = new DataSet();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;
            bd uEstatusTramite = new bd();
            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);


            string qExportarExcel = "DECLARE @FECHAINI  DATETIME;" +
               "DECLARE @FECHAFIN  DATETIME;" +
               "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
               "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
               "EXECUTE SabanaReportePrivada  @FECHAINI, @FECHAFIN," + IdUsuario.ToString();

            dt = uEstatusTramite.leeDatosDataSet(qExportarExcel);
            uEstatusTramite.cierraBD();
            return dt;
        }

        public DataSet SabanaReporteV2(DateTime fechaDesde, DateTime fechaHasta, int IdUsuario)
        {
            DataSet dt = new DataSet();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;
            bd uEstatusTramite = new bd();
            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);


            string qExportarExcel = "DECLARE @FECHAINI  DATETIME;" +
               "DECLARE @FECHAFIN  DATETIME;" +
               "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
               "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
               "EXECUTE SabanaReporteV2  @FECHAINI, @FECHAFIN," + IdUsuario.ToString();

            dt = uEstatusTramite.leeDatosDataSet(qExportarExcel);
            uEstatusTramite.cierraBD();
            return dt;
        }


        public DataTable SabanaConsultaBitacoraDescarga()
        {
            DataTable dt = new DataTable();
            bd ExportarExcel = new bd();
            string qExportarExcel = "EXEC SabanaConsultaBitacoraDescarga";
            dt = ExportarExcel.leeDatos(qExportarExcel);
            ExportarExcel.cierraBD();
            return dt;
        }
    }
}
