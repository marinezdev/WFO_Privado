using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using wfiplib;

namespace wfip.operacion
{
    public partial class esperaOperacion : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("../Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Mensajes mensajes = new Mensajes();
            if (!IsPostBack)
            {
                // warningMessages
                string mensajeShow = (new wfiplib.admCatMensajes()).mensaje("operador");
                if (mensajeShow.Length > 5)
                {
                    lblMessageBySystem.Visible = true;
                    lblMessageBySystem.Text = mensajeShow;
                    mensajes.MostrarMensaje(this, mensajeShow);
                }
                else
                {
                    lblMessageBySystem.Visible = false;
                    lblMessageBySystem.Text = string.Empty;
                }

                Session["tramiteActualId"] = null;
                Session["mesaActual"] = null;

                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    //Mensajes mensajes = new Mensajes();
                    List<wfiplib.mesa> Mesas = null;
                    int intIdFlujo = -1;
                    //int res = Convert.ToInt32(Request.QueryString["res"]);
                    int res = Convert.ToInt32(urlCifrardo.Respuesta);
                    if (res == 1)
                        mensajes.MostrarMensaje(this, "Por el momento NO hay trámites para la mesa.");

                    if (manejo_sesion.Credencial.usuarioMesa.Count == 0)
                    {
                        mensajes.MostrarMensaje(this, "El usuario NO tiene configurada una mesa, favor de validar con el Administrador.");
                        pnlMesas.Visible = false;
                    }
                    else
                    {

                        //intIdFlujo = Convert.ToInt32(Request.QueryString["flujo"]);
                        intIdFlujo = Convert.ToInt32(urlCifrardo.Flujo);
                        if (intIdFlujo > 0)
                            Mesas = (new wfiplib.admSeguridad()).getAssignMesas(manejo_sesion.Credencial.Id, intIdFlujo);
                        else if (intIdFlujo == 0)
                            Response.Redirect("SeleccionaFlujo.aspx");
                        else
                            Mesas = (new wfiplib.admSeguridad()).getAssignMesas(manejo_sesion.Credencial.Id);
                        // rpt_Mesas.DataSource = manejo_sesion.Credencial.usuarioMesa;

                        rpt_Mesas.DataSource = Mesas;
                        rpt_Mesas.DataBind();
                        pnlMesas.Visible = true;

                    }
                    Session.Contents.Remove("nota");
                }
                else
                {
                    Response.Redirect("SeleccionaFlujo.aspx");
                }
            }
        }

        protected void rpt_Mesas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
                int intIdFlujo = Convert.ToInt32(urlCifrardo.Flujo);
                //int intIdFlujo = Convert.ToInt32(Request.QueryString["flujo"]);

                /// TODO: Se realizó el cambio a la nueva página para procesar el trámite desde un storeProcedura
                //Response.Redirect("consultaTramite2.aspx?flujo="+intIdFlujo.ToString()+"&tp=" + e.CommandArgument);
                Response.Redirect(EncripParametros("flujo=" + intIdFlujo.ToString()+ ",tp="+ e.CommandArgument, "consultaTramite2.aspx").URL, true);
            }
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string flujo = "";
                string respuesta = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string Busqeudaflujo = stringBetween(s + ".", "flujo=", ".");
                    if (Busqeudaflujo.Length > 0)
                    {
                        flujo = Busqeudaflujo;
                    }
                    string BusqeudaRespuesta = stringBetween(s + ".", "res=", ".");
                    if (BusqeudaRespuesta.Length > 0)
                    {
                        respuesta = BusqeudaRespuesta;
                    }
                }

                if (flujo.Length > 0)
                {
                    urlCifrardo.Flujo = flujo.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.Flujo = "";
                    urlCifrardo.Result = false;
                }

                if (respuesta.Length > 0)
                {
                    urlCifrardo.Respuesta = respuesta.ToString();
                }
                else
                {
                    urlCifrardo.Respuesta = "0";
                }

            }
            catch (Exception ex)
            {
                urlCifrardo.Result = false;
            }

            return urlCifrardo;
        }
        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }
        public static string stringBetween(string Source, string Start, string End)
        {
            string result = "";
            if (Source.Contains(Start) && Source.Contains(End))
            {
                int StartIndex = Source.IndexOf(Start, 0) + Start.Length;
                int EndIndex = Source.IndexOf(End, StartIndex);
                result = Source.Substring(StartIndex, EndIndex - StartIndex);
                return result;
            }

            return result;
        }

    }
}