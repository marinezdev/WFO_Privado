using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.Data;
using System.IO;
using static iTextSharp.text.Font;
using System.Globalization;
using System.Web;
using System.Web.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace wfiplib
{
    public class admCartas
    {
        string strAgente = "";
        string strPromotoria = "";

        public void CartaHoldPDF(int Id, HttpResponse Response, int CreaPDF, int Credencial)
        {
            DataTable motivos = new DataTable();
            DataTable motivosObs = new DataTable();
            CultureInfo culture = new CultureInfo("es-MX");
            wfiplib.admTramite admTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG admEmision = new wfiplib.admEmisionVG();
            wfiplib.tramiteP oTramite = admTramite.carga(Id);
            wfiplib.EmisionVG oEmision = admEmision.carga(Id);

            string strFechaCarta = "Ciudad de México, ";
            string strTipoTramite = "";
            string strRamo = "";
            string strNegocio = "";
            string strTitular = "";

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    strTipoTramite = "Emisión.";
                    strRamo = "Gastos Médicos Mayores.";
                    strNegocio = "Nuevo Negocio.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    strTipoTramite = "Emisión.";
                    strRamo = "Conversiones.";
                    strNegocio = "Nuevo Negocio.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    strRamo = "Vida Individual Privado.";
                    strTipoTramite = "Emisión";
                    strNegocio = "Nuevo Negocio.";
                    break;

                default:
                    strTipoTramite = "";
                    strRamo = "";
                    strNegocio = "";
                    break;
            }

            strFechaCarta = (new wfiplib.Reportes()).MotivosRechazoFecha(Id);
            strFechaCarta = "Ciudad de México, " + (Convert.ToDateTime(strFechaCarta)).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            motivos = (new wfiplib.Reportes()).MotivosRechazo(Id);
            motivosObs = (new wfiplib.Reportes()).MotivosRechazoObservaciones(Id);
            strAgente = oTramite.AgenteClave.ToString() + " - " + oTramite.AgenteNombre;
            strPromotoria = oTramite.PromotoriaClave.ToString() + " - " + oTramite.PromotoriaNombre;
            //strNegocio = oTramite.FolioCompuesto;
            strNegocio = "Póliza Nueva";

            if (oEmision.TitularNombre.Length > 5)
            {
                strTitular = oEmision.TitularNombre + " " + oEmision.TitularApPat + " " + oEmision.TitularApMat;
            }
            else
            {
                strTitular = oEmision.Nombre + " " + oEmision.ApPaterno + " " + oEmision.ApMaterno;
            }


            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);

            var output = (dynamic)null;

            // VALIDAR SI ES CREADO O ALMACENADO
            if (CreaPDF == 1)
            {
                //string directorioTemporal = Server.MapPath("~") + "\\Cartas\\";
                string directorioTemporal = HttpContext.Current.Server.MapPath("~") + "\\Cartas\\";
                output = new FileStream(directorioTemporal + "CartaHold_" + Credencial + "_" + oTramite.FolioCompuesto + ".pdf", FileMode.Create);

            }
            else
            {
                output = new MemoryStream();
            }

            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;
            Font Text = new Font(FontFamily.HELVETICA, 11, Font.NORMAL, BaseColor.BLACK);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.BLACK);

            // COLOCA ENCABEZADO Y PIE DE PAGINA
            writer.PageEvent = new PDFFooter("");

            document.Open();

            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;


            cell = new PdfPCell(new Phrase(strFechaCarta, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 50f;
            cell.MinimumHeight = 20f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 45f;
            cell.MinimumHeight = 45f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Tipo de Trámite: ", Negrita));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strTipoTramite, Text));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Ramo: ", Negrita));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strRamo, Text));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strNegocio, Negrita));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Estimado (a): " + strTitular, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("En respuesta a tu solicitud de contratación de tu seguro te informamos que para poder continuar con el análisis requerimos que nos compartas la siguiente información: ", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // Motivos de Suspencion
            for (int intIndice = 0; intIndice < (motivos.Rows.Count); intIndice++)
            {
                cell = new PdfPCell(new Phrase("    •  " + motivos.Rows[intIndice]["motivoRechazo"].ToString(), Text));
                cell.Colspan = 10;
                cell.FixedHeight = 20f;
                cell.MinimumHeight = 20f;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            if (motivosObs.Rows.Count > 0)
            {
                cell = new PdfPCell(new Phrase("    -  OBSERVACIONES.", Negrita));
                cell.Colspan = 10;
                cell.FixedHeight = 25f;
                cell.MinimumHeight = 25f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;

                // Motivos de Suspención Observaciones
                for (int intIndice = 0; intIndice < (motivosObs.Rows.Count); intIndice++)
                {
                    cell = new PdfPCell(new Phrase("    •  " + motivosObs.Rows[intIndice]["ObservacionPublica"].ToString(), Text));
                    cell.Colspan = 10;
                    cell.FixedHeight = 20f;
                    cell.MinimumHeight = 20f;
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.Border = 0;
                    table.AddCell(cell);
                    cell = null;
                }
            }


            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Te pedimos hacernos llegar la información y/o respuesta a través de tu agente de seguros, en un máximo de 15 días hábiles, para evitar la cancelación de este trámite.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Sin más por el momento, nos reiteramos a tus órdenes.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;


            cell = new PdfPCell(new Phrase("Atentamente.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("METLIFE, MEXICO S.A. DE C.V.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            string directorioImagen = HttpContext.Current.Server.MapPath("~") + "\\img\\";

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(directorioImagen + "firma.png");
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 10; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.MinimumHeight = 125f;
            imageCell.FixedHeight = 125f;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(imageCell);
            imageCell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Agente: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strAgente, Text));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Promotoría: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strPromotoria, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;


            document.Add(table);
            document.Close();

            // COLOCAR O QUITAR DEPENDIENDO LA PETICION
            if (CreaPDF == 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }

            culture = null;
            motivos = null;
            Text = null;
            TextFooter = null;
            Negrita = null;
            cell = null;
            writer = null;
            output = null;
            document = null;

            /*
            DataTable motivos = new DataTable();
            DataTable motivosObs = new DataTable();
            CultureInfo culture = new CultureInfo("es-MX");
            wfiplib.admTramite admTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG admEmision = new wfiplib.admEmisionVG();
            wfiplib.tramiteP oTramite = admTramite.carga(Id);
            wfiplib.EmisionVG oEmision = admEmision.carga(Id);

            string strFechaCarta = "Ciudad de México, ";
            string strTipoTramite = "";
            string strRamo = "";
            string strNegocio = "";
            string strAgente = "";
            string strPromotoria = "";
            string strTitular = "";

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    strTipoTramite = "Emisión.";
                    strRamo = "Gastos Médicos Mayores. Individual Privado.";
                    strNegocio = "Nuevo Negocio.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    strRamo = "Vida. Individual Privado.";
                    strTipoTramite = "Emisión";
                    strNegocio = "Nuevo Negocio.";
                    break;

                default:
                    strTipoTramite = "";
                    strRamo = "";
                    strNegocio = "";
                    break;
            }

            strFechaCarta = (new wfiplib.Reportes()).MotivosRechazoFecha(Id);
            strFechaCarta = "Ciudad de México, " + (Convert.ToDateTime(strFechaCarta)).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            motivos = (new wfiplib.Reportes()).MotivosRechazo(Id);
            motivosObs = (new wfiplib.Reportes()).MotivosRechazoObservaciones(Id);
            strAgente = oTramite.AgenteClave.ToString() + " - " + oTramite.AgenteNombre;
            strPromotoria = oTramite.PromotoriaClave.ToString() + " - " + oTramite.PromotoriaNombre;
            strNegocio = oTramite.FolioCompuesto;
            if (oEmision.TitularNombre.Length > 5)
            {
                strTitular = oEmision.TitularNombre + " " + oEmision.TitularApPat + " " + oEmision.TitularApMat;
            }
            else
            {
                strTitular = oEmision.Nombre + " " + oEmision.ApPaterno + " " + oEmision.ApMaterno;
            }

            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);

            var output = (dynamic)null;

            // VALIDAR SI ES CREADO O ALMACENADO
            if (CreaPDF == 1)
            {
                //string directorioTemporal = Server.MapPath("~") + "\\Cartas\\";
                string directorioTemporal = HttpContext.Current.Server.MapPath("~") + "\\Cartas\\";
                output = new FileStream(directorioTemporal + "CartaHold_" + Credencial + "_" + oTramite.FolioCompuesto + ".pdf", FileMode.Create);
               
            }
            else
            {
                output = new MemoryStream();
            }
            
            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;
            Font Text = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);

            // COLOCA ENCABEZADO Y PIE DE PAGINA
            writer.PageEvent = new PDFFooter();

            document.Open();

            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 40f;
            cell.MinimumHeight = 20F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strFechaCarta, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 50f;
            cell.MinimumHeight = 20f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // ESPACIO EN BLANCO
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 20F;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Tipo de Trámite: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strTipoTramite, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Ramo: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strRamo, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strNegocio, Negrita));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Apreciable " + strTitular, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("En atención a tu solicitud, te indicamos que para poder continuar con el análisis, agradeceremos nos proporciones la siguiente información: ", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // Motivos de Hold
            for (int intIndice = 0; intIndice < (motivos.Rows.Count); intIndice++)
            {
                cell = new PdfPCell(new Phrase("   *   " + motivos.Rows[intIndice]["motivoRechazo"].ToString(), Text));
                cell.Colspan = 10;
                cell.FixedHeight = 20f;
                cell.MinimumHeight = 20f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("   >>> OBSERVACIONES.", Negrita));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // Motivos de Suspención Observaciones
            for (int intIndice = 0; intIndice < (motivosObs.Rows.Count); intIndice++)
            {
                cell = new PdfPCell(new Phrase("   >   " + motivosObs.Rows[intIndice]["ObservacionPublica"].ToString(), Text));
                cell.Colspan = 10;
                cell.FixedHeight = 20f;
                cell.MinimumHeight = 20f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Para poder dar continuidad a tu trámite te sugerimos hacer llegar tu información y/o respuesta a la presente en un máximo de 24 horas para evitar la suspensión de este folio.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Agradeceremos de antemano tu atención, quedamos a tus órdenes.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Atentamente.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("METLIFE, MEXICO S.A. DE C.V.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strAgente, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strPromotoria, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            document.Add(table);
            document.Close();

            // COLOCAR O QUITAR DEPENDIENDO LA PETICION
            if (CreaPDF == 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }
            
            culture = null;
            motivos = null;
            Text = null;
            TextFooter = null;
            Negrita = null;
            cell = null;
            writer = null;
            output = null;
            document = null;
            */
        }

        public void CartaRechazoPDF(int Id, HttpResponse Response, int CreaPDF, int Credencial)
        {
            DataTable motivos = new DataTable();
            DataTable motivosObs = new DataTable();

            CultureInfo culture = new CultureInfo("es-MX");
            wfiplib.admTramite admTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG admEmision = new wfiplib.admEmisionVG();
            wfiplib.tramiteP oTramite = admTramite.carga(Id);
            wfiplib.EmisionVG oEmision = admEmision.carga(Id);

            string strFechaCarta = "Ciudad de México, ";
            string strTipoTramite = "";
            string strRamo = "";
            string strTramite = "";
            string strFolio = "";
        
            string strNombre = "";
            string srtFechaRegistro = "";

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    strTipoTramite = "Emisión.";
                    strRamo = "GMM";
                    strTramite = "Gastos Médicos Mayores";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    strTipoTramite = "Emisión.";
                    strRamo = "Conversion";
                    strTramite = "Gastos Médicos Mayores";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    strRamo = "Vida";
                    strTipoTramite = "Emisión";
                    strTramite = "Vida Individual";
                    break;

                default:
                    strTipoTramite = "";
                    strRamo = "";
                    break;
            }

            
            strFechaCarta = (new wfiplib.Reportes()).MotivosRechazoFecha(Id);
            if (strFechaCarta.Length > 0)
            {

            }
            else
            {
                strFechaCarta = (new wfiplib.Reportes()).MotivosRechazoFechaRechazado(Id);
            }

            //strFechaCarta = "Ciudad de México, " + (Convert.ToDateTime(strFechaCarta)).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            strFechaCarta = (Convert.ToDateTime(strFechaCarta)).ToString("dd/MM/yyyy");
            motivos = (new wfiplib.Reportes()).MotivosRechazo(Id);
            motivosObs = (new wfiplib.Reportes()).MotivosRechazoObservaciones(Id);

            
            strAgente = oTramite.AgenteClave.ToString() + " - " + oTramite.AgenteNombre;
            strPromotoria = oTramite.PromotoriaClave.ToString() + " - " + oTramite.PromotoriaNombre;
            strFolio = oTramite.FolioCompuesto.ToString();
            srtFechaRegistro = oTramite.FechaRegistro.ToString();
            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);

            var output = (dynamic)null;

            // VALIDAR SI ES CREADO O ALMACENADO
            if (CreaPDF == 1)
            {
                //string directorioTemporal = Server.MapPath("~") + "\\Cartas\\";
                string directorioTemporal = HttpContext.Current.Server.MapPath("~") + "\\Cartas\\";
                output = new FileStream(directorioTemporal + "CartaRechazo_" + Credencial + "_" + oTramite.FolioCompuesto + ".pdf", FileMode.Create);
            }
            else
            {
                output = new MemoryStream();
            }

            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;
            Font Text = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
            Font Title = new Font(FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.BLUE);
            Font SubTitle = new Font(FontFamily.HELVETICA, 17, Font.BOLD, BaseColor.BLACK);

            writer.PageEvent = new PDFFooter(strAgente + "|" + strPromotoria);
            document.Open();
            
            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;

            cell = new PdfPCell(new Phrase(strFechaCarta, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Respuesta a solicitud de Seguro", Title));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Trámite: Emisión", SubTitle));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Ramo: ", Text));
            cell.Colspan = 1;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strRamo + " / Individual Privado", Negrita));
            cell.Colspan = 4;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("No. de solicitud: ", Text));
            cell.Colspan = 3;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strFolio, Negrita));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            if (oEmision.TitularNombre.Length > 5)
            {
                strNombre = oEmision.TitularNombre + " " + oEmision.TitularApPat + " " + oEmision.TitularApMat;
            }
            else
            {
                strNombre = oEmision.Nombre + " " + oEmision.ApPaterno + " " + oEmision.ApMaterno;
            }

            cell = new PdfPCell(new Phrase("Estimado(a): " + strNombre, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Queremos agradecer el interés para la contratación del Seguro de " + strTramite + " que recibimos el día " + srtFechaRegistro.Substring(0, 10) + ".", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 35f;
            cell.MinimumHeight = 35f;
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Después de analizar con detenimiento la solicitud, lamentamos informar que no nos será posible proceder con la contratación del seguro.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 35f;
            cell.MinimumHeight = 35f;
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Atentamente.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            string directorioImagen = HttpContext.Current.Server.MapPath("~") + "\\img\\";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(directorioImagen + "firma.png");
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 10; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.MinimumHeight = 125f;
            imageCell.FixedHeight = 125f;
            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(imageCell);
            imageCell = null;

            cell = new PdfPCell(new Phrase("METLIFE, MEXICO S.A. DE C.V.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            document.Add(table);
            document.Close();

            // COLOCAR O QUITAR DEPENDIENDO LA PETICION
            if (CreaPDF == 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }

            culture = null;
            motivos = null;
            Text = null;
            TextFooter = null;
            Negrita = null;
            cell = null;
            writer = null;
            output = null;
            document = null;
        }

        public void CartaSuspendidoPDF(int Id, HttpResponse Response, int CreaPDF, int Credencial)
        {
            DataTable motivos = new DataTable();
            DataTable motivosObs = new DataTable();
            CultureInfo culture = new CultureInfo("es-MX");
            wfiplib.admTramite admTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG admEmision = new wfiplib.admEmisionVG();
            wfiplib.tramiteP oTramite = admTramite.carga(Id);
            wfiplib.EmisionVG oEmision = admEmision.carga(Id);

            string strFechaCarta = "Ciudad de México, ";
            string strTipoTramite = "";
            string strRamo = "";
            string strNegocio = "";
            string strTitular = "";

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    strTipoTramite = "Emisión.";
                    strRamo = "Gastos Médicos Mayores";
                    strNegocio = "Nuevo Negocio.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    strTipoTramite = "Emisión.";
                    strRamo = "Conversiones";
                    strNegocio = "Nuevo Negocio.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    strRamo = "Vida";
                    strTipoTramite = "Emisión";
                    strNegocio = "Nuevo Negocio";
                    break;

                default:
                    strTipoTramite = "";
                    strRamo = "";
                    strNegocio = "";
                    break;
            }

            strFechaCarta = (new wfiplib.Reportes()).MotivosRechazoFecha(Id);
            //strFechaCarta = "Ciudad de México, " + (Convert.ToDateTime(strFechaCarta)).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            strFechaCarta = (Convert.ToDateTime(strFechaCarta)).ToString("dd/MM/yyyy");

            motivos = (new wfiplib.Reportes()).MotivosRechazo(Id);
            motivosObs = (new wfiplib.Reportes()).MotivosRechazoObservaciones(Id);
            strAgente = oTramite.AgenteClave.ToString() + " - " + oTramite.AgenteNombre;
            strPromotoria = oTramite.PromotoriaClave.ToString() + " - " + oTramite.PromotoriaNombre;
            //strNegocio = oTramite.FolioCompuesto;
            strNegocio = "Póliza Nueva";

            if (oEmision.TitularNombre.Length > 5)
            {
                strTitular = oEmision.TitularNombre + " " + oEmision.TitularApPat + " " + oEmision.TitularApMat;
            }
            else
            {
                strTitular = oEmision.Nombre + " " + oEmision.ApPaterno + " " + oEmision.ApMaterno;
            }


            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);

            var output = (dynamic)null;

            // VALIDAR SI ES CREADO O ALMACENADO
            if (CreaPDF == 1)
            {
                //string directorioTemporal = Server.MapPath("~") + "\\Cartas\\";
                string directorioTemporal = HttpContext.Current.Server.MapPath("~") + "\\Cartas\\";
                output = new FileStream(directorioTemporal + "CartaSuspendido_" + Credencial + "_" + oTramite.FolioCompuesto + ".pdf", FileMode.Create);

            }
            else
            {
                output = new MemoryStream();
            }

            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;
            Font Text = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
            Font TextResaltado = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLUE);
            Font SubTextResaltado = new Font(FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLUE);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
            Font Title = new Font(FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.BLUE);
            Font SubTitle = new Font(FontFamily.HELVETICA, 17, Font.BOLD, BaseColor.BLACK);

            writer.PageEvent = new PDFFooter(strAgente + "|" + strPromotoria);
            document.Open();

            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;

            cell = new PdfPCell(new Phrase(strFechaCarta, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Respuesta a solicitud de Seguro", Title));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Trámite: Emisión", SubTitle));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Ramo: ", Text));
            cell.Colspan = 1;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strRamo + " / Individual Privado", Negrita));
            cell.Colspan = 5;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("No. de solicitud: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(oTramite.FolioCompuesto, Negrita));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Estimado(a): " + strTitular, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("En respuesta a la solicitud de contratación del Seguro recibida el día " + oTramite.FechaRegistro.ToString("dd/MM/yyyy") + ", te informamos que, para continuar con el análisis necesario, requerimos que nos compartas la siguiente información:", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            // Motivos de Suspencion
            for (int intIndice = 0; intIndice < (motivos.Rows.Count); intIndice++)
            {
                cell = new PdfPCell(new Phrase("    •  " + motivos.Rows[intIndice]["motivoRechazo"].ToString(), TextResaltado));
                cell.Colspan = 10;
                cell.FixedHeight = 20f;
                cell.MinimumHeight = 20f;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            if (motivosObs.Rows.Count > 0)
            {
                iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance("c:\\search1.png");
                PdfPCell imageCell2 = new PdfPCell(jpg2);
                imageCell2.Colspan = 1; // either 1 if you need to insert one cell
                imageCell2.Border = 0;
                imageCell2.FixedHeight = 20f;
                imageCell2.MinimumHeight = 20f;
                imageCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                imageCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(imageCell2);
                
                cell = new PdfPCell(new Phrase("OBSERVACIONES.", Negrita));
                cell.Colspan = 9;
                cell.FixedHeight = 20f;
                cell.MinimumHeight = 20f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;

                // Motivos de Suspención Observaciones
                for (int intIndice = 0; intIndice < (motivosObs.Rows.Count); intIndice++)
                {
                    cell = new PdfPCell(new Phrase("    •  " + motivosObs.Rows[intIndice]["ObservacionPublica"].ToString(), Text));
                    cell.Colspan = 10;
                    cell.FixedHeight = 15f;
                    cell.MinimumHeight = 15f;
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.Border = 0;
                    table.AddCell(cell);
                    cell = null;
                }
            }

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Importante:", SubTextResaltado));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            //cell = new PdfPCell(new Phrase("    -     Como referencia se puede utilizar el formato de informe médico que se encuentra en ...", Text));
            //cell.Colspan = 10;
            //cell.FixedHeight = 15f;
            //cell.MinimumHeight = 15f;
            //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            cell = new PdfPCell(new Phrase("    -     Favor de enviar la información en un máximo de 15 días hábiles, para evitar la cancelación de este trámite.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;



            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Atentamente.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            string directorioImagen = HttpContext.Current.Server.MapPath("~") + "\\img\\";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(directorioImagen + "firma.png");
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 10; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.MinimumHeight = 125f;
            imageCell.FixedHeight = 125f;
            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(imageCell);
            imageCell = null;

            cell = new PdfPCell(new Phrase("METLIFE, MEXICO S.A. DE C.V.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            
            
            //cell = new PdfPCell(new Phrase("Te pedimos hacernos llegar la información y/o respuesta a través de tu agente de seguros, en un máximo de 15 días hábiles, para evitar la cancelación de este trámite.", Text));
            //cell.Colspan = 10;
            //cell.FixedHeight = 30f;
            //cell.MinimumHeight = 30f;
            //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            //cell = new PdfPCell(new Phrase("", Text));
            //cell.Colspan = 10;
            //cell.FixedHeight = 25f;
            //cell.MinimumHeight = 25f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            //cell = new PdfPCell(new Phrase("Sin más por el momento, nos reiteramos a tus órdenes.", Text));
            //cell.Colspan = 10;
            //cell.FixedHeight = 25f;
            //cell.MinimumHeight = 25f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;


            //cell = new PdfPCell(new Phrase("Atentamente.", Text));
            //cell.Colspan = 10;
            //cell.FixedHeight = 25f;
            //cell.MinimumHeight = 25f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            //cell = new PdfPCell(new Phrase("METLIFE, MEXICO S.A. DE C.V.", Text));
            //cell.Colspan = 10;
            //cell.FixedHeight = 15f;
            //cell.MinimumHeight = 15f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            //string directorioImagen = HttpContext.Current.Server.MapPath("~") + "\\img\\";

            //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(directorioImagen + "firma.png");
            //PdfPCell imageCell = new PdfPCell(jpg);
            //imageCell.Colspan = 10; // either 1 if you need to insert one cell
            //imageCell.Border = 0;
            //imageCell.MinimumHeight = 125f;
            //imageCell.FixedHeight = 125f;
            //imageCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //table.AddCell(imageCell);
            //imageCell = null;

            //cell = new PdfPCell(new Phrase("", Text));
            //cell.Colspan = 10;
            //cell.FixedHeight = 15f;
            //cell.MinimumHeight = 15f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            //cell = new PdfPCell(new Phrase("Agente: ", Text));
            //cell.Colspan = 2;
            //cell.FixedHeight = 15f;
            //cell.MinimumHeight = 15f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            //cell = new PdfPCell(new Phrase(strAgente, Text));
            //cell.Colspan = 8;
            //cell.FixedHeight = 15f;
            //cell.MinimumHeight = 15f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            //cell = new PdfPCell(new Phrase("Promotoría: ", Text));
            //cell.Colspan = 2;
            //cell.FixedHeight = 15f;
            //cell.MinimumHeight = 15f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;

            //cell = new PdfPCell(new Phrase(strPromotoria, Text));
            //cell.Colspan = 10;
            //cell.FixedHeight = 15f;
            //cell.MinimumHeight = 15f;
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //table.AddCell(cell);
            //cell = null;


            document.Add(table);
            document.Close();

            // COLOCAR O QUITAR DEPENDIENDO LA PETICION
            if (CreaPDF == 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }

            culture = null;
            motivos = null;
            Text = null;
            TextFooter = null;
            Negrita = null;
            cell = null;
            writer = null;
            output = null;
            document = null;
        }

        public void CartaAceptadoPDF(int Id, HttpResponse Response, int CreaPDF, int Credencial)
        {
            DataTable motivos = new DataTable();
            CultureInfo culture = new CultureInfo("es-MX");
            wfiplib.admTramite admTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG admEmision = new wfiplib.admEmisionVG();
            wfiplib.tramiteP oTramite = admTramite.carga(Id);
            wfiplib.EmisionVG oEmision = admEmision.carga(Id);

            string strFechaCarta = "";
            string strFechaVigencia = "";
            string strTipoTramite = "";
            string strRamo = "";
#pragma warning disable CS0219 // La variable 'strNegocio' está asignada pero su valor nunca se usa
            string strNegocio = "";
#pragma warning restore CS0219 // La variable 'strNegocio' está asignada pero su valor nunca se usa
            string strProducto = "";
            string strTitular = "";
            string strDCN = "";

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    strTipoTramite = "Emisión.";
                    //strRamo = "Gastos Médicos Mayores. Individual Privado.";
                    strRamo = "Gastos Médicos Mayores.";
                    strNegocio = "Nuevo Negocio.";
                    strFechaVigencia = (new wfiplib.Reportes()).getVigencia(Id, "DatosSeleccion");
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    strTipoTramite = "Emisión.";
                    //strRamo = "Gastos Médicos Mayores. Individual Privado.";
                    strRamo = "Conversiones.";
                    strNegocio = "Nuevo Negocio.";
                    strFechaVigencia = (new wfiplib.Reportes()).getVigencia(Id, "DatosSeleccion");
                    break;


                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    strRamo = "Vida Individual Privado.";
                    strTipoTramite = "Emisión";
                    strNegocio = "Nuevo Negocio.";
                    strFechaVigencia = (new wfiplib.Reportes()).getVigencia(Id, "DatosVidaSeleccion");
                    strDCN = oTramite.kwik;
                    break;

                default:
                    strTipoTramite = "";
                    strRamo = "";
                    strNegocio = "";
                    break;
            }


            strProducto = (new wfiplib.Reportes()).getProducto(Id);
            strFechaCarta = "Ciudad de México, " + (oTramite.FechaTermino).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            strFechaVigencia = (oTramite.FechaTermino).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            //strFechaCarta = "" + (Convert.ToDateTime(strFechaVigencia)).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            motivos = (new wfiplib.Reportes()).MotivosRechazo(Id);
            strAgente = oTramite.AgenteClave.ToString() + " - " + oTramite.AgenteNombre;
            strPromotoria = oTramite.PromotoriaClave.ToString() + " - " + oTramite.PromotoriaNombre;

            if (oEmision.TitularNombre.Length > 5)
            {
                strTitular = oEmision.TitularNombre + " " + oEmision.TitularApPat + " " + oEmision.TitularApMat;
            }
            else
            {
                strTitular = oEmision.Nombre + " " + oEmision.ApPaterno + " " + oEmision.ApMaterno;
            }

            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);

            var output = (dynamic)null;

            // VALIDAR SI ES CREADO O ALMACENADO
            if (CreaPDF == 1)
            {
                //string directorioTemporal = Server.MapPath("~") + "\\Cartas\\";
                string directorioTemporal = HttpContext.Current.Server.MapPath("~") + "\\Cartas\\";
                output = new FileStream(directorioTemporal + "CartaAceptado_" + Credencial + "_" + oTramite.FolioCompuesto + ".pdf", FileMode.Create);

            }
            else
            {
                output = new MemoryStream();
            }

            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;
            Font Text = new Font(FontFamily.HELVETICA, 11, Font.NORMAL, BaseColor.BLACK);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.BLACK);

            // COLOCA ENCABEZADO Y PIE DE PAGINA
            writer.PageEvent = new PDFFooter("");
            document.Open();

            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;
            
        
            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strFechaCarta, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 50f;
            cell.MinimumHeight = 20f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 40f;
            cell.MinimumHeight = 40f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Tipo de Trámite: ", Negrita  ));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strTipoTramite, Text));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Ramo: ", Negrita));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strRamo, Text));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            if (strRamo == "Vida. Individual Privado.")
            {
                cell = new PdfPCell(new Phrase("DCN: ", Negrita));
                cell.Colspan = 2;
                cell.FixedHeight = 15f;
                cell.MinimumHeight = 15f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;

                cell = new PdfPCell(new Phrase(strDCN, Text ));
                cell.Colspan = 8;
                cell.FixedHeight = 15f;
                cell.MinimumHeight = 15f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }

            cell = new PdfPCell(new Phrase("Nuevo Negocio", Negrita));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Estimado (a): " + strTitular + ".", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("En respuesta a tu solicitud de contratación de seguro, te informamos que está ha sido exitosa y cuenta con los siguientes datos:", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Póliza:", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(oTramite.IdSisLegados.ToString(), Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Inicio de Vigencia:", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strFechaVigencia, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Producto:", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strProducto, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Sin más por el momento, nos reiteramos a tus órdenes.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Atentamente.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("METLIFE, MEXICO S.A. DE C.V.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;
            string directorioImagen = HttpContext.Current.Server.MapPath("~") + "\\img\\";

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(directorioImagen + "firma.png");
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 10; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.MinimumHeight = 125f;
            imageCell.FixedHeight = 125f;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(imageCell);
            imageCell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Agente: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strAgente, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Promotoría: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strPromotoria, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;


            document.Add(table);
            document.Close();

            // COLOCAR O QUITAR DEPENDIENDO LA PETICION
            if (CreaPDF == 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }

            culture = null;
            motivos = null;
            Text = null;
            TextFooter = null;
            Negrita = null;
            cell = null;
            writer = null;
            output = null;
            document = null;
        }

        public void CartaCancelaPDF(int Id, HttpResponse Response, int CreaPDF, int Credencial)
        {
            DataTable motivos = new DataTable();
            DataTable motivosObs = new DataTable();

            CultureInfo culture = new CultureInfo("es-MX");
            wfiplib.admTramite admTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG admEmision = new wfiplib.admEmisionVG();
            wfiplib.tramiteP oTramite = admTramite.carga(Id);
            wfiplib.EmisionVG oEmision = admEmision.carga(Id);

            string strFechaCarta = "Ciudad de México, ";
            string strTipoTramite = "";
            string strRamo = "";
            string strTramite = "";
            string strNegocio = "";
            string strNombre = "";

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    strTipoTramite = "Emisión.";
                    strRamo = "GMM. Individual Privado.";
                    strTramite = "Gastos Médicos Mayores";
                    strNegocio = "Póliza Nueva.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    strTipoTramite = "Emisión.";
                    strRamo = "Conversion. Individual Privado.";
                    strTramite = "Conversiones";
                    strNegocio = "Póliza Nueva.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    strRamo = "Vida. Individual Privado.";
                    strTipoTramite = "Emisión";
                    strTramite = "Vida Individual";
                    strNegocio = "Póliza Nueva.";
                    break;

                default:
                    strTipoTramite = "";
                    strRamo = "";
                    strNegocio = "";
                    break;
            }

            /*
            strFechaCarta = (new wfiplib.Reportes()).MotivosRechazoFechaRechazado(Id);
            strFechaCarta = "Ciudad de México, " + (Convert.ToDateTime(strFechaCarta)).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            //motivos = (new wfiplib.Reportes()).MotivosRechazo(Id);
            */

            strFechaCarta = (new wfiplib.Reportes()).MotivosRechazoFecha(Id);
            if (strFechaCarta.Length > 0)
            {

            }
            else
            {
                strFechaCarta = (new wfiplib.Reportes()).MotivosRechazoFechaRechazado(Id);
            }

            strFechaCarta = "Ciudad de México, " + (Convert.ToDateTime(strFechaCarta)).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            motivos = (new wfiplib.Reportes()).MotivosRechazo(Id);
            motivosObs = (new wfiplib.Reportes()).MotivosRechazoObservaciones(Id);

            strAgente = oTramite.AgenteClave.ToString() + " - " + oTramite.AgenteNombre;
            strPromotoria = oTramite.PromotoriaClave.ToString() + " - " + oTramite.PromotoriaNombre;

            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);

            var output = (dynamic)null;

            // VALIDAR SI ES CREADO O ALMACENADO
            if (CreaPDF == 1)
            {
                //string directorioTemporal = Server.MapPath("~") + "\\Cartas\\";
                string directorioTemporal = HttpContext.Current.Server.MapPath("~") + "\\Cartas\\";
                output = new FileStream(directorioTemporal + "CartaCancela_" + Credencial + "_" + oTramite.FolioCompuesto + ".pdf", FileMode.Create);

            }
            else
            {
                output = new MemoryStream();
            }

            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;
            Font Text = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);

            // COLOCA ENCABEZADO Y PIE DE PAGINA
            writer.PageEvent = new PDFFooter("");
            document.Open();

            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strFechaCarta, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 50f;
            cell.MinimumHeight = 20f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Tipo de Trámite: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strTipoTramite, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Ramo: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strRamo, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strNegocio, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            if (oEmision.TitularNombre.Length > 5)
            {
                strNombre = oEmision.TitularNombre + " " + oEmision.TitularApPat + " " + oEmision.TitularApMat;
            }
            else
            {
                strNombre = oEmision.Nombre + " " + oEmision.ApPaterno + " " + oEmision.ApMaterno;
            }

            cell = new PdfPCell(new Phrase("Apreciable. " + strNombre, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("En atención a tu solicitud de contratación del seguro de " + strTramite + " y con base en la información que nos proporcionaste, lamentamos comunicarte que después de revisar con detenimiento tu caso, no será posible proceder con la contratación del plan propuesto.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            /******* LO NUEVO ******/
            // Motivos de Suspencion
            if (motivos.Rows.Count > 0)
            {
                for (int intIndice = 0; intIndice < (motivos.Rows.Count); intIndice++)
                {
                    cell = new PdfPCell(new Phrase("   *   " + motivos.Rows[intIndice]["motivoRechazo"].ToString(), Text));
                    cell.Colspan = 10;
                    cell.FixedHeight = 20f;
                    cell.MinimumHeight = 20f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    table.AddCell(cell);
                    cell = null;
                }
            }

            /******* LO NUEVO ******/

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("De manera sincera agradecemos haber considero a nuestra empresa,  esperando poder servirte en un futuro.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Atentamente.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("METLIFE, MEXICO S.A. DE C.V.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strAgente, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strPromotoria, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            document.Add(table);
            document.Close();

            // COLOCAR O QUITAR DEPENDIENDO LA PETICION
            if (CreaPDF == 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }

            culture = null;
            motivos = null;
            Text = null;
            TextFooter = null;
            Negrita = null;
            cell = null;
            writer = null;
            output = null;
            document = null;
        }

        public void CartaSuspencionCMPDF(int Id, HttpResponse Response, int CreaPDF, int Credencial)
        {
            DataTable motivos = new DataTable();
            CultureInfo culture = new CultureInfo("es-MX");
            wfiplib.admTramite admTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG admEmision = new wfiplib.admEmisionVG();
            wfiplib.tramiteP oTramite = admTramite.carga(Id);
            wfiplib.EmisionVG oEmision = admEmision.carga(Id);

            string strFechaCarta = "Ciudad de México, ";
            string strTipoTramite = "";
            string strRamo = "";
#pragma warning disable CS0219 // La variable 'strNegocio' está asignada pero su valor nunca se usa
            string strNegocio = "";
#pragma warning restore CS0219 // La variable 'strNegocio' está asignada pero su valor nunca se usa

            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    strTipoTramite = "Emisión.";
                    strRamo = "Gastos Médicos Mayores. Individual Privado.";
                    strNegocio = "Nuevo Negocio.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    strTipoTramite = "Emisión.";
                    strRamo = "Conversion. Individual Privado.";
                    strNegocio = "Nuevo Negocio.";
                    break;

                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    strRamo = "Vida. Individual Privado.";
                    strTipoTramite = "Emisión";
                    strNegocio = "Nuevo Negocio.";
                    break;

                default:
                    strTipoTramite = "";
                    strRamo = "";
                    strNegocio = "";
                    break;
            }

            string strObservaciones = (new wfiplib.Reportes()).getSuspencionObservacion(oTramite.Id, oTramite.IdFlujo);
            string strFechaGeneraSuspencion = (new wfiplib.Reportes()).getFechaSuspencion(oTramite.Id, oTramite.IdFlujo);
            strFechaCarta = "Ciudad de México, " + (Convert.ToDateTime(strFechaGeneraSuspencion)).ToString(culture.DateTimeFormat.LongDatePattern, culture);
            strAgente = oTramite.AgenteClave.ToString() + " - " + oTramite.AgenteNombre;
            strPromotoria = oTramite.PromotoriaClave.ToString() + " - " + oTramite.PromotoriaNombre;


            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);

            var output = (dynamic)null;

            // VALIDAR SI ES CREADO O ALMACENADO
            if (CreaPDF == 1)
            {
                //string directorioTemporal = Server.MapPath("~") + "\\Cartas\\";
                string directorioTemporal = HttpContext.Current.Server.MapPath("~") + "\\Cartas\\";
                output = new FileStream(directorioTemporal + "CartaSuspencionCM_" + Credencial + "_" + oTramite.FolioCompuesto + ".pdf", FileMode.Create);

            }
            else
            {
                output = new MemoryStream();
            }

            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell = null;
            Font Text = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);

            // COLOCA ENCABEZADO Y PIE DE PAGINA
            writer.PageEvent = new PDFFooter("");
            document.Open();

            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;
            
            cell = new PdfPCell(new Phrase(strFechaCarta, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 50f;
            cell.MinimumHeight = 20f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Número de Trámite: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(oTramite.FolioCompuesto, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Tipo de Trámite: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strTipoTramite, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Ramo: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(strRamo, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Ref. Solicitud No: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase(oEmision.NumeroOrden, Negrita));
            cell.Colspan = 8;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Nombre: ", Text));
            cell.Colspan = 2;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            if (oEmision.TitularNombre.Length > 5)
            {
                cell = new PdfPCell(new Phrase(oEmision.TitularNombre + " " + oEmision.TitularApPat + " " + oEmision.TitularApMat, Negrita));
                cell.Colspan = 8;
                cell.FixedHeight = 15f;
                cell.MinimumHeight = 15f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }
            else
            {
                cell = new PdfPCell(new Phrase(oEmision.Nombre + " " + oEmision.ApPaterno + " " + oEmision.ApMaterno, Negrita));
                cell.Colspan = 8;
                cell.FixedHeight = 15f;
                cell.MinimumHeight = 15f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("A nombre de MetLife deseamos agradecer la solicitud de contración del seguro de Vida que nos hiciste llegar el día " + (Convert.ToDateTime(strFechaGeneraSuspencion)).ToString(culture.DateTimeFormat.ShortDatePattern, culture) + " con núemro de trámite " + oTramite.FolioCompuesto + ".", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Con el fin de dar atención personal y poder brindarte el plan con las características que nos solicitaste, agradeceremos sirvas proporcionarnos la siguiten información: ", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            if (strObservaciones.Length > 5)
            {
                cell = new PdfPCell(new Phrase("> OBSERVACIONES.", Negrita));
                cell.Colspan = 10;
                cell.FixedHeight = 30f;
                cell.MinimumHeight = 30f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;

                cell = new PdfPCell(new Phrase("   >>> " + strObservaciones, Text));
                cell.Colspan = 10;
                cell.FixedHeight = 30f;
                cell.MinimumHeight = 30f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                table.AddCell(cell);
                cell = null;
            }


            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Sabemos que la tranquilidad y protección es muy importante. Para responder en tiempo y forma a tu solicitud, te pedimos nos hagas llegar tu información dentro de los siguientes 15 días hábiles.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 30f;
            cell.MinimumHeight = 30f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Atentamente.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 25f;
            cell.MinimumHeight = 25f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("METLIFE, MEXICO S.A. DE C.V.", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Agente: " + strAgente, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("Promotoría: " + strPromotoria, Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;

            cell = new PdfPCell(new Phrase("", Text));
            cell.Colspan = 10;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            table.AddCell(cell);
            cell = null;
            
            document.Add(table);
            document.Close();

            // COLOCAR O QUITAR DEPENDIENDO LA PETICION
            if (CreaPDF == 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }

            culture = null;
            motivos = null;
            Text = null;
            TextFooter = null;
            Negrita = null;
            cell = null;
            writer = null;
            output = null;
            document = null;
        }

        public void CartaPasePDF(int Id, HttpResponse Response, int CreaPDF, int Credencial)
        {
            DataTable motivos = new DataTable();
            CultureInfo culture = new CultureInfo("es-MX");
            wfiplib.admTramite admTramite = new wfiplib.admTramite();
            wfiplib.admEmisionVG admEmision = new wfiplib.admEmisionVG();
            wfiplib.tramiteP oTramite = admTramite.carga(Id);
            wfiplib.EmisionVG oEmision = admEmision.carga(Id);

            // Medidas de la hoja A4
            var document = new Document(PageSize.A4, 30, 30, 50, 50);
            document.SetMargins(30, 30, 80, 50);

            var output = (dynamic)null;

            // VALIDAR SI ES CREADO O ALMACENADO
            if (CreaPDF == 1)
            {
                //string directorioTemporal = Server.MapPath("~") + "\\Cartas\\";
                string directorioTemporal = HttpContext.Current.Server.MapPath("~") + "\\Cartas\\";
                output = new FileStream(directorioTemporal + "CartaPase" + Credencial + "_" + oTramite.FolioCompuesto + ".pdf", FileMode.Create);
            }
            else
            {
                output = new MemoryStream();
            }

            var writer = PdfWriter.GetInstance(document, output);

            PdfPCell cell;
            Font Text = new Font(FontFamily.HELVETICA, 10, NORMAL, BaseColor.BLACK);
            Font TextFooter = new Font(FontFamily.HELVETICA, 7, NORMAL, BaseColor.BLACK);
            Font Negrita = new Font(FontFamily.HELVETICA, 10, BOLD, BaseColor.BLACK);
            Font NegritaSubrayado = new Font(FontFamily.HELVETICA, 10, BOLD | UNDERLINE, BaseColor.BLACK);

            // COLOCA ENCABEZADO Y PIE DE PAGINA
            writer.PageEvent = new PDFFooter("");
            document.Open();

            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 100;

            cell = new PdfPCell(new Phrase("CARTA PASE", Negrita))
            {
                Colspan = 10,
                FixedHeight = 50f,
                MinimumHeight = 20f,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fecha de Solicitud.   /   /", Text))
            {
                Colspan = 10,
                FixedHeight = 50f,
                MinimumHeight = 20f,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hospital ANGELES:___________ ", Text))
            {
                Colspan = 5,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fecha de cita médica:   /   / ", Text))
            {
                Colspan = 5,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hora de cita médica:_____ ", Text))
            {
                Colspan = 5,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Atención:_____ ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Presente.", Text))
            {
                Colspan = 2,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nombre del Cliente:_____ ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fecha de Nacimiento:   /   / ", Text))
            {
                Colspan = 5,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Edad:_____ ", Text))
            {
                Colspan = 5,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Autorizamos a Hospital Angeles sucursal_____ a prestar sus servicios de acuerdo al convenio establecido, para el cliente arriba citado.", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("El paquete a practicar se identifica con la clave_____ ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Cabe señalar que dichos servicios no tienen costo para el paciente y que la facturación debe estar a nombre de MetLife México, S.A. de C.V. con los siguientes datos ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("RAZON SOCIAL: MetLife México, S.A. de C.V. ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("RFC: MME920427EM3 ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("DOMICILIO FISCAL: Av. Insurgentes Sur 1457, pisos 7 al 14, Col. Insurgentes Mixcoac, CP: 03920, Alcaldía: Benito Juárez, Cdmx.", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Enviar resultados médicos en PDF a la cuenta de correo: resultadosmedicos@mtelife.com.mx con copia a citasmedicas@metlife.com.mx ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Enviar factura electrónica al correo: citasmedicas@metlife.com.mx ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Enviar estudios originales a: Av. Insurgentes Sur #1457 Col. Insurgentes Mixcoac, Delegación Benito Juárez, CP 03920 CDMX, (Torre Manacar Piso 14).", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("En atención a: Gerardo Gonzáliez Díaz, Teléfono: 5328 9000 Ext. 6694", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("No están autorizados estudios adicionales a los indicados en esta carta. ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("NOTA: Favor de responder todas la preguntas en la Historia Clínica de Metlife así como las firmas del médico y el paciente", NegritaSubrayado))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Agradeciendo de antemano sus atenciones, quedamos a su disposición para cualquier duda o aclaración al respecto. ", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Atentamente.", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Gerardo González Díaz", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Área de citas médicas", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("MetLife México, S.A. de C.V.", Text))
            {
                Colspan = 10,
                FixedHeight = 15f,
                MinimumHeight = 15f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            table.AddCell(cell);

            document.Add(table);
            document.Close();

            // COLOCAR O QUITAR DEPENDIENDO LA PETICION
            if (CreaPDF == 0)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }            
        }
    
    }
    
    public class PDFFooter : PdfPageEventHelper
    {
        public string mParameters { get; set; }

        public PDFFooter(string parameters)
        {
            mParameters = parameters;
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            tabFot.TotalWidth = 540F;

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("c:\\logo_grap.jpg");
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 10; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.MinimumHeight = 50f;
            imageCell.FixedHeight = 50f;
            imageCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabFot.AddCell(imageCell);
            
            imageCell = null;
            tabFot.WriteSelectedRows(0, -1, 26, document.Top + 50, writer.DirectContent);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 2F, 3F, 1F });
            Font TextFooter = new Font(FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            PdfPCell cell;
            tabFot.TotalWidth = 540F;

            string _agente = string.Empty;
            string _promo = string.Empty;

            try
            {
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("c:\\logo_grap_separador.png");
                PdfPCell imageCell = new PdfPCell(jpg);
                imageCell.Colspan = 10; // either 1 if you need to insert one cell
                imageCell.Border = 0;
                //imageCell.MinimumHeight = 10f;
                //imageCell.FixedHeight = 10f;
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(imageCell);
            }
            catch
            { 
            }

            if (mParameters.Length > 0)
            {
                string[] valor = mParameters.Split('|');
                _agente = valor[0].ToString();
                _promo = valor[1].ToString();

                cell = new PdfPCell(new Phrase("Clave de la promotoría: " + _agente + " \n\r Clave del agente: " + _promo, TextFooter));
                cell.Colspan = 1;
                cell.FixedHeight = 15f;
                cell.MinimumHeight = 15f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                tabFot.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase(string.Empty, TextFooter));
                cell.Colspan = 1;
                cell.FixedHeight = 15f;
                cell.MinimumHeight = 15f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                tabFot.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("Avenida Insurgentes Sur No. 1457 pisos 7 al 14,\r\nCol. Insurgentes Mixcoac, Alcaldía Benito Juárez, C.P. 03920, CDMX.\r\nTel: 800-00-MetLife (6385433)\r\n", TextFooter));
            cell.Colspan = 1;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase("MetLife México S.A. de C.V.", TextFooter));
            cell.Colspan = 1;
            cell.FixedHeight = 15f;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            tabFot.AddCell(cell);

            tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}