using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace LAB4_ZADANIE_ZALICZENIOWE;

static class Program
{
    static void Main()
    {
        string directory = @"C:\Windows\System32";
        string checksumFilePath = "checksums.txt";
        
        Task.Run(async () =>
        {
            while (true)
            {
                ProcessDirectory(directory, checksumFilePath);
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }).Wait();
    }

    static void ProcessDirectory(string directoryPath, string checksumFilePath)
    {
        if (Directory.Exists(directoryPath))
        {
            var newChecksums = new StringBuilder();

            try
            {
                var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    try
                    {
                        string checksum = ComputeChecksum(file);
                        newChecksums.AppendLine($"{file}|{checksum}");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Access to the file {file} is denied.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file {file}: {ex.Message}");
                    }
                }

                CompareAndReportDiscrepancies(newChecksums.ToString(), checksumFilePath);
                File.WriteAllText(checksumFilePath, newChecksums.ToString());
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Access to the directory {directoryPath} is denied.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing directory {directoryPath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Directory {directoryPath} does not exist.");
        }
    }

    static string ComputeChecksum(string filePath)
    {
        using (var stream = File.OpenRead(filePath))
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }

    static void CompareAndReportDiscrepancies(string newChecksums, string checksumFilePath)
    {
        if (File.Exists(checksumFilePath))
        {
            var oldChecksums = File.ReadAllLines(checksumFilePath);
            var oldChecksumDict = new Dictionary<string, string>();

            foreach (var line in oldChecksums)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    oldChecksumDict[parts[0]] = parts[1];
                }
            }

            var newChecksumLines = newChecksums.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in newChecksumLines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    var filePath = parts[0];
                    var newChecksum = parts[1];

                    if (oldChecksumDict.TryGetValue(filePath, out var oldChecksum))
                    {
                        if (newChecksum != oldChecksum)
                        {
                            LogDiscrepancy(filePath, oldChecksum, newChecksum);
                        }
                    }
                    else
                    {
                        LogDiscrepancy(filePath, "new file", newChecksum);
                    }
                }
            }
        }
    }

    static void LogDiscrepancy(string filePath, string oldChecksum, string newChecksum)
    {
        string logMessage = $"Discrepancy detected in file {filePath}. Old checksum: {oldChecksum}, New checksum: {newChecksum}";
        Console.WriteLine(logMessage);
        EventLog.WriteEntry("Application", logMessage, EventLogEntryType.Warning);
    }
}