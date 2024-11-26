using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Text;

namespace wfip.administracion
{
    public partial class admDxFlujo : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            if (!IsPostBack)
            {
                Session.Remove("pIdFlujo");
            }
        }

        protected void grdTipoTramite_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            
            //object masterKeyValue = grdFlujos.GetRowValues(Convert.ToInt32(e.Parameters), "IdFlujo");
            //Session["pIdFlujo"] = masterKeyValue;
            int IdFlujo = Convert.ToInt32(grdFlujos.GetRowValues(Convert.ToInt32(e.Parameters), grdFlujos.KeyFieldName));
            Session["pIdFlujo"] = IdFlujo;
            grdTipoTramite.DataSource = objDsTiposTramite;
            grdTipoTramite.PageIndex = 0;
            grdTipoTramite.DataBind();
        }

        protected void grdEtapas_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            grdEtapas.DataSource = objDsEtapas;
            grdEtapas.PageIndex = 0;
            grdEtapas.DataBind();
        }

        protected void grdMesas_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            grdMesas.DataSource = objDsMesas;
            grdMesas.PageIndex = 0;
            grdMesas.DataBind();
        }

        protected void grdFlujos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["IdUsuario"] = manejo_sesion.Credencial.Id;
        }

        protected void grdTipoTramite_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView g = sender as ASPxGridView;
            if (Session["pIdFlujo"] != null)
            {
                wfiplib.TipoTramite datos = new wfiplib.TipoTramite();
                datos.IdFlujo = Convert.ToInt32(Session["pIdFlujo"]);
                datos.Nombre = e.NewValues["Nombre"].ToString();
                datos.IdUsuario = manejo_sesion.Credencial.Id;
                datos.IdEstado = Convert.ToInt32(e.NewValues["IdEstado"]);
                datos.Tabla = e.NewValues["Tabla"].ToString();
                (new wfiplib.admTipoTramite()).nuevo(datos);
                g.DataSource = objDsTiposTramite;
                g.DataBind();
                //e.NewValues["IdFlujo"] = Convert.ToInt32(Session["pIdFlujo"]);
                //e.NewValues["IdUsuario"] = ((wfiplib.credencial)Session["credencial"]).Id;
            } 
            g.CancelEdit();
            e.Cancel = true;
        }

        protected void grdTipoTramite_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView g = sender as ASPxGridView;
            if (Session["pIdFlujo"] != null)
            {
                wfiplib.TipoTramite datos = new wfiplib.TipoTramite();
                datos.Id = Convert.ToInt32(e.Keys[grdTipoTramite.KeyFieldName]); ;
                datos.Nombre = e.NewValues["Nombre"].ToString();
                datos.IdEstado = Convert.ToInt32(e.NewValues["IdEstado"]);
                datos.Tabla = e.NewValues["Tabla"].ToString();
                (new wfiplib.admTipoTramite()).modifica(datos);
                g.DataSource = objDsTiposTramite;
                g.DataBind();
            }
            g.CancelEdit();
            e.Cancel = true;
        }

        protected void grdEtapas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView g = sender as ASPxGridView;
            if (Session["pIdFlujo"] != null)
            {
                wfiplib.etapa datos = new wfiplib.etapa();
                datos.IdFlujo = Convert.ToInt32(Session["pIdFlujo"]);
                datos.Nombre = e.NewValues["Nombre"].ToString();
                datos.IdUsuario = manejo_sesion.Credencial.Id;
                datos.IdEstado = Convert.ToInt32(e.NewValues["IdEstado"]);
                (new wfiplib.admCatEtapas()).nuevo(datos);
                g.DataSource = objDsEtapas;
                g.DataBind();
            }
            g.CancelEdit();
            e.Cancel = true;
        }

        protected void grdEtapas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView g = sender as ASPxGridView;
            if (Session["pIdFlujo"] != null)
            {
                wfiplib.etapa datos = new wfiplib.etapa();
                datos.Id = Convert.ToInt32(e.Keys[grdEtapas.KeyFieldName]); ;
                datos.Nombre = e.NewValues["Nombre"].ToString();
                datos.IdEstado = Convert.ToInt32(e.NewValues["IdEstado"]);
                (new wfiplib.admCatEtapas()).modifica(datos);
                g.DataSource = objDsEtapas;
                g.DataBind();
            }
            g.CancelEdit();
            e.Cancel = true;
        }

        protected void grdMesas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView g = sender as ASPxGridView;
            if (Session["pIdFlujo"] != null)
            {
                wfiplib.mesa datos = new wfiplib.mesa();
                datos.IdFlujo = Convert.ToInt32(Session["pIdFlujo"]);
                datos.Nombre = e.NewValues["Nombre"].ToString();
                datos.IdTipo = (int)wfiplib.E_TipoMesa.General;
                datos.IdEtapa = (new wfiplib.admCatEtapas()).daIdPrimerEtapa(datos.IdFlujo);
                datos.ConApoyo = wfiplib.E_Estado.Inactivo;
                datos.ConCondicion = wfiplib.E_Estado.Inactivo;
                datos.IdEstado = Convert.ToInt32(e.NewValues["IdEstado"]);
                datos.IdMesaPadre = 0;
                datos.Apoyo = wfiplib.E_Estado.Inactivo;
                datos.IdUsuario = manejo_sesion.Credencial.Id;
                (new wfiplib.admMesa()).nuevo(datos);
                g.DataSource = objDsMesas;
                g.DataBind();
            }
            g.CancelEdit();
            e.Cancel = true;
        }

        protected void grdMesas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView g = sender as ASPxGridView;
            if (Session["pIdFlujo"] != null)
            {
                wfiplib.mesa datos = new wfiplib.mesa();
                datos.Id = Convert.ToInt32(e.Keys[g.KeyFieldName]); ;
                datos.Nombre = e.NewValues["Nombre"].ToString();
                datos.IdEstado = Convert.ToInt32(e.NewValues["IdEstado"]);
                string IdTipo = e.NewValues["Tipo"].ToString();
                if (IsNumeric(IdTipo))
                {
                    datos.IdTipo = Convert.ToInt32(e.NewValues["Tipo"]);
                }
                else
                {
                    if (IdTipo.Equals("GENERAL")) datos.IdTipo = 1;
                    else datos.IdTipo = 2;
                }
                (new wfiplib.admMesa()).modificaNombreEstado(datos);
                g.DataSource = objDsMesas;
                g.DataBind();
            }
            g.CancelEdit();
            e.Cancel = true;
        }

        protected void grdTipoTramite_PageIndexChanged(object sender, EventArgs e)
        {
            grdTipoTramite.DataSource = objDsTiposTramite;
            grdTipoTramite.DataBind();
        }

        protected void grdEtapas_PageSizeChanged(object sender, EventArgs e)
        {
            grdEtapas.DataSource = objDsEtapas;
            grdEtapas.DataBind();
        }

        protected void grdMesas_PageIndexChanged(object sender, EventArgs e)
        {
            grdMesas.DataSource = objDsMesas;
            grdMesas.DataBind();
        }

        protected void pnlCfgMesas_Callback(object sender, CallbackEventArgsBase e)
        {
            if (Session["pIdFlujo"] != null)
            {
                Int32 IdFlujo = Convert.ToInt32(Session["pIdFlujo"]);
                StringBuilder Datos = new StringBuilder("");

                wfiplib.admMesa _admMesa = new wfiplib.admMesa();
                List<wfiplib.etapa> lstEtapas = (new wfiplib.admCatEtapas()).Lista(IdFlujo);

                foreach (wfiplib.etapa _etapa in lstEtapas)
                {
                    Datos.AppendLine("<li data-id='N" + _etapa.Id.ToString() + "' data-name='" + _etapa.Nombre + "'>" + _etapa.Nombre + " <ol>");
                    List<wfiplib.mesa> lstMesas = _admMesa.ListaMesasRaizDelPaso(IdFlujo, _etapa.Id);
                    foreach (wfiplib.mesa _Mesa in lstMesas) { Datos.AppendLine(ArmaMesa(_Mesa)); }
                    Datos.AppendLine("</ol></li>");
                }
                ltFlujo.Text = Datos.ToString();
            }
        }

        private string ArmaMesa(wfiplib.mesa pMesa)
        {
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

        protected void callBackCfgMesas_Callback(object source, CallbackEventArgs e)
        {
            //System.Threading.Thread.Sleep(3000);
            if (!string.IsNullOrEmpty(e.Parameter))
            {

                wfiplib.admMesa _admMesa = new wfiplib.admMesa();
                wfiplib.csFlujoTemp pasos = Deserealiza(e.Parameter);

                foreach (wfiplib.FlujoTemp paso in pasos.Mesas)
                {
                    if (paso.children.Count > 0)
                    {
                        foreach (wfiplib.FlujoTemp mesa in paso.children)
                        {
                            actualizaConfiguracionMesa(_admMesa, paso.IdReal, 0, mesa);
                        }
                    }
                }
            }
            e.Result = "Configuración guardada";
        }

        private wfiplib.csFlujoTemp Deserealiza(string pDatos)
        {
            wfiplib.csFlujoTemp resultado = new wfiplib.csFlujoTemp();
            try
            {
                pDatos = pDatos.Replace("[[", "[");
                pDatos = pDatos.Replace("]]", "]");
                pDatos = "{\"Mesas\":" + pDatos + "}";
                System.Web.Script.Serialization.JavaScriptSerializer jsonSer = new System.Web.Script.Serialization.JavaScriptSerializer();
                resultado = jsonSer.Deserialize<wfiplib.csFlujoTemp>(pDatos);
            }
            catch (Exception) { }
            return resultado;
        }

        private void actualizaConfiguracionMesa(wfiplib.admMesa pAdmMesa, int pPaso, int pMesaPadre, wfiplib.FlujoTemp pMesa)
        {
            string conApoyo = "0";
            conApoyo = (pMesa.children.Count > 0) ? "1" : "0";
            pAdmMesa.configura(pPaso, pMesaPadre, conApoyo, pMesa.IdReal);
            if (pMesa.children.Count > 0) { foreach (wfiplib.FlujoTemp mesa in pMesa.children) { actualizaConfiguracionMesa(pAdmMesa, pPaso, pMesa.IdReal, mesa); } }
        }

        protected void grdMesas_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (grdMesas.IsEditing) { e.NewValues["Tipo"] = "1"; }
        }

        private bool IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
        }		
    }
}