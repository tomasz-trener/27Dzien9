using P03Zawodnicy.Shared.Services;
using P04Zawodnicy.Shared.Services;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P04AplikacjaZawodnicy
{
    public partial class TabelaDashboardBezMaster : System.Web.UI.Page
    {
        public Zawodnik[] Zawodnicy { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
            Zawodnicy = mz.WczytajZawodnikow();
        }
    }
}