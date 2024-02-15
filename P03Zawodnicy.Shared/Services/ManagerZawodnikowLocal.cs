
using P03Zawodnicy.Shared.Domain;
using P03Zawodnicy.Shared.Services;
using P04Zawodnicy.Shared.Domain;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    public class ManagerZawodnikowLocal : IManagerZawodnikow
    {
        private List<Zawodnik> zawodnicyCache;
        const string url = @"C:\dane\zawodnicy.txt";

        public Zawodnik[] WczytajZawodnikow()
        {
            // string url = "http://tomaszles.pl/wp-content/uploads/2019/06/zawodnicy.txt";


            WebClient wc = new WebClient();
            string dane = wc.DownloadString(url);

            string[] wiersze = dane.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Zawodnik[] zawodnicy = new Zawodnik[wiersze.Length - 1];

            for (int i = 1; i < wiersze.Length; i++)
            {
                string[] komorki = wiersze[i].Split(';');

                Zawodnik z = new Zawodnik();
                z.Id_zawodnika = Convert.ToInt32(komorki[0]);

                if (!string.IsNullOrEmpty(komorki[1]))
                    z.Id_trenera = Convert.ToInt32(komorki[1]);

                z.Imie = komorki[2];
                z.Nazwisko = komorki[3];
                z.Kraj = komorki[4];
                z.DataUrodzenia = Convert.ToDateTime(komorki[5]);
                z.Wzrost = Convert.ToInt32(komorki[6]);
                z.Waga = Convert.ToInt32(komorki[7]);

                zawodnicy[i - 1] = z;
            }
            zawodnicyCache = zawodnicy.ToList();
            return zawodnicy;
        }

        public string[] PodajKraje()
        {

            // unikam ponownego wczytania danych dzieki zastosowaniu cache'u
            // Zawodnik[] zawodnicy = WczytajZawodnikow();

            Zawodnik[] zawodnicy = zawodnicyCache.ToArray();

            if (zawodnicyCache == null)
                throw new Exception("Najpierw wczytaj zawodnikow");

            HashSet<string> kraje = new HashSet<string>();
            foreach (var z in zawodnicyCache)
                kraje.Add(z.Kraj);

            // konwersja HashSet do list aby móc sortować
            List<string> posortowaneKraje = kraje.ToList();
            posortowaneKraje.Sort(); // sortowanie alfabetycznie 
                                     //posortowaneKraje.Reverse(); // ewentualnie mozna odwrócić kolekność 

            return posortowaneKraje.ToArray();
        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            List<Zawodnik> zawodnicy = new List<Zawodnik>();
            foreach (var z in zawodnicyCache)
                if (z.Kraj == kraj)
                    zawodnicy.Add(z);



            Zawodnik[] zawPosortowani = zawodnicy.ToArray();
            PosorotujZawodnikowPoNazwisku(zawPosortowani);
            return zawPosortowani;
        }

        public double PodajSredniWzrost(string kraj)
        {
            Zawodnik[] zawodnicy = PodajZawodnikow(kraj);

            double suma = 0;
            foreach (var z in zawodnicy)
                suma += z.Wzrost;

            return suma / zawodnicy.Length;
        }

        public void PosorotujZawodnikowPoNazwisku(Zawodnik[] posortowaniZawodnicy)
        {
            for (int i = 0; i < posortowaniZawodnicy.Length - 1; i++)
            {
                for (int j = 0; j < posortowaniZawodnicy.Length - i - 1; j++)
                {
                    if (string.Compare(posortowaniZawodnicy[j].Nazwisko, posortowaniZawodnicy[j + 1].Nazwisko) > 0)
                    {
                        // zamiana miejscami 
                        Zawodnik temp = posortowaniZawodnicy[j];
                        posortowaniZawodnicy[j] = posortowaniZawodnicy[j + 1];
                        posortowaniZawodnicy[j + 1] = temp;
                    }
                }
            }
        }

        public void Zapisz()
        {
            const string naglowek = "id_zawodnika;id_trenera;imie;nazwisko;kraj;data urodzenia;wzrost;waga";
            const string szablon = "{0};{1};{2};{3};{4};{5};{6};{7}";

            StringBuilder sb = new StringBuilder(naglowek + Environment.NewLine);
            foreach (var z in zawodnicyCache)
            {
                string data_ur = z.DataUrodzenia == null ? "" : z.DataUrodzenia.Value.ToString("yyyy-MM-dd");
                string wiersz = string.Format(szablon,
                    z.Id_zawodnika, z.Id_trenera, z.Imie, z.Nazwisko, z.Kraj, data_ur, z.Wzrost, z.Waga);
                sb.AppendLine(wiersz);
            }
            File.WriteAllText(url, sb.ToString(), Encoding.UTF8);
        }

        public void Dodaj(Zawodnik zawodnik)
        {
            int maksId = 0;
            foreach (var z in zawodnicyCache)
                if (z.Id_zawodnika > maksId)
                    maksId = z.Id_zawodnika;

            zawodnik.Id_zawodnika = maksId + 1;
            zawodnicyCache.Add(zawodnik);

        }

        public void Usun(int id)
        {
            Zawodnik zawodnikDoUsuniecia = null;
            foreach (var z in zawodnicyCache)
                if (z.Id_zawodnika == id)
                {
                    zawodnikDoUsuniecia = z;
                    break;
                }

            zawodnicyCache.Remove(zawodnikDoUsuniecia);
        }

        public void Edytuj(Zawodnik edytowany)
        {
            throw new NotImplementedException();
        }

        public Trener[] PodajTrenerow()
        {
            throw new NotImplementedException();
        }

        public double PodajSredniWiekZawodnikow(string kraj)
        {
            throw new NotImplementedException();
        }

        public List<Osoba> WyszukajOsoby(string fragmentNazwy)
        {
            throw new NotImplementedException();
        }

        public GrupaKraju[] PodajSredniWzrostDlaKazdegoKraju()
        {
            throw new NotImplementedException();
        }
    }
}
