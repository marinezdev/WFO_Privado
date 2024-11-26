using System;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admServiciosVida
    {
        public bool nuevo(serviciosVidaP pServicioVida)
        //public bool nuevo(serviciosVida pServicioVida)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into TRAM00001 (IdTramite,NumPoliza,Nombre,ApPaterno,ApMaterno,RFC,CURP,Nacionalidad,TipoPersona,ConAfectacion");
            SqlCmd.Append(",C119");
            SqlCmd.Append(",C1110");
            SqlCmd.Append(",C1111");
            SqlCmd.Append(",C1112");
            SqlCmd.Append(",C1113");
            SqlCmd.Append(",C1114");
            SqlCmd.Append(",C1115");
            SqlCmd.Append(",C1116");
            SqlCmd.Append(",C1117");
            SqlCmd.Append(",C1118");
            SqlCmd.Append(",C1119");
            SqlCmd.Append(",C1120");
            SqlCmd.Append(",C1121");
            SqlCmd.Append(",C1122");
            SqlCmd.Append(",C1123");
            SqlCmd.Append(",C1124");
            SqlCmd.Append(",C1125");
            SqlCmd.Append(",C1126");
            SqlCmd.Append(",C1127");
            SqlCmd.Append(",C1128");
            SqlCmd.Append(",C1129");
            SqlCmd.Append(",C1130");
            SqlCmd.Append(",C1131");
            SqlCmd.Append(",C1132");
            SqlCmd.Append(",C1133");
            SqlCmd.Append(",C1134");
            SqlCmd.Append(",C1135");
            SqlCmd.Append(",C1136");
            SqlCmd.Append(",C1137");
            SqlCmd.Append(",C1138");
            SqlCmd.Append(",C1139");
            SqlCmd.Append(",C1140");
            SqlCmd.Append(",C1141");
            SqlCmd.Append(",C1142");
            SqlCmd.Append(",C1143");
            SqlCmd.Append(",C1144");
            SqlCmd.Append(",C1145");
            SqlCmd.Append(",C1146");
            SqlCmd.Append(",C1147");

            
            SqlCmd.Append(",CPDES");
            SqlCmd.Append(",FolioCPDES");
            SqlCmd.Append(",EstatusCPDES");
            SqlCmd.Append(",Detalle");
            SqlCmd.Append(") Values(");
            SqlCmd.Append(pServicioVida.IdTramite);
            SqlCmd.Append(",'" + pServicioVida.NumPoliza + "'");
            SqlCmd.Append(",'" + pServicioVida.Nombre + "'");
            SqlCmd.Append(",'" + pServicioVida.ApPaterno + "'");
            SqlCmd.Append(",'" + pServicioVida.ApMaterno + "'");
            SqlCmd.Append(",'" + pServicioVida.RFC + "'");
            SqlCmd.Append(",'" + pServicioVida.CURP + "'");
            SqlCmd.Append(",'" + pServicioVida.Nacionalidad + "'");
            SqlCmd.Append("," + pServicioVida.TipoPersona.ToString("d"));
            SqlCmd.Append("," + pServicioVida.ConAfectacion.ToString ());
            SqlCmd.Append("," + pServicioVida.C119);
            SqlCmd.Append("," + pServicioVida.C1110);
            SqlCmd.Append("," + pServicioVida.C1111);
            SqlCmd.Append("," + pServicioVida.C1112);
            SqlCmd.Append("," + pServicioVida.C1113);
            SqlCmd.Append("," + pServicioVida.C1114);
            SqlCmd.Append("," + pServicioVida.C1115);
            SqlCmd.Append("," + pServicioVida.C1116);
            SqlCmd.Append("," + pServicioVida.C1117);
            SqlCmd.Append("," + pServicioVida.C1118);
            SqlCmd.Append("," + pServicioVida.C1119);
            SqlCmd.Append("," + pServicioVida.C1120);
            SqlCmd.Append("," + pServicioVida.C1121);
            SqlCmd.Append("," + pServicioVida.C1122);
            SqlCmd.Append("," + pServicioVida.C1123);
            SqlCmd.Append("," + pServicioVida.C1124);
            SqlCmd.Append("," + pServicioVida.C1125);
            SqlCmd.Append("," + pServicioVida.C1126);
            SqlCmd.Append("," + pServicioVida.C1127);
            SqlCmd.Append("," + pServicioVida.C1128);
            SqlCmd.Append("," + pServicioVida.C1129);
            SqlCmd.Append("," + pServicioVida.C1130);
            SqlCmd.Append("," + pServicioVida.C1131);
            SqlCmd.Append("," + pServicioVida.C1132);
            SqlCmd.Append("," + pServicioVida.C1133);
            SqlCmd.Append("," + pServicioVida.C1134);
            SqlCmd.Append("," + pServicioVida.C1135);
            SqlCmd.Append("," + pServicioVida.C1136);
            SqlCmd.Append("," + pServicioVida.C1137);
            SqlCmd.Append("," + pServicioVida.C1138);
            SqlCmd.Append("," + pServicioVida.C1139);
            SqlCmd.Append("," + pServicioVida.C1140);
            SqlCmd.Append("," + pServicioVida.C1141);
            SqlCmd.Append("," + pServicioVida.C1142);
            SqlCmd.Append("," + pServicioVida.C1143);
            SqlCmd.Append("," + pServicioVida.C1144);
            SqlCmd.Append("," + pServicioVida.C1145);
            SqlCmd.Append("," + pServicioVida.C1146);
            SqlCmd.Append("," + pServicioVida.C1147);
            SqlCmd.Append(",'" + pServicioVida.CPDES + "'");
            SqlCmd.Append(",'" + pServicioVida.FolioCPDES + "'");
            SqlCmd.Append(",'" + pServicioVida.EstatusCPDES + "'");
            SqlCmd.Append(",'" + pServicioVida.Detalle + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            if(resultado)
            {
                
                String P1 = "Cambios sin afectación vida - (anexar formato 5)";
                String P2 = "Cambios sin afectación vida";
                String P3 = "Rehabilitación";
                String P4 = "Cambio de conducto de cobro";
                String P5 = "Duplicado de recibo";
                String P6 = "Cambios con afectación vida";
                String P7 = "Rescates, retiros y cancelaciones";
                String P8 = "Aclaración de pagos";
                String SP1 = ""; String SP2 = ""; String SP3 = ""; String SP4 = ""; String SP5 = ""; String SP6 = ""; String SP7 = ""; String SP8 = ""; String SP9 = ""; String SP10 = ""; String SP11 = ""; String SP12 = ""; String SP13 = ""; String SP14 = ""; String SP15 = ""; String SP16 = ""; String SP17 = ""; String SP18 = "";
                String SP19 = ""; String SP20 = ""; String SP21 = ""; String SP22 = ""; String SP23 = ""; String SP24 = ""; String SP25 = ""; String SP26 = ""; String SP27 = ""; String SP28 = ""; String SP29 = ""; String SP30 = ""; String SP31 = ""; String SP32 = ""; String SP33 = ""; String SP34 = ""; String SP35 = "";
                String SP36 = ""; String SP37 = ""; String SP38 = ""; String SP39 = "";

                /////// P1
                if (pServicioVida.C119 == 1)
                {
                    SP1 = "Corrección de nombre y apellidos";
                }
                if (pServicioVida.C1110 == 1)
                {
                    SP2 = "Cambio de contratante";
                }
                if (pServicioVida.C1111 == 1)
                {
                    SP3 = "Cambio de domicilio";
                }
                if (pServicioVida.C1112 == 1)
                {
                    SP4 = "Corrección de registro federal de contribuyentes";
                }
                if (pServicioVida.C1113 == 1)
                {
                    SP5 = "Corrección de género";
                }
                if (pServicioVida.C1114 == 1)
                {
                    SP6 = "Corrección de estado civil";
                }
                if (pServicioVida.C1115 == 1)
                {
                    SP7 = "Actualización de información artículo 492 ley de instituciones de seguro y de fianzas";
                }
                //////////////////////////// P2
                if (pServicioVida.C1116 == 1)
                {
                    SP8 = "Cambio de beneficiario (anexar formato de identificación de beneficiarios)";
                }
                if (pServicioVida.C1117 == 1)
                {
                    SP9 = "Aclaración de estado de cuenta";
                }
                if (pServicioVida.C1118 == 1)
                {
                    SP10 = "Carta estatus";
                }
                if (pServicioVida.C1119 == 1)
                {
                    SP11 = "Duplicado de póliza";
                }
                if (pServicioVida.C1120 == 1)
                {
                    SP12 = "Duplicado de endoso";
                }
                if (pServicioVida.C1121 == 1)
                {
                    SP13 = "Cambio clave de agente";
                }
                if (pServicioVida.C1122 == 1)
                {
                    SP14 = "Cambio de fecha de emisión";
                }
                if (pServicioVida.C1123 == 1)
                {
                    SP15 = "Estado de cuenta por jubilación y capitalizable a corto plazo";
                }
                /////////////////// P3
                if (pServicioVida.C1124 == 1)
                {
                    SP16 = "Rehabilitación";
                }
                /////////////////// P4
                if (pServicioVida.C1125 == 1)
                {
                    SP17 = "Cambio de conducto de cobro a tarjeta de crédito y débito";
                }
                if (pServicioVida.C1126 == 1)
                {
                    SP18 = "Cambio de conducto de cobro a CLABE Bancaria";
                }
                if (pServicioVida.C1127 == 1)
                {
                    SP19 = "Cambio de conducto de cobro a agente";
                }
                //////////////////// P5
                if (pServicioVida.C1128 == 1)
                {
                    SP20 = "Duplicado de recibo";
                }
                //////////////////// P6
                if (pServicioVida.C1129 == 1)
                {
                    SP21 = "Cambio de moneda";
                }
                if (pServicioVida.C1130 == 1)
                {
                    SP22 = "Cambio de suma asegurada";
                }
                if (pServicioVida.C1131 == 1)
                {
                    SP23 = "Cambio de forma de pago";
                }
                if (pServicioVida.C1132 == 1)
                {
                    SP24 = "Cambio de plan";
                }
                if (pServicioVida.C1133 == 1)
                {
                    SP25 = "Corrección de edad / corrección de fecha de nacimiento";
                }
                if (pServicioVida.C1134 == 1)
                {
                    SP26 = "Reconsideración de dictamen";
                }
                if (pServicioVida.C1135 == 1)
                {
                    SP27 = "Inclusión o exclusión de extra primas";
                }
                if (pServicioVida.C1136 == 1)
                {
                    SP28 = "Inclusión o exclusión de beneficios adicionales";
                }
                if (pServicioVida.C1137 == 1)
                {
                    SP29 = "Inclusión o exclusión del beneficio de no fumador";
                }
                if (pServicioVida.C1138 == 1)
                {
                    SP30 = "Inclusión de plan capitalizable corto plazo";
                }
                if (pServicioVida.C1139 == 1)
                {
                    SP31 = "Cambio de seguro prorrogado";
                }
                if (pServicioVida.C1140 == 1)
                {
                    SP32 = "Cambio de seguro saldado";
                }
                if (pServicioVida.C1141 == 1)
                {
                    SP33 = "Otros";
                }
                /////////////////////// P7
                if (pServicioVida.C1142 == 1)
                {
                    SP34 = "Cancelación de póliza";
                }
                if (pServicioVida.C1143 == 1)
                {
                    SP35 = "Rescate de fondo de pólizas de jubilación y capitalizable a corto plazo para pago de prima-póliza de vida mismo Asegurado";
                }
                if (pServicioVida.C1144 == 1)
                {
                    SP36 = "Rescate total";
                }
                if (pServicioVida.C1145 == 1)
                {
                    SP37 = "Rescate parcial";
                }
                /////////////////////// P8
                if (pServicioVida.C1146 == 1)
                {
                    SP38 = "Devolución de primas";
                }
                if (pServicioVida.C1147 == 1)
                {
                    SP39 = "Aclaración de pagos";
                }

                
                StringBuilder SqlCmd2 = new StringBuilder("EXEC [dbo].[InsertaProductos] @Tramite ='" + pServicioVida.IdTramite.ToString() + "',@TipoTramite='1'," +
                    "@P1 ='" + P1 + "',@SP1='" + SP1 + "'," +
                    "@P2 ='" + P1 + "',@SP2='" + SP2 + "'," +
                    "@P3 ='" + P1 + "',@SP3='" + SP3 + "'," +
                    "@P4 ='" + P1 + "',@SP4='" + SP4 + "'," +
                    "@P5 ='" + P1 + "',@SP5='" + SP5 + "'," +
                    "@P6 ='" + P1 + "',@SP6='" + SP6 + "'," +
                    "@P7 ='" + P1 + "',@SP7='" + SP7 + "'," +

                    "@P8 ='" + P2 + "',@SP8='" + SP8 + "'," +
                    "@P9 ='" + P2 + "',@SP9='" + SP9 + "'," +
                    "@P10 ='" + P2 + "',@SP10='" + SP10 + "'," +
                    "@P11 ='" + P2 + "', @SP11='" + SP11 + "'," +
                    "@P12 ='" + P2 + "', @SP12 = '" + SP12 + "'," +
                    "@P13 = '" + P2 + "',@SP13 = '" + SP13 + "'," +
                    "@P14 = '" + P2 + "',@SP14 = '" + SP14 + "'," +
                    "@P15 = '" + P2 + "',@SP15 = '" + SP15 + "'," +

                    "@P16 = '" + P3 + "',@SP16 = '" + SP16 + "'," +

                    "@P17 = '" + P4 + "',@SP17 = '" + SP17 + "'," +
                    "@P18 = '" + P4 + "',@SP18 = '" + SP18 + "'," +
                    "@P19 = '" + P4 + "',@SP19 = '" + SP19 + "'," +

                    "@P20 = '" + P5 + "',@SP20 = '" + SP20 + "'," +

                    "@P21 = '" + P6 + "',@SP21 = '" + SP21 + "'," +
                    "@P22 = '" + P6 + "',@SP22 = '" + SP22 + "'," +
                    "@P23 = '" + P6 + "',@SP23 = '" + SP23 + "'," +
                    "@P24 = '" + P6 + "',@SP24 = '" + SP24 + "'," +
                    "@P25 = '" + P6 + "',@SP25 = '" + SP25 + "'," +
                    "@P26 = '" + P6 + "',@SP26 = '" + SP26 + "'," +
                    "@P27 = '" + P6 + "',@SP27 = '" + SP27 + "'," +
                    "@P28 = '" + P6 + "',@SP28 = '" + SP28 + "'," +
                    "@P29 = '" + P6 + "',@SP29 = '" + SP29 + "'," +
                    "@P30 = '" + P6 + "',@SP30 = '" + SP30 + "'," +
                    "@P31 = '" + P6 + "',@SP31 = '" + SP31 + "'," +
                    "@P32 = '" + P6 + "',@SP32 = '" + SP32 + "'," +
                    "@P33 = '" + P6 + "',@SP33 = '" + SP33 + "'," +

                    "@P34 = '" + P7 + "',@SP34 = '" + SP34 + "'," +
                    "@P35 = '" + P7 + "',@SP35 = '" + SP35 + "'," +
                    "@P36 = '" + P7 + "',@SP36 = '" + SP36 + "'," +
                    "@P37 = '" + P7 + "',@SP37 = '" + SP37 + "'," +

                    "@P38 = '" + P8 + "',@SP38 = '" + SP38 + "'," +
                    "@P39 = '" + P8 + "',@SP39 = '" + SP39 + "'");
                
                resultado = BD.ejecutaCmd(SqlCmd2.ToString());
            }

            BD.cierraBD();
            return resultado;
        }

        public serviciosVidaP carga(int pId)
        {
            serviciosVidaP resultado = null;
            String SqlCmd = "SELECT * FROM TRAM00001 WHERE IdTramite = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public DataTable Checks(string IdTipoTramite,string P1, string SP1, string P2, string SP2,string P3, string SP3, string P4, string SP4, string P5, string SP5, string P6, string SP6, string P7, string SP7, string P8, string SP8, string P9, string SP9, string P10, string SP10, string P11, string SP11, string P12, string SP12, string P13, string SP13, string P14, string SP14, string P15, string SP15, string P16, string SP16, string P17, string SP17, string P18, string SP18, string P19, string SP19, string P20, string SP20, string P21, string SP21, string P22, string SP22, string P23, string SP23, string P24, string SP24, string P25, string SP25, string P26, string SP26, string P27, string SP27, string P28, string SP28, string P29, string SP29, string P30, string SP30, string P31, string SP31, string P32, string SP32, string P33, string SP33, string P34, string SP34, string P35, string SP35, string P36, string SP36, string P37, string SP37, string P38, string SP38, string P39, string SP39)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM RetornaProductos ('"+ 
                IdTipoTramite.ToString() + "','"+ 
                P1.ToString() + "','" + 
                SP1.ToString() + "','"+ 
                P2.ToString() + "','" + 
                SP2.ToString() + "','" + 
                P3.ToString() + "','" + 
                SP3.ToString() + "','" + 
                P4.ToString() + "','" + 
                SP4.ToString() + "','" + 
                P5.ToString() + "','" + 
                SP5.ToString() + "','" + 
                P6.ToString() + "','" + 
                SP6.ToString() + "','" + 
                P7.ToString() + "','" + 
                SP7.ToString() + "','" + 
                P8.ToString() + "','" + 
                SP8.ToString() + "','" + 
                P9.ToString() + "','" + 
                SP9.ToString() + "','" + 
                P10.ToString() + "','" + 
                SP10.ToString() + "','" + 
                P11.ToString() + "','" + 
                SP11.ToString() + "','" + 
                P12.ToString() + "','" + 
                SP12.ToString() + "','" + 

                P13.ToString() + "','" + 
                SP13.ToString() + "','" + 

                P14.ToString() + "','" + 
                SP14.ToString() + "','" + 

                P15.ToString() + "','" + 
                SP15.ToString() + "','" + 

                P16.ToString() + "','" + 
                SP16.ToString() + "','" + 

                P17.ToString() + "','" +
                SP17.ToString() + "','" +

                P18.ToString() + "','" +
                SP18.ToString() + "','" +

                P19.ToString() + "','" +
                SP19.ToString() + "','" +

                P20.ToString() + "','" +
                SP20.ToString() + "','" +

                P21.ToString() + "','" +
                SP21.ToString() + "','" +

                P22.ToString() + "','" +
                SP22.ToString() + "','" +

                P23.ToString() + "','" +
                SP23.ToString() + "','" +

                P24.ToString() + "','" +
                SP24.ToString() + "','" +

                P25.ToString() + "','" +
                SP25.ToString() + "','" +

                P26.ToString() + "','" +
                SP26.ToString() + "','" +

                P27.ToString() + "','" +
                SP27.ToString() + "','" +

                P28.ToString() + "','" +
                SP28.ToString() + "','" +

                P29.ToString() + "','" +
                SP29.ToString() + "','" +

                P30.ToString() + "','" +
                SP30.ToString() + "','" +

                P31.ToString() + "','" +
                SP31.ToString() + "','" +

                P32.ToString() + "','" +
                SP32.ToString() + "','" +

                P33.ToString() + "','" +
                SP33.ToString() + "','" +

                P34.ToString() + "','" +
                SP34.ToString() + "','" +

                P35.ToString() + "','" +
                SP35.ToString() + "','" +

                P36.ToString() + "','" +
                SP36.ToString() + "','" +

                P37.ToString() + "','" +
                SP37.ToString() + "','" +

                P38.ToString() + "','" +
                SP38.ToString() + "','" +

                P39.ToString() + "','" +
                SP39.ToString() + "')");

            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public serviciosVidaP cargaFolio(int pId)
        {
            serviciosVidaP resultado = null;
            String SqlCmd = "SELECT * FROM dbo.Folio WHERE dbo.Folio.IdTramite = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = armaFolio(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private serviciosVidaP armaFolio(DataRow pRegistro)
        {
            serviciosVidaP resultado = new serviciosVidaP();
            if (!pRegistro.IsNull("FolioCompuesto")) resultado.Folio = Convert.ToString(pRegistro["FolioCompuesto"]);
            return resultado;
        }

       
        public serviciosVidaP cargaProdructos(int pId)
        {
            serviciosVidaP resultado = null;
            String SqlCmd = "SELECT * FROM dbo.Producto WHERE dbo.Producto.IdTramite = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = armaProductos(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private serviciosVidaP armaProductos(DataRow pRegistro)
        {
            serviciosVidaP resultado = new serviciosVidaP();
            //if (!pRegistro.IsNull("idProducto")) resultado.Producto = Convert.ToString(pRegistro["idProducto"]);
            //if (!pRegistro.IsNull("IdTramite")) resultado.Producto = Convert.ToString(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("Producto")) resultado.Producto = Convert.ToString(pRegistro["Producto"]);
            if (!pRegistro.IsNull("Subproducto")) resultado.Subproducto = Convert.ToString(pRegistro["Subproducto"]);
            return resultado;
        }
        

        private serviciosVidaP arma(DataRow pRegistro)
        {
            serviciosVidaP resultado = new serviciosVidaP();
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("NumPoliza")) resultado.NumPoliza = Convert.ToString(pRegistro["NumPoliza"]);
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("ApPaterno")) resultado.ApPaterno = Convert.ToString(pRegistro["ApPaterno"]);
            if (!pRegistro.IsNull("ApMaterno")) resultado.ApMaterno = Convert.ToString(pRegistro["ApMaterno"]);
            if (!pRegistro.IsNull("RFC")) resultado.RFC = Convert.ToString(pRegistro["RFC"]);
            if (!pRegistro.IsNull("CURP")) resultado.CURP = Convert.ToString(pRegistro["CURP"]);
            if (!pRegistro.IsNull("TipoPersona")) resultado.TipoPersona = (E_TipoPersona)(pRegistro["TipoPersona"]);
            if (!pRegistro.IsNull("ConAfectacion")) resultado.ConAfectacion = Convert.ToInt32(pRegistro["ConAfectacion"]);
            if (!pRegistro.IsNull("C119")) resultado.C119 = Convert.ToInt32(pRegistro["C119"]);
            if (!pRegistro.IsNull("C1110")) resultado.C1110 = Convert.ToInt32(pRegistro["C1110"]);
            if (!pRegistro.IsNull("C1111")) resultado.C1111 = Convert.ToInt32(pRegistro["C1111"]);
            if (!pRegistro.IsNull("C1112")) resultado.C1112 = Convert.ToInt32(pRegistro["C1112"]);
            if (!pRegistro.IsNull("C1113")) resultado.C1113 = Convert.ToInt32(pRegistro["C1113"]);
            if (!pRegistro.IsNull("C1114")) resultado.C1114 = Convert.ToInt32(pRegistro["C1114"]);
            if (!pRegistro.IsNull("C1115")) resultado.C1115 = Convert.ToInt32(pRegistro["C1115"]);
            if (!pRegistro.IsNull("C1116")) resultado.C1116 = Convert.ToInt32(pRegistro["C1116"]);
            if (!pRegistro.IsNull("C1117")) resultado.C1117 = Convert.ToInt32(pRegistro["C1117"]);
            if (!pRegistro.IsNull("C1118")) resultado.C1118 = Convert.ToInt32(pRegistro["C1118"]);
            if (!pRegistro.IsNull("C1119")) resultado.C1119 = Convert.ToInt32(pRegistro["C1119"]);
            if (!pRegistro.IsNull("C1120")) resultado.C1120 = Convert.ToInt32(pRegistro["C1120"]);
            if (!pRegistro.IsNull("C1121")) resultado.C1121 = Convert.ToInt32(pRegistro["C1121"]);
            if (!pRegistro.IsNull("C1122")) resultado.C1122 = Convert.ToInt32(pRegistro["C1122"]);
            if (!pRegistro.IsNull("C1123")) resultado.C1123 = Convert.ToInt32(pRegistro["C1123"]);
            if (!pRegistro.IsNull("C1124")) resultado.C1124 = Convert.ToInt32(pRegistro["C1124"]);
            if (!pRegistro.IsNull("C1125")) resultado.C1125 = Convert.ToInt32(pRegistro["C1125"]);
            if (!pRegistro.IsNull("C1126")) resultado.C1126 = Convert.ToInt32(pRegistro["C1126"]);
            if (!pRegistro.IsNull("C1127")) resultado.C1127 = Convert.ToInt32(pRegistro["C1127"]);
            if (!pRegistro.IsNull("C1128")) resultado.C1128 = Convert.ToInt32(pRegistro["C1128"]);
            if (!pRegistro.IsNull("C1129")) resultado.C1129 = Convert.ToInt32(pRegistro["C1129"]);
            if (!pRegistro.IsNull("C1130")) resultado.C1130 = Convert.ToInt32(pRegistro["C1130"]);
            if (!pRegistro.IsNull("C1131")) resultado.C1131 = Convert.ToInt32(pRegistro["C1131"]);
            if (!pRegistro.IsNull("C1132")) resultado.C1132 = Convert.ToInt32(pRegistro["C1132"]);
            if (!pRegistro.IsNull("C1133")) resultado.C1133 = Convert.ToInt32(pRegistro["C1133"]);
            if (!pRegistro.IsNull("C1134")) resultado.C1134 = Convert.ToInt32(pRegistro["C1134"]);
            if (!pRegistro.IsNull("C1135")) resultado.C1135 = Convert.ToInt32(pRegistro["C1135"]);
            if (!pRegistro.IsNull("C1136")) resultado.C1136 = Convert.ToInt32(pRegistro["C1136"]);
            if (!pRegistro.IsNull("C1137")) resultado.C1137 = Convert.ToInt32(pRegistro["C1137"]);
            if (!pRegistro.IsNull("C1138")) resultado.C1138 = Convert.ToInt32(pRegistro["C1138"]);
            if (!pRegistro.IsNull("C1139")) resultado.C1139 = Convert.ToInt32(pRegistro["C1139"]);
            if (!pRegistro.IsNull("C1140")) resultado.C1140 = Convert.ToInt32(pRegistro["C1140"]);
            if (!pRegistro.IsNull("C1141")) resultado.C1141 = Convert.ToInt32(pRegistro["C1141"]);
            if (!pRegistro.IsNull("C1142")) resultado.C1142 = Convert.ToInt32(pRegistro["C1142"]);
            if (!pRegistro.IsNull("C1143")) resultado.C1143 = Convert.ToInt32(pRegistro["C1143"]);
            if (!pRegistro.IsNull("C1144")) resultado.C1144 = Convert.ToInt32(pRegistro["C1144"]);
            if (!pRegistro.IsNull("C1145")) resultado.C1145 = Convert.ToInt32(pRegistro["C1145"]);
            if (!pRegistro.IsNull("C1146")) resultado.C1146 = Convert.ToInt32(pRegistro["C1146"]);
            if (!pRegistro.IsNull("C1147")) resultado.C1147 = Convert.ToInt32(pRegistro["C1147"]);

            if (!pRegistro.IsNull("Detalle")) resultado.Detalle = Convert.ToString(pRegistro["Detalle"]);
            return resultado;
        }

        public bool ExistenTramitesparaRFC(string pRFC){
            bool respuesta = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM TRAM00001 WHERE Rfc='" + pRFC +"'");
            if (datos.Rows.Count > 0) { respuesta = true; }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public bool elimina(int pIdTramite)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("DELETE From TRAM00001 WHERE IdTramite=" + pIdTramite.ToString());
            BD.cierraBD();
            return resultado;
        }
    }

    [Serializable]
    public class serviciosVida
    {
        private int mIdTramite = 0;
        public int IdTramite { get { return mIdTramite; } set { mIdTramite = value; } }
        private E_TipoPersona mTipoPersona = E_TipoPersona.Fisica;
        public E_TipoPersona TipoPersona { get { return mTipoPersona; } set { mTipoPersona = value; } }
        private string mNumPoliza = "";
        public string NumPoliza { get { return mNumPoliza; } set { mNumPoliza = value; } }
        private string mNombre = "";
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mApPaterno = "";
        public string ApPaterno { get { return mApPaterno; } set { mApPaterno = value; } }
        private string mApMaterno = "";
        public string ApMaterno { get { return mApMaterno; } set { mApMaterno = value; } }
        private string mRFC = "";
        public string RFC { get { return mRFC; } set { mRFC = value; } }
        private string mCURP = "";
        public string CURP { get { return mCURP; } set { mCURP = value; } }
        private string mFolio = "";
        public string Folio { get { return mFolio; } set { mFolio = value; } }

        private string mProducto = "";
        public string Producto { get { return mProducto; } set { mProducto = value; } }
        private string mSubproducto = "";
        public string Subproducto { get { return mSubproducto; } set { mSubproducto = value; } }
        private int mConAfectacion = 0;
        public int ConAfectacion { get { return mConAfectacion; } set { mConAfectacion = value; } }
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

        public int C119 { get { return mC119; } set { mC119 = value; } }
        public int C1110 { get { return mC1110; } set { mC1110 = value; } }
        public int C1111 { get { return mC1111; } set { mC1111 = value; } }
        public int C1112 { get { return mC1112; } set { mC1112 = value; } }
        public int C1113 { get { return mC1113; } set { mC1113 = value; } }
        public int C1114 { get { return mC1114; } set { mC1114 = value; } }
        public int C1115 { get { return mC1115; } set { mC1115 = value; } }
        public int C1116 { get { return mC1116; } set { mC1116 = value; } }
        public int C1117 { get { return mC1117; } set { mC1117 = value; } }
        public int C1118 { get { return mC1118; } set { mC1118 = value; } }
        public int C1119 { get { return mC1119; } set { mC1119 = value; } }
        public int C1120 { get { return mC1120; } set { mC1120 = value; } }
        public int C1121 { get { return mC1121; } set { mC1121 = value; } }
        public int C1122 { get { return mC1122; } set { mC1122 = value; } }
        public int C1123 { get { return mC1123; } set { mC1123 = value; } }
        public int C1124 { get { return mC1124; } set { mC1124 = value; } }
        public int C1125 { get { return mC1125; } set { mC1125 = value; } }
        public int C1126 { get { return mC1126; } set { mC1126 = value; } }
        public int C1127 { get { return mC1127; } set { mC1127 = value; } }
        public int C1128 { get { return mC1128; } set { mC1128 = value; } }
        public int C1129 { get { return mC1129; } set { mC1129 = value; } }
        public int C1130 { get { return mC1130; } set { mC1130 = value; } }
        public int C1131 { get { return mC1131; } set { mC1131 = value; } }
        public int C1132 { get { return mC1132; } set { mC1132 = value; } }
        public int C1133 { get { return mC1133; } set { mC1133 = value; } }
        public int C1134 { get { return mC1134; } set { mC1134 = value; } }
        public int C1135 { get { return mC1135; } set { mC1135 = value; } }
        public int C1136 { get { return mC1136; } set { mC1136 = value; } }
        public int C1137 { get { return mC1137; } set { mC1137 = value; } }
        public int C1138 { get { return mC1138; } set { mC1138 = value; } }
        public int C1139 { get { return mC1139; } set { mC1139 = value; } }
        public int C1140 { get { return mC1140; } set { mC1140 = value; } }
        public int C1141 { get { return mC1141; } set { mC1141 = value; } }
        public int C1142 { get { return mC1142; } set { mC1142 = value; } }
        public int C1143 { get { return mC1143; } set { mC1143 = value; } }
        public int C1144 { get { return mC1144; } set { mC1144 = value; } }
        public int C1145 { get { return mC1145; } set { mC1145 = value; } }
        public int C1146 { get { return mC1146; } set { mC1146 = value; } }
        public int C1147 { get { return mC1147; } set { mC1147 = value; } }

        private string mDetalle = "";
        public string Detalle { get { return mDetalle; } set { mDetalle = value; } }

        public string CabeceraHtml
        {
            get
            {
                string cadena = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                cadena += "<tr><td style='width:150px'>Número trámite:</td><td><b>" + mIdTramite.ToString().PadLeft(5, '0') + "</b></td></tr>";
                cadena += "<tr><td>Número de POLIZA:</td><td><b>" + mNumPoliza + "</b></td></tr>";
                cadena += "<tr><td>Nombre(s):</td><td><b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></td></tr>";
                cadena += "</table>";
                return cadena;
            }
        }

        public string DatosHtml
        {
            get
            {
                string cadena = "<p>";
                cadena += "Nombre(s):<b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></br>";
                cadena += "RFC:<b>" + mRFC + "</b></br>";
                //cadena += "CURP:<b>" + mCURP + "</b>";
                cadena += "</p>";
                return cadena;
            }
        }

        public string TextoHtml
        {
            get
            {
                string cadena = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                cadena += "<tr><td style ='width :120px'>Número de POLIZA:</td><td><b>" + mNumPoliza + "</b></td></tr>";
                cadena += "<tr><td>Nombre(s):</td><td><b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></td></tr>";
                cadena += "<tr><td>RFC:</td><td><b>" + mRFC + "</b></td></tr>";
                if (mTipoPersona.Equals(E_TipoPersona.Fisica)) { 
                    cadena += "<tr><td>CURP:</td><td><b>" + mCURP + "</b></td></tr>";
                }

                cadena += "<tr><td colspan='2'>";
                    cadena += "</br></br>TRAMITES:</br></br> ";
                    if (mC119 == 1) cadena += "<b> - Corrección de nombre y apellidos</b></br>";
                    if (mC1110 == 1) cadena += "<b> - Cambio de contratante</b></br>";
                    if (mC1111 == 1) cadena += "<b> - Cambio de domicilio</b></br>";
                    if (mC1112 == 1) cadena += "<b> - Corrección de Registro Federal de Contribuyentes</b></br>";
                    if (mC1113 == 1) cadena += "<b> - Corrección de género</b></br>";
                    if (mC1114 == 1) cadena += "<b> - Correccion de estado civil</b></br>";
                    if (mC1115 == 1) cadena += "<b> - Actualización de Información Artículo 492 Ley de Instituciones de Seguro y de Fianzas</b></br>";
                    if (mC1116 == 1) cadena += "<b> - Cambio de beneficiario</b></br>";
                    if (mC1117 == 1) cadena += "<b> - Aclaración de estado de cuenta</b></br>";
                    if (mC1118 == 1) cadena += "<b> - Carta estatus</b></br>";
                    if (mC1119 == 1) cadena += "<b> - Duplicado de póliza</b></br>";
                    if (mC1120 == 1) cadena += "<b> - Duplicado de endoso</b></br>";
                    if (mC1121 == 1) cadena += "<b> - Cambio clave de agente</b></br>";
                    if (mC1122 == 1) cadena += "<b> - Cambio de fecha de emisión</b></br>";
                    if (mC1123 == 1) cadena += "<b> - Estado de cuenta por jubilación y capitalizable a corto plazo</b></br>";
                    if (mC1124 == 1) cadena += "<b> - Rehabilitación</b></br>";
                    if (mC1125 == 1) cadena += "<b> - Cambio de conducto de cobro a tarjeta de crédito y débito</b></br>";
                    if (mC1126 == 1) cadena += "<b> - Cambio de conducto de cobro a CLABE Bancaria</b></br>";
                    if (mC1127 == 1) cadena += "<b> - Cambio de conducto de cobro a agente</b></br>";
                    if (mC1128 == 1) cadena += "<b> - Duplicado de recibo</b></br>";
                    if (mC1129 == 1) cadena += "<b> - Cambio de moneda</b></br>";
                    if (mC1130 == 1) cadena += "<b> - Cambio de suma asegurada</b></br>";
                    if (mC1131 == 1) cadena += "<b> - Cambio de forma de pago</b></br>";
                    if (mC1132 == 1) cadena += "<b> - Cambio de plan</b></br>";
                    if (mC1133 == 1) cadena += "<b> - Corrección de edad / corrección de fecha de nacimiento</b></br>";
                    if (mC1134 == 1) cadena += "<b> - Reconsideración de dictamen</b></br>";
                    if (mC1135 == 1) cadena += "<b> - Inclusión o exclusión de extra primas</b></br>";
                    if (mC1136 == 1) cadena += "<b> - Inclusión o exclusión de beneficios adicionales</b></br>";
                    if (mC1137 == 1) cadena += "<b> - Inclusión o exclusión del beneficio de no fumador</b></br>";
                    if (mC1138 == 1) cadena += "<b> - Inclusion de plan capitalizable corto plazo</b></br>";
                    if (mC1139 == 1) cadena += "<b> - Cambio de seguro prorrogado</b></br>";
                    if (mC1140 == 1) cadena += "<b> - Cambio de seguro saldado</b></br>";
                    if (mC1141 == 1) cadena += "<b> - Otros</b></br>";
                    if (mC1142 == 1) cadena += "<b> - Cancelación de póliza</b></br>";
                    if (mC1143 == 1) cadena += "<b> - Rescate de fondo de pólizas de jubilacion y capitalizable a corto plazo para pago de prima-póliza de vida mismo Asegurado</b></br>";
                    if (mC1144 == 1) cadena += "<b> - Rescate total</b></br>";
                    if (mC1145 == 1) cadena += "<b> - Rescate parcial</b></br>";
                    if (mC1146 == 1) cadena += "<b> - Devolución de primas</b></br>";
                    if (mC1147 == 1) cadena += "<b> - Aclaración de pagos</b></br>";
                    
                    if (mDetalle != "") cadena += "</br>DETALLES:</br></br><b>" + mDetalle + "</b>";
                cadena += "</td></tr>";
                cadena += "</table>";

                return cadena;
            }
        }
    }
}
