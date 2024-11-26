using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admDoctosFlujo : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected System.Collections.ArrayList arlList = new System.Collections.ArrayList();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                hdIdFlujo.Value = Request.Params["Id"].ToString();
                this.CargaFLujo(Convert.ToInt32(hdIdFlujo.Value));
                this.LLenaTramites(Convert.ToInt32(hdIdFlujo.Value));
            }
        }
        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admListaFlujos.aspx"); }

        private void CargaFLujo(int IdFlujo){
           wfiplib.flujo oflujo = (new wfiplib.admFlujo()).carga(IdFlujo);
            lbFlujo.Text = "DOCUMENTOS DEL FLUJO: " + oflujo.Nombre.ToUpper();
        }
        
        private void LLenaTramites(int IdFLujo) {
            wfiplib.admTipoTramite adm = new wfiplib.admTipoTramite();
            rpTramites.DataSource = adm.Lista(IdFLujo);
            rpTramites.DataBind();
        }

        protected void btnRegresar_Click(object sender, EventArgs e) { mvContenedor.ActiveViewIndex = 0; }

        protected void rpTramites_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("VerDoctos")) {
                hdIdTramite.Value = e.CommandArgument.ToString();
                lbTipoTramite.Text = "Tipo de trámite: " + ((Label)e.Item.FindControl("lbTramite")).Text.ToUpper();
                int IdFlujo = Convert.ToInt32(hdIdFlujo.Value);
                int IdTramite = Convert.ToInt32(e.CommandArgument.ToString());

                this.llenaDocumentos(IdFlujo, IdTramite);
                                
                mvContenedor.ActiveViewIndex = 1;
            }
        }


        private void llenaDocumentos(int IdFLujo, int IdTramite)
        {
            lstDisponibles.Items.Clear();
            lstAgregados.Items.Clear();

            List<wfiplib.tipoTramiteDoctos> LstDiponibles = (new wfiplib.admTipoTramiteDoctos()).daDoctosPorAgregarTipoTramite(IdFLujo, IdTramite);
            if (LstDiponibles.Count > 0)
            {
                lstDisponibles.DataSource = LstDiponibles;
                lstDisponibles.DataTextField = "Nombre";
                lstDisponibles.DataValueField = "IdTipoDocto";
                lstDisponibles.DataBind();
            }

            List<wfiplib.tipoTramiteDoctos> LstAgregados = (new wfiplib.admTipoTramiteDoctos()).daDoctosContieneTipoTramite(IdFLujo, IdTramite);
            if (LstAgregados.Count > 0)
            {
                lstAgregados.DataSource = LstAgregados;
                lstAgregados.DataTextField = "Nombre";
                lstAgregados.DataValueField = "IdTipoDocto";
                lstAgregados.DataBind();
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (lstDisponibles.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstDisponibles.Items.Count; i++)
                {
                    if (lstDisponibles.Items[i].Selected)
                    {
                        if (!arlList.Contains(lstDisponibles.Items[i]))
                            arlList.Add(lstDisponibles.Items[i]);
                    }
                }
                for (int i = 0; i < arlList.Count; i++)
                {
                    if (!lstAgregados.Items.Contains((ListItem)arlList[i]))
                        lstAgregados.Items.Add((ListItem)arlList[i]);
                    lstDisponibles.Items.Remove((ListItem)arlList[i]);
                }
            }
        }

        protected void btnAgegarTodos_Click(object sender, EventArgs e)
        {
            foreach (ListItem list in lstDisponibles.Items)
            {
                lstAgregados.Items.Add(list);
            }
            lstDisponibles.Items.Clear();

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstAgregados.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstAgregados.Items.Count; i++)
                {
                    if (lstAgregados.Items[i].Selected)
                    {
                        if (!arlList.Contains(lstAgregados.Items[i]))
                            arlList.Add(lstAgregados.Items[i]);
                    }
                }
                for (int i = 0; i < arlList.Count; i++)
                {
                    if (!lstDisponibles.Items.Contains((ListItem)arlList[i]))
                        lstDisponibles.Items.Add((ListItem)arlList[i]);
                    lstAgregados.Items.Remove((ListItem)arlList[i]);
                }
            }
        }

        protected void btnRemoveTodo_Click(object sender, EventArgs e)
        {
            foreach (ListItem list in lstAgregados.Items)
            {
                lstDisponibles.Items.Add(list);
            }
            lstAgregados.Items.Clear();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int IdFLujo = Convert.ToInt32(hdIdFlujo.Value);
            int IdTramite = Convert.ToInt32(hdIdTramite.Value);

            wfiplib.admTipoTramiteDoctos adm=new wfiplib.admTipoTramiteDoctos();
            bool resultado = adm.EliminaDoctosTramite(IdFLujo, IdTramite);
            
            int Orden=1;
            wfiplib.tipoTramiteDoctos oTTDoc = new wfiplib.tipoTramiteDoctos();
            oTTDoc.IdFlujo=Convert.ToInt32(hdIdFlujo.Value);
            oTTDoc.IdTipoTramite=Convert.ToInt32(hdIdTramite.Value);
            foreach (ListItem list in lstAgregados.Items)
            {
                oTTDoc.IdTipoDocto = Convert.ToInt32(list.Value);
                oTTDoc.Orden = Orden;
                adm.agrega(oTTDoc);
                Orden += 1;
            }
            mvContenedor.ActiveViewIndex = 0;
        }
        
    }
}