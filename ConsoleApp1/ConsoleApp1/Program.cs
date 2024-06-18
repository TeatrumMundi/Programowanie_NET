using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1;

abstract class Program
{
    [Obsolete("Obsolete")]
    static void Main()
    {
        // Define data to be encrypted
        string data = "This is a sample text to be encrypted.";
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        
        // Initialize results table
        string[,] results = new string[4, 9];
        results[0, 0] = "AES (CSP) 128 bit";
        results[0, 1] = "AES (CSP) 256 bit";
        results[0, 2] = "AES Managed 128 bit";
        results[0, 3] = "AES Managed 256 bit";
        results[0, 4] = "Rijndael Managed 128 bit";
        results[0, 5] = "Rijndael Managed 256 bit";
        results[0, 6] = "DES 56 bit";
        results[0, 7] = "3DES 168 bit";

        // Perform encryption and measure time
        MeasureEncryptionTime(dataBytes, results, 1, new AesCryptoServiceProvider { KeySize = 128 });
        MeasureEncryptionTime(dataBytes, results, 2, new AesCryptoServiceProvider { KeySize = 256 });
        MeasureEncryptionTime(dataBytes, results, 3, new AesManaged { KeySize = 128 });
        MeasureEncryptionTime(dataBytes, results, 4, new AesManaged { KeySize = 256 });
        MeasureEncryptionTime(dataBytes, results, 5, new RijndaelManaged { KeySize = 128 });
        MeasureEncryptionTime(dataBytes, results, 6, new RijndaelManaged { KeySize = 256 });
        MeasureEncryptionTime(dataBytes, results, 7, new DESCryptoServiceProvider());
        MeasureEncryptionTime(dataBytes, results, 8, new TripleDESCryptoServiceProvider());

        // Output results
        for (int i = 0; i < results.GetLength(0); i++)
        {
            for (int j = 0; j < results.GetLength(1); j++)
            {
                Console.Write(results[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static void MeasureEncryptionTime(byte[] data, string[,] results, int columnIndex, SymmetricAlgorithm algorithm)
    {
        algorithm.GenerateKey();
        algorithm.GenerateIV();

        using (var encryptor = algorithm.CreateEncryptor())
        {
            var stopwatch = Stopwatch.StartNew();
            byte[] encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);
            stopwatch.Stop();
            results[1, columnIndex - 1] = stopwatch.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            results[2, columnIndex - 1] = (encryptedData.Length / stopwatch.Elapsed.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }

        algorithm.Dispose();
    }
}