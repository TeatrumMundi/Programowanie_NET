using System;
using System.IO;
using System.Security.Cryptography;

namespace Programowanie_NET
{
    static class RsaCrypto
    {
        public static void Main()
        {
            string publicKey;
            string privateKey;

            // Generate RSA keys
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                publicKey = rsa.ToXmlString(false); // tylko klucz publiczny
                privateKey = rsa.ToXmlString(true); // klucz publiczny + prywatny
            }

            string filePath = "file.txt";
            string encryptedFilePath = "encryptedFile.txt";
            string decryptedFilePath = "decryptedFile.txt";

            // Szyfrowanie pliku
            EncryptFile(filePath, encryptedFilePath, publicKey);

            // Deszyfrowanie pliku
            DecryptFile(encryptedFilePath, decryptedFilePath, privateKey);

            Console.WriteLine("Plik został zaszyfrowany i odszyfrowany.");
        }

        private static void EncryptFile(string inputFilePath, string outputFilePath, string publicKey)
        {
            byte[] dataToEncrypt = File.ReadAllBytes(inputFilePath);
            byte[] encryptedData;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }

            File.WriteAllBytes(outputFilePath, encryptedData);
        }

        private static void DecryptFile(string inputFilePath, string outputFilePath, string privateKey)
        {
            byte[] dataToDecrypt = File.ReadAllBytes(inputFilePath);
            byte[] decryptedData;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                decryptedData = rsa.Decrypt(dataToDecrypt, false);
            }

            File.WriteAllBytes(outputFilePath, decryptedData);
        }
    }
}