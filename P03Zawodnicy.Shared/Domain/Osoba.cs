using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03Zawodnicy.Shared.Domain
{
    public class Osoba : IComparable<Osoba>
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public DateTime? DataUrodzenia { get; set; }

        public int CompareTo(Osoba inna)
        {
            int pora1 = PoraRoku(this.DataUrodzenia);
            int pora2 = PoraRoku(inna.DataUrodzenia);

            return pora1.CompareTo(pora2);
        }

        private int PoraRoku(DateTime? data)
        {
            if (data == null)
                return 5;

            if (data.Value.Month >= 3 && data.Value.Month <= 5) return 1; // Wiosna
            if (data.Value.Month >= 6 && data.Value.Month <= 8) return 2; // Lato
            if (data.Value.Month >= 9 && data.Value.Month <= 11) return 3; // Jesien
            return 4; // zima
        }
    }
}
