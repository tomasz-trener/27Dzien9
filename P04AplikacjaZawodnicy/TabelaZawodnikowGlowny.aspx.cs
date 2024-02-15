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
    public partial class TabelaZawodnikowGlowny : System.Web.UI.Page
    {
        public int? IdPodswietlanego { get; set; }
        public List<int> NowoDodaniZawodnicy { get; set; }
        public Zawodnik[] Zawodnicy { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();

            // usuwanie 
            string idUsuwanegoStr = Request["idUsuwanego"];
            if (!string.IsNullOrEmpty(idUsuwanegoStr))
            {
                int idUsuwanego = Convert.ToInt32(idUsuwanegoStr);
                mz.Usun(idUsuwanego);
            }

            Zawodnicy = mz.WczytajZawodnikow();

            //podswietlenie edytowanego zawodnika
            string idPodswietlonegoStr = Request["podswietlonyId"];
            if (!string.IsNullOrEmpty(idPodswietlonegoStr))
            {
                IdPodswietlanego = Convert.ToInt32(idPodswietlonegoStr);
            }

            //podswietlenie nowo dodanych
            if (Session["nowoDodaniZawodnicy"] != null)
                NowoDodaniZawodnicy = Session["nowoDodaniZawodnicy"] as List<int>;
        }
    }
}