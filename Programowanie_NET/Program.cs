using System;
using System.IO;

namespace Programowanie_NET
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Ścieżka do pliku tekstowego
            string filePath = "ścieżka/do/pliku.txt";

            try
            {
                // Otwieranie pliku i wczytywanie linii
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Odwracanie linii
                        string reversedLine = ReverseString(line);
                        // Wyświetlanie odwróconej linii
                        Console.WriteLine(reversedLine);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Wystąpił błąd podczas wczytywania pliku:");
                Console.WriteLine(e.Message);
            }
        }

        static string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}