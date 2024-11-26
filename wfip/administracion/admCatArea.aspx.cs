using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admCatArea : System.Web.UI.Page
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
            if (!IsPostBack) { ListaAreas(); }
        }

        private void ListaAreas()
        {
            List<wfiplib.Area> lista = (new wfiplib.admCatAreas()).ListaAreas();
            rptAreas.DataSource = lista;
            rptAreas.DataBind();
        }

        protected void rptAreas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdArea.Value = e.CommandArgument.ToString();
                wfiplib.admCatAreas adm = new wfiplib.admCatAreas();
                wfiplib.Area oAgt = adm.carga(Convert.ToInt32(hdIdArea.Value));
                txNombre.Text = oAgt.Nombre;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            wfiplib.admCatAreas adm = new wfiplib.admCatAreas();
            wfiplib.Area oAgt = RecuperaDatos();
            if (!adm.Existe(oAgt.Nombre))
            {
                bool Resultado = adm.nuevo(oAgt);
                this.Limpiar();
                this.ListaAreas();
            }
            else { ltMsg.Text = "El area ya existe"; }
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            wfiplib.admCatAreas adm = new wfiplib.admCatAreas();
            wfiplib.Area oDt = RecuperaDatos();
            oDt.Id = Convert.ToInt32(hdIdArea.Value);
            adm.modifica(oDt);
            this.Limpiar();
            this.ListaAreas();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        private void Limpiar()
        {
            txNombre.Text = String.Empty;
        }

        private wfiplib.Area RecuperaDatos()
        {
            wfiplib.Area oDt = new wfiplib.Area();
            oDt.Nombre = txNombre.Text.ToUpper();

            return oDt;
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admsysEspera.aspx"); }
    }
}