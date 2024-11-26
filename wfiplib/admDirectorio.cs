using System;
using System.IO;

namespace wfiplib
{
    public class admDirectorio
    {
        public int daSiguienteIdArchivo()
        {
            int resultado = 0;
            bd BD = new bd();
            resultado = BD.ejecutaCmdScalar("INSERT INTO archivoCtrl(Fecha) VALUES(GetDate()) SELECT SCOPE_IDENTITY()");
            BD.cierraBD();
            return resultado;
        }

        public string daDirectorio(string pDirBase,long pId)
        {
            string resultado = "";
            directorio oDir = new directorio();
            if (pId >= 0)
            {
                string hex = (pId / 512).ToString("x").PadLeft(6,'0');
                if (hex.Length == 6)
                {
                    oDir.abuelo = hex.Substring(0,2);
                    oDir.padre = hex.Substring(2,2);
                    oDir.hijo = hex.Substring(4, 2);
                }
                resultado = validaDirectorio(pDirBase, oDir);
            }
            return resultado;
        }

        private string validaDirectorio(string pDirBase, directorio pDir)
        {
            string resultado = "";
            try
            {
                if (!Directory.Exists(pDirBase)) { Directory.CreateDirectory(pDirBase); resultado = pDirBase; }
                resultado = pDirBase + pDir.abuelo + "\\" + pDir.padre + "\\" + pDir.hijo + "\\";
                if (!Directory.Exists(resultado)) { Directory.CreateDirectory(resultado); }
            }
            catch(Exception){}

            return resultado;
        }

        public directorio consultaDir(long pId)
        {
            directorio oDir = new directorio();
            if (pId >= 0)
            {
                string hex = (pId / 512).ToString("x").PadLeft(6, '0');
                if (hex.Length == 6)
                {
                    oDir.abuelo = hex.Substring(0, 2);
                    oDir.padre = hex.Substring(2, 2);
                    oDir.hijo = hex.Substring(4, 2);
                }
            }
            return oDir;
        }
    }

    public class directorio
    {
        private string mAbuelo = String.Empty;
        public string abuelo { get { return mAbuelo; } set { mAbuelo = value; } }
        private string mPadre = String.Empty;
        public string padre { get { return mPadre; } set { mPadre = value; } }
        private string mHijo = String.Empty;
        public string hijo { get { return mHijo; } set { mHijo = value; } }
    }
}
