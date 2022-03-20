using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MenuData
{
    internal abstract class Menu
    {
        public static Menu Instance { get; set; }
        protected static List<Option> options;
        protected abstract string Banner { get; }
        public void InMenu()
        {
            Console.Clear();
            if (Banner != null)
            {
                Utils.WriteColour(ConsoleColor.DarkYellow, Banner);
            }
            Option.PrintOptions(options, 0);
            bool browsing = true;
            while (browsing)
            {
                int index = 0;
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
                                    if (Banner != null)
                                    {
                                        Console.CursorLeft = 0;
                                        Console.CursorTop = 7;
                                    }
                                    else
                                    {
                                        Console.CursorLeft = 0;
                                        Console.CursorTop = 0;
                                    }                                    
                                    Option.PrintOptions(options, index);
                                }
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            {
                                if (index >= 0 && index < options.Count - 1)
                                {
                                    index++;
                                    if (Banner != null)
                                    {
                                        Console.CursorLeft = 0;
                                        Console.CursorTop = 7;
                                    }
                                    else
                                    {
                                        Console.CursorLeft = 0;
                                        Console.CursorTop = 0;
                                    }
                                    Option.PrintOptions(options, index);
                                }
                            }
                            break;
                    }
                } while (InputSystem.InputKey != ConsoleKey.Enter);
                options[index].OnUse();
                browsing = false;
            }
        }
        protected abstract void InitMenu();

        public Menu()
        {
            options = new List<Option>();
            InitMenu();
        }
    }
}
