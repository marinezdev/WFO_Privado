using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using System.IO;

namespace wfiplib
{
    public class AdmPdf
    {
        public Boolean Fusiona(List<string> lstArchivos, string pArhivoDst)
        {
            bool continuar = false;
            PdfDocument outputDocument = new PdfDocument();

            foreach (string file in lstArchivos)
            {
                if (File.Exists(file))
                {
                    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        PdfPage page = inputDocument.Pages[idx];
                        outputDocument.AddPage(page);
                        continuar = true;
                    }
                }
            }

            if(continuar) outputDocument.Save(pArhivoDst);
            return true;
        }

        public Boolean Adiciona(List<string> lstArchivos, string pArchivoFusion, string pArhivoDst, string pNmUsuario)
        {
            bool continuar = false;
            PdfDocument outputDocument = new PdfDocument();

            if (File.Exists(pArchivoFusion))
            {
                PdfDocument inputDocument = PdfReader.Open(pArchivoFusion, PdfDocumentOpenMode.Import);
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    PdfPage page = inputDocument.Pages[idx];
                    outputDocument.AddPage(page);
                }
            }

            // Crea el separador
            XFont font = new XFont("Verdana", 16, XFontStyle.Bold);
            PdfPage pageSepara = outputDocument.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(pageSepara);

            gfx.DrawString("DOCUMENTOS ADICIONADOS", font, XBrushes.Blue, 50, 100, XStringFormats.Default);
            gfx.DrawString(pNmUsuario, font, XBrushes.Black, 50, 135, XStringFormats.Default);
            gfx.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), font, XBrushes.Black, new XRect(0, 0, pageSepara.Width, pageSepara.Height), XStringFormats.Center);

            foreach (string file in lstArchivos)
            {
                if (File.Exists(file))
                {
                    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        PdfPage page = inputDocument.Pages[idx];
                        outputDocument.AddPage(page);
                        continuar = true;
                    }
                }
            }

            if (continuar) outputDocument.Save(pArhivoDst);
            return true;
        }
    }
}
