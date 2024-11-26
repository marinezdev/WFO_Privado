using System;
using System.Data;
using System.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using w = wfiplib.Tablas.Institucional.Privado;
using OfficeOpenXml;
using System.Globalization;

namespace wfip.promotoria
{
    public partial class CorreoTramites : System.Web.UI.Page
    {
        public DataTable dt;

        w.InsServicios insservicios;
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        Mensajes mensajes = new Mensajes();

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
                //mensajes.MostrarMensaje(this, "Iniciando proceso (prueba, mensaje simple)");
            }
            else
            {
                if (Session["RegistrosTemporales"] == null)
                {
                    //Nuevo
                    dt = new DataTable();
                    dt.Columns.Add("Dependencia", typeof(string)).MaxLength = 150;    //Dependencia
                    dt.Columns.Add("APaterno", typeof(string)).MaxLength = 20;        //Apellido Paterno del titular
                    dt.Columns.Add("AMaterno", typeof(string)).MaxLength = 20;        //Apellido Materno del titular
                    dt.Columns.Add("Nombres", typeof(string)).MaxLength = 20;         //Nombre(s) del titular
                    dt.Columns.Add("FNacimiento", typeof(int));                       //Fecha Nacimiento del titular
                    dt.Columns.Add("RFC", typeof(string)).MaxLength = 13;             //RFC del titular
                    dt.Columns.Add("CURP", typeof(string)).MaxLength = 18;            //CURP del titular
                    dt.Columns.Add("Sexo", typeof(string)).MaxLength = 1;             //Sexo del titular
                    dt.Columns.Add("CEntidadFederativa", typeof(int));                //Codigo de la entidad federattiva donde esta adscrito el titular
                    dt.Columns.Add("CMunicipio", typeof(int));                        //Codigo de la ciudad o municipio donde esta adscrito el titular
                    dt.Columns.Add("NivelTabular", typeof(string)).MaxLength = 1;     //Nivel tabular del titular
                    dt.Columns.Add("NTabularAnterior", typeof(string)).MaxLength = 1; //Nivel tabular del titular anterior
                    dt.Columns.Add("NTabularNuevo", typeof(string)).MaxLength = 1;    //Nivel tabular del titular nuevo
                    dt.Columns.Add("MPercepcionOrdinaria", typeof(decimal));          //Monto de la percepcion ordinaria bruta mensual del titular concepto 06 y 07
                    dt.Columns.Add("MpercepcionOrdinariaBrutaAnterior", typeof(decimal)); //Monto de la percepcion ordinaria bruta mensual del titular anterior concepto 06 y 07
                    dt.Columns.Add("MPercepcionOrdinariaBrutaNuevo", typeof(decimal));    //Monto de la percepcion ordinaria bruta mensual del titular nuevo concepto 06 y 07
                    dt.Columns.Add("Eventual", typeof(string)).MaxLength = 2;         //Eventual
                    dt.Columns.Add("FMovimiento", typeof(int));                       //Fecha en que causa efecto el movimiento
                    dt.Columns.Add("APAsegurado", typeof(string)).MaxLength = 20;     //Apellido paterno del asegurado
                    dt.Columns.Add("AMAsegurado", typeof(string)).MaxLength = 20;     //Apellido materno del asegurado
                    dt.Columns.Add("NAsegurado", typeof(string)).MaxLength = 20;      //Nombres del asegurado
                    dt.Columns.Add("FNAsegurado", typeof(int));                       //Fecha de nacimiento del asegurado
                    dt.Columns.Add("CURPAsegurado", typeof(string)).MaxLength = 18;   //CURP del asegurado
                    dt.Columns.Add("SAsegurado", typeof(string)).MaxLength = 1;       //Sexo del asegurado
                    dt.Columns.Add("FAAsegurado", typeof(int));                       //Fecha de antigüedad del asegurado en el SGMM
                    dt.Columns.Add("TAsegurado", typeof(string)).MaxLength = 1;       //Tipo de asegurado
                    dt.Columns.Add("FIColectividad", typeof(int));                    //Fecha de ingreso a la colectividad
                    dt.Columns.Add("CampoCorregido", typeof(string)).MaxLength = 77;  //Campo Corregido
                    dt.Columns.Add("AltaBaja");                                       //Alta o baja
                    dt.Columns.Add("FEMovimiento", typeof(int));                      //Fecha en que causa efecto el movimiento
                    dt.Columns.Add("SABasica", typeof(decimal));                      //Suma Asegurada Basica
                    dt.Columns.Add("SABAnterior", typeof(decimal));                   //Suma asegurada basica anterior
                    dt.Columns.Add("SABNueva", typeof(decimal));                      //Suma asegurada basica nueva
                    dt.Columns.Add("SABIncorrecta", typeof(decimal));                 //suma asegurada basica incorrecta
                    dt.Columns.Add("SABCorrecta", typeof(decimal));                   //Suma asegurada basica correcta
                    dt.Columns.Add("SABTrimestreReportar", typeof(decimal));          //Suma asegurada basica del trimestre a reportar
                    dt.Columns.Add("PBTAnterior", typeof(decimal));                   //Prima basica total anterior
                    dt.Columns.Add("PBTNueva", typeof(decimal));                      //Prima basica total nueva por las quincenas cubiertas
                    dt.Columns.Add("PBTQCubiertas", typeof(decimal));                 //Prima basica total por las quincenas cubiertas
                    dt.Columns.Add("PBTIncorrecta", typeof(decimal));                 //Prima basica total incorrecta
                    dt.Columns.Add("PBTReportar", typeof(decimal));                   //Prima basica del trimestre a reportar
                    dt.Columns.Add("IPBasica", typeof(decimal));                      //Importe en la prima basica faltante orestante de la dependencia
                    dt.Columns.Add("IPDependencia", typeof(decimal));                 //Importe a pagar por la dependencia
                    dt.Columns.Add("MAPBasica", typeof(decimal));                     //Monto de ajuste en prima basica por altas, bajas, promociones o correcciones
                    dt.Columns.Add("SAPotenciada", typeof(decimal));                  //Suma Asegurada Potenciada
                    dt.Columns.Add("SAPAnterior", typeof(decimal));                   //Suma Asegurada Potenciada anterior
                    dt.Columns.Add("SAPNueva", typeof(decimal));                      //suma asegurada potenciada nueva
                    dt.Columns.Add("SAPIncorrecta", typeof(decimal));                 //Suma asegurada potenciada incorrecta
                    dt.Columns.Add("SAPCorrecta", typeof(decimal));                   //Suma asegurada potenciada correcta
                    dt.Columns.Add("PPTIncorrecta", typeof(decimal));                 //Prima potencial total incorrecta
                    dt.Columns.Add("PPTAnterior", typeof(decimal));                   //Prima potenciada total anterior
                    dt.Columns.Add("PPTNueva", typeof(decimal));                      //Prima potenciada total nueva por las quincenas cubiertas
                    dt.Columns.Add("PPTotal", typeof(decimal));                       //Prima potenciada total por las quincenas cubiertas
                    dt.Columns.Add("IPPFaltante", typeof(decimal));                   //Importe en la prima potenciada faltante o restante del servidor publico
                    dt.Columns.Add("FAASAsegurada", typeof(decimal));                 //Fecha de antigüedad del asegurado en la suma asegurada potenciada
                    dt.Columns.Add("SumaAseguradaTotal", typeof(decimal));            //Suma asegurada total
                    dt.Columns.Add("PPQReportar", typeof(decimal));                   //Prima potenciada quincenal a reportar
                    dt.Columns.Add("FormaPago", typeof(string)).MaxLength = 3;        //Forma de pago
                    dt.Columns.Add("MAPPotenciada", typeof(decimal));                 //Monto de ajuste en prima potenciada por altas, bajas, promociones o correcciones
                    dt.Columns.Add("ITPPPotenciada", typeof(decimal));                //Importe total a pagar de la prima potenciada faltante o restante del servidor publico

                    CargarTabla(gvAgregado, dt);

                    Session["RegistrosTemporales"] = dt;
                }
            }
        }

        protected void rblCaptura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblCaptura.SelectedValue == "1")
            {
                trCargaArchivo.Visible = true;
                tbTipoServicio.Visible = false;
                MovsAsegurados.Visible = false;
                tblCartas.Visible = false;
                tblListado.Visible = false;
                tbBtnAgregar.Visible = false;
            }
            else if (rblCaptura.SelectedValue == "2")
            {
                trCargaArchivo.Visible = false;
                tbTipoServicio.Visible = true;
                CargaInicialDDLs();
            }
        }

        protected void ddlTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoServicio.SelectedValue.Equals("1"))
            {
                MovsAsegurados.Visible = true;
                tblCartas.Visible = false;
                tblListado.Visible = false;
            }
            else if (ddlTipoServicio.SelectedValue.Equals("2"))
            {
                MovsAsegurados.Visible = true;
                tblCartas.Visible = false;
                tblListado.Visible = false;

                txtSubGrupo.Visible = false;
                txtSubGrupo.Visible = false;
                chkFechaAntiguedad.Visible = false;
                rbCertImpSi.Visible = false;
                rbCertImpNo.Visible = false;
                Label15.Visible = false;
                Label16.Visible = false;
                Label17.Visible = false;
                txtSubGrupo.Visible = false;
                ddlInsAccion.Visible = false;
                Label18.Visible = false;
            }
            else if (ddlTipoServicio.SelectedValue.Equals("3"))
            {
                MovsAsegurados.Visible = false;
                tblCartas.Visible = true;
                tblListado.Visible = false;

                Label9.Text = "Tipo de Cartas";
                Label10.Visible = true;
                txtNoCertificado.Visible = true;
                txtCNombres.Visible = true;
                txtCAPaterno.Visible = true;
                txtCAMaterno.Visible = true;
                Label11.Visible = true;
                Label12.Visible = true;
                Label13.Visible = true;
            }
            tbBtnAgregar.Visible = true;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("esperaPromotoria.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //Agregado de los datos a la tabla temporal
            dt = (DataTable)Session["RegistrosTemporales"];

            dt.Rows.Add(
            ddlTipoServicio.SelectedItem.Text,
            ddlTipoServicio.SelectedValue,
            ddlInsAccion.SelectedItem.Text == "Seleccionar" ? "" : ddlInsAccion.SelectedItem.Text,
            ddlInsAccion.SelectedValue,
            txtCertificado.Text,
            ddlTipoServicio.SelectedValue == "1" ? txtSubGrupo.Text : ddlTipoServicio.SelectedValue == "2" ? txtLSubGrupo.Text : "",
            ddlTipoServicio.SelectedValue == "1" ? txtCategoria.Text : ddlTipoServicio.SelectedValue == "2" ? txtLCategoria.Text : "",
            ddlTipoServicio.SelectedValue == "1" ? txtNombresAsegurados.Text : ddlTipoServicio.SelectedValue == "2" ? txtCNombres.Text : "",
            ddlTipoServicio.SelectedValue == "1" ? txtAPaternoAsegurados.Text : ddlTipoServicio.SelectedValue == "2" ? txtCAPaterno.Text : "",
            ddlTipoServicio.SelectedValue == "1" ? txtAMaternoAsegurados.Text : ddlTipoServicio.SelectedValue == "2" ? txtCAMaterno.Text : "",
            ddlParentesco.SelectedItem.Text,
            ddlParentesco.SelectedValue,
            txtFechaNacimiento.Text,
            ddlGenero.SelectedItem.Text,
            ddlGenero.SelectedValue,
            txtSueldo.Text,
            txtFechaMovimiento.Text,
            txtFechaAntiguedad.Text,
            txtPolizaRecAnt.Text,
            txtCiaAntRec.Text,
            txtCertImp.Text,
            ddlTipoCarta.SelectedItem.Text,
            ddlTipoCarta.SelectedValue,
            txtNoCertificado.Text,
            txtContratante.Text,
            txtRFC.Text,
            txtDomFiscal.Text,
            txtFormaPago.Text,
            ddlInsServicioListados.SelectedItem.Text,
            ddlInsServicioListados.SelectedValue,
            txtLComentarios.Text,
            txtFLMovimiento.Text
            );

            CargarTabla(gvAgregado, dt);

            //Agregar los datos obtenidos a la sesión 
            Session["RegistrosTemporales"] = dt;
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "MensajeGuardado", "bootbox.alert({ message: 'Los datos se guardaron exitosamente.', size: 'small'} );", true);
            //Limpiar campos
            rblVIP.ClearSelection();
            txtNoPoliza.Text = "";
            txtNoOficio.Text = "";
            txtCliente.Text = "";
            txtNombre.Text = "";
            txtAPaterno.Text = "";
            txtAMaterno.Text = "";
            txtClaveCorreo.Text = "";
            txtFechaSolicitud.Text = "";
            txtAsunto.Text = "";
            rblCaptura.ClearSelection();
            mensajes.MostrarMensaje(this, "Se guardaron los datos exitosamente. Número de Folio Asignado: 948847E345", "esperaPromotoria.aspx");
        }

        protected void btnSubirExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //if (fileUpDocumento.PostedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                if (AsyncFileUpload1.PostedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    //mensajes.MostrarMensaje(this, "El archivo que intenta usar no es de excel, revíse.");
                    //ClientScript.RegisterStartupScript(this.GetType(), "Mensajes", "$.notify('El archivo que intenta usar no es de excel, revíse.', { type: 'info', color: '#fff', background: '#D44950' });", true);
                    return;
                }

                //if (fileUpDocumento.HasFile)
                if (AsyncFileUpload1.HasFile)
                {
                    DataTable dt = new DataTable();
                    //ExcelPackage pagina = new ExcelPackage(fileUpDocumento.FileContent);
                    ExcelPackage pagina = new ExcelPackage(AsyncFileUpload1.FileContent);
                    dt = wfiplib.ExtensionPaqueteriaExcel.Excel_A_DataTable(pagina);

                    CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);

                    //System.Threading.Thread.Sleep(5000);

                    //Verificar si el grid tiene registros
                    if (gvAgregado.Rows.Count > 0)
                    {
                        DataTable dtt = new DataTable();
                        dtt = (DataTable)gvAgregado.DataSource;
                        dt.Merge(dtt);
                    }

                    Session["RegistrosTemporales"] = dt;

                    CargarTabla(gvAgregado, dt);

                    trCargaArchivo.Visible = false;

                    ProcesarEncabezadoFijo();

                }
                else
                {
                    //mensajes.MostrarMensaje(this, "no seleccionó un archivo para subir, intente de nuevo.");
                    //ClientScript.RegisterStartupScript(this.GetType(), "Mensajes", "$.notify('no seleccionó un archivo para subir.', { type: 'info', color: '#fff', background: '#D44950' });", true);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Referencia a objeto no establecida como instancia de un objeto."))
                    mensajes.MostrarMensaje(this, "No subió el archivo, intente de nuevo.");
                //ClientScript.RegisterStartupScript(this.GetType(), "Mensajes", "$.notify('Error al subir el archivo: " + ex.Message + "', { type: 'info', color: '#fff', background: '#D44950' });", true);
            }
        }

        protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            
        }

        protected void gvAgregado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Dependencia").ToString().Length > 150)
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "APaterno").ToString().Length > 150)
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "APaterno").ToString().Length == 0)
                {
                    e.Row.Cells[2].Text = ".";
                }
                if (DataBinder.Eval(e.Row.DataItem, "AMaterno").ToString().Length > 150)
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "AMaterno").ToString().Length == 0)
                {
                    e.Row.Cells[3].Text = ".";
                }
                if (DataBinder.Eval(e.Row.DataItem, "Nombres").ToString().Length > 20)
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "Nombres").ToString().Length == 0)
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                }
                if (DataBinder.Eval(e.Row.DataItem, "FNacimiento").ToString().Length > 8)
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                }
                DateTime fech1;
                if (!DateTime.TryParseExact(DataBinder.Eval(e.Row.DataItem, "FNacimiento").ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fech1))
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "RFC").ToString().Length > 13)
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "CURP").ToString().Length > 18)
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "Sexo").ToString().Length > 1 || (DataBinder.Eval(e.Row.DataItem, "Sexo").ToString() != "H" && DataBinder.Eval(e.Row.DataItem, "Sexo").ToString() != "M"))
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.White;
                }
                if (!Entidades().Contains(DataBinder.Eval(e.Row.DataItem, "CEntidadFederativa").ToString()))
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
                }
                if (!CiudadMpio().Contains(DataBinder.Eval(e.Row.DataItem, "CMunicipio").ToString()))
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
                }
                if (!NivelTabular().Contains(DataBinder.Eval(e.Row.DataItem, "NivelTabular").ToString()))
                {
                    e.Row.Cells[11].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
                }
                if (!NivelTabular().Contains(DataBinder.Eval(e.Row.DataItem, "NTabularAnterior").ToString()))
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                }
                if (!NivelTabular().Contains(DataBinder.Eval(e.Row.DataItem, "NTabularnuevo").ToString()))
                {
                    e.Row.Cells[13].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "MPercepcionOrdinaria").ToString().Contains(",") || DataBinder.Eval(e.Row.DataItem, "MPercepcionOrdinaria").ToString().Contains("$"))
                {
                    e.Row.Cells[14].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[14].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "MpercepcionOrdinariaBrutaAnterior").ToString().Contains(",") || DataBinder.Eval(e.Row.DataItem, "MpercepcionOrdinariaBrutaAnterior").ToString().Contains("$"))
                {
                    e.Row.Cells[15].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[15].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "MPercepcionOrdinariaBrutaNuevo").ToString().Contains(",") || DataBinder.Eval(e.Row.DataItem, "MPercepcionOrdinariaBrutaNuevo").ToString().Contains("$"))
                {
                    e.Row.Cells[16].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[16].ForeColor = System.Drawing.Color.White;
                }
                if (DataBinder.Eval(e.Row.DataItem, "Eventual").ToString().Length > 1)
                {
                    e.Row.Cells[17].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[17].ForeColor = System.Drawing.Color.White;
                }
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvAgregado, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";


                //Validaciones de reglas de negocio
                if (!ClavesCostos().Contains(DataBinder.Eval(e.Row.DataItem, "SABasica").ToString()))
                {
                    e.Row.Cells[31].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[31].ForeColor = System.Drawing.Color.White;
                }
                if (!ClavesCostos().Contains(DataBinder.Eval(e.Row.DataItem, "SABAnterior").ToString()))
                {
                    e.Row.Cells[32].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[32].ForeColor = System.Drawing.Color.White;
                }
                if (!ClavesCostos().Contains(DataBinder.Eval(e.Row.DataItem, "SABNueva").ToString()))
                {
                    e.Row.Cells[33].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[33].ForeColor = System.Drawing.Color.White;
                }
                if (!ClavesCostos().Contains(DataBinder.Eval(e.Row.DataItem, "SABIncorrecta").ToString()))
                {
                    e.Row.Cells[34].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[34].ForeColor = System.Drawing.Color.White;
                }
                if (!ClavesCostos().Contains(DataBinder.Eval(e.Row.DataItem, "SABCorrecta").ToString()))
                {
                    e.Row.Cells[35].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[35].ForeColor = System.Drawing.Color.White;
                }
                if (!ClavesCostos().Contains(DataBinder.Eval(e.Row.DataItem, "SABTrimestreReportar").ToString()))
                {
                    e.Row.Cells[36].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[36].ForeColor = System.Drawing.Color.White;
                }

            }
        }

        protected void gvAgregado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAgregado.EditIndex = e.NewEditIndex;
            gvAgregado.Rows[gvAgregado.EditIndex].BackColor = System.Drawing.Color.Gray;
            CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);
            DropDownList ddl = gvAgregado.Rows[e.NewEditIndex].FindControl("ddlDependencia") as DropDownList;
            if (ddl != null)
            {
                ddl.DataSource = Dependencias();
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("SELECCIONAR", "SELECCIONAR"));
            }
            
            gvAgregado.Rows[gvAgregado.EditIndex].BackColor = System.Drawing.Color.Gray;
        }

        protected void gvAgregado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAgregado.EditIndex = -1;
            CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);
            ProcesarEncabezadoFijo();
        }

        protected void gvAgregado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = (DataTable)Session["RegistrosTemporales"];

            gvAgregado.EditIndex = e.RowIndex;
            gvAgregado.Rows[gvAgregado.EditIndex].BackColor = System.Drawing.Color.Green;

            GridViewRow row = gvAgregado.Rows[e.RowIndex];
            dt.Rows[row.DataItemIndex]["Dependencia"] =                         ((DropDownList)(row.Cells[1].Controls[1])).SelectedValue;
            dt.Rows[row.DataItemIndex]["APaterno"] =                            ((TextBox)(row.Cells[2].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["AMaterno"] =                            ((TextBox)(row.Cells[3].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Nombres"] =                             ((TextBox)(row.Cells[4].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FNacimiento"] =                         ((TextBox)(row.Cells[5].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["RFC"] =                                 ((TextBox)(row.Cells[6].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CURP"] =                                ((TextBox)(row.Cells[7].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Sexo"] =                                ((TextBox)(row.Cells[8].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CEntidadFederativa"] =                  ((TextBox)(row.Cells[9].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CMunicipio"] =                          ((TextBox)(row.Cells[10].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["NivelTabular"] =                        ((TextBox)(row.Cells[11].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["NTabularAnterior"] =                    ((TextBox)(row.Cells[12].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["NTabularNuevo"] =                       ((TextBox)(row.Cells[13].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MPercepcionOrdinaria"] =                ((TextBox)(row.Cells[14].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MpercepcionOrdinariaBrutaAnterior"] =   ((TextBox)(row.Cells[15].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MPercepcionOrdinariaBrutaNuevo"] =      ((TextBox)(row.Cells[16].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["Eventual"] =                            ((TextBox)(row.Cells[17].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FMovimiento"] =                         ((TextBox)(row.Cells[18].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["APAsegurado"] =                         ((TextBox)(row.Cells[19].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["AMAsegurado"] =                         ((TextBox)(row.Cells[20].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["NAsegurado"] =                          ((TextBox)(row.Cells[21].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FNAsegurado"] =                         ((TextBox)(row.Cells[22].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CURPAsegurado"] =                       ((TextBox)(row.Cells[23].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAsegurado"] =                          ((TextBox)(row.Cells[24].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FAAsegurado"] =                         ((TextBox)(row.Cells[25].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["TAsegurado"] =                          ((TextBox)(row.Cells[26].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FIColectividad"] =                      ((TextBox)(row.Cells[27].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["CampoCorregido"] =                      ((TextBox)(row.Cells[28].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["AltaBaja"] =                            ((TextBox)(row.Cells[29].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FEMovimiento"] =                        ((TextBox)(row.Cells[30].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SABasica"] =                            ((TextBox)(row.Cells[31].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SABAnterior"] =                         ((TextBox)(row.Cells[32].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SABNueva"] =                            ((TextBox)(row.Cells[33].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SABIncorrecta"] =                       ((TextBox)(row.Cells[34].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SABCorrecta"] =                         ((TextBox)(row.Cells[35].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SABTrimestreReportar"] =                ((TextBox)(row.Cells[36].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PBTAnterior"] =                         ((TextBox)(row.Cells[37].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PBTNueva"] =                            ((TextBox)(row.Cells[38].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PBTQCubiertas"] =                       ((TextBox)(row.Cells[39].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PBTIncorrecta"] =                       ((TextBox)(row.Cells[40].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PBTReportar"] =                         ((TextBox)(row.Cells[41].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["IPBasica"] =                            ((TextBox)(row.Cells[42].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["IPDependencia"] =                       ((TextBox)(row.Cells[43].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MAPBasica"] =                           ((TextBox)(row.Cells[44].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAPotenciada"] =                        ((TextBox)(row.Cells[45].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAPAnterior"] =                         ((TextBox)(row.Cells[46].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAPNueva"] =                            ((TextBox)(row.Cells[47].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAPIncorrecta"] =                       ((TextBox)(row.Cells[48].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SAPCorrecta"] =                         ((TextBox)(row.Cells[48].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PPTIncorrecta"] =                       ((TextBox)(row.Cells[50].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PPTAnterior"] =                         ((TextBox)(row.Cells[51].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PPTNueva"] =                            ((TextBox)(row.Cells[52].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PPTotal"] =                             ((TextBox)(row.Cells[53].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["IPPFaltante"] =                         ((TextBox)(row.Cells[54].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FAASAsegurada"] =                       ((TextBox)(row.Cells[55].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["SumaAseguradaTotal"] =                  ((TextBox)(row.Cells[56].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["PPQReportar"] =                         ((TextBox)(row.Cells[57].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["FormaPago"] =                           ((TextBox)(row.Cells[58].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["MAPPotenciada"] =                       ((TextBox)(row.Cells[59].Controls[1])).Text.ToUpper();
            dt.Rows[row.DataItemIndex]["ITPPPotenciada"] =                      ((TextBox)(row.Cells[60].Controls[1])).Text.ToUpper();

            gvAgregado.EditIndex = -1;

            CargarTabla(gvAgregado, dt);
        }

        protected void gvAgregado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string currentCommand = e.CommandName;
            if (currentCommand == "Update")
            {
                ProcesarEncabezadoFijo();
            }
        }

        protected void txtNoPoliza_TextChanged(object sender, EventArgs e)
        {
            txtCliente.Text = "Nombre del Cliente";
        }







        private void CargarTabla(GridView gridview, DataTable dt)
        {
            gridview.DataSource = dt;
            gridview.DataBind();
        }

        private void CargaInicialDDLs()
        {
            insservicios = new w.InsServicios();

            //tipo de Servicio
            insservicios.LlenarInsTipoServicio_DropdownList(ref ddlTipoServicio);
            //Acción
            insservicios.LlenarInsAccion_DropdownList(ref ddlInsAccion);
            //Parentesco
            insservicios.LlenarInsParentesco_DropdownList(ref ddlParentesco);
            //Genero
            insservicios.LlenarInsGenero_DropdownList(ref ddlGenero);
            //Tipo Carta
            insservicios.LlenarInsTipoCarta_DropdownList(ref ddlTipoCarta);
            //Servicio Listados
            insservicios.LlenarInsServicioListados_DropDownList(ref ddlInsServicioListados);
        }

        protected string contarCaracteres(string cadena)
        {
            string caracs = cadena.Length.ToString();
            return cadena + "-" + caracs;
        }

        protected List<string> Dependencias()
        {
            List<string> Listado = new List<string>() {
            "OFICINA DE LA PRESIDENCIA DE LA REPÚBLICA" ,
            "SECRETARÍA DE GOBERNACIÓN" ,
            "SECRETARÍA DE RELACIONES EXTERIORES" ,
            "SECRETARÍA DE HACIENDA Y CRÉDITO PÚBLICO" ,
            "SECRETARÍA DE AGRICULTURA, GANADERÍA, DESARROLLO RURAL, PESCA Y ALIMENTACIÓN" ,
            "SECRETARÍA DE COMUNICACIONES Y TRANSPORTES" ,
            "SECRETARÍA DE ECONOMÍA" ,
            "SECRETARÍA DE EDUCACIÓN PÚBLICA" ,
            "SECRETARÍA DE SALUD" ,
            "SECRETARÍA DE MARINA" ,
            "SECRETARÍA DEL TRABAJO Y PREVISIÓN SOCIAL" ,
            "SECRETARÍA DE DESARROLLO AGRARIO, TERRITORIAL Y URBANO" ,
            "SECRETARÍA DE MEDIO AMBIENTE Y RECURSOS NATURALES" ,
            "PROCURADURÍA GENERAL DE LA REPÚBLICA" ,
            "SECRETARÍA DE ENERGÍA" ,
            "SECRETARÍA DE DESARROLLO SOCIAL" ,
            "SECRETARÍA DE TURISMO" ,
            "SECRETARÍA DE LA FUNCIÓN PÚBLICA" ,
            "TRIBUNALES AGRARIOS" ,
            "CONSEJERÍA JURÍDICA DEL EJECUTIVO FEDERAL" ,
            "INSTITUTO NACIONAL PARA EL FEDERALISMO Y EL DESARROLLO MUNICIPAL" ,
            "PREVENCIÓN Y READAPTACIÓN SOCIAL" ,
            "TRIBUNAL FEDERAL DE CONCILIACIÓN Y ARBITRAJE" ,
            "SECRETARÍA GENERAL DEL CONSEJO NACIONAL DE POBLACIÓN" ,
            "CENTRO NACIONAL DE PREVENCIÓN DE DESASTRES" ,
            "CENTRO DE INVESTIGACIÓN Y SEGURIDAD NACIONAL" ,
            "COMISIÓN PARA LA SEGURIDAD Y EL DESARROLLO INTEGRAL EN EL ESTADO DE MICHOACÁN" ,
            "INSTITUTO NACIONAL DE MIGRACIÓN" ,
            "POLICÍA FEDERAL" ,
            "SECRETARÍA TÉCNICA DE LA COMISIÓN CALIFICADORA DE PUBLICACIONES Y REVISTAS ILUSTRADAS" ,
            "COORDINACIÓN GENERAL DE LA COMISIÓN MEXICANA DE AYUDA A REFUGIADOS" ,
            "SERVICIO DE PROTECCIÓN FEDERAL" ,
            "COORDINACIÓN NACIONAL ANTISECUESTRO" ,
            "CENTRO DE PRODUCCIÓN DE PROGRAMAS INFORMATIVOS Y ESPECIALES" ,
            "SECRETARÍA TÉCNICA DEL CONSEJO DE COORDINACIÓN PARA LA IMPLEMENTACIÓN DEL SISTEMA DE JUSTICIA PENAL" ,
            "COMISIÓN NACIONAL PARA PREVENIR Y ERRADICAR LA VIOLENCIA CONTRA LAS MUJERES" ,
            "SECRETARIADO EJECUTIVO DEL SISTEMA NACIONAL DE SEGURIDAD PÚBLICA" ,
            "SECCIÓN MEXICANA DE LA COMISIÓN INTERNACIONAL DE LÍMITES Y AGUAS ENTRE MÉXICO Y ESTADOS UNIDOS" ,
            "SECCIONES MEXICANAS DE LAS COMISIONES INTERNACIONALES DE LÍMITES Y AGUAS ENTRE MÉXICO Y GUATEMALA, Y ENTRE MÉXICO Y BELIZE" ,
            "INSTITUTO MATÍAS ROMERO" ,
            "INSTITUTO DE LOS MEXICANOS EN EL EXTERIOR" ,
            "AGENCIA MEXICANA DE COOPERACIÓN INTERNACIONAL PARA EL DESARROLLO" ,
            "COMISIÓN NACIONAL BANCARIA Y DE VALORES" ,
            "COMISIÓN NACIONAL DE SEGUROS Y FIANZAS" ,
            "COMISIÓN NACIONAL DEL SISTEMA DE AHORRO PARA EL RETIRO" ,
            "SERVICIO DE ADMINISTRACIÓN TRIBUTARIA" ,
            "SERVICIO NACIONAL DE SANIDAD, INOCUIDAD Y CALIDAD AGROALIMENTARIA" ,
            "SERVICIO NACIONAL DE INSPECCIÓN Y CERTIFICACIÓN DE SEMILLAS" ,
            "COLEGIO SUPERIOR AGROPECUARIO DEL ESTADO DE GUERRERO" ,
            "AGENCIA DE SERVICIOS A LA COMERCIALIZACIÓN Y DESARROLLO DE MERCADOS AGROPECUARIOS" ,
            "SERVICIO DE INFORMACIÓN AGROALIMENTARIA Y PESQUERA" ,
            "COMISIÓN NACIONAL DE ACUACULTURA Y PESCA" ,
            "INSTITUTO MEXICANO DEL TRANSPORTE" ,
            "SERVICIOS A LA NAVEGACIÓN EN EL ESPACIO AÉREO MEXICANO" ,
            "COMISIÓN FEDERAL DE MEJORA REGULATORIA" ,
            "INSTITUTO NACIONAL DE LA ECONOMÍA SOCIAL (ANTES COORDINACIÓN NACIONAL DEL PROGRAMA NACIONAL DE APOYO PARA LAS EMPRESAS DE SOLIDARIDAD)" ,
            "UNIVERSIDAD PEDAGÓGICA NACIONAL" ,
            "INSTITUTO POLITÉCNICO NACIONAL" ,
            "ADMINISTRACIÓN FEDERAL DE SERVICIOS EDUCATIVOS EN EL DISTRITO FEDERAL" ,
            "RADIO EDUCACIÓN" ,
            "CONSEJO NACIONAL PARA LA CULTURA Y LAS ARTES" ,
            "INSTITUTO NACIONAL DEL DERECHO DE AUTOR" ,
            "INSTITUTO NACIONAL DE ESTUDIOS HISTÓRICOS DE LAS REVOLUCIONES DE MÉXICO" ,
            "ADMINISTRACIÓN DEL PATRIMONIO DE LA BENEFICENCIA PÚBLICA" ,
            "CENTRO NACIONAL DE LA TRANSFUSIÓN SANGUÍNEA" ,
            "CENTRO NACIONAL PARA LA PREVENCIÓN Y EL CONTROL DEL VIH/SIDA" ,
            "CENTRO NACIONAL DE EQUIDAD DE GÉNERO Y SALUD REPRODUCTIVA" ,
            "COMISIÓN NACIONAL DE ARBITRAJE MÉDICO" ,
            "SERVICIOS DE ATENCIÓN PSIQUIÁTRICA" ,
            "CENTRO NACIONAL DE PROGRAMAS PREVENTIVOS Y CONTROL DE ENFERMEDADES" ,
            "CENTRO NACIONAL DE TRASPLANTES" ,
            "CENTRO NACIONAL PARA LA SALUD DE LA INFANCIA Y LA ADOLESCENCIA" ,
            "COMISIÓN FEDERAL PARA LA PROTECCIÓN CONTRA RIESGOS SANITARIOS" ,
            "CENTRO NACIONAL DE EXCELENCIA TECNOLÓGICA EN SALUD" ,
            "COMISIÓN NACIONAL DE PROTECCIÓN SOCIAL EN SALUD" ,
            "COMISIÓN NACIONAL DE BIOÉTICA" ,
            "CENTRO NACIONAL PARA LA PREVENCIÓN Y EL CONTROL DE LAS ADICCIONES" ,
            "PROCURADURÍA FEDERAL DE LA DEFENSA DEL TRABAJO" ,
            "COMITÉ NACIONAL MIXTO DE PROTECCIÓN AL SALARIO" ,
            "REGISTRO AGRARIO NACIONAL" ,
            "COMISIÓN NACIONAL DEL AGUA" ,
            "PROCURADURÍA FEDERAL DE PROTECCIÓN AL AMBIENTE" ,
            "COMISIÓN NACIONAL DE ÁREAS NATURALES PROTEGIDAS" ,
            "DELEGACIONES" ,
            "AGREGADURÍAS LEGALES, REGIONALES Y OFICINAS DE ENLACE" ,
            "CENTRO NACIONAL DE PLANEACIÓN, ANÁLISIS E INFORMACIÓN PARA EL COMBATE A LA DELINCUENCIA" ,
            "INSTITUTO DE FORMACIÓN MINISTERIAL, POLICIAL Y PERICIAL" ,
            "CENTRO DE EVALUACIÓN Y CONTROL DE CONFIANZA" ,
            "CENTRO FEDERAL DE PROTECCIÓN A PERSONAS" ,
            "COMISIÓN NACIONAL DE SEGURIDAD NUCLEAR Y SALVAGUARDIAS" ,
            "COMISIÓN REGULADORA DE ENERGÍA" ,
            "COMISIÓN NACIONAL DE HIDROCARBUROS" ,
            "COMISIÓN NACIONAL PARA EL USO EFICIENTE DE LA ENERGÍA" ,
            "INSTITUTO NACIONAL DE DESARROLLO SOCIAL" ,
            "PROSPERA, PROGRAMA DE INCLUSIÓN SOCIAL" ,
            "INSTITUTO DE COMPETITIVIDAD TURÍSTICA (ANTES CENTRO DE ESTUDIOS SUPERIORES DE TURISMO)" ,
            "CORPORACIÓN DE SERVICIOS AL TURISTA ÁNGELES VERDES (CORPORACIÓN ANGELES VERDES)" ,
            "INSTITUTO DE ADMINISTRACIÓN Y AVALÚOS DE BIENES NACIONALES" ,
            "SISTEMA PÚBLICO DE RADIODIFUSIÓN DEL ESTADO MEXICANO" ,
            "TALLERES GRÁFICOS DE MÉXICO" ,
            "CONSEJO NACIONAL PARA PREVENIR LA DISCRIMINACIÓN" ,
            "ARCHIVO GENERAL DE LA NACIÓN" ,
            "COMISIÓN NACIONAL PARA EL DESARROLLO DE LOS PUEBLOS INDÍGENAS" ,
            "NOTIMEX, AGENCIA DE NOTICIAS DEL ESTADO MEXICANO" ,
            "PROCURADURÍA DE LA DEFENSA DEL CONTRIBUYENTE" ,
            "COMISIÓN EJECUTIVA DE ATENCIÓN A VÍCTIMAS (ANTES PROVICTIMA)" ,
            "FINANCIERA NACIONAL DE DESARROLLO AGROPECUARIO, RURAL, FORESTAL Y PESQUERO (ANTES FINANCIERA RURAL)" ,
            "INSTITUTO NACIONAL DE LAS MUJERES" ,
            "COMITÉ NACIONAL PARA EL DESARROLLO SUSTENTABLE DE LA CAÑA DE AZÚCAR" ,
            "COMISIÓN NACIONAL DE LAS ZONAS ÁRIDAS" ,
            "INSTITUTO NACIONAL DE INVESTIGACIONES FORESTALES, AGRÍCOLAS Y PECUARIAS" ,
            "INSTITUTO NACIONAL DE PESCA" ,
            "INSTITUTO NACIONAL PARA EL DESARROLLO DE CAPACIDADES DEL SECTOR RURAL, A.C." ,
            "FIDEICOMISO DE RIESGO COMPARTIDO (*2)" ,
            "FONDO DE EMPRESAS EXPROPIADAS DEL SECTOR AZUCARERO" ,
            "CAMINOS Y PUENTES FEDERALES DE INGRESOS Y SERVICIOS CONEXOS" ,
            "CENTRO NACIONAL DE METROLOGÍA" ,
            "PROCURADURÍA FEDERAL DEL CONSUMIDOR" ,
            "FIDEICOMISO DE FOMENTO MINERO" ,
            "PROMÉXICO" ,
            "COMISIÓN DE OPERACIÓN Y FOMENTO DE ACTIVIDADES ACADÉMICAS DEL INSTITUTO POLITÉCNICO NACIONAL" ,
            "COMISIÓN NACIONAL DE CULTURA FÍSICA Y DEPORTE" ,
            "COMISIÓN NACIONAL DE LIBROS DE TEXTO GRATUITOS" ,
            "INSTITUTO NACIONAL PARA LA EDUCACIÓN DE LOS ADULTOS" ,
            "INSTITUTO NACIONAL DE LENGUAS INDÍGENAS" ,
            "INSTITUTO MEXICANO DE CINEMATOGRAFÍA" ,
            "INSTITUTO NACIONAL DE LA INFRAESTRUCTURA FÍSICA EDUCATIVA" ,
            "PATRONATO DE OBRAS E INSTALACIONES DEL INSTITUTO POLITÉCNICO NACIONAL" ,
            "ESTUDIOS CHURUBUSCO AZTECA, S.A." ,
            "TELEVISIÓN METROPOLITANA, S.A. DE C.V." ,
            "FIDEICOMISO DE LOS SISTEMAS NORMALIZADO DE COMPETENCIA LABORAL Y DE CERTIFICACIÓN DE COMPETENCIA LABORAL" ,
            "FIDEICOMISO PARA LA CINETECA NACIONAL" ,
            "CENTRO REGIONAL DE ALTA ESPECIALIDAD DE CHIAPAS" ,
            "INSTITUTO NACIONAL DE PSIQUIATRÍA RAMÓN DE LA FUENTE MUÑIZ" ,
            "HOSPITAL JUÁREZ DE MÉXICO" ,
            "HOSPITAL GENERAL 'DR. MANUEL GEA GONZÁLEZ'" ,
            "HOSPITAL GENERAL DE MÉXICO" ,
            "HOSPITAL INFANTIL DE MÉXICO FEDERICO GÓMEZ" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DEL BAJÍO" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE OAXACA" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE LA PENÍNSULA DE YUCATÁN" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE CIUDAD VICTORIA 'BICENTENARIO 2010'" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE IXTAPALUCA (*4)" ,
            "INSTITUTO NACIONAL DE CANCEROLOGÍA" ,
            "INSTITUTO NACIONAL DE CARDIOLOGÍA IGNACIO CHÁVEZ" ,
            "INSTITUTO NACIONAL DE ENFERMEDADES RESPIRATORIAS ISMAEL COSÍO VILLEGAS" ,
            "INSTITUTO NACIONAL DE GERIATRÍA (*3)" ,
            "INSTITUTO NACIONAL DE CIENCIAS MÉDICAS Y NUTRICIÓN SALVADOR ZUBIRÁN" ,
            "INSTITUTO NACIONAL DE MEDICINA GENÓMICA (*1)" ,
            "INSTITUTO NACIONAL DE NEUROLOGÍA Y NEUROCIRUGÍA MANUEL VELASCO SUÁREZ" ,
            "INSTITUTO NACIONAL DE PEDIATRÍA" ,
            "INSTITUTO NACIONAL DE PERINATOLOGÍA ISIDRO ESPINOSA DE LOS REYES" ,
            "INSTITUTO NACIONAL DE REHABILITACIÓN" ,
            "INSTITUTO NACIONAL DE SALUD PÚBLICA" ,
            "SISTEMA NACIONAL PARA EL DESARROLLO INTEGRAL DE LA FAMILIA" ,
            "CENTROS DE INTEGRACIÓN JUVENIL, A.C." ,
            "COMISIÓN NACIONAL DE LOS SALARIOS MÍNIMOS" ,
            "PROCURADURÍA AGRARIA" ,
            "COMISIÓN NACIONAL DE VIVIENDA" ,
            "FIDEICOMISO FONDO NACIONAL DE HABITACIONES POPULARES" ,
            "COMISIÓN NACIONAL FORESTAL" ,
            "INSTITUTO MEXICANO DE TECNOLOGÍA DEL AGUA" ,
            "INSTITUTO NACIONAL DE ECOLOGÍA Y CAMBIO CLIMÁTICO (ANTES INSTITUTO NACIONAL DE ECOLOGÍA)" ,
            "INSTITUTO NACIONAL DE CIENCIAS PENALES" ,
            "CONSEJO NACIONAL DE EVALUACIÓN DE LA POLÍTICA DE DESARROLLO SOCIAL" ,
            "INSTITUTO NACIONAL DE LAS PERSONAS ADULTAS MAYORES" ,
            "DICONSA, S.A. DE C.V." ,
            "FONDO NACIONAL PARA EL FOMENTO DE LAS ARTESANÍAS" ,
            "FONATUR CONSTRUCTORA, S.A. DE C.V." ,
            "CONSEJO DE PROMOCIÓN TURÍSTICA DE MÉXICO, S.A. DE C.V." ,
            "FONATUR MANTENIMIENTO TURÍSTICO, S.A. DE C.V." ,
            "FONATUR OPERADORA PORTUARIA, S.A. DE C.V." ,
            "FONDO NACIONAL DE FOMENTO AL TURISMO" ,
            "INSTITUTO NACIONAL DE ESTADÍSTICA Y GEOGRAFÍA" ,
            "COMISIÓN FEDERAL DE COMPETENCIA ECONÓMICA (ANTES COMISIÓN FEDERAL DE COMPETENCIA)" ,
            "INSTITUTO NACIONAL PARA LA EVALUACIÓN DE LA EDUCACIÓN" ,
            "INSTITUTO FEDERAL DE TELECOMUNICACIONES (ANTES COMISIÓN FEDERAL DE TELECOMUNICACIONES)" ,
            "INSTITUTO NACIONAL DE TRANSPARENCIA, ACCESO A LA INFORMACIÓN Y PROTECCIÓN DE DATOS PERSONALES" };
            return Listado;
        }

        protected List<string> Entidades()
        {
            List<string> Listado = new List<string>() {"01","02","03","04","05","06","07","08","09","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32"};
            return Listado;
        }

        protected List<string> CiudadMpio()
        {
            List<string> Listado = new List<string>() {"001","002","003","004","005","006","007","008","009","010","011","012","013","014","015","016","017","018","019",
                "020","021","022","023","024","025","026","027","028","029","030","031","032", "033","034","035","036","037","038","039","040","041","042","043","044",
            "045","046","047","048","049","050","051","052","053","054","055","056","057","058"};
            return Listado;
        }

        protected List<string> NivelTabular()
        {
            List<string> Listado = new List<string>() {"G","H","I","J","K","L","M","N","O","P","CS"};
            return Listado;
        }

        protected List<string> ClavesCostos()
        {
            List<string> Listado = new List<string>() { "74", "111", "130", "148", "185", "222", "259", "266.4", "295", "333" };
            return Listado;
        }

        protected void ProcesarEncabezadoFijo()
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Key", "<script>MakeStaticHeader('" + gvAgregado.ClientID + "', 300, 1170, 80, true); </script>", false);
            //ClientScript.RegisterStartupScript(GetType(), "Key", "<script>MakeStaticHeader('" + gvAgregado.ClientID + "', 300, 1170, 80, true); </script>", true);
        }

    }




}
