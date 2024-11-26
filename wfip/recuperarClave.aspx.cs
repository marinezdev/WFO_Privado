using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using w = wfiplib;

namespace wfip
{
    public partial class recuperarClave : System.Web.UI.Page
    {
        w.admCredencial credencial = new w.admCredencial();
        w.admModulos modulos = new w.admModulos();
        w.Menu menu = new w.Menu();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lblMensajes.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).ToString("yyyyMMdd") + " " + DateTime.Now.ToLongTimeString();
                //lblMensajes.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                //LLenarTreeViewLocal();
                //LlenarTreeViewMenu();
                //lblMenu.Text = CrearMenu(1, 0);
                //CrearMainMenu();
                //lblMenuDinamico.Text = CrearMenu();
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //Proceso de recuperación de contraseña
            Mensajes mensajes = new Mensajes();
            w.Correo correo = new  w.Correo();
            string resultado = "";
            if (credencial.usuarioClaveRecuperada(txtUsuario.Text.Trim(), txtEMail.Text.Trim(), ref resultado))
            {
                string mensajesalida = "Tu clave es: " + resultado;
                string strRespuesta = "";
                strRespuesta = correo.ProcesarCorreo(txtEMail.Text.Trim(), "wto@asae.com.mx", "Recuperación de contraseña olvidada", mensajesalida);
                txtUsuario.Text = "";
                if (strRespuesta.Length > 0)
                {
                    if (strRespuesta == "Envío éxitoso.")
                    {
                        txtUsuario.Text = "";
                        txtEMail.Text = "";
                        lblMensajes.Text = "El acceso para recuperar tu contraseña se ha enviado a tu correo.";
                        mensajes.MostrarMensaje(this, "El acceso para recuperar tu contraseña se ha enviado a tu correo.");
                    }
                    else
                    {
                        txtUsuario.Text = "";
                        txtEMail.Text = "";
                        lblMensajes.Text = "No no enío el correo electrónico.";
                        mensajes.MostrarMensaje(this, "No no enío el correo electrónico.");
                    }
                }
                else
                {
                    txtUsuario.Text = "";
                    txtEMail.Text = "";
                    lblMensajes.Text = "Envío de Correo Elctrónico no se ha configurado.";
                    mensajes.MostrarMensaje(this, "Envío de Correo Elctrónico no se ha configurado.");
                }
            }
            else
            {
                txtUsuario.Text = "";
                txtEMail.Text = "";
                lblMensajes.Text = "No se reconoce el usuario / correo ingresado.";
                mensajes.MostrarMensaje(this, "No se reconoce el usuario / correo ingresado.");
            }
        }

        protected void btnAceptar2_Click(object sender, EventArgs e)
        {
            //Proceso para cifrar una clave
            //lblMensajes.Text = w.Cifrado.Encriptar(txtClaveACifrar.Text);
            
        }

        protected void LLenarTreeViewLocal()
        {
            //modulos = new w.admModulos();
            modulos.ObtenerModulos_TreeView(ref tvwOpciones);
        }

        protected void LlenarTreeViewMenu()
        {
            //menu = new w.Menu();
            menu.MenuLlenar_TreeView(ref tvwMenu);
            
        }

        //public string CrearMenu()
        //{
            //menu = new w.Menu();

            //string menuConstruido = "<div id='cssmenu'><ul>";

            //foreach (var padre in menu.MenuDinamicoObtener())
            //{
            //    if (int.Parse(padre.PerteneceA.ToString()) == 0)
            //    {
            //        menuConstruido += "<li class='has-sub'><a href='"+ padre.URL +"'>" + padre.Descripcion + "</a>";

            //        menuConstruido += "<ul>";
            //        foreach (var hijo1 in menu.MenuDinamicoObtener())
            //        {
            //            if (int.Parse(hijo1.PerteneceA.ToString()) == padre.IdMenu)
            //            {
            //                menuConstruido += "<li class='has-sub'><a href='"+ hijo1.URL +"'>" + hijo1.Descripcion + "</a>";

            //                menuConstruido += "<ul>";
            //                foreach (var hijo2 in menu.MenuDinamicoObtener())
            //                {
            //                    if (int.Parse(hijo2.PerteneceA.ToString()) == hijo1.IdMenu)
            //                    {
            //                        menuConstruido += "<li class='has-sub'><a href='"+ hijo2.URL+"'>" + hijo2.Descripcion + "</a>";

            //                        menuConstruido += "<ul>";
            //                        foreach (var hijo3 in menu.MenuDinamicoObtener())
            //                        {
            //                            if (int.Parse(hijo3.PerteneceA.ToString()) == hijo2.IdMenu)
            //                            {
            //                                menuConstruido += "<li><a href='"+ hijo3.URL +"'>" + hijo3.Descripcion + "</a></li>";
            //                            }
            //                        }
            //                        menuConstruido += "</ul></li>";
            //                    }
            //                }
            //                menuConstruido += "</li></ul>";
            //            }
            //        }
            //        menuConstruido += "</ul></li>";
            //   }
            //}

            //menuConstruido += "</ul></div>";

            //return menuConstruido;
        //}

        public void CrearMainMenu()
        {
            //menu = new w.Menu();
            List<w.MenuPropiedades> pmenu = new List<w.MenuPropiedades>();
            pmenu = menu.MenuDinamicoObtener(1);

            foreach (var padre in pmenu)
            {
                //Padre
                if (padre.PerteneceA == 0) 
                {
                    MenuItem menuitemPadre = new MenuItem(padre.Descripcion);       
                    MainMenu.Items.Add(menuitemPadre);

                    //hijo 1
                    foreach (var hijo1 in pmenu)
                    {
                        //hijo pertenece a Padre
                        if (hijo1.PerteneceA == padre.IdMenu) 
                        {
                            MenuItem ChildMenuItem1 = new MenuItem(hijo1.Descripcion);
                            ChildMenuItem1.NavigateUrl = hijo1.URL;
                            menuitemPadre.ChildItems.Add(ChildMenuItem1);

                            //hijo 2
                            foreach (var hijo2 in pmenu)
                            {
                                if (hijo2.PerteneceA == hijo1.IdMenu)
                                {
                                    MenuItem ChildMenuItem2 = new MenuItem(hijo2.Descripcion);
                                    ChildMenuItem2.NavigateUrl = hijo2.URL;
                                    ChildMenuItem1.ChildItems.Add(ChildMenuItem2);

                                    //hijo 3
                                    foreach (var hijo3 in pmenu)
                                    {
                                        if (hijo3.PerteneceA == hijo2.IdMenu)
                                        {
                                            MenuItem ChildMenuItem3 = new MenuItem(hijo3.Descripcion);
                                            ChildMenuItem3.NavigateUrl = hijo3.URL;
                                            ChildMenuItem2.ChildItems.Add(ChildMenuItem3);

                                            //hijo4
                                            foreach (var hijo4 in pmenu)
                                            {
                                                if (hijo4.PerteneceA == hijo3.IdMenu)
                                                {
                                                    MenuItem ChildMenuItem4 = new MenuItem(hijo4.Descripcion);
                                                    ChildMenuItem4.NavigateUrl = hijo4.URL;
                                                    ChildMenuItem3.ChildItems.Add(ChildMenuItem4);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}