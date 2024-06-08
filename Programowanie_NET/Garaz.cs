using System;

namespace Programowanie_NET
{
    public class Garaz
    {
        private string _adres;
        private int _pojemnosc;
        private int _liczbaSamochodow;
        private Samochod[] _samochody;

        public Garaz()
        {
            _adres = "nieznany";
            _pojemnosc = 0;
            _samochody = null;
        }
        
        // Konstruktor klasy Garaz
        public Garaz(string adres, int pojemnosc)
        {
            _adres = adres;
            _pojemnosc = pojemnosc;
            _samochody = new Samochod[pojemnosc];
        }

        // Metoda do dodawania samochodów do garażu
        public void WprowadzSamochod(Samochod nowySamochod)
        {
            if (_liczbaSamochodow < _pojemnosc)
            {
                _samochody[_liczbaSamochodow] = nowySamochod;
                _liczbaSamochodow++;
            } 
            Console.WriteLine("Garaż jest pełny.");
        }

        // Metoda do usuwania samochodów z garażu
        public Samochod WyprowadzSamochod()
        {
            if (_liczbaSamochodow == 0)
            {
                Console.WriteLine("Garaż jest pusty.");
                return null;
            }
            else
            {
                Samochod samochod = _samochody[_liczbaSamochodow - 1];
                _samochody[_liczbaSamochodow - 1] = null;
                _liczbaSamochodow--;
                return samochod;
            }
        }
        
        // Metoda WypiszInfo
        public void WypiszInfo()
        {
            Console.WriteLine($"Adres: {_adres}");
            Console.WriteLine($"Pojemnosc: {_pojemnosc}");
            Console.WriteLine($"Liczba samochodów: {_liczbaSamochodow}");

            for (int i = 0; i <  _liczbaSamochodow; i++)
            {
                Console.WriteLine($"\nSamochod {i + 1}:");
                _samochody[i].WypiszInfo();
            }
        }
        
        public string Adres
        {
            get => _adres;
            set => _adres = value;
        }

        public int Pojemnosc
        {
            get => _pojemnosc;
            set
            {
                _pojemnosc = value;
                _samochody = new Samochod[_pojemnosc];
            }
        }
    }

}