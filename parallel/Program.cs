using System;
using CustomThreadPoolLibrary.ThreadPool;
using Examples;
using HighloadTests = Examples.Highload;
using Generic = Examples.Examples.Generic;
using Void = Examples.Examples.Void;

namespace parallel
{
    class Program
    {
        //private static readonly IExample Example = new Void.WithAwait.Successful();
        private static readonly IExample Example = new Void.WithAwait.Successful();

        static void Main(string[] args)
        {
            CustomThreadPool.TrySetMaxThreads(Example.ThreadsCount);
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