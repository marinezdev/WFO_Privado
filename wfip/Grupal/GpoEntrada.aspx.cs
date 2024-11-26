using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.Grupal
{
    public partial class GpoEntrada : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GpoEspera.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                wfiplib.credencial _credencial = (wfiplib.credencial)Session["credencial"];
                ListaPendientes(_credencial.Id);
            }
        }

        private void ListaPendientes(int pUsuarioId)
        {
            wfiplib.AdmBuzonTramite _AdmBuzonTramite = new wfiplib.AdmBuzonTramite();
            RptBuzonEntrada.DataSource = _AdmBuzonTramite.LstTramitesEnBuzon(_AdmBuzonTramite.BuzonEntrada(pUsuarioId));
            RptBuzonEntrada.DataBind();
        }

        protected void RptBuzonEntrada_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Abrir")) { Response.Redirect($"GpoAtiende.aspx?id={e.CommandArgument}"); }
        }
    }
}