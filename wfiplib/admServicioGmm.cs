using System;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admServicioGmm
    {
        public bool nuevo(ServicioGmmP pDatos)
        {
            bool resultado = false;
            string SqlCmd = "INSERT INTO TRAM00002 (";
            SqlCmd += "IdTramite";
            SqlCmd += ",NumPoliza";
            SqlCmd += ",Nombre";
            SqlCmd += ",ApPaterno";
            SqlCmd += ",ApMaterno";
            SqlCmd += ",RFC";
            SqlCmd += ",Nacionalidad";
            SqlCmd += ",TipoPersona";
            SqlCmd += ",ConAfectacion";
            SqlCmd += ",C128";
            SqlCmd += ",C129";
            SqlCmd += ",C1210";
            SqlCmd += ",C1211";
            SqlCmd += ",C1212";
            SqlCmd += ",C1213";
            SqlCmd += ",C1214";
            SqlCmd += ",C1215";
            SqlCmd += ",C1216";
            SqlCmd += ",C1217";
            SqlCmd += ",C1218";
            SqlCmd += ",C1219";
            SqlCmd += ",C1220";
            SqlCmd += ",C1221";
            SqlCmd += ",C1222";
            SqlCmd += ",C1223";
            SqlCmd += ",C1224";
            SqlCmd += ",C1225";
            SqlCmd += ",C1226";
            SqlCmd += ",C1227";
            SqlCmd += ",C1228";
            SqlCmd += ",C1229";
            SqlCmd += ",C1230";
            SqlCmd += ",C1231";
            SqlCmd += ",C1232";
            SqlCmd += ",C1233";
            SqlCmd += ",Detalle";
                        
            SqlCmd += ")";

            SqlCmd += " VALUES (";
            SqlCmd += pDatos.IdTramite ;
            SqlCmd += ",'" + pDatos.NumPoliza + "'";
            SqlCmd += ",'" + pDatos.Nombre + "'";
            SqlCmd += ",'" + pDatos.ApPaterno + "'";
            SqlCmd += ",'" + pDatos.ApMaterno + "'";
            SqlCmd += ",'" + pDatos.RFC + "'";
            SqlCmd += ",'" + pDatos.Nacionalidad + "'";
            SqlCmd += "," + pDatos.TipoPersona.ToString("d");
            SqlCmd += "," + pDatos.ConAfectacion.ToString();
            SqlCmd += "," + pDatos.C128.ToString();
            SqlCmd += "," + pDatos.C129.ToString();
            SqlCmd += "," + pDatos.C1210.ToString();
            SqlCmd += "," + pDatos.C1211.ToString();
            SqlCmd += "," + pDatos.C1212.ToString();
            SqlCmd += "," + pDatos.C1213.ToString();
            SqlCmd += "," + pDatos.C1214.ToString();
            SqlCmd += "," + pDatos.C1215.ToString();
            SqlCmd += "," + pDatos.C1216.ToString();
            SqlCmd += "," + pDatos.C1217.ToString();
            SqlCmd += "," + pDatos.C1218.ToString();
            SqlCmd += "," + pDatos.C1219.ToString();
            SqlCmd += "," + pDatos.C1220.ToString();
            SqlCmd += "," + pDatos.C1221.ToString();
            SqlCmd += "," + pDatos.C1222.ToString();
            SqlCmd += "," + pDatos.C1223.ToString();
            SqlCmd += "," + pDatos.C1224.ToString();
            SqlCmd += "," + pDatos.C1225.ToString();
            SqlCmd += "," + pDatos.C1226.ToString();
            SqlCmd += "," + pDatos.C1227.ToString();
            SqlCmd += "," + pDatos.C1228.ToString();
            SqlCmd += "," + pDatos.C1229.ToString();
            SqlCmd += "," + pDatos.C1230.ToString();
            SqlCmd += "," + pDatos.C1231.ToString();
            SqlCmd += "," + pDatos.C1232.ToString();
            SqlCmd += "," + pDatos.C1233.ToString();
            SqlCmd += ",'" + pDatos.Detalle + "'";
            SqlCmd += ");";
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd);

            if (resultado)
            {
                String P1 = "Cambios sin afectación a prima";
                String SP1 = "";
                String SP2 = "";
                String SP3 = "";
                String SP4 = "";
                String SP5 = "";
                String SP6 = "";
                String SP7 = "";
                String SP8 = "";
                String SP9 = "";
                String P2 = "Rehabilitación";
                String SP10 = "";
                String P3 = "Cambio de conducto de cobro";
                String SP11 = "";
                String SP12 = "";
                String SP13 = "";
                String P4 = "Actualización de información artículo 492";
                String SP14 = "";
                String P5 = "Cambios con afectación a prima";
                String SP15 = "";
                String SP16 = "";
                String SP17 = "";
                String SP18 = "";
                String SP19 = "";
                String SP20 = "";
                String P6 = "Rescates, retiros y cancelaciones";
                String SP21 = "";
                String P7 = "Aclaración de pagos";
                String SP22 = "";
                String SP23 = "";

                if (pDatos.C128 == 1)
                {
                    SP1 = "Modificación de nombre y apellidos";
                }
                if (pDatos.C129 == 1)
                {
                    SP2 = "Cambio de contratante";
                }
                if (pDatos.C1210 == 1)
                {
                    SP3 = "Cambio de domicilio";
                }
                if (pDatos.C1211 == 1)
                {
                    SP4 = "Corrección registro federal de contribuyentes";
                }
                if (pDatos.C1212 == 1)
                {
                    SP5 = "Cambio de beneficiario cobertura muerte accidental y últimos gastos";
                }
                if (pDatos.C1213 == 1)
                {
                    SP6 = "Duplicado de póliza";
                }
                if (pDatos.C1214 == 1)
                {
                    SP7 = "Duplicado de endoso";
                }
                if (pDatos.C1215 == 1)
                {
                    SP8 = "Cambio clave de agente";
                }
                if (pDatos.C1216 == 1)
                {
                    SP9 = "Reconocimiento de antigüedad gastos médicos mayores";
                }
                if (pDatos.C1217 == 1)
                {
                    SP10 = "Rehabilitación";
                }
                ///////////////////////////////
                if (pDatos.C1218 == 1)
                {
                    SP11 = "Cambio de conducto de cobro a tarjeta de crédito y débito";
                }
                if (pDatos.C1219 == 1)
                {
                    SP12 = "Cambio de conducto de cobro a Clave Bancaria";
                }
                if (pDatos.C1220 == 1)
                {
                    SP13 = "Cambio de conducto de cobro a agente";
                }
                //////////////////////////////////////
                if (pDatos.C1221 == 1)
                {
                    SP14 = "Actualización de información artículo 492 de la ley de instituciones de seguros y de fianzas";
                }
                //////////////////////////////
                if (pDatos.C1222 == 1)
                {
                    SP15 = "Cambio de forma de pago";
                }
                if (pDatos.C1223 == 1)
                {
                    SP16 = "Reconsideración de dictamen";
                }
                if (pDatos.C1224 == 1)
                {
                    SP17 = "Inclusión o exclusión de extra primas";
                }
                if (pDatos.C1225 == 1)
                {
                    SP18 = "Inclusión o exclusión de coberturas";
                }
                if (pDatos.C1226 == 1)
                {
                    SP19 = "Inclusión y exclusión de dependientes";
                }
                if (pDatos.C1227 == 1)
                {
                    SP20 = "Inclusión o exclusión de asegurados";
                }
                //// P6
                if (pDatos.C1229 == 1)
                {
                    SP21 = "Cancelación de póliza";
                }
                //// P7
                if (pDatos.C1231 == 1)
                {
                    SP22 = "Devolución de primas";
                }
                if (pDatos.C1232 == 1)
                {
                    SP23 = "Aclaración de pagos";
                }

                StringBuilder SqlCmd2 = new StringBuilder("EXEC [dbo].[InsertaProductos] @Tramite ='" + pDatos.IdTramite.ToString() + "',@TipoTramite='2'," +
                    "@P1 ='" + P1 + "',@SP1='" + SP1 + "'," +
                    "@P2 ='" + P1 + "',@SP2='" + SP2 + "'," +
                    "@P3 ='" + P1 + "',@SP3='" + SP3 + "'," +
                    "@P4 ='" + P1 + "',@SP4='" + SP4 + "'," +
                    "@P5 ='" + P1 + "',@SP5='" + SP5 + "'," +
                    "@P6 ='" + P1 + "',@SP6='" + SP6 + "'," +
                    "@P7 ='" + P1 + "',@SP7='" + SP7 + "'," +
                    "@P8 ='" + P1 + "',@SP8='" + SP8 + "'," +
                    "@P9 ='" + P1 + "',@SP9='" + SP9 + "'," +

                    "@P10 ='" + P2 + "',@SP10='" + SP10 + "'," +

                    "@P11 ='" + P3 + "', @SP11='" + SP11 + "'," +
                    "@P12 ='" + P3 + "', @SP12 = '" + SP12 + "'," +
                    "@P13 = '" + P3 + "',@SP13 = '" + SP13 + "'," +

                    "@P14 = '" + P4 + "',@SP14 = '" + SP14 + "'," +

                    "@P15 = '" + P5 + "',@SP15 = '" + SP15 + "'," +
                    "@P16 = '" + P5 + "',@SP16 = '" + SP16 + "'," +
                    "@P17 = '" + P5 + "',@SP17 = '" + SP17 + "'," +
                    "@P18 = '" + P5 + "',@SP18 = '" + SP18 + "'," +
                    "@P19 = '" + P5 + "',@SP19 = '" + SP19 + "'," +
                    "@P20 = '" + P5 + "',@SP20 = '" + SP20 + "'," +

                    "@P21 = '" + P6 + "',@SP21 = '" + SP21 + "'," +

                    "@P22 = '" + P7 + "',@SP22 = '" + SP22 + "'," +
                    "@P23 = '" + P7 + "',@SP23 = '" + SP23 + "'," +

                    "@P24 = '',@SP24 = ''," +
                    "@P25 = '',@SP25 = ''," +
                    "@P26 = '',@SP26 = ''," +
                    "@P27 = '',@SP27 = ''," +
                    "@P28 = '',@SP28 = ''," +
                    "@P29 = '',@SP29 = ''," +
                    "@P30 = '',@SP30 = ''," +
                    "@P31 = '',@SP31 = ''," +
                    "@P32 = '',@SP32 = ''," +
                    "@P33 = '',@SP33 = ''," +
                    "@P34 = '',@SP34 = ''," +
                    "@P35 = '',@SP35 = ''," +
                    "@P36 = '',@SP36 = ''," +
                    "@P37 = '',@SP37 = ''," +
                    "@P38 = '',@SP38 = ''," +
                    "@P39 = '',@SP39 = ''");

                resultado = BD.ejecutaCmd(SqlCmd2.ToString());
            }

            BD.cierraBD();
            return resultado;
        }

        public DataTable Checks(string P1, string SP1, string P2, string SP2, string P3, string SP3, string P4, string SP4, string P5, string SP5, string P6, string SP6, string P7, string SP7, string P8, string SP8, string P9, string SP9, string P10, string SP10, string P11, string SP11, string P12, string SP12, string P13, string SP13, string P14, string SP14, string P15, string SP15, string P16, string SP16, string P17, string SP17, string P18, string SP18, string P19, string SP19, string P20, string SP20, string P21, string SP21, string P22, string SP22, string P23, string SP23, string P24, string SP24, string P25, string SP25, string P26, string SP26, string P27, string SP27, string P28, string SP28, string P29, string SP29, string P30, string SP30, string P31, string SP31, string P32, string SP32, string P33, string SP33, string P34, string SP34, string P35, string SP35, string P36, string SP36, string P37, string SP37, string P38, string SP38, string P39, string SP39)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM RetornaProductos ('2','" + P1 + "','" + SP1 + "','" + P2 + "','" + SP2 + "','" + P3 + "','" + SP3 + "','" + P4 + "','" + SP4 + "','" + P5 + "','" + SP5 + "','" + P6 + "','" + SP6 + "','" + P7 + "','" + SP7 + "','" + P8 + "','" + SP8 + "','" + P9 + "','" + SP9 + "','" + P10 + "','" + SP10 + "','" + P11 + "','" + SP11 + "','" + P12 + "','" + SP12 + "','" + P13 + "','" + SP13 + "','" + P14 + "','" + SP14 + "','" + P15 + "','" + SP15 + "','" + P16 + "','" + SP16 + "','" + P17 + "','" + SP17 + "','" + P18 + "','" + SP18 + "','" + P19 + "','" + SP19 + "','" + P20 + "','" + SP20 + "','" + P21 + "','" + SP21 + "','" + P22 + "','" + SP22 + "','" + P23 + "','" + SP23 + "','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','')");
            
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public ServicioGmmP cargaFolio(int pId)
        {
            ServicioGmmP resultado = null;
            String SqlCmd = "SELECT * FROM dbo.Folio WHERE dbo.Folio.IdTramite = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = armaFolio(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private ServicioGmmP armaFolio(DataRow pRegistro)
        {
            ServicioGmmP resultado = new ServicioGmmP();
            if (!pRegistro.IsNull("FolioCompuesto")) resultado.Folio = Convert.ToString(pRegistro["FolioCompuesto"]);
            return resultado;
        }

        public ServicioGmmP CargaP(int pId)
        {
            ServicioGmmP respuesta = new ServicioGmmP();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM TRAM00002 WHERE IdTramite=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = ArmaP(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private ServicioGmmP ArmaP(DataRow pRegistro)
        {
            ServicioGmmP servicioGmm = new ServicioGmmP();
            bool flag = !pRegistro.IsNull("IdTramite");
            if (flag)
            {
                servicioGmm.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            }
            bool flag2 = !pRegistro.IsNull("NumPoliza");
            if (flag2)
            {
                servicioGmm.NumPoliza = Convert.ToString(pRegistro["NumPoliza"]);
            }
            bool flag3 = !pRegistro.IsNull("Nombre");
            if (flag3)
            {
                servicioGmm.Nombre = Convert.ToString(pRegistro["Nombre"]);
            }
            bool flag4 = !pRegistro.IsNull("ApPaterno");
            if (flag4)
            {
                servicioGmm.ApPaterno = Convert.ToString(pRegistro["ApPaterno"]);
            }
            bool flag5 = !pRegistro.IsNull("ApMaterno");
            if (flag5)
            {
                servicioGmm.ApMaterno = Convert.ToString(pRegistro["ApMaterno"]);
            }
            bool flag6 = !pRegistro.IsNull("RFC");
            if (flag6)
            {
                servicioGmm.RFC = Convert.ToString(pRegistro["RFC"]);
            }
            bool flag7 = !pRegistro.IsNull("TipoPersona");
            if (flag7)
            {
                servicioGmm.TipoPersona = (E_TipoPersona)pRegistro["TipoPersona"];
            }
            bool flag8 = !pRegistro.IsNull("ConAfectacion");
            if (flag8)
            {
                servicioGmm.ConAfectacion = Convert.ToInt32(pRegistro["ConAfectacion"]);
            }
            bool flag9 = !pRegistro.IsNull("C128");
            if (flag9)
            {
                servicioGmm.C128 = Convert.ToInt32(pRegistro["C128"]);
            }
            bool flag10 = !pRegistro.IsNull("C129");
            if (flag10)
            {
                servicioGmm.C129 = Convert.ToInt32(pRegistro["C129"]);
            }
            bool flag11 = !pRegistro.IsNull("C1210");
            if (flag11)
            {
                servicioGmm.C1210 = Convert.ToInt32(pRegistro["C1210"]);
            }
            bool flag12 = !pRegistro.IsNull("C1211");
            if (flag12)
            {
                servicioGmm.C1211 = Convert.ToInt32(pRegistro["C1211"]);
            }
            bool flag13 = !pRegistro.IsNull("C1212");
            if (flag13)
            {
                servicioGmm.C1212 = Convert.ToInt32(pRegistro["C1212"]);
            }
            bool flag14 = !pRegistro.IsNull("C1213");
            if (flag14)
            {
                servicioGmm.C1213 = Convert.ToInt32(pRegistro["C1213"]);
            }
            bool flag15 = !pRegistro.IsNull("C1214");
            if (flag15)
            {
                servicioGmm.C1214 = Convert.ToInt32(pRegistro["C1214"]);
            }
            bool flag16 = !pRegistro.IsNull("C1215");
            if (flag16)
            {
                servicioGmm.C1215 = Convert.ToInt32(pRegistro["C1215"]);
            }
            bool flag17 = !pRegistro.IsNull("C1216");
            if (flag17)
            {
                servicioGmm.C1216 = Convert.ToInt32(pRegistro["C1216"]);
            }
            bool flag18 = !pRegistro.IsNull("C1217");
            if (flag18)
            {
                servicioGmm.C1217 = Convert.ToInt32(pRegistro["C1217"]);
            }
            bool flag19 = !pRegistro.IsNull("C1218");
            if (flag19)
            {
                servicioGmm.C1218 = Convert.ToInt32(pRegistro["C1218"]);
            }
            bool flag20 = !pRegistro.IsNull("C1219");
            if (flag20)
            {
                servicioGmm.C1219 = Convert.ToInt32(pRegistro["C1219"]);
            }
            bool flag21 = !pRegistro.IsNull("C1220");
            if (flag21)
            {
                servicioGmm.C1220 = Convert.ToInt32(pRegistro["C1220"]);
            }
            bool flag22 = !pRegistro.IsNull("C1221");
            if (flag22)
            {
                servicioGmm.C1221 = Convert.ToInt32(pRegistro["C1221"]);
            }
            bool flag23 = !pRegistro.IsNull("C1222");
            if (flag23)
            {
                servicioGmm.C1222 = Convert.ToInt32(pRegistro["C1222"]);
            }
            bool flag24 = !pRegistro.IsNull("C1223");
            if (flag24)
            {
                servicioGmm.C1223 = Convert.ToInt32(pRegistro["C1223"]);
            }
            bool flag25 = !pRegistro.IsNull("C1224");
            if (flag25)
            {
                servicioGmm.C1224 = Convert.ToInt32(pRegistro["C1224"]);
            }
            bool flag26 = !pRegistro.IsNull("C1225");
            if (flag26)
            {
                servicioGmm.C1225 = Convert.ToInt32(pRegistro["C1225"]);
            }
            bool flag27 = !pRegistro.IsNull("C1226");
            if (flag27)
            {
                servicioGmm.C1226 = Convert.ToInt32(pRegistro["C1226"]);
            }
            bool flag28 = !pRegistro.IsNull("C1227");
            if (flag28)
            {
                servicioGmm.C1227 = Convert.ToInt32(pRegistro["C1227"]);
            }
            bool flag29 = !pRegistro.IsNull("C1228");
            if (flag29)
            {
                servicioGmm.C1228 = Convert.ToInt32(pRegistro["C1228"]);
            }
            bool flag30 = !pRegistro.IsNull("C1229");
            if (flag30)
            {
                servicioGmm.C1229 = Convert.ToInt32(pRegistro["C1229"]);
            }
            bool flag31 = !pRegistro.IsNull("C1230");
            if (flag31)
            {
                servicioGmm.C1230 = Convert.ToInt32(pRegistro["C1230"]);
            }
            bool flag32 = !pRegistro.IsNull("C1231");
            if (flag32)
            {
                servicioGmm.C1231 = Convert.ToInt32(pRegistro["C1231"]);
            }
            bool flag33 = !pRegistro.IsNull("C1232");
            if (flag33)
            {
                servicioGmm.C1232 = Convert.ToInt32(pRegistro["C1232"]);
            }
            bool flag34 = !pRegistro.IsNull("C1233");
            if (flag34)
            {
                servicioGmm.C1233 = Convert.ToInt32(pRegistro["C1233"]);
            }
            return servicioGmm;
        }


        public ServicioGmm carga(int pId)
        {
            ServicioGmm respuesta = new ServicioGmm();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM TRAM00002 WHERE IdTramite=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private ServicioGmm arma(DataRow pRegistro)
        {
            ServicioGmm respuesta = new ServicioGmm();
            if (!pRegistro.IsNull("IdTramite")) respuesta.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("NumPoliza")) respuesta.NumPoliza = Convert.ToString(pRegistro["NumPoliza"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("ApPaterno")) respuesta.ApPaterno = Convert.ToString(pRegistro["ApPaterno"]);
            if (!pRegistro.IsNull("ApMaterno")) respuesta.ApMaterno = Convert.ToString(pRegistro["ApMaterno"]);
            if (!pRegistro.IsNull("RFC")) respuesta.RFC = Convert.ToString(pRegistro["RFC"]);
            if (!pRegistro.IsNull("Nacionalidad")) respuesta.Nacionalidad = Convert.ToString(pRegistro["Nacionalidad"]);
            if (!pRegistro.IsNull("TipoPersona")) respuesta.TipoPersona = (E_TipoPersona)(pRegistro["TipoPersona"]);
            if (!pRegistro.IsNull("ConAfectacion")) respuesta.ConAfectacion = Convert.ToInt32(pRegistro["ConAfectacion"]);
            if (!pRegistro.IsNull("C128")) respuesta.C128 = Convert.ToInt32(pRegistro["C128"]);
            if (!pRegistro.IsNull("C129")) respuesta.C129 = Convert.ToInt32(pRegistro["C129"]);
            if (!pRegistro.IsNull("C1210")) respuesta.C1210 = Convert.ToInt32(pRegistro["C1210"]);
            if (!pRegistro.IsNull("C1211")) respuesta.C1211 = Convert.ToInt32(pRegistro["C1211"]);
            if (!pRegistro.IsNull("C1212")) respuesta.C1212 = Convert.ToInt32(pRegistro["C1212"]);
            if (!pRegistro.IsNull("C1213")) respuesta.C1213 = Convert.ToInt32(pRegistro["C1213"]);
            if (!pRegistro.IsNull("C1214")) respuesta.C1214 = Convert.ToInt32(pRegistro["C1214"]);
            if (!pRegistro.IsNull("C1215")) respuesta.C1215 = Convert.ToInt32(pRegistro["C1215"]);
            if (!pRegistro.IsNull("C1216")) respuesta.C1216 = Convert.ToInt32(pRegistro["C1216"]);
            if (!pRegistro.IsNull("C1217")) respuesta.C1217 = Convert.ToInt32(pRegistro["C1217"]);
            if (!pRegistro.IsNull("C1218")) respuesta.C1218 = Convert.ToInt32(pRegistro["C1218"]);
            if (!pRegistro.IsNull("C1219")) respuesta.C1219 = Convert.ToInt32(pRegistro["C1219"]);
            if (!pRegistro.IsNull("C1220")) respuesta.C1220 = Convert.ToInt32(pRegistro["C1220"]);
            if (!pRegistro.IsNull("C1221")) respuesta.C1221 = Convert.ToInt32(pRegistro["C1221"]);
            if (!pRegistro.IsNull("C1222")) respuesta.C1222 = Convert.ToInt32(pRegistro["C1222"]);
            if (!pRegistro.IsNull("C1223")) respuesta.C1223 = Convert.ToInt32(pRegistro["C1223"]);
            if (!pRegistro.IsNull("C1224")) respuesta.C1224 = Convert.ToInt32(pRegistro["C1224"]);
            if (!pRegistro.IsNull("C1225")) respuesta.C1225 = Convert.ToInt32(pRegistro["C1225"]);
            if (!pRegistro.IsNull("C1226")) respuesta.C1226 = Convert.ToInt32(pRegistro["C1226"]);
            if (!pRegistro.IsNull("C1227")) respuesta.C1227 = Convert.ToInt32(pRegistro["C1227"]);
            if (!pRegistro.IsNull("C1228")) respuesta.C1228 = Convert.ToInt32(pRegistro["C1228"]);
            if (!pRegistro.IsNull("C1229")) respuesta.C1229 = Convert.ToInt32(pRegistro["C1229"]);
            if (!pRegistro.IsNull("C1230")) respuesta.C1230 = Convert.ToInt32(pRegistro["C1230"]);
            if (!pRegistro.IsNull("C1231")) respuesta.C1231 = Convert.ToInt32(pRegistro["C1231"]);
            if (!pRegistro.IsNull("C1232")) respuesta.C1232 = Convert.ToInt32(pRegistro["C1232"]);
            if (!pRegistro.IsNull("C1233")) respuesta.C1233 = Convert.ToInt32(pRegistro["C1233"]);
            
            if (!pRegistro.IsNull("Detalle")) respuesta.Detalle = Convert.ToString(pRegistro["Detalle"]);

            return respuesta;
        }

        public bool ExistenTramitesparaRFC(string pRFC)
        {
            bool respuesta = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM TRAM00002 WHERE Rfc='" + pRFC + "'");
            if (datos.Rows.Count > 0) { respuesta = true; }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;

        }

        public bool elimina(int pIdTramite)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("DELETE From TRAM00002 WHERE IdTramite=" + pIdTramite.ToString());
            BD.cierraBD();
            return resultado;
        }
    }

    [Serializable]
    public class ServicioGmm
    {
        private int mIdTramite = 0;
        public int IdTramite { get { return mIdTramite; } set { mIdTramite = value; } }
        private string mNumPoliza = "";
        public string NumPoliza { get { return mNumPoliza; } set { mNumPoliza = value; } }
        private E_TipoPersona mTipoPersona = E_TipoPersona.Fisica;
        public E_TipoPersona TipoPersona { get { return mTipoPersona; } set { mTipoPersona = value; } }
        private string mNombre = "";
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mApPaterno = "";
        public string ApPaterno { get { return mApPaterno; } set { mApPaterno = value; } }
        private string mApMaterno = ""; 
        public string ApMaterno { get { return mApMaterno; } set { mApMaterno = value; } }
        private string mRFC = "";
        public string RFC { get { return mRFC; } set { mRFC = value; } }

        private string mNacionalidad = "";
        public string Nacionalidad { get { return mNacionalidad; } set { mNacionalidad = value; } }
        private int mConAfectacion = 0;
        public int ConAfectacion { get { return mConAfectacion; } set { mConAfectacion = value; } }

        private int mC128 = 0;
        private int mC129 = 0;
        private int mC1210 = 0;
        private int mC1211 = 0;
        private int mC1212 = 0;
        private int mC1213 = 0;
        private int mC1214 = 0;
        private int mC1215 = 0;
        private int mC1216 = 0;
        private int mC1217 = 0;
        private int mC1218 = 0;
        private int mC1219 = 0;
        private int mC1220 = 0;
        private int mC1221 = 0;
        private int mC1222 = 0;
        private int mC1223 = 0;
        private int mC1224 = 0;
        private int mC1225 = 0;
        private int mC1226 = 0;
        private int mC1227 = 0;
        private int mC1228 = 0;
        private int mC1229 = 0;
        private int mC1230 = 0;
        private int mC1231 = 0;
        private int mC1232 = 0;
        private int mC1233 = 0;
        
        public int C128 { get { return mC128; } set { mC128 = value; } }
        public int C129 { get { return mC129; } set { mC129 = value; } }
        public int C1210 { get { return mC1210; } set { mC1210 = value; } }
        public int C1211 { get { return mC1211; } set { mC1211 = value; } }
        public int C1212 { get { return mC1212; } set { mC1212 = value; } }
        public int C1213 { get { return mC1213	; } set { mC1213 = value; } }
        public int C1214 { get { return mC1214	; } set { mC1214 = value; } }
        public int C1215 { get { return mC1215	; } set { mC1215 = value; } }
        public int C1216 { get { return mC1216	; } set { mC1216 = value; } }
        public int C1217 { get { return mC1217	; } set { mC1217 = value; } }
        public int C1218 { get { return mC1218	; } set { mC1218 = value; } }
        public int C1219 { get { return mC1219	; } set { mC1219 = value; } }
        public int C1220 { get { return mC1220	; } set { mC1220 = value; } }
        public int C1221 { get { return mC1221	; } set { mC1221 = value; } }
        public int C1222 { get { return mC1222	; } set { mC1222 = value; } }
        public int C1223 { get { return mC1223	; } set { mC1223 = value; } }
        public int C1224 { get { return mC1224	; } set { mC1224 = value; } }
        public int C1225 { get { return mC1225	; } set { mC1225 = value; } }
        public int C1226 { get { return mC1226	; } set { mC1226 = value; } }
        public int C1227 { get { return mC1227	; } set { mC1227 = value; } }
        public int C1228 { get { return mC1228	; } set { mC1228 = value; } }
        public int C1229 { get { return mC1229	; } set { mC1229 = value; } }
        public int C1230 { get { return mC1230	; } set { mC1230 = value; } }
        public int C1231 { get { return mC1231	; } set { mC1231 = value; } }
        public int C1232 { get { return mC1232	; } set { mC1232 = value; } }
        public int C1233 { get { return mC1233	; } set { mC1233 = value; } }
        

        private string  mDetalle = string.Empty;
        public string Detalle { get { return mDetalle; } set { mDetalle = value; } }

        public string CabeceraHtml
        {
            get
            {
                string cadena = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                cadena += "<tr><td style ='width:150px'>Número trámite:</td><td><b>" + mIdTramite.ToString().PadLeft(5, '0') + "</b></td></tr>";
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

                    cadena += "<tr><td colspan='2'>";
                    cadena += "</br></br>TRAMITES:</br></br> ";
                    if (mC128 == 1) cadena += "<b> - Modificación de nombre y apellidos</b></br>";
                    if (mC129 == 1) cadena += "<b> - Cambio de contratante (anexar formato 5 Actualización de información Artículo 492 de la Ley de Instituciones de Seguros y de Fianzas</b></br>";
                    if (mC1210 == 1) cadena += "<b> - Cambio de domicilio</b></br>";
                    if (mC1211 == 1) cadena += "<b> - Corrección Registro Federal de Contribuyentes</b></br>";
                    if (mC1212 == 1) cadena += "<b> - Cambio de beneficiario cobertura muerte accidental y últimos gastos (anexar formato identificación de beneficiarios)</b></br>";
                    if (mC1213 == 1) cadena += "<b> - Duplicado de póliza</b></br>";
                    if (mC1214 == 1) cadena += "<b> - Duplicado de endoso</b></br>";
                    if (mC1215 == 1) cadena += "<b> - Cambio clave de agente</b></br>";
                    if (mC1216 == 1) cadena += "<b> - Reconocimiento de antigüedad Gastos Médicos Mayores</b></br>";
                    if (mC1217 == 1) cadena += "<b> - Rehabilitación</b></br>";
                    if (mC1218 == 1) cadena += "<b> - Cambio de conducto de cobro a tarjeta de crédito y débito</b></br>";
                    if (mC1219 == 1) cadena += "<b> - Cambio de conducto de cobro a Clabe Bancaria</b></br>";
                    if (mC1220 == 1) cadena += "<b> - Cambio de conducto de cobro a agente</b></br>";
                    if (mC1221 == 1) cadena += "<b> - Actualización de información Artículo 492 de la Ley de Instituciones de Seguros y de Fianzas</b></br>";
                    if (mC1222 == 1) cadena += "<b> - Cambio de forma de pago</b></br>";
                    if (mC1223 == 1) cadena += "<b> - Reconsideración de dictamen</b></br>";
                    if (mC1224 == 1) cadena += "<b> - Inclusión o exclusión de extraprimas</b></br>";
                    if (mC1225 == 1) cadena += "<b> - Inclusión o exclusión de coberturas</b></br>";
                    if (mC1226 == 1) cadena += "<b> - Inclusión y exclusión de dependientes</b></br>";
                    if (mC1227 == 1) cadena += "<b> - Inclusión o exclusión de asegurados</b></br>";
                    if (mC1228 == 1) cadena += "<b> - Otros</b></br>";
                    if (mC1229 == 1) cadena += "<b> - Cancelación de póliza</b></br>";
                    if (mC1230 == 1) cadena += "<b> - Cancelación Gastos Médicos Mayores devolución nota de crédito</b></br>";
                    if (mC1231 == 1) cadena += "<b> - Devolución de primas</b></br>";
                    if (mC1232 == 1) cadena += "<b> - Aclaración de pagos</b></br>";
                    if (mC1233 == 1) cadena += "<b> - Duplicado de recibo</b></br>";
                    if (mDetalle != "") cadena += "</br>DETALLES:</br></br><b>" + mDetalle + "</b>";
                cadena += "</td></tr>";
                cadena += "</table></br>";
                return cadena;
            }
        }
    }
}
