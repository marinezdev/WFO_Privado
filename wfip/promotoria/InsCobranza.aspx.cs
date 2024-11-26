using OfficeOpenXml;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using w = wfiplib.Tablas.Institucional.Privado;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace wfip.promotoria
{
    public partial class Cobranza : System.Web.UI.Page
    {
        public DataTable dt;
        //public static string cobertura = string.Empty;
        public int alertasBasica = 0;
        public int alertasVacio = 0;
        public int advertenciasBasica = 0;
        public int alertasPotenciacion = 0;
        public int advertenciasPotenciacion = 0;
        public decimal SumaPagarDependencia = 0;

        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.InstitucionalTablas it = new wfiplib.InstitucionalTablas();
        wfiplib.admPolizasDetalle pd = new wfiplib.admPolizasDetalle();
        wfiplib.admDependencias dep = new wfiplib.admDependencias();
        wfiplib.admArchivosDependencias ad = new wfiplib.admArchivosDependencias();
        wfiplib.admArchivosDependenciasEstados ade = new wfiplib.admArchivosDependenciasEstados();
        wfiplib.admArchivosTramite at = new wfiplib.admArchivosTramite();
        Mensajes mensajes = new Mensajes();

        #region Eventos ***************************************************************************************************************************************

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["RegistrosTemporales"] = null;
                gvAgregado.DataSource = null;
                gvAgregado.DataBind();
            }
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToShortDateString();
                if (Request["folio"] != null)
                {
                    //Cargar los registros relacionados con el no. de folio (si los hubiere)
                    CargarTablaPorFolio(Request["folio"].ToString(), Request["cobertura"].ToString());
                    lblFolio.Text = "<strong>Folio:</strong> " + Request["folio"];

                    //Detalle
                    ProcesarDetalleFoliado(Request["folio"].ToString());
                }
            }
            else
            {
                if (Session["RegistrosTemporales"] == null)
                {
                    //Nuevo Básica y Potenciacion
                    dt = new DataTable();
                    dt.Columns.Add("Poliza");                           //Poliza
                    dt.Columns.Add("Dependencia");                      //Dependencia
                    dt.Columns.Add("APaterno");                         //Apellido Paterno del titular
                    dt.Columns.Add("AMaterno");                         //Apellido Materno del titular
                    dt.Columns.Add("Nombres");                          //Nombre(s) del titular
                    dt.Columns.Add("FNacimiento");                      //Fecha Nacimiento del titular
                    dt.Columns.Add("RFC");                              //RFC del titular
                    dt.Columns.Add("CURP");                             //CURP del titular
                    dt.Columns.Add("Sexo");                             //Sexo del titular
                    dt.Columns.Add("CEntidadFederativa");               //Codigo de la entidad federattiva donde esta adscrito el titular
                    dt.Columns.Add("CMunicipio");                       //Codigo de la ciudad o municipio donde esta adscrito el titular
                    dt.Columns.Add("NivelTabular");                     //Nivel tabular del titular
                    dt.Columns.Add("MPercepcionOBM");                   //Monto de la percepcion ordinaria bruta mensual del titular Concepto 06 y 07
                    dt.Columns.Add("Eventual");                         //Eventual
                    dt.Columns.Add("APAsegurado");                      //Apellido paterno del asegurado
                    dt.Columns.Add("AMAsegurado");                      //Apellido materno del asegurado
                    dt.Columns.Add("NAsegurado");                       //Nombres del asegurado
                    dt.Columns.Add("FNAsegurado");                      //Fecha de nacimiento del asegurado
                    dt.Columns.Add("CURPAsegurado");                    //CURP del asegurado
                    dt.Columns.Add("SAsegurado");                       //Sexo del asegurado
                    dt.Columns.Add("FAAsegurado");                      //Fecha de antigüedad del asegurado en el SGMM
                    dt.Columns.Add("TAsegurado");                       //Tipo de asegurado
                    dt.Columns.Add("FIColectividad");                   //Fecha de ingreso a la colectividad
                    dt.Columns.Add("SABasica");                         //Suma Asegurada Basica
                    dt.Columns.Add("PBTReportar");                      //Prima basica del trimestre a reportar
                    dt.Columns.Add("MAPBasica");                        //Monto de ajuste en prima basica por altas, bajas, promociones o correcciones
                    dt.Columns.Add("ITPDependencia");                   //Importe total a pagar por la dependencia

                    //Potenciación                        
                    dt.Columns.Add("SAPotenciada");                     //Suma asegurada potenciada
                    dt.Columns.Add("SAT");                              //Suma asegurada total
                    dt.Columns.Add("PrimaPotenciadaQR");                //Prima potenciada quincenal a reportar
                    dt.Columns.Add("FormaPago");                        //forma de pago
                    dt.Columns.Add("MontoAjustePrimaP");                //Monto de ajuste en prima potenciada por altas, bajas, promociones o correcciones
                    dt.Columns.Add("ImporteTotalPagar");                //Importe total a pagar de la prima potenciada
                    dt.Columns.Add("FAAseguradoSAP");                   //Fecha de antigüedad del asegurado en la suma asegurada                    

                    CargarTabla(gvAgregado, dt);

                    Session["RegistrosTemporales"] = dt;
                }
            }
        }

        protected void txtNoPoliza_TextChanged(object sender, EventArgs e)
        {
            string obtenido = (new wfiplib.admPolizas()).NombreCliente(txtNoPoliza.Text); ;
            if (obtenido == "Dato vacío")
                txtCliente.Text = "La póliza es incorrecta o no existe cliente asociado.";
            else
                txtCliente.Text = obtenido;
        }


        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            //Se cancelan los registros que corresponden al folio
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //pd.Eliminar(Request["folio"].ToString());

                //ad.Eliminar(Request["folio"].ToString());
                //Cambiar el estado a 3 del proceso a reenviado
                //ad.ActualizarErrores(Request["folio"].ToString(), txtErrores.Text);
                ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
                ade.Agregar(Request["folio"].ToString(), 2);
                ad.AgregarEstado(Request["folio"].ToString(), 2);
                mensajes.MostrarMensaje(this, "Se enviaron las observaciones de errores a la dependencia con el folio no. " + Request["folio"], "esperaPromotoria.aspx");

            }
            else
            {
                //Permanece igual sin cambios.
            }
        }

        protected void BtnValidacion_Click(object sender, EventArgs e)
        {
            //Crear tabla temporal para procesar lif (gvPotenciacion.Rows.Count <= 0)
            if (gvPotenciacion.Rows.Count <= 0)
            {
                mensajes.MostrarMensaje(this, "Ningún dato para guardar");
                return;
            }

            foreach (GridViewRow gv in gvPotenciacion.Rows)
            {
                pd.PolizaDetallePotenciacion(
                    txtNoPoliza.Text.ToUpper(),                                        //Poliza
                    ((DataBoundLiteralControl)gv.Cells[0].Controls[0]).Text.Trim(),    //Dependencia    .Substring(0,150)
                    ((DataBoundLiteralControl)gv.Cells[1].Controls[0]).Text.Trim(),    //Apaterno       .Substring(0,20)
                    ((DataBoundLiteralControl)gv.Cells[2].Controls[0]).Text.Trim(),    //AMaterno       .Substring(0,20)
                    ((DataBoundLiteralControl)gv.Cells[3].Controls[0]).Text.Trim(),    //Nombres        .Substring(0,20)
                    ((DataBoundLiteralControl)gv.Cells[4].Controls[0]).Text.Trim(),    //Fecha Nacimiento
                    ((DataBoundLiteralControl)gv.Cells[5].Controls[0]).Text.Trim(),    //RFC            .Substring(0,13)
                    ((DataBoundLiteralControl)gv.Cells[6].Controls[0]).Text.Trim(),    //CURP           .Substring(0,18)
                    ((DataBoundLiteralControl)gv.Cells[7].Controls[0]).Text.Trim(),    //Sexo
                    ((DataBoundLiteralControl)gv.Cells[8].Controls[0]).Text.Trim(),   //Código Entidad Federativa
                    ((DataBoundLiteralControl)gv.Cells[9].Controls[0]).Text.Trim(),   //Código Municipio
                    ((DataBoundLiteralControl)gv.Cells[10].Controls[0]).Text.Trim(),   //Nivel Tabular
                    ((DataBoundLiteralControl)gv.Cells[11].Controls[0]).Text.Trim(),   //Percepcion Ordinaria Bruta Mensual
                    ((DataBoundLiteralControl)gv.Cells[12].Controls[0]).Text.Trim(),   //Eventual
                    ((DataBoundLiteralControl)gv.Cells[13].Controls[0]).Text.Trim(),   //Apellido Paterno Asegurado .Substring(0,20)
                    ((DataBoundLiteralControl)gv.Cells[14].Controls[0]).Text.Trim(),   //Apellido Materno Asegurado .Substring(0,20)
                    ((DataBoundLiteralControl)gv.Cells[15].Controls[0]).Text.Trim(),   //Nombres Asegurado          .Substring(0,20)
                    ((DataBoundLiteralControl)gv.Cells[16].Controls[0]).Text.Trim(),   //Fecha Nacimiento Asegurado
                    ((DataBoundLiteralControl)gv.Cells[17].Controls[0]).Text.Trim(),   //CURP Asegurado             .Substring(0,18)
                    ((DataBoundLiteralControl)gv.Cells[18].Controls[0]).Text.Trim(),   //Sexo Asegurado
                    ((DataBoundLiteralControl)gv.Cells[19].Controls[0]).Text.Trim(),   //Fecha Afiliacion asegurado
                    ((DataBoundLiteralControl)gv.Cells[20].Controls[0]).Text.Trim(),   //Tipo Asegurado
                    ((DataBoundLiteralControl)gv.Cells[21].Controls[0]).Text.Trim(),   //Fecha Ingreso a la Colectividad
                    ((DataBoundLiteralControl)gv.Cells[22].Controls[0]).Text.Trim(),   //Suma Asegurada Basica
                    ((DataBoundLiteralControl)gv.Cells[23].Controls[0]).Text.Trim(),   //Suma Asegurada Potenciada
                    ((DataBoundLiteralControl)gv.Cells[24].Controls[0]).Text.Trim(),   //Suma Asegurada total
                    ((DataBoundLiteralControl)gv.Cells[25].Controls[0]).Text.Replace(',', ' ').Replace('$', ' ').Replace('-', ' ').Trim(),   //Prima potenciada quincenal a reportar
                    ((DataBoundLiteralControl)gv.Cells[26].Controls[0]).Text.Trim(),   //Forma de pago
                    ((DataBoundLiteralControl)gv.Cells[27].Controls[0]).Text.Replace(',', ' ').Replace('$', ' ').Replace('-', ' ').Trim(),   //Monto de ajuste en la prima potenciada
                    ((DataBoundLiteralControl)gv.Cells[28].Controls[0]).Text.Replace(',', ' ').Replace('$', ' ').Replace('-', ' ').Trim(),   //Importe total a pagar de la prima potenciada
                    ((DataBoundLiteralControl)gv.Cells[29].Controls[0]).Text.Trim(),   //Fecha de antigüedad del asegurado en la suma asegurada            
                    "2",
                    Request["folio"],
                    ddlTrimestreQuincena.SelectedValue,
                    ddlAnn.SelectedValue
                    );
            }

            //
            string rutaArchivos = "~/ArchivosExcel/";
            string sFileName1 = string.Empty;
            string sFileName2 = string.Empty;
            //Generar archivo 100 posiciones Pagos y Cancelaciones

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                var txtBuilder = new StringBuilder();
                int registros = 0;
                string retenedor = "";
                foreach (DataRow row in pd.SeleccionarCienPosicionesPagos(Request["folio"].ToString(), txtPeriodoReporte.Text).Rows)
                {
                    txtBuilder.AppendLine(row[0].ToString());
                    retenedor = row[0].ToString().Substring(1, 7);
                    registros++;
                }

                string suma = pd.CienPosicionesSumaPagos(Request["folio"].ToString()).Replace(".", "");
                int lognsuma = 12 - suma.Length;
                suma = suma.PadLeft(lognsuma + suma.Length, '0');

                int longcontados = 8 - registros.ToString().Length;
                string contados = registros.ToString().PadLeft(longcontados + registros.ToString().Length, '0');

                txtBuilder.AppendLine("2" + retenedor + "075P" + contados + suma);

                var txtContent = txtBuilder.ToString();
                var txtStream = new MemoryStream(Encoding.UTF8.GetBytes(txtContent));

                sFileName1 = "100PosicionesPagos.txt"; //System.IO.Path.GetRandomFileName();
                //string sGenName1 = "100PosicionesPagos.txt";

                using (StreamWriter SW = new StreamWriter(Server.MapPath(rutaArchivos + sFileName1)))
                {
                    SW.WriteLine(txtBuilder);
                    SW.Close();
                }

                //Para abrir el archivo y leerlo
                //FileStream fs = null;
                //fs = File.Open(Server.MapPath("../ArchivosExcel/" + sFileName1 + ".txt"), FileMode.Open);
                //byte[] btFile = new byte[fs.Length];
                //fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                //fs.Close();
                //Response.AddHeader("Content-disposition", "attachment; filename=" + sFileName1);
                //Response.ContentType = "application/octet-stream";
                //Response.BinaryWrite(btFile);
                //Response.End();

                if (pd.SeleccionarCienPosicionesCancelaciones(Request["folio"].ToString(), txtPeriodoReporte.Text).Rows.Count > 0)
                {
                    var txtBuilder2 = new StringBuilder();
                    int registros2 = 0;
                    string retenedor2 = "";
                    foreach (DataRow row2 in pd.SeleccionarCienPosicionesCancelaciones(Request["folio"].ToString(), txtPeriodoReporte.Text).Rows)
                    {
                        txtBuilder2.AppendLine(row2[0].ToString());
                        retenedor2 = row2[0].ToString().Substring(1, 7);
                        registros2++;
                    }

                    string suma2 = pd.CienPosicionesSumaCancelaciones(Request["folio"].ToString()).Replace(".", "");
                    int lognsuma2 = 12 - suma2.Length;
                    suma2 = suma2.PadLeft(lognsuma + suma.Length, '0');

                    int longcontados2 = 8 - registros2.ToString().Length;
                    string contados2 = registros2.ToString().PadLeft(longcontados2 + registros2.ToString().Length, '0');

                        txtBuilder2.AppendLine("2" + retenedor + "075C" + contados + suma);

                    var txtContent2 = txtBuilder.ToString();
                    var txtStream2 = new MemoryStream(Encoding.UTF8.GetBytes(txtContent));

                    sFileName2 = "100PosicionesCancelaciones.txt"; //System.IO.Path.GetRandomFileName();
                    //string sGenName2 = "100PosicionesPagos.txt";

                    StreamWriter SW2 = new StreamWriter(Server.MapPath(rutaArchivos + sFileName2));
                    SW2.WriteLine(txtBuilder2);
                    SW2.Close();


                    //using (StreamWriter SW2 = new StreamWriter(Server.MapPath(rutaArchivos + sFileName2)))
                    //{
                    //    SW2.WriteLine(txtBuilder2);
                    //    SW2.Close();
                    //}

                    //Para abrir el archivo y leerlo
                    //FileStream fs2 = null;
                    //fs2 = File.Open(Server.MapPath("../ArchivosExcel/" + sFileName2 + ".txt"), FileMode.Open);
                    //byte[] btFile2 = new byte[fs2.Length];
                    //fs2.Read(btFile2, 0, Convert.ToInt32(fs2.Length));
                    //fs2.Close();
                    //Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName2);
                    //Response.ContentType = "application/octet-stream";
                    //Response.BinaryWrite(btFile2);
                    //Response.End();
                }

                //Agregar los archivos
                ad.Agregar100PosicionesPagos(sFileName1, Request["folio"]);
                ad.Agregar100PosicionesCancelaciones(sFileName2, Request["folio"]);

                ade.Agregar(Request["folio"], 3);   //Genera 100 posiciones, solicitud a analista front
                ad.AgregarEstado(Request["folio"], 3);
                mensajes.MostrarMensaje(this, "Se guardaron los datos y se envió un mensaje al analista front para procesarlos.", "esperaPromotoria.aspx");
            }

            ProcesarEncabezadoFijo(gvPotenciacion);
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (rdbCobertura.SelectedValue == "1")
            {
                //guardar los datos del gridview a la base de datos
                AgregarGridABDBasica(gvAgregado);
                ade.Agregar(Request["folio"], 3); //Solicitando recibo fiscal
                ad.AgregarEstado(Request["folio"], 3);
                mensajes.MostrarMensaje(this, "Se solicita el recibo fiscal correspondiente.", "esperaPromotoria.aspx");
            }
            else if (rdbCobertura.SelectedValue == "2")
            {
                ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
                ade.Agregar(Request["folio"], 3);   //Genera 100 posiciones, solicitud a analista front
                ad.AgregarEstado(Request["folio"], 3);
                mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente.", "esperaPromotoria.aspx");
            }
        }

        protected void BtnTerminar_Click(object sender, EventArgs e)
        {
            //Terminado. entregado a Dependencia básica
            ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
            ade.Agregar(Request["folio"], 6);
            ad.AgregarEstado(Request["folio"], 6);
            mensajes.MostrarMensaje(this, "Se enviaron los archivos a la Dependencia.", "esperaPromotoria.aspx");
        }

        protected void BtnContinuar2_Click(object sender, EventArgs e)
        {
            //Guardar el folio y el movimiento

            ad.AgregarFolioMovimiento(txtPeriodoReporte.Text, txtMovimiento.Text, Request["folio"]);
            ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
            ade.Agregar(Request["folio"], 5);
            ad.AgregarEstado(Request["folio"], 5);
            mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente.", "esperaPromotoria.aspx");

        }

        protected void BtnContinuar3_Click(object sender, EventArgs e) //Potenciacion analista front
        {
            GuardarArchivosTramite();

            //Actualizar estados y terminar el proceso
            ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
            ade.Agregar(Request["folio"], 7);
            ad.AgregarEstado(Request["folio"], 7);
            mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente.", "esperaPromotoria.aspx");
        }

        protected void BtnContinuar4_Click(object sender, EventArgs e)
        {
            GuardarArchivosTramite();

            //guardar los mensajes de error para mostrar al front
            //ad.ActualizarErrores2(Request["folio"], txtErrores2.Text);
            ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
            ade.Agregar(Request["folio"], 6);
            ad.AgregarEstado(Request["folio"], 6);
            ad.ActualizarErrores(Request["folio"], ASPxHtmlEditor2.Html);
            mensajes.MostrarMensaje(this, "Devolución a Front por que los datos están incompletos.", "esperaPromotoria.aspx");
        }

        protected void BtnContinuar5_Click(object sender, EventArgs e)
        {
            //Enviar a dependencia potenciacion
            ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
            ade.Agregar(Request["folio"], 8);
            ad.AgregarEstado(Request["folio"], 8);
            mensajes.MostrarMensaje(this, "Se ha enviado un aviso por correo a la Dependencia para que pueda bajar sus archivos procesados.", "esperaPromotoria.aspx");
        }

        protected void BtnDocumento_Click(object sender, EventArgs e)
        {
            if (AsyncFileUpload3.HasFile)
            {
                string carpeta = "../ArchivosExcel/";
                carpeta = Server.MapPath(carpeta);
                string nombreArchivo3 = AsyncFileUpload3.FileName;
                AsyncFileUpload3.SaveAs(carpeta + nombreArchivo3);

                //Actualizar bd
                ad.AgregarDocumento(nombreArchivo3, Request["Folio"]);
                ad.ActualizarAsunto(Request["folio"].ToString(), ASPxHtmlEditor1.Html);
                ade.Agregar(Request["Folio"], 5);
                ad.AgregarEstado(Request["Folio"], 5);
                //Ver el archivo subido reflejado inmediatamente
                Documento.NavigateUrl = carpeta + nombreArchivo3.ToString();
                Documento.Text = nombreArchivo3.ToString() + "<br />";

                mensajes.MostrarMensaje(this, "Se subió el archivo exitosamente.", "esperaPromotoria.aspx");
            }
        }

        protected void BtnCartaPDF_Click(object sender, EventArgs e)
        {
            if (AsyncFileUpload4.HasFile)
            {
                string carpeta = "../DocsUpPotenciacion/";
                carpeta = Server.MapPath(carpeta);
                string nombreArchivo4 = AsyncFileUpload4.FileName;
                AsyncFileUpload4.SaveAs(carpeta + nombreArchivo4);

                //Instancia nueva tabla temporal
                DataTable dt = new DataTable();
                //Columnas para la nueva tabla
                dt.Columns.Add("Nombre");
                dt.Columns.Add("IdRegistro");

                //Fila para la tabla                            
                dt.Rows.Add(nombreArchivo4, "0");

                //Guardar el DataTable en el ViewState
                if (ViewState["archivosAgregados"] == null) //Primer registro
                    ViewState["archivosAgregados"] = dt;
                else //Registros que se vayan agregando
                {
                    DataTable dtt = new DataTable();
                    dtt.Columns.Add("Nombre");
                    dtt.Columns.Add("IdRegistro");
                    dtt = (DataTable)ViewState["archivosAgregados"];
                    dtt.Rows.Add(nombreArchivo4, "0");
                    ViewState["archivosAgregados"] = dtt;
                }

                gvArchivos.DataSource = ViewState["archivosAgregados"];
                gvArchivos.DataBind();
                ViewState["archivoTemporal"] = ViewState["archivosAgregados"];
            }

            //Actualizar bd
            //ad.AgregarCartaPDF(nombreArchivo4, Request["Folio"]);
            //ade.Agregar(Request["Folio"], 7);
            //ad.AgregarEstado(Request["Folio"], 7);
            //mensajes.MostrarMensaje(this, "Se subió el archivo exitosamente.", "esperaPromotoria.aspx");

        }




        protected void gvAgregado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int contar = gvAgregado.Rows.Count;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region Items

                //string _Poliza = DataBinder.Eval(e.Row.DataItem, "Poliza").ToString();
                string _Dependencia = DataBinder.Eval(e.Row.DataItem, "Dependencia").ToString();
                string _APaterno = DataBinder.Eval(e.Row.DataItem, "APaterno").ToString();
                string _AMaterno = DataBinder.Eval(e.Row.DataItem, "AMaterno").ToString();
                string _Nombres = DataBinder.Eval(e.Row.DataItem, "Nombres").ToString();
                string _FNacimiento = DataBinder.Eval(e.Row.DataItem, "FNacimiento").ToString();
                string _RFC = DataBinder.Eval(e.Row.DataItem, "RFC").ToString();
                string _CURP = DataBinder.Eval(e.Row.DataItem, "CURP").ToString();
                string _Sexo = DataBinder.Eval(e.Row.DataItem, "Sexo").ToString();
                string _CEntidadFederativa = DataBinder.Eval(e.Row.DataItem, "CEntidadFederativa").ToString();
                string _CMunicipio = DataBinder.Eval(e.Row.DataItem, "CMunicipio").ToString();
                string _NivelTabular = DataBinder.Eval(e.Row.DataItem, "NivelTabular").ToString();
                string _MPercepcionOBM = DataBinder.Eval(e.Row.DataItem, "MPercepcionOBM").ToString();
                string _Eventual = DataBinder.Eval(e.Row.DataItem, "Eventual").ToString();
                string _APAsegurado = DataBinder.Eval(e.Row.DataItem, "APAsegurado").ToString();
                string _AMAsegurado = DataBinder.Eval(e.Row.DataItem, "AMAsegurado").ToString();
                string _NAsegurado = DataBinder.Eval(e.Row.DataItem, "NAsegurado").ToString();
                string _FNAsegurado = DataBinder.Eval(e.Row.DataItem, "FNAsegurado").ToString();
                string _CURPAsegurado = DataBinder.Eval(e.Row.DataItem, "CURPAsegurado").ToString();
                string _SAsegurado = DataBinder.Eval(e.Row.DataItem, "SAsegurado").ToString();
                string _FAAsegurado = DataBinder.Eval(e.Row.DataItem, "FAAsegurado").ToString();
                string _TAsegurado = DataBinder.Eval(e.Row.DataItem, "TAsegurado").ToString();
                string _FIColectividad = DataBinder.Eval(e.Row.DataItem, "FIColectividad").ToString();
                string _SABasica = DataBinder.Eval(e.Row.DataItem, "SABasica").ToString();
                string _PBTReportar = DataBinder.Eval(e.Row.DataItem, "PBTReportar").ToString();
                string _MAPBasica = DataBinder.Eval(e.Row.DataItem, "MAPBasica").ToString();
                string _ITPDependencia = DataBinder.Eval(e.Row.DataItem, "ITPDependencia").ToString();

                //if (_Poliza == "")
                //{
                //    it.ValidacionCeldaAlerta(e.Row.Cells[1], "Campo vacío");
                //    alertasBasica += 1;
                //}
                if (_Dependencia.Length > 150)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[0], "Longitud del texto excesiva.");
                    alertasBasica += 1;
                }
                if (!it.Dependencias().Contains(_Dependencia))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[0], "La dependencia no existe o esta mal escrita.");
                    alertasBasica += 1;
                }
                if (_APaterno.Length > 150)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[1], "Campo vacío/longitud excesiva.");
                    alertasBasica += 1;
                }
                if (_AMaterno.Length > 150)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[2], "Longitud excesiva.");
                    alertasBasica += 1;
                }
                if (_Nombres.Length > 20)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[3], "Longitud excesiva.");
                    alertasBasica += 1;
                }

                DateTime fech;
                if (!DateTime.TryParseExact(_FNacimiento, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech) ||
                    _FNacimiento == "")
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[5], "Formato de fecha incorrecta/campo vacío.");
                    alertasBasica += 1;
                }
                if (_FNacimiento.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[5], "Celda vacía");
                    alertasVacio += 1;
                }
                if (_RFC.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[6], "Celda vacía");
                    alertasVacio += 1;
                }
                if (_RFC.Length > 13 || _RFC.Length < 13)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[6], "campo vacío/Longitud de texto excesiva o faltan datos.");
                    alertasBasica += 1;
                }
                if (_FNacimiento != "" && _FNacimiento.Length == 13)
                {
                    if (!_RFC.Contains(_FNacimiento.Substring(2, 6)))
                    {
                        it.ValidacionCeldaAlerta(e.Row.Cells[7], "La fecha de nacimiento no coincide con la del RFC.");
                        it.ValidacionCeldaAlerta(e.Row.Cells[8], "La fecha de nacimiento no coincide con la del RFC.");
                        alertasBasica += 1;
                    }
                }

                if (_CURP.Length > 18 || _CURP.Length < 18)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[9], "Campo vacío, longitud excesiva o faltan datos.");
                    alertasBasica += 1;
                }
                if (_CURP.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[9], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_Sexo.Length > 1 || (_Sexo != "H" && _Sexo != "M"))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[10], "Campo vacío, longitud incorrecta o indicador incorrecto.");
                    alertasBasica += 1;
                }

                if (_CEntidadFederativa.Length > 2 || _CEntidadFederativa.Length < 2 || !it.Entidades().Contains(_CEntidadFederativa))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[11], "Campo vacío, clave o longitud incorrecta.");
                    alertasBasica += 1;
                }
                if (_CEntidadFederativa.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[11], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_CMunicipio.Length > 3 || _CMunicipio.Length < 3 || !it.Municipios(_CEntidadFederativa, _CMunicipio))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[12], "Clave de Ciudad o Municipio no existe o el campo está vacío.");
                    alertasBasica += 1;
                }
                if (_CMunicipio.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[12], "Celda vacía");
                    alertasVacio += 1;
                }
                if (_NivelTabular.Length > 1 || !it.NivelTabular().Contains(_NivelTabular))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[13], "Campo vacío ó Nivel tabular incorrecto.");
                    alertasBasica += 1;
                }
                if (_NivelTabular.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[13], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_MPercepcionOBM.Length > 9 || _MPercepcionOBM.Contains(","))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[14], "Monto Percepcion Ordinaria incorrecta o tiene comas.");
                    alertasBasica += 1;
                }

                if (_Eventual != "SI" && _Eventual != "NO")
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[15], "Campo vacío o texto incorrecto");
                    alertasBasica += 1;
                }
                if (_Eventual.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[15], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_APAsegurado.Length > 20)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[16], "Campo vacío o tamaño excesivo.");
                    alertasBasica += 1;
                }
                if (_AMAsegurado.Length > 20)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[17], "Campo vacío o tamaño excesivo.");
                    alertasBasica += 1;
                }
                if (_NAsegurado.Length > 20)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[18], "Campo vacío o tamaño excesivo.");
                    alertasBasica += 1;
                }
                if (_NAsegurado.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[18], "Celda vacía");
                    alertasVacio += 1;
                }

                DateTime fech1;
                if (_FNAsegurado.Length > 8 || !DateTime.TryParseExact(_FNAsegurado, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech1))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[19], "Campo vacío o formato de fecha incorrecta.");
                    alertasBasica += 1;
                }
                if (_FNAsegurado.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[19], "Celda vacía");
                    alertasVacio += 1;
                }

                if (_CURPAsegurado.Length != 18)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[20], "campo vacío o longitud incorrecta.");
                    alertasBasica += 1;
                }

                if ((_SAsegurado != "H" && _SAsegurado != "M"))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[21], "Campo vacío o texto incorrecto.");
                    alertasBasica += 1;
                }

                DateTime fech2;
                if (_FAAsegurado.Length != 8 || !DateTime.TryParseExact(_FAAsegurado, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech2))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[22], "Campo vacío o formato fecha incorrecto.");
                    alertasBasica += 1;
                }

                if (_TAsegurado.Length > 1 || !it.TipoAsegurado().Contains(_TAsegurado))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[23], "Campo vacío, longitud incorrecta o tipo de asegurado incorrecto.");
                    alertasBasica += 1;
                }

                DateTime fech3;
                if (_FIColectividad.Length != 8 || !DateTime.TryParseExact(_FIColectividad, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech3))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[24], "Campo vacío o formato de fecha incorrecto.");
                    alertasBasica += 1;
                }

                if (!it.SumaAseguradaPorNivelTabular(_SABasica, _NivelTabular))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[25], "Campo vacío o tarifa incorrecta.");
                    alertasBasica += 1;
                }
                if (_SABasica.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[25], "Celda vacía");
                    alertasVacio += 1;
                }

                if (it.TarifaQuincenalTitulares(_NivelTabular, double.Parse(_SABasica), _TAsegurado) != _ITPDependencia)
                {
                    it.ValidacionCeldaAdvertencia(e.Row.Cells[25], "Nivel tabular, tipo de asegurado o súma básica incorrecta, verificar.");
                    alertasBasica += 1;
                }
                if (_PBTReportar.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[25], "Celda vacía");
                    alertasVacio += 1;
                }

                //if (_MAPBasica.Length == 0)
                //{
                //    e.Row.Cells[27].Text = "0";
                //    it.ValidacionCeldaAlerta(e.Row.Cells[26], "Tarifa incorrecta.");
                //}

                _MAPBasica = _MAPBasica.Length == 0 ? "0" : _MAPBasica;
                if (_MAPBasica.Contains("$"))
                    _MAPBasica = _MAPBasica.Replace('$', ' ');
                _PBTReportar = _PBTReportar.Length == 0 ? "0" : _PBTReportar;
                decimal suma = (decimal.Parse(_PBTReportar) + decimal.Parse(_MAPBasica));
                if (suma.ToString() != _ITPDependencia)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[25], "Importe incorrecto.");
                    alertasBasica += 1;
                }

                if (_ITPDependencia.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[25], "Celda vacía.");
                    alertasVacio += 1;
                }

                //Suma de Importe a Pagar por la Dependencia
                if (_ITPDependencia.Contains("$"))
                    _ITPDependencia = _ITPDependencia.Replace('$', ' ');
                if (_ITPDependencia == "") _ITPDependencia = "0";
                SumaPagarDependencia += decimal.Parse(_ITPDependencia);

                //e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvAgregado, "Edit$" + e.Row.RowIndex);
                //e.Row.Attributes["style"] = "cursor:pointer";

                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].RowSpan = 26;
                e.Row.Cells[0].Text = "<font style='font-size: 16px'>Registros Totales: " + contar.ToString() + ", <span style='color: red'>alertas: " + alertasBasica.ToString() + "</span>, <span style='color: navy'>celdas vacías: " + alertasVacio.ToString() + "</span>, Suma Total a Pagar por Dependencia: " + string.Format("{0:C}", SumaPagarDependencia) + "</font>";
            }
        }

        protected void gvAgregado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAgregado.EditIndex = e.NewEditIndex;
            //gvAgregado.Rows[gvAgregado.EditIndex].BackColor = System.Drawing.Color.Gray;
            CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);
            if (gvAgregado.Rows.Count > 0)
            {
                DropDownList ddl = gvAgregado.Rows[e.NewEditIndex].Cells[1].FindControl("ddlDependencia") as DropDownList;
                if (ddl != null)
                {
                    dep.SeleccionarDependencias_DropDrownList(ref ddl);
                    //ddl.DataSource = it.Dependencias();
                    //ddl.DataBind();
                    //ddl.Items.Insert(0, new ListItem("SELECCIONAR", "SELECCIONAR"));
                }
                gvAgregado.Rows[gvAgregado.EditIndex].BackColor = System.Drawing.Color.Gray;
            }
        }

        protected void gvAgregado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAgregado.EditIndex = -1;
            CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);
            ProcesarEncabezadoFijo(gvAgregado);
        }

        protected void gvAgregado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = (DataTable)Session["RegistrosTemporales"];

            gvAgregado.EditIndex = e.RowIndex;
            gvAgregado.Rows[gvAgregado.EditIndex].BackColor = System.Drawing.Color.Green;

            GridViewRow row = gvAgregado.Rows[e.RowIndex];
            //dt.Rows[row.DataItemIndex]["Poliza"] =                              ((TextBox)(row.Cells[1].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Dependencia"] = ((DropDownList)(row.Cells[2].Controls[1])).SelectedValue;
            dt.Rows[row.DataItemIndex]["APaterno"] = ((TextBox)(row.Cells[3].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["AMaterno"] = ((TextBox)(row.Cells[4].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Nombres"] = ((TextBox)(row.Cells[5].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FNacimiento"] = ((TextBox)(row.Cells[6].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["RFC"] = ((TextBox)(row.Cells[7].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CURP"] = ((TextBox)(row.Cells[8].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Sexo"] = ((TextBox)(row.Cells[9].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CEntidadFederativa"] = ((TextBox)(row.Cells[10].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CMunicipio"] = ((TextBox)(row.Cells[11].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["NivelTabular"] = ((TextBox)(row.Cells[12].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MPercepcionOBM"] = ((TextBox)(row.Cells[13].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Eventual"] = ((TextBox)(row.Cells[14].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["APAsegurado"] = ((TextBox)(row.Cells[15].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["AMAsegurado"] = ((TextBox)(row.Cells[16].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["NAsegurado"] = ((TextBox)(row.Cells[17].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FNAsegurado"] = ((TextBox)(row.Cells[18].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CURPAsegurado"] = ((TextBox)(row.Cells[19].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAsegurado"] = ((TextBox)(row.Cells[20].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FAAsegurado"] = ((TextBox)(row.Cells[21].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["TAsegurado"] = ((TextBox)(row.Cells[22].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FIColectividad"] = ((TextBox)(row.Cells[23].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SABasica"] = ((TextBox)(row.Cells[24].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PBTReportar"] = ((TextBox)(row.Cells[25].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MAPBasica"] = ((TextBox)(row.Cells[26].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["ITPDependencia"] = ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();

            gvAgregado.EditIndex = -1;

            CargarTabla(gvAgregado, dt);
        }

        protected void gvAgregado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string currentCommand = e.CommandName;
            if (currentCommand == "Update")
            {
                ProcesarEncabezadoFijo(gvAgregado);
            }
        }

        protected void gvPotenciacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int contar = gvPotenciacion.Rows.Count;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region Items

                //string _Poliza = DataBinder.Eval(e.Row.DataItem, "Poliza").ToString();
                string _Dependencia = DataBinder.Eval(e.Row.DataItem, "Dependencia").ToString();
                string _APaterno = DataBinder.Eval(e.Row.DataItem, "APaterno").ToString();
                string _AMaterno = DataBinder.Eval(e.Row.DataItem, "AMaterno").ToString();
                string _Nombres = DataBinder.Eval(e.Row.DataItem, "Nombres").ToString();
                string _FNacimiento = DataBinder.Eval(e.Row.DataItem, "FNacimiento").ToString();
                string _RFC = DataBinder.Eval(e.Row.DataItem, "RFC").ToString();
                string _CURP = DataBinder.Eval(e.Row.DataItem, "CURP").ToString();
                string _Sexo = DataBinder.Eval(e.Row.DataItem, "Sexo").ToString();
                string _CEntidadFederativa = DataBinder.Eval(e.Row.DataItem, "CEntidadFederativa").ToString();
                string _CMunicipio = DataBinder.Eval(e.Row.DataItem, "CMunicipio").ToString();
                string _NivelTabular = DataBinder.Eval(e.Row.DataItem, "NivelTabular").ToString();
                string _MPercepcionOBM = DataBinder.Eval(e.Row.DataItem, "MPercepcionOBM").ToString();
                string _Eventual = DataBinder.Eval(e.Row.DataItem, "Eventual").ToString();
                string _APAsegurado = DataBinder.Eval(e.Row.DataItem, "APAsegurado").ToString();
                string _AMAsegurado = DataBinder.Eval(e.Row.DataItem, "AMAsegurado").ToString();
                string _NAsegurado = DataBinder.Eval(e.Row.DataItem, "NAsegurado").ToString();
                string _FNAsegurado = DataBinder.Eval(e.Row.DataItem, "FNAsegurado").ToString();
                string _CURPAsegurado = DataBinder.Eval(e.Row.DataItem, "CURPAsegurado").ToString();
                string _SAsegurado = DataBinder.Eval(e.Row.DataItem, "SAsegurado").ToString();
                string _FAAsegurado = DataBinder.Eval(e.Row.DataItem, "FAAsegurado").ToString();
                string _TAsegurado = DataBinder.Eval(e.Row.DataItem, "TAsegurado").ToString();
                string _FIColectividad = DataBinder.Eval(e.Row.DataItem, "FIColectividad").ToString();
                string _SABasica = DataBinder.Eval(e.Row.DataItem, "SABasica").ToString();
                string _SAPotenciada = DataBinder.Eval(e.Row.DataItem, "SAPotenciada").ToString();
                string _SAT = DataBinder.Eval(e.Row.DataItem, "SAT").ToString();
                string _PrimaPQR = DataBinder.Eval(e.Row.DataItem, " PrimaPotenciadaQR").ToString();
                string _FormaPago = DataBinder.Eval(e.Row.DataItem, " FormaPago").ToString();
                string _MontoAPPABPC = DataBinder.Eval(e.Row.DataItem, " MontoAjustePrimaP").ToString();
                string _ImporteTPPPFRSP = DataBinder.Eval(e.Row.DataItem, " ImporteTotalPagar").ToString();
                string _FAAseguradoSAP = DataBinder.Eval(e.Row.DataItem, " FechaAASA").ToString();

                //if (_Dependencia.Length > 150)
                //{
                //    it.ValidacionCeldaAlerta(e.Row.Cells[0], "Longitud del texto excesiva.");
                //    alertasPotenciacion += 1;
                //}
                //if (!it.Dependencias().Contains(_Dependencia))
                //{
                //    it.ValidacionCeldaAlerta(e.Row.Cells[0], "La dependencía no existe o esta mal, seleccione la correcta.");
                //    alertasPotenciacion += 1;
                //}
                //if (_APaterno.Length > 150)
                //{
                //    it.ValidacionCeldaAlerta(e.Row.Cells[1], "Campo vacío/longitud excesiva.");
                //    alertasPotenciacion += 1;
                //}
                //if (_AMaterno.Length > 150)
                //{
                //    it.ValidacionCeldaAlerta(e.Row.Cells[2], "Longitud excesiva.");
                //    alertasPotenciacion += 1;
                //}
                //if (_Nombres.Length > 20)
                //{
                //    it.ValidacionCeldaAlerta(e.Row.Cells[3], "Longitud excesiva.");
                //    alertasPotenciacion += 1;
                //}
                if (_Nombres.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[3], "Celda no debe estar vacía.");
                    alertasVacio += 1;
                }

                DateTime fech;
                if (!DateTime.TryParseExact(_FNacimiento, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech) ||
                    _FNacimiento == "")
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[4], "Formato de fecha incorrecta/campo vacío.");
                    alertasPotenciacion += 1;
                }
                if (_FNacimiento.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[4], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_RFC.Length > 13 || _RFC.Length < 13)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[5], "campo vacío/Longitud de texto excesiva o faltan datos.");
                    alertasPotenciacion += 1;
                }
                if (_FNacimiento != "" && _FNacimiento.Length == 13)
                {
                    if (!_RFC.Contains(_FNacimiento.Substring(2, 6)))
                    {
                        it.ValidacionCeldaAlerta(e.Row.Cells[4], "La fecha de nacimiento no coincide con la del RFC.");
                        it.ValidacionCeldaAlerta(e.Row.Cells[5], "La fecha de nacimiento no coincide con la del RFC.");
                        alertasPotenciacion += 1;
                    }
                }
                if (_RFC.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[5], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_CURP.Length > 18 || _CURP.Length < 18)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[6], "Campo vacío, longitud excesiva o faltan datos.");
                    alertasPotenciacion += 1;
                }
                if (_CURP.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[6], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_Sexo.Length > 1 || (_Sexo != "H" && _Sexo != "M"))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[7], "Campo vacío, longitud incorrecta o indicador incorrecto.");
                    alertasPotenciacion += 1;
                }

                if (_CEntidadFederativa.Length > 2 || _CEntidadFederativa.Length < 2 || !it.Entidades().Contains(_CEntidadFederativa))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[8], "Campo vacío, clave o longitud incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_CEntidadFederativa.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[8], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_CMunicipio.Length > 3 || _CMunicipio.Length < 3 || !it.Municipios(_CEntidadFederativa, _CMunicipio))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[9], "Clave de Ciudad o Municipio no existe o el campo está vacío.");
                    alertasPotenciacion += 1;
                }
                if (_CMunicipio.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[9], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_NivelTabular.Length > 2 || !it.NivelTabular().Contains(_NivelTabular))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[10], "Campo vacío ó Nivel tabular incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_NivelTabular.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[10], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_MPercepcionOBM.Length > 9 || _MPercepcionOBM.Contains(","))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[11], "Monto Percepcion Ordinaria incorrecta o tiene comas.");
                    alertasPotenciacion += 1;
                }

                if ((_Eventual != "SI" && _Eventual != "NO"))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[12], "Campo vacío o texto incorrecto");
                    alertasPotenciacion += 1;
                }
                if (_Eventual.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[12], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_APAsegurado.Length > 20)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[13], "Campo vacío o tamaño excesivo.");
                    alertasPotenciacion += 1;
                }
                if (_AMAsegurado.Length > 20)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[14], "Campo vacío o tamaño excesivo.");
                    alertasPotenciacion += 1;
                }
                if (_NAsegurado.Length > 20)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[15], "Campo vacío o tamaño excesivo.");
                    alertasPotenciacion += 1;
                }
                if (_NAsegurado.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[15], "Celda vacía.");
                    alertasVacio += 1;
                }

                DateTime fech1;
                if (_FNAsegurado.Length > 8 || !DateTime.TryParseExact(_FNAsegurado, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech1))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[16], "Campo vacío o formato de fecha incorrecta.");
                    alertasPotenciacion += 1;
                }

                if (_CURPAsegurado.Trim().Length != 18)
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[17], "campo vacío o longitud incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_CURPAsegurado.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[17], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (_SAsegurado.Length == 0 || (_SAsegurado != "H" && _SAsegurado != "M"))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[18], "Campo vacío o texto incorrecto.");
                    alertasPotenciacion += 1;
                }

                DateTime fech2;
                if (_FAAsegurado.Length != 8 || !DateTime.TryParseExact(_FAAsegurado, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech2))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[19], "Campo vacío o formato fecha incorrecto.");
                    alertasPotenciacion += 1;
                }

                if (_TAsegurado.Length > 1 || !it.TipoAsegurado().Contains(_TAsegurado))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[20], "Campo vacío, longitud incorrecta o tipo de asegurado incorrecto.");
                    alertasPotenciacion += 1;
                }
                if (_TAsegurado.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[20], "Celda vacía.");
                    alertasVacio += 1;
                }

                DateTime fech3;
                if (_FIColectividad.Length != 8 || !DateTime.TryParseExact(_FIColectividad, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech3))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[21], "Campo vacío o formato de fecha incorrecto.");
                    alertasPotenciacion += 1;
                }


                if (!it.SumaAseguradaBasicaPotenciacion().Contains(_SABasica))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[22], "Campo vacío o SAB incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_SABasica.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[22], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (!it.SumaAseguradaPotenciada().Contains(_SAPotenciada))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[23], "Campo vacío o SAP incorrecta.");
                    alertasPotenciacion += 1;
                }
                if (_SAPotenciada.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[23], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_SABasica == "") _SABasica = "0";
                if (_SAPotenciada == "") _SAPotenciada = "0";
                decimal suma = decimal.Parse(_SABasica) + decimal.Parse(_SAPotenciada);
                if (!it.SumaAseguradaPotenciada().Contains(suma.ToString()))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[24], "Campo vacío o clave incorrecta.");
                    alertasPotenciacion += 1;
                }

                if (_PrimaPQR.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[25], "Celda vacía.");
                    alertasVacio += 1;
                }

                if (!it.TarifaPotenciada(_NivelTabular, _SAT, _TAsegurado).Contains(_PrimaPQR))
                {
                    it.ValidacionCeldaAdvertencia(e.Row.Cells[25], "Prima incorrecta.");
                    advertenciasPotenciacion += 1;
                }

                if (_FormaPago.Length == 0)
                {
                    //26
                }
                if (_MontoAPPABPC.Length == 0)
                {
                    //27
                    it.ValidacionCeldaVacia(e.Row.Cells[27], "Celda vacía.");
                    alertasVacio += 1;
                }
                if (_ImporteTPPPFRSP.Length == 0)
                {
                    //28
                    it.ValidacionCeldaVacia(e.Row.Cells[28], "Celda vacía.");
                    alertasVacio += 1;
                }
                DateTime fech5;
                if (!DateTime.TryParseExact(_FAAseguradoSAP, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech5))
                {
                    it.ValidacionCeldaAlerta(e.Row.Cells[29], "Campo vacío o formato de fecha incorrecto.");
                    advertenciasPotenciacion += 1;
                }
                if (_FAAseguradoSAP.Length == 0)
                {
                    it.ValidacionCeldaVacia(e.Row.Cells[29], "Celda vacía.");
                    alertasVacio += 1;
                }

                //e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPotenciacion, "Edit$" + e.Row.RowIndex);
                //e.Row.Attributes["style"] = "cursor:pointer";

                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].RowSpan = 27;
                e.Row.Cells[0].Text = "<font style='font-size: 16px'>Registros Totales: " + contar.ToString() + ", <span style='color: red'>errores: " + alertasPotenciacion.ToString() + "</span>, <span style='color: orange'>advertencias: " + advertenciasPotenciacion.ToString() + "</span>, <span>celdas vacías: " + alertasVacio.ToString() + "</span></font>";

            }
        }

        protected void gvPotenciacion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPotenciacion.EditIndex = e.NewEditIndex;
            //gvAgregado.Rows[gvAgregado.EditIndex].BackColor = System.Drawing.Color.Gray;
            CargarTabla(gvPotenciacion, (DataTable)Session["RegistrosTemporales"]);
            if (gvPotenciacion.Rows.Count > 0)
            {
                DropDownList ddl = gvPotenciacion.Rows[e.NewEditIndex].Cells[1].FindControl("ddlDependencia") as DropDownList;
                if (ddl != null)
                {
                    dep.SeleccionarDependencias_DropDrownList(ref ddl);
                    //ddl.DataSource = it.Dependencias();
                    //ddl.DataBind();
                    //ddl.Items.Insert(0, new ListItem("SELECCIONAR", "SELECCIONAR"));
                }
                gvPotenciacion.Rows[gvPotenciacion.EditIndex].BackColor = System.Drawing.Color.Gray;
            }
        }

        protected void gvPotenciacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPotenciacion.EditIndex = -1;
            CargarTabla(gvPotenciacion, (DataTable)Session["RegistrosTemporales"]);
            ProcesarEncabezadoFijo(gvPotenciacion);
        }

        protected void gvPotenciacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = (DataTable)Session["RegistrosTemporales"];

            gvPotenciacion.EditIndex = e.RowIndex;
            gvPotenciacion.Rows[gvPotenciacion.EditIndex].BackColor = System.Drawing.Color.Green;

            GridViewRow row = gvPotenciacion.Rows[e.RowIndex];
            dt.Rows[row.DataItemIndex]["Poliza"] = ((TextBox)(row.Cells[1].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Dependencia"] = ((DropDownList)(row.Cells[2].Controls[1])).SelectedValue;
            dt.Rows[row.DataItemIndex]["APaterno"] = ((TextBox)(row.Cells[3].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["AMaterno"] = ((TextBox)(row.Cells[4].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Nombres"] = ((TextBox)(row.Cells[5].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FNacimiento"] = ((TextBox)(row.Cells[6].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["RFC"] = ((TextBox)(row.Cells[7].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CURP"] = ((TextBox)(row.Cells[8].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Sexo"] = ((TextBox)(row.Cells[9].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CEntidadFederativa"] = ((TextBox)(row.Cells[10].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CMunicipio"] = ((TextBox)(row.Cells[11].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["NivelTabular"] = ((TextBox)(row.Cells[12].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MPercepcionOBM"] = ((TextBox)(row.Cells[13].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Eventual"] = ((TextBox)(row.Cells[14].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["APAsegurado"] = ((TextBox)(row.Cells[15].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["AMAsegurado"] = ((TextBox)(row.Cells[16].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["NAsegurado"] = ((TextBox)(row.Cells[17].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FNAsegurado"] = ((TextBox)(row.Cells[18].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CURPAsegurado"] = ((TextBox)(row.Cells[19].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAsegurado"] = ((TextBox)(row.Cells[20].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FAAsegurado"] = ((TextBox)(row.Cells[21].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["TAsegurado"] = ((TextBox)(row.Cells[22].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FIColectividad"] = ((TextBox)(row.Cells[23].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SABasica"] = ((TextBox)(row.Cells[24].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAPotenciada"] = ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAT"] = ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PrimaPotenciadaQR"] = ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FormaPago"] = ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MontoAjustePrimaP"] = ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["ImporteTotalPagar"] = ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FechaAASA"] = ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();

            gvPotenciacion.EditIndex = -1;

            CargarTabla(gvPotenciacion, dt);
        }

        protected void gvPotenciacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string currentCommand = e.CommandName;
            if (currentCommand == "Update")
            {
                ProcesarEncabezadoFijo(gvPotenciacion);
            }
        }

        protected void gvArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string carpeta = string.Empty;
            if (!at.SeleccionarContadosPorFolio(Request["folio"]))
                carpeta = "../DocsUpPotenciacion/";
            else
                carpeta = "../ArchivosExcel/";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowObject = (DataRowView)e.Row.DataItem;
                HyperLink hlkNombresarchivos = (HyperLink)e.Row.FindControl("hlkNombresArchivos");

                hlkNombresarchivos.NavigateUrl = carpeta + rowObject["Archivo"].ToString();
            }
        }

        protected void lnkQuitarArchivo_Click(object sender, EventArgs e)
        {
            LinkButton imgbtn1 = sender as LinkButton;
            GridViewRow row1 = imgbtn1.NamingContainer as GridViewRow;

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["archivosAgregados"];
            dt.Rows.RemoveAt(row1.RowIndex);
            ViewState["archivosAgregados"] = dt;

            ViewState["archivoTemporal"] = null;
            ViewState["archivoTemporal"] = ViewState["archivosAgregados"];

            string carpeta1 = "../ArchivosExcel/";
            carpeta1 = Server.MapPath(carpeta1);
            if (File.Exists(carpeta1 + imgbtn1.CommandArgument))
                File.Delete(carpeta1 + imgbtn1.CommandArgument);
            string carpeta2 = "../DocsUpPotenciacion/";
            carpeta2 = Server.MapPath(carpeta2);
            if (File.Exists(carpeta2 + imgbtn1.CommandArgument))
                File.Delete(carpeta2 + imgbtn1.CommandArgument);
            
            gvArchivos.DataSource = ViewState["archivosAgregados"];
            gvArchivos.DataBind();
        }


        protected void btnSubirExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (AsyncFileUpload1.PostedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    mensajes.MostrarMensaje(this, "El archivo que intenta usar no es de excel, revíse.");
                    return;
                }

                if (AsyncFileUpload1.HasFile)
                {
                    DataTable dt = new DataTable();

                    //sólo subir el archivo
                    string carpeta = "../ArchivosExcel/";
                    carpeta = Server.MapPath(carpeta);
                    string nombreArchivo = AsyncFileUpload1.FileName;
                    AsyncFileUpload1.SaveAs(carpeta + nombreArchivo);

                    if (string.IsNullOrEmpty(Request["folio"]))
                    {
                        string nofolio = string.Empty;
                        if (string.IsNullOrEmpty(txtFoliovalidar.Text))
                            nofolio = DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "DH" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "S";
                        else
                            nofolio = txtFoliovalidar.Text;

                        //Guardar en bd
                        if (rdbCobertura.SelectedValue == "1")
                            ad.AgregarBasica(nombreArchivo, nofolio, txtNoPoliza.Text, rdbCobertura.SelectedValue, txtNombreSolicitante.Text, txtSolicitanteCorreo.Text, ASPxHtmlEditor1.Html, ddlTrimestreQuincena.SelectedValue, ddlAnn.SelectedValue, manejo_sesion.Credencial.IdRol.ToString());
                        else
                            ad.AgregarPotenciacion(nombreArchivo, nofolio, txtNoPoliza.Text, rdbCobertura.SelectedValue, txtNombreSolicitante.Text, txtSolicitanteCorreo.Text, ASPxHtmlEditor1.Html, ddlTrimestreQuincena.SelectedValue, ddlAnn.SelectedValue, manejo_sesion.Credencial.IdRol.ToString());

                        ade.Agregar(nofolio, 1);
                        mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente. Número de Folio Asignado: " + nofolio, "esperaPromotoria.aspx");
                    }
                    else
                    {
                        ad.AgregarEstado(Request["folio"], 4);
                        mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente.", "esperaPromotoria.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Referencia a objeto no establecida como instancia de un objeto."))
                    mensajes.MostrarMensaje(this, "No subió el archivo, intente de nuevo.");
                else
                    mensajes.MostrarMensaje(this, "Error al subir el archivo: " + ex.Message);
            }
        }

        protected void BtnSubirExcelParaValidar_Click(object sender, EventArgs e)
        {
            //Subir el archivo por el analista para validarlo
            ExcelPackage pagina = new ExcelPackage(AsyncFileUpload2.FileContent);
            dt = wfiplib.ExtensionPaqueteriaExcel.Excel_A_DataTable(pagina);
            if (dt.Columns.Count == 26 && rdbCobertura.SelectedValue == "2")
            {
                mensajes.MostrarMensaje(this, "El archivo que intenta cargar no es de Cobertura Básica");
                return;
            }
            else if (dt.Columns.Count == 30 && rdbCobertura.SelectedValue == "1")
            {
                mensajes.MostrarMensaje(this, "El archivo que intenta cargar no es de Potenciación");
                return;
            }

            if (rdbCobertura.SelectedValue == "1")
            {
                CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);
            }
            else if (rdbCobertura.SelectedValue == "2")
            {
                CargarTabla(gvPotenciacion, (DataTable)Session["RegistrosTemporales"]);
            }

            //Verificar si el grid tiene registros

            if (rdbCobertura.SelectedValue == "1") //Basica
            {
                if (gvAgregado.Rows.Count > 0)
                {
                    DataTable dtt = new DataTable();
                    dtt = (DataTable)gvAgregado.DataSource;
                    dt.Merge(dtt);
                }
                trErrores.Visible = true;

                Session["RegistrosTemporales"] = dt;

                CargarTabla(gvAgregado, dt);

                ProcesarEncabezadoFijo(gvAgregado);

                BtnContinuar.Visible = true;
                BtnCancelar.Visible = true;
            }
            else if (rdbCobertura.SelectedValue == "2") //Potenciada
            {
                if (gvPotenciacion.Rows.Count > 0)
                {
                    DataTable dtt = new DataTable();
                    dtt = (DataTable)gvPotenciacion.DataSource;
                    dt.Merge(dtt);
                }

                Session["RegistrosTemporales"] = dt;

                trErrores.Visible = true;

                CargarTabla(gvPotenciacion, dt);

                ProcesarEncabezadoFijo(gvPotenciacion);

                BtnCancelar.Visible = true;
                BtnValidacion.Visible = true;
            }
        }

        protected void BtnSubirPDFXLS_Click(object sender, EventArgs e)
        {
            if (AsyncFileUpload5.HasFile)
            {
                string carpeta = "../ArchivosExcel/";
                carpeta = Server.MapPath(carpeta);
                string nombreArchivo5 = AsyncFileUpload5.FileName;
                AsyncFileUpload5.SaveAs(carpeta + nombreArchivo5);

                string nombreArchivo6 = AsyncFileUpload6.FileName;
                AsyncFileUpload6.SaveAs(carpeta + nombreArchivo6);

                //Actualizar bd
                ad.AgregarPDF(nombreArchivo5, Request["Folio"]);
                ad.AgregarXML(nombreArchivo6, Request["Folio"]);
                ade.Agregar(Request["Folio"], 5);
                ad.AgregarEstado(Request["Folio"], 5);
                mensajes.MostrarMensaje(this, "Se subieron los archivos exitosamente.", "esperaPromotoria.aspx");
            }
        }

        protected void rdbCobertura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbCobertura.SelectedIndex == 0)
            {
                ViewState["cobertura"] = "ba";

                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("1er. Trimestre", "1"));
                items.Add(new ListItem("2do. Trimestre", "2"));
                items.Add(new ListItem("3er. Trimestre", "3"));
                items.Add(new ListItem("4to. Trimestre", "4"));
                ddlTrimestreQuincena.Items.AddRange(items.ToArray());

                lblTrimestreQuincena.Text = "Trimestre:";
            }
            else if (rdbCobertura.SelectedIndex == 1)
            {
                ViewState["cobertura"] = "po";

                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("1era.", "1"));
                items.Add(new ListItem("2da.", "2"));
                items.Add(new ListItem("3era.", "3"));
                items.Add(new ListItem("4ta.", "4"));
                items.Add(new ListItem("5ta.", "5"));
                items.Add(new ListItem("6ta.", "6"));
                items.Add(new ListItem("7ma.", "7"));
                items.Add(new ListItem("8va.", "8"));
                items.Add(new ListItem("9na.", "9"));
                items.Add(new ListItem("10ma.", "10"));
                items.Add(new ListItem("11va.", "11"));
                items.Add(new ListItem("12va.", "12"));
                items.Add(new ListItem("13va.", "13"));
                items.Add(new ListItem("14va.", "14"));
                items.Add(new ListItem("15va.", "15"));
                items.Add(new ListItem("16va.", "16"));
                items.Add(new ListItem("17va.", "17"));
                items.Add(new ListItem("18va.", "18"));
                items.Add(new ListItem("19na.", "19"));
                items.Add(new ListItem("20ma.", "20"));
                items.Add(new ListItem("21a.", "21"));
                items.Add(new ListItem("22da.", "22"));
                items.Add(new ListItem("23ra.", "23"));
                items.Add(new ListItem("24ta.", "24"));

                ddlTrimestreQuincena.Items.AddRange(items.ToArray());

                lblTrimestreQuincena.Text = "Quincena:";
            }
            trTrimestre.Visible = true;
        }

        protected void txtFoliovalidar_TextChanged(object sender, EventArgs e)
        {
            ProcesarDetalleFoliado(txtFoliovalidar.Text);
        }

        #endregion

        #region Metodos ***************************************************************************************************************************************

        protected void ProcesarEncabezadoFijo(GridView grid)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Key", "<script>HacerEncabezadoEstatico('" + grid.ClientID + "', 300, 1170, 80, true); </script>", false);
            //ScriptManager.RegisterStartupScript(Page, GetType(), "Key", "<script>HacerEncabezadoEstatico('" + gvAgregado.ClientID + "', 300, 1170, 80, true); </script>", false);
        }

        protected void CargarTabla(GridView gridview, DataTable dt)
        {
            gridview.DataSource = dt;
            gridview.DataBind();
        }

        protected void CargarTablaPorFolio(string folio, string cobertura)
        {
            if (cobertura == "Básica")
            {
                CargarTabla(gvAgregado, pd.SeleccionarPorId(folio));
                Session["RegistrosTemporales"] = pd.SeleccionarPorId(folio);
                ProcesarEncabezadoFijo(gvAgregado);
            }
            else if (cobertura == "Potenciada")
            {
                CargarTabla(gvPotenciacion, pd.SeleccionarPorId(folio));
                Session["RegistrosTemporales"] = pd.SeleccionarPorId(folio);
                ProcesarEncabezadoFijo(gvPotenciacion);
            }
        }

        protected void ProcesarDetalleFoliado(string folio)
        {
            //Detalle
#if DEBUG
            string rutaArchivos = "../ArchivosExcel/";
#else
            string rutaArchivos = "~/ArchivosExcel/";
#endif
            DataRow detalle = null;
            if (!ad.SeleccionarDetalle(folio, ref detalle))
            {
                txtFoliovalidar.Focus();
                return;
            }

            txtNoPoliza.Text = detalle[4].ToString();
            txtNoPoliza_TextChanged(null, null);
            txtFecha.Text = detalle[2].ToString();
            txtNombreSolicitante.Text = detalle[6].ToString();

            txtSolicitanteCorreo.Text = detalle[7].ToString();
            //txtAsunto.Text = detalle[8].ToString();
            ASPxHtmlEditor1.Html = detalle[8].ToString();
            txtErrores.Text = detalle[9].ToString();

            string cobertura = "";
            //if (!string.IsNullOrEmpty(Request["cobertura"]))
            cobertura = detalle[5].ToString() == "1" ? "1" : "2";
            //else
            //    cobertura = Request["cobertura"].ToString();

            string estado = detalle[14].ToString();

            if (detalle[5].ToString() == "1") // Básica
            {
                switch (estado) //estado
                {
                    //común 

                    case "1":   //inicio (dependencia->analista)
                        trSubirExcel.Visible = false;
                        trArchivoExcel.Visible = true;
                        trSubirExcelParaValidar.Visible = true;

                        
                        break;
                    case "2":   //revisión analista (analista->dependencia ó analista->back)
                        trNoFolioExiste.Visible = true;
                        break;
                    case "3":   //procesando (back->analista)
                        trArchivoExcel.Visible = true;
                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        trErrores.Visible = false;

                        trSubirExcel.Visible = false;
                        trSubirPDFXLS.Visible = true;
                        trsubirPDF.Visible = true;
                        trsubirXLS.Visible = true;
                        break;
                    case "4":   //vuelto a procesar (dependencia->analista)
                        trArchivoExcel.Visible = true;
                        trSubirExcelParaValidar.Visible = true;

                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        trNoFolioExiste.Visible = false;
                        trSubirExcel.Visible = false;
                        trErrores.Visible = false;
                        //BtnContinuar.Visible = true;
                        //BtnCancelar.Visible = true;
                        break;
                    case "5":   //revisión documentos anaññista (analista->dependencia ó analista->back)
                        BtnContinuar.Visible = false;
                        BtnTerminar.Visible = true;
                        BtnCancelar.Visible = true;

                        trSubirExcel.Visible = false;
                        trSubirExcelParaValidar.Visible = false;
                        trArchivoExcel.Visible = true;

                        gvAgregado.Visible = false;
                        gvPotenciacion.Visible = false;

                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        PDF.NavigateUrl = rutaArchivos + detalle[10].ToString();
                        PDF.Text = detalle[10].ToString() + "<br />";

                        XML.NavigateUrl = rutaArchivos + detalle[11].ToString();
                        XML.Text = detalle[11].ToString() + "<br />";

                        dvEspacioPDF.Visible = true;
                        ltMuestraPdf.Text = "";
                        ltMuestraPdf.Text = "<embed type='application/pdf' height='100%' width='100%' src='" + rutaArchivos + detalle[10] + "'></embed>";
                        break;
                    case "6":   //concluído (analista->dependencia)
                        trSubirExcel.Visible = false;
                        trSubirExcelParaValidar.Visible = false;

                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";
                        //Archivos de 100 posiciones
                        CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                        CienPosPagos.Text = detalle[17].ToString() + "<br />";

                        CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                        CienPosCance.Text = detalle[18].ToString() + "<br />";

                        //Carta PDF (Ahora otrosdocumentos, todos los agregados por el back de potenciación)
                        CartaPDF.NavigateUrl = rutaArchivos + detalle[26].ToString();
                        CartaPDF.Text = detalle[26].ToString() + "<br />";
                        BtnContinuar.Visible = false;

                        ASPxHtmlEditor1.Settings.AllowDesignView = false;
                        ASPxHtmlEditor1.Settings.AllowHtmlView = false;
                        ASPxHtmlEditor1.Settings.AllowPreview = true;

                        break;
                }

                rdbCobertura.SelectedIndex = 0;
                rdbCobertura_SelectedIndexChanged(null, null);
                ddlTrimestreQuincena.SelectedValue = detalle[12].ToString();
                ddlAnn.SelectedValue = detalle[13].ToString();
                
                //BtnValidacion.Visible = false;
                //BtnContinuar.Visible = false;
                //if (manejo_sesion.Credencial.IdRol == 21)//Analista de básica
                //{
                    //trSubirExcel.Visible = false;
                    //trArchivoExcel.Visible = true;
                    //trSubirExcelParaValidar.Visible = true;

                    //hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                    //hlkExcel.Text = detalle[1].ToString() + "<br />";

                    //switch (estado) //Estado
                    //{
                    //    case "4": //devuelta para reprocesarse
                    //        trErrores.Visible = true;
                    //        BtnContinuar.Visible = true;
                    //        BtnCancelar.Visible = true;
                    //        break;
                    //    case "5": //a analista para enviar a dependencia
                    //        BtnContinuar.Visible = false;
                    //        BtnTerminar.Visible = true;
                    //        BtnCancelar.Visible = true;

                    //        trSubirExcel.Visible = false;
                    //        trSubirExcelParaValidar.Visible = false;

                    //        gvAgregado.Visible = false;
                    //        gvPotenciacion.Visible = false;

                    //        PDF.NavigateUrl = rutaArchivos + detalle[10].ToString();
                    //        PDF.Text = detalle[10].ToString() + "<br />";

                    //        XML.NavigateUrl = rutaArchivos + detalle[11].ToString();
                    //        XML.Text = detalle[11].ToString() + "<br />";

                    //        dvEspacioPDF.Visible = true;
                    //        ltMuestraPdf.Text = "";
                    //        ltMuestraPdf.Text = "<embed type='application/pdf' height='100%' width='100%' src='" + rutaArchivos + detalle[10] + "'></embed>";
                    //        break;
                    //    case "6": //Concluído, sólo para revisión
                    //        trSubirExcel.Visible = false;
                    //        trSubirExcelParaValidar.Visible = false;

                    //        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                    //        hlkExcel.Text = detalle[1].ToString() + "<br />";
                    //        //Archivos de 100 posiciones
                    //        CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                    //        CienPosPagos.Text = detalle[17].ToString() + "<br />";

                    //        CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                    //        CienPosCance.Text = detalle[18].ToString() + "<br />";

                    //        //Carta PDF (Ahora otrosdocumentos, todos los agregados por el back de potenciación)
                    //        CartaPDF.NavigateUrl = rutaArchivos + detalle[26].ToString();
                    //        CartaPDF.Text = detalle[26].ToString() + "<br />";
                    //        BtnContinuar.Visible = false;
                    //        break;
                    //}
                //} //Analista Básica ASAE
                //if (manejo_sesion.Credencial.IdRol == 22) //operador (Dependencia)
                //{
                //    trNoFolioExiste.Visible = true;
                //    trSubirExcel.Visible = true;
                //} //Dependencia
                //if (manejo_sesion.Credencial.IdRol == 23)//Analista back básica
                //{
                //    trArchivoExcel.Visible = true;
                //    hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                //    hlkExcel.Text = detalle[1].ToString() + "<br />";

                //    trErrores.Visible = false;

                //    trSubirExcel.Visible = false;
                //    trSubirPDFXLS.Visible = true;
                //    trsubirPDF.Visible = true;
                //    trsubirXLS.Visible = true;
                //} //Analista Back Metlife
            }
            else //Potenciada
            {
                rdbCobertura.SelectedIndex = 1;
                rdbCobertura_SelectedIndexChanged(null, null);
                ddlTrimestreQuincena.SelectedValue = detalle[15].ToString();
                ddlAnn.SelectedValue = detalle[16].ToString();
                trArchivoExcel.Visible = true;
                BtnValidacion.Visible = true;
                BtnContinuar.Visible = false;

                //comun

                switch (Request["es"])
                {
                    case "1":   //Revisión analista
                        break;
                    case "2":   //Devuelto Errores (dependencia->analista)
                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";
                        break;
                    case "3":   //Procesamiento analista back
                        BtnValidacion.Visible = false;
                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        //Archivos de 100 posiciones
                        CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                        CienPosPagos.Text = detalle[17].ToString() + "<br />";

                        CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                        CienPosCance.Text = detalle[18].ToString() + "<br />";

                        trNoFolioExiste.Visible = false;
                        trAnn2.Visible = false;

                        trDocumento1.Visible = true;

                        trSubirExcel.Visible = false;
                        BtnContinuar2.Visible = true;

                        //Otros documentos

                        string[] archivoslote = detalle[26].ToString().Split(',');

                        if (archivoslote.Length > 0)
                        {
                            Label lblOD1 = new Label();
                            lblOD1.Text = "Otros Documentos: ";
                            Panel2.Controls.Add(lblOD1);

                            for (int i = 0; i < archivoslote.Length; i++)
                            {
                                HyperLink ctrl = new HyperLink();
                                ctrl.Text = archivoslote[i];
                                ctrl.NavigateUrl = rutaArchivos + archivoslote[i].Trim() + " ";
                                ctrl.Target = "_blank";
                                Panel1.Controls.Add(ctrl);
                                Panel1.Controls.Add(new LiteralControl("<br />"));
                            }
                        }
                        break;
                    case "4":   //Reprocesamiento
                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        trSubirExcel.Visible = false;
                        trSubirExcelParaValidar.Visible = true;

                        BtnValidacion.Visible = false;

                        break;
                    case "5":   //Revisión analista front
                        BtnValidacion.Visible = false;
                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        //Archivos de 100 posiciones
                        CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                        CienPosPagos.Text = detalle[17].ToString() + "<br />";

                        CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                        CienPosCance.Text = detalle[18].ToString() + "<br />";

                        Documento.NavigateUrl = rutaArchivos + detalle[23].ToString();
                        Documento.Text = detalle[23].ToString() + "<br />";

                        trSubirExcel.Visible = false;
                        trSubirPDFXLS.Visible = false;
                        trsubirPDF.Visible = false;
                        trsubirXLS.Visible = false;
                        trDocumento1.Visible = false;

                        trErrores2.Visible = true;

                        trCartaPDF1.Visible = true;
                        trCartaPDF2.Visible = true;

                        BtnContinuar3.Visible = true;
                        BtnContinuar4.Visible = true;

                        //Cargar los archivos, si los hubiere
                        CargarArchivosTramite(detalle[3].ToString());
                        break;
                    case "6":   //Revisión analista back errores
                        trCartaPDF1.Visible = true;
                        trCartaPDF2.Visible = true;
                        //Cargar los archivos, si los hubiere
                        CargarArchivosTramite(detalle[3].ToString());
                        trErrores2.Visible = true;
                        BtnValidacion.Visible = false;
                        BtnContinuar3.Visible = true;
                        break;
                    case "7":   //Revisión analista
                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        //Archivos de 100 posiciones
                        CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                        CienPosPagos.Text = detalle[17].ToString() + "<br />";

                        CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                        CienPosCance.Text = detalle[18].ToString() + "<br />";

                        Documento.NavigateUrl = rutaArchivos + detalle[23].ToString();
                        Documento.Text = detalle[23].ToString() + "<br />";

                        //Otros documentos
                        string[] archivoslotea = detalle[26].ToString().Split(',');

                        if (archivoslotea.Length > 0)
                        {
                            Label lblOD2 = new Label();
                            lblOD2.Text = "Otros Documentos: ";
                            Panel2.Controls.Add(lblOD2);

                            for (int ii = 0; ii < archivoslotea.Length; ii++)
                            {
                                HyperLink ctrl = new HyperLink();
                                ctrl.Text = archivoslotea[ii];
                                ctrl.NavigateUrl = rutaArchivos + archivoslotea[ii].Trim() + " ";
                                ctrl.Target = "_blank";
                                Panel1.Controls.Add(ctrl);
                                Panel1.Controls.Add(new LiteralControl("<br />"));
                            }
                        }
                        BtnValidacion.Visible = false;
                        BtnContinuar5.Visible = true;
                        BtnContinuar4.Visible = false;
                        break;
                    case "8":   //Concluído
                        //Trámite terminado, sólo para revisión

                        trSubirExcel.Visible = false;

                        BtnValidacion.Visible = false;

                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        //Archivos de 100 posiciones
                        CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                        CienPosPagos.Text = detalle[17].ToString() + "<br />";

                        CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                        CienPosCance.Text = detalle[18].ToString() + "<br />";

                        Documento.NavigateUrl = rutaArchivos + detalle[23].ToString();
                        Documento.Text = detalle[23].ToString() + "<br />";

                        //Carta PDF
                        //CartaPDF.NavigateUrl = rutaArchivos + detalle[20].ToString();
                        //CartaPDF.Text = detalle[20].ToString() + "<br />";

                        //Otros documentos
                        string[] archivosloteF = detalle[26].ToString().Split(',');

                        Label lblOD3 = new Label();
                        lblOD3.Text = "Otros Documentos: ";
                        Panel2.Controls.Add(lblOD3);

                        for (int i = 0; i < archivosloteF.Length; i++)
                        {
                            HyperLink ctrl = new HyperLink();
                            ctrl.Text = archivosloteF[i];
                            ctrl.NavigateUrl = rutaArchivos + archivosloteF[i].Trim() + " ";
                            ctrl.Target = "_blank";
                            Panel1.Controls.Add(ctrl);
                            Panel1.Controls.Add(new LiteralControl("<br />"));
                        }

                        BtnContinuar5.Visible = false;

                        trSubirExcel.Visible = false;

                        BtnTerminar.Visible = false;
                        break;
                    default:    //comun/inicio
                        trNoFolioExiste.Visible = false;
                        trSubirExcel.Visible = false;
                        trSubirExcelParaValidar.Visible = true;
                        BtnValidacion.Visible = false;
                        trArchivoExcel.Visible = true;
                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";
                        break;
                }

                //Visibilidad de botones por rol
                /*
                if (manejo_sesion.Credencial.IdRol == 26) //Analista potenciación
                {
                    //trNoFolioExiste.Visible = false;
                    //trSubirExcel.Visible = false;
                    //trSubirExcelParaValidar.Visible = true;
                    //BtnValidacion.Visible = false;

                    if (Request["es"] == "7")
                    {
                        trSubirExcelParaValidar.Visible = false;
                        BtnValidacion.Visible = false;

                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        //Archivos de 100 posiciones
                        CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                        CienPosPagos.Text = detalle[17].ToString() + "<br />";

                        CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                        CienPosCance.Text = detalle[18].ToString() + "<br />";

                        Documento.NavigateUrl = rutaArchivos + detalle[23].ToString();
                        Documento.Text = detalle[23].ToString() + "<br />";

                        //Carta PDF
                        //CartaPDF.NavigateUrl = rutaArchivos + detalle[26].ToString();
                        //CartaPDF.Text = detalle[26].ToString() + "<br />";

                        //Otros documentos
                        string[] archivoslote = detalle[26].ToString().Split(',');

                        Label lblOD = new Label();
                        lblOD.Text = "Otros Documentos: ";
                        Panel2.Controls.Add(lblOD);

                        for (int i = 0; i < archivoslote.Length; i++)
                        {
                            HyperLink ctrl = new HyperLink();
                            ctrl.Text = archivoslote[i];
                            ctrl.NavigateUrl = rutaArchivos + archivoslote[i].Trim() + " ";
                            ctrl.Target = "_blank";
                            Panel1.Controls.Add(ctrl);
                            Panel1.Controls.Add(new LiteralControl("<br />"));
                        }

                        BtnContinuar5.Visible = true;
                    }
                    //trArchivoExcel.Visible = true;
                    //hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                    //hlkExcel.Text = detalle[1].ToString() + "<br />";

                    if (Request["es"] == "8")
                    {
                        //Trámite terminado, sólo para revisión

                        trSubirExcel.Visible = false;

                        BtnValidacion.Visible = false;

                        hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                        hlkExcel.Text = detalle[1].ToString() + "<br />";

                        //Archivos de 100 posiciones
                        CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                        CienPosPagos.Text = detalle[17].ToString() + "<br />";

                        CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                        CienPosCance.Text = detalle[18].ToString() + "<br />";

                        Documento.NavigateUrl = rutaArchivos + detalle[23].ToString();
                        Documento.Text = detalle[23].ToString() + "<br />";

                        //Carta PDF
                        //CartaPDF.NavigateUrl = rutaArchivos + detalle[20].ToString();
                        //CartaPDF.Text = detalle[20].ToString() + "<br />";

                        //Otros documentos
                        string[] archivoslote = detalle[26].ToString().Split(',');

                        Label lblOD = new Label();
                        lblOD.Text = "Otros Documentos: ";
                        Panel2.Controls.Add(lblOD);

                        for (int i = 0; i < archivoslote.Length; i++)
                        {
                            HyperLink ctrl = new HyperLink();
                            ctrl.Text = archivoslote[i];
                            ctrl.NavigateUrl = rutaArchivos + archivoslote[i].Trim() + " ";
                            ctrl.Target = "_blank";
                            Panel1.Controls.Add(ctrl);
                            Panel1.Controls.Add(new LiteralControl("<br />"));
                        }

                        BtnContinuar5.Visible = false;

                        trSubirExcel.Visible = false;

                        BtnTerminar.Visible = false;

                    }
                }
                if (manejo_sesion.Credencial.IdRol == 27) //Analista potenciación front
                {
                    //BtnValidacion.Visible = false;
                    //hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                    //hlkExcel.Text = detalle[1].ToString() + "<br />";

                    ////Archivos de 100 posiciones
                    //CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                    //CienPosPagos.Text = detalle[17].ToString() + "<br />";

                    //CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                    //CienPosCance.Text = detalle[18].ToString() + "<br />";

                    //trNoFolioExiste.Visible = false;
                    //trAnn2.Visible = false;

                    //trDocumento1.Visible = true;

                    //trSubirExcel.Visible = false;

                    //if (Request["es"] == "6")
                    //{
                    //    trCartaPDF1.Visible = true;
                    //    trCartaPDF2.Visible = true;
                    //    //Cargar los archivos, si los hubiere
                    //    CargarArchivosTramite(detalle[3].ToString());
                    //}

                    //BtnContinuar2.Visible = true;

                    ////Otros documentos

                    //string[] archivoslote = detalle[26].ToString().Split(',');

                    //if (archivoslote.Length > 0)
                    //{

                    //    Label lblOD = new Label();
                    //    lblOD.Text = "Otros Documentos: ";
                    //    Panel2.Controls.Add(lblOD);

                    //    for (int i = 0; i < archivoslote.Length; i++)
                    //    {
                    //        HyperLink ctrl = new HyperLink();
                    //        ctrl.Text = archivoslote[i];
                    //        ctrl.NavigateUrl = rutaArchivos + archivoslote[i].Trim() + " ";
                    //        ctrl.Target = "_blank";
                    //        Panel1.Controls.Add(ctrl);
                    //        Panel1.Controls.Add(new LiteralControl("<br />"));
                    //    }
                    //}

                    //if (Request["es"] == "6")
                    //{
                    //    trErrores2.Visible = true;
                    //}
                }
                if (manejo_sesion.Credencial.IdRol == 28) //Analista potenciación back
                {
                    //BtnValidacion.Visible = false;
                    //hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                    //hlkExcel.Text = detalle[1].ToString() + "<br />";

                    ////Archivos de 100 posiciones
                    //CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                    //CienPosPagos.Text = detalle[17].ToString() + "<br />";

                    //CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                    //CienPosCance.Text = detalle[18].ToString() + "<br />";

                    //Documento.NavigateUrl = rutaArchivos + detalle[23].ToString();
                    //Documento.Text = detalle[23].ToString() + "<br />";

                    //trSubirExcel.Visible = false;
                    //trSubirPDFXLS.Visible = false;
                    //trsubirPDF.Visible = false;
                    //trsubirXLS.Visible = false;
                    //trDocumento1.Visible = false;

                    //trErrores2.Visible = true;

                    //trCartaPDF1.Visible = true;
                    //trCartaPDF2.Visible = true;

                    //BtnContinuar3.Visible = true;
                    //BtnContinuar4.Visible = true;

                    ////Cargar los archivos, si los hubiere
                    //CargarArchivosTramite(detalle[3].ToString());
                }
                if (!string.IsNullOrEmpty(Request["es"].ToString()))
                {
                    //if (Request["es"] == "7")
                    //{
                    //    hlkExcel.NavigateUrl = rutaArchivos + detalle[1].ToString();
                    //    hlkExcel.Text = detalle[1].ToString() + "<br />";

                    //    //Archivos de 100 posiciones
                    //    CienPosPagos.NavigateUrl = rutaArchivos + detalle[17].ToString();
                    //    CienPosPagos.Text = detalle[17].ToString() + "<br />";

                    //    CienPosCance.NavigateUrl = rutaArchivos + detalle[18].ToString();
                    //    CienPosCance.Text = detalle[18].ToString() + "<br />";

                    //    Documento.NavigateUrl = rutaArchivos + detalle[23].ToString();
                    //    Documento.Text = detalle[23].ToString() + "<br />";

                    //    //Otros documentos
                    //    string[] archivoslotea = detalle[26].ToString().Split(',');

                    //    if (archivoslotea.Length > 0)
                    //    {
                    //        Label lblOD2 = new Label();
                    //        lblOD2.Text = "Otros Documentos: ";
                    //        Panel2.Controls.Add(lblOD2);

                    //        for (int ii = 0; ii < archivoslotea.Length; ii++)
                    //        {
                    //            HyperLink ctrl = new HyperLink();
                    //            ctrl.Text = archivoslotea[ii];
                    //            ctrl.NavigateUrl = rutaArchivos + archivoslotea[ii].Trim() + " ";
                    //            ctrl.Target = "_blank";
                    //            Panel1.Controls.Add(ctrl);
                    //            Panel1.Controls.Add(new LiteralControl("<br />"));
                    //        }
                    //    }
                    //}
                }

                */
            }

        }

        protected void AgregarGridABDBasica(GridView gridview)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["RegistrosTemporales"];
            foreach (DataRow row in dt.Rows)
            {
                pd.PolizasDetalleAgregar(txtNoPoliza.Text, row["Dependencia"].ToString(), row["APaterno"].ToString(), row["AMaterno"].ToString(),
                row["Nombres"].ToString(), row["FNacimiento"].ToString(), row["RFC"].ToString(), row["CURP"].ToString(), row["Sexo"].ToString(), row["CEntidadFederativa"].ToString(),
                row["CMunicipio"].ToString(), row["NivelTabular"].ToString(), row["MPercepcionOBM"].ToString(), row["Eventual"].ToString(), row["APAsegurado"].ToString(),
                row["AMAsegurado"].ToString(), row["NAsegurado"].ToString(), row["FNAsegurado"].ToString(), row["CURPAsegurado"].ToString(), row["SAsegurado"].ToString(),
                row["FAAsegurado"].ToString(), row["TAsegurado"].ToString(), row["FIColectividad"].ToString(), row["SABasica"].ToString(), row["PBTReportar"].ToString().Replace(',', ' ').Replace('$', ' ').Replace('-', ' '),
                row["MAPBasica"].ToString().Replace(',', ' ').Replace('$', ' ').Replace('-', ' '), row["ITPDependencia"].ToString().Replace(',', ' ').Replace('$', ' ').Replace('-', ' '), "S", Request["folio"], ddlTrimestreQuincena.SelectedValue, ddlAnn.SelectedValue);
            }
        }

        protected void AgregarGridABDPotenciacion(GridView gridview)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["RegistrosTemporales"];
            foreach (DataRow row in dt.Rows)
            {
                pd.PolizaDetallePotenciacion(txtNoPoliza.Text, row["Dependencia"].ToString(), row["APaterno"].ToString(), row["AMaterno"].ToString(),
                row["Nombres"].ToString(), row["FNacimiento"].ToString(), row["RFC"].ToString(), row["CURP"].ToString(), row["Sexo"].ToString(), row["CEntidadFederativa"].ToString(),
                row["CMunicipio"].ToString(), row["NivelTabular"].ToString(), row["MPercepcionOBM"].ToString(), row["Eventual"].ToString(), row["APAsegurado"].ToString(),
                row["AMAsegurado"].ToString(), row["NAsegurado"].ToString(), row["FNAsegurado"].ToString(), row["CURPAsegurado"].ToString(), row["SAsegurado"].ToString(),
                row["FAAsegurado"].ToString(), row["TAsegurado"].ToString(), row["FIColectividad"].ToString(), row["SABasica"].ToString(), row["SAPotenciada"].ToString(),
                row["SAT"].ToString(), row["PrimaPotenciadaQR"].ToString().Replace(',', ' ').Replace('$', ' ').Replace('-', ' '), row["FormaPago"].ToString(), row["MontoAjustePrimaP"].ToString().Replace(',', ' ').Replace('$', ' ').Replace('-', ' '),
                row["ImporteTotalPagar"].ToString().Replace(',', ' ').Replace('$', ' ').Replace('-', ' '), row["FAAseguradoSAP"].ToString(), "e", Request["folio"], ddlTrimestreQuincena.SelectedValue, ddlAnn.SelectedValue);
            }
        }

        protected void GuardarArchivosTramite()
        {
            string carpetaOrigen = "../DocsUpPotenciacion/";
            carpetaOrigen = Server.MapPath(carpetaOrigen);
            string carpetaDestino = "../ArchivosExcel/";
            carpetaDestino = Server.MapPath(carpetaDestino);

            string[] files = Directory.GetFiles(carpetaOrigen);

            //Copiar los archivos a la carpeta definitiva
            foreach (string s in files)
            {
                string fileName = Path.GetFileName(s);
                string destFile = Path.Combine(carpetaDestino, fileName);
                File.Copy(s, destFile, true);

                at.Agregar(Request["folio"], fileName);
            }

            //eliminar los archivos que ya no se usarán
            foreach (string s in files)
            {
                File.Delete(s);
            }

        }

        protected void CargarArchivosTramite(string poliza)
        {
            at.SeleccionarPorPoliza_GridView(poliza, ref gvArchivos);
        }



#endregion






    }
}
