namespace PasswordManagerInConsole
{
    internal class PasswordList
    {
        private static List<string> passwordFiles = new List<string>();
        private static int pageNumber = 1;  // Start from 1 for user clarity
        private const int pageSize = 10;

        private static int pageMax;

        public static void PwdListMain()
        {
            if (!GlobalVariables.loggedin)
            {
                Console.Clear();
                Program.Navigator();
            }
            else
            {
                Console.Clear();
                if (LoadPasswordList())
                {
                    DisplayCurrentPage();
                }
                else
                {
                    Console.Clear();
                    AddPassword.AddPwdMain();
                }
            }
        }

        private static bool LoadPasswordList()
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Passwords");

            if (Directory.Exists(folderPath))
            {
                passwordFiles = Directory.GetFiles(folderPath, "*.txt").ToList();
                // Calculate the max number of pages needed
                pageMax = (int)Math.Ceiling((double)passwordFiles.Count / pageSize);
                return true;
            }
            else
            {
                Console.WriteLine("No passwords were found, press any key to add a new password...");
                Console.ReadKey();
                return false;
            }
        }

        private static void DisplayCurrentPage()
        {
            Console.WriteLine("""
                
                 ██▓███   ▄▄▄        ██████   ██████  █     █░ ▒█████   ██▀███  ▓█████▄     ██▓     ██▓  ██████ ▄▄▄█████▓
                ▓██░  ██▒▒████▄    ▒██    ▒ ▒██    ▒ ▓█░ █ ░█░▒██▒  ██▒▓██ ▒ ██▒▒██▀ ██▌   ▓██▒    ▓██▒▒██    ▒ ▓  ██▒ ▓▒
                ▓██░ ██▓▒▒██  ▀█▄  ░ ▓██▄   ░ ▓██▄   ▒█░ █ ░█ ▒██░  ██▒▓██ ░▄█ ▒░██   █▌   ▒██░    ▒██▒░ ▓██▄   ▒ ▓██░ ▒░
                ▒██▄█▓▒ ▒░██▄▄▄▄██   ▒   ██▒  ▒   ██▒░█░ █ ░█ ▒██   ██░▒██▀▀█▄  ░▓█▄   ▌   ▒██░    ░██░  ▒   ██▒░ ▓██▓ ░ 
                ▒██▒ ░  ░ ▓█   ▓██▒▒██████▒▒▒██████▒▒░░██▒██▓ ░ ████▓▒░░██▓ ▒██▒░▒████▓    ░██████▒░██░▒██████▒▒  ▒██▒ ░ 
                ▒▓▒░ ░  ░ ▒▒   ▓▒█░▒ ▒▓▒ ▒ ░▒ ▒▓▒ ▒ ░░ ▓░▒ ▒  ░ ▒░▒░▒░ ░ ▒▓ ░▒▓░ ▒▒▓  ▒    ░ ▒░▓  ░░▓  ▒ ▒▓▒ ▒ ░  ▒ ░░   
                ░▒ ░       ▒   ▒▒ ░░ ░▒  ░ ░░ ░▒  ░ ░  ▒ ░ ░    ░ ▒ ▒░   ░▒ ░ ▒░ ░ ▒  ▒    ░ ░ ▒  ░ ▒ ░░ ░▒  ░ ░    ░    
                ░░         ░   ▒   ░  ░  ░  ░  ░  ░    ░   ░  ░ ░ ░ ▒    ░░   ░  ░ ░  ░      ░ ░    ▒ ░░  ░  ░    ░      
                               ░  ░      ░        ░      ░        ░ ░     ░        ░           ░  ░ ░        ░           

                """);

            int startingIndex = (pageNumber - 1) * pageSize; // Correct starting index based on page number

            Console.WriteLine($"Page {pageNumber}/{pageMax}"); // Show current page and total pages
            for (int i = startingIndex; i < passwordFiles.Count && i < startingIndex + pageSize; i++)
            {
                Console.WriteLine($"{i + 1 - startingIndex}. {Path.GetFileNameWithoutExtension(passwordFiles[i])}");
            }
            HandlePaging();
        }

        private static void HandlePaging()
        {
            Console.WriteLine($"\nOptions:\nNumbers 1 through {Math.Min(passwordFiles.Count - (pageNumber - 1) * pageSize, pageSize)} to display a password.\n(N)ext page, (P)revious page, (E)xit");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine()?.ToLower() ?? "";
            int choiceInNumber;
            try {
                choiceInNumber = Int32.Parse(choice);
            }
            catch {
                choiceInNumber = -1;
            }

            if (choiceInNumber > 0 && choiceInNumber <= Math.Min(passwordFiles.Count - (pageNumber - 1) * pageSize, pageSize))
            {
                Console.Clear();
                ShowPassword.DisplayPassword(pageNumber, choiceInNumber, passwordFiles);
            }
            else
            {

                switch (choice)
                {
                    case "n":
                        if (pageNumber < pageMax)
                        {
                            pageNumber++;
                        }
                        break;

                    case "p":
                        if (pageNumber > 1)
                        {
                            pageNumber--;
                        }
                        break;

                    case "e":
                        Console.Clear();
                        Program.Navigator();
                        break;

                    default:
                        Console.Write("Invalid input. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
                DisplayCurrentPage();
            }
        }
    }
}