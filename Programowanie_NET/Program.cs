using System;

namespace Programowanie_NET
{
    public abstract class Program
    {
        public static void Main(string[] args)
        {
            ManagerZadan manager = new ManagerZadan();
            string sciezkaPliku = "zadania.xml";

            // Wczytaj zadania z pliku na starcie programu
            manager.WczytajZPliku(sciezkaPliku);
            try
            {
                while (true)
                {
                    Console.WriteLine("1. Dodaj zadanie");
                    Console.WriteLine("2. Usuń zadanie");
                    Console.WriteLine("3. Wyświetl zadania");
                    Console.WriteLine("4. Wyjście");
                    Console.Write("Wybierz opcję: ");
                    string opcja = Console.ReadLine();

                    switch (opcja)
                    {
                        case "1":
                            Console.Clear();
                            DodajZadanie(manager);
                            Console.Clear();
                            break;
                        case "2":
                            Console.Clear();
                            UsunZadanie(manager);
                            break;
                        case "3":
                            Console.Clear();
                            manager.WyswietlZadania();
                            break;
                        case "4":
                            return;
                        default:
                            Console.WriteLine("Nieznana opcja, spróbuj ponownie.");
                            break;
                    }
                }
            }
            finally
            {
                manager.ZapiszDoPliku(sciezkaPliku);
            }
        }
        private static void DodajZadanie(ManagerZadan manager)
        {
            try
            {
                Console.Write("Podaj Id: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Nieprawidłowe Id. Id powinno być liczbą całkowitą.");
                    return;
                }

                Console.Write("Podaj nazwę: ");
                string nazwa = Console.ReadLine();
                if (string.IsNullOrEmpty(nazwa))
                {
                    Console.WriteLine("Nazwa nie może być pusta.");
                    return;
                }

                Console.Write("Podaj opis: ");
                string opis = Console.ReadLine();

                Console.Write("Podaj datę zakończenia (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataZakonczenia))
                {
                    Console.WriteLine("Nieprawidłowy format daty. Poprawny format to yyyy-MM-dd.");
                    return;
                }

                Console.Write("Czy zadanie jest wykonane? (tak/nie): ");
                string czyWykonaneStr = Console.ReadLine().ToLower();
                bool czyWykonane = czyWykonaneStr == "tak";

                Zadanie zadanie = new Zadanie(id, nazwa, opis, dataZakonczenia, czyWykonane);
                manager.DodajZadanie(zadanie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas dodawania zadania: {ex.Message}");
            }
        }
        private static void UsunZadanie(ManagerZadan manager)
        {
            try
            {
                Console.Write("Podaj Id zadania do usunięcia: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Nieprawidłowe Id. Id powinno być liczbą całkowitą.");
                    return;
                }
                manager.UsunZadanie(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas usuwania zadania: {ex.Message}");
            }
        }
    }
}
