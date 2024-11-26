using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReporteProductividadOperacion : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            CalDesde.EditFormatString = "yyyy-MM-dd";
            /*CalDesde.Date = DateTime.Now.AddDays(-1);*/
            CalDesde.Date = DateTime.Today;
            CalDesde.MaxDate = DateTime.Today;
            CalHasta.EditFormatString = "yyyy-MM-dd";
            CalHasta.Date = DateTime.Today;
            CalHasta.MaxDate = DateTime.Today;
            Panel1.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = (new wfiplib.admCatPrioridad()).ListaFlujos(manejo_sesion.Credencial.Id);
                cmbFlujo.DataSource = dt;
                cmbFlujo.DataTextField = "Nombre";
                cmbFlujo.DataValueField = "Id";
                cmbFlujo.DataBind();
            }
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                int IdFlujo = Convert.ToInt32(cmbFlujo.SelectedValue.ToString());
                if (IdFlujo == 1)
                {
                    DataTable dt = (new wfiplib.admMesa()).Mesa_Productividad_Flujo(1,0,CalDesde.Date, CalHasta.Date);
                    MuestraMesas(dt);
                }
                else if(IdFlujo == 2)
                {
                    DataTable dt = (new wfiplib.admMesa()).Mesa_Productividad_Flujo(2,3,CalDesde.Date, CalHasta.Date);
                    MuestraMesas(dt);
                }
                else if (IdFlujo == 4)
                {
                    DataTable dt = (new wfiplib.admMesa()).Mesa_Productividad_Flujo(4, 0, CalDesde.Date, CalHasta.Date);
                    MuestraMesas(dt);
                }
                else
                {
                    Mensaje.Text = "Especifica el flujo a consultar.";
                }
                
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
            }
        }


        protected void MuestraMesas(DataTable data)
        {
            string Mesas = "";
            int IdFlujo = Convert.ToInt32(cmbFlujo.SelectedValue.ToString());
            foreach (DataRow row in data.Rows)
            {

                Mesas += "<div class='col-md-6 col-sm-6 col-xs-12'><BR><BR>" +
                                    "<div class='card'>" +
                                        "<div class='card-header'>" +
                                            "<h5 class='card-title'>Mesa : " + row["MESA"].ToString() + "</h5>" +
                                        "</div>" +
                                        "<div class='card-body'>" +
                                        "<table class='able table-striped table-bordered table-hover' style='width:100%'>" +
                                          "<thead>" +
                                            "<tr>" +
                                              "<th scope='col'>Operador</th>" +
                                              "<th scope='col'>Total</th>" +
                                              "<th scope='col'>Reingresos</th>" +
                                              "<th scope='col'>Calidad</th>" +
                                            "</tr>" +
                                          "</thead>" +
                                          "<tbody>" +
                                            "<tr>" +
                                              "<th scope = 'row' >" + row["NOMBRE1"].ToString() + "</th>" +
                                              "<td> " + row["TOTAL1"].ToString() + " </td>" +
                                              "<td> " + row["REINGRESOS1"].ToString() + " </td>" +
                                              "<td> " + row["CALIDAD1"].ToString() + " </td>" +
                                            "</tr>" +
                                            "<tr>" +
                                              "<th scope='row'>" + row["NOMBRE2"].ToString() + "</th>" +
                                              "<td>" + row["TOTAL2"].ToString() + "</td>" +
                                              "<td>" + row["REINGRESOS2"].ToString() + "</td>" +
                                              "<td>" + row["CALIDAD2"].ToString() + " </td>" +
                                            "</tr>" +
                                            "<tr>" +
                                              "<th scope='row'>" + row["NOMBRE3"].ToString() + "</th>" +
                                              "<td> " + row["TOTAL3"].ToString() + " </td>" +
                                              "<td>" + row["REINGRESOS3"].ToString() + "</td>" +
                                              "<td>" + row["CALIDAD3"].ToString() + " </td>" +
                                            "</tr>" +
                                          "</tbody>" +
                                        "</table>" +
                                        "<hr>" +
                                        "<h5 class='card-title'>Fecha Inicio:"+ CalDesde.Date.ToShortDateString() + " - Fecha Fin:" + CalHasta.Date.ToShortDateString() + "</h5>" +
                                        "<div class='row'>" +
                                            "<div class='col-md-6 col-sm-6 col-xs-12'>" +
                                                "<a  class='btn btn-primary' onclick='DetalleMesa("+ row["ABRE"].ToString()+","+IdFlujo.ToString() +");'> Mostrar detalle </a>" +
                                            "</div>" +
                                            "<div class='col-md-6 col-sm-6 col-xs-12'>" +
                                                "<a  class='btn btn-info' onclick='DetalleMesaGraf(" + row["ABRE"].ToString() + "," + IdFlujo.ToString() + ");'> Mostrar gráfica </a>" +
                                            "</div>" +
                                        "</div> " +
                                        "</div>" +
                                    "</div>" +
                                "</div>";

                MesasLiteral.Text = Mesas;
                
            }
            Panel1.Visible = false;
        }

        [WebMethod]
        public static ModelGraficaPorcentaje DetalleMesaGraf(int IdOrden, int IdFlujo, string FechaIn, string FechaFin)
        {
            DateTime CalDesde = Convert.ToDateTime(FechaIn);
            DateTime CalHasta = Convert.ToDateTime(FechaFin);

            DataTable dt = null;
            if (IdFlujo == 1)
            {
                dt = (new wfiplib.admMesa()).Pruductividad_Mesa_Grafica_IdFlujo(1, 0, CalDesde.Date, CalHasta.Date, IdOrden);
            }
            else if (IdFlujo == 2)
            {
                dt = (new wfiplib.admMesa()).Pruductividad_Mesa_Grafica_IdFlujo(2, 3, CalDesde.Date, CalHasta.Date, IdOrden);
            }
            else if (IdFlujo == 4)
            {
                dt = (new wfiplib.admMesa()).Pruductividad_Mesa_Grafica_IdFlujo(4, 0, CalDesde.Date, CalHasta.Date, IdOrden);
            }

            /* LLENAR JSON PARA RETORNAR */
            ModelGraficaPorcentaje jsonObject = new ModelGraficaPorcentaje();
            jsonObject.tablaModels = new List<TablaModel>();
            jsonObject.tiempos = new List<PeriodosTiempo>();

            foreach (DataColumn column in dt.Columns)
            {
                jsonObject.tiempos.Add(new PeriodosTiempo()
                {
                    tiempo = column.ColumnName.ToString()
                });
            }
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<TablaDatoModel> tablaDatos = new List<TablaDatoModel>();
                    for (int i = 0; i <= dt.Columns.Count-1; i++)
                    {
                        tablaDatos.Add(new TablaDatoModel()
                        {
                            cantidad = row[i].ToString(),
                            YearCantidad = i
                        });
                    }
                    jsonObject.tablaModels.Add(new TablaModel()
                    {
                        label = row["NombreUsuario"].ToString(),
                        tablaDatos = tablaDatos
                    });
                }
            }
            
            return jsonObject;
        }

        [WebMethod]
        public static ConsultasDetalleMesa DetalleMesa(int IdOrden, int IdFlujo, string FechaIn, string FechaFin)
        {
            DateTime CalDesde = Convert.ToDateTime(FechaIn);
            DateTime CalHasta = Convert.ToDateTime(FechaFin);

            DataTable dt = null;
            if (IdFlujo == 1)
            {
                dt = (new wfiplib.admMesa()).Mesa_Productividad_Flujo_Mesa(1, 0, CalDesde.Date, CalHasta.Date, IdOrden);
            }
            else if (IdFlujo == 2)
            {
                dt = (new wfiplib.admMesa()).Mesa_Productividad_Flujo_Mesa(2, 3, CalDesde.Date, CalHasta.Date, IdOrden);
            }
            else if (IdFlujo == 4)
            {
                dt = (new wfiplib.admMesa()).Mesa_Productividad_Flujo_Mesa(4, 0, CalDesde.Date, CalHasta.Date, IdOrden);
            }

            /* LLENAR JSON PARA RETORNAR */
            ConsultasDetalleMesa jsonObject = new ConsultasDetalleMesa();
            jsonObject.consulta = new List<DetalleMesa>();
            foreach (DataRow row in dt.Rows)
            {
                jsonObject.consulta.Add(new DetalleMesa()
                {
                    Nombre = row["NOMBRE"].ToString(),
                    Total = Convert.ToInt32(row["TOTAL"].ToString()),
                    Reingreso = Convert.ToInt32(row["REINGRESOS"].ToString()),
                    Calidad = row["CALIDAD"].ToString(),
                    Productividad = row["PRODUCTIVIDAD"].ToString(),
                });
            }
            
            return jsonObject;
        }
    }

    public class DetalleMesa
    {
        public string Nombre { get; set; }
        public int Total { get; set; }
        public int Reingreso { get; set; }
        public string Calidad { get; set; }
        public string Productividad { get; set; }
    }

    public class ConsultasDetalleMesa
    {
        public List<DetalleMesa> consulta { get; set; }
    }

    public class PeriodosTiempo
    {
        public string tiempo { get; set; }
    }

    public class TablaDatoModel
    {
        public string cantidad { get; set; }
        public int YearCantidad { get; set; }
    }

    public class TablaModel
    {
        public int Id { get; set; }
        public string label { get; set; }
        public List<TablaDatoModel> tablaDatos { get; set; }
    }

    public class ModelGraficaPorcentaje
    {
        public List<PeriodosTiempo> tiempos { get; set; }
        public List<TablaModel> tablaModels { get; set; }
    }
}