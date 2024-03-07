using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PasswordManagerInConsole
{
    internal class LoginPage
    {
        public static void LoginMain(string error = "none")
        {
            if (error != "none")
            {
                Console.WriteLine(error);
            }

            const string fileName = "user.json";
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
            }


            if (passwordInput != passwordConfirmInput)
            {
                LoginMain("Your passwords don't match!");
            }
            Console.WriteLine("Saving...");

            

            string salt = Cryption.PasswordGenerator();
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            int iterations = 10000;
            int keySize = 128;

            string derivedKey = "";

            using (var pdkSomething = new Rfc2898DeriveBytes(passwordInput, saltBytes, iterations, HashAlgorithmName.SHA256))
            {
                byte[] derivedBytes = pdkSomething.GetBytes(keySize / 8);
                derivedKey = Convert.ToBase64String(derivedBytes);
            }

            string usrEncrypted = Cryption.Encrypt(usernameInput, derivedKey);
            string pwdEncrypted = Cryption.Encrypt(passwordInput, derivedKey);

            var userCredentials = new Dictionary<string, string>
            {
                {"username", usrEncrypted},
                {"password", pwdEncrypted},
                {"salt", Convert.ToBase64String(saltBytes)}
            };

            string json = JsonSerializer.Serialize(userCredentials);
            File.WriteAllText(filePath, json);

            Console.WriteLine("User registered successfully!");
            Thread.Sleep(1000);
            LoginMain();
        }

        static void LogIn(string filePath)
        {

            var encryptedData = File.ReadAllText(filePath);
            var userData = JsonSerializer.Deserialize<Dictionary<string, string>>(encryptedData);

            if (userData == null)
            {
                Environment.Exit(0);
            }


            string salt = userData["salt"];
            byte[] saltBytes = Convert.FromBase64String(salt);
            int iterations = 10000; // Must match the iteration count used in SignUp
            int keySize = 128; // Must match the key size used in SignUp

            
            Console.Write("Write your username: ");
            var usernameInput = Console.ReadLine();
            Console.Write("Write your password: ");
            var passwordInput = Console.ReadLine();

            if (usernameInput == null || passwordInput == null || passwordInput == "" || usernameInput == "")
            {
                Console.Clear();
                LoginMain("You forgot to write your name or your password!\n");
            }
            else
            {
                string derivedKey = "";
                using (var pdkSomething = new Rfc2898DeriveBytes(passwordInput, saltBytes, iterations, HashAlgorithmName.SHA256))
                {
                    byte[] derivedBytes = pdkSomething.GetBytes(keySize / 8);
                    derivedKey = Convert.ToBase64String(derivedBytes);
                }

                string decryptedUsername = Cryption.Decrypt(userData["username"], derivedKey);
                string decryptedPassword = Cryption.Decrypt(userData["password"], derivedKey);


                if (decryptedUsername != usernameInput || decryptedPassword != passwordInput)
                {
                    Console.Clear();
                    LoginMain("Your password or username is wrong!\n");
                }
                else
                {
                    Console.Clear();
                    GlobalVariables.loggedin = true;
                    Program.Navigator();
                }
            }
        }
    }
}
