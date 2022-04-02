using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TG.Core.Security
{
    public class Cryptography
    {
        private const string key = "90908080";

        public string Encrypt(string plainText)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var pass = Encoding.Unicode.GetBytes(key);
            var rijndaelCipher = new RijndaelManaged();
            var memoryStream = new MemoryStream();
            var rijndaelEncryptor = rijndaelCipher.CreateEncryptor(pass, pass);
            var cryptoStream = new CryptoStream(memoryStream, rijndaelEncryptor, CryptoStreamMode.Write);
            var plainBytes = Encoding.ASCII.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            var cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
            return cipherText;
        }

        public string Decrypt(string cipherText)
        {
            var pass = Encoding.Unicode.GetBytes(key);
            var rijndaelCipher = new RijndaelManaged();
            var memoryStream = new MemoryStream();
            var rijndaelDecryptor = rijndaelCipher.CreateDecryptor(pass, pass);
            var cryptoStream = new CryptoStream(memoryStream, rijndaelDecryptor, CryptoStreamMode.Write);
            var plainText = string.Empty;
            try
            {
                var cipherBytes = Convert.FromBase64String(cipherText);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.FlushFinalBlock();
                var plainBytes = memoryStream.ToArray();
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }

            return plainText;
        }
    }
}
