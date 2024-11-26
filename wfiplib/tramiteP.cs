using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class tramiteP
    {
        private int mId = 0;

        private E_TipoTramite mIdTipoTramite = E_TipoTramite.Ninguno;
        
        private DateTime mFechaRegistro = new DateTime(1900, 1, 1, 0, 0, 0);

        private DateTime mFechaTermino = new DateTime(1900, 1, 1, 0, 0, 0);

        private string mEstadoNombre = "";

        private string mDatosHtml = "";

        private int mIdPromotoria = 0;

        private int mIdInstituciones = 0;

        private int mIdUsuario = 0;

        private string mAgenteClave = "";

        private int mIdFlujo = 0;

        private string mFlujo = "";

        private string mTramiteNombre = "";

        private string mFolioCompuesto = "";

        private string mTabla = "";

        private string mPromotoriaNombre = "";

        private string mPromotoriaClave = "";

        private string mAgenteNombre = "";

        private string mUsuarioNombre = "";

        // SE AGREGAN DOS NUEVOS DATOS
        private string mNumeroOrden = "";

        private string mFechaSolicitud = "";

        private string mTipoTramite = "";

        /*private string mCPDES = "";

        private string mFolioCPDES = "";

        private string mEstatusCPDES = "";*/

        private int mIdRamo;
        private int mPriodidad;

        private string mkwik = "";

        

        public string kwik
        {
            get
            {
                return this.mkwik;
            }
            set
            {
                this.mkwik = value;
            }

        }

        public int Id
        {
            get
            {
                return this.mId;
            }
            set
            {
                this.mId = value;
            }
        }

        public E_TipoTramite IdTipoTramite
        {
            get
            {
                return this.mIdTipoTramite;
            }
            set
            {
                this.mIdTipoTramite = value;
            }
        }

        public DateTime FechaRegistro
        {
            get
            {
                return this.mFechaRegistro;
            }
            set
            {
                this.mFechaRegistro = value;
            }
        }

        public DateTime FechaTermino
        {
            get
            {
                return this.mFechaTermino;
            }
            set
            {
                this.mFechaTermino = value;
            }
        }

        public E_EstadoTramite Estado
        {
            get;
            set;
        }

        public string EstadoNombre
        {
            get
            {
                return this.mEstadoNombre;
            }
            set
            {
                this.mEstadoNombre = value;
            }
        }

        public string DatosHtml
        {
            get
            {
                return this.mDatosHtml;
            }
            set
            {
                this.mDatosHtml = value;
            }
        }

        public int IdPromotoria
        {
            get
            {
                return this.mIdPromotoria;
            }
            set
            {
                this.mIdPromotoria = value;
            }
        }

        public int IdUsuario
        {
            get
            {
                return this.mIdUsuario;
            }
            set
            {
                this.mIdUsuario = value;
            }
        }

        public string AgenteClave
        {
            get
            {
                return this.mAgenteClave;
            }
            set
            {
                this.mAgenteClave = value;
            }
        }

        public int IdFlujo
        {
            get
            {
                return this.mIdFlujo;
            }
            set
            {
                this.mIdFlujo = value;
            }
        }

        public string Flujo
        {
            get
            {
                return this.mFlujo;
            }
            set
            {
                this.mFlujo = value;
            }
        }

        public string TramiteNombre
        {
            get
            {
                return this.mTramiteNombre;
            }
            set
            {
                this.mTramiteNombre = value;
            }
        }

        public string FolioCompuesto
        {
            get
            {
                return this.mFolioCompuesto;
            }
            set
            {
                this.mFolioCompuesto = value;
            }
        }

        public string Tabla
        {
            get
            {
                return this.mTabla;
            }
            set
            {
                this.mTabla = value;
            }
        }

        public string PromotoriaNombre
        {
            get
            {
                return this.mPromotoriaNombre;
            }
            set
            {
                this.mPromotoriaNombre = value;
            }
        }

        public string PromotoriaClave
        {
            get
            {
                return this.mPromotoriaClave;
            }
            set
            {
                this.mPromotoriaClave = value;
            }
        }

        public string AgenteNombre
        {
            get
            {
                return this.mAgenteNombre;
            }
            set
            {
                this.mAgenteNombre = value;
            }
        }

        public string UsuarioNombre
        {
            get
            {
                return this.mUsuarioNombre;
            }
            set
            {
                this.mUsuarioNombre = value;
            }
        }

        public string NumeroOrden
        {
            get
            {
                return this.mNumeroOrden;
            }
            set
            {
                this.mNumeroOrden = value;
            }
        }

        public string FechaSolicitud
        {
            get
            {
                return this.mFechaSolicitud;
            }
            set
            {
                this.mFechaSolicitud = value;
            }
        }

        public string TipoTramite
        {
            get
            {
                return this.mTipoTramite;
            }
            set
            {
                this.mTipoTramite = value;
            }
        }

        public int IdInstituciones
        {
            get
            {
                return this.mIdInstituciones;
            }
            set
            {
                this.mIdInstituciones = value;
            }
        }


        public int IdRamo
        {
            get { return this.mIdRamo; }
            set { this.mIdRamo = value; }
        }

        public int Prioridad
        {
            get { return this.mPriodidad; }
            set { this.mPriodidad = value; }
        }

        private wfiplib.E_RamoTramite mIdRamoTramite;
        public wfiplib.E_RamoTramite IdRamoTramite
        {
            get { return this.mIdRamoTramite; }
            set { this.mIdRamoTramite = value; }
        }
        public string IdSisLegados { get; set; }
        public string CPDES { get; set; }
        public string EstatusCPDES { get; set; }
    }
}
