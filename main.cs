using System.Text;

namespace FirstSolution.main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string seperator = "=====================================\n";

            Console.WriteLine("Password generation");
            Console.WriteLine(seperator);

            // start of the program

            Console.Write("Specify password length between (4 - 20): ");

            int PasswordLength = 0;
            while (PasswordLength < 4 || PasswordLength > 20)
            {
                try
                {
                    var x = int.TryParse(Console.ReadLine(), out PasswordLength);
                    if (PasswordLength < 4 || PasswordLength > 20)
                        throw new Exception("Invalid length, try again: ");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.WriteLine(seperator);

            // Password options
            Console.WriteLine("Paswoord Options\n");
            bool op1, op2, op3, op4;
            op1 = op2 = op3 = op4 = false;

            PasswordOption("[1] Include Capital letters? (yes/no): ", ref op1);
            PasswordOption("[2] Include Small letters? (yes/no): ", ref op2);
            PasswordOption("[3] Include Numbers? (yes/no): ", ref op3);
            PasswordOption("[4] Include Symbols? (yes/no): ", ref op4);

            if (!op1 && !op2 && !op3 && !op4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCant generate a password without any attribute, closing...");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            // Output Password
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine($"\nPassword: {GeneratePassword(PasswordLength, op1, op2, op3, op4)}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Input Password options
        public static void PasswordOption(string optionText, ref bool optionState)
        {
           Console.Write(optionText);
           string choosed = "";

           while (choosed != "yes" && choosed != "no")
           {
                try
                {
                    choosed = Console.ReadLine();
                    if (choosed != "yes" && choosed != "no")
                        throw new Exception("Invalid option, try again: ");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
           }

            optionState = (choosed == "yes");
        }

        // Password generation proccess
        private class CustomEnum
        {
            public const string CapitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            public const string SmallLetters = "abcdefghijklmnopqrstuvwxyz";
            public const string Numbers = "0123456789";
            public const string Symbols = "!@#$%^&*()_+-=[]{}|;:'\",.<>?/";
        }

        public static StringBuilder GeneratePassword(int PasswordLength, bool op1, bool op2, bool op3, bool op4)
        {
            string MainBuffer = "";
            var Password = new StringBuilder();

            if (op1) MainBuffer += CustomEnum.CapitalLetters;
            if (op2) MainBuffer += CustomEnum.SmallLetters;
            if (op3) MainBuffer += CustomEnum.Numbers;
            if (op4) MainBuffer += CustomEnum.Symbols;

            var rnd = new Random();
            int nxt = 0;

            for(int i = 0; i < PasswordLength; i++)
            {
                nxt = rnd.Next(0, MainBuffer.Length);
                Password.Append(MainBuffer[nxt]);
            }

            return Password;
        }
    }
}
