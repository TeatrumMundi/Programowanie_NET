using System;

namespace Programowanie_NET
{
    public class Zadanie
    {
        // Id zadania
        public int Id { get; set; }

        // Nazwa zadania
        public string Nazwa { get; set; }

        // Opis zadania
        public string Opis { get; set; }

        // Data zakończenia zadania
        public DateTime DataZakonczenia { get; set; }

        // Czy zadanie jest wykonane
        public bool CzyWykonane { get; set; }

        // Konstruktor bezparametrowy
        public Zadanie()
        {
        }

        // Konstruktor parametrowy
        public Zadanie(int id, string nazwa, string opis, DateTime dataZakonczenia, bool czyWykonane)
        {
            Id = id;
            Nazwa = nazwa;
            Opis = opis;
            DataZakonczenia = dataZakonczenia;
            CzyWykonane = czyWykonane;
        }

        // Metoda do wyświetlania informacji o zadaniu
        public void WyswietlInformacje()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Nazwa: {Nazwa}");
            Console.WriteLine($"Opis: {Opis}");
            Console.WriteLine($"Data zakończenia: {DataZakonczenia.ToString("dd-MM-yyyy")}");
            Console.WriteLine($"Czy wykonane: {(CzyWykonane ? "Tak" : "Nie")}");
        }
    }
}