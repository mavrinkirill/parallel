using System;

namespace Infrastructure
{
    public static class ConsoleHelper
    {
        public static void WriteColorLine(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void WriteGreenLine(string text)
        {
            WriteColorLine(ConsoleColor.Green, text);
        }

        public static void WriteBlueLine(string text)
        {
            WriteColorLine(ConsoleColor.Blue, text);
        }

        public static void WriteCyanLine(string text)
        {
            WriteColorLine(ConsoleColor.Cyan, text);
        }

        public static void WriteRedLine(string text)
        {
            WriteColorLine(ConsoleColor.Red, text);
        }
    }
}
