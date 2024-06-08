using System;
using System.IO;

internal static class Program
{
    static void Main()
    {
        string filePath = "test.txt";

        // Sprawdzenie, czy plik istnieje
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Plik nie istnieje.");
            return;
        }

        // Użycie FileStream do otwarcia pliku
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            // Użycie StreamReader do odczytu zawartości pliku
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string content;

                // Odczytywanie zawartości pliku linia po linii
                while ((content = reader.ReadLine()) != null)
                {
                    // Wyświetlanie każdej linii na konsoli
                    Console.WriteLine(content);
                }
            }
        }
    }
}