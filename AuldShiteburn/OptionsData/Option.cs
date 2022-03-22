using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
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
            PrintOptions(options, index);
            bool browsing = true;
            while (browsing)
            {
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
                                    if (menuBanner != null)
                                    {
                                        Console.CursorLeft = 0;
                                        Console.CursorTop = 7;
                                    }
                                    else
                                    {
                                        Console.CursorLeft = 0;
                                        Console.CursorTop = 0;
                                    }
                                    PrintOptions(options, index);
                                }
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            {
                                if (index >= 0 && index < options.Count - 1)
                                {
                                    index++;
                                    if (menuBanner != null)
                                    {
                                        Console.CursorLeft = 0;
                                        Console.CursorTop = 7;
                                    }
                                    else
                                    {
                                        Console.CursorLeft = 0;
                                        Console.CursorTop = 0;
                                    }
                                    PrintOptions(options, index);
                                }
                            }
                            break;
                        case ConsoleKey.Backspace:
                            {
                                if (Menu.Instance.GetType() == typeof(PauseMenu))
                                {
                                    Menu.Instance.menuActive = false;
                                }
                                goto exit_loop;
                            }
                    }
                } while (InputSystem.InputKey != ConsoleKey.Enter);
                exit_loop:
                if (InputSystem.InputKey == ConsoleKey.Enter)
                {
                    options[index].OnUse();
                }
                browsing = false;
            }
        }
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
                    Console.WriteLine(option.DisplayString);
                }
            }
        }
    }
}
