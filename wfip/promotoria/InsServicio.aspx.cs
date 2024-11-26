using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using OfficeOpenXml;
using w = wfiplib.Tablas.Institucional.Privado;
using System.Configuration;

namespace wfip.promotoria
{

    public partial class InsServicio : System.Web.UI.Page
    {
        public DataTable dt;

        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        w.InsServicios insservicios;
        w.InsServicioDetalle insserviciodetalle;
        w.InsServiciosEntity insserviciosEntity;
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
            
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");

            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaInicialDDLs();
                Session.Remove("tramite");
                identificaPromotoria();
            }
            else
            {
                if (Session["RegistrosTemporales"] == null)
                {
                    //Nuevo
                    dt = new DataTable();
                    dt.Columns.Add("TipoServicio");
                    dt.Columns.Add("IdTipoServicio");
                    dt.Columns.Add("Accion");
                    dt.Columns.Add("IdAccion");
                    dt.Columns.Add("Certificado");
                    dt.Columns.Add("Subgrupo");
                    dt.Columns.Add("Categoria");
                    dt.Columns.Add("Nombres");
                    dt.Columns.Add("APaterno");
                    dt.Columns.Add("AMaterno");
                    dt.Columns.Add("Parentesco");
                    dt.Columns.Add("IdParentesco");
                    dt.Columns.Add("FNacimiento");
                    dt.Columns.Add("Genero");
                    dt.Columns.Add("IdGenero");
                    dt.Columns.Add("Sueldo");
                    dt.Columns.Add("FMovimiento");
                    dt.Columns.Add("FAntiguedad");
                    dt.Columns.Add("PolizaReCant");
                    dt.Columns.Add("CiaAnterior");
                    dt.Columns.Add("Certificadoimpreso");
                    dt.Columns.Add("TipoCarta");
                    dt.Columns.Add("IdTipoCarta");
                    dt.Columns.Add("NoCertificado2");
                    dt.Columns.Add("NombreContratante");
                    dt.Columns.Add("RFC");
                    dt.Columns.Add("DomFiscal");
                    dt.Columns.Add("FormaPago");
                    dt.Columns.Add("Servicio");
                    dt.Columns.Add("IdServicio");
                    dt.Columns.Add("Comentarios");
                    dt.Columns.Add("FLMovimiento");

                    CargarTabla(gvAgregado, dt);

                    Session["RegistrosTemporales"] = dt;
                }
            }

            // VALIDA EL TIPO DE TRAMITE TANTO PUBLICO COMO PRIVADO
            if (!String.IsNullOrEmpty(Request.QueryString["t"]))
            {
                // Query string value is there so now use it
                String TipoTramite = Request.QueryString["t"];
                if (TipoTramite == "privado" || TipoTramite == "publico")
                {
                    Label3.Text = TipoTramite.ToUpper();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("esperaPromotoria.aspx");
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                /*/ ALAMCENA LOS DATOS APARTIR DE LA FUNCION recuperaCaptura
                wfiplib.serviciosVidaP oDatos = recuperaCaptura();
                //wfiplib.serviciosVida oDatos = recuperaCaptura();

                int idTramite = armaTramiteYGuardaEnMemoria(oDatos.DatosHtml);
                if (idTramite > 0)
                {
                    oDatos.IdTramite = idTramite;
                    Session[wfiplib.E_TipoTramite.serviciosVida.ToString()] = oDatos;
                    Response.Redirect("anexaArchivos.aspx");
                }*/

                // Inicializamos Valores
                if (rbCertImpSi.Checked)
                    txtCertImp.Text = "1";
                else
                    txtCertImp.Text = "0";


                //Guardar los nuevos datos
                insservicios = new w.InsServicios();
                insserviciodetalle = new w.InsServicioDetalle();

                insserviciosEntity = new w.InsServiciosEntity()
                {
                    Producto = int.Parse(ddlTramiteTipo.SelectedValue),
                    Ramo = int.Parse(ddlRamo.SelectedValue),
                    ClavePromotoria = texClavePromotoria.Text,
                    Region = texRegion.Text,
                    Agente = txIdAgente.Text,
                    SubDireccion = texSubDireccion.Text,
                    NoPoliza = txPoliza.Text,
                    GerenteComercial = texGerenteComercial.Text,
                    NoOrden = textNumeroOrden.Text,
                    EjecutivoComercial = texEjecuticoComercial.Text,
                    Contratante = TextBox1.Text,
                    FechaSolicitud = txtFechaSolicitud.Text,
                    Observaciones = txDetalle.Text
                };

                string mensajeError = string.Empty;

                wfiplib.tramiteP tramite = new wfiplib.tramiteP
                {
                    Id = (new wfiplib.admTramite()).siguienteId(),
                    IdTipoTramite = wfiplib.E_TipoTramite.InsPrivadoServicios,
                    IdPromotoria = int.Parse((new wfiplib.admCredencial()).daPromotoria(manejo_sesion.Credencial.Id).ToString()),
                    IdUsuario = manejo_sesion.Credencial.Id,
                    AgenteClave = txIdAgente.Text.Trim().ToUpper(),
                    NumeroOrden = textNumeroOrden.Text.Trim().ToUpper(),
                    FechaSolicitud = txtFechaSolicitud.Text,
                    TipoTramite = "INSTITUCIONAL PRIVADO",
                    Estado = wfiplib.E_EstadoTramite.Registro
                };

                bool recibido = insservicios.AgregarServicio(insserviciosEntity, ref gvAgregado, ref mensajeError, ref tramite);

                MovsAsegurados.Visible = false;
                Cartas.Visible = false;
                Listado.Visible = false;
                LimpiarCampos();

                ddlTramiteTipo.SelectedIndex = 0;
                ddlRamo.SelectedIndex = 0;
                texClavePromotoria.Text = "";
                texRegion.Text = "";
                txIdAgente.Text = "";
                texSubDireccion.Text = "";
                txPoliza.Text = "";
                texGerenteComercial.Text = "";
                textNumeroOrden.Text = "";
                texEjecuticoComercial.Text = "";
                TextBox1.Text = "";
                txDetalle.Text = "";
                gvAgregado.DataSource = null;
                gvAgregado.DataBind();

                if (recibido)
                {
                    string strFolio = (new wfiplib.admTramite()).getFolio(tramite.Id);
                    Response.Redirect("listaMisTramites.aspx?msg=1&folio=" + strFolio);
                    //enviaMsgCliente("No. de folio generado: ");
                }
                else
                {
                    //enviaMsgCliente(mensajeError);
                    mensajes.MostrarMensaje(this, mensajeError);
                }
            }
            catch (Exception ex)
            {
                //enviaMsgCliente(ex.Message);
                mensajes.MostrarMensaje(this, ex.Message);
            }
        }

        protected void cboInsAccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInsAccion.SelectedValue.Equals("2"))
            {
                lblSueldo.Visible = false;
                txtSueldo.Visible = false;
                lblFechaNac.Visible = false;
                txtFechaNac.Visible = false;
                lblGenero.Visible = false;
                ddlGenero.Visible = false;
                Label8.Visible = false;
                if (ddlTramiteTipo.SelectedValue == "1")
                {
                    lblSueldo.Visible = true;
                    txtSueldo.Visible = true;
                }
                else
                {
                    lblSueldo.Visible = false;
                    txtSueldo.Visible = false;
                }
            }
            else
            {
                lblSueldo.Visible = true;
                txtSueldo.Visible = true;
                lblFechaNac.Visible = true;
                txtFechaNac.Visible = true;
                lblGenero.Visible = true;
                ddlGenero.Visible = true;
                Label8.Visible = true;
            }
            lbNombreAgente.Text = ObtenerNombreDeAgente(hf_IdPromotoria.Value, txIdAgente.Text);
        }

        protected void chkfechaAntiguedad_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfechaAntiguedad.Checked)
            {
                txtFechaAnt.Visible = true;
                Label2.Visible = true;
                Label6.Visible = true;
                txtPolizaRecAnt.Visible = true;
                Label7.Visible = true;
                txtCiaAntRec.Visible = true;
                Label14.Visible = true;
                FileUpload1.Visible = true;
                btnSubirSoporte.Visible = true;
            }
            else
            {
                txtFechaAnt.Visible = false;
                Label2.Visible = false;
                Label6.Visible = false;
                txtPolizaRecAnt.Visible = false;
                Label7.Visible = false;
                txtCiaAntRec.Visible = false;
                Label14.Visible = false;
                FileUpload1.Visible = false;
                btnSubirSoporte.Visible = true;
            }
        }

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTipoServicio.BackColor = Color.White;

            txtSubGrupo.Visible = true;
            txtSubGrupo.Visible = true;
            chkfechaAntiguedad.Visible = true;
            rbCertImpSi.Visible = true;
            rbCertImpNo.Visible = true;
            Label15.Visible = true;
            Label16.Visible = true;
            Label17.Visible = true;
            txtSubGrupo.Visible = true;
            ddlInsAccion.Visible = true;
            Label18.Visible = true;

            if (ddlTipoServicio.SelectedValue.Equals("1"))
            {
                MovsAsegurados.Visible = true;
                Cartas.Visible = false;
                Listado.Visible = false;
            }
            else if (ddlTipoServicio.SelectedValue.Equals("2"))
            {
                MovsAsegurados.Visible = true;
                Cartas.Visible = false;
                Listado.Visible = false;

                txtSubGrupo.Visible = false;
                txtSubGrupo.Visible = false;
                chkfechaAntiguedad.Visible = false;
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
                Cartas.Visible = true;
                Listado.Visible = false;

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
            else if (ddlTipoServicio.SelectedValue.Equals("4"))
            {
                //MovsAsegurados.Visible = false;
                //Listado.Visible = true;
                //Cartas.Visible = false;

                MovsAsegurados.Visible = false;
                Cartas.Visible = true;
                Listado.Visible = false;

                Label9.Text = "Tipo de Listados";
                Label10.Visible = false;
                txtNoCertificado.Visible = false;
                txtCNombres.Visible = false;
                txtCAPaterno.Visible = false;
                txtCAMaterno.Visible = false;
                Label11.Visible = false;
                Label12.Visible = false;
                Label13.Visible = false;
            }
            else
            {
                MovsAsegurados.Visible = false;
                Cartas.Visible = false;
                Listado.Visible = false;
            }

            lbNombreAgente.Text = ObtenerNombreDeAgente(hf_IdPromotoria.Value, txIdAgente.Text);
        }

        protected void TramiteTipPoliza_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTramiteTipo.SelectedValue == "1")
            {
                lblSueldo.Visible = true;
                txtSueldo.Visible = true;
            }
            else
            {
                lblSueldo.Visible = false;
                txtSueldo.Visible = false;
            }

            insservicios = new w.InsServicios();
            insservicios.LlenarInsRamo_DropdownList(ref ddlRamo, ddlTramiteTipo.SelectedValue);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ddlTipoServicio.SelectedValue == "0")
            {
                ddlTipoServicio.BackColor = Color.Pink;
                return;
            }

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
            ddlTipoServicio.SelectedValue == "1" ? txtNombres.Text : ddlTipoServicio.SelectedValue == "2" ? txtCNombres.Text : "",
            ddlTipoServicio.SelectedValue == "1" ? txtAPaterno.Text : ddlTipoServicio.SelectedValue == "2" ? txtCAPaterno.Text : "",
            ddlTipoServicio.SelectedValue == "1" ? txtAMaterno.Text : ddlTipoServicio.SelectedValue == "2" ? txtCAMaterno.Text : "",
            ddlParentesco.SelectedItem.Text,
            ddlParentesco.SelectedValue,
            txtFechaNac.Text,
            ddlGenero.SelectedItem.Text,
            ddlGenero.SelectedValue,
            txtSueldo.Text,
            txtFechaMov.Text,
            txtFechaAnt.Text,
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

            //Limpiar los campos luego de agregar a la tabla temporal
            LimpiarCampos();
        }

        protected void gvAgregado_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblTipoServicio = (Label)gvAgregado.Rows[e.RowIndex].FindControl("lblTipoServicio");

            string tiposervicio = lblTipoServicio.Text;

            dt = (DataTable)Session["RegistrosTemporales"];

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];

                if (dr["TipoServicio"].ToString() == tiposervicio)
                    dr.Delete();
            }

            Session["RegistrosTemporales"] = dt;
            CargarTabla(gvAgregado, dt);
        }

        protected void gvAgregado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //modificar los datos del registro existente
            gvAgregado.EditIndex = e.NewEditIndex;

            dt = (DataTable)Session["RegistrosTemporales"];

            //txFechaInVi.Text = "";

            CargarTabla(gvAgregado, dt);
        }

        protected void gvAgregado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Sólo cancela, no hace nada más
            gvAgregado.EditIndex = -1;
            CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);

        }

        protected void gvAgregado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //vuelve a actualizar la fila modificada y la debe agregar al datatable
            gvAgregado.EditIndex = -1;
            CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);
        }

        protected void btnSubirExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUpDocumento.PostedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    //enviaMsgCliente("El archivo que intenta usar no es de excel, revise.");
                    mensajes.MostrarMensaje(this, "El archivo que intenta usar no es de excel, revise.");
                    return;
                }
                if (fileUpDocumento.HasFile)
                {
                    DataTable dt = new DataTable();
                    ExcelPackage pagina = new ExcelPackage(fileUpDocumento.FileContent);
                    dt = wfiplib.ExtensionPaqueteriaExcel.Excel_A_DataTable(pagina);
                    //disponer del datatable

                    //agregar columnas faltantes al datatable

                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        dt.Columns.Add("TipoServicio");
                        //dt.Columns.Add("IdTipoServicio");
                        dt.Columns.Add("Accion");
                        //dt.Columns.Add("IdAccion");
                        //dt.Columns.Add("Certificado");
                        //dt.Columns.Add("SubGrupo");
                        //dt.Columns.Add("Categoria");
                        //dt.Columns.Add("Nombres");
                        //dt.Columns.Add("APaterno");
                        //dt.Columns.Add("AMaterno");
                        dt.Columns.Add("Parentesco");
                        //dt.Columns.Add("IdParentesco");
                        //dt.Columns.Add("FNacimiento");
                        dt.Columns.Add("Genero");
                        //dt.Columns.Add("IdGenero");
                        //dt.Columns.Add("Sueldo");
                        //dt.Columns.Add("FMovimiento");
                        //dt.Columns.Add("FAntiguedad");
                        //dt.Columns.Add("PolizaRecAnt");
                        dt.Columns.Add("CiaAnterior");
                        //dt.Columns.Add("CertificadoImpreso");
                        dt.Columns.Add("TipoCarta");
                        dt.Columns.Add("IdTipoCarta");
                        dt.Columns.Add("NoCertificado2");
                        dt.Columns.Add("NombreContratante");
                        dt.Columns.Add("RFC");
                        dt.Columns.Add("DomFiscal");
                        dt.Columns.Add("FormaPago");
                        dt.Columns.Add("Servicio");
                        dt.Columns.Add("IdServicio");
                        dt.Columns.Add("Comentarios");
                        dt.Columns.Add("FLMovimiento");
                    }
                    else if (dt.Rows[0][0].ToString() == "2")
                    {
                        dt.Columns.Add("TipoServicio");
                        //dt.Columns.Add("IdTipoServicio");
                        dt.Columns.Add("Accion");
                        //dt.Columns.Add("IdAccion");
                        //dt.Columns.Add("Certificado");
                        dt.Columns.Add("SubGrupo");
                        dt.Columns.Add("Categoria");
                        //dt.Columns.Add("Nombres");
                        //dt.Columns.Add("APaterno");
                        //dt.Columns.Add("AMaterno");
                        dt.Columns.Add("Parentesco");
                        //dt.Columns.Add("IdParentesco");
                        dt.Columns.Add("FNacimiento");
                        dt.Columns.Add("Genero");
                        dt.Columns.Add("IdGenero");
                        dt.Columns.Add("Sueldo");
                        dt.Columns.Add("FMovimiento");
                        dt.Columns.Add("FAntiguedad");
                        dt.Columns.Add("PolizaRecAnt");
                        dt.Columns.Add("CiaAnterior");
                        dt.Columns.Add("CertificadoImpreso");
                        dt.Columns.Add("TipoCarta");
                        dt.Columns.Add("IdTipoCarta");
                        dt.Columns.Add("NoCertificado2");
                        dt.Columns.Add("NombreContratante");
                        dt.Columns.Add("RFC");
                        dt.Columns.Add("DomFiscal");
                        dt.Columns.Add("FormaPago");
                        dt.Columns.Add("Servicio");
                        dt.Columns.Add("IdServicio");
                        dt.Columns.Add("Comentarios");
                        dt.Columns.Add("FLMovimiento");
                    }
                    else if (dt.Rows[0][0].ToString() == "3")
                    {
                        dt.Columns.Add("TipoServicio");
                        //dt.Columns.Add("IdTipoServicio");
                        dt.Columns.Add("Accion");
                        dt.Columns.Add("IdAccion");
                        dt.Columns.Add("Certificado");
                        //dt.Columns.Add("SubGrupo");
                        //dt.Columns.Add("Categoria");
                        dt.Columns.Add("Nombres");
                        dt.Columns.Add("APaterno");
                        dt.Columns.Add("AMaterno");
                        dt.Columns.Add("Parentesco");
                        dt.Columns.Add("IdParentesco");
                        dt.Columns.Add("FNacimiento");
                        dt.Columns.Add("Genero");
                        dt.Columns.Add("IdGenero");
                        dt.Columns.Add("Sueldo");
                        dt.Columns.Add("FMovimiento");
                        dt.Columns.Add("FAntiguedad");
                        dt.Columns.Add("PolizaRecAnt");
                        dt.Columns.Add("CiaAnterior");
                        dt.Columns.Add("CertificadoImpreso");
                        dt.Columns.Add("TipoCarta");
                        dt.Columns.Add("IdTipoCarta");
                        dt.Columns.Add("NoCertificado2");
                        //dt.Columns.Add("NombreContratante");
                        //dt.Columns.Add("RFC");
                        //dt.Columns.Add("DomFiscal");
                        //dt.Columns.Add("FormaPago");
                        dt.Columns.Add("Servicio");
                        //dt.Columns.Add("IdServicio");
                        //dt.Columns.Add("Comentarios");
                        //dt.Columns.Add("FLMovimiento");
                    }
                    else
                    {
                        //enviaMsgCliente("Error en el tipo de servicio, debe ser 1, 2 ó 3");
                        mensajes.MostrarMensaje(this, "Error en el tipo de servicio, debe ser 1, 2 ó 3");
                        return;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == "1")
                        {
                            dt.Rows[i][16] = "Movimientos de Asegurados";
                            if (dt.Rows[i][1].ToString() == "1")
                                dt.Rows[i][17] = "Alta";
                            if (dt.Rows[i][10].ToString() == "M")
                            {
                                dt.Rows[i][19] = "Masculino";
                                dt.Rows[i][20] = "2";
                            }
                            else if (dt.Rows[i][10].ToString() == "F")
                            {
                                dt.Rows[i][19] = "Femenino";
                                dt.Rows[i][20] = "3";
                            }
                            else
                            {
                                //enviaMsgCliente("Error en el Género, debe ser 1, ó 2");
                                mensajes.MostrarMensaje(this, "Error en el Género, debe ser 1, ó 2");
                                return;
                            }
                        }
                        else if (dt.Rows[i][0].ToString() == "2")
                        {
                            dt.Rows[i][7] = "Cartas";
                            if (dt.Rows[i][6].ToString() == "1")
                                dt.Rows[i][11] = "Titular";
                            else if (dt.Rows[i][6].ToString() == "2")
                                dt.Rows[i][11] = "Cónyuge";
                            else if (dt.Rows[i][6].ToString() == "3")
                                dt.Rows[i][11] = "Hijo";
                            else if (dt.Rows[i][6].ToString() == "4")
                                dt.Rows[i][11] = "Hijo de 25 o más";
                            else if (dt.Rows[i][6].ToString() == "5")
                                dt.Rows[i][11] = "Ascendientes";
                            else if (dt.Rows[i][6].ToString() == "6")
                                dt.Rows[i][11] = "Suegros";
                            else if (dt.Rows[i][6].ToString() == "7")
                                dt.Rows[i][11] = "Otro 1";
                            else if (dt.Rows[i][6].ToString() == "8")
                                dt.Rows[i][11] = "Otro 2";
                            else if (dt.Rows[i][6].ToString() == "9")
                                dt.Rows[i][11] = "Pareja mismo sexo";
                            else if (dt.Rows[i][6].ToString() == "10")
                                dt.Rows[i][11] = "Concubino";
                        }
                        else if (dt.Rows[i][0].ToString() == "3")
                            dt.Rows[i][10] = "Listados";

                    }

                    CargarTabla(gvAgregado, (DataTable)Session["RegistrosTemporales"]);

                    //Verificar si el grid tiene registros
                    if (gvAgregado.Rows.Count > 0)
                    {
                        DataTable dtt = new DataTable();
                        dtt = (DataTable)gvAgregado.DataSource;
                        dt.Merge(dtt);
                    }

                    Session["RegistrosTemporales"] = dt;
                    CargarTabla(gvAgregado, dt);
                }
            }
            catch (Exception ex)
            {
                //enviaMsgCliente("Error al subir el archivo: " + ex.Message);
                mensajes.MostrarMensaje(this, "Error al subir el archivo: " + ex.Message);
            }
        }

        #endregion

        #region Metodos ***************************************************************************************************************************************
        

        private void CargaInicialDDLs()
        {
            insservicios = new w.InsServicios();

            //Producto
            insservicios.LlenarInsProducto_DropdownList(ref ddlTramiteTipo);
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

            //Prueba
            //insservicios.PruebaLlenadoconDataReader();
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        private void identificaPromotoria()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria) //1 Promotoría
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    hf_IdPromotoria.Value = manejo_sesion.Credencial.IdPromotoria.ToString();
                }
            }
        }

        /// <summary>
        /// Muestra los datos obtenidos de una tabla temporal en un gridview
        /// y mostrarlos en la página
        /// </summary>
        /// <param name="gridview"></param>
        /// <param name="dt"></param>
        private void CargarTabla(GridView gridview, DataTable dt)
        {
            gridview.DataSource = dt;
            gridview.DataBind();
        }

        private void LimpiarCampos()
        {
            ddlInsAccion.SelectedIndex = 0;
            txtCertificado.Text = "";
            txtSubGrupo.Text = "";
            txtCategoria.Text = "";
            txtNombres.Text = "";
            txtAPaterno.Text = "";
            txtAMaterno.Text = "";
            ddlParentesco.SelectedIndex = 0;
            txtFechaNac.Text = "";
            ddlGenero.SelectedIndex = 0;
            txtSueldo.Text = "";
            txtFechaMov.Text = "";
            txtFechaAnt.Text = "";
            txtPolizaRecAnt.Text = "";
            txtCiaAntRec.Text = "";
            txtCertImp.Text = "";
            ddlTipoCarta.SelectedIndex = 0;
            txtNoCertificado.Text = "";
            txtCNombres.Text = "";
            txtCAPaterno.Text = "";
            txtCAMaterno.Text = "";
            txtLSubGrupo.Text = "";
            txtLCategoria.Text = "";
            txtContratante.Text = "";
            txtRFC.Text = "";
            txtDomFiscal.Text = "";
            txtFormaPago.Text = "";
            ddlInsServicioListados.SelectedIndex = 0;
            txtLComentarios.Text = "";
            txtFLMovimiento.Text = "";
            
        }

        #endregion

        #region Web Services***********************************************************************************************************************************

        [System.Web.Services.WebMethod()]
        public static string TieneTramitesanteriores(string rfc)
        {
            string dato = "0";
            bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedente(rfc);
            if (resultado)
            {
                dato = "1";
            }
            return dato;
        }

        [System.Web.Services.WebMethod()]
        public static string ValidarFechaVigencia(string FechaIn, string FechaVigencia)
        {
            string resultado = "FECHA REQUERIDA";
            if (!string.IsNullOrEmpty(FechaVigencia) && !string.IsNullOrEmpty(FechaIn))
            {
                DateTime.TryParseExact(FechaVigencia, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha1);
                DateTime.TryParseExact(FechaIn, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha2);

                /*DateTime fecha1 = Convert.ToDateTime(FechaIn);
                DateTime fecha2 = Convert.ToDateTime(FechaVigencia);
                */
                //resultado = FechaIn + " " + FechaVigencia;
                /*
                DateTime fecha1 = Convert.ToDateTime(FechaIn);
                DateTime fecha2 = Convert.ToDateTime(FechaVigencia);
                */
                if (fecha1 <= fecha2)
                {
                    resultado = "No puede ser menor o igual a FECHA INICIO";
                }
                else
                {
                    resultado = "ACEPTADO ";
                }
                ///    
                //wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria()).carga(Convert.ToInt32(pIdPromotoria));
                //wfiplib.agentePromotoria agente = (new wfiplib.admAgentesPromotoria()).buscaAgenteEnPromotoria(promotoria.Clave, pClaveAgente);
                //if (agente.clave > 0) resultado = agente.descripcion;

            }
            return resultado;
        }

        [System.Web.Services.WebMethod()]
        public static string ObtenerNombreDeAgente(string pIdPromotoria, string pClaveAgente)
        {
            string resultado = "NO EXISTE";
            if (!string.IsNullOrEmpty(pIdPromotoria) && !string.IsNullOrEmpty(pClaveAgente))
            {
                wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(pIdPromotoria));
                wfiplib.agentePromotoria agente = (new wfiplib.admAgentesPromotoria()).buscaAgenteEnPromotoria(promotoria.Clave, pClaveAgente);
                if (agente.clave > 0)
                    resultado = agente.descripcion;
            }
            return resultado;
        }

        [ScriptMethod(), WebMethod()]
        public static string[] ObtenerNombreRegion(string pIdPromotoria)
        {
            String[] array = new string[10];
            array[0] = string.Concat(pIdPromotoria, " Región");
            array[1] = string.Concat(pIdPromotoria, " Subdirección");
            array[2] = string.Concat(pIdPromotoria, " Gerente comercial");
            array[3] = string.Concat(pIdPromotoria, " Ejecutivo comercial");

            return array;
        }



        #endregion
    }
}