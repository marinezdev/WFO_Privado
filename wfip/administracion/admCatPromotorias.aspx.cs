using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admCatPromotorias : System.Web.UI.Page
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
            if (!IsPostBack) { ListaPromotorias(); }
        }

        private void ListaPromotorias()
        {
            List<wfiplib.Promotoria> lista = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).ListaPromotorias();
            rptPromo.DataSource = lista;
            rptPromo.DataBind();
        }

        protected void rptPromo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdProm.Value = e.CommandArgument.ToString();
                wfiplib.admCatPromotoria adm = new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString);
                wfiplib.Promotoria oDt = adm.carga(Convert.ToInt32(hdIdProm.Value));
                txNombre.Text = oDt.Nombre;
                txRfc.Text = oDt.Rfc;
                txClave.Text = oDt.Clave;
                txDireccion.Text = oDt.Direccion;
                txCiudad.Text = oDt.Ciudad;
                txCp.Text = oDt.Cp;
                txCorreo.Text = oDt.Correo;
                txTelefono.Text = oDt.Telefono;
                txExtencion.Text = oDt.Extencion;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            wfiplib.admCatPromotoria adm = new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString);
            wfiplib.Promotoria oDt = RecuperaDatos();
            if (!adm.Existe(oDt.Rfc))
            {
                bool Resultado = adm.nuevo(oDt);
                this.Limpiar();
                this.ListaPromotorias();
            }
            else { ltMsg.Text = "La prootoria ya existe"; }
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
            wfiplib.admCatPromotoria adm = new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString);
            wfiplib.Promotoria oDt = RecuperaDatos();
            oDt.Id = Convert.ToInt32(hdIdProm.Value);
            adm.modifica(oDt);
            this.Limpiar();
            this.ListaPromotorias();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
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

        private wfiplib.Promotoria RecuperaDatos()
        {
            wfiplib.Promotoria oDt = new wfiplib.Promotoria();
            oDt.Nombre = txNombre.Text.ToUpper();
            oDt.Rfc = txRfc.Text.ToUpper();
            oDt.Clave = txClave.Text.ToUpper();
            oDt.Direccion = txDireccion.Text;
            oDt.Ciudad = txCiudad.Text;
            oDt.Cp = txCp.Text;
            oDt.Correo = txCorreo.Text;
            oDt.Telefono = txTelefono.Text;
            oDt.Extencion = txExtencion.Text;
            return oDt;
        }

        protected void btnCerrar_Click(object sender, EventArgs e){Response.Redirect("admsysEspera.aspx");}
    }
}