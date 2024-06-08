using System;

namespace Programowanie_NET
{
    public class Samochod
    {
        // Prywatne pola klasy
        private String _marka;
        private String _model;
        private int _iloscDrzwi;
        private double _pojemnoscSilnika;
        private double _srednieSpalanie;
        
        // Prywatne statyczne pole klasy
        private static int _iloscSamochodow;

        // Konstruktor domyślny
        public Samochod() {
            this._marka = "nieznana";
            this._model = "nieznany";
            this._iloscDrzwi = 0;
            this._pojemnoscSilnika = 0.0;
            this._srednieSpalanie = 0.0;
            _iloscSamochodow++; // Inkrementacja liczby samochodów przy każdym utworzeniu obiektu
        }
        
        // Konstruktor klasy z parametrami
        public Samochod(String marka, String model, int iloscDrzwi, double pojemnoscSilnika, double srednieSpalanie) {
            this._marka = marka;
            this._model = model;
            this._iloscDrzwi = iloscDrzwi;
            this._pojemnoscSilnika = pojemnoscSilnika;
            this._srednieSpalanie = srednieSpalanie;
            _iloscSamochodow++; // Inkrementacja liczby samochodów przy każdym utworzeniu obiektu
        }
        
        // Prywatna metoda obliczająca spalanie
        private double ObliczSpalanie(double dlugoscTrasy) {
            return (_srednieSpalanie * dlugoscTrasy) / 100.0;
        }

        // Publiczna metoda obliczająca koszt przejazdu
        public double ObliczKosztPrzejazdu(double dlugoscTrasy, double cenaPaliwa) {
            double spalanie = ObliczSpalanie(dlugoscTrasy);
            return spalanie * cenaPaliwa;
        }
        
        // Publiczna metoda wypisująca informacje o samochodzie
        public void WypiszInfo() {
            Console.WriteLine("Marka: " + _marka);
            Console.WriteLine("Model: " + _model);
            Console.WriteLine("Ilość drzwi: " + _iloscDrzwi);
            Console.WriteLine("Pojemność silnika: " + _pojemnoscSilnika);
            Console.WriteLine("Średnie spalanie: " + _srednieSpalanie);
            Console.WriteLine("Ilość samochodów: " + _iloscSamochodow);
        }
        
        // Publiczna statyczna metoda wypisująca ilość samochodów
        public static void WypiszIloscSamochodow() {
            Console.WriteLine("Ilość samochodów: " + _iloscSamochodow);
        }
        
        // Gettery i settery dla każdego z pól
        public String _getMarka() {
            return _marka;
        }

        public void _setMarka(String marka) {
            this._marka = marka;
        }

        public String _getModel() {
            return _model;
        }

        public void _setModel(String model) {
            this._model = model;
        }

        public int _getIloscDrzwi() {
            return _iloscDrzwi;
        }

        public void _setIloscDrzwi(int iloscDrzwi) {
            this._iloscDrzwi = iloscDrzwi;
        }

        public double _getPojemnoscSilnika() {
            return _pojemnoscSilnika;
        }

        public void SetPojemnoscSilnika(double pojemnoscSilnika) {
            this._pojemnoscSilnika = pojemnoscSilnika;
        }

        public double GetSrednieSpalanie() {
            return _srednieSpalanie;
        }

        public void SetSrednieSpalanie(double srednieSpalanie) {
            this._srednieSpalanie = srednieSpalanie;
        }
        public static int GetIloscSamochodow() {
            return _iloscSamochodow;
        }
    }
}