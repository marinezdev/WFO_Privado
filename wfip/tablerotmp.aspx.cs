using System;
using System.Web.UI.DataVisualization.Charting;

namespace wfip
{
    public partial class tablerotmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { pintaDatos(); }
        }

        private void pintaDatos()
        {
            pintaPendientes();
            pintaConfianza();
            pintaTacometros();
        }

        private void pintaPendientes()
        {
            Random r = new Random(DateTime.Now.Millisecond);

            string colorExa = "#0099CC";
            System.Drawing.Color colorBase = System.Drawing.ColorTranslator.FromHtml(colorExa);

            grfPendientes.Series["seriePendientes"].Points.Add(creaPunto("TOTAL", 100, System.Drawing.Color.Red));
            grfPendientes.Series["seriePendientes"].Points.Add(creaPunto("ADMISION", r.Next(10, 90), colorBase));
            grfPendientes.Series["seriePendientes"].Points.Add(creaPunto("REVISION DOCUMENTAL", r.Next(10, 90), colorBase));
            grfPendientes.Series["seriePendientes"].Points.Add(creaPunto("REVISION PLAD", r.Next(10, 90), colorBase));
            grfPendientes.Series["seriePendientes"].Points.Add(creaPunto("SELECCION", r.Next(10, 90), colorBase));
            grfPendientes.Series["seriePendientes"].Points.Add(creaPunto("CONFIRMACION TECNICA", r.Next(10, 90), colorBase));
            grfPendientes.Series["seriePendientes"].Points.Add(creaPunto("MEDICO", r.Next(10, 90), colorBase));
            grfPendientes.Series["seriePendientes"].Points.Add(creaPunto("EJECUCION", r.Next(10, 90), System.Drawing.Color.Green));
        }

        private DataPoint creaPunto(string pEtiqueta, double pYvalor, System.Drawing.Color pColor)
        {
            DataPoint resultado = new DataPoint();
            resultado.AxisLabel = pEtiqueta;
            resultado.SetValueY(pYvalor);
            resultado.IsValueShownAsLabel = true;
            resultado.Color = pColor;
            return resultado;
        }

        private void pintaConfianza()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int valor = r.Next(45, 85);
            ASPxGaugeControl1.Value = valor.ToString();
            if (valor <= 50) ASPxGaugeControl2.Value = "1";
            if (valor > 50 && valor <= 80) ASPxGaugeControl2.Value = "2";
            if (valor > 80) ASPxGaugeControl2.Value = "3";
        }

        private void pintaTacometros()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            tacAdmision.Value = r.Next(10, 90).ToString();
            tacDocumental.Value = r.Next(10, 90).ToString();
            tacPlad.Value = r.Next(10, 90).ToString();
            tacSeleccion.Value = r.Next(10, 90).ToString();
            tacTecnica.Value = r.Next(10, 90).ToString();
            tacMedico.Value = r.Next(10, 90).ToString();
            tacEjecucion.Value = r.Next(10, 90).ToString();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            pintaDatos();
        }
    }
}