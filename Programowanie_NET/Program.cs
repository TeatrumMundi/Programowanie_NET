using System;

namespace Programowanie_NET
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Samochod s1 = new Samochod();
            
            s1.WypiszInfo();
            
            s1._setMarka("Fiat");
            s1._setModel("126p");
            s1._setIloscDrzwi(2);
            s1.SetPojemnoscSilnika(650);
            s1.SetSrednieSpalanie(6.0);
            
            s1.WypiszInfo();
            
            Samochod s2 = new Samochod("Syrena", "105", 2, 800, 7.6);
            
            s2.WypiszInfo();
            
            double kosztPrzejazdu = s2.ObliczKosztPrzejazdu(30.5, 4.85);
            Console.WriteLine("Koszt przejazdu: " + kosztPrzejazdu);
            
            Console.ReadKey();
        }
    }
}