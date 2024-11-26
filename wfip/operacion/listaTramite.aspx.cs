using System;
using System.Web.UI.WebControls;

namespace wfip.operacion
{
    public partial class listaTramite : System.Web.UI.Page
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
                pintaTramites();
                Session.Contents.Remove("nota");
            }
        }

        private void pintaTramites()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            wfiplib.admTramiteMesa adm = new wfiplib.admTramiteMesa();

            rptTrmPausa.DataSource = adm.daListaEnEspera(manejo_sesion.Credencial.Id);
            rptTrmPausa.DataBind();
            rpTrmProceso.DataSource = adm.daListaAtrapados(manejo_sesion.Credencial.Id);
            rpTrmProceso.DataBind();
            rpCitaMedica.DataSource = adm.daListaCitasMedicas(manejo_sesion.Credencial.Id);
            rpCitaMedica.DataBind();
            rpTrmCancelado.DataSource = adm.daListaCancelados(manejo_sesion.Credencial.Id);
            rpTrmCancelado.DataBind();
        }

        protected void rpCitaMedica_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "OpConsultaTramite.aspx").URL, true);
                //Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
            }
        }

        protected void rptTramitesCancelado_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                //Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "OpConsultaTramite.aspx").URL, true);
            }
        }

        protected void rptTrmPausa_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                //Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandName.ToString() + "&id=" + e.CommandArgument.ToString());
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString()+ ",tp="+e.CommandName.ToString(), "consultaTramite2.aspx").URL, true);
            }
        }

        protected void rpTrmProceso_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                //Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandName.ToString() + "&id=" + e.CommandArgument.ToString());
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString() + ",tp=" + e.CommandName.ToString(), "consultaTramite2.aspx").URL, true);
            }
        }

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }

    }
}