using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class Seleccion
    {
        private int mIdTramite = 0;

        private string mRamo = "";

        private string mID = "";

        private string mFechaVigencia = "";

        private string mRiesgo = "";

        private string mRiesgoFactor = "";

        private string mZona = "";

        private string mPlan = "";

        private string mFactor = "";

        private string mActivo = "";

        private string mParentesco = "";

        /// <summary>
        ///  DATOS SELECCION VIDA 
        /// </summary>

        private string mProducto = "";

        private string mSubProducto = "";

        private string mNumCaptura = "";

        private string mVigencia = "";

        private string mExtraPrima = "";

        private string mHabito = "";

        private string mEndosos = "";

        private string mOcupacion = "";

        public string Endosos
        {
            get
            {
                return this.mEndosos;
            }
            set
            {
                this.mEndosos = value;
            }
        }

        public string Habito
        {
            get
            {
                return this.mHabito;
            }
            set
            {
                this.mHabito = value;
            }
        }

        public string ExtraPrima
        {
            get
            {
                return this.mExtraPrima;
            }
            set
            {
                this.mExtraPrima = value;
            }
        }

        public string Vigencia
        {
            get
            {
                return this.mVigencia;
            }
            set
            {
                this.mVigencia = value;
            }
        }

        public string NumCaptura
        {
            get
            {
                return this.mNumCaptura;
            }
            set
            {
                this.mNumCaptura = value;
            }
        }

        public string SubProducto
        {
            get
            {
                return this.mSubProducto;
            }
            set
            {
                this.mSubProducto = value;
            }
        }

        public string Producto
        {
            get
            {
                return this.mProducto;
            }
            set
            {
                this.mProducto = value;
            }
        }

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

        public string Ramo
        {
            get
            {
                return this.mRamo;
            }
            set
            {
                this.mRamo = value;
            }
        }

        public string ID
        {
            get
            {
                return this.mID;
            }
            set
            {
                this.mID = value;
            }
        }

        public string FechaVigencia
        {
            get
            {
                return this.mFechaVigencia;
            }
            set
            {
                this.mFechaVigencia = value;
            }
        }

        public string Riesgo
        {
            get
            {
                return this.mRiesgo;
            }
            set
            {
                this.mRiesgo = value;
            }
        }

        public string RiesgoFactor
        {
            get
            {
                return this.mRiesgoFactor;
            }
            set
            {
                this.mRiesgoFactor = value;
            }
        }

        public string Zona
        {
            get
            {
                return this.mZona;
            }
            set
            {
                this.mZona = value;
            }
        }

        public string Plan
        {
            get
            {
                return this.mPlan;
            }
            set
            {
                this.mPlan = value;
            }
        }

        public string Factor
        {
            get
            {
                return this.mFactor;
            }
            set
            {
                this.mFactor = value;
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

        public string Parentesco
        {
            get
            {
                return this.mParentesco;
            }
            set
            {
                this.mParentesco = value;
            }
        }

        public string Ocupacion
        {
            get
            {
                return this.mOcupacion;
            }
            set
            {
                this.mOcupacion = value;
            }
        }
    }
}
