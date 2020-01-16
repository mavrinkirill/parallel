using System;
using Examples;

namespace parallel
{
    class Program
    {
        private static readonly IExample Example = new Examples.Examples.WithAwait.Generic.Successful();

        static void Main(string[] args)
        {
            Example.Initialization();
            Example.Work();
            Menu();
        }

        static void Menu()
        {
            while (true)
            {
                int command;
                var userInput = Console.ReadKey();
                Console.WriteLine();
                if (char.IsDigit(userInput.KeyChar))
                {
                    command = int.Parse(userInput.KeyChar.ToString());
                }
                else
                {
                    command = -1;
                }
                Example.MenuCommandProcessor(command);
            }
        }
    }
}