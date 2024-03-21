using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PasswordManagerInConsole
{
    internal class LoginPage
    {
        public static void LoginMain(string error = "none")
        {
            if (GlobalVariables.loggedin)
            {
                Console.Clear();
                Program.Navigator();
            }
            else
            {
                if (error != "none")
                {
                    Console.WriteLine(error);
                }

                const string fileName = "user.txt";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Please Sign Up");
                    SignUp(filePath);
                }
                else
                {
                    Console.WriteLine("Please Log In");
                    LogIn(filePath);
                }
            }
        }

        static void SignUp(string filePath)
        {
            Console.Write("Write your username: ");
            var usernameInput = Console.ReadLine();
            Console.Write("Write your password: ");
            var passwordInput = Console.ReadLine();
            Console.Clear();
            Console.Write("Confirm password: ");
            var passwordConfirmInput = Console.ReadLine();
            Console.Clear();

            if (usernameInput == null || usernameInput == "" || passwordInput == null || passwordInput == "" || passwordConfirmInput == null || passwordConfirmInput == "")
            {
                usernameInput = "";
                passwordInput = "";
                passwordConfirmInput = "0";
                LoginMain("One of your inputs were wrong!");
                return;
            }

            if (passwordInput != passwordConfirmInput)
            {
                LoginMain("Your passwords don't match!");
                return;
            }
            Console.WriteLine("Saving...");

            // Generate a salt
            string salt = Cryption.PasswordGenerator();
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            // Derive a key using SHA256
            byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordInput);
            byte[] derivedKeyBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                derivedKeyBytes = sha256.ComputeHash(passwordBytes.Concat(saltBytes).ToArray());
            }
            string derivedKey = Convert.ToBase64String(derivedKeyBytes);

            // Encrypt the username and password
            string usrEncrypted = Cryption.Encrypt(usernameInput, derivedKey);
            string pwdEncrypted = Cryption.Encrypt(passwordInput, derivedKey);

            string fileInfo = $"{usrEncrypted}\n{pwdEncrypted}\n{Convert.ToBase64String(saltBytes)}";

            File.WriteAllText(filePath, fileInfo);

            Console.WriteLine("User registered successfully!");
            Thread.Sleep(1000);
            LoginMain();
        }

        static void LogIn(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length < 3)
            {
                Console.WriteLine("Invalid user data file.");
                Environment.Exit(0);
            }

            string usrEncrypted = lines[0];
            string pwdEncrypted = lines[1];
            string saltBase64 = lines[2];


            string salt = saltBase64;
            byte[] saltBytes = Convert.FromBase64String(salt);

            Console.Write("Write your username: ");
            var usernameInput = Console.ReadLine();
            Console.Write("Write your password: ");
            var passwordInput = Console.ReadLine();

            if (usernameInput == null || passwordInput == null || passwordInput == "" || usernameInput == "")
            {
                Console.Clear();
                LoginMain("You forgot to write your name or your password!");
                return;
            }

            // Derive the key again to decrypt
            byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordInput);
            byte[] derivedKeyBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                derivedKeyBytes = sha256.ComputeHash(passwordBytes.Concat(saltBytes).ToArray());
            }
            string derivedKey = Convert.ToBase64String(derivedKeyBytes);

            // Attempt to decrypt the username and password
            string decryptedUsername = Cryption.Decrypt(usrEncrypted, derivedKey);
            string decryptedPassword = Cryption.Decrypt(pwdEncrypted, derivedKey);

            if (decryptedUsername != usernameInput || decryptedPassword != passwordInput)
            {
                Console.Clear();
                LoginMain("Your password or username is wrong!");
            }
            else
            {
                Console.Clear();
                GlobalVariables.loggedin = true;
                GlobalVariables.key = derivedKey; // Ensure GlobalVariables.key is compatible with the changes
                Program.Navigator();
            }
        }

    }
}
