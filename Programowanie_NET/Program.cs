using System;
using System.IO;

namespace Programowanie_NET
{
    static class Program
    {
        static void Main()
        {
            // Ścieżki do plików
            string sourceFilePath = @"C:\Users\micha\OneDrive\Desktop\test.txt";
            string destinationFilePath = @"C:\Users\micha\OneDrive\Desktop\test1.txt";

            try
            {
                // Otwieranie pliku źródłowego do odczytu
                using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    // Sprawdzamy, czy plik źródłowy jest pusty
                    if (sourceStream.Length == 0)
                    {
                        Console.WriteLine("Plik źródłowy jest pusty.");
                        return;
                    }

                    // Otwieranie pliku docelowego do zapisu
                    using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                    {
                        // Bufor do przechowywania danych
                        byte[] buffer = new byte[4096];
                        int bytesRead;

                        // Odczytywanie danych z pliku źródłowego i zapisywanie do pliku docelowego
                        while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            Console.WriteLine($"Odczytano {bytesRead} bajtów");
                            destinationStream.Write(buffer, 0, bytesRead);
                            Console.WriteLine("Zapisano dane do pliku docelowego");
                        }
                    }

                    // Sprawdzenie, czy plik docelowy został zapisany
                    if (File.Exists(destinationFilePath))
                    {
                        Console.WriteLine("Plik został zapisany pomyślnie.");
                    }
                    else
                    {
                        Console.WriteLine("Plik nie został zapisany.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Wystąpił błąd podczas kopiowania pliku:");
                Console.WriteLine(e.Message);
            }
        }
    }
}