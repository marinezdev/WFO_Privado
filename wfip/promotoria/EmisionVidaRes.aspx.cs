﻿using DevExpress.Web;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using RFC;
using System.Text;
using wfiplib;
using System.Text.RegularExpressions;

namespace wfip.promotoria
{
    public partial class EmisionVidaRes : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        admEntidadFederativa edos = new admEntidadFederativa();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarRamos();
                cargarMonedas();
                formatoFechas();
                cargarNacionalidadesCombo_db(ref txNacionalidad);
                cargarNacionalidadesCombo_db(ref txTiNacionalidad);

                edos.SeleccionarDependencias_DropDrownList(ref cboEstado);
                edos.SeleccionarDependencias_DropDrownList(ref cboEstado2);

                identificaPromotoria(manejo_sesion.Credencial.IdPromotoria);
               
                // CAMPOS INAVILITADOS 
                texClave.Attributes["disabled"] = "disabled";
                texRegion.Attributes["disabled"] = "disabled";
                texGerenteComercial.Attributes["disabled"] = "disabled";
                texEjecuticoComercial.Attributes["disabled"] = "disabled";
                texEjecuticoFront.Attributes["disabled"] = "disabled";

                if (Session["tramite"] != null)
                {
                    /************** RECONOCE APARTIR DE E_TIPOTRAMITE ****************/
                    pintLimpiar();
                    wfiplib.tramiteP oTramite = (wfiplib.tramiteP)Session["tramite"];
                    switch ((wfiplib.E_TipoTramite)Convert.ToInt32((oTramite.IdTipoTramite)))
                    {
                        case wfiplib.E_TipoTramite.indPriEmisionVida:
                            wfiplib.EmisionVG oEmisionVida = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()];
                            PostBack(oEmisionVida, wfiplib.E_TipoTramite.indPriEmisionVida.ToString("d"));
                            break;
                        case wfiplib.E_TipoTramite.indPriEmisionGMM:
                            wfiplib.EmisionVG oEmisionGmm = (wfiplib.EmisionVG)Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()];
                            PostBack(oEmisionGmm, wfiplib.E_TipoTramite.indPriEmisionGMM.ToString("d"));
                            break;
                    }
                }
                else
                {
                    Session.Remove("tramite");
                }
            }

            string _promotoriaPrueba = Properties.Settings.Default.promotoriaPrueba.ToString();
            string[] promotorias = _promotoriaPrueba.Split(',');
            var _resultPromotoria = Array.Find(promotorias, element => element == manejo_sesion.Credencial.Id.ToString()); // returns "Bill"
            if (_resultPromotoria != null)
            {
                if (manejo_sesion.Credencial.Id.ToString() == _resultPromotoria.ToString())
                {
                    BtnContinuar.Enabled = false;
                }
            }
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            double SumaAseguradaBasica = double.Parse(txtSumaAseguradaBasica.Text.ToString());
            int IdMoneda = int.Parse(cboMoneda.Text.ToString());
            Double SumaAseguradaBasicaConvertida = convertir(SumaAseguradaBasica, IdMoneda);

            SumaBasica.Text = "";
            if (SumaAseguradaBasicaConvertida < 10000.00)
            {
                SumaBasica.Text = "la suma asegurada no es mayor a 10,000.00 Pesos";
            }
            else
            {
                if (ValidantinuidadRFC())
                {
                    string script = "";
                    script = "$('#myModal').modal({backdrop: 'static', keyboard: false});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                    //Continuar();
                }
            }
        }
        protected void LisTitNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textNacionalidad.Text = "jajaja";
            textTitularNacionalidad.Text = "";
            textTitularNacionalidad.Text = validaPais(txTiNacionalidad.Text.Trim());
        }
        protected void dtFechaNacimiento_OnChanged(object sender, EventArgs e)
        {
            string strValMsj = "";
            DateTime dtValor = DateTime.Today;
            if (dtFechaNacimiento.Text.Length > 0)
            {
                if (IsDate(dtFechaNacimiento.Text.Trim(), ref strValMsj, ref dtValor))
                {
                    try
                    {
                        string strNombre = "";
                        strNombre = removerAcentos(txNombre.Text.ToUpper().Trim());

                        if (strNombre.Equals("MARIA") || strNombre.Equals("JOSE"))
                        {
                            strNombre += "A";
                        }

                        ObtieneRFC rfc = new ObtieneRFC();
                        txRfc.Text = rfc.RFC13Pocisiones(removerAcentos(txApPat.Text.ToUpper().Trim()), removerAcentos(txApMat.Text.ToUpper().Trim()), removerAcentos(strNombre), dtValor.ToString("yy/MM/dd"));
                        antecedentesRFC();
                    }
                    catch
                    {
                        txRfc.Text = "ERR000000AAA".ToUpper();
                    }
                }
                else
                {
                    dtFechaNacimiento.Text = "";
                    dtFechaNacimiento.Focus();
                    txRfc.Text = "";
                }
            }
        }
        protected void dtFechaConstitucion_OnChanged(object sender, EventArgs e)
        {
            string strValMsj = "";
            DateTime dtValor = DateTime.Today;
            if (dtFechaConstitucion.Text.Length > 0)
            {
                if (IsDate(dtFechaConstitucion.Text.Trim(), ref strValMsj, ref dtValor))
                {
                    try
                    {
                        string strMoral = removerAcentos(txNomMoral.Text.ToUpper().Trim());
                        String[] arrPalabrasNo = { " EL ", " S DE RL ", " DE ", " LAS ", " DEL ", " COMPAÑÍA ", " SOCIEDAD ", " COOPERATIVA ", " S EN C POR A ", " S EN NC ", " PARA ", " POR ", " AL ", " E ", " SCL ", " SNC ", " OF ", " COMPANY ", " MC ", " VON ", " MI ", " SRL CV ", " SA MI ", " LA ", " SA DE CV ", " LOS ", " Y ", " SA ", " CIA ", " SOC ", " COOP ", " A EN P ", " S EN C ", " EN ", " CON ", " SUS ", " SC ", " SCS ", " THE ", " AND ", " CO ", " MAC ", " VAN ", " A ", " SA DE CV ", " COMPAÑÍA ", " COMPANÍA ", " DE ", " LA ", " LAS ", " MC ", " VON ", " DEL ", " LOS ", " Y ", " MAC ", " VAN ", " MI ", " SRL CV MI ", " SRL MI" };
                        foreach (string strPalabra in arrPalabrasNo)
                        {
                            strMoral = strMoral.Replace(strPalabra, " ");
                        }

                        String[] arrPalabras = strMoral.Split(' ');
                        if (arrPalabras.Length > 3)
                        {
                            strMoral = "";
                            strMoral += arrPalabras[0].ToString() + " ";
                            strMoral += arrPalabras[1].ToString() + " ";
                            strMoral += arrPalabras[2].ToString() + " ";
                        }

                        PersonaMoral moral = new PersonaMoral();
                        txRfcMoral.Text = moral.RetornaLetrasFinalesRFC(strMoral, dtValor.ToString("yy/MM/dd"));
                        antecedentesRFC();
                    }
                    catch
                    {
                        txRfcMoral.Text = "ERR000000AAA".ToUpper();
                    }
                }
                else
                {
                    dtFechaConstitucion.Text = "";
                    dtFechaConstitucion.Focus();
                    txRfcMoral.Text = "";
                }
            }
        }

        protected void cboMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GrandesSumas())
            {
                GrandeSumas.Text = "Grandes sumas";
                PrimaTotalGrandeSumas.Text = "";
            }
            else if (GrandesSumasPrimaTotal())
            {
                GrandeSumas.Text = "Grandes sumas";
                PrimaTotalGrandeSumas.Text = "";
            }
            else
            {
                GrandeSumas.Text = "";
                PrimaTotalGrandeSumas.Text = "";
            }
        }

        protected void TramiteTipPoliza_SelectedIndexChanged(object sender, EventArgs e)
        {
            TramiteTipoPoliza();
            // COLOCA TABULADOR EN PRIMERO LUGAR
            TramiteTipPoliza.Focus();
        }
        protected void ActividadCPDES_SelectedIndexChanged(object sender, EventArgs e)
        {
            CPDES();
        }
        protected void LisProducto1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LisSbproductos();
        }
        protected void LisSubproducto1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidaProductoSubProducto();
        }

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoContratante();
        }
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckB2();
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckB1();
        }
        protected void LisNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            textNacionalidad.Text = "";
            textNacionalidad.Text = validaPais(txNacionalidad.Text.Trim());
        }
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("esperaPromotoria.aspx");
        }
        protected void Continuar(object sender, EventArgs e)
        {
            // REALIZA LA RECOPILACION DE DATOS
            wfiplib.EmisionVG oDatos = recuperaCaptura();
            // ES CREADA LA SESION Session["tramite"] 
            armaTramiteYGuardaEnMemoria(oDatos.DatosHtml);

            // DATOS EN VARIABLE DE SESION EN CREAIOCN DEL TRAMITE
            switch ((wfiplib.E_TipoTramite)Convert.ToInt32(TramiteTipPoliza.SelectedValue))
            {
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    Session[wfiplib.E_TipoTramite.indPriEmisionVida.ToString()] = oDatos;
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    Session[wfiplib.E_TipoTramite.indPriEmisionGMM.ToString()] = oDatos;
                    break;

                default:
                    break;
            }
            Response.Redirect("anexaArchivosRes.aspx");

        }
        protected void BtnLimpiar(object sender, EventArgs e)
        {
            Session.Remove("tramite");
            Session.Remove("TamExpedinte");
            Session.Remove("documentos");

            string url = Session["URL"].ToString();
            Response.Redirect(url);
        }


        #region OPERACIONES DE VALIDACION
        private void ValidaProductoSubProducto()
        {
            // VALORES SELCCIONADOS
            string Producto = "";
            string SubProducto = "";

            Producto = LisProducto1.SelectedItem.Text.Trim();
            SubProducto = LisSubproducto1.SelectedItem.Text.Trim();

            if (Producto == "METALIFE")
            {
                if (SubProducto == "RETIRO")
                {
                    // DESABILIAR FORMULARIO 
                    cboTipoContratante.SelectedValue = "Fisica";
                    TipoContratante();
                    cboTipoContratante.Attributes["disabled"] = "enabled";
                    CheckBox2.Checked = true;
                    CheckBox1.Checked = false;
                    CheckB2();
                    CheckBox2.Enabled = false;
                    CheckBox1.Enabled = false;
                }
                else
                {
                    // HABILITAR FORMULARIO
                    TipoContratante();
                    cboTipoContratante.Attributes.Remove("disabled");
                    CheckBox2.Enabled = true;
                    CheckBox1.Enabled = true;
                }
            }
            else
            {
                // HABILITAR FORMULARIO
                TipoContratante();
                cboTipoContratante.Attributes.Remove("disabled");
                CheckBox2.Enabled = true;
                CheckBox1.Enabled = true;
            }
        }
        private void PostBack(EmisionVG oEmisionVidaGMM, string TipoTramite)
        {
            /***********************************************************************/
            /*****************   PRIMER BLOQUE - POLIZA / SEGURO   *****************/
            /***********************************************************************/

            cboMoneda.SelectedValue = oEmisionVidaGMM.IdMoneda.ToString();
            txtSumaAseguradaBasica.Text = oEmisionVidaGMM.SumaAsegurada.ToString();
            //txtPrimaTotal.Text = oEmisionVidaGMM.SumaPolizas.ToString();
            if (GrandesSumas())
            {
                PrimaTotalGrandeSumas.Text = "";
                GrandeSumas.Text = "Grandes sumas";
            }
            TramiteTipPoliza.SelectedValue = TipoTramite;
            TramiteTipoPoliza();

            switch ((wfiplib.E_TipoTramite)Convert.ToInt32(TramiteTipPoliza.SelectedValue))
            {
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    txtSumaAseguradaPolizasVigentes.Text = oEmisionVidaGMM.SumaPolizas.ToString();
                    txtPrimaTotal.Text = oEmisionVidaGMM.PrimaTotal.ToString();
                    //resultado.SumaPolizas = txtSumaAseguradaPolizasVigentes.Text.ToString();
                    //resultado.PrimaTotal = txtPrimaTotal.Text.ToString();
                    if (GrandesSumasPrimaTotal())
                    {
                        GrandeSumas.Text = "";
                        PrimaTotalGrandeSumas.Text = "Grandes sumas";
                    }
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    txtPrimaTotalGMM.Text = oEmisionVidaGMM.PrimaTotal.ToString();
                    //resultado.PrimaTotal = txtPrimaTotalGMM.Text.ToString();
                    break;

                default:
                    break;
            }



            LisProducto1.SelectedValue = oEmisionVidaGMM.Producto1.ToString();
            LisSbproductos();
            LisSubproducto1.SelectedValue = oEmisionVidaGMM.Plan1.ToString();
            ActividadCPDES.SelectedValue = oEmisionVidaGMM.CPDES.ToString();
            CPDES();
            textFolioCPDES.Text = oEmisionVidaGMM.FolioCPDES.ToString();
            EstatusCPDES.SelectedValue = oEmisionVidaGMM.EstatusCPDES.ToString();

            HombresClave.Checked = oEmisionVidaGMM.HombreClave;

            chkOneShot.Checked = oEmisionVidaGMM.OneShot;

            /***********************************************************************/
            /********************   INFORMACION DE LA POLIZA    ********************/
            /***********************************************************************/

            dtFechaSolicitud.Text = oEmisionVidaGMM.FechaSolicitud.ToString();
            textNumeroOrden.Text = oEmisionVidaGMM.NumeroOrden.ToString();

            cboTipoContratante.SelectedValue = oEmisionVidaGMM.TipoPersona.ToString();
            TipoContratante();
            ValidaProductoSubProducto();
            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                txNombre.Text = oEmisionVidaGMM.Nombre.ToString();
                txApPat.Text = oEmisionVidaGMM.ApPaterno.ToString();
                txApMat.Text = oEmisionVidaGMM.ApMaterno.ToString();
                txNacionalidad.Value = oEmisionVidaGMM.Nacionalidad.ToString();
                dtFechaNacimiento.Text = oEmisionVidaGMM.FechaNacimiento.ToString();
                txSexo.SelectedValue = oEmisionVidaGMM.Sexo.ToString();
                txRfc.Text = oEmisionVidaGMM.RFC.ToString();
                cboEstado.SelectedValue = oEmisionVidaGMM.EntidadFederativa.ToString();
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                txNomMoral.Text = oEmisionVidaGMM.Nombre.ToString();
                dtFechaConstitucion.Text = oEmisionVidaGMM.FechaConst.ToString();
                txRfcMoral.Text = oEmisionVidaGMM.RFC.ToString();
                txTiNacionalidad.Value = oEmisionVidaGMM.Nacionalidad.ToString();
            }

            if (oEmisionVidaGMM.TitularNombre.ToString() != "")
            {
                CheckBox1.Checked = true;
                CheckB1();
                txTiNombre.Text = oEmisionVidaGMM.TitularNombre.ToString();
                txTiApPat.Text = oEmisionVidaGMM.TitularApPat.ToString();
                txTiApMat.Text = oEmisionVidaGMM.TitularApMat.ToString();
                txTiNacionalidad.Value = oEmisionVidaGMM.TitularNacionalidad.ToString();
                txtSexoM.SelectedValue = oEmisionVidaGMM.TitularSexo.ToString();
                dtFechaNacimientoTitular.Text = oEmisionVidaGMM.TitularFechaNacimiento.ToString();
                cboEstado2.SelectedValue = oEmisionVidaGMM.TitularEntidad.ToString();
            }

            txDetalle.Text = oEmisionVidaGMM.Detalle.ToString();
            antecedentesRFC();

            textNacionalidad.Text = "";
            textNacionalidad.Text = validaPais(txNacionalidad.Text.Trim());
            textTitularNacionalidad.Text = "";
            textTitularNacionalidad.Text = validaPais(txTiNacionalidad.Text.Trim());

        }
        private void pintLimpiar()
        {
            if (Session["URL"] != null)
            {
                Limpiar.Visible = true;
            }
        }
        private void armaTramiteYGuardaEnMemoria(string DatosHtml)
        {
            wfiplib.tramiteP oTramite = new wfiplib.tramiteP();

            if (chkMetaLifeEspecial.Checked.Equals(true))
            { 
                oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.MetalifeEspecial);
            }
            else
            {
                if (HombresClave.Checked.Equals(true))
                    oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.HombreClave);
                else
                    oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.Tramite);
            }

            

            switch ((wfiplib.E_TipoTramite)Convert.ToInt32((TramiteTipPoliza.SelectedValue)))
            {
                // VIDA
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    oTramite.IdTipoTramite = wfiplib.E_TipoTramite.indPriEmisionVida;
                    
                    // PRIORIDAD DE TRAMITE
                    if (GrandesSumas())
                    {
                        oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.GrandesSumas);
                    }
                    else if (GrandesSumasPrimaTotal())
                    {
                        oTramite.Prioridad = Convert.ToInt32(wfiplib.E_PrioridadTramite.GrandesSumasPrimas);
                    }

                    break;

                // GMM
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    oTramite.IdTipoTramite = wfiplib.E_TipoTramite.indPriEmisionGMM;
                    break;

                // Defaul
                default:
                    break;
            }

            //TODO: ###Pendiente: Prioridad de Tramites   5=>Normal, 4=>Alta, 3=>VIP
            oTramite.IdRamo = Convert.ToInt32(TramiteTipPoliza.SelectedValue);

            oTramite.IdPromotoria = (new wfiplib.admCredencial()).daPromotoria(manejo_sesion.Credencial.Id);
            oTramite.IdUsuario = manejo_sesion.Credencial.Id;
            oTramite.DatosHtml = DatosHtml;
            oTramite.NumeroOrden = textNumeroOrden.Text.Trim();
            oTramite.FechaSolicitud = dtFechaSolicitud.Text;
            oTramite.TipoTramite = Label2.Text.ToString();

            Session["tramite"] = oTramite;

            string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] Separado = cadena.Split('/');
            string Final = Separado[Separado.Length - 1];
            Session["URL"] = Final;
        }
        private wfiplib.EmisionVG recuperaCaptura()
        {
            // SE CREO LA CLASE serviciosVidaP para actualizar los nuevos datos
            wfiplib.EmisionVG resultado = new wfiplib.EmisionVG();
            try
            {
                if (ActividadCPDES.SelectedValue.Equals("True"))
                {
                    resultado.CPDES = true;
                    resultado.FolioCPDES = textFolioCPDES.Text.Trim();
                    resultado.EstatusCPDES = EstatusCPDES.Text.Trim();
                }

                if (HombresClave.Checked.Equals(true))
                    resultado.HombreClave = true;
                else
                    resultado.HombreClave = false;

                if (chkOneShot.Checked.Equals(true))
                    resultado.OneShot = true;
                else
                    resultado.OneShot = false;

                if (chkMetaLifeEspecial.Checked.Equals(true))
                    resultado.MetaLifeEspecial = true;
                else
                    resultado.MetaLifeEspecial = false;

                if (cboTipoContratante.SelectedValue.Equals("Fisica"))
                {
                    resultado.TipoPersona = wfiplib.E_TipoPersona.Fisica;
                    resultado.Nombre = txNombre.Text.Trim();
                    resultado.ApPaterno = txApPat.Text.Trim();
                    resultado.ApMaterno = txApMat.Text.Trim();
                    resultado.RFC = txRfc.Text.Trim().ToUpper();
                    resultado.Nacionalidad = txNacionalidad.Text.Trim();
                    resultado.FechaNacimiento = dtFechaNacimiento.Text.Trim();
                    resultado.Sexo = txSexo.Text.Trim();
                    resultado.EntidadFederativa = cboEstado.SelectedValue;
                }
                else if (cboTipoContratante.SelectedValue.Equals("Moral"))
                {
                    resultado.TipoPersona = wfiplib.E_TipoPersona.Moral;
                    resultado.Nombre = txNomMoral.Text.Trim();
                    resultado.FechaConst = dtFechaConstitucion.Text.Trim();
                    resultado.RFC = txRfcMoral.Text.Trim().ToUpper();
                    resultado.Nacionalidad = txTiNacionalidad.Text.Trim();
                }

                if (CheckBox1.Checked.Equals(true))
                {
                    resultado.TitularNombre = txTiNombre.Text.Trim();
                    resultado.TitularApPat = txTiApPat.Text.Trim();
                    resultado.TitularApMat = txTiApMat.Text.Trim();
                    resultado.TitularNacionalidad = txTiNacionalidad.Text.Trim();
                    resultado.TitularFechaNacimiento = dtFechaNacimientoTitular.Text.Trim();
                    resultado.TitularSexo = txtSexoM.Text.Trim();
                    resultado.TitularEntidad = cboEstado2.SelectedValue;
                }
               

                resultado.Producto1 = LisProducto1.Text.Trim();
                resultado.Plan1 = LisSubproducto1.Text.Trim();
                resultado.Producto2 = LisProducto2.Text.Trim();
                resultado.Plan2 = LisSubproducto2.Text.Trim();
                resultado.Detalle = txDetalle.Text.Trim();

                // NUEVOS DATOS DE AGREGACION
                resultado.NumeroOrden = textNumeroOrden.Text.Trim().ToUpper();
                resultado.FechaSolicitud = dtFechaSolicitud.Text;
                resultado.SumaAsegurada = txtSumaAseguradaBasica.Text.Trim();

                switch ((wfiplib.E_TipoTramite)Convert.ToInt32(TramiteTipPoliza.SelectedValue))
                {
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                        resultado.SumaPolizas = txtSumaAseguradaPolizasVigentes.Text.ToString();
                        resultado.PrimaTotal = txtPrimaTotal.Text.ToString();
                        break;

                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        resultado.PrimaTotal = txtPrimaTotalGMM.Text.ToString();
                        break;

                    default:
                        break;
                }
                resultado.IdMoneda = cboMoneda.Text.Trim();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }
        protected bool ValidantinuidadRFC()
        {
            bool resultado = false;
            Mensajes.Text = "";

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                string FiscoRFC = txRfc.Text.ToString().Trim();
                if (FiscoRFC != "" && FiscoRFC != null)
                {
                    if (FiscoRFC.Length == 13)
                    {
                        Regex Val = new Regex(@"[A-Z,Ñ,&amp;]{4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?");
                        if (Val.IsMatch(FiscoRFC))
                        {
                            resultado = true;
                        }
                        else
                        {
                            Mensajes.Text = "RFC Persona Física Inválido ";
                        }
                    }
                    else
                    {
                        Mensajes.Text = "El RFC No Contiene 13 Caracteres ";
                    }
                }
                else
                {
                    Mensajes.Text = "Coloca el RFC de la Persona Física";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                string MoralRFC = txRfcMoral.Text.ToString().Trim();
                if (MoralRFC != "" && MoralRFC != null)
                {
                    if (MoralRFC.Length == 12)
                    {
                        Regex Val = new Regex(@"^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$");
                        if (Val.IsMatch(MoralRFC))
                        {
                            resultado = true;
                        }
                        else
                        {
                            Mensajes.Text = "RFC Persona Moral Inválido ";
                        }
                    }
                    else
                    {
                        Mensajes.Text = "El RFC No Contiene 12 Caracteres ";
                    }
                }
                else
                {
                    Mensajes.Text = "Coloca el RFC Moral ";
                }
            }
            return resultado;
        }
        private void identificaPromotoria(int IdPromotoria)
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria) //1 Promotoría
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    wfiplib.comercialPromotoria comercial = (new wfiplib.admAgentesPromotoria()).ConsultaInformacionPromotoria(IdPromotoria);
                    texClave.Text = comercial.Clave;
                    texRegion.Text = comercial.ClaveRegion + " - " + comercial.Region;
                    texGerenteComercial.Text = comercial.ClaveGerente + " - " + comercial.Gerente;
                    texEjecuticoComercial.Text = comercial.ClaveEjecutivo + " - " + comercial.Ejecutivo;
                    texEjecuticoFront.Text = comercial.ClaveFront + " - " + comercial.Front;
                }
            }
        }
        private static string removerAcentos(String texto)
        {
            string consignos = "áàäéèëíìïóòöúùuÁÀÄÉÈËÍÌÏÓÒÖÚÙÜçÇñÑ";
            string sinsignos = "aaaeeeiiiooouuuAAAEEEIIIOOOUUUcCnN";

            StringBuilder textoSinAcentos = new StringBuilder(texto.Length);
            int indexConAcento;
            foreach (char caracter in texto)
            {
                indexConAcento = consignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos.Append(sinsignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos.Append(caracter);
            }
            return textoSinAcentos.ToString();
        }
        protected void CheckB2()
        {
            if (CheckBox2.Checked.Equals(true))
            {
                CheckBox1.Checked = false;
                CheckB1();
            }
        }
        protected void CheckB1()
        {
            if (CheckBox1.Checked.Equals(true))
            {
                CheckBox2.Checked = false;
                DiferenteContratante.Visible = true;
            }
            else if (CheckBox1.Checked.Equals(false))
            {
                DiferenteContratante.Visible = false;
            }
            else
            {
                DiferenteContratante.Visible = false;
            }
        }
        protected void TipoContratante()
        {
            //////////////// GrandeSumas.Text = "Grandes sumas";
            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
                CheckBox1.Enabled = true;
                CheckBox2.Enabled = true;
                // edos.SeleccionarDependencias_DropDrownList(ref cboEstado);
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                pnPrsMoral.Visible = true;
                pnPrsFisica.Visible = false;
                CheckBox1.Checked = true;
                CheckB1();
                CheckBox1.Enabled = false;
                CheckBox2.Enabled = false;
                // edos.SeleccionarDependencias_DropDrownList(ref cboEstado2);
            }
            else
            {
                pnPrsFisica.Visible = false;
                pnPrsMoral.Visible = false;
            }

        }
        private void TramiteTipoPoliza()
        {
            Tramite.Visible = false;
            subproducto1.Visible = false;
            subproducto2.Visible = false;
            ActCPDES.Visible = false;
            CPDS.Visible = false;
            ActividadCPDES.SelectedIndex = -1;
            EstatusCPDES.SelectedIndex = -1;

            switch ((wfiplib.E_TipoTramite)Convert.ToInt32(TramiteTipPoliza.SelectedValue))
            {
                // PRIVADO INDIVIDUAL VIDA
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    lblProductoRamo.Text = "* Producto";
                    lblSubProductoRamo.Text = "* Sub Producto";
                    Tramite.Visible = true;
                    ActCPDES.Visible = true;
                    subproducto1.Visible = true;
                    subproducto2.Visible = false;
                    LblOneShot.Visible = true;
                    chkOneShot.Visible = true;
                    ListaProductos();
                    LisSbproductos();

                    SumaAseguradaPolizasVigentes.Visible = true;
                    SumaAseguradaPolizasVigentesGMM.Visible = false;
                    cboMoneda.Attributes.Remove("disabled");
                    cargarMonedas();
                    break;

                // PRIVADO INDIVIDUAL GMM
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    lblProductoRamo.Text = "* Producto";
                    lblSubProductoRamo.Text = "* Plan";
                    Tramite.Visible = true;
                    ActCPDES.Visible = false;
                    subproducto1.Visible = true;
                    subproducto2.Visible = false;
                    LblOneShot.Visible = false;
                    chkOneShot.Visible = false;
                    ListaProductos();
                    LisSbproductos();

                    SumaAseguradaPolizasVigentes.Visible = false;
                    SumaAseguradaPolizasVigentesGMM.Visible = true;
                    cboMoneda.SelectedValue = "5";
                    //cboMoneda.Enabled = false;
                    cboMoneda.Attributes["disabled"] = "disabled";
                    break;

                // DEFAULT
                default:
                    lblProductoRamo.Text = "NA";
                    lblSubProductoRamo.Text = "NA";
                    Tramite.Visible = false;
                    ActCPDES.Visible = false;
                    subproducto1.Visible = false;
                    subproducto2.Visible = false;

                    // Nuevo requerimeinto de validacion 
                    SumaAseguradaPolizasVigentesGMM.Visible = false;
                    SumaAseguradaPolizasVigentes.Visible = false;
                    break;
            }
        }
        protected void PrimaTotalGrandesSumas(object sender, EventArgs e)
        {
            if (GrandesSumasPrimaTotal())
            {
                PrimaTotalGrandeSumas.Text = "Grandes sumas";
                GrandeSumas.Text = "";
            }
        }
        protected void CalculartSumaAsegurada(object sender, EventArgs e)
        {
            if (GrandesSumas())
            {
                GrandeSumas.Text = "Grandes sumas";
                PrimaTotalGrandeSumas.Text = "";
            }
            else if (GrandesSumasPrimaTotal())
            {
                GrandeSumas.Text = "Grandes sumas";
                PrimaTotalGrandeSumas.Text = "";
            }
            else
            {
                GrandeSumas.Text = "";
                PrimaTotalGrandeSumas.Text = "";
            }
        }
        protected bool GrandesSumas()
        {
            bool resultado = false;
            // Limpia los mensajes 
            GrandeSumas.Text = "";
            // Compara el valor de la seleccion
            if (cboMoneda.SelectedValue != "-1" && txtSumaAseguradaBasica.Text.ToString() != "0.00")
            {
                // Monto total de la suma aseguada y prima total, apartir del tipo de moneda seleccionada
                Double MontoTotal = Total(txtSumaAseguradaBasica.Text.ToString(), txtSumaAseguradaPolizasVigentes.Text.ToString(), cboMoneda.Text.ToString());
                // Validacion del monto "MAL PROGRAMADO" el id del dolar es = 2, el regitro esta en SQL cat_moneda, 1500000 dolares
                // double ValidacionMonto = double.Parse("1500000");
                
                // RMF (versión 20201013_001)
                //double ValidacionMonto = double.Parse("6000000.00");
                //ValidacionMonto = convertir(ValidacionMonto, 1);

                double ValidacionMonto = double.Parse("1500000.00");
                ValidacionMonto = convertir(ValidacionMonto, 2);
                if (MontoTotal >= ValidacionMonto)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        protected bool GrandesSumasPrimaTotal()
        {
            bool resultado = false;
            PrimaTotalGrandeSumas.Text = "";
            if (cboMoneda.SelectedValue != "-1")
            {
                int IdMoneda = int.Parse(cboMoneda.Text.ToString());
                string Primatotal = txtPrimaTotal.Text.ToString();
                double Monto;
                if (Primatotal != null && Primatotal.Length == 0)
                {
                    Monto = 0.00;
                }
                else
                {
                    Monto = double.Parse(Primatotal);
                }

                Monto = convertir(Monto, IdMoneda);

                double ValidacionMonto = convertir(double.Parse("200000.00"),1);
                if (Monto >= ValidacionMonto)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        protected double Total(String SumaAsegurda, String TotalCotiacion, String Moneda)
        {
            if (TotalCotiacion.Length == 0)
                TotalCotiacion = "0";

            double Total = 0;
            double SumaAsegurada = double.Parse(SumaAsegurda);
            double TotalCotizacion = double.Parse(TotalCotiacion);
            int IdMoneda = int.Parse(Moneda);
            SumaAsegurada = convertir(SumaAsegurada, IdMoneda);
            TotalCotizacion = convertir(TotalCotizacion, IdMoneda);

            Total = SumaAsegurada + TotalCotizacion;

            return Total;
        }
        private double convertir(double numero, int IdMoneda)
        {
            double total = 0;
            DataTable ValorMoneda = (new wfiplib.admEmisionVG()).ValorMoneda(IdMoneda);
            DataRow row = ValorMoneda.Rows[0];
            String valor = row["valor"].ToString();
            Double Moneda = Convert.ToDouble(valor);

            total = numero * Moneda;
            return total;
        }
        protected void CPDES()
        {
            if (ActividadCPDES.SelectedValue.Equals("True"))
            {
                CPDS.Visible = true;
            }
            else if (ActividadCPDES.SelectedValue.Equals("False"))
            {
                CPDS.Visible = false;
            }
            else
            {
                CPDS.Visible = false;
            }
        }
        public static bool IsDate(string inputDate, ref string strResultado, ref DateTime FechaValue)
        {
            bool isDate = true;
            try
            {
                FechaValue = DateTime.ParseExact(inputDate, "dd/MM/yyyy", null);

                if (FechaValue > DateTime.Today)
                {
                    strResultado = "La fecha no puede ser mayor al día de hoy.";
                    isDate = false;
                }
                else if (FechaValue < FechaValue.AddDays(-60))
                {
                    strResultado = "La fecha no puede ser menor a 60 días a partir del día de hoy.";
                    isDate = false;
                }
                else
                {
                    isDate = true;
                }

            }
            catch (Exception ex)
            {
                isDate = false;
                strResultado = ex.Message;
                Console.Write("Error al Validar la Fecha: " + ex.Message);
            }
            return isDate;
        }
        #endregion

        #region CONSULTA A BASE DE DATOS
        private void identificaPromotoria()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria) //1 Promotoría
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    //hf_IdPromotoria.Value = manejo_sesion.Credencial.IdPromotoria.ToString();
                    //texClave.Text = ClavePromotoria(manejo_sesion.Credencial.IdPromotoria.ToString());
                }
            }
        }
        private void cargarRamos()
        {
            DataTable dtRamos = (new wfiplib.admEmisionVG()).cargaRamos("INDIVIDUAL", "PRIVADO");
            TramiteTipPoliza.DataSource = dtRamos;
            TramiteTipPoliza.DataTextField = "Nombre";
            TramiteTipPoliza.DataValueField = "IdRamo";
            TramiteTipPoliza.DataBind();
            TramiteTipPoliza.SelectedIndex = 0;
            TramiteTipPoliza.Focus();

        }
        private void cargarMonedas()
        {
            DataTable dtMoneda = (new wfiplib.admEmisionVG()).cargaMonedas();
            cboMoneda.DataSource = dtMoneda;
            cboMoneda.DataTextField = "Nombre";
            cboMoneda.DataValueField = "IdMoneda";
            cboMoneda.DataBind();
        }
        private void ListaProductos()
        {
            DataTable listProductos = (new wfiplib.admEmisionVG()).cartgaCatProducto(TramiteTipPoliza.SelectedValue.ToString());
            LisProducto1.DataSource = listProductos;
            LisProducto1.DataBind();
            LisProducto1.DataTextField = "Nombre";
            LisProducto1.DataValueField = "IdCatProducto";
            LisProducto1.DataBind();
            //LisProducto1.Focus();

            LisProducto2.DataSource = listProductos;
            LisProducto2.DataBind();
            LisProducto2.DataTextField = "Nombre";
            LisProducto2.DataValueField = "IdCatProducto";
            LisProducto2.DataBind();
        }
        protected void LisSbproductos()
        {
            DataTable listProductos = (new wfiplib.admEmisionVG()).cartgaCatSupProducto(LisProducto1.SelectedValue.ToString());
            LisSubproducto1.DataSource = listProductos;
            LisSubproducto1.Items.Add("Seleccione");
            LisSubproducto1.DataBind();
            LisSubproducto1.DataTextField = "Nombre";
            LisSubproducto1.DataValueField = "IdCatSubProducto";
            LisSubproducto1.DataBind();

            DataTable listProductos2 = (new wfiplib.admEmisionVG()).cartgaCatSupProducto(LisProducto2.SelectedValue.ToString());
            LisSubproducto2.DataSource = listProductos2;
            LisSubproducto2.Items.Add("Seleccione");
            LisSubproducto2.DataBind();
            LisSubproducto2.DataTextField = "Nombre";
            LisSubproducto2.DataValueField = "IdCatSubProducto";
            LisSubproducto2.DataBind();

            //verificamos si es promotoria 770 para activar metalife especial y que el producto sea metalife
            // TODO: hace falta identificar el producto por nombre para que si llegará a cambiar el ID no haya problemas
            if (texClave.Text.ToString() == "770" && LisProducto1.SelectedValue.ToString() == "4")
            {
                lblMetaLifeEspecial.Visible = true;
                chkMetaLifeEspecial.Visible = true;
            }
            else
            {
                lblMetaLifeEspecial.Visible = false;
                chkMetaLifeEspecial.Visible = false;
            }


            //verificamos si es promotoria 770 para activar metalife especial y que el producto sea metalife
            // TODO: hace falta identificar el producto por nombre para que si llegará a cambiar el ID no haya problemas
            if (texClave.Text.ToString() == "770" && LisProducto1.SelectedValue.ToString() == "4")
            {
                // Se deshabilita funcionalidad porque metlife no ha solicitado que se realice la publicación de los cambios
                // hay que revisar la opción de prioridades y la opción de reportes que se genera porque es lo que se veía afectado
                lblMetaLifeEspecial.Visible = false;
                chkMetaLifeEspecial.Visible = false;
            }
            else
            {
                lblMetaLifeEspecial.Visible = false;
                chkMetaLifeEspecial.Visible = false;
            }

        }
        private void cargarNacionalidadesCombo_db(ref ASPxComboBox objDDL)
        {
            DataTable dtPaises = (new wfiplib.admEmisionVG()).cargaPaises();

            objDDL.DataSource = dtPaises;
            objDDL.TextField = "Nombre";
            //objDDL.TextField = "Nombre";
            //ddl_time.DataTextField = "Nombre";
            objDDL.ValueField = "Id";
            objDDL.DataBind();
            objDDL.Value = "MÉXICO";
        }
        private String validaPais(string nombre)
        {
            String respuesta = (new wfiplib.admEmisionVG()).validaPais(nombre);
            //String respuesta = id.ToString();
            return respuesta;
        }
        protected void antecedentesRFC()
        {
            TextantecedentesRFC.Text = "";
            textRFCFisica.Text = "";

            if (cboTipoContratante.SelectedValue.Equals("Fisica"))
            {
                string RFC = txRfc.Text.Trim().Replace("-", "");
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    textRFCFisica.Text = "Ya existen trámites registrados para el RFC";
                }
            }
            else if (cboTipoContratante.SelectedValue.Equals("Moral"))
            {
                string RFC = txRfcMoral.Text.Trim().Replace("-", "");
                bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedenteEmision(RFC);
                if (resultado)
                {
                    TextantecedentesRFC.Text = "Ya existen trámites registrados para el RFC";
                }
            }

        }
        protected void formatoFechas()
        {
            DateTime validateFechaSolicitud = DateTime.Today;

            dtFechaSolicitud.MaxDate = validateFechaSolicitud;
            dtFechaSolicitud.MinDate = validateFechaSolicitud.AddDays(-60);
            dtFechaSolicitud.UseMaskBehavior = true;
            dtFechaSolicitud.EditFormatString = GetFormatString("dd/MM/yyyy");
            dtFechaSolicitud.Date = DateTime.Today;

            dtFechaConstitucion.MaxDate = DateTime.Today;
            dtFechaConstitucion.UseMaskBehavior = true;
            dtFechaConstitucion.EditFormatString = GetFormatString("dd/MM/yyyy");

            dtFechaNacimiento.MaxDate = validateFechaSolicitud.AddYears(-18);
            dtFechaNacimiento.UseMaskBehavior = true;
            dtFechaNacimiento.EditFormatString = GetFormatString("dd/MM/yyyy");
            
            dtFechaNacimientoTitular.UseMaskBehavior = true;
            dtFechaNacimientoTitular.EditFormatString = GetFormatString("dd/MM/yyyy");

            dtFechaConstitucion.Date = DateTime.Today;
            dtFechaNacimiento.Date = DateTime.Today.AddYears(-18);
            dtFechaNacimientoTitular.Date = DateTime.Today;
        }
        protected string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        #endregion
    }
}