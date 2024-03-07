namespace PasswordManagerInConsole
{
    internal class PasswordList
    {
        public static void PwdListMain()
        {
            const string fileName = "pwdList.json";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            bool fileExist = false;

            if (File.Exists(filePath))
            {
                fileExist = true;
            }

            listCheck(fileExist);
        }

        static void listCheck(bool fileExistence)
        {
            if (GlobalVariables.loggedin != true)
            {
                Console.Clear();
                Console.Write("""
                          ___  _ ____  _       ____  ____  _____    
                          \  \///  _ \/ \ /\  /  _ \/  __\/  __/    
                           \  / | / \|| | ||  | / \||  \/||  \      
                           / /  | \_/|| \_/|  | |-|||    /|  /_     
                          /_/   \____/\____/  \_/ \|\_/\_\\____\    

                                   _      ____  _____               
                                  / \  /|/  _ \/__ __\              
                                  | |\ ||| / \|  / \                
                                  | | \||| \_/|  | |                
                                  \_/  \|\____/  \_/                

                     _     ____  _____ _____ _____ ____    _  _     
                    / \   /  _ \/  __//  __//  __//  _ \  / \/ \  /|
                    | |   | / \|| |  _| |  _|  \  | | \|  | || |\ ||
                    | |_/\| \_/|| |_//| |_//|  /_ | |_/|  | || | \||
                    \____/\____/\____\\____\\____\\____/  \_/\_/  \|
                    """);
                Thread.Sleep(1000);
                Console.Clear();
                Program.Navigator();
            }
            else
            {
                Display(fileExistence);
            }
        }

        static void Display(bool fileExists)
        {
            
        }
    }
}
