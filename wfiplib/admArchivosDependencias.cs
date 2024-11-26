using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admArchivosDependencias
    {
        ArchivosDependenciasTablas adp = new ArchivosDependenciasTablas();
        
        //Procesos públicos
        public DataTable Seleccionar()
        {
            return adp.Seleccionar();
        }

        public DataTable SeleccionarTramitesPorUsuario(int usuario)
        {
            return adp.SeleccionarTramitesPorUsuario(usuario);
        }

        public DataTable SeleccionarTramitesPorUsuarioPorEstadoPorCobertura(int usuario, string estado, string cobertura)
        {
            return adp.SeleccionarTramitesPorUsuarioPorEstadoPorCobertura(usuario, estado, cobertura);
        }

        public DataTable SeleccionarPorEstadoCobertura(string estado, string cobertura)
        {
            return adp.SeleccionarPorEstadoCobertura(estado, cobertura);
        }

        public DataTable SeleccionarPorDependencia(int dependencia)
        {
            return adp.SeleccionarPorDependencia(dependencia);
        }

        public DataTable SeleccionarPorDependenciaEstadoCobertura(string estado, string cobertura, int dependencia)
        {
            return adp.SeleccionarPorDependenciaEstadoCobertura(estado, cobertura, dependencia);
        }

        public DataTable SeleccionarPorFolio(string folio)
        {
            return adp.SeleccionarPorFolio(folio);
        }

        public DataTable SeleccionarPrimerTramiteDisponibleParaAnalista(int cobertura)
        {
            return adp.SeleccionarPrimerTramiteDisponibleParaAnalista(cobertura);
        }

        public string SeleccionarTramiteYaAsignadoAAnalista(int idusuario, int estado)
        {
            return adp.SeleccionarTramiteYaAsignadoAAnalista(idusuario, estado);
        }

        public bool SeleccionarTramiteAsignadoUsuario(int idusuario)
        {
            if (adp.SeleccionarTramiteAsignadoUsuario(idusuario) == "Dato vacío" || adp.SeleccionarTramiteAsignadoUsuario(idusuario) == "")
                return false;
            else
                return true;
        }

        public bool SeleccionarTramiteParaTerminarAsignadoAUsuario(int idusuario, ref string folio, ref string cobertura, ref string estado)
        {
            if (adp.SeleccionarTramiteParaTerminarAsignadoAUsuario(idusuario, ref folio, ref cobertura, ref estado).Rows.Count > 0)
            {
                var obtenido = adp.SeleccionarTramiteParaTerminarAsignadoAUsuario(idusuario, ref folio, ref cobertura, ref estado).Rows[0];
                folio = obtenido[0].ToString();
                cobertura = obtenido[1].ToString();
                estado = obtenido[3].ToString();
                return true;
            }
            else
                return false;
        }

        public bool SeleccionarTramiteParaTerminarAsignadoAUsuarioPotenciacion(int idusuario, ref string folio, ref string cobertura, ref string estado)
        {
            if (adp.SeleccionarTramiteParaTerminarAsignadoAUsuarioPotenciacion(idusuario, ref folio, ref cobertura, ref estado).Rows.Count > 0)
            {
                var obtenido = adp.SeleccionarTramiteParaTerminarAsignadoAUsuarioPotenciacion(idusuario, ref folio, ref cobertura, ref estado).Rows[0];
                folio = obtenido[0].ToString();
                cobertura = obtenido[1].ToString();
                estado = obtenido[3].ToString();
                return true;
            }
            else
                return false;
        }

        public DataTable SeleccionarPrimerTramiteDisponibleParaEjecutivoBasica()
        {
            return adp.SeleccionarPrimerTramiteDisponibleParaEjecutivoBasica();
        }

        public DataTable SeleccionarPrimerTramiteDisponibleParaAnalistaFrontPotenciacion()
        {
            return adp.SeleccionarPrimerTramiteDisponibleParaAnalistaFrontPotenciacion();
        }

        public DataTable SeleccionarTramiteDevueltoParaRevision()
        {
            return adp.SeleccionarTramiteDevueltoParaRevision();
        }

        public DataTable SeleccionarPrimerTramiteDisponibleParaAnalistaBackPotenciacion()
        {
            return adp.SeleccionarPrimerTramiteDisponibleParaAnalistaBackPotenciacion();
        }

        public bool SeleccionarUltimoEstado(string folio)
        {
            if (adp.SeleccionarUltimoEstado(folio) == "4" || adp.SeleccionarUltimoEstado(folio) == "5")
                return true;
            else
                return false;
        }

        public bool SeleccionarDetalle(string folio, ref DataRow datarow)
        {
            if (adp.SeleccionarDetalle(folio).Rows.Count > 0)
            {
                datarow = adp.SeleccionarDetalle(folio).Rows[0];
                return true;
            }
            else
                return false;
        }

        public DataTable SeleccionarEstadoArchivos()
        {
            return adp.SeleccionarEstadoArchivos();
        }

        public DataTable SeleccionarPorCoberturaBasicaSupervisor()
        {
            return adp.SeleccionarPorCoberturaBasicaSupervisor();
        }

        public DataTable SeleccionarPorCoberturaPotenciadaSupervisor()
        {
            return adp.SeleccionarPorCoberturaPotenciadaSupervisor();
        }

        public DataTable SeleccionarPorCoberturaBasicaDependencia(int dependencia)
        {
            return adp.SeleccionarPorCoberturaBasicaDependencia(dependencia);          
        }

        public DataTable SeleccionarPorCoberturaPotenciadaDependencia(int dependencia)
        {
            return adp.SeleccionarPorCoberturaPotenciadaDependencia(dependencia);
        }

        public DataTable SeleccionarPorCoberturaBasicaUsuario(int usuario)
        {
            return adp.SeleccionarPorCoberturaBasicaUsuario(usuario);
        }

        public DataTable SeleccionarPorCoberturaPotenciadaUsuario(int usuario)
        {
            return adp.SeleccionarPorCoberturaPotenciadaUsuario(usuario);
        }

        public DataTable SeleccionarArchivosDeTramite(string folio)
        {
            return adp.SeleccionarArchivosDeTramite(folio);
        }

        public int AgregarBasica(string nombre, string folio, string poliza, string cobertura, string subidopor, string correo, string asunto, string trimestre, string ann)
        {
            return adp.AgregarBasica(nombre, folio, poliza, cobertura, subidopor, correo, asunto, trimestre, ann);
        }

        public int AgregarPotenciacion(string nombre, string folio, string poliza, string cobertura, string subidopor, string correo, string asunto, string quincena, string ann2)
        {
            return adp.AgregarPotenciacion(nombre, folio, poliza, cobertura, subidopor, correo, asunto, quincena, ann2);
        }

        public int AgregarUsuarioAsignadoATramite(string usuario, string folio)
        {
            return adp.AgregarUsuarioAsignadoATramite(usuario, folio);
        }

        public int AgregarDependenciaATramite(int rol, string folio)
        {
            return adp.AgregarDependenciaATramite(rol, folio);
        }

        public int AgregarEstado(string folio, int estado)
        {
            return adp.AgregarEstado(folio, estado);
        }

        public int AgregarPDF(string archivopdf, string folio)
        {
            return adp.AgregarPDF(archivopdf, folio);
        }

        public int AgregarArchivosPotenciacion(string folio, string archivos)
        {
            return adp.AgregarArchivosPotenciacion(folio, archivos);
        }

        public int AgregarFolioMovimiento(string folio2, string movimiento, string folio)
        {
            return adp.AgregarFolioMovimiento(folio2, movimiento, folio);
        }

        public int AgregarDocumento(string archivo, string folio)
        {
            return adp.AgregarDocumento(archivo, folio);
        }

        public int AgregarCartaPDF(string archivopdf, string folio)
        {
            return adp.AgregarCartaPDF(archivopdf, folio);
        }

        public int AgregarXML(string archivoxls, string folio)
        {
            return adp.AgregarXML(archivoxls, folio);
        }

        public int Agregar100PosicionesPagos(string archivo, string folio)
        {
            return adp.Agregar100PosicionesPagos(archivo, folio);
        }

        public int Agregar100PosicionesCancelaciones(string archivo, string folio)
        {
            return adp.Agregar100PosicionesCancelaciones(archivo, folio);
        }

        public int ActualizarAsunto(string folio, string asunto)
        {
            return adp.ActualizarAsunto(folio, asunto);
        }

        public int ActualizarErrores(string folio, string errores)
        {
            return adp.ActualizarErrores(folio, errores);
        }

        public int ActualizarErrores2(string folio, string errores)
        {
            return adp.ActualizarErrores2(folio, errores);
        }

        public int ActualizarTerminacionProcesoExitoso(string folio)
        {
            return adp.ActualizarTerminacionProcesoExitoso(folio);
        }

        public int ActualizarReasignarTramiteAOtroUsuario(string folio)
        {
            return adp.ActualizarReasignarTramiteAOtroUsuario(folio);
        }

        [Obsolete ("No se usa por el momento, buscar utilidad práctica para el futuro")]
        public int Eliminar(string folio)
        {
            return adp.Eliminar(folio);
        }


        internal class ArchivosDependenciasTablas
        {
#pragma warning disable CS0169 // El campo 'admArchivosDependencias.ArchivosDependenciasTablas._parent' nunca se usa
            admArchivosDependencias _parent;
#pragma warning restore CS0169 // El campo 'admArchivosDependencias.ArchivosDependenciasTablas._parent' nunca se usa

            BaseDeDatos b = new BaseDeDatos();

            /// <summary>
            /// Obtiene todos los tramites para ser visualizados por el supervisor
            /// </summary>
            /// <returns></returns>
            public DataTable Seleccionar()
            {
                    string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
                    "a.Poliza, CASE a.Cobertura " +
                    "WHEN 1 THEN 'Básica' " +
                    "WHEN 2 THEN 'Potenciada' " +
                    "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF " +
                    "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                    "ON a.Folio = b.Folio " +
                    "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            /// <summary>
            /// Obtiene los trámites asignados por usuario
            /// </summary>
            /// <param name="usuario"></param>
            /// <returns></returns>
            public DataTable SeleccionarTramitesPorUsuario(int usuario)
            {
                string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, a.Poliza, " +
                "CASE a.Cobertura " +
                "WHEN 1 THEN 'Básica' " +
                "WHEN 2 THEN 'Potenciada' " +
                "END AS Cobertura, " +
                "a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF " +
                "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                "ON a.Folio = b.Folio " +
                "WHERE a.UsuarioAsignado=@usuario " +
                "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, " +
                "a.Archivo100PosCanc, a.Documento, a.CartaPDF";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@usuario", usuario, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarTramitesPorUsuarioPorEstadoPorCobertura(int usuario, string estado, string cobertura)
            {
                string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
                "a.Poliza, CASE a.Cobertura " +
                "WHEN 1 THEN 'Básica' " +
                "WHEN 2 THEN 'Potenciada' " +
                "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF " +
                "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                "ON a.Folio = b.Folio " +
                "WHERE a.UsuarioAsignado=@usuario " +
                "AND a.Estado=@estado " +
                "AND a.Cobertura=@cobertura " +
                "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@usuario", usuario, SqlDbType.Int);
                b.AddParameter("@estado", estado, SqlDbType.NVarChar);
                b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar);
                return b.Select();
            }


            /// <summary>
            /// Obtiene los trámites por estado y cobertura
            /// </summary>
            /// <param name="estado"></param>
            /// <param name="cobertura"></param>
            /// <returns></returns>
            public DataTable SeleccionarPorEstadoCobertura(string estado, string cobertura)
            {
                string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
                "a.Poliza, CASE a.Cobertura " +
                "WHEN 1 THEN 'Básica' " +
                "WHEN 2 THEN 'Potenciada' " +
                "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF " +
                "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                "ON a.Folio = b.Folio " +
                "WHERE a.Estado=@estado " +
                "AND a.Cobertura=@cobertura " +
                "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@estado", estado, SqlDbType.NVarChar);
                b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar);
                return b.Select();
            }

            public DataTable SeleccionarPorDependencia(int dependencia)
            {
                string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
                "a.Poliza, CASE a.Cobertura " +
                "WHEN 1 THEN 'Básica' " +
                "WHEN 2 THEN 'Potenciada' " +
                "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF, a.OpDep " +
                "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                "ON a.Folio = b.Folio " +
                "WHERE a.OpDep=@dependencia " +
                "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF, a.OpDep";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@dependencia", dependencia, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarPorDependenciaEstadoCobertura(string estado, string cobertura, int dependencia)
            {
                string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
                "a.Poliza, CASE a.Cobertura " +
                "WHEN 1 THEN 'Básica' " +
                "WHEN 2 THEN 'Potenciada' " +
                "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF, a.OpDep " +
                "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                "ON a.Folio = b.Folio " +
                "WHERE a.Estado=@estado " +
                "AND a.Cobertura=@cobertura " +
                "AND a.OpDep=@dependencia " +
                "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto, a.Errores, a.ArchivoPDF, a.ArchivoXLS, a.Archivo100PosPagos, a.Archivo100PosCanc, a.Documento, a.CartaPDF, a.OpDep";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@estado", estado, SqlDbType.NVarChar);
                b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar);
                b.AddParameter("@dependencia", dependencia, SqlDbType.Int);
                return b.Select();
            }



            public DataTable SeleccionarPorFolio(string folio)
            {
                string consulta = "SELECT a.Nombre, a.Fecha, a.Folio, MAX(b.Estado) AS Estado, " +
                "a.Poliza, CASE a.Cobertura " +
                "WHEN 1 THEN 'Básica' " +
                "WHEN 2 THEN 'Potenciada' " +
                "END AS Cobertura, a.SubidoPor, a.Correo, a.Asunto " +
                "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                "ON a.Folio = b.Folio " +
                "WHERE a.folio=@folio " +
                "GROUP BY a.Nombre, a.Fecha, a.Folio, a.Poliza, a.Cobertura, a.SubidoPor, a.Correo, a.Asunto";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar);
                return b.Select();
            }

            public DataTable SeleccionarPrimerTramiteDisponibleParaAnalista(int cobertura)
            {
                string consulta = "SELECT TOP 1 a.Folio, MAX(b.Estado) AS Estado " +
                "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                "ON a.Folio = b.Folio " +
                "WHERE b.Estado=1 AND a.Folio NOT IN(SELECT Folio FROM ArchivosDependenciasAsignados) " +
                "AND a.Cobertura=@cobertura " +
                "GROUP BY a.folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@cobertura", cobertura, SqlDbType.Int);
                return b.Select();
            }

            public string SeleccionarTramiteYaAsignadoAAnalista(int idusuario, int estado)
            {
                string consulta = "SELECT a.Folio " +
                "FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b ON a.Folio=b.Folio " +
                "LEFT JOIN ArchivosDependenciasAsignados c ON a.Folio = c.Folio " +
                "WHERE a.Folio NOT IN(SELECT Folio FROM ArchivosDependenciasEstados WHERE Estado=@estado) " +
                "AND c.IdUsuario=@idusuario " +
                "GROUP BY a.Folio ";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
                b.AddParameter("@estado", estado, SqlDbType.Int);
                return b.SelectString();
            }

            public string SeleccionarTramiteAsignadoUsuario(int idusuario)
            {
                string consulta = "SELECT UsuarioAsignado FROM ArchivosDependencias WHERE Estado=1";
                //"SELECT c.IdUsuario " +
                //"FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b ON a.Folio = b.Folio " +
                //"LEFT JOIN ArchivosDependenciasAsignados c ON a.Folio = c.Folio " +
                //"WHERE a.Estado=1 " +
                //"WHERE a.Folio NOT IN(SELECT Folio FROM ArchivosDependenciasEstados WHERE Estado=1 OR Estado=3 OR Estado=4 OR Estado=6) " +
                //"GROUP BY c.IdUsuario";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
                return b.SelectString();
            }

            public DataTable SeleccionarTramiteParaTerminarAsignadoAUsuario(int idusuario, ref string folio, ref string cobertura, ref string estado)
            {
                string consulta = "SELECT Folio, Cobertura, UsuarioAsignado, Estado " +
                "FROM ArchivosDependencias " +
                "WHERE Estado=5 OR Estado=4" +
                "AND UsuarioAsignado=@idusuario " +
                "AND Cobertura=1";
                //"SELECT TOP 1 a.Folio, a.Cobertura, c.IdUsuario, a.Estado " +
                //"FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                //"ON a.Folio = b.Folio " +
                //"LEFT JOIN ArchivosDependenciasAsignados c ON b.Folio = c.Folio " +
                //"WHERE a.Estado=5 Or a.Estado=7 AND c.idusuario=@idusuario ";
                //"GROUP BY a.folio, a.Cobertura, c.IdUsuario";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarTramiteParaTerminarAsignadoAUsuarioPotenciacion(int idusuario, ref string folio, ref string cobertura, ref string estado)
            {
                string consulta = "SELECT Folio, Cobertura, UsuarioAsignado, Estado " +
                "FROM ArchivosDependencias " +
                "WHERE Estado=4 OR Estado=7 " +
                "AND UsuarioAsignado=@idusuario " +
                "AND Cobertura=2";
                //"SELECT TOP 1 a.Folio, a.Cobertura, c.IdUsuario, a.Estado " +
                //"FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                //"ON a.Folio = b.Folio " +
                //"LEFT JOIN ArchivosDependenciasAsignados c ON b.Folio = c.Folio " +
                //"WHERE a.Estado=5 Or a.Estado=7 AND c.idusuario=@idusuario ";
                //"GROUP BY a.folio, a.Cobertura, c.IdUsuario";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@idusuario", idusuario, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarPrimerTramiteDisponibleParaEjecutivoBasica()
            {
                string consulta = "SELECT Folio, Estado From ArchivosDependencias WHERE Estado=3";
                //string consulta = "SELECT TOP 1 a.Folio, MAX(b.Estado) AS Estado " +
                //"FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                //"ON a.Folio=b.Folio " +
                //"WHERE b.Estado=3 " +
                //"GROUP BY a.folio";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarPrimerTramiteDisponibleParaAnalistaFrontPotenciacion()
            {
                string consulta = "SELECT Folio, Estado From ArchivosDependencias WHERE Estado=3 AND Cobertura=2";
                //string consulta = "SELECT TOP 1 a.Folio, MAX(b.Estado) AS Estado " +
                //"FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                //"ON a.Folio=b.Folio " +
                //"WHERE b.Estado=3 " +
                //"GROUP BY a.folio";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarTramiteDevueltoParaRevision()
            {
                string consulta = "SELECT Folio, Estado FROM ArchivosDependencias WHERE estado=6 AND cobertura=2";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarPrimerTramiteDisponibleParaAnalistaBackPotenciacion()
            {
                string consulta = "SELECT Folio, Estado From ArchivosDependencias WHERE Estado=5 OR Estado=7 AND Cobertura=2";
                //string consulta = "SELECT TOP 1 a.Folio, MAX(b.Estado) AS Estado " +
                //"FROM ArchivosDependencias a LEFT JOIN ArchivosDependenciasEstados b " +
                //"ON a.Folio=b.Folio " +
                //"WHERE b.Estado=3 " +
                //"GROUP BY a.folio";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            
            /// <summary>
            /// Verifica el estado de un trámite
            /// </summary>
            /// <param name="folio">Folio del trámite</param>
            /// <returns>6 si el estado del támite ha terminado (para básica)</returns>
            public string SeleccionarUltimoEstado(string folio)
            {
                //string consulta = "SELECT MAX(Estado) FROM ArchivosDependenciasEstados WHERE folio=@folio";
                string consulta = "SELECT Estado FROM ArchivosDependencias WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar);
                return b.SelectString();
            }

            public DataTable SeleccionarDetalle(string folio)
            {
                string consulta = "SELECT * FROM archivosdependencias WHERE Folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar);
                return b.Select();
            }

            public DataTable SeleccionarEstadoArchivos()
            {
                DataTable dt = new DataTable();
                try
                {
                    string consulta = "CREATE TABLE #EstadosTemporales " +
                    "( " +
                    "    Folio NVARCHAR(50) NULL, " +
                    "    Estado INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "CREATE TABLE #EstadosTemporales2 " +
                    "( " +
                    "    Orden int," +
                    "    Estado NVARCHAR(50) NULL, " +
                    "    Totales INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales SELECT a.folio, MAX(a.Estado) AS Estado, b.Cobertura FROM ArchivosDependenciasEstados a, ArchivosDependencias b WHERE a.Folio=b.Folio GROUP BY a.Folio, b.Cobertura; " +

                    "INSERT INTO #EstadosTemporales2 (Orden, Estado, Totales, Cobertura)  " +
                    "SELECT " +
                    "   CASE Estado " +
                    "       WHEN 1 THEN 1 " +
                    "       WHEN 2 THEN 2 " +
                    "       WHEN 3 THEN 3 " +
                    "       WHEN 4 THEN 4 " +
                    "       WHEN 5 THEN 5 " +
                    "       WHEN 6 THEN 6 " +
                    "   END AS Orden, " +
                    "   CASE Estado " +
                    "       WHEN 1 THEN 'En Trámite' " +
                    "       WHEN 2 THEN 'Suspendido' " +
                    "       WHEN 3 THEN 'En Proceso' " +
                    "       WHEN 4 THEN 'Reenvío Trámite' " +
                    "       WHEN 5 THEN 'En Revisión' " +
                    "       WHEN 6 THEN 'Concluído' " +
                    "   END AS Estado,  " +
                    "COUNT(Estado) AS Totales, Cobertura " +
                    "FROM #EstadosTemporales WHERE Cobertura=1 GROUP BY Estado, Cobertura " +

                    "INSERT INTO #EstadosTemporales2 (Orden, Estado, Totales, Cobertura) " +
                    "SELECT " +
                    "    CASE Estado " +
                    "        WHEN 1 THEN 1 " +
                    "        WHEN 2 THEN 2 " +
                    "        WHEN 3 THEN 3 " +
                    "        WHEN 4 THEN 4 " +
                    "        WHEN 5 THEN 5 " +
                    "        WHEN 6 THEN 6 " +
                    "        WHEN 7 THEN 7 " +
                    "        WHEN 8 THEN 8 " +
                    "    END AS Orden, " +
                    "	CASE Estado " +
                    "        WHEN 1 THEN 'En Trámite' " +
                    "        WHEN 2 THEN 'Suspendido' " +
                    "        WHEN 3 THEN 'En Proceso' " +
                    "        WHEN 4 THEN 'Reenvío Trámite' " +
                    "        WHEN 5 THEN 'En Revisión' " +
                    "        WHEN 6 THEN 'Incompleto' " +
                    "        WHEN 7 THEN 'Carta' " +
                    "        WHEN 8 THEN 'Concluído' " +
                    "    END AS Estado,  " +
                    "COUNT(Estado) AS Totales, Cobertura " +
                    "FROM #EstadosTemporales " +
                    "WHERE Cobertura=2 " +
                    "GROUP BY Estado, Cobertura " +

                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Trámite' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Trámite', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Suspendido' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'Suspendido', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Proceso' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Proceso', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Reenvío Trámite' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(4, 'Reenvío Trámite', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Revisión' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(5, 'En Revisión', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Concluído' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(6, 'Concluído', 0, 1) " +

                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Trámite' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Trámite', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Suspendido' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'Suspendido', 0, 2)  " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Proceso' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Proceso', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Reenvío Trámite' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(4, 'Reenvío Trámite', 0, 2)  " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Revisión' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(5, 'En Revisión', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Incompleto' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(6, 'Incompleto', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Carta' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(7, 'Carta', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Concluído' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(8, 'Concluído', 0, 2) " +

                    "SELECT * FROM #EstadosTemporales2 WHERE Cobertura=1 ORDER BY Orden";
                    b.ExecuteCommandQuery(consulta);

                    b.ConnectionOpenToTransaction();
                    b.BeginTransaction();

                    dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                    b.CommitTransaction();

                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    b.RollBackTransaction();
                }
                finally
                {
                    b.ConnectionCloseToTransaction();
                }

                return dt;
            }




            public DataTable SeleccionarPorCoberturaBasicaSupervisor()
            {
                DataTable dt = new DataTable();
                try
                {
                    string consulta = "CREATE TABLE #EstadosTemporales " +
                    "( " +
                    "    Folio NVARCHAR(50) NULL, " +
                    "    Estado INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "CREATE TABLE #EstadosTemporales2 " +
                    "( " +
                    "    Orden int," +
                    "    Estado NVARCHAR(50) NULL, " +
                    "    Totales INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales SELECT a.folio, MAX(a.Estado) AS Estado, b.Cobertura FROM ArchivosDependenciasEstados a, ArchivosDependencias b WHERE a.Folio=b.Folio GROUP BY a.Folio, b.Cobertura; " +

                    "INSERT INTO #EstadosTemporales2 (Orden, Estado, Totales, Cobertura)  " +
                    "SELECT " +
                    "   CASE Estado " +
                    "       WHEN 1 THEN 1 " +
                    "       WHEN 2 THEN 2 " +
                    "       WHEN 3 THEN 3 " +
                    "       WHEN 4 THEN 4 " +
                    "       WHEN 5 THEN 5 " +
                    "       WHEN 6 THEN 6 " +
                    "   END AS Orden, " +
                    "   CASE Estado " +
                    "       WHEN 1 THEN 'En Trámite' " +
                    "        WHEN 2 THEN 'Suspendido' " +
                    "       WHEN 3 THEN 'En Proceso' " +
                    "       WHEN 4 THEN 'Reenvío Trámite' " +
                    "       WHEN 5 THEN 'En Revisión' " +
                    "       WHEN 6 THEN 'Concluído' " +
                    "   END AS Estado,  " +
                    "COUNT(Estado) AS Totales, Cobertura " +
                    "FROM #EstadosTemporales WHERE Cobertura=1 GROUP BY Estado, Cobertura " +

                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Trámite' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Trámite', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Suspendido' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'Suspendido', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Proceso' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Proceso', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Reenvío Trámite' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(4, 'Reenvío Trámite', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Revisión' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(5, 'En Revisión', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Concluído' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(6, 'Concluído', 0, 1) " +

                    "SELECT * FROM #EstadosTemporales2 WHERE Cobertura=1 ORDER BY Orden";
                    b.ExecuteCommandQuery(consulta);

                    b.ConnectionOpenToTransaction();
                    b.BeginTransaction();

                    dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                    b.CommitTransaction();

                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    b.RollBackTransaction();
                }
                finally
                {
                    b.ConnectionCloseToTransaction();
                }

                return dt;
            }

            public DataTable SeleccionarPorCoberturaPotenciadaSupervisor()
            {
                DataTable dt = new DataTable();
                try
                {
                    string consulta = "CREATE TABLE #EstadosTemporales " +
                    "( " +
                    "    Folio NVARCHAR(50) NULL, " +
                    "    Estado INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "CREATE TABLE #EstadosTemporales2 " +
                    "( " +
                    "    Orden int," +
                    "    Estado NVARCHAR(50) NULL, " +
                    "    Totales INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales SELECT a.folio, MAX(a.Estado) AS Estado, b.Cobertura FROM ArchivosDependenciasEstados a, ArchivosDependencias b WHERE a.Folio=b.Folio GROUP BY a.Folio, b.Cobertura; " +

                    "INSERT INTO #EstadosTemporales2 (Orden, Estado, Totales, Cobertura) " +
                    "SELECT " +
                    "    CASE Estado " +
                    "        WHEN 1 THEN 1 " +
                    "        WHEN 2 THEN 2 " +
                    "        WHEN 3 THEN 3 " +
                    "        WHEN 4 THEN 4 " +
                    "        WHEN 5 THEN 5 " +
                    "        WHEN 6 THEN 6 " +
                    "        WHEN 7 THEN 7 " +
                    "        WHEN 8 THEN 8 " +
                    "    END AS Orden, " +
                    "	CASE Estado " +
                    "        WHEN 1 THEN 'En Trámite' " +
                    "        WHEN 2 THEN 'Suspendido' " +
                    "        WHEN 3 THEN 'En Proceso' " +
                    "        WHEN 4 THEN 'Reenvío Trámite' " +
                    "        WHEN 5 THEN 'En Revisión' " +
                    "        WHEN 6 THEN 'Incompleto' " +
                    "        WHEN 7 THEN 'Carta' " +
                    "        WHEN 8 THEN 'Concluído' " +
                    "    END AS Estado,  " +
                    "COUNT(Estado) AS Totales, Cobertura " +
                    "FROM #EstadosTemporales " +
                    "WHERE Cobertura=2 " +
                    "GROUP BY Estado, Cobertura " +

                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Trámite' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Trámite', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Suspendido' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'Suspendido', 0, 2)  " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Proceso' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Proceso', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Reenvío Trámite' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(4, 'Reenvío Trámite', 0, 2)  " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Revisión' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(5, 'En Revisión', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Incompleto' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(6, 'Incompleto', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Carta' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(7, 'Carta', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Concluído' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(8, 'Concluído', 0, 2) " +

                    "SELECT * FROM #EstadosTemporales2 WHERE Cobertura=2 ORDER BY Orden";
                    b.ExecuteCommandQuery(consulta);

                    b.ConnectionOpenToTransaction();
                    b.BeginTransaction();

                    dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                    b.CommitTransaction();

                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    b.RollBackTransaction();
                }
                finally
                {
                    b.ConnectionCloseToTransaction();
                }

                return dt;
            }

            public DataTable SeleccionarPorCoberturaBasicaDependencia(int dependencia)
            {
                DataTable dt = new DataTable();
                try
                {
                    string consulta = "CREATE TABLE #EstadosTemporales " +
                    "( " +
                    "    Folio NVARCHAR(50) NULL, " +
                    "    Estado INT, " +
                    "    Cobertura INT, " +
                    "    Dependencia INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales SELECT a.folio, MAX(a.Estado) AS Estado, b.Cobertura, b.OpDep AS Dependencia FROM ArchivosDependenciasEstados a, ArchivosDependencias b WHERE a.Folio=b.Folio AND b.OpDep=@dep GROUP BY a.Folio, b.Cobertura, b.OpDep; " +

                    "CREATE TABLE #EstadosTemporales2 " +
                    "( " +
                    "    Orden int," +
                    "    Estado NVARCHAR(50) NULL, " +
                    "    Totales INT, " +
                    "    Cobertura INT, " +
                    "    Dependencia INT   " +
                    ") " +

                    "INSERT INTO #EstadosTemporales2 (Orden, Estado, Totales, Cobertura, Dependencia)  " +
                    "SELECT " +
                    "   CASE Estado " +
                    "       WHEN 1 THEN 1 " +
                    "       WHEN 2 THEN 2 " +
                    "       WHEN 3 THEN 3 " +
                    "       WHEN 4 THEN 4 " +
                    "       WHEN 5 THEN 5 " +
                    "       WHEN 6 THEN 6 " +
                    "   END AS Orden, " +
                    "   CASE Estado " +
                    "       WHEN 1 THEN 'En Trámite' " +
                    "        WHEN 2 THEN 'Suspendido' " +
                    "       WHEN 3 THEN 'En Proceso' " +
                    "       WHEN 4 THEN 'Reenvío Trámite' " +
                    "       WHEN 5 THEN 'En Revisión' " +
                    "       WHEN 6 THEN 'Concluído' " +
                    "   END AS Estado,  " +
                    "COUNT(Estado) AS Totales, Cobertura, Dependencia " +
                    "FROM #EstadosTemporales WHERE Cobertura=1 GROUP BY Estado, Cobertura, Dependencia " +

                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Trámite' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Trámite', 0, 1, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Suspendido' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'Suspendido', 0, 1, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Proceso' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Proceso', 0, 1, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Reenvío Trámite' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(4, 'Reenvío Trámite', 0, 1, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Revisión' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(5, 'En Revisión', 0, 1, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Concluído' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(6, 'Concluído', 0, 1, @dep) " +

                    "SELECT * FROM #EstadosTemporales2 WHERE Cobertura=1 AND Dependencia=@dep ORDER BY Orden";
                    b.ExecuteCommandQuery(consulta);
                    b.AddParameter("@dep", dependencia, SqlDbType.Int);

                    b.ConnectionOpenToTransaction();
                    b.BeginTransaction();

                    dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                    b.CommitTransaction();

                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    b.RollBackTransaction();
                }
                finally
                {
                    b.ConnectionCloseToTransaction();
                }

                return dt;
            }

            public DataTable SeleccionarPorCoberturaPotenciadaDependencia(int dependencia)
            {
                DataTable dt = new DataTable();
                try
                {
                    string consulta = "CREATE TABLE #EstadosTemporales " +
                    "( " +
                    "    Folio NVARCHAR(50) NULL, " +
                    "    Estado INT, " +
                    "    Cobertura INT, " +
                    "    Dependencia INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales SELECT a.folio, MAX(a.Estado) AS Estado, b.Cobertura, b.OpDep AS Dependencia FROM ArchivosDependenciasEstados a, ArchivosDependencias b WHERE a.Folio=b.Folio AND b.OpDep=@dep GROUP BY a.Folio, b.Cobertura, b.OpDep; " +

                    "CREATE TABLE #EstadosTemporales2 " +
                    "( " +
                    "    Orden int," +
                    "    Estado NVARCHAR(50) NULL, " +
                    "    Totales INT, " +
                    "    Cobertura INT, " +
                    "    Dependencia INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales2 (Orden, Estado, Totales, Cobertura, Dependencia) " +
                    "SELECT " +
                    "    CASE Estado " +
                    "        WHEN 1 THEN 1 " +
                    "        WHEN 2 THEN 2 " +
                    "        WHEN 3 THEN 3 " +
                    "        WHEN 4 THEN 4 " +
                    "        WHEN 5 THEN 5 " +
                    "        WHEN 6 THEN 6 " +
                    "        WHEN 7 THEN 7 " +
                    "        WHEN 8 THEN 8 " +
                    "    END AS Orden, " +
                    "	CASE Estado " +
                    "        WHEN 1 THEN 'En Trámite' " +
                    "        WHEN 2 THEN 'Suspendido' " +
                    "        WHEN 3 THEN 'En Proceso' " +
                    "        WHEN 4 THEN 'Reenvío Trámite' " +
                    "        WHEN 5 THEN 'En Revisión' " +
                    "        WHEN 6 THEN 'Incompleto' " +
                    "        WHEN 7 THEN 'Carta' " +
                    "        WHEN 8 THEN 'Concluído' " +
                    "    END AS Estado,  " +
                    "COUNT(Estado) AS Totales, Cobertura, Dependencia " +
                    "FROM #EstadosTemporales " +
                    "WHERE Cobertura=2 " +
                    "GROUP BY Estado, Cobertura, Dependencia " +

                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Trámite' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Trámite', 0, 2, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Suspendido' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'Suspendido', 0, 2, @dep)  " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Proceso' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Proceso', 0, 2, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Reenvío Trámite' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(4, 'Reenvío Trámite', 0, 2, @dep)  " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Revisión' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(5, 'En Revisión', 0, 2, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Incompleto' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(6, 'Incompleto', 0, 2, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Carta' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(7, 'Carta', 0, 2, @dep) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Concluído' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(8, 'Concluído', 0, 2, @dep) " +

                    "SELECT * FROM #EstadosTemporales2 WHERE Cobertura=2 AND Dependencia=@dep ORDER BY Orden";
                    b.ExecuteCommandQuery(consulta);
                    b.AddParameter("@dep", dependencia, SqlDbType.Int);

                    b.ConnectionOpenToTransaction();
                    b.BeginTransaction();

                    dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                    b.CommitTransaction();

                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    b.RollBackTransaction();
                }
                finally
                {
                    b.ConnectionCloseToTransaction();
                }

                return dt;
            }

            public DataTable SeleccionarPorCoberturaBasicaUsuario(int usuario)
            {
                DataTable dt = new DataTable();
                try
                {
                    string consulta = "CREATE TABLE #EstadosTemporales " +
                    "( " +
                    "    Folio NVARCHAR(50) NULL, " +
                    "    Estado INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales SELECT a.folio, MAX(a.Estado) AS Estado, b.Cobertura FROM ArchivosDependenciasEstados a, ArchivosDependencias b WHERE a.Folio=b.Folio AND b.Usuarioasignado=@usuario GROUP BY a.Folio, b.Cobertura; " +

                    "CREATE TABLE #EstadosTemporales2 " +
                    "( " +
                    "    Orden int," +
                    "    Estado NVARCHAR(50) NULL, " +
                    "    Totales INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales2 (Orden, Estado, Totales, Cobertura)  " +
                    "SELECT " +
                    "   CASE Estado " +
                    "       WHEN 1 THEN 1 " +
                    "       WHEN 2 THEN 2 " +
                    "       WHEN 3 THEN 3 " +
                    "       WHEN 4 THEN 4 " +
                    "       WHEN 5 THEN 5 " +
                    "       WHEN 6 THEN 6 " +
                    "   END AS Orden, " +
                    "   CASE Estado " +
                    "       WHEN 1 THEN 'En Trámite' " +
                    "        WHEN 2 THEN 'Suspendido' " +
                    "       WHEN 3 THEN 'En Proceso' " +
                    "       WHEN 4 THEN 'Reenvío Trámite' " +
                    "       WHEN 5 THEN 'En Revisión' " +
                    "       WHEN 6 THEN 'Concluído' " +
                    "   END AS Estado,  " +
                    "COUNT(Estado) AS Totales, Cobertura " +
                    "FROM #EstadosTemporales WHERE Cobertura=1 GROUP BY Estado, Cobertura " +

                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Trámite' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Trámite', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Suspendido' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'Suspendido', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Proceso' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Proceso', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Reenvío Trámite' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(4, 'Reenvío Trámite', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Revisión' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(5, 'En Revisión', 0, 1) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Concluído' AND Cobertura=1) INSERT INTO #EstadosTemporales2 VALUES(6, 'Concluído', 0, 1) " +

                    "SELECT * FROM #EstadosTemporales2 WHERE Cobertura=1 ORDER BY Orden";
                    b.ExecuteCommandQuery(consulta);
                    b.AddParameter("@usuario", usuario, SqlDbType.Int);

                    b.ConnectionOpenToTransaction();
                    b.BeginTransaction();

                    dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                    b.CommitTransaction();

                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    b.RollBackTransaction();
                }
                finally
                {
                    b.ConnectionCloseToTransaction();
                }

                return dt;
            }

            public DataTable SeleccionarPorCoberturaPotenciadaUsuario(int usuario)
            {
                DataTable dt = new DataTable();
                try
                {
                    string consulta = "CREATE TABLE #EstadosTemporales " +
                    "( " +
                    "    Folio NVARCHAR(50) NULL, " +
                    "    Estado INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales SELECT a.folio, MAX(a.Estado) AS Estado, b.Cobertura FROM ArchivosDependenciasEstados a, ArchivosDependencias b WHERE a.Folio=b.Folio AND b.UsuarioAsignado=@usuario GROUP BY a.Folio, b.Cobertura; " +

                    "CREATE TABLE #EstadosTemporales2 " +
                    "( " +
                    "    Orden int," +
                    "    Estado NVARCHAR(50) NULL, " +
                    "    Totales INT, " +
                    "    Cobertura INT " +
                    ") " +

                    "INSERT INTO #EstadosTemporales2 (Orden, Estado, Totales, Cobertura) " +
                    "SELECT " +
                    "    CASE Estado " +
                    "        WHEN 1 THEN 1 " +
                    "        WHEN 2 THEN 2 " +
                    "        WHEN 3 THEN 3 " +
                    "        WHEN 4 THEN 4 " +
                    "        WHEN 5 THEN 5 " +
                    "        WHEN 6 THEN 6 " +
                    "        WHEN 7 THEN 7 " +
                    "        WHEN 8 THEN 8 " +
                    "    END AS Orden, " +
                    "	CASE Estado " +
                    "        WHEN 1 THEN 'En Trámite' " +
                    "        WHEN 2 THEN 'Suspendido' " +
                    "        WHEN 3 THEN 'En Proceso' " +
                    "        WHEN 4 THEN 'Reenvío Trámite' " +
                    "        WHEN 5 THEN 'En Revisión' " +
                    "        WHEN 6 THEN 'Incompleto' " +
                    "        WHEN 7 THEN 'Carta' " +
                    "        WHEN 8 THEN 'Concluído' " +
                    "    END AS Estado,  " +
                    "COUNT(Estado) AS Totales, Cobertura " +
                    "FROM #EstadosTemporales " +
                    "WHERE Cobertura=2 " +
                    "GROUP BY Estado, Cobertura " +

                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Trámite' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Trámite', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Suspendido' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'Suspendido', 0, 2)  " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Proceso' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(3, 'En Proceso', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Reenvío Trámite' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(4, 'Reenvío Trámite', 0, 2)  " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='En Revisión' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(5, 'En Revisión', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Incompleto' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(6, 'Incompleto', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Carta' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(7, 'Carta', 0, 2) " +
                    "IF NOT EXISTS(SELECT * FROM #EstadosTemporales2 WHERE Estado='Concluído' AND Cobertura=2) INSERT INTO #EstadosTemporales2 VALUES(8, 'Concluído', 0, 2) " +

                    "SELECT * FROM #EstadosTemporales2 WHERE Cobertura=2 ORDER BY Orden";
                    b.ExecuteCommandQuery(consulta);
                    b.AddParameter("@usuario", usuario, SqlDbType.Int);

                    b.ConnectionOpenToTransaction();
                    b.BeginTransaction();

                    dt = b.SelectExecuteFunctionsToTransaction().Tables[0];
                    b.CommitTransaction();

                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    b.RollBackTransaction();
                }
                finally
                {
                    b.ConnectionCloseToTransaction();
                }

                return dt;
            }


            public DataTable SeleccionarArchivosDeTramite(string folio)
            {
                string consulta = "SELECT Nombre, ArchivoPDF, ArchivoXLS, Archivo100PosPagos, Archivo100PosCanc, CartaPDF, Documento, ArchivosPotenciacion FROM archivosdependencias WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.VarChar);
                return b.Select();
            }

            public int AgregarBasica(string nombre, string folio, string poliza, string cobertura, string subidopor, string correo, string asunto, string trimestre, string ann)
            {
                string consulta = "INSERT INTO ArchivosDependencias (Nombre, Folio, Fecha, Poliza, Cobertura, SubidoPor, Correo, Asunto, Trimestre, Ann) " +
                    "VALUES(@nombre, @folio, getdate(), @poliza, @cobertura, @subidopor, @correo, @asunto, @trimestre, @ann)";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@nombre", nombre, SqlDbType.NVarChar, 250);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@poliza", poliza, SqlDbType.NVarChar, 50);
                b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar, 1);
                b.AddParameter("@subidopor", subidopor, SqlDbType.NVarChar, 150);
                b.AddParameter("@correo", correo, SqlDbType.NVarChar, 150);
                b.AddParameter("@asunto", asunto, SqlDbType.NVarChar, 350);
                b.AddParameter("@trimestre", trimestre, SqlDbType.Char, 1);
                b.AddParameter("@ann", ann, SqlDbType.Char, 4);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarPotenciacion(string nombre, string folio, string poliza, string cobertura, string subidopor, string correo, string asunto, string quincena, string ann2)
            {
                string consulta = "INSERT INTO ArchivosDependencias (Nombre, Folio, Fecha, Poliza, Cobertura, SubidoPor, Correo, Asunto, Quincena, Ann2) " +
                    "VALUES(@nombre, @folio, getdate(), @poliza, @cobertura, @subidopor, @correo, @asunto, @quincena, @ann2)";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@nombre", nombre, SqlDbType.NVarChar, 250);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@poliza", poliza, SqlDbType.NVarChar, 50);
                b.AddParameter("@cobertura", cobertura, SqlDbType.NVarChar, 1);
                b.AddParameter("@subidopor", subidopor, SqlDbType.NVarChar, 150);
                b.AddParameter("@correo", correo, SqlDbType.NVarChar, 150);
                b.AddParameter("@asunto", asunto, SqlDbType.NVarChar, 350);
                b.AddParameter("@quincena", quincena, SqlDbType.Int);
                b.AddParameter("@ann2", ann2, SqlDbType.Char, 4);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarUsuarioAsignadoATramite(string usuario, string folio)
            {
                string consulta = "UPDATE archivosdependencias SET UsuarioAsignado=@usuario WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@usuario", usuario, SqlDbType.Int);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarDependenciaATramite(int rol, string folio)
            {
                string consulta = "UPDATE archivosdependencias SET opdep=@rol WHERE Folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@rol", rol, SqlDbType.Int);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarEstado(string folio, int estado)
            {
                string consulta = "UPDATE archivosdependencias SET Estado=@estado WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@estado", estado, SqlDbType.Int);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarPDF(string archivopdf, string folio)
            {
                string consulta = "UPDATE archivosdependencias SET archivopdf=@archivopdf WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@archivopdf", archivopdf, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarFolioMovimiento(string folio2, string movimiento, string folio)
            {
                string consulta = "UPDATE ArchivosDependencia SET Folio2=@folio2, Movimiento=@movimiento WHERE Folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio2", folio2, SqlDbType.NChar, 20);
                b.AddParameter("@movimiento", movimiento, SqlDbType.NChar, 20);
                b.AddParameter("@folio", folio, SqlDbType.NChar, 20);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarDocumento(string archivo, string folio)
            {
                string consulta = "UPDATE archivosdependencias SET documento=@archivo WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@archivo", archivo, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarCartaPDF(string archivopdf, string folio)
            {
                string consulta = "UPDATE archivosdependencias SET cartapdf=@archivopdf WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@archivopdf", archivopdf, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarArchivosPotenciacion(string folio, string archivos)
            {
                string consulta = "UPDATE archivosdependencias SET archivospotenciacion=@archivos WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@archivos", archivos, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int AgregarXML(string archivoxls, string folio)
            {
                string consulta = "UPDATE archivosdependencias SET archivoxls=@archivoxls WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@archivoxls", archivoxls, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int Agregar100PosicionesPagos(string archivo, string folio)
            {
                string consulta = "UPDATE archivosdependencias SET Archivo100PosPagos=@archivo WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@archivo", archivo, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int Agregar100PosicionesCancelaciones(string archivo, string folio)
            {
                string consulta = "UPDATE archivosdependencias SET Archivo100PosCanc=@archivo WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@archivo", archivo, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }


            public int ActualizarAsunto(string folio, string asunto)
            {
                string consulta = "UPDATE archivosdependencias SET asunto=@asunto WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@asunto", asunto, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int ActualizarErrores(string folio, string errores)
            {
                string consulta = "UPDATE archivosdependencias SET Errores=@errores WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@errores", errores, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int ActualizarErrores2(string folio, string errores)
            {
                string consulta = "UPDATE archivosdependencias SET Errores2=@errores WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                b.AddParameter("@errores", errores, SqlDbType.NVarChar, 350);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int ActualizarTerminacionProcesoExitoso(string folio)
            {
                string consulta = "UPDATE archivosdependencias SET Estado=5, EstadoFecha=getdate() WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int ActualizarReasignarTramiteAOtroUsuario(string folio)
            {
                string consulta = "UPDATE ArchivosDependencias SET UsuarioAsignado=null WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }



            public int Eliminar(string folio)
            {
                string consulta = "DELETE FROM archivosdependencias WHERE folio=@folio";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@folio", folio, SqlDbType.NVarChar, 50);
                return b.InsertUpdateDeleteWithTransaction();
            }


        }    
    }

    public class ArchivosDependenciasPropiedades
    {
        public string Nombre { get; set; }
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string Poliza { get; set; }
        public string Cobertura { get; set; }
        public string SubidoPor { get; set; }
        public string Correo { get; set; }
        public string Asunto { get; set; }
        public string Estado { get; set; }
    }


}
