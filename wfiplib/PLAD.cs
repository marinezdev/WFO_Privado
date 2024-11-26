using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class PLAD
    {
        private int mIdTramite = 0;

        private string mFechaIdentificacion = "";

        private string mNombreRepreentante = "";

        private string mFolioMercantil = "";

        private string mGiroMercantil = "";

        private string mVigenciaComprovante = "";

        private string mDuracionSociedad = "";

        private string mActivo = "";

        public int IdTramite
        {
            get
            {
                return this.mIdTramite;
            }
            set
            {
                this.mIdTramite = value;
            }
        }

        public string FechaIdentificacion
        {
            get
            {
                return this.mFechaIdentificacion;
            }
            set
            {
                this.mFechaIdentificacion = value;
            }
        }

        public string NombreRepreentante
        {
            get
            {
                return this.mNombreRepreentante;
            }
            set
            {
                this.mNombreRepreentante = value;
            }
        }

        public string FolioMercantil
        {
            get
            {
                return this.mFolioMercantil;
            }
            set
            {
                this.mFolioMercantil = value;
            }
        }

        public string GiroMercantil
        {
            get
            {
                return this.mGiroMercantil;
            }
            set
            {
                this.mGiroMercantil = value;
            }
        }

        public string VigenciaComprovante
        {
            get
            {
                return this.mVigenciaComprovante;
            }
            set
            {
                this.mVigenciaComprovante = value;
            }
        }

        public string DuracionSociedad
        {
            get
            {
                return this.mDuracionSociedad;
            }
            set
            {
                this.mDuracionSociedad = value;
            }
        }

        public string Activo
        {
            get
            {
                return this.mActivo;
            }
            set
            {
                this.mActivo = value;
            }
        }
    }
}