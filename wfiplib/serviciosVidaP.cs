using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class serviciosVidaP
    {
        private int mIdTramite = 0;

        private E_TipoPersona mTipoPersona = E_TipoPersona.Fisica;

        private string mNumPoliza = "";

        private string mNombre = "";

        private string mApPaterno = "";

        private string mApMaterno = "";

        private string mRFC = "";

        private string mCURP = "";

        private string mFolio = "";

        private String[] mSubproductos = new string[40];

        private int mConAfectacion = 0;

        private int mC119 = 0;

        private int mC1110 = 0;

        private int mC1111 = 0;

        private int mC1112 = 0;

        private int mC1113 = 0;

        private int mC1114 = 0;

        private int mC1115 = 0;

        private int mC1116 = 0;

        private int mC1117 = 0;

        private int mC1118 = 0;

        private int mC1119 = 0;

        private int mC1120 = 0;

        private int mC1121 = 0;

        private int mC1122 = 0;

        private int mC1123 = 0;

        private int mC1124 = 0;

        private int mC1125 = 0;

        private int mC1126 = 0;

        private int mC1127 = 0;

        private int mC1128 = 0;

        private int mC1129 = 0;

        private int mC1130 = 0;

        private int mC1131 = 0;

        private int mC1132 = 0;

        private int mC1133 = 0;

        private int mC1134 = 0;

        private int mC1135 = 0;

        private int mC1136 = 0;

        private int mC1137 = 0;

        private int mC1138 = 0;

        private int mC1139 = 0;

        private int mC1140 = 0;

        private int mC1141 = 0;

        private int mC1142 = 0;

        private int mC1143 = 0;

        private int mC1144 = 0;

        private int mC1145 = 0;

        private int mC1146 = 0;

        private int mC1147 = 0;

        private string mDetalle = "";

        ////////////////////////////////////////////
        //////////// NUEVOS DATOS  /////////////////
        ////////////////////////////////////////////

        private string mNumeroOrden = "";

        private string mFechaSolicitud = "";

        private string mTipoTramite = "";

        private string mCPDES = "";

        private string mFolioCPDES = "";

        private string mEstatusCPDES = "";

        private string mNacionalidad = "";

        private string mProducto = "";

        private string mSubproducto = "";

        

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

        public string CPDES
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

        public string NumPoliza
        {
            get
            {
                return this.mNumPoliza;
            }
            set
            {
                this.mNumPoliza = value;
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

        public string CURP
        {
            get
            {
                return this.mCURP;
            }
            set
            {
                this.mCURP = value;
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

        public int C119
        {
            get
            {
                return this.mC119;
            }
            set
            {
                this.mC119 = value;
            }
        }

        public int C1110
        {
            get
            {
                return this.mC1110;
            }
            set
            {
                this.mC1110 = value;
            }
        }

        public int C1111
        {
            get
            {
                return this.mC1111;
            }
            set
            {
                this.mC1111 = value;
            }
        }

        public int C1112
        {
            get
            {
                return this.mC1112;
            }
            set
            {
                this.mC1112 = value;
            }
        }

        public int C1113
        {
            get
            {
                return this.mC1113;
            }
            set
            {
                this.mC1113 = value;
            }
        }

        public int C1114
        {
            get
            {
                return this.mC1114;
            }
            set
            {
                this.mC1114 = value;
            }
        }

        public int C1115
        {
            get
            {
                return this.mC1115;
            }
            set
            {
                this.mC1115 = value;
            }
        }

        public int C1116
        {
            get
            {
                return this.mC1116;
            }
            set
            {
                this.mC1116 = value;
            }
        }

        public int C1117
        {
            get
            {
                return this.mC1117;
            }
            set
            {
                this.mC1117 = value;
            }
        }

        public int C1118
        {
            get
            {
                return this.mC1118;
            }
            set
            {
                this.mC1118 = value;
            }
        }

        public int C1119
        {
            get
            {
                return this.mC1119;
            }
            set
            {
                this.mC1119 = value;
            }
        }

        public int C1120
        {
            get
            {
                return this.mC1120;
            }
            set
            {
                this.mC1120 = value;
            }
        }

        public int C1121
        {
            get
            {
                return this.mC1121;
            }
            set
            {
                this.mC1121 = value;
            }
        }

        public int C1122
        {
            get
            {
                return this.mC1122;
            }
            set
            {
                this.mC1122 = value;
            }
        }

        public int C1123
        {
            get
            {
                return this.mC1123;
            }
            set
            {
                this.mC1123 = value;
            }
        }

        public int C1124
        {
            get
            {
                return this.mC1124;
            }
            set
            {
                this.mC1124 = value;
            }
        }

        public int C1125
        {
            get
            {
                return this.mC1125;
            }
            set
            {
                this.mC1125 = value;
            }
        }

        public int C1126
        {
            get
            {
                return this.mC1126;
            }
            set
            {
                this.mC1126 = value;
            }
        }

        public int C1127
        {
            get
            {
                return this.mC1127;
            }
            set
            {
                this.mC1127 = value;
            }
        }

        public int C1128
        {
            get
            {
                return this.mC1128;
            }
            set
            {
                this.mC1128 = value;
            }
        }

        public int C1129
        {
            get
            {
                return this.mC1129;
            }
            set
            {
                this.mC1129 = value;
            }
        }

        public int C1130
        {
            get
            {
                return this.mC1130;
            }
            set
            {
                this.mC1130 = value;
            }
        }

        public int C1131
        {
            get
            {
                return this.mC1131;
            }
            set
            {
                this.mC1131 = value;
            }
        }

        public int C1132
        {
            get
            {
                return this.mC1132;
            }
            set
            {
                this.mC1132 = value;
            }
        }

        public int C1133
        {
            get
            {
                return this.mC1133;
            }
            set
            {
                this.mC1133 = value;
            }
        }

        public int C1134
        {
            get
            {
                return this.mC1134;
            }
            set
            {
                this.mC1134 = value;
            }
        }

        public int C1135
        {
            get
            {
                return this.mC1135;
            }
            set
            {
                this.mC1135 = value;
            }
        }

        public int C1136
        {
            get
            {
                return this.mC1136;
            }
            set
            {
                this.mC1136 = value;
            }
        }

        public int C1137
        {
            get
            {
                return this.mC1137;
            }
            set
            {
                this.mC1137 = value;
            }
        }

        public int C1138
        {
            get
            {
                return this.mC1138;
            }
            set
            {
                this.mC1138 = value;
            }
        }

        public int C1139
        {
            get
            {
                return this.mC1139;
            }
            set
            {
                this.mC1139 = value;
            }
        }

        public int C1140
        {
            get
            {
                return this.mC1140;
            }
            set
            {
                this.mC1140 = value;
            }
        }

        public int C1141
        {
            get
            {
                return this.mC1141;
            }
            set
            {
                this.mC1141 = value;
            }
        }

        public int C1142
        {
            get
            {
                return this.mC1142;
            }
            set
            {
                this.mC1142 = value;
            }
        }

        public int C1143
        {
            get
            {
                return this.mC1143;
            }
            set
            {
                this.mC1143 = value;
            }
        }

        public int C1144
        {
            get
            {
                return this.mC1144;
            }
            set
            {
                this.mC1144 = value;
            }
        }

        public int C1145
        {
            get
            {
                return this.mC1145;
            }
            set
            {
                this.mC1145 = value;
            }
        }

        public int C1146
        {
            get
            {
                return this.mC1146;
            }
            set
            {
                this.mC1146 = value;
            }
        }

        public int C1147
        {
            get
            {
                return this.mC1147;
            }
            set
            {
                this.mC1147 = value;
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

        public string[,] SubproductosC() //= new string[40];
        {
            String[,] array = new string[40,2];
            bool flag2 = this.mC119 == 1;
            if (flag2)
            {
                array[0, 0] = "Cambios sin afectación vida - (anexar formato 5)";
                array[0, 1] = "Corrección de nombre y apellidos";
            }
            bool flag3 = this.mC1110 == 1;
            if (flag3)
            {
                array[1, 0] = "Cambios sin afectación vida - (anexar formato 5)";
                array[1, 1] = "Cambio de contratante";
            }
            return array;
        }
        //private String[] mSubproductos = new string[40];
        public string CabeceraHtml
        {
            get
            {
                string text = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                text = text + "<tr><td style='width:150px'>Número trámite:</td><td><b>" + this.mIdTramite.ToString().PadLeft(5, '0') + "</b></td></tr>";
                text = text + "<tr><td>Número de POLIZA:</td><td><b>" + this.mNumPoliza + "</b></td></tr>";
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
                return text + "</table>";
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
                string text = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                text += "<tr><td colspan='2'>";
                text += "TRAMITES:</br></br> ";
                bool flag2 = this.mC119 == 1;
                if (flag2)
                {
                    text += "<b> - Corrección de nombre y apellidos</b></br>";
                }
                bool flag3 = this.mC1110 == 1;
                if (flag3)
                {
                    text += "<b> - Cambio de contratante</b></br>";
                }
                bool flag4 = this.mC1111 == 1;
                if (flag4)
                {
                    text += "<b> - Cambio de domicilio</b></br>";
                }
                bool flag5 = this.mC1112 == 1;
                if (flag5)
                {
                    text += "<b> - Corrección de Registro Federal de Contribuyentes</b></br>";
                }
                bool flag6 = this.mC1113 == 1;
                if (flag6)
                {
                    text += "<b> - Corrección de género</b></br>";
                }
                bool flag7 = this.mC1114 == 1;
                if (flag7)
                {
                    text += "<b> - Correccion de estado civil</b></br>";
                }
                bool flag8 = this.mC1115 == 1;
                if (flag8)
                {
                    text += "<b> - Actualización de Información Artículo 492 Ley de Instituciones de Seguro y de Fianzas</b></br>";
                }
                bool flag9 = this.mC1116 == 1;
                if (flag9)
                {
                    text += "<b> - Cambio de beneficiario</b></br>";
                }
                bool flag10 = this.mC1117 == 1;
                if (flag10)
                {
                    text += "<b> - Aclaración de estado de cuenta</b></br>";
                }
                bool flag11 = this.mC1118 == 1;
                if (flag11)
                {
                    text += "<b> - Carta estatus</b></br>";
                }
                bool flag12 = this.mC1119 == 1;
                if (flag12)
                {
                    text += "<b> - Duplicado de póliza</b></br>";
                }
                bool flag13 = this.mC1120 == 1;
                if (flag13)
                {
                    text += "<b> - Duplicado de endoso</b></br>";
                }
                bool flag14 = this.mC1121 == 1;
                if (flag14)
                {
                    text += "<b> - Cambio clave de agente</b></br>";
                }
                bool flag15 = this.mC1122 == 1;
                if (flag15)
                {
                    text += "<b> - Cambio de fecha de emisión</b></br>";
                }
                bool flag16 = this.mC1123 == 1;
                if (flag16)
                {
                    text += "<b> - Estado de cuenta por jubilación y capitalizable a corto plazo</b></br>";
                }
                bool flag17 = this.mC1124 == 1;
                if (flag17)
                {
                    text += "<b> - Rehabilitación</b></br>";
                }
                bool flag18 = this.mC1125 == 1;
                if (flag18)
                {
                    text += "<b> - Cambio de conducto de cobro a tarjeta de crédito y débito</b></br>";
                }
                bool flag19 = this.mC1126 == 1;
                if (flag19)
                {
                    text += "<b> - Cambio de conducto de cobro a CLABE Bancaria</b></br>";
                }
                bool flag20 = this.mC1127 == 1;
                if (flag20)
                {
                    text += "<b> - Cambio de conducto de cobro a agente</b></br>";
                }
                bool flag21 = this.mC1128 == 1;
                if (flag21)
                {
                    text += "<b> - Duplicado de recibo</b></br>";
                }
                bool flag22 = this.mC1129 == 1;
                if (flag22)
                {
                    text += "<b> - Cambio de moneda</b></br>";
                }
                bool flag23 = this.mC1130 == 1;
                if (flag23)
                {
                    text += "<b> - Cambio de suma asegurada</b></br>";
                }
                bool flag24 = this.mC1131 == 1;
                if (flag24)
                {
                    text += "<b> - Cambio de forma de pago</b></br>";
                }
                bool flag25 = this.mC1132 == 1;
                if (flag25)
                {
                    text += "<b> - Cambio de plan</b></br>";
                }
                bool flag26 = this.mC1133 == 1;
                if (flag26)
                {
                    text += "<b> - Corrección de edad / corrección de fecha de nacimiento</b></br>";
                }
                bool flag27 = this.mC1134 == 1;
                if (flag27)
                {
                    text += "<b> - Reconsideración de dictamen</b></br>";
                }
                bool flag28 = this.mC1135 == 1;
                if (flag28)
                {
                    text += "<b> - Inclusión o exclusión de extra primas</b></br>";
                }
                bool flag29 = this.mC1136 == 1;
                if (flag29)
                {
                    text += "<b> - Inclusión o exclusión de beneficios adicionales</b></br>";
                }
                bool flag30 = this.mC1137 == 1;
                if (flag30)
                {
                    text += "<b> - Inclusión o exclusión del beneficio de no fumador</b></br>";
                }
                bool flag31 = this.mC1138 == 1;
                if (flag31)
                {
                    text += "<b> - Inclusion de plan capitalizable corto plazo</b></br>";
                }
                bool flag32 = this.mC1139 == 1;
                if (flag32)
                {
                    text += "<b> - Cambio de seguro prorrogado</b></br>";
                }
                bool flag33 = this.mC1140 == 1;
                if (flag33)
                {
                    text += "<b> - Cambio de seguro saldado</b></br>";
                }
                bool flag34 = this.mC1141 == 1;
                if (flag34)
                {
                    text += "<b> - Otros</b></br>";
                }
                bool flag35 = this.mC1142 == 1;
                if (flag35)
                {
                    text += "<b> - Cancelación de póliza</b></br>";
                }
                bool flag36 = this.mC1143 == 1;
                if (flag36)
                {
                    text += "<b> - Rescate de fondo de pólizas de jubilacion y capitalizable a corto plazo para pago de prima-póliza de vida mismo Asegurado</b></br>";
                }
                bool flag37 = this.mC1144 == 1;
                if (flag37)
                {
                    text += "<b> - Rescate total</b></br>";
                }
                bool flag38 = this.mC1145 == 1;
                if (flag38)
                {
                    text += "<b> - Rescate parcial</b></br>";
                }
                bool flag39 = this.mC1146 == 1;
                if (flag39)
                {
                    text += "<b> - Devolución de primas</b></br>";
                }
                bool flag40 = this.mC1147 == 1;
                if (flag40)
                {
                    text += "<b> - Aclaración de pagos</b></br>";
                }
                text += "</td></tr>";
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
                //text = text + "CURP:<b>" + this.mCURP + "</b>";
                return text + "</p>";
            }
        }


        public string TextoHtml
        {
            get
            {
                string text = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                text = text + "<tr><td style ='width :120px'>Número de POLIZA:</td><td><b>" + this.mNumPoliza + "</b></td></tr>";
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
                    text = text + "<tr><td>CURP:</td><td><b>" + this.mCURP + "</b></td></tr>";
                }
                text += "<tr><td colspan='2'>";
                text += "</br></br>TRAMITES:</br></br> ";
                bool flag2 = this.mC119 == 1;
                if (flag2)
                {
                    text += "<b> - Corrección de nombre y apellidos</b></br>";
                }
                bool flag3 = this.mC1110 == 1;
                if (flag3)
                {
                    text += "<b> - Cambio de contratante</b></br>";
                }
                bool flag4 = this.mC1111 == 1;
                if (flag4)
                {
                    text += "<b> - Cambio de domicilio</b></br>";
                }
                bool flag5 = this.mC1112 == 1;
                if (flag5)
                {
                    text += "<b> - Corrección de Registro Federal de Contribuyentes</b></br>";
                }
                bool flag6 = this.mC1113 == 1;
                if (flag6)
                {
                    text += "<b> - Corrección de género</b></br>";
                }
                bool flag7 = this.mC1114 == 1;
                if (flag7)
                {
                    text += "<b> - Correccion de estado civil</b></br>";
                }
                bool flag8 = this.mC1115 == 1;
                if (flag8)
                {
                    text += "<b> - Actualización de Información Artículo 492 Ley de Instituciones de Seguro y de Fianzas</b></br>";
                }
                bool flag9 = this.mC1116 == 1;
                if (flag9)
                {
                    text += "<b> - Cambio de beneficiario</b></br>";
                }
                bool flag10 = this.mC1117 == 1;
                if (flag10)
                {
                    text += "<b> - Aclaración de estado de cuenta</b></br>";
                }
                bool flag11 = this.mC1118 == 1;
                if (flag11)
                {
                    text += "<b> - Carta estatus</b></br>";
                }
                bool flag12 = this.mC1119 == 1;
                if (flag12)
                {
                    text += "<b> - Duplicado de póliza</b></br>";
                }
                bool flag13 = this.mC1120 == 1;
                if (flag13)
                {
                    text += "<b> - Duplicado de endoso</b></br>";
                }
                bool flag14 = this.mC1121 == 1;
                if (flag14)
                {
                    text += "<b> - Cambio clave de agente</b></br>";
                }
                bool flag15 = this.mC1122 == 1;
                if (flag15)
                {
                    text += "<b> - Cambio de fecha de emisión</b></br>";
                }
                bool flag16 = this.mC1123 == 1;
                if (flag16)
                {
                    text += "<b> - Estado de cuenta por jubilación y capitalizable a corto plazo</b></br>";
                }
                bool flag17 = this.mC1124 == 1;
                if (flag17)
                {
                    text += "<b> - Rehabilitación</b></br>";
                }
                bool flag18 = this.mC1125 == 1;
                if (flag18)
                {
                    text += "<b> - Cambio de conducto de cobro a tarjeta de crédito y débito</b></br>";
                }
                bool flag19 = this.mC1126 == 1;
                if (flag19)
                {
                    text += "<b> - Cambio de conducto de cobro a CLABE Bancaria</b></br>";
                }
                bool flag20 = this.mC1127 == 1;
                if (flag20)
                {
                    text += "<b> - Cambio de conducto de cobro a agente</b></br>";
                }
                bool flag21 = this.mC1128 == 1;
                if (flag21)
                {
                    text += "<b> - Duplicado de recibo</b></br>";
                }
                bool flag22 = this.mC1129 == 1;
                if (flag22)
                {
                    text += "<b> - Cambio de moneda</b></br>";
                }
                bool flag23 = this.mC1130 == 1;
                if (flag23)
                {
                    text += "<b> - Cambio de suma asegurada</b></br>";
                }
                bool flag24 = this.mC1131 == 1;
                if (flag24)
                {
                    text += "<b> - Cambio de forma de pago</b></br>";
                }
                bool flag25 = this.mC1132 == 1;
                if (flag25)
                {
                    text += "<b> - Cambio de plan</b></br>";
                }
                bool flag26 = this.mC1133 == 1;
                if (flag26)
                {
                    text += "<b> - Corrección de edad / corrección de fecha de nacimiento</b></br>";
                }
                bool flag27 = this.mC1134 == 1;
                if (flag27)
                {
                    text += "<b> - Reconsideración de dictamen</b></br>";
                }
                bool flag28 = this.mC1135 == 1;
                if (flag28)
                {
                    text += "<b> - Inclusión o exclusión de extra primas</b></br>";
                }
                bool flag29 = this.mC1136 == 1;
                if (flag29)
                {
                    text += "<b> - Inclusión o exclusión de beneficios adicionales</b></br>";
                }
                bool flag30 = this.mC1137 == 1;
                if (flag30)
                {
                    text += "<b> - Inclusión o exclusión del beneficio de no fumador</b></br>";
                }
                bool flag31 = this.mC1138 == 1;
                if (flag31)
                {
                    text += "<b> - Inclusion de plan capitalizable corto plazo</b></br>";
                }
                bool flag32 = this.mC1139 == 1;
                if (flag32)
                {
                    text += "<b> - Cambio de seguro prorrogado</b></br>";
                }
                bool flag33 = this.mC1140 == 1;
                if (flag33)
                {
                    text += "<b> - Cambio de seguro saldado</b></br>";
                }
                bool flag34 = this.mC1141 == 1;
                if (flag34)
                {
                    text += "<b> - Otros</b></br>";
                }
                bool flag35 = this.mC1142 == 1;
                if (flag35)
                {
                    text += "<b> - Cancelación de póliza</b></br>";
                }
                bool flag36 = this.mC1143 == 1;
                if (flag36)
                {
                    text += "<b> - Rescate de fondo de pólizas de jubilacion y capitalizable a corto plazo para pago de prima-póliza de vida mismo Asegurado</b></br>";
                }
                bool flag37 = this.mC1144 == 1;
                if (flag37)
                {
                    text += "<b> - Rescate total</b></br>";
                }
                bool flag38 = this.mC1145 == 1;
                if (flag38)
                {
                    text += "<b> - Rescate parcial</b></br>";
                }
                bool flag39 = this.mC1146 == 1;
                if (flag39)
                {
                    text += "<b> - Devolución de primas</b></br>";
                }
                bool flag40 = this.mC1147 == 1;
                if (flag40)
                {
                    text += "<b> - Aclaración de pagos</b></br>";
                }
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
