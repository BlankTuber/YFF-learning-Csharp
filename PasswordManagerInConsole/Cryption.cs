using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerInConsole
{
    internal class Cryption
    {
        public static string PasswordGenerator()
        {
            string generatedPwd = "";
            string[] possibles =
            [
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
                "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
                "u", "v", "w", "x", "y", "z", "-", "!", "_", ".",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                "U", "V", "W", "X", "Y", "Z"
            ];

            Random randNum = new Random();

            for (int i = 0; i < 11; i++)
            {
                generatedPwd += possibles[randNum.Next(0, possibles.Length)];
            }

            return generatedPwd;
        }

        public static string Encrypt(string plainText, string keyString)
        {
            byte[] iv = new byte[16]; // AES block size in bytes, initialized to zeros for simplicity
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                // Use SHA256 to ensure the key size is 256 bits, which is valid for AES
                using (SHA256 sha256 = SHA256.Create())
                {
                    aes.Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyString));
                }

                aes.IV = iv; // Note: Ideally, the IV should be unique for each encryption and stored with the encrypted data

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }


        public static string Decrypt(string cipherText, string keyString)
        {
            byte[] iv = new byte[16]; // Ensure this matches how the IV was handled during encryption
            byte[] buffer = Convert.FromBase64String(cipherText);

            try
            {
                using (Aes aes = Aes.Create())
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        aes.Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyString)); // Derive key
                    }
                    aes.IV = iv;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (CryptographicException)
            {
                // Handle the scenario where decryption fails due to wrong key (possibly wrong password)
                return "Decryption failed. The provided password may be incorrect.";
            }
        }
    }
}
