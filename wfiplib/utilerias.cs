using System.Collections.Generic;
using System.Data;

namespace wfiplib
{
    public class Utilerias
    {
        public List<CboValorTexto> DaListaDeRolesUsuarios
        {
            get
            {
                List<CboValorTexto> resultado = new List<CboValorTexto>();
                resultado.Add(new CboValorTexto("0", "Ninguno"));
                resultado.Add(new CboValorTexto("10", "Operador"));
                resultado.Add(new CboValorTexto("20", "Supervisor"));
                resultado.Add(new CboValorTexto("30", "Jefe"));
                return resultado;
            }
        }
        public DataTable Menu(int idModulo, int grupo)
        {
            DataTable dt = new DataTable();
            bd menu = new bd();
            string query = "SELECT IdMenu, PerteneceA AS idMenuParent, Descripcion  AS Nombre,Url FROM Menus " +
                           "WHERE Modulo=" + idModulo + " AND Grupo=" + grupo;
            dt = menu.leeDatos(query);
            menu.cierraBD();
            return dt;
        }
    }

    public enum E_Aplicacion { Ninguno = 0, AdminSis = 1, Fto = 2, PolizaGrupal = 3 }
    public enum E_Estado { Inactivo = 0, Activo = 1 }
    public enum E_CredencialGrupo { Ninguno = 0, Operador = 10, Supervisor = 20, Jefe = 30, admsys = 1000, SupervisorRes= 40, PromotoriaRes = 40 }
    public enum E_Modulo { Ninguno = 0, Promotoria = 1, Apoyo = 2, Operacion = 3, Monitor = 4, MesaAyuda = 5, Buzones = 20,  AdmSys = 99 }

    public enum E_RamoTramite {
        GMM = 1,
        VIDA = 2
    }

    public enum E_PrioridadTramite
    {
        Supervisor = 1,
        GrandesSumas = 2,
        GrandesSumasPrimas = 3,
        HombreClave = 4,
        MetalifeEspecial = 5,
        Tramite = 6,
        Ninguno = 0
    }

    public enum E_EstadoTramite {
        NA = -1,
        Registro = 0,
        Proceso = 1,
        Hold = 2,
        Ejecucion = 3,
        Rechazo = 4,
        Suspendido = 5,
        PCI = 6,
        Cancelado = 7,
        Revisión = 8,
        EsperaResultados = 9,
        InfoCitaMedica = 10,
        RevMedica = 11,
        RevPromotoria = 12,
        RevPromotoriaOK = 13,
        RevPromotoriaKO = 14,
        PromotoriaCancela  = 15,
        CMRevProspecto = 16,
        CMConfirmacionPendiente = 17,
        Caducado = 18,
        PromotoriaReconsidera = 19,
        SuspensionCitaMedica = 20
    }

    public enum E_EstadoMesa {
        Registro = 0,
        Atrapado = 1,
        Hold = 2,
        RevisionHold = 3,
        ReingresoHold = 4,
        SolicitudApoyo = 5,
        ReingresoApoyo = 6,
        Pausa = 7,
        Procesado = 8,
        Rechazo = 9,
        Suspendido = 10,
        RevisionSuspencion = 11,
        ReingresoSuspencion = 12,
        PCI = 13,
        ReingresoPCI = 14,
        RevisionPCI = 15,
        Procesable = 16,
        NoProcesable = 17,
        Complementario = 18,
        CMRevisionProspecto = 19,
        CMConfirmacionPendiente = 20,
        CMCitaProgramada = 21,
        CMCitaReProgramada = 22,
        CMEnEsperaResult = 23,
        InfoCitasMedicas = 24,
        ResponseFromMesa = 25,
        SendToMesa = 26,
        RevPromotoria = 27,
        RevPromotoriaKO = 28,
        RevPromotoriaOK = 29,
        PromotoriaCancela = 30,
        Caducado = 31,
        FromMesa = 32,
        Cancelado = 33,
        CPDES = 34,
        CMCancelada = 35,
        PromotoriaReconsidera = 36,
        NoAplica = 37,
        SuspensionCitaMedica = 38,
        RevisionCitaMedica = 39,
        ReingresoCitaMedica = 40
    }
        
    //public enum e_TipoMesa { General = 1, Especializada = 2, Apoyo = 3 }
    public enum E_TipoMesa { General = 1, Especializada = 2 }
    public enum E_TipoCampo { Ninguno = 0, Int = 1, Varchar = 2 }
    public enum E_TipoPersona { Fisica = 1, Moral = 2}

    public enum E_TipoTramite { Ninguno = 0, serviciosVida = 100, ServicioGmm = 200, indPriEmisionGMM = 1, indPriEmisionVida = 2, indPriEmisionVidaCM = 3, indPriEmisionConversiones = 4, InsPrivadoServicios = 20}

    //public enum E_RamoTramite { Ninguno = 0, Vida = 1, GMM = 2 } 
    public enum E_SiNo { No = 0, Si = 1 }

    public enum E_EstadoBuzon { Inactivo = 0, Espera = 1, Procesando = 2, Atendido = 3 }

    public enum E_AppRoles {
        Administrador = 1,
        Promotoria = 2,
        Operador = 3,
        Configuracion = 4,
        Laboratorio = 5,
        Supervisor = 6,
        Front = 7, 
        SuperPromotoria = 8,
        SuperReporte = 9,

        /* SOLO APLICA PARA EL MENU RESPONSIVO DEL ROL OPERADOR */
        OperadorRes = 99,
    }
    
    public class CboValorTexto
    {
        public string valor { get; set; }
        public string texto { get; set; }
        public CboValorTexto() { }
        public CboValorTexto(string pValor, string pTexto) { valor = pValor; texto = pTexto; }
    }
}