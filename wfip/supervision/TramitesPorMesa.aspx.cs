using DevExpress.Export;
using DevExpress.XtraPrinting;
using System;

namespace wfip.supervision
{
    public partial class TramitesPorMesa : System.Web.UI.Page
    {
        wfiplib.admTramite admtramite = new wfiplib.admTramite();
        wfiplib.admMesa admmesa = new wfiplib.admMesa();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StatusTramite();
                Mesas();
            }
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            grid.ExportXlsxToResponse("ASAEConsultores.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void StatusTramite()
        {
            DDLStatusTramite.DataSource = admtramite.StatusTramite_DropDownList();
            DDLStatusTramite.DataTextField = "Nombre";
            DDLStatusTramite.DataValueField = "Nombre";
            DDLStatusTramite.DataBind();
            DDLStatusTramite.Items.Insert(0, "Seleccione");
        }

        protected void Mesas()
        {
            DDLMesa.DataSource = admmesa.Mesa_DropDownList();
            DDLMesa.DataTextField = "Nombre";
            DDLMesa.DataValueField = "Id";
            DDLMesa.DataBind();
            DDLMesa.Items.Insert(0, "Seleccione");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Buscar
            grid.DataSource = admtramite.TramitesPorMesa(
                PrepararFechaInicialParaConsulta(dtFechaInicio.Text), 
                PrepararFechaFinalParaConsulta(dtFechaFin.Text), 
                DDLStatusTramite.SelectedValue, DDLMesa.SelectedValue);
            grid.DataBind();
        }

        private string PrepararFechaInicialParaConsulta(string fecha)
        {
            string strFechaI = fecha + " 00:00:00";
            DateTime dt1 = DateTime.ParseExact(strFechaI, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            return strFechaI = dt1.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private string PrepararFechaFinalParaConsulta(string fecha)
        {
            string strFechaF = fecha + " 23:59:59";
            DateTime dt1 = DateTime.ParseExact(strFechaF, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            return strFechaF = dt1.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}