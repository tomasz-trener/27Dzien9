using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P02Wstawki
{
    public partial class Default : System.Web.UI.Page
    {
        public string[] Imiona { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Imiona = new string[]{ "jan","adam","ola"};
        }
    }
}