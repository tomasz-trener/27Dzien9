using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P01AplikacjaWebowaWstep
{
    public partial class Kalkulator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPolicz_Click(object sender, EventArgs e)
        {
            int a =Convert.ToInt32(txtLiczba1.Text);
            int b = Convert.ToInt32(txtLiczba2.Text);
            int c = a + b;

            lblWynik.Text = c.ToString();
        }
    }
}