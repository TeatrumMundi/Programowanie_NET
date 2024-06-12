using System;

namespace Programowanie_NET
{
    public class Zadanie
    {
        // Właściwości klasy Zadanie
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public DateTime DataZakonczenia { get; set; }
        public bool CzyWykonane { get; set; }

        // Konstruktor domyślny
        public Zadanie()
        {
        }

        // Konstruktor z parametrami
        public Zadanie(int id, string nazwa, string opis, DateTime dataZakonczenia, bool czyWykonane)
        {
            Id = id;
            Nazwa = nazwa;
            Opis = opis;
            DataZakonczenia = dataZakonczenia;
            CzyWykonane = czyWykonane;
        }

        // Metoda do reprezentacji obiektu w postaci tekstowej
        public override string ToString()
        {
            return $"Id: {Id}, Nazwa: {Nazwa}, Opis: {Opis}, Data zakończenia: {DataZakonczenia}, Czy wykonane: {CzyWykonane}";
        }
    }
}