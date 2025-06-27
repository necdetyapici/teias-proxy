using System.Security.Cryptography;
using System.Text;

namespace TeiasProxy.Helpers
{
    public static class EncryptionHelper
    {
        // 32 byte = 256 bit key
        private static readonly string EncryptionKey = "4B9f6eA1cD3xZpQ7LmNo8YwRtUj2HvK0";

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length); // IV başa yazılır

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
                writer.Write(plainText);

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);

            var iv = fullCipher.Take(16).ToArray();
            var cipher = fullCipher.Skip(16).ToArray();

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);

            return reader.ReadToEnd();
        }
    }
}
