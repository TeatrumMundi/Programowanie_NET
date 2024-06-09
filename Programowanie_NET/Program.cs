using System;
using System.Diagnostics;
using System.IO;

namespace Programowanie_NET
{
    static class Program
    {
        static void Main()
        {
            string sourceFilePath = @"C:\Users\micha\OneDrive\Desktop\test.txt";
            string destinationFilePath = @"C:\Users\micha\OneDrive\Desktop\test1.txt";

            // Generowanie pliku o wielkości 300 MB
            GenerateLargeFile(sourceFilePath, 300 * 1024 * 1024);

            // Testowanie kopiowania przy użyciu FileStream
            TimeAction("FileStream copy", () =>
            {
                CopyFileUsingFileStream(sourceFilePath, destinationFilePath);
            });

            // Testowanie kopiowania przy użyciu File.Copy
            TimeAction("File.Copy", () =>
            {
                File.Copy(sourceFilePath, destinationFilePath, true);
            });

            // Testowanie kopiowania przy użyciu BufferedStream
            TimeAction("BufferedStream copy", () =>
            {
                CopyFileUsingBufferedStream(sourceFilePath, destinationFilePath);
            });
        }

        static void GenerateLargeFile(string filePath, long size)
        {
            byte[] data = new byte[1024 * 1024]; // 1 MB buffer
            Random rng = new Random();
            rng.NextBytes(data);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                for (long i = 0; i < size; i += data.Length)
                {
                    fs.Write(data, 0, data.Length);
                }
            }

            Console.WriteLine("Plik o wielkości 300 MB został wygenerowany.");
        }

        static void TimeAction(string description, Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            Console.WriteLine($"{description}: {stopwatch.Elapsed.TotalSeconds} sekund");
        }

        static void CopyFileUsingFileStream(string sourceFilePath, string destinationFilePath)
        {
            using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destinationStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void CopyFileUsingBufferedStream(string sourceFilePath, string destinationFilePath)
        {
            using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            {
                using (BufferedStream bufferedSourceStream = new BufferedStream(sourceStream))
                {
                    using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (BufferedStream bufferedDestinationStream = new BufferedStream(destinationStream))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            while ((bytesRead = bufferedSourceStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bufferedDestinationStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }
    }
}
