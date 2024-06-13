using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Programowanie_NET
{
    public class ManagerZadan
    {
        private List<Zadanie> _listaZadan = new List<Zadanie>();
        
        // Metoda do dodawania zadania
        public void DodajZadanie(Zadanie zadanie)
        {
            _listaZadan.Add(zadanie);
            Console.WriteLine("Zadanie zostało dodane.");
        }

        // Metoda do usuwania zadania po Id
        public void UsunZadanie(int id)
        {
            Zadanie zadanieDoUsuniecia = _listaZadan.Find(z => z.Id == id);
            if (zadanieDoUsuniecia != null)
            {
                _listaZadan.Remove(zadanieDoUsuniecia);
                Console.WriteLine("Zadanie zostało usunięte.");
            }
            else
            {
                Console.WriteLine("Zadanie o podanym Id nie istnieje.");
            }
        }

        // Metoda do wyświetlania wszystkich zadań
        public void WyswietlZadania()
        {
            if (_listaZadan.Count == 0)
            {
                Console.WriteLine("Lista zadań jest pusta.\n");
            }
            else
            {
                Console.WriteLine("Lista zadań:");
                foreach (Zadanie zadanie in _listaZadan)
                {
                    zadanie.WyswietlInformacje();
                    Console.WriteLine();
                }
            }
        }
        public void ZapiszDoPliku(string sciezka)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Zadanie>));
                using (StreamWriter writer = new StreamWriter(sciezka))
                {
                    serializer.Serialize(writer, _listaZadan);
                }
                Console.WriteLine("Lista zadań została zapisana do pliku.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisywania pliku: {ex.Message}");
            }
        }
        public void WczytajZPliku(string sciezka)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Zadanie>));
                using (StreamReader reader = new StreamReader(sciezka))
                {
                    _listaZadan = (List<Zadanie>)serializer.Deserialize(reader);
                }
                Console.WriteLine("Lista zadań została wczytana z pliku.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Plik nie został znaleziony. Rozpoczęto z pustą listą zadań.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas wczytywania pliku: {ex.Message}");
            }
        }
    }
}