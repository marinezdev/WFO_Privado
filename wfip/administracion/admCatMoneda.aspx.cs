using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admCatMoneda: System.Web.UI.Page
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
                ListaMoneda();
            }

        }

        private void ListaMoneda()
        {
            List<wfiplib.Moneda> lista = (new wfiplib.admCatMoneda()).ListaMoneda();
            rptMonedas.DataSource = lista;
            rptMonedas.DataBind();
        }

        protected void rptMonedas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdMoneda.Value = e.CommandArgument.ToString();
                wfiplib.admCatMoneda adm = new wfiplib.admCatMoneda();
                wfiplib.Moneda oMnda = adm.carga(Convert.ToInt32(hdIdMoneda.Value));
                txNombre.Text = oMnda.Nombre;
                txValor.Text = oMnda.Valor;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            wfiplib.admCatMoneda adm = new wfiplib.admCatMoneda();
            wfiplib.Moneda oMnda = RecuperaDatos();
            oMnda.IdMoneda = Convert.ToInt32(hdIdMoneda.Value);
            adm.modifica(oMnda);
            this.Limpiar();
            this.ListaMoneda();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            wfiplib.admCatMoneda adm = new wfiplib.admCatMoneda();
            wfiplib.Moneda oMnda = RecuperaDatos();

            bool Resultado = adm.nuevo(oMnda);
            this.Limpiar();
            this.ListaMoneda();

        }

        private void Limpiar()
        {
            txNombre.Text = String.Empty;
            txValor.Text = String.Empty;

        }

        private wfiplib.Moneda RecuperaDatos()
        {
            wfiplib.Moneda oMnda = new wfiplib.Moneda();
            oMnda.Nombre = txNombre.Text.ToLower();
            oMnda.Valor = txValor.Text.ToLower();
            return oMnda;
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admsysEspera.aspx"); }

    }
}