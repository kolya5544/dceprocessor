using System;
using System.Collections.Generic;
using System.Text;

namespace DCEprocessor
{
    public static class ColorExtension
    {
        public static void Print(this ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
