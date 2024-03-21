namespace PasswordManagerInConsole
{
    internal class AddPassword
    {
        public static void AddPwdMain()
        {
            if (!GlobalVariables.loggedin)
            {
                Console.Clear();
                Program.Navigator();
            }
            else
            {

                Console.WriteLine("""
                                
                 █████  ██████  ██████      ██████   █████  ███████ ███████ ██     ██  ██████  ██████  ██████  
                ██   ██ ██   ██ ██   ██     ██   ██ ██   ██ ██      ██      ██     ██ ██    ██ ██   ██ ██   ██ 
                ███████ ██   ██ ██   ██     ██████  ███████ ███████ ███████ ██  █  ██ ██    ██ ██████  ██   ██ 
                ██   ██ ██   ██ ██   ██     ██      ██   ██      ██      ██ ██ ███ ██ ██    ██ ██   ██ ██   ██ 
                ██   ██ ██████  ██████      ██      ██   ██ ███████ ███████  ███ ███   ██████  ██   ██ ██████  

                """);
                Console.WriteLine("Select one of the options: ");
                Console.WriteLine("1. Create new password\n2. Cancel and go back");
                Console.Write("You select: ");
                var input = Console.ReadLine();
                string userInput;
                if (input == null)
                {
                    userInput = "";
                }
                else
                {
                    userInput = input.ToLower();
                }

                if (userInput == "1" || userInput == "one" || userInput == "create" || userInput == "new")
                {
                    Console.Clear();
                    CreateNewPwd();
                }
                else if (userInput == "2" || userInput == "two" || userInput == "cancel" || userInput == "exit")
                {
                    Console.Clear();
                    Program.Navigator();
                }
                else
                {
                    Console.Clear();
                    AddPwdMain();
                }
            }
        }

        public static void CreateNewPwd(bool cancelMaybe = false)
        {
            if (cancelMaybe)
            {
                Console.WriteLine("If you want to cancel, write \"cancel\". If not, write \"continue\"...");
                var userAnswer = Console.ReadLine();
                if (userAnswer == "cancel")
                {
                    Console.Clear();
                    Program.Navigator();
                }
            }

            Console.Clear();
            Console.Write("\nWrite the website or app name where the password will be used: ");
            string? locationInput = Console.ReadLine();
            string location;
            if (locationInput == null || locationInput == "")
            {
                location = "";
                Console.Clear();
                CreateNewPwd(true);
            }
            else
            {
                location = locationInput;
            }

            Console.Write("Write your username: ");
            string? userNameInput = Console.ReadLine();
            string username;
            if (userNameInput == null)
            {
                username = "";
            }
            else
            {
                username = userNameInput;
            }

            Console.Write("\nSelect one of the options:\n1. Generate password.\n2. Write your own password.\nYour choice: ");
            string? choiceInput = Console.ReadLine();
            string choice;
            if (choiceInput == null || choiceInput == "")
            {
                choice = "1";
            }
            else
            {
                choice = choiceInput;
            }


            string password;
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Generating password...");
                    password = Cryption.PasswordGenerator();
                    break;

                case "2":
                    Console.Write("Write your password: ");
                    string? pwdInput = Console.ReadLine();
                    if (pwdInput == null || pwdInput == "")
                    {
                        password = Cryption.PasswordGenerator();
                    }
                    else
                    {
                        password = pwdInput;
                    }
                    break;

                default:
                    Console.WriteLine("Generating password...");
                    password = Cryption.PasswordGenerator();
                    break;
            }

            Console.WriteLine($"This is the information inputted:\nLocation: {location}\nUsername: {username}\nPassword: {password}");
            Console.WriteLine("Select one of the following options: ");
            Console.WriteLine("1. Save\n2. Restart\n3. Cancel");
            Console.Write("Your choice: ");

            string? finalAnswer = Console.ReadLine();

            switch (finalAnswer)
            {
                case "1":
                    Console.Clear();
                    SavePassword(location, username, password);
                    break;

                case "2":
                    Console.Clear();
                    CreateNewPwd();
                    break;

                case "3":
                    Console.Clear();
                    Program.Navigator();
                    break;

                default:
                    Console.Clear();
                    SavePassword(location, username, password);
                    break;
            }
        }

        private static void SavePassword(string location, string username, string password)
        {
            string baseLocation = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "Passwords";
            string folderPath = Path.Combine(baseLocation, folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"Folder created at: {folderPath}");
            }

            string fileName = $"{location}.txt";
            string filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                Console.WriteLine("This location has already been used before, and there will therefore be added a part of your username as an extension...");
                Console.Write("Click the \"Enter\" key to continue...");
                Console.ReadLine();

                string usernamePart = username.Length > 3 ? username.Substring(0, 3) : username;
                string newFileName = $"{location}_{usernamePart}.txt";
                filePath = Path.Combine(folderPath, newFileName);
            }


            string encryptedData = Cryption.Encrypt($"{location}\n{username}\n{password}", GlobalVariables.key);
            File.WriteAllText(filePath, encryptedData);
            Console.WriteLine($"Password saved successfully at: {filePath}");
            Console.Write("Press \"Enter\" to return to navigation...");
            Console.ReadLine();
            Console.Clear();
            Program.Navigator();
        }

    }
}
