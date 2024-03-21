namespace PasswordManagerInConsole
{
    internal class ShowPassword
    {
        public static void DisplayPassword(int pageNum, int pwdNum, List<string> passwordList)
        {
            int index = ((pageNum - 1) * 10) + pwdNum - 1;
            if (index >= 0 && index < passwordList.Count)
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Passwords", passwordList[index]);

                string encryptedData = File.ReadAllText(filePath);

                string decryptedData = Cryption.Decrypt(encryptedData, GlobalVariables.key);

                string[] parts = decryptedData.Split('\n');
                if (parts.Length >= 3)
                {
                    Console.WriteLine($"Location: {parts[0]}");
                    Console.WriteLine($"Username: {parts[1]}");
                    Console.WriteLine($"Password: {parts[2]}");
                }
                else
                {
                    Console.WriteLine("Error: Password data format is incorrect.");
                }
            }
            else
            {
                Console.WriteLine("I don't know how you got here with a wrong index, but... SOMETHING WENT WRONG!");
            }
            Console.WriteLine("Click any key to return to the password list...");
            Console.ReadKey();
            Console.Clear();
            PasswordList.PwdListMain();
        }
    }
}
