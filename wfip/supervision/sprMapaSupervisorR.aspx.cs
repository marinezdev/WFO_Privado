using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprMapaSupervisorR : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        Mensajes mensajes = new Mensajes();

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
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    //if (Request.QueryString["Id"] != null && Request.QueryString["Id"].ToString().Length > 2)
                    if (urlCifrardo.Mesa != null && urlCifrardo.Mesa.ToString().Length > 2)
                    {
                        //string NombreMesa = Request.QueryString["Id"].ToString().Trim();
                        string NombreMesa = urlCifrardo.Mesa.ToString().Trim();
                        CargaFlujos(NombreMesa);
                        ASPxRoundPanel1.HeaderText = "MESA : " + NombreMesa;
                    }
                    else
                    {
                        Response.Redirect("esperaSupervisorPR.aspx");
                    }
                }
                else
                {
                    Response.Redirect("esperaSupervisorPR.aspx");
                }
            }
        }

        protected void CargaFlujos(string NombreMesa)
        {
            DataTable dt = (new wfiplib.admMesa()).Mesa_Detalle_Selecionar_PorNombre(NombreMesa, manejo_sesion.Credencial.Id);
            rptMesaDetalle.DataSource = dt;
            rptMesaDetalle.DataBind();
        }

        protected void rptImgListadoTramites_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    int Flujo1 = Convert.ToInt32(e.CommandName.ToString());
                    int Flujo2 = Convert.ToInt32(e.CommandArgument.ToString());
                    string NombreMesa = urlCifrardo.Mesa.ToString().Trim();
                    //string NombreMesa = Request.QueryString["Id"].ToString().Trim();

                    DataTable dt = (new wfiplib.admMesa()).Mesa_Detalle_Selecionar(Flujo1, Flujo2, NombreMesa);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Response.Redirect("esperaSupervisorPR.aspx");
                }
                
            }
        }

        protected void rptImgDetalleTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    int Flujo1 = Convert.ToInt32(e.CommandName.ToString());
                    //string NombreMesa = Request.QueryString["Id"].ToString().Trim();
                    Response.Redirect(EncripParametros("Id=" + Flujo1.ToString() + ",m=" + urlCifrardo.Mesa, "OpConsultaTramite.aspx").URL, true);
                    //Response.Redirect("OpConsultaTramite.aspx?Id=" + Flujo1.ToString()+"&m="+NombreMesa);
                }
                else
                {
                    Response.Redirect("esperaSupervisorPR.aspx");
                }
            }
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string Mesa = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaMesa = stringBetween(s + ".", "Id=", ".");
                    if (BusqeudaMesa.Length > 0)
                    {
                        Mesa = BusqeudaMesa;
                    }
                }

                if (Mesa.Length > 0)
                {
                    urlCifrardo.Mesa = Mesa.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdTramite = "";
                    urlCifrardo.Result = false;
                }
            }
            catch (Exception)
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