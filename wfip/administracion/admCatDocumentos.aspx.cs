using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admCatDocumentos : System.Web.UI.Page
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
            if (!IsPostBack) { DaListaDocumentos(); }
        }
        private void DaListaDocumentos()
        {
            List<wfiplib.catDocumento> lista = (new wfiplib.admCatDocumento()).ListaDocumentos();
            rptDoctos.DataSource = lista;
            rptDoctos.DataBind();
        }

        protected void rptDoctos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdTipoDocto.Value = e.CommandArgument.ToString();
                wfiplib.admCatDocumento adm = new wfiplib.admCatDocumento();
                wfiplib.catDocumento oDoc = adm.carga(Convert.ToInt32(hdIdTipoDocto.Value));
                txNombre.Text = oDoc.Nombre;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            wfiplib.admCatDocumento adm = new wfiplib.admCatDocumento();
            wfiplib.catDocumento oDoc = RecuperaDatos();
            oDoc.IdTipoDocto = Convert.ToInt32(hdIdTipoDocto.Value);
            adm.modifica(oDoc);
            this.Limpiar();
            this.DaListaDocumentos();
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
            wfiplib.admCatDocumento adm = new wfiplib.admCatDocumento();
            wfiplib.catDocumento oDoc = RecuperaDatos();
            if (!adm.Existe(oDoc.Nombre))
            {
                bool Resultado = adm.nuevo(oDoc);
                this.Limpiar();
                this.DaListaDocumentos();
            }
            else { ltMsg.Text = "El documento ya existe"; }
        }

        private void Limpiar()
        {
            txNombre.Text = String.Empty;
        }

        private wfiplib.catDocumento RecuperaDatos()
        {
            wfiplib.catDocumento oDoc = new wfiplib.catDocumento();
            oDoc.Nombre = txNombre.Text.ToUpper();
            return oDoc;
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admsysEspera.aspx"); }

    }
}