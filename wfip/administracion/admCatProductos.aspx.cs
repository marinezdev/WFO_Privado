using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admCatproductos : System.Web.UI.Page
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
                ListaProducto();
            }

        }

        private void ListaProducto()
        {
            List<wfiplib.Productos> lista = (new wfiplib.admCatProductos()).ListaProducto();
            rptProductos.DataSource = lista;
            rptProductos.DataBind();
        }

        protected void rptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdProductos.Value = e.CommandArgument.ToString();
                wfiplib.admCatProductos adm = new wfiplib.admCatProductos();
                wfiplib.Productos oPdt = adm.carga(Convert.ToInt32(hdIdProductos.Value));
                txNombre.Text = oPdt.Nombre;
                TxTipoTramite.Text = oPdt.Id_TipoTramite;
                txDescripcion.Text = oPdt.Descripcion;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            wfiplib.admCatProductos adm = new wfiplib.admCatProductos();
            wfiplib.Productos oPdt = RecuperaDatos();
            oPdt.idCatProducto = Convert.ToInt32(hdIdProductos.Value);
            adm.modifica(oPdt);
            this.Limpiar();
            this.ListaProducto();
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
            wfiplib.admCatProductos adm = new wfiplib.admCatProductos();
            wfiplib.Productos oPdt = RecuperaDatos();

            bool Resultado = adm.nuevo(oPdt);
            this.Limpiar();
            this.ListaProducto();

        }

        private void Limpiar()
        {
            txNombre.Text = String.Empty;
            TxTipoTramite.Text = String.Empty;
            txDescripcion.Text = String.Empty;

        }

        private wfiplib.Productos RecuperaDatos()
        {
            wfiplib.Productos oPdt = new wfiplib.Productos();
            oPdt.Nombre = txNombre.Text.ToLowerInvariant();
            oPdt.Id_TipoTramite = TxTipoTramite.Text.ToLowerInvariant();
            oPdt.Descripcion = txDescripcion.Text.ToLowerInvariant();
            return oPdt;
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admsysEspera.aspx"); }

    }
}