using GdPicture14;

namespace wfip.promotoria
{
    public class Jpgpdf
    {
        public bool convierte(string pOrigen, string pDestino)
        {
            bool resultado = false;
            LicenseManager oLicenseManager = new LicenseManager();
            oLicenseManager.RegisterKEY("21186996792927985131914633819454559454");

            if (System.IO.File.Exists(pDestino)) System.IO.File.Delete(pDestino);
            GdPictureImaging oGdPictureImaging = new GdPictureImaging(); // GdPictureImaging object for loading and saving images
            GdPicturePDF m_DestPDF = new GdPicturePDF(); // Variable to hold a destination pdf file
            int m_SrcImageID = oGdPictureImaging.CreateGdPictureImageFromFile(pOrigen);
            if (m_SrcImageID != 0)
            {
                GdPictureStatus m_Status = oGdPictureImaging.SaveAsPDF(m_SrcImageID, pDestino, false, "fto", "GdPicture", "", "", "GdPicture File Converter");
                resultado = (m_Status == GdPictureStatus.OK);
                oGdPictureImaging.ReleaseGdPictureImage(m_SrcImageID);
            }
            return resultado;
        }
    }
}