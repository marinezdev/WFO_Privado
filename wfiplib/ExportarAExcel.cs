using ClosedXML.Excel;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace wfiplib
{
    public class ExportarAExcel
    {
        /// <summary>
        /// Procesa los datos de un dataset para exportarlos a excel
        /// </summary>
        /// <param name="Pagina">this</param>
        /// <param name="dataset">Dataset con las tablas que procesará</param>
        public void ExportarDataSetAExcel(Page Pagina, DataSet dataset)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataset);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                for (int i = 1; i < dataset.Tables.Count; i++)
                {
                    wb.Worksheets.Worksheet(i);
                }

                Pagina.Response.Clear();
                Pagina.Response.Buffer = true;
                Pagina.Response.Charset = "";
                Pagina.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Pagina.Response.AddHeader("content-disposition", "attachment;filename=ASAE_Consultores.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Pagina.Response.OutputStream);
                    Pagina.Response.Flush();
                    Pagina.Response.End();
                }
            }
        }

    }
}
