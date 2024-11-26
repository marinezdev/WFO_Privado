using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

namespace wfip.promotoria
{
    public partial class esperaPromotoria : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admArchivosDependencias ad = new wfiplib.admArchivosDependencias();
        wfiplib.ArchivosDependenciasAsignados ada = new wfiplib.ArchivosDependenciasAsignados();
        wfiplib.admRolesDependencias rd = new wfiplib.admRolesDependencias();
        Mensajes mensajes = new Mensajes();
        

        #region Eventos *******************************************************************************************************

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // warningMessages
                string mensajeShow = (new wfiplib.admCatMensajes()).mensaje("promotoria");
                if (mensajeShow.Length > 5)
                {
                    lblMessageBySystem.Visible = true;
                    lblMessageBySystem.Text = mensajeShow;
                    mensajes.MostrarMensaje(this, mensajeShow);
                }
                else
                {
                    lblMessageBySystem.Visible = false;
                    lblMessageBySystem.Text = string.Empty;
                }
                graficaEstilo();
                ValidarRol(manejo_sesion.Credencial.IdRol);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            pintaDatos();
        }

        protected void grfGrupoUno_Click(object sender, ImageMapEventArgs e)
        {
            string estado = Convert.ToString(e.PostBackValue);
            string cobertura = RblCobertura.SelectedValue;
            ltTemp.Text = estado;
            if (manejo_sesion.Credencial.IdRol == 20 || 
                manejo_sesion.Credencial.IdRol == 21 || 
                manejo_sesion.Credencial.IdRol == 22 || 
                manejo_sesion.Credencial.IdRol == 23)
            {
                string es = "";
                if (cobertura == "1")
                {
                    switch (estado)
                    {
                        case "En Trámite":
                            es = "1";
                            break;
                        case "Suspendido":
                            es = "2";
                            break;
                        case "En Proceso":
                            es = "3";
                            break;
                        case "Reenvío Trámite":
                            es = "4";
                            break;
                        case "En Revisión":
                            es = "5";
                            break;
                        case "Concluído":
                            es = "6";
                            break;
                    }
                    Response.Redirect("listaCobranza.aspx?es=" + es + "&cob=" + RblCobertura.SelectedValue);
                }
            }
            if (manejo_sesion.Credencial.IdRol == 20 ||
                manejo_sesion.Credencial.IdRol == 22 ||
                manejo_sesion.Credencial.IdRol == 26 ||
                manejo_sesion.Credencial.IdRol == 27 ||
                manejo_sesion.Credencial.IdRol == 28 ||
                manejo_sesion.Credencial.IdRol == 29)
            {
                string es = "";
                if (cobertura == "2")
                {
                    switch (estado)
                    {
                        case "En Trámite":
                            es = "1";
                            break;
                        case "Suspendido":
                            es = "2";
                            break;
                        case "En Proceso":
                            es = "3";
                            break;
                        case "Reenvío Trámite":
                            es = "4";
                            break;
                        case "En Revisión": //analista front
                            es = "5";
                            break;
                        case "Incompleto": //analista front
                            es = "6";
                            break;
                        case "Carta":     //analista back
                            es = "7";
                            break;
                        case "Concluído": //analaista/dependencia
                            es = "8";
                            break;
                    }
                    Response.Redirect("listaCobranza.aspx?es=" + es + "&cob=" + RblCobertura.SelectedValue);
                }
            }
            else
            {
                Response.Redirect(EncripParametros("estado=" + estado, "tramitesPorEstado.aspx").URL, true);
                //Response.Redirect("tramitesPorEstado.aspx?estado=" + estado);
            }
                

        }

        protected void RblCobertura_SelectedIndexChanged(object sender, EventArgs e)
        {
            graficaEstilo();
            //Seleccionar un valor diferente para cada tipo de cobertura            

            if (RblCobertura.SelectedValue == "1")
                ValidarRolBasica(manejo_sesion.Credencial.IdRol);
            if (RblCobertura.SelectedValue == "2")
                ValidarRolPotenciacion(manejo_sesion.Credencial.IdRol);

        }


        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }

        #endregion

        #region Métodos *******************************************************************************************************

        protected void pintaTituloPromotoria()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    ltTituloPromotoria.Text = manejo_sesion.Promotoria.Nombre; //(new wfiplib.admCatPromotoria()).carga(manejo_sesion.Credencial.IdPromotoria).Nombre;
                }
            }
        }

        protected void pintaDatos()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria )
            {
                graficaEstilo();
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    DataTable datos = (new wfiplib.admIndicadores()).IndicadoresGeneralesPromotoria(manejo_sesion.Credencial.IdPromotoria);
                    llenaGraficaUno(datos);
                    datos.Dispose();
                }
                else
                {
                    ValidarRolBasica(manejo_sesion.Credencial.IdRol);
                }
            }
        }

        protected void graficaEstilo()
        {
            // Disable axis labels auto fitting of text
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.IsLabelAutoFit = false;

            // Habilita que se pinten todos los nombres de los puntos
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.Interval = 1;
            grfGrupoUno.ChartAreas["GrupoUno"].AxisY.Interval = 50;

            // Set axis labels font
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Font = new Font("Arial", 6.5F);
            grfGrupoUno.ChartAreas["GrupoUno"].AxisY.LabelStyle.Font = new Font("Arial", 6.5F);

            // Set axis labels angle
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Angle = 35;

            // Disable offset labels style
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.IsStaggered = false;

            // Enable X axis labels
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Enabled = true;

            // Enable AntiAliasing for either Text and Graphics or just Graphics
            grfGrupoUno.AntiAliasing = AntiAliasingStyles.All;
        }

        protected void llenaGraficaUno(DataTable pDatos)
        {
            if (pDatos.Rows.Count > 0)
            {
                grfGrupoUno.DataSource = pDatos;

                // Add serie Totales
                Series serieTotales = grfGrupoUno.Series.Add("totales");
                serieTotales.ChartArea = "GrupoUno";
                serieTotales.Font = new Font("Arial", 6.5F);
                serieTotales.ChartType = SeriesChartType.Column;
                serieTotales.IsValueShownAsLabel = true;
                serieTotales.XValueMember = "Estado";
                serieTotales.YValueMembers = "Totales";
                serieTotales.CustomProperties = "ShowMarkerLines=true";
                //serieTotales.PostBackValue = "item";
                serieTotales.PostBackValue ="#VALX";
                serieTotales.IsValueShownAsLabel = true;

                grfGrupoUno.DataBind();

                serieTotales.Points[1].Color = Color.Blue;          // Proceso
                serieTotales.Points[3].Color = Color.Green;         // Ejecucion
                serieTotales.Points[4].Color = Color.Red;           // Rechazo
                serieTotales.Points[2].Color = Color.Yellow;        // Hold
                serieTotales.Points[5].Color = Color.Orange;        // Suspendido
                //serieTotales.Points[6].Color = Color.DarkOrange;    // Suspendido
            }
        }

        protected void llenaGraficaUnoInstitucional(DataTable pDatos)
        {
            if (pDatos.Rows.Count > 0)
            {
                grfGrupoUno.DataSource = pDatos;

                // Add serie Totales
                Series serieTotales = grfGrupoUno.Series.Add("totales");
                serieTotales.ChartArea = "GrupoUno";
                serieTotales.Font = new Font("Arial", 6.5F);
                serieTotales.ChartType = SeriesChartType.Column;
                serieTotales.IsValueShownAsLabel = true;
                serieTotales.XValueMember = "Estado";
                serieTotales.YValueMembers = "Totales";
                serieTotales.CustomProperties = "ShowMarkerLines=true";
                //serieTotales.PostBackValue = "item";
                serieTotales.PostBackValue = "#VALX";
                serieTotales.IsValueShownAsLabel = true;

                grfGrupoUno.DataBind();

                serieTotales.Points[0].Color = Color.Blue;          // En trámite
                serieTotales.Points[1].Color = Color.Purple;        // Suspendido
                serieTotales.Points[2].Color = Color.Red;           // En Proceso
                serieTotales.Points[3].Color = Color.Yellow;        // Reenvío Trámite
                serieTotales.Points[4].Color = Color.Orange;        // En Revisión 
                serieTotales.Points[5].Color = Color.Green;         // Concluído
            }
        }

        protected void ValidarRol(int rol)
        {
            if (rol == 23) //Analista back básica
            {
                if (ad.SeleccionarPrimerTramiteDisponibleParaEjecutivoBasica().Rows.Count > 0)
                {
                    string folioAasignar = ad.SeleccionarPrimerTramiteDisponibleParaEjecutivoBasica().Rows[0][0].ToString();
                    string estado = ad.SeleccionarPrimerTramiteDisponibleParaEjecutivoBasica().Rows[0][1].ToString();
                    if (estado == "3")
                    {
                        ada.Agregar(folioAasignar, manejo_sesion.Credencial.Id.ToString());
                        Response.Redirect("Cobranza.aspx?folio=" + folioAasignar + "&cobertura=2&es=" + estado);
                    }
                }
                else
                    pintaDatos();
            }
            else if (rol == 21)// Analista (Básica)
            {
                string folio = string.Empty;
                string cobertura = string.Empty;
                string estadoFinal = string.Empty;

                if (ad.SeleccionarPrimerTramiteDisponibleParaAnalista(1).Rows.Count > 0)
                {
                    var items = ad.SeleccionarPrimerTramiteDisponibleParaAnalista(1).Rows;
                    if (items[0]["Estado"].ToString() == "1")
                    {
                        ada.Agregar(items[0]["Folio"].ToString(), manejo_sesion.Credencial.Id.ToString());
                        ad.AgregarUsuarioAsignadoATramite(manejo_sesion.Credencial.Id.ToString(), items[0]["Folio"].ToString());
                        Response.Redirect("Cobranza.aspx?folio=" + items[0]["Folio"].ToString() + "&cobertura=2&es=");
                    }
                }
                //Verifica si ya tiene un trámite asignado y lo dejó 
                else if (ad.SeleccionarTramiteAsignadoUsuario(manejo_sesion.Credencial.Id))
                {
                    string folioAsignado = ad.SeleccionarTramiteYaAsignadoAAnalista(manejo_sesion.Credencial.Id, 6);
                    if (folioAsignado != "Dato vacío")
                        Response.Redirect("Cobranza.aspx?folio=" + folioAsignado + "&cobertura=2&es=");
                    else
                        pintaDatos();
                }
                //Verifica si tiene pendiente terminar un trámite
                else if (ad.SeleccionarTramiteParaTerminarAsignadoAUsuario(manejo_sesion.Credencial.Id, ref folio, ref cobertura, ref estadoFinal))
                {
                    if (!ad.SeleccionarUltimoEstado(folio)) //Si ya cambió a estado 5
                    {
                        pintaDatos();
                    }
                    else
                        Response.Redirect("Cobranza.aspx?folio=" + folio + "&cobertura=" + cobertura + "&es=" + estadoFinal);
                }
                else
                    pintaDatos();
            }
            else if (rol == 26) //Analista (Potenciación)
            {
                string folio = string.Empty;
                string cobertura = string.Empty;
                string estadoFinal = string.Empty;

                //Validaciones
                //si no tiene un trámite asignado (se asigna o se reasigna trámite)
                if (ad.SeleccionarPrimerTramiteDisponibleParaAnalista(2).Rows.Count > 0)
                {
                    var items = ad.SeleccionarPrimerTramiteDisponibleParaAnalista(2).Rows[0];
                    if (items["Estado"].ToString() == "1")
                    {
                        ad.AgregarUsuarioAsignadoATramite(manejo_sesion.Credencial.Id.ToString(), items["Folio"].ToString());
                        ada.Agregar(items["Folio"].ToString(), manejo_sesion.Credencial.Id.ToString());
                        Response.Redirect("Cobranza.aspx?folio=" + items["Folio"].ToString() + "&cobertura=1&es=");
                    }
                }
                //si tiene un tramite pero lo dejó
                else if (ad.SeleccionarTramiteAsignadoUsuario(manejo_sesion.Credencial.Id))
                {
                    string folioAsignado = ad.SeleccionarTramiteYaAsignadoAAnalista(manejo_sesion.Credencial.Id, 7);
                    Response.Redirect("Cobranza.aspx?folio=" + folioAsignado + "&cobertura=&es=");
                }
                //si tiene uno pendiente en cualquier estado
                else if (ad.SeleccionarTramiteParaTerminarAsignadoAUsuarioPotenciacion(manejo_sesion.Credencial.Id, ref folio, ref cobertura, ref estadoFinal))
                {
                    if (ad.SeleccionarUltimoEstado(folio)) //Si ya cambió a estado 8
                    {
                        pintaDatos();
                    }
                    else if (estadoFinal == "1" || estadoFinal == "7")
                        Response.Redirect("Cobranza.aspx?folio=" + folio + "&cobertura=" + cobertura + "&es=" + estadoFinal);
                    else
                        pintaDatos();
                }
                else
                    pintaDatos();
            }
            else if (rol == 27) //Analista front potenciacion
            {
                //asignarle un trámite al vuelo
                if (ad.SeleccionarPrimerTramiteDisponibleParaAnalistaFrontPotenciacion().Rows.Count > 0)
                {
                    var obtenido = ad.SeleccionarPrimerTramiteDisponibleParaAnalistaFrontPotenciacion().Rows[0];
                    string folioAasignar = obtenido[0].ToString();
                    string estado = obtenido[1].ToString();
                    if (estado == "3" || estado == "6")
                    {
                        ada.Agregar(folioAasignar, manejo_sesion.Credencial.Id.ToString());
                        Response.Redirect("Cobranza.aspx?folio=" + folioAasignar + "&cobertura=1&es=" + estado);
                    }
                }
                else if (ad.SeleccionarTramiteDevueltoParaRevision().Rows.Count > 0) //Verificar si se ha devuelto un tramite para revisión
                {
                    var obtenido = ad.SeleccionarTramiteDevueltoParaRevision().Rows[0];
                    string folioAasignar = obtenido[0].ToString();
                    string estado = obtenido[1].ToString();
                    Response.Redirect("Cobranza.aspx?folio=" + folioAasignar + "&cobertura=2&es=" + estado);
                }
                else
                    pintaDatos();
            }
            else if (rol == 28) //Analista back potenciacion
            {
                //asignarle un tramite al vuelo
                if (ad.SeleccionarPrimerTramiteDisponibleParaAnalistaBackPotenciacion().Rows.Count > 0)
                {
                    var obtenido = ad.SeleccionarPrimerTramiteDisponibleParaAnalistaBackPotenciacion().Rows[0];
                    string folioAsignar = obtenido[0].ToString();
                    string estado = obtenido[1].ToString();
                    if (estado == "5" || estado == "7")
                    {
                        ada.Agregar(folioAsignar, manejo_sesion.Credencial.Id.ToString());
                        //ad.AgregarUsuarioAsignadoATramite(manejo_sesion.Credencial.Id.ToString(), folioAsignar);
                        Response.Redirect("Cobranza.aspx?folio=" + folioAsignar + "&cobertura=2&es=" + estado);
                    }
                }
                else
                    pintaDatos();
            }
            else if (rol == 20) //Supervisor
            {
                pintaTituloPromotoria();
                pintaDatos();
                Session.Contents.Remove("nota");
            }
            else if (rd.SeleccionarRolEnDependencia(rol)) //Dependencias
            {
                pintaTituloPromotoria();
                if (ad.SeleccionarPorCoberturaBasicaDependencia(rol).Rows.Count == 6)
                    llenaGraficaUnoInstitucional(ad.SeleccionarPorCoberturaBasicaDependencia(rol));
                else
                {
                    llenaGraficaUnoInstitucional(ad.SeleccionarPorCoberturaPotenciadaDependencia(rol));
                }
            }
            else
            {
                pintaTituloPromotoria();
                pintaDatos();
                Session.Contents.Remove("nota");
                RblCobertura.Visible = false;
            }
        }

        protected void ValidarRolBasica(int rol)
        {
            if (rol == 20) //Supervisor
                llenaGraficaUnoInstitucional(ad.SeleccionarPorCoberturaBasicaSupervisor());
            if (rd.SeleccionarRolEnDependencia(rol)) //Dependencia
                llenaGraficaUnoInstitucional(ad.SeleccionarPorCoberturaBasicaDependencia(rol));
            if (rol == 21 || rol == 26) //Analistas Básica/Potenciación
                llenaGraficaUnoInstitucional(ad.SeleccionarPorCoberturaBasicaUsuario(manejo_sesion.Credencial.Id));
        }

        protected void ValidarRolPotenciacion(int rol)
        {
            if (rol == 20) //Supervisor
                llenaGraficaUnoInstitucional(ad.SeleccionarPorCoberturaPotenciadaSupervisor());
            if (rd.SeleccionarRolEnDependencia(rol)) //Dependencia
                llenaGraficaUnoInstitucional(ad.SeleccionarPorCoberturaPotenciadaDependencia(rol));
            if (rol == 21 || rol == 26) //Analistas Básica/Potenciación
                llenaGraficaUnoInstitucional(ad.SeleccionarPorCoberturaPotenciadaUsuario(manejo_sesion.Credencial.Id));
        }
        #endregion
    }
}