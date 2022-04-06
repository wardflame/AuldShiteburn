using System;
using System.Collections.Generic;

namespace AuldShiteburn.OptionData
{
    internal abstract class Option
    {
        public abstract string DisplayString { get; }
        public abstract void OnUse();

        public static void SelectRunOption(List<Option> options, string menuBanner)
        {
            int index = 0;
            PrintOptions(options, index, menuBanner);
            bool browsing = true;
            while (browsing)
            {
                bool quit = false;
                do
                {
                    InputSystem.GetInput();
                    switch (InputSystem.InputKey)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (index <= options.Count - 1 && index > 0)
                                {
                                    index--;
                                    Console.CursorLeft = 0;
                                    Console.CursorTop = 0;
                                    PrintOptions(options, index, menuBanner);
                                }
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            {
                                if (index >= 0 && index < options.Count - 1)
                                {
                                    index++;
                                    Console.CursorLeft = 0;
                                    Console.CursorTop = 0;
                                    PrintOptions(options, index, menuBanner);
                                }
                            }
                            break;
                        case ConsoleKey.Backspace:
                            {
                                quit = true;
                            }
                            break;
                    }
                } while (InputSystem.InputKey != ConsoleKey.Enter && !quit);
                if (InputSystem.InputKey == ConsoleKey.Enter)
                {
                    options[index].OnUse();
                }
                browsing = false;
            }
        }

        public static void PrintOptions(List<Option> options, int index, string banner)
        {
            if (banner != null)
            {
                Utils.WriteColour(banner, ConsoleColor.DarkYellow);
            }
            foreach (Option option in options)
            {
                if (options.IndexOf(option) == index)
                {
                    Utils.WriteColour($"{option.DisplayString}\n", ConsoleColor.Yellow);
                }
                else
                {
                    Utils.WriteColour($"{option.DisplayString}\n");
                }
            }
        }
    }
}
