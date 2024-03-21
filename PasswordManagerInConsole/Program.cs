namespace PasswordManagerInConsole
{
    public static class GlobalVariables
    {
        public static bool loggedin = false;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Navigator();
        }

        public static void Navigator()
        {
            Console.WriteLine("""
                 _   _             _             _   _             
                | \ | |           (_)           | | (_)            
                |  \| | __ ___   ___  __ _  __ _| |_ _  ___  _ __  
                | . ` |/ _` \ \ / / |/ _` |/ _` | __| |/ _ \| '_ \ 
                | |\  | (_| |\ V /| | (_| | (_| | |_| | (_) | | | |
                \_| \_/\__,_| \_/ |_|\__, |\__,_|\__|_|\___/|_| |_|
                                      __/ |                        
                                     |___/                         
                """);
            Console.WriteLine("""
                 ______ ______ ______ ______ ______ ______ ______  
                |______|______|______|______|______|______|______|
                """);
            Console.Write("Status: ");
            if (GlobalVariables.loggedin == true)
            {
                Console.Write("logged in.\n");
            }
            else
            {
                Console.Write("not logged in.\n");
            }

            Console.Write("1: Login\n2: Password List (Needs Login)\n3. Add Password (Needs Login)\n4: Exit/Close\nYou choose number...: ");
            var input = Console.ReadLine();
            string userInput;
            if (input == null)
            {
                userInput = "";
            }
            else
            {
                userInput = input;
            }

            switch (userInput.ToLower())
            {
                case "1":
                    Console.Clear();
                    LoginPage.LoginMain();
                    break;

                case "login":
                    Console.Clear();
                    LoginPage.LoginMain();
                    break;

                case "2":
                    Console.Clear();
                    PasswordList.PwdListMain();
                    break;

                case "list":
                    Console.Clear();
                    PasswordList.PwdListMain();
                    break;

                case "passwordlist":
                    Console.Clear();
                    PasswordList.PwdListMain();
                    break;

                case "3":
                    Console.Clear();
                    AddPassword.AddPwdMain();
                    break;

                case "add":
                    Console.Clear();
                    AddPassword.AddPwdMain();
                    break;

                case "addpassword":
                    Console.Clear();
                    AddPassword.AddPwdMain();
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                case "exit":
                    Environment.Exit(0);
                    break;


                default:
                    Console.Clear();
                    Navigator();
                    break;
            }
        }
    }
}
