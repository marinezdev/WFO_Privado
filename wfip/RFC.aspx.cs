using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// using RfcFacil;

namespace wfip
{
    public partial class RFC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //var rfc = RfcBuilder.ForNaturalPerson()
            //    .WithName(txtNombres.Text)
            //    .WithFirstLastName(txtAPaterno.Text)
            //    .WithSecondLastName(txtAMaterno.Text)
            //    .WithDate(int.Parse(txtAnn.Text), int.Parse(txtMes.Text), int.Parse(txtDia.Text))
            //    .Build();
            //lblObtenido.Text = rfc.ToString();
        }

        protected void btnaceptarE_Click(object sender, EventArgs e)
        {
            //var rfc = RfcBuilder.ForJuristicPerson();
            //lblObtenido.Text = "No disponble";
        }

        protected void rblTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTipo.SelectedValue == "1")
            {
                rfcPersona1.Visible = true;
                rfcPersona2.Visible = true;
                rfcPersona3.Visible = true;
                rfcPersona4.Visible = true;
                rfcPersona5.Visible = true;
                rfcEmpresa1.Visible = false;
                rfcEmpresa2.Visible = false;
                rfcEmpresa3.Visible = false;
            }
            else if (rblTipo.SelectedValue == "2")
            {
                rfcPersona1.Visible = false;
                rfcPersona2.Visible = false;
                rfcPersona3.Visible = false;
                rfcPersona4.Visible = false;
                rfcPersona5.Visible = false;
                rfcEmpresa1.Visible = true;
                rfcEmpresa2.Visible = true;
                rfcEmpresa3.Visible = true;
            }

        }
    }
}