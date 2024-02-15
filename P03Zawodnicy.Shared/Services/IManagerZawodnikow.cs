using P03Zawodnicy.Shared.Domain;
using P04Zawodnicy.Shared.Domain;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03Zawodnicy.Shared.Services
{
    public interface IManagerZawodnikow
    {
        Zawodnik[] WczytajZawodnikow();

        string[] PodajKraje();

        Zawodnik[] PodajZawodnikow(string kraj);
        double PodajSredniWzrost(string kraj);
        void PosorotujZawodnikowPoNazwisku(Zawodnik[] posortowaniZawodnicy);
        void Edytuj(Zawodnik edytowany);

        void Dodaj(Zawodnik z);

        void Usun(int id);

        

        Trener[] PodajTrenerow();
        double PodajSredniWiekZawodnikow(string kraj);

        List<Osoba> WyszukajOsoby(string fragmentNazwy);

        GrupaKraju[] PodajSredniWzrostDlaKazdegoKraju();

        Zawodnik PodajZawodnika(int id);
         
    }
}
