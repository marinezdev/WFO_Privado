using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class servicioGmm : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                Session.Remove("tramite");
                Session.Remove(wfiplib.E_TipoTramite.ServicioGmm.ToString());
                llenaListaTramites();
                identificaPromotoria();
            }
            // VALIDA EL TIPO DE TRAMITE TANTO PUBLICO COMO PRIVADO
            if (!String.IsNullOrEmpty(Request.QueryString["t"]))
            {
                // Query string value is there so now use it
                String TipoTramite = Request.QueryString["t"];
                if (TipoTramite == "privado" || TipoTramite == "publico")
                {
                    Label2.Text = TipoTramite.ToUpper();
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

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        private void identificaPromotoria()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    hf_IdPromotoria.Value = manejo_sesion.Credencial.IdPromotoria.ToString();
                }
            }
        }

        private void llenaListaTramites()
        {
            List<wfiplib.tipoTramiteCampo> lstGrp1 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.ServicioGmm, 1);
            List<wfiplib.tipoTramiteCampo> lstGrp2 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.ServicioGmm, 2);
            List<wfiplib.tipoTramiteCampo> lstGrp3 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.ServicioGmm, 3);
            List<wfiplib.tipoTramiteCampo> lstGrp4 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.ServicioGmm, 4);
            List<wfiplib.tipoTramiteCampo> lstGrp5 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.ServicioGmm, 5);
            List<wfiplib.tipoTramiteCampo> lstGrp6 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.ServicioGmm, 6);
            List<wfiplib.tipoTramiteCampo> lstGrp7 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.E_TipoTramite.ServicioGmm, 7);
            //List<wfiplib.tipoTramiteCampo> lstGrp8 = (new wfiplib.admTipoTramiteCampo()).ListaPorGrupo(1, wfiplib.e_TipoTramite.ServicioGmm, 8);

            foreach (wfiplib.tipoTramiteCampo reg in lstGrp1)
            {
                ListItem itm = new ListItem();
                itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGpo1.Items.Add(itm);
            }
            foreach (wfiplib.tipoTramiteCampo reg in lstGrp2)
            {
                ListItem itm = new ListItem();
                itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGpo2.Items.Add(itm);
            }
            foreach (wfiplib.tipoTramiteCampo reg in lstGrp3)
            {
                ListItem itm = new ListItem();
                itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGpo3.Items.Add(itm);
            }
            foreach (wfiplib.tipoTramiteCampo reg in lstGrp4)
            {
                chkGpo4.Items.Add(new ListItem(reg.Descripcion, reg.Campo));
            }
            foreach (wfiplib.tipoTramiteCampo reg in lstGrp5)
            {
                ListItem itm = new ListItem();
                itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGpo5.Items.Add(itm);
            }
            foreach (wfiplib.tipoTramiteCampo reg in lstGrp6)
            {
                ListItem itm = new ListItem();
                itm.Attributes.Add("onMouseover", "mostrar_detalle(dvPop,'1','" + reg.Campo + "');");
                itm.Attributes.Add("onmouseout", "mostrar_detalle(dvPop,'0');");
                itm.Text = reg.Descripcion;
                itm.Value = reg.Campo;
                chkGpo6.Items.Add(itm);
            }
            foreach (wfiplib.tipoTramiteCampo reg in lstGrp7)
            {
                chkGpo7.Items.Add(new ListItem(reg.Descripcion, reg.Campo));
            }
            //foreach (wfiplib.tipoTramiteCampo reg in lstGrp8) { chkGpo8.Items.Add(new ListItem(reg.Descripcion, reg.Campo)); }
        }


        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                wfiplib.ServicioGmmP Datos = CargaDatos();
                if (armaTramiteYGuardaEnMemoria(Datos))
                {
                    Response.Redirect("anexaArchivos.aspx");
                }
            }
            catch (Exception ex)
            {
                enviaMsgCliente(ex.Message);
            }
        }

        private bool armaTramiteYGuardaEnMemoria(wfiplib.ServicioGmmP Datos)
        {
            int Id = (new wfiplib.admTramite()).siguienteId();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            wfiplib.tramiteP oTramite = new wfiplib.tramiteP();

            oTramite.Id = Id;
            oTramite.IdTipoTramite = wfiplib.E_TipoTramite.ServicioGmm;
            oTramite.IdPromotoria = manejo_sesion.Credencial.IdPromotoria;
            oTramite.IdUsuario = manejo_sesion.Credencial.Id;
            oTramite.DatosHtml = Datos.DatosHtml;
            oTramite.AgenteClave = Convert.ToString(txIdAgente.Text.Trim().ToUpper());
            oTramite.NumeroOrden = textNumeroOrden.Text.Trim().ToUpper();
            oTramite.FechaSolicitud = txFechaSol.Text.Trim().ToUpper();
            oTramite.TipoTramite = Label2.Text.ToString();
            Session["tramite"] = oTramite;

            Datos.IdTramite = Id;
            Session[wfiplib.E_TipoTramite.ServicioGmm.ToString()] = Datos;

            return (Id > 0);
        }

        private wfiplib.ServicioGmmP CargaDatos()
        {

            wfiplib.ServicioGmmP Datos = new wfiplib.ServicioGmmP();
            Datos.NumPoliza = txPoliza.Text;
            if (cboTipoContratante.SelectedValue.Equals("1"))
            {
                Datos.TipoPersona = wfiplib.E_TipoPersona.Fisica;
                Datos.Nombre = txNombre.Text;
                Datos.ApPaterno = txApPat.Text;
                Datos.ApMaterno = txApMat.Text;
                Datos.RFC = txRfc.Text;
                Datos.Nacionalidad = txNacionalidad.Text;
            }
            else if (cboTipoContratante.SelectedValue.Equals("2"))
            {
                Datos.TipoPersona = wfiplib.E_TipoPersona.Moral;
                Datos.Nombre = txNomMoral.Text.Trim().ToUpper();
                Datos.RFC = txRfcMoral.Text.Trim().ToUpper();
            }

            foreach (ListItem reg in chkGpo1.Items)
            {
                if (reg.Selected)
                {
                    if (reg.Value.Equals("C128")) { Datos.C128 = 1; }
                    if (reg.Value.Equals("C129")) { Datos.C129 = 1; }
                    if (reg.Value.Equals("C1210")) { Datos.C1210 = 1; }
                    if (reg.Value.Equals("C1211")) { Datos.C1211 = 1; }
                    if (reg.Value.Equals("C1212")) { Datos.C1212 = 1; }
                    if (reg.Value.Equals("C1213")) { Datos.C1213 = 1; }
                    if (reg.Value.Equals("C1214")) { Datos.C1214 = 1; }
                    if (reg.Value.Equals("C1215")) { Datos.C1215 = 1; }
                    if (reg.Value.Equals("C1216")) { Datos.C1216 = 1; }
                }
            }
            foreach (ListItem reg in chkGpo2.Items)
            {
                if (reg.Selected)
                {
                    if (reg.Value.Equals("C1217"))
                    {
                        Datos.C1217 = 1;
                    }
                }
            }

            foreach (ListItem reg in chkGpo3.Items)
            {
                if (reg.Selected)
                {
                    if (reg.Value.Equals("C1218"))
                    {
                        Datos.C1218 = 1;
                    }
                    if (reg.Value.Equals("C1219"))
                    {
                        Datos.C1219 = 1;
                    }
                    if (reg.Value.Equals("C1220"))
                    {
                        Datos.C1220 = 1;
                    }
                }
            }

            foreach (ListItem reg in chkGpo4.Items)
            {
                if (reg.Selected)
                {
                    if (reg.Value.Equals("C1221"))
                    {
                        Datos.C1221 = 1;
                    }
                }
            }

            bool ConAfectacion = false;

            foreach (ListItem reg in chkGpo5.Items)
            {
                if (reg.Selected)
                {
                    if (reg.Value.Equals("C1222"))
                    {
                        Datos.C1222 = 1;
                        ConAfectacion = true;
                    }
                    if (reg.Value.Equals("C1223"))
                    {
                        Datos.C1223 = 1;
                        ConAfectacion = true;
                    }
                    if (reg.Value.Equals("C1224"))
                    {
                        Datos.C1224 = 1;
                        ConAfectacion = true;
                    }
                    if (reg.Value.Equals("C1225"))
                    {
                        Datos.C1225 = 1;
                        ConAfectacion = true;
                    }
                    if (reg.Value.Equals("C1226"))
                    {
                        Datos.C1226 = 1;
                        ConAfectacion = true;
                    }
                    if (reg.Value.Equals("C1227"))
                    {
                        Datos.C1227 = 1;
                        ConAfectacion = true;
                    }
                    if (reg.Value.Equals("C1228"))
                    {
                        Datos.C1228 = 1;
                        ConAfectacion = true;
                    }
                }
            }


            foreach (ListItem reg in chkGpo6.Items)
            {
                if (reg.Selected)
                {
                    if (reg.Value.Equals("C1229"))
                    {
                        Datos.C1229 = 1;
                    }
                    if (reg.Value.Equals("C1230"))
                    {
                        Datos.C1230 = 1;
                    }
                }
            }

            foreach (ListItem reg in chkGpo7.Items)
            {
                if (reg.Selected)
                {
                    if (reg.Value.Equals("C1231"))
                    {
                        Datos.C1231 = 1;
                    }
                    if (reg.Value.Equals("C1232"))
                    {
                        Datos.C1232 = 1;
                    }
                }
            }

            if (ConAfectacion)
            {
                Datos.ConAfectacion = 1;
            }
            Datos.Detalle = txDetalle.Text;

            return Datos;
        }

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoContratante.SelectedValue.Equals("1"))
            {
                pnPrsFisica.Visible = true;
                pnPrsMoral.Visible = false;
            }
            else if (cboTipoContratante.SelectedValue.Equals("2"))
            {
                pnPrsMoral.Visible = true;
                pnPrsFisica.Visible = false;
            }
            else
            {
                pnPrsFisica.Visible = false;
                pnPrsMoral.Visible = false;
            }

            lbNombreAgente.Text = daNombreDeAgente(hf_IdPromotoria.Value, txIdAgente.Text);
        }


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
        public static string daNombreDeAgente(string pIdPromotoria, string pClaveAgente)
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
        public static string[] regresaNombreRegion(string pIdPromotoria)
        {
            String[] array = new string[10];
            array[0] = string.Concat(pIdPromotoria, " Región");
            array[1] = string.Concat(pIdPromotoria, " Subdirección");
            array[2] = string.Concat(pIdPromotoria, " Gerente Comercial");
            array[3] = string.Concat(pIdPromotoria, " Ejecutivo Comercial");

            return array;
        }
    }

}