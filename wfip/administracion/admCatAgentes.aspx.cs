using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admCatAgentes : System.Web.UI.Page
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
            if (!IsPostBack) { ListaAgentes(); }
        }

        private void ListaAgentes() {
            List<wfiplib.Agente> lista = (new wfiplib.admCatAgentes()).ListaAgentes();
            rptAgentes.DataSource = lista;
            rptAgentes.DataBind();
        }

        protected void rptAgentes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdAgente.Value = e.CommandArgument.ToString();
                wfiplib.admCatAgentes adm = new wfiplib.admCatAgentes();
                wfiplib.Agente oAgt = adm.carga(Convert.ToInt32(hdIdAgente.Value));
                txNombre.Text = oAgt.Nombre;
                txRfc.Text = oAgt.Rfc;
                txClave.Text = oAgt.Clave;
                txDireccion.Text = oAgt.Direccion;
                txCiudad.Text = oAgt.Ciudad;
                txCp.Text = oAgt.Cp;
                txCorreo.Text = oAgt.Correo;
                txTelefono.Text = oAgt.Telefono;
                txExtencion.Text = oAgt.Extencion;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            wfiplib.admCatAgentes adm = new wfiplib.admCatAgentes();
            wfiplib.Agente oAgt = RecuperaDatos();
            oAgt.Id = Convert.ToInt32(hdIdAgente.Value);
            adm.modifica(oAgt);
            this.Limpiar();
            this.ListaAgentes();
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
            wfiplib.admCatAgentes adm = new wfiplib.admCatAgentes();
            wfiplib.Agente oAgt = RecuperaDatos();
            if (!adm.Existe(oAgt.Rfc))
            {
                bool Resultado = adm.nuevo(oAgt);
                this.Limpiar();
                this.ListaAgentes();
            }
            else { ltMsg.Text = "El agente ya existe"; }
        }

        private void Limpiar()
        {
            txNombre.Text = String.Empty;
            txRfc.Text = String.Empty;
            txClave.Text = String.Empty;
            txDireccion.Text = String.Empty;
            txCiudad.Text = String.Empty;
            txCp.Text = String.Empty;
            txCorreo.Text = String.Empty;
            txTelefono.Text = String.Empty;
            txExtencion.Text = String.Empty;
        }

        private wfiplib.Agente RecuperaDatos()
        {
            wfiplib.Agente oAgt = new wfiplib.Agente();
            oAgt.Nombre = txNombre.Text.ToUpper();
            oAgt.Rfc = txRfc.Text.ToUpper();
            oAgt.Clave = txClave.Text.ToUpper();
            oAgt.Direccion = txDireccion.Text;
            oAgt.Ciudad = txCiudad.Text;
            oAgt.Cp = txCp.Text;
            oAgt.Correo = txCorreo.Text;
            oAgt.Telefono = txTelefono.Text;
            oAgt.Extencion = txExtencion.Text;
            return oAgt;
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admsysEspera.aspx"); }
        
    }
}