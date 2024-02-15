using P03Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Domain
{
    public class Trener : Osoba
    {
        public int Id { get; set; }
 
        public string PelneImie => $"{Imie} {Nazwisko}";

        
    }
}
