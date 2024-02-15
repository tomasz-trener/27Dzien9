
using P03Zawodnicy.Shared.Domain;
using P03Zawodnicy.Shared.Services;
using P04Zawodnicy.Shared.Domain;
using P05PolaczenieZBaza;
using P06Zawodnicy.Shared.Domain;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    //ten będzie działać na bazie danych
    public class ManagerZawodnikow : IManagerZawodnikow
    {
        //private List<Zawodnik> zawodnicyCache;
        //const string url = @"C:\dane\zawodnicy.txt";

         PolaczenieZBaza pzb = new PolaczenieZBaza();

        public Zawodnik[] WczytajZawodnikow()
        {
            object[][] dane = pzb.WyslijPolecenieSQL("select id_zawodnika, id_trenera, imie, nazwisko, kraj, data_ur, wzrost,waga from zawodnicy");

            mapujZawodnikow(dane, out Zawodnik[] zawodnicy);
            return zawodnicy;
        }

        public string[] PodajKraje()
        {
            object[][] dane = pzb.WyslijPolecenieSQL("select distinct kraj from zawodnicy");

            string[] kraje = new string[dane.Length];
            for (int i = 0; i < dane.Length; i++)
                kraje[i] = (string)dane[i][0];

            return kraje;
            
        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            object[][] dane = pzb.WyslijPolecenieSQL($"select id_zawodnika, id_trenera, imie, nazwisko, kraj, data_ur, wzrost , waga from zawodnicy where kraj='{kraj}'");

            mapujZawodnikow(dane, out Zawodnik[] zawodnicy);
            return zawodnicy;
        }

        public double PodajSredniWzrost(string kraj)
        {
            object[][] dane = pzb.WyslijPolecenieSQL($"select avg(wzrost) from zawodnicy where kraj = '{kraj}'");
            return dane[0][0] == DBNull.Value ? double.NaN : Convert.ToDouble(dane[0][0]);
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

        public void Edytuj(Zawodnik edytowany)
        {
            string id_trenera = edytowany.Id_trenera == null ? "null" : edytowany.Id_trenera.ToString();
            string dataUr = edytowany.DataUrodzenia == null ? "null" : "'" + edytowany.DataUrodzenia.Value.ToString("yyyyMMdd") + "'";


            string sql = $@"update zawodnicy set 
                            id_trenera = '{id_trenera}',
                            imie = '{edytowany.Imie}', 
                            nazwisko= '{edytowany.Nazwisko}',
                            kraj='{edytowany.Kraj}',
                            data_ur={dataUr},
                            wzrost ={edytowany.Wzrost}, 
                            waga ={edytowany.Waga}
                            where id_zawodnika ={edytowany.Id_zawodnika}";

            pzb.WyslijPolecenieSQL(sql);
        }

        // podataność na atak SQL injection
        // np: podczas podawania kraju podać:
        // POL','20240101',1,1); drop table zawodnicy--
        public void Dodaj(Zawodnik z)
        {
            string szablon = "insert into zawodnicy (id_trenera,imie, nazwisko,kraj,data_ur,wzrost,waga) values ({0},'{1}','{2}','{3}','{4}',{5},{6})";

            string dataUr = z.DataUrodzenia == null ? "null" : "'" + z.DataUrodzenia.Value.ToString("yyyyMMdd") + "'";

            string sql = string.Format(szablon,
                z.Id_trenera == null ? "null" : z.Id_trenera.ToString(),
                z.Imie, z.Nazwisko, z.Kraj, dataUr, z.Wzrost, z.Waga);

            pzb.WyslijPolecenieSQL(sql);
        }

        public void Usun(int id)
        {
            pzb.WyslijPolecenieSQL($"delete zawodnicy where id_zawodnika = {id}");
        }

        private void mapujZawodnikow(object[][] dane, out Zawodnik[] zawodnicy)
        {
            zawodnicy = new Zawodnik[dane.Length];
            for (int i = 0; i < dane.Length; i++)
            {
                var w = dane[i]; // i-ty wiersz 
                Zawodnik z = new Zawodnik();
                z.Id_zawodnika = (int)w[0];

                if (w[1] != DBNull.Value)
                    z.Id_trenera = (int)w[1];

                z.Imie = (string)w[2];
                z.Nazwisko = (string)w[3];
                z.Kraj = (string)w[4];

                if (w[5] != DBNull.Value)
                    z.DataUrodzenia = (DateTime)w[5];

                if (w[6] != DBNull.Value)
                    z.Wzrost = (int)w[6];

                if (w[7] != DBNull.Value)
                    z.Waga = (int)w[7];

                zawodnicy[i] = z;
            }
        }
        
        public Trener[] PodajTrenerow()
        {
            object[][] dane = pzb.WyslijPolecenieSQL("select id_trenera, imie_t, nazwisko_t, data_ur_t from trenerzy");
            Trener[] trenerzy = new Trener[dane.Length];
            for (int i = 0; i < dane.Length; i++)
            {
                trenerzy[i] = new Trener()
                {
                    Id = (int)dane[i][0],
                    Imie = (string)dane[i][1],
                    Nazwisko = (string)dane[i][2],
                };

                if (dane[i][3] != DBNull.Value)
                    trenerzy[i].DataUrodzenia = (DateTime)dane[i][3];
            }
            return trenerzy;
        }


        /*
         * select AVG(year(getdate()) - year(data_ur)) from zawodnicy



            create procedure SredniWiekZawodnikow
	            @Kraj varchar(3),
	            @SredniWiek INT OUTPUT 
            as
            begin
	            select @SredniWiek= AVG(year(getdate()) - year(data_ur)) from zawodnicy where kraj = @kraj
            end
                    */

        public double PodajSredniWiekZawodnikow(string kraj)
        {
            using (SqlConnection connection = new SqlConnection(pzb.ConnectionString))
            {
                SqlCommand command = new SqlCommand("SredniWiekZawodnikow", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Kraj", kraj));

                SqlParameter sredniWiekParam = new SqlParameter("@SredniWiek", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output,
                };
                command.Parameters.Add(sredniWiekParam);

                connection.Open();
                command.ExecuteNonQuery();

                if (sredniWiekParam.Value != DBNull.Value)
                {
                    return (int)sredniWiekParam.Value;
                }
                else
                {
                    throw new Exception("Nie udało sie policzyć średniego wieku ");
                }

               // connection.Close(); nie trzeba tego pisac 
            }
            
        }

        public List<Osoba> WyszukajOsoby(string fragmentNazwy)
        {
            List<Osoba> osoby = new List<Osoba>();
            osoby.AddRange(WczytajZawodnikow());
            osoby.AddRange(PodajTrenerow());

            List<Osoba> wyniki = new List<Osoba>();

            foreach (var o in osoby)
            {
                if(o.Imie.ToLower().Contains(fragmentNazwy.ToLower()) || o.Nazwisko.ToLower().Contains(fragmentNazwy.ToLower()))
                    wyniki.Add(o);
            }

            wyniki.Sort();
            return wyniki;
        }

        public GrupaKraju[] PodajSredniWzrostDlaKazdegoKraju()
        {
            throw new NotImplementedException();
        }
    }
}


// komunikacja z bazą danych może przebiegac na 3 sposoby :

//1) Polecenia SQL , parametryzacja zapytań
//2) procedury wbudowane 
//3) ORM (object-relation-mapping) 