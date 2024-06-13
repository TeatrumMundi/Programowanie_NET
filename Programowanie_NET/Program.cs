using System;

namespace Programowanie_NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ManagerZadan manager = new ManagerZadan();

            // Dodawanie zadań
            Zadanie zadanie1 = new Zadanie(1, "Zadanie 1", "Opis zadania 1", DateTime.Now.AddDays(1), false);
            Zadanie zadanie2 = new Zadanie(2, "Zadanie 2", "Opis zadania 2", DateTime.Now.AddDays(2), true);
            manager.DodajZadanie(zadanie1);
            manager.DodajZadanie(zadanie2);

            // Wyświetlanie zadań
            manager.WyswietlZadania();
            
            manager.UsunZadanie(2);

            // Zapis listy zadań do pliku XML
            manager.ZapiszDoPliku("zadania.xml");

            // Czyszczenie listy zadań
            manager = new ManagerZadan();

            // Wczytanie listy zadań z pliku XML
            manager.WczytajZPliku("zadania.xml");

            // Wyświetlanie zadań po wczytaniu z pliku
            manager.WyswietlZadania();
        }
    }
}
