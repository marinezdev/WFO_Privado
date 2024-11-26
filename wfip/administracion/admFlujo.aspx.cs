using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace wfip.administracion
{
    public partial class admFlujo : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                hdIdFlujo.Value = Request.Params["Id"];

                wfiplib.flujo oFLujo= (new wfiplib.admFlujo()).carga(Convert.ToInt32(hdIdFlujo.Value));
                lbNomFlujo.Text = "CONFIGURACION DEL FLUJO: " + oFLujo.Nombre.ToUpper();

                MuestraEstructura(oFLujo.Id);
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e){Response.Redirect("admListaFlujos.aspx");}

        private void MuestraEstructura(int idFlujo) {
            StringBuilder Datos = new StringBuilder("<ol class='listaOrdenada'>");
            
            wfiplib.admMesa _admMesa = new wfiplib.admMesa();
            List<wfiplib.etapa> lstPasos = (new wfiplib.admCatEtapas()).Lista(idFlujo);

            foreach (wfiplib.etapa _paso in lstPasos)
            {
                Datos.AppendLine("<li data-id='N" + _paso.IdEtapa.ToString() + "' data-name='" + _paso.Nombre + "'>" + _paso.Nombre + " <ol>");
                List<wfiplib.mesa> lstMesas = _admMesa.ListaMesasRaizDelPaso(idFlujo,_paso.IdEtapa);
                foreach (wfiplib.mesa _Mesa in lstMesas) { Datos.AppendLine(ArmaMesa(_Mesa)); }
                Datos.AppendLine("</ol></li>");
            }

            Datos.AppendLine("</ol>");
            ltFlujo.Text = Datos.ToString();
        }

        private string ArmaMesa(wfiplib.mesa pMesa){
            StringBuilder Resultado = new StringBuilder("<li data-id=M" + pMesa.Id.ToString() + " data-name='" + pMesa.Nombre + "'>" + pMesa.Nombre);
            if (pMesa.eTipo == wfiplib.E_TipoMesa.Especializada)
            {
                Resultado.AppendLine("<ol>");
                wfiplib.admMesa _admMesa = new wfiplib.admMesa();
                List<wfiplib.mesa> lstMesas = _admMesa.ListaMesasHijas(pMesa.Id);
                foreach (wfiplib.mesa _mesa in lstMesas)
                {
                    Resultado.AppendLine(ArmaMesa(_mesa));
                }
                Resultado.AppendLine("</ol>");
            }
            Resultado.AppendLine("</li>");
            return Resultado.ToString();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if(!string .IsNullOrEmpty(hdFlujoTrabajo.Value))
            {
                wfiplib.admMesa _admMesa = new wfiplib.admMesa();
                wfiplib.csFlujoTemp pasos = Deserealiza(hdFlujoTrabajo.Value);

                foreach (wfiplib.FlujoTemp paso in pasos.Mesas)
                {
                    if (paso.children.Count > 0) { foreach (wfiplib.FlujoTemp mesa in paso.children) { actualizaConfiguracionMesa(_admMesa, paso.IdReal, 0, mesa); } }
                }
                MuestraEstructura(Convert.ToInt32(hdIdFlujo.Value));

                //wfiplib.admMesaApoyo admApy = new wfiplib.admMesaApoyo();
                //int Nivel=1;
                //int IdFlujo = Convert.ToInt32(hdIdFlujo.Value);

                //admApy.Eliminar(IdFlujo);
                //foreach (wfiplib.FlujoTemp oReg in Niveles.Mesas)
                //{
                //    if (oReg.children.Count > 0) { this.RegistraMesas(IdFlujo,Nivel, oReg.children); }
                //    Nivel += 1;
                //}
                //Response.Redirect("admListaFlujos.aspx");
            }
        }

        private void actualizaConfiguracionMesa(wfiplib.admMesa pAdmMesa, int pPaso, int pMesaPadre, wfiplib.FlujoTemp pMesa)
        {
            string conApoyo = "0";
            conApoyo = (pMesa.children.Count > 0) ? "1" : "0";
            pAdmMesa.configura(pPaso, pMesaPadre, conApoyo, pMesa.IdReal);
            if (pMesa.children.Count > 0) { foreach (wfiplib.FlujoTemp mesa in pMesa.children) { actualizaConfiguracionMesa(pAdmMesa, pPaso, pMesa.IdReal, mesa); } }
        }

        private wfiplib.csFlujoTemp Deserealiza(string json)
        {
            wfiplib.csFlujoTemp resultado = new wfiplib.csFlujoTemp();
            try
            {
                json = json.Replace("[[", "[");
                json = json.Replace("]]", "]");
                json = "{\"Mesas\":" + json + "}";
                System.Web.Script.Serialization.JavaScriptSerializer jsonSer = new System.Web.Script.Serialization.JavaScriptSerializer();
                resultado = jsonSer.Deserialize<wfiplib.csFlujoTemp>(json);
            }
            catch (Exception) { }

            return resultado;
        }

        //private void RegistraMesas(int IdFlujo, int Nivel, List<wfiplib.FlujoTemp> pMesas){
        //    wfiplib.admMesa admM = new wfiplib.admMesa();
        //    foreach (wfiplib.FlujoTemp oReg in pMesas) {
        //        wfiplib.mesa oMesa= new wfiplib.mesa();
        //        oMesa.IdFlujo = IdFlujo;
        //        oMesa.IdMesa = Convert.ToInt32(oReg.id);
        //        oMesa.Nivel = Nivel;
        //        if(oReg.children.Count > 0){oMesa.Tipo = wfiplib.e_TipoMesa.Especializada;}
                
        //        admM.Actualiza(oMesa);
                
        //        if (oReg.children.Count > 0) {
        //            this.RegistraMesasApoyo(IdFlujo, Nivel, Convert.ToInt32(oReg.id), oReg.children);
        //        }
        //    } 
        //}

        //private void RegistraMesasApoyo(int IdFlujo,int Nivel, int IdMesa,List<wfiplib.FlujoTemp> Lista)
        //{
        //    wfiplib.admMesa _admMesa = new wfiplib.admMesa();
        //    wfiplib.admMesaApoyo admApoyo = new wfiplib.admMesaApoyo();
        //    foreach (wfiplib.FlujoTemp oReg in Lista)
        //    {
        //        wfiplib.mesa oMesa = new wfiplib.mesa();
        //        oMesa.IdFlujo = IdFlujo;
        //        oMesa.IdMesa = Convert.ToInt32(oReg.id);
        //        oMesa.Nivel = Nivel;
        //        oMesa.Tipo = wfiplib.e_TipoMesa.Apoyo;
        //        _admMesa.Actualiza(oMesa);
                                
        //        wfiplib.mesaApoyo oApoyo= new wfiplib.mesaApoyo();
        //        oApoyo.IdFlujo=IdFlujo;
        //        oApoyo.IdMesaPrincipal=IdMesa;
        //        oApoyo.IdMesaApoyo=Convert.ToInt32(oReg.id);
        //        admApoyo.Registrar(oApoyo);              
        //    }
        //}

    }
}