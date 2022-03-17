using System;
using System.Collections.Generic;

namespace AuldShiteburn.OptionData
{
    internal abstract class Option
    {
        public abstract string DisplayString { get; }
        public abstract void OnUse();

        public static void PrintOptions(List<Option> options, int index)
        {
            foreach (Option option in options)
            {
                if (options.IndexOf(option) == index)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(option.DisplayString);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(option.DisplayString + "\n");
                }
            }
        }
    }
}
