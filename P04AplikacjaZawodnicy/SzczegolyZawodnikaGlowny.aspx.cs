using P03Zawodnicy.Shared.Services;
using P04Zawodnicy.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P04AplikacjaZawodnicy
{
    public partial class SzczegolyZawodnikaGlowny : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idStr = Request["id"];

            if (!string.IsNullOrEmpty(idStr))
            {
                int id = Convert.ToInt32(idStr);

                IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
                var zawodnik = mz.PodajZawodnika(id);


            }
        }
    }
}