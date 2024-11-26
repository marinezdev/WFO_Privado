using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class usuariosV2_cat : System.Web.UI.Page
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
                PintaDatos();
        }

        private void PintaDatos()
        {
            Mensajes mensajes = new Mensajes();

            rptCatalogo.DataSource = (new wfiplib.admCredencial()).ListadoUsuario();
            rptCatalogo.DataBind();

            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

            if (urlCifrardo.Result)
            {
                int intMensaje = Convert.ToInt16(urlCifrardo.Mensaje);
                if (intMensaje == 1)
                    mensajes.MostrarMensaje(this, "El usuario se creo correctamente.");
            }
                
        }

        protected void rptCatalogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                wfiplib.credencial registro = (wfiplib.credencial)e.Item.DataItem;
                if (registro.Estado == wfiplib.E_Estado.Inactivo)
                {
                    ((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";
                }

                if (registro.Modulo == wfiplib.E_Modulo.Operacion || (new wfiplib.admSeguridad()).VerificaRol("OPERACIÓN MASTER", registro.IdRol))
                {
                    ((ImageButton)e.Item.FindControl("imgBtnOperacion")).Visible = true;
                }
                else
                {
                    ((ImageButton)e.Item.FindControl("imgBtnOperacion")).Visible = false;
                }
            }
        }

        protected void rptCatalogo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            wfiplib.admCredencial admCred = new wfiplib.admCredencial();
            wfiplib.credencial credencial = admCred.carga(Convert.ToInt32(e.CommandArgument));

            switch (e.CommandName)
            {
                case "activo":
                    if (credencial.Estado == wfiplib.E_Estado.Inactivo)
                        admCred.activa(credencial.Id.ToString());
                    else
                        admCred.desactiva(credencial.Id.ToString());

                    PintaDatos();
                    break;

                case "modificar":
                    // wfiplib.credencial credencial = (new wfiplib.admCredencial()).carga(Convert.ToInt32(e.CommandArgument));
                    //Response.Redirect("usuariosV2_det.aspx?Action=2&Id=" + e.CommandArgument, true);
                    Response.Redirect(EncripParametros("Action=2,Id=" + e.CommandArgument.ToString(), "usuariosV2_det.aspx").URL, true);
                    break;

                case "operacion":
                    //Response.Redirect("usuariosV2_WFO.aspx?id=" + e.CommandArgument, true);
                    Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "usuariosV2_WFO.aspx").URL, true);
                    break;
            }
        }

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            //Response.Redirect("usuariosV2_det.aspx?Action=1&Id=-1", true);
            Response.Redirect(EncripParametros("Action=1,Id=-1", "usuariosV2_det.aspx").URL, true);
        }

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            if (Request.QueryString["data"] != null)
            {
                try
                {
                    string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                    string mensaje = "";

                    String[] spearator = { "," };
                    String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                    foreach (String s in strlist)
                    {

                        string BusquedaMensaje = stringBetween(s + ".", "Mensaje=", ".");
                        if (BusquedaMensaje.Length > 0)
                        {
                            mensaje = BusquedaMensaje;
                        }
                    }

                    if (mensaje.Length > 0)
                    {
                        urlCifrardo.Mensaje = mensaje.ToString();
                        urlCifrardo.Result = true;
                    }
                    else
                    {
                        urlCifrardo.Mensaje = "";
                        urlCifrardo.Result = false;
                    }
                }
                catch (Exception)
                {
                    urlCifrardo.Result = false;
                }
            }
            else
            {
                urlCifrardo.Result = false;
            }
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