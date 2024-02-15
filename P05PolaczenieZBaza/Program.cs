using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P05PolaczenieZBaza
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PolaczenieZBaza pzb = new PolaczenieZBaza();

            object[][] wynik=  pzb.WyslijPolecenieSQL("select * from zawodnicy");

            foreach (object[] wiersz in wynik)
                Console.WriteLine(string.Join(" ", wiersz));

            Console.ReadLine();
        }
    }
}
