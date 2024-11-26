using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Text;
using OfficeOpenXml;

namespace wfip.promotoria
{
    public partial class ReporteColectividad : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admPolizasDetalle pd = new wfiplib.admPolizasDetalle();

        protected void Page_Init(object sender, EventArgs e)
        {
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void gvAgregado_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid1 = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.ColumnSpan = 26;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "1er. Período Vigencia 2017";
                HeaderCell1.BackColor = System.Drawing.Color.LightGray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "1er Trimestre 2018";
                HeaderCell1.BackColor = System.Drawing.Color.LightGray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "2do Trimestre 2018";
                HeaderCell1.BackColor = System.Drawing.Color.LightGray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "3er Trimestre 2018";
                HeaderCell1.BackColor = System.Drawing.Color.LightGray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "4to Trimestre 2018";
                HeaderCell1.BackColor = System.Drawing.Color.LightGray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "1er Trimestre 2019";
                HeaderCell1.BackColor = System.Drawing.Color.LightGray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Ultimo Período de Vigencia 2019";
                HeaderCell1.BackColor = System.Drawing.Color.LightGray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "1er. Período Vigencia 2017";
                HeaderCell1.BackColor = System.Drawing.Color.Gray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "1er Trimestre 2018";
                HeaderCell1.BackColor = System.Drawing.Color.Gray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "2do Trimestre 2018";
                HeaderCell1.BackColor = System.Drawing.Color.Gray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "3er Trimestre 2018";
                HeaderCell1.BackColor = System.Drawing.Color.Gray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "4to Trimestre 2018";
                HeaderCell1.BackColor = System.Drawing.Color.Gray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "1er Trimestre 2019";
                HeaderCell1.BackColor = System.Drawing.Color.Gray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Ultimo Período de Vigencia 2019";
                HeaderCell1.BackColor = System.Drawing.Color.Gray;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Prima Total por la Vigencia";
                HeaderCell1.BackColor = System.Drawing.Color.White;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 3;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                gvAgregado.Controls[0].Controls.AddAt(0, HeaderGridRow1);
            }
        }

        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            gvAgregado.AllowPaging = false;
            gvAgregado.DataSource = pd.SeleccionarPolizasColectividadExcel();
            gvAgregado.DataBind();
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.ClearHeaders();
            //response.ClearContent();
            //response.Charset = Encoding.UTF8.WebName;
            //response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
            //response.AddHeader("Content-Type", "application/Excel");
            //response.ContentType = "application/vnd.xlsx";
            //using (StringWriter sw = new StringWriter())
            //{
            //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            //    {
            //        GridView gridView = new GridView();
            //        gridView = gvAgregado;
            //        gridView.DataSource = pd.SeleccionarPolizasColectividadExcel();
            //        gridView.DataBind();
            //        gridView.RenderControl(htw);
            //        response.Write(sw.ToString());
            //        gridView.Dispose();
            //        response.End();
            //    }
            //}

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteDeColectividad.xls");
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvAgregado.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gvAgregado.HeaderRow.Cells)
                {
                    cell.BackColor = gvAgregado.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvAgregado.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvAgregado.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvAgregado.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvAgregado.RenderControl(hw);

                //Estilo para formatear números a cadena
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

                
            }
        }

        protected void BtnGenerarExcel_Click(object sender, EventArgs e)
        {
            gvAgregado.DataSource = pd.SeleccionarPolizasColectividadExcel();
            gvAgregado.DataBind();
            ProcesarEncabezadoFijo(gvAgregado);
            BtnExportar.Visible = true;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifica que el control esté renderizado */
        }

        protected void ProcesarEncabezadoFijo(GridView grid)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Key", "<script>HacerEncabezadoEstatico('" + grid.ClientID + "', 600, 1170, 120, true); </script>", false);
        }

        protected void gvAgregado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAgregado.PageIndex = e.NewPageIndex;
            gvAgregado.DataSource = pd.SeleccionarPolizasColectividadExcel();            
            gvAgregado.DataBind();
            ProcesarEncabezadoFijo(gvAgregado);
        }
    }
}