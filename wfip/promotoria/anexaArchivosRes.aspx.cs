using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;

namespace wfip.promotoria
{
    public partial class anexaArchivosRes : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pintaChecks();
            pintRegreso();
            ConsultaMegasPromotoria();

            if (!IsPostBack)
            {
                pintaCabeceraHtml();
                MuestraInfoExpediente();
                MuestraDocumentos();
                //MuestraDatos();
            }

        }

        protected void ConsultaMegasPromotoria()
        {
            DataTable data = (new wfiplib.admAgentesPromotoria()).ConsultaMegasPromotoria(manejo_sesion.Promotoria.Id);
            foreach (DataRow Megas in data.Rows)
            {
                lbNombreAgente.Text = Megas["Megas"].ToString();
                LabelTamExpediente.Text = Megas["Megas"].ToString();
            }
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (ValidaExpediente())
            {
                string script = "";
                script =


                "" +
                "$('#cph_areaTrabajo_BtnContinuar').attr('disabled','disabled');" +
                "swal({" +
                "icon: 'info',"+
                "title: 'Confirmación'," +

                        "text: '¿Deseas crear el tramite?'," +

                        "type: 'warning'," +

                       "buttons:" +
                    "{" +
                    "cancel: " +
                        "{" +
                        "visible: true," +
                                "text: 'Cancelar'," +
                                "className: 'btn btn-danger'" +
                            "},      " +
                            "confirm: " +
                        "{" +
                        "text: 'Aceptar'," +
                                "className: 'btn btn-primary'" +
                            "}" +
                    "}" +
                "}).then((Delete) => {" +
                   "if (Delete) {" +
                       "document.getElementById('cph_areaTrabajo_Button1').click(); " +
                   "}else {" +
                        "$('#cph_areaTrabajo_BtnContinuar').removeAttr('disabled');" +
                        "$('#btnAdd').trigger('click');" +
                        "swal.close();" +
                        
                   "}"+
                "});";

                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }
        protected void Button_Continuar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EvaluaDocumento()))
            {
                wfiplib.admTramite oAdmTramite = new wfiplib.admTramite();
                // CARGA CONTENIDO DEL TRAMITE.
                wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                wfiplib.EmisionVG oTramiteComple = null;

                switch (oTramite.IdTipoTramite)
                {
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        oTramiteComple = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                        break;

                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        oTramiteComple = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()];
                        break;
                    case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                        oTramiteComple = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionConversiones.ToString()];
                        break;
                }

                oTramite.AgenteClave = txIdAgente.Text.Trim();
                oTramite.UsuarioNombre = manejo_sesion.Credencial.Usuario;

                DataTable DataFolio = oAdmTramite.spTramiteNuevo(oTramite, oTramiteComple);

                string Folio = "";
                int IdTramite = 0;
                //string Error = "";

                foreach (DataRow dr in DataFolio.Rows)
                {
                    Folio = dr["Folio"].ToString();
                    IdTramite = Convert.ToInt32(dr["Id"].ToString());

                }

                if(Folio != "KO")
                {
                    List<wfiplib.expediente> LstExpediente = new List<wfiplib.expediente>();
                    if (Session["documentos"] != null)
                        LstExpediente = (List<wfiplib.expediente>)Session["documentos"];

                    string msgError = registraDocumentos(IdTramite, LstExpediente);

                    Session.Remove("documentos");
                    Session.Remove("insumos");
                    Session.Remove("AnexoArchivos");
                    Session.Remove("tramite");
                    Session.Remove("TamExpedinte");

                    /* EJECUTAR PROCEDIMEINTO DE FUCION DE ARCHIVOS*/
                    string script = "swal({" +
                                                "title:'Registro terminado.'," +
                                                "text: 'Nuevo folio:  " + Folio + " '," +
                                                "icon: 'success'," +
                                                "buttons:" +
                                            "{" +
                                            "confirm:" +
                                                "{" +
                                                "text: 'Aceptar'," +
                                                        "value: true," +
                                                        "visible: true," +
                                                        "className: 'btn btn-info'," +
                                                        "closeModal: true" +
                                                "}" +
                                            "}" +
                                        "}).then(" +
                                            "function() {" +
                                                "var url = 'listaMisTramites.aspx'; " +
                                                "$(location).attr('href', url); " +
                                            "}" +
                                        ");";

                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    string script = "";
                    script = "swal('Algo ocurrió!', 'No es posible registrar el trámite, inténtelo mas tarde.!', {" +
                                    "icon: 'warning'," +
                                    "buttons:" +
                                "{" +
                                "confirm:" +
                                "{" +
                                    "className: 'btn btn-warning'" +

                                "}" +
                            "}," +
                                "}); ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
            }
            else
            {
                string script = "";
                script = "swal('Advertencia!', 'Uno de tus archivos se encuentra dañado y no es posible procesarlo, recomendamos revisar y abrir el contenido de los archivos seleccionados !', {" + 
                                "icon: 'warning'," +
						        "buttons:" +
                            "{"+
                            "confirm:"+
                            "{"+
                                "className: 'btn btn-warning'"+

                            "}"+
                        "}," +
					        "}); ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
                
        }
        private string registraDocumentos(int pIdTramite, List<wfiplib.expediente> pLstDocumentos)
        {
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";
            try
            {
                wfiplib.expediente expediente = new wfiplib.expediente();

                //strRutaServidor = Server.MapPath("~") + "\\DocsUp\\";
                strRutaServidor = Server.MapPath("~") + expediente.CarpetaInicial;

                List<wfiplib.expediente> LstExpediente = new List<wfiplib.expediente>();
                wfiplib.admExpediente oAdmExp = new wfiplib.admExpediente();

                /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
                if (Session["documentos"] != null)
                {
                    LstExpediente = (List<wfiplib.expediente>)Session["documentos"];
                }

                List<string> lstArchivos = new List<string>();
                foreach (wfiplib.expediente oDocumento in LstExpediente)
                {
                    //strArchivoOrigen = Server.MapPath("~") + "\\DocsUp\\" + oDocumento.NmArchivo;
                    strArchivoOrigen = Server.MapPath("~") + oDocumento.CarpetaInicial + oDocumento.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        oDocumento.IdTramite = pIdTramite;
                        oAdmExp.NuevoRes(oDocumento); //archivos.Agregar_Expedientes_Tramite(pIdTramite, oDocumento.Id_Archivo, oDocumento.NmArchivo, oDocumento.NmOriginal, oDocumento.Activo, oDocumento.Fusion, oDocumento.Descripcion);
                        lstArchivos.Add(strArchivoOrigen);
                        
                    }
                }

                DataTable data = new wfiplib.admExpediente().ControlArchivoNuevoID();
                int _Id = 0;
                string nombreArchivo = "";

                foreach (DataRow control_Archivos in data.Rows)
                {
                    _Id = Convert.ToInt32(control_Archivos["Id"].ToString());
                    nombreArchivo = control_Archivos["Clave"].ToString();
                }

                int IdControlArchivo = _Id;
                // string nombreFusion = IdControlArchivo.ToString().PadLeft(8, '0') + ".pdf";
                string nombreFusion = nombreArchivo + ".pdf";
                msgError = (new wfiplib.admPdfFusion()).Fusiona(lstArchivos, strRutaServidor + nombreFusion);
                if (string.IsNullOrEmpty(msgError))
                {
                    //archivos.Agregar_Expedientes_Tramite(pIdTramite, IdControlArchivo, nombreFusion, "Archivo Fusion", 1, 1, "");
                    wfiplib.expediente oFusion = new wfiplib.expediente();
                    oFusion.Id_Archivo = IdControlArchivo;
                    oFusion.IdTramite = pIdTramite;
                    oFusion.NmArchivo = nombreFusion;
                    oFusion.NmOriginal = "";
                    oFusion.Activo = wfiplib.E_SiNo.Si;
                    oFusion.Fusion = wfiplib.E_SiNo.Si;
                    oAdmExp.NuevoRes(oFusion);

                    File.Copy(strRutaServidor + nombreFusion, expediente.CarpetaArchivada + nombreFusion);

                    msgError = "";
                }
            }

            catch (Exception ex) { msgError = ex.Message; }
            return "";

            /*
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";

            try
            {
                strRutaServidor = Server.MapPath("~") + "\\DocsUp\\";
                wfiplib.admExpediente oAdmExp = new wfiplib.admExpediente();
                List<string> lstArchivos = new List<string>();

                foreach (wfiplib.expediente oDocumento in pLstDocumentos)
                {
                    strArchivoOrigen = Server.MapPath("~") + "\\DocsUp\\" + oDocumento.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        oAdmExp.Nuevo(oDocumento);
                        lstArchivos.Add(strArchivoOrigen);
                    }
                }

                int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                string nombreFusion = IdArchivo.ToString().PadLeft(8, '0') + ".pdf";
                msgError = (new wfiplib.admPdfFusion()).Fusiona(lstArchivos, strRutaServidor + nombreFusion);
                if (string.IsNullOrEmpty(msgError))
                {
                    wfiplib.expediente oFusion = new wfiplib.expediente();
                    oFusion.Id = IdArchivo;
                    oFusion.IdTramite = pIdTramite;
                    oFusion.NmArchivo = nombreFusion;
                    oFusion.NmOriginal = "";
                    oFusion.Activo = wfiplib.E_SiNo.Si;
                    oFusion.Fusion = wfiplib.E_SiNo.Si;
                    oAdmExp.Nuevo(oFusion);

                    msgError = "";
                }
            }

            catch (Exception ex) { msgError = ex.Message; }
            return "";
            */
        }
        private string EvaluaDocumento() 
        {
            string msgError = "";
            string strRutaServidor = "";
            string strArchivoOrigen = "";

            try
            {
                wfiplib.expediente expedientes = new wfiplib.expediente();

                strRutaServidor = Server.MapPath("~") + "\\" + expedientes.CarpetaInicial + "\\";
                List<wfiplib.expediente> LstExpediente = new List<wfiplib.expediente>();
                /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
                if (Session["documentos"] != null)
                {
                    LstExpediente = (List<wfiplib.expediente>)Session["documentos"];
                }

                List<string> lstArchivos = new List<string>();
                foreach (wfiplib.expediente oDocumento in LstExpediente)
                {
                    strArchivoOrigen = Server.MapPath("~") + "\\" + expedientes.CarpetaInicial + "\\" + oDocumento.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        lstArchivos.Add(strArchivoOrigen);
                    }
                }

                // NUEVO ID DE NUEVO EXPEDIENTE FUSIONADO
                DataTable data = new wfiplib.admExpediente().ControlArchivoNuevoID();
                int _Id = 0;

                foreach (DataRow control_Archivos in data.Rows)
                {
                    _Id = Convert.ToInt32(control_Archivos["Id"].ToString());
                }

                int IdControlArchivo = _Id;
                string nombreFusion = IdControlArchivo.ToString().PadLeft(8, '0') + ".pdf";

                msgError = (new wfiplib.admPdfFusion()).Fusiona(lstArchivos, strRutaServidor + nombreFusion);
            }
            catch (Exception ex) { msgError = ex.Message; }
            return msgError;
        }
        protected void MuestraInfoExpediente()
        {
            LabelExpedienteRestante.Text = "";
            LabelRestantes.Text = "";

            int max = Convert.ToInt32(hfArchivos.Value);
            LabelExpedienteMax.Text = hfArchivos.Value;

            // CONSULTA EL TAMAÑO PERMITIDO PARA EL USUARIO.
            if (Session["TamExpedinte"] != null)
            {
                int tam = Convert.ToInt32(Session["TamExpedinte"]);

                int restante = max - tam;
                LabelExpedienteRestante.Text = restante.ToString();
                LabelRestantes.Text = restante.ToString();
            }
            else
            {
                LabelExpedienteRestante.Text = max.ToString();
                LabelRestantes.Text = max.ToString();
            }
        }
        protected void BtnposBack(object sender, EventArgs e)
        {
            string url = Session["URL"].ToString();
            GuardaDatos();
            Response.Redirect(url);
        }
        protected void daNombreDeAgente(object sender, EventArgs e)
        {
            NombreAgente();
        }
        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            LabRespuestaArchivosCarga.Text = "";
            /* LISTA LOS ARCHIVOS DEL DOCUMENTO */
            List<wfiplib.expediente> LstArchivosDocumento = new List<wfiplib.expediente>();
            // SI EXISTE LA VARIABLE DE SESION LLENA LA LISTA
            if (Session["documentos"] != null)
            {
                LstArchivosDocumento = (List<wfiplib.expediente>)Session["documentos"];
            }
            // VALIDA LA CARGA DE LOS ARHIVOS.
            if (fileUpDocumento.HasFile)
            {
                //Variable en donde se almacena los archivos seleccionados
                HttpFileCollection multipleFiles = Request.Files;

                // TAMAÑO MAXIMO DE SUBIDA VALOR DEL 1 MEGABIY A BITS
                int mega = 1048576;
                int Meg = 0;

                if (Session["TamExpedinte"] != null)
                {
                    Meg = Convert.ToInt32(hfArchivos.Value) - Convert.ToInt32(Session["TamExpedinte"]);
                }
                else
                {
                    // CONSULTA A LA BASE DE DATOS LOS 10 MEGAS NECESARIOS
                    Meg = Convert.ToInt32(hfArchivos.Value);
                }

                int TamMax = mega * Meg;
                int TamArchi = 0;
                int TamArchRegistrados = 0;

                //leer cada archivo seleccionado
                for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
                {
                    //archivo seleccionado
                    HttpPostedFile uploadedFile = multipleFiles[fileCount];
                    int num = uploadedFile.ContentLength;

                    TamArchi += num;
                }

                // AGREGAR VALIDACION DE VARIABLE DE SESION 
                if (TamArchi > TamMax)
                {
                    LabRespuestaArchivosCarga.Text = "La carga de archivos sobrepaso los " + Meg + " MG";
                }
                else
                {
                    int ArchivosRegistrados = 0;
                    int ArchivosNoRegistrados = 0;
                    //leer cada archivo seleccionado
                    for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
                    {
                        //archivo seleccionado
                        HttpPostedFile uploadedFile = multipleFiles[fileCount];

                        String fileExtension = System.IO.Path.GetExtension(uploadedFile.FileName).ToLower();

                        if (".pdf".Contains(fileExtension) ^ ".jpg".Contains(fileExtension) ^ ".png".Contains(fileExtension))
                        {
                            wfiplib.expediente expedientes = new wfiplib.expediente();
                            wfiplib.admPdfFusion ConJPGaPDF = new wfiplib.admPdfFusion();

                            // NUEVO ID DEL CADA ARCHIV A CARGAR EN  EL EXPEDIENTE.

                            DataTable data = new wfiplib.admExpediente().ControlArchivoNuevoID();
                            int _Id = 0;
                            string nombreArchivo = "";
                            foreach (DataRow control_Archivos in data.Rows)
                            {
                                _Id = Convert.ToInt32(control_Archivos["Id"].ToString());
                                nombreArchivo = control_Archivos["Clave"].ToString();
                            }
                         
                            int IdControlArchivo = _Id;
                            //nombreArchivo = IdControlArchivo.ToString().PadLeft(8, '0');
                            //string directorioTemporal = Server.MapPath("~") + "\\DocsUp\\";
                            string directorioTemporal = Server.MapPath("~") + expedientes.CarpetaInicial;
                            string fileExtension2 = "";

                            uploadedFile.SaveAs(directorioTemporal + nombreArchivo + fileExtension);
                            
                            if (!fileExtension.Equals(".pdf"))
                            {
                                if (ConJPGaPDF.ConviertePDF(directorioTemporal + nombreArchivo + fileExtension, directorioTemporal + nombreArchivo + ".pdf"))
                                {
                                    fileExtension2 = ".pdf";
                                }
                            }

                            fileExtension2 = ".pdf";

                            bool archivoEnPdf = false;
                            if (!fileExtension2.Equals(".pdf"))
                            {
                                archivoEnPdf = false;
                            }
                            else
                            {
                                nombreArchivo = nombreArchivo + fileExtension2;
                                archivoEnPdf = true;
                            }

                            if (archivoEnPdf)
                            {
                                expedientes.Id_Archivo = IdControlArchivo;
                                expedientes.NmArchivo = nombreArchivo;
                                expedientes.NmOriginal = uploadedFile.FileName;
                                expedientes.Tam = uploadedFile.ContentLength;
                                expedientes.Activo = wfiplib.E_SiNo.Si;
                                expedientes.Fusion = 0;
                                expedientes.Descripcion = "";

                                LstArchivosDocumento.Add(expedientes);
                                lstDocumentos.DataSource = LstArchivosDocumento;
                                lstDocumentos.DataValueField = "Id_Archivo";
                                lstDocumentos.DataTextField = "NmOriginal";
                                lstDocumentos.DataBind();

                                Session["documentos"] = LstArchivosDocumento;
                                manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
                            }
                            ArchivosRegistrados = ArchivosRegistrados + 1;
                            TamArchRegistrados = TamArchRegistrados + uploadedFile.ContentLength;
                        }
                        else
                        {
                            ArchivosNoRegistrados = ArchivosNoRegistrados + 1;

                        }
                    }

                    int total = TamArchRegistrados / mega;

                    if (Session["TamExpedinte"] != null)
                    {
                        total = total + Convert.ToInt32(Session["TamExpedinte"]);
                        Session["TamExpedinte"] = total;
                    }
                    else
                    {
                        Session["TamExpedinte"] = total;
                    }
                    LabRespuestaArchivosCarga.Text = "El archivo registrados " + ArchivosRegistrados + "<BR> Rechazados " + ArchivosNoRegistrados + "<BR> Tamaño total " + total + " MG";
                }
            }
            else
            {
                LabRespuestaArchivosCarga.Text = "No a cargado ningun tipo de archivo.";
            }
            MuestraInfoExpediente();
        }

        protected void MuestraDocumentos()
        {
            /* LISTA LOS ARCHIVOS DEL DOCUMENTO */
            List<wfiplib.expediente> LstArchivosDocumento = new List<wfiplib.expediente>();
            /* COMPRUEBA LA LISTA APÁRTIR DE LA SESION */
            if (Session["documentos"] != null)
            {
                LstArchivosDocumento = (List<wfiplib.expediente>)Session["documentos"];
            }

            lstDocumentos.DataSource = LstArchivosDocumento;
            lstDocumentos.DataValueField = "Id";
            lstDocumentos.DataTextField = "NmOriginal";
            lstDocumentos.DataBind();
        }

        protected void btnEliminaDocumento_Click(object sender, EventArgs e)
        {
            if (lstDocumentos.Items.Count > 0 && lstDocumentos.SelectedIndex > -1)
            {
                List<wfiplib.expediente> LstArchExpediente = new List<wfiplib.expediente>();
                List<wfiplib.expediente> LstArchExpedienteTmp = new List<wfiplib.expediente>();
                if (Session["documentos"] != null) { LstArchExpediente = (List<wfiplib.expediente>)Session["documentos"]; }
                int contador = 0;
                int TamArchRegistrados = 0;

                foreach (wfiplib.expediente oArchivo in LstArchExpediente)
                {
                    if (contador != lstDocumentos.SelectedIndex)
                    {
                        LstArchExpedienteTmp.Add(oArchivo);
                    }
                    else
                    {
                        // SUMA DE LOS ARCHIVOS NO ELIMINADO
                        TamArchRegistrados = TamArchRegistrados + oArchivo.Tam;
                    }

                    contador += 1;
                }
                // TAMAÑO MAXIMO DE SUBIDA VALOR DEL 1 MEGABIY A BITS
                int mega = 1048576;
                int max = Convert.ToInt32(hfArchivos.Value);
                int total = TamArchRegistrados / mega;
                if (Session["TamExpedinte"] != null)
                {
                    total = Convert.ToInt32(Session["TamExpedinte"]) - total;
                    Session["TamExpedinte"] = total;
                }

                int restante = max - total;
                LabRespuestaArchivosCarga.Text = "Archivo elimindado.";

                lstDocumentos.DataSource = LstArchExpedienteTmp;
                lstDocumentos.DataValueField = "Id";
                lstDocumentos.DataTextField = "NmOriginal";
                lstDocumentos.DataBind();
                Session["documentos"] = LstArchExpedienteTmp;


                string script = "";
                script = "EliminaExpediente(" + restante + "); ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            MuestraInfoExpediente();
        }
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("documentos");
            Session.Remove("insumos");
            Session.Remove("AnexoArchivos");
            Session.Remove("tramite");
            Response.Redirect("esperaPromotoria.aspx");
        }
        private bool ValidaExpediente()
        {
            bool respuesta = false;
            List<wfiplib.expediente> LstExpediente = new List<wfiplib.expediente>();

            if (Session["documentos"] != null)
                LstExpediente = (List<wfiplib.expediente>)Session["documentos"];

            if (LstExpediente.Count > 0)
            {
                respuesta = true;
            }
            else
            {
                string script = "";
                script = "ExpedinteIncompleto(); ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            return respuesta;
        }
        protected void NombreAgente()
        {
            wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
            //lbNombreAgente.Text = "NO EXISTE";
            Mensajes.Text = "";
            lbNombreAgente.Text = "";
            lbEmailAgente.Text = "";
            lbEmailAlternoAgente.Text = "";
            if (!string.IsNullOrEmpty(oTramite.IdPromotoria.ToString()) && !string.IsNullOrEmpty(txIdAgente.Text.ToString()))
            {
                DataTable data = (new wfiplib.admAgentesPromotoria()).AgentesPromotoria_Selecionar(oTramite.IdPromotoria, txIdAgente.Text.ToString());
                if (data.Rows.Count>0)
                {
                    foreach (DataRow agente in data.Rows)
                    {
                        lbNombreAgente.Text = agente["DESCRIPCION"].ToString();
                        lbEmailAgente.Text = agente["EMAIL"].ToString();
                        lbEmailAlternoAgente.Text = agente["EMAIL_ALTERNO"].ToString();
                    }
                }
                else
                {
                    Mensajes.Text = "Agente No Encotrado";
                }
            }
            else
            {
                Mensajes.Text = "Coloca la clave del Agente";
            }
        }
        private void pintaCabeceraHtml()
        {
            if (Session["tramite"] != null)
            {
                wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];

                string CabeceraHtml = "";
                switch (oTramite.IdTipoTramite)
                {
                    // VIDA
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                        CabeceraHtml = ((wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()]).CabeceraHtml;
                        break;
                    // VIDA CON CITA MEDICA 
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        CabeceraHtml = ((wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()]).CabeceraHtml;
                        break;
                    // GASTOS MEDICOS
                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        CabeceraHtml = ((wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()]).CabeceraHtml;
                        break;
                    // CONVERSIONES
                    case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                        CabeceraHtml = ((wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionConversiones.ToString()]).CabeceraHtml;
                        break;

                }
                ltInfContratante.Text = CabeceraHtml;
            }
        }
        private void pintRegreso()
        {
            if (Session["URL"] != null)
            {
                Regresar.Visible = true;
            }
        }
        private void GuardaDatos()
        {
            wfiplib.AnexoArchivos oAnexoArchivos = new wfiplib.AnexoArchivos();
            //oAnexoArchivos.AgenteClave = txIdAgente.Text.ToString();
            //if (CheckBox1.Checked == true)
            //{
            //    oAnexoArchivos.CheckInsumos = true;
            //}
            //oAnexoArchivos.IdTipoTramite = TextIdTipoTramite.Text.ToString();
            //oAnexoArchivos.TipoPersona = TextTipoPersona.Text.ToString();

            //for (int i = 0; i < DocRequerid.Items.Count; i++)
            //{
            //    if (DocRequerid.Items[i].Selected)
            //    {
            //        oAnexoArchivos.CheckDocumento.Add(DocRequerid.Items[i].Text.ToString());
            //    }
            //    else
            //    {
            //        oAnexoArchivos.CheckDocumento.Add("");
            //    }
            //}
            Session["AnexoArchivos"] = oAnexoArchivos;
        }
        private void pintaChecks()
        {
            if (Session["tramite"] != null)
            {
                wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                //TextIdTipoTramite.Text = oTramite.IdTipoTramite.ToString();

                switch ((wfiplib.E_TipoTramite)Convert.ToInt32((oTramite.IdTipoTramite)))
                {
                    // VIDA
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        wfiplib.EmisionVG oEmisionVidaCM = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                        DataTable tableVidaCM = new wfiplib.admEmisionVG().Checks2(oEmisionVidaCM.TipoPersona.ToString("d"), wfiplib.E_TipoTramite.indPriEmisionVida);
                        Checks(tableVidaCM);
                        // TextTipoPersona.Text = oEmisionVidaCM.TipoPersona.ToString("d");
                        break;
                    // GMM
                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()];
                        DataTable tableGMM = new wfiplib.admEmisionVG().Checks2(oEmisionGmm.TipoPersona.ToString("d"), wfiplib.E_TipoTramite.indPriEmisionGMM);
                        Checks(tableGMM);
                        //TextTipoPersona.Text = oEmisionGmm.TipoPersona.ToString("d");
                        break;
                    // CONVERSIONES
                    case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                        wfiplib.EmisionVG oEmisionConversiones = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionConversiones.ToString()];
                        DataTable tableConversiones = new wfiplib.admEmisionVG().Checks2(oEmisionConversiones.TipoPersona.ToString("d"), wfiplib.E_TipoTramite.indPriEmisionConversiones);
                        Checks(tableConversiones);
                        break;
                }
            }
        }
        private void Checks2(DataTable table)
        {
            DocRequerid.DataSource = table;
            DocRequerid.ID = "IdDocRecEmicion";
            DocRequerid.DataTextField = "Documentos";
            DocRequerid.DataValueField = "IdDocRecEmicion";
            DocRequerid.DataBind();
        }
        private void Checks(DataTable table)
        {
            DocRequerid.DataSource = table;
            DocRequerid.ID = "IdDocRecEmicion";
            DocRequerid.DataTextField = "Documentos";
            DocRequerid.DataValueField = "IdDocRecEmicion";
            DocRequerid.DataBind();
        }
    }
}