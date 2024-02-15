using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P05PolaczenieZBaza
{
    public class PolaczenieZBaza
    {
        private readonly string connectionString;

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }
        public PolaczenieZBaza()
        {
            connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=A_Zawodnicy;Integrated Security=True";
        }

        public PolaczenieZBaza(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public object[][] WyslijPolecenieSQL(string sql)
        {
            SqlConnection connection; // nawiazywanie polaczenia z baza 
            SqlCommand command; // przechowywanie polecen SQL 
            SqlDataReader sqlDataReader; // czytanie wyników z bazy

        
            connection = new SqlConnection(connectionString);
            command = new SqlCommand(sql, connection);
            connection.Open();
            sqlDataReader = command.ExecuteReader();

            int liczbakolumn = sqlDataReader.FieldCount;
            List<object[]> listaWierszy = new List<object[]>();

            while (sqlDataReader.Read())
            {
                object[] komorki = new object[liczbakolumn];
                for (int i = 0; i < liczbakolumn; i++) 
                    komorki[i] = sqlDataReader.GetValue(i);

                listaWierszy.Add(komorki);
            }

            connection.Close();
            return listaWierszy.ToArray();
        }


        public (string[] naglowki, object[][]) WyslijPolecenieSQLPlusNaglowki(string sql)
        {
            SqlConnection connection; // nawiazywanie polaczenia z baza 
            SqlCommand command; // przechowywanie polecen SQL 
            SqlDataReader sqlDataReader; // czytanie wyników z bazy


            connection = new SqlConnection(connectionString);
            command = new SqlCommand(sql, connection);
            connection.Open();
            sqlDataReader = command.ExecuteReader();

            int liczbakolumn = sqlDataReader.FieldCount;
            List<object[]> listaWierszy = new List<object[]>();

            string[] naglowki = new string[liczbakolumn];
            for (int i = 0; i < liczbakolumn; i++)
                naglowki[i] = sqlDataReader.GetName(i);


            while (sqlDataReader.Read())
            {
                object[] komorki = new object[liczbakolumn];
                for (int i = 0; i < liczbakolumn; i++)
                    komorki[i] = sqlDataReader.GetValue(i);

                listaWierszy.Add(komorki);
            }

            connection.Close();
            return (naglowki, listaWierszy.ToArray());
        }

    }
}
