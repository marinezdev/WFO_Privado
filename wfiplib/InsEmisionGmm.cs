using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    class InsEmisionGmm
    {
        private int mIdTramite = 0;

        private E_TipoPersona mTipoPersona = E_TipoPersona.Fisica;

        //private string mNumPoliza = "";

        private string mNombre = "";

        private string mApPaterno = "";

        private string mApMaterno = "";

        private string mRFC = "";

        private string mFechaNacimiento = "";

        private string mFechaConst = "";

        private string mDetalle = "";

        private string mFolio = "";

        private string mNumeroOrden = "";

        private string mAgenteClave = "";

        private string mFechaSolicitud = "";

        private string mTipoTramite = "";
        
        private string mTitularNombre = "";

        private string mTitularApPat = "";

        private string mTitularApMat = "";

        private string mNacionalidad = "";

        private string mTitularNacionalidad = "";

        private string mTitularFechaNacimiento = "";

        private string mTitularSexo = "";

        private string mIdMoneda = "";

        private string mSexo = "";

        private string mEdad = "";

        private string mSumaAsegurada = "";

        private string mMontoSuAsegurada = "";

        private string mSumaPolizas = "";

        private string mMontoSuPolizas = "";

        public string IdMoneda
        {
            get
            {
                return this.mIdMoneda;
            }
            set
            {
                this.mIdMoneda = value;
            }
        }


        public string TitularSexo
        {
            get
            {
                return this.mTitularSexo;
            }
            set
            {
                this.mTitularSexo = value;
            }
        }

        public string TitularFechaNacimiento
        {
            get
            {
                return this.mTitularFechaNacimiento;
            }
            set
            {
                this.mTitularFechaNacimiento = value;
            }
        }

        public string TitularNacionalidad
        {
            get
            {
                return this.mTitularNacionalidad;
            }
            set
            {
                this.mTitularNacionalidad = value;
            }
        }

        public string Nacionalidad
        {
            get
            {
                return this.mNacionalidad;
            }
            set
            {
                this.mNacionalidad = value;
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
        

        public string TitularNombre
        {
            get
            {
                return this.mTitularNombre;
            }
            set
            {
                this.mTitularNombre = value;
            }
        }

        public string TitularApPat
        {
            get
            {
                return this.mTitularApPat;
            }
            set
            {
                this.mTitularApPat = value;
            }
        }

        public string TitularApMat
        {
            get
            {
                return this.mTitularApMat;
            }
            set
            {
                this.mTitularApMat = value;
            }
        }

        public string FechaNacimiento
        {
            get
            {
                return this.mFechaNacimiento;
            }
            set
            {
                this.mFechaNacimiento = value;
            }
        }
        public string FechaConst
        {
            get
            {
                return this.mFechaConst;
            }
            set
            {
                this.mFechaConst = value;
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

        public E_TipoPersona TipoPersona
        {
            get
            {
                return this.mTipoPersona;
            }
            set
            {
                this.mTipoPersona = value;
            }
        }
        public string Nombre
        {
            get
            {
                return this.mNombre;
            }
            set
            {
                this.mNombre = value;
            }
        }

        public string ApPaterno
        {
            get
            {
                return this.mApPaterno;
            }
            set
            {
                this.mApPaterno = value;
            }
        }

        public string ApMaterno
        {
            get
            {
                return this.mApMaterno;
            }
            set
            {
                this.mApMaterno = value;
            }
        }

        public string RFC
        {
            get
            {
                return this.mRFC;
            }
            set
            {
                this.mRFC = value;
            }
        }

        

        public string Detalle
        {
            get
            {
                return this.mDetalle;
            }
            set
            {
                this.mDetalle = value;
            }
        }
        public string Folio
        {
            get
            {
                return this.mFolio;
            }
            set
            {
                this.mFolio = value;
            }
        }
        
        public string Sexo
        {
            get
            {
                return this.mSexo;
            }
            set
            {
                this.mSexo = value;
            }
        }

        public string Edad
        {
            get
            {
                return this.mEdad;
            }
            set
            {
                this.mEdad = value;
            }
        }

        public string SumaAsegurada
        {
            get
            {
                return this.mSumaAsegurada;
            }
            set
            {
                this.mSumaAsegurada = value;
            }
        }

        public string MontoSuAsegurada
        {
            get
            {
                return this.mMontoSuAsegurada;
            }
            set
            {
                this.mMontoSuAsegurada = value;
            }
        }

        public string SumaPolizas
        {
            get
            {
                return this.mSumaPolizas;
            }
            set
            {
                this.mSumaPolizas = value;
            }
        }

        public string MontoSuPolizas
        {
            get
            {
                return this.mMontoSuPolizas;
            }
            set
            {
                this.mMontoSuPolizas = value;
            }
        }
        
        // LA CABECERA ES MOSTRADA EN ANEXAR ACHIVOS
        public string CabeceraHtml
        {
            get
            {
                string text = "<table>";

                text = string.Concat(new string[]
                {
                    text,
                    "<tr><td>Nombre(s):</td><td><b>",
                    this.mNombre,
                    " ",
                    this.mApPaterno,
                    " ",
                    this.mApMaterno,
                    "</b></td></tr>"
                });
                text = text + "<tr><td>RFC:</td><td><b>" + this.mRFC + "</b></td></tr>";
                if (!String.IsNullOrEmpty(this.mTitularNombre))
                {
                    text = string.Concat(new string[]
                    {
                        text,
                        "<tr><td>Titular:</td>" +
                            "<td><b>",
                                this.mTitularNombre," ",this.mTitularApPat," ",this.mTitularApMat,"</b>" +
                           "</td>" +
                        "</tr>"
                    });
                }
                return text + "</table>";
            }
        }

        public string DatosHtml
        {
            get
            {
                string text = "<p>";
                text = string.Concat(new string[]
                {
                    text,
                    "Nombre(s):<b>",
                    this.mNombre,
                    " ",
                    this.mApPaterno,
                    " ",
                    this.mApMaterno,
                    "</b></br>"
                });
                text = text + "RFC:<b>" + this.mRFC + "</b></br>";
                if (!String.IsNullOrEmpty(this.mTitularNombre))
                {
                    text = string.Concat(new string[]
                    {
                        text,
                        "Titular:<b>",
                        this.mTitularNombre,
                        " ",
                        this.mTitularApPat,
                        " ",
                        this.mTitularApMat,
                        "</b></br>"
                    });
                }
                return text + "</p>";
            }
        }

        public string FolioHtml
        {
            get
            {
                string text = "<p>";
                text = text + "Folio:<b>" + this.mFolio + "</b></br>";
                return text + "</p>";
            }
        }
        /*
        public string ProductoHtml
        {
            get
            {
                string text = "<p>";
                text = text + "Producto:<b>" + this.mProducto + "</b></br>";
                text = text + "Producto:<b>" + this.mSubproducto + "</b></br>";
                return text + "</p>";
            }
        }
        */

        public string TextoHtml
        {
            get
            {
                string text = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                text = string.Concat(new string[]
                {
                    text,
                    "<tr><td>Nombre(s):</td><td><b>",
                    this.mNombre,
                    " ",
                    this.mApPaterno,
                    " ",
                    this.mApMaterno,
                    "</b></td></tr>"
                });
                text = text + "<tr><td>RFC:</td><td><b>" + this.mRFC + "</b></td></tr>";
                bool flag = this.mTipoPersona.Equals(E_TipoPersona.Fisica);
                if (flag)
                {
                    //text = text + "<tr><td>CURP:</td><td><b>" + this.mCURP + "</b></td></tr>";
                }
                text += "<tr><td colspan='2'>";
                if (!String.IsNullOrEmpty(this.mTitularNombre))
                {
                    text = string.Concat(new string[]
                    {
                        text,
                        "<tr><td>Nombre(s):</td><td><b>",
                        this.mTitularNombre,
                        " ",
                        this.mTitularApPat,
                        " ",
                        this.mTitularApMat,
                        "</b></td></tr>"
                    });
                }
                text += "</br></br>TRAMITES:</br></br> ";
                bool flag41 = this.mDetalle != "";
                if (flag41)
                {
                    text = text + "</br>DETALLES:</br></br><b>" + this.mDetalle + "</b>";
                }
                text += "</td></tr>";
                return text + "</table>";
            }
        }

    }
}
