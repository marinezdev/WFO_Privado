using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class EmisionVG
    {
        private int mIdTramite = 0;

        private E_TipoPersona mTipoPersona = E_TipoPersona.Fisica;

        public E_EstadoTramite EstadoEnum
        {
            get;
            set;
        }



        //private string mNumPoliza = "";

        private string mNombre = "";

        private string mApPaterno = "";

        private string mApMaterno = "";

        private string mRFC = "";

        private string mFechaNacimiento = "";

        private string mFechaConst = "";

        //private string mCURP = "";

        private int mConAfectacion = 0;

        // NUEVOS DATOS

        private string mDetalle = "";

        private string mProducto = "";

        private string mSubproducto = "";

        private string mFolio = "";

        ////////////////////////////////////////////
        //////////// NUEVOS DATOS  /////////////////
        ////////////////////////////////////////////

        private E_PrioridadTramite mPriodidad;

        private string mProducto1 = "";

        private string mPlan1 = "";

        private string mProducto2 = "";

        private string mPlan2 = "";

        private string mGProducto1 = "";

        private string mGPlan1 = "";

        private string mGProducto2 = "";

        private string mGPlan2 = "";

        private string mNumeroOrden = "";

        private string mAgenteClave = "";

        private string mFechaSolicitud = "";

        private string mTipoTramite = "";

        private bool mCPDES = false;

        private bool mHombreClave = false;

        private string mFolioCPDES = "";

        private string mEstatusCPDES = "";

        private string mTitularNombre = "";

        private string mTitularApPat = "";

        private string mTitularApMat = "";

        private string mNacionalidad = "";

        private string mTitularNacionalidad = "";

        private string mTitularFechaNacimiento = "";

        private string mTitularSexo = "";

        private string mTitularEntidad = "";

        private string mIdMoneda = "";

        private string mSexoCitaMedica = "";

        private string mIdPromotoria = "";

        private int mIdInstituciones = 0;

        private string mFechaRegistro = "";

        private int mFechaSeleccionada = 0;

        private int mActivo = 0;

        private bool mTempoLife = false;

        private string mFechaRecepcion = "";

        private string mIdSisLegados = "";

        private string mkwik = "";

        private string mEntidadFederativa = "";

        private string mExcel = "";

        private bool mOneShot = false;

        public string Excel
        {
            get
            {
                return this.mExcel;
            }
            set
            {
                this.mExcel = value;
            }
        }

        public string FechaRecepcion
        {
            get
            {
                return this.mFechaRecepcion;
            }
            set
            {
                this.mFechaRecepcion = value;
            }
        }

        public string IdSisLegados
        {
            get
            {
                return this.mIdSisLegados;
            }
            set
            {
                this.mIdSisLegados = value;
            }
        }

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

        public bool TempoLife
        {
            get
            {
                return this.mTempoLife;
            }
            set
            {
                this.mTempoLife = value;
            }
        }

        public int Activo
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

        public int FechaSeleccionada
        {
            get
            {
                return this.mFechaSeleccionada;
            }
            set
            {
                this.mFechaSeleccionada = value;
            }
        }

        public string FechaRegistro
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

        

        public string IdPromotoria
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

        public string SexoCitaMedica
        {
            get
            {
                return this.mSexoCitaMedica;
            }
            set
            {
                this.mSexoCitaMedica = value;
            }
        }


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

        public string TitularEntidad
        {
            get
            {
                return this.mTitularEntidad;
            }
            set
            {
                this.mTitularEntidad = value;
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
        public string Producto1
        {
            get
            {
                return this.mProducto1;
            }
            set
            {
                this.mProducto1 = value;
            }
        }

        public string Plan1
        {
            get
            {
                return this.mPlan1;
            }
            set
            {
                this.mPlan1 = value;
            }
        }

        public string Producto2
        {
            get
            {
                return this.mProducto2;
            }
            set
            {
                this.mProducto2 = value;
            }
        }

        public string Plan2
        {
            get
            {
                return this.mPlan2;
            }
            set
            {
                this.mPlan2 = value;
            }
        }

        public string GProducto1
        {
            get
            {
                return this.mGProducto1;
            }
            set
            {
                this.mGProducto1 = value;
            }
        }

        public string GPlan1
        {
            get
            {
                return this.mGPlan1;
            }
            set
            {
                this.mGPlan1 = value;
            }
        }

        public string GProducto2
        {
            get
            {
                return this.mGProducto2;
            }
            set
            {
                this.mGProducto2 = value;
            }
        }

        public string GPlan2
        {
            get
            {
                return this.mGPlan2;
            }
            set
            {
                this.mGPlan2 = value;
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
        /// 
        /// ///////////////////////////// DATOS NUEVOS 
        ///
        public bool CPDES
        {
            get
            {
                return this.mCPDES;
            }
            set
            {
                this.mCPDES = value;
            }
        }

        public bool HombreClave
        {
            get
            {
                return this.mHombreClave;
            }
            set
            {
                this.mHombreClave = value;
            }
        }

        public string FolioCPDES
        {
            get
            {
                return this.mFolioCPDES;
            }
            set
            {
                this.mFolioCPDES = value;
            }
        }

        public string EstatusCPDES
        {
            get
            {
                return this.mEstatusCPDES;
            }
            set
            {
                this.mEstatusCPDES = value;
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

        public string EntidadFederativa
        {
            get { return this.mEntidadFederativa; }
            set { this.mEntidadFederativa = value; }
        }

        public bool OneShot
        {
            get { return this.mOneShot; }
            set { this.mOneShot = value; }
        }

        public bool MetaLifeEspecial { get; set; }

        ///////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

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

        public int ConAfectacion
        {
            get
            {
                return this.mConAfectacion;
            }
            set
            {
                this.mConAfectacion = value;
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

        public string Subproducto
        {
            get
            {
                return this.mSubproducto;
            }
            set
            {
                this.mSubproducto = value;
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////
        /// </summary>
        /// 

        private string mSexo = "";

        private string mEdad = "";

        private string mSumaAsegurada = "";

        private string mMontoSuAsegurada = "";

        private string mSumaPolizas = "";

        private string mPrimaTotal = "";

        private string mMontoSuPolizas = "";

        private string mTotal = "";

        private string mCombo = "";

        private string mCel = "";

        private string mEstado = "";

        private string mCelAgentePromotor = "";

        private string mCiudad = "";

        private string mCorreo = "";

        private string mLaboratorioHospital = "";

        private string mDireccion = "";

        private string mFecha1 = "";

        private string mHora1 = "";

        private string mFecha2 = "";

        private string mHora2 = "";

        private string mFecha3 = "";

        private string mFecha4 = "";

        private string mHora3 = "";

        private string mNotas = "";

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

        public string PrimaTotal
        {
            get
            {
                return this.mPrimaTotal;
            }
            set
            {
                this.mPrimaTotal = value;
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

        public string Total
        {
            get
            {
                return this.mTotal;
            }
            set
            {
                this.mTotal = value;
            }
        }

        public string Combo
        {
            get
            {
                return this.mCombo;
            }
            set
            {
                this.mCombo = value;
            }
        }

        public string Cel
        {
            get
            {
                return this.mCel;
            }
            set
            {
                this.mCel = value;
            }
        }
        public string Estado
        {
            get
            {
                return this.mEstado;
            }
            set
            {
                this.mEstado = value;
            }
        }
        public string CelAgentePromotor
        {
            get
            {
                return this.mCelAgentePromotor;
            }
            set
            {
                this.mCelAgentePromotor = value;
            }
        }
        public string Ciudad
        {
            get
            {
                return this.mCiudad;
            }
            set
            {
                this.mCiudad = value;
            }
        }
        public string Correo
        {
            get
            {
                return this.mCorreo;
            }
            set
            {
                this.mCorreo = value;
            }
        }
        public string LaboratorioHospital
        {
            get
            {
                return this.mLaboratorioHospital;
            }
            set
            {
                this.mLaboratorioHospital = value;
            }
        }
        public string Direccion
        {
            get
            {
                return this.mDireccion;
            }
            set
            {
                this.mDireccion = value;
            }
        }
        public string Fecha1
        {
            get
            {
                return this.mFecha1;
            }
            set
            {
                this.mFecha1 = value;
            }
        }
        public string Hora1
        {
            get
            {
                return this.mHora1;
            }
            set
            {
                this.mHora1 = value;
            }
        }
        public string Fecha2
        {
            get
            {
                return this.mFecha2;
            }
            set
            {
                this.mFecha2 = value;
            }
        }
        public string Hora2
        {
            get
            {
                return this.mHora2;
            }
            set
            {
                this.mHora2 = value;
            }
        }
        public string Fecha3
        {
            get
            {
                return this.mFecha3;
            }
            set
            {
                this.mFecha3 = value;
            }
        }
        public string Fecha4
        {
            get
            {
                return this.mFecha4;
            }
            set
            {
                this.mFecha4 = value;
            }
        }
        public string Hora3
        {
            get
            {
                return this.mHora3;
            }
            set
            {
                this.mHora3 = value;
            }
        }

        public string Notas
        {
            get
            {
                return this.mNotas;
            }
            set
            {
                this.mNotas = value;
            }
        }

        private bool mCitaMedica;
        public bool CitaMedica
        {
            get { return this.mCitaMedica; }
            set { this.mCitaMedica = value; }
        }

        public E_PrioridadTramite Prioridad
        {
            get { return this.mPriodidad; }
            set { this.mPriodidad = value; }
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
