using System;
using System.IO;

namespace Programowanie_NET
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Zapisz dane do pliku binarnego");
            Console.WriteLine("2. Odczytaj dane z pliku binarnego");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SaveData();
                    break;
                case "2":
                    LoadData();
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór");
                    break;
            }
        }

        static void SaveData()
        {
            Person person = new Person();

            Console.Write("Podaj imię: ");
            person.Name = Console.ReadLine();

            Console.Write("Podaj wiek: ");
            person.Age = int.Parse(Console.ReadLine());

            Console.Write("Podaj adres: ");
            person.Address = Console.ReadLine();

            using (FileStream fs = new FileStream("person.dat", FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(person.Name);
                    writer.Write(person.Age);
                    writer.Write(person.Address);
                }
            }

            Console.WriteLine("Dane zostały zapisane do pliku.");
        }

        static void LoadData()
        {
            if (!File.Exists("person.dat"))
            {
                Console.WriteLine("Plik nie istnieje.");
                return;
            }

            Person person = new Person();

            using (FileStream fs = new FileStream("person.dat", FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    person.Name = reader.ReadString();
                    person.Age = reader.ReadInt32();
                    person.Address = reader.ReadString();
                }
            }

            Console.WriteLine("Odczytane dane:");
            Console.WriteLine($"Imię: {person.Name}");
            Console.WriteLine($"Wiek: {person.Age}");
            Console.WriteLine($"Adres: {person.Address}");
        }
    }
}