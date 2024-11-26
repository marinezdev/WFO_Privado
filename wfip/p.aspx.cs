using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using Microsoft.Reporting.WebForms;

namespace wfip

{
    public partial class p : System.Web.UI.Page
    {
        wfiplib.admAseguradosTitulares at = new wfiplib.admAseguradosTitulares();
        Mensajes mensajes = new Mensajes();
        //ServiciosDeCorreo.ServicioCorreoClient sc = new ServiciosDeCorreo.ServicioCorreoClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarChart();
                //sc.ProcesaCorreo("ruben.marines@asae.com.mx");
                
            }
        }


        protected void gvArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string carpeta = "DocsUpPotenciacion/";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowObject = (DataRowView)e.Row.DataItem;
                HyperLink hlkNombresarchivos = (HyperLink)e.Row.FindControl("hlkNombresArchivos");

                hlkNombresarchivos.NavigateUrl = carpeta + rowObject["Nombre"].ToString();
            }
        }

        protected void lnkQuitarArchivo_Click(object sender, EventArgs e)
        {
            LinkButton imgbtn1 = sender as LinkButton;
            GridViewRow row1 = imgbtn1.NamingContainer as GridViewRow;

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["archivosAgregados"];
            dt.Rows.RemoveAt(row1.RowIndex);
            ViewState["archivosAgregados"] = dt;

            ViewState["archivoTemporal"] = null;
            ViewState["archivoTemporal"] = ViewState["archivosAgregados"];

            string carpeta = "DocsUpPotenciacion/";
            carpeta = Server.MapPath(carpeta);
            File.Delete(carpeta + imgbtn1.CommandArgument);
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //Columnas para la nueva tabla
            dt.Columns.Add("Nombre");
            dt.Columns.Add("IdRegistro");

            //Fila para la tabla
            string directorioArchivosTemporales = Server.MapPath("~/DocsUpPotenciacion/");
            DirectoryInfo d = new DirectoryInfo(directorioArchivosTemporales);

            foreach (var file in d.GetFiles("*.*"))
            {
                dt.Rows.Add(file.Name, "0");
            }

            gvArchivos.DataSource = dt;
            gvArchivos.DataBind();

            //string[] files = Directory.GetFiles(directorioArchivosTemporales);

            ////eliminar los archivos que ya no se usarán
            //foreach (string s in files)
            //{
            //    File.Delete(s);
            //}

        }

        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string fileNametoupload = Server.MapPath("~/DocsUpPotenciacion/") + e.FileName.ToString();
            AjaxFileUpload1.SaveAs(fileNametoupload);
        }

        protected void AjaxFileUpload1_UploadCompleteAll(object sender, AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs e)
        {
            //DataTable dt = new DataTable();
            ////Columnas para la nueva tabla
            //dt.Columns.Add("Nombre");
            //dt.Columns.Add("IdRegistro");

            ////Fila para la tabla
            //string directorioArchivosTemporales = Server.MapPath("~/DocsUpPotenciacion/");
            //DirectoryInfo d = new DirectoryInfo(directorioArchivosTemporales);

            //foreach (var file in d.GetFiles("*.*"))
            //{
            //    dt.Rows.Add(file.Name, "0");
            //}

            //gvArchivos.DataSource = dt;
            //gvArchivos.DataBind();

            //string[] files = Directory.GetFiles(directorioArchivosTemporales);

            ////eliminar los archivos que ya no se usarán
            //foreach (string s in files)
            //{
            //    File.Delete(s);
            //}

            
        }

        protected void BtnHTMLEditorGuardar_Click(object sender, EventArgs e)
        {
            lblTextoEditor.Text = txtBody.Text;
        }

        protected void LlenarChart()
        {
            ChartUsuarios.ChartAreas["Roles"].AxisX.Interval = 1;
            ChartUsuarios.ChartAreas["Roles"].AxisY.Interval = 50;

            ChartUsuarios.ChartAreas["Roles"].AxisX.LabelStyle.Enabled = true;
            

            wfiplib.admCredencial cred = new wfiplib.admCredencial();
            ChartUsuarios.DataSource = cred.UsuariosParaChart();

            Series serieTotales = ChartUsuarios.Series.Add("Roles");
            serieTotales.ChartArea = "Roles";
            serieTotales.Font = new Font("Arial", 6.5F);
            serieTotales.ChartType = SeriesChartType.Column;
            serieTotales.IsValueShownAsLabel = true;
            serieTotales.XValueMember = "idrol";
            serieTotales.YValueMembers = "RolesPorUsuario";
            serieTotales.CustomProperties = "ShowMarkerLines=true";
            serieTotales.PostBackValue = "#VALX";
            serieTotales.IsValueShownAsLabel = true;

            ChartUsuarios.DataBind();

            foreach (DataRow row in cred.UsuariosParaChart().Rows)
            {
                PieChart1.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                {
                    Category = row["RolesPorUsuario"].ToString(),
                    Data = Convert.ToDecimal(row["idrol"])
                });
            }

        }

        protected void MostrarReporte()
        {
            wfiplib.admCredencial cred = new wfiplib.admCredencial();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Reportes/Report1.rdlc");
            ReportDataSource rds = new ReportDataSource("Usuarios", cred.UsuariosParaChart());
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);

        }

        protected void ChartUsuarios_Click(object sender, ImageMapEventArgs e)
        {

        }
    }
}